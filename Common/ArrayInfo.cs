/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         ArrayInfo

    File Name:          ArrayInfo.cs

    Synopsis:           This class organizes constants and routines for working
                        with arrays. The constants are mostly synonyms for
                        constants that exist in other classes and assemblies.

    Remarks:            Although technically synonymous, at the last minute, I
                        decided to break the dependency on WizardWrs.SharedUtl2,
                        which I want to eventually retire.

    Author:             David A. Gray, Simple Soft Services, Inc.

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

    Created:            Sunday, 14 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/14 1.0     DAG    Initial implementation.

	2015/06/06 5.4     DAG    Break completely free from WizardWrx.SharedUtl2.

	2015/06/20 5.5     DAG    Relocate to WizardWrx.DLLServices2 class library,
                              promote to the WizardWrx root namespace, change 
                              access modifier from static to sealed, and cross
                              references constants defined in other classes.

	2015/09/01 5.6     DAG    Correct syntax errors and other issues in the XML
                              documentation.

    2016/06/03 6.2     DAG    Minor documentation cleanup; code is unchanged.

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
    /// This class organizes constants and routines for working with arrays. The
    /// constants are mostly synonyms for constants that exist in other classes
    /// and assemblies.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <remarks>
	/// For ease of access, I promoted the classes that expose only constants to
	/// the root of the WizardWrx namespace.
	/// </remarks>
	/// <seealso cref="ListInfo"/>
	/// <seealso cref="MagicNumbers"/>
	/// <seealso cref="PathPositions"/>
	/// <seealso cref="SpecialCharacters"/>
	public static class ArrayInfo
    {
        #region Convenience Constants
        /// <summary>
        /// Since array subscripts start at zero, the first element of any array
        /// is zero. Since the same holds for most things that go into square
        /// brackets or are called some kind of index, this constant works as
        /// well with indexes.
		/// </summary>
		/// <seealso cref="ARRAY_IS_EMPTY"/>
		/// <seealso cref="ARRAY_INVALID_INDEX"/>
		/// <seealso cref="ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		/// <seealso cref="NEXT_INDEX"/>
		/// <seealso cref="ORDINAL_FROM_INDEX"/>
		public const int ARRAY_FIRST_ELEMENT = MagicNumbers.ZERO;

        /// <summary>
        /// The Length and LongLength properties of an array return zero
        /// (ARRAY_IS_EMPTY) when the array is empty.
        /// </summary>
		/// <seealso cref="ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ARRAY_INVALID_INDEX"/>
		/// <seealso cref="ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		/// <seealso cref="NEXT_INDEX"/>
		/// <seealso cref="ORDINAL_FROM_INDEX"/>
		public const int ARRAY_IS_EMPTY = MagicNumbers.ZERO;

        /// <summary>
        /// It follows from the fact that array indices count from zero that
        /// anything less is invalid.
        /// </summary>
		/// <seealso cref="ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ARRAY_IS_EMPTY"/>
		/// <seealso cref="ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		/// <seealso cref="NEXT_INDEX"/>
		/// <seealso cref="ORDINAL_FROM_INDEX"/>
		public const int ARRAY_INVALID_INDEX = MagicNumbers.MINUS_ONE;

		/// <summary>
		/// There is an amazing number of situations that require a refeerence
		/// to the second element of an array or list.
		/// </summary>
		/// <seealso cref="ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ARRAY_IS_EMPTY"/>
		/// <seealso cref="ARRAY_INVALID_INDEX"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		/// <seealso cref="NEXT_INDEX"/>
		/// <seealso cref="ORDINAL_FROM_INDEX"/>
		public const int ARRAY_SECOND_ELEMENT = MagicNumbers.PLUS_ONE;

        /// <summary>
        /// If ORDINAL_FROM_INDEX is +1, then its inverse should be -1. Thus,
        /// both operations are additions, which are typically a tad faster,
        /// since they don't have to manage a Carry flag.
        /// </summary>
        /// <seealso cref="ORDINAL_FROM_INDEX"/>
		/// <seealso cref="ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ARRAY_IS_EMPTY"/>
		/// <seealso cref="ARRAY_INVALID_INDEX"/>
		/// <seealso cref="ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="NEXT_INDEX"/>
		/// <seealso cref="ORDINAL_FROM_INDEX"/>
		public const int INDEX_FROM_ORDINAL = MagicNumbers.MINUS_ONE;

        /// <summary>
        /// The next index is plus one, which is ambiguous, at best, in code
        /// listings.
        /// </summary>
		/// <seealso cref="ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ARRAY_IS_EMPTY"/>
		/// <seealso cref="ARRAY_INVALID_INDEX"/>
		/// <seealso cref="ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		/// <seealso cref="ORDINAL_FROM_INDEX"/>
		public const int NEXT_INDEX = MagicNumbers.PLUS_ONE;

        /// <summary>
        /// This grain of syntactic sugar is used in OrdinalFromIndex and made
        /// visible as documentation and for coding similar math inline when
        /// space permits.
        /// </summary>
		/// <seealso cref="ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ARRAY_IS_EMPTY"/>
		/// <seealso cref="ARRAY_INVALID_INDEX"/>
		/// <seealso cref="ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		/// <seealso cref="NEXT_INDEX"/>
		/// <seealso cref="INDEX_FROM_ORDINAL"/>
		public const int ORDINAL_FROM_INDEX = MagicNumbers.PLUS_ONE;
        #endregion  // Convenience Constants


        #region Service Methods
        /// <summary>
        /// Given an ordinal, such as an array element count, return the
        /// equivalent index (subscript) value.
        /// </summary>
        /// <param name="pintOrdinal">
        /// Specify the ordinal to convert.
        /// </param>
        /// <returns>
        /// The return value is the index equivalent to pintIndex.
        /// </returns>
        /// <remarks>
        /// Mathematically, the index is pintOrdinal - ORDINAL_FROM_INDEX.
        /// Hence, this routine is syntactic sugar, which a good optimizer will
        /// optimize away by generating the code inline.
        /// </remarks>
        /// <seealso cref="OrdinalFromIndex"/>
        public static int IndexFromOrdinal ( int pintOrdinal )
        {
            return pintOrdinal + INDEX_FROM_ORDINAL;
        }   // public static int IndexFromOrdinal

        /// <summary>
        /// Given an index, such as an array subscript, return the equivalent
        /// ordinal value.
        /// </summary>
        /// <param name="pintIndex">
        /// Specify the index to convert.
        /// </param>
        /// <returns>
        /// The return value is the ordinal equivalent to pintIndex.
        /// </returns>
        /// <remarks>
        /// Mathematically, the ordinal is pintIndex + ORDINAL_FROM_INDEX.
        /// Hence, this routine is syntactic sugar, which a good optimizer will
        /// optimize away by generating the code inline.
        /// </remarks>
        /// <seealso cref="IndexFromOrdinal"/>
        public static int OrdinalFromIndex ( int pintIndex )
        {
            return pintIndex + ORDINAL_FROM_INDEX;
        }   // public static int OrdinalFromIndex
        #endregion  // Service Methods
    }   // public static class ArrayInfo
}   // partial namespace WizardWrx