using System;
using System.Collections.Generic;

using WizardWrx;
using WizardWrx.JSONSupport;

namespace DLLServices2TestStand
{
	internal class JsonFieldAccessorTests
	{
		private int _tests = 0;
		private int _passed = 0;
		private int _failed = 0;

		private void WritePass ( string message )
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ( $"PASS: {message}" );
			Console.ResetColor ( );
			_passed++;
		}   // private void WritePass

		private void WriteFail ( string message )
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine ( $"FAIL: {message}" );
			Console.ResetColor ( );
			_failed++;
		}   // private void WriteFail

		private void Run ( string description , Func<bool> test )
		{
			_tests++;
			try
			{
				if ( test ( ) )
					WritePass ( description );
				else
					WriteFail ( description );
			}
			catch ( Exception ex )
			{
				WriteFail ( $"{description} (EXCEPTION: {ex.Message})" );
			}
		}   // private void Run


		public void Execute ( )
		{
			Console.WriteLine ( "=== JsonFieldAccessor Test Suite ===" );
			Console.WriteLine ( );

			// ------------------------------------------------------------
			//  STRING TESTS
			// ------------------------------------------------------------
			Run ( "String: valid string" ,
				( ) =>
				{
					var json = @"{ ""name"": ""Alice"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsString ( "name" ) == "Alice";
				} );

			Run ( "String: missing field returns null" ,
				( ) =>
				{
					var json = @"{ ""name"": ""Alice"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsString ( "age" ) == null;
				} );

			// ------------------------------------------------------------
			//  BOOLEAN TESTS
			// ------------------------------------------------------------
			Run ( "Boolean: true literal" ,
				( ) =>
				{
					var json = @"{ ""active"": true }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsBoolean ( "active" ) == true;
				} );

			Run ( "Boolean: false literal" ,
				( ) =>
				{
					var json = @"{ ""active"": false }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsBoolean ( "active" ) == false;
				} );

			Run ( "Boolean: string 'true'" ,
				( ) =>
				{
					var json = @"{ ""active"": ""true"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsBoolean ( "active" ) == true;
				} );

			Run ( "Boolean: string 'yes' returns null" ,
				( ) =>
				{
					var json = @"{ ""active"": ""yes"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsBoolean ( "active" ) == null;
				} );

			// ------------------------------------------------------------
			//  INTEGER TESTS
			// ------------------------------------------------------------
			Run ( "Int: valid 32-bit integer" ,
				( ) =>
				{
					var json = @"{ ""age"": 42 }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsInt ( "age" ) == 42;
				} );

			Run ( "Int: out-of-range Int64 returns null" ,
				( ) =>
				{
					var json = @"{ ""age"": 999999999999 }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsInt ( "age" ) == null;
				} );

			Run ( "Int: string integer parses" ,
				( ) =>
				{
					var json = @"{ ""age"": ""123"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsInt ( "age" ) == 123;
				} );

			Run ( "Int: invalid string returns null" ,
				( ) =>
				{
					var json = @"{ ""age"": ""not-a-number"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsInt ( "age" ) == null;
				} );

			// ------------------------------------------------------------
			//  DOUBLE TESTS
			// ------------------------------------------------------------
			Run ( "Double: valid floating point" ,
				( ) =>
				{
					var json = @"{ ""value"": 3.14 }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsDouble ( "value" ) == 3.14;
				} );

			Run ( "Double: string floating point parses" ,
				( ) =>
				{
					var json = @"{ ""value"": ""2.718"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsDouble ( "value" ) == 2.718;
				} );

			Run ( "Double: invalid string returns null" ,
				( ) =>
				{
					var json = @"{ ""value"": ""NaNish"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsDouble ( "value" ) == null;
				} );

			// ------------------------------------------------------------
			//  GUID TESTS
			// ------------------------------------------------------------
			Run ( "Guid: valid GUID" ,
				( ) =>
				{
					var json = @"{ ""id"": ""c0a8012e-5f3c-4b8f-9b7a-2f4f0e9c1a12"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsGuid ( "id" ) ==
						   Guid.Parse ( "c0a8012e-5f3c-4b8f-9b7a-2f4f0e9c1a12" );
				} );

			Run ( "Guid: invalid GUID returns null" ,
				( ) =>
				{
					var json = @"{ ""id"": ""not-a-guid"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsGuid ( "id" ) == null;
				} );

			// ------------------------------------------------------------
			//  DATETIME TESTS
			// ------------------------------------------------------------
			Run ( "DateTime: valid ISO-8601" ,
				( ) =>
				{
					var json = @"{ ""created"": ""2024-10-12T14:30:00Z"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsDateTime ( "created" ) != null;
				} );

			Run ( "DateTime: invalid date returns null" ,
				( ) =>
				{
					var json = @"{ ""created"": ""not-a-date"" }";
					var w = new JSON_Deserialized_Object ( json );
					return w.GetFieldValueAsDateTime ( "created" ) == null;
				} );

			// ------------------------------------------------------------
			//  SUMMARY
			// ------------------------------------------------------------

			Console.WriteLine ( );
			Console.WriteLine ( "=== Test Summary ===" );

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine ( $"Total Tests:   {_tests,2}" );
			Console.ResetColor ( );

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ( $"Passed:        {_passed,2}" );
			Console.ResetColor ( );

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine ( $"Failed:        {_failed,2}" );
			Console.ResetColor ( );

			Console.WriteLine ( "{0}===================={0}" , Environment.NewLine );
			Exercise_JsonFieldValidator ( );
		}   // public void Execute

		private void Exercise_JsonFieldValidator ( )
		{
			// VT color helpers (explicit, intention-revealing)
			const string FG_GREEN = "\x1b[32m";
			const string FG_RED = "\x1b[31m";
			const string FG_CYAN = "\x1b[36m";
			const string FG_YELLOW = "\x1b[33m";
			const string RESET = "\x1b[0m";

			string [ ] astrRequiredProperties = new [ ]
			{
				"CallId",
				"Username",
				"AccountId",
				"UserId",
				"OrganizationId",
				"UserNumber",
				"Direction",
				"ContactNumber",
				"StartCallTime",
				"ExternalId",
				"Status",
				"EventName"
			};  // string [ ] astrRequiredProperties

			List<JsonValidatorTestCase> lstTests = new List<JsonValidatorTestCase>
			{
				new JsonValidatorTestCase
				{
					Name = "Valid JSON",
					ExpectedValid = ListInfo.LIST_IS_EMPTY,
					Json = @"
                    {
                      ""CallId"": ""1775218461.769610"",
                      ""Username"": ""test@m2c.com"",
                      ""AccountId"": ""4cea33cd-006e-4bc0-9b60-eb7a1584ab43"",
                      ""UserId"": ""114433"",
                      ""OrganizationId"": ""102233"",
                      ""UserNumber"": ""972539821111"",
                      ""Direction"": ""OUT"",
                      ""ContactNumber"": ""972537137777"",
                      ""StartCallTime"": ""2026-04-03T12:14:21"",
                      ""ExternalId"": ""someId1234"",
                      ""Status"": ""Ringing"",
                      ""EventName"": ""STARTCALL_OUT""
                    }"
				},
				new JsonValidatorTestCase
				{
					Name = "Missing Field (UserId)",
					ExpectedValid=ListInfo.EXACTLY_ONE_ITEM,
					Json = @"
                    {
                      ""CallId"": ""1775218461.769610"",
                      ""Username"": ""test@m2c.com"",
                      ""AccountId"": ""4cea33cd-006e-4bc0-9b60-eb7a1584ab43"",
                      ""OrganizationId"": ""102233"",
                      ""UserNumber"": ""972539821111"",
                      ""Direction"": ""OUT"",
                      ""ContactNumber"": ""972537137777"",
                      ""StartCallTime"": ""2026-04-03T12:14:21"",
                      ""ExternalId"": ""someId1234"",
                      ""Status"": ""Ringing"",
                      ""EventName"": ""STARTCALL_OUT""
                    }"
				},
				new JsonValidatorTestCase
				{
					Name = "Empty Value (Status)",
					ExpectedValid = ListInfo.EXACTLY_ONE_ITEM,
					Json = @"
                    {
                      ""CallId"": ""1775218461.769610"",
                      ""Username"": ""test@m2c.com"",
                      ""AccountId"": ""4cea33cd-006e-4bc0-9b60-eb7a1584ab43"",
                      ""UserId"": ""114433"",
                      ""OrganizationId"": ""102233"",
                      ""UserNumber"": ""972539821111"",
                      ""Direction"": ""OUT"",
                      ""ContactNumber"": ""972537137777"",
                      ""StartCallTime"": ""2026-04-03T12:14:21"",
                      ""ExternalId"": ""someId1234"",
                      ""Status"": """",
                      ""EventName"": ""STARTCALL_OUT""
                    }"
				},
				new JsonValidatorTestCase
				{
					Name = "Wrong Casing (username instead of Username)",
					ExpectedValid = ListInfo.LIST_IS_EMPTY ,   // Case-insensitive validator should accept it.
                    Json = @"
                    {
                      ""CallId"": ""1775218461.769610"",
                      ""username"": ""test@m2c.com"",
                      ""AccountId"": ""4cea33cd-006e-4bc0-9b60-eb7a1584ab43"",
                      ""UserId"": ""114433"",
                      ""OrganizationId"": ""102233"",
                      ""UserNumber"": ""972539821111"",
                      ""Direction"": ""OUT"",
                      ""ContactNumber"": ""972537137777"",
                      ""StartCallTime"": ""2026-04-03T12:14:21"",
                      ""ExternalId"": ""someId1234"",
                      ""Status"": ""Ringing"",
                      ""EventName"": ""STARTCALL_OUT""
                    }"
				}
			};

			int intValidationPass = 0;
			int intValidationFail = 0;
			int intCorrectnessPass = 0;
			int intCorrectnessFail = 0;

			foreach ( JsonValidatorTestCase test in lstTests )
			{
				Console.WriteLine ( $"{FG_CYAN}=== {test.Name} ==={RESET}" );

				JSON_Deserialized_Object jParsed = new JSON_Deserialized_Object ( test.Json , astrRequiredProperties );
				List<string> lstMissingItems = jParsed.Validate (true );

				// Existing PASS/FAIL (actual validation result)
				if ( lstMissingItems.Count == ListInfo.LIST_IS_EMPTY )
				{
					Console.WriteLine ( $"{FG_GREEN}VALIDATION: PASS{RESET}" );
					intValidationPass++;
				}   // TRUE (desired outcome) block, if ( missing.Count == ListInfo.LIST_IS_EMPTY )
				else
				{
					Console.WriteLine ( $"{FG_RED}VALIDATION: FAIL{RESET}" );
					intValidationFail++;

					foreach ( var f in lstMissingItems )
						Console.WriteLine ( $"{FG_YELLOW} - {f}{RESET}" );
				}   // FALSE (undesired outcome) block, if ( missing.Count == ListInfo.LIST_IS_EMPTY )

				// New correctness check
				if ( lstMissingItems.Count == test.ExpectedValid )
				{
					Console.WriteLine ( $"{FG_GREEN}CORRECTNESS: PASS{RESET}" );
					intCorrectnessPass++;
				}   // TRUE (anticipated outcome) block, if ( lstMissingItems.Count == test.ExpectedValid )
				else
				{
					Console.WriteLine ( $"{FG_RED}CORRECTNESS: FAIL (expected {test.ExpectedValid}){RESET}" );
					intCorrectnessFail++;
				}   // FALSE (unanticipated outcome) block, if ( lstMissingItems.Count == test.ExpectedValid )

				Console.WriteLine ( );
			}   // foreach ( TestCase test in lstTests )

			// Recap
			Console.WriteLine ( $"{FG_CYAN}=== TEST RECAP ==={RESET}" );
			Console.WriteLine ( $"{FG_CYAN}Total Test Cases: {lstTests.Count}{RESET}" );
			Console.WriteLine ( $"{FG_GREEN}Validation PASS:  {intValidationPass}{RESET}" );
			Console.WriteLine ( $"{FG_RED}Validation FAIL:  {intValidationFail}{RESET}" );
			Console.WriteLine ( $"{FG_GREEN}Correctness PASS: {intCorrectnessPass}{RESET}" );
			Console.WriteLine ( $"{FG_RED}Correctness FAIL: {intCorrectnessFail}{RESET}" );
		}   // private static void Exercise_JsonFieldValidator

		private class JsonValidatorTestCase
		{
			public string Name;
			public string Json;
			public int ExpectedValid;
		}   // private class JsonValidatorTestCase
	}   // internal class JsonFieldAccessorTests
}   // partial nsmespace DLLServices2TestStand