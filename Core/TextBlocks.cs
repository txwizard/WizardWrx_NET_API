/*
    ============================================================================

	Namespace Name:		WizardWrx

 	File Name:			TextBlocks.cs

	Class Name:			TextBlocks

	Synopsis:			This class exposes methods for parsing and otherwise
						manipulate text blocks.

	Remarks:			Beginning with version 2.46 of this class, the build and
						revision numbers are controlled by the build engine.

	Author:				David A. Gray

	License:            Copyright (C) 2008-2017, David A. Gray. All rights reserved.

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

	Contact:			dgray@wizardwrx.com

	Date Written:		24 March 2008.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2008/03/24 1.0.9.0 DAG    Initial build, tested as part of developing Web
                              form response page, thank_you.aspx, in the My
                              Place Massage Web application.

    2008/03/27 2,0,0,0 DAG    Move to SharedUtl2 library from WizardWrx.WebApp
                              namespace to its parent, WizardWrx.

    2008/03/30 2,0,1,0 DAG    Add static methods, CharToArray and StringToArray,
                              to create one-element arrays for use with the Split
                              method of the String class.

    2008/04/01 2,0,2,0 DAG    Add static method StringOfLinesToArray, with one
                              overload, for converting strings containing lines
                              of text into arrays of lines.

    2008/05/17 2.0.6.0 DAG    Add static method ExtractDivFromHTMLDoc, which,
                              given the ID of a division and the name of a HTML
                              document, returns the contents of the named
                              division.

    2008/05/17 2.0.6.1 DAG    Move static method ExtractDivFromHTMLDoc into a
                              new class, Divisions, in a new subsidiary
                              namespace, HTMLDocs.

    2008/06/03 2.0.6.4 DAG    Substitute the single static string.IsNullOrEmpty
                              method for pairs of tests for null and empty
                              strings, wherever they appear in any function,
                              with the same outcome, in this, or any other class
                              in this library.

	2010/04/04 2.51    DAG    Add missing bits of standard documentation to the
                              heading.

	2015/06/20 5.5     DAG    Relocate to WizardWrx.DLLServices2 namespace and
                              class library.
 
    2016/04/10 6.0     DAG    Scan for typographical errors flagged by the
							  spelling checker add-in, and correct what I find,
                              Update the formatting and marking of blocks, and
							  add my BSD license.

	2017/02/19 7.0     DAG    This class is moved to WizardWrx.Core.dll, and is
                              otherwise unchanged.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;

/* Added by DAG */

using System.Text.RegularExpressions;

namespace WizardWrx
{
    /// <summary>
    /// Methods for creating and manipulating test blocks.
    /// </summary>
    public class TextBlocks
    {
        #region Public Constants
        /// <summary>
        /// My text block begin markers always end with this string.
        /// </summary>
        public const string BLOCK_BEGIN_TEXT = @"_Begin";

        /// <summary>
        /// My text block ending markers always end with this string.
        /// </summary>
        public const string BLOCK_END_TEXT = @"_End";
		#endregion	// Public Constants


		#region Private Storage
        private string _strMarkerText = SpecialStrings.EMPTY_STRING;
		private string _strInputData = SpecialStrings.EMPTY_STRING;
		#endregion	// Private Storage


		#region Constructors
		/// <summary>
        /// The default constructor creates an empty class. Its MarkerText
        /// property must be set before its other properties are useful.
        /// </summary>
		public TextBlocks ( )
		{
		}	// TextBlocks constructor (1 of N)


        /// <summary>
        /// This overload creates an empty class, and initializes its MarkerText
        /// property.
        /// </summary>
        /// <param name="pstrMarkerText">
        /// String containing text from which marker prefixes and suffixes are
        /// constructed. Also sets the MarkerText property.
        /// </param>
		public TextBlocks ( string pstrMarkerText )
		{
			if ( pstrMarkerText != null )
				_strMarkerText = pstrMarkerText;
		}	// TextBlocks constructor (2 of 3)


        /// <summary>
        /// This overload creates an empty class, and initializes its rInputData
        /// and MarkerText properties.
        /// </summary>
        /// <param name="pstrInputData">
        /// String containing text from which marked blocks will be extracted.
        /// Also sets the InputData property.
        /// </param>
        /// <param name="pstrMarkerText">
        /// String containing text from which marker prefixes and suffixes are
        /// constructed. Also sets the MarkerText property.
        /// </param>
		public TextBlocks ( string pstrInputData , string pstrMarkerText )
		{
			if ( pstrInputData != null )
				_strInputData = pstrInputData;

			if ( pstrMarkerText != null )
				_strMarkerText = pstrMarkerText;
		}	// // TextBlocks constructor (3 of 3)
		#endregion	// Constructors


		#region Public Properties
		/// <summary>
        /// String from which to extract blocks.
        /// </summary>
        public string InputData
        {
            get
            {
                return _strInputData;
			}	// // public string InputData property Get method

            set
            {
				if ( value.Length > MagicNumbers.ZERO )
					_strInputData = value;
			}	// public string InputData property Set method
        }   // public string InputData property


        /// <summary>
        /// This is the text that forms the middle (and variable) part of the 
        /// TextBlocks marker strings.
        /// </summary>
        public string MarkerText
        {
            get
            {
                return _strMarkerText;
			}	// public string MarkerText property Get method

            set
            {
                if (value.Length > MagicNumbers.ZERO)
                    _strMarkerText = value;
			}	// public string MarkerText property Set method
        }   // public string MarkerText property


        /// <summary>
        /// Construct, on the fly, and return, the block prefix.
        /// </summary>
        public string Prefix
        {
            get
            {
				if ( _strMarkerText.Length > MagicNumbers.ZERO )
					return SpecialStrings.HTML_COMMENT_PREFIX_SP
						+ _strMarkerText
						+ BLOCK_BEGIN_TEXT
						+ SpecialStrings.HTML_COMMENT_SUFFIX_SP
						+ RegExpSupport.NEWLINE;
				else
					return SpecialStrings.EMPTY_STRING;
			}	// public string Prefix property Get method
        }   // public string Prefix property


        /// <summary>
        /// Construct, on the fly, and return, the block suffix.
        /// </summary>
        public string Suffix
        {
            get
            {
				if ( _strMarkerText.Length > MagicNumbers.ZERO )
					return RegExpSupport.NEWLINE
						+ SpecialStrings.HTML_COMMENT_PREFIX_SP
						+ _strMarkerText
						+ BLOCK_END_TEXT
						+ SpecialStrings.HTML_COMMENT_SUFFIX_SP;
				else
					return SpecialStrings.EMPTY_STRING;
			}	// public string Suffix property Get method
        }   // public string Suffix property
		#endregion	// Public Properties


		#region Public Instance Methods
		/// <summary>
        /// Extract a block marked by text constructed from the string in the
        /// MarkerText property from the text in the InputData property.
        /// </summary>
        /// <returns>
        /// Text between block markers. If the prefix marker is followed by a
        /// newline, the newline is removed. Likewise, if the suffix marker is
        /// preceded by a newline, that newline is also removed.
        /// </returns>
		public string ExtractBlock ( )
		{
			return ExtractText ( );
		}   // public string ExtractBlock (1 of 3)


        /// <summary>
        /// Extract a block marked by text constructed from the string in
        /// argument pstrMarkerText from the text in the InputData property, and
        /// update the MarkerText property.
        /// </summary>
        /// <param name="pstrMarkerText">
        /// String containing text from which marker prefixes and suffixes are
        /// constructed. Also sets the MarkerText property.
        /// </param>
        /// <returns>
        /// Text between block markers. If the prefix marker is followed by a
        /// newline, the newline is removed. Likewise, if the suffix marker is
        /// preceded by a newline, that newline is also removed.
        /// </returns>
		public string ExtractBlock ( string pstrMarkerText )
		{
			if ( pstrMarkerText != null )
				if ( pstrMarkerText.Length > MagicNumbers.ZERO )
					_strMarkerText = pstrMarkerText;

			return ExtractText ( );
		}   // public string ExtractBlock (2 of 3)


        /// <summary>
        /// Extract a block marked by text constructed from the string in
        /// argument pstrMarkerText from the text in argument pstrInputData.
        /// Update the InputData and MarkerText properties.
        /// </summary>
        /// <param name="pstrInputData">
        /// String containing text from which marked blocks will be extracted.
        /// Also sets the InputData property.
        /// </param>
        /// <param name="pstrMarkerText">
        /// String containing text from which marker prefixes and suffixes are
        /// constructed. Also sets the MarkerText property.
        /// </param>
        /// <returns>
        /// Text between block markers. If the prefix marker is followed by a
        /// newline, the newline is removed. Likewise, if the suffix marker is
        /// preceded by a newline, that newline is also removed.
        /// </returns>
		public string ExtractBlock (
			string pstrInputData ,
			string pstrMarkerText )
		{
			if ( pstrInputData != null )
				if ( pstrInputData.Length > MagicNumbers.ZERO )
					_strInputData = pstrInputData;

			if ( pstrMarkerText != null )
				if ( pstrMarkerText.Length > MagicNumbers.ZERO )
					_strMarkerText = pstrMarkerText;

			return ExtractText ( );
		}   // public string ExtractBlock (3 of 3)


        /// <summary>
        /// Extract a block marked by text constructed from the string in
        /// argument pstrMarkerText from the text in argument pstrInputData.
        /// Update the InputData and MarkerText properties.
        /// </summary>
        /// <returns>
        /// Array of strings containing the text between block markers. If the
        /// prefix marker is followed by a newline, the newline is removed.
        /// Likewise, if the suffix marker is preceded by a newline, that
        /// newline is also removed.
        /// </returns>
		public string [ ] ExtractBlockToArray ( )
		{
			return LinesInBlock ( );
		}	// public string [ ] ExtractBlockToArray (1 of 3)

        /// <summary>
        /// Extract a block marked by text constructed from the string in
        /// argument pstrMarkerText from the text in argument pstrInputData.
        /// Update the InputData and MarkerText properties.
        /// </summary>
        /// <param name="pstrMarkerText">
        /// String containing text from which marker prefixes and suffixes are
        /// constructed. Also sets the MarkerText property.
        /// </param>
        /// <returns>
        /// Array of strings containing the text between block markers. If the
        /// prefix marker is followed by a newline, the newline is removed.
        /// Likewise, if the suffix marker is preceded by a newline, that
        /// newline is also removed.
        /// </returns>
		public string [ ] ExtractBlockToArray ( string pstrMarkerText )
		{
			if ( pstrMarkerText != null )
				if ( pstrMarkerText.Length > MagicNumbers.ZERO )
					_strMarkerText = pstrMarkerText;

			return LinesInBlock ( );
		}	// public string [ ] ExtractBlockToArray (2 of 3)


        /// <summary>
        /// Extract a block marked by text constructed from the string in
        /// argument pstrMarkerText from the text in argument pstrInputData.
        /// Update the InputData and MarkerText properties.
        /// </summary>
        /// <param name="pstrInputData">
        /// String containing text from which marked blocks will be extracted.
        /// Also sets the InputData property.
        /// </param>
        /// <param name="pstrMarkerText">
        /// String containing text from which marker prefixes and suffixes are
        /// constructed. Also sets the MarkerText property.
        /// </param>
        /// <returns>
        /// Array of strings containing the text between block markers. If the
        /// prefix marker is followed by a newline, the newline is removed.
        /// Likewise, if the suffix marker is preceded by a newline, that
        /// newline is also removed.
        /// </returns>
		public string [ ] ExtractBlockToArray (
			string pstrInputData ,
			string pstrMarkerText )
		{
			if ( pstrInputData != null )
				if ( pstrInputData.Length > MagicNumbers.ZERO )
					_strInputData = pstrInputData;

			if ( pstrMarkerText != null )
				if ( pstrMarkerText.Length > MagicNumbers.ZERO )
					_strMarkerText = pstrMarkerText;

			return LinesInBlock ( );
		}	// public string [ ] ExtractBlockToArray (3 of 3)
		#endregion	// Public Instance Methods


		#region Static Methods
		/// <summary>
        /// Return a one-element array containing the input character.
        /// </summary>
        /// <param name="pchr">
        /// Character to place into the returned array.
        /// </param>
        /// <returns>
        /// Array of characters, containing exactly one element, which contains
        /// the single input character.
        /// </returns>
		public static char [ ] CharToArray ( char pchr )
		{
			return new char [ ] { pchr };
		}	// public static char [ ] CharToArray


        /// <summary>
        /// Split a string containing lines of text into an array of strings.
        /// </summary>
        /// <param name="pstrLines">
        /// String containing lines of text, terminated by CR/LF pairs.
        /// </param>
        /// <returns>
        /// Array of strings, one line per string. Blank lines are preserved as
        /// empty strings.
        /// </returns>
		public static string [ ] StringOfLinesToArray ( string pstrLines )
		{
			if ( pstrLines == null )
				return new string [ ] { };		// Return an empty array.

			if ( pstrLines.Length == MagicNumbers.ZERO )
				return new string [ ] { };		// Return an empty array.

			return pstrLines.Split (
				TextBlocks.StringToArray ( SpecialStrings.STRING_SPLIT_NEWLINE ) ,
				StringSplitOptions.None );
		}   // public static string [ ] StringOfLinesToArray {1 of 2)


        /// <summary>
        /// Split a string containing lines of text into an array of strings,
        /// as modified by the StringSplitOptions flag.
        /// </summary>
        /// <param name="pstrLines">
        /// String containing lines of text, terminated by CR/LF pairs.
        /// </param>
        /// <param name="penmStringSplitOptions">
        /// A member of the StringSplitOptions enumeration, presumably other
        /// than StringSplitOptions.None, which is assumed by the first
        /// overload. The only option supported by version 2 of the Microsoft
        /// .NET CLR is RemoveEmptyEntries.
        /// </param>
        /// <returns>
        /// Array of strings, one line per string. Blank lines are preserved as
        /// empty strings unless penmStringSplitOptions is RemoveEmptyEntries,
        /// as is most likely to be the case.
        /// </returns>
        /// <remarks>
        /// Use this overload to convert a string, discarding blank lines.
        /// </remarks>
		public static string [ ] StringOfLinesToArray (
			string pstrLines ,
			StringSplitOptions penmStringSplitOptions )
		{
			if ( string.IsNullOrEmpty ( pstrLines ) )
			{
				return new string [ ] { };     // Return an empty array.
			}	// if ( string.IsNullOrEmpty ( pstrLines ) )

			return pstrLines.Split (
				TextBlocks.StringToArray ( SpecialStrings.STRING_SPLIT_NEWLINE ) ,
				penmStringSplitOptions );
		}	// public static string [ ] StringOfLinesToArray {2 of 2)


        /// <summary>
        /// Return a one-element array containing the input string.
        /// </summary>
        /// <param name="pstr">
        /// String to place into the returned array.
        /// </param>
        /// <returns>
        /// Array of strings, containing exactly one element, which contains
        /// the single input string.
        /// </returns>
		public static string [ ] StringToArray ( string pstr )
		{
			return new string [ ] { pstr };
		}	// public static string [ ] StringToArray ( string pstr )
        #endregion	// Public static methods


        #region Private Code
		private string [ ] LinesInBlock ( )
		{
			string [ ] astrSplits = { SpecialStrings.STRING_SPLIT_NEWLINE };
			string strTheBlock = ExtractText ( );

			if ( strTheBlock.Length == MagicNumbers.ZERO )
				return new string [ ] { };		// Return an empty array.

			return strTheBlock.Split (
				astrSplits ,
				StringSplitOptions.None );
		}	// private string[] LinesInBlock


		private string ExtractText ( )
		{
			if ( _strInputData.Length == MagicNumbers.ZERO )
				return SpecialStrings.EMPTY_STRING;

			string strPattern = ConstructMatchString ( );

			if ( strPattern.Length == MagicNumbers.ZERO )
				return SpecialStrings.EMPTY_STRING;

			Regex rxpMatchTool = new Regex (
				strPattern ,
				RegexOptions.Singleline );

			if ( rxpMatchTool == null )
				return SpecialStrings.EMPTY_STRING;

			if ( rxpMatchTool.IsMatch ( _strInputData ) )
			{
				Match rxpTheMatch = rxpMatchTool.Match ( _strInputData );

				if ( rxpTheMatch.Success )
					return rxpTheMatch.Groups [ RegExpSupport.REGEXP_FIRST_SUBMATCH ].ToString ( );
				else
					return SpecialStrings.EMPTY_STRING;
			}   // TRUE block, if ( rxpMatchTool.IsMatch ( mstrInputData ) )
			else
			{
				return SpecialStrings.EMPTY_STRING;
			}   // FALSE block, if ( rxpMatchTool.IsMatch ( mstrInputData ) )
		}	// private string ExtractTex


		private string ConstructMatchString ( )
		{
			if ( _strMarkerText.Length == MagicNumbers.ZERO )
				return SpecialStrings.EMPTY_STRING;

			return this.Prefix
				+ RegExpSupport.MATCH_GROUP_BEGIN
				+ RegExpSupport.MatchAnyCharacterLeastGreedy ( )
				+ RegExpSupport.MATCH_GROUP_END
				+ this.Suffix;
		}	// private string ConstructMatchString
		#endregion	// Private Code
	}   // public class TextBlocks
}   // partial namespace WizardWrx