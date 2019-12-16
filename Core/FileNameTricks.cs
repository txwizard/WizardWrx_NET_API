/*
    ============================================================================

    Namespace Name:		WizardWrx

    Class Name:			FileNameTricks

    File Name:			FileNameTricks.cs

    Synopsis:			This class exposes static methods for manipulating file
                        names. Unlike the objects in the System.File namespace,
                        these methods don't need a real file object. All work on
                        strings that represent file names.

    Remarks:            Though it is exposed at the root level of the WizardWrx
                        namespace, this class is implemented by a separate
                        assembly, WizardWrx.Core.dll. Keeping the class in the
                        root namespace is for backwards compatibility; there are
                        too many references that would break if it were moved.

                        Beginning with version 2.46 of this class, the build and
                        revision numbers are controlled by the build engine.

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

    Date       Version By  Description
    ---------- ------- --- --------------------------------------------------
    2008/06/12 2.0.7.0 DAG Initial build, tested as part of developing a
                           one-off console program, TitleLinksFixups.

    2010/04/04 2.51    DAG 1) Add missing bits of standard documentation to the
                              heading.

                           2) Change the access modifier from sealed to static,
                              which implies sealed, AND signals the C# compiler
                              to flag creation of instance variables as fatal
                              errors.

    2011/11/19 2.61    DAG Add aliases for the backslash functions with names
                           that match those of the equivalent ShlWAPI functions.

                              Original Method             New Method
                              --------------------------  -------------------
                              EnsureHasTerminalBackslash  PathAddBackslash
                              EnsureNoTerminalBackslash   PathRemoveBackslash

                           To give the aliases the edge, they execute the
                           code that was in the original two. Since PathFixup
                           condenses a request for either into one method call,
                           I changed it to call the aliases.

                           The original methods, now a shade less efficient, are
                           deprecated.

                           In the course of reviewing this code, I found and
                           fixed a few errors that made the documentation a tad
                           confusing.

    2011/11/28 2.63    DAG Add PathMakeRelative method.

    2016/05/20 6.1     DAG Move to WizardWrx.DllServices2 namespace and class
                           library, making its debut in version 6.1, keeping its
                           spot in the root of the WizardWrx hierarchy, so that
                           existing code "just works" when bound to this library.
                           Relocation also brings this module under my
                           three-clause BSD license, and the error message text
                           constants are replaced with resource strings, paving
                           the way for localization.

    2017/03/25 7.0     DAG Break this library apart, so that smaller subsets of
                           classes can be distributed independently.

    2017/06/24 7.0     DAG Define an improved PathAddBackslash, keeping the old
                           one, renamed PathAddBackslashDeprecated1, for
                           benchmarking, along with another revision,
                           PathAddBackslashDeprecated2, that interpolates the
                           system property into the string, along with the name.
 
    2017/06/29 7.0     DAG Define constants from which to construct properly
                           documented wild card filename masks in code.

    2017/07/16 7.0     DAG Replace references to string.empty, which is not a
                           true constant, with SpecialStrings.EMPTY_STRING,
                           which is one.

    2019/12/16 7.23    DAG Allow the tab consistency add-in to replace tabs with
                           spaces. The code is otherwise unchanged, although the
                           new build is required to add a binding redirect, and
                           the version numbering transitions to the SemVer
                           scheme.
    ============================================================================
*/

using System;
using System.IO;

namespace WizardWrx
{
    /// <summary>
    /// This class exposes numerous static methods for manipulating file names.
    /// Unlike the objects in the System.File namespace, these methods don't 
    /// need a real file object. All work on strings that represent file names.
    /// </summary>
    public static class FileNameTricks
    {
        #region Public Constants and Enumerations
        /// <summary>
        /// Use a member of this enumeration with FileDirName and PathFixup, to
        /// specify whether you want a path string with or without a trailing
        /// backslash.
        /// </summary>
        /// <see cref="FileDirName"/>
        /// <see cref="PathFixup"/>
        public enum TerminaBackslash
        {
            /// <summary>
            /// Include the trailing backslash, for example, if you intend to
            /// append another string containing a relative path name or an
            /// unqualified file name.
            /// </summary>
            Include,
            /// <summary>
            /// Exclude the trailing backslash, for example, if you intend to
            /// display the name in an unambiguous context.
            /// </summary>
            Omit,
        }	// TerminaBackslash


        /// <summary>
        /// Use this to insert an extension delimiter into a file specification,
        /// since there seems to be nothing in the System.IO namespace.
        /// </summary>
        /// <remarks>
        /// Strangely, I haven't found a property that returns the extension delimiter.
        /// </remarks>
        /// <seealso cref="OS_DRIVE_PATH_DELIMITER"/>
        /// <seealso cref="UNC_SERVER_DELIM"/>
        /// <seealso cref="OS_WILD_CARD_MULTIPLE"/>
        /// <seealso cref="OS_WILD_CARD_SINGLE"/>
        public const char OS_EXTENSION_DELIM = '.';

        /// <summary>
        /// </summary>
        /// Use this to insert a multiple-character wild card character into a
        /// Windows or Unix file name mask.
        /// <remarks>
        /// There doesn't appear to be any provision for generating these from anything
        /// in the System.IO namespace.
        /// </remarks>
        /// <seealso cref="OS_WILD_CARD_SINGLE"/>
        /// <seealso cref="OS_EXTENSION_DELIM"/>
        /// <seealso cref="OS_DRIVE_PATH_DELIMITER"/>
        /// <seealso cref="UNC_SERVER_DELIM"/>
        public const char OS_WILD_CARD_MULTIPLE = SpecialCharacters.ASTERISK;

        /// <summary>
        /// Use this to insert a multiple-character wild card character into a
        /// Windows or Unix file name mask.
        /// </summary>
        /// <remarks>
        /// There doesn't appear to be any provision for generating these from anything
        /// in the System.IO namespace.
        /// </remarks>
        /// <seealso cref="OS_WILD_CARD_MULTIPLE"/>
        /// <seealso cref="OS_EXTENSION_DELIM"/>
        /// <seealso cref="OS_DRIVE_PATH_DELIMITER"/>
        /// <seealso cref="UNC_SERVER_DELIM"/>
        public const char OS_WILD_CARD_SINGLE = SpecialCharacters.QUESTION_MARK;


        /// <summary>
        /// Along with the OS extension delimiter, the sequence of volume
        /// separator, followed immediately by a directory separator, is
        /// undefined, though one could easily be constructed at run time,
        /// although such a construction wouldn't qualify as a constant.
        /// </summary>
        /// <seealso cref="OS_WILD_CARD_MULTIPLE"/>
        /// <seealso cref="OS_WILD_CARD_SINGLE"/>
        /// <seealso cref="OS_EXTENSION_DELIM"/>
        /// <seealso cref="UNC_SERVER_DELIM"/>
        public const string OS_DRIVE_PATH_DELIMITER = @":\";


        /// <summary>
        /// The server delimiter string.
        /// </summary>
        /// <seealso cref="OS_DRIVE_PATH_DELIMITER"/>
        /// <seealso cref="OS_EXTENSION_DELIM"/>
        /// <seealso cref="OS_WILD_CARD_MULTIPLE"/>
        /// <seealso cref="OS_WILD_CARD_SINGLE"/>
        public const string UNC_SERVER_DELIM = @"\\";
        #endregion	// Public Constants and Enumerations


        #region Public Static Methods
        /// <summary>
        /// Ensure that a path string has NO terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String to evaluate and change if needed.
        /// </param>
        /// <returns>
        /// Path string that is guaranteed to HAVE a terminal backslash.
        /// </returns>
        /// <remarks>
        /// This method is deprecated. Use PathAddBackslash.
        /// </remarks>
        public static string EnsureHasTerminalBackslash (
            string pstrInputPath )
        {
            return PathAddBackslash ( pstrInputPath );
        }   // public static string EnsureHasTerminalBackslash


        /// <summary>
        /// Ensure that a path string has a terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String to evaluate and change if needed.
        /// </param>
        /// <returns>
        /// Path string that is guaranteed NOT to have a terminal backslash.
        /// </returns>
        /// <remarks>
        /// This method is deprecated. Use PathRemoveBackslash.
        /// </remarks>
        public static string EnsureNoTerminalBackslash (
            string pstrInputPath )
        {
            return PathRemoveBackslash ( pstrInputPath );
        }   // public static string EnsureNoTerminalBackslash


        /// <summary>
        /// Ensure that a path string has NO terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String to evaluate and change if needed.
        /// </param>
        /// <returns>
        /// Path string that is guaranteed to HAVE a terminal backslash.
        /// </returns>
        public static string PathAddBackslash (	string pstrInputPath )
        {
            if ( string.IsNullOrEmpty ( pstrInputPath ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRINPUTPATH );
            else if ( pstrInputPath.EndsWith ( Path.DirectorySeparatorChar.ToString ( ) ) )
                return pstrInputPath;
            else
                return string.Concat (
                    pstrInputPath ,
                    Path.DirectorySeparatorChar );
        }   // public static method PathAddBackslash


        /// <summary>
        /// Ensure that a path string has NO terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String to evaluate and change if needed.
        /// </param>
        /// <returns>
        /// Path string that is guaranteed to HAVE a terminal backslash.
        /// </returns>
        public static string PathAddBackslashDeprecated1 (
            string pstrInputPath )
        {
            const string NEW_FORMAT = "{0}\\";

            if ( string.IsNullOrEmpty ( pstrInputPath ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRINPUTPATH );

            if ( pstrInputPath.EndsWith ( Path.DirectorySeparatorChar.ToString ( ) ) )
                return pstrInputPath;
            else
                return string.Format (
                    NEW_FORMAT ,
                    pstrInputPath );
        }   // public static method PathAddBackslashDeprecated1


        /// <summary>
        /// Ensure that a path string has NO terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String to evaluate and change if needed.
        /// </param>
        /// <returns>
        /// Path string that is guaranteed to HAVE a terminal backslash.
        /// </returns>
        public static string PathAddBackslashDeprecated2 (
            string pstrInputPath )
        {
            const string NEW_FORMAT = "{0}{1}";

            if ( string.IsNullOrEmpty ( pstrInputPath ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRINPUTPATH );
            else if ( pstrInputPath.EndsWith ( Path.DirectorySeparatorChar.ToString ( ) ) )
                return pstrInputPath;
            else
                return string.Format (
                    NEW_FORMAT ,
                    pstrInputPath ,
                    Path.DirectorySeparatorChar );
        }   // public static method PathAddBackslashDeprecated2



        /// <summary>
        /// Given a path, such as the fully qualified name of a resource DLL, and
        /// a second fully qualified name, such as that of the program directory
        /// or current working directory, compute a relative path to the first
        /// named path.
        /// </summary>
        /// <param name="pstrFQPath">
        /// This string is the fully qualified path for which a relative path is
        /// required. This string may be the name of a file or a directory, but
        /// it must exist in the file system.
        /// </param>
        /// <param name="pstrDirectoryRelativeTo">
        /// This string is the fully qualified path relative to which a path to
        /// pstrFQPath is required. This string may be the name of a file or a
        /// directory, but it must exist in the file system. Whether you supply
        /// a file name or that of its directory, the outcome is the same, since
        /// this has no effect on the relative path to pstrFQPath.
        /// </param>
        /// <returns>
        /// The returned string is a relative path string, ready for use.
        /// </returns>
        /// <remarks>
        /// This method uses methods of the System.Uri class to perform its path
        /// math, taking advantage of the fact that a path in the file system is
        /// a valid URI.
        /// </remarks>
        public static string PathMakeRelative (
            string pstrFQPath ,
            string pstrDirectoryRelativeTo )
        {
            /*
                ----------------------------------------------------------------
                References: "Getting Path Relative to the Current Working Directory,"
                            http://stackoverflow.com/questions/703281/getting-path-relative-to-the-current-working-directory
                ----------------------------------------------------------------
            */

            const string ARG1_NAME = @"pstrFQPath";
            const string ARG2_NAME = @"pstrDirectoryRelativeTo";

            if ( string.IsNullOrEmpty ( pstrFQPath ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG1_NAME );

            if ( string.IsNullOrEmpty ( pstrDirectoryRelativeTo ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG2_NAME );

            Uri uriFPath = null;
            Uri uriDirectoryRelativeTo = null;

            //  ----------------------------------------------------------------
            //  These two try/catch blocks could have been replaced by unguarded
            //  calls to the static TryParse method of the Uri class. Calling a
            //  constructor within a try/catch block should yield more useful
            //  diagnostic information.
            //  ----------------------------------------------------------------

            try
            {
                uriFPath = new Uri ( pstrFQPath );
            }
            catch ( Exception exUriNew1 )
            {
                throw new ArgumentException (
                    string.Format (
                        WizardWrx.Common.Properties.Resources.ERRMSG_BADSTRING ,
                        pstrFQPath ) ,
                    ARG1_NAME ,
                    exUriNew1 );
            }

            try
            {

                uriDirectoryRelativeTo = new Uri ( pstrDirectoryRelativeTo );
            }
            catch ( Exception exUriNew2 )
            {
                throw new ArgumentException (
                    string.Format (
                        WizardWrx.Common.Properties.Resources.ERRMSG_BADSTRING ,
                        pstrFQPath ) ,
                    ARG1_NAME ,
                    exUriNew2 );
            }

            //  ----------------------------------------------------------------
            //  At this point, both URIs should be initialized. The magic comes
            //  down to this single function call, which initially may seem too
            //  complex to grasp.
            //
            //  1)  Given a path, such as that of a satellite DLL, represented
            //      as a URI, and another path, such as that of the current
            //      working directory, represented as a second URI, create a
            //      relative path to the resource by calling the MakeRelativeUri
            //      instance method on the first path.
            //
            //  2)  The return value is a new URI, whose default ToString method
            //      returns its string representation.
            //
            //  3)  The string representation of the relative path is then fed
            //      to the static UnescapeDataString method of the Uri class,
            //      which substitutes spaces for the "%20" substrings that took
            //      their place when the URI was constructed.
            //
            //  4)  The string returned by UnescapeDataString is fed through its
            //      Replace method, to replace the URI path delimiters with the
            //      required OS path delimiter.
            //  ----------------------------------------------------------------

            return Uri.UnescapeDataString (
                uriDirectoryRelativeTo.MakeRelativeUri ( uriFPath ).ToString ( ) ).Replace (
                    System.IO.Path.AltDirectorySeparatorChar ,
                    System.IO.Path.DirectorySeparatorChar );
        }   // public static string PathMakeRelative

        
        /// <summary>
        /// Ensure that a path string has a terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String to evaluate and change if needed.
        /// </param>
        /// <returns>
        /// Path string that is guaranteed NOT to have a terminal backslash.
        /// </returns>
        public static string PathRemoveBackslash (
            string pstrInputPath )
        {
            if ( string.IsNullOrEmpty ( pstrInputPath ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRINPUTPATH );

            if ( pstrInputPath.EndsWith ( Path.DirectorySeparatorChar.ToString ( ) ) )
                return pstrInputPath.Substring (
                    MagicNumbers.STRING_SUBSTR_BEGINNING ,
                    pstrInputPath.Length - Path.DirectorySeparatorChar.ToString ( ).Length );
            else
                return pstrInputPath;
        }   // private static method PathRemoveBackslash


        /// <summary>
        /// Extract the directory name from a fully qualified file name.
        /// </summary>
        /// <param name="pstrFQFN">
        /// String containing file name to evaluate.
        /// </param>
        /// <param name="penmTerminaBackslash">
        /// A member of the TerminaBackslash, which specifies whether the
        /// returned string should have a terminal backslash.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the directory name
        /// stripped of its file name.
        /// </returns>
        /// <see cref="TerminaBackslash"/>
        public static string FileDirName (
            string pstrFQFN ,
            TerminaBackslash penmTerminaBackslash )
        {
            if ( string.IsNullOrEmpty ( pstrFQFN ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRFQFN );

            if ( pstrFQFN.IndexOf ( Path.DirectorySeparatorChar ) == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            return PathFixup (
                pstrFQFN.Substring (
                    MagicNumbers.STRING_SUBSTR_BEGINNING ,
                    pstrFQFN.LastIndexOf ( Path.DirectorySeparatorChar ) ) ,
                penmTerminaBackslash );
        }   // public static method FileDirName


        /// <summary>
        /// Given a string that contains a partially or fully qualified file
        /// name, return the extension, without the delimiting dot.
        /// </summary>
        /// <param name="pstrFQFN">
        /// File name string to evaluate.
        /// </param>
        /// <returns>
        /// Extension, less the delimiting dot.
        /// </returns>
        public static string FileExtn (
            string pstrFQFN )
        {
            if ( string.IsNullOrEmpty ( pstrFQFN ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRFQFN );

            if ( pstrFQFN.IndexOf ( OS_EXTENSION_DELIM ) == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return SpecialStrings.EMPTY_STRING;

            return pstrFQFN.Substring ( pstrFQFN.LastIndexOf ( OS_EXTENSION_DELIM ) + MagicNumbers.PLUS_ONE );
        }   // pubic static string FileExtn


        /// <summary>
        /// Extract the fully qualified base name, that is, all but the
        /// extension, from a partially or fully qualified file name.
        /// </summary>
        /// <param name="pstrFQFN">
        /// Fully or partially qualified file name to evaluate.
        /// </param>
        /// <returns>
        /// All of pstrFQFN except its extension and extension delimiter.
        /// </returns>
        public static string FQFBasename (
            string pstrFQFN )
        {
            if ( string.IsNullOrEmpty ( pstrFQFN ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRFQFN );

            if ( pstrFQFN.IndexOf ( OS_EXTENSION_DELIM ) == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                return pstrFQFN;

            return pstrFQFN.Substring (
                MagicNumbers.STRING_SUBSTR_BEGINNING ,
                pstrFQFN.LastIndexOf ( OS_EXTENSION_DELIM ) );
        }   // public static method FQFBasename


        /// <summary>
        /// Given a file name and a default directory, which may be a null
        /// reference or an empty string, return a file name that is guaranteed
        /// to be fully qualified.
        /// </summary>
        /// <param name="pstrUQFN">
        /// String which is assumed to be a relative file name.
        /// </param>
        /// <param name="pstrDefaultDir">
        /// String to use as a default path, unless argument pstrUQFN is a fully
        /// qualified file name.
        /// 
        /// If this value is null, or an empty string, the current working
        /// directory is used.
        /// </param>
        /// <returns>
        /// String that contains a string that is guaranteed to represent a
        /// fully qualified file name.
        /// </returns>
        public static string MakeFQFN (
            string pstrUQFN ,
            string pstrDefaultDir )
        {
            const int DRIVE_PATH_DLM_INDEX = 1;
            const string FQFN_TAMPLATE = @"{0}{1}";

            if ( string.IsNullOrEmpty ( pstrUQFN ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRUQFN );

            if ( pstrUQFN.Substring ( DRIVE_PATH_DLM_INDEX , OS_DRIVE_PATH_DELIMITER.Length ) == OS_DRIVE_PATH_DELIMITER )
                return pstrUQFN;

            if ( pstrUQFN.StartsWith ( UNC_SERVER_DELIM ) )
                return pstrUQFN;

            if ( string.IsNullOrEmpty ( pstrDefaultDir ) )
                return string.Format (
                    FQFN_TAMPLATE ,
                    PathFixup (
                        Environment.CurrentDirectory ,
                        TerminaBackslash.Include ) ,
                    pstrUQFN );
            else
                return string.Format (
                    FQFN_TAMPLATE ,
                    PathFixup (
                        pstrDefaultDir ,
                        TerminaBackslash.Include ) ,
                    pstrUQFN );
        }   // public static method MakeFQFN


        /// <summary>
        /// Originally a private method, this method returns a path (directory)
        /// name string that is guaranteed to meet the specified requirement,
        /// with respect to presence or absence of a terminal backslash.
        /// </summary>
        /// <param name="pstrInputPath">
        /// String containing path (directory) name to evaluate.
        /// </param>
        /// <param name="penmBackslash">
        /// A member of the TerminaBackslash, which specifies whether the
        /// returned string should have a terminal backslash.
        /// </param>
        /// <returns>
        /// Path (directory) name string that is guaranteed to either have, or
        /// omit, a terminal backslash, as specified.
        /// </returns>
        /// <see cref="TerminaBackslash"/>
        public static string PathFixup (
            string pstrInputPath ,
            TerminaBackslash penmBackslash )
        {
            if ( string.IsNullOrEmpty ( pstrInputPath ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRINPUTPATH );

            switch ( penmBackslash )
            {
                case TerminaBackslash.Include:
                    return PathAddBackslash ( pstrInputPath );
                case TerminaBackslash.Omit:
                    return PathRemoveBackslash ( pstrInputPath );
                default:
                    return PathAddBackslash ( pstrInputPath );
            }	// switch (penmBackslash)
        }   // public static method PathFixup


        /// <summary>
        /// Given a string that represents the name of a file, extract only the
        /// base name (EXCLUDING the extension).
        /// </summary>
        /// <param name="pstrFQFN">
        /// String to evaluate.
        /// </param>
        /// <returns>
        /// Base name extracted from string. This means the unqualified file
        /// name, less its directory and its extension.
        /// </returns>
        public static string UQFBasename (
            string pstrFQFN )
        {
            if ( string.IsNullOrEmpty ( pstrFQFN ) )
                throw new ArgumentException (
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_NAME_PSTRFQFN );

            if ( pstrFQFN.EndsWith ( Path.DirectorySeparatorChar.ToString ( ) ) )
                return SpecialStrings.EMPTY_STRING;											 // The string names a directory.

            int intLastPathPos = pstrFQFN.LastIndexOf ( Path.DirectorySeparatorChar );
            int intUQBNStart = MagicNumbers.STRING_INDEXOF_NOT_FOUND;			// Initialize to invalid value.

            if ( intLastPathPos == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
                intUQBNStart = MagicNumbers.STRING_SUBSTR_BEGINNING;			// Start from the beginning.
            else
                intUQBNStart = intLastPathPos + MagicNumbers.PLUS_ONE;			// Start from the last path delimiter.

            int intExtensionPos = pstrFQFN.LastIndexOf ( OS_EXTENSION_DELIM );

            if ( intExtensionPos == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
            {   // Name has NO extension.
                return pstrFQFN.Substring ( intUQBNStart );						// The file name has no extension.
            }   // TRUE (degenerate) block, if (intExtensionPos == MagicNumbers.STRING_INDEXOF_NOT_FOUND)
            else
            {   // Name HAS an extension to be discarded.
                int intUQBNLength = intExtensionPos - intUQBNStart;				// The file name has an extension.

                return pstrFQFN.Substring (
                    intUQBNStart ,
                    intUQBNLength );
            }   // FALSE (normal) block, if (intExtensionPos == StandardConstants.STRING_INDEXOF_NOT_FOUND)
        }   // public static method UQFBasename
        #endregion	// Public Static Methods


        #region Private Constants
        const string ARG_NAME_PINTDATEPART = @"pintDatePart";
        const string ARG_NAME_PSTRFQFN = @"pstrFQFN";
        const string ARG_NAME_PSTRINPUTPATH = @"pstrInputPath";
        const string ARG_NAME_PSTRFNPATTERN = @"pstrFNPattern";
        const string ARG_NAME_PSTRPATHIN = @"pstrPathIn";
        const string ARG_NAME_PSTRUQFN = @"pstrUQFN";
        #endregion	// Private Constants
    }   // static class FileNameTricks
}   // partial namespace WizardWrx