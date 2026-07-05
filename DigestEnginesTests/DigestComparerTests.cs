using System;
using System.IO;

using WizardWrx.Cryptography;

using Xunit;

namespace WizardWrx.Core.Tests
{
	/// <summary>
	/// Unit tests for the digest comparer subsystem using the same Test_Data
	/// files used by the FileComparer and DigestEngines test suites.
	/// </summary>
	public class DigestComparerTests
	{
		private static readonly string TestDataRoot = Path.Combine ( GitRepositoryNavigation.DiscoverRepoRoot ( Environment.CurrentDirectory ) , @"Test_Data" );

		private static string TD ( string fileName ) =>
			Path.Combine ( TestDataRoot , fileName );

		private const string SMALL_FILE_1 = "SmallFile1.txt";
		private const string SMALL_FILE_2 = "SmallFile2.txt";
		private const string SMALL_FILE_DUPLICATE = "SmallFileDuplicate.txt";

		private const string LARGE_FILE_1 = "LargeFile1.bin";
		private const string LARGE_FILE_2 = "LargeFile2.bin";
		private const string LARGE_FILE_DUPLICATE = "LargeFileDuplicate.bin";

		private const string EMPTY_FILE = "EmptyFile.txt";
		private const string NON_EXISTENT_FILE = "NoSuchFile.xyz";

		// --------------------------------------------------------------------
		// Small file tests
		// --------------------------------------------------------------------

		[Fact]
		public void DigestComparer_SmallFiles_Identical_ReturnsTrue ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_DUPLICATE ) );

			Assert.True ( result );
		}   // ublic void DigestComparer_SmallFiles_Identical_ReturnsTrue

		[Fact]
		public void DigestComparer_SmallFiles_Different_ReturnsFalse ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_2 ) );

			Assert.False ( result );
		}   // public void DigestComparer_SmallFiles_Different_ReturnsFalse

		// --------------------------------------------------------------------
		// Large file tests
		// --------------------------------------------------------------------

		[Fact]
		public void DigestComparer_LargeFiles_Identical_ReturnsTrue ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( LARGE_FILE_1 ) ,
				TD ( LARGE_FILE_DUPLICATE ) );

			Assert.True ( result );
		}   // public void DigestComparer_LargeFiles_Identical_ReturnsTrue

		[Fact]
		public void DigestComparer_LargeFiles_Different_ReturnsFalse ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( LARGE_FILE_1 ) ,
				TD ( LARGE_FILE_2 ) );

			Assert.False ( result );
		}   // public void DigestComparer_LargeFiles_Different_ReturnsFalse

		// --------------------------------------------------------------------
		// Empty file tests
		// --------------------------------------------------------------------

		[Fact]
		public void DigestComparer_EmptyFile_ThrowsArgumentException ( )
		{
			Assert.Throws<ArgumentException> ( ( ) =>
				DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( EMPTY_FILE ) ) );
		}   // public void DigestComparer_EmptyFile_ThrowsArgumentException

		// --------------------------------------------------------------------
		// Nonexistent file tests
		// --------------------------------------------------------------------

		[Fact]
		public void DigestComparer_NonexistentFile_ThrowsFileNotFound ( )
		{
			Assert.Throws<FileNotFoundException> ( ( ) =>
				DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( NON_EXISTENT_FILE ) ) );
		}   // public void DigestComparer_NonexistentFile_ThrowsFileNotFound

		// --------------------------------------------------------------------
		// Cross-validation against FileComparer
		// --------------------------------------------------------------------

		[Fact]
		public void DigestComparer_CrossValidate_FileComparer_AgreeOnIdentity ( )
		{
			bool fc = FileComparer.CompareFiles (
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_DUPLICATE ) );

			bool dc = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_DUPLICATE ) );

			Assert.True ( fc );
			Assert.True ( dc );
		}   // public void DigestComparer_CrossValidate_FileComparer_AgreeOnIdentity

		[Fact]
		public void DigestComparer_CrossValidate_FileComparer_AgreeOnDifference ( )
		{
			bool fc = FileComparer.CompareFiles (
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_2 ) );

			bool dc = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA256 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_2 ) );   // ✔ Correct file pair

			Assert.False ( fc );
			Assert.False ( dc );
		}   // public void DigestComparer_CrossValidate_FileComparer_AgreeOnDifference

		// --------------------------------------------------------------------
		// SHA384 and SHA512 tests, one of each kind
		//
		// NOTE: The SHA384 and SHA512 algorithms are not as widely used as
		//		 SHA256, but they are still important for certain applications.
		//		 These tests ensure that the DigestComparer works correctly
		//		 with these algorithms as well, as they should because the code
		//		 is designed to be algorithm-agnostic.
		//
		//		 In particular, the DigestEngines class that DigestComparer
		//		 uses internally supports these algorithms by implementing a
		//		 HashAlgorithm instance and calling its ComputeDigest method,
		//		 so we want to make sure that the comparison logic works for
		//		 them too, although it should, since all digests are generated
		//		 by calling the ComputeDigest method on an instance of a class
		//		 derived from HashAlgorithm.
		// --------------------------------------------------------------------

		[Fact]
		public void DigestComparer_SHA384_SmallFiles_Identical_ReturnsTrue ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA384 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_DUPLICATE ) );

			Assert.True ( result );
		}   // public void DigestComparer_SHA384_SmallFiles_Identical_ReturnsTrue

		[Fact]
		public void DigestComparer_SHA384_SmallFiles_Different_ReturnsFalse ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA384 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_2 ) );

			Assert.False ( result );
		}   // public void DigestComparer_SHA384_SmallFiles_Different_ReturnsFalse

		[Fact]
		public void DigestComparer_SHA512_SmallFiles_Identical_ReturnsTrue ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA512 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_DUPLICATE ) );

			Assert.True ( result );
		}   // public void DigestComparer_SHA512_SmallFiles_Identical_ReturnsTrue

		[Fact]
		public void DigestComparer_SHA512_SmallFiles_Different_ReturnsFalse ( )
		{
			bool result = DigestComparer.CompareDigests (
				DigestAlgorithm.SHA512 ,
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_2 ) );

			Assert.False ( result );
		}   // public void DigestComparer_SHA512_SmallFiles_Different_ReturnsFalse
	}   // public class DigestComparerTests
}   // partial namespace WizardWrx.Core.Tests