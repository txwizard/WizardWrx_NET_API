/*
    ============================================================================

    Namespace Name:     WizardWrx

    Class Name:         StringTricks

    File Name:          StringTricks.cs

    Synopsis:           This sealed class exposes functions that add features to
                        manipulate strings in ways that were omitted from the
                        base class libraries of the Microsoft .NET Framework.

    Remarks:            Beginning with version 2.46 of this class, the build and
                        revision numbers are controlled by the build engine.

                        I've discovered another method which is missing from the
                        System.String class: Truncate. Also missing are Left,
                        Right, and Mid, all present in BASIC, going all the way
                        back to its initial implementation at Dartmouth College.
                        Strictly speaking, it has all three, as overloads of its
                        Substring method, but its behavior differs in some very
                        technical respects.

                        Although I can, and may, hold Left and Right for another
                        update, I need Truncate today, for use with strings that
                        are destined for SQL Server tables and text boxes on Web
                        and Windows forms.

                        For additional background, see references 1 and 2.

                        In Visual Basic, two overloads cannot differ only with
                        respect to their modifiers (ByVal and ByRef). See
                        reference 3.

                        The second reference suggests implementing Truncate as
                        an extension method, which is a new feature of the .NET
                        Framework 3.5 and later. The same technique could be
                        applied to my FileInfoExtension class.

                        The fourth reference is to the application in which
                        method LengthOfLongestString was developed and tested.

    References:         1)  "C# Truncate String,"
                            http://www.dotnetperls.com/truncate-string

                        2)  "How do I truncate a .NET string?"
                            http://stackoverflow.com/questions/2776673/how-do-i-truncate-a-net-string

                        3)  "Overloaded Properties and Methods: Overloading Rules"
                            http://msdn.microsoft.com/en-us/library/1z71zbeh(v=VS.80).aspx

                        4)  Static method LengthOfLongestString, in
                            C:\Documents and Settings\DAG\My Documents\Visual Studio 2010\Projects\_Laboratory\_MyPlayPen\_MyPlayPen\Program.cs

    Author:             David A. Gray

	License:            Copyright (C) 2008-2021, David A. Gray. All rights reserved.

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

    Contact:            dgray@p6c.com

    Date Written:       Saturday, 02 August 2008

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2008/04/02 2,0,2,0 DAG    Initial version.

    2008/05/20 2.0.6.1 DAG    Add CountSubstrings method.

    2008/06/03 2.0.6.4 DAG    1) Add MakeToken method, plus one overload.

                              2) Substitute the static string.IsNullOrEmpty
                                 method for pairs of tests for null and empty
                                 strings, wherever they appear in any function,
                                 with the same outcome, in this, or any other
                                 class in this library.

                              3) Move static method, ReplaceToken, from version
                                 2.3.3.6 of library WizardWrx.WebApp.UtlLib.

    2008/06/05 2.0.6.5 DAG    Add ReplaceTokensFromList method, with overloads
                              that take a Dictionary collection of tokens and
                              their substitution values, and an optional
                              Dictionary that contains default values. Overloads
                              are provided that use the strings returned by the
                              ToString method of each object, and the other
                              overloads accept a format string, which is passed
                              into the static String.Format method, along with
                              the string returned by ToString.

    2008/07/02 2.0.8.0 DAG    Add ParseCommentInHTMLComment method.

    2008/07/02 2.2.0.1 DAG    Fix off-by-one error in ParseCommentInHTMLComment,
                              which caused it to return a trailing space with
                              the final key-value pair.

    2010/02/17 2.46    DAG    1) Add string constants for formats used in every
                                 console program.

                              2) Add ArrayOfOne Methods which, given a character
                                 or string, return it as an array of one element
                                 for use with the Split method of a string.

                              3) Mark the class sealed, and give it a default
                                 constructor, marked private, to prevent the
                                 class from being instantiated.

                              4) Upgrade the internal documentation.

    2010/04/04 2.51    DAG    Change the access modifier from sealed to static,
                              which implies sealed, but also signals the C#
                              compiler to flag creation of instance variables as
                              fatal errors.

    2011/02/27 2.53    DAG    Add the Truncate method. See Remarks.

    2011/11/23 2.62    DAG    Add the ExtractBetweenIndexOfs and
                              ExtractBoundedSubstrings methods.

    2012/09/07 2.69    DAG    Implement a C# version of LengthOfLongestString
                              for vertically aligning labeled text.

    2015/06/20 5.5     DAG    Move from WizardWrX.SharedUtl2.dll to
                              WizardWrX.DLLServices2.dll, while retaining the
                              original namespace designation.

    2015/08/31 5.6     DAG    Add a robust chop function, which behaves exactly
                              like Perl's chop.
 
    2015/10/10 5.8     DAG    Implement AppendFullStopIfMissing.
 
    2016/04/10 6.0     DAG    Scan for typographical errors flagged by the
							  spelling checker add-in, and correct what I find,
                              Update the formatting and marking of blocks, and
							  add my BSD license.

	2017/02/21 7.0     DAG    This class is moved to WizardWrx.Core.dll, and is
                              otherwise unchanged.

	2017/07/17 7.0     DAG    1) Implement the existing methods that have a
                                 string as their first argument (which is all
                                 but one method) as extension methods, retaining
                                 the existing methods as obsolete, but legal.

                                 Of necessity, the new extension methods go into
                                 a new static class that parallels this one, for
								 the most part.

                              2) In the string quoting routines (MakeToken and
                                 EncloseInChar, replace the srtring.Format calls
                                 with string.Concat, which should be marginally
                                 more efficient, since they dispense with string
                                 parsing.

                              3) Substitute SpecialStrings.EMPTY_STRING, which is not a true
                                 constant, for SpecialStrings.EMPTY_STRING,
                                 which is one.
 
                                 This instigated a global replacement, even in
                                 the unit test routines.

	2017/08/05 7.0     DAG    Move this class from library WizardWrx.Core.dll to
                              WizarddWrx.Common.dll, to break a circular
                              dependency that arose when I moved class 
                              WizardWrx.FormatStringEngine to this constellation
                              from WizardWrx.SharedUtl4, which is now completely
                              merged into this constellation.

	2018/09/09 7.0     DAG    Add DisplayNullSafely.

	2018/11/17 7.11    DAG    ParseCommentInHTMLComment: Wrap the XML comment in
                              a code block, about which I learned while 
                              researching an unrelated issue concerning cross
                              references to methods exposed by other assemblies.
                              This enabled me to close "Unexpected, though 
                              Unsurprising, Rendering of XML Comment Embedded in
                              Triple-Slash Comment #3493," at
                              https://github.com/dotnet/docfx/issues/3493.

                              Eliminate the unreferenced using directive for the
                              System.Text namespace.

	2021/06/09 8.0.146 DAG    Define a new StrFill method, implemented by code
                              that I imported from another C# source module, in
                              which it had its unit tests.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;


namespace WizardWrx
{
    /// <summary>
    /// This is a class of static methods (functions) for performing common
	/// tasks not provided by the System.String class. Most, but not all, have
	/// become obsolete, and are being retired in favor of the StringExtensions
	/// class in the WizardWrx.Core namespace.
    /// </summary>
    public static class StringTricks
    {
        #region Public Constants
        /// <summary>
        /// Default token terminator string used by the version of public static
        /// method, MakeToken, which takes one argument.
        /// </summary>
        public const string DEFAULT_TOKEN_DELM = @"$$";
		#endregion	// Public Constants


		#region Private Constants
		const string OBSOLETE_MSG = @"Use the corresponding extension method.";
		const bool OBSOLETE_USAGE_IS_LEGAL = false;
		#endregion	// Private Constants


		#region AdjustNumberOfNoun Method
		/// <summary>
		/// If the count of objects to which a noun refers is greater than 1,
		/// replace its singular form with its plural form. Use this method to
		/// generate grammatically correct sentences in which the noun's number
		/// is grammatically correct.
		/// </summary>
		/// <param name="puintNumber">
		/// Base the adjustment on this number.
		/// </param>
		/// <param name="pstrNounSingular">
		/// Specify the noun to adjust, which is assumed to be in its singular
		/// form, and that its plural is the same word with the letter "S"
		/// appended.
		/// </param>
		/// <param name="pstrPhrase">
		/// Replace all instances of pstrNoun in this string with the plural of
		/// pstrNoun if pintNumber is greater than 1.
		/// </param>
		/// <param name="pstrPluralForm">
		/// Specify the plural form of pstrSingularForm, either outright or as a
		/// plus sign followed immediately by the suffix to append.
		/// 
		/// If this arguments is a null reference or the empty string, the
		/// hard coded default suffix, a lower case s, is appended.
		/// </param>
		/// <returns>
		/// The return value is pstrPhrase, amended if needed to reflect the
		/// correct number for pstrNoun.
		/// </returns>
		public static string AdjustNumberOfNoun (
			uint puintNumber ,
			string pstrNounSingular ,
			string pstrPluralForm ,
			string pstrPhrase )
		{
			const uint SINGULAR = 1;
			const int FIRST_CHARACTER_IN_STRING = 0;
			const int STARTING_AT_SECOND_CHARACTER = 1;
			const char PLURAL_FORM_IS_SUFFIX = '+';
			const char PLURAL_SUFFIX_DEFAULT = 's';

			if ( puintNumber > SINGULAR )
			{
				if ( string.IsNullOrEmpty ( pstrPluralForm ) )
				{   // Append the default suffix.
					return pstrPhrase.Replace (
						pstrNounSingular ,
						string.Concat (
							pstrNounSingular ,
							PLURAL_SUFFIX_DEFAULT ) );
				}   // TRUE (degenerate case) block, if ( string.IsNullOrEmpty ( pstrPluralForm ) )
				else
				{   // Override the default suffix.
					if ( pstrPluralForm [ FIRST_CHARACTER_IN_STRING ] == PLURAL_FORM_IS_SUFFIX )
					{   // The plural form of the noun requires a suffix.
						return pstrPhrase.Replace (
							pstrNounSingular ,
							string.Concat (
								pstrNounSingular ,
								pstrPluralForm.Substring ( STARTING_AT_SECOND_CHARACTER ) ) );
					}   // TRUE block, if ( pstrPluralForm [ FIRST_CHARACTER_IN_STRING ] == PLURAL_FORM_IS_SUFFIX )
					else
					{   // The plural form is a best handled by substitution.
						return pstrPhrase.Replace (
							pstrNounSingular ,
							pstrPluralForm );
					}   // iFALSE block, if ( pstrPluralForm [ FIRST_CHARACTER_IN_STRING ] == PLURAL_FORM_IS_SUFFIX )
				}   // FALSE (standard case) block, if ( string.IsNullOrEmpty ( pstrPluralForm ) )
			}   // TRUE (standard case) block, if ( puintNumber > SINGULAR )
			else
			{   // Leave the noun singular.
				return pstrPhrase;
			}   // FALSE (degenerate case) block, if ( puintNumber > SINGULAR )
		}   // AdjustNumberOfNoun
		#endregion	// AdjustNumberOfNoun Method


		#region AppendFullStopIfMissing Method
		/// <summary>
		/// Unless the last character of the input string is a period (full
		/// stop), append one to the returned string.
		/// </summary>
		/// <param name="pstrInput">
		/// Specify the input string to evaluate and edit as needed.
		/// </param>
		/// <returns>
		/// The input string is returned with a period appended to it. If it already
		/// has one, the input string is returned without changes.
		/// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string AppendFullStopIfMissing ( string pstrInput )
		{
			const string FULL_STOP_AS_STRING = @".";

			if ( pstrInput.EndsWith ( FULL_STOP_AS_STRING ) )
				return pstrInput;
			else
				return string.Concat ( pstrInput , FULL_STOP_AS_STRING );
		}	// public static string AppendFullStopIfMissing
		#endregion	// AppendFullStopIfMissing Method


		#region ArrayOfOne Methods
		/// <summary>
        /// Return a one-element array containing the input character, for use
        /// as input to the Split method of the system.string class.
        /// </summary>
        /// <param name="pchrTheCharacter">
        /// Character to use as the split delimiter.
        /// </param>
        /// <returns>
        /// Array of one element, ready to feed to the string.split method.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static char [ ] ArrayOfOne ( char pchrTheCharacter )
        {
            return new char [ ] { pchrTheCharacter };
        }   // ArrayOfOne (1 of 2)

        /// <summary>
        /// Return a one-element array containing the input string, for use
        /// as input to the Split method of the system.string class.
        /// </summary>
        /// <param name="pstrTheString">
        /// String to use as the split delimiter.
        /// </param>
        /// <returns>
        /// Array of one element, ready to feed to the string.split method.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string [ ] ArrayOfOne ( string pstrTheString )
        {
            return new string [ ] { pstrTheString };
        }   // ArrayOfOne (2 of 2)
		#endregion	// ArrayOfOne Methods


		#region Chop Methods
		/// <summary>
		/// Return a new string with the terminal newline, if present, removed.
		/// </summary>
		/// <param name="pstrIn">
		/// String to be chopped
		/// </param>
		/// <returns>
		/// Chopped string
		/// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string Chop ( string pstrIn )
		{
			if ( string.IsNullOrEmpty ( pstrIn ) )
			{
				return pstrIn;
			}
			else if ( pstrIn.EndsWith ( Environment.NewLine ) )
			{
				return pstrIn.Substring ( 
					WizardWrx.ArrayInfo.ARRAY_FIRST_ELEMENT ,
					pstrIn.Length - Environment.NewLine.Length );
			}
			else if ( pstrIn.EndsWith ( SpecialStrings.STRING_SPLIT_CARRIAGE_RETURN ) )
			{
				return pstrIn.Substring (
					WizardWrx.ArrayInfo.ARRAY_FIRST_ELEMENT ,
					pstrIn.Length - SpecialStrings.STRING_SPLIT_CARRIAGE_RETURN.Length );
			}
			else if ( pstrIn.EndsWith ( SpecialStrings.STRING_SPLIT_LINEFEED ) )
			{
				return pstrIn.Substring (
					WizardWrx.ArrayInfo.ARRAY_FIRST_ELEMENT ,
					pstrIn.Length - SpecialStrings.STRING_SPLIT_LINEFEED.Length );
			}
			else
			{
				return pstrIn;
			}
		}	// public static string
		#endregion	// Chop Methods


		#region CountSubstrings Methods
		/// <summary>
        /// Strangely, the String class is missing an important static method to
        /// count substrings within a string. This is the missing method.
        /// </summary>
        /// <param name="pstrSource">
        /// String in which to count occurrences of substring pstrToCount.
        ///
        /// If pstrSource is null or empty, the method returns zero.
        /// </param>
        /// <param name="pstrToCount">
        /// Substring to count in string pstrSource. An empty string causes the
        /// method to return MagicNumbers.STRING_INDEXOF_NOT_FOUND, or -1.
        /// </param>
        /// <returns>
        /// Number of times, if any, that string pstrToCount occurs in string
        /// pstrSource, or MagicNumbers.STRING_INDEXOF_NOT_FOUND (-1) if
        /// pstrToCount is either null or empty.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static int CountSubstrings (
            string pstrSource ,
            string pstrToCount )
        {
            /*
                ----------------------------------------------------------------
                Function to count no. of occurences of Substring in Main string

                Remarks:        This isn't anything I couldn't have easily
                                written myself, but why, when it's both trivial
                                and freely available?

                                The code posted at the above reference called
                                this function CharCount, and all the variables
                                had different names.

                                The example posted on the Web recycles input
                                string pstrSource, which is bad form, for two
                                reasons.

                                1)  Inputs to functions should never be altered
                                    by the function code.

                                2)  The input string could be very long, causing
                                    the code to consume gobs of extra memory to
                                    make a new copy of part of the string with
                                    each iteration. These unnecessary copies put
                                    an unnecessarily heavy load on the garbage
                                    collector.

                                I rewrote it to use the fourth overload of the
                                String.IndexOf method, which takes a reference
                                to the original String and an Int32, which is
                                the index of a point just past where the last
                                substring was found.

                                After testing, this function will become a
                                public static method of a core library.

                References:     "String Jargon in CSharp," at
                                http://www.csharphelp.com/archives/archive12.html

                                "String.IndexOf Method (String, Int32)," at
                                http://msdn.microsoft.com/en-us/library/7cct0x33(VS.80).aspx

                ----------------------------------------------------------------
            */

			if ( string.IsNullOrEmpty ( pstrSource ) )
			{   // Treat null strings as empty, and treat as a valid, but degnerate, case.
				return MagicNumbers.ZERO;
			}	// if ( string.IsNullOrEmpty ( pstrSource ) )

			if ( string.IsNullOrEmpty ( pstrToCount ) )
			{   // This is an error. String pstrToCount should never be null or empty.
				return MagicNumbers.STRING_INDEXOF_NOT_FOUND;
			}	// if ( string.IsNullOrEmpty ( pstrToCount ) )

			int rintCount = MagicNumbers.ZERO;									// Assume none.
            int intPos = pstrSource.IndexOf(pstrToCount);						// Look for first instance.

			while ( intPos != MagicNumbers.STRING_INDEXOF_NOT_FOUND )
            {																	// Found at least one.
                rintCount++;													// Count it.
				intPos = pstrSource.IndexOf (
					pstrToCount ,
					( intPos + ArrayInfo.NEXT_INDEX ) );						// Search for more.
			}	// while ( intPos != MagicNumbers.STRING_INDEXOF_NOT_FOUND )

            return rintCount;													// Report.
        }   // public static method CountSubstrings (1 of 2)


        /// <summary>
        /// Strangely, the String class is missing an important static method to
        /// count substrings within a string. This is the missing method.
        /// </summary>
        /// <param name="pstrSource">
        /// String in which to count occurrences of substring pstrToCount.
        ///
        /// If pstrSource is null or empty, the method returns zero.
        /// </param>
        /// <param name="pstrToCount">
        /// Substring to count in string pstrSource. An empty string causes the
        /// method to return MagicNumbers.STRING_INDEXOF_NOT_FOUND, or -1.
        /// </param>
        /// <param name="penmComparisonType">
        /// A member of the StringComparison enumeration, which defines the
        /// rules for performing the comparison.
        /// </param>
        /// <returns>
        /// Number of times, if any, that string pstrToCount occurs in string
        /// pstrSource, or MagicNumbers.STRING_INDEXOF_NOT_FOUND (-1) if
        /// pstrToCount is either null or empty.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static int CountSubstrings (
            string pstrSource ,
            string pstrToCount ,
            StringComparison penmComparisonType )
        {
            /*
                ----------------------------------------------------------------
                References:     "String.IndexOf Method (String, Int32, StringComparison)," at
                                http://msdn.microsoft.com/en-us/library/ms224424(VS.80).aspx

                                "StringComparison Enumeration," at
                                http://msdn.microsoft.com/en-us/library/system.stringcomparison(VS.80).aspx
                ----------------------------------------------------------------
            */

			if ( string.IsNullOrEmpty ( pstrSource ) )
			{   // Treat null strings as empty, and treat as a valid, but degenerate, case.
				return MagicNumbers.ZERO;
			}

			if ( string.IsNullOrEmpty ( pstrToCount ) )
			{   // This is an error. String pstrToCount should never be null or empty.
				return MagicNumbers.STRING_INDEXOF_NOT_FOUND;
			}

			int rintCount = MagicNumbers.ZERO;									// Assume none.

			int intPos = pstrSource.IndexOf (
				pstrToCount ,
				penmComparisonType );											// Look for first instance.

			while ( intPos != MagicNumbers.STRING_INDEXOF_NOT_FOUND )
            {																	// Found at least one.
                rintCount++;													// Count it.
				intPos = pstrSource.IndexOf (
					pstrToCount ,
					( intPos + 1 ) ,
					penmComparisonType );										// Search for more.
			}	// while ( intPos != MagicNumbers.STRING_INDEXOF_NOT_FOUND )

            return rintCount;													// Report.
        }   // public static method CountSubstrings (2 of 2)
		#endregion // CountSubstrings Methods


		#region DisplayNullSafely Methods
		/// <summary>
		/// Wrap an object reference that may happen to be null in a call to
		/// this method to guarantee that passing it into a method such as
		/// string.Format, Console.WriteLine, or one of their many cousins,
		/// to prevent it throwing a null reference exception.
		/// </summary>
		/// <param name="pobjAnything">
		/// Since everything inherits a Tostring method from Object, this method
		/// is safe for all types.
		/// </param>
		/// <returns>
		/// The return value is either the string representation of the input or
		/// a string representation of the word "null" enclosed in square
		/// brackets.
		/// </returns>
		public static string DisplayNullSafely ( object pobjAnything )
		{
			return pobjAnything != null
				? pobjAnything.ToString ( )
				: WizardWrx.Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL;
		}   // DisplayNullSafely Method
		#endregion //  DisplayNullSafely Methods


		#region EncloseInChar Methods
		/// <summary>
		/// Append a specified character to both ends of a string, unless it is
		/// already present.
		/// </summary>
		/// <param name="pstrIn">
		/// String to evaluate, which may, or may not, end with the character
		/// specified in pchrEnd.
		/// </param>
		/// <param name="pchrEnd">
		/// Character to append, if absent.
		/// </param>
		/// <returns>
		/// String with character pchrEnd at both ends.
		/// </returns>
		[ Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string EncloseInChar (
			string pstrIn ,
			char pchrEnd )
		{
			string strEndChar = pchrEnd.ToString ( );

			if ( string.IsNullOrEmpty ( pstrIn ) )
				return strEndChar + strEndChar;

			if ( pstrIn.StartsWith ( strEndChar ) && pstrIn.EndsWith ( strEndChar ) )
				return pstrIn;
			else
				if ( pstrIn.StartsWith ( strEndChar ) )
				return pstrIn + strEndChar;
			else
					if ( pstrIn.EndsWith ( strEndChar ) )
				return strEndChar + pstrIn;
			else
				return strEndChar + pstrIn + strEndChar;
		}   // EncloseInChar
		#endregion // EncloseInChar Methods


		#region ExtractBetweenIndexOfs Methods
		/// <summary>
		/// Extract the substring bounded by the characters at either end of it.
		/// </summary>
		/// <param name="pstrWholeString">
		/// Extract the substring from this string.
		/// </param>
		/// <param name="pintPosBegin">
		/// This integer is the position, given by IndexOf, of the character
		/// that bounds the left end of the desired substring.
		/// </param>
		/// <param name="pintPosEnd">
		/// This integer is the position, given by IndexOf, of the character
		/// that bounds the right end of the desired substring.
		/// </param>
		/// <returns>
		/// The returned substring begins with the character immediately to the
		/// right of the left hand bounding character, and ending with the last
		/// character before the right hand bounding character.
		/// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ExtractBetweenIndexOfs (
            string pstrWholeString ,
            int pintPosBegin ,
            int pintPosEnd )
        {
			const int DISCARD = MagicNumbers.PLUS_ONE;

            if ( string.IsNullOrEmpty ( pstrWholeString ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pintPosBegin > MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                if ( pintPosEnd > pintPosBegin )
                    if ( pintPosEnd < pstrWholeString.Length )
                        return pstrWholeString.Substring (
                            pintPosBegin + DISCARD ,
                            ( pintPosEnd - pintPosBegin ) - DISCARD );

            return SpecialStrings.EMPTY_STRING;
        }   // static string ExtractBetweenIndexOfs (1 of 2)


        /// <summary>
        /// Extract the substring bounded by the characters at either end of it.
        /// </summary>
        /// <param name="pstrWholeString">
        /// Extract the substring from this string.
        /// </param>
        /// <param name="pstrLeftMarker">
        /// This overload handles the case where the left boundary is bounded by
        /// a string. The method needs a copy of the string in order to find the
        /// true beginning of the substring to extract, and to compute its
        /// length.
        /// </param>
        /// <param name="pintPosBegin">
        /// This integer is the position, given by IndexOf, of the character
        /// that bounds the left end of the desired substring.
        /// </param>
        /// <param name="pintPosEnd">
        /// This integer is the position, given by IndexOf, of the character
        /// that bounds the right end of the desired substring.
        /// </param>
        /// <returns>
        /// The returned substring begins with the character immediately to the
        /// right of the left hand bounding character, and ending with the last
        /// character before the right hand bounding character.
        ///
        /// Inputs and computed values are thoroughly sanity checked to prevent
        /// run-time exceptions. If anything is out of order, an empty string is
        /// returned.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ExtractBetweenIndexOfs (
            string pstrWholeString ,
            string pstrLeftMarker ,
            int pintPosBegin ,
            int pintPosEnd )
        {
            if ( string.IsNullOrEmpty ( pstrWholeString ) )
                return SpecialStrings.EMPTY_STRING;

            if ( string.IsNullOrEmpty ( pstrLeftMarker ) )
                return SpecialStrings.EMPTY_STRING;

            int intToDiscard = pstrLeftMarker.Length;

            if ( pintPosBegin > MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                if ( pintPosEnd > pintPosBegin )
                    if ( pintPosEnd < pstrWholeString.Length - intToDiscard )
                        return pstrWholeString.Substring (
                            pintPosBegin + intToDiscard ,
                            ( pintPosEnd - pintPosBegin ) - intToDiscard );

            return SpecialStrings.EMPTY_STRING;
        }   // static string ExtractBetweenIndexOfs (2 of 2)
		#endregion	// ExtractBetweenIndexOfs Methods


		#region ExtractBoundedSubstrings Methods
		/// <summary>
        /// Extract a substring that is bounded by a character. See Remarks.
        /// </summary>
        /// <param name="pstrWholeString">
        /// The substring is extracted from this string.
        /// </param>
        /// <param name="pchrBoundingCharacter">
        /// This is the bounding character. Please see Remarks.
        /// </param>
        /// <returns>
        /// This is the desired substring, without its bounding characters. See
        /// Remarks.
        /// </returns>
        /// <remarks>
        /// The left and right ends must be bounded by the same character. To
        /// extract a string bounded on each end by a different character, use
        /// the next overload.
        ///
        /// Inputs and computed values are thoroughly sanity checked to prevent
        /// run-time exceptions. If anything is out of order, an empty string is
        /// returned.
        /// </remarks>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ExtractBoundedSubstrings (
            string pstrWholeString ,
            char pchrBoundingCharacter )
        {
            if ( string.IsNullOrEmpty ( pstrWholeString ) )
                return SpecialStrings.EMPTY_STRING;

            if ( pchrBoundingCharacter == MagicNumbers.ZERO )
                return SpecialStrings.EMPTY_STRING;

            int intPosLeftEnd = pstrWholeString.IndexOf ( pchrBoundingCharacter );

            if ( intPosLeftEnd == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            int intPosRightEnd = pstrWholeString.IndexOf (
                pchrBoundingCharacter ,
                intPosLeftEnd + MagicNumbers.PLUS_ONE );

            if ( intPosRightEnd == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            return WizardWrx.StringTricks.ExtractBetweenIndexOfs (
                pstrWholeString ,
                intPosLeftEnd ,
                intPosRightEnd );
        }   // public static string ExtractBoundedSubstrings (1 of 3)


        /// <summary>
        /// Extract a substring that is bounded by a character. See Remarks.
        /// </summary>
        /// <param name="pstrWholeString">
        /// The substring is extracted from this string.
        /// </param>
        /// <param name="pchrLeftBound">
        /// This is the character that marks the left end of the string. See
        /// Remarks.
        /// </param>
        /// <param name="pchrRightBound">
        /// This is the character that marks the right end of the string. See
        /// Remarks.
        /// </param>
        /// <returns>
        /// This is the desired substring, without its bounding characters. See
        /// Remarks.
        /// </returns>
        /// <remarks>
        /// The left and right ends are expected to be bounded by different
        /// characters. To  extract a string bounded on each end by the same
        /// character, use the previous overload.
        ///
        /// Inputs and computed values are thoroughly sanity checked to prevent
        /// run-time exceptions. If anything is out of order, an empty string is
        /// returned.
        /// </remarks>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ExtractBoundedSubstrings (
            string pstrWholeString ,
            char pchrLeftBound ,
            char pchrRightBound )
        {
            if ( string.IsNullOrEmpty ( pstrWholeString ) )
                return SpecialStrings.EMPTY_STRING;

            if ( pchrLeftBound == MagicNumbers.ZERO )
                return SpecialStrings.EMPTY_STRING;

            if ( pchrRightBound == MagicNumbers.ZERO )
                return SpecialStrings.EMPTY_STRING;

            int intPosLeftEnd = pstrWholeString.IndexOf ( pchrLeftBound );

            if ( intPosLeftEnd == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            int intPosRightEnd = pstrWholeString.IndexOf (
                pchrRightBound ,
                intPosLeftEnd + MagicNumbers.PLUS_ONE );

            if ( intPosRightEnd == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            return WizardWrx.StringTricks.ExtractBetweenIndexOfs (
                pstrWholeString ,
                intPosLeftEnd ,
                intPosRightEnd );
        }   // public static string ExtractBoundedSubstrings (2 of 3)


        /// <summary>
        /// Extract a substring that is bounded by a pair of substrings. See
        /// Remarks.
        /// </summary>
        /// <param name="pstrWholeString">
        /// The substring is extracted from this string.
        /// </param>
        /// <param name="pstrLeftBound">
        /// This is the substring that marks the left end of the string. See
        /// Remarks.
        /// </param>
        /// <param name="pstrRightBound">
        /// This is the substring that marks the right end of the string. See
        /// Remarks.
        /// </param>
        /// <returns>
        /// This is the desired substring, without its bounding substrings. See
        /// Remarks.
        /// </returns>
        /// <remarks>
        /// The left and right ends are expected to be bounded by different
        /// substrings. To  extract a string bounded on each end by the same
        /// substring, use the same value for the third and fourth arguments.
        ///
        /// Inputs and computed values are thoroughly sanity checked to prevent
        /// run-time exceptions. If anything is out of order, an empty string is
        /// returned.
        /// </remarks>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ExtractBoundedSubstrings (
            string pstrWholeString ,
            string pstrLeftBound ,
            string pstrRightBound )
        {
            if ( string.IsNullOrEmpty ( pstrWholeString ) )
                return SpecialStrings.EMPTY_STRING;

            if ( string.IsNullOrEmpty ( pstrLeftBound ) )
                return SpecialStrings.EMPTY_STRING;

            if ( string.IsNullOrEmpty ( pstrRightBound ) )
                return SpecialStrings.EMPTY_STRING;

            int intPosLeftEnd = pstrWholeString.IndexOf ( pstrLeftBound );

            if ( intPosLeftEnd == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            int intPosRightEnd = pstrWholeString.IndexOf (
                pstrRightBound ,
                intPosLeftEnd + WizardWrx.MagicNumbers.PLUS_ONE );

            if ( intPosRightEnd == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            return WizardWrx.StringTricks.ExtractBetweenIndexOfs (
                pstrWholeString ,
                pstrLeftBound ,
                intPosLeftEnd ,
                intPosRightEnd );
        }   // public static string ExtractBoundedSubstrings (3 of 3)
		#endregion	// ExtractBoundedSubstrings Methods


		#region LengthOfLongestString Method
		/// <summary>
        /// Given an array of strings, return the length of the longest string.
        /// </summary>
        /// <param name="pastrTheseStrings">
        /// The list of strings to evaluate is supplied as an array of strings,
        /// which may be an anonymous array, constructed on the fly in the
        /// argument list.
        /// </param>
        /// <returns>
        /// The return value is the length of the longest string. If the array
        /// is empty or is composed entirely of empty strings, the return value
        /// is zero. Since it begins with a null reference test, this routine is
        /// always successful.
        /// </returns>
        /// <remarks>
        /// Feed the return value to the PadRight method on a string to get back
        /// a string that, when used as a label, yields vertically aligned data.
        /// </remarks>
        public static int LengthOfLongestString ( string [ ] pastrTheseStrings )
        {
            int rintLongest = MagicNumbers.EMPTY_STRING_LENGTH;
            int intCurrStrLen = MagicNumbers.EMPTY_STRING_LENGTH;

            if ( pastrTheseStrings == null )
            {
                return MagicNumbers.EMPTY_STRING_LENGTH;
            }   // if ( pastrTheseStrings == null )

            foreach ( string strThisString in pastrTheseStrings )
            {
                intCurrStrLen = strThisString.Length;

                if ( intCurrStrLen > rintLongest )
                {
                    rintLongest = intCurrStrLen;
                }   // if ( strThisString.Length > rintLongest )
            }   // foreach ( string strThisString in pastrTheseStrings )

            return rintLongest;
        }   // private static int LengthOfLongestString
		#endregion	// LengthOfLongestString Method


		#region MakeToken Methods
		/// <summary>
        /// Given a string containing the name of a form control (field) or
        /// other token, create its place holder token.
        /// </summary>
        /// <param name="pstrFieldName">
        /// String containing the name of the token.
        /// </param>
        /// <returns>
        /// String containing the text of the corresponding template text place
        /// holder. See Remarks.
        /// </returns>
        /// <remarks>
        /// The string is constructed by appending a standard token delimiter,
        /// which is a pair of dollar signs, to each end of the string.
        ///
        /// The token is exposed as a static property, DEFAULT_TOKEN_DELM.
        /// </remarks>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string MakeToken ( string pstrFieldName )
		{
			if ( string.IsNullOrEmpty ( pstrFieldName ) )
				return SpecialStrings.EMPTY_STRING;

			return string.Concat (
				DEFAULT_TOKEN_DELM ,
				pstrFieldName ,
				DEFAULT_TOKEN_DELM );
		}   // public static function MakeToken (1 of 2)


        /// <summary>
        /// Given a string containing the name of a form control (field) or
        /// other token, and another string containing a static token, create
        /// its place holder token.
        /// </summary>
        /// <param name="pstrFieldName">
        /// String containing the name of the token.
        /// </param>
        /// <param name="pstrTokenEnds"></param>
        /// <returns>
        /// The string is constructed by appending the token delimiter specified
        /// in argument pstrTokenEnds to both ends of the string specified in
        /// argument pstrFieldName.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string MakeToken (
			string pstrFieldName ,
			string pstrTokenEnds )
		{
			if ( string.IsNullOrEmpty ( pstrFieldName ) )
				return SpecialStrings.EMPTY_STRING;

			if ( string.IsNullOrEmpty ( pstrTokenEnds ) )
				return SpecialStrings.EMPTY_STRING;

			return string.Concat (
				pstrTokenEnds ,
				pstrFieldName ,
				pstrTokenEnds );
		}   // public static function MakeToken (2 of 2)
        #endregion // MakeToken Methods


        #region ParseCommentInHTMLComment Method
        /// <summary>
        /// Extract parameters, expressed as key-value pairs, from a standard
        /// HTML comment.
        /// </summary>
        /// <param name="pstrInput">
        /// String containing a well formed HTML comment, surrounding the
        /// key-value pairs. If the string is not a well formed HTML comment,
        /// with a single space between the comment delimiters and the body,
        /// or the string is null or empty, the returned collection is empty.
        /// </param>
        /// <returns>
        /// A NameValueCollection of parameter names and values, which may be
        /// empty, but is guaranteed to be returned, empty or not.
        /// </returns>
        /// <example>
        /// <code>
        /// Parse this: &lt;!-- ForPage=default;UseTable=False --&gt;
        ///
        /// Return this:
        ///
        ///			=======================
        ///			Name		Value
        ///			-----------	-----------
        ///			ForPage		default
        ///			UseTable	False
        ///			=======================
        /// </code>
        /// <para>The returned NameValueCollection contains two members.</para>
        /// <para>Since this method guarantees to return an initialized
        /// NameValueCollection, the empty collection is allocated by the first
        /// statement, and is unconditionally returned by the last statement.</para>
        /// </example>
        [Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
        public static NameValueCollection ParseCommentInHTMLComment ( string pstrInput )
        {
            // Parse this: <!-- ForPage=default;UseTable=False -->

            const string LEFT_DELIMITER = @"<!-- ";
            const string RIGHT_DELIMITER = @" -->";

            const char ARG_DELIM = ';';
            const char VALUE_DELIM = '=';

            const int VALUE_FROM_NAME = 2;
            const int NAME_INDEX = 0;
            const int VALUE_INDEX = 1;

            NameValueCollection rnvcArgs = new NameValueCollection ( );

            if ( !string.IsNullOrEmpty ( pstrInput ) )
            {
                if ( pstrInput.StartsWith ( LEFT_DELIMITER ) && pstrInput.EndsWith ( RIGHT_DELIMITER ) )
                {
                    int intTotalLen = pstrInput.Length;
                    int intLeftLen = LEFT_DELIMITER.Length;
                    int intRightLen = RIGHT_DELIMITER.Length;
                    int intMiddleLen = intTotalLen - ( intLeftLen + intRightLen );

                    string strMeat = pstrInput.Substring ( intLeftLen , intMiddleLen );
                    string [ ] astrParams = strMeat.Split ( ARG_DELIM );

                    foreach ( string strParam in astrParams )
                    {
                        string [ ] astrPVPair = strParam.Split (
                            new char [ ] { VALUE_DELIM } ,
                            VALUE_FROM_NAME );
                        rnvcArgs.Add (
                            astrPVPair [ NAME_INDEX ] ,
                            astrPVPair [ VALUE_INDEX ] );
                    }   // foreach ( string strParam in astrParams )
                }   // if ( pstrInput.StartsWith ( LEFT_DELIMITER ) && pstrInput.EndsWith ( RIGHT_DELIMITER ) )
            }   // if ( !string.IsNullOrEmpty ( pstrInput ) )

            return rnvcArgs;
        }   // ParseCommentInHTMLComment
        #endregion // ParseCommentInHTMLComment Methods


        #region QuoteString Method
        /// <summary>
        /// Append a quote character to both ends of a string, unless it is
        /// already present.
        /// </summary>
        /// <param name="pstrIn">
        /// String to evaluate, which may, or may not, end with the characterr
        /// specified in pchrEnd.
        /// </param>
        /// <returns>
        /// String with quote character at both ends.
        /// </returns>
        [Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string QuoteString ( string pstrIn )
		{
			return EncloseInChar (
				pstrIn ,
				SpecialCharacters.DOUBLE_QUOTE );
		}   // function QuoteString
		#endregion	// QuoteString Method


		#region RemoveEndQuotes Method
		/// <summary>
        /// Remove ending quotation marks from a string, if present.
        /// </summary>
        /// <param name="pstrIn">
        /// String to evaluate, which may, or may not, end with quotes.
        /// </param>
        /// <returns>
        /// String with ending quotes, if present, removed.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string RemoveEndQuotes ( string pstrIn )
        {
			return RemoveEndChars (
				pstrIn ,
				SpecialCharacters.DOUBLE_QUOTE );
        }   // method RemoveEndQuotes
		#endregion	// RemoveEndQuotes Method


		#region RemoveEndChars Method
		/// <summary>
        /// Remove ending character, such as brackets, from a string, if present.
        /// </summary>
        /// <param name="pstrIn">
        /// String to evaluate, which may, or may not, end with the characterr
        /// specified in pchrEnd.
        /// </param>
        /// <param name="pchrEnd">
        /// Character to remove, if present.
        /// </param>
        /// <returns>
        /// String with character pchrEnd removed from both ends.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string RemoveEndChars (
			string pstrIn ,
			char pchrEnd )
		{
			if ( pstrIn == null )
				return null;

			string strEndChar = pchrEnd.ToString ( );

			switch ( pstrIn.Length )
			{
				case MagicNumbers.ZERO:
					return SpecialStrings.EMPTY_STRING;
				case MagicNumbers.PLUS_ONE:
					if ( pstrIn == strEndChar )
						return SpecialStrings.EMPTY_STRING;
					else
						return pstrIn;
			}   // switch (pstrIn.Length)

			int intCharsToKeep = MagicNumbers.ZERO;
			int intKeepFromPos = MagicNumbers.ZERO;

			if ( pstrIn.StartsWith ( strEndChar ) && pstrIn.EndsWith ( strEndChar ) )
			{   // Trim both ends.
				intKeepFromPos = MagicNumbers.PLUS_ONE;
				intCharsToKeep = pstrIn.Length - MagicNumbers.PLUS_TWO;
				return pstrIn.Substring ( intKeepFromPos , intCharsToKeep );
			}	// True block, if ( pstrIn.StartsWith ( strEndChar ) && pstrIn.EndsWith ( strEndChar ) )
			else
			{   // At most, one end needs trimming.
				if ( pstrIn.StartsWith ( strEndChar ) )
				{   // Trim the left end.
					intKeepFromPos = MagicNumbers.PLUS_ONE;
					intCharsToKeep = pstrIn.Length - MagicNumbers.PLUS_ONE;
					return pstrIn.Substring ( intKeepFromPos , intCharsToKeep );
				}	// True block, if ( pstrIn.StartsWith ( strEndChar ) )
				else
				{   // Check the right end.
					intKeepFromPos = ArrayInfo.ARRAY_FIRST_ELEMENT;

					if ( pstrIn.EndsWith ( strEndChar ) )
					{
						intCharsToKeep = pstrIn.Length - MagicNumbers.PLUS_ONE;
						return pstrIn.Substring ( intKeepFromPos , intCharsToKeep );
					}	// True block, if ( pstrIn.EndsWith ( strEndChar ) )
					else
					{   // The string is already fit for use.
						return pstrIn;
					}	// False block, if ( pstrIn.EndsWith ( strEndChar ) )
				}   // False block, if ( pstrIn.StartsWith ( strEndChar ) )
			}	// False block, if ( pstrIn.StartsWith ( strEndChar ) && pstrIn.EndsWith ( strEndChar ) )
		}   // RemoveEndChars
		#endregion	// RemoveEndChars Methods


		#region ReplaceToken Method
		/// <summary>
        /// Given a string of text, another string of place holder text, which
        /// occurs zero or more times in the input string, return a string in
        /// which the place holder text is replaced with new text, supplied by
        /// the third argument.
        /// </summary>
        /// <param name="pstrToSearch">
        /// String to be searched and changed.
        /// </param>
        /// <param name="pstrToFind">
        /// String to be found and replaced.
        /// </param>
        /// <param name="strReplaceWith">
        /// String to substitute for all occurrences of string pstrToFind.
        /// </param>
        /// <returns>
        /// String pstrToSearch, with all occurrences of string pstrToFind
        /// replaced with string strReplaceWith.
        /// </returns>
        /// <remarks>
        /// Say it's syntactic sugar if you insist, but I'll keep using it in my
        /// code, and may eventually implement a version that takes references
        /// as arguments.
        /// </remarks>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceToken (
				string pstrToSearch ,
				string pstrToFind ,
				string strReplaceWith )
		{
			/*
			----------------------------------------------------------------

			Author:         David A. Gray, Chief Wizard, WizardWrx,
							http://www.wizardwrx.com/

			Date Written:   Wednesday, 31 October 2007

			History

			Date       Version Who Synopsis
			----------- ------ --- ---------------------------------------------
			2007/10/31 1.1.1.0 DAG Initial version, written in VB, in library
								   WizardWrx.SharedUtl1.

			2008/01/29 1.0.2.0 DAG Port to C#, in library WizardWrx.WebApp. The
								   indicated version is the library version in
								   which this function first appears.

			2008/06/03 2.0.6.4 DAG Move from class UtlLib of library
								   WizardWrx.WebApp into this library, which is
								   a more logical home for it.

								   The last version of WizardWrx.WebApp.UtlLib
								   that exports this method is 2.3.3.6. From
								   version 2.3.3.7 onwards, that library imports
								   the method from this library, from which it
								   already imports numerous other functions and
								   constants.

			--------------------------------------------------------------------
			*/

			/*
				----------------------------------------------------------------
				Sanity check the argument list, to ensure that everything is
				something the Replace method of a System.String object can
				handle gracefully.
				----------------------------------------------------------------
			*/

			if ( pstrToSearch == null )
				return SpecialStrings.EMPTY_STRING;

			if ( pstrToFind == null )
				return pstrToSearch;

			if ( pstrToFind == SpecialStrings.EMPTY_STRING )
				return pstrToSearch;

			/*
				----------------------------------------------------------------
				Call the Replace method on the input string, pstrToSearch, which
				returns a brand new string.
				----------------------------------------------------------------
			*/

			return pstrToSearch.Replace (
				pstrToFind ,
				strReplaceWith );
		}   // ReplaceToken
		#endregion	// ReplaceToken Method


		#region ReplaceTokensFromList Methods
		/// <summary>
        /// Given a string containing tokens of the form "^^ListKeyValue^^"
        /// where ListKeyValue is the value of a key in the pnvcList collection,
        /// which may or may not exist in the collection, replace all such
        /// tokens with the contents of the like named value in the collection.
        /// </summary>
        /// <param name="pstrMsg">
        /// String containing the message containing the substitution tokens.
        /// </param>
        /// <param name="pnvcList">
        /// A NameValueCollection, in which each key represents a token, and its
        /// value represents the value to be substituted for it.
        /// </param>
        /// <returns>
        /// String with tokens replaced, and tokens that have no corresponding
        /// object in the pnvcList collection preserved.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceTokensFromList (
			string pstrMsg ,
			NameValueCollection pnvcList )
		{
			if ( string.IsNullOrEmpty ( pstrMsg ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pnvcList == null )
				return pstrMsg;

			if ( pnvcList.Count == WizardWrx.MagicNumbers.ZERO )
				return pstrMsg;

			string strNewMsg = pstrMsg;

			foreach ( string strVarName in pnvcList.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = pnvcList [ strVarName ];
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}	// foreach ( string strVarName in pnvcList.Keys )

			return strNewMsg;
		}   // static method ReplaceTokensFromList (1 of 6)


        /// <summary>
        /// Replace place holders (markers) with the value of the corresponding
        /// form control (field), or a default value, if the field is empty and
        /// a default is designated.
        /// </summary>
        /// <param name="pstrTemplate">
        /// String containing a template containing the text and place markers.
        /// </param>
        /// <param name="pnvcFields">
        /// NameValueCollection containing the form control (field) values.
        /// </param>
        /// <param name="pnvcDefaults">
        /// NameValueCollection containing the form control (field) or token
        /// default values.
        ///
        /// Default values are optional. If omitted, the method substitutes an
        /// empty string.
        /// </param>
        /// <returns>
        /// String containing the text in the template, with all tokens
        /// formatted with default endings replaced by the contents of the
        /// corresponding, and like named, control (field) on the input form.
        ///
        /// Tokens bounded by "##" are replaced by strings from the Session
        /// variables collection. The same defaults collection is used for both.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceTokensFromList (
			string pstrTemplate ,
			NameValueCollection pnvcFields ,
			NameValueCollection pnvcDefaults )
		{
			if ( string.IsNullOrEmpty ( pstrTemplate ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pnvcFields == null )
				return SpecialStrings.EMPTY_STRING;

			if ( pnvcFields.Count == MagicNumbers.ZERO )
				return pstrTemplate;        // Since there are no tokens, return the template as is.

			string rstrMsg = pstrTemplate;  // Make a copy of the template.

			try
			{
				/*
					------------------------------------------------------------
					Replace tokens with default ($$) endings with data from the
					form fields collection.
					-------------------------------------------------------------
				*/

				foreach ( string strName in pnvcFields )
				{
					string strToken = WizardWrx.StringTricks.MakeToken ( strName );
					string strValue = pnvcFields [ strName ];

					if ( strValue.Length == MagicNumbers.ZERO )
						if ( pnvcDefaults [ strName ] != null )
							if ( pnvcDefaults [ strName ].Length > MagicNumbers.ZERO )
								strValue = pnvcDefaults [ strName ];

					if ( strToken.Length > MagicNumbers.ZERO )
					{
						rstrMsg = rstrMsg.Replace (
							strToken ,
							strValue );
					}   // if (strToken.Length > MagicNumbers.ZERO)
				}   // foreach (string strName in pnvcFields)
			}
			catch
			{
				throw;
			}

			return rstrMsg;
		}  // private static function ReplaceTokensFromList (2 of 6)


        /// <summary>
        /// Given a string containing tokens of the form "^^ListKeyValue^^"
        /// where ListKeyValue is the value of a key in the pnvcList collection,
        /// which may or may not exist in the collection, replace all such
        /// tokens with the contents of the like named value in the collection.
        /// </summary>
        /// <param name="pstrMsg">
        /// String containing the message containing the substitution tokens.
        /// </param>
        /// <param name="pdctList">
        /// A Dictionary, in which each key represents a token, and its value
        /// represents the value to be substituted for it.
        ///
        /// The Dictionary may contain anything, as it accepts any Object. The
        /// required substitution string is obtained by calling the default
        /// ToString method on each Object. To supply a format string, which
        /// will be applied to each Object, in turn, use the next overload.
        /// </param>
        /// <returns>
        /// String with tokens replaced, and tokens that have no corresponding
        /// object in the pnvcList collection preserved.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceTokensFromList (
			string pstrMsg ,
			Dictionary<string , object> pdctList )
		{
			if ( string.IsNullOrEmpty ( pstrMsg ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pdctList == null )
				return pstrMsg;

			if ( pdctList.Count == WizardWrx.MagicNumbers.ZERO )
				return pstrMsg;

			string strNewMsg = pstrMsg;

			foreach ( string strVarName in pdctList.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = pdctList [ strVarName ].ToString ( );
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}	// foreach ( string strVarName in pdctList.Keys )

			return strNewMsg;
		}   // static method ReplaceTokensFromList (3 of 6)


        /// <summary>
        /// Given a string containing tokens of the form "^^ListKeyValue^^"
        /// where ListKeyValue is the value of a key in the pnvcList collection,
        /// which may or may not exist in the collection, replace all such
        /// tokens with the contents of the like named value in the collection.
        /// </summary>
        /// <param name="pstrMsg">
        /// String containing the message containing the substitution tokens.
        /// </param>
        /// <param name="pdctList">
        /// A Dictionary, in which each key represents a token, and its value
        /// represents the value to be substituted for it.
        ///
        /// The Dictionary may contain anything, as it accepts any Object. The
        /// required substitution string is obtained by calling the default
        /// ToString method on each Object. To supply a format string, which
        /// will be applied to each Object, in turn, use the next overload.
        /// </param>
        /// <param name="pdctDefaults">
        /// A Dictionary, in which each key represents a token, and its value
        /// represents the default value to be substituted for it, if there is
        /// no corresponding key in dictionary pdctList.
        ///
        /// The Dictionary may contain anything, as it accepts any Object. The
        /// required substitution string is obtained by calling the default
        /// ToString method on each Object. To supply a format string, which
        /// will be applied to each Object, in turn, use the next overload.
        /// </param>
        /// <returns>
        /// String with tokens replaced, and tokens that have no corresponding
        /// object in the pnvcList OR the pdctDefaults collection preserved.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceTokensFromList (
			string pstrMsg ,
			Dictionary<string , object> pdctList ,
			Dictionary<string , object> pdctDefaults )
		{
			if ( string.IsNullOrEmpty ( pstrMsg ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pdctList == null )
				return pstrMsg;

			if ( pdctDefaults == null )
				return pstrMsg;

			if ( pdctList.Count == WizardWrx.MagicNumbers.ZERO )
				return pstrMsg;

			string strNewMsg = pstrMsg;

			foreach ( string strVarName in pdctList.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = pdctList [ strVarName ].ToString ( );
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}   // foreach (string strVarName in pdctList.Keys)

			//  ----------------------------------------------------------------
			//  Repeat for defaults, which should cover all remaining tokens.
			//  ----------------------------------------------------------------

			foreach ( string strVarName in pdctDefaults.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = pdctDefaults [ strVarName ].ToString ( );
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}   // foreach (string strVarName in pdctDefaults.Keys)

			return strNewMsg;
		}   // static method ReplaceTokensFromList (4 of 6)


        /// <summary>
        /// Given a string containing tokens of the form "^^ListKeyValue^^"
        /// where ListKeyValue is the value of a key in the pnvcList collection,
        /// which may or may not exist in the collection, replace all such
        /// tokens with the contents o object.
        /// </summary>
        /// <param name="pstrMsg">
        /// String containing the message containing the substitution tokens.
        /// </param>
        /// <param name="pdctList">
        /// A Dictionary, in which each key represents a token, and its value
        /// represents the value to be substituted for it.
        ///
        /// The Dictionary may contain anything, as it accepts any Object. The
        /// required substitution string is obtained by calling the default
        /// ToString method on each Object. To supply a format string, which
        /// will be applied to each Object, in turn, use the next overload.
        /// </param>
        /// <param name="pstrFormat">
        /// Format string, acceptable to the static String.Format method, which
        /// is used to format the string representation of each object.
        ///
        /// The string must contain a "[0}" placeholder, which may occur one or
        /// more times in the format string, which is replaced by the string
        /// returned by the ToString method of each object.
        /// </param>
        /// <returns>
        /// String with tokens replaced, and tokens that have no corresponding
        /// object in the pnvcList collection preserved.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceTokensFromList (
			string pstrMsg ,
			Dictionary<string , object> pdctList ,
			string pstrFormat )
		{
			if ( string.IsNullOrEmpty ( pstrMsg ) )
				return SpecialStrings.EMPTY_STRING;

			if ( string.IsNullOrEmpty ( pstrFormat ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pdctList == null )
				return pstrMsg;

			if ( pdctList.Count == WizardWrx.MagicNumbers.ZERO )
				return pstrMsg;

			string strNewMsg = pstrMsg;

			foreach ( string strVarName in pdctList.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = string.Format (
					pstrFormat ,
					pdctList [ strVarName ].ToString ( ) );
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}	// foreach ( string strVarName in pdctList.Keys )

			return strNewMsg;
		}   // static method ReplaceTokensFromList (5 of 6)


        /// <summary>
        /// Given a string containing tokens of the form "^^ListKeyValue^^"
        /// where ListKeyValue is the value of a key in the pnvcList collection,
        /// which may or may not exist in the collection, replace all such
        /// tokens with the contents of the like named session object.
        /// </summary>
        /// <param name="pstrMsg">
        /// String containing the message containing the substitution tokens.
        /// </param>
        /// <param name="pdctList">
        /// A Dictionary, in which each key represents a token, and its value
        /// represents the value to be substituted for it.
        ///
        /// The Dictionary may contain anything, as it accepts any Object. The
        /// required substitution string is obtained by calling the default
        /// ToString method on each Object. To supply a format string, which
        /// will be applied to each Object, in turn, use the next overload.
        /// </param>
        /// <param name="pdctDefaults">
        /// A Dictionary, in which each key represents a token, and its value
        /// represents the default value to be substituted for it, if there is
        /// no corresponding key in dictionary pdctList.
        ///
        /// The Dictionary may contain anything, as it accepts any Object. The
        /// required substitution string is obtained by calling the default
        /// ToString method on each Object. To supply a format string, which
        /// will be applied to each Object, in turn, use the next overload.
        /// </param>
        /// <param name="pstrFormat">
        /// Format string, acceptable to the static String.Format method, which
        /// is used to format the string representation of each object.
        ///
        /// The string must contain a "[0}" placeholder, which may occur one or
        /// more times in the format string, which is replaced by the string
        /// returned by the ToString method of each object.
        /// </param>
        /// <returns>
        /// String with tokens replaced, and tokens that have no corresponding
        /// object in the pnvcList OR the pdctDefaults collection preserved.
        /// </returns>
		[Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string ReplaceTokensFromList (
			string pstrMsg ,
			Dictionary<string , object> pdctList ,
			Dictionary<string , object> pdctDefaults ,
			string pstrFormat )
		{
			if ( string.IsNullOrEmpty ( pstrMsg ) )
				return SpecialStrings.EMPTY_STRING;

			if ( string.IsNullOrEmpty ( pstrFormat ) )
				return SpecialStrings.EMPTY_STRING;

			if ( pdctList == null )
				return pstrMsg;

			if ( pdctDefaults == null )
				return pstrMsg;

			if ( pdctList.Count == WizardWrx.MagicNumbers.ZERO )
				return pstrMsg;

			string strNewMsg = pstrMsg;

			foreach ( string strVarName in pdctList.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = string.Format (
					pstrFormat ,
					pdctList [ strVarName ].ToString ( ) );
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}	// foreach ( string strVarName in pdctList.Keys )

			//	----------------------------------------------------------------
			//	Repeat for defaults, which should cover all remaining tokens.
			//	----------------------------------------------------------------

			foreach ( string strVarName in pdctDefaults.Keys )
			{
				string strVarToken = WizardWrx.StringTricks.MakeToken (
					strVarName ,
					DEFAULT_TOKEN_DELM );
				string strVarValue = string.Format (
					pstrFormat ,
					pdctDefaults [ strVarName ].ToString ( ) );
				strNewMsg = strNewMsg.Replace (
					strVarToken ,
					strVarValue );
			}   // foreach ( string strVarName in pdctDefaults.Keys )

			return strNewMsg;
		}   // static method ReplaceTokensFromList (6 of 6)
        #endregion // ReplaceTokensFromList Methods


        #region StrFill Methods
        /// <summary>
        /// Return a string composed of a specified number of a specified
        /// character.
        /// </summary>
        /// <param name="pchr">
        /// Specify the character with which to fill the string, which may be
        /// anything except the null character.
        /// </param>
        /// <param name="pintCount">
        /// Specify how many characters should be in the returned string.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a string composed of a
        /// specified number of a specified character.
        /// 
        /// If pintCount is less than 1, the return value is a null reference.
        /// 
        /// If <paramref name="pchr"/> is the null character, the return value
        /// is the empty string.
        /// </returns>
        /// <remarks>
        /// Reserving both the empty string and the null reference for different
        /// input conditions permits complete diagnostic reporting without
        /// throwing any exceptions.
        /// </remarks>
        public static string StrFill (
            char pchr ,
            int pintCount )
        {
            if ( pintCount > ListInfo.LIST_IS_EMPTY )
            {
                if ( pchr != SpecialCharacters.NULL_CHAR )
                {
                    StringBuilder sbResult = new StringBuilder ( pintCount );

                    for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                              intIndex < pintCount ;
                              intIndex++ )
                    {
                        sbResult.Append ( pchr );
                    }   // for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < pintCount ; intIndex++ )

                    return sbResult.ToString ( );
                }   // TRUE (anticipated outcome) block, if ( pchr != SpecialCharacters.NULL_CHAR )
                else
                {
                    return SpecialStrings.EMPTY_STRING;
                }   // FALSE (unanticipated outcome) block, if ( pchr != SpecialCharacters.NULL_CHAR )
            }   // TRUE (anticipated outcome) block, if ( pintCount > ListInfo.LIST_IS_EMPTY )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( pintCount > ListInfo.LIST_IS_EMPTY )
        }   // StrFill
        #endregion  // StrFill Methods


        #region Truncate Method
        /// <summary>
        /// Supply the missing Truncate function to members of the String class.
        /// </summary>
        /// <param name="pstrSource">
        /// String to truncate, if it is longer than pintMaxLength.
        ///
        /// A null string is treated as an empty string, and the return value
        /// for either is an empty string.
        /// </param>
        /// <param name="pintMaxLength">
        /// Desired maximum length of the returned string. If pstrSource is
        /// longer than pintMaxLength characters, the first pintMaxLength are
        /// returned. Otherwise, the whole string is returned.
        ///
        /// If pintMaxLength is less than or equal to zero, an empty string is
        /// returned.
        /// </param>
        /// <returns>
        /// If the string length is less than or equal to the specified maximum
        /// length, the whole string is returned.
        ///
        /// Otherwise, the first pintMaxLength characters are returned.
        ///
        /// Regardless, the return value is a new System.String object.
        /// </returns>
        [Obsolete ( OBSOLETE_MSG , OBSOLETE_USAGE_IS_LEGAL )]
		public static string Truncate (
            string pstrSource ,
            int pintMaxLength )
        {
            if ( string.IsNullOrEmpty ( pstrSource ) )
                return SpecialStrings.EMPTY_STRING;
            else
                if ( pintMaxLength <= MagicNumbers.EMPTY_STRING_LENGTH )
                    return SpecialStrings.EMPTY_STRING;
                else
                    if ( pstrSource.Length > pintMaxLength )
                        return pstrSource.Substring (
                            MagicNumbers.STRING_SUBSTR_BEGINNING ,
                            pintMaxLength );
                    else
                        return pstrSource;
        }   // public static string Truncate method
		#endregion	// Truncate Method
	}   // class StringTricks
}   // partial namespace WizardWrx