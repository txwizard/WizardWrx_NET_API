/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         SpecialStrings

    File Name:          SpecialStrings.cs

    Synopsis:           Define a handful of frequently used fixed strings that
                        have precise contents that are constant irrespective of
                        culture, and can be challenging to input correctly when
                        needed.

	Remarks:            This class is one of a constellation of static classes
						that define a wide variety of symbolic constants that I
						use to make my code easier to understand when I need a
						refresher or am about to change it.

						This class implements a subset of the characters defined
                        in WizardWrx.MagicNumbers. Some of those constants,
                        especially those intended mainly for use with arrays and
                        lists, have moved into sibling classes in this library.

						The XML documentation in this class incorporates cross
						references to classes defined in other modules that are
						compiled into the same assembly. A couple of hours has
						yielded the following lessons.

						1)	You can refer to any other class in the assembly, if
							you fully qualify it with respect to the namespace
							declared at the top of the same source module.

						2)	All references are subject to the above rule:

								- Argument types used to disambiguate overloads
								- Method names
								- Argument types used to disambiguate overloads
								- Named constants

							Examples:

								These don't work.

									GetReservedErrorMessage(ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(ExceptionLogger.ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(DLLServices2.ExceptionLogger.ErrorExitOptions)

								This works.

									DLLServices2.ExceptionLogger.GetReservedErrorMessage(DLLServices2.ExceptionLogger.ErrorExitOptions)

							Notwithstanding assertions in the documentation that
							these cross references respect using irectiv4es when
							resolving, it appears that this courtesy may exclude
							references to the current assembly (the one being
							built), which makes sense, upon reflection, since it
							is in a state of flux, and has yet to be persisted.

						3)	See and SeeAlso XML tags can refer to Web pages, but
							the correct keyword is not CREF, but HREF.

    Author:             David A. Gray

    License:            Copyright (C) 2014-2018, David A. Gray. 
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

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2015/06/23 5.5     DAG    Relocate to WizardWrx.DLLServices2 class library, 
                              promote to the root WizardWrx namespace, add
                              special characters that I originally defined in
                              class WizardWrx.SharedUt2.MagicNumbers, and
                              cross reference related constants defined in other
                              classes.

    2015/08/31 5.6     DAG    Add a couple of overlooked special strings.

	2016/05/12 6.1     DAG    Add ERRMSG_SUCCESS_PLACEHOLDER and cross reference
                              related routines in this library and companion
                              ConsoleAppAids2.

	2016/06/04 6.2     DAG    Resolve ambiguous and unreachable cross references
							  tags in the XML documentation.

	2017/02/28 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

						      This module moved into WizardWrx.Common.dll, while
                              remaining a part of the root WizardWrx namespace.

	2017/06/24 7.0     DAG    Cross reference the managed string resources that
                              correspond to string constants, which are under
                              consideration for being marked as deprecated.

	2018/10/07 7.1     DAG    Define SPACE_CHAR for use when only a string will
	                          do, and cross reference the new constant to its
							  antecedent, SpecialCharacters.SPACE_CHAR.

	2019/04/27 7.15    DAG    Define the following single-character strings:

                                   COLON
                                   COMMA
                                   DOUBLE_QUOTE
                                   FULL_STOP
                                   HYPHEN
                                   SEMICOLON
                                   SINGLE_QUOTE
                                   TAB_CHAR
                                   UNDERSCORE_CHAR
    ============================================================================
*/

namespace WizardWrx
{
	/// <summary>
	/// This class defines special purpose strings that are either difficult to
	/// get right in the first place, or display ambiguously in text editors and
	/// printed source code listings.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <remarks>
	/// For ease of access, I promoted the classes that expose only constants to
	/// the root of the WizardWrx namespace.
	/// </remarks>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="MagicNumbers"/>
	/// <seealso cref="SpecialCharacters"/>
	public static class SpecialStrings
	{
		/// <summary>
		/// Use this when you need to remove or replace ampersand literals in a string.
		/// </summary>
		/// <seealso cref="SpecialCharacters.AMPERSAND"/>
		public const string AMPERSAND = "&";

		/// <summary>
		/// Use this when your code requires a string composed of a single AT
		/// character literal, and you want the listing to be crystal clear
		/// about what it is.
		/// </summary>
		/// <seealso cref="SpecialCharacters.AT_CHAR"/>
		public const string AT_CHAR = "@";

		/// <summary>
		/// A URI that ends with a forward slash calls forth this specially
		/// named page.
		/// </summary>
		/// <seealso cref="ASP_APP_START_PAGE_KEY"/>
		/// <seealso cref="ASP_RELATIVE_PATH_BEGIN"/>
		/// <seealso cref="ASP_REL_EXEC_PATH_PREFIX"/>
		public const string ASP_APP_DIR_DEFAULT_START_PAGE = @"default.aspx";

		/// <summary>
		/// Key, in web.config, that contains the name of the application's
		/// start (home) page.
		/// </summary>
		/// <seealso cref="ASP_APP_DIR_DEFAULT_START_PAGE"/>
		/// <seealso cref="ASP_RELATIVE_PATH_BEGIN"/>
		/// <seealso cref="ASP_REL_EXEC_PATH_PREFIX"/>
		public const string ASP_APP_START_PAGE_KEY = @"StartPagePath";

		/// <summary>
		/// Relative path strings returned by the Request object begin with one
		/// of these.
		/// </summary>
		/// <seealso cref="ASP_APP_DIR_DEFAULT_START_PAGE"/>
		/// <seealso cref="ASP_APP_START_PAGE_KEY"/>
		/// <seealso cref="ASP_REL_EXEC_PATH_PREFIX"/>
		public const string ASP_RELATIVE_PATH_BEGIN = @"~/";

		/// <summary>
		/// Relative path strings returned by the Request object begin with one
		/// of these.
		/// </summary>
		/// <seealso cref="ASP_APP_DIR_DEFAULT_START_PAGE"/>
		/// <seealso cref="ASP_APP_START_PAGE_KEY"/>
		/// <seealso cref="ASP_RELATIVE_PATH_BEGIN"/>
		public const string ASP_REL_EXEC_PATH_PREFIX = @"~/";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.COLON"/>
        public const string COLON = ":";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.COMMA"/>
        public const string COMMA = ",";
        
        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.DOUBLE_QUOTE"/>
        public const string DOUBLE_QUOTE = "\"";

        /// <summary>
        /// I like having a way to initialize a constant to the empty string.
        /// </summary>
        /// <seealso cref="ArrayInfo.ARRAY_FIRST_ELEMENT"/>
        /// <seealso cref="ArrayInfo.ARRAY_IS_EMPTY"/>
        /// <seealso cref="ArrayInfo.ARRAY_INVALID_INDEX"/>
        /// <seealso cref="ArrayInfo.ARRAY_SECOND_ELEMENT"/>
        /// <seealso cref="ArrayInfo.INDEX_FROM_ORDINAL"/>
        /// <seealso cref="ArrayInfo.NEXT_INDEX"/>
        /// <seealso cref="ArrayInfo.ORDINAL_FROM_INDEX"/>
        /// <seealso cref="MagicNumbers.EMPTY_STRING_LENGTH"/>
        public const string EMPTY_STRING = @"";

		/// <summary>
		/// Since ErrorExit is never invoked for ERROR_SUCCESS, and the table of
		/// error messages is indexed by status code, this string holds its spot
		/// in the table of error messages, but is never rendered. Hence, it can
		/// be kept out of the managed string resources for applications. This
		/// string is publicly accessible through a static method exported by
		/// this library, ExceptionLogger.GetSpecifiedReservedErrorMessage.
		/// </summary>
		/// <remarks>
		/// A related managed string resource, ERRMSG_SUCCESS, is available for
		/// public consumption; use it in lieu of this string for I18N.
		/// 
		/// Two other strings, ERRMSG_RUNTIME and ERRMSG_INVALID_COMMAND_LINE,
		/// are also defined in this assembly.
		/// </remarks>
		/// <see cref="MagicNumbers.ERROR_SUCCESS"/>
		/// <seealso cref="MagicNumbers.ERROR_RUNTIME"/>
		public const string ERRMSG_SUCCESS_PLACEHOLDER = @"ERROR_SUCCESS Placeholder";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.FULL_STOP"/>
        public const string FULL_STOP = ".";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.HYPHEN"/>
        public const string HYPHEN = @"-";

        /// <summary>
        /// Minimal HTML (and XML) comment prefix string.
        /// </summary>
        public const string HTML_COMMENT_PREFIX = @"<!--";

		/// <summary>
		/// HTML (and XML) comment prefix string, followed by a single space.
		/// </summary>
		public const string HTML_COMMENT_PREFIX_SP = @"<!-- ";

		/// <summary>
		/// Minimal HTML (and XML) comment suffix string.
		/// </summary>
		public const string HTML_COMMENT_SUFFIX = @"-->";

		/// <summary>
		/// HTML (and XML) comment suffix string, preceded by a single space.
		/// </summary>
		public const string HTML_COMMENT_SUFFIX_SP = @" -->";

		/// <summary>
		/// Web Developers are always needing a non-breaking space, for use as
		/// filler, especially in dynamically generated tables.
		/// </summary>
		public const string HTML_NONBREAKING_SPACE = @"&nbsp;";

		/// <summary>
		/// The DNS name of the local loop-back is always "localhost".
		/// </summary>
		public const string LOCALHOST = @"localhost";

		/// <summary>
		/// The local loop-back has the reserved IP address of 127.0.0.1.
		/// </summary>
		public const string LOCALHOST_IP_ADDR = @"127.0.0.1";

		/// <summary>
		/// Use this when you need to remove or replace ampersand literals in a
		/// string.
		/// </summary>
		/// <seealso cref="SpecialCharacters.PERCENT_SIGN"/>
		public const string PERCENT_SIGN = "%";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.SEMICOLON"/>
        public const string SEMICOLON = @";";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.SINGLE_QUOTE"/>
        public const string SINGLE_QUOTE = "'";
 
		/// <summary>
		/// Use this when you need a space character in a context that requires
		/// a string representation.
		/// </summary>
		/// <remarks>
		/// Heretofore, I have made do with the ToString method on the 
		/// like-named character, but it annoyed me whenever I did so because I
		/// wanted th use a real constant.
		/// </remarks>
		/// <seealso cref="SpecialCharacters.SPACE_CHAR"/>
		public const string SPACE_CHAR = @" ";

		/// <summary>
		/// Use this template when you need to either precede or follow a line
		/// of otherwise static text with a newline.
		/// </summary>
		public const string SPACING_TEMPLATE = @"{0}{1}";

		/// <summary>
		/// Use this string as the solitary element of an array of strings to
		/// split a string that contains text from a file of lines delimited by
		/// carriage returns only.
		/// </summary>
		/// <seealso cref="SpecialCharacters.CARRIAGE_RETURN"/>
		/// <seealso cref="SpecialCharacters.LINEFEED"/>
		/// <seealso cref="STRING_SPLIT_LINEFEED"/>
		/// <seealso cref="STRING_SPLIT_NEWLINE"/>
		public const string STRING_SPLIT_CARRIAGE_RETURN = "\r";

		/// <summary>
		/// Use this string as the solitary element of an array of strings to
		/// split a string that contains text from a file of lines delimited by
		/// line feeds only.
		/// </summary>
		/// <seealso cref="SpecialCharacters.CARRIAGE_RETURN"/>
		/// <seealso cref="SpecialCharacters.LINEFEED"/>
		/// <seealso cref="STRING_SPLIT_CARRIAGE_RETURN"/>
		/// <seealso cref="STRING_SPLIT_NEWLINE"/>
		public const string STRING_SPLIT_LINEFEED = "\n";

		/// <summary>
		/// Use this string as the solitary element of an array of strings to
		/// split a string that contains text from a file into an array of
		/// strings, each element of which is a line of text, stripped of its
		/// line ending, if any.
		/// </summary>
		/// <seealso cref="STRING_SPLIT_CARRIAGE_RETURN"/>
		/// <seealso cref="STRING_SPLIT_LINEFEED"/>
		/// <seealso cref="SpecialCharacters.CARRIAGE_RETURN"/>
		/// <seealso cref="SpecialCharacters.LINEFEED"/>
		/// <seealso cref="STRING_SPLIT_CARRIAGE_RETURN"/>
		public const string STRING_SPLIT_NEWLINE = "\r\n";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.TAB_CHAR"/>
        public const string TAB_CHAR = "\t";

        /// <summary>
        /// This is one of many single characters that are frequently needed as
        /// single-character string constants.
        /// </summary>
        /// <seealso cref="SpecialCharacters.UNDERSCORE_CHAR"/>
        public const string UNDERSCORE_CHAR = @"_";
    }	// public sealed class SpecialStrings
}	// partial namespace WizardWrx