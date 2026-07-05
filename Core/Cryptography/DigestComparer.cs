using System;
using System.IO;

using WizardWrx.Core;

namespace WizardWrx.Cryptography
{
	/// <summary>
	/// This static class exposes a method that compares two message digests
	/// represented as byte arrays. The CompareDigests method takes two byte
	/// arrays as input and returns a boolean value indicating whether the two
	/// digests are equal. The method first checks for null arguments and
	/// throws an ArgumentNullException if either of the input arrays is null.
	/// It then compares the lengths of the two arrays, returning false if they
	/// are not equal. Finally, it iterates through each byte in the arrays,
	/// comparing them one by one. If any byte differs, the method returns
	/// false; otherwise, it returns true, indicating that the two digests are
	/// identical.
	/// </summary>
	public static class DigestComparer
	{
		/// <summary>
		/// This static method compares two arrays, each of which represents a
		/// message digest, and returns a boolean value indicating whether the
		/// two digests are equal. The method first checks for null arguments
		/// and throws an ArgumentNullException if either of the input arrays
		/// is null. It then compares the lengths of the two arrays, returning
		/// false if they are not equal. Finally, it iterates through each byte
		/// in the arrays, comparing them one by one. If any byte differs, the
		/// method returns false; otherwise, it returns true, indicating that
		/// the two digests are identical.
		/// </summary>
		/// <param name="pabytDigest1">
		/// This required reference to a byte array represents tThe first of two
		/// digests to compare.
		/// </param>
		/// <param name="pabytDigest2">
		/// This required reference to a byte array represents tThe second of
		/// two digests to compare.
		/// </param>
		/// <returns>
		/// If it succeeds, the return value is True if the digests are
		/// identical; otherwise, false.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// An ArgumentNullException arises when either byte array 
		/// (<paramref name="pabytDigest1"/> or <paramref name="pabytDigest2"/>)
		/// is null.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// An ArgumentNullException arises when either byte array 
		/// (<paramref name="pabytDigest1"/> or <paramref name="pabytDigest2"/>)
		/// is empty. ALL message digest algorithms return a non-empty array.
		/// </exception>
		public static bool CompareDigests ( byte [ ] pabytDigest1 , byte [ ] pabytDigest2 )
		{
			if ( pabytDigest1 == null )
			{
				throw new ArgumentNullException ( nameof ( pabytDigest1 ) );
			}   // TRUE (unanticipated outcome) block, if ( pabytDigest1 == null )

			if ( pabytDigest2 == null )
			{
				throw new ArgumentNullException ( nameof ( pabytDigest2 ) );
			}   // TRUE (unanticipated outcome) block, if ( pabytDigest2 == null )

			if ( pabytDigest1.Length == ArrayInfo.ARRAY_IS_EMPTY)
			{
				throw new ArgumentOutOfRangeException ( $"The FIRST digest array is empty. It is not the output of a message digest algorithm. {nameof ( pabytDigest1 )} Length = {pabytDigest1.Length}" );
			}   // TRUE (If the array length is equal to zero, the array is NOT the output of a message digest.) block, if ( pabytDigest1.Length == ArrayInfo.ARRAY_IS_EMPTY)

			if ( pabytDigest2.Length == ArrayInfo.ARRAY_IS_EMPTY )
			{
				throw new ArgumentOutOfRangeException ( $"The SECOND digest array is empty. It is not the output of a message digest algorithm. {nameof ( pabytDigest2 )} Length = {pabytDigest2.Length}" );
			}   // TRUE (If the array length is equal to zero, the array is NOT the output of a message digest.) block, if ( pabytDigest1.Length == ArrayInfo.ARRAY_IS_EMPTY)

			if ( pabytDigest1.Length != pabytDigest2.Length )
			{
				throw new ArgumentException ( $"Digest lengths are unequal. Either the arrays represent the outputs of different digest algorithms, which CANNOT be compared, as such a comparison is meaningless, or one or both is NOT the output of a message digest algorithm. {nameof ( pabytDigest1 )} Length = {pabytDigest1.Length}, {nameof ( pabytDigest2 )} Length = {pabytDigest2.Length}" );
			}   // TRUE (If the array lengths are unequal, they probably don't represent message digests. if ( pabytDigest1.Length != pabytDigest2.Length )

			for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ;
				      i < pabytDigest1.Length ;
					  ++i )
			{
				if ( pabytDigest1 [ i ] != pabytDigest2 [ i ] )
					return false;
			}   // for ( int i = 0 ; i < abytDigest1.Length ; ++i )

			return true;
		}   // public static bool CompareDigests


		/// <summary>
		/// Compare the digests of two files, which may be specified by either
		/// relative or absolute paths, and return a boolean value indicating
		/// whether the digests are identical.
		/// </summary>
		/// <param name="penmAlgorithm">
		/// This DigestAlgorithm enumeration member identifies the message
		/// digest algorithm to use to compute the digests of the two files.
		/// </param>
		/// <param name="pstrAbsoluteOrRelativeFileName1">
		/// This required string represents the path to the first file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <param name="pstrAbsoluteOrRelativeFileName2">
		/// This required string represents the path to the second file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <returns>
		/// If it succeeds, the return value is True if the digests are
		/// identical; otherwise, false.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// A ArgumentNullException arises when either string argument is null
		/// or empty.
		/// </exception>
		public static bool CompareDigests ( DigestAlgorithm penmAlgorithm, string pstrAbsoluteOrRelativeFileName1 , string pstrAbsoluteOrRelativeFileName2 )
		{
			if ( string.IsNullOrEmpty ( pstrAbsoluteOrRelativeFileName1 ) )
			{
				throw new ArgumentNullException ( nameof ( pstrAbsoluteOrRelativeFileName1 ) );
			}   // TRUE (unanticipated outcome) block, if ( string.IsNullOrEmpty ( pstrAbsoluteOrRelativeFileName1 ) )
			if ( string.IsNullOrEmpty ( pstrAbsoluteOrRelativeFileName2 ) )
			{
				throw new ArgumentNullException ( nameof ( pstrAbsoluteOrRelativeFileName2 ) );
			}   // TRUE (unanticipated outcome) block, if ( string.IsNullOrEmpty ( pstrAbsoluteOrRelativeFileName2 ) )

			FileInfo fi1 = new FileInfo ( pstrAbsoluteOrRelativeFileName1 );
			FileInfo fi2 = new FileInfo ( pstrAbsoluteOrRelativeFileName2 );

			byte [ ] abytDigest1 = ComputeDigestForFile ( penmAlgorithm , fi1.FullName );
			byte [ ] abytDigest2 = ComputeDigestForFile ( penmAlgorithm , fi2.FullName );

			return CompareDigests ( abytDigest1 , abytDigest2 );
		}   // public static bool CompareDigests


		/// <summary>
		/// Compute the digest of one file, which may be specified by either
		/// relative or absolute paths, and return a byte array representing
		/// the computed digest. File processing is optimized to avoid reading
		/// the entire file into memory if the file size exceeds a predefined
		/// threshold (DEFAULT_READ_ALL_BYTES_CEILING). In such cases, the file
		/// is read in chunks using a FileStream, which is more memory-efficient
		/// for large files. For smaller files, the entire content is read into 
		/// memory for digest computation.
		/// </summary>
		/// <param name="penmAlgorithm">
		/// This DigestAlgorithm enumeration member identifies the message
		/// digest algorithm to use to compute the digest.
		/// </param>
		/// <param name="pstrAbsoluteOrRelativeFileName">
		/// This required string represents the path to the file to process, 
		/// which may be either a relative or absolute path.
		/// </param>
		/// <returns>
		/// The return value is a byte array representing the computed digest.
		/// </returns>
		private static byte [ ] ComputeDigestForFile ( DigestAlgorithm penmAlgorithm , string pstrAbsoluteOrRelativeFileName )
		{
			FileInfo fi = new FileInfo ( pstrAbsoluteOrRelativeFileName );

			if ( fi.Length > FileComparer.DEFAULT_READ_ALL_BYTES_CEILING )
			{
				using ( FileStream fs = new FileStream (
					pstrAbsoluteOrRelativeFileName ,
					FileMode.Open ,
					FileAccess.Read ,
					FileShare.Read ) )
				{
					return DigestEngines.ComputeDigest ( penmAlgorithm , fs );
				}   // using ( FileStream fs = new FileStream ( pstrAbsoluteOrRelativeFileName , FileMode.Open , FileAccess.Read , FileShare.Read ) )
			}   // if ( fi.Length > FileComparer.DEFAULT_READ_ALL_BYTES_CEILING )

			return DigestEngines.ComputeDigest (
				penmAlgorithm ,
				DigestFile.GetFileContent ( pstrAbsoluteOrRelativeFileName ) );
		}   // private static byte [ ] ComputeDigestForFile
	}   // public static class DigestComparer
}   // partial namespace WizardWrx.Cryptography