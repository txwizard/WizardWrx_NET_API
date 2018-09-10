/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:			CmdLneArgsBasic

	File Name:			CmdLneArgsBasic.cs

    Synopsis:			Parse valid commands into a Dictionary, discarding 
						invalid arguments.

    Remarks:			Beginning with version 2.0, I added an option to treat
						the empty string as a valid default value for a switch 
						or named argument. Since this change has the potential
						to change the behavior of existing code, of which there
						is more than I want to review, I added a read/write
						property (which CANNOT be set by a constructor!) to
						govern the behavior of the methods affected by this
						change, which are as follows.

						1)  GetArgDefaultToUse returns values of named
							arguments.

						2)  GetSwitchDefaultToUse returns values of named
							switches.

						The new property, AllowEmptyStringAsDefault, defaults to
						FALSE, which keeps the library backwards compatible.

						I chose to inherit from Dictionary <string ,string> so
						the internal backing store is accessible to callers. In
						retrospect, this decision was unwise, since it violates
						the principle of data hiding.

    Author:				David A. Gray

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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2008/12/28 1.0.0.0 DAG    Initial version, as part of WWWebConfigClassGen, a
                              project begun in 2008, which remains incomplete as
                              of June 2011.

    2011/06/27 1.1     DAG    Move to the WizardWrs.ConsoleHelpers namespace and
                              class library.

    2011/07/04 1.2     DAG    1) Add missing combinations of structure types
                                 (array and dictionary) for passing lists of
                                 supported arguments and switches into the
                                 constructors.

                              2) Add an array of state flags, mapped 1 for 1 to
                                 the arguments in the command line.

    2011/07/12 1.3     DAG    Change the default value set for an unmodified
                              switch from SWITCH_IS_OFF to SWITCH_IS_ON.

                              Affected Method: GetSwitchArgValue

    2011/07/16 1.5     DAG    Change namespace name from ConsoleHelpers to
                              ApplicationHelpers, to reflect its broadened range
                              of applications (pun intended).

    2012/03/03 1.8     DAG    Correct the way that default values of named 
                              arguments are treated.

    2012/03/04 1.8     DAG    Add switch accessors that returns Boolean, and
                              disallows modifiers.

    2012/03/11 2.00    DAG    Correct the way that default values of switches
                              arguments are treated, by applying the same change
                              to switch processing that was implemented for
                              named arguments in version 1.8. See Remarks.

    2012/12/16 2.90    DAG    Add method GetArgByMultipleAliases, which returns 
                              the first of a list of argument synonyms, listed
                              in precedence order, and an instance version of 
                              the same method.

    2013/01/21 2.93    DAG    New methods:
                              1) FirstCharFromString (static, 2 overloads)
                              2) GetArgByNameAsChar (instance, 3 overloads)

    2013/01/22 2.94    DAG    Correct an inverted logic error in the second
                              overload of FirstCharFromString.

    2013/02/15 2.95    DAG    Convert the FirstCharFromString method from an
                              instance method to a static method, to conform to
                              its documented binding.

    2014/05/15 4.1     DAG    1) DefineNewNamedArg Method: Handle the duplicate 
                                 key exception a tad more gracefully.

                              2) Add methods for returning integer values.

    2014/06/23 5.1     DAG    Major namespace reorganization and documentation
                              corrections.

    2015/06/23 5.5     DAG    Move this class from WizardWrx.ApplicationHelpers2
                              to WizardWrx.DLLServices2, add static method
                              ArgNameFromKeyValue, and move the error message
                              constant strings, EMPTY_ALIAS_LIST, to the string
                              resources bound into this DLL.

	2016/03/29 6.0     DAG    Define methods to return named and positional
                              arguments as integers.

	2016/05/11 6.0     DAG    This update is confined to correcting spelling
                              errors in the internal documentation that came to
                              my attention during an investigation of why the
							  class was returning "OFF" for missing switches.

							  Since the update is confined to spelling errors in
							  comments, and relocation of two messages from 
							  hard coded constant strings to managed string
                              resources, the version number stays the same, but
							  the build number increments by one.

	2017/02/28 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

							  Upon further consideration, this class is moved to
                              WizardWrx.Core.dll, and moved to namespace
                              WizardWrx.Core
 
                              This class is essentially unchanged, apart from 
                              its use of a Dictionary as its data store being
                              hidden by replacing inheritance with a private
                              object, so that it implements the Adapter design
                              pattern. It stays put, at the small expense of a 
                              dependency upon the new WizardWrx.Common class,
                              most of which remains part of the main WizardWrx
                              namespace.

	2017/07/15 7.0     DAG    Correct GetArgDefaultToUse for the case where
                              pfAllowEmptyStringAsDefault is FALSE so that it
                              takes pstrSuppliedDefault at face value for that
                              use case.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.

	2017/07/18 7.0     DAG	  Add the breaking change alert remarks to the other
                              routines that call GetArgDefaultToUse, and to the
                              AllowEmptyStringAsDefault property.
    ============================================================================
*/


using System;
using System.Collections.Generic;


namespace WizardWrx.Core
{
    /// <summary>
    /// An instance of this class efficiently processes command line switches,
    /// named arguments, and positional arguments, in such a way that they are
    /// easily accessible. The command line is completely parsed in a single
    /// pass.
    ///
    /// Switches, named arguments, and positional arguments may be freely mixed
    /// in any way; your users aren't confined to specifying all switches and/or
    /// named arguments first, last, or in any other order.
    ///
    /// Since command line parsing happens in the constructor, your code must
    /// define all the rules in its parameters. To that end, there are 26
    /// constructors, which provide access to all of the rich capabilities of
    /// this object. In spite of the multitude of constructors, their
    /// organization, coupled with consistent naming and documentation of their
    /// arguments, is intended to make their features readily accessible.
    ///
    /// While it is fairly trivial to reverse engineer the underlying Dictionary
    /// object, and read the arguments directly, the supported method of reading
    /// them is through its methods: GetArgByName, GetArgByPosition, and
    /// GetSwitchByName.
    /// </summary>
    public class CmdLneArgsBasic
    {
        #region Public Enumerations
        /// <summary>
        /// Argument matching may be either case sensitive (default) or case
        /// insensitive (by constructor override). Constructor argument
        /// penmArgMatching is of this type.
        /// </summary>
        public enum ArgMatching
        {
            /// <summary>
            /// Argument matching is case sensitive.
            /// </summary>
            CaseSensitive,

            /// <summary>
            /// Argument matching is case insensitive.
            /// </summary>
            CaseInsensitive
        }   // ArgMatching


        /// <summary>
        /// Arguments may be of three types:
        ///
        /// 1) Named
        /// 2) Positional
        /// 3) Switch
        ///
        /// Further, the first and third types may be invalid arguments of their
        /// respective types. That is, a switch may be invalid, or the name of a
        /// Named Argument may be invalid.        ///
        /// </summary>
        public enum ArgType
        {
            /// <summary>
            /// The specified argument appears to be a named argument, but its
            /// name is undefined (not on the list of valid names).
            /// </summary>
            InvalidNamed,

            /// <summary>
            /// The specified argument appears to be a switch, but its name is
            /// undefined (not on the list of valid switch characters).
            /// </summary>
            InvalidSwitch,

            /// <summary>
            /// The argument is named, and its value is in the list of valid
            /// names.
            /// </summary>
            Named,

            /// <summary>
            /// The argument is positional. That is, it is neither a named
            /// argument, nor a switch. Its meaning is defined by its order of
            /// appearance in the command line.
            /// </summary>
            Positional,

            /// <summary>
            /// The argument is a switch, and its value is in the list of valid
            /// switch characters.
            /// </summary>
            Switch
        }   // ArgType
		#endregion	// Public Enumerations


		#region Public Constants
		/// <summary>
        /// When the ArgListIsEmpty property has this value, the command line
        /// contains at least one argument. See Remarks.
        /// </summary>
        /// <remarks>
        /// ArgListIsEmpty being false means only that there is an argument in
        /// the command line, which may be invalid. Check the ArgMatching array
        /// or the two invalid argument counters, InvalidNamedArgsInCmd and
        /// InvalidSwitchesInCmd.
        /// </remarks>
        public const bool ARG_LIST_HAS_ARGS = false;


        /// <summary>
        /// When the ArgListIsEmpty property has this value, the command line is
        /// empty, consisting solely of the name of the executing assembly.
        /// </summary>
        public const bool ARG_LIST_IS_EMPTY = true;


        /// <summary>
        /// When the AllowEmptyStringAsDefault property has this value, an empty
        /// string is permitted as a default value. See the Remarks section of 
        /// the on-line documentation of the AllowEmptyStringAsDefault property
        /// for complete details.
        /// </summary>
        public const bool BLANK_AS_DEFAULT_ALLOWED = true;


        /// <summary>
        /// When the AllowEmptyStringAsDefault property has this value, an empty
        /// string as a default value is forbidden. See the Remarks section of 
        /// the on-line documentation of the AllowEmptyStringAsDefault property
        /// for complete details.
        /// </summary>
        public const bool BLANK_AS_DEFAULT_FORBIDDEN = false;


        /// <summary>
        /// Use this constant with the GetArgByPosition method to get the first
        /// positional argument, or as the initializer of a For loop to get all
        /// positional arguments.
        /// </summary>
		public const int FIRST_POSITIONAL_ARG = MagicNumbers.PLUS_ONE;


        /// <summary>
        /// This class uses the equal sign as its delimiter between the name and
        /// value of named arguments.
        /// </summary>
        public const char NAME_VALUE_DELIMITER = SpecialCharacters.EQUALS_SIGN;


        /// <summary>
        /// Counts are initialized to this value (zero). Making it public
        /// simplifies coding well-documented tests.
        /// </summary>
        public const int NONE = MagicNumbers.ZERO;


        /// <summary>
        /// This constant defines the default number of positional arguments
        /// that instances of this class will capture.
        /// </summary>
        public const int POSITIONAL_ARGS_COUNT_LIMIT = 9;


        /// <summary>
        /// This symbolic constant maps to the empty string, and indicates that
        /// a specified named or positional argument was omitted from the
        /// command line.
        /// </summary>
        public const string VALUE_NOT_SET = SpecialStrings.EMPTY_STRING;
		#endregion	// Public Constants


		#region Private Constants
		private const string ARG_KEY_FMT = @"{0}_{1}";
        private const string ARGNAME_VALID_NAMES = @"pastrValiddArgNames";

        private const int COLLECTION_IS_EMPTY = MagicNumbers.ZERO;

        private const int NVP_STOP_AT_FIRST_DELIMITER = MagicNumbers.PLUS_TWO;
        private const int NVP_NAME = ArrayInfo.ARRAY_FIRST_ELEMENT;
        private const int NVP_VALUE = MagicNumbers.PLUS_ONE;

        private const int PATENTLY_INVALID_ORDINAL = MagicNumbers.ZERO;

        private const int PGM_NAME_ONLY = MagicNumbers.PLUS_ONE;

        private const int SKIP_PGM_NAME_ARG = MagicNumbers.PLUS_ONE;

        private const int SUBSTRING_NOT_FOUND = MagicNumbers.MINUS_ONE;

        private const int SW_ARG_POS = MagicNumbers.PLUS_TWO;
        private const int SW_MIN_LEN = MagicNumbers.PLUS_TWO;
        private const int SW_NAME_POS = MagicNumbers.PLUS_ONE;
        private const int SW_NAME_LEN = MagicNumbers.PLUS_ONE;

        private const char NAMED_ARG_PREFIX = 'N';

        private const char POSITIONAL_ARG_PREFIX = 'P';

        private const char SWITCH_ARG_PREFIX = 'S';
		#endregion	// Private Constants


		#region Private Static Data Members
		private static char [ ] s_achrNameValueDelimiter = { NAME_VALUE_DELIMITER };
		private static readonly string s_strSwitchIsOff = Core.Properties.Resources.SWITCH_IS_OFF;
		private static readonly string s_strSwitchIsOn = Core.Properties.Resources.SWITCH_IS_ON;
		#endregion	// Private Static Data Members


		#region Private Storage for Class Instances
		private Dictionary<string , string> _dctValidArgs = null;
		private bool _fArgListIsEmpty = ARG_LIST_IS_EMPTY;
        private bool _fAllowEmptyStringAsDefault = BLANK_AS_DEFAULT_FORBIDDEN;

        private int _intDefinedNamedArgs = NONE;
        private int _intDefinedSwitches = NONE;

        private int _intInvalidNamedArgsInCmd = NONE;
        private int _intInvalidSwitchesInCmd = NONE;

        private int _intPositionalArgCountLimit = POSITIONAL_ARGS_COUNT_LIMIT;
        private int _intPositionalArgsInCmdLine = PATENTLY_INVALID_ORDINAL;

        private int _intValidNmedArgsInCmd = NONE;
        private int _intValidSwitchesInCmd = NONE;

        private ArgMatching _enmArgMatching = ArgMatching.CaseSensitive;

        private ArgType [ ] _enmArgType;
		#endregion	// Private Storage for Class Instances


		#region Constructors
		/// <summary>
        /// Initialize an instance that supports exclusively positional
        /// arguments.
        /// </summary>
        public CmdLneArgsBasic ( )
        {
            ParseArgs ( );
        }   // default CmdLneArgs constructor, accepting only positional arguments (1 of 33)


        /// <summary>
        /// Initialize an instance that supports exclusively positional
        /// arguments, and enforces a user-specified limit, in lieu of a default
        /// limit of 99 positional arguments
        ///
        /// Please see Remarks for important security considerations.
        /// </summary>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic ( int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            ParseArgs ( );
        }   // default CmdLneArgs constructor, accepting only a user-defined number of  positional arguments (2 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, all having a
        /// default value of Properties.Resources.SWITCH_IS_OFF.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        public CmdLneArgsBasic ( char [ ] pachrValidSwitches )
        {
            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (3 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having a default value of VALUE_NOT_SET.
        /// </summary>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        public CmdLneArgsBasic ( string [ ] pastrValidNamedArgs )
        {
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments as an array of strings (4 of 33)


        /// <summary>
        /// Initialize the instance with lists of valid switches and named arguments.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            string [ ] pastrValidNamedArgs )
        {
            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (5 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the value specified in the corresponding item in pdctValidSwitches.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        public CmdLneArgsBasic ( Dictionary<char , string> pdctValidSwitches )
        {
            InitSwitchesFromDictionary ( pdctValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches and their default values as a dictionary indexed by switch name (6 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having the default value specified in the corresponding item in
        /// pdctValidNamedArgs.
        /// </summary>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        public CmdLneArgsBasic (
			Dictionary<string , string> pdctValidNamedArgs )
        {
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (7 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value specified in the corresponding item in
        /// pdctValidSwitches, and a separate list of valid named arguments,
        /// defined in the same manner.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches,
            Dictionary<string , string> pdctValidNamedArgs )
        {
            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (8 of 33)

        /// <summary>
        /// Initialize the instance with a list of valid switches, all having a
        /// default value of Properties.Resources.SWITCH_IS_OFF.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (9 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having a default value of VALUE_NOT_SET.
        /// </summary>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            string [ ] pastrValidNamedArgs ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments as an array of strings (10 of 33)


        /// <summary>
        /// Initialize the instance with lists of valid switches and named arguments.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            string [ ] pastrValidNamedArgs ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (11 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the value specified in the corresponding item in pdctValidSwitches.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromDictionary ( pdctValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches and their default values as a dictionary indexed by switch name (12 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having the default value specified in the corresponding item in
        /// pdctValidNamedArgs.
        /// </summary>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            Dictionary<string , string> pdctValidNamedArgs ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (13 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value specified in the corresponding item in
        /// pdctValidSwitches, and a separate list of valid named arguments,
        /// defined in the same manner.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            Dictionary<string , string> pdctValidNamedArgs ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (14 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, all having a
        /// default value of Properties.Resources.SWITCH_IS_OFF.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (15 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having a default value of VALUE_NOT_SET.
        /// </summary>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            string [ ] pastrValidNamedArgs ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments as an array of strings (16 of 33)


        /// <summary>
        /// Initialize the instance with lists of valid switches and named arguments.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            string [ ] pastrValidNamedArgs ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (17 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the value specified in the corresponding item in pdctValidSwitches.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitSwitchesFromDictionary ( pdctValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches and their default values as a dictionary indexed by switch name (18 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having the default value specified in the corresponding item in
        /// pdctValidNamedArgs.
        /// </summary>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<string , string> pdctValidNamedArgs ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (19 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value specified in the corresponding item in
        /// pdctValidSwitches, and a separate list of valid named arguments,
        /// defined in the same manner.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            Dictionary<string , string> pdctValidNamedArgs ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (20 of 33)

        /// <summary>
        /// Initialize the instance with a list of valid switches, all having a
        /// default value of Properties.Resources.SWITCH_IS_OFF.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (21 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having a default value of VALUE_NOT_SET.
        /// </summary>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            string [ ] pastrValidNamedArgs ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments as an array of strings (22 of 33)


        /// <summary>
        /// Initialize the instance with lists of valid switches and named arguments.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            string [ ] pastrValidNamedArgs ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches as an array of characters (23 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the value specified in the corresponding item in pdctValidSwitches.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromDictionary ( pdctValidSwitches );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches and their default values as a dictionary indexed by switch name (24 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, each
        /// having the default value specified in the corresponding item in
        /// pdctValidNamedArgs.
        /// </summary>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<string , string> pdctValidNamedArgs ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (25 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value specified in the corresponding item in
        /// pdctValidSwitches, and a separate list of valid named arguments,
        /// defined in the same manner.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            Dictionary<string , string> pdctValidNamedArgs ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as a dictionary indexed by argument name (26 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value specified in the corresponding item in
        /// pdctValidSwitches, and a separate list of valid named arguments, in
        /// an array of strings, with the class supplying a standard default.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            string [ ] pastrValidNamedArgs ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments and their default values as an array of string, and a dictionary of switches with strings for default values, indexed by characters, and a member of the ArgMatching enumeration (27 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid named arguments, in
        /// a dictionary strings, where each value is the corresponding default,
        /// and a list of valid switches in an array of characters, with their
        /// default values as the class default.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches,
            Dictionary<string , string> pdctValidNamedArgs ,
            ArgMatching penmArgMatching )
        {
            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid named arguments from a dictionary of strings for default values, indexed by strings, and a member of the ArgMatching enumeration (28 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value defined by the class, and a separate list of valid
        /// named arguments, each defined as a DictionaryItem, with its default
        /// value given in the value member.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            Dictionary<string , string> pdctValidNamedArgs )
        {
            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches, defined by an array of characters, taking the class default value, and  named arguments from a dictionary of strings for default values, indexed by strings (29 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value defined by the class, and a separate list of valid
        /// named arguments, each defined as a DictionaryItem, with its default
        /// value given in the value member.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            Dictionary<string , string> pdctValidNamedArgs ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches, defined by an array of characters, taking the class default value, and  named arguments from a dictionary of strings for default values, indexed by strings (30 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// its default value defined by the value member of a DictionaryItem,
        /// and a separate list of valid named arguments, each defined as a
        /// DictionaryItem, with its default value given in the value member.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="penmArgMatching">
        /// Use this member of the CmdLneArgs.ArgMatching enumeration to set the
        /// argument matching rules. At present, two rules are defined. See the
        /// IntelliSense documentation of any CmdLneArgs argument for details.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            string [ ] pastrValidNamedArgs ,
            ArgMatching penmArgMatching ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            _enmArgMatching = penmArgMatching;									// This must be set before anything major happens.

            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches, defined by an array of characters, taking the class default value, and  named arguments from a dictionary of strings for default values, indexed by strings (31 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// the default value defined by the class, and a separate list of valid
        /// named arguments, each defined as a DictionaryItem, with its default
        /// value given in the value member.
        /// </summary>
        /// <param name="pachrValidSwitches">
        /// Array of characters, each of which is a valid (supported) switch.
        /// All switches are initialized to Properties.Resources.SWITCH_IS_OFF.
        /// </param>
        /// <param name="pdctValidNamedArgs">
        /// Dictionary, keyed by argument name, a string, containing its desired
        /// default value, also a string.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            char [ ] pachrValidSwitches ,
            Dictionary<string , string> pdctValidNamedArgs ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitSwitchesFromArrayOfChars ( pachrValidSwitches );
            InitNamedArgsFromDictionary ( pdctValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches, defined by an array of characters, taking the class default value, and  named arguments from a dictionary of strings for default values, indexed by strings (32 of 33)


        /// <summary>
        /// Initialize the instance with a list of valid switches, each having
        /// its default value defined by the value member of a DictionaryItem,
        /// and a separate list of valid named arguments, defined in an array of
        /// strings, with the default value supplied by the class.
        /// </summary>
        /// <param name="pdctValidSwitches">
        /// Dictionary, keyed by switch name, a character, containing its desired
        /// default value, a string.
        /// </param>
        /// <param name="pastrValidNamedArgs">
        /// Array of strings, each of which is a valid (supported) argument
        /// name. All arguments are initialized to VALUE_NOT_SET.
        /// </param>
        /// <param name="pintPositionalArgCountLimit">
        /// The default limit on the number of positional arguments is
        /// POSITIONAL_ARGS_COUNT_LIMIT, which is currently set to 9. Use this
        /// argument to set a lower or higher limit.
        ///
        /// A pintPositionalArgCountLimit value of less than zero is discarded
        /// silently, and the default limit remains in force.
        ///
        /// Please see Remarks for important security considerations.
        /// </param>
        /// <remarks>
        /// SECURITY NOTE
        ///
        /// All constructors in this class enforce a limit on the number of
        /// positional arguments permitted. The default limit is 9, which is
        /// defined as a public constant, POSITIONAL_ARGS_COUNT_LIMIT.
        ///
        /// This limit is imposed to protect your program from being fed an
        /// excessively long command line, in an attempt to cause a denial of
        /// service. Although few applications require more than nine positional
        /// command line arguments, the level is made adjustable, to meet that
        /// once per career need for more than nine arguments.
        ///
        /// While objects of this class cannot do anything about the number of
        /// characters in the command line (That is the responsibility of the
        /// Common Language Runtime.), they can, and do, limit the amount of
        /// memory allowed to be consumed to hold arguments parsed from it.
        ///
        /// No such limits are imposed on switches and named arguments, because
        /// the number of valid arguments (hence, the upper limit on memory
        /// consumed by them) is under programmer control, and unlikely to be an
        /// issue, since invalid arguments are discarded.
        ///
        /// WHY NINE?
        ///
        /// I chose the number nine mostly for historical reasons. Starting with
        /// MS-DOS 1.0, the number of positional parameters supported by batch
        /// files (without resorting to tricks with SHIFT) is 9.
        ///
        /// This limit has been carried forward into modern command processors,
        /// including CMD.EXE, the default console mode command processor of
        /// Windows NT, 2000, XP, Vista, etc.
        /// </remarks>
        public CmdLneArgsBasic (
            Dictionary<char , string> pdctValidSwitches ,
            string [ ] pastrValidNamedArgs ,
            int pintPositionalArgCountLimit )
        {
            if ( pintPositionalArgCountLimit > MagicNumbers.ZERO )
                _intPositionalArgCountLimit = pintPositionalArgCountLimit;

            InitSwitchesFromDictionary ( pdctValidSwitches );
            InitNamedArgsFromArrayOfStrings ( pastrValidNamedArgs );
            ParseArgs ( );
        }   // CmdLneArgs constructor, taking a list of valid switches, defined by an array of characters, taking the class default value, and  named arguments from a dictionary of strings for default values, indexed by strings (33 of 33)
		#endregion	// Constructors


		#region Public Instance Methods
		/// <summary>
        /// Return the value of an argument that has two or more aliases.
        /// </summary>
        /// <param name="pastrValiddArgNames">
        /// This is an array of strings, each of which is the name of a command
        /// line argument that is a synonym. See Remarks.
        /// </param>
        /// <returns>
        /// The return value is the specified value, or its default value, of
        /// none of the synonyms appears in the input. See Remarks.
        /// </returns>
        /// <remarks>
        /// Arguments are evaluated in the order specified in pastrValiddArgNames,
        /// Synonyms are assumed to return the same default value.
        /// </remarks>
        public string GetArgByMultipleAliases (
			string [ ] pastrValiddArgNames )
        {
            if ( pastrValiddArgNames == null )
                throw new ArgumentNullException ( ARGNAME_VALID_NAMES );

            if ( pastrValiddArgNames.Length == MagicNumbers.ZERO )
                throw new ArgumentException (
					Core.Properties.Resources.EMPTY_ALIAS_LIST ,
					ARGNAME_VALID_NAMES );

            foreach ( string strArgName in pastrValiddArgNames )
            {
                string rstrArgValue = this.GetArgByName (
                    strArgName ,
                    null ,
                    CmdLneArgsBasic.BLANK_AS_DEFAULT_ALLOWED );

                if ( !string.IsNullOrEmpty ( rstrArgValue ) )
                {
                    return rstrArgValue;
                }   // if ( !string.IsNullOrEmpty ( rstrArgValue ) )
            }   // foreach ( string strArgName in pastrValiddArgNames )

            return SpecialStrings.EMPTY_STRING;
        }   //GetArgByMultipleAliases

        
        /// <summary>
        /// Given the external name of a named argument (the name that was
        /// supplied to the constructor), return its value, which is either the
        /// value given on the command line or the default specified in this
        /// method call.
        /// </summary>
        /// <param name="pstrArgName">
        /// Specify the external name of the argument, as it was identified to
        /// the constructor.
        /// </param>
        /// <returns>
        /// If the requested name is valid, and the argument was entered on the
        /// command line, the value supplied on the command line is returned. If
        /// the argument was omitted from the command line, or if the specified
        /// name is invalid, the value specified in argument pstrDefault is
        /// returned.
        /// </returns>
        /// <remarks>
        /// This overload is a wrapper around the second overload,
        /// GetArgByName ( int, string ), which does the real work.
        /// This method calls it internally, passing a default value of
        /// VALUE_NOT_SET.
        /// </remarks>
        public string GetArgByName (
			string pstrArgName )
        {
            return GetArgByName (
                pstrArgName ,
                VALUE_NOT_SET );
        }   // GetArgByName (1 of 3)

        /// <summary>
        /// Given the external name of a named argument (the name that was
        /// supplied to the constructor), return its value, which is either the
        /// value given on the command line or the default specified in this
        /// method call.
        /// </summary>
        /// <param name="pstrArgName">
        /// Specify the external name of the argument, as it was identified to
        /// the constructor.
        /// </param>
        /// <param name="pstrDefault">
        /// Specify a default value to return if the argument was omitted from
        /// the command line, or VALUE_NOT_SET or a null reference (Nothing in
        /// Visual Basic), to use an empty string as the default.
        /// </param>
        /// <returns>
        /// If the requested name is valid, and the argument was entered on the
        /// command line, the value supplied on the command line is returned. If
        /// the argument was omitted from the command line, or if the specified
        /// name is invalid, the value specified in argument pstrDefault is
        /// returned.
        /// </returns>
		/// <remarks>
		/// POTENTIAL BREAKING CHANGE:
		/// 
		/// When the AllowEmptyStringAsDefault property is FALSE and argument
		/// pstrDefault is a null reference, this method returns the null
		/// reference. Previous versions returned the empty string anyway, which
		/// wasn't the intended behavior, and a missing command line argument
		/// thereby escaped detection.
		/// 
		/// Hence, going forward, when pstrDefault is NULL, you must be prepared
		/// to handle a null reference as the return value.
		/// </remarks>
		public string GetArgByName (
            string pstrArgName,
            string pstrDefault )
        {
            string strArgKey = GetNamedArgKey (
                pstrArgName ,
                _enmArgMatching );
            string strArgValue;

            if ( string.IsNullOrEmpty ( strArgKey ) )
                return GetArgDefaultToUse (
                    pstrDefault ,
                    _fAllowEmptyStringAsDefault );
            else
				if ( _dctValidArgs.TryGetValue ( strArgKey , out strArgValue ) )
                    if ( strArgValue == VALUE_NOT_SET )
                        return GetArgDefaultToUse (
                            pstrDefault ,
                            _fAllowEmptyStringAsDefault );
                    else
                        return strArgValue;
                else
                    return GetArgDefaultToUse (
                        pstrDefault ,
                        _fAllowEmptyStringAsDefault );
        }   // GetArgByName (2 of 3)

        /// <summary>
        /// Given the external name of a named argument (the name that was
        /// supplied to the constructor), return its value, which is either the
        /// value given on the command line or the default specified in this
        /// method call.
        /// </summary>
        /// <param name="pstrArgName">
        /// Specify the external name of the argument, as it was identified to
        /// the constructor.
        /// </param>
        /// <param name="pstrDefault">
        /// Specify a default value to return if the argument was omitted from
        /// the command line, or VALUE_NOT_SET or a null reference (Nothing in
        /// Visual Basic), to use an empty string as the default.
        /// </param>
        /// <param name="pfAllowEmptyStringAsDefault">
        /// Set this to TRUE if you want an empty string for the value of the
        /// pstrDefault argument treated as a valid default value. Otherwise,
        /// empty strings are treated as null references (Nothing in Visual
        /// Basic).
        /// </param>
        /// <returns>
        /// If the requested name is valid, and the argument was entered on the
        /// command line, the value supplied on the command line is returned. If
        /// the argument was omitted from the command line, or if the specified
        /// name is invalid, the value specified in argument pstrDefault is
        /// returned.
        /// </returns>
		/// <remarks>
		/// POTENTIAL BREAKING CHANGE:
		/// 
		/// When the AllowEmptyStringAsDefault property is FALSE and argument
		/// pstrDefault is a null reference, this method returns the null
		/// reference. Previous versions returned the empty string anyway, which
		/// wasn't the intended behavior, and a missing command line argument
		/// thereby escaped detection.
		/// 
		/// Hence, going forward, when pstrDefault is NULL, you must be prepared
		/// to handle a null reference as the return value.
		/// </remarks>
		public string GetArgByName (
            string pstrArgName ,
            string pstrDefault ,
            bool pfAllowEmptyStringAsDefault )
        {
            string strArgKey = GetNamedArgKey (
                pstrArgName ,
                _enmArgMatching );
            string strArgValue;

            if ( string.IsNullOrEmpty ( strArgKey ) )
                return GetArgDefaultToUse (
                    pstrDefault ,
                    pfAllowEmptyStringAsDefault );
            else
				if ( _dctValidArgs.TryGetValue ( strArgKey , out strArgValue ) )
                    if ( strArgValue == VALUE_NOT_SET )
                        return GetArgDefaultToUse (
                            pstrDefault ,
                            pfAllowEmptyStringAsDefault );
                    else
                        return strArgValue;
                else
                    return GetArgDefaultToUse (
                        pstrDefault ,
                        pfAllowEmptyStringAsDefault );
        }   // GetArgByName (3 of 3)


        /// <summary>
        /// Given the external name of a named argument (the name that was
        /// supplied to the constructor), return its value, which is either the
        /// value given on the command line or the default specified in this
        /// method call.
        /// </summary>
        /// <param name="pstrArgName">
        /// Specify the external name of the argument, as it was identified to
        /// the constructor.
        /// </param>
        /// <returns>
        /// If the requested name is valid, and the argument was entered on the
        /// command line, the value supplied on the command line is returned. If
        /// the argument was omitted from the command line, or if the specified
        /// name is invalid, the value specified in argument pchrDefault is
        /// returned.
        /// </returns>
        /// <remarks>
        /// This method returns through GetArgByName, by way of 
        /// FirstCharFromString. Hence, everything that applies to either of
        /// them applies as well to this method.
        /// </remarks>
        public char GetArgByNameAsChar (
			string pstrArgName )
        {
            return FirstCharFromString ( GetArgByName ( pstrArgName ) );
        }   // GetArgByNameAsChar (1 of 3)

        /// <summary>
        /// Given the external name of a named argument (the name that was
        /// supplied to the constructor), return its value, which is either the
        /// value given on the command line or the default specified in this
        /// method call.
        /// </summary>
        /// <param name="pstrArgName">
        /// Specify the external name of the argument, as it was identified to
        /// the constructor.
        /// </param>
        /// <param name="pchrDefault">
        /// Specify a default value to return if the argument was omitted from
        /// the command line, or VALUE_NOT_SET or a null reference (Nothing in
        /// Visual Basic), to use a null character as the default.
        /// </param>
        /// <returns>
        /// If the requested name is valid, and the argument was entered on the
        /// command line, the value supplied on the command line is returned. If
        /// the argument was omitted from the command line, or if the specified
        /// name is invalid, the value specified in argument pchrDefault is
        /// returned.
        /// </returns>
        /// <remarks>
        /// This method returns through GetArgByName, by way of 
        /// FirstCharFromString. Hence, everything that applies to either of
        /// them applies as well to this method.
        /// </remarks>
        public char GetArgByNameAsChar (
            string pstrArgName ,
            char pchrDefault)
        {
            return FirstCharFromString (
                GetArgByName (
                    pstrArgName ,
                    pchrDefault.ToString ( ) ) ,
                pchrDefault );
        }   // GetArgByNameAsChar (2 of 3)

        /// <summary>
        /// Given the external name of a named argument (the name that was
        /// supplied to the constructor), return its value, which is either the
        /// value given on the command line or the default specified in this
        /// method call.
        /// </summary>
        /// <param name="pstrArgName">
        /// Specify the external name of the argument, as it was identified to
        /// the constructor.
        /// </param>
        /// <param name="pchrDefault">
        /// Specify a default value to return if the argument was omitted from
        /// the command line, or VALUE_NOT_SET or a null reference (Nothing in
        /// Visual Basic), to use a null character as the default.
        /// </param>
        /// <param name="pfAllowEmptyStringAsDefault">
        /// Set this to TRUE if you want an empty string for the value of the
        /// pstrDefault argument treated as a valid default value. Otherwise,
        /// empty strings are treated as null references (Nothing in Visual
        /// Basic).
        /// </param>
        /// <returns>
        /// If the requested name is valid, and the argument was entered on the
        /// command line, the value supplied on the command line is returned. If
        /// the argument was omitted from the command line, or if the specified
        /// name is invalid, the value specified in argument pchrDefault is
        /// returned.
        /// </returns>
        /// <remarks>
        /// This method returns through GetArgByName, by way of 
        /// FirstCharFromString. Hence, everything that applies to either of
        /// them applies as well to this method.
        /// </remarks>
        public char GetArgByNameAsChar ( 
            string pstrArgName ,
            char pchrDefault , 
            bool pfAllowEmptyStringAsDefault )
        {
            return FirstCharFromString ( 
                GetArgByName (
                    pstrArgName ,
                    pchrDefault.ToString ( ) ,
                    pfAllowEmptyStringAsDefault ) ,
                pchrDefault );
        }   // GetArgByNameAsChar (3 of 3)


		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// zero.
		/// </summary>
		/// <param name="pstrArgName">
		/// Specify the external name of the argument, as it was identified to
		/// the constructor.
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetArgByName to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetArgByNameAsInt (
			string pstrArgName )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetArgByName ( pstrArgName ) , out rintValue ) )
				return rintValue;
			else
				return MagicNumbers.ZERO;
		}   // GetArgByNameAsInt (1 of 3)

		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// pintDefault.
		/// </summary>
		/// <param name="pstrArgName">
		/// Specify the external name of the argument, as it was identified to
		/// the constructor.
		/// </param>
		/// <param name="pintDefault">
		/// Specify a default value to return if the argument was omitted from
		/// the command line.
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetArgByName to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetArgByNameAsInt (
			string pstrArgName ,
			int pintDefault )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetArgByName ( pstrArgName , pintDefault.ToString ( ) ) , out rintValue ) )
				return rintValue;
			else
				return pintDefault;
		}   // GetArgByNameAsInt (2 of 3)

		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// pintDefault.
		/// </summary>
		/// <param name="pstrArgName">
		/// Specify the external name of the argument, as it was identified to
		/// the constructor.
		/// </param>
		/// <param name="pintDefault">
		/// Specify a default value to return if the argument was omitted from
		/// the command line.
		/// </param>
		/// <param name="pfAllowEmptyStringAsDefault">
		/// Set this to TRUE if you want an empty string for the value of the
		/// pstrDefault argument treated as a valid default value. Otherwise,
		/// empty strings are treated as null references (Nothing in Visual
		/// Basic).
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetArgByName to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetArgByNameAsInt (
			string pstrArgName ,
			int pintDefault ,
			bool pfAllowEmptyStringAsDefault )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetArgByName ( pstrArgName , pintDefault.ToString ( ) , pfAllowEmptyStringAsDefault ) , out rintValue ) )
				return rintValue;
			else
				return pintDefault;
		}   // GetArgByNameAsInt (3 of 3)


        /// <summary>
        /// Get a positional argument by its ordinal position.
        /// </summary>
        /// <param name="pintIndex">
        /// Position of argument in list. Arguments are numbered from 1.
        /// </param>
        /// <returns>
        /// The argument value at the ordinal position specified by pintIndex is
        /// returned, unless pintIndex is out of range, in which case an empty
        /// string is returned.
        /// </returns>
        /// <remarks>
        /// This overload is a wrapper around the second overload,
        /// GetArgByPosition ( int, string ), which does the real work.
        /// This method calls it internally, passing a default value of
        /// VALUE_NOT_SET.
        /// </remarks>
        public string GetArgByPosition (
			int pintIndex )
        {
            return GetArgByPosition (
                pintIndex ,
                VALUE_NOT_SET );
        }   // GetArgByPosition (1 of 2)


        /// <summary>
        /// Get a positional argument by its ordinal position.
        /// </summary>
        /// <param name="pintIndex">
        /// Position of argument in list. Arguments are numbered from 1.
        /// </param>
        /// <param name="pstrDefault">
        /// Default value to return if argument ordinal is greater than the
        /// number of positional arguments entered on the command line.
        /// </param>
        /// <returns>
        /// The argument value at the ordinal position specified by pintIndex is
        /// returned, unless pintIndex is out of range, in which case, the
        /// default value specified in pstrDefault is returned.
        /// </returns>
		/// <remarks>
		/// POTENTIAL BREAKING CHANGE:
		/// 
		/// When the AllowEmptyStringAsDefault property is FALSE and argument
		/// pstrDefault is a null reference, this method returns the null
		/// reference. Previous versions returned the empty string anyway, which
		/// wasn't the intended behavior, and a missing command line argument
		/// thereby escaped detection.
		/// 
		/// Hence, going forward, when pstrDefault is NULL, you must be prepared
		/// to handle a null reference as the return value.
		/// </remarks>
		public string GetArgByPosition (
            int pintIndex ,
            string pstrDefault )
        {
            if ( pintIndex > PATENTLY_INVALID_ORDINAL && pintIndex <= _intPositionalArgsInCmdLine )
				return _dctValidArgs [ string.Format (
                    ARG_KEY_FMT ,
                    POSITIONAL_ARG_PREFIX ,
                    pintIndex ) ];
            else
                return GetArgDefaultToUse (
                    pstrDefault ,
                    _fAllowEmptyStringAsDefault );
        }   // GetArgByPosition (2 of 2)


		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// zero.
		/// </summary>
		/// <param name="pintIndex">
		/// Position of argument in list. Arguments are numbered from 1.
		/// the constructor.
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetArgByPosition to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetArgByPositionAsInt (
			int pintIndex )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetArgByPosition ( pintIndex ) , out rintValue ) )
				return rintValue;
			else
				return MagicNumbers.ZERO;
		}   // GetArgByPositionAsInt (1 of 2)

		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// pintDefault.
		/// </summary>
		/// <param name="pintIndex">
		/// Position of argument in list. Arguments are numbered from 1.
		/// the constructor.
		/// </param>
		/// <param name="pintDefault">
		/// Specify a default value to return if the argument was omitted from
		/// the command line.
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetArgByPosition to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetArgByPositionAsInt (
			int pintIndex ,
			int pintDefault )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetArgByPosition ( pintIndex , pintDefault.ToString ( ) ) , out rintValue ) )
				return rintValue;
			else
				return pintDefault;
		}   // GetArgByPositionAsInt (2 of 2)


        /// <summary>
        /// Test for the presence of a defined switch. The modifier, if any, is
        /// ignored.
        /// </summary>
        /// <param name="pchrName">
        /// Character to identify switch. This must be one of the characters
        /// stored in the array, or used as keys in the dictionary passed into
        /// the instance constructor.
        /// </param>
        /// <returns>
        /// The return value is TRUE if the switch is present, with or without a
        /// modifier (which is ignored). Otherwise, the return value is FALSE.
        /// </returns>
        public bool GetBooleanSwitchByName (
			char pchrName )
        {
			//	----------------------------------------------------------------
			//	The down side of replacing the embedded constants with resource
			//	strings is that the super-efficient Switch block must give way
			//	to a cascading IF block.
			//	----------------------------------------------------------------

			string strSwitchValue = GetSwitchByName ( pchrName );

			if ( strSwitchValue == s_strSwitchIsOff )
				return false;
			else if ( strSwitchValue == s_strSwitchIsOn )
				return true;
			else
				return true;
        }   // GetBooleanSwitchByName


        /// <summary>
        /// Return the value stored for a switch argument.
        /// </summary>
        /// <param name="pchrName">
        /// Character to identify switch. This must be one of the characters
        /// stored in the array, or used as keys in the dictionary passed into
        /// the instance constructor.
        /// </param>
        /// <returns>
        /// If the switch was defined in the constructor, and it was specified
        /// on the command line of the executing assembly, the value defined on
        /// the command line is returned. Otherwise, the default value, if any,
        /// passed into the constructor is returned. Failing both of those, the
        /// generic default value constant, Properties.Resources.SWITCH_IS_OFF,
		/// is returned.
        /// </returns>
        /// <remarks>
        /// This overload is a wrapper around the other overload,
        /// GetSwitchByName ( char, string ), which does the real work.
        /// This method calls it internally, passing a default value of
        /// Properties.Resources.SWITCH_IS_OFF.
        /// </remarks>
        public string GetSwitchByName (
			char pchrName )
        {
            return GetSwitchByName (
                pchrName ,
                Core.Properties.Resources.SWITCH_IS_OFF );
        }   // GetSwitchByName method (1 of 3)


        /// <summary>
        /// Return the value stored for a switch argument.
        /// </summary>
        /// <param name="pchrName">
        /// Character to identify switch. This must be one of the characters
        /// stored in the array, or used as keys in the dictionary passed into
        /// the instance constructor.
        /// </param>
        /// <param name="pstrDefault">
        /// Default value to return if argument pchrName is not in the list of
        /// valid switches passed into the constructor. See pchrName.
        /// </param>
        /// <returns>
        /// If the switch was defined in the constructor, and it was specified
        /// on the command line of the executing assembly, the value defined on
        /// the command line is returned. Otherwise, the default value, if any,
        /// passed into the constructor is returned. Failing both of those, the
        /// generic default value constant, Properties.Resources.SWITCH_IS_OFF,
		/// is returned.
        /// </returns>
		/// <remarks>
		/// POTENTIAL BREAKING CHANGE:
		/// 
		/// When the AllowEmptyStringAsDefault property is FALSE and argument
		/// pstrDefault is a null reference, this method returns the null
		/// reference. Previous versions returned the empty string anyway, which
		/// wasn't the intended behavior, and a missing command line argument
		/// thereby escaped detection.
		/// 
		/// Hence, going forward, when pstrDefault is NULL, you must be prepared
		/// to handle a null reference as the return value.
		/// </remarks>
		public string GetSwitchByName (
            char pchrName ,
            string pstrDefault )
        {
            //  ----------------------------------------------------------------
            //  Construct key name from switch name.
            //  ----------------------------------------------------------------

            string strSwitchArg;
            string strInternalName = GetSwitchInternalName (
                pchrName ,
                _enmArgMatching );

            //  ----------------------------------------------------------------
            //  Return value saved from command line, or default, if none was.
            //  ----------------------------------------------------------------

			if ( _dctValidArgs.TryGetValue ( strInternalName , out strSwitchArg ) )
                if ( strSwitchArg == Core.Properties.Resources.SWITCH_IS_OFF )  // Argument is defined. Make a local copy.
                    return GetSwitchDefaultToUse (
                        pstrDefault,
                        _fAllowEmptyStringAsDefault );							// Argument was omitted from command line. Return its default value.
                else
                    return strSwitchArg;										// Switch was specified on the command line. Return its setting.
            else
                return GetSwitchDefaultToUse (
                    pstrDefault ,
                    _fAllowEmptyStringAsDefault );								//  Return the specified or generic default value.
        }   // GetSwitchByName (2 of 3)


        /// <summary>
        /// Return the value stored for a switch argument.
        /// </summary>
        /// <param name="pchrName">
        /// Character to identify switch. This must be one of the characters
        /// stored in the array, or used as keys in the dictionary passed into
        /// the instance constructor.
        /// </param>
        /// <param name="pstrDefault">
        /// Default value to return if argument pchrName is not in the list of
        /// valid switches passed into the constructor. See pchrName.
        /// </param>
        /// <param name="pfAllowEmptyStringAsDefault">
        /// Set this to TRUE if you want an empty string for the value of the
        /// pstrDefault argument treated as a valid default value. Otherwise,
        /// empty strings are treated as null references (Nothing in Visual
        /// Basic).
        /// </param>
        /// <returns>
        /// If the switch was defined in the constructor, and it was specified
        /// on the command line of the executing assembly, the value defined on
        /// the command line is returned. Otherwise, the default value, if any,
        /// passed into the constructor is returned. Failing both of those, the
        /// generic default value constant, Properties.Resources.SWITCH_IS_OFF,
		/// is returned.
        /// </returns>
        public string GetSwitchByName (
            char pchrName ,
            string pstrDefault,
            bool pfAllowEmptyStringAsDefault )
        {
            //  ----------------------------------------------------------------
            //  Construct key name from switch name.
            //  ----------------------------------------------------------------

            string strSwitchArg;
            string strInternalName = GetSwitchInternalName (
                pchrName ,
                _enmArgMatching );

            //  ----------------------------------------------------------------
            //  Return value saved from command line, or default, if none was.
            //  ----------------------------------------------------------------

			if ( _dctValidArgs.TryGetValue ( strInternalName , out strSwitchArg ) )
                if ( strSwitchArg == Core.Properties.Resources.SWITCH_IS_OFF )       // Argument is defined. Make a local copy.
                    return GetSwitchDefaultToUse (
                        pstrDefault ,
                        pfAllowEmptyStringAsDefault );							// Argument was omitted from command line. Return its default value.
                else
                    return strSwitchArg;										// Switch was specified on the command line. Return its setting.
            else
                return GetSwitchDefaultToUse (
                    pstrDefault ,
                    pfAllowEmptyStringAsDefault );								//  Return the specified or generic default value.
        }   // GetSwitchByName (3 of 3)


		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// zero.
		/// </summary>
		/// <param name="pchrArgName">
		/// Specify the external name of the argument, as it was identified to
		/// the constructor.
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetSwitchByName to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetSwitchByNameAsInt (
			char pchrArgName )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetSwitchByName ( pchrArgName ) , out rintValue ) )
				return rintValue;
			else
				return MagicNumbers.ZERO;
		}   // GetSwitchByNameAsInt (1 of 3)

		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// pintDefault.
		/// </summary>
		/// <param name="pchrArgName">
		/// Specify the external name of the argument, as it was identified to
		/// the constructor.
		/// </param>
		/// <param name="pintDefault">
		/// Specify a default value to return if the argument was omitted from
		/// the command line.
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetSwitchByName to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetSwitchByNameAsInt (
			char pchrArgName ,
			int pintDefault )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetSwitchByName ( pchrArgName , pintDefault.ToString ( ) ) , out rintValue ) )
				return rintValue;
			else
				return pintDefault;
		}   // GetSwitchByNameAsInt (2 of 3)

		/// <summary>
		/// If the value of a switch is an integer, return it. Otherwise, return
		/// pintDefault.
		/// </summary>
		/// <param name="pchrArgName">
		/// Specify the external name of the argument, as it was identified to
		/// the constructor.
		/// </param>
		/// <param name="pintDefault">
		/// Specify a default value to return if the argument was omitted from
		/// the command line.
		/// </param>
		/// <param name="pfAllowEmptyStringAsDefault">
		/// Set this to TRUE if you want an empty string for the value of the
		/// pstrDefault argument treated as a valid default value. Otherwise,
		/// empty strings are treated as null references (Nothing in Visual
		/// Basic).
		/// </param>
		/// <returns>
		/// This method passes the value returned by GetSwitchByName to the 
		/// int.TryParse method. Hence, everything that applies to either of
		/// them applies as well to this method.
		/// </returns>
		public int GetSwitchByNameAsInt (
			char pchrArgName ,
			int pintDefault ,
			bool pfAllowEmptyStringAsDefault )
		{
			int rintValue = MagicNumbers.ZERO;

			if ( int.TryParse ( GetSwitchByName ( pchrArgName , pintDefault.ToString ( ) , pfAllowEmptyStringAsDefault ) , out rintValue ) )
				return rintValue;
			else
				return pintDefault;
		}   // GetSwitchByNameAsInt (3 of 3)
		#endregion	// Public Instance Methods


        #region Public Static Methods
		/// <summary>
		/// Extract the argument name from a string that consists of the key
		/// (index) of the collection.
		/// </summary>
		/// <param name="pstrArgKey">
		/// Specify the key to parse and strip, so that it can be fed into an
		/// instance method to retrieve the value of a named argument or switch.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the name of a named
		/// argument or of a switch, which can be fed into GetArgByName, 
		/// GetSwitchByName, or one of their siblings.
		/// </returns>
		/// <remarks>
		/// Since this class derives from a Dictionary whose keys and values are
		/// strings, given the string that becomes the pstrArgKey argument of
		/// this method, you could just as easily get the value directly.
		/// However, if you did so, you would lose the benefit of default value
		/// assignment, not to mention the accessors that bundle conversions to
		/// Integer, Boolean, and Character types. Moreover, reaching into the
		/// base class makes it slightly less obvious that the value retrieved
		/// is that of a command line argument, let alone its name and intrinsic
		/// type (switch versus named argument versus positional argument).
		/// 
		/// A wiser design would hide the dictionary from view. Notwithstanding
		/// this one-off use, it is not too late to do so, since this is the
		/// only case in which I have reached into the base class in this way.
		/// </remarks>
		public static string ArgNameFromKeyValue ( 
			string pstrArgKey )
		{
			const int PREFIX_LENGTH = MagicNumbers.PLUS_TWO;

			if ( string.IsNullOrEmpty ( pstrArgKey ) )
			{	// A null reference or the empty string get the same treatment; either is a degenerate case.
				return SpecialStrings.EMPTY_STRING;
			}	// TRUE (degenerate case) block, if ( string.IsNullOrEmpty ( pstrArgName ) )
			else
			{
				if ( pstrArgKey.Length < PREFIX_LENGTH )
				{	// The string being too short is the third of three degenerate cases.
					return SpecialStrings.EMPTY_STRING;
				}	// TRUE (degenerate case) block, if ( pstrArgName.Length < PREFIX_LENGTH )
				else
				{	// The string passed the smell test. Clip the prefix, and return the rest.
					return pstrArgKey.Substring ( PREFIX_LENGTH );
				}	// FALSE (desired outcome) block, if ( pstrArgName.Length < PREFIX_LENGTH )
			}	// FALSE (desired outcome) block, if ( string.IsNullOrEmpty ( pstrArgName ) )
		}	// public static string ArgNameFromKeyValue


        /// <summary>
        /// Extract the first character from a string, and return it as a char
        /// value type.
        /// </summary>
        /// <param name="pstrIn">
        /// The first character in this string is returned.
        /// </param>
        /// <returns>
        /// The return value is the first character of string pstrIn, as a
        /// scalar value type.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If the input string, pstrIn, is null or empty, an ArgumentException
        /// is thrown. See the next overload for an alternative that doesn't
        /// throw exceptions.
        /// </exception>
        public static char FirstCharFromString ( string pstrIn )
        {
            const string ARGNAME = @"pstrIn";

            if ( string.IsNullOrEmpty ( pstrIn ) )
            {
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARGNAME );
            }   // TRUE (normal) block, if ( string.IsNullOrEmpty ( pstrIn ) )
            else
            {
                char [ ] achrFirst = pstrIn.ToCharArray (
                    ArrayInfo.ARRAY_FIRST_ELEMENT ,
                    MagicNumbers.PLUS_ONE );
				return achrFirst [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
            }   // FALSE (exception) block, if ( string.IsNullOrEmpty ( pstrIn ) )
        }   // public static char FirstCharFromString (1 of 2)


        /// <summary>
        /// Extract the first character from a string, and return it as a char
        /// value type.
        /// </summary>
        /// <param name="pstrIn">
        /// The first character in this string is returned.
        /// </param>
        /// <param name="pchrDefault">
        /// If string pstrIn is null or empty, this character is returned.
        /// </param>
        /// <returns>
        /// The return value is the first character of string pstrIn, as a
        /// scalar value type.
        /// </returns>
        public static char FirstCharFromString (
			string pstrIn ,
			char pchrDefault )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
            {
                return pchrDefault;
            }   // TRUE (normal) block, if ( string.IsNullOrEmpty ( pstrIn ) )
            else
            {
                char [ ] achrFirst = pstrIn.ToCharArray (
					ArrayInfo.ARRAY_FIRST_ELEMENT ,
                    MagicNumbers.PLUS_ONE );
				return achrFirst [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
            }   // FALSE (exception) block, if ( string.IsNullOrEmpty ( pstrIn ) )
        }   // public static char FirstCharFromString (2 of 2)
		#endregion	// Public Static Methods


		#region Public Properties, All but One Read Only
		/// <summary>
        /// This property returns True (ARG_LIST_IS_EMPTY) when the command line
        /// is devoid of arguments.
        ///
        /// Otherwise, it returns False (ARG_LIST_HAS_ARGS), indicating that the
        /// command line contains at least one argument. See Remarks.
        /// </summary>
        /// <remarks>
        /// ArgListIsEmpty being false means only that there is an argument in
        /// the command line, which may be invalid. Check the ArgMatching array
        /// or the two invalid argument counters, InvalidNamedArgsInCmd and
        /// InvalidSwitchesInCmd.
        /// </remarks>
        public bool ArgListIsEmpty { get { return _fArgListIsEmpty; } }


        /// <summary>
        /// The value of this property determines how the GetArgDefaultToUse and
        /// GetSwitchDefaultToUse methods treat an empty string supplied as a
        /// default value to return. See Remarks for complete details.
        /// </summary>
        /// <remarks>
        /// The AllowEmptyStringAsDefault property is initialized to FALSE, to
        /// maintain backwards compatibility with previous versions of the
        /// library that exports this class.
        /// 
        /// By default switches return a string value of "OFF" to indicate their
        /// omission from the command line. To override this behavior, you must
        /// call the two-argument overload of GetSwitchDefaultToUse, specifying
        /// a default string value as the second argument.
        /// 
        /// Prior to version 2.0, this overload treated both null references
        /// (Nothing in Visual Basic) and empty strings equally, returning the
        /// class default value of "OFF" in both cases.
        /// 
        /// Beginning with version 2.0, this behavior can be overridden in one
        /// of two ways.
        /// 
        /// 1) Setting the AllowEmptyStringAsDefault property to TRUE (signified
        /// by symbolic constant BLANK_AS_DEFAULT_ALLOWED) changes the behavior
        /// for the entire class for the rest of its lifetime, or until it is
        /// set back to its default, whichever comes first.
        /// 
        /// 2) Calling method GetSwitchDefaultToUse or GetArgDefaultToUse with a
        /// third (Boolean) argument that affects only the behavior of that one
        /// call, regardless of the current AllowEmptyStringAsDefault setting.
        /// This method leaves the AllowEmptyStringAsDefault property unchanged.
        /// </remarks>
		/// <remarks>
		/// POTENTIAL BREAKING CHANGE:
		/// 
		/// When the AllowEmptyStringAsDefault property is FALSE and argument
		/// pstrDefault is a null reference, this method returns the null
		/// reference. Previous versions returned the empty string anyway, which
		/// wasn't the intended behavior, and a missing command line argument
		/// thereby escaped detection.
		/// 
		/// Hence, going forward, when pstrDefault is NULL, you must be prepared
		/// to handle a null reference as the return value.
		/// </remarks>
		public bool AllowEmptyStringAsDefault
        {
            get { return _fAllowEmptyStringAsDefault; }
            set { _fAllowEmptyStringAsDefault = value; }
        }   // AllowEmptyStringAsDefault (Read/Write)


        /// <summary>
        /// Although you cannot change the rules in the middle of the game,
        /// transparency is highly desirable.
        /// </summary>
        public ArgMatching ArgumentMatching { get { return _enmArgMatching; } }


        /// <summary>
        /// This property is an array of ArgType enumerated types, which has one
        /// element for each command line argument. Use this list to find and
        /// report invalid command line arguments, if any.
        /// </summary>
        public ArgType [ ] ArgumentTypeArray
        {
            get
            {
                if ( _fArgListIsEmpty == ARG_LIST_HAS_ARGS )
                    return _enmArgType;
                else
                    throw new Exception (
						string.Format (
							"Since the ArgListIsEmpty property is True (ARG_LIST_IS_EMPTY), this property is both null and meaningless.{0}To prevent this exception, check the ArgListIsEmpty property before attempting to check this property." ,
							Environment.NewLine ) );
            }   // public ArgType [ ] ArgumentTypeArray Get method
        }   // ArgumentTypeArray property definition.


		/// <summary>
		/// Get the total number of valid arguments of all types.
		/// </summary>
		public int Count { get { return _dctValidArgs.Count; } }
        /// <summary>
        /// Get the count of named arguments defined by the constructor.
        /// </summary>
        public int DefinedNamedArgs { get { return _intDefinedNamedArgs; } }


        /// <summary>
        /// Count of switches defined by the constructor
        /// </summary>
        public int DefinedSwitches { get { return _intDefinedSwitches; } }


        /// <summary>
        /// Total number of invalid arguments found in the command line. This is
        /// the sum of InvalidNamedArgsInCmd and InvalidSwitchesInCmd. Use it as
        /// a quick check for invalid arguments.
        /// </summary>
        public int InvalidArgsCount { get { return _intInvalidNamedArgsInCmd + _intInvalidSwitchesInCmd; } }


        /// <summary>
        /// Count of invalid named arguments found in the command line.
        /// </summary>
        public int InvalidNamedArgsInCmd { get { return _intInvalidNamedArgsInCmd; } }


        /// <summary>
        /// Count of invalid switches found in the command line.
        /// </summary>
        public int InvalidSwitchesInCmd { get { return _intInvalidSwitchesInCmd; } }


        /// <summary>
        /// Count of positional arguments found in the command line.
        /// </summary>
        public int PositionalArgsInCmdLine { get { return _intPositionalArgsInCmdLine; } }


		/// <summary>
		/// Gets the SWITCH_IS_OFF string, which is loaded from the string
		/// resources of the DLL into a private static read-only string.
		/// </summary>
		/// <remarks>
		/// This static read-only property replaces a like named constant, which
		/// I moved into the resource strings because that is really where it
		/// belongs, so that the library can be adapted for another language by
		/// substituting a localized string resource.
		/// 
		/// While the simplest approach would be to assign these names to the
		/// private strings, and mark them public, doing so would obscure their
		/// their true nature.
		/// </remarks>
		public static string SWITCH_IS_OFF
		{
			get
			{
				return s_strSwitchIsOff;
			}	// SWITCH_IS_OFF Property Get method
		}	// SWITCH_IS_OFF Property


		/// <summary>
		/// Gets the SWITCH_IS_ON string, which is loaded from the string
		/// resources of the DLL into a private static read-only string.
		/// </summary>
		/// <remarks>
		/// This static read-only property replaces a like named constant, which
		/// I moved into the resource strings because that is really where it
		/// belongs, so that the library can be adapted for another language by
		/// substituting a localized string resource.
		/// 
		/// While the simplest approach would be to assign these names to the
		/// private strings, and mark them public, doing so would obscure their
		/// their true nature.
		/// </remarks>
		public static string SWITCH_IS_ON
		{
			get
			{
				return s_strSwitchIsOn;
			}	// SWITCH_IS_ON Property Get method
		}	// SWITCH_IS_ON Property

        /// <summary>
        /// Count of valid named arguments found in the command line.
        /// </summary>
        public int ValidNamedArgsInCmdLine { get { return _intValidNmedArgsInCmd; } }

		/// <summary>
        /// Count of valid switches found in the command line.
        /// </summary>
        public int ValidSwitchesInCmdLine { get { return _intValidSwitchesInCmd; } }
		#endregion	// Public Properties, All but One Read Only


		#region Private Instance Methods
		private bool ArgIsNamed (
			string pstrArg )
        {
            if ( _intDefinedNamedArgs > NONE )
                return ArgHasName ( pstrArg );
            else
                return false;
        }   // ArgIsNamed


        private bool ArgIsSwitch (
			string pstrArg )
        {
            if ( _intDefinedSwitches > NONE )
                return ArgHasSwitchPrefix ( pstrArg );
            else
                return false;
        }   // ArgIsSwitch


        private void DefineNewSwitch (
            char pchrSwitchName,
            string pstrDefault )
        {
            try
            {
				if ( _dctValidArgs == null )
					_dctValidArgs = new Dictionary<string , string> ( POSITIONAL_ARGS_COUNT_LIMIT );

                switch ( _enmArgMatching )
                {
                    case ArgMatching.CaseInsensitive:
						_dctValidArgs.Add ( string.Format (
                            ARG_KEY_FMT ,
                            SWITCH_ARG_PREFIX ,
                            char.ToLower ( pchrSwitchName ) ) ,
                            GetSwitchDefaultToUse (
                                pstrDefault ,
                                _fAllowEmptyStringAsDefault ) );
						break;	// case ArgMatching.CaseInsensitive

                    case ArgMatching.CaseSensitive:
                    default:
						_dctValidArgs.Add ( string.Format (
                            ARG_KEY_FMT ,
                            SWITCH_ARG_PREFIX ,
                            pchrSwitchName ) ,
                            GetSwitchDefaultToUse (
                                pstrDefault ,
                                _fAllowEmptyStringAsDefault ) );
						break;	// case ArgMatching.CaseSensitive: and default case
                }   // switch ( _enmArgMatching ) block

                _intDefinedSwitches++;
            }
            catch ( ArgumentException errBadArg )
            {
                string strMsg = string.Format (
					Core.Properties.Resources.ERRMSG_DUPLICATE_SWITCH ,
                    pchrSwitchName ,
                    pstrDefault );
                throw new Exception (
                    strMsg ,
                    errBadArg );
            }
            catch ( Exception errOtherKinds )
            {
                string strMsg = string.Format (
					Core.Properties.Resources.ERRMSG_EXCEPTION_IN_DEFINE_NEW_SWITCH ,
                    errOtherKinds.GetType ( ).FullName ,
                    pchrSwitchName ,
                    pstrDefault );
                throw new Exception (
                    strMsg ,
                    errOtherKinds );
            }
        }   // DefineNewSwitch


        private void DefineNewNamedArg (
            string pstrArgName,
            string pstrArgValue )
        {
			if ( _dctValidArgs == null )
				_dctValidArgs = new Dictionary<string , string> ( POSITIONAL_ARGS_COUNT_LIMIT );

			switch ( _enmArgMatching )
            {
                case ArgMatching.CaseInsensitive:
					_dctValidArgs.Add ( string.Format (
                        ARG_KEY_FMT ,
                        NAMED_ARG_PREFIX ,
                        pstrArgName.ToLower ( ) ) ,
                        pstrArgValue );
					break;	// case ArgMatching.CaseInsensitive

                case ArgMatching.CaseSensitive:
                default:
					_dctValidArgs.Add ( string.Format (
                        ARG_KEY_FMT ,
                        NAMED_ARG_PREFIX ,
                        pstrArgName ) ,
                        pstrArgValue );
					break;	// case ArgMatching.CaseSensitive: and default case
            }   // witch ( _enmArgMatching )

            _intDefinedNamedArgs++;
        }   // DefineNewNamedArg


        private void InitNamedArgsFromArrayOfStrings (
			string [ ] pastrValidNamedArgs )
        {
            if ( pastrValidNamedArgs == null )
                _intDefinedNamedArgs = NONE;									// Argument is null. Fail gracefully. (degenerate case 1 of 2)
            else
                if ( pastrValidNamedArgs.Length == COLLECTION_IS_EMPTY )
                    _intDefinedNamedArgs = NONE;								// The array is empty (degenerate case 2 of 2)
                else
                    foreach ( string strArgName in pastrValidNamedArgs )
                        DefineNewNamedArg (										// Add the item to the dictionary, initializing its value to VALUE_NOT_SET.
                            strArgName ,
                            VALUE_NOT_SET );
        }   // InitNamedArgsFromArrayOfStrings


        private void InitNamedArgsFromDictionary (
			Dictionary<string , string> pdctValidNamedArgs )
        {
            if ( pdctValidNamedArgs == null )
                _intDefinedNamedArgs = NONE;									// Argument is null. Fail gracefully. (degenerate case 1 of 2)
            else
                if ( pdctValidNamedArgs.Count == COLLECTION_IS_EMPTY )
                    _intDefinedNamedArgs = NONE;								// The array is empty (degenerate case 2 of 2)
                else
                    foreach ( string strArgName in pdctValidNamedArgs.Keys )
                        DefineNewNamedArg (										// Add the item to the dictionary, initializing its value to VALUE_NOT_SET.
                            strArgName ,
                            pdctValidNamedArgs [ strArgName ] );
        }   // InitNamedArgsFromDictionary


        private void InitSwitchesFromArrayOfChars (
			char [ ] pachrValidSwitches )
        {
            if ( pachrValidSwitches == null )
                _intDefinedSwitches = NONE;										// Argument is null. Fail gracefully. (degenerate case 1 of 2)
            else
                if ( pachrValidSwitches.Length == COLLECTION_IS_EMPTY )
                    _intDefinedSwitches = NONE;									// The array is empty (degenerate case 2 of 2)
                else
                    foreach ( char chrSwitchName in pachrValidSwitches )
                        DefineNewSwitch (										// Add the item to the dictionary, initializing its value to VALUE_NOT_SET.
                            chrSwitchName ,
							Core.Properties.Resources.SWITCH_IS_OFF );
        }   // InitSwitchesFromArrayOfChars


        private void InitSwitchesFromDictionary (
			Dictionary<char , string> pdctValidSwitches )
        {
            if ( pdctValidSwitches == null )
                _intDefinedSwitches = NONE;										// Argument is null. Fail gracefully. (degenerate case 1 of 2)
            else
                if ( pdctValidSwitches.Count == COLLECTION_IS_EMPTY )
                    _intDefinedSwitches = NONE;									// The array is empty (degenerate case 2 of 2)
                else
                    foreach ( char chrSwitchName in pdctValidSwitches.Keys )
                        DefineNewSwitch (										// Add the item to the dictionary, initializing its value to VALUE_NOT_SET.
                            chrSwitchName ,
                            pdctValidSwitches [ chrSwitchName ] );
        }   // InitSwitchesFromDictionary


		/// <summary>
		/// Every constructor finishes in this routine, which does as its name
		/// implies; using the list of valid arguments, it parses the command
		/// line.
		/// </summary>
        private void ParseArgs ( )
        {
			if ( _dctValidArgs == null )
				_dctValidArgs = new Dictionary<string , string> ( );

            string [ ] astrEnvCmdLineArgs = Environment.GetCommandLineArgs ( );

            //  ----------------------------------------------------------------
            //  Check for the degenerate case of an empty argument list. If so,
            //  since the ArgListIsEmpty property is initialized based on this
            //  assumption, this routine is done.
            //  ----------------------------------------------------------------

            if ( astrEnvCmdLineArgs.Length == PGM_NAME_ONLY )
                return;

            //  ----------------------------------------------------------------
            //  There is at least one argument. Set the ArgListIsEmpty property,
            //  and parse them.
            //  ----------------------------------------------------------------

            _fArgListIsEmpty = ARG_LIST_HAS_ARGS;
			int intArgPosition = ArrayInfo.ARRAY_INVALID_INDEX;
            _enmArgType = new ArgType [ astrEnvCmdLineArgs.Length - SKIP_PGM_NAME_ARG ];

            foreach ( string strArg in astrEnvCmdLineArgs )
            {   // The first element in the array is the name of the executing program, as read from the shell prompt, shortcut, shell script, or CreateProcess argument list.
				if ( intArgPosition > ArrayInfo.ARRAY_INVALID_INDEX )
                {   // Process remaining arguments.
                    if ( ArgIsSwitch ( strArg ) )
                    {   // The argument has a valid switch prefix.
                        string strSwitchName = GetSwitchInternalName (
                            strArg ,
                            _enmArgMatching );

                        if ( string.IsNullOrEmpty ( strSwitchName ) == false )
                        {   // Switch is followed by a character. Empty strings are ignored.
							if ( _dctValidArgs.ContainsKey ( strSwitchName ) )
                            {   // Character is in list of valid switches.
								_dctValidArgs [ strSwitchName ] = GetSwitchArgValue ( strArg );
                                _enmArgType [ intArgPosition ] = ArgType.Switch;
                                _intValidSwitchesInCmd++;
                            }   // TRUE block (normal), if (base.ContainsKey(strSwitchName))
                            else
                            {   // Mark as invalid in list, and increment the invalid switch count.
                                _enmArgType [ intArgPosition ] = ArgType.InvalidSwitch;
                                _intInvalidSwitchesInCmd++;
                            }   // FALSE block (error), if (base.ContainsKey(strSwitchName))
                        }   // if (string.IsNullOrEmpty(strSwitchName))
                    }   // TRUE block, if ( ArgIsSwitch ( strArg ) )
                    else
                    {   // If the argument isn't a switch, see if it's a named argument.
                        if ( ArgIsNamed ( strArg ) )
                        {
                            //  ------------------------------------------------
                            //  It is, so split it into a two-element array of
                            //  name and value. Ignore the untreated argument in
                            //  the array, and use strArgNameKey.
                            //  ------------------------------------------------

                            string [ ] astrNVP;
                            string strArgNameKey = GetNamedArgName (
                                strArg ,
                                _enmArgMatching ,
                                out astrNVP );

							if ( _dctValidArgs.ContainsKey ( strArgNameKey ) )
                            {   // Argument name is valid. Update its value.
								_dctValidArgs [ strArgNameKey ] = astrNVP [ NVP_VALUE ];
                                _enmArgType [ intArgPosition ] = ArgType.Named;
                                _intValidNmedArgsInCmd++;
                            }   // TRUE block (normal), if ( base.ContainsKey ( strArgNameKey ) )
                            else
                            {   // Mark as invalid in list, and increment the invalid switch count.
                                _enmArgType [ intArgPosition ] = ArgType.InvalidNamed;
                                _intInvalidNamedArgsInCmd++;
                            }   // FALSE block (error), if ( base.ContainsKey ( strArgNameKey ) )
                        }   // TRUE block, if ( ArgIsNamed ( strArg ) )
                        else
                        {   // By default, the argument is defined by its position in the command line.
							if ( _dctValidArgs == null )
								_dctValidArgs = new Dictionary<string , string> ( POSITIONAL_ARGS_COUNT_LIMIT );

							_dctValidArgs.Add ( string.Format (
                                    ARG_KEY_FMT ,
                                    POSITIONAL_ARG_PREFIX ,
                                    ++_intPositionalArgsInCmdLine ) ,			// Count it once only.
                                    strArg );
                            _enmArgType [ intArgPosition ] = ArgType.Positional;
                        }   // FALSE block, if ( ArgIsNamed ( strArg ) )
                    }   // FALSE block, if ( ArgIsSwitch ( strArg ) )
                }   // End if ( intArgPosition > MagicNumbers.ARRAY_FIRST_ELEMENT )

                intArgPosition++;												// This is always incremented.
            }   // foreach (string strArg in Environment.GetCommandLineArgs())
        }   // ParseArgs
		#endregion	// Private Instance Methods


		#region Private Static Methods
		private static bool ArgHasName ( string pstrArg )
        {
            if ( string.IsNullOrEmpty ( pstrArg ) )
                return false;
            else
                if ( pstrArg.IndexOf ( NAME_VALUE_DELIMITER ) > SUBSTRING_NOT_FOUND )
                    return true;
                else
                    return false;
        }   // ArgHasName


        private static bool ArgHasSwitchPrefix ( string pstrArg )
        {
            const string SW_DLM_FWD_SLASH = @"/";
            const string SW_DLM_HYPHEN = @"-";

            if ( string.IsNullOrEmpty ( pstrArg ) )
                return false;
            else
                if ( pstrArg.StartsWith ( SW_DLM_FWD_SLASH ) || pstrArg.StartsWith ( SW_DLM_HYPHEN ) )
                    return true;
                else
                    return false;
        }   // private static bool ArgHasSwitchPrefix method


        private static string GetArgDefaultToUse (
            string pstrSuppliedDefault , 
            bool pfAllowEmptyStringAsDefault )
        {
            if ( pfAllowEmptyStringAsDefault )
                if ( pstrSuppliedDefault == null )
                    return VALUE_NOT_SET;
                else
                    return pstrSuppliedDefault;
            else
                return pstrSuppliedDefault;
        }   // private static string GetArgDefaultToUse method


        private static string GetSwitchDefaultToUse (
            string pstrSuppliedDefault ,
            bool pfAllowEmptyStringAsDefault )
        {
            if ( pfAllowEmptyStringAsDefault )
                if ( pstrSuppliedDefault == null )
					return Core.Properties.Resources.SWITCH_IS_OFF;
                else
                    return pstrSuppliedDefault;
            else
                return pstrSuppliedDefault;
        }   // GetSwitchDefaultToUse


        private static string GetNamedArgKey (
            string pstrArgName ,
            ArgMatching penmArgMatching )
        {
            if ( string.IsNullOrEmpty ( pstrArgName ) )
            {   // Empty string causes graceful failure.
                return SpecialStrings.EMPTY_STRING;
            }   // TRUE (error) case, if ( string.IsNullOrEmpty ( pstrArgName ) )
            else
            {   // Follow the defined argument matching rule.
                switch ( penmArgMatching )
                {
                    case ArgMatching.CaseInsensitive:
                        return string.Format (
                            ARG_KEY_FMT ,
                            NAMED_ARG_PREFIX ,
                            pstrArgName.ToLower ( ) );

                    case ArgMatching.CaseSensitive:
                    default:
                        return string.Format (
                            ARG_KEY_FMT ,
                            NAMED_ARG_PREFIX ,
                            pstrArgName );
                }   // switch ( penmArgMatching )
            }   // FALSE (normal) case, if ( string.IsNullOrEmpty ( pstrArgName ) )
        }   // GetNamedArgKey


        private static string GetNamedArgName (
            string pstrArg ,													// Raw command line argument (in)
            ArgMatching penmArgMatching ,										// Matching rule (in)
            out string [ ] pastrNVP )											// Array containing name and value, saving some work for the parser [out]
        {   // The argument name in the array is raw, and is best ignored by callers. Use the treated one returned by the function.
            pastrNVP = pstrArg.Split (
                CmdLneArgsBasic.s_achrNameValueDelimiter ,
                NVP_STOP_AT_FIRST_DELIMITER );									// Stop after the first delimiter. Subsequent delimiters become part of the argument value.

            switch ( penmArgMatching )
            {
                case ArgMatching.CaseInsensitive:
                    return string.Format (
                        ARG_KEY_FMT ,
                        NAMED_ARG_PREFIX ,
                        pastrNVP [ NVP_NAME ].ToLower ( ) );

                case ArgMatching.CaseSensitive:
                default:
                    return string.Format (
                        ARG_KEY_FMT ,
                        NAMED_ARG_PREFIX ,
                        pastrNVP [ NVP_NAME ] );
            }   // switch ( penmArgMatching )
        }   // GetNamedArgName


        private static string GetSwitchArgValue ( string pstrArg )
        {
            if ( string.IsNullOrEmpty ( pstrArg ) )
				return Core.Properties.Resources.SWITCH_IS_OFF;
            else
                if ( pstrArg.Length > SW_MIN_LEN )
                    return pstrArg.Substring ( SW_ARG_POS );
                else
					return Core.Properties.Resources.SWITCH_IS_ON;
        }   // GetSwitchArgValue


        private static string GetSwitchInternalName (
            char pchrArg ,
            ArgMatching penmArgMatching )
        {
            switch ( penmArgMatching )
            {
                case ArgMatching.CaseInsensitive:
                    return string.Format (
                        ARG_KEY_FMT ,
                        SWITCH_ARG_PREFIX ,
                        char.ToLower ( pchrArg ) );

                case ArgMatching.CaseSensitive:
                default:
                    return string.Format (
                        ARG_KEY_FMT ,
                        SWITCH_ARG_PREFIX ,
                        pchrArg );
            }   // switch ( penmArgMatching )
        }   // GetSwitchInternalName


        private static string GetSwitchInternalName (
            string pstrArg ,
            ArgMatching penmArgMatching )
        {
            if ( pstrArg.Length >= SW_MIN_LEN )
            {   // At a minimum, the argument contains a switch name (character).
                switch ( penmArgMatching )
                {
                    case ArgMatching.CaseInsensitive:
                        return string.Format (
                            ARG_KEY_FMT ,
                            SWITCH_ARG_PREFIX ,
                            pstrArg.Substring (
                                SW_NAME_POS ,
                                SW_NAME_LEN ).ToLower ( ) );

                    case ArgMatching.CaseSensitive:
                    default:
                        return string.Format (
                            ARG_KEY_FMT ,
                            SWITCH_ARG_PREFIX ,
                            pstrArg.Substring (
                                SW_NAME_POS ,
                                SW_NAME_LEN ) );
                }   // switch ( penmArgMatching )
            }   // TRUE block (normal case), if ( pstrArg.Length >= SW_MIN_LEN )
            else
            {   // This argument is an invalid switch, because it consists solely of an indicator.
                return VALUE_NOT_SET;
            }   // FALSE block (degenerate case), if ( pstrArg.Length >= SW_MIN_LEN )
        }   // GetSwitchInternalName
		#endregion	// Private Static Methods
	}   // ParsedCmdArgs class
}   // partial namespace WizardWrx.Core