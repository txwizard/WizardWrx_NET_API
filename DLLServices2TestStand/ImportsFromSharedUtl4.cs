/*
    ============================================================================

    Module Name:        ImportsFromSharedUtl4.cs

    Namespace Name:     DLLServices2TestStand

    Class Name:         ImportsFromSharedUtl4

    Synopsis:           This module defines a static class for testing classes
						that were imported from the WizardWrx.SharedUtl4 class
						libraries.

    Remarks:            This class is composed of a set of static methods that
						originated in the WizardWrx.SharedUtl4.dll test stand
						and one visible method that launches them.

    License:            Copyright (C) 2012-2017, David A. Gray. 
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

    Created:            Monday, 19 November 2012 - Thursday, 22 November 2012

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2017/08/27 7.0     DAG    This class makes its appearance.
    2017/09/04 7.0     DAG    Fully integrate the test selection algorithm so it
                              works correctly in its new home.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.IO;

using WizardWrx;

namespace DLLServices2TestStand
{
	internal static class ImportsFromSharedUtl4
	{
		#region Public Interface
		internal const int STATUS_SKIP_FURTHER_TESTS = MagicNumbers.MINUS_ONE;

		internal static int DoTheTests ( string [ ] pastrProgramArgs )
		{
			InitializeTestSelections ( pastrProgramArgs );

			try
			{
				if ( DoThisTest ( CHOICE_MAXSTRINGLENGTH ) )
					MaxStringLength_Tests ( );

				if ( DoThisTest ( CHOICE_MERGENEWITEMSINTOARRAY ) )
					MergeNewItemsIntoArray_Tests ( );

				if ( DoThisTest ( CHOICE_REPORTDETAILS ) )
				{
					ReportDetailsTests ( TEST_UNSORTED );
					ReportDetailsTests ( TEST_SORTED );
				}	// if ( DoThisTest ( CHOICE_REPORTDETAILS ) )

				if ( DoThisTest ( CHOICE_REPORTHELPERS ) )
					ReportHelpersTests ( );

				if ( DoThisTest ( CHOICE_FORMAT_ITEM_GEN ) )
					FormatStringParsing_Drills.TestFormatItemBuilders ( );

				if ( DoThisTest ( CHOICE_ASCII_TABLE_GEN ) )
					FormatStringParsing_Drills.ASCII_Table_Gen ( );

				return s_fSkipRemainingTests
					? STATUS_SKIP_FURTHER_TESTS
					: MagicNumbers.ERROR_SUCCESS;
			}
			catch ( Exception exAllKinds )
			{
				Program.s_smTheApp.AppExceptionLogger.ReportException ( exAllKinds );
				return s_fSkipRemainingTests
					? STATUS_SKIP_FURTHER_TESTS
					: MagicNumbers.ERROR_RUNTIME;
			}
		}	// DoTheTests
		#endregion	// Public Interface


		#region Private Constants and Static Data
		const string IDS_MSG_BATCH = @"{2}{0} {1}{2}";		// In the SharedUtl4 test stand, this was a constant; I moved it out, and might move it to SpecialStrings.

		const bool RUN_REMAIING_TESTS = false;
		const bool SKIP_REMAINING_TESTS = true;

		const bool TEST_SORTED = true;
		const bool TEST_UNSORTED = false;	
		
		static WizardWrx.FormatStringEngine.FormatItem.Alignment [ ] s_aenmAlignment =
        {
            WizardWrx.FormatStringEngine.FormatItem.Alignment.Left ,
            WizardWrx.FormatStringEngine.FormatItem.Alignment.Right
        };  // private static FormatItem.Alignment [ ] s_aenmAlignment

		//	--------------------------------------------------------------------
		//	At compile time, the following strings are loaded into a static read
		//	only table (array), s_astrTestSelections. The integers that follow
		//	are subscripts into the table, which are passed to method DoThisTest
		//	to indicate which test is up next for selection to be executed or
		//	skipped. My thinking is that integers are more efficient than string
		//	pointers to pass around among subroutines, although they have the
		//	further advantage of reducing a brute force search to a simple
		//	lookup.
		//
		//	The line comment on each string is the name of the routine that runs
		//	when its test is selected. Unqualified methods are defined as static
		//	methods in this class.
		//
		//	The first constant, TEST_SHAREDUTL4_ALL, is new to this class, and
		//	is intended to permit specifying a run of all tests defined in this
		//	class with a single command line argument.
		//	--------------------------------------------------------------------

		const string TEST_SHAREDUTL4_ALL = @"ImportsFromSharedUtl4";			// Run everything in this set, skipping all others.

		const string TEST_MAXSTRINGLENGTH = @"MaxStringLength";                 // MaxStringLength_Tests: CHOICE_MAXSTRINGLENGTH = 0
		const string TEST_MERGENEWITEMSINTOARRAY = @"MergeNewItemsIntoArray";   // MergeNewItemsIntoArray_Tests: CHOICE_MERGENEWITEMSINTOARRAY = 1
		const string TEST_DIGESTFILE = @"DigestFile";                           // DigestFileTests: CHOICE_DIGESTFILE = 2
		const string TEST_REPORTDETAILS = @"ReportDetails";                     // ReportDetailsTests, of which there are two, without and with sorting: CHOICE_REPORTDETAILS = 3
		const string TEST_REPORTHELPERS = @"ReportHelpers";                     // ReportHelpersTests: CHOICE_REPORTHELPERS = 4
		const string TEST_FORMAT_ITEM_GEN = @"FormatItemGen";                   // FormatStringParsing_Drills.TestFormatItemBuilders: CHOICE_FORMAT_ITEM_GEN = 5
		const string TEST_ASCII_TABLE_GEN = @"ASCIITableGen";					// FormatStringParsing_Drills.ASCII_Table_Gen: TEST_ASCII_TABLE_GEN = 6

		const int CHOICE_MAXSTRINGLENGTH = 0;
		const int CHOICE_MERGENEWITEMSINTOARRAY = 1;
		const int CHOICE_DIGESTFILE = 2;
		const int CHOICE_REPORTDETAILS = 3;
		const int CHOICE_REPORTHELPERS = 4;
		const int CHOICE_FORMAT_ITEM_GEN = 5;
		const int CHOICE_ASCII_TABLE_GEN = 6;

		static readonly string [ ] s_astrTestSelections =
		{
			TEST_MAXSTRINGLENGTH ,												// CHOICE_MAXSTRINGLENGTH			= 0
			TEST_MERGENEWITEMSINTOARRAY ,										// CHOICE_MERGENEWITEMSINTOARRAY	= 1
			TEST_DIGESTFILE ,													// CHOICE_DIGESTFILE				= 2
			TEST_REPORTDETAILS ,												// CHOICE_REPORTDETAILS				= 3
			TEST_REPORTHELPERS ,												// CHOICE_REPORTHELPERS				= 4
			TEST_FORMAT_ITEM_GEN ,												// CHOICE_FORMAT_ITEM_GEN			= 5
			TEST_ASCII_TABLE_GEN												// TEST_ASCII_TABLE_GEN				= 6
		};	// s_astrTestSelections

		static readonly string [ ] s_astrFormats =
        {
            NumericFormats.NUMBER_PER_REG_SETTINGS_0D ,
            NumericFormats.HEXADECIMAL_2 ,
            NumericFormats.HEXADECIMAL_4 ,
            NumericFormats.HEXADECIMAL_8
        };  // [ ] s_astrFormats

		static readonly int s_intNTests = s_astrTestSelections.Length;
		static string [ ] s_astrPgmArgs;										// This gets a copy of the command line arguments from InitializeTestSelections.
		static int s_intNArgs;													// This gets the count of command line arguments from InitializeTestSelections.
		private static bool s_fSkipRemainingTests = RUN_REMAIING_TESTS;			// Making the initial default explicit satisfies a compiler quirk when it is subsequently evaluated.
		#endregion	// Private Constants and Static Data


		#region Private Processing Methods
		private static void MaxStringLength_Tests ( )
		{
			int intTestNumber = MagicNumbers.ZERO;

			Console.WriteLine (
				Properties.Resources.IDS_MAX_STRLEN_BEGIN ,
				Environment.NewLine );

			MaxStringLength_Test_Case (
				new MaxStringLength_Tester ( ) ,
				ref intTestNumber );

			Console.WriteLine (
				Properties.Resources.IDS_MAX_STRLEN_END ,
				Environment.NewLine );
		}   // MaxStringLength_Tests


		private static void MergeNewItemsIntoArray_Tests ( )
		{
			int intTestNumber = MagicNumbers.ZERO;

			Console.WriteLine (
				Properties.Resources.IDS_MERGENEWITEMSINTOARRAY_BEGIN ,
				Environment.NewLine );
			string strInitialMasterFile = Program.AbsolutePathStringFromSettings ( Properties.Settings.Default.MergeNewItemsIntoArray_Master );
			string strOutputFilenmetemplate = Program.AbsolutePathStringFromSettings ( Properties.Settings.Default.MergeNewItemsIntoArray_Outputs );
			string strNewItemsListFileSpec = Program.AbsolutePathStringFromSettings ( Properties.Settings.Default.MergeNewItemsIntoArray_Cases );
			int intMaxDigitsInCaseNumber = TestCaseMaxDigits ( strNewItemsListFileSpec );
			string strSummaryFileFQFN = Program.AbsolutePathStringFromSettings ( Properties.Settings.Default.MergeNewItemsIntoArray_Summary );
			string strOutputFileFQFN = null;
			string strInputFileName = null;
			string strReportLabels = MergeNewItemsIntoArray_Tester.ReportLabels.Replace (
				SpecialCharacters.TAB_CHAR ,
				SpecialCharacters.PIPE_CHAR );
			List<string> lstReportDetails = new List<string> ( );
			lstReportDetails.Add ( strReportLabels );

			string strReportDetailTemplate = ReportHelpers.DetailTemplateFromLabels (
				strReportLabels ,
				SpecialCharacters.PIPE_CHAR );

			foreach ( string strNewDataFile in Directory.GetFiles ( Program.s_strAbsoluteDataDirectoryName , strNewItemsListFileSpec , SearchOption.TopDirectoryOnly ) )
			{
				strInputFileName = SelectInputFle (
					intTestNumber ,
					strInitialMasterFile ,
					strOutputFileFQFN );
				strOutputFileFQFN = MergeOutputFQFN (
					strOutputFilenmetemplate ,
					Program.s_strAbsoluteDataDirectoryName ,
					intMaxDigitsInCaseNumber ,
					++intTestNumber );

				MergeNewItemsIntoArray_Tester mergetester = new MergeNewItemsIntoArray_Tester (
					strInputFileName ,
					strNewDataFile ,
					strOutputFileFQFN ,
					lstReportDetails ,
					strReportDetailTemplate );
				MergeNewItemsIntoArray_Test_Case (
					mergetester ,
					ref intTestNumber );
			}   // foreach ( string strNewDataFile in Directory.GetFiles ( rs_strDataDirectory , strNewItemsListFileSpec , SearchOption.TopDirectoryOnly ) )

			string [ ] astrSummaryReport = new string [ lstReportDetails.Count ];
			lstReportDetails.CopyTo ( astrSummaryReport );
			File.WriteAllLines (
				strSummaryFileFQFN ,
				astrSummaryReport );
			Console.WriteLine (
				Properties.Resources.IDS_MERGENEWITEMSINTOARRAY_END ,
				Environment.NewLine );
		}   // MergeNewItemsIntoArray_Tests


		private static void ReportDetailsTests ( bool pfTestSorting )
		{
			const int OUT_OF_ORDER = 900;

			string strTaskName = System.Reflection.MethodBase.GetCurrentMethod ( ).Name;

			Console.WriteLine (
				IDS_MSG_BATCH ,
				strTaskName ,
				Properties.Resources.IDS_MSG_BEGIN ,
				Environment.NewLine );

			if ( pfTestSorting )
				FormatStringParsing_Drills.TestFormatItemCounters ( );

			ReportDetails rdColl = new ReportDetails ( );

			if ( pfTestSorting )
				Console.WriteLine (
					Properties.Resources.MSG_REPORT_DETAILS_SELECTIVELY_OVERRIDDEN ,
					Environment.NewLine );
			else
				Console.WriteLine (
					Properties.Resources.MSG_REPORT_DETAILS_AUTO_ORDERED ,
					Environment.NewLine );

			rdColl.Add ( new ReportDetail (
				Properties.Resources.IDS_MSG_REPORT_LABEL_1 ,
				Properties.Resources.IDS_MSG_REPORT_LABEL_1.Length ,
				Properties.Resources.IDS_MSG_REPORT_LABEL_1.Length.ToString ( ) ) );
			rdColl.Add ( new ReportDetail (
				Properties.Resources.IDS_MSG_REPORT_LABEL_2 ,
				Properties.Resources.IDS_MSG_REPORT_LABEL_2.Length ,
				Properties.Resources.IDS_MSG_REPORT_LABEL_2.Length.ToString ( ) ) );
			rdColl.Add ( new ReportDetail (
				Properties.Resources.IDS_MSG_REPORT_LABEL_3 ,
				Properties.Resources.IDS_MSG_REPORT_LABEL_3.Length ,
				Properties.Resources.IDS_MSG_REPORT_LABEL_3.Length.ToString ( ) ,
				( ReportDetail.ItemDisplayOrder ) OUT_OF_ORDER ) );                                   // This one is intentionally out of order.
			rdColl.Add ( new ReportDetail (
				Properties.Resources.IDS_MSG_REPORT_LABEL_4 ,
				Properties.Resources.IDS_REALLY_LONG_STRING.Length ) ); // Leave the display value NULL.

			Console.WriteLine (
				Properties.Resources.IDS_MSG_LONGEST_LABEL ,
				rdColl.WidthOfWidestLabel );
			Console.WriteLine (
				Properties.Resources.IDS_MSG_LONGEST_VALUE ,
				rdColl.WidthOfWidestValue ,
				Environment.NewLine );

			if ( pfTestSorting )
			{   // The sorting pass shows it twice.
				Console.WriteLine (
					Properties.Resources.MSG_REPORT_DETAILS_UNSORTED ,
					Environment.NewLine );
			}   // if ( pfTestSorting )

			//  ----------------------------------------------------------------
			//  Since the value of this property is computed on demand by
			//  enumerating the collection, it is more efficient to hoist it out
			//  of the loop that also enumerates it. This change improves the
			//  performance of the foreach loop that follows it from an O(n^2)
			//  operation to an O(n) operation.
			//
			//  Since the typical size of these report item collections is on
			//  tho order of a few hundred items or less, the computational 
			//  impact of this change is too small to measure without running
			//  hundreds or thousands of iterations. Nevertheless, when it's so
			//  easy to do so, I believe that any operation should be designed
			//  to scale as well as possible.
			//  ----------------------------------------------------------------

			int intWidthOfWidestLabel = rdColl.WidthOfWidestLabel;
			int intWidthOfWidestValue = rdColl.WidthOfWidestValue;

			foreach ( ReportDetail rdItem in rdColl )
			{
				Console.WriteLine (
					ReportDetail.DEFAULT_FORMAT ,
					intWidthOfWidestLabel ,
					rdItem.GetPaddedValue (
						intWidthOfWidestValue ,
						WizardWrx.FormatStringEngine.FormatItem.Alignment.Right ,
						NumericFormats.DECIMAL ) );
			}   // foreach ( ReportDetail rdItem in rdColl )

			if ( pfTestSorting )
			{   // The second pass follows a sort.
				Console.WriteLine (
					Properties.Resources.MSG_REPORT_DETAILS_UNSORTED ,
					Environment.NewLine );

				rdColl.Sort ( );    // ReportDetails implements IComparable. Use it.

				foreach ( ReportDetail rdItem in rdColl )
				{
					Console.WriteLine (
						ReportDetail.DEFAULT_FORMAT ,
						rdItem.GetPaddedLabel ( intWidthOfWidestLabel ) ,
						rdItem.GetPaddedValue (
							intWidthOfWidestValue ,
							WizardWrx.FormatStringEngine.FormatItem.Alignment.Right ,
							NumericFormats.DECIMAL ) );
				}   // foreach ( ReportDetail rdItem in rdColl )
			}   // if ( pfTestSorting )

			Console.WriteLine (
				IDS_MSG_BATCH ,
				strTaskName ,
				Properties.Resources.IDS_MSG_DONE ,
				Environment.NewLine );
		}   // private void ReportDetailsTests


		private static void ReportHelpersTests ( )
		{
			const uint DESIRED_WIDTH_MINIMUM = 1;
			const uint DESIRED_WIDTH_MAXIMUM = 10;

			const uint ITEM_NUMBER_MINIMUM = 0;
			const uint ITEM_NUMBER_MAXIMUM = 4;

			string strTaskName = System.Reflection.MethodBase.GetCurrentMethod ( ).Name;

			Console.WriteLine (
				IDS_MSG_BATCH ,
				strTaskName ,
				Properties.Resources.IDS_MSG_BEGIN ,
				Environment.NewLine );

			uint uintTestNumber = MagicNumbers.ZERO;
			uint uintExceptionCount = MagicNumbers.ZERO;
			long lngTotalTests = MagicNumbers.ZERO;
			string strHeadingFormat = null;

#if DBG_REPORTHELPERSTESTS
            uint uintItemIndexCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

			for ( uint uintItemIndex = ITEM_NUMBER_MINIMUM ;
					   uintItemIndex <= ITEM_NUMBER_MAXIMUM ;
					   uintItemIndex++ )
			{

#if DBG_REPORTHELPERSTESTS
                Console.WriteLine (
                    "LOOP DEBUG: uintItemIndexCounter = {0}, uintItemIndex = {1}" ,
                    ++uintItemIndexCounter ,
                    uintItemIndex );
                uint uintMaximumWidthCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

				for ( uint uintMaximumWidth = DESIRED_WIDTH_MINIMUM ;
						   uintMaximumWidth < DESIRED_WIDTH_MAXIMUM ;
						   uintMaximumWidth++ )
				{

#if DBG_REPORTHELPERSTESTS
                    Console.WriteLine (
                        "LOOP DEBUG: uintMaximumWidthCounter = {0}, uintMaximumWidth = {1}" ,
                        ++uintMaximumWidthCounter ,
                        uintMaximumWidth );
                    uint uintAlignmentCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

					foreach ( WizardWrx.FormatStringEngine.FormatItem.Alignment enmAlignment in s_aenmAlignment )
					{

#if DBG_REPORTHELPERSTESTS
                        Console.WriteLine (
                            "LOOP DEBUG: uintAlignmentCounter = {0}, enmAlignment = {1}" ,
                            ++uintAlignmentCounter ,
                            enmAlignment );
                        uint uintTestFormatCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

						foreach ( string strTestFormat in s_astrFormats )
						{

#if DBG_REPORTHELPERSTESTS
                        Console.WriteLine (
                            "LOOP DEBUG: uintTestFormatCounter = {0}, strTestFormat = {1}" ,
                            ++uintTestFormatCounter ,
                            strTestFormat );
#endif  // DBG_REPORTHELPERSTESTS

							if ( lngTotalTests == MagicNumbers.ZERO )
							{
								lngTotalTests = ( ( ITEM_NUMBER_MAXIMUM - ITEM_NUMBER_MINIMUM ) + ArrayInfo.ORDINAL_FROM_INDEX )
												* ( DESIRED_WIDTH_MAXIMUM - DESIRED_WIDTH_MINIMUM )
												* s_aenmAlignment.Length
												* s_astrFormats.Length;
								strHeadingFormat = WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem (
									Properties.Resources.MSG_NEW_TEST ,
									ITEM_NUMBER_MINIMUM ,
									WizardWrx.FormatStringEngine.FormatItem.AdjustToMaximumWidth (
										ITEM_NUMBER_MINIMUM ,
										( uint ) lngTotalTests.ToString (
											NumericFormats.NUMBER_PER_REG_SETTINGS_0D ).Length ,
										WizardWrx.FormatStringEngine.FormatItem.Alignment.Right ,
										NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) );
							}   // if ( lngTotalTests == MagicNumbers.ZERO )

							Console.WriteLine (
								strHeadingFormat ,
								++uintTestNumber ,
								lngTotalTests ,
								Environment.NewLine );
							string strUpgradedFormat = WizardWrx.FormatStringEngine.FormatItem.AdjustToMaximumWidth (
								uintItemIndex ,
								uintMaximumWidth ,
								enmAlignment ,
								strTestFormat );

							ReportDetails rdColl = new ReportDetails ( );

							rdColl.Add ( new ReportDetail (
								Properties.Resources.MSG_PENMALIGNMENT ,
								( object ) uintItemIndex ) );
							rdColl.Add ( new ReportDetail (
								Properties.Resources.MSG_PUINTMAXIMUMWIDTH ,
								( object ) uintMaximumWidth ) );
							rdColl.Add ( new ReportDetail (
								Properties.Resources.MSG_PENMALIGNMENT ,
								enmAlignment ) );
							rdColl.Add ( new ReportDetail (
								Properties.Resources.MSG_PSTRFORMATSTRING ,
								( object ) strTestFormat ) );               // Coerce strTestFormat into the object.

							rdColl.Add ( new ReportDetail (
								Properties.Resources.MSG_UPGRADED_FORMAT ,
								( object ) strUpgradedFormat ) );

							int uintRequiredWidth = rdColl.WidthOfWidestLabel;

							foreach ( ReportDetail rdItem in rdColl )
							{
								Console.WriteLine (
									ReportDetail.DEFAULT_FORMAT ,
									rdItem.Label.PadRight ( ( int ) uintRequiredWidth ) ,
									rdItem.Value );
							}   // foreach ( ReportDetail rdItem in rdColl )


							//  ------------------------------------------------
							//  There are intentionally programed exceptions in
							//  this test.
							//  ------------------------------------------------

							try
							{
								Console.WriteLine (
									Properties.Resources.MSG_SHOW_SAMPLE_BEFORE_AND_AFTER ,
									Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
									WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem (
										Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
										uintItemIndex ,
										strUpgradedFormat ) ,
									Environment.NewLine );
							}
							catch ( Exception exAllKinds )
							{
								Program.s_smTheApp.AppExceptionLogger.ReportException ( exAllKinds );
								uintExceptionCount++;
							}

							if ( uintTestNumber == lngTotalTests )
							{   // We need just one test of this type of error.
								try
								{
									Console.WriteLine (
										Properties.Resources.MSG_SHOW_SAMPLE_BEFORE_AND_AFTER ,
										Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
										WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem (
											Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
											uintItemIndex ,
											"(4,9:X8)" ) ,      // This format item is invalid.
										Environment.NewLine );
								}
								catch ( Exception exAllKinds )
								{
									Program.s_smTheApp.AppExceptionLogger.ReportException ( exAllKinds );
									uintExceptionCount++;
								}
							}   // if ( uintTestNumber == lngTotalTests )
						}   // foreach ( string strTestFormat in s_astrFormats )
					}   // foreach ( FormatItem.Alignment enmAlignment in s_aenmAlignment )
				}   // for ( uint uintMaximumWidth = MINIMUM_DESIRED_WIDTH ; uintMaximumWidth <= MAXIMUM_DESIRED_WIDTH ; uintMaximumWidth++ )
			}   // for ( uint uintItemIndex = MINIMUM_ITEM_NUMBER ; uintItemIndex < FORMAT_ITEM_LIMIT ; uintItemIndex++ )

			Console.WriteLine (
				Properties.Resources.MSG_EXCETIONS_COUNTED ,
				uintExceptionCount ,
				Environment.NewLine );
			Console.WriteLine (
				IDS_MSG_BATCH ,
				strTaskName ,
				Properties.Resources.IDS_MSG_DONE ,
				Environment.NewLine );
		}   // ReportHelpersTests
		#endregion	// Private Processing Methods


		#region Private Processing Test Worker Methods
		private static void MaxStringLength_Test_Case (
			MaxStringLength_Tester pTestKit ,
			ref int pintTestNumber )
		{
			Console.WriteLine (
				Properties.Resources.IDS_TEST_BEGIN ,
				++pintTestNumber ,
				Environment.NewLine );

			pTestKit.Go ( );

			Console.WriteLine (
				Properties.Resources.IDS_TEST_END ,
				pintTestNumber ,
				Environment.NewLine );
		}   // MaxStringLength_Test_Case


		private static void MergeNewItemsIntoArray_Test_Case (
			MergeNewItemsIntoArray_Tester pTestKit ,
			ref int pintTestNumber )
		{
			Console.WriteLine (
				Properties.Resources.IDS_TEST_BEGIN ,
				pintTestNumber ,
				Environment.NewLine );

			pTestKit.Go ( );

			Console.WriteLine (
				Properties.Resources.IDS_TEST_END ,
				pintTestNumber ,
				Environment.NewLine );
		}   // MergeNewItemsIntoArray_Test_Case
		#endregion	// Private Processing Test Worker Methods


		#region Private Helper Methods Used by Private Processing Methods
		/// <summary>
		/// Return TRUE if the test specified in its argument should be run. See
		/// Remarks.
		/// </summary>
		/// <param name="pintTestNumber">
		/// Use an integer from the CHOICE_* constants defined above to indicate
		/// the test routine to call if the method returns TRUE. See Remarks.
		/// </param>
		/// <returns>
		/// Return TRUE if either of two conditions is TRUE.
		/// 
		/// 1) The command line tail is empty; the program was called without
		/// arguments.
		/// 
		/// 2) The command line tail contains one or more strings, one of which
		/// matches the string at the offset specified in the argument into the
		/// s_astrTestSelections array of selection names. 
		/// 
		/// See Remarks.
		/// </returns>
		/// <remarks>
		/// Reviewing this code three years after I wrote it reinforced the
		/// value of using comments to document algorithms, since, on first
		/// glance, this routine appeared to be using a brute force search when
		/// a simple indexed lookup would do. Closer inspection revealed that it
		/// was iterating the command line arguments, because the intent was to
		/// permit two or more test sets to be executed in the same invocation.
		/// </remarks>
		private static bool DoThisTest ( int pintTestNumber )
		{	// TEST_SHAREDUTL4_ALL
			if ( s_intNArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
			{   // No arguments == run all.
				return true;    // Just do it.
			}   // TRUE (short circuit) block, if ( s_intNArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
			else
			{	// There is at least one argument.
				if ( pintTestNumber < s_intNTests && pintTestNumber > ArrayInfo.ARRAY_INVALID_INDEX )
				{   // Argument pintTestNumber passed the sniff test.

					if ( s_astrPgmArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == TEST_SHAREDUTL4_ALL )
					{	// Short circuit if the whole set was selected. s_fSkipRemainingTests is already set.
						return true;
					}	// if ( s_astrPgmArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == TEST_SHAREDUTL4_ALL )

					int intPgmArgIndex = ArrayInfo.ARRAY_INVALID_INDEX;			// Set the argument index to start at argument zero.

					while ( ++intPgmArgIndex < s_intNArgs )
					{	// Stop when we run out of arguments to examine.
						if ( s_astrPgmArgs [ intPgmArgIndex ] == s_astrTestSelections [ pintTestNumber ] )
						{	// Stop early when we find a match.
							s_fSkipRemainingTests = SKIP_REMAINING_TESTS;
							return true;										// Found one we like.
						}   // if ( s_astrPgmArgs [ intPgmArgIndex ] == s_astrTestSelections [ pintTestNumber ] )
					}   // while ( intPgmArgIndex < s_intNArgs )

					return false;												// Didn't find one we like, but leave s_fSkipRemainingTests alone.
				}   // TRUE (expected outcome) block, if ( pintTestNumber < s_intNTests && pintTestNumber > ArrayInfo.ARRAY_INVALID_INDEX )
				else
				{   // Argument pintTestNumber is out of range.
					Console.WriteLine (
						Properties.Resources.ERRMSG_INVALID_TEST_INDEX ,
						pintTestNumber ,
						s_intNTests ,
						Environment.NewLine );
					return false;												// The test is skipped; this should probably throw.
				}   // FALSE (UNexpected outcome) block, if ( pintTestNumber < s_intNTests && pintTestNumber > ArrayInfo.ARRAY_INVALID_INDEX )
			}   // FALSE (selective testing) block, if ( s_intNArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
		}   // DoThisTest


		private static void InitializeTestSelections ( string [ ] pastrPgmArgs )
		{	// These must be initialized at runtime.
			s_astrPgmArgs = pastrPgmArgs;
			s_intNArgs = pastrPgmArgs.Length;

			switch ( pastrPgmArgs.Length )
			{
				case ArrayInfo.ARRAY_IS_EMPTY:
					s_fSkipRemainingTests =  RUN_REMAIING_TESTS;
					break;
				case ArrayInfo.ARRAY_SECOND_ELEMENT:
				default:
					if ( pastrPgmArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == TEST_SHAREDUTL4_ALL )
					{	// Caller asked for everything in this set.
						s_fSkipRemainingTests = SKIP_REMAINING_TESTS;
					}	// TRUE block, if ( pastrPgmArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == TEST_SHAREDUTL4_ALL )
					else
					{	// Caller specified one test, which may or may not be part of this set.
						s_fSkipRemainingTests = RUN_REMAIING_TESTS;
					}	// FALSE block, if ( pastrPgmArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == TEST_SHAREDUTL4_ALL )

					if ( s_fSkipRemainingTests )
					{	// Selecting the whole group trumps all other selections in the list, since the other selectors expect a single selection.
						WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine (
							WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor ,
							WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor ,
							Properties.Resources.MSG_WARNING_SUPERFLUOUS_ARGS_DISCARDED ,
							TEST_SHAREDUTL4_ALL ,
							Environment.NewLine );
					}	// if ( s_fSkipRemainingTests )
					break;
			}	// switch ( pastrPgmArgs.Length )
		}   // InitializeTestSelections


		private static string MergeOutputFQFN (
			string pstrNameTemplate ,
			string pstrDataDirectoryName ,
			int pintMaxDigitsInCaseNumber ,
			int pintCaseNumber )
		{
			//  ----------------------------------------------------------------
			//  Construct a fully qualified name from a template like this:
			//
			//     $$DATADIRNAME$$\MergeNewItemsIntoArray_Output_$$CASENBR$$.TXT
			//  ----------------------------------------------------------------

			const string CASE_NUMBER_FORMAT = @"D{0}";
			const string TOKEN_CASENUMBER = @"$$CASENBR$$";
			const string TOKEN_DATADIRNAME = @"$$DATADIRNAME$$";

			string strCaseNumberFormat = string.Format (
				CASE_NUMBER_FORMAT ,
				pintMaxDigitsInCaseNumber );
			string strCaseNumberToken = pintCaseNumber.ToString ( strCaseNumberFormat );
			return pstrNameTemplate.Replace (
				TOKEN_CASENUMBER ,
				strCaseNumberToken ).Replace (
					TOKEN_DATADIRNAME ,
					pstrDataDirectoryName );
		}   // MergeOutputFQFN


		private static string SelectInputFle (
			int pintTestNumber ,
			string pstrInitialMasterFile ,
			string pstrOutputFileFQFN )
		{
			if ( pintTestNumber == MagicNumbers.ZERO )
				return pstrInitialMasterFile;   // First pas uses this, and no output file has yet been named.
			else
				return pstrOutputFileFQFN;      // Since this function is called first, this is the output from the previous loop.
		}   // SelectInputFle


		private static int TestCaseMaxDigits ( string pstrNewItemsListFileSpec )
		{
			const char FILEMASK_WILD_ONE = '?';

			int rintMaxDigits = MagicNumbers.ZERO;

			foreach ( char chr in pstrNewItemsListFileSpec.ToCharArray ( ) )
				if ( chr == FILEMASK_WILD_ONE )
					rintMaxDigits++;

			return rintMaxDigits;
		}   // TestCaseMaxDigits
		#endregion	// Private Helper Methods Used by Private Processing Methods
	}	// class ImportsFromSharedUtl4
}	// partial namespace DLLServices2TestStand