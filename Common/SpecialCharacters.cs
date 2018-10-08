/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         SpecialCharacters

    File Name:          SpecialCharacters.cs

    Synopsis:           Define a handful of frequently used characters that can
                        be difficult to correctly differentiate in a source code
                        listing, either in print or in a text editor window.

	Remarks:            This class is one of a constellation of static classes
						that define a wide variety of symbolic constants that I
						use to make my code easier to understand when I need a
						refresher or am about to change it.

						This class implements a subset of the characters defined
                        in WizardWrx.MagicNumbers. Some of those constants,
                        especially those intended mainly for use with arrays and
                        lists, have moved into sibling classes in this library.

    Author:             David A. Gray

    License:            Copyright (C) 2014-2018, David A. Gray 
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

    Created:            Sunday, 14 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/14 1.0     DAG    Initial implementation.

	2015/06/06 5.4     DAG    Break completely free from WizardWrx.SharedUtl2.

	2015/06/20 5.5     DAG    Relocate to WizardWrx class library, 
                              promote to the root WizardWrx namespace, add
                              special characters that I originally defined in
                              class WizardWrx.SharedUt2.MagicNumbers, and
                              cross reference related constants defined in other
                              classes.

    2015/08/31 5.6     DAG    Add a couple of overlooked special characters.

    2016/04/06 6.0     DAG    Add a full-stop character that I discovered was
                              overlooked in the last review.

    2016/06/07 6.3     DAG    Adjust the internal documentation to correct a few
                              inconsistencies uncovered while preparing this
							  library for publication on GetHub.

	2017/02/28 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

						      This module moved into WizardWrx.Common.dll, while
                              remaining a part of the root WizardWrx namespace.
 
	2017/06/29 7.0     DAG    Define the ASTERISK and QUESTION_MARK constants.

	2017/07/15 7.0     DAG    Define the DLM_FORMAT_ITEM_BEGIN, BRACE_LEFT, and
                              BRACE_RIGHT symbolic constants. BRACE_LEFT is the
                              antecedent of DLM_FORMAT_ITEM_BEGIN, and
                              BRACE_RIGHT matches its left handed twin.

	2017/08/26 7.0     DAG    Complete the constellation of parenthetical pairs,
                              adding BRACKET_* and PARENTHESIS_*, along with
                              ENV_STR_DLM, which motivated this edit, and the
                              long overdue HASH_TAG.

	2017/09/03 7.0     DAG    Correct XML documentation errors found during the
                              last otherwise successful build, and define a few
                              more special characters: NOLBREAKING_SPACE,
                              CHECK_MARK_CHAR, LAST_ASCII_CHAR.

	2018/10/07 7.1     DAG    Cross reference the new SpecialStrings.SPACE_CHAR,
	                          and put the out-of-order constants into alphabetic
							  order by name.
    ============================================================================
*/


namespace WizardWrx
{
    /// <summary>
    /// Use these constants when you want or need your listings to be crystal
    /// clear about certain ambiguous literals.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <remarks>
	/// For ease of access, I promoted the classes that expose only constants to
	/// the root of the WizardWrx namespace.
	/// </remarks>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="MagicNumbers"/>
	/// <seealso cref="SpecialStrings"/>
	public static class SpecialCharacters
    {
		/// <summary>
		/// Use this when your code requires a ampersand literal, when you want the
		/// listing to be crystal clear about what it is.
		/// </summary>
		/// <seealso cref="SpecialStrings.AMPERSAND"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char AMPERSAND = '&';

		/// <summary>
		/// Asterisks are everywhere; use this constant to make asterisks that
		/// are intended to be treated as characters unambiguous.
		/// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char ASTERISK = '*';

		/// <summary>
		/// Use this when your code requires the AT character literal, when you
		/// want the listing to be crystal clear about what it is.
		/// </summary>
		/// <seealso cref="AT_SIGN"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char AT_CHAR = '@';

		/// <summary>
		/// Use this in your code to specify a left French brace, also called a
		/// left brace or opening brace, as a character literal.
		/// </summary>
		/// <remarks>
		/// In addition to implementing DLM_FORMAT_ITEM_BEGIN for a specific use
		/// case, I implemented the generic use case and its twin, BRACE_RIGHT.
		/// </remarks>
		/// <seealso cref="DLM_FORMAT_ITEM_BEGIN"/>
		/// <seealso cref="BRACE_RIGHT"/>
		public const char BRACE_LEFT = '{';

		/// <summary>
		/// Use this in your code to specify a right French brace, also called a
		/// right brace or closing brace, as a character literal.
		/// </summary>
		/// <seealso cref="BRACE_LEFT"/>
		public const char BRACE_RIGHT = '}';

		/// <summary>
		/// Use this in your code to specify a left square bracket, also called
		/// an opening bracket, as a character literal.
		/// </summary>
		/// <seealso cref="BRACKET_RIGHT"/>
		/// <seealso cref="BRACE_LEFT"/>
		public const char BRACKET_LEFT = '[';

		/// <summary>
		/// Use this in your code to specify a right square bracket, also called
		/// a closing bracket, as a character literal.
		/// </summary>
		/// <seealso cref="BRACKET_LEFT"/>
		/// <seealso cref="BRACE_RIGHT"/>
		public const char BRACKET_RIGHT = ']';

		/// <summary>
		/// Both ASTERISK_CHAR and ASTERISK resolve to the same character.
		/// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char ASTERISK_CHAR = ASTERISK;

		/// <summary>
		/// Both AT_SIGH and AT_CHAR resolve to the same character.
		/// </summary>
		/// <remarks>
		/// The '@' character has many uses in computing circles, mostly obscure
		/// ones, such as their use in many command line tools to denote that a
		/// specified file is not, itself, the object of interest, but is a list
		/// of files or other entities that are.
		/// </remarks>
		/// <see cref="AT_CHAR"/>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char AT_SIGN = AT_CHAR;

		/// <summary>
		/// Use this character anywhere in your code that requires a bare
		/// carriage return character.
		/// </summary>
		/// <seealso cref="LINEFEED"/>
		/// <seealso cref="SpecialStrings.STRING_SPLIT_CARRIAGE_RETURN"/>
		/// <seealso cref="SpecialStrings.STRING_SPLIT_LINEFEED"/>
		/// <seealso cref="SpecialStrings.STRING_SPLIT_NEWLINE"/>
		public const char CARRIAGE_RETURN = '\r';

		/// <summary>
		/// Numeric character 0, for use in code where it might be mistaken for
		/// a letter O.
		/// </summary>
		/// <seealso cref="CHAR_LC_O"/>
		/// <seealso cref="CHAR_UC_O"/>
		public const char CHAR_NUMERAL_0 = '0';

		/// <summary>
		/// Numeric character 1, for use in code where it might be mistaken for
		/// a lower case l or an upper case I.
		/// </summary>
		/// <seealso cref="CHAR_UC_I"/>
		/// <seealso cref="CHAR_LC_L"/>
		public const char CHAR_NUMERAL_1 = '1';

		/// <summary>
		/// Numeric character 2, for use in code where it might be mistaken for
		/// a letter Z or a numeral 7.
		/// </summary>
		/// <seealso cref="CHAR_LC_Z"/>
		/// <seealso cref="CHAR_UC_Z"/>
		/// <seealso cref="CHAR_NUMERAL_7"/>
		public const char CHAR_NUMERAL_2 = '2';

		/// <summary>
		/// Numeric character 2, for use in code where it might be mistaken for
		/// a letter Z or a numeral 7.
		/// </summary>
		/// <seealso cref="CHAR_LC_Z"/>
		/// <seealso cref="CHAR_UC_Z"/>
		/// <seealso cref="CHAR_NUMERAL_2"/>
		public const char CHAR_NUMERAL_7 = '7';

		/// <summary>
		/// Lower case I, for use in code, where it might be easily mistaken for
		/// a number 1 or a letter L.
		/// </summary>
		public const char CHAR_LC_I = 'i';

		/// <summary>
		/// Upper case I, for use in code, where it might be easily mistaken for
		/// a number 1 or a letter L.
		/// </summary>
		/// <seealso cref="CHAR_LC_L"/>
		/// <seealso cref="CHAR_NUMERAL_1"/>
		public const char CHAR_UC_I = 'I';

		/// <summary>
		/// Lower case L, for use in code, where it might be easily mistaken for
		/// a number 1 or a letter I.
		/// </summary>
		/// <seealso cref="CHAR_UC_I"/>
		/// <seealso cref="CHAR_NUMERAL_1"/>
		public const char CHAR_LC_L = 'l';

		/// <summary>
		/// Upper case L, for use in code, where it might be easily mistaken for
		/// a number 1 or a letter I.
		/// </summary>
		/// <seealso cref="CHAR_LC_L"/>
		/// <seealso cref="CHAR_NUMERAL_1"/>
		public const char CHAR_UC_L = 'L';

		/// <summary>
		/// Lower case O, for use in code where it might be easily mistaken for
		/// a number zero. 
		/// </summary>
		/// <seealso cref="CHAR_LC_O"/>
		/// <seealso cref="CHAR_UC_O"/>
		/// <seealso cref="CHAR_NUMERAL_0"/>
		public const char CHAR_LC_O = 'o';

		/// <summary>
		/// Upper case O, for use in code where it might be easily mistaken for
		/// a number zero.
		/// </summary>
		/// <seealso cref="CHAR_LC_O"/>
		/// <seealso cref="CHAR_UC_O"/>
		/// <seealso cref="CHAR_NUMERAL_0"/>
		public const char CHAR_UC_O = 'O';

		/// <summary>
		/// Lower case Z, for use in code where it might be easily mistaken for
		/// a numeric character 2 or 7.
		/// </summary>
		/// <seealso cref="CHAR_UC_Z"/>
		/// <seealso cref="CHAR_NUMERAL_2"/>
		/// <seealso cref="CHAR_NUMERAL_7"/>
		public const char CHAR_LC_Z = 'z';

		/// <summary>
		/// Upper case Z, for use in code where it might be easily mistaken for
		/// a numeric character 2 or 7.
		/// </summary>
		/// <seealso cref="CHAR_LC_Z"/>
		/// <seealso cref="CHAR_NUMERAL_2"/>
		/// <seealso cref="CHAR_NUMERAL_7"/>
		public const char CHAR_UC_Z = 'Z';

		/// <summary>
		/// The check-mark character prints as such only in selected Windows
		/// fonts.
		/// </summary>
		public const char CHECK_MARK_CHAR = '\xFB';

		/// <summary>
        /// Use this when your code requires a colon literal, when you want the
        /// listing to be crystal clear about what it is.
        /// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char COLON = ':';

		/// <summary>
        /// Use this when your code requires a comma literal, when you want the
        /// listing to be crystal clear about what it is.
        /// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char COMMA = ',';

		/// <summary>
		/// Use this when you code requires  a Left French Brace literal, such
		/// as at the beginning of a Format Item.
		/// </summary>
		/// <remarks>
		/// This constant is copied from WizardWrx.FormatStringEngine.FormatItem,
		/// in which it is called DLM_ITEM_BEGIN, for use independently of that
		/// library, which brings with it a chain of otherwise unused dependent
		/// DLLs.
		/// 
		/// It came to this library to fill a need for finding the first format
		/// item in a format control string, to determine at run time how many
		/// characters precede a format item that spans two or more lines.
		/// </remarks>
		/// <seealso cref="BRACE_LEFT"/>
		/// <seealso cref="BRACE_RIGHT"/>
		public const char DLM_FORMAT_ITEM_BEGIN = BRACE_LEFT;

		/// <summary>
        /// Use this when your code requires a double quotation mark literal, 
        /// when you want the listing to be crystal clear about what it is.
        /// </summary>
		/// <seealso cref="SINGLE_QUOTE"/>
		/// <seealso cref="SPACE_CHAR"/>
		public const char DOUBLE_QUOTE = '"';

		/// <summary>
		/// Environment strings that appear in REG_EXPAND_SZ Registry keys and
		/// elsewhere are enclosed in pairs of this character.
		/// </summary>
		/// <remarks>
		/// Construct a valid environment string substitution token from the
		/// name of an environment string by calling the MakeToken extension
		/// method, which is visible when WizardWrx.Core is imported into your
		/// module.
		/// </remarks>
		/// <see cref="StringTricks.MakeToken(string,string)"/>
		public const char ENV_STR_DLM = PERCENT_SIGN;

		/// <summary>
		/// Use this constant when your code requires a literal equals sign.
		/// </summary>
		/// <seealso cref="HYPHEN"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char EQUALS_SIGN = '=';

		/// <summary>
		/// Use this character to unambiguously denote a period character, for
		/// example, when specifying a delimiter character or appending a full
		/// stop character to a string.
		/// </summary>
		public const char FULL_STOP = '.';

		/// <summary>
		/// Use this constant to specify a hash-tag literal character
		/// </summary>
		public const char HASH_TAG = '#';

		/// <summary>
		/// Literal hyphens are also easily confused in code, especially with
		/// minus signs.
		/// </summary>
		/// <seealso cref="EQUALS_SIGN"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char HYPHEN = '-';

		/// <summary>
		/// The highest integer that can represent any ASCII character is 255,
		/// which is all 8 bits turned ON.
		/// </summary>
		public const char LAST_ASCII_CHAR = '\xFF';

		/// <summary>
		/// Use this character anywhere in your code that requires a bare
		/// linefeed character.
		/// </summary>
		/// <seealso cref="SpecialStrings.STRING_SPLIT_NEWLINE"/>
		/// <seealso cref="SpecialStrings.STRING_SPLIT_CARRIAGE_RETURN"/>
		/// <seealso cref="SpecialStrings.STRING_SPLIT_LINEFEED"/>
		public const char LINEFEED ='\n';

		/// <summary>
		/// The non-breaking space character doesn't print; although the Unicode
		/// encoding treats it as a white space character, most other encodings,
		/// including both Unicode and US-ASCII, do not.
		/// </summary>
		public const char NONBREAKING_SPACE_CHAR = '\xA0';

		/// <summary>
        /// Use this when your code requires a literal null character, and you
        /// want the listing to be crystal clear. This can be especially useful
		/// to distinguish a null character from a null reference.
        /// </summary>
        public const char NULL_CHAR = '\0';

		/// <summary>
		/// Use this in your code to specify a left parenthesis, also called an
		/// opening parenthesis, as a character literal.
		/// </summary>
		/// <seealso cref="PARENTHESIS_RIGHT"/>
		/// <seealso cref="BRACE_LEFT"/>
		/// <seealso cref="BRACKET_LEFT"/>
		public const char PARENTHESIS_LEFT = '(';

		/// <summary>
		/// Use this in your code to specify a right parenthesis, also called a
		/// closing parenthesis, as a character literal.
		/// </summary>
		/// <seealso cref="PARENTHESIS_LEFT"/>
		/// <seealso cref="BRACE_RIGHT"/>
		/// <seealso cref="BRACKET_RIGHT"/>
		public const char PARENTHESIS_RIGHT = ')';

		/// <summary>
		/// Use this when your code requires a ampersand literal, when you want the
		/// listing to be crystal clear about what it is.
		/// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		/// <seealso cref="SpecialStrings.PERCENT_SIGN"/>
		public const char PERCENT_SIGN = '%';

		/// <summary>
		/// How have I got on this long without my faithful field separator?
		/// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char PIPE_CHAR = '|';

		/// <summary>
		/// The question mark is another special character that is frequently
		/// used as an operator; use this to differentiate such use from that of
		/// an operand.
		/// </summary>
		public const char QUESTION_MARK = '?';

        /// <summary>
        /// Use this when your code requires a semicolon literal, when you want
        /// the listing to be crystal clear about what it is.
        /// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char SEMICOLON = ';';

        /// <summary>
        /// Use this when your code requires a single quotation mark literal, 
        /// when you want the listing to be crystal clear about what it is.
        /// </summary>
		/// <seealso cref="DOUBLE_QUOTE"/>
		/// <seealso cref="SPACE_CHAR"/>
        public const char SINGLE_QUOTE = '\x0027';

		/// <summary>
		/// Use this when your code requires a single space when you want the
		/// listing to be crystal clear about what it is.
		/// </summary>
		/// <seealso cref="SpecialStrings.SPACE_CHAR"/>
		/// <seealso cref="DOUBLE_QUOTE"/>
		/// <seealso cref="EQUALS_SIGN"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="SINGLE_QUOTE"/>
		/// <seealso cref="SPACE_CHAR"/>
		/// <seealso cref="TAB_CHAR"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char SPACE_CHAR = ' ';

        /// <summary>
        /// Use this when your code requires a tab literal, when you want the
        /// listing to be crystal clear about what it is.
        /// </summary>
		/// <seealso cref="AMPERSAND"/>
		/// <seealso cref="COLON"/>
		/// <seealso cref="COMMA"/>
		/// <seealso cref="HASH_TAG"/>
		/// <seealso cref="PERCENT_SIGN"/>
		/// <seealso cref="PIPE_CHAR"/>
		/// <seealso cref="SEMICOLON"/>
		/// <seealso cref="UNDERSCORE_CHAR"/>
		public const char TAB_CHAR = '\t';

		/// <summary>
		/// Underscores can be really hard to see in code, both on paper and
		/// on screen.
		/// </summary>
		/// <seealso cref="DOUBLE_QUOTE"/>
		/// <seealso cref="SINGLE_QUOTE"/>
		/// <seealso cref="SPACE_CHAR"/>
		/// <seealso cref="EQUALS_SIGN"/>
		/// <seealso cref="HYPHEN"/>
		/// <seealso cref="TAB_CHAR"/>
		public const char UNDERSCORE_CHAR = '_';
	}   // public static class SpecialCharacters
}   // partial namespace WizardWrx