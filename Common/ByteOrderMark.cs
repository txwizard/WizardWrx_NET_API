/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         ByteOrderMark

    File Name:          ByteOrderMark.cs

    Synopsis:           The methods and properties of this class simplify the
						task of evaluating a byte array for presence of a Byte
						Order Mark, and identifying it.

    Remarks:            A sibling class uses an instance of this class to strip
						the Byte Order Mark, if present, from the byte array into
						which a custom resource is read from the containing
						assembly.

    Author:             David A. Gray

	License:            Copyright (C) 2014-2022, David A. Gray.
						All rights reserved.

                        Redistribution and use in source and binary forms, with
                        or without modification, are permitted provided that the
                        following conditions are met:

                        *   Redistributions of source code must retain the above
                            copyright notice, this list of conditions and the
                            following disclaimer.

                        *   Redistributions in binary form must reproduce the
                            above copyright notice, this list of conditions and
                            the following disclaimer in the documentation and/or
                            other materials provided with the distribution.

                        *   Neither the name of David A. Gray, nor the names of
                            his contributors may be used to endorse or promote
                            products derived from this software without specific
                            prior written permission.

                        THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
                        CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
                        WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
                        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
                        PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
                        David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
                        INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
                        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
                        SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
                        PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
                        ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
                        LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
                        ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
                        IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    Created:            Sunday, 19 March 2017 and Monday, 20 March 2017

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2017/03/20 7.0     DAG    This class arose from the discovery that a text
                              file created by the Visual Studio text editor gets
                              a Byte Order Mark.

	2018/11/11 7.11    DAG    Re-cast the text of the embedded help topics in an
                              active voice whereever it made sense to do so.

	2022/11/22 9.0.246 DAG    Move this class from WizardWrx.EmbeddedTextFile to
                              WizardWrx.Common, trading the dedicated namespace
                              for the root WizardWrx namespace. 
    ============================================================================
*/

namespace WizardWrx
{
	/// <summary>
	/// Use this class to evaluate arbitrary byte arrays for the presence of a Byte Order Mark.
	/// </summary>
    public class ByteOrderMark
	{
		#region Public Enumerations
		/// <summary>
		/// Members of this enumeration report the type of byte order mark, if
		/// any, present at its beginning.
		/// </summary>
		public enum BOMType
		{
			/// <summary>
			/// The array has no Byte Order Mark.
			/// </summary>
			None = 0 ,

			/// <summary>
			/// The array has a UTF-8 Byte Order Mark.
			/// </summary>
			UTF8 = 1 ,

			/// <summary>
			/// The array has a UTF-16 Byte Order Mark that indicates 
			/// little-endian character encoding.
			/// </summary>
			/// <remarks>
			/// The little-endian encoding is the native encoding of the entire
			/// Intel processor family and its clones (e. g., AMD).
			/// </remarks>
			UTF16LE = 2 ,

			/// <summary>
			/// The array has a UTF-16 Byte Order Mark that indicates big-endian
			/// character encoding.
			/// </summary>
			/// <remarks>
			/// The big-endian encoding is the native encoding of the MIPS Alpha
			/// family of processors, among others.
			/// </remarks>
			UTF16BE = 3 ,

			/// <summary>
			/// UnicodeLittleEndian is a synonym of UTF16LE, the encoding that
			/// corresponds to the wide character encoding on Microsoft Windows.
			/// </summary>
			UnicodeLittleEndian = 2 ,

			/// <summary>
			/// UnicodeLittleEndian is a synonym of UTF16BE, the encoding that
			/// corresponds to the wide character encoding on Microsoft Windows,
			/// running on a big-endian processor, which MAY include code that
			/// runs on ARM processors.
			/// </summary>
			UnicodeBigEndian = 3 ,

			/// <summary>
			/// The array has a UTF-32 Byte Order Mark that indicates 
			/// little-endian character encoding.
			/// </summary>
			/// <remarks>
			/// This seldom-used encoding is defined for completeness, and in
			/// anticipation of its usage increasing.
			/// </remarks>
			UTF32LE = 4 ,

			/// <summary>
			/// The array has a UTF-32 Byte Order Mark that indicates big-endian
			/// character encoding.
			/// </summary>
			/// <remarks>
			/// This seldom-used encoding is defined for completeness, and in
			/// anticipation of its usage increasing.
			/// </remarks>
			UTF32BE = 5
		};	// BOMType
		#endregion	// Public Enumerations


		#region Private Constants, Enumerations, and Data Structures
		struct MetaData
		{
			public BOMType Kind;
			public byte [ ] Defintion;

			public MetaData
				(
					BOMType penmkind ,
					byte [ ] pabytDefinition
				)
			{
				this.Kind = penmkind;
				this.Defintion = pabytDefinition;
			}	// public MetaData constructor
		}	// struct MetaData


		//	--------------------------------------------------------------------
		//	Sources	1)	"Byte order mark," retrieved 2017/03/16 at 01:24
		//				https://en.wikipedia.org/wiki/Byte_order_mark
		//
		//			2)	"UTF-8, UTF-16, UTF-32 & BOM: General Questions, Relating to UTF or Encoding Form"
		//				http://unicode.org/faq/utf_bom.html
		//	--------------------------------------------------------------------

		const byte UTF8_BYTE_1_OF_3 = 0xEF;
		const byte UTF8_BYTE_2_OF_3 = 0xBB;
		const byte UTF8_BYTE_3_OF_3 = 0xBF;

		//	--------------------------------------------------------------------
		//	This encoding corresponds to the original Unicode Byte Order Mark, 
		//	about which I first learned in about 2005.
		//	--------------------------------------------------------------------

		const byte UTF16_LE_BYTE_1_OF_2 = 0xFF;
		const byte UTF16_LE_BYTE_2_OF_2 = 0xFE;

		const byte UTF16_BE_BYTE_1_OF_2 = 0xFE;
		const byte UTF16_BE_BYTE_2_OF_2 = 0xFF;

		//	--------------------------------------------------------------------
		//	For the record, I documented these but this encoding is unsupported.
		//	--------------------------------------------------------------------

		const byte UTF32_LE_BYTE_1_OF_4 = 0xFF;
		const byte UTF32_LE_BYTE_2_OF_4 = 0xFE;
		const byte UTF32_LE_BYTE_3_OF_4 = 0x00;
		const byte UTF32_LE_BYTE_4_OF_4 = 0x00;

		const byte UTF32_BE_BYTE_1_OF_4 = 0x00;
		const byte UTF32_BE_BYTE_2_OF_4 = 0x00;
		const byte UTF32_BE_BYTE_3_OF_4 = 0xFE;
		const byte UTF32_BE_BYTE_4_OF_4 = 0xFF;

		const int INAPPLICABLE = MagicNumbers.ZERO;
		const int UNINITIALIZED = MagicNumbers.MINUS_ONE;
		#endregion	// Private Constants, Enumerations, and Data Structures


		#region Static Arrays
		//  --------------------------------------------------------------------
		//  These static read-only byte arrays store the arrays of characters
		//  that define the various Byte Order Marks.
		//  --------------------------------------------------------------------

		//  --------------------------------------------------------------------
		//	Since UTF8 is a single-byte encoding, processor endianness plays no
		//	role in its encoding.
		//  --------------------------------------------------------------------

		static readonly byte [ ] s_achrUTF8 =
		{
			UTF8_BYTE_1_OF_3 ,
			UTF8_BYTE_2_OF_3 ,
			UTF8_BYTE_3_OF_3
		};	// static readonly byte [ ] s_achrUTF8

		//  --------------------------------------------------------------------
		//	Because UTF-16 is a two-byte encoding, the definition of its Byte
		//	Order Mark must take into account the endianness of the processor
		//	that encoded the text that follows it.
		//  --------------------------------------------------------------------

		static readonly byte [ ] s_achrUTF16_LE =
		{
			UTF16_LE_BYTE_1_OF_2 ,
			UTF16_LE_BYTE_2_OF_2
		};	// static readonly byte [ ] s_achrUTF16_LE

		static readonly byte [ ] s_achrUTF16_BE =
		{
			UTF16_BE_BYTE_1_OF_2 ,
			UTF16_BE_BYTE_2_OF_2
		};	// static readonly byte [ ] s_achrUTF16_BE

		//  --------------------------------------------------------------------
		//	Because UTF-32 is a four-byte encoding, the definition of its Byte
		//	Order Mark must take into account the endianness of the processor
		//	that encoded the text that follows it.
		//  --------------------------------------------------------------------

		static readonly byte [ ] s_achrUTF32_LE =
		{
			UTF32_LE_BYTE_1_OF_4 ,
			UTF32_LE_BYTE_2_OF_4 ,
			UTF32_LE_BYTE_3_OF_4 ,
			UTF32_LE_BYTE_4_OF_4
		};	// static readonly byte [ ] s_achrUTF32_LE

		static readonly byte [ ] s_achrUTF32_BE =
		{
			UTF32_BE_BYTE_1_OF_4 ,
			UTF32_BE_BYTE_2_OF_4 ,
			UTF32_BE_BYTE_3_OF_4 ,
			UTF32_BE_BYTE_4_OF_4
		};	// static readonly byte [ ] s_achrUTF32_BE

		//  --------------------------------------------------------------------
		//	This static read-only array stores a list of the unique known UTFs,
		//	along with the information required to identify it and establish its
		//	public properties.
		//  --------------------------------------------------------------------

		static readonly MetaData [ ] s_utpMetaData =
		{
			new MetaData (
				BOMType.None ,
				null ) ,
			new MetaData (
				BOMType.UTF8 ,
				s_achrUTF8 ) ,
			new MetaData (
				BOMType.UTF16LE ,
				s_achrUTF16_LE ) ,
			new MetaData (
				BOMType.UTF16BE ,
				s_achrUTF16_BE ) ,
			new MetaData ( BOMType.UTF32LE ,
				s_achrUTF32_LE ) ,
			new MetaData (
				BOMType.UTF32BE ,
				s_achrUTF32_BE )
		};	// static readonly BOMType [ ] s_aenmBOMTypes

		//  --------------------------------------------------------------------
		//  These static read-only integers store the lengths of the arrays of
		//  Byte Order Marks and the list of known UTFs.
		//
		//	While these could be made constants, making them read only affords 
		//	more design flexibility, yet still effectively establishing their
		//	values at compile time.
		//  --------------------------------------------------------------------

		#endregion	//  Static Arrays


		#region Constructors
		private ByteOrderMark ( )
		{ }	// Constructor 1 of 2 is marked as private. to force the caller to supply a buffer.


		/// <summary>
		/// The only public constructor demands a reference to the byte array to evaluate.
		/// </summary>
		/// <param name="bytes">
		/// Supply a reference to the byte array to test for a Byte Order Mark.
		/// </param>
		public ByteOrderMark ( byte [ ] bytes )
		{
			_abytBytes = bytes;
		}	// Constructor 2 of 2, demands the byte array to evaluate
		#endregion	// ByteOrderMark Constructors


		#region Public Properties
		/// <summary>
		/// This read-only property returns the type of Byte Order Mark present
		/// in the byte array that was supplied to the constructor.
		/// </summary>
		public BOMType Kind
		{
			get
			{
				if ( _intLengh == UNINITIALIZED )
					TestForBOM ( );

				return _enmBOMType;
			}	// Kind property getter
		}	// public BOMType Kind, a read-only property


		/// <summary>
		/// This read-only property returns the length of the Byte Order Mark.
		/// </summary>
		/// <remarks>
		/// Since the property is initialized on first use, it may display an
		/// invalid value of -1 in a watch window.
		/// </remarks>
		public int Length
		{
			get
			{
				if ( _intLengh == UNINITIALIZED )
					TestForBOM ( );

				return _intLengh;
			}	// Length property getter
		}	// public int Length, a read-only property
		#endregion	// Public Properties


		#region Public Instance Methods
		/// <summary>
		/// If it hasn't been directly called, the first call to either property
		/// getter calls this method, so that the work required to evaluate the
		/// array for a byte order mark is deferred until it is needed, and it
        /// is never subsequently repeated for the lifetime of the instance.
		/// </summary>
		public void TestForBOM ( )
		{
			if ( _intLengh == UNINITIALIZED )
			{	// The first time this method is called, _intLengh is UNINITIALIZED, an invalid length value, even when there is no BOM.
				foreach ( MetaData utpMetaData in s_utpMetaData )
				{	// The first iteration sets default values, without performing any further processing.
					if ( _intLengh == UNINITIALIZED )
					{	// The first entry in the table is the degenerate case, where the Byte Order Mark is absent.
						_intLengh = utpMetaData.Defintion == null
							? INAPPLICABLE
							: utpMetaData.Defintion.Length;
						_enmBOMType = utpMetaData.Kind;
					}	// if ( _intLengh == UNINITIALIZED )

					if ( utpMetaData.Defintion != null )
					{	// The Description member of the first member, which corresponds to no BOM, is NULL.
						int intNDefnBytes = utpMetaData.Defintion.Length;
						int intPosition;

						for ( intPosition = ArrayInfo.ARRAY_FIRST_ELEMENT ;
							  intPosition < intNDefnBytes ;
							  intPosition++ )
						{	// Compare the byte in the input array with the corresponding byte in the BOM definition.
							if ( _abytBytes [ intPosition ] != utpMetaData.Defintion [ intPosition ] )
							{	// Stop at the first mismatch.
								break;
							}	// if ( _abytBytes [ intPosition ] != utpMetaData.Defintion [ intPosition ] )
						}	// for ( intPosition = ArrayInfo.ARRAY_FIRST_ELEMENT ; intPosition < intNDefnBytes ; intPosition++ )

						if ( intPosition == intNDefnBytes )
						{	// Completing the loop indicates a match. Set both of the read-only properties and return.
							_intLengh = utpMetaData.Defintion.Length;
							_enmBOMType = utpMetaData.Kind;
						}	// if ( intPosition == intNDefnBytes )
					}	// if ( utpMetaData.Defintion != null )
				}	// foreach ( MetaData utpMetaData in s_utpMetaData )
			}	// if ( _intLengh == UNINITIALIZED )
		}	// public void TestForBOM
		#endregion	// Private Instance Methods


		#region Private Instance Storage
		private byte [ ] _abytBytes;
		private BOMType _enmBOMType;
		private int _intLengh = UNINITIALIZED;
		#endregion	// Private Instance Storage
	}	// public class ByteOrderMark
}	// partial namespace WizardWrx