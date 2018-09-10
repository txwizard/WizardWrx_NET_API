/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         MagicBooleans

    File Name:          MagicBooleans.cs

    Synopsis:           Define a handful of frequently used Boolean values whose
                        correct values are easier to remember with the help of a
                        mnemonic.

    Remarks:			This class implements all Boolean constants defined in
                        WizardWrx.MagicNumbers and exposed by class library
                        WizardWrx.SharetUtl2. Most of the other constants have
                        migrated to other classes in this library.

						As of version 6.3, all but the first two are synonyms
						of constants also in WizardWrx.DLLServices2.FileIOFlags;
                        they shall remain, at least for the time being, to avoid
						breaking existing code. However, thee definitions may be
						kept indefinitely, for the same reason that 
						DisplayFormats exists alongside NumericFormats.

						The XML documentation in this class incorporates cross
						references to classes defined in other modules that are
						compiled into the same assembly. A couple of hours has
						yielded the following lessons.

						1)	You can refer to any other class in the assembly, if
							you fully qualify it with respect to the namespace
							declared at the top of the same source module.

						2)	All references are subject to the above rule:

								- Argument types used to disambiguate overloads
								- Method names
								- Argument types used to disambiguate overloads
								- Named constants

							Examples:

								These don't work.

									GetReservedErrorMessage(ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(ExceptionLogger.ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(DLLServices2.ExceptionLogger.ErrorExitOptions)

								This works.

									DLLServices2.ExceptionLogger.GetReservedErrorMessage(DLLServices2.ExceptionLogger.ErrorExitOptions)

							Notwithstanding assertions in the documentation that
							these cross references respect using irectiv4es when
							resolving, it appears that this courtesy may exclude
							references to the current assembly (the one being
							built), which makes sense, upon reflection, since it
							is in a state of flux, and has yet to be persisted.

						3)	See and SeeAlso XML tags can refer to Web pages, but
							the correct keyword is not CREF, but HREF.

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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2015/06/23 5.5     DAG    Move the Boolean constants from StandardConstants,
                              and add a couple of new ones for use with the
                              static Enum.Parse method.
    2015/09/01 5.7     DAG    Correct spelling errors flagged by the spelling
                              checker add-in that I recently installed into the
                              Visual Studio 2013 IDE, along with a documentation
                              XML error that I overlooked.

	2016/06/04 6.2     DAG    Resolve ambiguous and unreachable cross references
							  tags in the XML documentation.
 
    2016/06/07 6.3     DAG    Correct technical errors uncovered while preparing
                              this library for publication on GitHub, and turn
                              the constants for working with streams into
                              synonyms of the constants in FileIOFlags, which
                              are slightly less accessible, since they belong to
                              a subsidiary namespace.

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
	/// This class defines frequently used Boolean values whose correct values
	/// are easier to remember with the help of a mnemonic.
	/// 
	/// This class is one of a constellation of static classes that define a
	/// wide variety of symbolic constants that I use to make my code easier to
	/// understand when I need a refresher or am about to change it.
	/// </summary>
	/// <remarks>
	/// The constants defined herein are pairs. The first part of the name of
	/// each pair associates it with the method or constructor with which it is
	/// intended to be used. The remainder of the name identifies the behavior
	/// elicited from the object or method by specifying this value.
	/// </remarks>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="ListInfo"/>
	/// <seealso cref="FileIOFlags"/>
	/// <seealso cref="PathPositions"/>
	/// <seealso cref="SpecialCharacters"/>
	public static class MagicBooleans
	{
		/// <summary>
		/// Use this constant as the third (ignoreCase) argument of the static
		/// Enum.Parse method to cause the evaluation to be case INsensitive.
		/// </summary>
		/// <see href="!:https://msdn.microsoft.com/en-us/library/vstudio/kxydatf9(v=vs.80).aspx"/>
		/// <seealso cref="ENUM_PARSE_CASE_SENSITIVE"/>
		//	----------------------------------------------------------------------------------------------------------------------------------
		//	Reference:	"C# XML Documentation Website Link," http://stackoverflow.com/questions/6960426/c-sharp-xml-documentation-website-link
		//	----------------------------------------------------------------------------------------------------------------------------------
		public const bool ENUM_PARSE_CASE_INSENSITIVE = true;

		/// <summary>
		/// Use this constant as the third (ignoreCase) argument of the static
		/// Enum.Parse method to cause the evaluation to be case sensitive. This
		/// is the default behavior of Enum.Parse exhibited by the two-argument
		/// overload of this method.
		/// </summary>
		/// <see href="!:https://msdn.microsoft.com/en-us/library/vstudio/essfb559(v=vs.80).aspx"/>
		/// <seealso cref="ENUM_PARSE_CASE_INSENSITIVE"/>
		//	----------------------------------------------------------------------------------------------------------------------------------
		//	Reference:	"C# XML Documentation Website Link," http://stackoverflow.com/questions/6960426/c-sharp-xml-documentation-website-link
		//	----------------------------------------------------------------------------------------------------------------------------------
		public const bool ENUM_PARSE_CASE_SENSITIVE = false;

		/// <summary>
		/// Use with the third argument of the static Copy method of the File 
		/// class to explicitly forbid file overwriting. See Remarks.
		/// </summary>
		/// <remarks>
		/// If you NEVER want overwriting, use the default (two-argument)
		/// form of the Copy method.
		/// </remarks>
		/// <seealso cref="FILE_COPY_OVERWRITE_PERMITTED"/>
		public const bool FILE_COPY_OVERWRITE_FORBIDDEN = WizardWrx.FileIOFlags.FILE_COPY_OVERWRITE_FORBIDDEN;

		/// <summary>
		/// Use with the third argument of the static Copy method of the File 
		/// class to enable file overwriting, which is forbidden by default.
		/// </summary>
		/// <seealso cref="FILE_COPY_OVERWRITE_FORBIDDEN"/>
		public const bool FILE_COPY_OVERWRITE_PERMITTED = WizardWrx.FileIOFlags.FILE_COPY_OVERWRITE_PERMITTED;

		/// <summary>
		/// Use this symbolic constant to set the append argument to an
		/// overloaded StreamWriter constructor, to cause it to append to a file
		/// if one exists.
		/// </summary>
		/// <seealso cref="FILE_OUT_CREATE"/>
		public const bool FILE_OUT_APPEND = WizardWrx.FileIOFlags.FILE_OUT_APPEND;

		/// <summary>
		/// Use this symbolic constant to set the append argument to an
		/// overloaded StreamWriter constructor, to cause it to overwrite a file
		/// if one exists.
		/// </summary>
		/// <seealso cref="FILE_OUT_APPEND"/>
		public const bool FILE_OUT_CREATE = WizardWrx.FileIOFlags.FILE_OUT_CREATE;

		/// <summary>
		/// Use this symbolic constant to set the useAsync argument to a
		/// FileStream constructor to TRUE, allowing I/O to be asynchronous.
		/// </summary>
		/// <seealso cref="MAKE_STREAM_IO_SYNCHRONOUS"/>
		public const bool MAKE_STREAM_IO_ASYNCHRONOUS = WizardWrx.FileIOFlags.MAKE_STREAM_IO_ASYNCHRONOUS;

		/// <summary>
		/// Use this symbolic constant to set the useAsync argument to a
		/// FileStream constructor to FALSE, allowing I/O to be synchronous,
		/// which is the default.
		/// </summary>
		/// <seealso cref="MAKE_STREAM_IO_ASYNCHRONOUS"/>
		public const bool MAKE_STREAM_IO_SYNCHRONOUS = WizardWrx.FileIOFlags.MAKE_STREAM_IO_SYNCHRONOUS;
	}	// public static class MagicBooleans
}	// partial namespace WizardWrx