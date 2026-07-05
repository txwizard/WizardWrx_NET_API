using System;
using System.IO;
using Xunit;

using WizardWrx;

namespace WizardWrx.Core.Tests
{
	public class FileComparerTests
	{
		private static readonly string TestDataRoot = Path.Combine ( GitRepositoryNavigation.DiscoverRepoRoot ( Environment.CurrentDirectory ) , @"Test_Data" );

		private static string TD ( string fileName ) => Path.Combine ( TestDataRoot , fileName );

		private const string SMALL_FILE_1 = "SmallFile1.txt";
		private const string SMALL_FILE_2 = "SmallFile2.txt";
		private const string SMALL_FILE_DUPLICATE = "SmallFileDuplicate.txt";

		private const string LARGE_FILE_1 = "LargeFile1.bin";
		private const string LARGE_FILE_2 = "LargeFile2.bin";
		private const string LARGE_FILE_DUPLICATE = "LargeFileDuplicate.bin";

		private const string EMPTY_FILE = "EmptyFile.txt";
		private const string NON_EXISTENT_FILE = "NoSuchFile.xyz";

		[Fact]
		public void CompareFiles_SmallFiles_Identical_ReturnsTrue ( )
		{
			bool result = FileComparer.CompareFiles (
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_DUPLICATE ) );

			Assert.True ( result );
		}   // public void CompareFiles_SmallFiles_Identical_ReturnsTrue


		[Fact]
		public void CompareFiles_SmallFiles_Different_ReturnsFalse ( )
		{
			bool result = FileComparer.CompareFiles (
				TD ( SMALL_FILE_1 ) ,
				TD ( SMALL_FILE_2 ) );

			Assert.False ( result );
		}   // public void CompareFiles_SmallFiles_Different_ReturnsFalse


		[Fact]
		public void CompareFiles_LargeFiles_Identical_ReturnsTrue ( )
		{
			bool result = FileComparer.CompareFiles (
				TD ( LARGE_FILE_1 ) ,
				TD ( LARGE_FILE_DUPLICATE ) );

			Assert.True ( result );

			FileComparer.ReadAllBytesCeiling = FileComparer.DEFAULT_READ_ALL_BYTES_CEILING;
		}   // public void CompareFiles_LargeFiles_Identical_ReturnsTrue


		[Fact]
		public void CompareFiles_LargeFiles_Different_ReturnsFalse ( )
		{
			bool result = FileComparer.CompareFiles (
				TD ( LARGE_FILE_1 ) ,
				TD ( LARGE_FILE_2 ) );

			Assert.False ( result );

			FileComparer.ReadAllBytesCeiling = FileComparer.DEFAULT_READ_ALL_BYTES_CEILING;
		}   // public void CompareFiles_LargeFiles_Different_ReturnsFalse


		[Fact]
		public void CompareFiles_FirstFileMissing_Throws ( )
		{
			Assert.Throws<FileNotFoundException> ( ( ) =>
				FileComparer.CompareFiles (
					TD ( NON_EXISTENT_FILE ) ,
					TD ( SMALL_FILE_1 ) ) );
		}   // public void CompareFiles_FirstFileMissing_Throws


		[Fact]
		public void CompareFiles_SecondFileMissing_Throws ( )
		{
			Assert.Throws<FileNotFoundException> ( ( ) =>
				FileComparer.CompareFiles (
					TD ( SMALL_FILE_1 ) ,
					TD ( NON_EXISTENT_FILE ) ) );
		}   // public void CompareFiles_SecondFileMissing_Throws


		[Fact]
		public void TryCompareFiles_FileMissing_ReturnsFalseAndOutputsException ( )
		{
			bool result = FileComparer.TryCompareFiles (
				TD ( NON_EXISTENT_FILE ) ,
				TD ( SMALL_FILE_1 ) ,
				out Exception ex );

			Assert.False ( result );
			Assert.NotNull ( ex );
			Assert.IsType<FileNotFoundException> ( ex );
		}   // public void TryCompareFiles_FileMissing_ReturnsFalseAndOutputsException


		[Fact]
		public void CompareByteArrays_Identical_ReturnsTrue ( )
		{
			byte [ ] a = new byte [ ] { 1 , 2 , 3 , 4 };
			byte [ ] b = new byte [ ] { 1 , 2 , 3 , 4 };

			Assert.True ( FileComparer.CompareByteArrays ( a , b ) );
		}   // public void CompareByteArrays_Identical_ReturnsTrue 


		[Fact]
		public void CompareByteArrays_Different_ReturnsFalse ( )
		{
			byte [ ] a = new byte [ ] { 1 , 2 , 3 , 4 };
			byte [ ] b = new byte [ ] { 1 , 2 , 3 , 9 };

			Assert.False ( FileComparer.CompareByteArrays ( a , b ) );
		}   // public void CompareByteArrays_Different_ReturnsFalse 


		[Fact]
		public void CompareByteArrays_Empty_Throws ( )
		{
			byte [ ] empty = Array.Empty<byte> ( );
			byte [ ] nonEmpty = new byte [ ] { 1 };

			Assert.Throws<ArgumentException> ( ( ) =>
				FileComparer.CompareByteArrays ( empty , nonEmpty ) );

			Assert.Throws<ArgumentException> ( ( ) =>
				FileComparer.CompareByteArrays ( nonEmpty , empty ) );
		}   // public void CompareByteArrays_Empty_Throws


		[Fact]
		public void CompareByteArrays_Null_Throws ( )
		{
			byte [ ] valid = new byte [ ] { 1 };

			Assert.Throws<ArgumentNullException> ( ( ) =>
				FileComparer.CompareByteArrays ( null , valid ) );

			Assert.Throws<ArgumentNullException> ( ( ) =>
				FileComparer.CompareByteArrays ( valid , null ) );
		}   // public void CompareByteArrays_Null_Throws


		[Fact]
		public void CompareFiles_EmptyFiles_ThrowsArgumentException ( )
		{
			string emptyFile = TD ( EMPTY_FILE );

			Assert.Throws<ArgumentException> ( ( ) =>
				FileComparer.CompareFiles ( emptyFile , emptyFile ) );
		}   // public void CompareFiles_EmptyFiles_ThrowsArgumentException


		[Fact]
		public void TryCompareFiles_EmptyFiles_ReturnsFalseAndOutputsException ( )
		{
			string emptyFile = TD ( EMPTY_FILE );

			bool result = FileComparer.TryCompareFiles (
				emptyFile ,
				emptyFile ,
				out Exception ex );

			Assert.False ( result );
			Assert.NotNull ( ex );
			Assert.IsType<ArgumentException> ( ex );
		}   // public void TryCompareFiles_EmptyFiles_ReturnsFalseAndOutputsException
	}   // public class FileComparerTests
}   // namespace WizardWrx.Core.Tests