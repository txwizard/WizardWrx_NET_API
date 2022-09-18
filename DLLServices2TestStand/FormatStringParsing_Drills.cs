/*
    ============================================================================

    Namespace:          DLLServices2TestStand     

    Class Name:         FormatStringParsing_Drills

    File Name:          FormatStringParsing_Drills.cs

    Synopsis:           Exercise the FormatStringParser class, exported by the
                        FormatStringEngine class library, which must surely soon
                        have a more appropriate name.

    Remarks:            This project started as an ostensibly simple test, to
                        bounds check the inputs to a function for replacing a
                        plain vanilla format item with a composite item that is
                        generated at run time.

    License:            Copyright (C) 2014-2022, David A. Gray. 
						All rights reserved.

                        Redistribution and use in source and binary forms, with
                        or without modification, are permitted provided that the
                        following conditions are met:

                        *   Redistributions of source code must retain the above
                            copyright notice, this list of conditions and the
                            following disclaimer.

                        *   Redistributions in binary form must reproduce the
                            above copyright notice, this list of conditions and
                            the following disclaimer in the documentation and/or
                            other materials provided with the distribution.

                        *   Neither the name of David A. Gray, nor the names of
                            his contributors may be used to endorse or promote
                            products derived from this software without specific
                            prior written permission.

                        THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
                        CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
                        WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
                        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
                        PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
                        David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
                        INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
                        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
                        SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
                        PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
                        ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
                        LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
                        ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
                        IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    Created:            Sunday, 13 July 2014 - Saturday, 19 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version  Author Description
    ---------- -------- ------ --------------------------------------------------
    2014/07/19 2.5      DAG    Initial implementation.

	2014/09/23 2.8      DAG    Add a couple of tests for AdjustToMaximumWidth to
                               cover the revisions made in version 1.2.

	2016/06/12 3.0      DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                               correct misspelled words flagged by the spelling
                               checker add-in, and incorporate my three-clause
                               BSD license, and document why I suppressed
                               warning CS0618. Please see item 2 in Remarks
                               (above).

                               Since there is one case that is expected to throw
                               an exception, equip TestFormatItemCounters with a
                               try/catch block that intercepts and reports the
                               error, taking advantage of new features in the
                               exception logger to write two copies if Standard
                               Output is redirected.

    2017/08/26 7.0      DAG    Move the classes exercised by this class from 
                               WizardWrx.SharedUtl4.dll into WizardWrx.Core.dll,
                               and bring this class into DllServices2TestStand,
                               and mark it as internal static.

	2017/09/07 7.0      DAG    Implement ItemDisplayOrder as a signe integer.

	2018/10/07 7.1      DAG    Exercise static method DisplayCharacterInfo on the
	                           ASCIICharacterDisplayInfo class BEFORE singleton
							   ASCII_Character_Display_Table is referenced.

	2021/02/06 8.0      DAG    Cover the improved ASCII table display, which has
                               many new attributes.

	2022/07/10 9.0.1536 DAG    Implement Unicode code points and a new tab
                               delimited reference table.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.IO;

using WizardWrx;
using WizardWrx.FormatStringEngine;


namespace DLLServices2TestStand
{
	internal static class FormatStringParsing_Drills
	{
		static readonly string [ ] s_astrFormatStringSamples =
        {
            @"This is the degenerate case; it is devoid of format items." ,                                                              // String 1
            @"This string contains one {0} format item." ,                                                                               // String 2
            @"This string contains two {0} format {1} item." ,                                                                           // String 3
            @"{0}This string contains three {2} format {1} item." ,                                                                      // String 4
            @"{0}This string contains four {2} format {10} item, one {1} of which has a high index." ,                                   // String 5
            @"{0}This string contains four and 1/2 {0) {2} format {10} item, one {1} of which has a high index." ,                       // String 6
            @"{0}This string contains four {2} format {7} item, one {1} of which {{9}} has a high index, and an escaped format item." ,  // String 7
            @"        String {0,2:N0}: {1}{4}                   Balanced:      {2}{6}                   Highest Index: {3}{4}" ,         // String 8: This is PROGRESS_MSG, taken from this class.
            @"        String {5,2:N0}: {1}{4}                   Balanced:      {2}{4}                   Highest Index: {3}{4}" ,         // String 9: This is PROGRESS_MSG, modified to assign the highest index to the composite format item.
        };  // s_astrFormatStringSamples

		static readonly string [ ] s_astrLabelsOfVariousLengths =
        {
			@"Label 1" ,
			@"Label 2" ,
			@"Label 2a" ,
			@"Label 3"
		};  // s_astrLabelsOfVariousLengths

		static readonly string [ ] s_astrValuseOfIrrelevantLengths =
        {
			@"Value 1" ,
			@"Value 2" ,
			@"Value 2a" ,
			@"Value 3"
		};  // s_astrValuseOfIrrelevantLengths

		const int ARRAY_FIRST_ELEMENT = 0;
		const int ARRAY_LIST_ORDINAL_TO_SUBSCRIPT = +1;


		internal static void ASCII_Table_Gen ( )
		{
			StreamWriter swAsciiTableLists = null;
			StreamWriter swAsciiReferenceTable = null;

			try
			{
				swAsciiTableLists = GetAsciiTableListFileHandle ( );
				swAsciiReferenceTable = GetAsciiTableListFileHandle ( Properties.Settings.Default.ASCII_Table_Reference_File_Name );

				Console.WriteLine (
					Properties.Resources.IDS_ASCII_TABLE_PREAMBLE ,             // Print this message above both tables.
					Environment.NewLine );                                      // Format Item 0 = Embedded Newline
				swAsciiTableLists.WriteLine (
					Properties.Resources.IDS_ASCII_TABLE_PREAMBLE ,             // Print this message above both tables.
					Environment.NewLine );                                      // Format Item 0 = Embedded Newline

				Console.WriteLine (
					Properties.Resources.IDS_ASCII_TABLE_CHARACTER_PROPERTIES , // Print this message above the first table.
					Environment.NewLine );                                      // Format Item 0 = Embedded Newline
				swAsciiTableLists.WriteLine (
					Properties.Resources.IDS_ASCII_TABLE_CHARACTER_PROPERTIES , // Print this message above the first table.
					Environment.NewLine );                                      // Format Item 0 = Embedded Newline

				string strDetailFormatTemplate = Properties.Resources.IDS_ASCII_CHARACTER_INFO;
				string strDisplayInfoTemplate = Properties.Resources.MSG_ASCII_TABLE_DETAIL;
				string strReferenceTableTemplate = Properties.Resources.IDS_ASCII_TABLE_LABEL_ROW;
				swAsciiReferenceTable.WriteLine (
					strReferenceTableTemplate ,
					SpecialCharacters.TAB_CHAR );

				strReferenceTableTemplate = strReferenceTableTemplate.Replace ( @"Decimal" , @"{1}" ).Replace ( @"Hexadecimal" , @"{2}" ).Replace ( @"HTML Entity" , @"{3}" ).Replace ( @"URL Encoding" , @"{4}" ).Replace ( @"Unicode Code Point" , @"{5}" ).Replace ( @"Display Value" , @"{6}" ).Replace ( @"Comment" , @"{7}" );

				try
				{
					ASCII_Character_Display_Table aSCII_Character_Display_Table = ASCII_Character_Display_Table.GetTheSingleInstance ( );

					for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
							  intJ < aSCII_Character_Display_Table.AllASCIICharacters.Length ;
							  intJ++ )
					{
						ASCIICharacterDisplayInfo displayInfo = aSCII_Character_Display_Table.AllASCIICharacters [ intJ ];
						Console.WriteLine (
							strDisplayInfoTemplate ,							// Format Control String
							intJ ,                                              // Format Item 0: Character {0:-3}: Numerical
							displayInfo.CodeAsDecimal ,                         // Format Item 1: Numerical Value = {1,3} (
							displayInfo.CodeAsHexadecimal ,                     // Format Item 2: ({2} hex),
							displayInfo.DisplayText ,                           // Format Item 3: Display Value = {3}
							displayInfo.HTMLName );                             // Format Item 4: HTML Entity = {4}
						swAsciiTableLists.WriteLine (
							strDisplayInfoTemplate ,                            // Format Control String
							intJ ,                                              // Format Item 0: Character {0:-3}: Numerical
							displayInfo.CodeAsDecimal ,                         // Format Item 1: Numerical Value = {1,3} (
							displayInfo.CodeAsHexadecimal ,                     // Format Item 2: ({2} hex),
							displayInfo.DisplayText ,                           // Format Item 3: Display Value = {3}
							displayInfo.HTMLName );                             // Format Item 4: HTML Entity = {4}
						swAsciiReferenceTable.WriteLine (
							strReferenceTableTemplate ,                         // Format Control String
							SpecialCharacters.TAB_CHAR ,                        // Format Item 0: Field delimiter
							displayInfo.CodeAsDecimal ,                         // Format Item 1: Decimal
							displayInfo.CodeAsHexadecimal ,                     // Format Item 2: Hexadecimal
							displayInfo.HTMLName ,                              // Format Item 3: HTML Entity
							displayInfo.URLEncoding ,                           // Format Item 4: URL Encoding
							displayInfo.UnicodeCodePoint ,                      // Format Item 5: Unicode Code Point
							displayInfo.DisplayText ,                           // Format Item 6: Display Value
							displayInfo.Comment );								// Format Item 7: Comment
					}   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < aSCII_Character_Display_Table.AllASCIICharacters.Length ; intJ++ )
				}
				catch ( Exception exAll )
				{
					Program.s_smTheApp.AppExceptionLogger.ReportException ( exAll );
				}

				Console.WriteLine (
					Properties.Resources.IDS_ASCII_TABLE_ENUMERATION ,          // Print this message between the two tables.
					Environment.NewLine );                                      // Format Item 0 = Embedded Newline
				swAsciiTableLists.WriteLine (
					Properties.Resources.IDS_ASCII_TABLE_ENUMERATION ,          // Print this message between the two tables.
					Environment.NewLine );                                      // Format Item 0 = Embedded Newline

				ASCII_Character_Display_Table asciiShowMan = ASCII_Character_Display_Table.GetTheSingleInstance ( );
				strDetailFormatTemplate = Properties.Resources.IDS_ASCII_TABLE_ITEM;

				foreach ( ASCIICharacterDisplayInfo asciiInfo
						  in asciiShowMan.AllASCIICharacters )
				{
					Console.WriteLine (
						strDetailFormatTemplate ,                               // Format control string, read above the loop
						new object [ ]
						{
							asciiInfo.CodeAsDecimal ,							// Format Item 0: Character {0} (
							asciiInfo.CodeAsHexadecimal ,						// Format Item 1: ({1}): Display
							asciiInfo.URLEncoding ,								// Format Item 2: URL Encoding = {2}
							asciiInfo.DisplayText ,								// Format Item 3: Display Value = {3}
							string.IsNullOrEmpty ( asciiInfo.Comment )			// Format Item 4: Comment, if present, else nothing
								? string.Empty									//		Since there is no comment, show nothing.
								: string.Concat (								//		Comments get wrapped in parentheses and separated by a space.
									SpecialCharacters.SPACE_CHAR ,				//		Leading space 
									SpecialCharacters.PARENTHESIS_LEFT ,		//      and left parenthesis
									asciiInfo.Comment ,							//		Comment from document
									SpecialCharacters.PARENTHESIS_RIGHT )       //		Right parenthesis
						} );
					swAsciiTableLists.WriteLine (
						strDetailFormatTemplate ,                               // Format control string, read above the loop
						new object [ ]
						{
							asciiInfo.CodeAsDecimal ,							// Format Item 0: Character {0} (
							asciiInfo.CodeAsHexadecimal ,						// Format Item 1: ({1}): Display
							asciiInfo.URLEncoding ,								// Format Item 2: URL Encoding = {2}
							asciiInfo.DisplayText ,								// Format Item 3: Display Value = {3}
							string.IsNullOrEmpty ( asciiInfo.Comment )			// Format Item 4: Comment, if present, else nothing
								? string.Empty									//		Since there is no comment, show nothing.
								: string.Concat (								//		Comments get wrapped in parentheses and separated by a space.
									SpecialCharacters.SPACE_CHAR ,				//		Leading space 
									SpecialCharacters.PARENTHESIS_LEFT ,		//      and left parenthesis
									asciiInfo.Comment ,							//		Comment from document
									SpecialCharacters.PARENTHESIS_RIGHT )       //		Right parenthesis
						} );
				}   // foreach ( ASCIICharacterDisplayInfo asciiInfo in asciiShowMan.AllASCIICharacters )

				swAsciiTableLists.Close ( );
				swAsciiTableLists.Dispose ( );

				swAsciiReferenceTable.Close ( );
				swAsciiReferenceTable.Dispose ( );
			}
			catch ( Exception exAll )
			{
				Program.s_smTheApp.AppExceptionLogger.ReportException ( exAll );
			}
        }   // ASCII_Table_Gen


        private static StreamWriter GetAsciiTableListFileHandle ( string pstrAlternameSettingsKeyName = null )
        {
			string strAsciiTestFileName = Program.AbsolutePathStringFromSettings (
				pstrAlternameSettingsKeyName == null                            // string		pstrRawStringValue	- Tokenized string to process
				? Properties.Settings.Default.ASCII_Table_Listings				// Default
				: pstrAlternameSettingsKeyName );								// Override
			Console.WriteLine (
				Properties.Resources.MSG_ASCII_TABLES_REPORT ,					// Format Control String
				strAsciiTestFileName ,                                          // Format Item 0: The ASCII Table details are reproduced as a separate text file, {0}
				Environment.NewLine );                                          // Format Item 1: {1}The ASCII AND .{1}
			return new StreamWriter (
				strAsciiTestFileName ,                                          // path			String				- The complete file path to write to.
				FileIOFlags.FILE_OUT_CREATE ,                                   // append		Boolean				- true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.
				System.Text.Encoding.Unicode ,                                  // encoding		Encoding			- The character encoding to use.
				MagicNumbers.CAPACITY_08KB );                                   // bufferSize	Int32				- The buffer size, in bytes.
		}   // private static StreamWriter GetAsciiTableListFileHandle


		internal static void TestFormatItemBuilders ( )
		{
			const string ERRMSG_UNEQUAL_ARRAYS = @"An internal error has occurred. Arrays s_astrLabelsOfVariousLengths and s_astrValuseOfIrrelevantLengths are of unequal length.{2}s_astrLabelsOfVariousLengths Length    = {0}{2}s_astrValuseOfIrrelevantLengths Length = {1}";
			const string MSG_PROLOGUE = @"{1}Exercising routine AdjustToMaximumWidth: Item Count = {0}{1}";
			const string MSG_REPORT_LINE = @"    Item {0}: {1} = {2}";
			const int REPORT_LABEL_ITEM_INDEX = 1;
			const string REPORT_SUMMARY = @"{0}AdjustToMaximumWidth exercises completed!{0}";

			int intNLabels = s_astrLabelsOfVariousLengths.Length;

			if ( intNLabels == s_astrValuseOfIrrelevantLengths.Length )
			{
				Console.WriteLine ( MSG_PROLOGUE ,                              // Format template string
					intNLabels ,                                                // Format Item 0 = Item count
					Environment.NewLine );                                      // Format Item 1 = Newline, my way
				string strDynamicFormat = FormatItem.UpgradeFormatItem (
					MSG_REPORT_LINE ,                                           // string     pstrFormat       = Prototype format template string
					REPORT_LABEL_ITEM_INDEX ,                                   // int        pintItemIndex    = Index (zero based) of format item to upgrade
					FormatItem.Alignment.Left ,                                 // Alignment  penmAlignment    = Left or Right
					null ,                                                      // string     pstrFormatString = Omit
					s_astrLabelsOfVariousLengths );                             // string [ ] pastrValueArray  = Must fit all of these.

				for ( int intItem = ARRAY_FIRST_ELEMENT ; intItem < intNLabels ; intItem++ )
				{
					Console.WriteLine (
						strDynamicFormat ,                                      // Dynamically generated Format template string
						intItem + ARRAY_LIST_ORDINAL_TO_SUBSCRIPT ,             // Format Item 0 = Item number (1 based)
						s_astrLabelsOfVariousLengths [ intItem ] ,              // Format Item 1 = Label
						s_astrValuseOfIrrelevantLengths [ intItem ] );          // Format Item 1 = Value
				}   // for ( int intItem = ARRAY_FIRST_ELEMENT ; intItem < intNLabels ; intItem++ )

				Console.WriteLine (
					REPORT_SUMMARY ,
					Environment.NewLine );
			}   // TRUE (expected outcome) block, if ( intNLabels == s_astrValuseOfIrrelevantLengths.Length )
			else
			{
				throw new InvalidOperationException (
					string.Format (
						ERRMSG_UNEQUAL_ARRAYS ,                                 // Format template string
						s_astrLabelsOfVariousLengths.Length ,                   // Format Item 0 = Length of s_astrLabelsOfVariousLengths array
						s_astrValuseOfIrrelevantLengths.Length ,                // Format Item 1 = Length of s_astrValuseOfIrrelevantLengths array
						Environment.NewLine ) );                                // Format Item 2 = Newline, my way
			}   // FALSE (UNexpected outcome) block, if ( intNLabels == s_astrValuseOfIrrelevantLengths.Length )
		}   // TestFormatItemBuilders


		internal static void TestFormatItemCounters ( )
		{
			const string ANNOUNCE_BEGIN = @"{1}    Format strings to test = {0}{1}";
			const string ANNOUNCE_DONE = @"{1}    {0} format strings have been tested.";
			const string PROGRESS_MSG = @"        String {0,2:N0}: {1}{3}                   Highest Index: {2}{3}";
			const string FORMAT_STRING_ERRORS_PROLOGUE = @"{0} error found in format string: {1}{2}";

			int intNStrings = s_astrFormatStringSamples.Length;

			int [ ] aintHightesItemIndex = new int [ intNStrings ];

			ASCII_Table_Gen ( );

			Console.WriteLine (
				ANNOUNCE_BEGIN ,
				intNStrings ,
				Environment.NewLine );

			try
			{
				for ( int intCurrStr = ARRAY_FIRST_ELEMENT ;
						  intCurrStr < intNStrings ;
						  intCurrStr++ )
				{
					int intStringNumber = intCurrStr
										  + ARRAY_LIST_ORDINAL_TO_SUBSCRIPT;

					FormatStringParser fsp = new FormatStringParser ( s_astrFormatStringSamples [ intCurrStr ] );
					aintHightesItemIndex [ intCurrStr ] = fsp.HighestFormatItemIndex;

					Console.WriteLine (
						PROGRESS_MSG ,
						new object [ ]
						{
							intStringNumber ,									// Format Item 0
							s_astrFormatStringSamples [ intCurrStr ] ,			// Format Item 1
							aintHightesItemIndex [ intCurrStr ] ,				// Format Item 2
							Environment.NewLine									// Format Item 3
						} );

					if ( fsp.FormatStringErrorCount > FormatStringParser.NO_ERRORS )
					{
						Console.WriteLine (
							FORMAT_STRING_ERRORS_PROLOGUE ,						// Message template string
							fsp.FormatStringErrorCount ,						// Format Item 0 = Error count
							StringTricks.AdjustNumberOfNoun (					// Format Item 1 = Aligned error count
								( uint ) fsp.FormatStringErrorCount ,           //		puintNumber
								Properties.Resources.ERRMSG_LITERAL_ERROR ,     //		pstrNounSingular
								null ,                                          //		pstrPluralForm
								fsp.FormatString ) ,                            //		pstrPhrase
							Environment.NewLine );								// Format Item 2 = Embedded Newline
						ListAllErrors ( fsp.FormatStringErrors );
						Console.WriteLine ( );
					}   // if ( fsp.FormatStringErrorCount > FormatStringParser.NO_ERRORS )
				}   // for ( int intCurrStr = ARRAY_FIRST_ELEMENT ; intCurrStr < intNStrings ; intCurrStr++ )
			}
			catch ( Exception exAll )
			{	// Preserve this expected exception report in the standard output, in addition to sending it to Standard Error.
				Program.s_smTheApp.AppExceptionLogger.ReportException ( exAll );
			}

			//	----------------------------------------------------------------
			//	After adding another format item into which to have WriteLine
			//	stuff the newline, I searched down here, only to find that the
			//	argument list already had one. The lesson from this is that you
			//	can have extra format items in the list, but the list must have
			//	at least enough format items to account for the highest index in
			//	the format control string.
			//	----------------------------------------------------------------

			Console.WriteLine (
				ANNOUNCE_DONE ,
				intNStrings ,
				Environment.NewLine );
		}   // TestFormatItemCounters


		private static void ListAllErrors ( List<FormatStringError> plstAllErrors )
		{
			const int ORDINAL_FROM_SUBSCRIPT = ArrayInfo.INDEX_FROM_ORDINAL;
			const int POS_LABEL = ArrayInfo.ARRAY_FIRST_ELEMENT;
			const int POS_VALUE = ArrayInfo.ARRAY_SECOND_ELEMENT;
			const char DELIMITER = FormatStringError.LABEL_VALUE_DELIMITER;

			ReportDetail.DetailFormat = "{3}    {0} = {1}";

			int intErrorCount = plstAllErrors.Count;

			for ( int intI = ArrayInfo.ARRAY_FIRST_ELEMENT ;
					  intI < intErrorCount ;
					  intI++ )
			{
				FormatStringError fse = plstAllErrors [ intI ];
				string [ ] astrErrorDetails = fse.Split ( );
				int intNDetails = astrErrorDetails.Length;						// Save a trip into the collection.
				ReportDetails rptDtlsColl = new ReportDetails ( intNDetails );

				string [ ] astrErrorSummaryFirst = new string [ ]
                { 
                    string.Format (
                        Properties.Resources.MSG_FORMAT_ERROR_DETAILS ,			// Format template string
                        FormatItem.XofY (										// Format Item 0
                            intI + ORDINAL_FROM_SUBSCRIPT ,						//		X = Item Number
                            intErrorCount ) )									//		Y = Item Count
                };  // string [ ] astrErrorSummaryFirst

				//  ------------------------------------------------------------
				//  Maybe there is a better way to accompish this. Nevertheless,
				//  this method works for generating a completely blank string
				//  of a length that must be determined at runtime.
				//  ------------------------------------------------------------

				string [ ] astrErrorSummaryAllOthers = new string [ ]
                {
                    string.Empty.PadRight ( astrErrorSummaryFirst [ ArrayInfo.ARRAY_FIRST_ELEMENT ].Length )
                };  // string [ ] astrErrorSummaryAllOthers

				for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNDetails ; intJ++ )
				{
					string [ ] astrLabelAndValue = astrErrorDetails [ intJ ].Split ( DELIMITER );
					rptDtlsColl.Add (
						new ReportDetail (
							astrLabelAndValue [ POS_LABEL ].Trim ( ) ,								// Label (Format Item = 0)
							astrLabelAndValue [ POS_VALUE ].Trim ( ) ,								// Value (Format Item = 1) as String
							( ReportDetail.ItemDisplayOrder ) ArrayInfo.OrdinalFromIndex ( intJ ) ,	// Item Number (Format Item = 2) - The arithmetic operation casts it to int (signed), so it must be forcibly cast back to unsigned int.
							null ,																	// Individual Format String (Null = Use Default from static property)
							( intJ == ArrayInfo.ARRAY_FIRST_ELEMENT )								// Additional Details - Condition
								? astrErrorSummaryFirst												//		Value if Condition is True
								: astrErrorSummaryAllOthers ) );									//		Value if Condition is False
				}   // for ( uint uintJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; uintJ < intNDetails ; uintJ++ )

				rptDtlsColl.ListAllItems ( );
			}   // for ( int intI = ArrayInfo.ARRAY_FIRST_ELEMENT ; intI < intErrorCount ; intI++ )
		}   // ListAllErrors
	}   // class FormatStringParsing_Drills
}   // partial namespace DLLServices2TestStand     