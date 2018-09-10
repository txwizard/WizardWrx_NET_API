/*
    ============================================================================

    Namespace:          WizardWrx.ConsoleStreams

    Class Name:         ErrorMessagesInColor

    File Name:          ErrorMessagesInColor.cs

    Synopsis:           Expose a combination of static and instance methods that
                        reduce writing error messages onto STDOUT in any
                        supported combination of foreground and background colors
                        to method calls that mirror the Console.Error.WriteLine
                        methods that you already know and use.

    Remarks:            This code is derived from MessagesInColor.cs, version
                        3.4, dated 2014/02/05, in the sense of being a copy and
                        edit of the original, as opposed to the object oriented
                        sense. The resemblance ends at the interface; the
                        implementation is different throughout.

                        I put the remarks into the XML documentation blocks, to
                        make them visible to the Visual Studio editor, and with
                        the goal of generating help text from them.

						Code Analysis warning message CA2122: Do not directly
						expose methods with link demands, arises from my calls
						into unmanaged libraries the hook deeply into the
						Windows API.

    References:         1)	"Using Custom Classes with Application Settings,"
							Richard Carr,
							http://www.blackwasp.co.uk/CustomAppSettings.aspx

    Author:             David A. Gray

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

    Created:            Sunday, 18 May 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/05/18 3.6     DAG    Initial implementation and testing, is class
                              library WizardWrx.ConsoleAppAids2.

    2014/05/20 4.1     DAG    CLASS RELOCATION NOTICE

                              It finally occurred to me that I already have a
                              class that is an ideal candidate to house these
                              classes that were the source of the tight coupling
                              between the WizardWrx.ApplicationHelpers and
                              WizardWrx.ConsoleAppAids2. Moving the tightly
                              coupled classes into this class, which was created
                              to house a single abstract base class decouples
                              both libraries from each other, in exchange fro a
                              common coupling to this class library, yielding a
                              much more robust architecture that will be easier
                              to maintain.

                              I elected to take the code breakge hits, which are
                              limited to a handful of new console programs, most
                              of which are library test stands, in exchange for
                              eliminating some assembly reference conflicts that
                              are an even bigger nightmare, since Visual Studio
                              and the C# compiler both do a nice job of pointing
                              out the places that need to be fixed.

    2014/06/06 5.0     DAG    Correct typographical errors in the IntelliSense
                              documentation that I discovered while chasing down
                              the source of a type conversion exception that was
                              thrown at run time by a class that consumes this
                              class and its converters.

    2014/07/20 5.1     DAG    1) Add a mechanism to allow users to choose their
                                 own default colors, while storing two sets of
                                 default colors, as four read only ConsoleColors
                                 properties.

                              2) Correct an oversight that stranded this class
                                 in the WizardWrx.ApplicationHelpers namespace.
                                 Since this change affects only two other DLLs,
                                 and, at most one user program, I took davantage
                                 of the opportunity to promote the DLLServices2
                                 namespace to the first rank under the overall
                                 WizardWrx namespace.

                              3) Add static methods that return the four static
                                 color properties as pairs of complete color
                                 schemes.

                                    ----------------------------------------------------
                                    To get one of these:    Call this static method.
                                    --------------------    ----------------------------
                                    ErrorMessagesInColor    GetDefaultErrorMessageColors
                                    MessageInColor          GetDefaultMessageColors
                                    ----------------------------------------------------

                                 WizardWrx.ConsoleStreams.dll.config is a regular
                                 application configuration file that travels
                                 with the DLL that exports this class, and keeps
                                 the default color scheme values, which are set
                                 as string representations of the ConsoleColor
                                 (System.ConsoleColor, strictly speaking)
                                 enumeration.

                              4) Swap the method names, so that the instance
                                 methods whose signatures exactly mirror those
                                 of the Console.Write* methods have the same
                                 base method names, while the static methods,
                                 which require two additional arguments to name
                                 the foreground and background colors have names
                                 that make clear that you must specify colors in
                                 any call.

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

    2015/06/05 5.4     DAG    Change the singleton constructors so that they
                              truly succeed at all costs, without reporting any
                              exceptions. The exceptions are caught, and
                              default settings are implemented, so that the
                              constructed object works correctly, if not exactly
                              as desired.

    2015/06/23 5.5     DAG    Correct technical errors in the XML documentation,
                              eliminate an assigned, but otherwise unused local
                              variable, and correct the capitalization of DLL
                              string resource Argname_penmConstructionStage, so
                              that its casing is consistent with that of the
                              other strings.

    2016/04/03 6.0     DAG    Add methods to report their respective redirection
                              states, both of which are wrappers around existing
                              static StandardHandleState and GetStdHandleFQFN
                              methods on the StateManager class.

    2016/06/07 6.3     DAG    Adjust the internal documentation to correct a few
                              inconsistencies uncovered while preparing this
							  library for publication on GetHub.

	2017/02/27 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

						      This module moved into WizardWrx.ConsoleStreams, a
                              new namespace, which is exported by a like named
							  dynamic-link library.

	2017/07/11 7.0     DAG    Override the ToString on the base (object) class.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

/* Added by DAG */

using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;

using WizardWrx.Core;

namespace WizardWrx.ConsoleStreams
{
    /// <summary>
    /// Console.Error.Write and Console.Error.WriteLine methods that write in
    /// living color.
    /// </summary>
    /// <remarks>
    /// There are two identical sets of methods.
    ///
    /// 1) Static methods write text in your choice of foreground and background
    /// colors, then revert the console colors to their initial values (that is,
    /// the values they had when the program loaded).
    ///
    /// 2) Instance methods go a step further, by maintaining a record of the
    /// current colors, so that the colors can progress through a range, without
    /// reverting to the initial colors.
    ///
    /// For each overload of Console.Error.Write, there are corresponding static
    /// and instance methods. The only difference in their signatures is that
    /// these methods specify a foreground color and a background color,
    /// followed by the same arguments that would apply to the corresponding
    /// overload of the Console.Error.Write method.
    /// </remarks>
    /// <seealso cref="MessageInColor"/>
    [TypeConverter(typeof(ErrorMessagesInColorConverter))]
    [SettingsSerializeAs ( SettingsSerializeAs.String )]
    public class ErrorMessagesInColor
    {
        #region Public Enumerations
        /// <summary>
        /// Use this enumeration to specify the desired color scheme to the
        /// GetDefaultErrorMessageColors and GetDefaultMessageColors methods.
        /// </summary>
        public enum ErrorSeverity
        {
            /// <summary>
            /// The error is fatal, and the program has terminated.
            /// </summary>
            Fatal ,

            /// <summary>
            /// The program recovered from the error, and the message is for
            /// your information. You can probably prevent it by some corrective
            /// action.
            /// </summary>
            Recoverable
        }	// ErrorSeverity
        #endregion  // Public Enumerations


        #region Private Static Storage and Initializers
        const string ARGNAME_SEVERITY = @"penmErrorSeverity";

		static DefaultErrorMessageColors s_DLLProperties = null;

        static ConsoleColor s_clrFatalExceptionTextColor;
        static ConsoleColor s_clrFatalExceptionBackgroundColor;

        static ConsoleColor s_clrRecoverableExceptionTextColor;
        static ConsoleColor s_clrRecoverableExceptionBackgroundColor;


		/// <summary>
		/// Call this static method to cause the configuration file linked to
		/// another DLL to be the source from which default properties are read.
		/// 
		/// To have the intended effect, a call to this method must be the first
		/// reference to this class.
		/// </summary>
		/// <param name="pasmLinkedAssembly">
		/// Specify the assembly with which the desired configuration file is
		/// linked.
		/// </param>
		public static void InitializeDefaultPropertiesFromDllConfogurationFile ( System.Reflection.Assembly pasmLinkedAssembly )
		{	
			bool fForceSystemDefault = false;	// Anticipate success by explicitly initializing to False.

			try
			{
				//	------------------------------------------------------------
				//	Unless s_DLLProperties is null, a previous call already read
				//	the configuration file, and repeating it is wasted motion.
				//
				//	The overloaded constructor argument uses a null-coalescing
				//	operator to pass a reference to the assembly that hosts this
				//	class if pasmLinkedAssembly is null.
				//	------------------------------------------------------------

				if ( s_DLLProperties == null )
				{
					s_DLLProperties = new DefaultErrorMessageColors ( pasmLinkedAssembly ?? typeof ( ErrorMessagesInColor ).Assembly );

					if ( s_DLLProperties == null )
					{	// Initialize unless InitializeDefaultPropertiesFromDllConfogurationFile already did it.
						s_DLLProperties = new DefaultErrorMessageColors ( );
					}	// If ( s_DLLProperties == null )

					s_clrFatalExceptionBackgroundColor = s_DLLProperties.FatalExceptionBackgroundColor;
					s_clrFatalExceptionTextColor = s_DLLProperties.FatalExceptionTextColor;
					s_clrRecoverableExceptionBackgroundColor = s_DLLProperties.RecoverableExceptionBackgroundColor;
					s_clrRecoverableExceptionTextColor = s_DLLProperties.RecoverableExceptionTextColor;
				}	// if ( s_DLLProperties == null )
			}   // Outer try block
			catch ( Exception )
			{
				fForceSystemDefault = true;
			}   // Outer catch block

			//  ----------------------------------------------------------------
			//  This constructor is designed to succeed at any cost, even if it
			//	means that it must ignore the user's miscommunicated intent.
			//  ----------------------------------------------------------------

			if ( fForceSystemDefault )
			{   // Setting everything to one color imposes the existing default color scheme.
				s_clrFatalExceptionTextColor = ConsoleColor.Black;
				s_clrFatalExceptionBackgroundColor = ConsoleColor.Black;
				s_clrRecoverableExceptionTextColor = ConsoleColor.Black;
				s_clrRecoverableExceptionBackgroundColor = ConsoleColor.Black;
			}   // if ( fForceSystemDefault )
		}	// InitializeDefaultPropertiesFromDllConfogurationFile
        #endregion  // Private Static Storage and Initializers


        #region Private Instance Storage
        ConsoleColor _clrOrigBackColor;
        ConsoleColor _clrOrigForeColor;

        ConsoleColor _clrSaveBackColor;
        ConsoleColor _clrSaveForeColor;

        ConsoleColor _clrTextBackColor;
        ConsoleColor _clrTextForeColor;
        #endregion  //  Private Instance Storage


        #region Public Constructors
        /// <summary>
        /// Constructing an instance saves the current foreground and background
        /// colors into two pairs of read only ConsoleColor properties. Methods
        /// facilitate changing one or both colors, while retaining the original
        /// colors in the other two ConsoleColor properties, which are never
        /// changed after the class instance comes into being.
        /// </summary>
        /// <remarks>
        /// This method is provided to facilitate construction of a List or
        /// other sortable collection of ErrorMessagesInColor objects, and 
		/// allows for a future version of this class to be exposed to COM.
        /// </remarks>
        public ErrorMessagesInColor ( )
        {
            InitializeInstance ( );
            SaveTextColors (
                _clrOrigForeColor ,
                _clrOrigBackColor );
        }   // public ErrorMessagesInColor constructor (1 of 2)


        /// <summary>
        /// This constructor creates an instance with its foreground and
        /// background colors properties set to the specified ConsoleColor
        /// values, which presumably differ from the initial foreground and
        /// background colors.
        /// </summary>
        /// <param name="pclrTextForeColor">
        /// Specify the ConsoleColor property to use as the text (foreground)
        /// color in generated messages.
        /// </param>
        /// <param name="pclrTextBackColor">
        /// Specify the ConsoleColor property to use as the background color in
        /// generated messages.
        /// </param>
        public ErrorMessagesInColor (
            ConsoleColor pclrTextForeColor ,
            ConsoleColor pclrTextBackColor )
        {
            InitializeInstance ( );
            SaveTextColors (
                pclrTextForeColor ,
                pclrTextBackColor );
        }   // public ErrorMessagesInColor constructor (2 of 2)
        #endregion  // Public Constructors


        #region Public Instance Properties
        /// <summary>
        /// Gets the Console.BackgroundColor that was in force when the instance
        /// was constructed.
        /// </summary>
        public ConsoleColor OriginalBackgroundColor
        {
            get { return _clrOrigBackColor; }
        }   // public ConsoleColor OriginalBackgroundColor Property


        /// <summary>
        /// Gets the Console.ForegroundColor that was in force when the instance
        /// was constructed.
        /// </summary>
        public ConsoleColor OriginalForegroundColor
        {
            get { return _clrOrigForeColor; }
        }   // public ConsoleColor OriginalForegroundColor


        /// <summary>
        /// Gets or sets the Console.BackgroundColor to use for messages
        /// generated by the instance Write and WriteLine methods.
        /// </summary>
        public ConsoleColor MessageBackgroundColor
        {
            get { return _clrTextBackColor; }
            set { _clrTextBackColor = value; }
        }   // public ConsoleColor MessageBackgroundColor


        /// <summary>
        /// Gets or sets the Console.ForegroundColor to use for messages
        /// generated by the instance Write and WriteLine methods.
        /// </summary>
        public ConsoleColor MessageForegroundColor
        {
            get { return _clrTextForeColor; }
            set { _clrTextForeColor = value; }
        }   // public ConsoleColor MessageForegroundColor
        #endregion  // Public Instance Properties


        #region Instance ColorWriteLine (WriteLine) Methods
        /// <summary>
        /// Write the string representation of a bool (Boolean) variable.
        /// </summary>
        /// <param name="value">
        /// Specify the Boolean value to display.
        /// </param>
        public void WriteLine (
            bool value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (1 of 18)


        /// <summary>
        /// Write the string representation of a char (a Unicode character).
        /// </summary>
        /// <param name="value">
        /// Specify the Unicode character to display.
        /// </param>
        public void WriteLine (
            char value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (2 of 18)


        /// <summary>
        /// Write the string representation of a character array.
        /// </summary>
        /// <param name="buffer">
        /// Specify the array of Unicode characters to display.
        /// </param>
        public void WriteLine (
            char [ ] buffer )
        {
            SetMessageColors ( );
            Console.Error.Write ( buffer );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (3 of 18)


        /// <summary>
        /// Write the string representation of a decimal variable.
        /// </summary>
        /// <param name="value">
        /// Specify the decimal value to display.
        /// </param>
        public void WriteLine (
            decimal value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (4 of 18)


        /// <summary>
        /// Write the string representation of a double precision variable.
        /// </summary>
        /// <param name="value">
        /// Specify the double precision value to display.
        /// </param>
        public void WriteLine (
            double value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (5 of 18)


        /// <summary>
        /// Write the string representation of a floating point variable.
        /// </summary>
        /// <param name="value">
        /// Specify the floating point value to display.
        /// </param>
        public void WriteLine (
            float value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (6 of 18)


        /// <summary>
        /// Write the string representation of an integer variable.
        /// </summary>
        /// <param name="value">
        /// Specify the integer value to display.
        /// </param>
        public void WriteLine (
            int value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (7 of 18)


        /// <summary>
        /// Write the string representation of a long integer variable.
        /// </summary>
        /// <param name="value">
        /// Specify the long integer value to display.
        /// </param>
        public void WriteLine (
            long value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (8 of 18)


        /// <summary>
        /// Write the string representation of a generic Object variable.
        /// </summary>
        /// <param name="value">
        /// Specify the object value to display.
        /// </param>
        public void WriteLine (
            object value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (9 of 18)


        /// <summary>
        /// Write a string variable.
        /// </summary>
        /// <param name="value">
        /// Specify the string value to display.
        /// </param>
        public void WriteLine (
            string value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (10 of 18)


        /// <summary>
        /// Write the string representation of a uint (unsigned integer)
        /// variable.
        /// </summary>
        /// <param name="value">
        /// Specify the uint (unsigned integer) value to display.
        /// </param>
        public void WriteLine (
            uint value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (11 of 18)


        /// <summary>
        /// Write the string representation of a ulong (unsigned long integer)
        /// variable.
        /// </summary>
        /// <param name="value">
        /// Specify the ulong (unsigned long integer) value to display.
        /// </param>
        public void WriteLine (
            ulong value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (12 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may include at most
        /// one substitution token.
        /// </param>
        /// <param name="arg0">
        /// Replace the substitution token with the string representation of this
        /// generic object.
        /// </param>
        public void WriteLine (
            string format ,
            object arg0 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (13 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contains
        /// substitution tokens for each object in an array of generic Object
        /// variables.
        /// </param>
        /// <param name="arg">
        /// Substitute elements from this array of generic Object variables into
        /// the format string, replacing tokens with the element whose subscript
        /// is the number within its brackets.
        /// </param>
        public void WriteLine (
            string format ,
            params object [ ] arg )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (14 of 18)


        /// <summary>
        /// Write a formatted message that includes a range of characters taken
        /// from an array of Unicode characters.
        /// </summary>
        /// <param name="buffer">
        /// Extract characters from this array of Unicode characters.
        /// </param>
        /// <param name="index">
        /// Subscript of character to substitute for token {0} in format.
        /// </param>
        /// <param name="count">
        /// Number of characters from buffer to substitute into string, which
        /// must contain at least count - 1 substitution tokens.
        /// </param>
        public void WriteLine (
            char [ ] buffer ,
            int index ,
            int count )
        {
            SetMessageColors ( );
            Console.Error.Write ( buffer , index , count );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (15 of 18)


        /// <summary>
        /// Write a formatted message that includes up to two substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to two
        /// substitution tokens, {0} and {1}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        public void WriteLine (
            string format ,
            object arg0 ,
            object arg1 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 , arg1 );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (16 of 18)


        /// <summary>
        /// Write a formatted message that includes up to three substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 3
        /// substitution tokens, {0}, {1}, and {2}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        public void WriteLine (
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 , arg1 , arg2 );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (17 of 18)


        /// <summary>
        /// Write a formatted message that includes up to four substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 4
        /// substitution tokens, {0}, {1}, {2}, and {3}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        /// <param name="arg3">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {3}.
        /// </param>
        public void WriteLine (
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 ,
            object arg3 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 , arg1 , arg2 , arg3 );
            RestoreMessageColors ( );
            Console.Error.WriteLine ( );
        }   // public void WriteLine (18 of 18)
        #endregion  // Instance WriteLine Methods


        #region Instance ColorWrite (Write) Methods
        /// <summary>
        /// Write the string representation of a bool (Boolean) variable.
        /// </summary>
        /// <param name="value">
        /// Specify the Boolean value to display.
        /// </param>
        public void Write (
            bool value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (1 of 18)


        /// <summary>
        /// Write the string representation of a char (a Unicode character).
        /// </summary>
        /// <param name="value">
        /// Specify the Unicode character to display.
        /// </param>
        public void Write (
            char value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (2 of 18)


        /// <summary>
        /// Write the string representation of a character array.
        /// </summary>
        /// <param name="buffer">
        /// Specify the array of Unicode characters to display.
        /// </param>
        public void Write (
            char [ ] buffer )
        {
            SetMessageColors ( );
            Console.Error.Write ( buffer );
            RestoreMessageColors ( );
        }   // public void Write (3 of 18)


        /// <summary>
        /// Write the string representation of a decimal variable.
        /// </summary>
        /// <param name="value">
        /// Specify the decimal value to display.
        /// </param>
        public void Write (
            decimal value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (4 of 18)


        /// <summary>
        /// Write the string representation of a double precision variable.
        /// </summary>
        /// <param name="value">
        /// Specify the double precision value to display.
        /// </param>
        public void Write (
            double value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (5 of 18)


        /// <summary>
        /// Write the string representation of a floating point variable.
        /// </summary>
        /// <param name="value">
        /// Specify the floating point value to display.
        /// </param>
        public void Write (
            float value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (6 of 18)


        /// <summary>
        /// Write the string representation of an integer variable.
        /// </summary>
        /// <param name="value">
        /// Specify the integer value to display.
        /// </param>
        public void Write (
            int value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (7 of 18)


        /// <summary>
        /// Write the string representation of a long integer variable.
        /// </summary>
        /// <param name="value">
        /// Specify the long integer value to display.
        /// </param>
        public void Write (
            long value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (8 of 18)


        /// <summary>
        /// Write the string representation of a generic Object variable.
        /// </summary>
        /// <param name="value">
        /// Specify the object value to display.
        /// </param>
        public void Write (
            object value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (9 of 18)


        /// <summary>
        /// Write a string variable.
        /// </summary>
        /// <param name="value">
        /// Specify the string value to display.
        /// </param>
        public void Write (
            string value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (10 of 18)


        /// <summary>
        /// Write the string representation of a uint (unsigned integer)
        /// variable.
        /// </summary>
        /// <param name="value">
        /// Specify the uint (unsigned integer) value to display.
        /// </param>
        public void Write (
            uint value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (11 of 18)


        /// <summary>
        /// Write the string representation of a ulong (unsigned long integer)
        /// variable.
        /// </summary>
        /// <param name="value">
        /// Specify the ulong (unsigned long integer) value to display.
        /// </param>
        public void Write (
            ulong value )
        {
            SetMessageColors ( );
            Console.Error.Write ( value );
            RestoreMessageColors ( );
        }   // public void Write (12 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may include at most
        /// one substitution token.
        /// </param>
        /// <param name="arg0">
        /// Replace the substitution token with the string representation of this
        /// generic object.
        /// </param>
        public void Write (
            string format ,
            object arg0 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 );
            RestoreMessageColors ( );
        }   // public void Write (13 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contains
        /// substitution tokens for each object in an array of generic Object
        /// variables.
        /// </param>
        /// <param name="arg">
        /// Substitute elements from this array of generic Object variables into
        /// the format string, replacing tokens with the element whose subscript
        /// is the number within its brackets.
        /// </param>
        public void Write (
            string format ,
            params object [ ] arg )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg );
            RestoreMessageColors ( );
        }   // public void Write (14 of 18)


        /// <summary>
        /// Write a formatted message that includes a range of characters taken
        /// from an array of Unicode characters.
        /// </summary>
        /// <param name="buffer">
        /// Extract characters from this array of Unicode characters.
        /// </param>
        /// <param name="index">
        /// Subscript of character to substitute for token {0} in format.
        /// </param>
        /// <param name="count">
        /// Number of characters from buffer to substitute into string, which
        /// must contain at least count - 1 substitution tokens.
        /// </param>
        public void Write (
            char [ ] buffer ,
            int index ,
            int count )
        {
            SetMessageColors ( );
            Console.Error.Write ( buffer , index , count );
            RestoreMessageColors ( );
        }   // public void Write (15 of 18)


        /// <summary>
        /// Write a formatted message that includes up to two substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to two
        /// substitution tokens, {0} and {1}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        public void Write (
            string format ,
            object arg0 ,
            object arg1 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 , arg1 );
            RestoreMessageColors ( );
        }   // public void Write (16 of 18)


        /// <summary>
        /// Write a formatted message that includes up to three substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 3
        /// substitution tokens, {0}, {1}, and {2}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        public void Write (
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 , arg1 , arg2 );
            RestoreMessageColors ( );
        }   // public void Write (17 of 18)


        /// <summary>
        /// Write a formatted message that includes up to four substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 4
        /// substitution tokens, {0}, {1}, {2}, and {3}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        /// <param name="arg3">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {3}.
        /// </param>
        public void Write (
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 ,
            object arg3 )
        {
            SetMessageColors ( );
            Console.Error.Write ( format , arg0 , arg1 , arg2 , arg3 );
            RestoreMessageColors ( );
        }   // public void Write (18 of 18)
        #endregion  // Instance ColorWriteLine (WriteLine) Methods


		#region Public Object Method Overrides
		/// <summary>
		/// Override the default ToString method inherited from the base class
		/// (object) to display the most significant properties, the text, or
		/// foreground, and background colors set by the constructor, followed
		/// by the fully qualified type name.
		/// </summary>
		/// <returns>
		/// The return value is a string of the following format.
		/// 
		/// {Text = ConsoleColorText, Background = BackroundColor} WizardWrx.ConsoleStreams.ErrorMessagesInColor
		/// </returns>
		/// <remarks>
		/// Though this method could have easily been implemented inline, using
		/// the shared message template, moving the implementation out of line
		/// affords the flexibility to rearrange the display consistently, even
		/// if that requires the properties to be reordered.
		/// </remarks>
		public override string ToString ( )
		{
			return ConsoleColorPropertyDisplay.MessageColorsToString (
				_clrTextForeColor ,							// ConsoleColor pclrText
				_clrTextBackColor ,							// ConsoleColor pclrBackground
				this.GetType ( ) );							// Type ptypMessageType
		}	// public override string ToString
		#endregion // Public Object Method Overrides


        #region Static Properties, All Read/Write
        /// <summary>
        /// Get the color to apply to the text of a fatal exception message.
        ///
        /// The return value is a member of the System.ConsoleColors enumeration
        /// that is intended to be treated as a foreground (text) color.
        /// </summary>
        public static ConsoleColor FatalExceptionTextColor
        {
            get { return s_clrFatalExceptionTextColor; }
            set { s_clrFatalExceptionTextColor = value; }
        }   // public static ConsoleColor FatalExceptionTextColor


        /// <summary>
        /// Get the color to use as the background of a fatal exception message.
        ///
        /// The return value is a member of the System.ConsoleColors enumeration
        /// that is intended to be treated as a background color.
        /// </summary>
        public static ConsoleColor FatalExceptionBackgroundColor
        {
            get { return s_clrFatalExceptionBackgroundColor; }
            set { s_clrFatalExceptionBackgroundColor = value; }
        }   // public static ConsoleColor FatalExceptionBackgroundColor


        /// <summary>
        /// Get the color to apply to the text of a recoverable exception
        /// message.
        ///
        /// The return value is a member of the System.ConsoleColors enumeration
        /// that is intended to be treated as a foreground (text) color.
		/// </summary>
		public static ConsoleColor RecoverableExceptionTextColor
        {
            get { return s_clrRecoverableExceptionTextColor; }
            set { s_clrRecoverableExceptionTextColor = value; }
        }   // public static ConsoleColor RecoverableExceptionTextColor


        /// <summary>
        /// Get the color to use as the background of a recoverable exception
        /// message.
        ///
        /// The return value is a member of the System.ConsoleColors enumeration
        /// that is intended to be treated as a background color.
        /// </summary>
        public static ConsoleColor RecoverableExceptionBackgroundColor
        {
            get { return s_clrRecoverableExceptionBackgroundColor; }
            set { s_clrRecoverableExceptionBackgroundColor = value; }
        }   // public static ConsoleColor RecoverableExceptionBackgroundColor
        #endregion  // Static Properties, All Read/Write


        #region Static WriteLine Methods
        /// <summary>
        /// Write the string representation of a bool (Boolean) variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the Boolean value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            bool value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (1 of 18)


        /// <summary>
        /// Write the string representation of a char (a Unicode character).
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the Unicode character to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            char value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (2 of 18)


        /// <summary>
        /// Write the string representation of a character array.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="buffer">
        /// Specify the array of Unicode characters to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            char [ ] buffer )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( buffer );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (3 of 18)


        /// <summary>
        /// Write the string representation of a decimal variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the decimal value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            decimal value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (4 of 18)


        /// <summary>
        /// Write the string representation of a double precision variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the double precision value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            double value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (5 of 18)


        /// <summary>
        /// Write the string representation of a floating point variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the floating point value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            float value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (6 of 18)


        /// <summary>
        /// Write the string representation of an integer variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the integer value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            int value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (7 of 18)


        /// <summary>
        /// Write the string representation of a long integer variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the long integer value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            long value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (8 of 18)


        /// <summary>
        /// Write the string representation of a generic Object variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the object value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            object value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (9 of 18)


        /// <summary>
        /// Write a string variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the string value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (10 of 18)


        /// <summary>
        /// Write the string representation of a uint (unsigned integer)
        /// variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the uint (unsigned integer) value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            uint value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (11 of 18)


        /// <summary>
        /// Write the string representation of a ulong (unsigned long integer)
        /// variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the ulong (unsigned long integer) value to display.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            ulong value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (12 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may include at most
        /// one substitution token.
        /// </param>
        /// <param name="arg0">
        /// Replace the substitution token with the string representation of this
        /// generic object.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (13 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contains
        /// substitution tokens for each object in an array of generic Object
        /// variables.
        /// </param>
        /// <param name="arg">
        /// Substitute elements from this array of generic Object variables into
        /// the format string, replacing tokens with the element whose subscript
        /// is the number within its brackets.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            params object [ ] arg )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (14 of 18)


        /// <summary>
        /// Write a formatted message that includes a range of characters taken
        /// from an array of Unicode characters.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="buffer">
        /// Extract characters from this array of Unicode characters.
        /// </param>
        /// <param name="index">
        /// Subscript of character to substitute for token {0} in format.
        /// </param>
        /// <param name="count">
        /// Number of characters from buffer to substitute into string, which
        /// must contain at least count - 1 substitution tokens.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            char [ ] buffer ,
            int index ,
            int count )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( buffer , index , count );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (15 of 18)


        /// <summary>
        /// Write a formatted message that includes up to two substitution
        /// tokens.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to two
        /// substitution tokens, {0} and {1}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 ,
            object arg1 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 , arg1 );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (16 of 18)


        /// <summary>
        /// Write a formatted message that includes up to three substitution
        /// tokens.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 3
        /// substitution tokens, {0}, {1}, and {2}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 , arg1 , arg2 );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (17 of 18)


        /// <summary>
        /// Write a formatted message that includes up to four substitution
        /// tokens.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 4
        /// substitution tokens, {0}, {1}, {2}, and {3}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        /// <param name="arg3">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {3}.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 ,
            object arg3 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 , arg1 , arg2 , arg3 );
            Console.ResetColor ( );
            Console.Error.WriteLine ( );
        }   // public static void RGBWriteLine (18 of 18)
        #endregion  // Static WriteLine Methods


        #region Static Write Methods
        /// <summary>
        /// Write the string representation of a bool (Boolean) variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the Boolean value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            bool value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (1 of 18)


        /// <summary>
        /// Write the string representation of a char (a Unicode character).
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the Unicode character to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            char value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (2 of 18)


        /// <summary>
        /// Write the string representation of a character array.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="buffer">
        /// Specify the array of Unicode characters to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            char [ ] buffer )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( buffer );
            Console.ResetColor ( );
        }   // public static void RGBWrite (3 of 18)


        /// <summary>
        /// Write the string representation of a decimal variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the decimal value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            decimal value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (4 of 18)


        /// <summary>
        /// Write the string representation of a double precision variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the double precision value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            double value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (5 of 18)


        /// <summary>
        /// Write the string representation of a floating point variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the floating point value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            float value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (6 of 18)


        /// <summary>
        /// Write the string representation of an integer variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the integer value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            int value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (7 of 18)


        /// <summary>
        /// Write the string representation of a long integer variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the long integer value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            long value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (8 of 18)


        /// <summary>
        /// Write the string representation of a generic Object variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the object value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            object value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (9 of 18)


        /// <summary>
        /// Write a string variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the string value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (10 of 18)


        /// <summary>
        /// Write the string representation of a uint (unsigned integer)
        /// variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the uint (unsigned integer) value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            uint value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (11 of 18)


        /// <summary>
        /// Write the string representation of a ulong (unsigned long integer)
        /// variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="value">
        /// Specify the ulong (unsigned long integer) value to display.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            ulong value )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( value );
            Console.ResetColor ( );
        }   // public static void RGBWrite (12 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may include at most
        /// one substitution token.
        /// </param>
        /// <param name="arg0">
        /// Replace the substitution token with the string representation of this
        /// generic object.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 );
            Console.ResetColor ( );
        }   // public static void RGBWrite (13 of 18)


        /// <summary>
        /// Write a formatted message that includes the string representation of
        /// an generic object variable.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contains
        /// substitution tokens for each object in an array of generic Object
        /// variables.
        /// </param>
        /// <param name="arg">
        /// Substitute elements from this array of generic Object variables into
        /// the format string, replacing tokens with the element whose subscript
        /// is the number within its brackets.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            params object [ ] arg )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg );
            Console.ResetColor ( );
        }   // public static void RGBWrite (14 of 18)


        /// <summary>
        /// Write a formatted message that includes a range of characters taken
        /// from an array of Unicode characters.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="buffer">
        /// Extract characters from this array of Unicode characters.
        /// </param>
        /// <param name="index">
        /// Subscript of character to substitute for token {0} in format.
        /// </param>
        /// <param name="count">
        /// Number of characters from buffer to substitute into string, which
        /// must contain at least count - 1 substitution tokens.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            char [ ] buffer ,
            int index ,
            int count )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( buffer , index , count );
            Console.ResetColor ( );
        }   // public static void RGBWrite (15 of 18)


        /// <summary>
        /// Write a formatted message that includes up to two substitution
        /// tokens.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to two
        /// substitution tokens, {0} and {1}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 ,
            object arg1 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 , arg1 );
            Console.ResetColor ( );
        }   // public static void RGBWrite (16 of 18)


        /// <summary>
        /// Write a formatted message that includes up to three substitution
        /// tokens.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 3
        /// substitution tokens, {0}, {1}, and {2}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 , arg1 , arg2 );
            Console.ResetColor ( );
        }   // public static void RGBWrite (17 of 18)


        /// <summary>
        /// Write a formatted message that includes up to four substitution
        /// tokens.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use for the foreground (text).
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use for the background.
        /// </param>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 4
        /// substitution tokens, {0}, {1}, {2}, and {3}.
        /// </param>
        /// <param name="arg0">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {0}.
        /// </param>
        /// <param name="arg1">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {1}.
        /// </param>
        /// <param name="arg2">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {2}.
        /// </param>
        /// <param name="arg3">
        /// The default string representation of this generic Object variable is
        /// substituted into format for token {3}.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 ,
            object arg1 ,
            object arg2 ,
            object arg3 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Error.Write ( format , arg0 , arg1 , arg2 , arg3 );
            Console.ResetColor ( );
        }   // public static void RGBWrite (18 of 18)
        #endregion  // Static Write Methods


        #region Static Utility Methods
        /// <summary>
        /// Return a new ErrorMessagesInColor object with its text and
        /// background colors initialized from the specified default color pair.
        /// </summary>
        /// <param name="penmErrorSeverity">
        /// Specify a member of the ErrorSeverity enumeration to indicate which
        /// of the two default color schemes is wanted.
        /// </param>
        /// <returns>
        /// The returned ErrorMessagesInColor object is ready for use with the
        /// instance Write and WriteLine methods to display error messages of
        /// the specified type on the STDERR (Standard Error) console stream.
        /// </returns>
        public static ErrorMessagesInColor GetDefaultErrorMessageColors (
            ErrorSeverity penmErrorSeverity )
        {
            switch ( penmErrorSeverity )
            {
                case ErrorSeverity.Fatal:
                    return new ErrorMessagesInColor (
                        s_clrFatalExceptionTextColor ,
                        s_clrFatalExceptionBackgroundColor );
                case ErrorSeverity.Recoverable:
                    return new ErrorMessagesInColor (
                        s_clrRecoverableExceptionTextColor ,
                        s_clrRecoverableExceptionBackgroundColor );
                default:
                    throw new InvalidEnumArgumentException (
                        ARGNAME_SEVERITY ,
                        ( int ) penmErrorSeverity ,
                        typeof ( ErrorSeverity ) );
            }   // public static ErrorMessagesInColor GetDefaultErrorMessageColors
        }   // public static ErrorMessagesInColor


        /// <summary>
        /// Return a new MessageInColor object with its text and background
        /// colors initialized from the specified default color pair.
        /// </summary>
        /// <param name="penmErrorSeverity">
        /// Specify a member of the ErrorSeverity enumeration to indicate which
        /// of the two default color schemes is wanted.
        /// </param>
        /// <returns>
        /// The returned MessageInColor object is ready for use with the
        /// instance Write and WriteLine methods of its class to display
        /// messages of the specified type on the STDOUT (Standard Output)
        /// console stream.
        /// </returns>
        public static MessageInColor GetDefaultMessageColors (
            ErrorSeverity penmErrorSeverity )
        {
            switch ( penmErrorSeverity )
            {
                case ErrorSeverity.Fatal:
                    return new MessageInColor (
                        s_clrFatalExceptionTextColor ,
                        s_clrFatalExceptionBackgroundColor );
                case ErrorSeverity.Recoverable:
                    return new MessageInColor (
                        s_clrRecoverableExceptionTextColor ,
                        s_clrRecoverableExceptionBackgroundColor );
                default:
                    throw new InvalidEnumArgumentException (
                        ARGNAME_SEVERITY ,
                        ( int ) penmErrorSeverity ,
                        typeof ( ErrorSeverity ) );
            }   // switch ( penmErrorSeverity )
        }   // public static MessageInColor GetDefaultMessageColors


        /// <summary>
        /// Simultaneously override the default text and background colors that
        /// are read into four static ConsoleColor properties when the library
        /// initializes. This can also be accomplished by setting the text and
        /// background color properties separately, but this accomplishes the
        /// task with one method call.
        /// </summary>
        /// <param name="penmErrorSeverity">
        /// Specify a member of the ErrorSeverity enumeration to indicate which
        /// of the two default color schemes is wanted.
        /// </param>
        /// <param name="pclrTextForeColor">
        /// Specify the ConsoleColor property to use as the text (foreground)
        /// color in generated messages.
        /// </param>
        /// <param name="pclrTextBackColor">
        /// Specify the ConsoleColor property to use as the background color in
        /// generated messages.
        /// </param>
        public static void SetDefaultErrorMessageColors (
            ErrorSeverity penmErrorSeverity ,
            ConsoleColor pclrTextForeColor ,
            ConsoleColor pclrTextBackColor )
        {
            switch ( penmErrorSeverity )
            {
                case ErrorSeverity.Fatal:
                    s_clrFatalExceptionTextColor = pclrTextForeColor;
                    s_clrFatalExceptionBackgroundColor = pclrTextBackColor;
                    return;
                case ErrorSeverity.Recoverable:
                    s_clrRecoverableExceptionTextColor = pclrTextForeColor;
                    s_clrRecoverableExceptionBackgroundColor = pclrTextBackColor;
                    return;
                default:
                    throw new InvalidEnumArgumentException (
                        ARGNAME_SEVERITY ,
                        ( int ) penmErrorSeverity ,
                        typeof ( ErrorSeverity ) );
            }   // switch ( penmErrorSeverity )
        }   // public static void SetDefaultErrorMessageColors
        #endregion  // Static Utility Methods


        #region Private Instance Methods
        /// <summary>
        /// Use this method to simultaneously restore the background and
        /// foreground colors to the values that they had when the ErrorMessagesInColor
        /// instance was constructed.
        /// </summary>
        /// <remarks>
        /// The current background and foreground colors can be obtained at any
        /// time by querying the corresponding read/write Console property
        /// (BackgroundColor property and ForegroundColor, respectively).
        /// </remarks>
        private void RestoreOrigColors ( )
        {
            Console.BackgroundColor = _clrOrigBackColor;
            _clrTextBackColor = _clrOrigBackColor;

            Console.ForegroundColor = _clrOrigForeColor;
            _clrTextForeColor = _clrOrigBackColor;
        }   // private void RestoreOrigColors


        /// <summary>
        /// The constructors use this method to save the console colors as they
        /// were when the constructor ran.
        /// </summary>
        private void InitializeInstance ( )
        {
			if ( s_DLLProperties == null )
				InitializeDefaultPropertiesFromDllConfogurationFile ( null );

            _clrOrigBackColor = Console.BackgroundColor;
            _clrOrigForeColor = Console.ForegroundColor;
        }   // private void InitializeInstance


        /// <summary>
        /// The constructors use this method to initialize the properties that
        /// keep the selected foreground and background colors for use by the
        /// instance Write and WriteLine methods.
        /// </summary>
        /// <param name="pclrForeColor">
        /// Specify the ConsoleColor to use as the text (foreground) color.
        /// </param>
        /// <param name="pclrBackColor">
        /// Specify the ConsoleColor to use as the background color behind the
        /// text.
        /// </param>
        /// <remarks>
        /// The constructors pass in the same colors that were saved by the
        /// SaveOrigColors method for any unused color.
        /// </remarks>
        private void SaveTextColors (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor )
        {
            _clrTextBackColor = pclrBackColor;
            _clrTextForeColor = pclrForeColor;
        }   // private void SaveTextColors


        /// <summary>
        /// This instance method saves the current Console.BackgroundColor and
        /// Console.ForegroundColor properties, so that they can be subsequently
        /// restored, then sets them to the message colors to the values stored
        /// in the MessageBackgroundColor and MessageForegroundColor properties.
        /// </summary>
        private void SetMessageColors ( )
        {
            _clrSaveBackColor = Console.BackgroundColor;
            _clrSaveForeColor = Console.ForegroundColor;

            Console.BackgroundColor = _clrTextBackColor;
            Console.ForegroundColor = _clrTextForeColor;
        }   // private void SetMessageColors


        /// <summary>
        /// This instance method uses the ConsoleColor values saved by the
        /// SetMessageColors method to restore the colors to the values that
        /// were in use when the instance Write or WriteLine method was called.
        /// </summary>
        private void RestoreMessageColors ( )
        {
            Console.BackgroundColor = _clrSaveBackColor;
            Console.ForegroundColor = _clrSaveForeColor;
        }   // private void RestoreMessageColors
        #endregion  // Private Instance Methods


        #region Private Static Methods
        /// <summary>
        /// Change the foreground and background colors, but make no effort to
        /// retain their old or new values. See Remarks.
        /// </summary>
        /// <param name="pclrNewForeColor">
        /// Specify a member of the ConsoleColor enumeration that will become
        /// the new foreground color, which remains in effect until changed by a
        /// subsequent call to SetMessageColors.
        /// </param>
        /// <param name="pclrNewBackColor">
        /// Specify a member of the ConsoleColor enumeration that will become
        /// the new background color, which remains in effect until changed by a
        /// subsequent call to SetMessageColors.
        /// </param>
        /// <remarks>
        /// The current background and foreground colors can be obtained at any
        /// time by querying the corresponding read/write Console property
        /// (BackgroundColor property and ForegroundColor, respectively).
        /// </remarks>
        private static void SetMessageColors (
            ConsoleColor pclrNewForeColor ,
            ConsoleColor pclrNewBackColor )
        {
            Console.BackgroundColor = pclrNewBackColor;
            Console.ForegroundColor = pclrNewForeColor;
        }   // private static void SetMessageColors
        #endregion  // Private Static Methods
    }   // public class ErrorMessagesInColor


    /// <summary>
    /// Although its scope is public, the only practical use for this class is
    /// to facilitate storage of default or user specified ErrorMessagesInColor
	/// values in application settings files. That being the case, I put it at
	/// the end of the source file that defines that class.
    /// </summary>
    public class ErrorMessagesInColorConverter : TypeConverter
    {
        /// <summary>
        /// Return True if inputs of the specified type can be converted.
        /// </summary>
        /// <param name="pIContext">
        /// This argument provides internal details about the type. Treat it as
        /// a black box.
        /// </param>
        /// <param name="ptypSourceType">
		/// This argument specifies the System.Type to be evaluated. Treat it as
		/// a black box.
        /// </param>
        /// <returns>
        /// This method returns TRUE if ptypSourceType is typeof ( string ). Any
        /// other type returns FALSE.
        /// </returns>
		/// <remarks>
		/// This method and its companions ConvertFrom and ConvertTo are 
		/// delegates, which the runtime engine calls as needed. Hence, the
		/// arguments described above as black boxes are required, although this
		/// implementation ignores them, since it processes string
		/// representations of the System.Console.ConsoleColors enumerated type.
		/// </remarks>
		public override bool CanConvertFrom (
            ITypeDescriptorContext pIContext ,
            Type ptypSourceType )
        {
            return ptypSourceType == typeof ( string );
        }   // public override bool CanConvertFrom


        /// <summary>
        /// Convert from string (the only supported source type) to
        /// ErrorMessagesInColor.
        /// </summary>
        /// <param name="pIContext">
        /// This argument provides internal details about the type. Treat it as
        /// a black box.
        /// </param>
        /// <param name="pCulture">
        /// This argument supplies a reference to the current CultureInfo object
		/// that drives many aspects of text and numeric conversions. Treat it as
		/// a black box.
        /// </param>
        /// <param name="pobjValue">
        /// Specify the source object to be converted. Although the method
        /// signature requires this argument to be cast to Object, the only type
		/// supported is System.string. In any event, treat it as a black box.
        /// </param>
        /// <returns>
        /// Although specified as object to meet the requirements of the base
        /// class, the return value is expected to be an ErrorMessagesInColor
        /// object.
        /// </returns>
		/// <remarks>
		/// This method and its companions CanConvertFrom and ConvertTo are 
		/// delegates, which the runtime engine calls as needed. Hence, the
		/// arguments described above as black boxes are required, although this
		/// implementation ignores them, since it processes string
		/// representations of the System.Console.ConsoleColors enumerated type.
		/// </remarks>
		public override object ConvertFrom (
            ITypeDescriptorContext pIContext ,
            System.Globalization.CultureInfo pCulture ,
            object pobjValue )
        {
            if ( pobjValue is string )
            {
                string [ ] astrTokens = ( ( string ) pobjValue ).Split ( new char [ ] { SpecialCharacters.COMMA } );
                ErrorMessagesInColor MsgColors = new ErrorMessagesInColor (
					( ConsoleColor ) Enum.Parse (
						typeof ( ConsoleColor ) ,
						astrTokens [ 0 ] ) ,
					astrTokens.Length > 1 ?
						( ConsoleColor ) Enum.Parse (
							typeof ( ConsoleColor ) ,
							astrTokens [ 1 ] ) :
						Console.BackgroundColor );
				return MsgColors;							 // The normal execution path ends here.
            }   // if ( pobjValue is string )

            return base.ConvertFrom (
                pIContext ,
                pCulture ,
                pobjValue );
        }   // public override object ConvertFrom


        /// <summary>
        /// Given an ErrorMessagesInColor object, return a string representation
		/// that is suitable for storage in a standard application settings file.
        /// </summary>
        /// <param name="pIContext">
        /// This argument provides internal details about the type. Treat it as
        /// a black box.
        /// </param>
        /// <param name="pCulture">
        /// This argument supplies a reference to the current CultureInfo object
		/// that drives many aspects of text and numeric conversions. Treat it as
		/// a black box.
        /// </param>
        /// <param name="pobjValue">
        /// Although the method signature calls for an generic System.Object,
        /// this argument must actually be an ErrorMessagesInColor object.
        /// </param>
        /// <param name="pDestType">
        /// The only valid value for this argument is typeof ( string ). The
        /// specification type is dictated by the signature of the ConvertTo
        /// method in the base class.
        /// </param>
        /// <returns>
        /// Although specified as object to meet the requirements of the base
        /// class, the return value is expected to be a System.string.
        /// </returns>
		/// <remarks>
		/// This method and its companions CanConvertFrom and ConvertFrom are 
		/// delegates, which the runtime engine calls as needed. Hence, the
		/// arguments described above as black boxes are required, although this
		/// implementation ignores them, since it processes string
		/// representations of the System.Console.ConsoleColors enumerated type.
		/// </remarks>
        public override object ConvertTo (
            ITypeDescriptorContext pIContext ,
            System.Globalization.CultureInfo pCulture ,
            object pobjValue ,
            Type pDestType )
        {
            const string TEMPLATE = @"{0},{1}";

            if ( pDestType == typeof ( string ) )
            {
                ErrorMessagesInColor MsgColors = pobjValue as ErrorMessagesInColor;
                return string.Format (
                    TEMPLATE ,
                    MsgColors.MessageForegroundColor ,
                    MsgColors.MessageBackgroundColor );		// The normal execution path ends here.
            }   // if ( pDestType == typeof ( string ) )

            return base.ConvertTo (
                pIContext ,
                pCulture ,
                pobjValue ,
                pDestType );
        }   // public override object ConvertTo
    }   // public class ErrorMessagesInColorConverter : TypeConverter
}   // partial namespace WizardWrx.ConsoleStreams