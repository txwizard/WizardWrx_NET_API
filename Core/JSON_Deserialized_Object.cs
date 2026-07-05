/*
    ============================================================================

    Module Name:        JSON_Deserialized_Object.cs

    Namespace Name:     Sweeper365_DAL

    Class Name:         JSON_Deserialized_Object

    Synopsis:           Instances of this class identify themselves as JSON
                        strings, although they perform no serious validation due
                        to the limited objective it serves of ensuring that the
                        JsonConvert method returned SOMETHING that looks 
                        superficially like valid JSON.

    Remarks:            This class is syntactic sugar, intended to communicate
                        that its contents are expected to be valid JSON.

    Reference:

    Author:             David A. Gray

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Author Synopsis
    ----------  ------ ---------------------------------------------------------
    2021/07/05 DAGray Initial implementation.
	2026/04/09 DAGray Refactor to be schema-aware and optionally  sanitize/parse
                      eagerly, and relocate to the LeadLife.Core.Common 
                      namespace and DLL.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WizardWrx;

namespace LeadLife.Core.Common
{
	/// <summary>
	/// Wrapper for JSON text that can optionally sanitize and parse
	/// according to a schema-aware set of required fields.
	/// </summary>
	public sealed class JSON_Deserialized_Object
	{
		private readonly Regex _repairRegex;
		private JObject _parsedJObject;

		/// <summary>
		/// The raw (or sanitized) JSON text associated with this instance.
		/// </summary>
		public string JSON { get; private set; }

		/// <summary>
		/// Optional per-instance schema: field names that must be JSON strings
		/// and are eligible for repair if unquoted.
		/// </summary>
		public string [ ] RequiredFields { get; }


		private JSON_Deserialized_Object ( )
		{
			// Prevent uninitialized construction.
		}


		/// <summary>
		/// Construct with JSON only; no schema, no sanitizer.
		/// </summary>
		/// <param name="pstrJSON">
		/// This string represents the JSON to be encapsulated by this instance.
		/// No validation or sanitization is performed on this string by this
		/// constructor; it is stored as-is. If the caller intends to perform
		/// schema-aware sanitization, it should use the other constructor and
		/// pass the appropriate parameters to enable that behavior.
		/// </param>
		public JSON_Deserialized_Object ( string pstrJSON )
			: this ( pstrJSON , pastrRequiredFields: null , pfSanitize: false )
		{
		}   // public JSON_Deserialized_Object constructor (1 of 2)


		/// <summary>
		/// Construct with JSON and an optional schema; optionally sanitize immediately.
		/// </summary>
		/// <param name="pstrJSON">
		/// JSON text as received (possibly malformed).
		/// </param>
		/// <param name="pastrRequiredFields">
		/// Optional list of field names that must be JSON strings and may need repair.
		/// </param>
		/// <param name="pfSanitize">
		/// When true, applies the schema-aware sanitizer immediately and parses eagerly.
		/// </param>
		public JSON_Deserialized_Object ( string pstrJSON , string [ ] pastrRequiredFields , bool pfSanitize = false )
		{
			if ( pstrJSON == null )
			{
				throw new ArgumentNullException ( nameof ( pstrJSON ) );
			}   // if ( pstrJSON == null )

			if ( pastrRequiredFields == null || pastrRequiredFields.Length == ListInfo.LIST_IS_EMPTY )
			{
				throw new ArgumentException (
					$"When using this constructor, {nameof (pastrRequiredFields)} must be non-null and contain at least one entry." ,
					nameof ( pastrRequiredFields ) );
			}   // if ( pastrRequiredFields == null || pastrRequiredFields.Length == ListInfo.LIST_IS_EMPTY )

			JSON = pstrJSON;
			RequiredFields = pastrRequiredFields ?? Array.Empty<string> ( );
			_repairRegex = BuildRepairRegex ( RequiredFields );

			if ( pfSanitize && _repairRegex != null )
			{
				Sanitize ( );
				EnsureParsed ( );
			}	// if ( pfSanitize && _repairRegex != null )
		}   // public JSON_Deserialized_Object constructor (2 of 2)


		/// <summary>
		/// Superficial structural check: non-empty and starts/ends with
		/// expected delimiters.
		/// </summary>
		public bool MaybeValid ( )
		{
			if ( string.IsNullOrEmpty ( JSON ) )
				return false;

			return JSON.StartsWith ( Properties.Resources.JSON_PREFIX )
				&& JSON.EndsWith ( Properties.Resources.JSON_SUFFIX );
		}   // public bool MaybeVali


		/// <summary>
		/// Applies the schema-aware sanitizer to repair unquoted string values
		/// for this instance's RequiredFields, mutating the JSON property.
		/// </summary>
		public void Sanitize ( )
		{
			if ( _repairRegex == null )
				return;    // No schema => nothing to repair.

			JSON = _repairRegex.Replace (
				JSON ,
				"\"$1\": " + SpecialCharacters.DOUBLE_QUOTE + "$2" + SpecialCharacters.DOUBLE_QUOTE
			);

			// Invalidate any previously parsed representation.
			_parsedJObject = null;
		}   // public void Sanitize


		/// <summary>
		/// Returns the JSON as a JObject, parsing lazily if needed.
		/// </summary>
		public JObject AsJObject ( )
		{
			EnsureParsed ( );
			return _parsedJObject;
		}   // public JObject AsJObject


		/// <summary>
		/// Deserializes the JSON into a strongly-typed model.
		/// </summary>
		/// <typeparam name="T">
		/// Generic type parameter T identifies the strongly typed model against
		/// which to parse the JObject.
		/// </typeparam>
		/// <returns>
		/// If the method succeeds, it returns an instance of type T populated
		/// with data from the JSON.
		/// </returns>
		public T ParseTo<T> ( ) where T : class
		{
			EnsureParsed ( );
			return _parsedJObject?.ToObject<T> ( );
		}   // public T ParseTo<T>


		/// <summary>
		/// <para>
		/// Validate that the JSON object contains all required fields.
		/// </para>
		/// <para>
		/// Optionally enforces that the values are non-empty (non-null, non-
		/// whitespace).
		/// </para>
		/// <para>
		/// Field name matching is case-insensitive.
		/// </para>
		/// </summary>
		/// <param name="pfRequireNonEmptyValues">
		/// This flag indicates whether to enforce that the required fields have non-
		/// empty values. If <c>true</c>, then any required field that is either null
		/// or a string consisting only of whitespace will be considered missing.
		/// </param>
		/// <returns>
		/// This method returns a list of missing required fields. If the JSON
		/// document is invalid, the list will contain a single entry "**Invalid
		/// JSON**". If all required fields are present (and non-empty when
		/// required), the returned list is empty.
		/// </returns>
		public List<string> Validate (  bool pfRequireNonEmptyValues )
		{
			List<string> rlstMissingFields = new List<string> ( );

			try
			{
				EnsureParsed ( );

				//	-------------------------------------------------------------------------
				//	Build a case-insensitive lookup table for top-level fields.
				//	-------------------------------------------------------------------------

				Dictionary<string , JToken> lookup = new Dictionary<string , JToken> ( StringComparer.OrdinalIgnoreCase );

				foreach ( JProperty prop in _parsedJObject.Properties ( ) )
				{
					lookup [ prop.Name ] = prop.Value;
				}   // foreach ( JProperty prop in obj.Properties ( ) )

				//	-------------------------------------------------------------------------
				//	Validate required fields.
				//	-------------------------------------------------------------------------

				foreach ( string strRequiredField in RequiredFields )
				{
					if ( lookup.TryGetValue ( strRequiredField , out JToken token ) )
					{
						if ( pfRequireNonEmptyValues )
						{
							if ( token.Type == JTokenType.Null ||
								( token.Type == JTokenType.String &&
								   string.IsNullOrWhiteSpace ( ( string ) token ) ) )
							{
								rlstMissingFields.Add ( strRequiredField );
							}   // if ( token.Type == JTokenType.Null || ( token.Type == JTokenType.String && string.IsNullOrWhiteSpace ( ( string ) token ) ) )
						}   // if ( pfRequireNonEmptyValues )
					}   // TRUE (anticipated outcome) block, if ( lookup.TryGetValue ( strRequiredField , out JToken token ) )
					else
					{
						rlstMissingFields.Add ( strRequiredField );
					}   // FALSE (unanticipated outcome) block, if ( lookup.TryGetValue ( strRequiredField , out JToken token ) )
				}   // foreach ( string strRequiredField in piestrRequiredFields )
			}
			catch
			{
				rlstMissingFields.Add ( "**Invalid JSON**" );
			}

			return rlstMissingFields;
		}   // public List<string> Validate


		/// <summary>
		/// Ensure that the internal JObject is populated from the current JSON
		/// string.
		/// </summary>
		/// <exception cref="JsonException">
		/// An exception of this type is thrown when the JSON string fails the
		/// superficial validity check in MaybeValid, or when JObject.Parse fails due
		/// to malformed JSON.
		/// </exception>
		private void EnsureParsed ( )
		{
			if ( _parsedJObject == null )
			{
				if ( MaybeValid ( ) )
				{
					_parsedJObject = JObject.Parse ( JSON );
				}   // TRUE (anticipated outcome) block, if ( MaybeValid ( ) )
				else
				{
					throw new JsonException ( $"JSON_Deserialized_Object contains text that is not superficially valid JSON. String that failed validation = {JSON ?? WizardWrx.Common.Properties.Resources.VALUE_IS_NULL}" );
				}   // FALSE (unanticipated outcome) block, if ( MaybeValid ( ) )
			}   // if ( _parsedJObject == null )
		}   // private void EnsureParsed


		/// <summary>
		/// Build a schema-aware, value-agnostic, quote-aware regex for this instance,
		/// or null if no required fields are supplied.
		/// </summary>
		/// <param name="pastrRequiredFields">
		/// This array contains the field names that must exist as properties in
		/// the JSON and are eligible for repair if unquoted. If null or empty,
		/// the method returns null, indicating that no repair can be performed.
		/// </param>
		/// <returns>
		/// The returned regex matches unquoted string values for the specified
		/// field names, allowing for optional whitespace around the colon. If
		/// no required fields are supplied, the method returns null, indicating
		/// that repairs cannot be performed.
		/// </returns>
		private static Regex BuildRepairRegex ( string [ ] pastrRequiredFields )
		{
			if ( pastrRequiredFields == null || pastrRequiredFields.Length == ListInfo.LIST_IS_EMPTY )
				return null;

			// Bare identifiers only; no need to escape.
			string alternation = string.Join ( SpecialStrings.PIPE_CHAR , pastrRequiredFields );

			string pattern = "\"(" + alternation + ")\"\\s*:\\s*(?!"
							 + SpecialCharacters.DOUBLE_QUOTE
							 + ")([A-Za-z0-9._%+-]+)";

			return new Regex ( pattern , RegexOptions.Compiled );
		}   // private static Regex BuildRepairRegex
	}   // public sealed class JSON_Deserialized_Object
}   // partial namespace LeadLife.Core.Common