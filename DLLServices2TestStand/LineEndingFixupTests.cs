/*
    ============================================================================

    Namespace:			DLLServices2TestStand

    Class Name:			LineEndingFixupTests

	File Name:			LineEndingFixupTests.cs

    Synopsis:			Test the new line ending fixup extension methods.

    Author:				David A. Gray

	License:            Copyright (C) 2011-2019, David A. Gray.
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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Synopsis
    ---------- ------- --- -----------------------------------------------------
	2019/06/03 7.19    DAG Implement tests for the following string extension
                           methods:

                           UnixLineEndings      Replace CR/LF pairs and bare CRs
                                                with bare LFs.

                           WindowsLineEndings   Replace bare LFs with CR/LF
                                                pairs.

                           OldMacLineEndings    Replace CR/LF pairs and bare LFs
                                                with bare CRs.

	2019/06/04 7.19    DAG Add two extra sets of test cases to cover empty lines
                           and unterminated lines, along with a better label for
                           the test report.
    ============================================================================
*/


using System;
using WizardWrx;

namespace DLLServices2TestStand
{
    internal class LineEndingFixupTests
    {
        const string OUTCOME_REPORT_TEMPLATE = @"            Test Case {0} of {1}: Output String Name = {2}, Input Length = {3}, Output Length = {4}, Output Line Count = {5}, Outcome = {6}{7}";

        struct TestCase
        {
            public string InputString;
            public int LineCount;

            public TestCase ( string pstrInputString , int pintLineCount )
            {
                InputString = pstrInputString;
                LineCount = pintLineCount;
            }
        };  // struct TestCases

        static readonly TestCase [ ] s_astrTestStrings = new TestCase [ ]
        {   //             InputString                                                                                                                                                                                                                                                                                                                                                                                      LineCount
            //             ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   ---------
            new TestCase ( "Test line 1 is followed by a Unix newline.\nTest line 2 is also followed by a Unix newline.\nTest line 3 is followed by a Windows line break.\r\nTest line 4 is followed by the unusual line LF/CR line break.\n\rTest line 5 is followed by the old Macintosh line break, CR.\rTest line 6 is followed by a Unix newline.\nTest line 7 is followed by one last Unix newline.\n"              , 7         ) ,
            new TestCase ( "Test line 1 is followed by Windows newline.\r\nTest line 2 is also followed by a Windows newline.\r\nTest line 3 is followed by a Windows line break.\r\nTest line 4 is followed by the unusual line LF/CR line break.\n\rTest line 5 is followed by the old Macintosh line break, CR.\rTest line 6 is followed by a Unix newline.\nTest line 7 is followed by one last Windows newline.\r\n" , 7         ) ,
            new TestCase ( "Test line 1 is followed by a Windows newline.\r\nTest line 2 is also followed by a Windows newline.\r\nTest line 3 is followed by yet another Windows neline.\r\n"                                                                                                                                                                                                                            , 3         ) ,
            new TestCase ( "Test line 1 is followed by a Unix newline.\nTest line 2 is also followed by a Unix newline.\nTest line 3 is followed by yet another Unix neline.\n"                                                                                                                                                                                                                                           , 3         ) ,
            new TestCase ( "Test line 1 is followed by a Unix newline.\nTest line 2 is followed by 2 Unix newlines.\n\nTest line 4 is followed by one Unix newline.\nTest line 5 is unterminated."                                                                                                                                                                                                                        , 4         ) ,
            new TestCase ( "Test line 1 is followed by a Old Macintosh newline.\rTest line 2 is followed by 2 Old Macintosh newlines.\r\rTest line 4 is followed by one Old Macintosh newline.\rTest line 5 is unterminated."                                                                                                                                                                                             , 4         ) ,
            new TestCase ( "Test line 1 is followed by a Windows newline.\r\nTest line 2 is followed by 2 Windows newlines.\r\n\r\nTest line 4 is followed by one Windows newline.\r\nTest line 5 is unterminated."                                                                                                                                                                                                       , 4         ) ,
        };  // static readonly TestCase [ ] s_astrTestStrings

        static int s_intBadOutcomes = ListInfo.LIST_IS_EMPTY;
        static int s_intGoodOutcomes = ListInfo.LIST_IS_EMPTY;

        internal static int Exercise ( ref int pintTestNumber )
        {
            const int SCENARIO_COUNT = 3;

            NewClassTests_20140914.BeginTest (
                nameof ( StringExtensions ) ,
                ref pintTestNumber ,
                Program.BEGIN_TEST_TAKE_METHOD_NAME_AT_FACE_VALUE );

            int intInputCase = ListInfo.LIST_IS_EMPTY;
            int intOverallCase = ListInfo.LIST_IS_EMPTY;
            int intTotalTestCases = s_astrTestStrings.Length * SCENARIO_COUNT;

            WizardWrx.ConsoleStreams.MessageInColor micSuccessfulOutcomeMessage = new WizardWrx.ConsoleStreams.MessageInColor (
                Properties.Settings.Default.SuccessfulOutcomeMessageColor.EnumFromString<ConsoleColor> ( ) ,
                Properties.Settings.Default.SuccessfulOutcomeMessageBackgroundColor.EnumFromString<ConsoleColor> ( ) );

            WizardWrx.ConsoleStreams.MessageInColor micUnsuccessfulOutcomeMessage = new WizardWrx.ConsoleStreams.MessageInColor (
                WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor ,
                WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < s_astrTestStrings.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"        Input string test {0} of {1}: Input String Length = {2}, Input String:{4}{3}{4}" ,
                    new object [ ]
                    {
                            ++intInputCase ,                                    // Format Item 0: Input string test {0}
                            s_astrTestStrings.Length ,                          // Format Item 1: of {1}:
                            s_astrTestStrings [ intJ ].InputString.Length ,     // Format Item 2: Input String Length = {2}
                            s_astrTestStrings [ intJ ].InputString ,            // Format Item 3:  (s_astrTestStrings [ intJ ].InputString):{4}{3}
                            Environment.NewLine                                 // Format Item 4
                    } );

                string strFormattedForMac = s_astrTestStrings [ intJ ].InputString.OldMacLineEndings ( );
                string strFormattedForUnix = s_astrTestStrings [ intJ ].InputString.UnixLineEndings ( );
                string strFormattedForWindows = s_astrTestStrings [ intJ ].InputString.WindowsLineEndings ( );

                intOverallCase = ReportTestOutcome (
                    intOverallCase ,
                    intTotalTestCases ,
                    intJ ,
                    nameof ( strFormattedForMac ) ,
                    strFormattedForMac ,
                    SpecialCharacters.CARRIAGE_RETURN );
                intOverallCase = ReportTestOutcome (
                    intOverallCase ,
                    intTotalTestCases ,
                    intJ ,
                    nameof ( strFormattedForUnix ) ,
                    strFormattedForUnix ,
                    SpecialCharacters.LINEFEED );
                intOverallCase = ReportTestOutcome (
                    intOverallCase ,
                    intTotalTestCases ,
                    intJ ,
                    nameof ( strFormattedForWindows ) ,
                    strFormattedForWindows ,
                    SpecialStrings.STRING_SPLIT_NEWLINE );
            }   // for ( int intK = ArrayInfo.ARRAY_FIRST_ELEMENT ; intK < s_astrTestStrings.Length ; intK++ )

            Console.WriteLine (
                @"{1}    Test summary: Successful Outcome Count   = {0}" ,
                s_intGoodOutcomes ,
                Environment.NewLine );
            Console.WriteLine (
                @"                  Unsuccessful Outcome Count = {0}{1}" ,
                s_intBadOutcomes ,
                Environment.NewLine );

            if ( s_intBadOutcomes == ListInfo.LIST_IS_EMPTY )
            {
                micSuccessfulOutcomeMessage.WriteLine ( @"    A-OK!" );
            }   // TRUE (antiicpated outcome) block, if ( s_intBadOutcomes == ListInfo.LIST_IS_EMPTY )
            else
            {
                micUnsuccessfulOutcomeMessage.WriteLine ( @"    YIKES!" );
            }   // FALSE (unantiicpated outcome) block, if ( s_intBadOutcomes == ListInfo.LIST_IS_EMPTY )

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // internal static int Exercise


        private static int ReportTestOutcome (
            int pintOverallCase ,
            int pintTotalTestCases ,
            int pintK ,
            string pstrFormattedStringName ,
            string pstrFormattedString ,
            char pchrExpectedLineBreak )
        {
            int intActualLineCount = ActualLineCount (
                pstrFormattedString ,                                           // int pintExpectedLineCount
                pchrExpectedLineBreak );                                        // int pintActualLineCount

            if ( EvaluateOutcomeForCounting ( s_astrTestStrings [ pintK ].LineCount , intActualLineCount ) )
            {
                s_intGoodOutcomes++;
            }   // TRUE (anticipated outcome) block, if ( EvaluateOutcomeForCounting ( s_astrTestStrings [ pintK ].LineCount , intActualLineCount ) )
            else
            {
                s_intBadOutcomes++;
            }   // FALSE (unanticipated outcome) block, if ( EvaluateOutcomeForCounting ( s_astrTestStrings [ pintK ].LineCount , intActualLineCount ) )

            Console.WriteLine (
                OUTCOME_REPORT_TEMPLATE ,                                       // Format Control String
                new object [ ]
                {
                    ++pintOverallCase ,                                         // Format Item 0: Test Case {0}
                    pintTotalTestCases ,                                        // Format Item 1: of {1}: Input
                    pstrFormattedStringName ,                                   // Format Item 2: Output String Name = {2},
                    s_astrTestStrings [ pintK ].InputString.Length ,            // Format Item 2: Input Length = {3}
                    pstrFormattedString.Length ,                                // Format Item 3: Output Length = {4}
                    intActualLineCount ,                                        // Format Item 5: Output Line Count = {5}
                    EvaluateOutcomeForDisplay (                                 // Format Item 6: Outcome = {6}
                        s_astrTestStrings [ pintK ].LineCount ,                     // int pintExpectedLineCount
                        intActualLineCount ) ,                                      // int pintActualLineCount
                    Environment.NewLine                                         // Format Item 7: Platform-dependent newline.
                } );
            return pintOverallCase;
        }   // private static int ReportTestOutcome (1 of 2)


        private static int ReportTestOutcome (
            int pintOverallCase ,
            int pintTotalTestCases ,
            int pintK ,
            string pstrFormattedStringName ,
            string pstrFormattedString ,
            string pstrExpectedLineBreak )
        {
            int intActualLineCount = ActualLineCount (
                pstrFormattedString ,                                           // int pintExpectedLineCount
                pstrExpectedLineBreak );                                        // int pintActualLineCount

            if ( EvaluateOutcomeForCounting ( s_astrTestStrings [ pintK ].LineCount , intActualLineCount ) )
            {
                s_intGoodOutcomes++;
            }   // TRUE (anticipated outcome) block, if ( EvaluateOutcomeForCounting ( s_astrTestStrings [ pintK ].LineCount , intActualLineCount ) )
            else
            {
                s_intBadOutcomes++;
            }   // FALSE (unanticipated outcome) block, if ( EvaluateOutcomeForCounting ( s_astrTestStrings [ pintK ].LineCount , intActualLineCount ) )

            Console.WriteLine (
                OUTCOME_REPORT_TEMPLATE ,                                       // Format Control String
                new object [ ]
                {
                    ++pintOverallCase ,                                         // Format Item 0: Test Case {0}
                    pintTotalTestCases ,                                        // Format Item 1: of {1}: Input
                    pstrFormattedStringName ,                                   // Format Item 2: Output String Name = {2},
                    s_astrTestStrings [ pintK ].InputString.Length ,            // Format Item 3: Input Length = {3}
                    pstrFormattedString.Length ,                                // Format Item 4: Output Length = {4}
                    intActualLineCount ,                                        // Format Item 5: Output Line Count = {5}
                    EvaluateOutcomeForDisplay (                                 // Format Item 6: Outcome = {6}
                        s_astrTestStrings [ pintK ].LineCount ,                     // int pintExpectedLineCount
                        intActualLineCount ) ,                                      // int pintActualLineCount
                    Environment.NewLine                                         // Format Item 7: Platform-dependent newline.
                } );
            return pintOverallCase;
        }   // private static int ReportTestOutcome (2 of 2)


        private static bool EvaluateOutcomeForCounting (
            int pintExpectedLineCount ,
            int pintActualLineCount )
        {
            return pintExpectedLineCount == pintActualLineCount;
        }   // private static bool EvaluateOutcomeForCounting


        private static string EvaluateOutcomeForDisplay (
            int pintExpectedLineCount ,
            int pintActualLineCount )
        {
            return pintExpectedLineCount == pintActualLineCount ? @"OK" : @"ERROR";
        }   // private static string Evaluate,Outcome


        private static int ActualLineCount (
            string pstrFormattedString ,
            string pstrExpectedLineBreak )
        {
            return pstrFormattedString.CountSubstrings ( pstrExpectedLineBreak );
        }   // private static int ActualLineCount (1 of 2)


        private static int ActualLineCount (
            string pstrFormattedString ,
            char pchrExpectedLineBreak )
        {
            return pstrFormattedString.CountCharacterOccurrences ( pchrExpectedLineBreak );
        }   // private static int ActualLineCount (2 of 2)
    }   // internal class LineEndingFixupTests
}   // partial namespace DLLServices2TestStand