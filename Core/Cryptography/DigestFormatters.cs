using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WizardWrx.Cryptography
{
	/// <summary>
	/// This static class exposes methods that format message digests according
	/// to the specified output format. The class provides a method that takes a
	/// byte array representing the digest, an output format enumeration value,
	/// and a digest type enumeration value, and returns a formatted string
	/// representation of the digest based on the specified output format. The
	/// class also contains a dictionary that maps each digest type to its
	/// corresponding string prefix, which is used when the output format is
	/// PrefixedBase64String.
	/// </summary>
	public static class DigestFormatters
	{
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
		/// <param name="penmDigestAlgorithm">
		/// This parameter specifies the digest algorithm, which is used to
		/// determine the appropriate prefix when the output format is
		/// PrefixedBase64String. It is of type DigestAlgorithm, which is an
		/// enumeration that defines the supported digest algorithms. The method
		/// uses this parameter to look up the corresponding prefix in the
		/// s_dctHashAlgorithmPrefixes dictionary when the output format is
		/// PrefixedBase64String. The possible values for this parameter are 
		/// members of the DigestAlgorithm enumeration, which includes values
		/// such as DigestAlgorithm.MD5, DigestAlgorithm.SHA1,
		/// DigestAlgorithm.SHA256, DigestAlgorithm.SHA384, and
		/// DigestAlgorithm.SHA512.
		/// </param>
		/// <returns>
		/// The returned string is the formatted message digest, based on the
		/// specified output format. If the output format is
		/// OuutputFormat.HexadecimalDigits, the returned string consists of
		/// hexadecimal characters representing the bytes of the digest. If the
		/// output format is OuutputFormat.Base64String or
		/// OuutputFormat.PrefixedBase64String, the output is a Base64 encoded
		/// string, with or without a prefix that identifies the hashing
		/// algorithm used to generate the	string. If the output format does
		/// not match any of the specified cases, it defaults to returning the
		/// digest as a string of hexadecimal digits.
		/// </returns>
		public static string FormatDigestPerOutputFormat ( byte [ ] abytDigest , OutputFormat penmOutputFormat , DigestAlgorithm penmDigestAlgorithm )
		{
			switch ( penmOutputFormat )
			{
				case OutputFormat.HexadecimalDigits:
					return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( abytDigest );
				case OutputFormat.Base64String:
					return Convert.ToBase64String ( abytDigest );
				case OutputFormat.PrefixedBase64String:
					return $"{s_dctHashAlgorithmPrefixes [ penmDigestAlgorithm ]}{SpecialStrings.HYPHEN}{Convert.ToBase64String ( abytDigest )}";
				default:
					return Core.ByteArrayFormatters.ByteArrayToHexDigitString ( abytDigest );
			}   // switch ( penmOutputFormat )
		}   // private static string FormatDigestPerOutputFormat


		/// <summary>
		/// This dictionary maps each DigestAlgorithm to its corresponding
		/// string prefix, which is used when the output format is
		/// PrefixedBase64String. The prefixes are derived from the names of the
		/// hashing algorithms, and are followed by a hyphen
		/// </summary>
		/// <see href="https://github.com/w3c/webappsec-subresource-integrity/blob/master/spec_v1.markdown">webappsec-subresource-integrity</see>
		private static readonly ImmutableDictionary<DigestAlgorithm , string> s_dctHashAlgorithmPrefixes = ImmutableDictionary.CreateRange ( new [ ]
		{
			new KeyValuePair<DigestAlgorithm, string>(DigestAlgorithm.MD5,    "md5"),
			new KeyValuePair<DigestAlgorithm, string>(DigestAlgorithm.SHA1,   "sha1"),
			new KeyValuePair<DigestAlgorithm, string>(DigestAlgorithm.SHA256, "sha256"),
			new KeyValuePair<DigestAlgorithm, string>(DigestAlgorithm.SHA384, "sha384"),
			new KeyValuePair<DigestAlgorithm, string>(DigestAlgorithm.SHA512, "sha512")
		} );    // private static readonly ImmutableDictionary<DigestAlgorithm , string> s_dctHashAlgorithmPrefixes
	}   // public static class DigestFormatters
}   // partial namespace WizardWrx.Cryptography