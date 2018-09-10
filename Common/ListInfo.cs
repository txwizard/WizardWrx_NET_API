/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         ListInfo

    File Name:          ListInfo.cs

    Synopsis:           This static class exposes handy constants for working
                        with buffers, lists, and substrings.

    Remarks:            This class consists entirely of constants.

                        Since strings are arrays of Unicode characters, and
                        lists are, syntactically, dynamic arrays there is much
                        overlap between concepts that apply to arrays and those
                        that apply to lists. This is reflected in the heavy use
                        of synonyms in these constant definitions. When one is
                        employed, it refers to ArrayInfo or MagicNumbers, both 
						sibling classes.

    Author:             David A. Gray.

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

    Created:            Sunday, 14 September 2014 and Monday, 15 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/15 5.2     DAG    Initial implementation.

	2015/06/20 5.5     DAG    Relocate to WizardWrx namespace and
                              class library.

	2015/09/01 5.7     DAG    Correct spelling errors flagged by the spelling
                              checker add-in that I recently installed into the
                              Visual Studio 2013 IDE.


	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
                              of classes can be consumed independently.

						      This module moved into WizardWrx.Common.dll, a new
                              dynamic-link library, but stays in the WizardWrx
                              root namespace.
    ============================================================================
*/


namespace WizardWrx
{
    /// <summary>
    /// This static class exposes handy constants for working with buffers,
    /// lists, and substrings.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="MagicNumbers"/>
	/// <seealso cref="PathPositions"/>
	/// <seealso cref="SpecialCharacters"/>
	public static class ListInfo
    {
        #region Convenience Constants
        /// <summary>
        /// Use this to signify the beginning of a binary I/O buffer.
        /// 
        /// Coincidentally, this happens to be ArrayInfo.ARRAY_FIRST_ELEMENT,
        /// which is logical, since a buffer is an array.
        /// </summary>
        public const int BEGINNING_OF_BUFFER = ArrayInfo.ARRAY_FIRST_ELEMENT;

        /// <summary>
        /// Since the BinarySearch method on a List returns the array subscript
        /// where the value was found, a return value of less than zero
        /// (BINARY_SEARCH_NOT_FOUND) means that no matching item exists in the
        /// list.
        /// 
        /// Coincidentally, this happens to be ArrayInfo.ARRAY_INVALID_INDEX.
        /// </summary>
        public const int BINARY_SEARCH_NOT_FOUND = ArrayInfo.ARRAY_INVALID_INDEX;

        /// <summary>
        /// The Length property of a string returns a value of zero
        /// (EMPTY_STRING_LENGTH) when the string is empty.
        /// </summary>
		public const int EMPTY_STRING_LENGTH = MagicNumbers.ZERO;

        /// <summary>
        /// This constant defines the value returned by the IndexOf method on a
        /// string to indicate that the search character or substring is not
        /// found.
        /// 
        /// Coincidentally, this happens to be ArrayInfo.ARRAY_INVALID_INDEX.
        /// </summary>
        public const int INDEXOF_NOT_FOUND = ArrayInfo.ARRAY_INVALID_INDEX;

        /// <summary>
        /// The Count property of a list returns a value of zero (LIST_IS_EMPTY)
        /// when the list is empty.
        /// </summary>
        public const int LIST_IS_EMPTY = 0;

        /// <summary>
        /// Since a string is an array of Unicode characters, it makes sense to
        /// treat substrings as arrays.
        /// 
        /// Coincidentally, this happens to be ArrayInfo.ARRAY_FIRST_ELEMENT,
        /// which is logical, since a substring is an array of Unicode
        /// characters.
        /// </summary>
        public const int SUBSTR_BEGINNING = ArrayInfo.ARRAY_FIRST_ELEMENT;

        /// <summary>
        /// Since a string is an array of Unicode characters, it makes sense to
        /// treat substrings as arrays.
        /// 
        /// Coincidentally, this happens to be ArrayInfo.ARRAY_SECOND_ELEMENT,
        /// which is logical, since a substring is an array of Unicode
        /// characters.
        /// </summary>
        /// 
        /// If the function cannot return the requested character, the return
        /// value is SpecialCharacters.NUL, the null character.
        public const int SUBSTR_SECOND_CHAR = ArrayInfo.ARRAY_SECOND_ELEMENT;
        #endregion  // Convenience Constants


        #region Service Methods
        /// <summary>
        /// Return the first character of a string.
        /// </summary>
        /// <param name="pstrIn">
        /// Specify the string from which to return the first character.
        /// </param>
        /// <returns>
        /// Unless the input string is empty (or null), the return value is its
        /// first character.
        /// 
        /// If the function cannot return the requested character, the return
        /// value is SpecialCharacters.NUL, the null character.
        /// </returns>
        public static char FirstCharOfString ( string pstrIn )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
                return SpecialCharacters.NULL_CHAR;
            else
                return pstrIn [ SUBSTR_BEGINNING ];
        }   // public static char FirstCharOfString

        /// <summary>
        /// Return the last character of a string.
        /// </summary>
        /// <param name="pstrIn">
        /// Specify the string from which to return the last character.
        /// </param>
        /// <returns>
        /// Unless the input string is empty (or null), the return value is its
        /// last character.
        /// 
        /// If the function cannot return the requested character, the return
        /// value is SpecialCharacters.NUL, the null character.
        /// </returns>
        public static char LastCharacterOfString ( string pstrIn )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
                return SpecialCharacters.NULL_CHAR;
            else
                return pstrIn [ pstrIn.Length + ArrayInfo.INDEX_FROM_ORDINAL ];
        }   // public static char LastCharacterOfString


        /// <summary>
        /// Returns the character at the Nth position in a string.
        /// </summary>
        /// <param name="pstrIn">
        /// Specify the string from which to return the last character.
        /// </param>
        /// <param name="pintOrdinalPosition">
        /// Specify the ordinal position of the desired character. Unlike array
        /// subscripts, which start at zero, ordinals start at one.
        /// </param>
        /// <returns>
        /// Unless the input string is empty (or null), or pintOrdinalPosition
        /// is less than 1 or greater than the length of the string, the return
        /// value is the character at the specified (Nth) position.
        /// 
        /// If the function cannot return the requested character, the return
        /// value is SpecialCharacters.NUL, the null character.
        /// </returns>
        public static char NthCharacterOfString (
            string pstrIn ,
            int pintOrdinalPosition )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
                return SpecialCharacters.NULL_CHAR;
            else
                if ( pintOrdinalPosition <= ArrayInfo.ARRAY_FIRST_ELEMENT || pintOrdinalPosition > pstrIn.Length )
                    return SpecialCharacters.NULL_CHAR;
                else
                    return pstrIn [ ArrayInfo.IndexFromOrdinal ( pintOrdinalPosition ) ];
        }   // public static char NthCharacterOfString

        /// <summary>
        /// Return the next to last, or penultimate, character of a string.
        /// </summary>
        /// <param name="pstrIn">
        /// Specify the string from which to return the penultimate character.
        /// </param>
        /// <returns>
        /// Unless the input string is empty (or null), the return value is its
        /// penultimate character.
        /// 
        /// If the function cannot return the requested character, the return
        /// value is SpecialCharacters.NUL, the null character.
        /// </returns>
        public static char PenultimateCharactrOfString ( string pstrIn )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
                return SpecialCharacters.NULL_CHAR;
            else
                if ( pstrIn.Length > ArrayInfo.ARRAY_SECOND_ELEMENT )
                    return pstrIn [ pstrIn.Length - 2 ];
                else
                    return SpecialCharacters.NULL_CHAR;
        }   // public static char PenultimateCharactrOfString

        /// <summary>
        /// Return the second character of a string.
        /// </summary>
        /// <param name="pstrIn">
        /// Specify the string from which to return the second character.
        /// </param>
        /// <returns>
        /// Unless the input string is empty (or null), the return value is its
        /// second character.
        /// 
        /// If the function cannot return the requested character, the return
        /// value is SpecialCharacters.NUL, the null character.
        /// </returns>
        public static char SecondCharacterOfString ( string pstrIn )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
                return SpecialCharacters.NULL_CHAR;
            else
                if ( pstrIn.Length > ArrayInfo.ARRAY_SECOND_ELEMENT )
                    return pstrIn [ SUBSTR_SECOND_CHAR ];
                else
                    return SpecialCharacters.NULL_CHAR;
        }   // public static char SecondCharacterOfString
        #endregion  // Service Methods
    }   // public static class ListInfo
}   // partial namespace WizardWrx