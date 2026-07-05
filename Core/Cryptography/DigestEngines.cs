using System;
using System.Security.Cryptography;

namespace WizardWrx.Cryptography
{
	/// <summary>
	/// The methods of this static class implement the message digest algorithms
	/// required by the DigestFile and DigestString classes. Their methods are
	/// made public so that the message digests can be consumed independently by
	/// the DigestMatch class.
	/// </summary>
	public static class DigestEngines
	{
		#region ComputeDigest (workhorse) Methods
		/// <summary>
		/// This method computes hashes (message digests) using any algorithm
		/// that is derived from the HashAlgorithm abstract class.
		/// </summary>
		/// <param name="pAlgorithm">
		/// This HashAlgorithm object represents a class derived from abstract
		/// class System.Security.Cryptography.HashAlgorithm. Its ComputeHash
		/// method computes the returned message digest (hash).
		/// </param>
		/// <param name="pabytData">
		/// <para>
		/// This byte array represents data, from whatever source, over which to
		/// compute a message digest (hash) using the HashAlgorithm identified
		/// by the <paramref name="pAlgorithm"/> parameter.
		/// </para>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		/// <exception cref="ArgumentNullException">
		/// An ArgumentNullException Exception arises when either parameter,
		/// <paramref name="pAlgorithm"/> or <paramref name="pabytData"/>, is a
		/// null reference.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// An ArgumentException Exception arises when parameter
		/// <paramref name="pabytData"/> is an empty array.
		/// </exception>
		public static byte [ ] ComputeDigest ( HashAlgorithm pAlgorithm , byte [ ] pabytData )
		{
			if ( pAlgorithm == null )
				throw new ArgumentNullException ( nameof ( pAlgorithm ) );

			if ( pabytData == null )
				throw new ArgumentNullException ( nameof ( pabytData ) );

			if ( pabytData.Length == ArrayInfo.ARRAY_IS_EMPTY )
				throw new ArgumentException (
					Core.Properties.Resources.ERRMSG_MESSAGE_TO_DIGEST_CANNOT_BE_EMPTY ,
					nameof ( pabytData ) );

			return pAlgorithm.ComputeHash ( pabytData );
		}   // public static byte [ ] ComputeDigest (1 of 2)


		/// <summary>
		/// Compute a message digest over the contents of a readable stream.
		/// </summary>
		/// <param name="pAlgorithm">
		/// This HashAlgorithm object represents a class derived from abstract
		/// class System.Security.Cryptography.HashAlgorithm. Its ComputeHash
		/// method computes the returned message digest (hash).
		/// </param>
		/// <param name="pstrmData">
		/// This System.IO.Stream object represents a System.IO.Stream, or an
		/// instance of a derived class, that contains the data to digest. The
		/// stream must be readable and <b>cannot</b> be empty.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		/// <exception cref="ArgumentNullException">
		/// An ArgumentNullException Exception arises when either parameter,
		/// <paramref name="pAlgorithm"/> or <paramref name="pstrmData"/>, is a
		/// null reference.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// An ArgumentException Exception arises when parameter
		/// <paramref name="pstrmData"/> is an empty stream.
		/// </exception>
		public static byte [ ] ComputeDigest ( HashAlgorithm pAlgorithm , System.IO.Stream pstrmData )
		{
			if ( pAlgorithm == null )
				throw new ArgumentNullException ( nameof ( pAlgorithm ) );

			if ( pstrmData == null )
				throw new ArgumentNullException ( nameof ( pstrmData ) );

			if ( !pstrmData.CanRead || pstrmData.Length == ArrayInfo.ARRAY_IS_EMPTY )
				throw new ArgumentException (
					Core.Properties.Resources.ERRMSG_MESSAGE_TO_DIGEST_CANNOT_BE_EMPTY ,
					nameof ( pstrmData ) );

			return pAlgorithm.ComputeHash ( pstrmData );
		}   // public static byte [ ] ComputeDigest (2 of 2)
		#endregion // ComputeDigest (workhorse) Methods


		#region ComputeDigest and CreateAlgorithm "factory" Methods
		/// <summary>
		/// Compute a message digest over a byte array using the algorithm identified
		/// by the <paramref name="penmAlgorithm"/> parameter.
		/// </summary>
		/// <param name="penmAlgorithm">
		/// A value from the <see cref="DigestAlgorithm"/> enumeration that identifies
		/// the message digest algorithm to apply.
		/// </param>
		/// <param name="pabytData">
		/// The byte array over which to compute the digest.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <returns>
		/// A byte array containing the computed message digest.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="penmAlgorithm"/> or
		/// <paramref name="pabytData"/> is null.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="pabytData"/> is empty.
		/// </exception>
		public static byte [ ] ComputeDigest ( DigestAlgorithm penmAlgorithm , byte [ ] pabytData )
		{
			using ( HashAlgorithm ha = CreateAlgorithm ( penmAlgorithm ) )
				return ComputeDigest ( ha , pabytData );
		}   // public static byte [ ] ComputeDigest Method (1 of 2)


		/// <summary>
		/// Compute a message digest over the contents of a readable stream using the
		/// specified <see cref="HashAlgorithm"/> instance.
		/// </summary>
		/// <param name="penmAlgorithm">
		/// A concrete <see cref="HashAlgorithm"/> instance used to compute the digest.
		/// </param>
		/// <param name="pstrmData">
		/// A readable stream containing the data to digest.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <returns>
		/// A byte array containing the computed message digest.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when either parameter is null.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown when the stream is empty or unreadable.
		/// </exception>
		public static byte [ ] ComputeDigest ( DigestAlgorithm penmAlgorithm , System.IO.Stream pstrmData )
		{
			using ( HashAlgorithm ha = CreateAlgorithm ( penmAlgorithm ) )
				return ComputeDigest ( ha , pstrmData );
		}   // public static byte [ ] ComputeDigest Method (2 of 2)


		/// <summary>
		/// Create a concrete <see cref="HashAlgorithm"/> instance corresponding to
		/// the specified <see cref="DigestAlgorithm"/> value.
		/// </summary>
		/// <param name="penmAlgorithm">
		/// A value from the <see cref="DigestAlgorithm"/> enumeration that identifies
		/// the desired message digest algorithm.
		/// </param>
		/// <returns>
		/// A concrete <see cref="HashAlgorithm"/> instance suitable for computing
		/// message digests using the specified algorithm.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when <paramref name="penmAlgorithm"/> does not identify a supported
		/// digest algorithm.
		/// </exception>
		public static HashAlgorithm CreateAlgorithm ( DigestAlgorithm penmAlgorithm )
		{
			switch ( penmAlgorithm )
			{
				case DigestAlgorithm.SHA256:
					return SHA256.Create ( );

				case DigestAlgorithm.SHA384:
					return SHA384.Create ( );

				case DigestAlgorithm.SHA512:
					return SHA512.Create ( );

				default:
					throw new System.ComponentModel.InvalidEnumArgumentException (
						nameof ( penmAlgorithm ) ,                              // string argumentName
						( int ) penmAlgorithm ,                                 // int invalidValue
						typeof ( DigestAlgorithm ) );                           // Type enumClass
			}   // switch ( penmAlgorithm )
		}   // public static HashAlgorithm CreateAlgorithm Method
		#endregion // ComputeDigest and CreateAlgorithm "factory" Methods


		#region ComputeSHA* over Byte Array
		/// <summary>
		/// Compute a SHA‑256 digest over a byte array.
		/// </summary>
		/// <param name="pabytData">
		/// <para>
		/// This byte array represents data, from whatever source, over which to
		/// compute a message digest (hash) using the SHA384 HashAlgorithm.
		/// </para>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		public static byte [ ] ComputeSHA256 ( byte [ ] pabytData )
		{
			using ( HashAlgorithm sha = SHA256.Create ( ) )
				return ComputeDigest ( sha , pabytData );
		}   // public static byte [ ] ComputeSHA256


		/// <summary>
		/// Compute a SHA‑384 digest over a byte array.
		/// </summary>
		/// <param name="pabytData">
		/// <para>
		/// This byte array represents data, from whatever source, over which to
		/// compute a message digest (hash) using the SHA384 HashAlgorithm.
		/// </para>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		public static byte [ ] ComputeSHA384 ( byte [ ] pabytData )
		{
			using ( HashAlgorithm sha = SHA384.Create ( ) )
				return ComputeDigest ( sha , pabytData );
		}   // public static byte [ ] ComputeSHA384


		/// <summary>
		/// Compute a SHA‑512 digest over a byte array.
		/// </summary>
		/// <param name="pabytData">
		/// <para>
		/// This byte array represents data, from whatever source, over which to
		/// compute a message digest (hash) using the SHA384 HashAlgorithm.
		/// </para>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		public static byte [ ] ComputeSHA512 ( byte [ ] pabytData )
		{
			using ( HashAlgorithm sha = SHA512.Create ( ) )
				return ComputeDigest ( sha , pabytData );
		}   // public static byte [ ] ComputeSHA512
		#endregion // ComputeSHA* over Byte Array                                


		#region // ComputeSHA* over readable stream
		/// <summary>
		/// Compute a SHA-256 digest over a readable stream.
		/// </summary>
		/// <param name="pstrmData">
		/// This System.IO.Stream object represents a System.IO.Stream, or an
		/// instance of a derived class, that contains the data to digest. The
		/// stream must be readable and <b>cannot</b> be empty.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		public static byte [ ] ComputeSHA256 ( System.IO.Stream pstrmData )
		{
			using ( HashAlgorithm sha = SHA256.Create ( ) )
				return ComputeDigest ( sha , pstrmData );
		}   // public static byte [ ] ComputeSHA256


		/// <summary>
		/// Compute a SHA-384 digest over a readable stream.
		/// </summary>
		/// <param name="pstrmData">
		/// This System.IO.Stream object represents a System.IO.Stream, or an
		/// instance of a derived class, that contains the data to digest. The
		/// stream must be readable and <b>cannot</b> be empty.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		public static byte [ ] ComputeSHA384 ( System.IO.Stream pstrmData )
		{
			using ( HashAlgorithm sha = SHA384.Create ( ) )
				return ComputeDigest ( sha , pstrmData );
		}   // public static byte [ ] ComputeSHA384


		/// <summary>
		/// Compute a SHA-512 digest over a readable stream.
		/// </summary>
		/// <param name="pstrmData">
		/// This System.IO.Stream object represents a System.IO.Stream, or an
		/// instance of a derived class, that contains the data to digest. The
		/// stream must be readable and <b>cannot</b> be empty.
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestEmptyInputWarning']/*" />
		/// </param>
		/// <include file="../InternalDocumentationXmlCopyBooks/DigestLengths.XML"
		///          path="doc/members/member[@name='DigestLengthTable']/*" />
		public static byte [ ] ComputeSHA512 ( System.IO.Stream pstrmData )
		{
			using ( HashAlgorithm sha = SHA512.Create ( ) )
				return ComputeDigest ( sha , pstrmData );
		}   // public static byte [ ] ComputeSHA512
		#endregion // // ComputeSHA* over readable stream                                        
	}   // public static class DigestEngines
}  // namespace WizardWrx.Cryptography