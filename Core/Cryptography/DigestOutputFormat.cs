namespace WizardWrx.Cryptography
{
	/// <summary>
	/// Members of this public enumeration specify the output format of the message digest.
	/// </summary>
	public enum OutputFormat
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
}   // partial namespace WizardWrx.Core.Cryptography