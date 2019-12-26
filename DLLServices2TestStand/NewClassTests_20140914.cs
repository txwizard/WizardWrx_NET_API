// #define STRINGS_TO_CONSOLE				// Defined the classic C/C++ way, their effect is confined to one module.

/*
    ============================================================================

    Namespace:          DLLServices2TestStand

    Class Name:         NewClassTests_20140914

    File Name:          NewClassTests_20140914.cs

    Synopsis:           This static class exercises the new classes by listing
                        the constants defined by each, and exercising each of
                        their service methods.

    Remarks:            Each class has a corresponding static method, which
                        returns an integer status code of zero unless there is a
                        reason to shut down the program.

    Author:             David A. Gray

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

    Created:            Sunday, 14 September 2014 and Monday, 15 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/15 5.2     DAG    Initial implementation.

    2015/06/21 5.5     DAG    Move to WizardWrx.DLLServices2TestStand.exe, to 
                              which the associated classes moved, extend to test
                              the loop index state evaluators and the
                              implementation in C# of the Unless idiom that I
                              borrowed from the Perl scripting language, and put
                              under my 3-clause BSD license.

    2015/08/31 5.6     DAG    Add a couple of overlooked special characters and
                              strings, along with a new method, ChopChop, to
                              test the new Chop method.

    2016/04/05 6.0     DAG    Add overlooked special characters to the listing.

    2016/06/04 6.2     DAG    Add the two new stream I/O flags and the routine
                              to put the new hex digit formatter through its
                              paces. IntegerToHexStrExercises is the name of the
                              new test routine.

    2017/03/16 7.0     DAG    Adjust for the breakup of WizardWrx.DllServices2,
                              and add ExerciseUtf8ResourceReader, to exercise
                              the new UTF-8 embedded resource reader.

                              Add a mapping for the 16 numeral hexadecimal long
                              integer format string.

    2017/06/24 7.0     DAG    Enumerate the MagicNumbers constants the hard way.

    2017/07/17 7.0     DAG    1) Replace references to string.empty, which isn't
                                 a constant, with SpecialString.s.EMPTY_STRING,
                                 which is one.

                              2) Add the three new characters (BRACE_LEFT,
                                 BRACE_RIGHT, and DLM_FORMAT_ITEM_BEGIN) to the
                                 listing of special characters.

                              3) Incorporate and activate alternative blocks to
                                 exercise the new extension methods on the
                                 System.string class.

    2017/07/19 7.0     DAG    Define EnumerateStringResourcesInAssembly, an 
                              experimental method to enumerate every string
                              resource defined in an assembly, and suppress the
                              UTF-8 file reading test unless the command line is
                              empty.
 
    2017/09/04 7.0     DAG    Add the new character constants, several of which
                              are aliases of existing character constants.

    2017/09/10 7.0     DAG    Make BeginTest and TestDone visible to the whole
                              assembly.

    2017/09/17 7.0     DAG    Add 3 NumericFormat and MagicNumbers constants.

    2018/10/07 7.1     DAG    1) Test the new CapitalizeWords extension method.

                              2) Define SpecialStringExercises to list and show
                                 the SpecialStrings constants.

    2018/11/10 7.11    DAG    1) Add EnumFromStringExercises to test a new 
                                 string extension method that converts a string 
                                 to an enumeration.

                              2) Display decimal and hexadecimal representations
                                 of all MagicNumbers constants, including the 2
                                 new ones, EXACTLY_TEN and EVENLY_DIVISIBLE.

    2017/09/17 7.13    DAG    Display decimal and hexadecimal representations of
                              new integer constants EXACTLY_ONE_HUNDRED_MILLION,
                              EXACTLY_ONE_HUNDRED_MILLION_LONG, and
                              EXACTLY_ONE_HUNDRED_THOUSAND.

    2018/12/24 7.14    DAG    Add the TICKS_PER_* constants incorporated into
                              the MagicNumbers class to the listing in method
                              DisplayFormatsExercises. The breaking change is
                              self-correcting.

    2019/06/09 7.20    DAG    Amend EnumerateStringResourcesInAssembly to test
                              the ListResourcesInAssemblyByName overload that
                              takes a StreamWriter into which it is expected to
                              write a tab-delimited list of the string resources
                              stored in the specified assembly.

    2019/07/04 7.21    DAG    1) Amend Registry key and value name constants in
                                 UtilsExercises, so that all keys exist, and can
                                 be expected to exist on all Windows
                                 installations.

                              2) Move the REGISTRY_VALUE_TYPE_* strings into the
                                 public string resources exposed by the Common
                                 library.

    2019/07/18 7.21    DAG    1) Add generalization of SpecialStringExercises to
                                 the task list. This gets a revision history
                                 item because adding the task list item changes
                                 the file modified date.

                              2) Add an argument to PauseForPictures that takes
                                 a string that is displayed on the error console
                                 when standard output is redirected.

    2019/11/17 7.23    DAG    Add code to exercise most of the static methods in
                              the NumericFormats class, in particular the new
                              FormatIntegerLeftPadded method.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Linq;

using WizardWrx;
using WizardWrx.Core;
using WizardWrx.DLLConfigurationManager;


namespace DLLServices2TestStand
{
    internal static class NewClassTests_20140914
    {
        #region Private Test Data Arrays
        const string TEST_STRING_1 = "This test line has a Windows line terminator.\r\n";
        const string TEST_STRING_2 = "This test line has a Unix line terminator.\n";
        const string TEST_STRING_3 = "This test line is terminated by a carriage return.\r";
        const string TEST_STRING_4 = "This test line is unterminated.";

        static readonly string [ ] s_astrTestStrings =
        {
            TEST_STRING_1 ,
            TEST_STRING_2 ,
            TEST_STRING_3 ,
            TEST_STRING_4
        };  // s_astrTestStrings array

        static readonly string [ ] s_astrStringsToCapitalize =
        {
            @"false" ,
            @"true" ,
            @"united states of america"
        };  // s_astrStringsToCapitalize array

        //	--------------------------------------------------------------------
        //	This list comprises a full set of IntegerToHexStrExercises test 
        //	cases, including a couple of illogical combinations.
        //	--------------------------------------------------------------------

        static readonly NumericFormats.HexFormatDecoration [ ] s_enmFormatFlags =
        {
            NumericFormats.HexFormatDecoration.None ,
            NumericFormats.HexFormatDecoration.Glyphs_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC ,
            NumericFormats.HexFormatDecoration.Glyphs_LC | NumericFormats.HexFormatDecoration.Prefix_Oh_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_LC | NumericFormats.HexFormatDecoration.Prefix_Oh_UC ,
            NumericFormats.HexFormatDecoration.Glyphs_LC | NumericFormats.HexFormatDecoration.Prefix_Ox_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_LC | NumericFormats.HexFormatDecoration.Prefix_Ox_UC ,
            NumericFormats.HexFormatDecoration.Glyphs_LC | NumericFormats.HexFormatDecoration.Suffix_h_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_LC | NumericFormats.HexFormatDecoration.Suffix_h_UC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Prefix_Ox_LC | NumericFormats.HexFormatDecoration.Suffix_h_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Prefix_Oh_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Prefix_Oh_UC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Prefix_Ox_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Prefix_Ox_UC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Suffix_h_LC ,
            NumericFormats.HexFormatDecoration.Glyphs_UC | NumericFormats.HexFormatDecoration.Suffix_h_UC
        };  // s_enmFormatFlags array

        //  --------------------------------------------------------------------
        //  EnumFromStringExercises uses these as its test cases, the last three 
        //  of which are invalid.
        //  --------------------------------------------------------------------

        static readonly string [ ] s_astrEnumFromStringTestsValues =
        {
            @"None" ,
            @"Prefix_Ox_LC" ,
            @"Prefix_Ox_UC" ,
            @"Prefix_Oh_LC" ,
            @"Prefix_Oh_UC" ,
            @"Suffix_h_LC" ,
            @"Suffix_h_UC" ,
            @"Glyphs_LC" ,
            @"Glyphs_UC" ,
            @"All_Prefixes" ,
            @"All_Suffixes" ,
            @"All_Glyphs" ,
            @"All_Flags" ,
            SpecialStrings.EMPTY_STRING ,
            null ,
            @"Everything"
        };  // s_astrEnumFromStringTestsValues
        #endregion  // Private Test Data Arrays


        #region Public Methods
        internal static int ArrayInfoExercises ( ref int pintTestNumber )
        {
            const int TEST_INDEX = 10;

            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine ( "    Public  Constant ArrayInfo.ARRAY_FIRST_ELEMENT  = {0}" , ArrayInfo.ARRAY_FIRST_ELEMENT );
            Console.WriteLine ( "    Public  Constant ArrayInfo.ARRAY_IS_EMPTY       = {0}" , ArrayInfo.ARRAY_IS_EMPTY );
            Console.WriteLine ( "    Public  Constant ArrayInfo.ARRAY_INVALID_INDEX  = {0}" , ArrayInfo.ARRAY_INVALID_INDEX );
            Console.WriteLine ( "    Public  Constant ArrayInfo.INDEX_FROM_ORDINAL   = {0}" , ArrayInfo.INDEX_FROM_ORDINAL );
            Console.WriteLine ( "    Public  Constant ArrayInfo.ARRAY_SECOND_ELEMENT = {0}" , ArrayInfo.ARRAY_SECOND_ELEMENT );
            Console.WriteLine ( "    Public  Constant ArrayInfo.NEXT_INDEX           = {0}" , ArrayInfo.NEXT_INDEX );
            Console.WriteLine ( "    Public  Constant ArrayInfo.ORDINAL_FROM_INDEX   = {0}{1}" , ArrayInfo.ORDINAL_FROM_INDEX , Environment.NewLine );

            Console.WriteLine ( "    Public method ArrayInfo.IndexFromOrdinal       = {0,2:N0} , for pintOrdinal = {1,2:N0}" , ArrayInfo.IndexFromOrdinal ( TEST_INDEX ) , TEST_INDEX );
            Console.WriteLine ( "    Public method ArrayInfo.OrdinalFromIndex       = {0,2:N0} , for pintIndex   = {1,2:N0}" , ArrayInfo.OrdinalFromIndex ( TEST_INDEX ) , TEST_INDEX );

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // ArrayInfoExercises method


        internal static int CapitalizeWordsExercises ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intCaseIndex < s_astrStringsToCapitalize.Length ;
                      intCaseIndex++ )
            {
                Console.WriteLine (
                    @"    Test {0}: Input = {1}, Output = {2}" ,
                    ArrayInfo.OrdinalFromIndex ( intCaseIndex ) ,
                    s_astrStringsToCapitalize [ intCaseIndex ].QuoteString ( ) ,
                    s_astrStringsToCapitalize [ intCaseIndex ].CapitalizeWords ( ).QuoteString ( ) );
            }   // for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCaseIndex < s_astrStringsToCapitalize.Length ; intCaseIndex++ )

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // CapitalizeWordsExercises method


        internal static int ChopChop ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            int intCaseNumber = MagicNumbers.ZERO;

            foreach ( string strTestCase in s_astrTestStrings )
            {
                #if USE_STRING_EXTENSION_METHODS
                    string strChopped = strTestCase.Chop ( );
                #else
                    string strChopped = StringTricks.Chop ( strTestCase );
                #endif	// #if USE_STRING_EXTENSION_METHODS
                Console.WriteLine (
                    Properties.Resources.CHOP_TEST_REPORT ,						// Format Control String (message template)
                    new object [ ]
                    {
                        ++intCaseNumber ,										// Format Item 0 = Case Number
                        strTestCase.Length ,									// Format Item 1 = Input String Length
                        strTestCase ,											// Format Item 2 = Input String
                        strChopped.Length ,										// Format Item 3 = Output String Length
                        strChopped ,											// Format Item 4 = Output String
                        Environment.NewLine } );								// Format Item 5 = Newline, My Way
            }	// foreach ( string strTestCase in s_astrTestStrings )

            {	// Test the empty string degenerate case.
                #if USE_STRING_EXTENSION_METHODS
                    string strChopped = SpecialStrings.EMPTY_STRING.Chop ( );
                #else
                    string strChopped = StringTricks.Chop ( WizardWrx.SpecialStrings.EMPTY_STRING );
                #endif	// #if USE_STRING_EXTENSION_METHODS

                Console.WriteLine (
                    Properties.Resources.CHOP_TEST_REPORT ,						// Format Control String (message template)
                    new object [ ]
                    {
                        ++intCaseNumber ,										// Format Item 0 = Case Number
                        SpecialStrings.EMPTY_STRING.Length ,					// Format Item 1 = Input String Length
                        Properties.Resources.MESSAGE_EMPTY_STRING ,	 			// Format Item 2 = Input String
                        strChopped.Length ,										// Format Item 3 = Output String Length
                        string.IsNullOrEmpty ( strChopped )
                            ? Properties.Resources.MESSAGE_EMPTY_STRING :
                            strChopped ,										// Format Item 4 = Output String
                        Environment.NewLine } );								// Format Item 5 = Newline, My Way
            }	// Empty string degenerate case.

            {	// Test the null string degenerate case.
                #if USE_STRING_EXTENSION_METHODS
                    string strChopped = "Since there is no object to extend, the null reference case is meaningless for an extension method.";
                #else
                    string strChopped = StringTricks.Chop ( null );
                #endif	// #if USE_STRING_EXTENSION_METHODS

                Console.WriteLine (
                    Properties.Resources.CHOP_TEST_REPORT ,						// Format Control String (message template)
                    new object [ ]
                    {
                        ++intCaseNumber ,										// Format Item 0 = Case Number
                        SpecialStrings.EMPTY_STRING.Length ,					// Format Item 1 = Input String Length
                        Properties.Resources.MESSAGE_NULL_STRING ,				// Format Item 2 = Input String
                        strChopped == null
                            ? ListInfo.EMPTY_STRING_LENGTH
                            : strChopped.Length ,								// Format Item 3 = Output String Length
                        strChopped ?? Properties.Resources.MESSAGE_NULL_STRING ,// Format Item 4 = Output String
                        Environment.NewLine } );								// Format Item 5 = Newline, My Way
            }	// Null string degenerate case.

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }	// ChopChop method


        internal static int CSVFileInfoExercises ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine ( @"    Public  Constant CSVFileInfo.EMPTY_FILE     = {0}" , CSVFileInfo.EMPTY_FILE );
            Console.WriteLine ( @"    Public  Constant CSVFileInfo.LABEL_ROW      = {0}" , CSVFileInfo.LABEL_ROW );
            Console.WriteLine ( @"    Public  Constant CSVFileInfo.FIRST_RECORD   = {0}{1}" , CSVFileInfo.FIRST_RECORD , Environment.NewLine );

            string [ ] astrDummyRecords = new string [ ]
            {
                @"LabelRow" ,
                @"DataRRow1" ,
                @"DataRow2" ,
                @"DataRow3" ,
                @"DataRow4" ,
                @"DataRow5"
            };	// string [ ] astrDummyRecords

            Console.WriteLine (
                @"    Public method CSVFileInfo.IndexFromOrdinal = {0} , for pastrWholeFile = {1}" ,
                CSVFileInfo.RecordCount ( astrDummyRecords ) ,
                astrDummyRecords.Length );

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // CSVFileInfoExercises method


        internal static int DisplayFormatsExercises ( ref int pintTestNumber )
        {
            const int SAMPLE_INTEGER = 65533;               // 65,535 = (2 ^^ 16) - 3
            const double SAMPLE_FIXED_POINT = 0.997;     // 99.7%

            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );
            Console.WriteLine (
                @"Integral format strings - sample = {0}{1}" ,
                SAMPLE_INTEGER ,
                Environment.NewLine );
            Console.WriteLine (
                @"Fixed point decimal - sample = {0}{1}" ,
                SAMPLE_FIXED_POINT ,
                Environment.NewLine );

            Console.WriteLine ( @"    IMPORTANT: As of version 6.2, the definitions of these formatting strings{0}               are in the NumericFormats class, but the indirect definitions{0}               in DisplayFormats are retained for convenient access to all{0}               display formats through a single class.{0}{0}               This report shows both sets.{0}" , Environment.NewLine );

            //	----------------------------------------------------------------
            //	Each group is enough to fill a screen, and the prologue gets one
            //	of its own.
            //	----------------------------------------------------------------

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                null );

            Console.WriteLine ( @"{0}    --------------{0}    DisplayFormats{0}    --------------{0}" , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL                       = {0} (Sample = {1})" , DisplayFormats.HEXADECIMAL_UC , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_2                     = {0} (Sample = {1})" , DisplayFormats.HEXADECIMAL_2 , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_4                     = {0} (Sample = {1})" , DisplayFormats.HEXADECIMAL_4 , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_4 ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_8                     = {0} (Sample = {1}){2}" , DisplayFormats.HEXADECIMAL_8 , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_8 ) , Environment.NewLine );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_16                    = {0} (Sample = {1}){2}" , DisplayFormats.HEXADECIMAL_8 , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_16 ) , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_PREFIX_0H_LC          = {0} (Sample = {0}{1})" , DisplayFormats.HEXADECIMAL_PREFIX_0H_LC , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_PREFIX_0H_UC          = {0} (Sample = {0}{1})" , DisplayFormats.HEXADECIMAL_PREFIX_0H_UC , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_PREFIX_0X_LC          = {0} (Sample = {0}{1})" , DisplayFormats.HEXADECIMAL_PREFIX_0X_LC , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_PREFIX_0X_UC          = {0} (Sample = {0}{1}){2}" , DisplayFormats.HEXADECIMAL_PREFIX_0X_UC , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_PREFIX_0X_LC          = {0} (Sample = {0}{1})" , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) , NumericFormats.HEXADECIMAL_SUFFIX_H_LC );
            Console.WriteLine ( @"    Public Constant DisplayFormats.HEXADECIMAL_PREFIX_0X_UC          = {0} (Sample = {0}{1}){2}" , SAMPLE_INTEGER.ToString ( DisplayFormats.HEXADECIMAL_UC ) , NumericFormats.HEXADECIMAL_SUFFIX_H_UC , Environment.NewLine );

            //	----------------------------------------------------------------
            //	Each group is enough to fill a screen.
            //	----------------------------------------------------------------

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                @"DisplayFormats.HEXADECIMAL constants" );

            Console.WriteLine ( @"{0}    ------------------------{0}    NumericFormats Constants{0}    ------------------------{0}" , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL                       = {0} (Sample = {1})" , NumericFormats.HEXADECIMAL_UC , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_2                     = {0} (Sample = {1})" , NumericFormats.HEXADECIMAL_2 , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_4                     = {0} (Sample = {1})" , NumericFormats.HEXADECIMAL_4 , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_4 ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_8                     = {0} (Sample = {1}){2}" , NumericFormats.HEXADECIMAL_8 , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_8 ) , Environment.NewLine );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_16                    = {0} (Sample = {1}){2}" , NumericFormats.HEXADECIMAL_16 , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_16 ) , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_PREFIX_0H_LC          = {0} (Sample = {0}{1})" , NumericFormats.HEXADECIMAL_PREFIX_0H_LC , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_PREFIX_0H_UC          = {0} (Sample = {0}{1})" , NumericFormats.HEXADECIMAL_PREFIX_0H_UC , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_PREFIX_0X_LC          = {0} (Sample = {0}{1})" , NumericFormats.HEXADECIMAL_PREFIX_0X_LC , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_PREFIX_0X_UC          = {0} (Sample = {0}{1}){2}" , NumericFormats.HEXADECIMAL_PREFIX_0X_UC , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_PREFIX_0X_LC          = {0} (Sample = {0}{1})" , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) , NumericFormats.HEXADECIMAL_SUFFIX_H_LC );
            Console.WriteLine ( @"    Public Constant NumericFormats.HEXADECIMAL_PREFIX_0X_UC          = {0} (Sample = {0}{1}){2}" , SAMPLE_INTEGER.ToString ( NumericFormats.HEXADECIMAL_UC ) , NumericFormats.HEXADECIMAL_SUFFIX_H_UC , Environment.NewLine );

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                @"NumericFormats.HEXADECIMAL constants" );

            
            Console.WriteLine ( @"{0}    -----------------------------{0}    NumericFormats Static Methods{0}    -----------------------------{0}" , Environment.NewLine );

            {   // Constrain the scope of strings strFormatString and strSampleOutput.
                string strFormatString = NumericFormats.FixedPointDecimal ( );
                string strSampleOutput = SAMPLE_FIXED_POINT.ToString (
                    strFormatString );
                Console.WriteLine (
                    @"        FixedPointDecimal without arguments     = {0}     - Sample output = {1}{2}" ,
                    strFormatString ,
                    strSampleOutput ,
                    Environment.NewLine );
            }   // Allow strings strFormatString and strSampleOutput to go out of scope.

            for ( int intFractionDigits = MagicNumbers.PLUS_ONE ;
                      intFractionDigits <= MagicNumbers.PLUS_SEVEN ;
                      intFractionDigits++ )
            {
                string strFormatString = NumericFormats.FixedPointDecimal ( intFractionDigits );
                string strSampleOutput = SAMPLE_FIXED_POINT.ToString (
                    strFormatString );
                Console.WriteLine (
                    @"        FixedPointDecimal for intFractionDigits = {0} = {1} - Sample output = {2}" ,
                    intFractionDigits ,
                    strFormatString ,
                    strSampleOutput );
            }   // for ( int intFractionDigits = MagicNumbers.PLUS_ONE ; intFractionDigits <= MagicNumbers.PLUS_SEVEN ; intFractionDigits++ )

            {   // Constrain the scope of strings strFormatString and strSampleOutput.
                string strFormatString = NumericFormats.FixedWidthInteger ( );
                string strSampleOutput = SAMPLE_INTEGER.ToString (
                    strFormatString );
                Console.WriteLine (
                    @"{2}        FixedWidthInteger without arguments         = {0}     - Sample output = {1}{2}" ,
                    strFormatString ,
                    strSampleOutput ,
                    Environment.NewLine );
            }   // Allow strings strFormatString and strSampleOutput to go out of scope.

            for ( int intTotalDigits = MagicNumbers.PLUS_ONE ;
                      intTotalDigits <= MagicNumbers.PLUS_SEVEN ;
                      intTotalDigits++ )
            {
                string strFormatString = NumericFormats.FixedWidthInteger (
                    intTotalDigits );
                string strSampleOutput = SAMPLE_INTEGER.ToString (
                    strFormatString );
                Console.WriteLine (
                    @"        FixedWidthInteger for pintTotalDigits       = {0} = {1} - Sample output = {2}" ,
                    intTotalDigits ,
                    strFormatString ,
                    strSampleOutput );
                Console.WriteLine (
                    @"        FormatIntegerLeftPadded for pintTotalDigits = {0} - Raw = {1} - Formatted = {2}{3}" ,
                    intTotalDigits ,                                            // Format Item 0 = FormatIntegerLeftPadded for pintTotalDigits = {0}
                    SAMPLE_INTEGER ,                                            // Format Item 1 = Raw = {1}
                    NumericFormats.FormatIntegerLeftPadded (                    // Format Item 2 = Formatted = {2}
                        SAMPLE_INTEGER ,                                        // int pintValue
                        intTotalDigits ) ,                                      // int pintDesiredWidth
                    Environment.NewLine );                                      // Format Item 3 = Platform-dependent newline.
            }   // for ( int intTotalDigits = MagicNumbers.PLUS_ONE ; intTotalDigits <= MagicNumbers.PLUS_SEVEN ; intTotalDigits++ )

            {   // Constrain the scope of integer array auintStatusCodes.
                uint [ ] auintStatusCodes = new uint [ ]
                {
                    MagicNumbers.ERROR_SUCCESS ,
                    MagicNumbers.ERROR_RUNTIME ,
                    MagicNumbers.APPLICATION_ERROR_MASK
                };

                for ( int intStatusCodeIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intStatusCodeIndex < auintStatusCodes.Length ;
                          intStatusCodeIndex++ )
                {
                    string strFormatString = NumericFormats.FormatStatusCode (
                        ( int ) auintStatusCodes [ intStatusCodeIndex ] );
                    string strSampleOutput = SAMPLE_INTEGER.ToString (
                        strFormatString );
                    Console.WriteLine (
                        @"{3}        FormatStatusCode for pintStatusCode = {0} = {1} - Sample output = {2}{3}" ,
                        auintStatusCodes [ intStatusCodeIndex ] ,
                        strFormatString ,
                        strSampleOutput ,
                        intStatusCodeIndex.Equals ( ArrayInfo.ARRAY_FIRST_ELEMENT ) || intStatusCodeIndex.Equals ( ArrayInfo.IndexFromOrdinal ( auintStatusCodes.Length ) )
                            ? Environment.NewLine
                            : SpecialStrings.EMPTY_STRING );
                }   // for ( int intStatusCodeIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intStatusCodeIndex < auintStatusCodes.Length ; intStatusCodeIndex++ )
            }   // Allow integer array auintStatusCodes to go out of scope.

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                @"NumericFormats Static Methods" );

            IntegerToHexStrExercises ( );

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                @"IntegerToHexStrExercises" );

            MagicNumberExercises ( );

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                @"MagicNumberExercises" );

            NumericFormatterExercises ( );

            Program.PauseForPictures (
                Program.OMIT_LINEFEED ,
                @"NumericFormatterExercises" );

            Console.WriteLine (
                @"{1}More Integral format strings - sample = {0}{1}" ,
                SAMPLE_INTEGER ,
                Environment.NewLine );

            Console.WriteLine ( @"    Public Constant DisplayFormats.NUMBER_PER_REG_SETTINGS           = {0} (Sample = {1})" , DisplayFormats.NUMBER_PER_REG_SETTINGS , SAMPLE_INTEGER.ToString ( DisplayFormats.NUMBER_PER_REG_SETTINGS ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.NUMBER_PER_REG_SETTINGS_0D        = {0} (Sample = {1})" , DisplayFormats.NUMBER_PER_REG_SETTINGS_0D , SAMPLE_INTEGER.ToString ( DisplayFormats.NUMBER_PER_REG_SETTINGS_0D ) );
            Console.WriteLine ( @"    Public Constant NumericFormats.INTEGER_PER_REG_SETTINGS (alias)  = {0} (Sample = {1})" , NumericFormats.INTEGER_PER_REG_SETTINGS , SAMPLE_INTEGER.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.INTEGER_PER_REG_SETTINGS (alias)  = {0} (Sample = {1})" , DisplayFormats.INTEGER_PER_REG_SETTINGS , SAMPLE_INTEGER.ToString ( DisplayFormats.INTEGER_PER_REG_SETTINGS ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.NUMBER_PER_REG_SETTINGS_2D        = {0} (Sample = {1})" , DisplayFormats.NUMBER_PER_REG_SETTINGS_2D , SAMPLE_INTEGER.ToString ( DisplayFormats.NUMBER_PER_REG_SETTINGS_2D ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.NUMBER_PER_REG_SETTINGS_3D        = {0} (Sample = {1})" , DisplayFormats.NUMBER_PER_REG_SETTINGS_3D , SAMPLE_INTEGER.ToString ( DisplayFormats.NUMBER_PER_REG_SETTINGS_3D ) );

            Console.WriteLine ( @"{1}Floating point format strings - sample = {0}{1}" , SAMPLE_FIXED_POINT , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant DisplayFormats.PERCENT                           = {0} (Sample = {1})" , DisplayFormats.PERCENT , SAMPLE_FIXED_POINT.ToString ( DisplayFormats.PERCENT ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.PERCENT_DIGITS_2                  = {0} (Sample = {1})" , DisplayFormats.PERCENT_DIGITS_2 , SAMPLE_FIXED_POINT.ToString ( DisplayFormats.PERCENT_DIGITS_2 ) );

            Program.PauseForPictures (
                Program.APPEND_LINEFEED ,
                @"DisplayFormats Miscellaneous Numerics" );

            DateTime dtmSample = DateTime.Now;

            Console.WriteLine ( @"{1}Date and Time format strings - sample = {0}{1}" , dtmSample , Environment.NewLine );

            Console.WriteLine ( @"    Public Constant DisplayFormats.STANDARD_DISPLAY_DATE_FORMAT      = {0} (Sample = {1})" , SysDateFormatters.STANDARD_DISPLAY_DATE_FORMAT , SysDateFormatters.ReformatSysDate ( dtmSample , SysDateFormatters.STANDARD_DISPLAY_DATE_FORMAT ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.STANDARD_DISPLAY_DATE_TIME_FORMAT = {0} (Sample = {1})" , SysDateFormatters.STANDARD_DISPLAY_DATE_TIME_FORMAT , SysDateFormatters.ReformatSysDate ( dtmSample , SysDateFormatters.STANDARD_DISPLAY_DATE_TIME_FORMAT ) );
            Console.WriteLine ( @"    Public Constant DisplayFormats.STANDARD_DISPLAY_TIME_FORMAT      = {0} (Sample = {1})" , SysDateFormatters.STANDARD_DISPLAY_TIME_FORMAT , SysDateFormatters.ReformatSysDate ( dtmSample , SysDateFormatters.STANDARD_DISPLAY_TIME_FORMAT ) );

            Console.WriteLine ( @"{1}    Public Method DisplayFormats.FormatDateForShow                   = {0}" , SysDateFormatters.FormatDateForShow ( dtmSample ) , Environment.NewLine );
            Console.WriteLine ( @"    Public Method DisplayFormats.FormatDateTimeForShow               = {0}" , SysDateFormatters.FormatDateTimeForShow ( dtmSample ) );
            Console.WriteLine ( @"    Public Method DisplayFormats.FormatTimeForShow                   = {0}" , SysDateFormatters.FormatTimeForShow ( dtmSample ) );

            return TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // DisplayFormatsExercises method


        internal static int EnumerateStringResourcesInAssembly (
            ref int pintTestNumber ,
            System.Reflection.Assembly pasmInWhichEmbedded ,
            System.IO.StreamWriter pswCommonStringsReportFileName = null )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            //  ----------------------------------------------------------------
            //  Run the test in such a way that the optional argument is omitted
            //  when it would be a null reference, thus demonstrating that it is
            //  truly optional.
            //  ----------------------------------------------------------------

            if ( pswCommonStringsReportFileName == null )
                WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName (
                    pasmInWhichEmbedded );
            else
                WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName (
                    pasmInWhichEmbedded ,
                    pswCommonStringsReportFileName );

            return TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // EnumerateStringResourcesInAssembly method


        private static void MagicNumberExercises ( )
        {
            Console.WriteLine ( "{0}Enumerate all MagicNumbers Constants:{0}" , Environment.NewLine );

            Console.WriteLine ( "    MagicNumbers.APPLICATION_ERROR_MASK            = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.APPLICATION_ERROR_MASK , MagicNumbers.APPLICATION_ERROR_MASK.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.APPLICATION_ERROR_MASK.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_01KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_01KB , MagicNumbers.CAPACITY_01KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_01KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_02KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_02KB , MagicNumbers.CAPACITY_02KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_02KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_04KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_04KB , MagicNumbers.CAPACITY_04KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_04KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_08KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_08KB , MagicNumbers.CAPACITY_08KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_08KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_16KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_16KB , MagicNumbers.CAPACITY_16KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_16KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_32KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_32KB , MagicNumbers.CAPACITY_32KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_32KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_64KB                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_64KB , MagicNumbers.CAPACITY_64KB.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_64KB.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.CAPACITY_MAX_PATH                 = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.CAPACITY_MAX_PATH , MagicNumbers.CAPACITY_MAX_PATH.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.CAPACITY_MAX_PATH.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EMPTY_STRING_LENGTH               = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EMPTY_STRING_LENGTH , MagicNumbers.EMPTY_STRING_LENGTH.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EMPTY_STRING_LENGTH.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.ERROR_INVALID_CMD_LNE_ARGS        = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.ERROR_INVALID_CMD_LNE_ARGS , MagicNumbers.ERROR_INVALID_CMD_LNE_ARGS.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.ERROR_INVALID_CMD_LNE_ARGS.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.ERROR_RUNTIME                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.ERROR_RUNTIME , MagicNumbers.ERROR_RUNTIME.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.ERROR_RUNTIME.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.ERROR_SUCCESS                     = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.ERROR_SUCCESS , MagicNumbers.ERROR_SUCCESS.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.ERROR_SUCCESS.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EVENLY_DIVISIBLE                  = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EVENLY_DIVISIBLE , MagicNumbers.EVENLY_DIVISIBLE.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EVENLY_DIVISIBLE.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_TEN                       = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_TEN , MagicNumbers.EXACTLY_TEN.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_TEN.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_HUNDRED               = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_ONE_HUNDRED , MagicNumbers.EXACTLY_ONE_HUNDRED.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_HUNDRED.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_THOUSAND              = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_ONE_THOUSAND , MagicNumbers.EXACTLY_ONE_THOUSAND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_THOUSAND.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_TEN_THOUSAND              = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_TEN_THOUSAND , MagicNumbers.EXACTLY_TEN_THOUSAND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_TEN_THOUSAND.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_HUNDRED_THOUSAND      = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_TEN_THOUSAND , MagicNumbers.EXACTLY_ONE_HUNDRED_THOUSAND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_HUNDRED_THOUSAND.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_MILLION               = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_ONE_MILLION , MagicNumbers.EXACTLY_ONE_MILLION.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_MILLION.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION       = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION , MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION_LONG  = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION_LONG , MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION_LONG.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_HUNDRED_MILLION_LONG.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.EXACTLY_ONE_BILLION               = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.EXACTLY_ONE_BILLION , MagicNumbers.EXACTLY_ONE_BILLION.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.EXACTLY_ONE_BILLION.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.MILLISECONDS_PER_SECOND           = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.MILLISECONDS_PER_SECOND , MagicNumbers.MILLISECONDS_PER_SECOND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.MILLISECONDS_PER_SECOND.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.MINUS_ONE                         = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.MINUS_ONE , MagicNumbers.MINUS_ONE.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.MINUS_ONE.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.NUMBER_BASE_DECIMAL               = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.NUMBER_BASE_DECIMAL , MagicNumbers.NUMBER_BASE_DECIMAL.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.NUMBER_BASE_DECIMAL.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.NUMBER_BASE_HEXADECIMAL           = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.NUMBER_BASE_HEXADECIMAL , MagicNumbers.NUMBER_BASE_HEXADECIMAL.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.NUMBER_BASE_HEXADECIMAL.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.PLUS_ONE                          = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.PLUS_ONE , MagicNumbers.PLUS_ONE.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.PLUS_ONE.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.PLUS_TWO                          = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.PLUS_TWO , MagicNumbers.PLUS_TWO.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.PLUS_TWO.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.PLUS_SEVEN                        = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.PLUS_SEVEN , MagicNumbers.PLUS_SEVEN.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.PLUS_SEVEN.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.STRING_INDEXOF_NOT_FOUND          = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.STRING_INDEXOF_NOT_FOUND , MagicNumbers.STRING_INDEXOF_NOT_FOUND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.STRING_INDEXOF_NOT_FOUND.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.STRING_SUBSTR_BEGINNING           = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.STRING_SUBSTR_BEGINNING , MagicNumbers.STRING_SUBSTR_BEGINNING.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.STRING_SUBSTR_BEGINNING.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_SECOND                  = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_SECOND , MagicNumbers.TICKS_PER_SECOND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_SECOND.ToString ( NumericFormats.HEXADECIMAL_8 ) );

            Console.WriteLine ( "    MagicNumbers.TICKS_PER_1_WEEK                  = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_1_WEEK , MagicNumbers.TICKS_PER_1_WEEK.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_1_WEEK.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_1_DAY                   = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_1_DAY , MagicNumbers.TICKS_PER_1_DAY.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_1_DAY.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_23_59_59                = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_23_59_59 , MagicNumbers.TICKS_PER_23_59_59.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_23_59_59.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_23_59_00                = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_23_59_00 , MagicNumbers.TICKS_PER_23_59_00.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_23_59_00.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_1_HOUR                  = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_1_HOUR , MagicNumbers.TICKS_PER_1_HOUR.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_1_HOUR.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_1_MINUTE                = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_1_MINUTE , MagicNumbers.TICKS_PER_1_MINUTE.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_1_MINUTE.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_1_SECOND                = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_1_SECOND , MagicNumbers.TICKS_PER_1_SECOND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_1_SECOND.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.TICKS_PER_1_MILLISECOND           = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.TICKS_PER_1_MILLISECOND , MagicNumbers.TICKS_PER_1_MILLISECOND.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.TICKS_PER_1_MILLISECOND.ToString ( NumericFormats.HEXADECIMAL_8 ) );

            Console.WriteLine ( "    MagicNumbers.UNC_PREFIX_START_POS              = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.UNC_PREFIX_START_POS , MagicNumbers.UNC_PREFIX_START_POS.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.UNC_PREFIX_START_POS.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.UNC_PREFIX_START_LEN              = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.UNC_PREFIX_START_LEN , MagicNumbers.UNC_PREFIX_START_LEN.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.UNC_PREFIX_START_LEN.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            Console.WriteLine ( "    MagicNumbers.ZERO                              = {0} (Formatted: {1}, Hexadecimal: 0x{2})" , MagicNumbers.ZERO , MagicNumbers.ZERO.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) , MagicNumbers.ZERO.ToString ( NumericFormats.HEXADECIMAL_8 ) );

            Console.WriteLine ( "{0}MagicNumbers Constants enumerated!{0}" , Environment.NewLine );
        }   // DisplayFormatsExercises method


        internal static int EnumFromStringExercises ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intIndex < s_astrEnumFromStringTestsValues.Length ;
                      intIndex++ )
            {
                try
                {
                    NumericFormats.HexFormatDecoration hexFormatDecoration = s_astrEnumFromStringTestsValues [ intIndex ].EnumFromString<NumericFormats.HexFormatDecoration> ( );
                    Console.WriteLine (
                        @"    Input String {0}: {1} = {2} ({3}, {4} decimal)" , // Format control string
                        new object [ ]
                        {
                            ArrayInfo.OrdinalFromIndex ( intIndex ) ,           // Format Item 0: Input String # {0}:
                            s_astrEnumFromStringTestsValues [ intIndex ] ,      // Format Item 1: : {1} =
                            hexFormatDecoration ,                               // Format Item 2: = {2} (
                            NumericFormats.IntegerToHexStr (
                                ( int ) hexFormatDecoration ) ,                 // Format Item 3: ({3},
                            ( int ) hexFormatDecoration                         // Format Item 4: , {4} decimal)
                        } );
                }
                catch ( Exception exAll )
                {
                    Console.WriteLine (
                        @"    Input String {0}: {1} = threw the following exception:" ,
                        new object [ ]
                        {
                            ArrayInfo.OrdinalFromIndex ( intIndex ) ,           // Format Item 0: Input String # {0}:
                            s_astrEnumFromStringTestsValues [ intIndex ]        // Format Item 1: : {1} =
                        } );
                    ExceptionLogger.GetTheSingleInstance ( ).ReportException ( exAll );
                }
            }   // for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < s_astrEnumFromStringTestsValues.Length ; intIndex++ )

            return TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // EnumFromStringExercises methods


        internal static int SpecialStringExercises ( ref int pintTestNumber )
        {   // ToDo: Move this into the core library as a generic property enumerator.
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            List<System.Reflection.FieldInfo> fieldInfos = typeof ( SpecialStrings ).GetFields (
                System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.FlattenHierarchy ).Where (
                fi => fi.IsLiteral && !fi.IsInitOnly ).ToList ( );

            foreach ( System.Reflection.FieldInfo fieldInfo in fieldInfos )
            {
                Console.WriteLine (
                    @"    SpecialStrings.{0} = {1}" ,
                    fieldInfo.Name ,
                    fieldInfo.GetRawConstantValue ( ) );
            }   // foreach ( System.Reflection.FieldInfo fieldInfo in fieldInfos )

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // SpecialStringExercises


        internal static int FileIOFlagsExercises ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine ( "    Public Constant FileIOFlags.FILE_COPY_OVERWRITE_FORBIDDEN    = {0}" , FileIOFlags.FILE_COPY_OVERWRITE_FORBIDDEN );
            Console.WriteLine ( "    Public Constant FileIOFlags.FILE_COPY_OVERWRITE_PERMITTED    = {0}{1}" , FileIOFlags.FILE_COPY_OVERWRITE_PERMITTED , Environment.NewLine );

            Console.WriteLine ( "    Public Constant FileIOFlags.FILE_OUT_APPEND                  = {0}" , FileIOFlags.FILE_OUT_APPEND );
            Console.WriteLine ( "    Public Constant FileIOFlags.FILE_OUT_CREATE                  = {0}{1}" , FileIOFlags.FILE_OUT_CREATE , Environment.NewLine );

            Console.WriteLine ( "    Public Constant FileIOFlags.FILE_READ_ENCODING_CHECK_FOR_BOM = {0}" , FileIOFlags.FILE_READ_ENCODING_CHECK_FOR_BOM );
            Console.WriteLine ( "    Public Constant FileIOFlags.FILE_READ_ENCODING_IGNORE_BOM    = {0}{1}" , FileIOFlags.FILE_READ_ENCODING_IGNORE_BOM , Environment.NewLine );

            Console.WriteLine ( "    Public Constant FileIOFlags.MAKE_STREAM_IO_ASYNCHRONOUS      = {0}" , FileIOFlags.MAKE_STREAM_IO_ASYNCHRONOUS );
            Console.WriteLine ( "    Public Constant FileIOFlags.MAKE_STREAM_IO_SYNCHRONOUS       = {0}" , FileIOFlags.MAKE_STREAM_IO_SYNCHRONOUS );

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // FileIOFlagsExercises method


        private static void IntegerToHexStrExercises ( )
        {
            //	----------------------------------------------------------------
            //	All but the last test use the same sample integer.
            //	----------------------------------------------------------------

            const int TEST_INTEGER = 123456789;
            const double TEST_NON_INTEGER = 3.15159;
            const int HEX_GLYPHS_DEFAULT_MINIMUM = 8;

            int intTotalCases = s_enmFormatFlags.Length;

            Console.WriteLine (
                "{2}Thoroughly exercise the IntegerToHexStr method in class NumericFormats.{2}{2}    All {0} test cases use the same integer, {1:n0}.{2}" ,
                intTotalCases ,			// Format Item 0 = Total test cases, read from Length property of the array of test cases
                TEST_INTEGER ,			// Format Item 1 = Integer used for all tests
                Environment.NewLine );	// Format Item 2 = Embedded newline

            for ( int intCurrentCase = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intCurrentCase < intTotalCases ;
                      intCurrentCase++ )
            {
                int intScenarioOrdinal = ArrayInfo.OrdinalFromIndex ( intCurrentCase );
                Console.WriteLine (
                    "    Scenario {0,2} of {1,2}: HexFormatDecoration = {2} (as integer = {3}):{4}" ,   // Format control string
                    new object [ ]
                    {
                        intScenarioOrdinal ,														    // Format Item 0 = Scenario number
                        intTotalCases ,											    					// Format Item 1 = Total number of scenarios
                        s_enmFormatFlags [ intCurrentCase ] ,						    				// Format Item 2 = Enumeration value, as member name
                        ( int ) s_enmFormatFlags [ intCurrentCase ] ,					    			// Format Item 3 = Enumeration value, as integer
                        Environment.NewLine													    		// Format Item 4 = Embedded Newline
                    } );

                for ( int intMinDigits = MagicNumbers.PLUS_ONE ;
                          intMinDigits <= HEX_GLYPHS_DEFAULT_MINIMUM ;
                          intMinDigits++ )
                {
                    Console.WriteLine (
                        "    Test case {0}: HexFormatDecoration = {1,-12} Minimum Digits = {2} Result = {3}" ,
                        new object [ ]
                            {
                                intScenarioOrdinal ,												// Format Item 0 = Ordinal test case number derived from array subscript
                                s_enmFormatFlags [ intCurrentCase ] ,								// Format Item 1 = HexFormatDecoration value
                                intMinDigits ,														// Format Item 2 = Minimum digits to display in this test.
                                NumericFormats.IntegerToHexStr (									// Format Item 3 = Formatted integer
                                    TEST_INTEGER ,													//		T pintegralValue
                                    intMinDigits ,													//		int pintTotalDigits
                                    s_enmFormatFlags [ intCurrentCase ] )							//		HexFormatDecoration penmHexDecoration
                            } );
                }	// for ( int intMinDigits = MagicNumbers.PLUS_ONE ; intMinDigits <= HEX_GLYPHS_DEFAULT_MINIMUM ; intMinDigits++ )

                Console.WriteLine ( );                                                              // PauseForPictures adds its line feed after the message. I want one before.
                Program.PauseForPictures (
                    Program.OMIT_LINEFEED ,
                    @"IntegerToHexStr" );
            }	// for ( int intCurrentCase = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCurrentCase < intTotalCases ; intCurrentCase++ )

            try
            {
                Console.WriteLine ( "    Scenario {0} deliberately throws, because the input is non-integral." , intTotalCases++ );
                Console.WriteLine (
                    "    Test case {0}: HexFormatDecoration = {1,-12} Minimum Digits = {2} Result = {3}" ,
                    new object [ ]
                            {
                                intTotalCases ,														// Format Item 0 = Ordinal test case number derived from array subscript
                                s_enmFormatFlags [ ArrayInfo.ARRAY_SECOND_ELEMENT ] ,				// Format Item 1 = HexFormatDecoration value
                                HEX_GLYPHS_DEFAULT_MINIMUM ,										// Format Item 2 = Minimum digits to display in this test.
                                NumericFormats.IntegerToHexStr (									// Format Item 3 = Formatted integer
                                    TEST_NON_INTEGER ,												//		T pintegralValue
                                    HEX_GLYPHS_DEFAULT_MINIMUM ,									//		int pintTotalDigits
                                    s_enmFormatFlags [ ArrayInfo.ARRAY_SECOND_ELEMENT ] )			//		HexFormatDecoration penmHexDecoration
                            } );
            }
            catch ( Exception exBadArg )
            {
                StateManager.GetTheSingleInstance ( ).AppExceptionLogger.ReportException ( exBadArg );
            }
            finally
            {
                Console.WriteLine (
                    "{0}IntegerToHexStr method exercised.{0}" ,
                    Environment.NewLine );
            }		// IntegerToHexStrExercises
        }	// IntegerToHexStrExercises method


        internal static int ListInfoExercises ( ref int pintTestNumber )
        {
            const string SAMPLE_1 = @"The quick brown fox jumped over the lazy dog.";
            const string SAMPLE_2 = @"A";

            const int NTH_1 = 5;
            const int NTH_2 = 100;
            const string NULL_CHAR = @"NUL";

            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine ( "    Public Constant ListInfo.BEGINNING_OF_BUFFER       = {0}" , ListInfo.BEGINNING_OF_BUFFER );
            Console.WriteLine ( "    Public Constant ListInfo.BINARY_SEARCH_NOT_FOUND   = {0}" , ListInfo.BINARY_SEARCH_NOT_FOUND );
            Console.WriteLine ( "    Public Constant ListInfo.EMPTY_STRING_LENGTH       = {0}" , ListInfo.EMPTY_STRING_LENGTH );
            Console.WriteLine ( "    Public Constant ListInfo.INDEXOF_NOT_FOUND         = {0}" , ListInfo.INDEXOF_NOT_FOUND );
            Console.WriteLine ( "    Public Constant ListInfo.LIST_IS_EMPTY             = {0}" , ListInfo.LIST_IS_EMPTY );
            Console.WriteLine ( "    Public Constant ListInfo.SUBSTR_BEGINNING          = {0}" , ListInfo.SUBSTR_BEGINNING );
            Console.WriteLine ( "    Public Constant ListInfo.SUBSTR_SECOND_CHAR        = {0}" , ListInfo.SUBSTR_SECOND_CHAR );

            Console.WriteLine ( "{1}Input Sample for the following Public Method tests = {0}{1}" , SAMPLE_1 , Environment.NewLine );
            Console.WriteLine ( "    Public Method ListInfo.FirstCharOfString           = {0}" , ListInfo.FirstCharOfString ( SAMPLE_1 ) );
            Console.WriteLine ( "    Public Method ListInfo.LastCharacterOfString       = {0}" , ListInfo.LastCharacterOfString ( SAMPLE_1 ) );

            Console.WriteLine ( "    Public Method ListInfo.NthCharacterOfString        = {0}, where N = {1}" , ListInfo.NthCharacterOfString ( SAMPLE_1 , NTH_1 ) , NTH_1 );
            Console.WriteLine ( "    Public Method ListInfo.NthCharacterOfString        = {0}, where N = {1}" , ListInfo.NthCharacterOfString ( SAMPLE_1 , NTH_2 ) , NTH_2 );

            //  ----------------------------------------------------------------
            //  Since the null character returned for the error doesn't print,
            //  and I want to display a string when that happens, this test is a
            //  tad artificial, because it masks the fact that both methods
            //  (PenultimateCharactrOfString and SecondCharacterOfString) return
            //  a character (char).
            //  ----------------------------------------------------------------

            Console.WriteLine ( "    Public Method ListInfo.PenultimateCharactrOfString = {0}" , ListInfo.PenultimateCharactrOfString ( SAMPLE_1 ) != SpecialCharacters.NULL_CHAR ? ListInfo.PenultimateCharactrOfString ( SAMPLE_1 ).ToString ( ) : NULL_CHAR );
            Console.WriteLine ( "    Public Method ListInfo.PenultimateCharactrOfString = {0}" , ListInfo.PenultimateCharactrOfString ( SAMPLE_2 ) != SpecialCharacters.NULL_CHAR ? ListInfo.PenultimateCharactrOfString ( SAMPLE_2 ).ToString ( ) : NULL_CHAR );

            Console.WriteLine ( "    Public Method ListInfo.SecondCharacterOfString     = {0}" , ListInfo.SecondCharacterOfString ( SAMPLE_1 ) != SpecialCharacters.NULL_CHAR ? ListInfo.SecondCharacterOfString ( SAMPLE_1 ).ToString ( ) : NULL_CHAR );
            Console.WriteLine ( "    Public Method ListInfo.SecondCharacterOfString     = {0}" , ListInfo.SecondCharacterOfString ( SAMPLE_2 ) != SpecialCharacters.NULL_CHAR ? ListInfo.SecondCharacterOfString ( SAMPLE_2 ).ToString ( ) : NULL_CHAR );

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // ListInfoExercises method


        private static void NumericFormatterExercises ( )
        {
            const int TEST_INTEGER_16_BITS = 65535;
            const int TEST_INTEGER_4_BITS = 255;
            const float TEST_DOLLARS_FLOAT = 4095.95F;
            const double TEST_DOLLARS_DOUBLE = 4095.95;

            const long TEST_LONG = ( long ) TEST_INTEGER_16_BITS * ( long ) TEST_INTEGER_16_BITS;

            Console.WriteLine (
                "{0}Exercising the WizardWrx.NumberFormatters Class:{0}" ,
                Environment.NewLine );

            Console.WriteLine (
                "    TEST_DOLLARS_FLOAT: raw = {0}; formatted by NumberFormatters.DollarsAndCents = {1}" ,
                TEST_DOLLARS_FLOAT ,
                NumberFormatters.DollarsAndCents ( TEST_DOLLARS_FLOAT ) );
            Console.WriteLine (
                "    TEST_DOLLARS_DOUBLE: raw = {0}; formatted by NumberFormatters.DollarsAndCents = {1}" ,
                TEST_DOLLARS_FLOAT ,
                NumberFormatters.DollarsAndCents ( TEST_DOLLARS_DOUBLE ) );

            Console.WriteLine (
                "    TEST_INTEGER_16_BITS: raw = {0}; formatted by NumberFormatters.Integer = {1}" ,
                TEST_INTEGER_16_BITS ,
                NumberFormatters.Integer ( TEST_INTEGER_16_BITS ) );
            Console.WriteLine (
                "    TEST_LONG: raw = {0}; formatted by NumberFormatters.Integer = {1}" ,
                TEST_INTEGER_16_BITS ,
                NumberFormatters.Integer ( TEST_LONG ) );

            Console.WriteLine (
                "    TEST_INTEGER_4_BITS: raw = {0}; formatted by NumberFormatters.Hexadecimal2 = {1}" ,
                TEST_INTEGER_4_BITS ,
                NumberFormatters.Hexadecimal2 ( TEST_INTEGER_16_BITS ) );
            Console.WriteLine (
                "    TEST_INTEGER_16_BITS: raw = {0}; formatted by NumberFormatters.Hexadecimal4 = {1}" ,
                TEST_INTEGER_16_BITS ,
                NumberFormatters.Hexadecimal4 ( TEST_INTEGER_16_BITS ) );
            Console.WriteLine (
                "    TEST_INTEGER_16_BITS: raw = {0}; formatted by NumberFormatters.Hexadecimal8 = {1}" ,
                TEST_INTEGER_16_BITS ,
                NumberFormatters.Hexadecimal8 ( TEST_INTEGER_16_BITS ) );
            Console.WriteLine (
                "    TEST_LONG: raw = {0}; formatted by NumberFormatters.Hexadecimal16 = {1}" ,
                TEST_LONG ,
                NumberFormatters.Hexadecimal16 ( TEST_LONG ) );

            Console.WriteLine (
                "{0}End of WizardWrx.NumberFormatters Class Exercises{0}" ,
                Environment.NewLine );
        }	// NumericFormatterExercises method


        internal static int PathPositionsExercises ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine ( "    Public Constant PathPositions.FQFN_PREFIX_START_POS   = {0}" , PathPositions.FQFN_PREFIX_START_POS );
            Console.WriteLine ( "    Public Constant PathPositions.FQFN_PREFIX_START_LEN   = {0}" , PathPositions.FQFN_PREFIX_START_LEN );
            Console.WriteLine ( "    Public Constant PathPositions.MAX_PATH                = {0}" , PathPositions.MAX_PATH );
            Console.WriteLine ( "    Public Constant PathPositions.UNC_HOSTNAME_PREFIX_POS = {0}" , PathPositions.UNC_HOSTNAME_PREFIX_POS );
            Console.WriteLine ( "    Public Constant PathPositions.UNC_HOSTNAME_START_POS  = {0}" , PathPositions.UNC_HOSTNAME_START_POS );

            return TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // PathPositionsExercises method


        internal static int SpecialCharactersExercises ( ref int pintTestNumber )
        {
            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine ( "    Public Constant SpecialCharacters.CARRIAGE_RETURN       = [CR] (ASCII code = {0,2:N0} (0x{1}){2}" , ( int ) SpecialCharacters.CARRIAGE_RETURN , ( ( int ) SpecialCharacters.CARRIAGE_RETURN ).ToString ( DisplayFormats.HEXADECIMAL_2 ) , Environment.NewLine );
            Console.WriteLine ( "    Public Constant SpecialCharacters.LINEFEED              = [LF] (ASCII code = {0,2:N0} (0x{1}){2}" , ( int ) SpecialCharacters.LINEFEED , ( ( int ) SpecialCharacters.LINEFEED ).ToString ( DisplayFormats.HEXADECIMAL_2 ) , Environment.NewLine );

            Console.WriteLine ( "    Public Constant SpecialCharacters.NUL_CHAR              = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.NULL_CHAR , ( int ) SpecialCharacters.NULL_CHAR , ( ( int ) SpecialCharacters.NULL_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.NONBREAKING_SPACE     = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.NONBREAKING_SPACE_CHAR , ( int ) SpecialCharacters.NONBREAKING_SPACE_CHAR , ( ( int ) SpecialCharacters.NONBREAKING_SPACE_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHECK_MARK_CHAR       = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHECK_MARK_CHAR , ( int ) SpecialCharacters.CHECK_MARK_CHAR , ( ( int ) SpecialCharacters.CHECK_MARK_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.LAST_ASCII_CHAR       = {0} (ASCII code = {1,2:N0} (0x{2}){3}" , SpecialCharacters.LAST_ASCII_CHAR , ( int ) SpecialCharacters.LAST_ASCII_CHAR , ( ( int ) SpecialCharacters.LAST_ASCII_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) , Environment.NewLine ); ;

            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_LC_I             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_LC_I , ( int ) SpecialCharacters.CHAR_LC_I , ( ( int ) SpecialCharacters.CHAR_LC_I ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_LC_L             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_LC_L , ( int ) SpecialCharacters.CHAR_LC_L , ( ( int ) SpecialCharacters.CHAR_LC_L ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_LC_O             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_LC_O , ( int ) SpecialCharacters.CHAR_LC_O , ( ( int ) SpecialCharacters.CHAR_LC_O ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_LC_Z             = {0} (ASCII code = {1,2:N0} (0x{2}){3}" , SpecialCharacters.CHAR_LC_Z , ( int ) SpecialCharacters.CHAR_LC_Z , ( ( int ) SpecialCharacters.CHAR_LC_Z ).ToString ( DisplayFormats.HEXADECIMAL_2 ) , Environment.NewLine );

            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_UC_I             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_UC_I , ( int ) SpecialCharacters.CHAR_UC_I , ( ( int ) SpecialCharacters.CHAR_UC_I ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_UC_L             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_UC_L , ( int ) SpecialCharacters.CHAR_UC_L , ( ( int ) SpecialCharacters.CHAR_UC_L ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_UC_O             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_UC_O , ( int ) SpecialCharacters.CHAR_UC_O , ( ( int ) SpecialCharacters.CHAR_UC_O ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_UC_Z             = {0} (ASCII code = {1,2:N0} (0x{2}){3}" , SpecialCharacters.CHAR_UC_Z , ( int ) SpecialCharacters.CHAR_UC_Z , ( ( int ) SpecialCharacters.CHAR_UC_Z ).ToString ( DisplayFormats.HEXADECIMAL_2 ) , Environment.NewLine );

            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_NUMERAL_0        = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_NUMERAL_0 , ( int ) SpecialCharacters.CHAR_NUMERAL_0 , ( ( int ) SpecialCharacters.CHAR_NUMERAL_0 ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_NUMERAL_1        = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_NUMERAL_1 , ( int ) SpecialCharacters.CHAR_NUMERAL_1 , ( ( int ) SpecialCharacters.CHAR_NUMERAL_1 ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_NUMERAL_2        = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.CHAR_NUMERAL_2 , ( int ) SpecialCharacters.CHAR_NUMERAL_2 , ( ( int ) SpecialCharacters.CHAR_NUMERAL_2 ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.CHAR_NUMERAL_7        = {0} (ASCII code = {1,2:N0} (0x{2}){3}" , SpecialCharacters.CHAR_NUMERAL_7 , ( int ) SpecialCharacters.CHAR_NUMERAL_7 , ( ( int ) SpecialCharacters.CHAR_NUMERAL_7 ).ToString ( DisplayFormats.HEXADECIMAL_2 ) , Environment.NewLine );

            Console.WriteLine ( "    Public Constant SpecialCharacters.AMPERSAND             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.AMPERSAND , ( int ) SpecialCharacters.AMPERSAND , ( ( int ) SpecialCharacters.AMPERSAND ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.ASTERISK              = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.ASTERISK , ( int ) SpecialCharacters.ASTERISK , ( ( int ) SpecialCharacters.ASTERISK ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.ASTERISK_CHAR         = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.ASTERISK_CHAR , ( int ) SpecialCharacters.ASTERISK_CHAR , ( ( int ) SpecialCharacters.ASTERISK_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.AT_CHAR               = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.AT_CHAR , ( int ) SpecialCharacters.AT_CHAR , ( ( int ) SpecialCharacters.AT_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.AT_SIGN               = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.AT_SIGN , ( int ) SpecialCharacters.AT_SIGN , ( ( int ) SpecialCharacters.AT_SIGN ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.BRACE_LEFT            = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.BRACE_LEFT , ( int ) SpecialCharacters.BRACE_LEFT , ( ( int ) SpecialCharacters.BRACE_LEFT ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.BRACE_RIGHT           = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.BRACE_RIGHT , ( int ) SpecialCharacters.BRACE_RIGHT , ( ( int ) SpecialCharacters.BRACE_RIGHT ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.BRACKET_LEFT          = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.BRACKET_LEFT , ( int ) SpecialCharacters.BRACKET_LEFT , ( ( int ) SpecialCharacters.BRACKET_LEFT ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.BRACKET_RIGHT         = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.BRACKET_RIGHT , ( int ) SpecialCharacters.BRACKET_RIGHT , ( ( int ) SpecialCharacters.BRACKET_RIGHT ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.DLM_FORMAT_ITEM_BEGIN = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.DLM_FORMAT_ITEM_BEGIN , ( int ) SpecialCharacters.DLM_FORMAT_ITEM_BEGIN , ( ( int ) SpecialCharacters.DLM_FORMAT_ITEM_BEGIN ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.COLON                 = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.COLON , ( int ) SpecialCharacters.COLON , ( ( int ) SpecialCharacters.COLON ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.COMMA                 = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.COMMA , ( int ) SpecialCharacters.COMMA , ( ( int ) SpecialCharacters.COMMA ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.DOUBLE_QUOTE          = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.DOUBLE_QUOTE , ( int ) SpecialCharacters.DOUBLE_QUOTE , ( ( int ) SpecialCharacters.DOUBLE_QUOTE ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.ENV_STR_DLM           = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.ENV_STR_DLM , ( int ) SpecialCharacters.ENV_STR_DLM , ( ( int ) SpecialCharacters.ENV_STR_DLM ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.EQUALS_SIGN           = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.EQUALS_SIGN , ( int ) SpecialCharacters.EQUALS_SIGN , ( ( int ) SpecialCharacters.EQUALS_SIGN ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.FULL_STOP             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.FULL_STOP , ( int ) SpecialCharacters.FULL_STOP , ( ( int ) SpecialCharacters.FULL_STOP ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.HASH_TAG              = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.HASH_TAG , ( int ) SpecialCharacters.HASH_TAG , ( ( int ) SpecialCharacters.HASH_TAG ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.HYPHEN                = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.HYPHEN , ( int ) SpecialCharacters.HYPHEN , ( ( int ) SpecialCharacters.HYPHEN ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.PARENTHESIS_LEFT      = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.PARENTHESIS_LEFT , ( int ) SpecialCharacters.PARENTHESIS_LEFT , ( ( int ) SpecialCharacters.PARENTHESIS_LEFT ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.PARENTHESIS_RIGHT     = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.PARENTHESIS_RIGHT , ( int ) SpecialCharacters.PARENTHESIS_RIGHT , ( ( int ) SpecialCharacters.PARENTHESIS_RIGHT ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );			
            Console.WriteLine ( "    Public Constant SpecialCharacters.PERCENT_SIGN          = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.PERCENT_SIGN , ( int ) SpecialCharacters.PERCENT_SIGN , ( ( int ) SpecialCharacters.PERCENT_SIGN ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.PIPE_CHAR             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.PIPE_CHAR , ( int ) SpecialCharacters.PIPE_CHAR , ( ( int ) SpecialCharacters.PIPE_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.QUESTION_MARK         = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.QUESTION_MARK , ( int ) SpecialCharacters.QUESTION_MARK , ( ( int ) SpecialCharacters.QUESTION_MARK ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.SEMICOLON             = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.SEMICOLON , ( int ) SpecialCharacters.SEMICOLON , ( ( int ) SpecialCharacters.SEMICOLON ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.SINGLE_QUOTE          = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.SINGLE_QUOTE , ( int ) SpecialCharacters.SINGLE_QUOTE , ( ( int ) SpecialCharacters.SINGLE_QUOTE ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.SPACE                 = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.SPACE_CHAR , ( int ) SpecialCharacters.SPACE_CHAR , ( ( int ) SpecialCharacters.SPACE_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.TAB                   = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.TAB_CHAR , ( int ) SpecialCharacters.TAB_CHAR , ( ( int ) SpecialCharacters.TAB_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );
            Console.WriteLine ( "    Public Constant SpecialCharacters.UNDERSCORE_CHAR       = {0} (ASCII code = {1,2:N0} (0x{2})" , SpecialCharacters.UNDERSCORE_CHAR , ( int ) SpecialCharacters.UNDERSCORE_CHAR , ( ( int ) SpecialCharacters.UNDERSCORE_CHAR ).ToString ( DisplayFormats.HEXADECIMAL_2 ) );

            return TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // SpecialCharactersExercises method


        internal static int UtilsExercises ( ref int pintTestNumber )
        {
            const string TEST_FILENAME_1 = @"WEBPMTS.TXT";
            const string TEST_FILENAME_2 = @"WEBPMTS.PSV";

            const string TEST_MASK_1 = @"WEBPMTS\.*";
            const string TEST_MASK_2 = @"WEBPMTS.TXT";

            const int    REG_DWORD_DEFAULT	  = 0;

            const long   REG_QWORD_DEFAULT	  = 0;

            const bool   REGISTRY_WRITING_OFF = false;

            const string REGISTRY_KEY_1 = @"Control Panel\Desktop\WindowMetrics";												// in HKEY_CURRENT_USER
            const string REGISTRY_KEY_2 = @"Control Panel\International";														// in HKEY_CURRENT_USER
            const string REGISTRY_KEY_3 = @"SYSTEM\CurrentControlSet\Control\Session Manager";									// in HKEY_LOCAL_MACHINE
            //	To keep the key and value string names synced, strings REGISTRY_KEY_4 and REGISTRY_5 are reserved, but unused.
            const string REGISTRY_KEY_6 = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";                      // in HKEY_LOCAL_MACHINE
            const string REGISTRY_KEY_7 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\StorageSense\StorageHealth";				// in HKEY_LOCAL_MACHINE
            const string REGISTRY_KEY_8 = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones\Central Standard Time";		// in HKEY_LOCAL_MACHINE

            const string REGISTRY_VALUE_1 = @"AppliedDPI";																		// Value exists, and its type is REG_DWORD.
            const string REGISTRY_VALUE_2 = @"iDigits";																			// Value exists, but its type is REG_SZ, rather than the expected REG_DWORD.
            const string REGISTRY_VALUE_3 = @"ObjectDirectories";																// This REG_MULTI_SZ value, which lives in Registry key HKLM\SYSTEM\CurrentControlSet\Control\Session Manager, contains two substrings.
            const string REGISTRY_VALUE_4 = @"SETUPEXECUTE";																	// This REG_MULTI_SZ value, which lives in Registry key HKLM\SYSTEM\CurrentControlSet\Control\Session Manager, contains ZERO substrings.
            const string REGISTRY_VALUE_5 = @"RunLevelExecute";																	// This REG_MULTI_SZ value doesn't exist in Registry key HKLM\SYSTEM\CurrentControlSet\Control\Session Manager.
            const string REGISTRY_VALUE_6 = @"ComSpec";																			// This REG_EXPAND_SZ value exists, and is of the expected type.
            const string REGISTRY_VALUE_7 = @"refreshAfter";
            const string REGISTRY_VALUE_8 = @"TZI";

            BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber );

            Console.WriteLine (
                Properties.Resources.REGEXP_TEST_ALL_BUT_LAST ,					// Format Control String
                RegExpSupport.MatchFileName (
                    TEST_FILENAME_1 ,
                    TEST_MASK_1 ) ,												// Format Item 0 = Files matching mask
                TEST_FILENAME_1 ,												// Format Item 1 = File name from which mask was constructed
                TEST_MASK_1 );													// Format Item 2 = Regular Expression match expression generated from filename
            Console.WriteLine (
                Properties.Resources.REGEXP_TEST_ALL_BUT_LAST ,					// Format Control String
                RegExpSupport.MatchFileName (
                    TEST_FILENAME_1 ,
                    TEST_MASK_2 ) ,												// Format Item 0 = Files matching mask
                TEST_FILENAME_1 ,												// Format Item 1 = File name from which mask was constructed
                TEST_MASK_2 );													// Format Item 2 = Regular Expression match expression generated from filename
            Console.WriteLine (
                Properties.Resources.REGEXP_TEST_ALL_BUT_LAST ,					// Format Control String
                RegExpSupport.MatchFileName (
                    TEST_FILENAME_2 ,
                    TEST_MASK_1 ) ,												// Format Item 0 = Files matching mask
                TEST_FILENAME_2 ,												// Format Item 1 = File name from which mask was constructed
                TEST_MASK_1 );													// Format Item 2 = Regular Expression match expression generated from filename

            Console.WriteLine (
                Properties.Resources.REGEXP_TEST_LAST ,							// Format Control String
                new object [ ]
                {
                    RegExpSupport.MatchFileName (
                        TEST_FILENAME_2 ,
                        TEST_MASK_2 ) ,											// Format Item 0 = Files matching mask
                    TEST_FILENAME_2 ,											// Format Item 1 = File name from which mask was constructed
                    TEST_MASK_2 ,												// Format Item 2 = Regular Expression match expression generated from filename
                    Environment.NewLine											// Format Item 3 = Newline, my way
                } );

            Program.PauseForPictures (
                Program.APPEND_LINEFEED ,
                @"Regular Expression Helpers" );

            //  ----------------------------------------------------------------
            //  Since GetDisplayTimeZone uses GetSystemTimeZoneInfo, this test
            //  case covers both.
            //  ----------------------------------------------------------------

#if NET35
            Console.WriteLine ( "{2}Current Local Time Zone for Machine {0} = {1}{2}" ,     // Message Template
                Environment.MachineName ,                                                   // Format Item 0 = Machine Name
                WizardWrx.SysDateFormatters.GetDisplayTimeZone (                                // Format Item 1 = Time Zone
                    DateTime.Now ,                                                              // DateTime pdtmTestDate = Current time from clock
                    TimeZoneInfo.Local.Id ) ,                                                   // string pstrTimeZoneID = ID of Local Machine time zone, per TimeZoneInfo
                Environment.NewLine );                                                      // Format Item 2 = Newline, my way
#endif	// #if NET35

            {   // Constrain the scope of Microsoft.Win32.RegistryKey hK and string strTpl.
                string strTpl					= Properties.Resources.MSG_REG_KEY_VALUE;
                Microsoft.Win32.RegistryKey hK	= Microsoft.Win32.Registry.CurrentUser.OpenSubKey (
                    REGISTRY_KEY_1 ,
                    REGISTRY_WRITING_OFF );

                Console.WriteLine (
                    strTpl ,
                    new string [ ]
                    {
                        hK.Name ,												                    // Format Item 0 = Key Name
                        REGISTRY_VALUE_1 ,										                    // Format Item 1 = Value Name
                        WizardWrx.Common.Properties.Resources.REGISTRY_VALUE_TYPE_DWORD ,           // Format Item 2 = Value Type
                        RegistryValues.RegQueryValue (
                            hK ,
                            REGISTRY_VALUE_1 ,
                            REG_DWORD_DEFAULT ).ToString ( ) ,					                    // Format Item 3 = Value Data
                        Environment.NewLine										                    // Format Item 4 = Newline my way
                    } );

                hK.Close ( );
                hK.Dispose ( );
                hK = null;

                hK = Microsoft.Win32.Registry.CurrentUser.OpenSubKey (
                    REGISTRY_KEY_2 ,
                    REGISTRY_WRITING_OFF );

                Console.WriteLine (
                    strTpl ,
                    new string [ ]
                    {
                        hK.Name ,												                    // Format Item 0 = Key Name
                        REGISTRY_VALUE_2 ,										                    // Format Item 1 = Value Name
                        WizardWrx.Common.Properties.Resources.REGISTRY_VALUE_TYPE_DWORD ,			// Format Item 2 = Value Type
                        RegistryValues.RegQueryValue (
                            hK ,
                            REGISTRY_VALUE_2 ,
                            REG_DWORD_DEFAULT ).ToString ( ) ,					                    // Format Item 3 = Value Data
                        Environment.NewLine										                    // Format Item 4 = Newline my way
                    } );

                hK.Close ( );
                hK.Dispose ( );
                hK = null;

                hK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (
                    REGISTRY_KEY_6 ,
                    REGISTRY_WRITING_OFF );

                Console.WriteLine (
                    strTpl ,
                    new object [ ]
                    {
                        hK.Name ,												                    // Format Item 0 = Key Name
                        REGISTRY_VALUE_6 ,										                    // Format Item 1 = Value Name
                        WizardWrx.Common.Properties.Resources.REGISTRY_VALUE_TYPE_EXPAND ,			// Format Item 2 = Value Type
                        RegistryValues.RegQueryValue (
                            hK ,
                            REGISTRY_VALUE_6 ,
                            SpecialStrings.EMPTY_STRING ) ,						                    // Format Item 3 = Value Data
                        Environment.NewLine										                    // Format Item 4 = Newline my way
                    } );

                hK.Close ( );
                hK.Dispose ( );
                hK = null;

                Program.PauseForPictures (
                    Program.APPEND_LINEFEED ,
                    @"Registry Key Query and Value Formatters, Group 1 of 3" );

                string [ ] astrValueNames = new string [ ]
                    {
                        REGISTRY_VALUE_3 ,
                        REGISTRY_VALUE_4 ,
                        REGISTRY_VALUE_5
                    };  // string [ ] astrValueNames = new string [ ]

                hK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (
                    REGISTRY_KEY_3 ,
                    REGISTRY_WRITING_OFF );

                foreach ( string strValue in astrValueNames )
                {
                    string [ ] astrIndividualValues = RegistryValues.RegQueryValue (
                        hK ,
                        strValue );
                    int intNValues = astrIndividualValues.Length;

                    Console.WriteLine (
                        strTpl ,
                        new string [ ]
                    {
                        hK.Name ,												                    // Format Item 0 = Key Name
                        strValue ,												                    // Format Item 1 = Value Name
                        WizardWrx.Common.Properties.Resources.REGISTRY_VALUE_TYPE_MULTI ,			// Format Item 2 = Value Type
                        string.Concat (
                            intNValues ,
                            Properties.Resources.MSG_SUBSTRING_SUMMARY_1 ,
                            intNValues > ArrayInfo.ARRAY_IS_EMPTY
                                ? Properties.Resources.MSG_SUBSTRING_SUMMARY_2
                                : SpecialStrings.EMPTY_STRING ) ,				                    // Format Item 3 = Value Data
                        Environment.NewLine										                    // Format Item 4 = Newline my way, but only if substrings follow.
                    } );

                    //  --------------------------------------------------------
                    //  This loop exercises methods IsFirstForIteration and
                    //  IsLastForIterationLE.
                    //  --------------------------------------------------------

                    for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                              intIndex < intNValues ;
                              intIndex++ )
                    {
                        Console.WriteLine (
                            Properties.Resources.MSG_SUBSTRING_VALUE ,
                            new object [ ]
                            {	// These ternary expressions use some of the routines under test.
                                ArrayInfo.OrdinalFromIndex ( intIndex ) ,		// Format Item 0 = Ordinal Position of Substring
                                astrIndividualValues [ intIndex ] ,				// Format Item 1 = Substring Value
                                Logic.IsLastForIterationLT ( intIndex ,
                                                             intNValues )
                                    ? Environment.NewLine
                                    : SpecialStrings.EMPTY_STRING				// Format Item 2 = Newline on last item, otherwise, nothing
                            } );
                    }   // for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < intNValues ; intNValues++ )
                }   // foreach ( string strValue in astrValueNames )

                hK.Close ( );
                hK.Dispose ( );
                hK = null;

                Program.PauseForPictures (
                    Program.APPEND_LINEFEED ,
                    @"Registry Key Query and Value Formatters, Group 2 of 3" );

                hK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (
                    REGISTRY_KEY_7 ,
                    REGISTRY_WRITING_OFF );
                Console.WriteLine (
                    strTpl ,
                    new object [ ]
                    {
                        hK.Name ,												                    // Format Item 0 = Key Name
                        REGISTRY_VALUE_7 ,										                    // Format Item 1 = Value Name
                        WizardWrx.Common.Properties.Resources.REGISTRY_VALUE_TYPE_QWORD ,			// Format Item 2 = Value Type
                        ( long ) RegistryValues.RegQueryValue (
                            hK ,
                            REGISTRY_VALUE_7 ,
                            REG_QWORD_DEFAULT ) ,								                    // Format Item 3 = Value Data
                        Environment.NewLine										                    // Format Item 4 = Newline my way
                    } );

                hK.Close ( );
                hK.Dispose ( );
                hK = null;

                hK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (
                    REGISTRY_KEY_8 ,
                    REGISTRY_WRITING_OFF );
                Console.WriteLine (
                    strTpl ,
                    new string [ ]
                    {
                        hK.Name ,												                    // Format Item 0 = Key Name
                        REGISTRY_VALUE_8 ,										                    // Format Item 1 = Value Name
                        WizardWrx.Common.Properties.Resources.REGISTRY_VALUE_TYPE_BINARY ,			// Format Item 2 = Value Type
                        ByteArrayFormatters.ByteArrayToHexDigitString (
                            RegistryValues.RegQueryValue (
                                hK ,
                                REGISTRY_VALUE_8 ,
                                RegistryValues.REG_BINARY_NULL_FOR_ABSENT ) ,
                            ByteArrayFormatters.BYTES_TO_STRING_BLOCK_OF_4 ) ,	                    // Format Item 3 = Value Data
                        Environment.NewLine										                    // Format Item 4 = Newline my way
                    } );

                hK.Close ( );
                hK.Dispose ( );
                hK = null;
            }   // Microsoft.Win32.RegistryKey hK and string strTpl go out of scope.

            //	----------------------------------------------------------------
            //	Test the Unless idiom.
            //	----------------------------------------------------------------

            UnlessWhat ( );

            Program.PauseForPictures (
                Program.APPEND_LINEFEED ,
                @"Registry Key Query and Value Formatters, Group 3 of 3" );

            //	----------------------------------------------------------------
            //	Test the loop state evaluators.
            //	----------------------------------------------------------------

            EvaluateLoopState ( );

            return TestDone (
                WizardWrx.MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // UtilsExercises method
        #endregion  // Public Methods


        #region Private Enumerations and Instance Storage
        private const string ARGNAME_COND = @"LimitCondition";

        private enum LimitCondition
        {
            EqualTo ,
            GreaterThan ,
            GreaterThanOrEqualTo ,
            LessThanOrEqualTo ,
            LessThan
        }	// LimitCondition enumeration
        
        private enum LimitValue
        {
            Lower ,
            Upper
        }	// LimitValue enumeration

        static readonly LimitCondition [ ] s_aenmLimitConditions =
        {
            LimitCondition.LessThanOrEqualTo ,
            LimitCondition.LessThan ,
            LimitCondition.GreaterThan ,
            LimitCondition.GreaterThanOrEqualTo ,
            LimitCondition.EqualTo
        };	// s_aenmLimitConditions array

        static int s_intStopWhenEquals;
        static bool s_fEqualsFound = false;

        static readonly string [ ] s_astrLimitConditionMessages =
        {
            Properties.Resources.MSG_LOOP_STATE_EQ ,
            Properties.Resources.MSG_LOOP_STATE_GT ,
            Properties.Resources.MSG_LOOP_STATE_GE ,
            Properties.Resources.MSG_LOOP_STATE_LE ,
            Properties.Resources.MSG_LOOP_STATE_LT ,
        };	// static const string [ ] s_astrLimitConditionMessages

        //  --------------------------------------------------------------------
        //  Once populated by the static constructor, the BeginTest method uses
        //  this dictionary to look up the name of the class processed by the
        //  calling method.
        //  --------------------------------------------------------------------

        static Dictionary<string , string> s_dctClassTestMap;
        #endregion	// Private Enumerations and Instance Storage


        #region Private Methods
        private static bool AreWeDoneYet (
            LimitCondition penmLimitCondition ,
            int pintCurrent ,
            int pintLoopLimit )
        {
            switch ( penmLimitCondition )
            {
                case LimitCondition.EqualTo:
                    if ( pintCurrent == pintLoopLimit )
                    {
                        if ( s_fEqualsFound )
                        {	// When the last iteration found the match, we are done.
                            s_fEqualsFound = false;		// Clear the flag, since it's static, and, threfore, survives the loop.
                            return false;
                        }	// TRUE block, if ( s_fEqualsFound )
                        else
                        {	// Since the condition test is at the top of a FOR loop, the code inside the block hasn't yet executed.
                            s_fEqualsFound = true;
                            return true;
                        }	// FALSE block, if ( s_fEqualsFound )
                    }	// TRUE (Match found) block, if ( pintCurrent == pintLoopLimit )
                    else
                    {
                        return true;
                    }	// FALSE (no match yet) block, if ( pintCurrent == pintLoopLimit )
                case LimitCondition.GreaterThan:
                    return pintCurrent > pintLoopLimit;
                case LimitCondition.GreaterThanOrEqualTo:
                    return pintCurrent >= pintLoopLimit;
                case LimitCondition.LessThan:
                    return pintCurrent < pintLoopLimit;
                case LimitCondition.LessThanOrEqualTo:
                    return pintCurrent <= pintLoopLimit;
                default:
                    throw new ArgumentOutOfRangeException (
                        ARGNAME_COND ,
                        penmLimitCondition ,
                        Properties.Resources.ERRMSG_LIMIT_CONDITON );
            }	// switch ( penmLimitCondition )
        }   // AreeWeDoneYet method


        /// <summary>
        /// Increment <paramref name="pintTestNumber"/>, then log a console
        /// message that uses <paramref name="pstrMethodName"/> to identify the
        /// test.
        /// </summary>
        /// <param name="pstrMethodName">
        /// The newer tests use this parameter directly, while the older ones
        /// look it up in a static dictionary that allows it to avoid using
        /// Reflection to get it.
        /// <para>
        /// To invoke the newer method, set <paramref name="pfIgnoreClassTestMap"/>
        /// to TRUE.
        /// </para>
        /// </param>
        /// <param name="pintTestNumber">
        /// The test number is passed in by reference, so that the routine can
        /// be put in charge of incrementing it. Before the first call, this
        /// parameter must be initialized to zero; since this is the default
        /// initial value for an integer, explicit initialization is recommended
        /// but optional.
        /// </param>
        /// <param name="pfIgnoreClassTestMap">
        /// Set this parameter to TRUE to cause <paramref name="pstrMethodName"/>
        /// to be taken at face value. Omitting this parameter or setting it
        /// explicitly to FALSE causes the <paramref name="pstrMethodName"/> to
        /// be treated as the index into a static array of method names that is
        /// hard coded into the program.
        /// </param>
        internal static void BeginTest (
            string pstrMethodName ,
            ref int pintTestNumber ,
            bool pfIgnoreClassTestMap = false )
        {
            const string ERRMSG_UNDEFINED_METHOD = @"Method {0} is undefined.";
            const string MSG_BEGIN = @"{2}Test # {0} - Exercising class {1}:{2}";

            if ( pfIgnoreClassTestMap )
            {
                Console.WriteLine (
                    MSG_BEGIN ,                             // Message Template
                    ++pintTestNumber ,                      // Format Item 0 = Test Number - Increment, then print
                    pstrMethodName ,                        // Format Item 1 = Method Name per WizardWrx.DiagnosticInfo, which gathers the information at compile time
                    Environment.NewLine );                  // Format Item 2 = Newline
            }   // TRUE (Since the method name is available by more reliable means, ignore the ClassTestMap dictionary.) block, if ( pfIgnoreClassTestMap )
            else
            {
                if ( s_dctClassTestMap.ContainsKey ( pstrMethodName ) )
                {
                    Console.WriteLine (
                        MSG_BEGIN ,                             // Message Template
                        ++pintTestNumber ,                      // Format Item 0 = Test Number - Increment, then print
                        s_dctClassTestMap [ pstrMethodName ] ,  // Format Item 1 = Method Name per System.Reflection
                        Environment.NewLine );                  // Format Item 2 = Newline
                }   // TRUE (expected outcome) block, if ( s_dctClassTestMap.ContainsKey ( pstrMethodName ) )
                else
                {
                    throw new Exception ( string.Format (
                        ERRMSG_UNDEFINED_METHOD ,
                        pstrMethodName ) );
                }   // FALSE (UNexpected outcome) block, if ( s_dctClassTestMap.ContainsKey ( pstrMethodName ) )
            }   // FALSE (The legacy tests rely on the ClassTestMap dictionary.) block, if ( pfIgnoreClassTestMap )
        }   // BeginTest


        private static void EvaluateLoopState ( )
        {
            const int LOOP_START_1_OF_3	= 1;
            const int LOOP_START_2_OF_3 = 0;
            const int LOOP_START_3_OF_3 = -10;

            const int LOOP_STOP_1_OF_3	= 10;
            const int LOOP_STOP_2_OF_3	= 12;
            const int LOOP_STOP_3_OF_3	= 5;

            int intNLimitConditions = s_aenmLimitConditions.Length;
            int intLimitOrdinal = MagicNumbers.ZERO;

            foreach ( LimitCondition enmLimitCondition in s_aenmLimitConditions )
            {
                intLimitOrdinal++;
                ExerciseLoopStateEvaluators ( enmLimitCondition , SetStartStop ( enmLimitCondition , LimitValue.Lower , LOOP_START_1_OF_3 , LOOP_STOP_1_OF_3 ) , SetStartStop ( enmLimitCondition , LimitValue.Upper , LOOP_START_1_OF_3 , LOOP_STOP_1_OF_3 ) , true );
                ExerciseLoopStateEvaluators ( enmLimitCondition , SetStartStop ( enmLimitCondition , LimitValue.Lower , LOOP_START_2_OF_3 , LOOP_STOP_2_OF_3 ) , SetStartStop ( enmLimitCondition , LimitValue.Upper , LOOP_START_2_OF_3 , LOOP_STOP_2_OF_3 ) , true );
                ExerciseLoopStateEvaluators ( enmLimitCondition , SetStartStop ( enmLimitCondition , LimitValue.Lower , LOOP_START_3_OF_3 , LOOP_STOP_3_OF_3 ) , SetStartStop ( enmLimitCondition , LimitValue.Upper , LOOP_START_3_OF_3 , LOOP_STOP_3_OF_3 ) , intLimitOrdinal != intNLimitConditions );
            }	// foreach ( LimitCondition enmLimitCondition in s_aenmLimitConditions )
        }	// EvaluateLoopState method


        private static void ExerciseLoopStateEvaluators (
            LimitCondition penmLimitCondition ,
            int pintLoopStart ,
            int pintLoopLimit ,
            bool pfPauseForPictures )
        {
            const string COLUMN_HEADER = @"-----";

            Console.WriteLine (
                s_astrLimitConditionMessages [ ( int ) penmLimitCondition ] ,
                Environment.NewLine );
            Console.WriteLine (
                Properties.Resources.MSG_LOOP_LIMIT_VALUES ,
                pintLoopStart ,
                pintLoopLimit , Environment.NewLine );

            #if STRINGS_TO_CONSOLE
                string strDummy = string.Format (
                    Properties.Resources.MSG_LOOP_STATE_TABLE_LABELS ,
                    SpecialCharacters.TAB );
                Console.WriteLine ( strDummy ) ;
            #else
                Console.WriteLine (
                    Properties.Resources.MSG_LOOP_STATE_TABLE_LABELS ,
                    SpecialCharacters.TAB_CHAR );
            #endif // #if STRINGS_TO_CONSOLE

            Console.WriteLine (
                Properties.Resources.MSG_LOOP_STATE_TABLE_DATA ,										// Format control string
                new string [ ]																			// Array of format items (>3 items)
                    {
                        COLUMN_HEADER ,																	// Format Item 0 = Index
                        COLUMN_HEADER ,																	// Format Item 1 = First
                        COLUMN_HEADER ,																	// Format Item 2 = Next
                        COLUMN_HEADER ,																	// Format Item 3 = More
                        COLUMN_HEADER ,																	// Format Item 4 = Last
                        SpecialCharacters.TAB_CHAR.ToString ( )												// Format Item 5 = TAB character
                    } );

            for ( int intCurrent = pintLoopStart ;
                      AreWeDoneYet ( penmLimitCondition , intCurrent , pintLoopLimit ) ;
                      NextIteration ( penmLimitCondition , intCurrent , out intCurrent ) )
            {
                Console.WriteLine (
                    Properties.Resources.MSG_LOOP_STATE_TABLE_DATA ,									// Format control string
                    new string [ ]																		// Array of format items (>3 items)
                    {
                        intCurrent.ToString ( ) ,														// Format Item 0 = Index
                        Logic.IsFirstForIteration ( intCurrent , pintLoopStart ) .ToString ( ) ,		// Format Item 1 = First
                        Logic.IsNextForIteration ( intCurrent , pintLoopStart ).ToString ( ) ,			// Format Item 2 = Next
                        MoreIterations ( penmLimitCondition , intCurrent , pintLoopLimit ) ,			// Format Item 3 = More
                        LastIteration  ( penmLimitCondition , intCurrent , pintLoopLimit ) ,			// Format Item 4 = Last
                        SpecialCharacters.TAB_CHAR.ToString ( )												// Format Item 5 = TAB character
                    } );
            }	// for ( int intCurrent = pintLoopStart ; AreeWeDoneYet ( penmLimitCondition , intCurrent , pintLoopLimit ) ; NextIteration ( penmLimitCondition , intCurrent , out intCurrent ) )

            if ( pfPauseForPictures )
            {   // Stop all but the last time through.
                Program.PauseForPictures (
                    Program.OMIT_LINEFEED ,
                    @"ExerciseLoopStateEvaluators" );
            }	// if ( pfPauseForPictures )
        }	// ExerciseLoopStateEvaluators method


        private static string LastIteration (
            LimitCondition penmLimitCondition ,
            int pintCurrent ,
            int pintLoopLimit )
        {
            switch ( penmLimitCondition )
            {
                case LimitCondition.LessThanOrEqualTo:
                    return Logic.IsLastForIterationLE ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.LessThan:
                    return Logic.IsLastForIterationLT ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.GreaterThanOrEqualTo:
                    return Logic.IsLastForIterationGE ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.GreaterThan:
                    return Logic.IsLastForIterationGT ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.EqualTo:
                    return Logic.IsLastForIterationEQ ( pintCurrent , pintLoopLimit ).ToString ( );
                default:
                    return SpecialStrings.EMPTY_STRING;
            }	// switch ( penmLimitCondition )
        }	// LastIteration method


        private static string MoreIterations (
            LimitCondition penmLimitCondition ,
            int pintCurrent ,
            int pintLoopLimit )
        {
            switch ( penmLimitCondition )
            {
                case LimitCondition.LessThanOrEqualTo:
                    return Logic.MoreForIterationsToComeLE ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.LessThan:
                    return Logic.MoreForIterationsToComeLT ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.GreaterThanOrEqualTo:
                    return Logic.MoreForIterationsToComeGE ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.GreaterThan:
                    return Logic.MoreForIterationsToComeGT ( pintCurrent , pintLoopLimit ).ToString ( );
                case LimitCondition.EqualTo:
                    return Logic.MoreForIterationsToComeEQ ( pintCurrent , pintLoopLimit ).ToString ( );
                default:
                    return SpecialStrings.EMPTY_STRING;
            }	// switch ( penmLimitCondition )
        }	// MoreIterations method


        private static void NextIteration (
            LimitCondition penmLimitCondition ,
            int pintOldCurrent ,
            out	int pintNewCurrent )
        {
            switch ( penmLimitCondition )
            {
                case LimitCondition.LessThanOrEqualTo:
                case LimitCondition.LessThan:
                    pintNewCurrent = pintOldCurrent + 1;
                    break;
                case LimitCondition.GreaterThanOrEqualTo:
                case LimitCondition.GreaterThan:
                    pintNewCurrent = pintOldCurrent - 1;
                    break;
                case LimitCondition.EqualTo:
                    pintNewCurrent = s_intStopWhenEquals;
                    break;
                default:
                    throw new ArgumentOutOfRangeException (
                        ARGNAME_COND ,
                        penmLimitCondition ,
                        Properties.Resources.ERRMSG_LIMIT_CONDITON );
            }	// switch ( penmLimitCondition )
        }	// NextIteration method


        private static int SetStartStop (
            LimitCondition penmLimitCondition ,
            LimitValue penmLimitValue ,
            int pintLowerInteger ,
            int pintUpperInteger )
        {
            const string ARGNAME_COND = @"LimitCondition";
            const string ARGNAME_LIMIT = @"penmLimitValue";

            switch ( penmLimitCondition )
            {
                case LimitCondition.LessThanOrEqualTo:
                case LimitCondition.LessThan:
                    switch ( penmLimitValue )
                    {
                        case LimitValue.Lower:
                            return pintLowerInteger;
                        case LimitValue.Upper:
                            return pintUpperInteger;
                        default:
                            throw new ArgumentOutOfRangeException (
                                ARGNAME_LIMIT ,
                                penmLimitValue ,
                                Properties.Resources.ERRMSG_LIMIT_VALUE );
                    }	// switch ( penmLimitValue)
                case LimitCondition.GreaterThanOrEqualTo:
                case LimitCondition.GreaterThan:
                    switch ( penmLimitValue )
                    {
                        case LimitValue.Lower:
                            return pintUpperInteger;
                        case LimitValue.Upper:
                            return pintLowerInteger;
                        default:
                            throw new ArgumentOutOfRangeException (
                                ARGNAME_LIMIT ,
                                penmLimitValue ,
                                Properties.Resources.ERRMSG_LIMIT_VALUE );
                    }	// switch ( penmLimitValue)
                case LimitCondition.EqualTo:
                    switch ( penmLimitValue )
                    {
                        case LimitValue.Lower:
                            return pintLowerInteger;
                        case LimitValue.Upper:
                            s_intStopWhenEquals = pintUpperInteger;
                            return s_intStopWhenEquals;
                        default:
                            throw new ArgumentOutOfRangeException (
                                ARGNAME_LIMIT ,
                                penmLimitValue ,
                                Properties.Resources.ERRMSG_LIMIT_VALUE );
                    }	// switch ( penmLimitValue)
                default:
                    throw new ArgumentOutOfRangeException (
                        ARGNAME_COND ,
                        penmLimitCondition ,
                        Properties.Resources.ERRMSG_LIMIT_CONDITON );
            }	// switch ( penmLimitCondition )
        }	// SetStartStop method


        internal static int TestDone (
            int pintFinalStatusCode ,
            int pintTestNumber )
        {
            const string MSG_DONE = @"{2}Test # {0} Done - Final Status Code = {1}";

            Console.WriteLine ( 
                MSG_DONE ,                  // Message Template
                pintTestNumber ,            // Format Item 0 = Test Number
                pintFinalStatusCode ,       // Format Item 1 = Final Status Code
                Environment.NewLine );      // Format Item 2 = Newline
            return pintFinalStatusCode;
        }   // TestDone method


        private static void UnlessWhat ( )
        {
            const bool UNLESS_CASE_T = true;
            const bool UNLESS_CASE_F = false;

            Console.WriteLine (
                Properties.Resources.MSG_UNLESS_BEGIN ,
                Environment.NewLine );

            Console.WriteLine (
                Properties.Resources.MSG_UNLESS_WHAT ,
                UNLESS_CASE_T ,
                Logic.Unless ( UNLESS_CASE_T ) );
            Console.WriteLine (
                Properties.Resources.MSG_UNLESS_WHAT ,
                UNLESS_CASE_F ,
                Logic.Unless ( UNLESS_CASE_F ) );

            Console.WriteLine (
                Properties.Resources.MSG_UNLESS_END ,
                Environment.NewLine );
        }	// UnlessWhat method


        static NewClassTests_20140914 ( )
        {
            //  ----------------------------------------------------------------
            //  The first executable statement in this routine exercises the
            //  whole LoadTextFileFromCallingAssembly chain.
            //  ----------------------------------------------------------------

            const string CLASS_MAP_TABLE_NAME = @"ClassTestMap.TXT";

            const string ERRMSG_MISSING_CLASS_MAP_TABLE = @"Class map table resource {0} is invalid.";
            const string ERRMSG_INVALID_CLASS_MAP_LABELS = @"Class map table label row is invalid.{2}Labels found = {0}{2}Labels expected = {1}";
            const string ERRMSG_INVALID_CLASS_MAP_RECORD = @"Class map {0} record {1} field count is invalid.{4}Field Count found = {2}{4}Expected field count = {3}";
            const string ERRMSG_DUPLICATE_CLASS_MAP_KEY = @"Class map {0} record {1} method name field is a duplicate.{4}Method Name = {2}{4}Class Name = {3}";

            const int FIELD_ROUTINE_NAME = ArrayInfo.ARRAY_FIRST_ELEMENT;
            const int FIELD_CLASS_NAME = FIELD_ROUTINE_NAME + ArrayInfo.NEXT_INDEX;
            const int FIELD_EXPECTED_COUNT = FIELD_CLASS_NAME + ArrayInfo.ORDINAL_FROM_INDEX;

            const string VALID_LABEL_ROW = @"Method Name	Class Tested";

            string [ ] astrClassMap = WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly ( CLASS_MAP_TABLE_NAME );

            if ( Environment.GetCommandLineArgs ( ).Length == ListInfo.LIST_IS_EMPTY )
            {	// Run this only when the command line argument list is empty.
                ExerciseUtf8ResourceReader ( );
            }	// if ( Environment.GetCommandLineArgs ( ).Length == ListInfo.LIST_IS_EMPTY )

            int intNRecs = CSVFileInfo.RecordCount ( astrClassMap );

            if ( intNRecs > CSVFileInfo.EMPTY_FILE )
            {   // There is a label row and at least one data row.
                if ( astrClassMap [ CSVFileInfo.LABEL_ROW ] == VALID_LABEL_ROW )
                {   // The label row matches the design specification.
                    s_dctClassTestMap = new Dictionary<string , string> ( intNRecs );

                    for ( int intIndex = CSVFileInfo.FIRST_RECORD ;
                              intIndex <= intNRecs ;
                              intIndex++ )
                    {   // Process each record.
                        string [ ] astrFields = astrClassMap [ intIndex ].Split ( SpecialCharacters.TAB_CHAR );

                        if ( astrFields.Length == FIELD_EXPECTED_COUNT )
                        {   // Field count matches the design.
                            if ( s_dctClassTestMap.ContainsKey ( astrFields [ FIELD_ROUTINE_NAME ] ) )
                            {   // Croak on duplicate record.
                                throw new Exception ( string.Format (
                                    ERRMSG_DUPLICATE_CLASS_MAP_KEY ,   // Message template                    
                                    new object [ ]
                                {
                                    CLASS_MAP_TABLE_NAME ,              // Format Item 0 = Table File Name (per constant)
                                    intIndex ,                          // Format Item 1 = Record number
                                    astrFields [ FIELD_ROUTINE_NAME ] , // Format Item 2 = Reported routine name
                                    astrFields [ FIELD_CLASS_NAME ] ,   // Format Item 3 = Expected class name
                                    Environment.NewLine                 // Format Item 4 = Newline
                                } ) );
                            }   // TRUE (UNexpected outcome) block, if ( s_dctClassTestMap.ContainsKey ( astrFields [ FIELD_ROUTINE_NAME ] ) )
                            else
                            {   // Record is unique; add it to the list.
                                s_dctClassTestMap.Add (
                                    astrFields [ FIELD_ROUTINE_NAME ] ,
                                    astrFields [ FIELD_CLASS_NAME ] );
                            }   // FALSE (expected outcome) block, if ( s_dctClassTestMap.ContainsKey ( astrFields [ FIELD_ROUTINE_NAME ] ) )
                        }   // TRUE (expected outcome) block, if ( astrFields.Length == FIELD_EXPECTED_COUNT )
                        else
                        {   // Field count differs from specification.
                            throw new Exception ( string.Format (
                                ERRMSG_INVALID_CLASS_MAP_RECORD ,   // Message template                    
                                new object [ ]
                                {
                                    CLASS_MAP_TABLE_NAME ,          // Format Item 0 = Table File Name (per constant)
                                    intIndex ,                      // Format Item 1 = Record number
                                    astrFields.Length ,             // Format Item 2 = Reported field count
                                    FIELD_EXPECTED_COUNT ,          // Format Item 3 = Expected field count
                                    Environment.NewLine             // Format Item 4 = Newline
                                } ) );
                        }   // FALSE (UNexpected outcome) block, if ( astrFields.Length == FIELD_EXPECTED_COUNT )
                    }   // for ( int intIndex = CSVFileInfo.FIRST_RECORD ; intIndex <= intNRecs ; intIndex++ )
                }   // TRUE (expected outcome) block, if ( astrClassMap [ CSVFileInfo.LABEL_ROW ] == VALID_LABEL_ROW )
                else
                {   // Label row is invalid. File in file system and constants in this routine differ.
                    throw new Exception ( string.Format (
                        ERRMSG_INVALID_CLASS_MAP_LABELS ,
                        astrClassMap [ CSVFileInfo.LABEL_ROW ] ,
                        VALID_LABEL_ROW ,
                        Environment.NewLine ) );
                }   // FALSE (UNexpected outcome) block, if ( astrClassMap [ CSVFileInfo.LABEL_ROW ] == VALID_LABEL_ROW )
            }   // TRUE (expected outcome) block, if ( intNRecs > CSVFileInfo.EMPTY_FILE )
            else
            {   // The required resource is either absent or empty.
                throw new Exception ( string.Format (
                    ERRMSG_MISSING_CLASS_MAP_TABLE ,
                    CLASS_MAP_TABLE_NAME ) );
            }   // FALSE (UNexpected outcome) block, if ( intNRecs > CSVFileInfo.EMPTY_FILE )
        }   // NewClassTests_20140914 static constructor


        private static void ExerciseUtf8ResourceReader ( )
        {
            const string TEXT_FILE_WITH_UTF_8_BOM = @"TextWithUTF_8_BOM.TXT";
            const string MSG_UTF8_TEST_PREAMBLE = @"{1}Following is the content of {0}, an embedded UTF-8 encoded text file that has a Byte Order Mark.{1}";
            const string MSG_UTF8_TEST_LINE = @"    {0}: {1}";
            const string MSG_UTF8_TEST_EPILOGUE = @"{1}Test completed, line count = {0}{1}";

            Console.WriteLine (
                MSG_UTF8_TEST_PREAMBLE ,
                TEXT_FILE_WITH_UTF_8_BOM ,
                Environment.NewLine );
            int intNUtf8Lines = MagicNumbers.ZERO;

            foreach ( string strLorenIpsum in WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly ( TEXT_FILE_WITH_UTF_8_BOM ) )
            {
                Console.WriteLine (
                    MSG_UTF8_TEST_LINE ,
                    ++intNUtf8Lines ,
                    strLorenIpsum );
            }	// foreach ( string strLorenIpsum in WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly ( TEXT_FILE_WITH_UTF_8_BOM ) )

            Console.WriteLine ( MSG_UTF8_TEST_EPILOGUE , intNUtf8Lines , Environment.NewLine );
        }	// ExerciseUtf8ResourceReader method
        #endregion  // Private Methods
    }   // internal static class NewClassTests_20140914
}   // partial namespace DLLServices2TestStand