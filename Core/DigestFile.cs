/*
    ============================================================================

    Namespace:          WizardWrx.Cryptography

    Class Name:         DigestFile

    File Name:          DigestFile.cs

    Synopsis:           This static class exposes methods for generating message
                        digests of files.

    Remarks:            The initial implementation supports only the MD5 hashing
                        algorithm, to meet an immediate need.

                        This class supports the following algorithms.

                        ------------------------------------------------------
                        Algorithm   Strength    Hexadecimal String  Use This
                        Name        in Bits     Length in Bytes     Method
                        ---------   --------    ------------------  ----------
                        MD5         128          32                 MD5Hash *
                        SHA-1       160          40                 SHA1Hash *
                        SHA-256     256          64                 SHA256Hash
                        SHA-384     384          96                 SHA384Hash
                        SHA-512     512         128                 SHA512Hash
                        ------------------------------------------------------

                        *   MD5 and SHA-1 are classified as broken and unsafe.
                            Both are retained only for backward compatibility.
                            References in code will cause the C# compiler to
                            emit warning CS0618, and other compilers will emit
                            similar warnings.

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

    Created:            Monday, 17 February 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/02/17 2.2     DAG    Initial implementation.

    2014/06/28 2.3     DAG    1) Add the whole set of SHA algorithms. See the
                                 Remarks section for a complete chart.

                              2) In routine MD5Hash, replace the hand coded I/O
                                 loop, by passing a FileStream into the hasher.

                              3) Mark the MD5Hash and SHA1Hash as obsolete.

    2014/09/21 2.8     DAG    Move DigestToHexDigits from this class into class
                              Utl, rename it ByteArrayToHexDigitString, 
                              reflecting its true calling, and mark it public.

                              Correct a namespace oversight; this class belongs
                              in the Cryptography subsidiary namespace.

                              BREAKING CHANGE ALERT

                              Moving this class to Cryptography is a minor, easy
                              to correct, breaking change.

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
using System.IO;
using System.Security.Cryptography;

using WizardWrx;

namespace WizardWrx.Cryptography
{
    /// <summary>
    /// Static methods for computing message digests for files, using the most
    /// common algorithms.
    /// 
    /// iMPORTANT: Since files are read in binary, they are loaded directly
    /// into the hash algorithm as byte arrays. This means that the digest
    /// of a file of ASCII characters and the hash of the file contents read
    /// into a CLR string object, which is a string of Unicode characters,
    /// will differ. The reason for this is that the string of Unicode 
    /// characters yields a different byte stream than the byte stream that
    /// came from reading the file in binary mode.
    /// </summary>
    public static class DigestFile
    {
        const int BUFSIZE = 8192;

        /// <summary>
        /// Given the name of a file, return its MD5 message digest as a 32 
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pstrFileName">
        /// String containing the name of the file to be digested. See Remarks
        /// for important information about the internal implementation.
        /// 
        /// iMPORTANT: Since files are read in binary, they are loaded directly
        /// into the hash algorithm as byte arrays. This means that the digest
        /// of a file of ASCII characters and the hash of the file contents read
        /// into a CLR string object, which is a string of Unicode characters,
        /// will differ. The reason for this is that the string of Unicode 
        /// characters yields a different byte stream than the byte stream that
        /// came from reading the file in binary mode.
        /// </param>
        /// <returns>
        /// The message digest, consisting of a string of 32 hexadecimal
        /// characters. This string is identical with the strings returned by
        /// the reference implementation, published by Dr. Ronald Rivest, who is
        /// credited with inventing the MD5 digest algorithm.
        /// </returns>
        ///<remarks>
        /// Since the internal hashing implementations expect byte arrays, the
        /// input string must be converted. The Encoding.Default.GetBytes method
        /// is called upon to convert the string into a byte array.
        /// 
        /// A slightly modified version of the Rivest code, written in ANSI C,
        /// is the engine in my MD5WIN stand-alone program and my MD5Digest
        /// Windows Dynamic Link Library.
        /// 
        /// However, this function uses a MD5CryptoServiceProvider object, which
        /// provides a managed interface to the Cryptographic Service Provider
        /// in the host's installation of Microsoft Windows. I chose this over
        /// the 100% managed implementation for two reasons.
        /// 
        /// 1) The CSP implementation uses native machine code for the
        /// computation, and should outperform managed code on large plain-texts.
        /// 
        /// 2) By using the native implementation, the CLR is eliminated as a
        /// potential point of failure due to a weakness in the implementation
        /// of the algorithm.
        ///</remarks>
        [Obsolete ( "This algorithm is classified as broken and unsafe. Use SHA256Hash, SHA384Hash, or SHA512Hash." )]
        public static string MD5Hash ( string pstrFileName )
        {
            MD5 md5Hasher = new MD5CryptoServiceProvider ( );

            using ( md5Hasher )
            {
                //byte [ ] bytInputData = ProcessFile (
                //    pstrFileName ,
                //    md5Hasher );

                byte [ ] bytInputData = md5Hasher.ComputeHash (
                    new FileStream (
                        pstrFileName ,
                        FileMode.Open ,
                        FileAccess.Read ,
                        FileShare.Read ,
                        BUFSIZE ,
                        FileOptions.SequentialScan
                        | FileOptions.Asynchronous ) );

				return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( bytInputData );
            }	// using ( md5Hasher )
        }   // public static string MD5Hash


        /// <summary>
        /// Given the name of a file, return its SHA-1 message digest as a 32
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pstrFileName">
        /// String containing the name of the file to be digested.
        /// 
        /// iMPORTANT: Since files are read in binary, they are loaded directly
        /// into the hash algorithm as byte arrays. This means that the digest
        /// of a file of ASCII characters and the hash of the file contents read
        /// into a CLR string object, which is a string of Unicode characters,
        /// will differ. The reason for this is that the string of Unicode 
        /// characters yields a different byte stream than the byte stream that
        /// came from reading the file in binary mode.
        /// </param>
        /// <returns>
        /// The message digest, consisting of a string of 40 hexadecimal
        /// characters.
        /// </returns>
        [Obsolete ( "This algorithm is classified as broken and unsafe. Use SHA256Hash, SHA384Hash, or SHA512Hash." )]
        public static string SHA1Hash ( string pstrFileName )
        {
            SHA1 SHA1Hasher = new SHA1CryptoServiceProvider ( );

            using ( SHA1Hasher )
            {
                byte [ ] bytInputData = SHA1Hasher.ComputeHash (
                    new FileStream (
                        pstrFileName ,
                        FileMode.Open ,
                        FileAccess.Read ,
                        FileShare.Read ,
                        BUFSIZE ,
                        FileOptions.SequentialScan 
                        | FileOptions.Asynchronous ) );

				return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( bytInputData );
            }	// using ( SHA1Hasher )
        }   // public static string SHA1Hash


        /// <summary>
        /// Given the name of a file, return its SHA-256 message digest as a 64
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pstrFileName">
        /// String containing the name of the file to be digested.
        /// 
        /// iMPORTANT: Since files are read in binary, they are loaded directly
        /// into the hash algorithm as byte arrays. This means that the digest
        /// of a file of ASCII characters and the hash of the file contents read
        /// into a CLR string object, which is a string of Unicode characters,
        /// will differ. The reason for this is that the string of Unicode 
        /// characters yields a different byte stream than the byte stream that
        /// came from reading the file in binary mode.
        /// </param>
        /// <returns>
        /// The message digest, consisting of a string of 64 hexadecimal
        /// characters.
        /// </returns>
        public static string SHA256Hash ( string pstrFileName )
        {
            SHA256 SHA256Hasher = new SHA256Managed ( );

            using ( SHA256Hasher )
            {
                byte [ ] bytInputData = SHA256Hasher.ComputeHash (
                    new FileStream (
                        pstrFileName ,
                        FileMode.Open ,
                        FileAccess.Read ,
                        FileShare.Read ,
                        BUFSIZE ,
                        FileOptions.SequentialScan
                        | FileOptions.Asynchronous ) );

				return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( bytInputData );
            }	// using ( SHA256Hasher )
        }   // public static string SHA256Hash


        /// <summary>
        /// Given the name of a file, return its SHA-384 message digest as a 96
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pstrFileName">
        /// String containing the name of the file to be digested.
        /// 
        /// iMPORTANT: Since files are read in binary, they are loaded directly
        /// into the hash algorithm as byte arrays. This means that the digest
        /// of a file of ASCII characters and the hash of the file contents read
        /// into a CLR string object, which is a string of Unicode characters,
        /// will differ. The reason for this is that the string of Unicode 
        /// characters yields a different byte stream than the byte stream that
        /// came from reading the file in binary mode.
        /// </param>
        /// <returns>
        /// The message digest, consisting of a string of 64 hexadecimal
        /// characters.
        /// </returns>
        public static string SHA384Hash ( string pstrFileName )
        {
            SHA384 SHA384Hasher = new SHA384Managed ( );

            using ( SHA384Hasher )
            {
                byte [ ] bytInputData = SHA384Hasher.ComputeHash (
                    new FileStream (
                        pstrFileName ,
                        FileMode.Open ,
                        FileAccess.Read ,
                        FileShare.Read ,
                        BUFSIZE ,
                        FileOptions.SequentialScan
                        | FileOptions.Asynchronous ) );

				return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( bytInputData );
            }	// using ( SHA384Hasher )
        }   // public static string SHA384Hash


        /// <summary>
        /// Given the name of a file, return its SHA-512 message digest as a 128
        /// character string of hexadecimal digits.
        /// </summary>
        /// <param name="pstrFileName">
        /// String containing the name of the file to be digested.
        /// 
        /// iMPORTANT: Since files are read in binary, they are loaded directly
        /// into the hash algorithm as byte arrays. This means that the digest
        /// of a file of ASCII characters and the hash of the file contents read
        /// into a CLR string object, which is a string of Unicode characters,
        /// will differ. The reason for this is that the string of Unicode 
        /// characters yields a different byte stream than the byte stream that
        /// came from reading the file in binary mode.
        /// </param>
        /// <returns>
        /// The message digest, consisting of a string of 64 hexadecimal
        /// characters.
        /// </returns>
        public static string SHA512Hash ( string pstrFileName )
        {
            SHA512 SHA512Hasher = new SHA512Managed ( );

            using ( SHA512Hasher )
            {
                byte [ ] bytInputData = SHA512Hasher.ComputeHash (
                    new FileStream (
                        pstrFileName ,
                        FileMode.Open ,
                        FileAccess.Read ,
                        FileShare.Read ,
                        BUFSIZE ,
                        FileOptions.SequentialScan
                        | FileOptions.Asynchronous ) );

				return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( bytInputData );
            }	// using ( SHA512Hasher )
        }   // public static string SHA512Hash
    }   // class public static class
}   // partial namespace WizardWrx.Cryptography