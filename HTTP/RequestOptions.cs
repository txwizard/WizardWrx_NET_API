using WizardWrx.DLLConfigurationManager;


namespace WizardWrx.HTTP
{
	/// <summary>
	/// Instances of this object represent options for configuring request
	/// behavior, including provision of exception logging tooling and OAuth
	/// token retrieval methods.
	/// </summary>
	public sealed class RequestOptions
	{
		/// <summary>
		/// Let's be really clear about the shape of the default Accept HTTP header.
		/// </summary>
		public const string HTTP_ACCEPT_WILDCARD = @"*/*";


		/// <summary>
		/// Let's also document for posterity the Cache-Control directive that
		/// says "don't."
		/// </summary>
		public const string HTTP_NEVER_CACHE_ANYTHING = @"no-cache";


		/// <summary>
		/// The Microsoft .NET Framework supports only two of the common compression
		/// formats, gzip and deflate.
		/// </summary>
		public const string HTTP_SUPPORTED_COMPRESSION = @"gzip, deflate";


		/// <summary>
		/// This delegate signature represents a method that attempts to obtain an
		/// OAuth token and return it through an output parameter, along with the
		/// existing true/false outcome.
		/// </summary>
		/// <param name="token">
		/// When this method returns, contains the retrieved OAuth token if
		/// successful; otherwise, null.
		/// </param>
		/// <param name="logger">
		/// This represents the optional logger object for recording exceptions
		/// that arise during token retrieval.
		/// </param>
		/// <returns>
		/// The delegated function returns true if the token was successfully
		/// obtained; otherwise, it returns false and the <paramref name="token"/>
		/// value is undefined. The supplied copy of the access token is for use
		/// by the HTTP access method; the object that exposes the OAuth Access
		/// Token Getter is responsible for stashing a copy in protectd storage.
		/// </returns>
		public delegate bool OAuthTokenGetter (
			out string token ,
			ExceptionLogger logger = null
		);

		/// <summary>
		/// This read-only property returns an instance of ExceptionLogger for
		/// logging exceptions encountered during the processing of HTTP
		/// requests.
		/// </summary>
		public ExceptionLogger Logger { get; }


		/// <summary>
		/// This read-only property returns a delegate that retrieves OAuth Bearer
		/// tokens for inclusion in the Authorization header of HTTP requests.
		/// </summary>
		public OAuthTokenGetter TokenGetter { get; }


		/// <summary>
		/// When specified (not null), this string represents the value to 
		/// specify as the Accept HTTP header value.
		/// </summary>
		public string AcceptHeaderValue { get; }


		/// <summary>
		/// When specified (not null), this string represents the value to 
		/// specify as the Accept-Encoding HTTP header value.
		/// </summary>
		public string AcceptEncodingValue { get; }


		/// <summary>
		/// When specified (not null), this string represents the value to 
		/// specify as the Cache-Control HTTP header value.
		/// </summary>
		public string CacheControlValue { get; }

		/// <summary>
		/// This read-only string property gets the current OAuth token,
		/// retrieving and caching it if necessary.
		/// </summary>
		public string CurrentOAuthToken
		{
			get
			{
				// Only try to acquire a token when:
				//   1. A getter exists, and
				//   2. We haven't cached one yet
				if ( TokenGetter != null && _currentOAuthToken == null )
				{
					string strNewToken;

					if ( TokenGetter ( out strNewToken , Logger ) )
					{
						_currentOAuthToken = strNewToken;
					}   // if ( TokenGetter ( out strNewToken , Logger ) )
				}   // if ( TokenGetter != null && _currentOAuthToken == null )

				return _currentOAuthToken;
			}   // public string CurrentOAuthToken property get

			set
			{
				_currentOAuthToken = ( !string.IsNullOrEmpty ( value ) && value.Length > ListInfo.EMPTY_STRING_LENGTH
					? value
					: _currentOAuthToken );
			}   // public string CurrentOAuthToken property set
		}   // public string CurrentOAuthToken property


		/// <summary>
		/// The public constructor initializes both read-only properties of this
		/// class, so that consumers of this class must make their choices about
		/// these properties explicit.
		/// </summary>
		/// <param name="plogger">
		/// The ExceptionLogger instance to be used for logging Exceptions is
		/// optional; if no object is supplied, no Exception logging happens
		/// inside the other methods exposed by this library.
		/// </param>
		/// <param name="ptokenGetter">
		/// The delegate that retrieves OAuth Bearer tokens for inclusion in the
		/// Authorization header of HTTP requests is optional; if no delegate is
		/// provided, requests proceed without Authorization headers.
		/// </param>
		/// <param name="pstrInitialOAuthToken">
		/// This optional parameter allows the caller to supply an initial OAuth
		/// token that the class caches for future use. If this parameter is 
		/// null and <paramref name="ptokenGetter"/> is not null, the class 
		/// attempts to obtain a token the first time the property value is
		/// queried.
		/// </param>
		/// <param name="pstrAcceptHeaderValue">
		/// When specified, thiis string represents the value to include as the
		/// Accept HTTP header value. By default, the null value, "/", is added.
		/// </param>
		/// <param name="pstrAcceptEncodingValue">
		/// When specified, this string represents the value to include as the
		/// Accept-Encoding HTTP header value. By default, this value is null,
		/// and is, therefore, omitted.
		/// </param>
		/// <param name="pstrCacheControlValue">
		/// When specified, this string represents the value to include as the
		/// Cache-Control HTTP header value. The default  value is "no-cache".
		/// </param>
		public RequestOptions ( ExceptionLogger plogger , OAuthTokenGetter ptokenGetter , string pstrInitialOAuthToken = null , string pstrAcceptHeaderValue = HTTP_ACCEPT_WILDCARD , string pstrAcceptEncodingValue = null , string pstrCacheControlValue = HTTP_NEVER_CACHE_ANYTHING )
		{
			Logger = plogger;
			TokenGetter = ptokenGetter;
			_currentOAuthToken = pstrInitialOAuthToken;
			AcceptHeaderValue = pstrAcceptHeaderValue;
			AcceptEncodingValue = pstrAcceptEncodingValue;
			CacheControlValue = pstrCacheControlValue;
		}   // public RequestOptions


		/// <summary>
		/// The default constructor is hidden to enforce instatiation of only
		/// explicitly initialized instances.
		/// </summary>
		private RequestOptions ( ) { }


		/// <summary>
		/// This private field caches the current OAuth token. Provision of this
		/// private property supports lazy loading of the token.
		/// </summary>
		private string _currentOAuthToken = null;
	}   // public sealed class RequestOptions
}   // partial namespace WizardWrx.HTT