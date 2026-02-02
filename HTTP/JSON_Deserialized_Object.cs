/*
    ============================================================================

    Module Name:        JSON_Deserialized_Object.cs

    Namespace Name:     WizardWrx.HTTP

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

    Date       Version Author Synopsis
    ---------- ------- ------ -------------------------------------------------
    2021/07/05 1.0     DAGray Initial implementation
    2026/02/01 9.0     DAGray Brought over intact from Sweeper365_DAL
    ============================================================================
*/


using System;

namespace WizardWrx.HTTP
{
	/// <summary>
	/// 
	/// </summary>
	public class JSON_Deserialized_Object
	{
		/// <summary>
		/// The default constructor is hidden to enforce instatiation of only
		/// fully initialized objects.
		/// </summary>
		private JSON_Deserialized_Object ( ) { }


		/// <summary>
		/// The public constructor sets the solitary property of this class, an
		/// ordinary string that is expected to contain valid JSON.
		/// </summary>
		/// <param name="pstrJSON"></param>
		public JSON_Deserialized_Object (
			string pstrJSON )
		{
			JSON = MustBeValid( pstrJSON) ? pstrJSON : null;
		}   // public JSON_Deserialized_Object


		/// <summary>
		/// Rather than throwing, this lambda expression permits the constructor
		/// to leave its value null, allowing it to appear to succeed when the
		/// input is invalid, while leaving the property value null.
		/// </summary>
		/// <param name="pstrJSON">
		/// This requirrd string parameter is the JSON string candidate to
		/// evaluate.
		/// </param>
		/// <returns>
		/// Input parameter <paramref name="pstrJSON"/> cannot be null or the
		/// empty string, AND its first character must be a left brace OR a left
		/// bracket, and its last character must be a right brace OR a right
		/// bracket.
		/// </returns>
		private bool MustBeValid ( string pstrJSON ) => ( !string.IsNullOrEmpty ( pstrJSON ) ) && ( pstrJSON.StartsWith ( Properties.Resources.JSON_OBJECT_PREFIX ) && pstrJSON.EndsWith ( Properties.Resources.JSON_OBJECT_SUFFIX ) ) || ( pstrJSON.StartsWith ( Properties.Resources.JSON_ARRAY_PREFIX ) && pstrJSON.EndsWith ( Properties.Resources.JSON_ARRAY_SUFFIX ) );


		/// <summary>
		/// The solitary property of this class is the string that stores the
		/// text returned by a JSON serializer.
		/// </summary>
		public string JSON { get; private set; }


		/// <summary>
		/// Evaluate whether the string stored in the JSON property is
		/// superficially valid.
		/// </summary>
		/// <returns>
		/// Return True when the string has at least 4 characters, starting with
		/// the defined prefix and ending with the defined suffix.
		/// </returns>
		[Obsolete("This method is obsolete because the constructor now evaluates the constraint that it was intended to evaluate.")]
		public bool MaybeValid ( ) => MustBeValid ( JSON );
	}   // public class JSON_Deserialized_Object
}   // partial namespace WizardWrx.HTTP