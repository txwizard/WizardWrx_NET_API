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
    ============================================================================
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace WizardWrx.HTTP
{
	/// <summary>
	/// Instances of this class export a single method that supports all five
	/// HTTP Request verbs.
	/// </summary>
	public class RequestEngine
	{
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
			/// This value maps to the dangerous DELETE verb, and is usually 
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
		/// The default constructor leaves the Options property null, which is
		/// OK for most use cases.
		/// </summary>
		public RequestEngine ( ) { }   // public RequestEngine constructor (1 of 2)


		/// <summary>
		/// This constructor initializes the Options property, which is not
		/// strictly required for most use cases, and can be passed in as a
		/// RequestOptions constructor call, since the RequestOptions object is
		/// a POCO that doesn't even need System; its only reference is to the
		/// WizardWrx.DLLConfigurationManager namespace, where the 
		/// ExceptionLogger lives.
		/// </summary>
		/// <param name="pobjRequestOptions">
		/// When supplied, the RequestOptions object reference is stored in the
		/// Options property, providing a read-only reference to everything but
		/// the OAuth Access Token, which must, of course, be updateable because
		/// access tokens are short-lived.
		/// </param>
		public RequestEngine ( RequestOptions pobjRequestOptions )
		{
			Options = pobjRequestOptions;
		}   // public RequestEngine constructor (2 of 2)


		/// <summary>
		/// Calls the protected web API and processes the result
		/// </summary>
		/// <param name="pstrWebApiUrl">
		/// URL of the web API to call (supposed to return Json)
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
		/// <exception cref="Exception">
		/// Two distinct circumstances can give rise to an Exception exception.
		/// <list type="number">
		/// <item>
		/// The OAuth token expired and a replacement cannot be obtained from
		/// the Microsoft Azure identity server.
		/// </item>
		/// <item>
		/// Another type of exception arose while processing a request against
		/// the Microsoft Graph API endpoints.
		/// </item>
		/// </list>
		/// </exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
		/// An InvalidEnumArgumentException Exception arises when the value of
		/// optional argument <paramref name="penmVerb"/> is invalid, ordinarily
		/// due to a programming error, since it is reasonbly safe to assume
		/// that the verb is always hard coded per toe API.
		/// </exception>
		public object CallWebApiAndProcessResultASync (
			string pstrWebApiUrl ,
			Action<JObject> pfunProcessResultCallback = null ,
			JSON_Deserialized_Object pjSON_Deserialized = null ,
			HttpVerb penmVerb = HttpVerb.POST ,
			bool pfExpectJSON = true )
		{
			Uri uriAsInput = new Uri ( pstrWebApiUrl );			
			bool fTry = true;
			HttpVerb enmRealVerb = ( ( pjSON_Deserialized == null ) && ( penmVerb != HttpVerb.DELETE ) ) ? HttpVerb.GET : penmVerb;

			while ( fTry )
			{
				using ( HttpRequestMessage httpRequest = new HttpRequestMessage ( s_dctVerbMap [ penmVerb ] , uriAsInput ) )
				{
					ApplyHeaders (
						httpRequest ,                       // HttpRequestMessage phttpRequest
						Options.CurrentOAuthToken );        // string pstrBearerToken = null
															// bool pfAcceptJson = true
					httpRequest.Content = pjSON_Deserialized == null ? null : new StringContent (
						pjSON_Deserialized.JSON ,           // string               content (the JSON string)
						Encoding.UTF8 ,                     // System.Text.Encoding encoding
						JSON_MIME_TYPE );                   // string               mediatype (MIME type)

					using ( Task<HttpResponseMessage> httpTask1 = s_HttpClient.SendAsync ( httpRequest ) )
					{
						httpTask1.Wait ( );

						using ( HttpResponseMessage response = httpTask1.Result )
						{   // HttpResponseMessage is Disposable.
							if ( response.IsSuccessStatusCode )
							{   // Satisfy the condition of the enclosing While loop.
								fTry = false;

								Task<string> httpTask2 = response.Content.ReadAsStringAsync ( );
								httpTask2.Wait ( );
								string strResult = httpTask2.Result;

								if ( pfExpectJSON )
								{
									JObject jstrResult = JsonConvert.DeserializeObject ( strResult ) as JObject;

									if ( pfunProcessResultCallback != null )
									{   // Unused for POST requests.
										pfunProcessResultCallback ( jstrResult );
									}   // if ( pfunProcessResultCallback != null )

									return jstrResult;
								}   // TRUE (outcome given default value for pfExpectJSON) block, if ( pfExpectJSON )
								else
								{
									return strResult;
								}   // FALSE (outcome given overridden value for pfExpectJSON) block, if ( pfExpectJSON ) 
							}   // TRUE (anticipated outcome) block, if ( response.IsSuccessStatusCode ) 
							else
							{   // Check for an expired token.
								if ( response.StatusCode == HttpStatusCode.Unauthorized )
								{
									string strNewOAuthToken = null;

									if ( Options.TokenGetter != null && Options.TokenGetter ( out strNewOAuthToken , Options.Logger ) )
									{
										fTry = false;
									}
									else
									{   // Request denied. Throw up our hands and bug out.
										if ( httpTask1 != null )
										{   // The Task<HttpResponseMessage> doesn't lend itself to a Using block.
											httpTask1.Dispose ( );
										}   // if ( httpTask1 != null )

										throw new Exception ( Properties.Resources.ERRMSG_TOKEN_REFRESH_FAIL );
									}   // FALSE (unanticipated outcome) block, if ( _clientApplication_Adapter.GetOAuthToken ( ) )
								}   // TRUE (anticipated outcome) block, if ( response.StatusCode == System.Net.HttpStatusCode.Unauthorized )
								else
								{
									StringBuilder builder = new StringBuilder ( MagicNumbers.CAPACITY_01KB );
									builder.AppendLine ( $"Web API Call failed: {response.StatusCode}\n" );

									using ( Task<string> httpTask2 = response.Content.ReadAsStringAsync ( ) )
									{
										httpTask2.Wait ( );
										string strResultContent = httpTask2.Result;

										//  ----------------------------------------
										//	When calling the Microsoft Graph API,
										//  note that if you got reponse.Code == 403
										//  and reponse.content.code ==
										//  "Authorization_RequestDenied" that this
										//  is because the tenant admin  has not
										//  granted consent for the application to
										//  call the Web API.
										//  ----------------------------------------

										builder.AppendLine ( $"Content: {strResultContent}" );

										throw new Exception ( builder.ToString ( ) );
									}   // using ( Task<string> httpTask2 = response.Content.ReadAsStringAsync ( ) )
								}   // FALSE (unanticipated outcome) block, if ( response.StatusCode == System.Net.HttpStatusCode.Unauthorized )
							}   // FALSE (unanticipated outcome) block, if ( response.IsSuccessStatusCode ) 
						}   // using ( HttpResponseMessage response = httpTask1.Result )
					}   // using ( Task<HttpResponseMessage> httpTask1 = s_HttpClient.SendAsync ( httpRequest ) )
				}   // using ( HttpRequestMessage httpRequest = new HttpRequestMessage ( s_dctVerbMap [ penmVerb ] , uriAsInput ) )
			}   // while ( fTry )

			throw new InvalidOperationException ( @"This Exception should never happen because the code should return from one of two exit points inside the while loop." );
		}   // public void CallWebApiAndProcessResultASyn


		/// <summary>
		/// Extract the Query from any URI into an IDictionary of name-value
		/// pairs that can be URL encoded properly.
		/// </summary>
		/// <param name="puriToParse">
		/// Pass in a reference to the System.Uri to process.
		/// </param>
		/// <returns>
		/// The return value is the IDictionary populated with keys for each
		/// named value in the Query of <paramref name="puriToParse"/>. If the query is
		/// empty or absent, the returned dictionary is empty.
		/// </returns>
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
						string [ ] astrKeyAndValue = astrPairs [ intCurrentPair ].Split ( new [ ] { SpecialCharacters.EQUALS_SIGN } , MagicNumbers.PLUS_TWO );

						string strKey = astrKeyAndValue [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
						string strValue = astrKeyAndValue.Length > ListInfo.EXACTLY_ONE_ITEM ? astrKeyAndValue [ ArrayInfo.ARRAY_SECOND_ELEMENT ] : SpecialStrings.EMPTY_STRING;

						// Do NOT decode here — keep raw values.
						dctQueryValues [ strKey ] = strValue;
					}   // if ( !string.IsNullOrWhiteSpace ( astrPairs [ intCurrentPair ] ) )
				}   // for ( int intCurrentPair = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCurrentPair < intPairCount ; intCurrentPair++ )
			}   // if ( !string.IsNullOrWhiteSpace ( strQuery ) )

			return dctQueryValues;
		}   // public static IDictionary<string , string> ParseQueryToDictionary


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
		/// <param name="pstrBearerToken">
		/// This string represents the optional OAuth Bearer Access Token, which
		/// goes into the AuthenticationHeaderValue that becomes the value of
		/// the Authorization header that is appended to the collection of HTTP
		/// headers.
		/// </param>
		/// <param name="pfAcceptJson">
		/// When its value is True, the MediaTypeWithQualityHeaderValue header
		/// that represents the JSON MIME type is appended to the headers.
		/// </param>
		private static void ApplyHeaders (
			HttpRequestMessage phttpRequest ,
			string pstrBearerToken = null ,
			bool pfAcceptJson = true )
		{
			if ( phttpRequest.Headers.Accept == null || !phttpRequest.Headers.Accept.Any ( m => m.MediaType == JSON_MIME_TYPE ) )
			{   // Add the JSON MIME type to the list of acceptable response format types.
				s_HttpClient.DefaultRequestHeaders.Accept.Add (
					new MediaTypeWithQualityHeaderValue (               // T     item
						JSON_MIME_TYPE ) );                             // string mediaType
			}   // if ( defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any ( m => m.MediaType == JSON_MIME_TYPE ) )

			if ( phttpRequest.RequestUri.AbsolutePath.IndexOf ( Properties.Resources.MESSAGES_ENDPOINT ) > ListInfo.INDEXOF_NOT_FOUND )
			{   // This heading is added when the URI represents a call to the Microsoft Graph Messages endpoint.
				phttpRequest.Headers.Add (
					Properties.Resources.HTTP_HDR_PREFER ,              // string name
					Properties.Resources.HTTP_HDR_IMMUTABLE_ID );       // string value
			}   // if ( pstrWebApiUrl.IndexOf ( Properties.Resources.MESSAGES_ENDPOINT ) > ListInfo.INDEXOF_NOT_FOUND )

			if ( pfAcceptJson )
			{
				phttpRequest.Headers.Accept.Add (
					new MediaTypeWithQualityHeaderValue (
						JSON_MIME_TYPE ) );
			}   // if ( pfAcceptJson )

			if ( !string.IsNullOrEmpty ( pstrBearerToken ) )
			{
				phttpRequest.Headers.Authorization = new AuthenticationHeaderValue (
					OAUTH_TOKEN_TYPE ,
					pstrBearerToken );
			}   // if ( !string.IsNullOrEmpty ( bearerToken ) )
		}   // private static void ApplyHeaders


		/// <summary>
		/// The constructor stashes a reference to the HTTPClient in a protected
		/// location.
		/// </summary>
		private static HttpClient s_HttpClient = new HttpClient ( );


		private static readonly IReadOnlyDictionary<HttpVerb , HttpMethod> s_dctVerbMap =
			new Dictionary<HttpVerb , HttpMethod>
			{
				{ HttpVerb.GET,    HttpMethod.Get },
				{ HttpVerb.PATCH,  new HttpMethod ( @"PATCH" ) },
				{ HttpVerb.POST,   HttpMethod.Post },
				{ HttpVerb.PUT,    HttpMethod.Put },
				{ HttpVerb.DELETE, HttpMethod.Delete }
			};
		const string JSON_MIME_TYPE = @"application/json";
		const string OAUTH_TOKEN_TYPE = @"Bearer";
	}   // public class RequestEngine
}   // partial namespace WizardWrx.HTTP