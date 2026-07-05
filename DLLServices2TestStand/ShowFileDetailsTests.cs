/*
    ============================================================================

    Namespace:          DLLServices2TestStand

    Class Name:         ShowFileDetailsTests

    File Name:          ShowFileDetailsTests.cs

    Synopsis:           This static class exercises ShowFileDetails, a new
                        new extension method for the FileInfo class.

    Remarks:            This method justifies its own class because the test is
                        driven by a set of static arrays.

    Author:             David A. Gray

	License:            Copyright (C) 2019, David A. Gray. 
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
    2019/05/16 7.17    DAG    Initial implementation.

    2019/05/22 7.18    DAG    The first real application of ShowFileDetails
                              exposed a couple of cases that got overlooked
                              during preliminary testing, which was rushed into
                              production to meet the very pressing need that
                              eventually exposed the bugs, along with a minor
                              performance optimization for applications that
                              call it more than once. In addition, it exposed a
                              small performance optimization in this class.

    2019/06/06 7.20    DAG    Correct the clipboard copy error that caused the
                              alternative absolute file name to go unused, and
                              add a test for a missing file, which was the real
                              motivation for this update.
	============================================================================
*/

using System;

using System.IO;

using WizardWrx;

namespace DLLServices2TestStand
{
    static class ShowFileDetailsTests
    {
        static readonly FileInfoExtensionMethods.FileDetailsToShow [ ] s_aenmDetailsToShow = new FileInfoExtensionMethods.FileDetailsToShow [ ]
        {
            FileInfoExtensionMethods.FileDetailsToShow.None ,
            FileInfoExtensionMethods.FileDetailsToShow.AccessedTime ,
            FileInfoExtensionMethods.FileDetailsToShow.CreatedTime ,
            FileInfoExtensionMethods.FileDetailsToShow.LocalTime ,
            FileInfoExtensionMethods.FileDetailsToShow.UtcTime ,
            FileInfoExtensionMethods.FileDetailsToShow.AllTimes ,
            FileInfoExtensionMethods.FileDetailsToShow.AllTimesWithUtc ,
            FileInfoExtensionMethods.FileDetailsToShow.Size ,
            FileInfoExtensionMethods.FileDetailsToShow.Attributes ,
            FileInfoExtensionMethods.FileDetailsToShow.Everything
        };  // static readonly FileInfoExtensionMethods.FileDetailsToShow [ ] s_aenmDetailsToShow


        internal static int Exercise ( ref int pintTestNumber )
        {
            const string TEST_REPORT_PREAMBLE = @"{2}Testing ShowFileDetails extension method with FileDetailsToShow = {0} ({1}):";
            const string TEST_REPORT_TEMPLATE_ABS = @"    Case {0}: Information gathered based on absolute file name:    ";
            const string TEST_REPORT_TEMPLATE_REL = @"    Case {0}: Information gathered based on relative file name:    ";

            NewClassTests_20140914.BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber ,
                Program.BEGIN_TEST_TAKE_METHOD_NAME_AT_FACE_VALUE );

            string strRootAssemblyDirectory = Program.s_smTheApp.AppRootAssemblyFileDirName;

            string strRelativeFileName = @"..\..\..\Test_Data\MD5_File_Digests.DOCX";
            string strRelativeFileNotFound = @"..\..\..\Test_Data\MD5_File_Digests.DOC";

            string strAbsoluteFileName = Path.Combine (
                strRootAssemblyDirectory ,
                strRelativeFileName );
            string strAbsoluteFileNotFound = Path.Combine (
                strRootAssemblyDirectory ,
                strRelativeFileNotFound );

            Console.WriteLine (
                @"strRootAssemblyDirectory = {0}{1}" ,
                strRootAssemblyDirectory ,
                Environment.NewLine );
            Console.WriteLine (
                @"strRelativeFileName      = {0}" ,
                strRelativeFileName );
            Console.WriteLine (
                @"strAbsoluteFileName      = {0}" ,
                strAbsoluteFileName );
            Console.WriteLine (
                @"strRelativeFileNotFound  = {0}" ,
                strRelativeFileNotFound );

            //  ----------------------------------------------------------------
            //  Since everything uses the same two FileInfo objects, they may as
            //  well be hoisted above the For loop.
            //  ----------------------------------------------------------------

            FileInfo infoAbsolute1 = new FileInfo ( strAbsoluteFileName );
            FileInfo infoAbsolute2 = new FileInfo ( infoAbsolute1.FullName );
            FileInfo infoAbsolute3 = new FileInfo ( strAbsoluteFileNotFound );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; 
                      intJ < s_aenmDetailsToShow.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    TEST_REPORT_PREAMBLE ,                                      // Format Control String with 4 tokens
                    ( int ) s_aenmDetailsToShow [ intJ ] ,                      // Format Item 0: FileDetailsToShow = {0} (
                    s_aenmDetailsToShow [ intJ ] ,                              // Format Item 1: ({1}):
                    intJ == ArrayInfo.ARRAY_FIRST_ELEMENT                       // Format Item 2: {2}Testing ShowFileDetails
                        ? Environment.NewLine                                       // First iteration
                        : SpecialStrings.EMPTY_STRING );                            // Subsequent iterations
                Console.WriteLine (
                    infoAbsolute1.ShowFileDetails (
                        s_aenmDetailsToShow [ intJ ] ,                          // FileDetailsToShow penmFileDetailsToShow = FileDetailsToShow.Everything
                        string.Format (                                         // string pstrLabel = null
                            TEST_REPORT_TEMPLATE_REL ,                              // Format Control String with 1 token
                            ArrayInfo.OrdinalFromIndex ( intJ ) ) ,                 // Format Item 0: Case {0}: Information
                        true ,                                                  // bool pfPrefixWithNewline = false
                        true ) );                                               // bool pfSuffixWithNewline = false
                Console.WriteLine (
                    infoAbsolute2.ShowFileDetails (
                        s_aenmDetailsToShow [ intJ ] ,                          // FileDetailsToShow penmFileDetailsToShow = FileDetailsToShow.Everything
                        string.Format (                                         // string pstrLabel = null
                            TEST_REPORT_TEMPLATE_ABS ,                              // Format Control String with 1 token
                            ArrayInfo.OrdinalFromIndex ( intJ ) ) ,                 // Format Item 0: Case {0}: Information
                        true ,                                                  // bool pfPrefixWithNewline = false
                        true ) );                                               // bool pfSuffixWithNewline = false
                Console.WriteLine (
                    infoAbsolute3.ShowFileDetails (
                        s_aenmDetailsToShow [ intJ ] ,                          // FileDetailsToShow penmFileDetailsToShow = FileDetailsToShow.Everything
                        string.Format (                                         // string pstrLabel = null
                            TEST_REPORT_TEMPLATE_ABS ,                              // Format Control String with 1 token
                            ArrayInfo.OrdinalFromIndex ( intJ ) ) ,                 // Format Item 0: Case {0}: Information
                        false ,                                                 // bool pfPrefixWithNewline = false
                        true ) );                                               // bool pfSuffixWithNewline = false
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_aenmDetailsToShow.Length ; intJ++ )

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // internal static int Exercise
    }   // static class ShowFileDetailsTests
}   // namespace DLLServices2TestStand