/*
    ============================================================================

    Module Name:        RequestEngine.cs

    Namespace Name:     WizardWrx.HTTP

    Class Name:         RequestEngine

    Synopsis:           This class provides methods to call RESTful web APIs via
						the HttpClient class and process the results safely and
						with proper error handling and a retry mechanism.

    Remarks:            This class exists to supply an independent container for
						the CallWebApiAndProcessResultASync method, which was
						originally part of the Sweeper365_DAL class.

    Reference:

    Author:             David A. Gray

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ -------------------------------------------------
    2021/07/05 1.0     DAGray Initial implementation
    2026/02/01 9.0     DAGray Brought over intact from Sweeper365_DAL
	2026/04/16 9.0.81  DAGray Correct a bug in ApplyHeaders that caused the
                              Accept header to go into the default collection
                              instead of the local collection.
    ============================================================================
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using WizardWrx.JsonSupport;

namespace WizardWrx.HTTP
{
	/// <summary>
	/// Instances of this class export a single method that supports all five
	/// HTTP Request verbs.
	/// </summary>
	public class RequestEngine
	{
		/// <summary>
		/// Use this constant to explicitly assert the default value of the last
		/// parameter to <see cref="SendRequest"/>,
		/// <c>pfExpectJSON</c>.
		/// </summary>
		public const bool REQUEST_EXPECTS_JSON_RESPONSE = true;


		/// <summary>
		/// Use this constant to override the default value of the last
		/// parameter to <see cref="SendRequest"/>,
		/// <c>pfExpectJSON</c>.
		/// </summary>
		public const bool REQUEST_EXPECTS_OTHER_RESPONSE = false;


		/// <summary>
		/// The HttpVerb enumeration identifies the HTTP verbs supported by the
		/// HttpClient object, and establishes which HttpClienti instance method
		/// CallWebApiAndProcessResultASync invokes.
		/// </summary>
		public enum HttpVerb
		{
			/// <summary>
			/// This value maps to the most basic verb, GET.
			/// </summary>
			GET,

			/// <summary>
			/// This value maps to the increasingly popular PATCH verb, usually used to
			/// update selected columns of a row in a database table.
			/// </summary>
			PATCH,

			/// <summary>
			/// This value maps to the most commonly used verb after GET.
			/// </summary>
			POST,

			/// <summary>
			/// This value maps to the infrequently used PUT verb, usually used
			/// to update selected columns of a row in a database table.
			/// </summary>
			PUT,

			/// <summary>
			/// This value maps to the dangerous DELETE verb, which is usually
			/// sent without a payload.
			/// </summary>
			DELETE
		}   // public enum HttpVerb


		/// <summary>
		/// This immutable reference to a RequestOptions object is a concise way
		/// to attach options such as an OAuth access token and a method to get
		/// a new token, along with a robust exception logging object that has
		/// many configurable settings that determine how much detail it reports
		/// and how its reports are delivered.
		/// </summary>
		public RequestOptions Options { get; }


		/// <summary>
		/// Each instance must construct and use its own HttoClient to prevent
		/// recycling HTTP request headers.
		/// </summary>
		private readonly HttpClient _httpClient;


		/// <summary>
		/// The default constructor leaves the Options property null, which is
		/// OK for most use cases.
		/// </summary>
		public RequestEngine (
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = MagicNumbers.ZERO
		)
		{
			_httpClient = CreateHttpClient ( null , memberName , sourceFilePath , sourceLineNumber );
		}   // public RequestEngine constructor (1 of 2)


		/// <summary>
		/// This constructor initializes the Options property, which is not
		/// strictly required for most use cases, and can be passed in as a
		/// RequestOptions constructor call, since the RequestOptions object is
		/// a POCO that doesn't even need System; its only reference is to the
		/// WizardWrx.DLLConfigurationManager namespace, where the 
		/// ExceptionLogger lives.
		/// </summary>
		/// <include file="../InternalDocumentationXmlCopyBooks/CallerInfo.XML"
		///          path="doc/members/member[@name='CallerInfoSummary']/*" />
		/// <param name="pobjRequestOptions">
		/// When supplied, the RequestOptions object reference is stored in the
		/// Options property, providing a read-only reference to everything but
		/// the OAuth Access Token, which must, of course, be updateable because
		/// access tokens are short-lived.
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/CallerInfo.XML"
		///          path="doc/members/member[@name='CallerInfoParameters']/*" />
		public RequestEngine (
			RequestOptions pobjRequestOptions ,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = MagicNumbers.ZERO
			)
		{
			Options = pobjRequestOptions;
			_httpClient = CreateHttpClient ( Options , memberName , sourceFilePath , sourceLineNumber );

			if ( Options != null )
			{
				if ( Options.Timeout > 0 )
				{
					_httpClient.Timeout = TimeSpan.FromSeconds ( Options.Timeout );
				}   // if ( Options != null && Options.Timeout > 0 )

				if ( Options.LoggerCallback != null )
				{
					Options.LoggerCallback (
						$"RequestEngine constructor: RequestOptions supplied properties as follows: {Options}" ,
						memberName ,
						sourceFilePath ,
						sourceLineNumber );
				}   // if ( Options.LoggerCallback != null )
			}
		}   // public RequestEngine constructor (2 of 2)


		/// <summary>
		/// Get a formatted list of the HTTP headers attached to the request.
		/// </summary>
		/// <param name="requestUri">
		/// Specify the URL that will accompany the actual request when you call
		/// SendRequest.
		/// </param>
		/// <param name="pfExpectJson">
		/// Specify the Boolean ExpectJSON flag that will accomapny the actual
		/// request when you call SendRequest.
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/CallerInfo.XML"
		///          path="doc/members/member[@name='CallerInfoParameters']/*" />
		/// <returns>
		/// The return value is a human-readable list of the HTTP headers that
		/// will be attached to the actual request.
		/// </returns>
		public string GetDiagnosticHeadersDump (
			Uri requestUri ,
			bool pfExpectJson ,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = MagicNumbers.ZERO )
		{
			HttpRequestMessage temp = new HttpRequestMessage ( HttpMethod.Get , requestUri );

			//	----------------------------------------------------------------
			//	Apply headers exactly as a real request would.
			//	----------------------------------------------------------------

			ApplyHeaders ( temp , pfExpectJson );

			StringBuilder sb = new StringBuilder ( 512 );

			sb.AppendLine ( $"Request URI     = {requestUri}" );
			sb.AppendLine ( $"ExpectJSON Flag = {pfExpectJson}" );
			sb.AppendLine ( $"{Environment.NewLine}Headers applied:{Environment.NewLine}" );
			sb.AppendLine ( "----------------------------------------" );

			foreach ( var header in temp.Headers )
			{
				foreach ( var value in header.Value )
				{
					sb.AppendLine ( header.Key + ": " + value );
				}   // foreach ( var value in header.Value )
			}   // foreach ( var header in temp.Headers )

			if ( temp.Content != null )
			{
				foreach ( var header in temp.Content.Headers )
				{
					foreach ( var value in header.Value )
					{
						sb.AppendLine ( header.Key + ": " + value );
					}   // foreach ( var value in header.Value )
				}   // foreach ( var header in temp.Content.Headers )
			}   // if ( temp.Content != null )

			sb.AppendLine ( $"{Environment.NewLine}----------------------------------------{Environment.NewLine}" );

			return sb.ToString ( );
		}   // public string GetDiagnosticHeadersDump


		/// <summary>
		/// Call the web API and process the result.
		/// </summary>
		/// <typeparam name="T">
		/// <para>
		/// Specifies the expected return type of the Web API call.
		/// </para>
		/// <para>
		/// When pfExpectJSON is true, T must be either JObject or a type
		/// that can be deserialized from the JSON response body.
		/// </para>
		/// <para>
		/// When pfExpectJSON is false, T may be:
		/// </para>
		/// <list type="bullet">
		/// <item>
		/// string  — for plain text responses
		/// </item>
		/// <item>
		/// byte[]  — for binary responses such as audio files
		/// </item>
		/// <item>
		/// any other type for which the caller provides its own interpretation
		/// of the raw response body.
		/// </item>
		/// </list>
		/// <para>
		/// The method does not attempt to infer the correct type; the caller is
		/// responsible for specifying a T that matches the expected response format.
		/// </para>
		/// </typeparam>
		/// <param name="pstrWebApiUrl">
		/// URL of the web API to call (presumeed to return Json unless
		/// indicated otherwise)
		/// </param>
		/// <param name="pfunProcessResultCallback">
		/// <para>
		/// This optional Action JObject reference points to the callback used
		/// to process the result of the call to the web API. This was intended
		/// for processing JSON objects of assorted arbitrary shapes returned by
		/// various Microsoft Graph endpoints.
		/// </para>
		/// <para>
		/// Now that the routine is evolving to meet additional needs, it seemed
		/// wiser to abandon the void return type in favor of returning the
		/// result in a more conventional fashion.
		/// </para>
		/// <para>
		/// Though it now returns in a more "natural" way, the callback remains
		/// relevant for the Graph API because it allows a single entry point to
		/// process 
		/// </para>
		/// </param>
		/// <param name="pjSON_Deserialized">
		/// <para>
		/// When present, the optional JSON_Deserialized_Object object is
		/// syntactic sugar sprinkled over a standard string that is expected to
		/// have passed a very cursory validity check.
		/// </para>
		/// <para>
		/// This string is the payload required of all POST nad PUT requests.
		/// </para>
		/// <para>
		/// When this value is null, unless <paramref name="penmVerb"/> happens
		/// to be DELETE, the verb is coerced to GET, the only other verb that
		/// dispenses with a payload.
		/// </para>
		/// </param>
		/// <param name="penmVerb">
		/// <para>
		/// This optional member of the HttpVerb enumeration identifies the HTT
		/// verb to emit for the request. For backwards compatibility, its value
		/// defaults to POST.
		/// </para>
		/// <para>
		/// When <paramref name="pjSON_Deserialized"/> is null, unless this value
		/// is DELETE, the verb is coerced to GET, the only other verb that can
		/// function without a payload.
		/// </para>
		/// </param>
		/// <param name="pfExpectJSON">
		/// By default, we expect the returned string to be JSON, and use the
		/// JsonConvert.DeserializeObject method to return it as a JObject.
		/// Otherwise, the raw string is returned as is.
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/CallerInfo.XML"
		///          path="doc/members/member[@name='CallerInfoParameters']/*" />
		/// <returns>
		/// <para>
		/// This routine was originally intended for use as part of a library
		/// that interacts with the Microsoft Graph API and expects to return an
		/// arbitrarily shaped object as a JObject, which it passed to the
		/// callback routine <paramref name="pfunProcessResultCallback"/>.
		/// </para>
		/// <para>
		/// Since it is being pulled out of that class library so that it can be
		/// applied elsewhere, it is changed so that it returns whichever object
		/// it has, either JSON or an ordinary string. Hence, altough the return
		/// type is specified as object, in practice, it is either a JObject or
		/// a garden variety String.
		/// </para>
		/// </returns>
		/// <exception cref="JsonException">
		/// Thrown when pfExpectJSON is true but the response body cannot be
		/// deserialized into the requested type T or is not valid JSON.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when pfExpectJSON is false and T is incompatible with the
		/// non‑JSON response mode. For example, this occurs when T is not
		/// string or byte[] and the method has no defined conversion for the
		/// returned content.
		/// </exception>
		/// <exception cref="Exception">
		/// Thrown when the Web API returns an error status code other than
		/// Unauthorized, or when token refresh fails after receiving a 401
		/// Unauthorized response. The exception message includes the HTTP
		/// status code and the response content for diagnostic purposes.
		/// </exception>
		public T SendRequest<T> (
			string pstrWebApiUrl ,
			Action<JObject> pfunProcessResultCallback = null ,
			JSON_Deserialized_Object pjSON_Deserialized = null ,
			HttpVerb penmVerb = HttpVerb.POST ,
			bool pfExpectJSON = true ,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = MagicNumbers.ZERO )
		{
			const string LOGGER_PREFIX = @"RequestEngine.CreateHttpClient ";
			const int MAX_STRING_LENGTH_FOR_LOGGING = 40;

			string strLogPrefix = $"{LOGGER_PREFIX}[{memberName} in {System.IO.Path.GetFileName ( sourceFilePath )} at line {sourceLineNumber}] ";

			if ( Options?.LoggerCallback != null )
			{
				Options.LoggerCallback ( $"{strLogPrefix}Arguments: {nameof ( pstrWebApiUrl )} = {pstrWebApiUrl} , {nameof ( pjSON_Deserialized )} = {( pjSON_Deserialized != null ? "Provided" : "Null" )} , {nameof ( penmVerb )} = {penmVerb} , {nameof ( pfExpectJSON )} = {pfExpectJSON}" );
			}   // if ( Options?.LoggerCallback != null )

			Uri uriAsInput = new Uri ( pstrWebApiUrl );
			bool fTry = true;
			HttpVerb enmRealVerb = ( ( pjSON_Deserialized == null ) && ( penmVerb != HttpVerb.DELETE ) ) ? HttpVerb.GET : penmVerb;

			if ( Options?.LoggerCallback != null )
			{
				Options.LoggerCallback ( $"{strLogPrefix}Verb = {enmRealVerb}" );
			}   // if ( Options?.LoggerCallback != null )

			while ( fTry )
			{
				if ( Options?.LoggerCallback != null )
				{
					Options.LoggerCallback ( $"{strLogPrefix}Entering Try Loop" );
				}   // if ( Options?.LoggerCallback != null )

				using ( HttpRequestMessage httpRequest = new HttpRequestMessage ( s_dctVerbMap [ enmRealVerb ] , uriAsInput ) )
				{
					if ( ( Options != null ) )
					{
						if ( Options?.LoggerCallback != null )
						{
							Options.LoggerCallback ( $"{strLogPrefix}Applying headers" );
						}   // if ( Options?.LoggerCallback != null )

						ApplyHeaders (
							httpRequest ,                   // HttpRequestMessage   phttpRequest
							pfExpectJSON );                 // bool                 pfAcceptJson    = true
					}   // f ( ( Options != null ) )

					if ( pjSON_Deserialized != null )
					{
						httpRequest.Content = new StringContent (
							pjSON_Deserialized.JSON ,       // string               content (the JSON string)
							Encoding.UTF8 ,                 // System.Text.Encoding encoding
							JSON_MIME_TYPE );               // string               mediatype (MIME type)

						if ( Options?.LoggerCallback != null )
						{
							Options.LoggerCallback ( $"{strLogPrefix}Request content set = {pjSON_Deserialized.JSON}" );
						}   // if ( Options?.LoggerCallback != null )
					}   // if ( pjSON_Deserialized != null )

					HttpResponseMessage response = _httpClient
						.SendAsync ( httpRequest )
						.ConfigureAwait ( false )
						.GetAwaiter ( )
						.GetResult ( );

					using ( response )
					{
						if ( response.IsSuccessStatusCode )
						{   // Satisfy the condition of the enclosing While loop.
							fTry = false;

							string strResult = response.Content
								.ReadAsStringAsync ( )
								.ConfigureAwait ( false )
								.GetAwaiter ( )
								.GetResult ( );

							if ( Options?.LoggerCallback != null )
							{
								Options.LoggerCallback ( $"{strLogPrefix}Request Succeeded, Response String = {strResult}" );
							}   // if ( Options?.LoggerCallback != null )

							if ( pfExpectJSON )
							{
								if ( Options?.LoggerCallback != null )
								{
									Options.LoggerCallback ( $"{strLogPrefix}Expecting JSON response" );
								}   // if ( Options?.LoggerCallback != null )

								JObject jstrResult = JsonConvert.DeserializeObject ( strResult ) as JObject;

								if ( jstrResult == null )
								{
									if ( Options?.LoggerCallback != null )
									{
										Options.LoggerCallback ( $"{strLogPrefix}Throwing because expected a JSON object but got something else: {strResult}" );
									}   // if ( Options?.LoggerCallback != null )

									throw new JsonException ( @"Expected a JSON object but got something else." );
								}   // if ( jstrResult == null )

								if ( pfunProcessResultCallback != null )
								{   // Unused for POST requests.
									if ( Options?.LoggerCallback != null )
									{
										Options.LoggerCallback ( $"{strLogPrefix}Passing request as JObject to callback = {RequestOptions.FormatDelegate ( pfunProcessResultCallback )}" );
									}   // if ( Options?.LoggerCallback != null )

									pfunProcessResultCallback ( jstrResult );
								}   // if ( pfunProcessResultCallback != null )

								// If T is JObject, return it directly.
								if ( typeof ( T ) == typeof ( JObject ) )
								{
									if ( Options?.LoggerCallback != null )
									{
										Options.LoggerCallback ( $"{strLogPrefix}Returning JObject = {jstrResult}" );
									}   // if ( Options?.LoggerCallback != null )

									return ( T ) ( object ) jstrResult;
								}   // if ( typeof ( T ) == typeof ( JObject ) )

								// Otherwise, deserialize to T.
								T typedResult = JsonConvert.DeserializeObject<T> ( strResult );

								if ( Options?.LoggerCallback != null )
								{
									Options.LoggerCallback ( $"{strLogPrefix}Returning deserialized object of type {typeof ( T )} = {typedResult}" );
								}   // if ( Options?.LoggerCallback != null )

								return typedResult;
							}   // TRUE (outcome given default value for pfExpectJSON) block, if ( pfExpectJSON )
							else
							{
								if ( Options?.LoggerCallback != null )
								{
									Options.LoggerCallback ( $"{strLogPrefix}Expecting something other than JSON" );
								}   // if ( Options?.LoggerCallback != null )

								if ( typeof ( T ) == typeof ( byte [ ] ) )
								{
									byte [ ] bytes = response.Content
										.ReadAsByteArrayAsync ( )
										.ConfigureAwait ( false )
										.GetAwaiter ( )
										.GetResult ( );

									if ( Options?.LoggerCallback != null )
									{
										Options.LoggerCallback ( $"{strLogPrefix}Returning byte array, byte count = {bytes.LongLength}" );
									}   // if ( Options?.LoggerCallback != null )

									return ( T ) ( object ) bytes;
								}   // if ( typeof ( T ) == typeof ( byte [ ] ) )

								if ( typeof ( T ) == typeof ( string ) )
								{
									if ( Options?.LoggerCallback != null )
									{
										string strPrefix = string.IsNullOrEmpty ( strResult ) ? "Returning empty string" : strResult.Length >= MAX_STRING_LENGTH_FOR_LOGGING ? $"first {MAX_STRING_LENGTH_FOR_LOGGING} characters = {strResult.Substring ( ListInfo.SUBSTR_BEGINNING , MAX_STRING_LENGTH_FOR_LOGGING )}" : $"whole string: {strResult}";
										Options.LoggerCallback ( $"{strLogPrefix}Returning string, length = {strResult.Length}, {strPrefix}" );
									}   // if ( Options?.LoggerCallback != null )

									return ( T ) ( object ) strResult;
								}   // if ( typeof ( T ) == typeof ( string ) )

								throw new InvalidOperationException ( $"pfExpectJSON is false, but the requested return type ({typeof ( T )}) is not string." );
							}   // FALSE (outcome given overridden value for pfExpectJSON) block, if ( pfExpectJSON )
						}   // TRUE (anticipated outcome) block, if ( response.IsSuccessStatusCode )
						else
						{
							if ( Options?.LoggerCallback != null )
							{
								Options.LoggerCallback ( $"{strLogPrefix}Response Status Code = {response.StatusCode}" );
							}   // if ( Options?.LoggerCallback != null )

							// Check for an expired token.

							if ( response.StatusCode == HttpStatusCode.Unauthorized )
							{
								string strNewOAuthToken = null;

								if ( Options != null &&
									 Options.TokenGetter != null &&
									 Options.TokenGetter ( out strNewOAuthToken , Options.Logger ) &&
									 !string.IsNullOrEmpty ( strNewOAuthToken ) )
								{
									if ( Options?.LoggerCallback != null )
									{
										Options.LoggerCallback ( $"{strLogPrefix}Got new OAuth Token" );
									}   // if ( Options?.LoggerCallback != null )

									Options.CurrentOAuthToken = strNewOAuthToken;
									continue;   // Retry with new token.
								}
								else
								{   // Request denied. Throw up our hands and bug out.
									if ( Options?.LoggerCallback != null )
									{
										Options.LoggerCallback ( $"{strLogPrefix}Failed to get new OAuth Token: throwing" );
									}   // if ( Options?.LoggerCallback != null )

									throw new Exception ( Properties.Resources.ERRMSG_TOKEN_REFRESH_FAIL );
								}   // FALSE (unanticipated outcome) block, if ( _clientApplication_Adapter.GetOAuthToken ( ) )
							}   // TRUE (anticipated outcome) block, if ( response.StatusCode == System.Net.HttpStatusCode.Unauthorized )
							else
							{
								string strResultContent = response.Content
									.ReadAsStringAsync ( )
									.ConfigureAwait ( false )
									.GetAwaiter ( )
									.GetResult ( );

								//  ----------------------------------------
								//  When calling the Microsoft Graph API,
								//  note that if you got reponse.Code == 403
								//  and reponse.content.code ==
								//  "Authorization_RequestDenied" that this
								//  is because the tenant admin  has not
								//  granted consent for the application to
								//  call the Web API.
								//  ----------------------------------------

								StringBuilder builder = new StringBuilder ( MagicNumbers.CAPACITY_01KB );
								builder.AppendLine ( $"Web API Call failed: {response.StatusCode}\n" );
								builder.AppendLine ( $"Content: {strResultContent}" );

								if ( Options?.LoggerCallback != null )
								{
									Options.LoggerCallback ( $"{strLogPrefix}Other Failure: throwing because {builder}" );
								}   // if ( Options?.LoggerCallback != null )

								throw new Exception ( builder.ToString ( ) );
							}   // FALSE (unanticipated outcome) block, if ( response.StatusCode == System.Net.HttpStatusCode.Unauthorized )
						}   // FALSE (unanticipated outcome) block, if ( response.IsSuccessStatusCode )
					}   // using ( response )
				}   // using ( HttpRequestMessage httpRequest = new HttpRequestMessage ( s_dctVerbMap [ enmRealVerb ] , uriAsInput ) )
			}   // while ( fTry )

			if ( Options?.LoggerCallback != null )
			{
				Options.LoggerCallback ( $"{strLogPrefix}Fell off end of SendRequest: throwing" );
			}   // if ( Options?.LoggerCallback != null )

			throw new InvalidOperationException ( @"This Exception should never happen because the code should return from one of two exit points inside the while loop." );
		}   // public T SendRequest<T>


		/// <summary>
		/// Extract the Query from any URI into an IDictionary of name-value
		/// pairs that can be URL encoded properly.
		/// </summary>
		/// <param name="puriToParse">
		/// Pass in a reference to the System.Uri to process.
		/// </param>
		/// <returns>
		/// The return value is the IDictionary populated with keys for each
		/// named value in the Query of <paramref name="puriToParse"/>. If the
		/// query is empty or absent, the returned dictionary is empty.
		/// </returns>
		/// <remarks>
		/// Since this method performs no logging, it is not necessary to pass
		/// in caller info parameters, and, in fact, they are not supported. If
		/// you need to log the query values, call this method from a wrapper
		/// that does support caller info parameters and logs the returned
		/// dictionary.
		/// </remarks>
		public static IDictionary<string , string> ParseQueryToDictionary ( Uri puriToParse )
		{
			Dictionary<string , string> dctQueryValues = new Dictionary<string , string> ( StringComparer.OrdinalIgnoreCase );

			string strQuery = puriToParse.Query;

			if ( !string.IsNullOrWhiteSpace ( strQuery ) )
			{
				// Trim leading '?'.
				if ( strQuery.StartsWith ( SpecialStrings.QUESTION_MARK ) )
				{
					strQuery = strQuery.Substring ( ListInfo.SUBSTR_SECOND_CHAR );
				}   // if ( strQuery.StartsWith ( SpecialStrings.QUESTION_MARK ) )

				// Split into key=value pairs.
				string [ ] astrPairs = strQuery.Split ( SpecialCharacters.AMPERSAND );
				int intPairCount = astrPairs.Length;

				for ( int intCurrentPair = ArrayInfo.ARRAY_FIRST_ELEMENT ;
						  intCurrentPair < intPairCount ;
						  intCurrentPair++ )
				{
					if ( !string.IsNullOrWhiteSpace ( astrPairs [ intCurrentPair ] ) )
					{
						string [ ] astrKeyAndValue = astrPairs [ intCurrentPair ].Split (
							new [ ] { SpecialCharacters.EQUALS_SIGN } ,
							MagicNumbers.PLUS_TWO );

						string strKey = astrKeyAndValue [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
						string strValue = astrKeyAndValue.Length > ListInfo.EXACTLY_ONE_ITEM
							? astrKeyAndValue [ ArrayInfo.ARRAY_SECOND_ELEMENT ]
							: SpecialStrings.EMPTY_STRING;

						// Do NOT decode here — keep raw values.
						dctQueryValues [ strKey ] = strValue;
					}   // if ( !string.IsNullOrWhiteSpace ( astrPairs [ intCurrentPair ] ) )
				}   // for ( int intCurrentPair = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCurrentPair < intPairCount ; intCurrentPair++ )
			}   // if ( !string.IsNullOrWhiteSpace ( strQuery ) )

			return dctQueryValues;
		}   // public static IDictionary<string , string> ParseQueryToDictionary


		/// <summary>
		/// This private factory method initializes the private HttpClient
		/// used by the instance.
		/// </summary>
		/// <param name="pobjPresets">
		/// The RequestOptions object is technically optional, and, when so, the
		/// Automatic Decompression for GZip and Deflate is left off.
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/CallerInfo.XML"
		///          path="doc/members/member[@name='CallerInfoParameters']/*" />
		/// <returns>
		/// A reference to the returned HTTPClient goes into the private
		/// instance member that is set aside for that purpose. Each
		/// constructor, including the default constructor, calls this routiner
		/// with or without an Options object reference.
		/// </returns>
		private static HttpClient CreateHttpClient ( RequestOptions pobjPresets = null , string memberName = SpecialStrings.EMPTY_STRING , string sourceFilePath = SpecialStrings.EMPTY_STRING , int sourceLineNumber = MagicNumbers.ZERO )
		{
			const string LOGGER_PREFIX = @"RequestEngine.CreateHttpClient: ";

			string strLogPrefix = $"{LOGGER_PREFIX}[{memberName} in {System.IO.Path.GetFileName ( sourceFilePath )} at line {sourceLineNumber}] ";

			if ( pobjPresets?.LoggerCallback != null )
			{
				pobjPresets.LoggerCallback ( $"{strLogPrefix}Constructing a preset for use with the calling RequestEngine instance" );
			}   // if ( Options?.LoggerCallback != null )

			HttpClientHandler handler = new HttpClientHandler ( );

			if ( !string.IsNullOrEmpty ( pobjPresets?.AcceptEncodingValue ) )
			{
				if ( pobjPresets?.LoggerCallback != null )
				{
					pobjPresets.LoggerCallback ( $"{strLogPrefix}Configuring automatic decompression for use with preset Accept-Encoding = {pobjPresets.AcceptEncodingValue}" );
				}   // if ( Options?.LoggerCallback != null )

				handler.AutomaticDecompression =
					DecompressionMethods.GZip |
					DecompressionMethods.Deflate;
			}   // if ( !string.IsNullOrEmpty ( options?.AcceptEncodingValue ) )

			return new HttpClient ( handler , disposeHandler: true );
		}   // private static HttpClient CreateHttpClient


		/// <summary>
		/// Instance method CallWebApiAndProcessResultASyn invokes this routine
		/// on each iteration of its retry loop to attach HTTP headers directly
		/// to the HttpRequestMessage specified by <paramref name="phttpRequest"/>
		/// so that the request always provides its own set of request headers.
		/// </summary>
		/// <param name="phttpRequest">
		/// This HttpRequestMessage parameter is a reference to the new Request
		/// object that is created on each iteration of the request retry loop.
		/// </param>
		/// <param name="pfAcceptJson">
		/// When its value is True, the MediaTypeWithQualityHeaderValue header
		/// that represents the JSON MIME type is appended to the headers.
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/CallerInfo.XML"
		///          path="doc/members/member[@name='CallerInfoParameters']/*" />
		private void ApplyHeaders ( 
			HttpRequestMessage phttpRequest , 
			bool pfAcceptJson ,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = MagicNumbers.ZERO )
		{
			const string LOGGER_PREFIX = @"RequestEngine.ApplyHeaders: ";

			string strLogPrefix = $"{LOGGER_PREFIX}[{memberName} in {System.IO.Path.GetFileName ( sourceFilePath )} at line {sourceLineNumber}] ";

			if ( Options?.LoggerCallback != null )
			{
				Options.LoggerCallback ( $"{strLogPrefix}Applying headers to request for {phttpRequest.RequestUri} with pfAcceptJson = {pfAcceptJson}" );
			}   // if ( Options?.LoggerCallback != null )

			// ------------------------------------------------------------
			// Apply Accept header preset (overrides JSON Accept fallback).
			// ------------------------------------------------------------

			if ( !string.IsNullOrEmpty ( Options?.AcceptHeaderValue ) )
			{
				if ( Options?.LoggerCallback != null )
				{
					Options.LoggerCallback ( $"{strLogPrefix}Applying Accept header preset: {Options.AcceptHeaderValue}" );
				}   // if ( Options?.LoggerCallback != null )

				phttpRequest.Headers.Accept.Clear ( );
				phttpRequest.Headers.Accept.Add (
					new MediaTypeWithQualityHeaderValue ( Options.AcceptHeaderValue ) );
			}   // if ( !string.IsNullOrEmpty ( Options?.AcceptHeaderValue ) )

			// ------------------------------------------------------------
			// Apply Accept-Encoding preset.
			// ------------------------------------------------------------

			if ( !string.IsNullOrEmpty ( Options?.AcceptEncodingValue ) )
			{
				if ( !string.IsNullOrEmpty ( Options?.AcceptEncodingValue ) )
				{
					if ( Options?.LoggerCallback != null )
					{
						Options.LoggerCallback ( $"{strLogPrefix} Applying Accept-Encoding header preset: {Options.AcceptEncodingValue}" );
					}   // if ( Options?.LoggerCallback != null )

					phttpRequest.Headers.AcceptEncoding.Clear ( );

					string [ ] astrEncodings = Options.AcceptEncodingValue.Split ( SpecialCharacters.COMMA );

					foreach ( string strEncodingToken in astrEncodings )
					{
						string strTrimmedEncodingToken = strEncodingToken.Trim ( );

						if ( strTrimmedEncodingToken.Length > ListInfo.EMPTY_STRING_LENGTH )
						{
							phttpRequest.Headers.AcceptEncoding.Add (
								new StringWithQualityHeaderValue ( strTrimmedEncodingToken ) );
						}   // if ( strTrimmedEncodingToken.Length > ListInfo.EMPTY_STRING_LENGTH )
					}   // foreach ( string strEncodingToken in astrEncodings )
				}   // if ( !string.IsNullOrEmpty ( Options?.AcceptEncodingValue ) )
			}   // if ( !string.IsNullOrEmpty ( Options?.AcceptEncodingValue ) )

			// ------------------------------------------------------------
			// Apply Cache-Control preset.
			// ------------------------------------------------------------

			if ( !string.IsNullOrEmpty ( Options?.CacheControlValue ) )
			{
				if ( Options?.LoggerCallback != null )
				{
					Options.LoggerCallback ( $"{strLogPrefix} Applying Accept-Encoding header preset: {Options.AcceptEncodingValue}" );
				}   // if ( Options?.LoggerCallback != null )

				phttpRequest.Headers.CacheControl =
					CacheControlHeaderValue.Parse ( Options.CacheControlValue );
			}   // if ( !string.IsNullOrEmpty ( Options?.CacheControlValue ) )

			// ------------------------------------------------------------
			// Apply JSON Accept fallback ONLY if:
			//   1. pfAcceptJson == true
			//   2. No Accept preset was supplied
			// ------------------------------------------------------------

			if ( pfAcceptJson && string.IsNullOrEmpty ( Options?.AcceptHeaderValue ) )
			{
				bool hasJson = false;

				foreach ( MediaTypeWithQualityHeaderValue mt in phttpRequest.Headers.Accept )
				{
					if ( string.Equals ( mt.MediaType , JSON_MIME_TYPE , StringComparison.Ordinal ) )
					{
						hasJson = true;

						if ( Options?.LoggerCallback != null )
						{
							Options.LoggerCallback ( $"{strLogPrefix} Applying JSON Accept fallback	{JSON_MIME_TYPE} is already present." );
						}   // if ( Options?.LoggerCallback != null )

						break;
					}   // if ( string.Equals ( mt.MediaType , JSON_MIME_TYPE , StringComparison.Ordinal ) )
				}   // foreach ( MediaTypeWithQualityHeaderValue mt in request.Headers.Accept )

				if ( !hasJson )
				{
					if ( Options?.LoggerCallback != null )
					{
						Options.LoggerCallback ( $"{strLogPrefix} Applying JSON Accept fallback: {JSON_MIME_TYPE}" );
					}   // if ( Options?.LoggerCallback != null )

					phttpRequest.Headers.Accept.Add (
						new MediaTypeWithQualityHeaderValue ( JSON_MIME_TYPE ) );
				}   // if ( !hasJson )
			}   // if ( pfAcceptJson && string.IsNullOrEmpty ( Options?.AcceptHeaderValue ) )

			// ------------------------------------------------------------
			// Apply Prefer header for Microsoft Graph Messages endpoint.
			// ------------------------------------------------------------

			if ( phttpRequest.RequestUri.AbsolutePath.IndexOf (
					Properties.Resources.MESSAGES_ENDPOINT ,
					StringComparison.OrdinalIgnoreCase ) > ListInfo.INDEXOF_NOT_FOUND )
			{
				if ( Options?.LoggerCallback != null )
				{
					Options.LoggerCallback ( $"{strLogPrefix} Applying Prefer header for Microsoft Graph Messages endpoint ({Properties.Resources.MESSAGES_ENDPOINT}= {Properties.Resources.HTTP_HDR_IMMUTABLE_ID})" );
				}   // if ( Options?.LoggerCallback != null )

				phttpRequest.Headers.Add (
					Properties.Resources.HTTP_HDR_PREFER ,
					Properties.Resources.HTTP_HDR_IMMUTABLE_ID );
			}   // if ( request.RequestUri.AbsolutePath.IndexOf ( Properties.Resources.MESSAGES_ENDPOINT , StringComparison.OrdinalIgnoreCase ) > ListInfo.INDEXOF_NOT_FOUND )

			// ------------------------------------------------------------
			// Apply Authorization header (Bearer token).
			// ------------------------------------------------------------

			if ( !string.IsNullOrEmpty ( Options?.CurrentOAuthToken ) )
			{
				if ( Options?.LoggerCallback != null )
				{
					Options.LoggerCallback ( $"{strLogPrefix} Applying Authorization header (Bearer token) = {Options.CurrentOAuthToken}." );
				}   // if ( Options?.LoggerCallback != null )

				phttpRequest.Headers.Authorization =
					new AuthenticationHeaderValue (
						OAUTH_TOKEN_TYPE ,
						Options.CurrentOAuthToken );
			}   // if ( !string.IsNullOrEmpty ( Options?.CurrentOAuthToken ) )
		}   // private void ApplyHeaders


		/// <summary>
		/// Since the built-in HttpMethod object omits the seldom-used `PATCH`
		/// method, it is implemented as a static property so that it is created
		/// once only, when the class is first referenced.
		/// </summary>
		private static readonly HttpMethod s_PatchMethod = new HttpMethod ( @"PATCH" );


		/// <summary>
		/// Since the built-in HttpMethod object omits the seldom-used `PATCH`
		/// method, this IReadOnlyDictionary maps a complete set of HTTP verbs
		/// to the corresponding HttpMethod object, including the `PATCH` method
		/// that is defined and initialized when the class is first referenced,
		/// as is this dictionary.
		/// </summary>
		private static readonly IReadOnlyDictionary<HttpVerb , HttpMethod> s_dctVerbMap =
			new Dictionary<HttpVerb , HttpMethod>
			{
				{ HttpVerb.GET,    HttpMethod.Get },
				{ HttpVerb.PATCH,  s_PatchMethod },
				{ HttpVerb.POST,   HttpMethod.Post },
				{ HttpVerb.PUT,    HttpMethod.Put },
				{ HttpVerb.DELETE, HttpMethod.Delete }
			};

		/// <summary>
		/// We define our own string for the commonly required JSON MIME type.
		/// </summary>
		const string JSON_MIME_TYPE = @"application/json";


		/// <summary>
		/// We define our own string for the standard prefix used with most
		/// authorization tokens.
		/// </summary>
		const string OAUTH_TOKEN_TYPE = @"Bearer";
	}   // public class RequestEngine
}   // partial namespace WizardWrx.HTTP