using System;
using System.IO;
using System.Text;

using Newtonsoft.Json.Linq;

using WizardWrx;
using WizardWrx.ConsoleAppAids3;
using WizardWrx.HTTP;


namespace HttpRequestEngineTestStand
{
	internal static class Program
	{
		private const string HORIZONTAL_RULE =
			"------------------------------------------------------------";

		private static ConsoleAppStateManager s_theApp = ConsoleAppStateManager.GetTheSingleInstance ( );

		private static void Main ( string [ ] args )
		{
			Console.OutputEncoding = Encoding.UTF8;

			s_theApp.DisplayBOJMessage ( );

			Console.WriteLine ( HORIZONTAL_RULE );
			Console.WriteLine ( "RequestEngine Test Stand" );
			Console.WriteLine ( HORIZONTAL_RULE );

			TestResult [ ] results = new TestResult [ 8 ];

			results [ 0 ] = RunTest ( "JSON as JObject" , TestJsonAsJObject );
			results [ 1 ] = RunTest ( "JSON as POCO" , TestJsonAsPoco );
			results [ 2 ] = RunTest ( "Plain text" , TestPlainText );
			results [ 3 ] = RunTest ( "Binary download (byte[])" , TestBinaryDownload );
			results [ 4 ] = RunTest ( "Header presets" , TestHeaderPresets );
			results [ 5 ] = RunTest ( "Gzip decompression" , TestGzipDecompression );
			results [ 6 ] = RunTest ( "Get M2C Call Recording" , TestM2CCallRecordingGetter );
			results [ 7 ] = RunTest ( "Invoke M2C Dialer Invoker" , TestM2CDialerInvoker );

			Console.WriteLine ( );
			Console.WriteLine ( $"{Environment.NewLine}{HORIZONTAL_RULE}" );
			Console.WriteLine ( "TEST MATRIX" );
			Console.WriteLine ( $"{HORIZONTAL_RULE}{Environment.NewLine}" );

			Console.WriteLine ( "Test Name                                Result" );
			Console.WriteLine ( "---------------------------------------- -------" );

			foreach ( TestResult tr in results )
			{
				string paddedName = tr.Name.PadRight ( 40 );
				string status = tr.Success ? "PASS" : "FAIL";
				Console.WriteLine ( paddedName + " " + status );
			}   // foreach ( TestResult tr in results )

			Console.WriteLine ( HORIZONTAL_RULE );
			Console.WriteLine ( "All tests completed." );
			Console.WriteLine ( HORIZONTAL_RULE );
			s_theApp.DisplayEOJMessage ( );
		}

		private static void TestM2CDialerInvoker ( )
		{
			const string API_KEY = @"YLa0hkDaVzkfQOYaptPfe1zp1zPaaGKOY915a4MxRinCYiNGXxNhPn0m0YSSIlPsiEyPtXPl41qHtlCYOy70y4eVSuvjAqDSK0rxsVF6IVBGfetMT9Jbz0mYYFuVeVvzrpz/r3rJKS0vCCV1eEzdAoFII9nPAn0nBAbq7D+EX1tkpxoc4sTbK8TCv8wwepcEaBK3PmaEtyhA5vbkAiCIbDmKfAKJNkRU94evX67w0zo=";
			const string PAYLOAD = "{ \"Destination\" : \"17704834570\", \"UserIdentifier\" : \"budp @salesrelevance.com\", \"CurrentCallerID\": \"14805691477\", \"SendCLI\":\"14805691477\", \"ExternalId\": \"4523\" }";
			Console.WriteLine ( $"{Environment.NewLine}TEST 8: M2C Dialer{Environment.NewLine}" );
			Console.WriteLine ( HORIZONTAL_RULE );

			// public RequestOptions ( ExceptionLogger plogger = null , OAuthTokenGetter ptokenGetter = null , string pstrInitialOAuthToken = null , string pstrAcceptHeaderValue = HTTP_ACCEPT_WILDCARD , string pstrAcceptEncodingValue = null , string pstrCacheControlValue = HTTP_NEVER_CACHE_ANYTHING , int pintRetryLimit = 10 , int pintRetryDelay = 10 , HttpEngineLogCallback pdiLogCallback = null )
			RequestOptions requestOptions = new RequestOptions (
				plogger: null ,                                                 // ExceptionLogger       plogger                 = null
				ptokenGetter: null ,                                            // OAuthTokenGetter      ptokenGetter            = null
				pstrInitialOAuthToken: API_KEY ,                                // string                pstrInitialOAuthToken   = null
				pstrAcceptHeaderValue: RequestOptions.HTTP_ACCEPT_WILDCARD ,    // string                pstrAcceptHeaderValue   = HTTP_ACCEPT_WILDCARD
				pstrAcceptEncodingValue: RequestOptions.HTTP_COMPRESSION_ALGS , // string                pstrAcceptEncodingValue = null
				pstrCacheControlValue: RequestOptions.HTTP_NEVER_CACHE ,        // string                pstrCacheControlValue   = HTTP_NEVER_CACHE_ANYTHING
																				// int                   pintRetryLimit          = 10
																				// int                   pintRetryDelay          = 10
				pdiLogCallback: LogIt ,                                         // HttpEngineLogCallback pdiLogCallback          = null
				pstrContentType: RequestOptions.HTTP_CONTENT_TYPE_JSON );       // string                pstrContentType         = HTTP_CONTENT_TYPE_JSON

			RequestEngine engine = new RequestEngine ( requestOptions );

			// The sample Call Recording created by Bud's test.
			string url = "https://api.spikko.com/api/Call/InitiateOutgoingCallRequestForOrg/";
			Console.WriteLine ( engine.GetDiagnosticHeadersDump ( new Uri ( url ) , false ) );

			(string strResult,string strStatusMessage) = engine.SendRequest<string> (
				url ,                                                           // string                   pstrWebApiUrl
				null ,                                                          // Action<JObject>          pfunProcessResultCallback = null
				new WizardWrx.JsonSupport.JSON_Deserialized_Object ( PAYLOAD ) ,// JSON_Deserialized_Object pjSON_Deserialized        = null
				RequestEngine.HttpVerb.POST ,                                   // HttpVerb                 penmVerb                  = HttpVerb.POST
				pfExpectJSON: false );                                          // bool                     pfExpectJSON              = true
			Console.WriteLine ( $"Response String Value   = {strStatusMessage}" );
			Console.WriteLine ( $"Response status Message = {strStatusMessage}" );
		}   // private static void TestM2CDialerInvoker

		private sealed class TestResult
		{
			public string Name;
			public bool Success;
			public Exception Error;
		}   // private sealed class TestResult

		private static TestResult RunTest ( string name , Action testMethod )
		{
			TestResult result = new TestResult ( );
			result.Name = name;

			try
			{
				testMethod ( );
				result.Success = true;
			}
			catch ( Exception ex )
			{
				result.Success = false;
				result.Error = ex;

				Console.WriteLine ( );
				Console.WriteLine ( $"{Environment.NewLine}Test FAILED: {name}" );
				Console.WriteLine ( "Exception:" );
				Console.WriteLine ( ex.ToString ( ) );
			}

			return result;
		}   // private static TestResult RunTest

		private static void TestGzipDecompression ( )
		{
			Console.WriteLine ( );
			Console.WriteLine ( "TEST 6: Gzip decompression" );
			Console.WriteLine ( HORIZONTAL_RULE );

			RequestOptions opts = new RequestOptions (
				plogger: null ,
				ptokenGetter: null ,
				pstrInitialOAuthToken: null ,
				pstrAcceptHeaderValue: "application/json" ,
				pstrAcceptEncodingValue: "gzip" ,
				pstrCacheControlValue: "no-cache"
			);

			RequestEngine engine = new RequestEngine ( opts );

			string url = "https://httpbin.org/gzip";

			(JObject result, string strStatusMessage) = engine.SendRequest<JObject> (
				url ,
				null ,
				null ,
				RequestEngine.HttpVerb.GET ,
				pfExpectJSON: true );

			Console.WriteLine ( "Result JSON:" );
			Console.WriteLine ( result.ToString ( ) );
			Console.WriteLine ( $"{Environment.NewLine}Response status Message = {strStatusMessage}{Environment.NewLine}" );

			// httpbin returns: { "gzipped": true, ... }
			if ( !result.ContainsKey ( "gzipped" ) )
				throw new Exception ( "Gzip decompression failed or unexpected response." );

			if ( result [ "gzipped" ].ToString ( ) != "True" )
				throw new Exception ( "Gzip decompression did not produce expected JSON." );
		}   // private static void TestGzipDecompression


		private static void TestHeaderPresets ( )
		{
			Console.WriteLine ( );
			Console.WriteLine ( "TEST 5: Header presets" );
			Console.WriteLine ( HORIZONTAL_RULE );

			RequestOptions opts = new RequestOptions (
				plogger: null ,
				ptokenGetter: null ,
				pstrInitialOAuthToken: null ,
				pstrAcceptHeaderValue: "application/xml" ,
				pstrAcceptEncodingValue: "gzip" ,
				pstrCacheControlValue: "max-age=0"
			);

			RequestEngine engine = new RequestEngine ( opts );

			Uri dummy = new Uri ( "https://example.com/" );

			string dump = engine.GetDiagnosticHeadersDump ( dummy , pfExpectJson: false );

			Console.WriteLine ( dump );

			// Simple PASS/FAIL checks
			if ( !dump.Contains ( "Accept: application/xml" ) )
				throw new Exception ( "Accept header preset not applied." );

			if ( !dump.Contains ( "Accept-Encoding: gzip" ) )
				throw new Exception ( "Accept-Encoding preset not applied." );

			if ( !dump.Contains ( "Cache-Control: max-age=0" ) )
				throw new Exception ( "Cache-Control preset not applied." );
		}

		private static void TestJsonAsJObject ( )
		{
			Console.WriteLine ( $"{Environment.NewLine}TEST 1: JSON as JObject{Environment.NewLine}" );
			Console.WriteLine ( HORIZONTAL_RULE );

			RequestEngine engine = new RequestEngine ( );

			// Postman Echo returns JSON describing your request.
			string url = "https://postman-echo.com/get?foo=bar";

			(JObject result, string strStatusMessage) = engine.SendRequest<JObject> (
				url ,
				null ,
				null ,
				RequestEngine.HttpVerb.GET ,
				pfExpectJSON: true );

			Console.WriteLine ( "Result type: " + result.GetType ( ).FullName );
			Console.WriteLine ( "JSON keys:   " + string.Join ( ", " , result.Properties ( ) ) );
			Console.WriteLine ( $"{Environment.NewLine}Response status Message = {strStatusMessage}{Environment.NewLine}" );
		}   // private static void TestJsonAsJObject


		private static void TestJsonAsPoco ( )
		{
			Console.WriteLine ( $"{Environment.NewLine}TEST 2: JSON as POCO{Environment.NewLine}" );
			Console.WriteLine ( HORIZONTAL_RULE );

			RequestEngine engine = new RequestEngine ( );

			string url = "https://postman-echo.com/get?alpha=beta";

			// Define a simple POCO that matches the JSON structure.
			// Postman Echo returns: { "args": { "alpha": "beta" }, ... }
			(EchoResponse poco, string strStatusMessage) = engine.SendRequest<EchoResponse> (
				url ,
				null ,
				null ,
				RequestEngine.HttpVerb.GET ,
				pfExpectJSON: true );

			Console.WriteLine ( "POCO type: " + poco.GetType ( ).FullName );
			Console.WriteLine ( "args.alpha = " + poco.args.alpha );
			Console.WriteLine ( $"{Environment.NewLine}Response status Message = {strStatusMessage}{Environment.NewLine}" );
		}   // private static void TestJsonAsPoco


		private static void TestPlainText ( )
		{
			Console.WriteLine ( $"{Environment.NewLine}TEST 3: Plain Text{Environment.NewLine}" );
			Console.WriteLine ( HORIZONTAL_RULE );

			RequestEngine engine = new RequestEngine ( );

			// This endpoint returns plain text.
			string url = "https://www.example.com/";

			(string text, string strStatusMessage) = engine.SendRequest<string> (
				url ,
				null ,
				null ,
				RequestEngine.HttpVerb.GET ,
				pfExpectJSON: false );

			Console.WriteLine ( "Returned string length: " + text.Length );
			Console.WriteLine ( "First 80 chars:" );
			Console.WriteLine ( text.Substring ( 0 , Math.Min ( 80 , text.Length ) ) );
			Console.WriteLine ( $"{Environment.NewLine}Response status Message = {strStatusMessage}{Environment.NewLine}" );
		}   // private static void TestPlainText


		private static void TestBinaryDownload ( )
		{
			Console.WriteLine ( $"{Environment.NewLine}TEST 4: Binary download (byte[]){Environment.NewLine}" );
			Console.WriteLine ( HORIZONTAL_RULE );

			RequestEngine engine = new RequestEngine ( );

			// A small binary file for testing.
			// This is a 1x1 PNG pixel hosted by GitHub.
			string url =
				"https://raw.githubusercontent.com/github/explore/main/topics/csharp/csharp.png";

			(byte [ ] bytes, string strStatusMessage) = engine.SendRequest<byte [ ]> (
				url ,
				null ,
				null ,
				RequestEngine.HttpVerb.GET ,
				pfExpectJSON: false );

			Console.WriteLine ( "Downloaded byte count: " + bytes.Length );

			// Save to disk so the user can verify the file.
			string outFile = Path.Combine ( Environment.CurrentDirectory , "test_download.png" );
			File.WriteAllBytes ( outFile , bytes );

			Console.WriteLine ( "Saved file: " + outFile );
			Console.WriteLine ( $"{Environment.NewLine}Response status Message = {strStatusMessage}{Environment.NewLine}" );
		}   // private static void TestBinaryDownload


		private static void TestM2CCallRecordingGetter ( )
		{
			const string API_KEY = @"YLa0hkDaVzkfQOYaptPfe1zp1zPaaGKOY915a4MxRinCYiNGXxNhPn0m0YSSIlPsiEyPtXPl41qHtlCYOy70y4eVSuvjAqDSK0rxsVF6IVBGfetMT9Jbz0mYYFuVeVvzrpz/r3rJKS0vCCV1eEzdAoFII9nPAn0nBAbq7D+EX1tkpxoc4sTbK8TCv8wwepcEaBK3PmaEtyhA5vbkAiCIbDmKfAKJNkRU94evX67w0zo=";

			Console.WriteLine ( $"{Environment.NewLine}TEST 7: M2C Download{Environment.NewLine}" );
			Console.WriteLine ( HORIZONTAL_RULE );

			// public RequestOptions ( ExceptionLogger plogger = null , OAuthTokenGetter ptokenGetter = null , string pstrInitialOAuthToken = null , string pstrAcceptHeaderValue = HTTP_ACCEPT_WILDCARD , string pstrAcceptEncodingValue = null , string pstrCacheControlValue = HTTP_NEVER_CACHE_ANYTHING , int pintRetryLimit = 10 , int pintRetryDelay = 10 , HttpEngineLogCallback pdiLogCallback = null )
			RequestOptions requestOptions = new RequestOptions (
				plogger: null ,                                                 // ExceptionLogger       plogger                 = null
				ptokenGetter: null ,                                            // OAuthTokenGetter      ptokenGetter            = null
				pstrInitialOAuthToken: API_KEY ,                                // string                pstrInitialOAuthToken   = null
				pstrAcceptHeaderValue: RequestOptions.HTTP_ACCEPT_WILDCARD ,    // string                pstrAcceptHeaderValue   = HTTP_ACCEPT_WILDCARD
				pstrAcceptEncodingValue: RequestOptions.HTTP_COMPRESSION_ALGS , // string                pstrAcceptEncodingValue = null
				pstrCacheControlValue: RequestOptions.HTTP_NEVER_CACHE ,        // string                pstrCacheControlValue   = HTTP_NEVER_CACHE_ANYTHING
																				// int                   pintRetryLimit          = 10
																				// int                   pintRetryDelay          = 10
				pdiLogCallback: LogIt );                                        // HttpEngineLogCallback pdiLogCallback          = null )

			RequestEngine engine = new RequestEngine ( requestOptions );

			// The sample Call Recording created by Bud's test.
			string url = "https://secure.spikko.com/api/Media/Play/1776989670.671340.mp3";
			Console.WriteLine ( engine.GetDiagnosticHeadersDump ( new Uri ( url ) , false ) );
			(byte [ ] bytes, string strStatusMessage) = engine.SendRequest<byte [ ]> (
				url ,
				null ,
				null ,
				RequestEngine.HttpVerb.GET ,
				pfExpectJSON: false );

			Console.WriteLine ( "Downloaded byte count: " + bytes.Length );

			// Save to disk so the user can verify the file by playing it back.
			string outFile = Path.Combine ( Environment.CurrentDirectory , "test_M2C_Recording.MP3" );
			File.WriteAllBytes ( outFile , bytes );

			Console.WriteLine ( $"Saved file: {outFile}" );
			Console.WriteLine ( $"{Environment.NewLine}Response status Message = {strStatusMessage}{Environment.NewLine}" );
		}   // private static void TestM2CCallRecordingGetter

		public static void LogIt ( string message ,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = SpecialStrings.EMPTY_STRING ,
			[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = MagicNumbers.ZERO )
		{
			if ( !Console.IsOutputRedirected )
			{
				// Interactive console → safe to use ConsoleColor
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.ForegroundColor = ConsoleColor.Yellow;
			}

			Console.WriteLine ( Sanitize ( $"[{DateTime.UtcNow:yyyy/MM/dd HH:mm:ss.fff} {memberName} at {Path.GetFileName ( sourceFilePath )}:{sourceLineNumber}] {message}" ) );

			if ( !Console.IsOutputRedirected )
			{
				Console.ResetColor ( );
			}

			//	----------------------------------------------------------------
			//	In a real application, you might write this to a log file.
			//	For this test stand, we'll just print it to the console.
			//
			//	You can also add timestamps or other formatting as needed.
			//
			//	This callback must be thread-safe, as it may be called from
			//	multiple threads if the RequestEngine is used concurrently.
			//
			//	This callback cannot be implemented as a lambda because lambdas
			//	cannot have caller info attributes on their parameters.
			//	----------------------------------------------------------------
		}   // public static void LogIt


		// POCO for TestJsonAsPoco
		private sealed class EchoResponse
		{
			public Args args { get; set; }

			public sealed class Args
			{
				public string alpha { get; set; }
			}
		}   // private sealed class EchoRespons

		private static readonly System.Collections.Generic.Dictionary<char , char> Map = new System.Collections.Generic.Dictionary<char , char> ( )
		{
			[ '–' ] = '-' ,
			[ '—' ] = '-' ,
			[ '•' ] = '*' ,
			[ '’' ] = '\'' ,
			[ '‘' ] = '\'' ,
			[ '“' ] = '"' ,
			[ '”' ] = '"' ,
			[ '\u00A0' ] = ' ' , // NBSP
			[ '\u200B' ] = ' ' , // zero-width space
		};

		private static string Sanitize ( string s )
		{
			var sb = new StringBuilder ( s.Length );
			foreach ( char c in s )
				sb.Append ( Map.TryGetValue ( c , out var r ) ? r : c );
			return sb.ToString ( );
		}

	}   // internal static class Program
}   // namespace HttpRequestEngineTestStand