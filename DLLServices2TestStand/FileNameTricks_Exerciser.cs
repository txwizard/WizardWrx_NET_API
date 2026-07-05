/*
    ============================================================================

	Name:				FileNameTricks_Exerciser

	Synopsis:			Put the FileNameTricks class through its paces.

    License:            Copyright (C) 2008-2022, David A. Gray.
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

    Date       Version  Author Synopsis
    ---------- -------- ------ --------------------------------------------------
	2008/06/12 2.0.7.0  DAG    Add code to exercise the new FileNameTricks class.

    2009/12/15 2.4.15   DAG    Add text graphics to draw attention to internal
                               comments, and to separate them from the code.

    2010/10/22 2.52     DAG    Replace "Visual Studio 2005" with "Visual Studio
                               2010" in all string literals.

                               This fix is required to compensate for relocation
                               of this project, due to the upgrade to Visual
                               Studio 2010. Otherwise, the test suite crashes
                               with an unhandled exception.

    2011/11/28 2.63     DAG    Add PathMakeRelativeToPath method.

	2017/02/22 7.0      DAG    Adjust for the breakup of WizardWrx.DllServices2.

	2017/06/29 7.0      DAG    Add the new properties, along with others that got
                               overlooked. I took the opportunity to make the way
                               newlines are processed platform agnostic.

	2017/08/27 7.0      DAG    Mark the class as static, and eliminate the hidden
                               default constructor, which is thereby made
                               redundant.

	2022/04/07 8.0.1497 DAG    Add the use case that exposed a flaw in static
                               method PathMakeRelative, which was based on work
                               published by Rick Strahl on 20 December 2010.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;

using WizardWrx;

namespace DLLServices2TestStand
{
    internal static class FileNameTricks_Exerciser
    {
        const string DFLT_DIR_1 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2_Test_Harness\";
        const string DFLT_DIR_2 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2_Test_Harness";
        const string DFLT_DIR_3 = "";
        const string DFLT_DIR_4 = null;

        const string FQFN_1 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2_Test_Harness\Directions_to_My_Place_Massage_DATA.htm";
        const string FQFN_2 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2_Test_Harness\SharedUtl2_Test_Harness.CMD";

        const string PATH_1 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2_Test_Harness\";
        const string PATH_2 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2_Test_Harness";
        const string PATH_3 = @"SharedUtl2\SharedUtl2_Test_Harness";
        const string PATH_4 = @"\SharedUtl2\SharedUtl2_Test_Harness";

        const string UQFN_1 = @"Directions_to_My_Place_Massage_DATA.htm";
        const string UQFN_2 = @"SharedUtl2_Test_Harness.CMD";
        const string UQFN_3 = @"Directions_to_My_Place_Massage_DATA.";
        const string UQFN_4 = @"SharedUtl2_Test_Harness";

        const string TEST_REPORT_1 = "        Input String  = {0}\r\n        Output String   = {1}\r\n";
        const string TEST_REPORT_2 = "        Input String  = {0}\r\n        Include/Exclude = {1}\r\n        Output String = {2}\r\n";
        const string TEST_REPORT_3 = "        Input String  = {0}\r\n        Default Dir     = {1}\r\n        Output String = {2}\r\n";
        const string TEST_REPORT_4 = "        Resource Path = {0}\r\n        Working Dir   = {1}\r\n        Relative Path = {2}\r\n";

        const string SAMPLE_RESOURCE_PATH_1 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl2\SharedUtl2\bin\Release\WizardWrx.SharedUtl2.dll";
        const string SAMPLE_RESOURCE_PATH_2 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\_TESTING WizardWrx Libs\SharedUtl2\SharedUtl2\bin\Release\WizardWrx.SharedUtl2.dll";
        
        const string SAMPLE_WORKING_PATH_FILE = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\Tools_for_Programmers\AssemblyReferenceFixup\_Notes\WebApp.csproj";
        const string SAMPLE_WORKING_PATH_DIR1 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\Tools_for_Programmers\AssemblyReferenceFixup\_Notes\";
        const string SAMPLE_WORKING_PATH_DIR2 = @"C:\Documents and Settings\David\My Documents\Visual Studio 2010\Projects\Tools_for_Programmers\AssemblyReferenceFixup\_Notes";

        static string [ ] s_astrDfltDirNames =
		{
            DFLT_DIR_1 ,
            DFLT_DIR_2 ,
            DFLT_DIR_3 ,
            DFLT_DIR_4
		};	// astrDfltDirNames

        static string [ ] a_astrTestPathsAll =
		{
            FQFN_1 ,
            FQFN_2 ,
            PATH_1 ,
            PATH_2 ,
            PATH_3 ,
            PATH_4 ,
            UQFN_1 ,
            UQFN_2 ,
            UQFN_3 ,
            UQFN_4
        };	// astrTestPathsAll

        static string [ ] s_astrTestPathStrings =
		{
            PATH_1 ,
            PATH_2 ,
            PATH_3 ,
            PATH_4
		};	// astrTestPathStrings

		static string [ ] a_astrTestFileNameStrings =
		{
            FQFN_1 ,
            FQFN_2 ,
            UQFN_1 ,
            UQFN_2 ,
            UQFN_3 ,
            UQFN_4
        };	// astrTestFileNameStrings

        static string [ ] s_astreResources =
        {
            SAMPLE_RESOURCE_PATH_1 ,
            SAMPLE_RESOURCE_PATH_2
        };	// astreResources

        static string [ ] astrWorkingDirs =
        {
            SAMPLE_WORKING_PATH_FILE ,
            SAMPLE_WORKING_PATH_DIR1 ,
            SAMPLE_WORKING_PATH_DIR2
        };	// astrWorkingDirs


		public static void Drill ( )
		{
			int intTestNumber = WizardWrx.MagicNumbers.ZERO;

			Console.WriteLine ( @"{0}Begin exercising the WizardWrx.FileNameTricks class{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Display all the enumeration values and other constants.
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Exercising enumerations and constants:{1}" , ++intTestNumber , Environment.NewLine );
			Console.WriteLine ( "        Enumeration TerminaBackslash.Include     = {0}" , WizardWrx.FileNameTricks.TerminaBackslash.Include );
			Console.WriteLine ( "        Enumeration TerminaBackslash.Omit        = {0}{1}" , WizardWrx.FileNameTricks.TerminaBackslash.Omit , Environment.NewLine );
			Console.WriteLine ( "        Character Constant OS_EXTENSION_DELIM    = {0}" , WizardWrx.FileNameTricks.OS_EXTENSION_DELIM );
			Console.WriteLine ( "        Character Constant OS_WILD_CARD_MULTIPLE = {0}" , WizardWrx.FileNameTricks.OS_WILD_CARD_MULTIPLE );
			Console.WriteLine ( "        Character Constant OS_WILD_CARD_SINGLE   = {0}{1}" , WizardWrx.FileNameTricks.OS_WILD_CARD_SINGLE , Environment.NewLine );
			Console.WriteLine ( "        String Constant OS_DRIVE_PATH_DELIMITER  = {0}" , WizardWrx.FileNameTricks.OS_DRIVE_PATH_DELIMITER );
			Console.WriteLine ( "        String Constant UNC_SERVER_DELIM         = {0}" , WizardWrx.FileNameTricks.UNC_SERVER_DELIM );

			Console.WriteLine ( "{0}    Finished exercising enumerations and constants{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise EnsureHasTerminalBackslash
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method EnsureHasTerminalBackslash:{1}" , ++intTestNumber, Environment.NewLine );

			foreach ( string strTestString in s_astrTestPathStrings )
			{
				string strOutputString = WizardWrx.FileNameTricks.EnsureHasTerminalBackslash (
					strTestString );
				Console.WriteLine (
					TEST_REPORT_1 ,
					strTestString ,
					strOutputString );
			}	// foreach ( string strTestString in astrTestPathStrings )

			Console.WriteLine ( "    Finished testing method EnsureHasTerminalBackslash{0}", Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise EnsureNoTerminalBackslash
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method EnsureNoTerminalBackslash:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strTestString in s_astrTestPathStrings )
			{
				string strOutputString = WizardWrx.FileNameTricks.EnsureNoTerminalBackslash (
					strTestString );
				Console.WriteLine (
					TEST_REPORT_1 ,
					strTestString ,
					strOutputString );
			}	// foreach ( string strTestString in astrTestPathStrings )

			Console.WriteLine ( "    Finished testing method EnsureNoTerminalBackslash{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise FileDirName
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method FileDirName:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strTestString in a_astrTestFileNameStrings )
			{	//	------------------------------------------------------------
				//	Include trailing backslash.
				//	------------------------------------------------------------

				WizardWrx.FileNameTricks.TerminaBackslash enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Include;
				{	//	Constrain the scope of string variable strOutputString.
					string strOutputString = WizardWrx.FileNameTricks.FileDirName (
						strTestString ,
						enmIncludeOrOmit );
					Console.WriteLine (
						TEST_REPORT_2 ,
						strTestString ,
						enmIncludeOrOmit ,
						strOutputString );
				}	// String strOutputString goes out of scope.


				//	------------------------------------------------------------
				//	Omit the trailing backslash.
				//	------------------------------------------------------------

				enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Omit;
				{
					string strOutputString = WizardWrx.FileNameTricks.FileDirName (
						strTestString ,
						enmIncludeOrOmit );
					Console.WriteLine (
						TEST_REPORT_2 ,
						strTestString ,
						enmIncludeOrOmit ,
						strOutputString );
				}	// enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Omit
			}	// foreach ( string strTestString in astrTestFileNameStrings )

			Console.WriteLine ( "    Finished testing method FileDirName{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise FileExtn
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method FileExtn:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strTestString in a_astrTestFileNameStrings )
			{
				string strOutputString = WizardWrx.FileNameTricks.FileExtn (
					strTestString );
				Console.WriteLine (
					TEST_REPORT_1 ,
					strTestString ,
					strOutputString );
			}	// foreach ( string strTestString in astrTestFileNameStrings )

			Console.WriteLine ( "    Finished testing method FileExtn{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise FQFBasename
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method FQFBasename:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strTestString in a_astrTestFileNameStrings )
			{
				string strOutputString = WizardWrx.FileNameTricks.FQFBasename (
					strTestString );
				Console.WriteLine (
					TEST_REPORT_1 ,
					strTestString ,
					strOutputString );
			}	// foreach ( string strTestString in astrTestFileNameStrings )

			Console.WriteLine ( "    Finished testing method FQFBasename{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise MakeFQFN
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method MakeFQFN:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strDefaultDir in s_astrDfltDirNames )
			{
				foreach ( string strTestString in a_astrTestFileNameStrings )
				{
					string strOutputString = WizardWrx.FileNameTricks.MakeFQFN (
						strTestString ,
						strDefaultDir );
					Console.WriteLine (
						TEST_REPORT_3 ,
						strTestString ,
						strDefaultDir ,
						strOutputString );
				}	// foreach ( string strTestString in astrTestFileNameStrings )
			}	// foreach ( string strDefaultDir in astrDfltDirNames )

			Console.WriteLine ( "    Finished testing method MakeFQFN{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise PathFixup
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method PathFixup:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strTestString in s_astrTestPathStrings )
			{	//	------------------------------------------------------------
				//	Include trailing backslash.
				//	------------------------------------------------------------

				WizardWrx.FileNameTricks.TerminaBackslash enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Include;
				{
					string strOutputString = WizardWrx.FileNameTricks.PathFixup (
						strTestString ,
						enmIncludeOrOmit );
					Console.WriteLine (
						TEST_REPORT_2 ,
						strTestString ,
						enmIncludeOrOmit ,
						strOutputString );
				}	// WizardWrx.FileNameTricks.TerminaBackslash enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Include;

				//	------------------------------------------------------------
				//	Omit the trailing backslash.
				//	------------------------------------------------------------

				enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Omit;
				{
					string strOutputString = WizardWrx.FileNameTricks.PathFixup (
						strTestString ,
						enmIncludeOrOmit );
					Console.WriteLine (
						TEST_REPORT_2 ,
						strTestString ,
						enmIncludeOrOmit ,
						strOutputString );
				}	// enmIncludeOrOmit = WizardWrx.FileNameTricks.TerminaBackslash.Omit;
			}	// foreach ( string strTestString in astrTestPathStrings )

			Console.WriteLine ( "    Finished testing method PathFixup{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise UQFBasename
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method UQFBasename:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strTestString in a_astrTestPathsAll )
			{
				string strOutputString = WizardWrx.FileNameTricks.UQFBasename (
					strTestString );
				Console.WriteLine (
					TEST_REPORT_1 ,
					strTestString ,
					strOutputString );
			}	// foreach ( string strTestString in astrTestPathsAll )

			Console.WriteLine ( "    Finished testing method UQFBasename{0}" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Exercise PathMakeRelative.
			//	----------------------------------------------------------------

			Console.WriteLine ( "    Test {0} - Testing method PathMakeRelativeToPath:{1}" , ++intTestNumber , Environment.NewLine );

			foreach ( string strReource in s_astreResources )
			{
				foreach ( string strWorkingDir in astrWorkingDirs )
				{
					string strOutputString = WizardWrx.FileNameTricks.PathMakeRelative (
						strReource ,
						strWorkingDir );
					Console.WriteLine (
						TEST_REPORT_4 ,
						strReource ,
						strWorkingDir ,
						strOutputString );
				}   // foreach ( string strWorkingDir in astrWorkingDirs )
			}   // foreach ( string strReource in astreResources )

			string strReource2 = @"D:\SalesTalk\_Say2Sell\Free_Trial\~$ee_Trial_Enrollment_Journal.DOCX";
			string strWorkingDir2 = @"D:\SalesTalk";
			string strOutputString2 = WizardWrx.FileNameTricks.PathMakeRelative (
				strReource2 ,
				strWorkingDir2 );
			Console.WriteLine (
				TEST_REPORT_4 ,
				strReource2 ,
				strWorkingDir2 ,
				strOutputString2 );

			Console.WriteLine ( "    Finished testing method PathMakeRelativeToPath.{0}" , Environment.NewLine );

			Console.WriteLine ( @"Finished exercising the WizardWrx.FileNameTricks class" );
		}   // public static method Drill
    }   // class FileNameTricks_Exerciser
}   // partial namespace DLLServices2TestStand