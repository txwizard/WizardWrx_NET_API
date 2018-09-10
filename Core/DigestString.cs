/*
    ============================================================================

    Namespace:          namespace WizardWrx.Cryptography

    Class Name:         DigestString

    File Name:          DigestString.cs

    Synopsis:           This static class exposes methods for generating message
                        digests of strings.

    Remarks:            The initial implementation supports only the MD5 hashing
                        algorithm, to meet an immediate need.

                        This class supports the following algorithms.

                        ------------------------------------------------------
                        Algorithm  Strength  Hexadecimal String  Use This
                        Name       in Bits   Length in Bytes     Method
                        ---------  --------  ------------------  -------------
                        MD5        128        32                 Unsupported *
                        SHA-1      160        40                 SHA1Hash *
                        SHA-256    256        64                 SHA256Hash
                        SHA-384    384        96                 SHA384Hash
                        SHA-512    512       128                 SHA512Hash
                        ------------------------------------------------------

                        *   MD5 and SHA-1 are classified as broken and unsafe.
                            Both are retained only for backward compatibility.
                            References in code will cause the C# compiler to
                            emit warning CS0618.

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

    Created:            Sunday, 21 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/21 2.8     DAG    Initial implementation.


    2016/06/12 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, incorporate my three-clause BSD
                              license, and break a dependency upon the local
                              utility class, which is going away.

    2017/08/03 7.0     DAG    Move this class from WizardWrx.SharedUtl4.dll into
                              WizardWrx.Core.dll.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using WizardWrx;


namespace WizardWrx.Cryptography
{
    /// <summary>
    /// Static methods for computing message digests for strings, using the most
    /// common algorithms.
    /// </summary>
    public static class DigestString
    {
        /// <summary>
        /// Given a string, return its SHA-256 message digest as a 64
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="strPlaintext">
        /// Supply a pointer to the string to be digested.
        /// </param>
        /// <returns>
        /// The return value is a message digest, consisting of a string of 64
        /// hexadecimal characters.
        /// </returns>
        public static string SHA256Hash ( string strPlaintext )
        {
            byte [ ] bytePlainText = Encoding.Unicode.GetBytes ( strPlaintext );
            SHA256 SHA256Hasher = new SHA256Managed ( );
            byte [ ] byteDigest = SHA256Hasher.ComputeHash ( bytePlainText );
			string strDigestedPrefix = Core.ByteArrayFormatters.ByteArrayToHexDigitString ( byteDigest );
            return strDigestedPrefix;
        }   // public static string SHA256Hash


        /// <summary>
        /// Given a byte array, return its SHA-256 message digest as a 64
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pabytPlainText">
        /// Supply a pointer to the byte array to be digested.
        /// </param>
        /// <returns>
        /// The return value is a message digest, consisting of a string of 64
        /// hexadecimal characters.
        /// </returns>
        public static string SHA256Hash ( byte [ ] pabytPlainText )
        {
            SHA256 SHA256Hasher = new SHA256Managed ( );
            byte [ ] byteDigest = SHA256Hasher.ComputeHash ( pabytPlainText );
			string strDigestedPrefix = Core.ByteArrayFormatters.ByteArrayToHexDigitString ( byteDigest );
            return strDigestedPrefix;
        }   // public static string SHA256Hash


        /// <summary>
        /// Given a string, return its SHA-384 message digest as a 96
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="strPlaintext">
        /// Supply a pointer to the string to be digested.
        /// </param>
        /// <returns>
        /// The return value is a message digest, consisting of a string of 96
        /// hexadecimal characters.
        /// </returns>
        public static string SHA384Hash ( string strPlaintext )
        {
            byte [ ] bytePlainText = Encoding.Unicode.GetBytes ( strPlaintext );
            SHA384 SHA384Hasher = new SHA384Managed ( );
            byte [ ] byteDigest = SHA384Hasher.ComputeHash ( bytePlainText );
			string strDigestedPrefix = Core.ByteArrayFormatters.ByteArrayToHexDigitString ( byteDigest );
            return strDigestedPrefix;
        }   // public static string SHA384Hash


        /// <summary>
        /// Given a byte array, return its SHA-384 message digest as a 96
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pabytPlainText">
        /// Supply a pointer to the byte array to be digested.
        /// </param>
        /// <returns>
        /// The return value is a message digest, consisting of a string of 96
        /// hexadecimal characters.
        /// </returns>
        public static string SHA384Hash ( byte [ ] pabytPlainText )
        {
            SHA384 SHA384Hasher = new SHA384Managed ( );
            byte [ ] byteDigest = SHA384Hasher.ComputeHash ( pabytPlainText );
			string strDigestedPrefix = Core.ByteArrayFormatters.ByteArrayToHexDigitString ( byteDigest );
            return strDigestedPrefix;
        }   // public static string SHA384Hash


        /// <summary>
        /// Given a string, return its SHA-512 message digest as a 128
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="strPlaintext">
        /// Supply a pointer to the string to be digested.
        /// </param>
        /// <returns>
        /// The return value is a message digest, consisting of a string of 128
        /// hexadecimal characters.
        /// </returns>
        public static string SHA512Hash ( string strPlaintext )
        {
            byte [ ] bytePlainText = Encoding.Unicode.GetBytes ( strPlaintext );
            SHA512 SHA512Hasher = new SHA512Managed ( );
            byte [ ] byteDigest = SHA512Hasher.ComputeHash ( bytePlainText );
			string strDigestedPrefix = Core.ByteArrayFormatters.ByteArrayToHexDigitString ( byteDigest );
            return strDigestedPrefix;
        }   // public static string SHA512Hash


        /// <summary>
        /// Given a byte array, return its SHA-512 message digest as a 128
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pabytPlainText">
        /// Supply a pointer to the byte array to be digested.
        /// </param>
        /// <returns>
        /// The return value is a message digest, consisting of a string of 128
        /// hexadecimal characters.
        /// </returns>
        public static string SHA512Hash ( byte [ ] pabytPlainText )
        {
            SHA512 SHA512Hasher = new SHA512Managed ( );
            byte [ ] byteDigest = SHA512Hasher.ComputeHash ( pabytPlainText );
			string strDigestedPrefix = Core.ByteArrayFormatters.ByteArrayToHexDigitString ( byteDigest );
            return strDigestedPrefix;
        }   // public static string SHA512Hash
    }   // public static class DigestString
}   // partial namespace WizardWrx.Cryptography