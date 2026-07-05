/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         FileIOFlags

    File Name:          FileIOFlags.cs

    Synopsis:           Use these flags to document stream I/O method calls.

    Remarks:            This class consists entirely of constants.
 
						Though it is exposed at the root level of the WizardWrx
						namespace, this class is implemented by a separate
						assembly, WizardWrx.Core.dll. Keeping the class in the
						root namespace is for backwards compatibility; there are
						too many references that would break if it were moved.


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

    Created:            Sunday, 14 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/14 5.2     DAG    Initial implementation.

	2015/06/09 5.5     DAG    Relocate to WizardWrx.DLLServices2 namespace and
                              class library.

    2016/06/03 6.2     DAG    Add flags for Byte Order Mark checks when opening
                              text files for input, and convert everything from
                              static to const storage class, and expand the
                              documentation of most of the flags, to save trips
                              to the MSDN library.

	2017/02/19 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

                              The only substantive change to this library is its
                              promotion to the root WizardWrx namespace and its
                              relocation to WizardWrx.Common.dll.
    ============================================================================
*/


using System;

namespace WizardWrx
{
    /// <summary>
    /// Use these flags to document stream I/O method calls.
    /// </summary>
    public static class FileIOFlags
    {
        /// <summary>
        /// File copy and stream I/O operations on existing files must fail.
		/// 
		/// This is the default behavior, and an existing file raises an
		/// IOException exception.
        /// </summary>
		/// <seealso cref="FILE_COPY_OVERWRITE_PERMITTED"/>
        public const bool FILE_COPY_OVERWRITE_FORBIDDEN = false;

        /// <summary>
        /// File copy and stream I/O operations on existing files overwrite.
        /// </summary>
		/// <seealso cref="FILE_COPY_OVERWRITE_FORBIDDEN"/>
        public const bool FILE_COPY_OVERWRITE_PERMITTED = true;


		/// <summary>
		/// When opening a text file for input, use the Byte Order Mark, if 
		/// present, to establish its encoding.
		/// 
		/// The documentation in the MSDN library describes this parameter as
		/// follows.
		/// 
		/// "The detectEncodingFromByteOrderMarks parameter detects the encoding
		/// by looking at the first three bytes of the stream. It automatically
		/// recognizes UTF-8, little-endian Unicode, and big-endian Unicode text
		/// if the file starts with the appropriate byte order marks. Otherwise,
		/// the UTF8Encoding is used."
		/// 
		/// The documentation implies, without explicitly saying so, the text is
		/// assumed to be UTF-8 encoded unless you check for a Byte Order Mark,
		/// one is present, and it indicates otherwise.
		/// </summary>
		/// <seealso cref="FILE_READ_ENCODING_IGNORE_BOM"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/9y86s1a9(v=vs.110).aspx"/>
		public const bool FILE_READ_ENCODING_CHECK_FOR_BOM = true;

		/// <summary>
		/// When opening a text file for input, ignore (expect none) the Byte
		/// Order Mark to establish its encoding.
		/// 
		/// The documentation in the MSDN library describes this parameter as
		/// follows.
		/// 
		/// "The detectEncodingFromByteOrderMarks parameter detects the encoding
		/// by looking at the first three bytes of the stream. It automatically
		/// recognizes UTF-8, little-endian Unicode, and big-endian Unicode text
		/// if the file starts with the appropriate byte order marks. Otherwise,
		/// the UTF8Encoding is used."
		/// 
		/// The documentation implies, without explicitly saying so, the text is
		/// assumed to be UTF-8 encoded unless you check for a Byte Order Mark,
		/// one is present, and it indicates otherwise.
		/// </summary>
		/// <seealso cref="FILE_READ_ENCODING_CHECK_FOR_BOM"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/9y86s1a9(v=vs.110).aspx"/>
		/// <seealso cref="MAKE_STREAM_IO_ASYNCHRONOUS"/>
		/// <seealso cref="MAKE_STREAM_IO_SYNCHRONOUS"/>
		public const bool FILE_READ_ENCODING_IGNORE_BOM = false;


		/// <summary>
        /// Opening an output stream on a file that exists extends the file. The
		/// file is created if it doesn't exist.
		/// 
		/// The documentation implies, without explicitly saying so, that the
		/// default is overwrite.
		/// </summary>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/0wf7ab94(v=vs.110).aspx"/>
		/// <seealso cref="FILE_OUT_CREATE"/>
		/// <seealso cref="MAKE_STREAM_IO_ASYNCHRONOUS"/>
		/// <seealso cref="MAKE_STREAM_IO_SYNCHRONOUS"/>
		public const bool FILE_OUT_APPEND = true;

        /// <summary>
        /// Opening an output stream on a file that exists overwrites the file.
        /// Otherwise, a new file is created.
		/// 
		/// The documentation implies, without explicitly saying so, that the
		/// default is overwrite.
		/// </summary>
		/// <seealso cref="FILE_OUT_APPEND"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/0wf7ab94(v=vs.110).aspx"/>
		/// <seealso cref="MAKE_STREAM_IO_ASYNCHRONOUS"/>
		/// <seealso cref="MAKE_STREAM_IO_SYNCHRONOUS"/>
		public const bool FILE_OUT_CREATE = false;


        /// <summary>
        /// Open stream for asynchronous I/O.
        /// </summary>
		/// <seealso cref="FILE_OUT_APPEND"/>
		/// <seealso cref="FILE_OUT_CREATE"/>
		/// <seealso cref="MAKE_STREAM_IO_SYNCHRONOUS"/>
        public const bool MAKE_STREAM_IO_ASYNCHRONOUS = true;

        /// <summary>
        /// Open stream for synchronous I/O. This is the default.
        /// </summary>
		/// <seealso cref="MAKE_STREAM_IO_ASYNCHRONOUS"/>
        public const bool MAKE_STREAM_IO_SYNCHRONOUS = false;
    }   // public static class FileIOFlags
}   // partial namespace WizardWrx