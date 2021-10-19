/*
    ============================================================================

    Namespace:			DLLServices2TestStand

    Class Name:			Program

    File Name:			Program.cs

    Synopsis:			Exercise the classes in WizardWrx.DLLServices2.dll,
                        which houses classes that once belonged to another
                        library, WizardWrx.ApplicationHelpers.dll, but were
                        moved out of it, to break a circular dependency between
                        it and yet another library, WizardWrx.ConsoleAppAids,
                        and their respective test stand programs.

    Remarks:			To prevent the circular dependencies that led to
                        creation of WizardWrx.DLLServices2.dll, this test stand
                        program deviates from my usual practice, and sticks to
                        classes available from either WizardWrx.DLLServices2.dll
                        or from one of my strong name signed base libraries.

    Author:				David A. Gray

    License:            Copyright (C) 2011-2021, David A. Gray.
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

    Date       Version   By  Synopsis
    ---------- --------- --- -----------------------------------------------------
    2013/02/22 1.0       DAG This class, and its project, make their first 
                             appearance.

    2014/05/31 4.1       DAG Bind against upgraded WizardWrx.DLLServices,
                             WizardWrx.ApplicationHelpers, and
                             WizardWrx.ConsoleAppAids2 libraries, and eliminate
                             references to unused system name spaces and
                             libraries, to clarify what is actually required,
                             and to slightly streamline the build process.

    2014/06/07 5.0       DAG Major namespace reorganization. This test program
                             confines itself to classes exposed by the class
                             library under test.

    2014/07/20 5.1       DAG 1) Correct an oversight that left this class in the
                                old WizardWrx.ApplicationHelpers namespace.
                                Since this change affects only two other DLLs,
                                and, at most one user program, I took advantage
                                of the opportunity to promote the DLLServices2
                                namespace to the first rank under the overall
                                WizardWrx namespace.

                             2) Swap the method names, so that the instance
                                methods whose signatures exactly mirror those of
                                the Console.Write* methods have the same base
                                method names, while the static methods, which
                                require two additional arguments to name the
                                foreground and background colors have names that
                                remind you that you must specify colors in any
                                call.

                                The following table lists the substitutions.

                                ------------------------------------------------------
                                FindStr                    ReplStr
                                ------------------------   ---------------------------
                                public static void Write   public static void RGBWrite
                                public void ColorWrite     public void Write
                                ------------------------------------------------------

                                This change means that any method that requires
                                additional arguments has a name that differs
                                slightly from that of the corresponding Console
                                method, and the difference is a prefix, to hint
                                that the colors go in front of the arguments to
                                the analogous console method. Likewise, methods
                                that have identical signatures have identical
                                base names to the corresponding Console method.

    2014/10/14 5.3       DAG As was true of the last point upgrade, the test 
                             stand is unchanged.
 
    2015/07/10 5.5       DAG Incorporate tests for the classes that just moved
                             into WizardWrx.DLLServices2.dll, along with the new
                             native routine that reports the state of any
                             standard console handle for assemblies running in
                             ANY version of the Microsoft .NET Framework.

    2015/09/01 5.6       DAG Add a couple of overlooked special characters and
                             strings, along with a new method, ChopChop, to test
                             the new Chop method.

    2015/09/01 5.7       DAG Add a very simple product name and version format.

    2015/10/28 5.8       DAG Add the console redirection file displays, which I
                             decided to keep in this library, so that the calls
                             into WWConAid.dll are all together.

    2016/04/12 6.0       DAG Exercise the redirection state testers, including
                             timed tests of both approaches.

                             Investigate a remedy that would permit me to allow
                             the Visual Studio Hosting Process to execute
                             without interfering with my execution subsystem
                             tests.

    2016/05/11 6.0       DAG Eliminate an unused constant.

    2016/05/12 6.1       DAG Exercise the new routines to retrieve the special
                             error message strings.

    2016/05/20 6.1       DAG Incorporate FileNameTricks_Exerciser.Drill to test
                             its namesake class.

    2016/06/04 6.2       DAG  Move the last two tests above the EOJ message.

    2016/06/06 6.3       DAG Add a small routine to test the improved exception
                             reporting logic that suppresses printing a message
                             twice when reporting to both standard output 
                             streams is enabled, and neither is redirected.

    2017/03/28 7.0       DAG Adjust for the breakup of WizardWrx.DllServices2.

    2017/06/27 7.0       DAG Evaluate the AppEventSourceID property before any
                             exceptions are reported, which is the only way to
                             perform a valid test of its lazy initializer.

    2017/07/11 7.0       DAG Adjust to support the improved 100% managed code
                             that processes standard console handles.

    2017/07/13 7.0       DAG Display the processor architecture and subsystem
                             (32 or 64 bits) and the Windows version on which
                             the process is executing.

    2017/07/17 7.0       DAG Incorporate and activate alternative blocks to
                             exercise the new extension methods on the string
                             class.

    2017/07/19 7.0       DAG 1) Integrate EnumerateStringResourcesInAssembly, an
                                experimental method to enumerate every string
                                resource defined in an assembly into a special
                                case test and the main test body.

                             2) Move the exercises that comprise the main body
                                into the IF statement that pauses the output if
                                the test succeeded, or throws an exception if it
                                fails.

    2017/09/10 7.0       DAG Integrate the unit tests that cover the classes
                             that were merged into this library constellation
                             from WizardWrx.SharedUtl4 and elsewhere.

    2017/09/13 7.0       DAG Add the "s_" prefix to AbsoluteDataDirectoryName.

    2018/10/07 7.1       DAG Add CapitalizeWordsExercises to test a new string
                             class extension method, and add a new method,
                             SpecialStringExercises, that lists and displays the
                             SpecializedStrings constants.

    2018/11/15 7.11      DAG Add EnumFromStringExercises to test a new string
                             extension method that converts a string to an
                             enumeration, and MoreMathTests.Run to test the new
                             MoreMath class.

    2019/02/17 7.15      DAG Replace Reflection.MethodBase.GetCurrentMethod
                             with ClassAndMethodDiagnosticInfo.GetMyMethodName,
                             which shifts most of the work to the compile phase
                             of the build cycle, and report the contents of the
                             ExceptionLogger.s_strSettingsOmittedFromConfigFile
                             property, a static string that returns a message to
                             be displayed to the program operator.

    2019/05/03 7.15      DAG Implement and deploy RecoveredExceptionTests.

    2019/05/19 7.17      DAG Implement tests for the following improvements:

                             1) StringFixups: Demonstrate its ToString method.

                             2) ReplaceEscapedTabsInResourceString: Demonstrate
                                this string extension method.

                             3) ShowFileDetails: Demonstrate this FileInfo
                                extension method, and put it through its paces,
                                and demonstrate the ToString method on the
                                array of StringFixups structures.

    2019/05/30 7.19      DAG Implement tests for the following string extension
                             methods via class LinEndingFixupTests:

                             UnixLineEndings      Replace CR/LF pairs and bare
                                                  CRs with bare LFs.

                             WindowsLineEndings   Replace bare LFs with CR/LF
                                                  pairs.

                             OldMacLineEndings    Replace CR/LF pairs and bare
                                                  LFs with bare CRs.

    2019/06/09 7.20      DAG Add a section to invoke the amended test method
                             EnumerateStringResourcesInAssembly to test the
                             ListResourcesInAssemblyByName overload that takes a
                             StreamWriter into which it is expected to write a
                             tab delimited list of the string resources stored
                             in a specified assembly.

    2019/06/09 7.20      DAG Add the assembly name to the report listing its
                             string resources.

    2019/07/18 7.21      DAG 1) Move most of the new tests outside the block that
                                deals with standard stream redirection.

                             2) Add an argument to PauseForPictures that takes a
                                string that is displayed on the error console
                                when standard output is redirected.

    2019/10/03 7.22      DAG Since the tab consistency checker add-in flagged
                             this file as inconsistently tabbed, I let it 
                             replace tabs with spaces. The code is otherwise
                             unchanged.

    2019/12/17 7.23.120  DAG Static method ExerciseGetDependentAssemblyInfos 
                             makes its debut.

    2019/12/25 7.23.121  DAG Static method ExerciseGetDependentAssemblyInfos 
                             gets a list of additional assemblies to process,
                             and a bug of long standing gets fixed so that
                             nonzero return codes reach the calling process.

    2020/10/11 7.23.1312 DAG Implement a new command line argument,
                             DirectDependentAssemblyEnumerator, that invokes the
                             ExercisseDirectDependentAssemblyEnumerator method,
                             then terminates the program.

    2021/02/04 8.0.1382  DAG Implement a small loop to test a new static method,
                             GetAssemblyVersionInfo, exported by class library
                             WizardWrx.AssemblyUtils. The new static method,
                             ExerciseGetAssemblyVersionInfo, tests all valid
                             use cases.

    2021/03/21 8.0.1384  DAG Implement s_strAbsoluteReportsDirectoryName, which
                             constructs an absolute directory name to which an
                             unqualified file name can be appended to create an
                             absolute (fully qualiffied) file name.

    2021/04/18 8.0.1389 DAG Test NameValueCollectionFromEmbbededList.

    2021/05/19 8.0.1401 DAG Test new methods GetAssemblyCompanyNameSnakeCased
                            and GetAssemblyAppDataDirectoryName.

    2021/06/09 8.0.1417     Add the test routine for ListObjectProperties, a new
                            static method on ObjectPropertyEnumerators.

    2021/06/10 8.0.1419 DAG Rearrange the argument list of ListObjectProperties
                            to put the optional arguments in order most likely
                            to be overrridden.

    2021/06/17 8.0.1426 DAG Exercise StringTricks.TruncateValueToOneLine.

    2021/06/29 8.0.1442 DAG Exercise method ListObjectPropertyTypesAndValues on
                            ObjectPropertyEnumerators.

    2021/07/04 8.0.1448 DAG Exercise the new RemoveAt extension method for all
                            types of Arrays.

	2021/07/11 8.0.1453 DAG Exercise the new Base64 conversion methods.

    2021/10/19 8.0.1463 DAG Implement code to exercise class EnvTokenExpander.
    ============================================================================
*/


using System;
using System.ComponentModel;
using System.IO;

using WizardWrx;
using WizardWrx.AssemblyUtils;
using WizardWrx.Core;
using WizardWrx.ConsoleStreams;
using WizardWrx.DLLConfigurationManager;
using System.Collections.Generic;


namespace DLLServices2TestStand
{
    class Program
    {
        internal const string ERRMSG_BADARG_PARAMINFO_TPL = @"Argument Name = {0} - Argument Value = {1}";
        internal const string ERRMSG_INTERNAL_ERROR_TPL = @"INTERNAL ERROR: {0}{1}";
        internal const string ERRMSG_INTERNAL_INNER_MSG_TPL = @"Inner Exception: Message = {0}";
        internal const string ERRMSG_INTERNAL_OBJNAME_TPL = @"Name of disposed object = {0}";
        internal const string ERRMSG_INTERNAL_SOURCE_TPL = @"Source = {0}{1}";
        internal const string ERRMSG_INTERNAL_STACKTRACE_TPL = @"{1}Stack Trace:{1}{0}{1}{1}";
        internal const string ERRMSG_INTERNAL_TARGETSITE_TPL = @"TargetSite = {0}{1}";
        internal const string ERRMSG_GENERIC_TPL = @"Error writing to log file {0}. Message = {1}{5}Source = {2}{5}TargetSite = {3}{5}StackTrace Begin:{5}{4}{5}StackTrace End";

        internal const bool OMIT_LINEFEED = false;
        internal const bool APPEND_LINEFEED = true;

        internal static readonly string s_strAbsoluteDataDirectoryName = Path.Combine (
            Environment.CurrentDirectory ,
            Properties.Settings.Default.Data_Directory.EnsureLastCharIs ( Path.DirectorySeparatorChar ) );
        internal static readonly string s_strAbsoluteReportsDirectoryName = Path.Combine (
            Environment.CurrentDirectory ,
            Properties.Settings.Default.Reports_Directory.EnsureLastCharIs ( Path.DirectorySeparatorChar ) );

        static readonly int [ ] s_aenmAssemblyVersionRequests =
        {
            ( int ) StateManager.AssemblyVersionRequest.MajorOnly ,
            ( int ) StateManager.AssemblyVersionRequest.MajorAndMinor ,
            ( int ) StateManager.AssemblyVersionRequest.MajroMinorBuild ,
            ( int ) StateManager.AssemblyVersionRequest.MajorMinorExceptRevision ,
            ( int ) StateManager.AssemblyVersionRequest.MajorMinroBuildRevision ,
            ( int ) StateManager.AssemblyVersionRequest.Complete ,
            MagicNumbers.ZERO
        };	// s_aenmAssemblyVersionRequests

        static readonly Type [ ] s_atypCommonExceptionTypes = new Type [ ]
        {
            typeof ( System.Exception ) ,
            typeof ( System.ArgumentException ) ,
            typeof ( System.ArgumentOutOfRangeException ) ,
            typeof ( System.ArgumentNullException ) ,
            typeof ( System.ObjectDisposedException ) ,
            typeof ( System.IO.IOException ) ,
            typeof ( System.FormatException ) ,

            typeof ( System.IO.FileNotFoundException ) ,
            typeof ( System.IO.DirectoryNotFoundException ) ,
            typeof ( System.IO.DriveNotFoundException ),
            typeof ( System.IO.EndOfStreamException ) ,
            typeof ( System.IO.InternalBufferOverflowException ) ,
            typeof ( System.IO.InvalidDataException ) ,
            typeof ( System.IO.PathTooLongException ) ,

            typeof ( System.InvalidCastException) ,
            typeof ( System.InvalidOperationException) ,
            typeof ( System.NullReferenceException) ,
            typeof ( System.OutOfMemoryException) ,
            typeof ( System.OverflowException ) ,
            typeof ( System.PlatformNotSupportedException ) ,
            typeof ( System.RankException ) ,
            typeof ( System.StackOverflowException ) ,
            typeof ( System.TimeoutException ) ,
            typeof ( System.TypeInitializationException ) ,
            typeof ( System.TypeLoadException ) ,
            typeof ( System.TypeUnloadedException ) ,
            typeof ( System.UnauthorizedAccessException ) ,
            typeof ( System.UriFormatException ) ,

            typeof ( System.Text.DecoderFallbackException ) ,
            typeof ( System.Text.EncoderFallbackException ) ,
            typeof ( System.Security.SecurityException ) ,
            typeof ( System.Security.VerificationException ) ,
            typeof ( System.Security.XmlSyntaxException ) ,

            typeof ( System.Xml.XmlException ) ,								// This reference creates a dependency on System.XML.

            typeof ( System.Configuration.SettingsPropertyIsReadOnlyException ) ,
            typeof ( System.Configuration.SettingsPropertyNotFoundException ) ,
            typeof ( System.Configuration.SettingsPropertyWrongTypeException ) ,

            typeof ( System.Reflection.AmbiguousMatchException ) ,
            typeof ( System.Reflection.CustomAttributeFormatException ) ,
            typeof ( System.Reflection.InvalidFilterCriteriaException ) ,
            typeof ( System.Reflection.TargetParameterCountException ) ,

            typeof ( System.Text.ASCIIEncoding ) ,
            typeof ( System.Text.UnicodeEncoding) ,
            typeof ( System.Text.UTF7Encoding ) ,
            typeof ( System.Text.UTF8Encoding ) ,
            typeof ( System.Text.UTF32Encoding )
        };	// s_atypCommonExceptionTypes

        static readonly Guid [ ] s_agidSupportedExceptionTypes = new Guid [ ]
        {
            typeof ( System.Exception ).GUID ,
            typeof ( System.ArgumentException ).GUID ,
            typeof ( System.ArgumentOutOfRangeException ).GUID ,
            typeof ( System.ArgumentNullException ).GUID ,
            typeof ( System.ObjectDisposedException ).GUID ,
            typeof ( System.IO.IOException ).GUID ,
            typeof ( System.FormatException ).GUID ,
        };	// s_agidSupportedExceptionTypes

        static readonly string [ ] s_astrErrorMessagres =
        {
            Properties.Resources.ERRMSG_SUCCESS,
            Properties.Resources.ERRMSG_RUNTIME,
        };  // s_astrErrorMessagres

        static readonly int [ ] s_aintUnaryMinusUseCases =
        {
            MagicNumbers.PLUS_ONE ,
            MagicNumbers.PLUS_TWO ,
            MagicNumbers.PLUS_SEVEN ,
            MagicNumbers.ZERO ,
            MagicNumbers.MINUS_ONE ,
            MagicNumbers.PLUS_TWO * MagicNumbers.MINUS_ONE ,
            MagicNumbers.PLUS_SEVEN * MagicNumbers.MINUS_ONE
        };	//  s_aintUnaryMinusUseCases

        struct OutputOptionTestData
        {
            public string Label;
            public ExceptionLogger.OutputOptions NewFlags;

            public OutputOptionTestData (
                string pstrLabel ,
                ExceptionLogger.OutputOptions penmNewOptions )
            {
                this.Label = pstrLabel;
                this.NewFlags = penmNewOptions;
            }
        };	//	OutputOptionTestData

        const string OUTPUT_OPTIONS_BASELINE = @"Baseline       ";
        const string OUTPUT_OPTIONS_NL_STRIP = @"NewLine Strip  ";
        const string OUTPUT_OPTIONS_NL_REPLACE = @"NewLine Replace";
        const string OUTPUT_OPTIONS_NL_AUGMENT = @"NewLine Augment";
        const string OUTPUT_OPTIONS_FINAL = @"Final Settings ";

        const string STRING_FIXUP_INPUT_STRING_1 = "{\n     \"Error Message\": \"Invalid API call. Please retry or visit the documentation (https://www.alphavantage.co/documentation/) for SYMBOL_SEARCH.\"\r}";
        const string STRING_FIXUP_INPUT_STRING_2 = "{\n     \"ErrorMessage\": \"Invalid API call. Please retry or visit the documentation (https://www.alphavantage.co/documentation/) for SYMBOL_SEARCH.\"\r}";
        const string STRING_FIXUP_INPUT_STRING_3 = "{\n     \"Foo Bar\": \"It's a Foo Bar, Sir!}";

        static readonly string [ ] s_astrFixupInputs = {
            STRING_FIXUP_INPUT_STRING_1 ,
            STRING_FIXUP_INPUT_STRING_2 ,
            STRING_FIXUP_INPUT_STRING_3
        };  // static readonly string [ ] s_astrFixupInputs

        static readonly StringFixups.StringFixup [ ] s_astrStringFixups =
        {
            new StringFixups.StringFixup ( @"Error Message" , "ErrorMessage" ) ,
            new StringFixups.StringFixup ( @"Foo Bar" , "FooBar" ) ,
        };  // static readonly StringFixups.StringFixup [ ] s_astrStringFixups

        internal const bool BEGIN_TEST_TAKE_METHOD_NAME_AT_FACE_VALUE = true;

        static readonly OutputOptionTestData [ ] s_utpOutputOptionTestData =
        {
            new OutputOptionTestData (
                OUTPUT_OPTIONS_BASELINE ,
                ExceptionLogger.OutputOptions.NoFlags ),
            new OutputOptionTestData (
                OUTPUT_OPTIONS_NL_STRIP ,
                ExceptionLogger.OutputOptions.DiscardNewlines ),
            new OutputOptionTestData (
                OUTPUT_OPTIONS_NL_REPLACE ,
                ExceptionLogger.OutputOptions.ReplaceNewlines ),
            new OutputOptionTestData (
                OUTPUT_OPTIONS_NL_AUGMENT ,
                ExceptionLogger.OutputOptions.NBSpaceForNewlines )
        };	// s_utpOutputOptionTestData

        //  --------------------------------------------------------------------
        //  Use this array to thoroughly exercise GetAssemblyVersionInfo.
        //  --------------------------------------------------------------------

        private static AssemblyAttributeHelpers.AttributeFriendlyName [ ] s_aenmAttributeFriendlyName =
        {
            AssemblyAttributeHelpers.AttributeFriendlyName.Company ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Copyright ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Culture ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Description ,
            AssemblyAttributeHelpers.AttributeFriendlyName.FileVersion ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Product ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Title ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Trademark ,
            AssemblyAttributeHelpers.AttributeFriendlyName.Version
        };  // s_aenmAttributeFriendlyName

        private static string [ ] s_astrStringsToTruncate =
        {
            "Line 1 of a three-line CR/LF terminated string\r\nLine 2 of a three-line CR/LF terminated string\r\nLine 3 of a three-line CR/LF terminated string",
            "Line 1 of a three-line CR terminated string\rLine 2 of a three-line CR terminated string\rLine 3 of a three-line CR terminated string",
            "Line 1 of a three-line LF terminated string\nLine 2 of a three-line LF terminated string\nLine 3 of a three-line LF terminated string",
            "This line is suppoed to be very long, but devoid of line breaks. Its objective is to push the inbuilt length limit of the object truncation routine, which limits its output to the width of an old-fashioned wide carriage printer or a standard mainframe or minicomputer line printer.",
            "This line is intentionally shorter than the one above it. Its intention is to be too long by one character, which is to be truncated.",
            "This line is short enough to be rendereed in its entirety.",
            SpecialStrings.EMPTY_STRING ,
            null
        };

        private static string [ ] s_astrRemoveAtTest1 =
        {
            @"This element is at index 0.",
            @"This element is at index 1.",
            @"This element is at index 2.",
            @"This element is at index 3.",
            @"This element is at index 4.",
            @"This element is at index 5.",
            @"This element is at index 6.",
            @"This element is at index 7.",
            @"This element is at index 8."
        };

        private static int [ ] s_aintRemoveAtTest2 = { 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 };

        internal static StateManager s_smTheApp = StateManager.GetTheSingleInstance ( );

        static void Main ( string [ ] pastrArgs )
        {
            const string ARCH_EQUALS = @" = ";
            const string PADDED_EQUALS = @"                 = ";

            TimeDisplayFormatter dtmfApp = s_smTheApp.ConsoleMessageTimeFormat;
            int intTestNumber = MagicNumbers.ZERO;

            try
            {
                Console.WriteLine (
                    Properties.Resources.BOJ_MSG_TPL ,
                    new string [ ]
                    {
                        s_smTheApp.AppRootAssemblyFileBaseName ,
                        System.Diagnostics.FileVersionInfo.GetVersionInfo ( s_smTheApp.GetAssemblyFQFN ( ) ).FileVersion ,
                        dtmfApp.FormatThisTime ( s_smTheApp.AppStartupTimeLocal ) ,
                        dtmfApp.FormatThisTime ( s_smTheApp.AppStartupTimeUtc ) ,
                        Environment.NewLine
                    } );
                ExceptionLogger.TimeStampedTraceWrite (
                    string.Format (
                        "BOJ {0}" ,
                        s_smTheApp.AppRootAssemblyFileBaseName ) );
                s_smTheApp.LoadErrorMessageTable ( s_astrErrorMessagres );

                //	------------------------------------------------------------
                //	Replace the equals sign and its surrounding spaces with more
                //	spaces, to align the data with the items that follow it.
                //	------------------------------------------------------------

                Console.WriteLine (
                    BasicSystemInfoDisplayMessages.DisplayProcessorArchitecture ( ).Replace (
                        ARCH_EQUALS ,
                        PADDED_EQUALS ) );
                Console.WriteLine (
                    "OS Version                             = {0}{1}" ,			// Format Control String
                    Environment.OSVersion.ToString ( ) ,						// OS Version
                    Environment.NewLine );										// Embedded Newline to add white space
                Console.WriteLine (
                    s_smTheApp.AppExceptionLogger.OutputOptionsDisplay (
                        "Initial                 " ) );
                s_smTheApp.AppExceptionLogger.OutputOptionTurnOn (
                    ExceptionLogger.OutputOptions.EventLog );
                Console.WriteLine (
                    s_smTheApp.AppExceptionLogger.OutputOptionsDisplay (
                        "Event Logging Enabled   " ) );
                Console.WriteLine (
                    "Default Windows Event Source ID String = {0}" ,
                    s_smTheApp.AppExceptionLogger.AppEventSourceID );
                Console.WriteLine (
                    "{1}Real Entry Assembly Name               = {0}{1}" ,
                    s_smTheApp.AppRootAssemblyFileName ,
                    Environment.NewLine );

                //	------------------------------------------------------------
                //  Use GetAssemblyVersionInfo to display version details about
                //  the entry assembly and one of its dependents.
                //	------------------------------------------------------------

                ExerciseGetAssemblyVersionInfo ( );

                //	------------------------------------------------------------
                //	DisplayDefaultAppDomainProperties is the improved method
                //	from the VSHostingProcess_Demo project, which replaces the
                //	original that was in this module, and found to be deficient.
                //	Since I imported the entire class, I decided that I may as
                //	will get the benefit of DisplayProcessProperties, its other
                //	reporting method.
                //	------------------------------------------------------------

                AppDomainDetails.DisplayDefaultAppDomainProperties ( );
                AppDomainDetails.DisplayProcessProperties ( s_smTheApp.AppStartupTimeUtc );

                ExerciseDynamicExceptionReporting ( );

                if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_DETECTION )
                {   // Focus on this single test, without any other stuff to interfere.
                    System.Diagnostics.Debugger.Launch ( );
                    EvaluateConsoleHandleStates ( );
                    ExploreProcessModulesCollection ( );
                    ExerciseClearScreen ( );
                }   // TRUE block, if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_DETECTION )
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_STATE_TESTS )
                {
                    Console.WriteLine ( "This method has been retired." );
                }   // TRUE block, else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_STATE_TESTS )
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_ENUMERATE_EXCEPTION_GUIDS )
                {   // This test gathers research data to go into the table of exception message formats.
                    EnumExcpetionGUIDs ( );
                }   // TRUE block, else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_ENUMERATE_EXCEPTION_GUIDS )
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_GENERATE_EXCEPTION_MESSAGE_FORMAT_TABLE )
                {   // This task generates the exception message configuration file.
                    GenerateExceptionMessageFormatTable ( );
                }   // TRUE block, else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_ENUMERATE_EXCEPTION_GUIDS )
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_LIST_COMMON_STRINGS )
                {   // This task enumerates the publicly visible string resources in a specified namespace.
                    NewClassTests_20140914.EnumerateStringResourcesInAssembly (
                        ref intTestNumber ,
                        System.Reflection.Assembly.GetAssembly (
                            typeof ( ArrayInfo ) ) );
                }   // TRUE block, else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_LIST_COMMON_STRINGS )
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_EVENT_MSG_CLEANUP_TESTS )
                {
                    EventMessageCleanupTests ( ref intTestNumber );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_DIRECT_DEPENDENT_ASSEMBLY_ENUMERATION )
                {
                    ExercisseDirectDependentAssemblyEnumerator ( ref intTestNumber );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_EMBEDDED_NVC_TEST )
                {
                    NewClassTests_20140914.Exercise_NameValueCollectionFromEmbbededList ( ref intTestNumber );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_STRING_FIXUPS )
                {
                    NewClassTests_20140914.ExerciseStringFixups_Extended ( );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_LIST_OBJECT_PROPERTIES )
                {
                    EnumerateObjectProperties ( 
                        s_smTheApp ,
                        MagicNumbers.ZERO ,
                        nameof ( s_smTheApp ) );
                    EnumerateObjectProperties (
                        s_smTheApp.AppExceptionLogger ,
                        MagicNumbers.PLUS_FOUR ,
                        nameof ( s_smTheApp.AppExceptionLogger ) );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_EXERCISE_TRUNCATEVALUETOONELINE )
                {
                    Exercise_TruncateValueToOneLine ( );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REMOVE_AT )
                {
                    Exercise_RemoveAtArrayExtensionMethod ( );
                }
                else if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_BASE64_ENCODING )
                {
                    Base64RoundTripExperiments.Exercise_Base64_ToAndFrom ( );
                }
                else
                {	// Run the whole set, starting with this test, which leaves the flags set so that the original message can be reconstructed from a psLogList export.
                    EnumerateDependentAssemblies ( );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"EnumerateDependentAssemblies" );

                    EnumerateObjectProperties (
                        s_smTheApp ,
                        MagicNumbers.ZERO ,
                        nameof ( s_smTheApp ) );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"EnumerateObjectProperties test 1 of 2" );

                    EnumerateObjectProperties (
                        s_smTheApp.AppExceptionLogger ,
                        MagicNumbers.PLUS_FOUR ,
                        nameof ( s_smTheApp.AppExceptionLogger ) );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"EnumerateObjectProperties test 2 of 2" );

                    ExerciseEnvTokenExpander ( );
                    Exercise_TruncateValueToOneLine ( );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"Exercise_TruncateValueToOneLine" );
                    Exercise_RemoveAtArrayExtensionMethod ( );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"Exercise_RemoveAtArrayExtensionMethod" );
                    Base64RoundTripExperiments.Exercise_Base64_ToAndFrom ( );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"Exercise_Base64_ToAndFrom" );

                    try
                    {
                        intTestNumber = ExercisseDirectDependentAssemblyEnumerator ( ref intTestNumber );
                    }
                    catch ( Exception exAll )
                    {
                        s_smTheApp.AppExceptionLogger.ReportException ( exAll );
                        s_smTheApp.AppReturnCode = MagicNumbers.ERROR_RUNTIME;
                    }

                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"ExercisseDirectDependentAssemblyEnumerator" );

                    if ( s_smTheApp.AppReturnCode > MagicNumbers.ERROR_SUCCESS )
                    {   // Bug out if error
                        goto FinalReport;
                    }   // if ( s_smTheApp.AppReturnCode > MagicNumbers.ERROR_SUCCESS )

                    intTestNumber = RecoveredExceptionTests ( ref intTestNumber );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"RecoveredExceptionTests" );

                    intTestNumber = ExerciseStringFixups ( ref intTestNumber );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"ExerciseStringFixups" );

                    {   // Constrain the scope of strings strMsgWithEscapedTabs and strDetailRowFormatString.
                        string strMsgWithEscapedTabs = Properties.Resources.MESSAGE_CONTAINS_ESCAPED_TABS;
                        Console.WriteLine ( @"MESSAGE_CONTAINS_ESCAPED_TABS = {0}" , strMsgWithEscapedTabs );
                        string strDetailRowFormatString = strMsgWithEscapedTabs.ReplaceEscapedTabsInStringFromResX ( );
                        Console.WriteLine ( @"strDetailRowFormatString      = {0}" , strDetailRowFormatString );
                    }   // Let strings strMsgWithEscapedTabs and strDetailRowFormatString go out of scope.

                    intTestNumber = ShowFileDetailsTests.Exercise ( ref intTestNumber );
                    PauseForPictures (
                        OMIT_LINEFEED ,
                        @"ShowFileDetailsTests" );

                    intTestNumber = LineEndingFixupTests.Exercise ( ref intTestNumber );
                    PauseForPictures (
                        OMIT_LINEFEED ,
                        @"LineEndingFixupTests" );

                    intTestNumber = NewClassTests_20140914.Exercise_NameValueCollectionFromEmbbededList ( ref intTestNumber );
                    PauseForPictures (
                        OMIT_LINEFEED ,
                        @"Exercise_NameValueCollectionFromEmbbededList" );

                    intTestNumber = EventMessageCleanupTests ( ref intTestNumber );
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"EventMessageCleanupTests" );

                    //	------------------------------------------------------------
                    //	This method exercises the remaining routines that I moved
                    //	into this constellation of libraries from the SharedUtl set.
                    //	This routine handles its own exceptions, and looks after its
                    //	own test selections based on command line arguments, just as
                    //	do the other routines in this assembly.
                    //	------------------------------------------------------------

                    if ( ImportsFromSharedUtl4.DoTheTests ( pastrArgs ) == ImportsFromSharedUtl4.STATUS_SKIP_FURTHER_TESTS )
                    {   // This is one of those very rare instances when I think the goto statement justifies its existence.
                        goto FinalReport;
                    }   // if ( ImportsFromSharedUtl4.DoTheTests ( pastrArgs ) == ImportsFromSharedUtl4.STATUS_SKIP_FURTHER_TESTS )

                    //  --------------------------------------------------------
                    //  Put the new GetAssemblyProductAndVersion method through 
                    //	its paces.
                    //  --------------------------------------------------------

                    Console.WriteLine (
                        Properties.Resources.MSG_VERSIONIFNO_TESTS_BEGIN ,
                        Environment.NewLine );

                    try
                    {   // The last iteration is an invalid enumeration value, and it always throws.
                        for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_aenmAssemblyVersionRequests.Length ; intJ++ )
                        {
                            Console.WriteLine (
                                Properties.Resources.MSG_VERSIONINFO ,
                                s_aenmAssemblyVersionRequests [ intJ ] ,
                                s_aenmAssemblyVersionRequests [ intJ ] ,
                                s_smTheApp.GetAssemblyProductAndVersion ( ( StateManager.AssemblyVersionRequest ) s_aenmAssemblyVersionRequests [ intJ ] ) );
                        }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_aenmAssemblyVersionRequests.Length ; intJ++ )
                    }
                    catch ( Exception exAll )
                    {
                        s_smTheApp.AppExceptionLogger.ReportException ( exAll );
                    }

                    Console.WriteLine (
                        Properties.Resources.MSG_VERSIONINFO_TESTS_DONE ,
                        s_smTheApp.GetAssemblyProductAndVersion ( ) ,
                        Environment.NewLine );
                    PauseForPictures (
                        OMIT_LINEFEED ,
                        @"StateManager.GetAssemblyProductAndVersion" );

                    //  --------------------------------------------------------
                    //  Display information about the library under test.
                    //  --------------------------------------------------------

#if DEBUGGER_IN_SHELL_SCRIPT
                    if ( System.Diagnostics.Debugger.IsAttached )
                    {
                        Console.Error.WriteLine ( @"***** Assembly is already being debugged ahead of EvaluateConsoleHandleStates." );
                    }
                    else
                    {
                        Console.Error.Write ( @"***** Assembly launched outside a debugger. Attaching it to one ahead of EvaluateConsoleHandleStates ..." );

                        if ( System.Diagnostics.Debugger.Launch ( ) )
                        {
                            Console.Error.WriteLine ( @" succeeded" );
                        }   // TRUE (anticipated outcome) block, if ( System.Diagnostics.Debugger.Launch ( ) )
                        else
                        {
                            Console.Error.WriteLine ( @" FAILED" );
                        }   // FALSE (unanticipated outcome) block, if ( System.Diagnostics.Debugger.Launch ( ) )
                    }   // if ( System.Diagnostics.Debugger.IsAttached )
#endif  // DEBUGGER_IN_SHELL_SCRIPT

                    Console.WriteLine (
                        Properties.Resources.MSG_ASBSOLUTE_ASSEMBLYNAME ,
                        s_smTheApp.GetAssemblyFQFN ( ) ,
                        Environment.NewLine );
                    Console.WriteLine ( Properties.Resources.MSG_ASSEMBLY_SUBSYSTEM ,
                        s_smTheApp.AppSubsystemID ,
                        s_smTheApp.GetAppSubsystemString ( ) ,
                        Environment.NewLine );

                    //  --------------------------------------------------------
                    //  Display information about the standard stream handles.
                    //  --------------------------------------------------------

                    EvaluateConsoleHandleStates ( );

                    MessageInColor mic = new MessageInColor (
                        ConsoleColor.White ,
                        ConsoleColor.DarkGray );
                    mic.WriteLine (
                        Properties.Resources.COLOR_MESSAGE_TEXT ,
                        mic.MessageForegroundColor ,
                        mic.MessageBackgroundColor ,
                        Environment.NewLine );

                    Console.WriteLine (
                        Properties.Resources.MSG_SHOWING_LIBRARY_INFO ,		// This is the message template.
                        Environment.NewLine );                              // This token adds newlines my way.

                    PauseForPictures (
                        OMIT_LINEFEED ,
                        @"EvaluateConsoleHandleStates" );                   // SKIPPED

                    ShowCurrentDefaultErrorMessageColors ( Properties.Resources.MSG_SHOWING_CONFIGURED_COLORS );

                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"ShowCurrentDefaultErrorMessageColors" );          // SKIPPED

                    Console.WriteLine (
                        @"{1}The following message is the value of the static ExceptionLogger.s_strSettingsOmittedFromConfigFile property:{1}{1}{0}" ,
                        ExceptionLogger.s_strSettingsOmittedFromConfigFile ,
                        Environment.NewLine );

                    ErrorMessagesInColor emcOld = s_smTheApp.AppExceptionLogger.SaveCurrentColors ( );

                    Console.WriteLine ( s_smTheApp.AppExceptionLogger.OutputOptionsDisplay ( "Before Adding STDERR    " ) );
                    s_smTheApp.AppExceptionLogger.OutputOptionTurnOn ( ExceptionLogger.OutputOptions.StandardError );
                    Console.WriteLine ( s_smTheApp.AppExceptionLogger.OutputOptionsDisplay ( "Output to Standard Error" ) );

                    int intTestCase = ArrayInfo.ARRAY_FIRST_ELEMENT;

                    try
                    {
                        Console.WriteLine (
                            Properties.Resources.TEST_EXCEPTION_LABEL ,
                            ++intTestCase ,
                            Environment.NewLine );
                        string strMsg = string.Format (
                            Properties.Resources.TEST_EXCEPTION_MESSAGE ,
                            intTestCase );
                        throw new Exception ( strMsg );
                    }
                    catch ( Exception exAll )
                    {
                        s_smTheApp.AppExceptionLogger.ReportException ( exAll );
                    }

                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"Default AppExceptionLogger option flags reinstated" );// SKIPPED

                    ChangeDefaultErrorMessageColors ( );
                    ShowCurrentDefaultErrorMessageColors ( Properties.Resources.MSG_SHOWING_PROGRAMMATIC_COLORS );

                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"ShowCurrentDefaultErrorMessageColors" );          // SKIPPED

                    s_smTheApp.AppExceptionLogger.RestoreSavedColors ( );

                    try
                    {
                        Console.WriteLine (
                            Properties.Resources.TEST_EXCEPTION_LABEL ,
                            ++intTestCase ,
                            Environment.NewLine );
                        string strMsg = string.Format (
                            Properties.Resources.TEST_EXCEPTION_MESSAGE ,
                            intTestCase );
                        throw new Exception ( strMsg );
                    }
                    catch ( Exception exAll )
                    {
                        s_smTheApp.AppExceptionLogger.ReportException ( exAll );
                    }
                }	// FALSE block, if ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_DETECTION )
            }
            catch ( Exception exAll )
            {
                s_smTheApp.AppExceptionLogger.ReportException ( exAll );
                s_smTheApp.AppReturnCode = MagicNumbers.ERROR_RUNTIME;
            }

            //	----------------------------------------------------------------
            //	Unless restricted to one test by a command line argument, run
            //	the remainder of the tests.
            //	----------------------------------------------------------------

            if ( pastrArgs.Length == CmdLneArgsBasic.NONE )
            {
                UnaryMinusExercises ( );

                if ( Logic.Unless ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_DETECTION ) )
                {
                    PauseForPictures (
                        APPEND_LINEFEED ,
                        @"UnaryMinusExercises" );                               // NOT SKIPPED
                }   // if ( Logic.Unless ( pastrArgs.Length > CmdLneArgsBasic.NONE && pastrArgs [ ArrayInfo.ARRAY_FIRST_ELEMENT ] == Properties.Resources.CMDARG_REDIRECTION_DETECTION ) )

                try
                {
                    //	----------------------------------------------------
                    //  Exercise the new utility classes.
                    //
                    //	To simplify the back-port, these tests get a
                    //	dedicated class, NewClassTests_20140914.
                    //	----------------------------------------------------

                    int intRetCode = NewClassTests_20140914.ArrayInfoExercises ( ref intTestNumber );

                    if ( intRetCode != MagicNumbers.ERROR_SUCCESS )
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following ArrayInfoExercises" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"ArrayInfoExercises" ,
                                intRetCode ) );
                    }   // if ( intRetCode != MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.CSVFileInfoExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"CSVFileInfoExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following CSVFileInfoExercises" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"CSVFileInfoExercises" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.CSVFileInfoExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.EnumerateStringResourcesInAssembly (
                        ref intTestNumber ,
                        System.Reflection.Assembly.GetAssembly (
                            typeof ( ArrayInfo ) ) ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"EnumerateStringResourcesInAssembly for WizardWrx.Common.dll" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine (
                            @"Exception thrown following {0}" ,
                            Properties.Resources.CMDARG_LIST_COMMON_STRINGS );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                Properties.Resources.CMDARG_LIST_COMMON_STRINGS ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.EnumerateStringResourcesInAssembly (

                    intRetCode = ListEmbeddedResources (
                        ref intTestNumber ,
                        typeof ( ArrayInfo ) ,
                        Properties.Settings.Default.Common_Strings_Report_FileName );
                    intRetCode = ListEmbeddedResources (
                        ref intTestNumber ,
                        typeof ( Program ) ,
                        Properties.Settings.Default.Startup_Assembly_Strings_Report_FileName );

                    if ( ( intRetCode = NewClassTests_20140914.DisplayFormatsExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"DisplayFormatsExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following DisplayFormats" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"DisplayFormats" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.DisplayFormatsExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.FileIOFlagsExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"FileIOFlagsExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following FileIOFlags" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"FileIOFlags" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.FileIOFlagsExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.ListInfoExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"ListInfoExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following ListInfo" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"ListInfo" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.ListInfoExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    //	----------------------------------------------------
                    //	These two tests fit comfortably on one screen.
                    //	----------------------------------------------------

                    if ( ( intRetCode = NewClassTests_20140914.PathPositionsExercises ( ref intTestNumber ) ) != MagicNumbers.ERROR_SUCCESS )
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following PathPositions" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"PathPositions" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.PathPositionsExercises ( ref intTestNumber ) ) != MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.SpecialCharactersExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"SpecialCharactersExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following SpecialCharacters" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"SpecialCharacters" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.SpecialCharactersExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.SpecialStringExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"SpecialStringExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following SpecialStrings" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"SpecialStrings" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.SpecialStringExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.ChopChop ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"ChopChop" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following ChopChop" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"ChopChop" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.ChopChop ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.CapitalizeWordsExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"CapitalizeWordsExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following CapitalizeWordsExercises" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"CapitalizeWordsExercises" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.CapitalizeWordsExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

                    if ( ( intRetCode = NewClassTests_20140914.EnumFromStringExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )
                    {
                        PauseForPictures (
                            OMIT_LINEFEED ,
                            @"EnumFromStringExercises" );
                    }
                    else
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following EnumFromStringExercises" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"EnumFromStringExercises" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.EnumFromStringExercises ( ref intTestNumber ) ) == MagicNumbers.ERROR_SUCCESS )

#if DEBUGGER_IN_SHELL_SCRIPT
                    if ( System.Diagnostics.Debugger.IsAttached )
                    {
                        Console.Error.WriteLine ( @"***** Assembly is already being debugged ahead of MoreMathTests." );
                    }
                    else
                    {
                        Console.Error.Write ( @"***** Assembly launched outside a debugger. Attaching it to one ahead of MoreMathTests ..." );

                        if ( System.Diagnostics.Debugger.Launch ( ) )
                        {
                            Console.Error.WriteLine ( @" succeeded" );
                        }   // TRUE (anticipated outcome) block, if ( System.Diagnostics.Debugger.Launch ( ) )
                        else
                        {
                            Console.Error.WriteLine ( @" FAILED" );
                        }   // FALSE (unanticipated outcome) block, if ( System.Diagnostics.Debugger.Launch ( ) )
                    }   // if ( System.Diagnostics.Debugger.IsAttached )
#endif  // DEBUGGER_IN_SHELL_SCRIPT

                    MoreMathTests.Run ( );

                    //	----------------------------------------------------
                    //	The last stop doesn't merit a photo op, because the
                    //	program is ending, and there is ample room
                    //	remaining on the screen to display the end of job
                    //	message.
                    //	----------------------------------------------------

                    if ( ( intRetCode = NewClassTests_20140914.UtilsExercises ( ref intTestNumber ) ) != MagicNumbers.ERROR_SUCCESS )
                    {
                        s_smTheApp.AppExceptionLogger.ErrorMessageColors.WriteLine ( @"Exception thrown following UtilsExercises" );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                                @"UtilsExercises" ,
                                intRetCode ) );
                    }   // if ( ( intRetCode = NewClassTests_20140914.UtilsExercises ( ref intTestNumber ) ) != MagicNumbers.ERROR_SUCCESS )
                }
                catch ( Exception errAnyKind )
                {
                    s_smTheApp.AppExceptionLogger.ReportException ( errAnyKind );
                }

                //	------------------------------------------------------------
                //	Exercise the FileNameTricks and special messages classes.
                //	------------------------------------------------------------

                FileNameTricks_Exerciser.Drill ( );
                ExerciseSpecialMessageGenerators ( );
                Console.Beep ( );		// WaitForCarbonUnit emits a beep.
                Console.ReadLine ( );

                //	------------------------------------------------------------
                //	Write the final report, clean up, and shut down.
                //	------------------------------------------------------------

                ExceptionLogger.TimeStampedTraceWrite (
                    string.Format (
                        "EOJ {0}." ,
                        s_smTheApp.AppRootAssemblyFileBaseName ) );
                System.Diagnostics.Trace.Close ( );

                //	------------------------------------------------------------
                //	Exercise the new duplicate output suppression feature of the
                //	ExceptionLogger ReportException methods.
                //	------------------------------------------------------------

                DeduplicateExceptionLogs ( );
            }   // if ( pastrArgs.Length == CmdLneArgsBasic.NONE )

            FinalReport:

            Console.WriteLine (
                Properties.Resources.EOJ_MSG_TPL ,
                new string [ ]
                {
                    s_smTheApp.AppRootAssemblyFileBaseName ,
                    s_smTheApp.AppUptime.ToString ( ) ,
                    Environment.NewLine
                } );
            Environment.Exit ( s_smTheApp.AppReturnCode );
        }   // Main method


        #region Local Test Implementations
        private static void DeduplicateExceptionLogs ( )
        {
            Console.WriteLine (
                "{1}ExceptionLogger flags before changes for test: {0}" ,
                s_smTheApp.AppExceptionLogger.OptionFlags ,
                Environment.NewLine );
            s_smTheApp.AppExceptionLogger.OutputOptionTurnOn (
                ExceptionLogger.OutputOptions.ActivePropsToStdOut );
            Console.WriteLine (
                "ExceptionLogger flags after changes for test: {0}{1}" ,
                s_smTheApp.AppExceptionLogger.OptionFlags ,
                Environment.NewLine );

#if DOUBLE_TROUBLE_TEST
                System.Diagnostics.Debugger.Launch ( );
#endif  // #if DOUBLE_TROUBLE_TEST

            try
            {
                throw new Exception ( "Double Trouble: Up to version 6.2, this message would have displayed twice when neither standard stream is redirected." );
            }
            catch ( Exception exDoubleTrouble )
            {
                s_smTheApp.AppExceptionLogger.ReportException ( exDoubleTrouble );
            }
        }	// DeduplicateExceptionLogs method


        private static void EnumExcpetionGUIDs ( )
        {
            using ( StreamWriter swOutputStream = new StreamWriter (
                AbsolutePathStringFromSettings ( Properties.Settings.Default.ExceptionGUIDsListingFile ) ,
                FileIOFlags.FILE_OUT_CREATE ,
                System.Text.Encoding.ASCII ,
                MagicNumbers.CAPACITY_08KB ) )
            {
                swOutputStream.WriteLine ( "FullName\tName\tGUID\tTypeHandle\tGUIDBytes" );			// Label the columns.

                foreach ( Type typCurrent in s_atypCommonExceptionTypes )
                {
                    swOutputStream.WriteLine (
                        "{0}\t{1}\t{2}\t{3}\t{4}" ,
                        new object [ ]
                        {
                            typCurrent.FullName ,													// Token 0
                            typCurrent.Name ,														// Token 1
                            typCurrent.GUID ,														// Token 2
                            typCurrent.TypeHandle.Value.ToString ( NumericFormats.HEXADECIMAL_8 ) ,	// Token 3
                            SerializeByteArray ( typCurrent.GUID.ToByteArray ( ) )					// Token 4
                        } );
                }	// foreach ( Type typCurrent in s_atypCommonExceptionTypes )

                FileInfo fiOutputFile = new FileInfo ( AbsolutePathStringFromSettings ( Properties.Settings.Default.ExceptionGUIDsListingFile ) );
                Console.WriteLine (
                    "{1}Properties of selected System Object Types written onto file {0}{1}" ,
                    fiOutputFile.FullName ,
                    Environment.NewLine );
            }	// using ( System.IO.StreamWriter swOutputStream = new System.IO.StreamWriter ( ... )
        }	// EnumExcpetionGUIDs method


        private static void EnumerateDependentAssemblies ( )
        {	// Segregating this little block sets a scope boundary around the DependentAssemblies object that it creates.
            DependentAssemblies appDependents = new DependentAssemblies ( );
            appDependents.EnumerateDependents ( );				// List the dependents' full names.

            string strDependentAssemblyInfoReportFileName = AbsolutePathStringFromSettings ( Properties.Settings.Default.DependentAssemblyInfoReport );

            if ( string.IsNullOrEmpty ( strDependentAssemblyInfoReportFileName ) )
            {
                appDependents.DisplayProperties ( );
            }	// TRUE (degenerate case block, if ( string.IsNullOrEmpty ( strDependentAssemblyInfoReportFileName ) )
            else
            {
                using ( StreamWriter swOut = new StreamWriter ( strDependentAssemblyInfoReportFileName ,
                                                                FileIOFlags.FILE_OUT_CREATE ,
                                                                System.Text.Encoding.UTF8 ,
                                                                MagicNumbers.CAPACITY_08KB ) )
                {
                    appDependents.DisplayProperties (
                        swOut ,
                        SpecialCharacters.TAB_CHAR );
                }	// using ( StreamWriter swOut = new StreamWriter ( strDependentAssemblyInfoReportFileName , FileIOFlags.FILE_OUT_CREATE , System.Text.Encoding.UTF8 , MagicNumbers.CAPACITY_08KB ) )
            }	// FALSE (standard case block, if ( string.IsNullOrEmpty ( strDependentAssemblyInfoReportFileName ) )
        }	// EnumerateDependentAssemblies method


        private static void EnumerateObjectProperties (
            object pobj ,
            int pintLeftPadding = MagicNumbers.ZERO ,
            string pstrObjectName = null )
        {
            if ( pintLeftPadding == MagicNumbers.ZERO )
            {
                ObjectPropertyEnumerators.ListObjectProperties (
                    pstrObjectName ,                                            // string                         pstrNameOfObject
                    pobj );                                                     // System.Reflection.BindingFlags penmbindingFlags
                ObjectPropertyEnumerators.ListObjectPropertyTypesAndValues (
                    pstrObjectName ,                                            // string                         pstrNameOfObject
                    pobj );                                                     // System.Reflection.BindingFlags penmbindingFlags
            }   // TRUE (degenerate case, no left padding) block, if ( pintLeftPadding == MagicNumbers.ZERO )
            else
            {
                ObjectPropertyEnumerators.ListObjectProperties (
                    pstrObjectName ,                                            // string                         pstrNameOfObject
                    pobj ,                                                      // System.Reflection.BindingFlags penmbindingFlags
                    pintLeftPadding );                                          // int                            pintLeftPadding
                ObjectPropertyEnumerators.ListObjectPropertyTypesAndValues (
                    pstrObjectName ,                                            // string                         pstrNameOfObject
                    pobj ,                                                      // System.Reflection.BindingFlags penmbindingFlags
                    pintLeftPadding );                                          // int                            pintLeftPadding
            }   // FALSE (standard case, left padding specified)) block, if ( pintLeftPadding == MagicNumbers.ZERO )
        }   // private static void EnumerateObjectProperties


        private static int EventMessageCleanupTests ( ref int pintTestNumber )
        {
            const string DEMO_EXCEPTION_REPORT = @"Exception condition: {0}";
            const string RESTORE_ERROR = @"The RestoreSavedOptions yielded an unexpected outcome.{2}    Options expected to be restored = {0}.{2}    Options actually restored       = {1}.";

            NewClassTests_20140914.BeginTest (
                ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                ref pintTestNumber );

            ExceptionLogger logger = s_smTheApp.AppExceptionLogger;

            foreach ( OutputOptionTestData utpTestCase in s_utpOutputOptionTestData )
            {	// Since each iteration raises an exception, most of the body of this loop must be a TRY block.

                ExceptionLogger.OutputOptions enmPrevOpts = logger.SaveCurrentOptions ( );

                try
                {
                    if ( utpTestCase.NewFlags != ExceptionLogger.OutputOptions.NoFlags )
                    {	// Save the method call for when it actually changes something.
                        logger.OutputOptionTurnOn ( utpTestCase.NewFlags );
                    }	// if ( utpTestCase.NewFlags != ExceptionLogger.OutputOptions.NoFlags )

                    string strStateMessage = logger.OutputOptionsDisplay (
                        utpTestCase.Label );
                    Console.WriteLine (
                        strStateMessage );

                    string strPlannedExceptionMessage = string.Format (
                        DEMO_EXCEPTION_REPORT ,					// Format control string
                        strStateMessage );						// Format Item 0: Exception condition: {0}
                    throw new Exception ( strPlannedExceptionMessage );
                }
                catch ( Exception exAll )
                {
                    logger.ReportException ( exAll );
                }
                finally
                {	// Since it would execute regardless, this finally block is syntactic sugar.
                    if ( utpTestCase.NewFlags != ExceptionLogger.OutputOptions.NoFlags )
                    {	// Save the method call for when it actually changes something.
                        if ( enmPrevOpts != logger.RestoreSavedOptions ( ) )
                        {	// Raise an exception in the unlikely event that this happens, and let it bubble up to the main routine.
                            string strUnplannedMsg = string.Format (
                                RESTORE_ERROR ,					// Format control string
                                enmPrevOpts ,					// Format Item 0:    Options expected to be restored = {0}.
                                logger.OptionFlags ,			// Format Item 1:    Options actually restored       = {1}.
                                Environment.NewLine );			// Format Item 2: Embedded platform-dependent newline
                            throw new Exception ( strUnplannedMsg );
                        }	// if ( enmPrevOpts != logger.RestoreSavedOptions ( ) )
                    }	// if ( utpTestCase.NewFlags != ExceptionLogger.OutputOptions.NoFlags )
                }
            }	// foreach ( OutputOptionTestData utpTestCase in s_utpOutputOptionTestData )

            logger.OutputOptionTurnOn ( ExceptionLogger.OutputOptions.ReplaceNewlines );
            logger.OutputOptionsDisplay ( OUTPUT_OPTIONS_FINAL );

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // EventMessageCleanupTests method


        private static void ExerciseClearScreen ( )
        {
            Console.WriteLine (
                "{0}To exercise the ClearScreen function, the next statement will clear the console buffer.{0}If you need the contents of the output buffer, save it now, before you press the return.{0}" ,
                Environment.NewLine );
            Console.ReadLine ( );

            MessageInColor.SafeConsoleClear ( );

            Console.WriteLine (
                "The {0} method just cleared the console screen.{1}When you press the Return key, the program will fall through, display its exit report, and terminate.{1}" ,
                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                Environment.NewLine );
            Console.ReadLine ( );
        }   // ExerciseClearScreen method


        private static int ExercisseDirectDependentAssemblyEnumerator ( ref int pintTestNumber )
        {
            const int COL_CASE_NUMBER = ArrayInfo.ARRAY_FIRST_ELEMENT;
            const int COL_CASE_LABEL = COL_CASE_NUMBER + ArrayInfo.NEXT_INDEX;
            const int COL_CASE_VALUE = COL_CASE_LABEL + ArrayInfo.NEXT_INDEX;
            const int COL_COUNT_EXPECTED = COL_CASE_VALUE + ArrayInfo.NEXT_INDEX;

            const string CASE_LABEL_PATH = @"TestAssemblyDirectoryName";
            const string LABEL_ROW = @"Case #	Case Label	Case Value";

            Console.WriteLine (
                @"{1}Begin test {0}: ExercisseDirectDependentAssemblyEnumerator:{1}" ,
                ++pintTestNumber ,
                Environment.NewLine );

            string strAbsoluteInputFileName = AbsolutePathStringFromSettings ( Properties.Settings.Default.DependentAssemblies_Test_Case_Data_FileName );

            Console.Write (
                @"{1}    Reading list from input file {0}" ,
                strAbsoluteInputFileName ,
                Environment.NewLine );

            if ( File.Exists ( strAbsoluteInputFileName ) )
            {
                string [ ] astrInputRecords = File.ReadAllLines ( strAbsoluteInputFileName );

                if ( astrInputRecords.Length > CSVFileInfo.FIRST_RECORD )
                {
                    Console.WriteLine (
                        @", which defines {0} test cases:{1}" ,
                        ArrayInfo.IndexFromOrdinal (
                            astrInputRecords.Length ) ,
                        Environment.NewLine );

                    if ( astrInputRecords [ ArrayInfo.ARRAY_FIRST_ELEMENT ].Equals ( LABEL_ROW ) )
                    {
                        string strTestAssemblyDirectoryName = null;

                        for ( int intJ = ArrayInfo.ARRAY_SECOND_ELEMENT ;
                                  intJ < astrInputRecords.Length ;
                                  intJ++ )
                        {
                            string [ ] astrCaseParameters = astrInputRecords [ intJ ].Split ( SpecialCharacters.TAB_CHAR );

                            if ( astrCaseParameters.Length.Equals ( COL_COUNT_EXPECTED ) )
                            {
                                if ( astrCaseParameters [ COL_CASE_LABEL ].Equals ( CASE_LABEL_PATH ) )
                                {
                                    strTestAssemblyDirectoryName = astrCaseParameters [ COL_CASE_VALUE ];
                                    Console.WriteLine (
                                        @"        Default Directory for Test Assemblies = {0}{1}" ,
                                        strTestAssemblyDirectoryName ,
                                        Environment.NewLine );
                                }   // TRUE (anticipated outcome of first iteration) block, if ( astrCaseParameters [ COL_CASE_LABEL ].Equals ( CASE_LABEL_PATH ) )
                                else
                                {
                                    Console.WriteLine (
                                        @"        Case # {0}: {1} test subject = {2}{3}" ,
                                        astrCaseParameters [ COL_CASE_NUMBER ] ,// Format Item 0: Case # {0}
                                        astrCaseParameters [ COL_CASE_LABEL ] , // Format Item 1: : {1}
                                        astrCaseParameters [ COL_CASE_VALUE ] , // Format Item 2: test subject = {2}
                                        Environment.NewLine );                  // Format Item 3: Platform-dependent newline at end of line
                                    string strAbsoluteAssemblyFileName = Path.Combine (
                                        strTestAssemblyDirectoryName ,
                                        astrCaseParameters [ COL_CASE_VALUE ] );

                                    if ( File.Exists ( strAbsoluteAssemblyFileName ) )
                                    {
                                        DependentAssemblies dependentAssemblies = new DependentAssemblies (
                                            System.Reflection.Assembly.LoadFrom (
                                                strAbsoluteAssemblyFileName ) );
                                        dependentAssemblies.EnumerateDependents ( );
                                    }   // TRUE (anticipated outcome) block, if ( File.Exists ( strAbsoluteAssemblyFileName ) )
                                    else
                                    {
                                        Console.WriteLine (
                                            @"            WARNING: Assembly file {0} cannot be found, skipping case" ,
                                            strAbsoluteAssemblyFileName );
                                    }   // FALSE (unanticipated outcome) block, if ( File.Exists ( strAbsoluteAssemblyFileName ) )
                                }   // FALSE (anticipated outcome of subsequent iterations) block, if ( astrCaseParameters [ COL_CASE_LABEL ].Equals ( CASE_LABEL_PATH ) )
                            }   // TRUE (anticipated outcome) block, if ( astrCaseParameters.Length.Equals ( COL_COUNT_EXPECTED ) )
                            else
                            {
                                Console.WriteLine (
                                    @"    ABORTING: Detail record {0} is invalid." ,
                                    intJ );
                                s_smTheApp.AppReturnCode = MagicNumbers.ERROR_RUNTIME;
                            }   // FALSE (unanticipated outcome) block, if ( astrCaseParameters.Length.Equals ( COL_COUNT_EXPECTED ) )
                        }   // for ( int intJ = ArrayInfo.ARRAY_SECOND_ELEMENT ; intJ <= astrInputRecords.Length ; intJ++ )
                    }   // TRUE (anticipated outcome) block, if ( astrInputRecords [ ArrayInfo.ARRAY_FIRST_ELEMENT ].Equals ( LABEL_ROW ) )
                    else
                    {
                        Console.WriteLine (
                            @"    ABORTING: The specified input file is invalid.{0}" ,
                            Environment.NewLine );
                        s_smTheApp.AppReturnCode = MagicNumbers.ERROR_RUNTIME;
                    }   // FALSE (unanticipated outcome) block, if ( astrInputRecords [ ArrayInfo.ARRAY_FIRST_ELEMENT ].Equals ( LABEL_ROW ) )
                }   // TRUE (anticipated outcome) block, if ( astrInputRecords.Length > CSVFileInfo.FIRST_RECORD )
                else
                {
                    Console.WriteLine (
                        @"    ABORTING: The specified input file is empty.{0}" ,
                        Environment.NewLine );
                    s_smTheApp.AppReturnCode = MagicNumbers.ERROR_RUNTIME;
                }   // FALSE (unanticipated outcome) block, if ( astrInputRecords.Length > CSVFileInfo.FIRST_RECORD )
            }   // TRUE (anticipated outcome) block, if ( File.Exists ( strAbsoluteInputFileName ) )
            else
            {
                Console.WriteLine (
                    @"    ABORTING: The specified input file cannot be found.{0}" ,
                    Environment.NewLine );
                s_smTheApp.AppReturnCode = MagicNumbers.ERROR_RUNTIME;
            }   // FALSE (unanticipated outcome) block, if ( File.Exists ( strAbsoluteInputFileName ) )

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // ExercisseDirectDependentAssemblyEnumerator


        private static void ExerciseDynamicExceptionReporting ( )
        {
            const string ASM_METHOD_NAME = @"GetTheSingleInstance";
            const string ASM_REPORT_NAME = @"ReportException";

            System.IO.IOException exIO = new IOException ( "This is a dummy Syatem.IOException Exception." );
            //ToDo: Either replace or remove this block.
            //switch ( exIO.GetType().GUID)
            //{
            //    case s_agidSupportedExceptionTypes [ 6 ]:
            //        Console.WriteLine ( "{0} is an I/O Exception." , nameof ( exIO ) );
            //        break;
            //    default:
            //        Console.WriteLine ( "{0} is some other kind of Exception." , nameof ( exIO ) );
            //        break;
            //}
            Type typeOfExceptionLogger = typeof ( WizardWrx.DLLConfigurationManager.ExceptionLogger );
            System.Reflection.AssemblyName assemblyNameOfExceptionLogger = new System.Reflection.AssemblyName (
                System.Reflection.Assembly.GetAssembly (
                    typeOfExceptionLogger ).FullName );
            DependentAssemblies daOfEntryAsm = ExerciseGetDependentAssemblyInfos ( );

            System.Reflection.Assembly asmDllCfgMgr = daOfEntryAsm.GetDependentAssemblyByName ( assemblyNameOfExceptionLogger );

            if ( !asmDllCfgMgr.ReflectionOnly )
            {   // This is a sanity check. The dependent assembly cannot be loaded into the Reflection-only context.
                System.Reflection.MethodInfo miLoggerrInstance = typeOfExceptionLogger.GetMethod (
                    ASM_METHOD_NAME ,
                    Type.EmptyTypes );
                object objLogger = miLoggerrInstance.Invoke ( null , null );
                System.Reflection.MethodInfo miReporter = typeOfExceptionLogger.GetMethod (
                    ASM_REPORT_NAME ,
                    new Type [ ] { typeof ( IOException ) } );
                object objDummy = miReporter.Invoke (
                    objLogger ,
                    new object [ ] { exIO } );
            }   // if ( !asmDllCfgMgr.ReflectionOnly )

            asmDllCfgMgr = null;
            daOfEntryAsm.DestroyDependents ( );
        }   // ExerciseDynamicExceptionReporting


        private static void ExerciseEnvTokenExpander ( )
        {
            const string STRING_CONTAINS_VALID_TOKEN = @"%APPDATA%\WizardWrx\wwBldNbrMgr.LOG";
            const string STRING_CONTAINS_UNDEFINED_TOKEN= @"%APPLICATION_DATA%\WizardWrx\wwBldNbrMgr.LOG";
            const string STRING_IS_DEVOID_OF_TOKENS = @"FooBar.LOG";

            const string ENVIRONMENT_STRING_NAME = @"APPLICATION_DATA";

            Console.WriteLine ( $"{Environment.NewLine}ExerciseEnvTokenExpander Begin:{Environment.NewLine}" );

            EnvTokenExpander envTokenExpander = EnvTokenExpander.GetTokenExpander ( );

            Console.WriteLine ( $"    Input String = {STRING_CONTAINS_VALID_TOKEN}{Environment.NewLine}    ContainsEnvToken = {envTokenExpander.ContainsEnvToken ( STRING_CONTAINS_VALID_TOKEN )}{Environment.NewLine}" );
            Console.WriteLine ( $"    Input String = {STRING_CONTAINS_UNDEFINED_TOKEN}{Environment.NewLine}    ContainsEnvToken = {envTokenExpander.ContainsEnvToken ( STRING_CONTAINS_UNDEFINED_TOKEN )}{Environment.NewLine}" );
            Console.WriteLine ( $"    Input String = {STRING_IS_DEVOID_OF_TOKENS}{Environment.NewLine}    ContainsEnvToken = {envTokenExpander.ContainsEnvToken ( STRING_IS_DEVOID_OF_TOKENS )}{Environment.NewLine}" );

            Console.WriteLine ( $"    Input String = {STRING_CONTAINS_VALID_TOKEN}{Environment.NewLine}    ExtractEnvToken = {envTokenExpander.ContainsEnvToken ( STRING_CONTAINS_VALID_TOKEN )}{Environment.NewLine}" );
            Console.WriteLine ( $"    Input String = {STRING_CONTAINS_UNDEFINED_TOKEN}{Environment.NewLine}    ExtractEnvToken = {envTokenExpander.ContainsEnvToken ( STRING_CONTAINS_UNDEFINED_TOKEN )}{Environment.NewLine}" );
            Console.WriteLine ( $"    Input String = {STRING_IS_DEVOID_OF_TOKENS}{Environment.NewLine}    ExtractEnvToken = {envTokenExpander.ContainsEnvToken ( STRING_IS_DEVOID_OF_TOKENS )}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}" );

            try
            {
                Console.WriteLine ( $"    Input String = {STRING_CONTAINS_VALID_TOKEN}{Environment.NewLine}    ExpandEnvironmentTokens = {EnvTokenExpander.ExpandEnvironmentTokens ( STRING_CONTAINS_VALID_TOKEN )}{Environment.NewLine}" );
            }
            catch ( Exception exAll )
            {
                s_smTheApp.AppExceptionLogger.ReportException ( exAll );
            }

            try
            {
                Console.WriteLine ( $"    Input String = {STRING_CONTAINS_UNDEFINED_TOKEN}{Environment.NewLine}    ExpandEnvironmentTokens = {EnvTokenExpander.ExpandEnvironmentTokens ( STRING_CONTAINS_UNDEFINED_TOKEN )}{Environment.NewLine}" );
            }
            catch ( Exception exAll )
            {
                s_smTheApp.AppExceptionLogger.ReportException ( exAll );
            }

            try
            {
                Console.WriteLine ( $"    Input String = {STRING_CONTAINS_UNDEFINED_TOKEN}{Environment.NewLine}    ExpandEnvironmentTokens = {EnvTokenExpander.ExpandEnvironmentTokens ( STRING_CONTAINS_UNDEFINED_TOKEN , SpecialStrings.EMPTY_STRING )}{Environment.NewLine}" );
            }
            catch ( Exception exAll )
            {
                s_smTheApp.AppExceptionLogger.ReportException ( exAll );
            }

            try
            {
                Console.WriteLine ( $"    Input String = {STRING_IS_DEVOID_OF_TOKENS}{Environment.NewLine}    ExpandEnvironmentTokens = {EnvTokenExpander.ExpandEnvironmentTokens ( STRING_IS_DEVOID_OF_TOKENS )}{Environment.NewLine}{Environment.NewLine}" );
            }
            catch ( Exception exAll )
            {
                s_smTheApp.AppExceptionLogger.ReportException ( exAll );
            }

            Console.WriteLine ( $"    Input String = {ENVIRONMENT_STRING_NAME}{Environment.NewLine}    TokenizeEnvVarName = {EnvTokenExpander.TokenizeEnvVarName ( ENVIRONMENT_STRING_NAME )}" );

            Console.WriteLine ( $"{Environment.NewLine}ExerciseEnvTokenExpander End{Environment.NewLine}" );
        }

        private static void ExerciseGetAssemblyVersionInfo ( )
        {
            Console.WriteLine (
                @"{0}Use GetAssemblyVersionInfo to report on the entry assembly.{0}" ,
                Environment.NewLine );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < s_aenmAttributeFriendlyName.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"    Entry Assembly {0} = {1}" ,                               // Format Control String
                    s_aenmAttributeFriendlyName [ intJ ] ,                      // Format Item 0: Entry Assembly {0}
                    AssemblyAttributeHelpers.GetAssemblyVersionInfo (           // Format Item 1:  = {1}
                        s_aenmAttributeFriendlyName [ intJ ] ,                  // AttributeFriendlyName penmAttributeFriendlyName
                        null ) );                                               // Assembly              pasm                       = null
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_aenmAttributeFriendlyName.Length ; intJ++ )

            Console.WriteLine (
                @"{0}End of GetAssemblyVersionInfo report on the entry assembly{0}" ,
                Environment.NewLine );

            Console.WriteLine (
                @"Use GetAssemblyVersionInfo to report on another assembly.{0}" ,
                Environment.NewLine );

            System.Reflection.Assembly assemblyThis = System.Reflection.Assembly.GetAssembly ( typeof ( WizardWrx.AnyCSV.CSVParseEngine ) );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < s_aenmAttributeFriendlyName.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"    CSVParseEngine Assembly {0} = {1}" ,                      // Format Control String
                    s_aenmAttributeFriendlyName [ intJ ] ,                      // Format Item 0: Entry Assembly {0}
                    AssemblyAttributeHelpers.GetAssemblyVersionInfo (           // Format Item 1:  = {1}
                        s_aenmAttributeFriendlyName [ intJ ] ,                  // AttributeFriendlyName penmAttributeFriendlyName
                        assemblyThis ) );                                       // Assembly              pasm                       = null
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_aenmAttributeFriendlyName.Length ; intJ++ )

            Console.WriteLine (
                @"{0}Use GetCompanyNameSnakeCased to return a string suitable for use as a directory name.{0}" ,
                Environment.NewLine );

            Console.WriteLine (
                @"    Entry Assembly Company Name = {0}" ,                      // Format Control String
                AssemblyAttributeHelpers.GetAssemblyVersionInfo (               // Format Item 0: Assembly Company Name = {0}
                    AssemblyAttributeHelpers.AttributeFriendlyName.Company ,    // AttributeFriendlyName penmAttributeFriendlyName
                    null ) );                                                   // Assembly              pasm                       = null
            Console.WriteLine (
                @"    Entry Assembly Company Path = {0}{1}" ,                   // Format Control String
                AssemblyAttributeHelpers.GetAssemblyCompanyNameSnakeCased (     // Format Item 0: Assembly Company Path = {0}
                    null ) ,                                                    // Assembly              pasm                       = null
                Environment.NewLine );                                          // Format Item 1: Platform-dependent newline

            Console.WriteLine (
                @"    CSVParseEngine Assembly Company Name = {0}" ,             // Format Control String
                AssemblyAttributeHelpers.GetAssemblyVersionInfo (               // Format Item 0: Assembly Company Name = {0}
                    AssemblyAttributeHelpers.AttributeFriendlyName.Company ,    // AttributeFriendlyName penmAttributeFriendlyName
                    assemblyThis ) );                                           // Assembly              pasm                       = null
            Console.WriteLine (
                @"    CSVParseEngine Assembly Company Path = {0}{1}" ,          // Format Control String
                AssemblyAttributeHelpers.GetAssemblyCompanyNameSnakeCased (     // Format Item 0: Assembly Company Path = {0}
                    assemblyThis ) ,                                            // Assembly              pasm                       = null
                Environment.NewLine );                                          // Format Item 1: Platform-dependent newline

            string strAssemblyAppDataDirName = AssemblyAttributeHelpers.GetAssemblyAppDataDirectoryName ( false );

            if ( Directory.Exists ( strAssemblyAppDataDirName ) )
            {
                if ( Directory.GetFiles ( strAssemblyAppDataDirName ).Length == ListInfo.LIST_IS_EMPTY && Directory.GetDirectories ( strAssemblyAppDataDirName ).Length == ListInfo.LIST_IS_EMPTY )
                {
                    Directory.Delete ( strAssemblyAppDataDirName );
                }   // TRUE (desired outcome) block, if ( Directory.GetFiles( strAssemblyAppDataDirName ).Length == ListInfo.LIST_IS_EMPTY && Directory.GetDirectories( strAssemblyAppDataDirName).Length == ListInfo.LIST_IS_EMPTY )
                else
                {
                    Console.WriteLine (
                        "ERROR: Directory {0} must be empty. Test results will be invalid." ,
                        strAssemblyAppDataDirName );
                }   // FALSE (undesired outcome) block, if ( Directory.GetFiles( strAssemblyAppDataDirName ).Length == ListInfo.LIST_IS_EMPTY && Directory.GetDirectories( strAssemblyAppDataDirName).Length == ListInfo.LIST_IS_EMPTY )
            }   // if ( Directory.Exists ( strAssemblyAppDataDirName ) )

            strAssemblyAppDataDirName = AssemblyAttributeHelpers.GetAssemblyAppDataDirectoryName ( false );
            Console.WriteLine (
                @"    Assembly Company Directory name = {0}{1}" ,
                strAssemblyAppDataDirName ,
                Environment.NewLine );

            if ( Directory.Exists ( strAssemblyAppDataDirName ) )
                Console.WriteLine ( @"    On the first pass, the directory exists." );
            else
                Console.WriteLine ( @"    On the first pass, the directory does NOT exist." );

            strAssemblyAppDataDirName = AssemblyAttributeHelpers.GetAssemblyAppDataDirectoryName ( true );

            if ( Directory.Exists ( strAssemblyAppDataDirName ) )
                Console.WriteLine ( @"    On the second pass, the directory exists." );
            else
                Console.WriteLine ( @"    On the second pass, the directory does NOT exist." );

            strAssemblyAppDataDirName = AssemblyAttributeHelpers.GetAssemblyAppDataDirectoryName ( true );

            if ( Directory.Exists ( strAssemblyAppDataDirName ) )
                Console.WriteLine ( @"    On the third pass, the directory exists." );
            else
                Console.WriteLine ( @"    On the third pass, the directory does NOT exist." );

            Console.WriteLine (
                @"{0}End of GetAssemblyVersionInfo report on the another assembly{0}" ,
                Environment.NewLine );
        }   // ExerciseGetAssemblyVersionInfo


        private static void Exercise_RemoveAtArrayExtensionMethod ( )
        {
            const int REMOVE_INDEX = 4;

            Console.WriteLine (
                @"{0}Exercising the new RemoveAt extension method for arrays of any type.{0}" ,
                Environment.NewLine );

            //  ---------------------------------------------------------------
            //  Exercise with an array of strings.
            //  ----------------------------------------------------------------

            Console.WriteLine (
                @"    Exercsing RemoveAt with an array of Strings:{0}" ,
                Environment.NewLine );

            {   // Constrain the scope of array astrArrayOfStrings and integer intNElements.
                string [ ] astrArrayOfStrings = s_astrRemoveAtTest1;
                int intNElements = astrArrayOfStrings.Length;
                Console.WriteLine (
                    @"        Count of elements in input array = {0}{1}" ,
                    intNElements ,
                    Environment.NewLine );

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < intNElements ;
                          intJ++ )
                {
                    Console.WriteLine (
                        @"            Original Element at index {0} = {1}" ,
                        intJ ,
                        astrArrayOfStrings [ intJ ] );
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNElements ; intJ++ )

                Console.WriteLine (
                    @"{1}            Remmoving element at index {0}{1}" ,
                    REMOVE_INDEX ,
                    Environment.NewLine );
                astrArrayOfStrings = astrArrayOfStrings.RemoveAt ( REMOVE_INDEX );
                intNElements = astrArrayOfStrings.Length;
                Console.WriteLine (
                    @"        Count of elements in output array = {0}{1}" ,
                    intNElements ,
                    Environment.NewLine );

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < intNElements ;
                          intJ++ )
                {
                    Console.WriteLine (
                        @"            Original Element at index {0} = {1}" ,
                        intJ ,
                        astrArrayOfStrings [ intJ ] );
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNElements ; intJ++ )
            }   // Array astrArrayOfStrings and integer intNElements go out of scope.

            //  ---------------------------------------------------------------
            //  Exercise with an array of integers.
            //  ----------------------------------------------------------------

            Console.WriteLine (
                @"{0}    Exercsing RemoveAt with an array of Integers:{0}" ,
                Environment.NewLine );

            {   // Constrain the scope of array aintArrayOfIntegers and integer intNElements.
                int [ ] aintArrayOfIntegers = s_aintRemoveAtTest2;
                int intNElements = aintArrayOfIntegers.Length;

                Console.WriteLine (
                    @"        Count of elements in input array = {0}{1}" ,
                    intNElements ,
                    Environment.NewLine );

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < intNElements ;
                          intJ++ )
                {
                    Console.WriteLine (
                        @"            Original Element at index {0} = {1}" ,
                        intJ ,
                        aintArrayOfIntegers [ intJ ] );
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNElements ; intJ++ )

                Console.WriteLine (
                    @"{1}            Remmoving element at index {0}{1}" ,
                    REMOVE_INDEX ,
                    Environment.NewLine );
                aintArrayOfIntegers = aintArrayOfIntegers.RemoveAt ( REMOVE_INDEX );
                intNElements = aintArrayOfIntegers.Length;
                Console.WriteLine (
                    @"        Count of elements in output array = {0}{1}" ,
                    intNElements ,
                    Environment.NewLine );

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < intNElements ;
                          intJ++ )
                {
                    Console.WriteLine (
                        @"            Original Element at index {0} = {1}" ,
                        intJ ,
                        aintArrayOfIntegers [ intJ ] );
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNElements ; intJ++ )
            }   // Array aintArrayOfIntegers and integer intNElements go out of scope.

            Console.WriteLine (
                @"{0}That's all, folks!{0}" ,
                Environment.NewLine );
        }   // private static void Exercise_RemoveAtArrayExtensionMethod


        private static void ExerciseSpecialMessageGenerators ( )
        {
            Console.WriteLine ( "{0}Exercising the new special message generators.{0}" , Environment.NewLine );
            Console.WriteLine ( "    Test 1: Get the ERROR_SUCCESS placeholder from ExceptionLogger.GetSpecifiedReservedErrorMessage         : {0}" , ExceptionLogger.GetSpecifiedReservedErrorMessage ( ExceptionLogger.ErrorExitOptions.Succeeded ) );
            Console.WriteLine ( "    Test 2: Get the runtime error message for EvtLog from ExceptionLogger.GetSpecifiedReservedErrorMessage  : {0}" , ExceptionLogger.GetSpecifiedReservedErrorMessage ( ExceptionLogger.ErrorExitOptions.RecordedInEventLog ) );
            Console.WriteLine ( "    Test 3: Get the runtime error message for STDERR from ExceptionLogger.GetSpecifiedReservedErrorMessage  : {0}" , ExceptionLogger.GetSpecifiedReservedErrorMessage ( ExceptionLogger.ErrorExitOptions.RecordedInStandardError ) );
            Console.WriteLine ( "    Test 4: Get the runtime error message for STDOUT from ExceptionLogger.GetSpecifiedReservedErrorMessage  : {0}" , ExceptionLogger.GetSpecifiedReservedErrorMessage ( ExceptionLogger.ErrorExitOptions.RecordedInStandardOutput ) );
            Console.WriteLine ( "    Test 5: Get the runtime error message for Program from ExceptionLogger.GetSpecifiedReservedErrorMessage : {0}" , s_smTheApp.AppExceptionLogger.GetReservedErrorMessage ( ) );
            Console.WriteLine ( "{0}Special message generators have been exercised.{0}" , Environment.NewLine );
        }	// ExerciseSpecialMessageGenerators method


        private static int ExerciseStringFixups ( ref int pintTestNumber )
        {
            NewClassTests_20140914.BeginTest (
                ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                ref pintTestNumber ,
                BEGIN_TEST_TAKE_METHOD_NAME_AT_FACE_VALUE );

            Console.WriteLine ( @"All tests use the following input array:{0}" , Environment.NewLine );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < s_astrStringFixups.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"    Element {0}: Input String  = {1}{4}                 Output String = {2}{4}                 ToString      = {3}" ,
                    new object [ ]
                    {
                        intJ ,                                                  // Format Item 0: Element {0}:
                        s_astrStringFixups [ intJ ].InputValue ,                // Format Item 1: Input String  = {1}
                        s_astrStringFixups [ intJ ].OutputValue ,               // Format Item 2: Output String = {2}
                        s_astrStringFixups[intJ].ToString ( ) ,                 // Format Item 3: ToString      = {3}
                        Environment.NewLine                                     // Format Item 4: Platform-dependent newline
                    } );
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_astrStringFixups.Length ; intJ++ )

            Console.WriteLine (
                @"{0}The following tests use a StringFixups instance.{0}" ,
                Environment.NewLine );

            StringFixups fixups = new StringFixups ( s_astrStringFixups );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < s_astrFixupInputs.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"    Element {0}: Input String  = {1}{3}                 Output String = {2}" ,
                    new object [ ]
                    {
                        intJ ,                                                  // Format Item 0: Element {0}:
                        s_astrFixupInputs [ intJ ] ,                            // Format Item 1: Input String  = {1}
                        fixups.ApplyFixups ( s_astrFixupInputs [ intJ ] ) ,     // Format Item 2: Output String = {2}
                        Environment.NewLine                                     // Format Item 3: Platform-dependent newline
                    } );
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_astrFixupInputs.Length ; intJ++ )

            Console.WriteLine (
                @"{0}The following tests use the ApplyFixups extension method on the System.String class.{0}" ,
                Environment.NewLine );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < s_astrFixupInputs.Length ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"    Element {0}: Input String  = {1}{3}                 Output String = {2}" ,
                    new object [ ]
                    {
                        intJ ,                                                  // Format Item 0: Element {0}:
                        s_astrFixupInputs [ intJ ] ,                            // Format Item 1: Input String  = {1}
                        s_astrFixupInputs [ intJ ].ApplyFixups (
                            s_astrStringFixups ) ,                              // Format Item 2: Output String = {2}
                        Environment.NewLine                                     // Format Item 3: Platform-dependent newline
                    } );
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < s_astrFixupInputs.Length ; intJ++ )

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // ExerciseStringFixups method


        private static void Exercise_TruncateValueToOneLine ( )
        {
            int intTestCount = s_astrStringsToTruncate.Length;
            int intDesiredWidth = StringTricks.MAXIMUM_CHARACTERS_TO_DISPLAY.ToString ( ).Length;
            Console.WriteLine ( $"\r\nUsing {intTestCount} strings of varying lengths to test TruncateValueToOneLine:\r\n" );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < intTestCount ;
                      intJ++ )
            {
                string strTruncated = StringTricks.TruncateValueToOneLine ( s_astrStringsToTruncate [ intJ ] );
                Console.WriteLine ( $"    Test {ArrayInfo.OrdinalFromIndex ( intJ )}: Length of Result = {NumericFormats.FormatIntegerLeftPadded ( strTruncated.Length , intDesiredWidth )}, Truncated Result = {strTruncated}" );
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intTestCount ; intJ++ )

            Console.WriteLine ( "\r\nEnd of TruncateValueToOneLine tests\r\n" );
        }   // private static void Exercise_TruncateValueToOneLine


        private static void GenerateExceptionMessageFormatTable ( )
        {
            const string RESERVED_GUID = @"{733C6022-332D-4D3A-B9AE-41600AAE349F}";
            Guid gidReserved = new Guid ( RESERVED_GUID );
            byte [ ] abytReservedGuid = gidReserved.ToByteArray ( );
            Console.WriteLine (
                "My Reserved GUID = {0}{2}     Byte Array  = {1}" ,
                RESERVED_GUID ,
                SerializeByteArray ( abytReservedGuid )
                , Environment.NewLine );
            Guid gidReconstituted = new Guid ( abytReservedGuid );

            if ( Guid.Equals ( gidReconstituted , gidReserved ) )
                Console.WriteLine ( "{0}The GUID reconstituted correctly from the byte array.{0}" , Environment.NewLine );
            else
                Console.WriteLine ( "{0}The GUID reconstituted INcorrectly from the byte array.{0}" , Environment.NewLine );
        }   // GenerateExceptionMessageFormatTable method


        private static DependentAssemblies ExerciseGetDependentAssemblyInfos ( )
        {
            DependentAssemblies daOfEntryAsm = new DependentAssemblies ( );

            List<DependentAssemblyInfo> lstDependents = daOfEntryAsm.GetDependentAssemblyInfos ( );
            int intCountWidth = lstDependents.Count.ToString ( ).Length;
            Console.WriteLine (
                @"Assembly {0} has the following {1} dependents:{2}" ,          // Format Control String
                daOfEntryAsm.Root.FullName ,                                    // Format Item 0: Assembly {0}
                lstDependents.Count ,                                           // Format Item 1: has the following {1}
                Environment.NewLine );                                          // Format Item 2: dependents:{2}

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < lstDependents.Count ;
                      intJ++ )
            {
                Console.WriteLine (
                    @"    {0}: {1}{2}" ,                                        // Format Control String
                    NumericFormats.FormatIntegerLeftPadded (                    // Format Item 0: Ordinal number
                        ArrayInfo.OrdinalFromIndex ( intJ ) ,                   // Convert zero based index to one based ordinal.
                        intCountWidth ) ,                                       // Use width computed from count above loop.
                    lstDependents [ intJ ].FullName ,                           // Format Item 1: Full Name
                    Logic.IsLastForIterationLT ( intJ , lstDependents.Count )   // Format Item 2: Empty string, except on last item, which gets a newline
                        ? Environment.NewLine                                   // When expression evaluates to TRUE, emit a newline.
                        : SpecialStrings.EMPTY_STRING );                        // Otherwise, emit the empty string.
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < lstDependents.Count ; intJ++ )

            return daOfEntryAsm;
        }   // ExerciseGetDependentAssemblyInfos


        private static int ListEmbeddedResources (
            ref int pintTestNumber ,
            Type ptype ,
            string pstrReportFileName )
        {
            int rintRetCode;

            string strCommonStringsReportFileName = AbsolutePathStringFromSettings ( pstrReportFileName );
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly ( ptype );

            using ( StreamWriter swCommonStringsReportFileName = new StreamWriter ( strCommonStringsReportFileName ,
                                                                                    FileIOFlags.FILE_OUT_CREATE ,
                                                                                    System.Text.Encoding.Unicode ,
                                                                                    MagicNumbers.CAPACITY_08KB ) )
            {
                if ( ( rintRetCode = NewClassTests_20140914.EnumerateStringResourcesInAssembly (
                    ref pintTestNumber ,
                    assembly ,
                    swCommonStringsReportFileName ) ) == MagicNumbers.ERROR_SUCCESS )
                {
                    PauseForPictures (
                        OMIT_LINEFEED ,
                        @"EnumerateStringResourcesInAssembly" );
                }   // TRUE (anticipated outcome) block, if ( ( rintRetCode = NewClassTests_20140914.EnumerateStringResourcesInAssembly ( ref intTestNumber , System.Reflection.Assembly.GetAssembly ( typeof ( ArrayInfo ) ) , strCommonStringsReportFileName ) ) == MagicNumbers.ERROR_SUCCESS )
                else
                {
                    throw new Exception (
                        string.Format (
                            Properties.Resources.ERRMSG_NEW_CLASS_TESTS_20140914 ,
                            Properties.Resources.CMDARG_LIST_COMMON_STRINGS ,
                            rintRetCode ) );
                }   // FALSE (unanticipated outcome) block, if ( ( rintRetCode = NewClassTests_20140914.EnumerateStringResourcesInAssembly ( ref intTestNumber , System.Reflection.Assembly.GetAssembly ( typeof ( ArrayInfo ) ) , strCommonStringsReportFileName ) ) == MagicNumbers.ERROR_SUCCESS )
            }   // using ( StreamWriter swCommonStringsReportFileName = new StreamWriter ( strCommonStringsReportFileName , FileIOFlags.FILE_OUT_CREATE , System.Text.Encoding.Unicode , MagicNumbers.CAPACITY_08KB ) )

            Console.WriteLine ( new FileInfo ( strCommonStringsReportFileName ).ShowFileDetails (
                FileInfoExtensionMethods.FileDetailsToShow.Everything ,
                string.Format (
                    @"Tab -delimited list of string resources stored in assembly {0} " ,
                    assembly.GetName ( ).Name ) ) );
            return rintRetCode;
        }   // private static int ListEmbeddedResources


        private static int RecoveredExceptionTests ( ref int pintTestNumber )
        {
            const int TEST_COUNTER_1 = 1;
            const int TEST_COUNTER_2 = 2;

            NewClassTests_20140914.BeginTest (
                ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                ref pintTestNumber ,
                BEGIN_TEST_TAKE_METHOD_NAME_AT_FACE_VALUE );

            try
            {
                throw new Exception ( @"Get a real stack trace." );
            }
            catch ( Exception ex )
            {
                RecoveredException recoveredException1 = new RecoveredException (
                    ex.Message ,
                    ex.Source ,
                    ex.StackTrace ,
                    ex.TargetSite.Name );
                ShowRecoveredExceptionProperties (
                    recoveredException1 ,
                    TEST_COUNTER_1 );
            }

            RecoveredException recoveredException2 = new RecoveredException (
                @"Simulate a recovered exception." ,
                s_smTheApp.AppRootAssemblyName.ToString ( ) ,
                Environment.StackTrace ,
                ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) );
            ShowRecoveredExceptionProperties (
                recoveredException2 ,
                TEST_COUNTER_2 );

            return NewClassTests_20140914.TestDone (
                MagicNumbers.ERROR_SUCCESS ,
                pintTestNumber );
        }   // RecoveredExceptionTests method
        #endregion // Local Test Implementations


        #region Subroutines, Some Scoped to the Application
        //	====================================================================
        //	Subroutines, one of which is marked Internal, so that routines in
        //	related classes can call it.
        //	====================================================================


        /// <summary>
        /// Convert a file name string read from a setting into an absolute file
        /// name by replacing a substitution token with the string stored in
        /// AbsoluteDataDirectoryName, a read-only string defined with internal
        /// scope in this class.
        /// </summary>
        /// <param name="pstrRawSettingValue">
        /// To preserve the linkage with the settings, the caller must pass in the
        /// value read from the application settings.
        /// </param>
        /// <returns>
        /// The returned string is anticipated to be an absolute (fully qualified)
        /// file name.
        /// </returns>
        internal static string AbsolutePathStringFromSettings ( string pstrRawSettingValue )
        {
            const string DATA_DIRECTORY_NAME_TOKEN = @"$$DATADIRNAME$$\";
            const string REPORTS_DIRECTORY_NAME_TOKEN = @"$$REPORTSDIRNAME$$";

            return pstrRawSettingValue.Replace (
                DATA_DIRECTORY_NAME_TOKEN ,
                s_strAbsoluteDataDirectoryName ).Replace (
                REPORTS_DIRECTORY_NAME_TOKEN ,
                s_strAbsoluteReportsDirectoryName );
        }	// AbsolutePathStringFromSettings method


        /// <summary>
        /// Since StateManager.GetStdHandleFQFN returns a well formed filename
        /// string suitable for use anywhere that I valid filename is required,
        /// the filename needs a leading space to make it look right in the test
        /// program's message. Otherwise, the returned string is acceptable as
        /// is.
        /// </summary>
        /// <param name="penmStdHandelID">
        /// The StandardHandleInfo.StandardConsoleHandle enumeration simplifies
        /// gathering information about any of the three streams, each
        /// represented by a property on the static Console object, by mapping
        /// them to an enumeration.
        /// </param>
        /// <returns>
        /// If the standard stream that corresponds to argument penmStdHandelID
        /// is redirected, the return value is a single space character followed
        /// by the name of the file to which it is redirected. Otherwise, the
        /// string returned by StateManager.GetStdHandleFQFN is returned as is.
        /// </returns>
        /// <remarks>
        /// Implementing this transformation as a method saves the second trip
        /// through StateManager.GetStdHandleFQFN that would be required if it
        /// were implemented inline as a ternary expression.
        /// 
        /// ConfigurationManagement.Properties.Resources.MSG_CONSOLE_HAS_STD_HANDLE
        /// is read from the managed resources of the DLL, which must be able to return
        /// it through StateManager.GetStdHandleFQFN.
        /// </remarks>
        private static string BeautifyStandardHandleFQFN ( StandardHandleInfo.StandardConsoleHandle penmStdHandelID )
        {
            string strTempStandardHandleFQFN = s_smTheApp.GetStdHandleFQFN ( penmStdHandelID );

            //	----------------------------------------------------------------
            //	Since several imported namespaces have a Properties.Resources
            //	namespace in their public scopes, one of which is this one, it
            //	must be disambiguated, though WizardWrx.DLLConfigurationManager
            //	is imported.
            //	----------------------------------------------------------------

            if ( strTempStandardHandleFQFN == WizardWrx.DLLConfigurationManager.Properties.Resources.MSG_CONSOLE_HAS_STD_HANDLE )
                return strTempStandardHandleFQFN;
            else
                return string.Concat ( SpecialCharacters.SPACE_CHAR , strTempStandardHandleFQFN );
        }	// BeautifyStandardHandleFQFN method


        private static void ChangeDefaultErrorMessageColors ( )
        {
            ErrorMessagesInColor.FatalExceptionTextColor = ConsoleColor.Red;
            ErrorMessagesInColor.FatalExceptionBackgroundColor = ConsoleColor.White;

            ErrorMessagesInColor.RecoverableExceptionTextColor = ConsoleColor.Yellow;
            ErrorMessagesInColor.RecoverableExceptionBackgroundColor = ConsoleColor.DarkMagenta;
        }   // ChangeDefaultErrorMessageColors method


        private static string ConsoleHandleStateMessage ( StandardHandleInfo.StandardHandleState penmHsHandleState )
        {
            switch ( penmHsHandleState )
            {
                case StandardHandleInfo.StandardHandleState.Attached:
                    return Properties.Resources.MSG_HANDLE_IS_ATTACHED;
                case StandardHandleInfo.StandardHandleState.Redirected:
                    return Properties.Resources.MSG_HANDLE_IS_REDIRECTED;
                default:
                    return Properties.Resources.MSG_HANDLE_IS_UNDEFINED;
            }	// switch ( penmHsHandleState )
        }	// ConsoleHandleStateMessage method


        private static void EvaluateConsoleHandleStates ( )
        {   // The function nesting is encoded in the stepwise indentations.
#if USE_STRING_EXTENSION_METHODS
            Console.WriteLine (
                string.Format (
                    Properties.Resources.MSG_STANDARD_HANDLE_STATE ,                                                    // Format control string
                    Properties.Resources.MSG_STDIN ,                                                                    // Format Item 0: Textual description of stream
                    ConsoleHandleStateMessage (
                        s_smTheApp.StandardHandleState (
                            StandardHandleInfo.StandardConsoleHandle.StandardInput ) ) ,                                // Format Item 1: Standard handle state
                    BeautifyStandardHandleFQFN (
                        StandardHandleInfo.StandardConsoleHandle.StandardInput ) ).AppendFullStopIfMissing ( ) );       // Format Item 2: File name to which redirected, if applicable.
            Console.WriteLine (
                string.Format (
                    Properties.Resources.MSG_STANDARD_HANDLE_STATE ,                                                    // Format control string
                    Properties.Resources.MSG_STDOUT ,                                                                   // Format Item 0: Textual description of stream
                    ConsoleHandleStateMessage (
                        s_smTheApp.StandardHandleState (
                            StandardHandleInfo.StandardConsoleHandle.StandardOutput ) ) ,                               // Format Item 1: Standard handle state
                    BeautifyStandardHandleFQFN (
                        StandardHandleInfo.StandardConsoleHandle.StandardOutput ) ).AppendFullStopIfMissing ( ) );      // Format Item 2: File name to which redirected, if applicable.
            Console.WriteLine (
                string.Format (
                    Properties.Resources.MSG_STANDARD_HANDLE_STATE ,                                                    // Format control string
                    Properties.Resources.MSG_STDERR ,                                                                   // Format Item 0: Textual description of stream
                    ConsoleHandleStateMessage (
                        s_smTheApp.StandardHandleState (
                            StandardHandleInfo.StandardConsoleHandle.StandardError ) ) ,                                // Format Item 1: Standard handle state
                    BeautifyStandardHandleFQFN (
                        StandardHandleInfo.StandardConsoleHandle.StandardError ) ).AppendFullStopIfMissing ( ) );		// Format Item 2: File name to which redirected, if applicable.
#else
                Console.WriteLine ( StringTricks.AppendFullStopIfMissing (
                    string.Format (
                        Properties.Resources.MSG_STANDARD_HANDLE_STATE ,								// Format control string
                        Properties.Resources.MSG_STDIN ,												// Format Item 0: Textual description of stream
                        ConsoleHandleStateMessage (
                            s_smTheApp.StandardHandleState (
                                StandardHandleInfo.StandardConsoleHandle.StandardInput ) ) ,			// Format Item 1: Standard handle state
                        BeautifyStandardHandleFQFN (
                            StandardHandleInfo.StandardConsoleHandle.StandardInput ) ) ) );				// Format Item 2: File name to which redirected, if applicable.
                Console.WriteLine ( StringTricks.AppendFullStopIfMissing (
                    string.Format (
                        Properties.Resources.MSG_STANDARD_HANDLE_STATE ,								// Format control string
                        Properties.Resources.MSG_STDOUT ,												// Format Item 0: Textual description of stream
                        ConsoleHandleStateMessage (
                            s_smTheApp.StandardHandleState (
                                StandardHandleInfo.StandardConsoleHandle.StandardOutput ) ) ,			// Format Item 1: Standard handle state
                        BeautifyStandardHandleFQFN (
                            StandardHandleInfo.StandardConsoleHandle.StandardOutput ) ) ) );			// Format Item 2: File name to which redirected, if applicable.
                Console.WriteLine ( StringTricks.AppendFullStopIfMissing (
                    string.Format (
                        Properties.Resources.MSG_STANDARD_HANDLE_STATE ,								// Format control string
                        Properties.Resources.MSG_STDERR ,												// Format Item 0: Textual description of stream
                        ConsoleHandleStateMessage (
                            s_smTheApp.StandardHandleState (
                                StandardHandleInfo.StandardConsoleHandle.StandardError ) ) ,			// Format Item 1: Standard handle state
                        BeautifyStandardHandleFQFN (
                            StandardHandleInfo.StandardConsoleHandle.StandardError ) ) ) );				// Format Item 2: File name to which redirected, if applicable.
#endif  // #if USE_STRING_EXTENSION_METHODS
        }	// EvaluateConsoleHandleStates method


        private static void ExploreProcessModulesCollection ( )
        {
            Console.WriteLine ( @"{0}ExploreProcessModulesCollection Begin:{0}" , Environment.NewLine );
            System.Diagnostics.Process cpInfo = System.Diagnostics.Process.GetCurrentProcess ( );
            System.Diagnostics.ProcessModuleCollection apmLoadedModules = cpInfo.Modules;

            Console.WriteLine ( @"{0}ExploreProcessModulesCollection Done{0}" , Environment.NewLine );
        }	// ExploreProcessModulesCollection method


        private static int GetIterationCount ( string [ ] pastrArgs )
        {
            const int DEFAULT = 10;

            if ( pastrArgs.Length > CmdLneArgsBasic.FIRST_POSITIONAL_ARG )
            {
                int rintArgValue = MagicNumbers.ZERO;

                if ( int.TryParse ( pastrArgs [ CmdLneArgsBasic.FIRST_POSITIONAL_ARG ] , out rintArgValue ) )
                {	// A valid iteration count was specified. use it.
                    return rintArgValue;
                }	// TRUE (anticipated outcome) block, if ( int.TryParse ( pastrArgs [ CmdLneArgsBasic.FIRST_POSITIONAL_ARG ] , out rintArgValue ) )
                else
                {	// The specified iteration count is invalid. Use the default.
                    return DEFAULT;
                }	// FALSE (unanticipated outcome) block, if ( int.TryParse ( pastrArgs [ CmdLneArgsBasic.FIRST_POSITIONAL_ARG ] , out rintArgValue ) )
            }	// TRUE (anticipated outcome) block, if ( pastrArgs.Length > CmdLneArgsBasic.NONE )
            else
            {	// The iteration count is unspecified. Use the default.
                return DEFAULT;
            }	// FALSE (unanticipated outcome) block, if ( pastrArgs.Length > CmdLneArgsBasic.NONE )
        }	// GetIterationCount method


        internal static void PauseForPictures (
            bool pfAppendLineFeed ,
            string pstrMessageForStandardError )
        {
            bool fPauseing = true;

            if ( ( s_smTheApp.StandardHandleState ( StandardHandleInfo.StandardConsoleHandle.StandardOutput ) == StandardHandleInfo.StandardHandleState.Redirected ) && ( !string.IsNullOrEmpty ( pstrMessageForStandardError ) ) )
            {
                Console.Error.WriteLine (
                    @"{0} completed successfully." ,
                    pstrMessageForStandardError );
            }   // if ( ( s_smTheApp.StandardHandleState ( StandardHandleInfo.StandardConsoleHandle.StandardOutput ) == StandardHandleInfo.StandardHandleState.Redirected ) && ( !string.IsNullOrEmpty ( pstrMessageForStandardError ) ) )

            ExceptionLogger.TimeStampedTraceWrite (
                string.Format (
                    @"Entering PauseForPictures method with {0} = {1} and {2} = {3}." ,
                    nameof ( pfAppendLineFeed ) ,                               // Format Item 0: Entering PauseForPictures method with {0}
                    pfAppendLineFeed ,                                          // Format Item 1:  = {1} and
                    nameof ( pstrMessageForStandardError ) ,                    // Format Item 2: and {2}
                    StringTricks.DisplayNullSafely (
                        pstrMessageForStandardError ) ) );                      // Format Item 3: = {3}.

            if ( ( s_smTheApp.StandardHandleState ( StandardHandleInfo.StandardConsoleHandle.StandardOutput ) == StandardHandleInfo.StandardHandleState.Redirected ) )
            {	// Emit a line feed in lieu of the page break.
                ExceptionLogger.TimeStampedTraceWrite ( @"     PauseForPictures: IsStdOutRedirected returned TRUE." );
                Console.WriteLine ( );
                fPauseing = false;
            }	// TRUE block, if ( MessageInColor.IsStdOutRedirected ( ) )
            else
            {	// Display a prompt, wait for input, clear the screen (and the I/O buffer), and emit a line feed if requested.
                ExceptionLogger.TimeStampedTraceWrite ( @"     PauseForPictures: IsStdOutRedirected returned FALSE." );

                if ( fPauseing )
                {	// Skip the pauses if standard output is redirected.
                    Console.Error.Write ( Properties.Resources.MSG_PHOTO_OP );
                    Console.ReadLine ( );
                }   // if ( fPauseing )

                MessageInColor.SafeConsoleClear ( );	// SafeConsoleClear is a non-throwing wrapper around Console.Clear ( );

                if ( pfAppendLineFeed )
                {	// Caller requested a lone feed.
                    Console.WriteLine ( );
                }	// if ( pfAppendLineFeed )
            }	// FALSE block, if ( MessageInColor.IsStdOutRedirected ( ) )
        }   // PauseForPictures method


        private static string SerializeByteArray ( byte [ ] pbytArbitraryBytes )
        {
            int intByteCount = pbytArbitraryBytes.Length;
            System.Text.StringBuilder rsbSerializedBytes = new System.Text.StringBuilder ( intByteCount * MagicNumbers.PLUS_TWO );

            for ( int intCurrentByte = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCurrentByte < intByteCount ; intCurrentByte++ )
                rsbSerializedBytes.Append ( pbytArbitraryBytes [ intCurrentByte ].ToString ( NumericFormats.HEXADECIMAL_2 ) );

            return rsbSerializedBytes.ToString ( );
        }	// EnumExcpetionGUIDs method


        private static void ShowCurrentDefaultErrorMessageColors ( string pstrHeadingMessage )
        {
            Console.WriteLine ( pstrHeadingMessage , Environment.NewLine );
            ShowDefaulErrorMessageColors (
                ErrorMessagesInColor.FatalExceptionTextColor ,
                ErrorMessagesInColor.FatalExceptionBackgroundColor ,
                ErrorMessagesInColor.ErrorSeverity.Fatal );
            ShowDefaulErrorMessageColors (
                ErrorMessagesInColor.RecoverableExceptionTextColor ,
                ErrorMessagesInColor.RecoverableExceptionBackgroundColor ,
                ErrorMessagesInColor.ErrorSeverity.Recoverable );
        }   // ShowCurrentDefaultErrorMessageColors method


        private static void ShowDefaulErrorMessageColors (
            ConsoleColor pclrFG ,
            ConsoleColor pclrBG ,
            ErrorMessagesInColor.ErrorSeverity penmSeverity )
        {
            string strMsgType;  // The following switch block initializes this, or throws up.

            switch ( penmSeverity )
            {
                case ErrorMessagesInColor.ErrorSeverity.Fatal:
                    strMsgType = Properties.Resources.EXCEPTION_IS_FATAL;
                    break;
                case ErrorMessagesInColor.ErrorSeverity.Recoverable:
                    strMsgType = Properties.Resources.EXCEPTION_IS_RECOVERABLE;
                    break;
                default:
                    throw new InvalidEnumArgumentException (
                        "penmSeverity" ,
                        ( int ) penmSeverity ,
                        typeof ( ErrorMessagesInColor.ErrorSeverity ) );
            }   // switch ( penmSeverity )

            if ( pclrFG != pclrBG )
            {
                MessageInColor.RGBWriteLine (
                    pclrFG ,
                    pclrBG ,
                    Properties.Resources.DEFAULT_EXCEPTION_COLORS_MESSAGE ,
                    new string [ ]
                    {
                        strMsgType ,
                        Properties.Resources.DEFAULT_EXCEPTION_COLORS_PROPERTIES ,
                        pclrFG.ToString ( ) ,
                        pclrBG.ToString ( ) ,
                        Environment.NewLine
                    } );

                //  ------------------------------------------------------------
                //  An ErrorMessagesInColor object writes to the Standard Error
                //  (STDERR) stream.
                //  ------------------------------------------------------------

                {   // Severely constrain the scope of the new ErrorMessagesInColor. 
                    ErrorMessagesInColor emic = ErrorMessagesInColor.GetDefaultErrorMessageColors ( penmSeverity );
                    MessageInColor.RGBWriteLine (
                        pclrFG ,
                        pclrBG ,
                        Properties.Resources.DEFAULT_EXCEPTION_COLORS_MESSAGE ,
                        new string [ ]
                    {
                        strMsgType ,
                        Properties.Resources.DEFAULT_EXCEPTION_COLORS_METHOD_1 ,
                        emic.MessageForegroundColor.ToString ( ) ,
                        emic.MessageBackgroundColor.ToString ( ) ,
                        Environment.NewLine
                    } );
                }   // ErrorMessagesInColor object emic has done its job, and goes out of scope.

                //  ------------------------------------------------------------
                //  A MessageInColor object writes to the Standard Output
                //  (STDOUT) stream.
                //  ------------------------------------------------------------

                {   // Severely constrain the scope of the new MessageInColor. 
                    MessageInColor mic = ErrorMessagesInColor.GetDefaultMessageColors ( penmSeverity );
                    MessageInColor.RGBWriteLine (
                        pclrFG ,
                        pclrBG ,
                        Properties.Resources.DEFAULT_EXCEPTION_COLORS_MESSAGE ,
                        new string [ ]
                    {
                        strMsgType ,
                        Properties.Resources.DEFAULT_EXCEPTION_COLORS_METHOD_2 ,
                        pclrFG.ToString ( ) ,
                        pclrBG.ToString ( ) ,
                        Environment.NewLine
                    } );
                }   // MessageInColor object mic has done its job, and goes out of scope.
            }   // if ( pclrFG != pclrBG )
            else
            {
                Console.WriteLine (
                    Properties.Resources.DEFAULT_EXCEPTION_COLORS_MESSAGE ,
                    new string [ ]
                    {
                        strMsgType ,
                        Properties.Resources.DEFAULT_EXCEPTION_COLORS_PROPERTIES ,
                        pclrFG.ToString ( ) ,
                        pclrBG.ToString ( ) ,
                        Environment.NewLine
                    } );
            }   // if ( pclrFG != pclrBG )
        }   // ShowDefaulErrorMessageColors method


        private static void ShowRecoveredExceptionProperties (
            RecoveredException pexRecoveredException ,
            int pintOrdinal )
        {
            Console.WriteLine ( @"RecoveredException test {0}: Message    = {1}" , pintOrdinal , pexRecoveredException.Message );
            Console.WriteLine ( @"                           TargetSite = {0}" , pexRecoveredException.TargetSite );
            Console.WriteLine ( @"                           Source     = {0}" , pexRecoveredException.Source );
            Console.WriteLine ( @"                           StackTrace = {0}{1}" , pexRecoveredException.StackTrace , Environment.NewLine );
        }   // ShowRecoveredExceptionProperties method


        private static void UnaryMinusExercises ( )
        {
            Console.WriteLine (
                "{0}UnaryMinusExercises Begin:{0}" ,
                Environment.NewLine );

            int intUseCase = MagicNumbers.ZERO;

            foreach ( int intTestValue in s_aintUnaryMinusUseCases )
            {
                Console.WriteLine (
                    "    Case {0}: Input = {1}, Output = {2}" ,					// Format Control String
                    ++intUseCase ,												// Format Item 0 = Case {0}, incremented before formatting
                    intTestValue ,												// Format Item 1 = Input = {1}, taken from list by enumerator
                    -intTestValue );											// Format Item 2 = Output = {2}, unary minus applied to value shown in format item 1
            }	// foreach ( int intUseCase in s_aintUnaryMinusUseCases )

            Console.WriteLine (
                "{0}UnaryMinusExercises Done!{0}" ,
                Environment.NewLine );
        }   // UnaryMinusExercises method
        #endregion  // Subroutines, Some Scoped to the Application
    }   // class Program
}   // partial namespace DLLServices2TestStand