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

	2025/10/19 9.0.365 DAG    To resolve a file contention that caused the hash
                              routine to prevent another routine from gaining
                              access to a file that it was asked to process, the
                              FileStream object that feeds data to all message
                              digest algorithms is moved out of the method call,
                              and the stream is instead owned by the using block
                              that wraps the message digest object, which was
                              already in a using block of its own, thus ensuring
                              that the stream is properly closed and releases
                              its unmanaged resources including its file handle.

	2026/03/31 9.0.386 DAG    This update extends output formats to include
                              Base64 encoded strings, with or without a prefix
                              that identifies the hashing algorithm used to
                              generate the digest. The prefix is derived from
                              the name of the hashing algorithm, and is followed
                              by an underscore character. For example, a 16-byte
                              MD5 digest would be represented by a string of 26
                              characters, beginning with "MD5_", and a 64-byte
                              SHA-512 digest would be represented by a string of
                              92 characters, beginning with "SHA512_". The 
                              output format is specified by an optional
                              parameter of type OuutputFormat, which is an
                              enumeration that defines the supported output
                              formats for the digest. The method uses a switch
                              statement to determine how to format the digest
                              based on the specified output format.

                              Though the immediate need for the additional
                              formats is confined to SHA-384, all hashing
                              algorithms are updated to support the new output
                              formats, and the new output formats are added to
                              the documentation for all hashing algorithms, and
                              the file I/O is moved into a private nethod, which
                              is called by all hashing algorithms, eliminating
                              the file I/O code from the hashing algorithm
                              methods, and centralizing it in a single method
                              that can be easily maintained and updated as
                              needed, and making the code slightly more compact.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;


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
		/// <summary>
		/// Members of this public enumeration specify the output format of the message digest.
		/// </summary>
		public enum OuutputFormat
		{
			/// <summary>
			/// Output the message digest as a string of hexadecimal digits. The
			/// number of hexadecimal digits is twice the number of bytes in the
			/// digest, since each byte is represented by two hexadecimal
			/// digits. For example, the 16-byte MD5 digest is represented by a
			/// string of 32 hexadecimal digits, and the 64-byte SHA-512 digest
			/// is represented by a string of 128 hexadecimal digits.
			/// </summary>
			HexadecimalDigits,

			/// <summary>
			/// Output the message as a Base64 encoded string. The number of
			/// characters in the Base64 string is approximately 4/3 the number
			/// of bytes in the digest, since each group of 3 bytes is
			/// represented by 4 Base64 characters. For example, the 16-byte MD5
			/// digest is represented by a string of 22 Base64 characters, and
			/// the 64-byte SHA-512 digest is represented by a string of 88
			/// Base64 characters.
			/// </summary>
			Base64String,

			/// <summary>
			/// A Base64-encoded string that includes a prefix that identifies
			/// the hashing algorithm used to generate the digest. The prefix is
			/// derived from the name of the hashing algorithm, and is followed
			/// by an underscore character. For example, the 16-byte MD5 digest
			/// is represented by a string of 26 characters, beginning with
			/// "MD5_", and the 64-byte SHA-512 digest is represented by a
			/// string of 92 characters, beginning with "SHA512_".
			/// </summary>
			PrefixedBase64String
		}   // public enum OutputFormat


		/// <summary>
		/// This private enumeration is used to identify the hashing algorithm
		/// used to generate the digest, so that the appropriate prefix can be
		/// added when the output format is PrefixedBase64String.
		/// </summary>
		enum DigestType
		{
			DIGEST_TYPE_MD5,
			DIGEST_TYPE_SHA1,
			DIGEST_TYPE_SHA256,
			DIGEST_TYPE_SHA384,
			DIGEST_TYPE_SHA512
		}   // enum DIGEST_TYPE


		/// <summary>
		/// This dictionary maps each DigestType to its corresponding string
		/// prefix, which is used when the output format is
		/// PrefixedBase64String. The prefixes are derived from the names of the
		/// hashing algorithms, and are followed by an underscore character. For
		/// example, the prefix for DigestType DIGEST_TYPE_MD5 is "MD5_", and
		/// the prefix for DigestType DIGEST_TYPE_SHA512 is "SHA512_".
		/// </summary>
		static readonly Dictionary<DigestType , string> s_dctHashAlgorithmPrefixes = new Dictionary<DigestType , string> ( )
		{
			{ DigestType.DIGEST_TYPE_MD5 , @"MD5" } ,
			{ DigestType.DIGEST_TYPE_SHA1 , @"SHA1" } ,
			{ DigestType.DIGEST_TYPE_SHA256 , @"SHA256" } ,
			{ DigestType.DIGEST_TYPE_SHA384 , @"SHA384" } ,
			{ DigestType.DIGEST_TYPE_SHA512 , @"SHA512" }
		};


		/// <summary>
		/// Given the name of a file, return its MD5 message digest as a 32 
		/// character string of hexadecimal digits.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string represents the name of the file to process, which may be
		/// a relative or absolute path. The file must exist, and the caller
		/// must have permission to read it, or an exception will be thrown.
		/// <para>
		/// iMPORTANT: Since files are read in binary, they are loaded directly
		/// into the hash algorithm as byte arrays. This means that the digest
		/// of a file of ASCII characters and the hash of the file contents read
		/// into a CLR string object, which is a string of Unicode characters,
		/// will differ. The reason for this is that the string of Unicode 
		/// characters yields a different byte stream than the byte stream that
		/// came from reading the file in binary mode.
		/// </para>
		/// </param>
		/// <param name="penmOutputFormat">
		/// This optional parameter specifies the output format of the message
		/// digest. If omitted, the default is OuutputFormat.HexadecimalDigits,
		/// which outputs the digest as a string of hexadecimal digits. The
		/// other options are OuutputFormat.Base64String, which outputs the
		/// digest as a Base64 encoded string, and
		/// OuutputFormat.PrefixedBase64String, which outputs a Base64 encoded
		/// string that includes a prefix that identifies the hashing algorithm
		/// used to generate the digest. The prefix is derived from the name of
		/// the hashing algorithm, and is followed by an underscore character.
		/// For example, the 16-byte MD5 digest is represented by a string of 26
		/// characters, beginning with "MD5_", and the 64-byte SHA-512 digest is
		/// represented by a string of 92 characters, beginning with "SHA512_".
		/// </param>
		/// <returns>
		/// The message digest, consisting of a string of 32 hexadecimal
		/// characters. This string is identical with the strings returned by
		/// the reference implementation, published by Dr. Ronald Rivest, who is
		/// credited with inventing the MD5 digest algorithm.
		/// </returns>
		///<remarks>
		///<para>
		/// Since the internal hashing implementations expect byte arrays, the
		/// input string must be converted. The Encoding.Default.GetBytes method
		/// is called upon to convert the string into a byte array.
		///</para>
		///<list>
		///<item>
		/// The CSP implementation uses native machine code for the computation,
		/// and should outperform managed code on large plain-texts.
		///</item>
		///<item>
		/// By using the native implementation, the CLR is eliminated as a 
		/// potential point of failure due to a weakness in the implementation
		/// of the algorithm.
		///</item>
		///</list>
		///</remarks>
		[Obsolete ( "This algorithm is classified as broken and unsafe. Use SHA256Hash, SHA384Hash, or SHA512Hash." )]
        public static string MD5Hash ( string pstrFileName , OuutputFormat penmOutputFormat = OuutputFormat.HexadecimalDigits )
		{
			byte [ ] abytFileContents = GetFileContent ( pstrFileName );

			using ( MD5 sha = MD5.Create ( ) )
			{
				byte [ ] abytDigest = sha.ComputeHash ( abytFileContents );
				return FormatDigestPerOutputFormat (
					abytDigest ,
					penmOutputFormat ,
					DigestType.DIGEST_TYPE_MD5 );
			}   // using ( MD5 sha = MD5.Create ( ) )
		}   // public static string MD5Hash


		/// <summary>
		/// Given the name of a file, return its SHA-1 message digest as a 32
		/// character string of hexadecimal digits.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string represents the name of the file to process, which may be
		/// a relative or absolute path. The file must exist, and the caller
		/// must have permission to read it, or an exception will be thrown.
		/// <para>
		/// iMPORTANT: Since files are read in binary, they are loaded directly
		/// into the hash algorithm as byte arrays. This means that the digest
		/// of a file of ASCII characters and the hash of the file contents read
		/// into a CLR string object, which is a string of Unicode characters,
		/// will differ. The reason for this is that the string of Unicode 
		/// characters yields a different byte stream than the byte stream that
		/// came from reading the file in binary mode.
		/// </para>
		/// </param>
		/// <param name="penmOutputFormat">
		/// This optional parameter specifies the output format of the message
		/// digest. If omitted, the default is OuutputFormat.HexadecimalDigits,
		/// which outputs the digest as a string of hexadecimal digits. The
		/// other options are OuutputFormat.Base64String, which outputs the
		/// digest as a Base64 encoded string, and
		/// OuutputFormat.PrefixedBase64String, which outputs a Base64 encoded
		/// string that includes a prefix that identifies the hashing algorithm
		/// used to generate the digest. The prefix is derived from the name of
		/// the hashing algorithm, and is followed by an underscore character.
		/// For example, the 16-byte MD5 digest is represented by a string of 26
		/// characters, beginning with "MD5_", and the 64-byte SHA-512 digest is
		/// represented by a string of 92 characters, beginning with "SHA512_".
		/// </param>
		/// <returns>
		/// The message digest, consisting of a string of 40 hexadecimal
		/// characters.
		/// </returns>
		[Obsolete ( "This algorithm is classified as broken and unsafe. Use SHA256Hash, SHA384Hash, or SHA512Hash." )]
        public static string SHA1Hash ( string pstrFileName , OuutputFormat penmOutputFormat = OuutputFormat.HexadecimalDigits )
		{
			byte [ ] abytFileContents = GetFileContent ( pstrFileName );

			using ( SHA1 sha = SHA1.Create ( ) )
			{
				byte [ ] abytDigest = sha.ComputeHash ( abytFileContents );
				return FormatDigestPerOutputFormat (
					abytDigest ,
					penmOutputFormat ,
					DigestType.DIGEST_TYPE_SHA1 );
			}   // using ( SHA1 sha = SHA1.Create ( ) )
		}   // public static string SHA1Hash


		/// <summary>
		/// Given the name of a file, return its SHA-256 message digest as a 64
		/// character string of hexadecimal digits.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string represents the name of the file to process, which may be
		/// a relative or absolute path. The file must exist, and the caller
		/// must have permission to read it, or an exception will be thrown.
		/// <para>
		/// iMPORTANT: Since files are read in binary, they are loaded directly
		/// into the hash algorithm as byte arrays. This means that the digest
		/// of a file of ASCII characters and the hash of the file contents read
		/// into a CLR string object, which is a string of Unicode characters,
		/// will differ. The reason for this is that the string of Unicode 
		/// characters yields a different byte stream than the byte stream that
		/// came from reading the file in binary mode.
		/// </para>
		/// </param>
		/// <param name="penmOutputFormat">
		/// This optional parameter specifies the output format of the message
		/// digest. If omitted, the default is OuutputFormat.HexadecimalDigits,
		/// which outputs the digest as a string of hexadecimal digits. The
		/// other options are OuutputFormat.Base64String, which outputs the
		/// digest as a Base64 encoded string, and
		/// OuutputFormat.PrefixedBase64String, which outputs a Base64 encoded
		/// string that includes a prefix that identifies the hashing algorithm
		/// used to generate the digest. The prefix is derived from the name of
		/// the hashing algorithm, and is followed by an underscore character.
		/// For example, the 16-byte MD5 digest is represented by a string of 26
		/// characters, beginning with "MD5_", and the 64-byte SHA-512 digest is
		/// represented by a string of 92 characters, beginning with "SHA512_".
		/// </param>
		/// <returns>
		/// The message digest, consisting of a string of 64 hexadecimal
		/// characters.
		/// </returns>
		public static string SHA256Hash ( string pstrFileName , OuutputFormat penmOutputFormat = OuutputFormat.HexadecimalDigits )
		{
			byte [ ] abytFileContents = GetFileContent ( pstrFileName );

			using ( SHA256 sha = SHA256.Create ( ) )
			{
				byte [ ] abytDigest = sha.ComputeHash ( abytFileContents );
				return FormatDigestPerOutputFormat (
					abytDigest ,
					penmOutputFormat ,
					DigestType.DIGEST_TYPE_SHA256 );
			}   // using ( SHA256 sha = SHA256.Create ( ) )
		}   // public static string SHA256Hash


		/// <summary>
		/// Given the name of a file, return its SHA-384 message digest as a 96
		/// character string of hexadecimal digits.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string represents the name of the file to process, which may be
		/// a relative or absolute path. The file must exist, and the caller
		/// must have permission to read it, or an exception will be thrown.
		/// <para>
		/// iMPORTANT: Since files are read in binary, they are loaded directly
		/// into the hash algorithm as byte arrays. This means that the digest
		/// of a file of ASCII characters and the hash of the file contents read
		/// into a CLR string object, which is a string of Unicode characters,
		/// will differ. The reason for this is that the string of Unicode 
		/// characters yields a different byte stream than the byte stream that
		/// came from reading the file in binary mode.
		/// </para>
		/// </param>
		/// <param name="penmOutputFormat">
		/// This optional parameter specifies the output format of the message
		/// digest. If omitted, the default is OuutputFormat.HexadecimalDigits,
		/// which outputs the digest as a string of hexadecimal digits. The
		/// other options are OuutputFormat.Base64String, which outputs the
		/// digest as a Base64 encoded string, and
		/// OuutputFormat.PrefixedBase64String, which outputs a Base64 encoded
		/// string that includes a prefix that identifies the hashing algorithm
		/// used to generate the digest. The prefix is derived from the name of
		/// the hashing algorithm, and is followed by an underscore character.
		/// For example, the 16-byte MD5 digest is represented by a string of 26
		/// characters, beginning with "MD5_", and the 64-byte SHA-512 digest is
		/// represented by a string of 92 characters, beginning with "SHA512_".
		/// </param>
		/// <returns>
		/// The message digest, consisting of a string of 64 hexadecimal
		/// characters.
		/// </returns>
		public static string SHA384Hash ( string pstrFileName , OuutputFormat penmOutputFormat = OuutputFormat.HexadecimalDigits )
		{
			byte [ ] abytFileContents = GetFileContent ( pstrFileName );

			using ( SHA384 sha = SHA384.Create ( ) )
			{
				byte [ ] abytDigest = sha.ComputeHash ( abytFileContents );
				return FormatDigestPerOutputFormat (
					abytDigest ,
					penmOutputFormat ,
					DigestType.DIGEST_TYPE_SHA384 );
			}   // using ( SHA384 sha = SHA384.Create ( ) )
		}   // public static string SHA384Hash



		/// <summary>
		/// Given the name of a file, return its SHA-512 message digest as a 128
		/// character string of hexadecimal digits.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string represents the name of the file to process, which may be
		/// a relative or absolute path. The file must exist, and the caller
		/// must have permission to read it, or an exception will be thrown.
		/// <para>
		/// iMPORTANT: Since files are read in binary, they are loaded directly
		/// into the hash algorithm as byte arrays. This means that the digest
		/// of a file of ASCII characters and the hash of the file contents read
		/// into a CLR string object, which is a string of Unicode characters,
		/// will differ. The reason for this is that the string of Unicode 
		/// characters yields a different byte stream than the byte stream that
		/// came from reading the file in binary mode.
		/// </para>
		/// </param>
		/// <param name="penmOutputFormat">
		/// This optional parameter specifies the output format of the message
		/// digest. If omitted, the default is OuutputFormat.HexadecimalDigits,
		/// which outputs the digest as a string of hexadecimal digits. The
		/// other options are OuutputFormat.Base64String, which outputs the
		/// digest as a Base64 encoded string, and
		/// OuutputFormat.PrefixedBase64String, which outputs a Base64 encoded
		/// string that includes a prefix that identifies the hashing algorithm
		/// used to generate the digest. The prefix is derived from the name of
		/// the hashing algorithm, and is followed by an underscore character.
		/// For example, the 16-byte MD5 digest is represented by a string of 26
		/// characters, beginning with "MD5_", and the 64-byte SHA-512 digest is
		/// represented by a string of 92 characters, beginning with "SHA512_".
		/// </param>
		/// <returns>
		/// The message digest, consisting of a string of 64 hexadecimal
		/// characters.
		/// </returns>
		public static string SHA512Hash ( string pstrFileName , OuutputFormat penmOutputFormat = OuutputFormat.HexadecimalDigits )
		{
			byte [ ] abytFileContents = GetFileContent ( pstrFileName );

			using ( SHA512 sha = SHA512.Create ( ) )
			{
				byte [ ] abytDigest = sha.ComputeHash ( abytFileContents );
				return FormatDigestPerOutputFormat (
					abytDigest ,
					penmOutputFormat ,
					DigestType.DIGEST_TYPE_SHA512 );
			}   // using ( SHA512 sha = SHA512.Create ( ) )
		}   // public static string SHA512Hash


		/// <summary>
		/// This private method formats the message digest according to the
		/// specified output format. The digest is provided as a byte array, and
		/// the output format is specified by the OuutputFormat enumeration. The
		/// method also takes a DigestType parameter to determine the
		/// appropriate prefix when the output format is PrefixedBase64String.
		/// The method uses a switch statement to determine how to format the
		/// digest based on the specified output format. If the output format is
		/// HexadecimalDigits, it converts the byte array to a string of
		/// hexadecimal digits using the ByteArrayToHexDigitString method. If
		/// the output format is Base64String, it converts the byte array to a
		/// Base64 encoded string using the Convert.ToBase64String method. If
		/// the output format is PrefixedBase64String, it retrieves the
		/// appropriate prefix from the s_dctHashAlgorithmPrefixes dictionary
		/// using the DigestType parameter, and then constructs the output
		/// string by concatenating the prefix, an underscore character, and the
		/// Base64 encoded string of the digest. If the output format does not
		/// match any of the specified cases, it defaults to returning the
		/// digest as a string of hexadecimal digits.
		/// </summary>
		/// <param name="abytDigest">
		/// This byte array represents the message digest that was computed by
		/// the hashing algorithm. The contents of this byte array will be
		/// formatted according to the specified output format and returned as a
		/// string. The length of the byte array depends on the hashing
		/// algorithm used to compute the digest. For example, the MD5 algorithm
		/// produces a 16-byte digest, the SHA-1 algorithm produces a 20-byte
		/// digest, the SHA-256 algorithm produces a 32-byte digest, the SHA-384
		/// algorithm produces a 48-byte digest, and the SHA-512 algorithm
		/// produces a 64-byte digest. The contents of the byte array will be
		/// the raw binary representation of the message digest, and it will be
		/// formatted into a human-readable string based on the specified output
		/// format.
		/// </param>
		/// <param name="penmOutputFormat">
		/// This parameter specifies the output format of the message digest. It
		/// is of type OuutputFormat, which is an enumeration that defines the
		/// possible output formats for the digest. The method will use this
		/// parameter to determine how to format the byte array of the digest
		/// into a string. The possible values for this parameter are:
		/// <list type="number">
		/// <item>
		/// OuutputFormat.HexadecimalDigits produces a string of hexadecimal
		/// digits, where each byte in the digest is represented by two
		/// hexadecimal characters. For example, a 16-byte digest would be
		/// represented by a string of 32 hexadecimal characters.
		/// </item>
		/// <item>
		/// OuutputFormat.Base64String produces a Base64 encoded string,
		/// where each group of 3 bytes in the digest is represented by 4 Base64
		/// characters. For example, a 16-byte digest would be represented by a
		/// string of 22 Base64 characters.
		/// </item>
		/// <item>
		/// OuutputFormat.PrefixedBase64String produces a Base64 encoded string
		/// that includes a prefix that identifies the hashing algorithm used to
		/// generate the digest. The prefix is derived from the name of the
		/// hashing algorithm, and is followed by an underscore character. For
		/// example, a 16-byte MD5 digest would be represented by a string of 26
		/// characters, beginning with "MD5_", and a 64-byte SHA-512 digest
		/// would be represented by a string of 92 characters, beginning with
		/// "SHA512_".
		/// </item>
		/// </list>
		/// </param>
		/// <param name="penmDigestType">
		/// This parameter specifies the type of the digest, which is used to
		/// determine the appropriate prefix when the output format is
		/// PrefixedBase64String. It is of type DigestType, which is an
		/// enumeration that defines the possible types of digests. The method
		/// uses  this parameter to look up the corresponding prefix in the
		/// s_dctHashAlgorithmPrefixes dictionary when the output format is
		/// PrefixedBase64String. The possible values for this parameter are:
		/// <list type="number">
		/// <item>
		/// DigestType.DIGEST_TYPE_MD5
		/// </item>
		/// <item>
		/// DigestType.DIGEST_TYPE_SHA1
		/// </item>
		/// <item>
		/// DigestType.DIGEST_TYPE_SHA256
		/// </item>
		/// <item>
		/// DigestType.DIGEST_TYPE_SHA384
		/// </item>
		/// <item>
		/// DigestType.DIGEST_TYPE_SHA512
		/// </item>
		/// </list>
		/// </param>
		/// <returns>
		/// The returned string is the formatted message digest, based on the
		/// specified output format. If the output format is
		/// OuutputFormat.HexadecimalDigits, the returned string will consist of
		/// hexadecimal characters representing the bytes of the digest. If the
		/// output format is OuutputFormat.Base64String or
		/// OuutputFormat.PrefixedBase64String, the output is a Base64 encoded
		/// string, with or without a prefix that identifies the hashing
		/// algorithm used to generate the	string. If the output format does
		/// not match any of the specified cases, it defaults to returning the
		/// digest as a string of hexadecimal digits.
		/// </returns>
		private static string FormatDigestPerOutputFormat ( byte [ ] abytDigest , OuutputFormat penmOutputFormat , DigestType penmDigestType )
		{
			switch ( penmOutputFormat )
			{
				case OuutputFormat.HexadecimalDigits:
					return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( abytDigest );
				case OuutputFormat.Base64String:
					return Convert.ToBase64String ( abytDigest );
				case OuutputFormat.PrefixedBase64String:
					return $"{s_dctHashAlgorithmPrefixes [ penmDigestType ]}{SpecialStrings.UNDERSCORE_CHAR}{Convert.ToBase64String ( abytDigest )}";
				default:
					return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( abytDigest );
			}   // switch ( penmOutputFormat )
		}   // private static string FormatDigestPerOutputFormat


		/// <summary>
		/// This private method reads the contents of the specified file and
		/// returns it as a byte array. The method takes a string parameter that
		/// represents the name of the file to read, which may be a relative or
		/// absolute path. The method checks if the file name is null and throws
		/// an ArgumentNullException if it is. It also checks if the file exists
		/// and throws a FileNotFoundException if it does not. If the file
		/// exists, the method reads all bytes from the file using the
		/// File.ReadAllBytes method and returns the byte array containing the
		/// file contents. This byte array becomes the input to the hashing
		/// algorithms to compute the message digest.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string represents the name of the file to process, which may be
		/// a relative or absolute path. The file must exist, and the caller
		/// must have permission to read it, or an exception will be thrown.
		/// <para>
		/// iMPORTANT: Since files are read in binary, they are loaded directly
		/// into the hash algorithm as byte arrays. This means that the digest
		/// of a file of ASCII characters and the hash of the file contents read
		/// into a CLR string object, which is a string of Unicode characters,
		/// will differ. The reason for this is that the string of Unicode 
		/// characters yields a different byte stream than the byte stream that
		/// came from reading the file in binary mode.
		/// </para>
		/// </param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">
		/// An ArgumentNullException Exception arises when 
		/// <paramref name="pstrFileName"/> is null.
		/// </exception>
		/// <exception cref="FileNotFoundException">
		/// An FileNotFoundException Exception arises when the file specified by
		/// <paramref name="pstrFileName"/> cannot be found. A file is reported
		/// as "not found" when the user does not have permission to read the
		/// file, or when the file does not exist at the specified or implied
		/// path.
		/// </exception>
		private static byte [ ] GetFileContent ( string pstrFileName )
		{
			if ( pstrFileName == null )
				throw new ArgumentNullException ( nameof ( pstrFileName ) );

			if ( !File.Exists ( pstrFileName ) )
				throw new FileNotFoundException ( @"Input file not found." , pstrFileName );

			return  File.ReadAllBytes ( pstrFileName );
		}   // private static byte [ ] GetFileContent
	}   // class public static class
}   // partial namespace WizardWrx.Cryptography