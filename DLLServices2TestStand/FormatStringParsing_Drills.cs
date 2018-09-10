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

    License:            Copyright (C) 2014-2017, David A. Gray. 
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

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/07/19 2.5     DAG    Initial implementation.

	2014/09/23 2.8     DAG    Add a couple of tests for AdjustToMaximumWidth, to
                              cover the revisions made in version 1.2.

	2016/06/12 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license, and document why I suppressed warning
                              CS0618. Please see item 2 in Remarks (above).

                              Since there is one case that is expected to throw
                              an exception, equip TestFormatItemCounters with a
                              try/catch block that intercepts and reports the
                              error, taking advantage of new features in the
                              exception logger to write two copies if Standard
                              Output is redirected.

    2017/08/26 7.0     DAG    Move the classes exercised by this class from 
                              WizardWrx.SharedUtl4.dll into WizardWrx.Core.dll,
                              and bring this class into DllServices2TestStand,
                              and mark it as internal static.

	2017/09/07 7.0     DAG    Implement ItemDisplayOrder as a signe integer.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using WizardWrx;
using WizardWrx.FormatStringEngine;


namespace DLLServices2TestStand
{
	internal static class FormatStringParsing_Drills
	{
		static readonly string [ ] s_astrFormatStringSamples =
        {
            "This is the degenerate case; it is devoid of format items." ,                                                              // String 1
            "This string contains one {0} format item." ,                                                                               // String 2
            "This string contains two {0} format {1} item." ,                                                                           // String 3
            "{0}This string contains three {2} format {1} item." ,                                                                      // String 4
            "{0}This string contains four {2} format {10} item, one {1} of which has a high index." ,                                   // String 5
            "{0}This string contains four and 1/2 {0) {2} format {10} item, one {1} of which has a high index." ,                       // String 6
            "{0}This string contains four {2} format {7} item, one {1} of which {{9}} has a high index, and an escaped format item." ,  // String 7
            "        String {0,2:N0}: {1}{4}                   Balanced:      {2}{6}                   Highest Index: {3}{4}" ,         // String 8: This is PROGRESS_MSG, taken from this class.
            "        String {5,2:N0}: {1}{4}                   Balanced:      {2}{4}                   Highest Index: {3}{4}" ,         // String 9: This is PROGRESS_MSG, modified to assign the highest index to the composite format item.
        };  // s_astrFormatStringSamples

		static readonly string [ ] s_astrLabelsOfVariousLengths =
        {
            "Label 1" ,
            "Label 2" ,
            "Label 2a" ,
            "Label 3"
        };  // s_astrLabelsOfVariousLengths

		static readonly string [ ] s_astrValuseOfIrrelevantLengths =
        {
            "Value 1" ,
            "Value 2" ,
            "Value 2a" ,
            "Value 3"
        };  // s_astrValuseOfIrrelevantLengths

		const int ARRAY_FIRST_ELEMENT = 0;
		const int ARRAY_INVALID_INDEX = -1;
		const int ARRAY_LIST_ORDINAL_TO_SUBSCRIPT = +1;
		const int JUST_ONE = +1;
		const int NO_FORMAT_ITEMS = 0;
		const int REGEXP_FIRST_MATCH = 0;										// The first member of the Groups collection is the entire match.
		const int REGEXP_FIRST_SUBMATCH = 1;									// Subexpressions follow the match, and are numbered as they are in Perl.
		const int SUBSTR_2ND_CHAR = +1;
		const int SUBSTR_ALL_BUT_TWO = +2;

		const int COMPLETE_FORMAT_ITEM = REGEXP_FIRST_MATCH;					// This has no direct analogue in Perl.
		const int FORMAT_ITEM_INDEX = REGEXP_FIRST_SUBMATCH;					// As in Perl, sub-matches are numbered from 1.
		const int WIDTH_AND_ALIGNMENT = 2;
		const int FORMAT_STRING = 6;


		internal static void ASCII_Table_Gen ( )
		{
			Console.WriteLine (
				Properties.Resources.IDS_ASCII_TABLE_PREAMBLE ,					// Print this message above the table.
				Environment.NewLine );											// Format Item 0 = Embedded Newline
			ASCII_Character_Display_Table asciiShowMan = ASCII_Character_Display_Table.GetTheSingleInstance ( );
			string strDetailFormatTemplate = Properties.Resources.IDS_ASCII_TABLE_ITEM;
			foreach ( ASCIICharacterDisplayInfo asciiInfo in asciiShowMan.AllASCIICharacters )
				Console.WriteLine (
					strDetailFormatTemplate ,									// Format control string, read above the loop
					new object [ ]
					{
						asciiInfo.CodeAsDecimal ,								// Format Item 0 = Decimal code of character
						asciiInfo.CodeAsHexadecimal ,							// Format Item 1 = Hexadecimal representation of character code
						asciiInfo.DisplayText ,									// Format Item 2 = Character as displayed, or substitute for characters that won't print anything usable
						string.IsNullOrEmpty ( asciiInfo.Comment )				// Format Item 3 = Comment, if present, else nothing			
							? string.Empty										//		Since there is no comment, show nothing.
							: string.Concat (									//		Comments get wrapped in parentheses, and separated by a space.
								" (" ,											//		Leading space and left parenthesis
								asciiInfo.Comment ,								//		Comment from XML document
								")" )											//		Right parenthesis
					} );
		}	// ASCII_Table_Gen


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
                    intStringNumber ,											// Format Item 0
                    s_astrFormatStringSamples [ intCurrStr ] ,					// Format Item 1
                    aintHightesItemIndex [ intCurrStr ] ,						// Format Item 2
                    Environment.NewLine											// Format Item 3
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