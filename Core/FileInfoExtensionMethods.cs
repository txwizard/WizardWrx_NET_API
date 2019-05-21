/*
    ============================================================================

    Namespace:          WizardWrx
 
    Class:              FileInfoExtensionMethods

	File Name:			FileInfoExtensionMethods.cs
 
    Synopsis            Extend the FileInfo class with additional methods that
						simplify common tasks that, while feasible, require some
						advanced coding techniques which are tricky to get right.
 
    Remarks:            Version 7.0 adds a set of extension methods to the
						the FileInfo class that are intended to replace the old
						FileInfoExtension class, which was a workaround intended
						to support version 2.0 of the .NET Framework, which did
						not support extension methods.

						Since querying the file attributes is rather obtuse,
						because they are a bit mask, version 2.66 adds a set of
                        read only properties that return Boolean values.

                        For obvious reasons, the Directory and ReparsePoint
                        attributes are omitted.

    References:         None.

    Author:             David A. Gray

    License:            Copyright (C) 2009-2019, David A. Gray. 
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
	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

	2018/09/06 7.0     DAG    Add this headnote, which I discovered was missing
	                          when I rewrote the ReadMe file.

    2019/05/20 7.17    DAG    ShowFileDetails is a new method that returns a
                              string containing a formatted report of properties
                              suitable for display on a report.
    ============================================================================
*/

using System;
using System.IO;
using System.Text;

namespace WizardWrx
{
    /// <summary>
    /// The new extension methods that supersede the instance methods on the
    /// companion FileInfoExtension class go into a dedicated class, because a
    /// static class must expose them.
    /// </summary>
    public static class FileInfoExtensionMethods
    {
        /// <summary>
        /// Use with ShowFileDetails parameter penmFileDetailsToShow.
        /// </summary>
        [Flags]
        public enum FileDetailsToShow : byte
        {
            /// <summary>
            /// Show only basic information: no UTC times, only LastWriteTime,
            /// and no Attributes.
            /// </summary>
            None = 0,

            /// <summary>
            /// Render all times as Local.
            /// </summary>
            LocalTime = 1,

            /// <summary>
            /// Render all times as UTC.
            /// </summary>
            UtcTime = 2,

            /// <summary>
            /// Include the Created time.
            /// </summary>
            CreatedTime = 4,

            /// <summary>
            /// Include the Accessed time.
            /// </summary>
            AccessedTime = 8,

            /// <summary>
            /// Include the file size in bytes.
            /// </summary>
            Size = 16,

            /// <summary>
            /// Include the file attributes, both as a mask and as
            /// human-readable values.
            /// </summary>
            Attributes = 32,

            /// <summary>
            /// Render time stamps as both Local and UTC.
            /// </summary>
            LocalAndUtc = LocalTime | UtcTime,

            /// <summary>
            /// Report all three time stamps.
            /// </summary>
            AllTimes = CreatedTime | AccessedTime,

            /// <summary>
            /// Report all times, showing both Local and UTC time stamps.
            /// </summary>
            AllTimesWithUtc = AllTimes | LocalAndUtc,

            /// <summary>
            /// Show everything.
            /// </summary>
            Everything = AllTimesWithUtc | Attributes
        }   // public enum FileDetailsToShow


        #region Static Constructor
        struct SubstringStartAndLength
        {
            public int SubstringStart;
            public int SubstringLength;

            public SubstringStartAndLength (
                int pintSubstringStart ,
                int pintSubstringLength )
            {
                SubstringStart = pintSubstringStart;
                SubstringLength = pintSubstringLength;
            }   // public SubstringStartAndLength

            public override string ToString ( )
            {
                return string.Format (
                    Core.Properties.Resources.FORMAT_SUBSTRINGSTARTANDLENGTH_TOSSTRING ,
                    SubstringStart ,                                            // Format Item 0: SubstringStart = {0},
                    SubstringLength );                                          // Format Item 1: SubstringLength = {1}
            }   // public override string ToString
        }   // struct SubstringStartAndLength


        static SubstringStartAndLength s_sslName;
        static SubstringStartAndLength s_sslWritten;
        static SubstringStartAndLength s_sslAccessed;
        static SubstringStartAndLength s_sslCreated;
        static SubstringStartAndLength s_sslSize;
        static SubstringStartAndLength s_sslAttributes;


        static FileInfoExtensionMethods ( )
        {
            const string FILE_DETAILS_LABEL_TOKEN = @"{7} ";
            const string FILE_DETAILS_NAME_TOKEN = @"{1}{8}";                   // s_sslName
            const string FILE_DETAILS_WRITTEN_TOKEN = @"{2}{8}";                // s_sslWritten
            const string FILE_DETAILS_CREATED_TOKEN = @"{3}{8}";                // s_sslCreated
            const string FILE_DETAILS_ACCESSED_TOKEN = @"{4}{8}";               // s_sslAccessed
            const string FILE_DETAILS_SIZE_TOKEN = @"{5}{8}";                   // s_sslSize
            const string FILE_DETAILS_ATTRIBUTES_TOKEN = @"{6}{10}";            // s_sslAttributes

            string strFormatString = Core.Properties.Resources.FILE_DETAILS;

            int [ ] aintLabelTokenPositions = strFormatString.EnumerateSubstringPositions ( FILE_DETAILS_LABEL_TOKEN );

            int intPosWrittenTokenEnd = strFormatString.IndexOf ( FILE_DETAILS_WRITTEN_TOKEN );
            int intPosAccessedTokenEnd = strFormatString.IndexOf ( FILE_DETAILS_ACCESSED_TOKEN );
            int intPosCreatedTokenEnd = strFormatString.IndexOf ( FILE_DETAILS_CREATED_TOKEN );
            int intPosSizeTokenEnd = strFormatString.IndexOf ( FILE_DETAILS_SIZE_TOKEN );
            int intPosAttributesTokenEnd = strFormatString.IndexOf ( FILE_DETAILS_ATTRIBUTES_TOKEN );

            int intPosWrittenTokenBegin = Array.FindLast<int> (
                aintLabelTokenPositions ,                                       // array T []
                x => x < intPosWrittenTokenEnd );                               // match Predicate<T>, a lambda expression
            int intPosAccessedTokenBegin = Array.FindLast<int> (
                aintLabelTokenPositions ,                                       // array T []
                x => x < intPosAccessedTokenEnd );                              // match Predicate<T>, a lambda expression
            int intPosCreatedTokenBegin = Array.FindLast<int> (
                aintLabelTokenPositions ,                                       // array T []
                x => x < intPosCreatedTokenEnd );                               // match Predicate<T>, a lambda expression
            int intPosSizeTokenBegin = Array.FindLast<int> (
                aintLabelTokenPositions ,                                       // array T []
                x => x < intPosSizeTokenEnd );                                  // match Predicate<T>, a lambda expression
            int intPosAttributesTokenBegin = Array.FindLast<int> (
                aintLabelTokenPositions ,                                       // array T []
                x => x < intPosAttributesTokenEnd );                            // match Predicate<T>, a lambda expression

            //  ----------------------------------------------------------------
            //  Initializing the first SubstringStartAndLength differs from the
            //  remainder because it stores the position of the first segment of
            //  the format string. The other sections refer to substrings taken
            //  from the middle or end of the string, and could be initialized
            //  in any order. Using the same structure for the first substring
            //  enables ShowFileDetails to use the same method to extract all of
            //  its format items from it.
            //  ----------------------------------------------------------------

            s_sslName = new SubstringStartAndLength (
                ListInfo.SUBSTR_BEGINNING ,                                     // int pintSubstringStart
                ComputeTokenEnd (                                               // int pintSubstringLength
                    ListInfo.SUBSTR_BEGINNING ,                                 // int pintPosTokenBegin
                    strFormatString.IndexOf (                                   // int pintPosTokenEnd
                        FILE_DETAILS_NAME_TOKEN ) ,                             // string pstrToken
                    FILE_DETAILS_NAME_TOKEN ) );                                // string pstrToken
            s_sslWritten = new SubstringStartAndLength (
                intPosWrittenTokenBegin ,                                       // int pintSubstringStart
                ComputeTokenEnd (                                               // int pintSubstringLength
                    intPosWrittenTokenBegin ,                                   // int pintPosTokenBegin
                    intPosWrittenTokenEnd ,                                     // int pintPosTokenEnd
                    FILE_DETAILS_WRITTEN_TOKEN ) );                             // string pstrToken
            s_sslAccessed = new SubstringStartAndLength (
                intPosAccessedTokenBegin ,                                      // int pintSubstringStart
                ComputeTokenEnd (                                               // int pintSubstringLength
                    intPosAccessedTokenBegin ,                                  // int pintPosTokenBegin
                    intPosAccessedTokenEnd ,                                    // int pintPosTokenEnd
                    FILE_DETAILS_ACCESSED_TOKEN ) );                            // string pstrToken
            s_sslCreated = new SubstringStartAndLength (
                intPosCreatedTokenBegin ,                                       // int pintSubstringStart
                ComputeTokenEnd (                                               // int pintSubstringLength
                    intPosCreatedTokenBegin ,                                   // int pintPosTokenBegin
                    intPosCreatedTokenEnd ,                                     // int pintPosTokenEnd
                    FILE_DETAILS_CREATED_TOKEN ) );                             // string pstrToken
            s_sslSize = new SubstringStartAndLength (
                intPosAttributesTokenBegin ,                                    // int pintSubstringStart
                ComputeTokenEnd (                                               // int pintSubstringLength
                    intPosSizeTokenBegin ,                                      // int pintPosTokenBegin
                    intPosSizeTokenEnd ,                                        // int pintPosTokenEnd
                    FILE_DETAILS_SIZE_TOKEN ) );                                // string pstrToken
            s_sslAttributes = new SubstringStartAndLength (
                intPosAttributesTokenBegin ,                                    // int pintSubstringStart
                ComputeTokenEnd (                                               // int pintSubstringLength
                    intPosAttributesTokenBegin ,                                // int pintPosTokenBegin
                    intPosAttributesTokenEnd ,                                  // int pintPosTokenEnd
                    FILE_DETAILS_ATTRIBUTES_TOKEN ) );                          // string pstrToken
        }   // static FileInfoExtensionMethods


        /// <summary>
        /// Given <paramref name="pintPosTokenBegin"/>, the position of the
        /// beginning of a substring, <paramref name="pintPosTokenEnd"/>, the
        /// position of the token that marks the end of the substring, and the
        /// token, itself, represented by <paramref name="pstrToken"/>, from
        /// which to determine its length, return the length of the substring,
        /// inclusive of its terminal token.
        /// </summary>
        /// <param name="pintPosTokenBegin">
        /// Integer containing the position at which the substring of interest
        /// begins
        /// </param>
        /// <param name="pintPosTokenEnd">
        /// Integer containing the position at which the token that marks the
        /// end of the substring of interest begins
        /// </param>
        /// <param name="pstrToken">
        /// String containing the token that marks the end of the substring of
        /// interest
        /// </param>
        /// <returns>
        /// The return value is the length of the substring of interest,
        /// inclusive of its terminal token.
        /// </returns>
        /// <remarks>
        /// Though intended for use by the static constructor, this method may
        /// warrant being exposed as a public static method for wider use.
        /// </remarks>
        private static int ComputeTokenEnd (
            int pintPosTokenBegin ,
            int pintPosTokenEnd ,
            string pstrToken )
        {
            return ( pintPosTokenEnd + pstrToken.Length ) - pintPosTokenBegin;
        }   // private static int ComputeTokenEnd
        #endregion  // Static Constructor


        #region Extension Methods for the Archive Attribute
        /// <summary>
        /// Clear the Archive FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the Archive attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeArchiveClear ( this FileInfo pfi )
        {
            return FileAttributeClear ( pfi , FileAttributes.Archive );
        }


        /// <summary>
        /// Reinstate the Archive flag on the specified file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <param name="penmInitialStatus">
        /// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
        /// FileAttributeArchiveClear.
        /// </param>
        /// <returns>
        /// If the Archive attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeArchiveReinstate (
            this FileInfo pfi ,
            FileInfoExtension.enmInitialStatus penmInitialStatus )
        {
            if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
            {
                return FileInfoExtension.enmInitialStatus.WasSet;
            }   // TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
            else
            {
                return FileAttributeSet ( pfi , FileAttributes.Archive );
            }   // FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
        }   // FileAttributeArchiveReinstate


        /// <summary>
        /// Set the Archive FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the Archive attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeArchiveSet ( this FileInfo pfi )
        {
            return FileAttributeSet ( pfi , FileAttributes.Archive );
        }
        #endregion // Extension Methods for the Archive Attribute


        #region Extension Methods for the Hidden Attribute
        /// <summary>
        /// Clear the Hidden FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the Hidden attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeHiddenClear ( this FileInfo pfi )
        {
            return FileAttributeClear ( pfi , FileAttributes.Hidden );
        }


        /// <summary>
        /// Reinstate the Hidden flag on the specified file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <param name="penmInitialStatus">
        /// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
        /// FileAttributeArchiveClear.
        /// </param>
        /// <returns>
        /// If the Hidden attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeHiddeneReinstate (
            this FileInfo pfi ,
            FileInfoExtension.enmInitialStatus penmInitialStatus )
        {
            if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
            {
                return FileInfoExtension.enmInitialStatus.WasSet;
            }   // TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
            else
            {
                return FileAttributeSet ( pfi , FileAttributes.Hidden );
            }   // FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
        }   // FileAttributeHiddeneReinstate


        /// <summary>
        /// Set the Hidden FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the Hidden attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeHiddenSet ( this FileInfo pfi )
        {
            return FileAttributeSet ( pfi , FileAttributes.Hidden );
        }
        #endregion  // Extension Methods for the Hidden Attribute


        #region Extension Methods for the ReadOnly Attribute
        /// <summary>
        /// Clear the ReadOnly FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the ReadOnly attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeReadOnlyClear ( this FileInfo pfi )
        {
            return FileAttributeClear ( pfi , FileAttributes.ReadOnly );
        }


        /// <summary>
        /// Reinstate the ReadOnly flag on the specified file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <param name="penmInitialStatus">
        /// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
        /// FileAttributeArchiveClear.
        /// </param>
        /// <returns>
        /// If the ReadOnly attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeReadOnlyReinstate (
            this FileInfo pfi ,
            FileInfoExtension.enmInitialStatus penmInitialStatus )
        {
            if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
            {
                return FileInfoExtension.enmInitialStatus.WasSet;
            }   // TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
            else
            {
                return FileAttributeSet ( pfi , FileAttributes.ReadOnly );
            }   // FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
        }   // FileAttributeReadOnlyReinstate


        /// <summary>
        /// Set the ReadOnly FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the ReadOnly attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeReadOnlySet ( this FileInfo pfi )
        {
            return FileAttributeSet ( pfi , FileAttributes.ReadOnly );
        }
        #endregion    // Extension Methods for the ReadOnly Attribute


        #region Extension Methods for the System Attribute
        /// <summary>
        /// Clear the System FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the specified bit (It's usually just one.) is set on entry, the
        /// return value is WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeSystemClear ( this FileInfo pfi )
        {
            return FileAttributeClear ( pfi , FileAttributes.System );
        }


        /// <summary>
        /// Reinstate the System flag on the specified file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <param name="penmInitialStatus">
        /// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
        /// FileAttributeArchiveClear.
        /// </param>
        /// <returns>
        /// If the System attribute is set on entry, the return value is
        /// WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeSystemReinstate (
            this FileInfo pfi ,
            FileInfoExtension.enmInitialStatus penmInitialStatus )
        {
            if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
            {
                return FileInfoExtension.enmInitialStatus.WasSet;
            }   // TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
            else
            {
                return FileAttributeSet ( pfi , FileAttributes.System );
            }   // FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
        }   // FileAttributeSystemReinstate


        /// <summary>
        /// Set the System FileAttributes on a file.
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <returns>
        /// If the specified bit (It's usually just one.) is set on entry, the
        /// return value is WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeSystemSet ( this FileInfo pfi )
        {
            return FileAttributeSet ( pfi , FileAttributes.System );
        }
        #endregion  // Extension Methods for the System Attribute


        #region Extension Methods for Any File Attribute
        /// <summary>
        /// Clear the specified FileAttributes bit(s).
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <param name="penmFileAttributes">
        /// This argument specifies the member(s) of the FileAttributes bitwise
        /// enumeration to clear.
        /// </param>
        /// <returns>
        /// If the specified bit (It's usually just one.) is set on entry, the
        /// return value is WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeClear (
            this FileInfo pfi ,
            FileAttributes penmFileAttributes )
        {
            if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
            {   // The specified attribute is set; clear it, and report that it was set.
                pfi.Attributes = pfi.Attributes & ( ~penmFileAttributes );
                return FileInfoExtension.enmInitialStatus.WasSet;
            }   // TRUE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
            else
            {   // The specified attribute is cleared; return without taking any action beyond reporting.
                return FileInfoExtension.enmInitialStatus.WasCleared;
            }   // FALSE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
        }   // FileAttributeClear


        /// <summary>
        /// Set the specified FileAttributes bit(s).
        /// </summary>
        /// <param name="pfi">
        /// As an extension method, the first argument must be a reference to
        /// the instance that invoked it, which is used in much the same way as
        /// the implicit this parameter that leads the list of parameters for a
        /// call to any C# instance method.
        /// </param>
        /// <param name="penmFileAttributes">
        /// This argument specifies the member(s) of the FileAttributes bitwise
        /// enumeration to clear.
        /// </param>
        /// <returns>
        /// If the specified bit (It's usually just one.) is set on entry, the
        /// return value is WasSet; otherwise, it is WasCleared.
        /// </returns>
        public static FileInfoExtension.enmInitialStatus FileAttributeSet (
            this FileInfo pfi ,
            FileAttributes penmFileAttributes )
        {
            if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
            {   // The specified bit(s) are set; return without taking any action.
                return FileInfoExtension.enmInitialStatus.WasSet;
            }   // TRUE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
            else
            {   // At least one of the specified bits is cleared; set it, then return.
                pfi.Attributes = pfi.Attributes | penmFileAttributes;
                return FileInfoExtension.enmInitialStatus.WasCleared;
            }   // FALSE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
        }   // FileInfoExtension.enmInitialStatus
        #endregion // Extension Methods for Any File Attribute


        #region ShowFileDetails Extension Methods
        /// <summary>
        /// This is a general-purpose method for reporting details about a file.
        /// </summary>
        /// <param name="pfi">
        /// The framework supplies a reference to the FileInfo instance with
        /// which the call is associated.
        /// </param>
        /// <param name="penmFileDetailsToShow">
        /// Use one or more members of the FileDetailsToShow enumeration to
        /// specify the details to display. The FileDetailsToShow enumeration is
        /// a bit mask, and the enumeration defines the combinations that are
        /// most likely to be useful as additional members.
        /// 
        /// The default value is FileDetailsToShow.Everything.
        /// </param>
        /// <param name="pstrLabel">
        /// The label string is optional and, if specified, appears at the start
        /// of the returned string. String magic is thereafter employed to keep
        /// subsequent lines of text aligned vertically.
        /// 
        /// The default value is NULL, which resolves internally to the empty
        /// string.
        /// </param>
        /// <param name="pfPrefixWithNewline">
        /// If TRUE, the whole string begins with a platform-dependent newline.
        /// Otherwise, the string begins with the label specified by parameter
        /// <paramref name="pstrLabel"/>, if any, followed by the absolute file
        /// name.
        /// 
        /// The default value is FALSE.
        /// </param>
        /// <param name="pfSuffixWithNewline">
        /// If TRUE, the string is terminated with a platform-dependent newline.
        /// Otherwise, the string ends with the last value specified by the
        /// <paramref name="penmFileDetailsToShow"/> bit mask.
        /// 
        /// The default value is FALSE.
        /// </param>
        /// <returns>
        /// The return value is a human-readable multi-line report suitable for
        /// display on a console log, event log, print file, or message box.
        /// </returns>
        public static string ShowFileDetails (
            this FileInfo pfi ,
            FileDetailsToShow penmFileDetailsToShow = FileDetailsToShow.Everything ,
            string pstrLabel = null ,
            bool pfPrefixWithNewline = false ,
            bool pfSuffixWithNewline = false )
        {
            string strCompleteFormatString = Core.Properties.Resources.FILE_DETAILS;
            StringBuilder builder = new StringBuilder (
                strCompleteFormatString.Substring (
                    s_sslName.SubstringStart ,
                    s_sslName.SubstringLength ) ,
                strCompleteFormatString.Length );

            builder.Append (
                strCompleteFormatString.Substring (
                    s_sslWritten.SubstringStart ,
                    s_sslWritten.SubstringLength ) );

            //  -----------------------------------------------------------------
            //  These must be initialized to satisfy the compiler. Though I could
            //  use a null-coalescing operator in the output array constructor, I
            //  suspect that initializing it at compile time is a bit cheaper.
            //  -----------------------------------------------------------------

            string strAccessTime = null;
            string strCreateTime = null;
            string strWriteTime = null;
            string strFileSize = null;
            string strAttributes = null;

            //  -----------------------------------------------------------------
            //  Since the message always contains at least two lines, this is
            //  always used at least once.
            //  -----------------------------------------------------------------

            string strLabelSpacer = string.IsNullOrEmpty ( pstrLabel )
                                    ? SpecialStrings.EMPTY_STRING
                                    : string.Empty.PadRight ( pstrLabel.Length );

            if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
            {
                builder.Append (
                    strCompleteFormatString.Substring (
                        s_sslCreated.SubstringStart ,
                        s_sslCreated.SubstringLength ) );
            }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )

            if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )
            {
                builder.Append (
                    strCompleteFormatString.Substring (
                        s_sslAccessed.SubstringStart ,
                        s_sslAccessed.SubstringLength ) );
            }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )

            if ( ( penmFileDetailsToShow & FileDetailsToShow.Size ) == FileDetailsToShow.Size )
            {
                builder.Append (
                    strCompleteFormatString.Substring (
                        s_sslSize.SubstringStart ,
                        s_sslSize.SubstringLength ) );
                strFileSize = pfi.Length.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D );
            }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.Size ) == FileDetailsToShow.Size )

            if ( ( penmFileDetailsToShow & FileDetailsToShow.Attributes ) == FileDetailsToShow.Attributes )
            {
                builder.Append (
                    strCompleteFormatString.Substring (
                        s_sslAttributes.SubstringStart ,
                        s_sslAttributes.SubstringLength ) );
                strAttributes = string.Format (
                    @"{0} ({1})" ,
                    ( int ) pfi.Attributes ,
                    pfi.Attributes.ToString ( ) );
            }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.Attributes ) == FileDetailsToShow.Attributes )

            if ( ( penmFileDetailsToShow & FileDetailsToShow.AllTimes ) == FileDetailsToShow.AllTimes )
            {
                strWriteTime = Core.TimeDisplayFormatter.PrepareLocalAndUTCTimes (
                    pfi.LastWriteTime ,
                    pfi.LastWriteTimeUtc );

                if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )
                {
                    strAccessTime = Core.TimeDisplayFormatter.PrepareLocalAndUTCTimes (
                        pfi.LastAccessTime ,
                        pfi.LastAccessTimeUtc );
                }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )

                if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                {
                    strCreateTime = Core.TimeDisplayFormatter.PrepareLocalAndUTCTimes (
                    pfi.CreationTime ,
                    pfi.CreationTimeUtc );
                }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
            }   // TRUE block, if ( ( penmFileDetailsToShow & FileDetailsToShow.AllTimes ) == FileDetailsToShow.AllTimes )
            else
            {
                if ( ( penmFileDetailsToShow & FileDetailsToShow.LocalTime ) == FileDetailsToShow.LocalTime )
                {
                    strWriteTime = SysDateFormatters.ReformatSysDate (
                        pfi.LastWriteTime ,
                        SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );

                    if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )
                    {
                        strAccessTime = SysDateFormatters.ReformatSysDate (
                            pfi.LastAccessTime ,
                            SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );
                    }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )

                    if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                    {
                        strCreateTime = SysDateFormatters.ReformatSysDate (
                            pfi.CreationTime ,
                            SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );
                    }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.LocalTime ) == FileDetailsToShow.LocalTime )

                if ( ( penmFileDetailsToShow & FileDetailsToShow.UtcTime ) == FileDetailsToShow.UtcTime )
                {
                    strWriteTime = SysDateFormatters.ReformatSysDate (
                        pfi.LastWriteTimeUtc ,
                        SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );

                    if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )
                    {
                        strAccessTime = SysDateFormatters.ReformatSysDate (
                            pfi.LastAccessTimeUtc ,
                            SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );
                    }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )

                    if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                    {
                        strCreateTime = SysDateFormatters.ReformatSysDate (
                        pfi.CreationTimeUtc ,
                        SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );
                    }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.UtcTime ) == FileDetailsToShow.UtcTime )

                //  ------------------------------------------------------------
                //  Show local time in the absence of a contrary preference.
                //  ------------------------------------------------------------

                if ( ( ( penmFileDetailsToShow & FileDetailsToShow.LocalTime ) != FileDetailsToShow.LocalTime )
                    && ( ( penmFileDetailsToShow & FileDetailsToShow.UtcTime ) != FileDetailsToShow.UtcTime ) )
                {
                    strWriteTime = SysDateFormatters.ReformatSysDate (
                        pfi.LastWriteTime ,
                        SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );

                    if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )
                    {
                        strAccessTime = SysDateFormatters.ReformatSysDate (
                            pfi.LastAccessTime ,
                            SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );
                    }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.AccessedTime ) == FileDetailsToShow.AccessedTime )

                    if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                    {
                        strCreateTime = SysDateFormatters.ReformatSysDate (
                            pfi.CreationTime ,
                            SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS );
                    }   // if ( ( penmFileDetailsToShow & FileDetailsToShow.CreatedTime ) == FileDetailsToShow.CreatedTime )
                }   // TRUE block, if ( ( penmFileDetailsToShow & FileDetailsToShow.AllTimes ) == FileDetailsToShow.AllTimes )
            }   // FALSE block, if ( ( penmFileDetailsToShow & FileDetailsToShow.AllTimes ) == FileDetailsToShow.AllTimes )

            return string.Format (
                builder.ToString ( ) ,                                          // Extract the format control string from the StringBuilder.
                new string [ ]
                {
                    pstrLabel ,                                                 // Format Item 0: {0} Absolute Name
                    pfi.FullName ,                                              // Format Item 1: Absolute Name = {1}
                    strWriteTime ,                                              // Format Item 2: Modified Date = {2}
                    strCreateTime ,                                             // Format Item 3: Date Created  = {3}
                    strAccessTime ,                                             // Format Item 4: Date Accessed = {4}
                    strFileSize ,                                               // Format Item 5: Size in bytes = {5}
                    strAttributes ,                                             // Format Item 6: Attributes    = {6}
                    strLabelSpacer ,                                            // Format Item 7: Spacer preceding each of the above items 1 through 6
                    Environment.NewLine ,                                       // Format Item 8: platform-dependent newline following each of the above items 1 through 6
                    pfPrefixWithNewline ? Environment.NewLine : string.Empty ,  // Format Item 9: Prefix is either a newline or the empty string
                    pfSuffixWithNewline ? Environment.NewLine : string.Empty    // Format Item 10: Suffix is either a newline or the empty string
                } );
        }   // public static string ShowFileDetails
        #endregion  // ShowFileDetails Extension Methods
    }   // public static class FileInfoExtensionMethods
}   // partial namespace WizardWrx