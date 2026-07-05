/*
    ============================================================================

    Module Name:        ReportHelpers.cs

    Namespace Name:     WizardWrx

    Class Name:         ReportHelpers

    Synopsis:           This class module defines some static utility methods to
                        simplify formatting of text reports, such as those
                        rendered by console mode applications.

    Remarks:            Since the inputs and internal variables are generics,
                        the MaxStringLength method can process objects of all
                        types, although the outcome depends on how they resolve
                        to a string.

                        Since this is tested code, I started the version number
                        at 2.0.

    License:            Copyright (C) 2012-2019, David A. Gray. 
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
    2012/11/17 1.7     DAG    This class makes its first appearance, in program
                              DEDocTR_Listing_Splitter.
    
    2012/11/18 1.71    DAG    Optimize MergeNewestIntoArray, and have it return
                              a new array of <T>.

    2012/11/22 2.0     DAG    Extract utility code from DEDocTR_Listing_Splitter
                              into two general purpose utility classes.

    2013/01/05 2.1     DAG    Overload DetailTemplateFromLabels to accept an
                              alternative character as its field separator.

    2014/06/29 2.3     DAG    1) Add AdjustToMaximumWidth method and Alignment
                                 enumeration.

                              2) Move all message literals from string constants
                                 to resource strings.

    2014/07/18 2.5     DAG    Move AdjustToMaximumWidth and UpgradeFormatItem to
                              the FormatItem class in the FormatStringEngine
                              class library.

    2014/09/26 2.81    DAG    Change MaxStringLength to accept its input from a
                              generic List.

	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

	2017/08/12 7.0     DAG    Move this class into WizardWrx.Core.dll, alongside
                              its siblings, so that WizardWrx.SharedUtl4.dll can
                              be retired.

    2019/05/15 7.17    DAG    Significantly simplify CreateFormatString and 
                              CreateLastToken by way of a much more efficient
                              algorithm that consumes much less memory and CPU
                              time.

	2022/11/22 9.0.253 DAG    Move this class from WizardWrx.Core to acommodate
                              the needs of SortableManagedResourceItem, which is
                              moving up the namespace tree and into this library
                              to acommodate the classes that moved here as part
                              of the consolidation of EmbeddedTextFile into this
                              library. (I copied everything except static method
                              ListResourcesInAssemblyByName yesterday.)
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WizardWrx
{
    /// <summary>
    /// This static class exposes methods to help prepare strings for use on
    /// reports.
    /// </summary>
    public static class ReportHelpers
    {
        #region Public Enumerations and Constants
        /// <summary>
        /// Set the penmAlignment argument of the AdjustToMaximumWidth method to
        /// a member of this enumeration.
        /// </summary>
        public enum Alignment
        {
            /// <summary>
            /// Align text to left.
            /// </summary>
            Left,

            /// <summary>
            /// Align text to right.
            /// </summary>
            Right
        };  // Alignment

        /// <summary>
        /// Overlooked constant - two consecutive spaces.
        /// </summary>
        public const string DOUBLE_SPACE = @"  ";

        /// <summary>
        /// Tab characters, as they must be entered into resource (.RESX) strings.
        /// </summary>
        public const string EMBEDDED_TAB = SpecialStrings.EMBEDDED_TAB;

        /// <summary>
        /// Tab characters, as they must appear in the string before it can be
        /// used.
        /// </summary>
        public const string OUTPUT_TAB = SpecialStrings.EMBEDDED_TAB;
        #endregion  // Public Enumerations and Constants


        #region Private Constants
        private const string DELIMITER_TOKEN = @"{DLM}";
        private const string TOKEN_TEMPLATE = @"{##}{DLM}";
        private const string INDEX_TOKEN = @"##";
        #endregion  // Private Constants


        #region Public Methods
        /// <summary>
        /// Given a formatted string for a label row, generate a format string
        /// for the corresponding detail row.
        /// </summary>
        /// <param name="pstrReportLabels">
        /// Label string for which to generate a detail string.
        /// </param>
        /// <returns>
        /// String, for use with string.Format method.
        /// </returns>
        public static string DetailTemplateFromLabels ( string pstrReportLabels )
        {
            return CreateFormatString (
                pstrReportLabels ,
                SpecialCharacters.TAB_CHAR );
        }   // public static string DetailTemplateFromLabels (1 of 2)


        /// <summary>
        /// Given a formatted string for a label row, generate a format string
        /// for the corresponding detail row.
        /// </summary>
        /// <param name="pstrReportLabels">
        /// Label string for which to generate a detail string.
        /// </param>
        /// <param name="pchrFieldSeparator">
        /// Character to use in lieu of TAB as field separator.
        /// </param>
        /// <returns>
        /// String, for use with string.Format method.
        /// </returns>
        public static string DetailTemplateFromLabels (
            string pstrReportLabels ,
            char pchrFieldSeparator )
        {
            return CreateFormatString (
                pstrReportLabels ,
                pchrFieldSeparator );
        }   // public static string DetailTemplateFromLabels (2 of 2)


        /// <summary>
        /// Given an array of objects of any type, return the length of the
        /// longest string made from them. See Remarks.
        /// </summary>
        /// <typeparam name="T">
        /// This is a generic method; its argument may be of any type. This 
        /// method needs only its ToString method.
        /// </typeparam>
        /// <param name="plstObjs">
        /// This argument expects an array of objects, which may be of different
        /// kinds.
        /// 
        /// Since the generic List class has a constructor that takes an array,
        /// which it copies into the new list that it returns, you can use this
        /// method to process arrays by specifying a new List of the appropriate
        /// type for this argument.
        /// For additional information, please see the Remarks section.
        /// </param>
        /// <example>
        /// The following example returns the length of the longest string 
        /// contained in array astrLinesFromFile, an array of strings.
        /// 
        /// int intLogestLine = WizardWrx.ReportHelpers.MaxStringLength ( new List&lt;string&gt; ( astrLinesFromFile ) );
        /// 
        /// Note use of the List constructor, which transforms the array into a
        /// disposable generic List.
        /// </example>
        /// <returns>
        /// The return value is the length of the longest string made from the
        /// objects in the input array. Since it is intended for use with the
        /// PadRight method on a string, it is cast to int. See Remarks.
        /// </returns>
        /// <remarks>
        /// The goal of this routine is to determine the maximum number of
        /// characters required to represent any of a collection of objects such
        /// as labels or members of an enumerated type. This method has at least
        /// three use cases.
        /// 
        /// 1) Pad the strings to a uniform length, so that all of a set of
        /// labeled values aligns vertically.
        /// 
        /// 2) Construct a key from several substrings, such that the substrings
        /// are of uniform length, and the keys can be grouped by any of the
        /// leading substrings, and ordered by the values of the last substring.
        /// 
        /// 3) Construct a composite format string that reserves enough room for
        /// the widest item in the list, so that whatever follows it on a report
        /// line aligns vertically.
        /// </remarks>
        public static int MaxStringLength<T> ( List<T> plstObjs )
        {   // Treat a null reference gracefully, as a degenerate case that returns zero.
            int rintMaxLength = MagicNumbers.ZERO;

            if ( plstObjs != null )
            {
                foreach ( T objCurrent in plstObjs )
                {   // Convert each object, in turn, to a string.
                    string strObjectAsString = objCurrent.ToString ( );

                    if ( strObjectAsString.Length > rintMaxLength )
                    {   // Update return value if string is longest so far.
                        rintMaxLength = strObjectAsString.Length;
                    }   // if ( strObjectAsString.Length > rintMaxLength )
                }   // foreach ( T objCurrent in plstObjs )
            }   // if ( plstObjs != null )

            return rintMaxLength;
        }   // public static int MaxStringLength
        #endregion  // Public Methods


        #region Private Methods
        /// <summary>
        /// <para>
        /// Strangely, the String class is missing an important static method to
        /// count occurrences of a specified character within a string. This is
        /// that missing method.
        /// </para>
        /// <para>
        /// This method, which originated in StringExtensions, moves here as a
        /// private method because this class needs it and the extension methods
        /// class from which it cames must remain where it is to minimize the
        /// number of breaking changes.
        /// </para>
        /// </summary>
        /// <param name="pstrSource">
        /// Specify the string in which to count occurrences of substring 
        /// pstrToCount.
        ///
        /// If pstrSource is null or empty, the method returns zero.
        /// 
        /// Since this is an extension method, pstrIn is supplied by the BCL
        /// when it binds this method to an instance of System.string.
        /// </param>
        /// <param name="pchrToCount">
        /// Specify the substring to count in string pstrSource. An empty string
        /// causes the method to return MagicNumbers.STRING_INDEXOF_NOT_FOUND,
        /// or -1.
        /// </param>
        /// <returns>
        /// The return value is the number of times, if any, that string
        /// pstrToCount occurs in string pstrSource, or
        /// MagicNumbers.STRING_INDEXOF_NOT_FOUND (-1) if pstrToCount is either
        /// null reference or empty the empty string.
        /// </returns>
        /// <remarks>
        /// This method implements the only overload of the string.IndexOf
        /// method that takes a character as its second argument for which I
        /// have yet to make use. There is currently no implementation of the
        /// overload that stops looking after scanning count characters, nor do
        /// I have immediate plans to implement one, though it wouldn't be hard.
        /// 
        /// This method uses the same algorithm as CountSubstrings, except that
        /// its second argument is a single character, which CANNOT be the NULL
        /// character.
        /// </remarks>
        private static int CountCharacterOccurrences (
            string pstrSource ,
            char pchrToCount )
        {
            if ( string.IsNullOrEmpty ( pstrSource ) )
            {   // Treat null strings as empty, and treat both as a valid, but degenerate, case.
                return MagicNumbers.ZERO;
            }   // if ( string.IsNullOrEmpty ( pstrSource ) )

            if ( pchrToCount == SpecialCharacters.NULL_CHAR )
            {   // This is an error. String pstrToCount should never be null or empty.
                return MagicNumbers.STRING_INDEXOF_NOT_FOUND;
            }   // if ( string.IsNullOrEmpty ( pstrToCount ) )

            int rintCount = MagicNumbers.ZERO;

            //	----------------------------------------------------------------
            //	Unless pstrSource contains at least one instance of pstrToCount,
            //	this first IndexOf is the only one that executes.
            //
            //	If there are no matches, intPos is STRING_INDEXOF_NOT_FOUND (-1)
            //	and the WHILE loop is skipped. Hence, if control falls into the
            //	loop, at least one item was found, and must be counted, and the
            //	loop continues until intPos becomes STRING_INDEXOF_NOT_FOUND.
            //	----------------------------------------------------------------

            int intPos = pstrSource.IndexOf ( pchrToCount );                    // Look for first instance.

            while ( intPos != MagicNumbers.STRING_INDEXOF_NOT_FOUND )
            {                                                                   // Found at least one.
                rintCount++;                                                    // Count it.
                intPos = pstrSource.IndexOf (
                    pchrToCount ,
                    ( intPos + ArrayInfo.NEXT_INDEX ) );                        // Search for more.
            }   // while ( intPos != MagicNumbers.STRING_INDEXOF_NOT_FOUND )

            return rintCount;                                                   // Report.
        }   // CountCharacterOccurrences


        /// <summary>
        /// Given a formatted string for a label row, generate a format string
        /// for the corresponding detail row. See Remarks.
        /// </summary>
        /// <param name="pstrReportLabels">
        /// Label string for which to generate a detail string.
        /// </param>
        /// <param name="pchrFieldSeparator">
        /// Character to use in lieu of TAB as field separator.
        /// </param>
        /// <returns>
        /// String, for use with string.Format method.
        /// </returns>
        private static string CreateFormatString (
            string pstrReportLabels ,
            char pchrFieldSeparator )
        {
            int intDelimiterCount = CountCharacterOccurrences (
                pstrReportLabels ,                          // string pstrSource
                pchrFieldSeparator );                       // char pchrToCount

            if ( intDelimiterCount > ListInfo.LIST_IS_EMPTY )
            {
                string strTokenTemplate = TOKEN_TEMPLATE.Replace (
                    DELIMITER_TOKEN ,
                    pchrFieldSeparator.ToString ( ) );

                StringBuilder sbFormat = new StringBuilder ( pstrReportLabels.Length );

                for ( int intTokenIndex = ArrayInfo.ARRAY_FIRST_ELEMENT;
                          intTokenIndex <= intDelimiterCount;
                          intTokenIndex++ )
                {
                    if ( intTokenIndex < intDelimiterCount )
                    {
                        sbFormat.Append (
                            strTokenTemplate.Replace (
                              INDEX_TOKEN ,
                              intTokenIndex.ToString ( ) ) );
                    }   // TRUE (There is at least one more iteration to go.) block, if ( intTokenIndex < intDelimiterCount )
                    else
                    {
                        string strNewTemplate = CreateLastToken (
                            strTokenTemplate ,
                            pchrFieldSeparator );
                        sbFormat.Append ( strNewTemplate.Replace (
                            INDEX_TOKEN ,
                            intTokenIndex.ToString ( ) ) );
                    }   // FALSE (This is the last iteration.) block, if ( intTokenIndex < intDelimiterCount )
                }   // for ( int intTokenIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intTokenIndex <= intDelimiterCount ; intTokenIndex++ )

                //  ------------------------------------------------------------
                //  Unless the field separator is a TAB, it is ready to go.
                //  However, if it is, the escaped TAB characters must be
                //  be replaced by plain, unescaped TAB characters on the way
                //  out the door.
                //  ------------------------------------------------------------

                if ( pchrFieldSeparator == MagicNumbers.EMPTY_STRING_LENGTH )
                {
                    return sbFormat.ToString ( ).Replace (
                        EMBEDDED_TAB ,
                        OUTPUT_TAB );
                }
                else
                {
                    return sbFormat.ToString ( );
                }   // if ( pchrFieldSeparator == SpecialCharacters.TAB_CHAR )
            }   // TRUE (anticipated outcome) block, if ( intDelimiterCount > ListInfo.LIST_IS_EMPTY )
            else if ( pstrReportLabels.Contains ( DOUBLE_SPACE ) )
            {
                throw new NotImplementedException ( @"Word parsing is not yet implemented." );
            }   // TRUE block, else if ( pstrReportLabels.Contains ( DOUBLE_SPACE ) )
            else
            {
                throw new ArgumentException (
                    string.Format (
                        Common.Properties.Resources.ERRMSG_CANNOT_PARSE ,
                        pstrReportLabels ,
                        Environment.NewLine ) );
            }   // FALSE block of anticipated outcome) block, if ( intDelimiterCount > ListInfo.LIST_IS_EMPTY ) AND else if ( pstrReportLabels.Contains ( DOUBLE_SPACE ) )
        }   // private static string CreateFormatString


        /// <summary>
        /// The last token is appended without a field delimiter. See Remarks.
        /// </summary>
        /// <param name="pstrTokenTemplate">
        /// The token used for the preceding operations is fed into this routine
        /// to have its delimiter removed. See Remarks.
        /// </param>
        /// <param name="pchrFieldSeparator">
        /// Obviously, this routine needs a copy of the delimiter character, but
        /// its use is a bit more complex. See Remarks.
        /// </param>
        /// <returns>
        /// Regardless of whether the delimiter is a TAB, which requires special
        /// handling, or another character, the return value is a bare token.
        /// See Remarks.
        /// </returns>
        /// <remarks>
        /// Since they are escaped when entered into a .NET resource (.RESX)
        /// file, such TAB characters must be handled a tad differently, using
        /// the doubly escaped token that represents a TAB character embedded in
        /// a resource string as the first argument to the Replace method on the
        /// input string, pstrTokenTemplate. To cover the case where the string
        /// is a constant, embedded in the source file, this case must call the
        /// Replace method on the new string returned by the first Replace call,
        /// passing a string representation of the regular TAB character, since
        /// the replacement is the empty string.
        /// 
        /// In all other cases, the Replace method is called once, using the
        /// actual delimiter character, pchrFieldSeparator.
        /// </remarks>
        private static string CreateLastToken (
            string pstrTokenTemplate ,
            char pchrFieldSeparator )
        {
            if ( pchrFieldSeparator == SpecialCharacters.TAB_CHAR )
                return pstrTokenTemplate.Replace (
                    EMBEDDED_TAB ,
                    SpecialStrings.EMPTY_STRING ).Replace (
                        SpecialStrings.TAB_CHAR ,
                        SpecialStrings.EMPTY_STRING );
            else
                return pstrTokenTemplate.Replace (
                    pchrFieldSeparator.ToString ( ) ,
                    SpecialStrings.EMPTY_STRING );
        }   // private static string CreateLastToken
        #endregion  // Private Methods
    }   // public class ReportHelpers
}   // partial namespace WizardWrx