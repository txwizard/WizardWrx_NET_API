/*
    ============================================================================

    Namespace:          DLLServices2TestStand

    Class Name:         CmdLneArgsTests

    File Name:          CmdLneArgsTests.cs

    Synopsis:           This sealed class exercises the new classes by listing
                        the constants defined by each, and exercising each of
                        their service methods.

    Remarks:            Each class has a corresponding static method, which
                        returns an integer status code of zero unless there is a
                        reason to shut down the program.

    Author:             David A. Gray

	License:            Copyright (C) 2011-2017, David A. Gray. 
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
    2015/06/20 5.5     DAG    Move the CmdLneArgsBasic exercises from the test
                              stand for WizardWrx.ApplicationServices2 into this
                              assembly, and put them under my 3-clause BSD
                              license.

    2016/03/29 6.0    DAG	  Resolve the ambiguous references in the XML help.

	2017/02/22 7.0     DAG    Adjust for the breakup of WizardWrx.DllServices2.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;

using WizardWrx;
using WizardWrx.Core;
using WizardWrx.DLLConfigurationManager;


namespace DLLServices2TestStand
{
	internal sealed class CmdLneArgsTests
	{
		const string LIST_IS_EMPTY = @"empty";
		const string LIST_IS_NULL = @"null";

		const string NAMED_ARG_UC_1 = @"NamedArg1";
        const string NAMED_ARG_UC_2 = @"NamedArg2";
        const string NAMED_ARG_UC_3 = @"NamedArg3";

        const string NAMED_ARG_1_DEFAULT = @"NamedArg1DefaultValue";
        const string NAMED_ARG_2_DEFAULT = @"NamedArg2DefaultValue";
        const string NAMED_ARG_3_DEFAULT = @"NamedArg3DefaultValue";

        const char SWITCH_UC_Q = 'Q';
        const char SWITCH_UC_R = 'R';
        const char SWITCH_UC_S = 'S';
        const char SWITCH_UC_T = 'T';

        const string SWITCH_1_DEFAULT = @"Switch1Default";
        const string SWITCH_2_DEFAULT = @"Switch2Default";
        const string SWITCH_3_DEFAULT = @"Switch3Default";

		static readonly char [ ] s_achrSwitchesWithDupicates =
        {
            SWITCH_UC_Q,
            SWITCH_UC_R ,
            SWITCH_UC_S ,
            SWITCH_UC_Q
        };  // static readonly char [ ] s_achrSwitchesWithDupicates =

        static readonly string [ ] s_astrValidArgumentNames =
        {
            NAMED_ARG_UC_1 ,
            NAMED_ARG_UC_2 ,
            NAMED_ARG_UC_3
        };  // static readonly string [ ] astrValidArgumentNames

        static readonly string [ ] s_astrNamedArgDefaults =
        {
            NAMED_ARG_1_DEFAULT ,
            NAMED_ARG_2_DEFAULT ,
            NAMED_ARG_3_DEFAULT
        };  // static readonly string [ ] s_astrNamedArgDefaults

        static readonly string [ ] s_astrSwitchDefaults =
        {
            CmdLneArgsBasic.SWITCH_IS_ON ,
            SWITCH_1_DEFAULT ,
            SWITCH_2_DEFAULT ,
            SWITCH_3_DEFAULT
        };  // static readonly string [ ] s_astrSwitchDefaults

		static readonly char [ ] s_achrSwitchSetAllUC =
        {
            SWITCH_UC_Q ,
            SWITCH_UC_R ,
            SWITCH_UC_S ,
            SWITCH_UC_T
        };  // static readonly char [ ] s_achrSwitchSetAllUC

		static readonly bool [ ] s_afBlankAsDefault =
        {
            CmdLneArgsBasic.BLANK_AS_DEFAULT_FORBIDDEN ,
            CmdLneArgsBasic.BLANK_AS_DEFAULT_ALLOWED
        };  // static readonly bool [ ] _afBlankAsDefault

		internal static void Go ( )
		{
			int intTestNumber = 0;

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports ONLY positional
			//  arguments. This is the simplest kind, and the least useful.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around CmdLneArgsBasic SimpleParser.
				CmdLneArgsBasic SimpleParser = new CmdLneArgsBasic ( );
				DisplayParsedArgs (
					"Supports positional arguments only, and does case sensitive parsing" ,
					SimpleParser ,
					ref intTestNumber );
			}   // CmdLneArgsBasic object SimpleParser goes out of scope.

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports positional
			//  arguments and switches. This is probably the most useful
			//  implementation.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around CmdLneArgsBasic SwitchParser.
				CmdLneArgsBasic SwitchParser = new CmdLneArgsBasic ( s_achrSwitchSetAllUC );
				DisplayParsedArgs (
					"Supports positional arguments and switches, and does case sensitive parsing" ,
					SwitchParser ,
					ref intTestNumber );
			}   // CmdLneArgsBasic SwitchParser goes out of scope.

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports positional
			//  arguments and switches, including an invalid switch.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around InvalidSwitchParser SwitchParser.
				try
				{
					CmdLneArgsBasic InvalidSwitchParser = new CmdLneArgsBasic ( s_achrSwitchesWithDupicates );

					DisplayParsedArgs (
						"Supports positional arguments and switches, and does case sensitive parsing" ,
						InvalidSwitchParser ,
						ref intTestNumber );
				}
				catch ( Exception exAll )
				{
					System.Diagnostics.Trace.WriteLine (
						string.Format (
							"{0}: {1}" ,
							SysDateFormatters.FormatDateTimeForShow ( DateTime.Now ) ,
							ExceptionLogger.GetTheSingleInstance().ReportException ( exAll ) ) );	// Log the error with the event viewer and the trace listener(s), if any.
				}
			}   // CmdLneArgsBasic InvalidSwitchParser goes out of scope.

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports positional
			//  arguments and switches, including an invalid switch.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around IntegerSwitchParser SwitchParser.
				try
				{
					CmdLneArgsBasic IntegerSwitchParser = new CmdLneArgsBasic ( s_achrSwitchSetAllUC );

					DisplayParsedArgs (
						"Supports positional arguments and switches, and does case sensitive parsing" ,
						IntegerSwitchParser ,
						ref intTestNumber );
					Console.WriteLine (
						"    Querying switch Q with a default value of -1 returns {0}" ,
						IntegerSwitchParser.GetSwitchByNameAsInt (
							'Q' ,
							-1 ) );
					Console.WriteLine (
						"    Querying switch R with a default value of -1 returns {0}" ,
						IntegerSwitchParser.GetSwitchByNameAsInt (
							'S' ,
							-1 ) );
				}
				catch ( Exception exAll )
				{
					Console.WriteLine (
						"Exception caught while constructing InvalidSwitchParser{2}    Message = {0}{2}    Method = {1}{2}" ,
						exAll.Message ,
						exAll.TargetSite.Name ,
						Environment.NewLine );
				}
			}   // CmdLneArgsBasic IntegerSwitchParser goes out of scope.

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports positional
			//  arguments and switches. This is probably the most useful
			//  implementation for die-hard Windows programmers, because its
			//  parsing rules are case inssensitive.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around CmdLneArgsBasic CaseInsensitiveSwitchParser.
				CmdLneArgsBasic CaseInsensitiveSwitchParser = new CmdLneArgsBasic (
					s_achrSwitchSetAllUC ,
					CmdLneArgsBasic.ArgMatching.CaseInsensitive );
				DisplayParsedArgs (
					"Supports positional arguments and switches, and does case INsensitive parsing" ,
					CaseInsensitiveSwitchParser ,
					ref intTestNumber );
			}   // CmdLneArgsBasic SwitchParser goes out of scope.

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports all three kinds
			//  of arguments. This is the most versatile parser, period.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around CmdLneArgsBasic FullyLoadedParser.
				CmdLneArgsBasic FullyLoadedParser1 = new CmdLneArgsBasic (
					s_achrSwitchSetAllUC ,
					s_astrValidArgumentNames );
				DisplayParsedArgs (
					"Supports named and positional arguments and switches, and does case sensitive parsing" ,
					FullyLoadedParser1 ,
					ref intTestNumber );

				Console.WriteLine (
					"     GetArgByMultipleAliases, NAMED_ARG_UC_2 first = {0}" ,
					FullyLoadedParser1.GetArgByMultipleAliases (
						new string [ ]
                        {
                            NAMED_ARG_UC_2 ,
                            NAMED_ARG_UC_3
                        } ) );

				//  ---------------------------------------------------------
				//  Swap preferences, and repeat.
				//  ---------------------------------------------------------

				Console.WriteLine (
					"     GetArgByMultipleAliases, NAMED_ARG_UC_3 first = {0}" ,
					FullyLoadedParser1.GetArgByMultipleAliases (
						new string [ ]
                        {
                            NAMED_ARG_UC_3 ,
                            NAMED_ARG_UC_2
                        } ) );
			}   // CmdLneArgsBasic FullyLoadedParser goes out of scope.

			//  ------------------------------------------------------------
			//  Test a CmdLneArgsBasic object that supports all three kinds
			//  of arguments, and implements case insensitive parsing. This
			//  is the most versatile parser for die-hard Window
			//  programmers.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around CmdLneArgsBasic FullyLoadedWinCmdParser.
				CmdLneArgsBasic FullyLoadedWinCmdParser = new CmdLneArgsBasic (
					s_achrSwitchSetAllUC ,
					s_astrValidArgumentNames ,
					CmdLneArgsBasic.ArgMatching.CaseInsensitive );
				DisplayParsedArgs (
					"Supports named and positional arguments and switches, and does case INsensitive parsing" ,
					FullyLoadedWinCmdParser ,
					ref intTestNumber );
			}   // CmdLneArgsBasic FullyLoadedWinCmdParser goes out of scope.

			//  ------------------------------------------------------------
			//  Finally, test a CmdLineArgs object that supports all three
			//  kinds of arguments, and takes its lists of supported named
			//  arguments and switches from dictionaries of names and
			//  defaults. By making this object do  case sensitive parsing
			//  we get the default for the third named argument, as was the
			//  case in the next to last test above.
			//  ------------------------------------------------------------

			{   // Set scope boundaries around CmdLneArgsBasic FullyLoadedParser2.
				Dictionary<string , string> dctNamedArgsWithDefaults = CreateNamedArgListWithDefaults ( );
				Dictionary<char , string> dctSwitchesWithDefaults = CreateSwitchListWithDefaults ( );

				CmdLneArgsBasic FullyLoadedParser2 = new CmdLneArgsBasic (
					dctSwitchesWithDefaults ,
					dctNamedArgsWithDefaults );
				DisplayParsedArgs (
					"Supports named and positional arguments and switches WITH DEFAULTS, and does case sensitive parsing" ,
					FullyLoadedParser2 ,
					ref intTestNumber );
			}   // CmdLneArgsBasic FullyLoadedParser2 goes out of scope.
		}	// internal static void Go

		private static Dictionary<string , string> CreateNamedArgListWithDefaults ( )
		{
			const string ERRMSG_OUT_OF_BALANCE = @"The arrays of argument names and their default values are unbalanced.{2}     Size of names array, astrValidArgumentNames = {0}{2}     Size of defaults array, astrNamedArgDefaults = {1}";

			Dictionary<string , string> rdctOfNamedArgsWithDefaults = new Dictionary<string , string> ( );

			if ( s_astrValidArgumentNames.Length != s_astrNamedArgDefaults.Length )
			{   // This is a serious coding error.
				string strErrorMsg = string.Format (
					ERRMSG_OUT_OF_BALANCE ,
					s_astrValidArgumentNames.Length ,
					s_astrNamedArgDefaults.Length ,
					Environment.NewLine );
				throw new Exception ( strErrorMsg );
			}   // if ( astrValidArgumentNames.Length != astrNamedArgDefaults.Length )

			int intArgCount = s_astrValidArgumentNames.Length;

			for ( int intCurrArgIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
				      intCurrArgIndex < intArgCount ;
				      intCurrArgIndex++ )
			{
				rdctOfNamedArgsWithDefaults.Add (
					s_astrValidArgumentNames [ intCurrArgIndex ] ,
					s_astrNamedArgDefaults [ intCurrArgIndex ] );
			}   // for ( int intCurrArgIndex = MagicNumbers.ARRAY_FIRST_ELEMENT ; ...

			return rdctOfNamedArgsWithDefaults;
		}   // private static Dictionary<string , string> CreateNamedArgListWithDefaults method


		private static Dictionary<char , string> CreateSwitchListWithDefaults ( )
		{
			const string ERRMSG_OUT_OF_BALANCE = @"The arrays of switch names and their default values are unbalanced.{2}     Size of names array, achrSwitchSetAllUC = {0}{2}     Size of defaults array, astrSwitchDefaults = {1}";

			Dictionary<char , string> rdctOfSwitchesWithDefaults = new Dictionary<char , string> ( );

			if ( s_achrSwitchSetAllUC.Length != s_astrSwitchDefaults.Length )
			{   // This is a serious coding error.
				string strErrorMsg = string.Format (
					ERRMSG_OUT_OF_BALANCE ,
					s_achrSwitchSetAllUC.Length ,
					s_astrSwitchDefaults.Length ,
					Environment.NewLine );
				throw new Exception ( strErrorMsg );
			}   // if ( astrValidArgumentNames.Length != astrNamedArgDefaults.Length )

			int intSwitchCount = s_achrSwitchSetAllUC.Length;

			for ( int intCurrSwitchIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
					  intCurrSwitchIndex < intSwitchCount ;
					  intCurrSwitchIndex++ )
			{
				rdctOfSwitchesWithDefaults.Add (
					s_achrSwitchSetAllUC [ intCurrSwitchIndex ] ,
					s_astrSwitchDefaults [ intCurrSwitchIndex ] );
			}   // for ( int intCurrSwitchIndex = MagicNumbers.ARRAY_FIRST_ELEMENT ; ...

			return rdctOfSwitchesWithDefaults;
		}   // private static Dictionary<char , string> CreateSwitchListWithDefaults method


		private static void DisplayParsedArgs (
			string pstrTestLabel ,
			CmdLneArgsBasic pCmdLineArgs ,
			ref int pintTestNumber )
		{
			const string EXERCISE_TPL_BEGIN = @"Test {0}: Exercising A CmdLneArgsBasic object with the following capabilities: {1}{2}";
			const string EXERCISE_TPL_END = @"{1}Finished exercising CmdLneArgsBasic object {0}{1}";

			Console.WriteLine (
				EXERCISE_TPL_BEGIN ,        // Format string (template)
				++pintTestNumber ,          // Substituted for token 0
				pstrTestLabel ,             // Substituted for token 1
				Environment.NewLine );      // Substituted for token 2

			DisplayReadOnlyProperties ( pCmdLineArgs );
			EmumerateDefinedSwitches (
				pCmdLineArgs ,
				s_achrSwitchSetAllUC );
			EnumerateDefinedNamedArguments (
				pCmdLineArgs ,
				s_astrValidArgumentNames );
			EnumeratePositionalArgs ( pCmdLineArgs );

			Console.WriteLine (             // Format string (template)
				EXERCISE_TPL_END ,          // Substituted for token 0
				pCmdLineArgs ,              // Substituted for token 1
				Environment.NewLine );      // Substituted for token 2
		}   // private static void DisplayParsedArgs method


		private static void DisplayPublicConstants ( )
		{
			const string MSG_BEGIN = "\r\nBegin display of public constants exposed by a CmdLneArgsBasic object:\r\n";
			const string MSG_END = "\r\nEnd of public constants\r\n";

			Console.WriteLine ( MSG_BEGIN );

			Console.WriteLine ( @"     ARG_LIST_HAS_ARGS           = {0}" , CmdLneArgsBasic.ARG_LIST_HAS_ARGS );
			Console.WriteLine ( @"     ARG_LIST_IS_EMPTY           = {0}" , CmdLneArgsBasic.ARG_LIST_IS_EMPTY );
			Console.WriteLine ( @"     FIRST_POSITIONAL_ARG        = {0}" , CmdLneArgsBasic.FIRST_POSITIONAL_ARG );
			Console.WriteLine ( @"     NAME_VALUE_DELIMITER        = {0}" , CmdLneArgsBasic.NAME_VALUE_DELIMITER );
			Console.WriteLine ( @"     NONE                        = {0}" , CmdLneArgsBasic.NONE );
			Console.WriteLine ( @"     POSITIONAL_ARGS_COUNT_LIMIT = {0}" , CmdLneArgsBasic.POSITIONAL_ARGS_COUNT_LIMIT );
			Console.WriteLine ( @"     SWITCH_IS_OFF               = {0}" , CmdLneArgsBasic.SWITCH_IS_OFF );
			Console.WriteLine ( @"     SWITCH_IS_ON                = {0}" , CmdLneArgsBasic.SWITCH_IS_ON );
			Console.WriteLine ( @"     VALUE_NOT_SET               = {0}" , CmdLneArgsBasic.VALUE_NOT_SET );

			Console.WriteLine ( MSG_END );
		}   // private static void DisplayPublicConstants


		private static void DisplayReadOnlyProperties ( CmdLneArgsBasic pcmdLineArgs )
		{
			const string ARG_IS_NULL_MSG = @"     Method DisplayReadOnlyProperties was called with a null reference.";
			const string PROP_MSG_BEGIN = @"     Begin Read-Only Properties of object {0}{1}";
			const string PROP_MSG_END = @"{0}     End of Read-Only Properties{0}";

			const string PROP_NAME_COUNT = @"Count";

			const string PROP_NAME_ARG_LIST_IS_EMPTY = @"ArgListIsEmpty";
			const string PROP_NAME_ARG_MATCHING_METHOD = @"ArgumentMatching";
			const string PROP_NAME_ARGUMENT_TYPE_ARRAY = @"ArgumentTypeArray";

			const string PROP_NAME_DEFINED_NAMED_ARGS = @"DefinedNamedArgs";
			const string PROP_NAME_DEFINED_SWITCHES_ARGS = @"DefinedSwitches";

			const string PROP_NAME_INVALID_NAMED_ARGS = @"InvalidNamedArgsInCmd";
			const string PROP_NAME_INVALID_SWITCHES_ARGS = @"InvalidSwitchesInCmd";

			const string PROP_NAME_POSITIONAL_ARGS_IN_CMD = @"PositionalArgsInCmdLine";

			const string PROP_NAME_VALID_NAMED_ARGS = @"ValidNamedArgsInCmd";
			const string PROP_NAME_VALID_SWITCHES_ARGS = @"ValidSwitchesInCmd";

			const string PROP_VALUE_TPL = @"          {0} = {1}";
			const string PROP_ARRAY_SIZE_TPL = @"{2}          {0} is an array of {1} elements, as follows.{2}";
			const string PROP_ARRAY_ELEMENT_TPL = @"               Element {0} = {1}";

			if ( pcmdLineArgs == null )
			{   // Save the NullReferenceException for when you really need it.
				Console.Write ( ARG_IS_NULL_MSG );
			}   // TRUE (abnormal) block, if ( pcmdLineArgs == null )
			else
			{   // Display the properties in a formatted listing.
				Console.WriteLine (
					PROP_MSG_BEGIN ,
					pcmdLineArgs ,
					Environment.NewLine );

				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_COUNT ,
					pcmdLineArgs.Count );

				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_ARG_LIST_IS_EMPTY ,
					pcmdLineArgs.ArgListIsEmpty );
				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_ARG_MATCHING_METHOD ,
					pcmdLineArgs.ArgumentMatching );

				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_DEFINED_NAMED_ARGS ,
					pcmdLineArgs.DefinedNamedArgs );
				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_DEFINED_SWITCHES_ARGS ,
					pcmdLineArgs.DefinedSwitches );

				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_INVALID_NAMED_ARGS ,
					pcmdLineArgs.InvalidNamedArgsInCmd );
				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_INVALID_SWITCHES_ARGS ,
					pcmdLineArgs.InvalidSwitchesInCmd );

				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_POSITIONAL_ARGS_IN_CMD ,
					pcmdLineArgs.PositionalArgsInCmdLine );

				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_VALID_NAMED_ARGS ,
					pcmdLineArgs.ValidNamedArgsInCmdLine );
				Console.WriteLine (
					PROP_VALUE_TPL ,
					PROP_NAME_VALID_SWITCHES_ARGS ,
					pcmdLineArgs.ValidSwitchesInCmdLine );

				int intArgPosition = ArrayInfo.ARRAY_INVALID_INDEX;

				try
				{
					Console.WriteLine (
						PROP_ARRAY_SIZE_TPL ,                       // Template string
						PROP_NAME_ARGUMENT_TYPE_ARRAY ,             // Token 0
						pcmdLineArgs.ArgumentTypeArray.Length ,     // Token 1
						Environment.NewLine );                      // Token 2
					intArgPosition = ArrayInfo.ARRAY_FIRST_ELEMENT;
				}
				catch ( Exception errAllKinds )
				{
					Console.WriteLine ( @"          {0}" , errAllKinds.Message );
				}

				if ( pcmdLineArgs.ArgListIsEmpty == CmdLneArgsBasic.ARG_LIST_HAS_ARGS )
				{   // Display a summary, followed by an enumeration.
					if ( intArgPosition == ArrayInfo.ARRAY_INVALID_INDEX )
					{
						Console.WriteLine (
							PROP_ARRAY_SIZE_TPL ,                       // Template string
							PROP_NAME_ARGUMENT_TYPE_ARRAY ,             // Token 0
							pcmdLineArgs.ArgumentTypeArray.Length ,     // Token 1
							Environment.NewLine );                      // Token 2
						intArgPosition = ArrayInfo.ARRAY_FIRST_ELEMENT;
					}   // if ( intArgPosition == ArrayInfo.ARRAY_INVALID_INDEX )

					foreach ( CmdLneArgsBasic.ArgType enmArgType in pcmdLineArgs.ArgumentTypeArray )
					{   // Use the implicit array enumerator.
						Console.WriteLine (
							PROP_ARRAY_ELEMENT_TPL ,                // Template string
							++intArgPosition ,                      // Token 0
							enmArgType );                           // Token 1
					}   // foreach ( CmdLneArgsBasic.ArgType enmArgType in pcmdLineArgs.ArgumentTypeArray )
				}   // TRUE (normal) case, if ( pcmdLineArgs.ArgListIsEmpty == CmdLneArgsBasic.ARG_LIST_HAS_ARGS )
				else
				{
					Console.WriteLine ( @"          The command line is devoid of arguments." );
				}   // FALSE (degenerate) case, if ( pcmdLineArgs.ArgListIsEmpty == CmdLneArgsBasic.ARG_LIST_HAS_ARGS )

				Console.WriteLine (
					PROP_MSG_END ,
					Environment.NewLine );
			}   // FALSE (normal) block, if ( pcmdLineArgs == null )
		}   // private static void DisplayReadOnlyProperties


		private static void EnumeratePositionalArgs ( CmdLneArgsBasic pcmdLineArgs )
		{
			const string ARG_DISP_TPL_COUNT = @"{1}     Count of positional arguments = {0}{1}";
			const string ARG_DISP_TPL_END = @"{0}     End of positional argument list{0}";
			const string ARG_DISP_TPL_POSITIONAL = @"          Positional argument # {0} = {1}";
			const string MSG_NO_POSITIONAL_ARGS = @"     The command line contains no positional arguments.";

			//bool fMoreArgs = true;
			int intArgOrdinal=ArrayInfo.ARRAY_FIRST_ELEMENT;

			//  ----------------------------------------------------------------
			//  This is the old way, before I had these properties to query.
			//  ----------------------------------------------------------------

			//while ( fMoreArgs )
			//{
			//    string strThisArg = pcmdLineArgs.GetArgByPosition ( ++intArgOrdinal );

			//    if ( strThisArg == CmdLneArgsBasic.VALUE_NOT_SET )
			//        fMoreArgs = false;
			//    else
			//        Console.WriteLine (
			//            ARG_DISP_TPL_POSITIONAL ,
			//            intArgOrdinal ,
			//            strThisArg );
			//}   // while ( fMoreArgs )

			//if ( intArgOrdinal == ArrayInfo.NEXT_INDEX )
			//{   // No positional arguments found.
			//    Console.WriteLine ( MSG_NO_POSITIONAL_ARGS );
			//}   // if ( intArgOrdinal == ArrayInfo.NEXT_INDEX )

			//  ----------------------------------------------------------------
			//  This is the new way, which leverages the new properties.
			//  ----------------------------------------------------------------

			int intNPositionalArgsFound = pcmdLineArgs.PositionalArgsInCmdLine;

			if ( intNPositionalArgsFound == ArrayInfo.ARRAY_FIRST_ELEMENT )
			{   // Report the absence of command line arguments.
				Console.WriteLine ( MSG_NO_POSITIONAL_ARGS );
			}   // TRUE block (the degenerate case), if ( intNPositionalArgsFound == ArrayInfo.ARRAY_FIRST_ELEMENT )
			else
			{   // List arguments in order by position.
				Console.WriteLine (
					ARG_DISP_TPL_COUNT ,
					intNPositionalArgsFound ,
					Environment.NewLine );

				for ( intArgOrdinal = ArrayInfo.NEXT_INDEX ;
					  intArgOrdinal <= intNPositionalArgsFound ;
					  intArgOrdinal++ )
				{
					Console.WriteLine (
						ARG_DISP_TPL_POSITIONAL ,
						intArgOrdinal ,
						pcmdLineArgs.GetArgByPosition ( intArgOrdinal ) );
				}   // for ( intArgOrdinal = ArrayInfo.NEXT_INDEX ; ...

				Console.WriteLine (
					ARG_DISP_TPL_END ,
					Environment.NewLine );
			}   // FALSE block (the normal case), if ( intNPositionalArgsFound == ArrayInfo.ARRAY_FIRST_ELEMENT )
		}   // private static void method EnumeratePositionalArgs


		private static void EnumerateDefinedNamedArguments (
			CmdLneArgsBasic pcmdLineArgs ,
			string [ ] pastrValidArgumentNames )
		{
			const string ARG_DISP_TPL_NAMED_ARG = @"     Argument {0} = {1} - First character = {2}";
			const string MSG_NO_NAMED_ARGS_DEFINED = @"     No named arguments are defined. Input {0} is {1}.";

			if ( pcmdLineArgs == null )
			{
				Console.WriteLine (
					MSG_NO_NAMED_ARGS_DEFINED ,
					pcmdLineArgs ,
					LIST_IS_NULL );
			}   // TRUE block, if ( pcmdLineArgs == null )
			else if ( pcmdLineArgs.DefinedNamedArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
			{
				Console.WriteLine (
					MSG_NO_NAMED_ARGS_DEFINED ,
					pcmdLineArgs ,
					LIST_IS_EMPTY );
			}   // TRUE block, else if ( pcmdLineArgs.DefinedNamedArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
			else
			{
				foreach ( string strArgName in pastrValidArgumentNames )
				{
					try
					{
						Console.WriteLine (
							ARG_DISP_TPL_NAMED_ARG ,
							strArgName ,
							pcmdLineArgs.GetArgByName ( strArgName ) ,
							FormatSpecialCharacters ( pcmdLineArgs.GetArgByNameAsChar ( strArgName ) ) );
					}
					catch ( Exception exAny )
					{
						if ( exAny.Message.StartsWith ( WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ) && exAny.TargetSite.Name == "FirstCharFromString" )
						{
							Console.WriteLine (
								ARG_DISP_TPL_NAMED_ARG ,
								strArgName ,
								pcmdLineArgs.GetArgByName ( strArgName ) ,
								FormatSpecialCharacters (
									pcmdLineArgs.GetArgByNameAsChar (
										strArgName ,
										SpecialCharacters.NULL_CHAR ) ) );
						}   // TRUE block, if ( exAny.Message.StartsWith(MagicNumbers.ERRMSG_ARG_IS_NULL_OR_EMPTY) && exAny.TargetSite.Name == "FirstCharFromString" )
						else
						{
							StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );

							sbMsg.AppendFormat (
								Program.ERRMSG_INTERNAL_ERROR_TPL ,
								exAny.Message ,
								Environment.NewLine );

							sbMsg.AppendFormat (
								Program.ERRMSG_INTERNAL_SOURCE_TPL ,
								exAny.Source ,
								Environment.NewLine );
							sbMsg.AppendFormat (
								Program.ERRMSG_INTERNAL_TARGETSITE_TPL ,
								exAny.TargetSite.Name ,
								Environment.NewLine );

							// ---------------------------------------------------------
							// 2012/07/07 - DAG    - Added environment.NewLine to the
							//                       argument list. This same change
							//                       should be applied to the routine in
							//                       TextLog.cs from which this code is
							//                       derived.
							// ---------------------------------------------------------

							sbMsg.AppendFormat (
								Program.ERRMSG_INTERNAL_STACKTRACE_TPL ,
								exAny.StackTrace ,
								Environment.NewLine );

							if ( exAny.InnerException != null )
								sbMsg.AppendFormat (
									Program.ERRMSG_INTERNAL_INNER_MSG_TPL ,
									exAny.InnerException.Message );

							string strMsg = sbMsg.ToString ( );

							Console.WriteLine ( strMsg );
							System.Diagnostics.EventLog.WriteEntry (
								ExceptionLogger.GetTheSingleInstance ( ).AppEventSourceID ,
								strMsg ,
								System.Diagnostics.EventLogEntryType.Warning );
						}   // FALSE block, if ( exAny.Message.StartsWith(MagicNumbers.ERRMSG_ARG_IS_NULL_OR_EMPTY) && exAny.TargetSite.Name == "FirstCharFromString" )
					}   // catch block
				}   // foreach ( string strArgName in pastrValidArgumentNames )
			}   // FALSE block, else if ( pcmdLineArgs.DefinedNamedArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
		}   // private static void EnumerateDefinedNamedArguments


		/// <summary>
		/// Return a printable representation of a nonprintable character.
		/// </summary>
		/// <param name="pchr">
		/// Character to be formatted.
		/// </param>
		/// <returns>
		/// String representation of special character, or string representation
		/// of pchr, if it isn't special.
		/// </returns>
		/// <remarks>
		/// As written, this routine handles only a handful of characters (NULL,
		/// TAB, CR, and LF). A complete version that handles more nonprintable
		/// characters would make a useful addition to a library, unless, of 
		/// course, I can find a suitable routine online.
		/// </remarks>
		private static string FormatSpecialCharacters ( char pchr )
		{
			switch ( pchr )
			{
				case SpecialCharacters.NULL_CHAR:
					return @"[NULL]";
				case SpecialCharacters.TAB_CHAR:
					return @"[TAB]";
				case SpecialCharacters.CARRIAGE_RETURN:
					return @"[CR]";
				case SpecialCharacters.LINEFEED:
					return @"[LF]";
				default:
					return pchr.ToString ( );
			}   // switch ( pchr )
		}   // private static string FormatSpecialCharacters


		private static void EmumerateDefinedSwitches (
			CmdLneArgsBasic pcmdLineArgs ,
			char [ ] pachrSwitchSetAllDefined )
		{
			const string MSG_DISP_BLANK_AS_DEFAULT = @"   BlankAsDefault Property = {0}{1}";
			const string ARG_DISP_TPL_SWITCH   = @"     Switch {0} = {1}";
			const string MSG_NO_SWITCHES_DEFINED = @"     No switches are defined. Input {0} is {1}.";
			const string MSG_SWITCH_OMITTED = @"** OMITTED **";

			if ( pachrSwitchSetAllDefined == null )
			{
				Console.WriteLine (
					MSG_NO_SWITCHES_DEFINED ,
					pcmdLineArgs ,
					LIST_IS_NULL );
			}   // TRUE block, if ( pachrSwitchSetAllDefined == null )
			else
			{
				if ( pachrSwitchSetAllDefined.Length == ArrayInfo.ARRAY_FIRST_ELEMENT )
				{
					Console.WriteLine (
						MSG_NO_SWITCHES_DEFINED ,
						pcmdLineArgs ,
						LIST_IS_EMPTY );
				}   // TRUE block, if ( pachrSwitchSetAllDefined.Length == ArrayInfo.ARRAY_FIRST_ELEMENT )
				else
				{
					foreach ( bool fBlankAsDefault in s_afBlankAsDefault )
					{
						pcmdLineArgs.AllowEmptyStringAsDefault = fBlankAsDefault;
						Console.WriteLine (
							MSG_DISP_BLANK_AS_DEFAULT ,
							pcmdLineArgs.AllowEmptyStringAsDefault ,
							Environment.NewLine );

						foreach ( char chrSwitch in pachrSwitchSetAllDefined )
						{
							Console.WriteLine (
								ARG_DISP_TPL_SWITCH ,
								chrSwitch ,
								pcmdLineArgs.GetSwitchByName ( chrSwitch ) );
							Console.WriteLine (
								ARG_DISP_TPL_SWITCH ,
								chrSwitch ,
								pcmdLineArgs.GetSwitchByName (
									chrSwitch ,
									MSG_SWITCH_OMITTED ) );
							Console.WriteLine (
								ARG_DISP_TPL_SWITCH ,
								chrSwitch ,
								pcmdLineArgs.GetSwitchByName (
									chrSwitch ,
									SpecialStrings.EMPTY_STRING ,
									CmdLneArgsBasic.BLANK_AS_DEFAULT_FORBIDDEN ) );
							Console.WriteLine (
								ARG_DISP_TPL_SWITCH ,
								chrSwitch ,
								pcmdLineArgs.GetSwitchByName (
									chrSwitch ,
									SpecialStrings.EMPTY_STRING ,
									CmdLneArgsBasic.BLANK_AS_DEFAULT_ALLOWED ) );
							Console.WriteLine (
								ARG_DISP_TPL_SWITCH ,
								chrSwitch ,
								pcmdLineArgs.GetBooleanSwitchByName ( chrSwitch ) );
						}   // foreach ( char chrSwitch in pachrSwitchSetAllDefined )
					}   // foreach ( bool fBlankAsDefault in _afBlankAsDefault )
				}   // FALSE block, if ( pachrSwitchSetAllDefined.Length == ArrayInfo.ARRAY_FIRST_ELEMENT )
			}   // FALSE block, if ( pachrSwitchSetAllDefined == null )
		}   // private static void EmumerateDefinedSwitches method


		private static void DisplayRawArgs ( string [ ] pastrArgs )
		{
			const string ARG_COUNT_MSG_TPL = @"     Command line Argument Count = {0}";
			const string RAW_ARGS_DISPLAY_BEGIN = @"{0}Command line arguments, as seen by the Common Language Runtime:{0}";
			const string RAW_ARGS_DISPLAY_END = @"{0}End of report on raw command line arguments.{0}";
			const string RAW_ARGS_EMPTY_MSG = @"{0}     The command line argument list is empty.{0}";
			const string RAW_ARGS_DETAIL_HEADING = @"{0}     Arguments, in order of appearance, are as follows:{0}";
			const string RAW_ARGS_DETAIL_ITEM = @"          Argument # {0} = {1}";

			Console.WriteLine (
				RAW_ARGS_DISPLAY_BEGIN ,
				Environment.NewLine );

			int intNArgs = pastrArgs.Length;

			Console.WriteLine (
				ARG_COUNT_MSG_TPL ,
				intNArgs );

			if ( intNArgs > ArrayInfo.ARRAY_FIRST_ELEMENT )
			{   // Command line contains at least one argument.
				Console.WriteLine (
					RAW_ARGS_DETAIL_HEADING ,
					Environment.NewLine );

				int intArgOrdinal = ArrayInfo.ARRAY_FIRST_ELEMENT;

				foreach ( string strThisArg in pastrArgs )
				{   // Microsoft .NET arrays implement an IEnumerable interface.
					Console.WriteLine (
						RAW_ARGS_DETAIL_ITEM ,
						++intArgOrdinal ,
						strThisArg );
				}   // foreach ( string strThisArg in pastrArgs )
			}   // TRUE (normal case) block, if ( intNArgs > ArrayInfo.ARRAY_FIRST_ELEMENT )
			else
			{   // Cmmand line is minimal.
				Console.WriteLine (
					RAW_ARGS_EMPTY_MSG ,
					Environment.NewLine );
			}   // FALSE (degenerate case) block, if ( intNArgs > ArrayInfo.ARRAY_FIRST_ELEMENT )

			Console.WriteLine (
				RAW_ARGS_DISPLAY_END ,
				Environment.NewLine );
		}   // private static void DisplayRawArgs method
	}	// internal sealed class CmdLneArgsTests
}	// partial namespace DLLServices2TestStand