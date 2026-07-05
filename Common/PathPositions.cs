/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         PathPositions

    File Name:          PathPositions.cs

    Synopsis:           Use these constants to document path string parsing.

	Remarks:            This class is one of a constellation of static classes
						that define a wide variety of symbolic constants that I
						use to make my code easier to understand when I need a
						refresher or am about to change it.

						This class consists entirely of synonymous constants.

                        Since strings are arrays of Unicode characters, there is
                        much overlap between concepts that apply to arrays and
                        those that apply to strings. This is reflected in the
                        heavy use of synonyms in these constant definitions.
                        When one is employed, it refers to ArrayInfo or
						MagicNumbers, both sibling classes.

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

	2015/06/20 5.5     DAG    Relocate to WizardWrx class library,
                              promote to the WizardWrx root namespace, change 
                              access modifier from static to sealed, and cross
                              references constants defined in other classes.
	
	2015/09/01 5.6     DAG    Correct syntax errors and other issues in the XML
                              documentation.

	2015/09/01 5.7     DAG    Correct spelling errors flagged by the spelling
                              checker add-in that I recently installed into the
                              Visual Studio 2013 IDE.

    2016/06/07 6.3     DAG    Adjust the internal documentation to correct a few
                              inconsistencies uncovered while preparing this
							  library for publication on GetHub.

	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.
    ============================================================================
*/


namespace WizardWrx
{
    /// <summary>
    /// Use these constants to document path string parsing.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="ListInfo"/>
	/// <seealso cref="MagicNumbers"/>
	/// <seealso cref="SpecialCharacters"/>
	public static class PathPositions
    {
        /// <summary>
        /// The FQFN prefix refers to the drive and path substring, ":\" which
        /// starts in the second position of the substring, preceded by the
        /// logical drive letter. In keeping with the treatment of strings and
        /// substrings as arrays, its value is an index. Accordingly, its value
        /// is equated to ArrayInfo.ARRAY_SECOND_ELEMENT.
        /// </summary>
        public const int FQFN_PREFIX_START_POS = ArrayInfo.ARRAY_SECOND_ELEMENT;

        /// <summary>
        /// The prefix of an absolute path string is TWO characters. Some very 
        /// OLD programs, display little or no awareness of directories. Hence,
        /// they render only the first of the two characters (the full colon).
        /// 
        /// By this means, you can tell whether a path name is one of these
        /// 'old" path strings, or, at the very least, must be interpreted
        /// relative to the logged directory on the specified logical drive.
        /// </summary>
		public const int FQFN_PREFIX_START_LEN = MagicNumbers.PLUS_TWO;

        /// <summary>
        /// In theory, Windows NT supports really long file names that can run
        /// to thousands of characters. However, due to the limitations of the
        /// commonly used Windows file system APIs, the practical limit on the
        /// length of a file name string remains stuck at MAX_PATH, 260
        /// characters.
        /// </summary>
		public const int MAX_PATH = MagicNumbers.CAPACITY_MAX_PATH;

        /// <summary>
        /// By their very nature, a canonical UNC path string is absolute, and
        /// it must begin with two fixed characters, "\\". In keeping with the
        /// treatment of string and substrings as arrays, its value is an index.
        /// Accordingly, its value is equated to ArrayInfo.ARRAY_SECOND_ELEMENT.
        /// </summary>
        public const int UNC_HOSTNAME_PREFIX_POS = ArrayInfo.ARRAY_FIRST_ELEMENT;

        /// <summary>
        /// Since a UNC path string begins with two fixed characters, it follows
        /// that the hostname, itself, begins at offset 2 (character 3). In
        /// keeping with the treatment of string and substrings as arrays, its
        /// value is an index.
        /// </summary>
		public const int UNC_HOSTNAME_START_POS = MagicNumbers.PLUS_TWO;
    }   // public static class PathPositions
}   // partial namespace WizardWrx