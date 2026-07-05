using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardWrx.Cryptography
{
	/// <summary>
	/// These symbolic constants work in conjunction with the DigestAlgorithm
	/// enumeration, defined in a like-named source file in this same directory,
	/// to establish invariants (symbolic constants) that represent the lengths,
	/// in bytes, of the supported message digest (hash) algorithms.
	/// </summary>
	public class DigestConstants
	{
		/// <summary>
		/// This integer symbolic constant represents the length, in bytes, of a
		/// SHA‑256 message digest.
		/// </summary>
		public const int SHA256_DIGEST_LENGTH = ( int ) DigestAlgorithm.SHA256 / MagicNumbers.BITS_PER_BYTE;


		/// <summary>
		/// This integer symbolic constant represents the length, in bytes, of a
		/// SHA‑384 message digest.
		/// </summary>
		public const int SHA384_DIGEST_LENGTH = ( int ) DigestAlgorithm.SHA384 / MagicNumbers.BITS_PER_BYTE;


		/// <summary>
		/// This integer symbolic constant represents the length, in bytes, of a
		/// SHA‑512 message digest.
		/// </summary>
		public const int SHA512_DIGEST_LENGTH = ( int ) DigestAlgorithm.SHA512 / MagicNumbers.BITS_PER_BYTE;
	}   // public class DigestConstants
}   // partial namespace WizardWrx.Core.Cryptography