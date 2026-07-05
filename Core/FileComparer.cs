using System;
using System.IO;

namespace WizardWrx.Core
{
	/// <summary>
	/// Compare files and byte arrays for strict, byte-for-byte equality.
	/// This class supports both small-file comparison using
	/// <see cref="File.ReadAllBytes(string)"/> and scalable streaming
	/// comparison for large files.
	/// </summary>
	public static class FileComparer
	{
		/// <summary>
		/// Default ceiling (in bytes) below which file comparison uses
		/// <see cref="File.ReadAllBytes(string)"/> instead of streaming.
		/// </summary>
		public const long DEFAULT_READ_ALL_BYTES_CEILING = MagicNumbers.CAPACITY_ONE_MEGABYTE;


		/// <summary>
		/// Default streaming buffer size (in bytes). This value is a
		/// power of two, consistent with typical buffered I/O practice.
		/// </summary>
		public const int DEFAULT_STREAMING_BUFFER_SIZE = MagicNumbers.BUFFER_SIZE_80KB;


		/// <summary>
		/// This private static long integer is the backing store for read/write
		/// property <see cref="ReadAllBytesCeiling"/>. It represents the
		/// maximum file size (in bytes) for which <see cref="File.ReadAllBytes(string)"/>
		/// is used instead of streaming comparison.
		/// </summary>
		private static long s_readAllBytesCeiling = DEFAULT_READ_ALL_BYTES_CEILING;


		/// <summary>
		/// This private static integer is the backing store for read/write
		/// property <see cref="StreamingBufferSize"/>. It represents the
		/// buffer size (in bytes) used for streaming file comparison.
		/// </summary>
		private static int s_streamingBufferSize = DEFAULT_STREAMING_BUFFER_SIZE;


		/// <summary>
		/// This read/write property gets or sets the maximum file size (in
		/// bytes) for which <see cref="File.ReadAllBytes(string)"/> is used
		/// instead of streaming comparison.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// An ArgumentOutOfRangeException Exception arises when the assigned
		/// value is less than or equal to zero.
		/// </exception>
		public static long ReadAllBytesCeiling
		{
			get => s_readAllBytesCeiling;
			set
			{
				if ( value <= 0 )
					throw new ArgumentOutOfRangeException ( nameof ( value ) , $"Ceiling must be positive. Specified value = {value}" );
				s_readAllBytesCeiling = value;
			}   // public static long ReadAllBytesCeiling property getter
		}   // public static long ReadAllBytesCeiling property


		/// <summary>
		/// This read/write property gets or sets the buffer size (in bytes)
		/// used for streaming file comparison.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// An ArgumentOutOfRangeException Exception arises when the assigned
		/// value is less than or equal to zero.
		/// </exception>
		public static int StreamingBufferSize
		{
			get => s_streamingBufferSize;
			set
			{
				if ( value <= 0 )
					throw new ArgumentOutOfRangeException ( nameof ( value ) , "Buffer size must be positive." );
				s_streamingBufferSize = value;
			}   // public static int StreamingBufferSize property getter
		}   // public static int StreamingBufferSize property


		/// <summary>
		/// Compare two files for strict, byte-for-byte equality. This method
		/// throws exceptions for invalid input or I/O errors.
		/// </summary>
		/// <param name="pstrAbsoluteOrRelativeFileName1">
		/// This required string represents the path to the first file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <param name="pstrAbsoluteOrRelativeFileName2">
		/// This required string represents the path to the second file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <returns>
		/// <c>true</c> if the files are identical byte-for-byte;
		/// otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown if either file path is null or empty.
		/// </exception>
		/// <exception cref="FileNotFoundException">
		/// Thrown if either file does not exist.
		/// </exception>
		public static bool CompareFiles ( string pstrAbsoluteOrRelativeFileName1 , string pstrAbsoluteOrRelativeFileName2 )
		{
			if ( string.IsNullOrWhiteSpace ( pstrAbsoluteOrRelativeFileName1 ) )
				throw new ArgumentNullException ( nameof ( pstrAbsoluteOrRelativeFileName1 ) );
			if ( string.IsNullOrWhiteSpace ( pstrAbsoluteOrRelativeFileName2 ) )
				throw new ArgumentNullException ( nameof ( pstrAbsoluteOrRelativeFileName2 ) );

			FileInfo fi1 = new FileInfo ( pstrAbsoluteOrRelativeFileName1 );
			FileInfo fi2 = new FileInfo ( pstrAbsoluteOrRelativeFileName2 );

			if ( !fi1.Exists )
				throw new FileNotFoundException ( $"File not found. Specified FileName = {pstrAbsoluteOrRelativeFileName1.QuoteString ( )}, Resolved (absolute) FileName = {fi1.FullName.QuoteString ( )}" );
			if ( !fi2.Exists )
				throw new FileNotFoundException ( $"File not found. Specified FileName = {pstrAbsoluteOrRelativeFileName2.QuoteString ( )}, Resolved (absolute) FileName = {fi2.FullName.QuoteString ( )}" );

			if ( fi1.Length != fi2.Length )
				return false;

			if ( fi1.Length <= ReadAllBytesCeiling )
			{
				byte [ ] abytFile1Bytes = File.ReadAllBytes ( pstrAbsoluteOrRelativeFileName1 );
				byte [ ] aBytFile2Bytes = File.ReadAllBytes ( pstrAbsoluteOrRelativeFileName2 );
				return CompareByteArrays ( abytFile1Bytes , aBytFile2Bytes );
			}   // if ( fi1.Length <= ReadAllBytesCeiling )

			return CompareFilesStreaming ( pstrAbsoluteOrRelativeFileName1 , pstrAbsoluteOrRelativeFileName2 );
		}   // public static bool CompareFiles


		/// <summary>
		/// Safe version of <see cref="CompareFiles(string, string)"/>.
		/// This method never throws; instead, it returns <c>false</c>
		/// and outputs the exception that would have been thrown.
		/// </summary>
		/// <param name="pstrAbsoluteOrRelativeFileName1">
		/// This required string represents the path to the first file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <param name="pstrAbsoluteOrRelativeFileName2">
		/// This required string represents the path to the second file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <param name="opex">
		/// Receives the exception if an error occurs; otherwise, null.
		/// </param>
		/// <returns>
		/// <c>true</c> if the files are identical; otherwise, <c>false</c>.
		/// </returns>
		public static bool TryCompareFiles ( string pstrAbsoluteOrRelativeFileName1 , string pstrAbsoluteOrRelativeFileName2 , out Exception opex )
		{
			try
			{
				opex = null;
				return CompareFiles ( pstrAbsoluteOrRelativeFileName1 , pstrAbsoluteOrRelativeFileName2 );
			}
			catch ( Exception ex )
			{
				opex = ex;
				return false;
			}
		}   // public static bool TryCompareFiles


		/// <summary>
		/// Compare two byte arrays for strict equality.
		/// </summary>
		/// <param name="pabytByteArray1">First byte array.</param>
		/// <param name="pabytByteArray2">Second byte array.</param>
		/// <returns>
		/// <c>true</c> if the arrays are identical; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown if either array is null.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown if either array is empty.
		/// </exception>
		public static bool CompareByteArrays ( byte [ ] pabytByteArray1 , byte [ ] pabytByteArray2 )
		{
			if ( pabytByteArray1 == null )
				throw new ArgumentNullException ( nameof ( pabytByteArray1 ) );
			if ( pabytByteArray2 == null )
				throw new ArgumentNullException ( nameof ( pabytByteArray2 ) );

			if ( ReferenceEquals ( pabytByteArray1 , pabytByteArray2 ) )
				return true;

			if ( pabytByteArray1.Length == 0 || pabytByteArray2.Length == 0 )
				throw new ArgumentException ( "Byte arrays must not be empty." );

			if ( pabytByteArray1.Length != pabytByteArray2.Length )
				return false;

			for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ;
				      i < pabytByteArray1.Length ;
					  i++ )
			{
				if ( pabytByteArray1 [ i ] != pabytByteArray2 [ i ] )
					return false;
			}   // for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ; i < a.Length ; i++ )

			return true;
		}   // public static bool CompareByteArrays


		/// <summary>
		/// Streaming comparison of two files using a configurable buffer.
		/// Intended for large files where <see cref="File.ReadAllBytes(string)"/>
		/// would be inefficient.
		/// </summary>
		/// <param name="pstrAbsoluteOrRelativeFileName1">
		/// This required string represents the path to the first file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <param name="pstrAbsoluteOrRelativeFileName2">
		/// This required string represents the path to the second file, which
		/// may be either a relative or absolute path.
		/// </param>
		/// <returns>
		/// <c>true</c> if the files are identical; otherwise, <c>false</c>.
		/// </returns>
		public static bool CompareFilesStreaming ( string pstrAbsoluteOrRelativeFileName1 , string pstrAbsoluteOrRelativeFileName2 )
		{
			using ( var fs1 = new FileStream ( pstrAbsoluteOrRelativeFileName1 , FileMode.Open , FileAccess.Read , FileShare.Read ) )
			using ( var fs2 = new FileStream ( pstrAbsoluteOrRelativeFileName2 , FileMode.Open , FileAccess.Read , FileShare.Read ) )
			{
				byte [ ] abytFileBuffer1 = new byte [ StreamingBufferSize ];
				byte [ ] abytFileBuffer2 = new byte [ StreamingBufferSize ];

				while ( true )
				{
					//	------------------------------------------------------------------------
					//	The total number of bytes read into the buffer. This might be less than
					//	the number of bytes requested if that number of bytes are not currently
					//	available, or zero if the end of the stream is reached.
					//	------------------------------------------------------------------------

					int intStreamReadCount1 = fs1.Read ( abytFileBuffer1 , ListInfo.BEGINNING_OF_BUFFER , StreamingBufferSize );
					int intStreamReadCount2 = fs2.Read ( abytFileBuffer2 , ListInfo.BEGINNING_OF_BUFFER , StreamingBufferSize );

					if ( intStreamReadCount1 != intStreamReadCount2 )
						return false;

					if ( intStreamReadCount1 == MagicNumbers.ZERO )
						break;

					for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ; 
						      i < intStreamReadCount1 ; 
							  i++ )
					{
						if ( abytFileBuffer1 [ i ] != abytFileBuffer2 [ i ] )
							return false;
					}   // for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ; i < r1 ; i++ )
				}   // while ( true )
			}   //     using ( var fs1 = new FileStream ( pstrAbsoluteOrRelativeFileName1 , FileMode.Open , FileAccess.Read , FileShare.Read ) )
				// AND using ( var fs2 = new FileStream ( pstrAbsoluteOrRelativeFileName2 , FileMode.Open , FileAccess.Read , FileShare.Read ) )

			return true;
		}   // public static bool CompareFilesStreaming
	}   // public static class FileComparer
}   // partial namespace WizardWrx.Core