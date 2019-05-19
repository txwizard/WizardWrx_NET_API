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
	============================================================================
*/

using System;
using WizardWrx;
using WizardWrx.Core;

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
            FileInfoExtensionMethods.FileDetailsToShow.Attributes ,
            FileInfoExtensionMethods.FileDetailsToShow.Everything
        };  // static readonly FileInfoExtensionMethods.FileDetailsToShow [ ] s_aenmDetailsToShow


        internal static int Exercise ( ref int pintTestNumber )
        {
            NewClassTests_20140914.BeginTest (
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                ref pintTestNumber ,
                Program.BEGIN_TEST_TAKE_METHOD_NAME_AT_FACE_VALUE );

            string strRootAssemblyDirectory = Program.s_smTheApp.AppRootAssemblyFileDirName;
            string strRelativeFileName = @"..\..\..\Test_Data\MD5_File_Digests.DOCX";
            string strAbsoluteFileName = System.IO.Path.Combine ( strRootAssemblyDirectory , strRelativeFileName );

            Console.WriteLine ( @"strRootAssemblyDirectory = {0}" , strRootAssemblyDirectory );
            Console.WriteLine ( @"strRelativeFileName      = {0}" , strRelativeFileName );
            Console.WriteLine ( @"strAbsoluteFileName      = {0}" , strAbsoluteFileName );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; 
                      intJ < s_aenmDetailsToShow.Length ;
                      intJ++ )
            {
                System.IO.FileInfo infoAbsolute = new System.IO.FileInfo ( strAbsoluteFileName );

                Console.WriteLine (
                    @"{2}Testing ShowFileDetails extension method with FileDetailsToShow = {0} ({1}):{2}" ,
                    ( int ) s_aenmDetailsToShow [ intJ ] ,
                    s_aenmDetailsToShow [ intJ ] ,
                    Environment.NewLine );
                Console.WriteLine (
                    infoAbsolute.ShowFileDetails (
                        s_aenmDetailsToShow [ intJ ] ,
                        string.Format (
                            @"    Case {0}: Information gathered based on absolute file name:    " ,
                            ArrayInfo.OrdinalFromIndex ( intJ ) ) ,
                        true ,
                        true ) );
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_aenmDetailsToShow.Length ; intJ++ )

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // internal static int Exercise
    }   // static class ShowFileDetailsTests
}   // namespace DLLServices2TestStand