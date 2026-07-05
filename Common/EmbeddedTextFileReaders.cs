/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         EmbeddedTextFileReaders

    File Name:          EmbeddedTextFileReaders.cs

    Synopsis:           This static class exposes methods for loading data from
                        text files that have been embedded into the assembly as
						custom resources.

    Remarks:            A sibling class tests the resource for the presence of a
						Byte Order Mark, so that it can be used to evaluate the
						character encoding of the string to guide the conversion
						before being discarded.

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
	2017/03/20 7.0     DAG    The methods in this class came out of the old Util
                              class in WizardWrx.DllServices2.

	2017/07/19 7.0     DAG    Move method GetInternalResourceName into class
                              AssemblyUtils.SortableManagedResourceItem, and
                              promote it from private to public, so that both
							  classes can share it.

	2018/11/11 7.11    DAG    Re-cast the text of the embedded help topics in an
                              active voice whereever it made sense to do so, and
                              make technical corrections in the help text,
                              including coverage of exceptions that I discovered
                              was missing.

	2021/04/18 8.0.127 DAG    Merge NameValueCollectionFromEmbbededList from the
                              deprecated WizardWrx.ApplicationHelpers2 library.

	2022/11/23 9.0.246 DAG    Move this class from WizardWrx.EmbeddedTextFile to
                              WizardWrx.Common, trading the dedicated namespace
                              for the root WizardWrx namespace and renaming it
                              from Readers to EmbeddedTextFileReaders.
    ============================================================================
*/

using System;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;


namespace WizardWrx
{
	/// <summary>
	/// This static class exposes methods for loading text from custom resources
	/// that are embedded in an assembly.
	/// </summary>
	public static class EmbeddedTextFileReaders
	{
		/// <summary>
		/// Load the lines of a plain ASCII text file that has been stored with
		/// the assembly as a embedded resource into an array of native strings.
		/// </summary>
		/// <param name="pstrResourceName">
		/// Specify the absolute (fully qualified) resource name, which is its
        /// source file name appended to the default assembly namespace.
		/// </param>
		/// <returns>
		/// The return value is an array of Unicode strings, each of which is
		/// the text of a line from the original text file, sans terminator.
		/// </returns>
		/// <see cref="LoadTextFileFromAnyAssembly"/>
		/// <seealso cref="LoadTextFileFromEntryAssembly"/>
		public static string [ ] LoadTextFileFromCallingAssembly (
			string pstrResourceName )
		{
			return LoadTextFileFromAnyAssembly (
				pstrResourceName ,
				Assembly.GetCallingAssembly ( ) );
		}   // LoadTextFileFromCallingAssembly


		/// <summary>
		/// Load the lines of a plain ASCII text file that has been stored with
		/// the assembly as a embedded resource into an array of native strings.
		/// </summary>
		/// <param name="pstrResourceName">
		/// Specify the fully qualified resource name, which is its source file
		/// name appended to the default application namespace.
		/// </param>
		/// <returns>
		/// The return value is an array of Unicode strings, each of which is
		/// the text of a line from the original text file, sans terminator.
		/// </returns>
		/// <see cref="LoadTextFileFromAnyAssembly"/>
		/// <seealso cref="LoadTextFileFromCallingAssembly"/>
		public static string [ ] LoadTextFileFromEntryAssembly (
			string pstrResourceName )
		{
			return LoadTextFileFromAnyAssembly (
				pstrResourceName ,
				Assembly.GetEntryAssembly ( ) );
		}   // LoadTextFileFromEntryAssembly


		/// <summary>
		/// Load the lines of a plain ASCII text file that has been stored with
		/// a specified assembly as a embedded resource into an array of native
		/// strings.
		/// </summary>
		/// <param name="pstrResourceName">
		/// Specify the absolute (fully qualified) resource name, which is its
        /// source file name appended to the default assembly namespace name.
		/// </param>
		/// <param name="pasmSource">
		/// Pass in a reference to the Assembly from which you expect to load
		/// the text file. Use any means at your disposal to obtain a reference
		/// from the System.Reflection namespace.
		/// </param>
		/// <returns></returns>
		/// <seealso cref="LoadTextFileFromCallingAssembly"/>
		/// <seealso cref="LoadTextFileFromEntryAssembly"/>
		public static string [ ] LoadTextFileFromAnyAssembly (
			string pstrResourceName ,
			Assembly pasmSource )
		{
			byte [ ] abytWholeFile = LoadBinaryResourceFromAnyAssembly (
				pstrResourceName ,
				pasmSource );

			//  ----------------------------------------------------------------
			//  The file is stored in single-byte ASCII characters. The native
			//  character set of the Common Language Runtime is Unicode. A new
			//  array of Unicode characters serves as a translation buffer which
			//  is filled a character at a time from the byte array.
			//  ----------------------------------------------------------------

			//  ----------------------------------------------------------------
			//  The character array converts to a Unicode string in one fell
			//  swoop. Since the new string vanishes when StringOfLinesToArray
			//  returns, the transformation is nested in the call to the
			//  consumer function, StringOfLinesToArray, which splits the lines
			//  of text, with their DOS line terminators, into the required
			//  array of strings.
			//
			//  Ideally, the blank line should be removed. However, since the
			//  RemoveEmptyEntries member of the StringSplitOptions enumeration
			//  does it for me, I may as well use it, and save myself the future
			//  aggravation, when I will have probably forgotten why it happens.
			//  ----------------------------------------------------------------

			return StringOfLinesToArray (
				StringFromANSICharacterArray ( abytWholeFile ) ,
				StringSplitOptions.RemoveEmptyEntries );
		}   // LoadTextFileFromAnyAssembly


        /// <summary>
        /// Load the named embedded binary resource into a byte array.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the external name of the file as it appears in the source
        /// file tree and the Solution Explorer.
        /// </param>
        /// <param name="pasmSource">
        /// Supply a System.Reflection.Assembly reference to the assembly that
        /// contains the embedded resource.
        /// </param>
        /// <returns>
        /// If the function succeeds, it returns a byte array containing the raw
        /// bytes that comprise the embedded resource. Hence, this method can
        /// load ANY embedded resource.
        /// </returns>
        /// <remarks>
        /// Since all other resource types ultimately come out as byte arrays,
        /// the text file loaders call upon this routine to extract their data.
        ///
        /// The notes in the cited reference refreshed my memory of observations
        /// that I made and documented a couple of weeks ago. However, it was a
        /// lot easier to let Google find a reference document, which was
        /// intended for students in the Computer Science department at Columbia
        /// University, at http://www1.cs.columbia.edu/~lok/csharp/refdocs/System.IO/types/Stream.html"/>,
        /// than find my own source.
        /// </remarks>
        /// <exception cref="Exception">
        /// An Exception (the base Exception type) arises when the method is
        /// called with a <paramref name="pstrResourceName"/> value that cannot
        /// be found in the <paramref name="pasmSource"/> assembly. When the
        /// exception arises during the read operation, the generic Exception
        /// wraps an InvalidDataException exception, which is returned as its
        /// InnnerException property.
        /// </exception>
        public static byte [ ] LoadBinaryResourceFromAnyAssembly (
			string pstrResourceName ,
			Assembly pasmSource )
		{
			string strInternalName = SortableManagedResourceItem.GetInternalResourceName (
				pstrResourceName ,
				pasmSource );

			if ( strInternalName == null )
			{   // This exception is thrown clear.
				throw new Exception (
					string.Format (
						Common.Properties.Resources.ERRMSG_EMBEDDED_RESOURCE_NOT_FOUND ,               // Message Template
						pstrResourceName ,                                                      // Format Item 0 = Name of resource, as specified in the call
						pasmSource.FullName ) );                                                // Format Item 1 = Assembly full name (Base, Version, PKT, Culture)
			}   // if ( strInternalName == null )

			Stream stgTheFile = null;

			try
			{   // Subsequent exceptions are caught, packaged, and re-thrown.
				stgTheFile = pasmSource.GetManifestResourceStream (
					strInternalName );

				//  ------------------------------------------------------------
				//  The character count is used several times, always as an
				//  integer. Cast it once, and keep it, since implicit casts
				//  create new, short lived local variables that quickly land in
				//  the recycle bin (a. k. a., the Garbage Collector).
				//
				//  The integer is immediately put to use, to allocate a byte
				//  array, which must have room for every character in the input
				//  file. Since this value is given by the logical size of the
				//  embedded resource, it makes sense to allocate all of it at
				//  once, and to use the least expensive data structure, the old
				//  fashioned array.
				//  ------------------------------------------------------------

				byte [ ] abytWholeFile;
				int intTotalBytesAsInt = ( int ) stgTheFile.Length;
				abytWholeFile = new Byte [ intTotalBytesAsInt ];
				int intBytesRead = stgTheFile.Read (
					abytWholeFile ,                                                             // Buffer sufficient to hold it.
					ListInfo.BEGINNING_OF_BUFFER ,                                              // Read from the beginning of the file.
					intTotalBytesAsInt );                                                       // Swallow the file whole.

				//  ------------------------------------------------------------
				//  Though its backing store is a resource embedded in the
				//  assembly, the stream must be treated like any other.
				//  Investigating in the Visual Studio Debugger showed me that
				//  it is implemented as an UnmanagedMemoryStream. That
				//  "unmanaged" prefix is a clarion call that the stream must be
				//  closed, disposed, and destroyed. To that end, this method
				//  handles its own exceptions, although it eventually throws an
				//  exception of its own, in a way that ensures that a Finally
				//  block cleans up the stream.
				//  ------------------------------------------------------------

				stgTheFile.Close ( );
				stgTheFile.Dispose ( );
				stgTheFile = null;      // Signal the Finally block that the stream closed normally.

				//  ------------------------------------------------------------
				//  In the unlikely event that the byte count is short or long,
				//  the program must croak. Since the three items that we want
				//  to include in the report are stored in local variables,
				//  including the reported file length, we go ahead and close
				//  the stream before the count of bytes read is evaluated.
				//  ------------------------------------------------------------

				if ( intBytesRead == intTotalBytesAsInt )
				{   // Byte count matches count stored in metadata.
					return abytWholeFile;
				}   // TRUE (expected outcome) block, if ( intBytesRead == intTotalBytesAsInt )
				else
				{   //  Organize everything we know into an exception report,
					//  which is caught, packaged, and re-thrown in this
					//  routine.

					throw new InvalidDataException (
						string.Format (
							Common.Properties.Resources.ERRMSG_EMBEDDED_RESOURCE_READ_TRUNCATED ,	// Message Template
							new object [ ]
                        {
                            strInternalName ,														// Format Item 0 = Internal name derived from name passed into call
                            intTotalBytesAsInt ,													// Format Item 1 = Expected byte count
                            intBytesRead ,															// Format Item 2 = Actual byte count
                            Environment.NewLine														// Format Item 3 = Newline
                        } ) );
				}   // FALSE (UNexpected outcome) block, if ( intBytesRead == intTotalBytesAsInt )
			}
			catch ( Exception exAll )
			{
				throw new Exception (
					string.Format (
						Common.Properties.Resources.ERRMSG_EMBEDDED_RESOURCE_READ_ERROR ,			// Message Template
						new string [ ]
                        {
                            pasmSource.FullName ,													// Format Item 0 = Assembly full name (Base, Version, PKT, Culture)
                            strInternalName ,														// Format Item 1 = Internal name derived from name passed into call
                            exAll.Message ,															// Format Item 2 = Message taken from Inner Exception
                            Environment.NewLine														// Format Item 3 = Newline
                        } ) ,
					exAll );
			}   // The stream remains open if there was a mishap.
			finally
			{   // Whatever happens, clean up the unmanaged memory.
				if ( stgTheFile != null )
				{   // The stream read succeeded, closing the stream normally.
					if ( stgTheFile.CanRead )
					{   // The stream is open if CanRead is TRUE.
						stgTheFile.Close ( );
					}   // if ( stgTheFile.CanRead )

					//  --------------------------------------------------------
					//  Presumably, Close calls Dispose. However, since it is
					//  marked public, I prefer to call it anyway. For similar
					//  reasons, I set the stream to NULL, even though hough it
					//  is about to go out of scope.
					//  --------------------------------------------------------

					stgTheFile.Dispose ( );
					stgTheFile = null;
				}   // if ( stgTheFile != null )
			}   // Try/Catch/Finally block
		}   // LoadBinaryResourceFromAnyAssembly


		/// <summary>
		/// Construct a NameValueCollection from the tab delimited list read
		/// from a text file stored in an embedded resource.
		/// </summary>
		/// <param name="pstrUnqualifiedResourceName">
		/// Specify the name of the text file as it appears in the Solution
		/// Explorer (or the Windows Explorer, for that matter).
		///
		/// IMPORTNAT: The file must be marked as an embedded resource; do this
		/// in the Solution Explorer by displaying the properties of the file.
		/// </param>
		/// <param name="pstrExpectedLabelRow">
		/// The specified string is checked against the label row; unless both
		/// are identical, the resource is assumed to be corrupted, and the
		/// method throws an exception and croaks.
		/// </param>
		/// <returns>
		/// If the method succeeds, it returns a NameValueCollection of
		/// key/value pairs that contains exactly one value per key.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// And ArgumentException is thrown when the data file referenced by
		/// <paramref name="pstrUnqualifiedResourceName"/> contains duplicate
		/// keys, if one of the records contains two or more values, or the
		/// input file is empty. The messages attached to the exceptions are
		/// explicit about which of the three conditions caused the exception,
		/// and they attempt to provide enough detail to enable the programmer
		/// to quickly identify and resolve the problem.
		/// </exception>
		public static NameValueCollection NameValueCollectionFromEmbbededList (
			string pstrUnqualifiedResourceName ,
			string pstrExpectedLabelRow )
		{
			const int FIELD_KEY = ArrayInfo.ARRAY_FIRST_ELEMENT;
			const int FIELD_VALUE = ArrayInfo.ARRAY_SECOND_ELEMENT;
			const int FIELD_COUNT = FIELD_VALUE + ArrayInfo.ORDINAL_FROM_INDEX;

			string [ ] astrMapInfoFromResource = LoadTextFileFromEntryAssembly ( pstrUnqualifiedResourceName );

			int intKeyCount = CSVFileInfo.RecordCount ( astrMapInfoFromResource );

			if ( astrMapInfoFromResource [ CSVFileInfo.LABEL_ROW ] == pstrExpectedLabelRow )
			{   // Each element is a TAB delimited string, which must contain exactly two fields, which become the key and value of a pair.
				if ( astrMapInfoFromResource.Length > ListInfo.LIST_IS_EMPTY )
				{   // Have data. Create index.
					NameValueCollection rnvcConfigKeyMap = new NameValueCollection ( astrMapInfoFromResource.Length );

					for ( int intRow = CSVFileInfo.FIRST_RECORD ;
							  intRow <= intKeyCount ;
							  intRow++ )
					{
						string [ ] astrKVP = astrMapInfoFromResource [ intRow ].Split ( SpecialCharacters.TAB_CHAR );

						if ( astrKVP.Length == FIELD_COUNT )
						{   // Test for duplicates.
							if ( rnvcConfigKeyMap.Get ( astrKVP [ FIELD_KEY ] ) == null )
							{   // Key is unique. Add it.
								rnvcConfigKeyMap.Add (
									astrKVP [ FIELD_KEY ] ,
									astrKVP [ FIELD_VALUE ] );
							}   // TRUE (expected outcome) block, if ( rdctConfigKeyMap.Get ( astrKVP [ FIELD_KEY ] ) == null )
							else
							{   // Oops! Duplicate keys indicate an invalid or corrupted table.
								throw new ArgumentException (
									string.Format (													// Message
										Common.Properties.Resources.ERRMSG_DUPE_KEY ,				// Message template
										new object [ ]
										{
											pstrUnqualifiedResourceName ,							// Format Item 0 = External name of embedded resource
                                            intRow ,												// Format Item 1 = Logical row number
                                            astrKVP [ FIELD_KEY ] ,									// Format Item 2 = Value of duplicate key
                                            astrMapInfoFromResource [ intRow ] ,					// Format Item 3 = Copy of entire row
                                            Environment.NewLine										// Format Item 4 = NewLine, my way
                                        } ) ,
									nameof ( pstrUnqualifiedResourceName ) );						// ParamName
							}   // FALSE (UNexpected outcome) block, if ( rdctConfigKeyMap.Get ( astrKVP [ FIELD_KEY ] ) == null )
						}   // TRUE (expected outcome) block, if ( astrKVP.Length == FIELD_COUNT )
						else
						{   // Throw up and croak.
							throw new ArgumentException (
								string.Format (														// Message
									Common.Properties.Resources.ERRMSG_BAD_RECORD ,					// Message template
									new object [ ]
									{
										pstrUnqualifiedResourceName ,								// Format Item 0 = External name of embedded resource
                                        intRow ,													// Format Item 1 = Logical row number
                                        astrKVP.Length ,											// Format Item 2 = Actual field count
                                        FIELD_COUNT ,												// Format Item 3 = Expected field count
                                        astrMapInfoFromResource [ intRow ] ,						// Format Item 4 = Copy of entire row
                                        Environment.NewLine } ) ,									// Format Item 5 = NewLine, my way
								nameof ( pstrUnqualifiedResourceName ) );							// ParamName
						}   // FALSE (UNexpected outcome) block, if ( astrKVP.Length == FIELD_COUNT )
					}   // for ( int intRowIndex = CSVFileInfo.FIRST_RECORD ; intRowIndex <= intKeyCount ; intRowIndex++ )

					return rnvcConfigKeyMap;
				}   // TRUE (expected outcome) block, if ( astrMapInfoFromResource.Length > ListInfo.LIST_IS_EMPTY )
				else
				{   // The map resource is empty or corrupted.
					throw new ArgumentException (
						string.Format (
							Common.Properties.Resources.ERRMSG_EMPTY_MAP ,
							pstrUnqualifiedResourceName ) ,
						nameof ( pstrUnqualifiedResourceName ) );
				}   // FALSE (UNexpected outcome) block, if ( astrMapInfoFromResource.Length > ListInfo.LIST_IS_EMPTY )
			}   // TRUE (expected outcome) block, if ( astrMapInfoFromResource [ CSVFileInfo.LABEL_ROW ] == pstrExpectedLabelRow )
			else
			{   // Throw up and croak.
				throw new ArgumentException (
					string.Format (																	// Message
						Common.Properties.Resources.ERRMSG_BAD_LABEL_ROW,							// Message template
						new string [ ]
						{
							pstrUnqualifiedResourceName ,											// Format Item 0 = External resource name
                            astrMapInfoFromResource [ CSVFileInfo.LABEL_ROW ] ,						// Format Item 1 = Actual label row read from resource
                            pstrExpectedLabelRow ,													// Format Item 2 = Expected label row
                            Environment.NewLine														// Format Item 3 = Newline, my way
                        } ) ,
					nameof ( pstrExpectedLabelRow ) );                                              // ParamName
			}   // FALSE (UNexpected outcome) block, if ( astrMapInfoFromResource [ CSVFileInfo.LABEL_ROW ] == pstrExpectedLabelRow )
		}   // public static Dictionary<string , string> NameValueCollectionFromEmbbededList


		/// <summary>
		/// Transform an array of bytes, each representing one ANSI character, into a string.
		/// </summary>
		/// <param name="pabytWholeFile">
		/// Specify the array to transform.
		/// </param>
		/// <returns>
		/// The <paramref name="pabytWholeFile"/> array is returned as a string.
		/// </returns>
		/// <remarks>
		/// I did this refactoring, thinking that I had a new use for the code,
		/// only to realize as I finished cleaning it up that I can't use it,
		/// because it deals in ANSI characters, and my present need involves
		/// Unicode characters. Nevertheless, the exercise is not a total loss,
		/// because it reminded me of the trick that I needed to transform the
		/// array of Unicode characters into a string.
		/// </remarks>
		public static string StringFromANSICharacterArray ( byte [ ] pabytWholeFile )
		{
			int intNCharacters = pabytWholeFile.Length;
			ByteOrderMark bom = new ByteOrderMark ( pabytWholeFile );

			char [ ] achrWholeFile = new char [ intNCharacters - bom.Length ];

			int intCurrentCharacter = ArrayInfo.ARRAY_FIRST_ELEMENT;

			for ( int intCurrentByte = bom.Length ;
					  intCurrentByte < intNCharacters ;
					  intCurrentByte++ )
				achrWholeFile [ intCurrentCharacter++ ] = ( char ) pabytWholeFile [ intCurrentByte ];

			//  ----------------------------------------------------------------
			//  The character array converts to a Unicode string in one fell
			//  swoop.
			//  ----------------------------------------------------------------

			return new string ( achrWholeFile );
		}   // public static string StringFromANSICharacterArray


		/// <summary>
		/// Split a string containing lines of text into an array of strings.
		/// </summary>
		/// <param name="pstrLines">
		/// String containing lines of text, terminated by CR/LF pairs.
		/// </param>
		/// <returns>
		/// Array of strings, one line per string. Blank lines are preserved as
		/// empty strings.
		/// </returns>
		public static string [ ] StringOfLinesToArray ( string pstrLines )
		{
			if ( pstrLines == null )
				return new string [ ] { };     // Return an empty array.

			if ( pstrLines.Length == ArrayInfo.ARRAY_IS_EMPTY )
				return new string [ ] { };     // Return an empty array.

			return pstrLines.Split (
				StringToArray ( SpecialStrings.STRING_SPLIT_NEWLINE ) ,
				StringSplitOptions.None );
		}   // public static string [ ] StringOfLinesToArray {1 of 2)


		/// <summary>
		/// Split a string containing lines of text into an array of strings,
		/// as modified by the StringSplitOptions flag.
		/// </summary>
		/// <param name="pstrLines">
		/// String containing lines of text, terminated by CR/LF pairs.
		/// </param>
		/// <param name="penmStringSplitOptions">
		/// A member of the StringSplitOptions enumeration, presumably other
		/// than StringSplitOptions.None, which is assumed by the first
		/// overload. The only option supported by version 2 of the Microsoft
		/// .NET CLR is RemoveEmptyEntries.
		/// </param>
		/// <returns>
		/// Array of strings, one line per string. Blank lines are preserved as
		/// empty strings unless penmStringSplitOptions is RemoveEmptyEntries,
		/// as is most likely to be the case.
		/// </returns>
		/// <remarks>
		/// Use this overload to convert a string, discarding blank lines.
		/// </remarks>
		public static string [ ] StringOfLinesToArray (
			string pstrLines ,
			StringSplitOptions penmStringSplitOptions )
		{
			if ( string.IsNullOrEmpty ( pstrLines ) )
				return new string [ ] { };     // Return an empty array.

			return pstrLines.Split (
				StringToArray ( SpecialStrings.STRING_SPLIT_NEWLINE ) ,
				penmStringSplitOptions );
		}   // public static string [ ] StringOfLinesToArray {2 of 2)


		/// <summary>
		/// Return a one-element array containing the input string.
		/// </summary>
		/// <param name="pstr">
		/// String to place into the returned array.
		/// </param>
		/// <returns>
		/// Array of strings, containing exactly one element, which contains
		/// the single input string.
		/// </returns>
		public static string [ ] StringToArray ( string pstr )
		{
			return new string [ ] { pstr };
		}   // public static string [ ] StringToArray
	}   // public static class EmbeddedTextFileReaders
}	// partial namespace WizardWrx