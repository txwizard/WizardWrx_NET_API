/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         CSVFileInfo

    File Name:          CSVFileInfo.cs

    Synopsis:           Use these constants and service routines to simplify
                        working with CSV type files.

    Remarks:            Although technically synonymous, at the last minute, I
                        decided to break the dependency on WizardWrs.SharedUtl2,
                        which I want to eventually retire.

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

    Created:            Sunday, 14 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/14 1.0     DAG    Initial implementation.

	2015/06/20 5.5     DAG    Relocate to WizardWrx namespace and
                              class library.

	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
                              of classes can be consumed independently.

						      This module moved into WizardWrx.Common.dll, a new
                              dynamic-link library, but stays in the WizardWrx
                              root namespace.
    ============================================================================
*/


using System;

namespace WizardWrx
{
    /// <summary>
    /// Use these constants and service routines to simplify working with CSV
    /// type files.
    /// </summary>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="ListInfo"/>
	/// <seealso cref="MagicNumbers"/>
    public static class CSVFileInfo
    {
        /// <summary>
        /// The Length property of a file returns this value to indicate that a
        /// file is absolutely empty.
        /// </summary>
		/// <seealso cref="FIRST_RECORD"/>
		/// <seealso cref="LABEL_ROW"/>
		/// <seealso cref="RecordCount"/>
		/// <seealso cref="LongRecordCount"/>
		/// <seealso cref="ListInfo.LIST_IS_EMPTY"/>
        public const int EMPTY_FILE = ListInfo.LIST_IS_EMPTY;

        /// <summary>
        /// When you use Syatem.IO.File.ReadAllLines to read an entire file into
        /// an array of strings, this constant refers to the first data row of a
        /// labeled CSV file.
        /// </summary>
		/// <seealso cref="EMPTY_FILE"/>
		/// <seealso cref="LABEL_ROW"/>
		/// <seealso cref="RecordCount"/>
		/// <seealso cref="LongRecordCount"/>
		/// <seealso cref="ArrayInfo.ARRAY_SECOND_ELEMENT"/>
        public const int FIRST_RECORD = ArrayInfo.ARRAY_SECOND_ELEMENT;

		/// <summary>
		/// When you use Syatem.IO.File.ReadAllLines to read an entire file into
		/// an array of strings, this constant refers to the label row of a
		/// labeled CSV file.
		/// </summary>
		/// <seealso cref="EMPTY_FILE"/>
		/// <seealso cref="FIRST_RECORD"/>
		/// <seealso cref="RecordCount"/>
		/// <seealso cref="LongRecordCount"/>
		/// <seealso cref="ArrayInfo.ARRAY_FIRST_ELEMENT"/>
		public const int LABEL_ROW = ArrayInfo.ARRAY_FIRST_ELEMENT;
		
		/// <summary>
        /// Derive the record count from the length of an array of records
        /// loaded from a labeled CSV type file.
        /// </summary>
        /// <param name="pastrWholeFile">
		/// Array populated with all records read from a text file
		/// 
		/// Please see the Remarks section.
		/// </param>
        /// <returns>
		/// Number of records in file, excluding the expected label row
		/// 
		/// Please see the Remarks section.
		/// </returns>
		/// <remarks>
		/// This method starts from the Length property of array pastrWholeFile.
		/// 
		/// For the most part, this function is syntactic sugar. Given an array,
		/// pastrWholeFile, populated with strings that represent every record
		/// in a text file, this function returns the record count, adjusted for
		/// the label row that is assumed to be present.
		/// 
		/// This assumption is justified by the fact that I almost always put a
		/// label row in my delimited ASCII text files, even if the intended use
		/// doesn't require one, because it makes diagnostic studies so much
		/// easier. In the long run, I prefer to have my programs discard the
		/// unneeded label row than have a carbon unit try to figure out what is
		/// supposed to be in each of its columns.
		/// </remarks>
		/// <seealso cref="EMPTY_FILE"/>
		/// <seealso cref="FIRST_RECORD"/>
		/// <seealso cref="LABEL_ROW"/>
		/// <seealso cref="LongRecordCount"/>
		public static int RecordCount ( string [ ] pastrWholeFile )
		{
			return pastrWholeFile.Length - ArrayInfo.ORDINAL_FROM_INDEX;
		}	// public static int RecordCount

		/// <summary>
		/// Derive the record count from the length of an array of records
		/// loaded from a labeled CSV type file.
		/// </summary>
		/// <param name="pastrWholeFile">
		/// Array populated with all records read from a text file
		/// 
		/// Please see the Remarks section.
		/// </param>
		/// <returns>
		/// Number of records in file, excluding the expected label row
		/// 
		/// Please see the Remarks section.
		/// </returns>
		/// <remarks>
		/// This method starts from the LongLength property of array
		/// pastrWholeFile.
		/// 
		/// For the most part, this function is syntactic sugar. Given an array,
		/// pastrWholeFile, populated with strings that represent every record
		/// in a text file, this function returns the record count, adjusted for
		/// the label row that is assumed to be present.
		/// 
		/// This assumption is justified by the fact that I almost always put a
		/// label row in my delimited ASCII text files, even if the intended use
		/// doesn't require one, because it makes diagnostic studies so much
		/// easier. In the long run, I prefer to have my programs discard the
		/// unneeded label row than have a carbon unit try to figure out what is
		/// supposed to be in each of its columns.
		/// </remarks>
		/// <seealso cref="EMPTY_FILE"/>
		/// <seealso cref="FIRST_RECORD"/>
		/// <seealso cref="LABEL_ROW"/>
		/// <seealso cref="RecordCount"/>
		public static long LongRecordCount ( string [ ] pastrWholeFile )
		{
			return pastrWholeFile.LongLength - ArrayInfo.ORDINAL_FROM_INDEX;
		}	// public static long LongRecordCount
    }   // public static class CSVFileInfo
}   // partial namespace WizardWrx