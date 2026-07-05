using System;
using System.IO;

using WizardWrx.Cryptography;

using Xunit;

namespace WizardWrx.Core.Tests
{
	/// <summary>
	/// Unit tests for the digest subsystem using the same Test_Data files
	/// used by the FileComparer test suite. This ensures consistent behavior
	/// across both subsystems and validates digest correctness under realistic
	/// conditions.
	/// </summary>
	public class DigestEnginesTests
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

		// Known digest of empty file (MD5 of empty string)
		private const string MD5_EMPTY = "d41d8cd98f00b204e9800998ecf8427e";

		// --------------------------------------------------------------------
		// Small file tests (gulping)
		// --------------------------------------------------------------------

		[Fact]
		public void Digest_SmallFiles_Identical_ReturnsSameDigest ( )
		{
			string d1 = DigestFile.SHA256Hash( TD ( SMALL_FILE_1 ) );
			string d2 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_DUPLICATE ) );

			Assert.Equal ( d1 , d2 );
			Assert.True ( FileComparer.CompareFiles ( TD ( SMALL_FILE_1 ) , TD ( SMALL_FILE_DUPLICATE ) ) );
		}

		[Fact]
		public void Digest_SmallFiles_Different_ReturnsDifferentDigest ( )
		{
			string d1 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_1 ) );
			string d2 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_2 ) );

			Assert.NotEqual ( d1 , d2 );
			Assert.False ( FileComparer.CompareFiles ( TD ( SMALL_FILE_1 ) , TD ( SMALL_FILE_2 ) ) );
		}

		// --------------------------------------------------------------------
		// Large file tests (streaming)
		// --------------------------------------------------------------------

		[Fact]
		public void Digest_LargeFiles_Identical_ReturnsSameDigest ( )
		{
			string d1 = DigestFile.SHA256Hash ( TD ( LARGE_FILE_1 ) );
			string d2 = DigestFile.SHA256Hash ( TD ( LARGE_FILE_DUPLICATE ) );

			Assert.Equal ( d1 , d2 );
			Assert.True ( FileComparer.CompareFiles ( TD ( LARGE_FILE_1 ) , TD ( LARGE_FILE_DUPLICATE ) ) );
		}

		[Fact]
		public void Digest_LargeFiles_Different_ReturnsDifferentDigest ( )
		{
			string d1 = DigestFile.SHA256Hash ( TD ( LARGE_FILE_1 ) );
			string d2 = DigestFile.SHA256Hash ( TD ( LARGE_FILE_2 ) );

			Assert.NotEqual ( d1 , d2 );
			Assert.False ( FileComparer.CompareFiles ( TD ( LARGE_FILE_1 ) , TD ( LARGE_FILE_2 ) ) );
		}

		// --------------------------------------------------------------------
		// Empty file tests
		// --------------------------------------------------------------------

		[Fact]
		public void Digest_EmptyFile_ThrowsArgumentException ( )
		{
			Assert.Throws<ArgumentException> ( ( ) =>
				DigestFile.SHA256Hash ( TD ( EMPTY_FILE ) ) );
		}

		[Fact]
		public void Digest_EmptyFile_CompareFilesThrows ( )
		{
			Assert.Throws<ArgumentException> ( ( ) =>
				FileComparer.CompareFiles ( TD ( EMPTY_FILE ) , TD ( EMPTY_FILE ) ) );
		}

		// --------------------------------------------------------------------
		// Nonexistent file tests
		// --------------------------------------------------------------------

		[Fact]
		public void Digest_NonexistentFile_ThrowsFileNotFound ( )
		{
			Assert.Throws<FileNotFoundException> ( ( ) =>
				DigestFile.SHA256Hash ( TD ( NON_EXISTENT_FILE ) ) );
		}

		//[Fact]
		//public void Digest_NonexistentFile_TryDigestReturnsFalse ( )
		//{
		//	bool result = DigestEngines.TryComputeMD5 (
		//		TD ( NON_EXISTENT_FILE ) ,
		//		out string digest ,
		//		out Exception ex );

		//	Assert.False ( result );
		//	Assert.Null ( digest );
		//	Assert.NotNull ( ex );
		//	Assert.IsType<FileNotFoundException> ( ex );
		//}

		// --------------------------------------------------------------------
		// Cross-validation tests
		// --------------------------------------------------------------------

		[Fact]
		public void Digest_CrossValidate_FileComparer_AgreeOnIdentity ( )
		{
			bool fc = FileComparer.CompareFiles ( TD ( SMALL_FILE_1 ) , TD ( SMALL_FILE_DUPLICATE ) );
			string d1 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_1 ) );
			string d2 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_DUPLICATE ) );

			Assert.True ( fc );
			Assert.Equal ( d1 , d2 );
		}

		[Fact]
		public void Digest_CrossValidate_FileComparer_AgreeOnDifference ( )
		{
			bool fc = FileComparer.CompareFiles ( TD ( SMALL_FILE_1 ) , TD ( SMALL_FILE_2 ) );
			string d1 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_1 ) );
			string d2 = DigestFile.SHA256Hash ( TD ( SMALL_FILE_2 ) );

			Assert.False ( fc );
			Assert.NotEqual ( d1 , d2 );
		}
	}
}   // partial namespace WizardWrx.Core.Tests