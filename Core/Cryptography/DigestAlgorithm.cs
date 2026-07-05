using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardWrx.Cryptography
{
	/// <summary>
	/// Members of this enumeration identify the supported message digest 
	/// algorithms. The assigned values represent the bit length of the
	/// digest produced by each algorithm, enabling direct computation of
	/// the corresponding byte length.
	/// </summary>
	public enum DigestAlgorithm
	{
		/// <summary>
		/// 
		/// </summary>
		MD5= 128,

		/// <summary>
		/// 
		/// </summary>
		SHA1 = 160,
		/// <summary>
		/// The SHA‑256 algorithm, which produces a 256‑bit (32‑byte) digest.
		/// </summary>
		SHA256 = 256,

		/// <summary>
		/// The SHA‑384 algorithm, which produces a 384‑bit (48‑byte) digest.
		/// </summary>
		SHA384 = 384,

		/// <summary>
		/// The SHA‑512 algorithm, which produces a 512‑bit (64‑byte) digest.
		/// </summary>
		SHA512 = 512
	}   // public enum DigestAlgorithm
}   // partial namespace WizardWrx.Cryptography