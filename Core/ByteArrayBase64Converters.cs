/*
    ============================================================================

	Project Name:       CSharp_Lab

    File Name:          ByteArrayBase64Converters.cs

    Class Name:         ByteArrayBase64Converters

    Namespace Name:     CSharp_Lab

	Project Synopsis:   This class is part of a Visual C# laboratory project,
	                    which I use to develop and test new coding techniques
						and classes.

    Class Synopsis:     This class implements Base64 encoding and decoding to
                        and from byte arrays.

    Author:             David A. Gray

    Date Created:       Sunday, 11 July 2021

    License:            Copyright (C) 2021, David A. Gray.
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
    ---------- ------- --- -----------------------------------------------------
	2021/07/11 7.6     DAG This class makes its debut, composed of code moved
                           from Program.cs, in which it proved itself.
    ============================================================================
*/


using System;


namespace WizardWrx.Core
{
    /// <summary>
    /// This static class exposes methods that efficiently encode binary data as
    /// Base64 byte arrays and converts such byte arrays into binary data in the
    /// form of a new byte array or binary data written into a disk file.
    /// </summary>
    public static class ByteArrayBase64Converters
    {
        /// <summary>
        /// Read an input file into a byte array of Base64 encoded characters
        /// that represents its contents in a form that can be included in a
        /// MIME encoded email message.
        /// </summary>
        /// <param name="pstrInputFileName">
        /// Pass in the absolute or relative (to the Current Working Directory)
        /// name of the file to read and encode into a byte array that represents
        /// the Base64 encoded characters.
        /// </param>
        /// <returns>
        /// The return value is a byte array that contains 4 characters for each
        /// three bytes read from the input file.
        /// </returns>
        public static byte [ ] Base64EncodeBinaryFile (
            string pstrInputFileName )
        {
            byte [ ] abytBinaryBytes2Encode = System.IO.File.ReadAllBytes ( pstrInputFileName );
            string strBase64String = Convert.ToBase64String ( abytBinaryBytes2Encode );
            
            return System.Text.Encoding.UTF8.GetBytes ( strBase64String );
        }   // public static byte [ ] Base64EncodeBinaryFile



        /// <summary>
        /// Decode byte array <paramref name="pabytBase64Characters"/> and write
        /// the decoded bytes into file <paramref name="pstrFileName"/>.
        /// </summary>
        /// <param name="pstrFileName">
        /// Specify the name (absolute or relative to the current working
        /// directory) of the file into which to write the decoded bytes
        /// represented by <paramref name="pabytBase64Characters"/>.
        /// </param>
        /// <param name="pabytBase64Characters">
        /// Pass in a reference to the array of Base64 encoded bytes to be
        /// decoded and saved into file <paramref name="pstrFileName"/>.
        /// </param>
        public static void Base64DecodeByteArray2File (
            string pstrFileName ,
            byte [ ] pabytBase64Characters  )
        {
            System.IO.File.WriteAllBytes (
                pstrFileName ,                                                  // string   path
                Base64DecodeByteArray (                                         // byte [ ] bytes
                    pabytBase64Characters ) );                                  // byte [ ] pabytBase64Characters
        }   // public static void Base64DecodeByteArray2File


        /// <summary>
        /// Decode a byte array that represents a set of Base64 encoded
        /// characters, returning a new byte array containing their decoded
        /// representation.
        /// </summary>
        /// <param name="pabytBase64Characters">
        /// Pass in a reference to the array of Base64 encoded bytes to be
        /// decoded and returned.
        /// </param>
        /// <returns>
        /// The return value is the byte array containing the Base64 decoded
        /// bytes. Expect an array containing 3/4 as many elements as the input.
        /// </returns>
        public static byte [ ] Base64DecodeByteArray (
            byte [ ] pabytBase64Characters )
        {
            int intByteCount = pabytBase64Characters.Length;
            char [ ] achrOutputBytes = new char [ intByteCount ];
            
            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < intByteCount ;
                      intJ++ )
            {
                achrOutputBytes [ intJ ] = Convert.ToChar ( pabytBase64Characters [ intJ ] );
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intByteCount ; intJ++ )

            return Convert.FromBase64CharArray (
                achrOutputBytes ,
                ArrayInfo.ARRAY_FIRST_ELEMENT ,
                achrOutputBytes.Length );
        }   // internal static byte [ ] Base64DecodeByteArray
    }   // public static class ByteArrayBase64Converters
}   // partial namespace WizardWrx.Core