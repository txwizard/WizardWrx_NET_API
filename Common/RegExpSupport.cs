/*
    ============================================================================
 	
	Namespace Name:		WizardWrx

	Class Name:			RegExpSupport

	File Name:			RegExpSupport.cs

	Synopsis:			This class exposes static helper methods to simplify
						working with regular expressions.

	Remarks:			Beginning with version 2.46 of this class, the build and
						revision numbers are controlled by the build engine.

	Author:				David A. Gray

    License:            Copyright (C) 2015-2017, David A. Gray. All rights reserved.

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

    Reference:          "Using a Regular Expression to Match HTML,"
                        http://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Synopsis
    ---------- ------- --- --------------------------------------------------
    2008/03/24 1.0.9.0 DAG Initial build, tested as part of developing Web form
                           response page, thank_you.aspx, in the My Place
                           Massage Web application.

    2008/03/27 2,0,0,0 DAG Move to SharedUtl2 library, and from WizardWrx.WebApp
                           namespace to its parent, WizardWrx.

    2008/05/17 2.0.6.1 DAG Add constants for more of the commonly used
                           metacharacters: MATCH_STRING_START, MATCH_STRING_END.

    2008/05/21 2.0.6.2 DAG Add an integer constant, REGEXP_FIRST_MATCH, for
                           returning the first match from the MatchCollection
                           property of a Regular Expression object.

    2008/05/21 2.0.6.3 DAG Simplify all the methods which initially used
                           StringBuilder objects to use the static String.Format
                           method.

    2009/01/31 2.2.0.3 DAG 1)   Add constants for matching HTML and XML tags and
                                tag bodies, and method, MatchArbitraryHtmlTag,
                                to return an expression for matching any HTML or
                                XML tag.

                            2)  Mark the entire class as Sealed, so that it
                                cannot be inherited. Since everything in it is
                                static, this is probably redundant.

    2009/02/01 2.3.0.0 DAG The previous update should have been classified as a
                           Minor Version, instead of simply a Revision. In any
                           case, today's update is so marked, and includes more
                           changes, including match expressions and methods for
                           extracting collections of arbitrary HTML or XML tags.

                           The new match expression is FRIEDL_HTML_TAG_MATCH.

    2010/03/27 2.48 DAG    Add MATCH_DTM_ match expressions, all of which were
                           proven in the course of developing a more complex
                           regular expression, REGEX_MATCH_TICKS, which is part
                           of module, Program.cs of TimeStampLogAnalyzer.exe.

    2010/04/04 2.51 DAG    Change the access modifier from sealed to static,
                           which implies sealed, but also signals the C#
                           compiler to flag creation of instance variables as
                           fatal errors.

	2015/06/20 5.5  DAG    Relocate to WizardWrx.DLLServices2 namespace and
                           class library.

	2016/04/10 6.0  DAG    Scan for typographical errors flagged by the spelling
                           checker add-in, and correct what I find, update the
                           formatting and marking of blocks, and add my BSD
                           license.

    2016/06/07 6.3     DAG Convert the reference to the Regular Expressions
						   Library Web site into a link that makes it accessible
                           from the IntelliSense help.

	2017/03/30 7.0     DAG Break this library apart, so that smaller subsets of
	                       classes can be consumed independently.

						   This module moved into WizardWrx.Common.dll, a new
                           dynamic-link library, but stays in the WizardWrx
                           root namespace.

	2017/06/29 7.0     DAG Correct a doubled word noted while in search of a
                           defined constant that returns one asterisk.

	2017/07/16 7.0     DAG Replace references to string.empty, which is not a
                           true constant, with SpecialStrings.EMPTY_STRING,
                           which is one.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WizardWrx
{
    /// <summary>
    /// Constants, some built from others by static methods, to expedite common
    /// tasks that use regular expressions.
    /// </summary>
    /// <remarks>
    /// Reference: RegExLib.com Regular Expression Cheat Sheet (.NET), at the
	/// cross reference cited below.
    /// </remarks>
	/// <see href="http://regexlib.com/CheatSheet.aspx"/>
	/// <see href="http://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx"/>
    public static class RegExpSupport
    {
        #region Character Constants
        /// <summary>
        /// Like the binary Logical OR operator in a logical expression, this
        /// character says "match either the character on its left OR the
        /// character on its right.
        ///
        /// Regular expressions may contain many alternations, forming a group
        /// that behaves commutatively.
        /// </summary>
        public const char MATCH_ALTERNATION = '|';


        /// <summary>
        /// Preceding another meta-character, one of these tells the Engine to
        /// treat the meta character as a literal.
        ///
        /// Preceding certain other characters, one of these signals a special,
        /// non-printing character. For example, preceding a lower case a, it
        /// signifies an Alarm (Bell). More commonly, however, before a lower
        /// case t, this character signifies a Tab, before a lower case n means
        /// a Newline, and a lower case r denotes a Carriage Return.
        ///
        /// N. B. A Newline in the .NET RegExp Engine and in the Perl RegExp
        /// Engine are two different things.
        /// </summary>
        public const char MATCH_ESCAPE = '\\';


        /// <summary>
        /// Define the start of a group. This is the same as a subexpression in
        /// Perl.
        /// </summary>
        public const char MATCH_GROUP_BEGIN = '(';


        /// <summary>
        /// Define the end of a group. This is the same as a subexpression in
        /// Perl.
        /// </summary>
        public const char MATCH_GROUP_END = ')';


        /// <summary>
        /// Match zero or more of the previous character or expression.
        /// </summary>
        public const char MATCH_MULTIPLE_PREVIOUS_CHAR = '*';


        /// <summary>
        /// Match one or more of of the previous character or expression.
        /// </summary>
        public const char MATCH_ONE_OR_MORE_PREVIOUS_CHAR = '+';


        /// <summary>
        /// Append to a greedy match to make it match the fewest possible characters.
        /// </summary>
        public const char MATCH_SHORTEST = '?';


        /// <summary>
        /// Match start of line, absent the String modifier, which changes its
        /// meaning to match start of the entire String.
        /// </summary>
        public const char MATCH_STRING_START = '^';


        /// <summary>
        /// Match end of line, absent the String modifier, which changes its
        /// meaning to match end of the entire String.
        /// </summary>
        public const char MATCH_STRING_END = '$';


        /// <summary>
        /// Match one of any character, except a Newline (absent the String
        /// modifier, which adds the Newline to the list of matched characters.
        ///
        /// Use MATCH_MULTIPLE_PREVIOUS_CHAR to extend the match to a string of
        /// the same character.
        ///
        /// Use MATCH_SHORTEST, following this character, followed by
        /// MATCH_MULTIPLE_PREVIOUS_CHAR, to limit the match.
        /// </summary>
        public const char MATCH_WILDCARD_CHAR = '.';
		#endregion	// Character Constants


		#region Constant Strings
		/// <summary>
        /// Represents a Carriage Return (CR in Windows text) in a Regular Expression
        /// </summary>
        public const string CARRIAGE_RETURN = @"\r";

        /// <summary>
        /// Match a date in YYYY/MM/DD format, where the delimiter can be ANY
        /// character.
        /// </summary>    
		const string MATCH_DTM_YYYYMMDD_FORMATTED = @"\d\d\d\d.\d\d.\d\d";

        /// <summary>
        /// Match a time in HH:MM:SS format, where the delimiter MUST be a
        /// colon, and all digits are required.
        /// </summary>
        const string MATCH_DTM_HHMMSS_FORMATTED = @"\d\d:\d\d:\d\d";

        /// <summary>
        /// Match a time in HH:MM:SS.TTT format, where the delimiter between
        /// hours, minutes, and seconds MUST be a colon, and the delimiter
        /// between seconds and milliseconds MUST be a point. All digits are
        /// required.
        /// </summary>
		const string MATCH_DTM_HHMMSSTTT_FORMATTED = @"\d\d:\d\d:\d\d.\d\d\d";

        /// <summary>
        /// Escaped quote, used to embed quotation marks in regular expressions.
        /// </summary>
        public const string ESCAPED_QUOTE = @"\""";


        /// <summary>
        /// Represents a Newline (CR/LF in Windows text) in a Regular Expression
        /// </summary>
        /// <remarks>
        /// See "How to avoid VBScript regular expression gotchas," at
        /// http://www.xaprb.com/blog/2005/11/04/vbscript-regular-expression-gotchas/,
        /// especially the responses.
        /// </remarks>
        public const string NEWLINE = @"\r\n";


        /// <summary>
        /// Match the beginning of the Page tag in a ASP.NET page.
        /// </summary>
        public const string PAGE_TAG_PREFIX= "<%@ Page";


        /// <summary>
        /// Match the end of the Page tag in a ASP.NET page.
        /// </summary>
        public const string PAGE_TAG_SUFFIX= "%>";


        /// <summary>
        /// Title attribute of the ASP.NET Page tag looks like this.
        /// </summary>
        public const string TITLE_ATTRIBUTE_LABEL= "Title=";
		#endregion	// Constant Strings


		#region Integer Constants
		/// <summary>
        /// In the .NET version of the regular expression matching engine, the
        /// first group, whose index is zero, matches the whole expression.
        /// </summary>
        public const int REGEXP_WHOLE_MATCH = 0;


        /// <summary>
        /// Not surprisingly, the .NET regular expression returns a collection
        /// of matches. Like all collections, individual members are numbered
        /// from zero.
        /// </summary>
        public const int REGEXP_FIRST_MATCH = 0;


        /// <summary>
        /// In the .NET version of the regular expression matching engine, the
        /// subexpressions are numbered from 1, just as they are in Perl.
        /// </summary>
        public const int REGEXP_FIRST_SUBMATCH = 1;


		/*
			--------------------------------------------------------------------
			The author cited below states that the following regular expression,
			taken from Mastering Regular Expressions, by Jeffrey Friedl, is the
			correct expression to match an arbitrary HTML tag.

			Reference:  "Using a Regular Expression to Match HTML,"
						http://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx
			--------------------------------------------------------------------
		*/

		/// <summary>
        /// Jeffrey Friedl's regular expression for matching any arbitrary HTML
        /// tag.
		/// 
		/// Jeffrey Friedl is the author of Mastering Regular Expressions,
        /// published by O'Reily, which is regarded as the "Bible" of Regular
        /// Expressions.
        /// </summary>
        public const string FRIEDL_HTML_TAG_MATCH = "</?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>";


        /// <summary>
        /// This is a derivation of Jeffrey Friedl's regular expression, adapted
        /// to capture the tag name in the first submatch.
        /// </summary>
        public const string MODIFIED_FRIEDL_HTML_TAG_MATCH = "<(/?\\w+)((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>";


        /// <summary>
        /// Use this to get the whole XML body in one long string. Repeated uses
        /// should allow you to perform stepwise refinements, until you get to
        /// the innermost tag.
        /// </summary>
        public const string FRIEDL_GRAY_WHOLE_HTML_TAG_MATCH = "<(/?\\w+)((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>.+?</\\1>";


        /// <summary>
        /// Match the whole body of any HTML document. Except in special cases,
        /// you must employ the String and IgnoreCase modifiers to get this
        /// expression to work.
        /// </summary>
        public const string SGML_COMPLETE_BODY = @"<body .*?>(.*?)</body>";


        /// <summary>
        /// Match the entire Head section of any HTML document. Except in
        /// special cases, you must employ the String and IgnoreCase modifiers
        /// to get this expression to work.
        /// </summary>
        public const string SGML_COMPLETE_HEAD = @"<head.*?>(.*?)</head>";


        /// <summary>
        /// Match the entirety of any HTML document. Use this expression to
        /// discard preceding HTTP headers. Except in special cases, you must
        /// employ the String and IgnoreCase modifiers to get this expression to
        /// work.
        /// </summary>
        public const string SGML_COMPLETE_HTML_DOC = @"<html>(.*?)</html>";


        /// <summary>
        /// Match any opening HTML or XML tag.
        ///
        /// Except in special cases, you should employ the IgnoreCase modifier.
        /// </summary>
        public const string SGML_OPENING_TAG_ANY = @"<(.*?)(.*?)>";


        /// <summary>
        /// Match any closing HTML or XML tag.
        ///
        /// Except in special cases, you should employ the IgnoreCase modifier.
        /// </summary>
        public const string SGML_CLOSING_TAG_ANY = @"</(.*?)>";


        /// <summary>
        /// Match an arbitrary HTML or XML tag that appears on a single line (or
        /// multiple lines, if the String modifier is employed).
        ///
        /// Except in special cases, you should employ the IgnoreCase modifier.
        ///
        /// You must interpolate the tag name into this string by calling the
        /// the static string.Format method, passing this string as the format
        /// and the tag as the sole substitution value.
        ///
        /// You may also pass a tag name to static method MatchArbitraryHtmlTag,
        /// which returns a pattern. For example, to find all Anchor tags, pass
        /// "A" to MatchArbitraryHtmlTag.
        /// </summary>
        public const string SGML_COMPLETE_TAG_ARBITRARY = @"<({0})(.*?)>(.*?)</{0}>";


        /// <summary>
        /// Match an arbitrary opening HTML or XML tag.
        ///
        /// Except in special cases, you should employ the IgnoreCase modifier.
        ///
        /// You must interpolate the tag name into this string by calling the
        /// the static string.Format method, passing this string as the format
        /// and the tag as the sole substitution value.
        ///
        /// You may also pass a tag name to method MatchArbitraryHtmlOpeningTag,
        /// which returns a pattern. For example, to find all Anchor tags, pass
        /// "A" to MatchArbitraryHtmlOpeningTag.
        /// </summary>
        public const string SGML_OPENING_TAG_ARBITRARY = @"<({0})(.*?)>";


        /// <summary>
        /// Match an arbitrary closing HTML or XML tag.
        ///
        /// Except in special cases, you should employ the IgnoreCase modifier.
        ///
        /// You must interpolate the tag name into this string by calling the
        /// the static string.Format method, passing this string as the format
        /// and the tag as the sole substitution value.
        ///
        /// You may also pass a tag name to method MatchArbitraryHtmlClosingTag,
        /// which returns a pattern. For example, to find all Anchor tags, pass
        /// "A" to MatchArbitraryHtmlClosingTag.
        /// </summary>
        public const string SGML_CLOSING_TAG_ARBITRARY = @"</({0})>";
		#endregion	//  Integer Constants


		#region Constructor and Private Constants
		private const string JOIN_TWO_PATTERNS = @"{0}{1}";
		private const string JOIN_FOUR_PATTERNS = @"{0}{1}{2}{3}";
		private const string JOIN_SIX_PATTERNS = @"{0}{1}{2}{3}{4}{5}";
		#endregion	// Constructor and Private Constants


		#region Static Methods
		/// <summary>
        /// Return a string that matches the maximum number of any character.
        /// </summary>
        /// <returns></returns>
		public static string MatchAnyCharacterGreedy ( )
		{
			return string.Format (
				JOIN_TWO_PATTERNS ,
				MATCH_WILDCARD_CHAR ,
				MATCH_MULTIPLE_PREVIOUS_CHAR );
		}	// MatchAnyCharacterMosttGreedy


		/// <summary>
		/// Return a string that matches the minimum number of any character.
		/// </summary>
		/// <returns></returns>
		public static string MatchAnyCharacterLeastGreedy ( )
		{
			return string.Format (
				JOIN_TWO_PATTERNS ,
				MatchAnyCharacterGreedy ( ) ,
				MATCH_SHORTEST );
		}	// MatchAnyCharacterLeastGreedy


		/// <summary>
        /// Interpolate a tag name into the SGML_COMPLETE_TAG_ARBITRARY match
        /// expression template.
        /// </summary>
        /// <param name="pstrTagName">
        /// String containing the name of the tag to match.
        /// </param>
        /// <returns>
        /// A Regular Expression match expression that will match the tag named
        /// in argument pstrTagName.
        /// </returns>
		public static string MatchArbitraryHtmlTag ( string pstrTagName )
		{
			if ( string.IsNullOrEmpty ( pstrTagName ) )
				return SpecialStrings.EMPTY_STRING;
			else
				return string.Format (
					SGML_COMPLETE_TAG_ARBITRARY ,
					pstrTagName );
		}	// MatchArbitraryHtmlTag


        /// <summary>
        /// Interpolate a tag name into the SGML_OPENING_TAG_ARBITRARY match
        /// expression template.
        /// </summary>
        /// <param name="pstrTagName">
        /// String containing the name of the tag to match.
        /// </param>
        /// <returns>
        /// A Regular Expression match expression that will match the opening
        /// tag named in argument pstrTagName.
        /// </returns>
		public static string MatchArbitraryHtmlOpeningTag ( string pstrTagName )
		{
			if ( string.IsNullOrEmpty ( pstrTagName ) )
				return SpecialStrings.EMPTY_STRING;
			else
				return string.Format (
					SGML_OPENING_TAG_ARBITRARY ,
					pstrTagName );
		}	// MatchArbitraryHtmlOpeningTag


        /// <summary>
        /// Interpolate a tag name into the SGML_CLOSING_TAG_ARBITRARY match
        /// expression template.
        /// </summary>
        /// <param name="pstrTagName">
        /// String containing the name of the tag to match.
        /// </param>
        /// <returns>
        /// A Regular Expression match expression that will match the closing
        /// tag named in argument pstrTagName.
        /// </returns>
		public static string MatchArbitraryHtmlClosingTag ( string pstrTagName )
		{
			if ( string.IsNullOrEmpty ( pstrTagName ) )
				return SpecialStrings.EMPTY_STRING;
			else
				return string.Format (
					SGML_CLOSING_TAG_ARBITRARY ,
					pstrTagName );
		}   // MatchArbitraryHtmlClosingTag


		/// <summary>
		/// Return a string that matches the Page tag in a ASP.NET document.
		/// </summary>
		/// <returns></returns>
		public static string MatchAspNetPageTag ( )
		{
			return string.Format (
				JOIN_FOUR_PATTERNS ,
				new string [ ] {
                    MATCH_GROUP_BEGIN.ToString ( ) ,
                    MatchAnyCharacterLeastGreedy ( ) ,
                    MATCH_GROUP_END.ToString ( ) ,
                    PAGE_TAG_SUFFIX
                } );
		}	// PAGE_TAG_MATCH


		/// <summary>
		/// Match file names against a true regular expression, as opposed to
		/// the anemic masks supported by DOS and Windows. Though occasionally
		/// referred to as regular expressions, file specifications that use
		/// DOS wild cards are a far cry from true regular expressions.
		/// </summary>
		/// <param name="pstrPathString">
		/// Specify the path string to match against PCRE pstrRegExpToMatch.
		/// </param>
		/// <param name="pstrRegExpToMatch">
		/// Specify the Perl Compatible Regular Expression against which to
		/// evaluate pstrFileName.
		/// </param>
		/// <returns>
		/// The function returns TRUE if neither string is null or empty AND
		/// pstrRegExpToMatch matches PCRE pstrFileName.
		/// </returns>
		/// <remarks>
		/// This method could have been coded inline. However, since I have at
		/// least one other project in the works that requires it, I segregated
		/// it in this routine in this small, easily navigable class.
		/// </remarks>
		public static bool MatchFileName (
			string pstrPathString ,
			string pstrRegExpToMatch )
		{
			if ( string.IsNullOrEmpty ( pstrPathString ) )
				return false;

			if ( string.IsNullOrEmpty ( pstrRegExpToMatch ) )
				return false;

			return Regex.IsMatch (
				pstrPathString ,
				pstrRegExpToMatch ,
				RegexOptions.CultureInvariant | RegexOptions.IgnoreCase );
		}   // MatchFileName


		/// <summary>
		/// Expression to match the Title attribute of an ASP.NET page.
		/// </summary>
		/// <returns></returns>
		public static string MatchHTMLPageTitleAttribute ( )
		{
			return string.Format (
				JOIN_SIX_PATTERNS ,
				new string [ ] {
                    TITLE_ATTRIBUTE_LABEL ,
                    ESCAPED_QUOTE ,
                    MATCH_GROUP_BEGIN.ToString ( ) ,
                    MatchAnyCharacterLeastGreedy ( ) ,
                    MATCH_GROUP_END.ToString ( ) ,
                    ESCAPED_QUOTE
                } );
		}	// MatchHTMLPageTitleAttribute

        public static string GoOrNoGo (string NuGetPackageConfigFileName )
        {
            string NuGetPackagePublishFlag = ( System.IO.File.GetAttributes ( NuGetPackageConfigFileName ) & System.IO.FileAttributes.Archive ) == System.IO.FileAttributes.Archive ? "true" : "false";
            return NuGetPackagePublishFlag;
        }

		#endregion	// Static Methods
	}   // class RegExpSupport
}   // partial namespace WizardWrx