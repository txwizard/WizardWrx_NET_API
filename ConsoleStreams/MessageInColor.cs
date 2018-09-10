/*
    ============================================================================

    Namespace:          WizardWrx.ConsoleStreams

    Class Name:         MessageInColor

    File Name:          MessageInColor.cs

    Synopsis:           Expose a combination of static and instance methods that
                        reduce writing console messages in any supported
                        combination of foreground and background colors to
                        method calls that mirror the Console.Write and
						Console.WriteLine methods that you already know and use.

    Remarks:            I put the remarks into the XML documentation blocks, to
                        make them visible to the Visual Studio editor, and with
                        the goal of generating help text from them in mind.

    References:         "Using Custom Classes with Application Settings,"
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

    Created:            Monday, 06 January 2014 - Tuesday, 04 February 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/02/04 3.2     DAG    Initial implementation.

    2014/02/05 3.4     DAG    Make this class serializable as a string, to
                              simplify saving its attributes into application or
                              user settings.

    2014/05/21 4.1     DAG    CLASS RELOCATION NOTICE

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

                              I elected to take the code breakage hits, which
							  are limited to a handful of new console programs,
                              mostly library test stands, in exchange for
                              eliminating some assembly reference conflicts that
                              are an even bigger nightmare, since Visual Studio
                              and the C# compiler both do a nice job of pointing
                              out the places that need to be fixed.

    2014/06/24 5.1     DAG    1) Correct an oversight that left this class in
                                 the old WizardWrx.ApplicationHelpers namespace.
                                 Since this change affects only two other DLLs
                                 and, at most, one user program, I took the
                                 opportunity to promote the DLLServices2
                                 namespace to the first rank under the overall
                                 WizardWrx namespace.

                              2) Swap the method names, so that the instance
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

	2015/06/21 5.5     DAG    Define static methods to determine whether the
                              Standard Output, Standard Error, or Standard input
                              stream is redirected.

									IsStdErrRedirected returns true if Standard
                                                       Error is redirected.
									IsStdInpRedirected returns true if Standard
                                                       Error is redirected.
									IsStdOutRedirected returns true if Standard
                                                       Output is redirected.

                              These methods apply something that I discovered
                              while experimenting with the PauseForPictures
                              routine in the test stand program. Since it can
                              leverage this discovery to suppress stops when
                              standard output is redirected, the third method
                              listed above, IsStdOutRedirected, stays.

    2016/03/29 6.0     DAG    Add methods to report their respective redirection
                              states, both of which are wrappers around existing
                              static StandardHandleState and GetStdHandleFQFN
                              methods on the StateManager class.

	2016/04/06 6.0     DAG    Move the redirection testers to StateManager.

	2017/03/29 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

						      This module moved into WizardWrx.ConsoleStreams, a
                              new namespace, which is exported by a like named
							  dynamic-link library.

	2017/07/11 7.0     DAG    Override the ToString on the base (object) class.

	2018/09/09 7.0     DAG    Correct erroneous reference to a retired class,
	                          EnhancedIOException, and the NewtonSoft NuGet
							  package that it used, and simplify handling of the
							  unexpected exception.
    ============================================================================
*/

#define SUPPRESS_TRACE

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

/* Added by DAG */

using System.Configuration;
using System.ComponentModel;


namespace WizardWrx.ConsoleStreams
{
    /// <summary>
    /// Console.Write and Console.WriteLine methods that write in living color.
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
    /// For each overload of Console.Write, there are corresponding static and
    /// instance methods. The only difference in their signatures is that these
    /// methods specify a foreground color and a background color, followed by
    /// the same arguments that would apply to the corresponding overload of the
    /// Console.Write method.
    /// </remarks>
    /// <seealso cref="ErrorMessagesInColor"/>
	/// <seealso cref="WizardWrx.Core.PropertyDefaults"/>
	[TypeConverter ( typeof ( MessageInColorConverter ) )]
    [SettingsSerializeAs ( SettingsSerializeAs.String )]
    public class MessageInColor
    {
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
        /// other sortable collection of MessageInColor objects, and allows for
        /// a future version of this class to be exposed to COM.
        /// </remarks>
        public MessageInColor ( )
        {
            SaveOrigColors ( );
            SaveTextColors (
                _clrOrigForeColor ,
                _clrOrigBackColor );
        }   // public MessageInColor constructor (1 of 2)


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
        public MessageInColor (
            ConsoleColor pclrTextForeColor ,
            ConsoleColor pclrTextBackColor )
        {
            SaveOrigColors ( );
            SaveTextColors (
                pclrTextForeColor ,
                pclrTextBackColor );
        }   // public MessageInColor constructor (2 of 2)
        #endregion  // Public Constructors


        #region Public Properties
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
        #endregion  // Public Properties


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
		/// {Text = ConsoleColorText, Background = BackroundColor} WizardWrx.ConsoleStreams.MessageInColor
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( buffer );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
        /// Replace the substition token with the string representation of this
        /// generic object.
        /// </param>
        public void WriteLine (
            string format ,
            object arg0 )
        {
            SetMessageColors ( );
            Console.Write ( format , arg0 );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( format , arg );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( buffer , index , count );
            RestoreMessageColors ( );
            Console.WriteLine ( );
        }   // public void WriteLine (15 of 18)


        /// <summary>
        /// Write a formatted message that includes up to two substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to two
        /// substition tokens, {0} and {1}.
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
            Console.Write ( format , arg0 , arg1 );
            RestoreMessageColors ( );
            Console.WriteLine ( );
        }   // public void WriteLine (16 of 18)


        /// <summary>
        /// Write a formatted message that includes up to three substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 3
        /// substition tokens, {0}, {1}, and {2}.
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
            Console.Write ( format , arg0 , arg1 , arg2 );
            RestoreMessageColors ( );
            Console.WriteLine ( );
        }   // public void WriteLine (17 of 18)


        /// <summary>
        /// Write a formatted message that includes up to four substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 4
        /// substition tokens, {0}, {1}, {2}, and {3}.
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
            Console.Write ( format , arg0 , arg1 , arg2 , arg3 );
            RestoreMessageColors ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( buffer );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
        /// Replace the substition token with the string representation of this
        /// generic object.
        /// </param>
        public void Write (
            string format ,
            object arg0 )
        {
            SetMessageColors ( );
            Console.Write ( format , arg0 );
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
            Console.Write ( format , arg );
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
            Console.Write ( buffer , index , count );
            RestoreMessageColors ( );
        }   // public void Write (15 of 18)


        /// <summary>
        /// Write a formatted message that includes up to two substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to two
        /// substition tokens, {0} and {1}.
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
            Console.Write ( format , arg0 , arg1 );
            RestoreMessageColors ( );
        }   // public void Write (16 of 18)


        /// <summary>
        /// Write a formatted message that includes up to three substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 3
        /// substition tokens, {0}, {1}, and {2}.
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
            Console.Write ( format , arg0 , arg1 , arg2 );
            RestoreMessageColors ( );
        }   // public void Write (17 of 18)


        /// <summary>
        /// Write a formatted message that includes up to four substitution
        /// tokens.
        /// </summary>
        /// <param name="format">
        /// Use this string as the message template, which may contain up to 4
        /// substition tokens, {0}, {1}, {2}, and {3}.
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
            Console.Write ( format , arg0 , arg1 , arg2 , arg3 );
            RestoreMessageColors ( );
        }   // public void Write (18 of 18)
        #endregion  // Instance ColorWriteLine (WriteLine) Methods


		#region Public Static Methods
		/// <summary>
		/// Use this method as a non-throwing replacement for Console.Clear,
		/// which throws an System.IO.IOException exception if the standard
		/// output stream is redirected. This catches that exception and eats it
		/// unless the user enables logging of all exceptions, typically in a
		/// testing scenario.
		/// </summary>
		/// <remarks>
		/// Comparing the HResult to a local constant, E_HANDLE, means that the
		/// error test works correctly in any locale.
		///
		/// Thankfully, Microsoft came to their senses, and made the HResult
		/// visible in later frameworks. Meanwhile, I've found a workaround
		/// that should do the job in code that targets .NET Framework 3.5.
		/// </remarks>
		public static void SafeConsoleClear ( )
		{
			const int E_HANDLE = -2147024890;   // 0x80070006, per WinError.h.

			try
			{
				#if DEBUG
					System.Diagnostics.Debugger.Launch ( );
				#endif  // #if DEBUG

				Console.Clear ( );
			}
			catch ( IOException exIO )
			{   // Use Marshal.GetHRForException() in System.Runtime.InteropServices.
				// Source:	 "Getting the Exceptions HResult value in .net 4 or earlier"
				//			 https://www.experts-exchange.com/questions/28620045/Getting-the-Exceptions-HResult-value-in-net-4-or-earlier.html
				// See also: "Marshal.GetHRForException Method (Exception)"
				//			 https://msdn.microsoft.com/en-us/library/system.runtime.interopservices.marshal.gethrforexception(v=vs.110).aspx

				int intHResult = System.Runtime.InteropServices.Marshal.GetHRForException ( exIO );

				if ( intHResult != E_HANDLE )
				{   // Report the exception, unless its HRESULT is E_HANDLE. In that case, eat it.
					string strMsg = string.Format (
						Properties.Resources.ERRMSG_UNEXPECTED_SAFE_CLEAR_SCREEN_EXCEPTION ,
						new string [ ]
						{
							MethodBase.GetCurrentMethod ( ).Name ,
							NumericFormats.FormatStatusCode ( intHResult ) ,
							exIO.Message
						} );
				}	// if ( System.Runtime.InteropServices.Marshal.GetHRForException ( exIO ) != E_HANDLE )
			}   // catch ( System.IO.IOException exIO )
		}   // public static void SafeConsoleClear
		#endregion  // Public Static Methods


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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( buffer );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
        /// Replace the substition token with the string representation of this
        /// generic object.
        /// </param>
        public static void RGBWriteLine (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Write ( format , arg0 );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( format , arg );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( buffer , index , count );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
        /// substition tokens, {0} and {1}.
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
            Console.Write ( format , arg0 , arg1 );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
        /// substition tokens, {0}, {1}, and {2}.
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
            Console.Write ( format , arg0 , arg1 , arg2 );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
        /// substition tokens, {0}, {1}, {2}, and {3}.
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
            Console.Write ( format , arg0 , arg1 , arg2 , arg3 );
            Console.ResetColor ( );
            Console.WriteLine ( );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( buffer );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
            Console.Write ( value );
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
        /// Replace the substition token with the string representation of this
        /// generic object.
        /// </param>
        public static void RGBWrite (
            ConsoleColor pclrForeColor ,
            ConsoleColor pclrBackColor ,
            string format ,
            object arg0 )
        {
            SetMessageColors ( pclrForeColor , pclrBackColor );
            Console.Write ( format , arg0 );
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
            Console.Write ( format , arg );
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
            Console.Write ( buffer , index , count );
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
        /// substition tokens, {0} and {1}.
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
            Console.Write ( format , arg0 , arg1 );
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
        /// substition tokens, {0}, {1}, and {2}.
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
            Console.Write ( format , arg0 , arg1 , arg2 );
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
        /// substition tokens, {0}, {1}, {2}, and {3}.
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
            Console.Write ( format , arg0 , arg1 , arg2 , arg3 );
            Console.ResetColor ( );
        }   // public static void RGBWrite (18 of 18)
		#endregion  // Static Write Methods


		#region Private Instance Methods
		/// <summary>
        /// Use this method to simultaneously restore the background and
        /// foreground colors to the values that they had when the MessageInColor
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
        private void SaveOrigColors ( )
        {
            _clrOrigBackColor = Console.BackgroundColor;
            _clrOrigForeColor = Console.ForegroundColor;
        }   // private void SaveOrigColors


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
        }   // SetMessageColors
		#endregion  // Private Static Methods
    }   // public class MessageInColor


    /// <summary>
    /// Although its scope is public, the only practical use for this class is
    /// to facilitate storage of default or user specified MessageInColor values
    /// in application settings files.
    /// </summary>
    public class MessageInColorConverter : TypeConverter
    {
        /// <summary>
        /// Return True if inputs of the specified type can be converted.
        /// </summary>
        /// <param name="pIContext">
        /// This argument provides internal details about the type. Treat it as
        /// a black box.
        /// </param>
        /// <param name="ptypSourceType">
        /// This argument specifies the System.Type to be evaluated.
        /// </param>
        /// <returns>
        /// This method returns TRUE if ptypSourceType is typeof ( string ). Any
        /// other type returns FALSE.
        /// </returns>
        public override bool CanConvertFrom (
            ITypeDescriptorContext pIContext ,
            Type ptypSourceType )
        {
            return ptypSourceType == typeof ( string );
        }   // CanConvertFrom


        /// <summary>
        /// Convert from string (the only supported source type) to
        /// MessageInColor.
        /// </summary>
        /// <param name="pIContext">
        /// This argument provides internal details about the type. Treat it as
        /// a black box.
        /// </param>
        /// <param name="pCulture">
        /// This argument supplies a reference to the current CultureInfo object
        /// that drives many aspects of text and numeric conversions.
        /// </param>
        /// <param name="pobjValue">
        /// Specify the source object to be converted. Although the method
        /// signature requires this argument to be cast to Object, the only type
        /// supported is System.string.
        /// </param>
        /// <returns>
        /// Although specified as object to meet the requirements of the base
        /// class, the actual return value is expected to be a MessageInColors
        /// object.
        /// </returns>
        public override object ConvertFrom (
            ITypeDescriptorContext pIContext ,
            System.Globalization.CultureInfo pCulture ,
            object pobjValue )
        {
            if ( pobjValue is string )
            {
                string [ ] astrTokens = ( ( string ) pobjValue ).Split ( new char [ ] { SpecialCharacters.COMMA } );
                MessageInColor MsgColors = new MessageInColor (
                   ( ConsoleColor ) Enum.Parse ( typeof ( ConsoleColor ) , astrTokens [ 0 ] ) ,
                    astrTokens.Length > 1 ? ( ConsoleColor ) Enum.Parse ( typeof ( ConsoleColor ) , astrTokens [ 1 ] ) : Console.BackgroundColor );
                return MsgColors;               // Execution is anticipated to end here.
            }   // if ( pobjValue is string )

            return base.ConvertFrom (
                pIContext ,
                pCulture ,
                pobjValue );
        }   // ConvertFrom


        /// <summary>
        /// Given a MessageInColors object, return a string representation that
        /// is suitable for storage in a standard application settings file.
        /// </summary>
        /// <param name="pIContext">
        /// This argument provides internal details about the type. Treat it as
        /// a black box.
        /// </param>
        /// <param name="pCulture">
        /// This argument supplies a reference to the current CultureInfo object
        /// that drives many aspects of text and numeric conversions.
        /// </param>
        /// <param name="pobjValue">
        /// Although the method signature calls for an generic System.Object,
        /// this argument must actually be a MessageInColors object.
        /// </param>
        /// <param name="pDestType">
        /// The only valid value for this argument is typeof ( string ). The
        /// specification type is dictated by the signature of the ConvertTo
        /// method in the base class.
        /// </param>
        /// <returns>
        /// Although specified as object to meet the requirements of the base
        /// class, the actual return value is expected to be a System.string.
        /// </returns>
        public override object ConvertTo (
            ITypeDescriptorContext pIContext ,
            System.Globalization.CultureInfo pCulture ,
            object pobjValue ,
            Type pDestType )
        {
            const string TEMPLATE = @"{0},{1}";

            if ( pDestType == typeof ( string ) )
            {
                MessageInColor MsgColors = pobjValue as MessageInColor;
                return string.Format (
                    TEMPLATE ,
                    MsgColors.MessageForegroundColor ,
                    MsgColors.MessageBackgroundColor ); // Execution is anticipated to end here.
            }   // if ( pDestType == typeof ( string ) )

            return base.ConvertTo (
                pIContext ,
                pCulture ,
                pobjValue ,
                pDestType );
        }   // ConvertTo
    }   // public class MessageInColorConverter : TypeConverter
}   // partial namespace WizardWrx.ConsoleStreams