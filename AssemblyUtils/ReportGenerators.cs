/*
    ============================================================================

    Namespace:			WizardWrx.AssemblyUtils

    Class Name:			ReportGenerators

	File Name:			ReportGenerators.cs

    Synopsis:			This class provides static methods for reporting on an
						assembly and its dependents.

    Remarks:			This class was originally intended to process references
						to a single assembly. It was moved into this library, to
						simplify processing an assembly and its dependents.

	License:            Copyright (C) 2017-2019, David A. Gray.
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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2017/03/28 7.0     DAG    This class makes its first appearance.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.

	2019/05/20 7.17    DAG    Add the AssemblyVersion, and document the
                              provenance of both reported version strings, which
                              may, and usually do, differ.

	2019/06/07 7.20    DAG    ListKeyAssemblyProperties was missing the summary
                              from its XML documentation.
    ============================================================================
*/


using System;
using System.IO;
using System.Reflection;
using System.Text;

using WizardWrx.Core;


namespace WizardWrx.AssemblyUtils
{
	/// <summary>
	/// The static members of this class generate reports about assemblies and
	/// their dependents.
	/// </summary>
	public class ReportGenerators
	{
		/// <summary>
		/// Get the GUID string (Registry format) attached to an assembly.
		/// </summary>
		/// <param name="pasm">
		/// Assembly from which to return the GUID string.
		/// </param>
		/// <returns>
		/// If the method succeeds, the return value is the GUID attached to it
		/// and intended to be associated with its type library if the assembly
		/// is exposed to COM.
		/// </returns>
		public static string GetAssemblyGuidString ( Assembly pasm )
		{
			object [ ] objAttribs = pasm.GetCustomAttributes (
				typeof ( System.Runtime.InteropServices.GuidAttribute ) ,
				false );

			if ( objAttribs.Length > ListInfo.EMPTY_STRING_LENGTH )
			{
				System.Runtime.InteropServices.GuidAttribute oMyGuid = ( System.Runtime.InteropServices.GuidAttribute ) objAttribs [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
				return oMyGuid.Value.ToString ( );
			}   // TRUE (anticipated outcome) block, if ( objAttribs.Length > ListInfo.EMPTY_STRING_LENGTH )
			else
			{
				return SpecialStrings.EMPTY_STRING;
			}   // FALSE (UNanticipated outcome) block, if ( objAttribs.Length > ListInfo.EMPTY_STRING_LENGTH )
		}   // public static string GetAssemblyGuidString



        /// <summary>
        /// Generate the label row on the <paramref name="pswOut"/> StreamWriter
        /// that is delimited by <paramref name="pchrDlm"/> characters.
        /// </summary>
        /// <param name="pswOut">
		/// Specify the open StreamWriter upon which to write.
		/// </param>
		/// <param name="pchrDlm">
		/// Specify the delimiter character.
		/// </param>
		/// <remarks>
		/// The label template is a managed resource string, REPORT_FIELD_NAMES,
        /// which governs the field order.
		/// </remarks>
		public static void LabelKeyAssemblyProperties (
			StreamWriter pswOut ,
			char pchrDlm )
		{
			if ( pswOut == null )
				throw new ArgumentNullException ( "pswOut" );

			if ( pchrDlm == SpecialCharacters.NULL_CHAR )
				throw new ArgumentNullException ( "pchrDlm" );

			if ( pchrDlm == SpecialCharacters.COMMA )
				pswOut.WriteLine ( Properties.Resources.REPORT_FIELD_NAMES );
			else
				pswOut.WriteLine ( Properties.Resources.REPORT_FIELD_NAMES.Replace ( SpecialCharacters.COMMA , pchrDlm ) );
		}	// public static void LabelKeyAssemblyProperties


		/// <summary>
		/// Create a record and append it to the flat file behind a
        /// StreamWriter.
		/// </summary>
		/// <param name="pasmSubject">
		/// Specify the assembly to be evaluated.
		/// </param>
		/// <param name="pswOut">
		/// Specify the open StreamWriter upon which to write.
		/// </param>
		/// <param name="pchrDlm">
		/// Specify the delimiter character.
		/// </param>
		/// <remarks>
		/// The label template is a managed resource string, REPORT_FIELD_NAMES,
        /// which governs the field order.
		/// </remarks>
		public static void ListKeyAssemblyProperties (
			Assembly pasmSubject ,
			StreamWriter pswOut ,
			char pchrDlm )
		{
			if ( pasmSubject == null )
				throw new ArgumentNullException ( "pasmSubject" );

			if ( pswOut == null )
				throw new ArgumentNullException ( "pswOut" );

			if ( pchrDlm == SpecialCharacters.NULL_CHAR )
				throw new ArgumentNullException ( "pchrDlm" );

			AssemblyName MyNameIs = AssemblyName.GetAssemblyName ( pasmSubject.Location );
			System.Diagnostics.FileVersionInfo myVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo ( pasmSubject.Location );
			FileInfo fiLibraryFile = new FileInfo ( pasmSubject.Location );

			StringBuilder sbDetail = new StringBuilder ( MagicNumbers.CAPACITY_04KB );

			sbDetail.Append ( pasmSubject.FullName );							// FullName
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( MyNameIs.Name );									// AssemblyFileBaseName
			sbDetail.Append ( pchrDlm );
	
			sbDetail.Append ( Path.GetFileName ( pasmSubject.Location ) );		// AssemblyFileName
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( GetAssemblyGuidString ( pasmSubject ) );			// AssemblyGuidString
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( Path.GetDirectoryName ( pasmSubject.Location ) );	// AssembyDirName
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( myVersionInfo.Comments );							// Comments
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( myVersionInfo.CompanyName );						// CompanyName
			sbDetail.Append ( pchrDlm );										// Field delimiter
			
			sbDetail.Append ( MyNameIs.CultureInfo.DisplayName );				// Culture
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( myVersionInfo.FileDescription );					// Description
			sbDetail.Append ( pchrDlm );										// Field delimiter
			
			sbDetail.Append ( TimeDisplayFormatter.PrepareLocalAndUTCTimes (
				fiLibraryFile.CreationTime ,
				fiLibraryFile.CreationTimeUtc ) );								// FileCreationDate
			sbDetail.Append ( pchrDlm );										// Field delimiter
			
			sbDetail.Append ( TimeDisplayFormatter.PrepareLocalAndUTCTimes (
				fiLibraryFile.LastWriteTime ,
				fiLibraryFile.LastWriteTimeUtc ) );								// FileModifiedDate
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( myVersionInfo.LegalCopyright );					// LegalCopyright
			sbDetail.Append ( pchrDlm );										// Field delimiter
			
			sbDetail.Append ( myVersionInfo.LegalTrademarks );					// LegalTrademarks
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( myVersionInfo.ProductName );						// ProductName
			sbDetail.Append ( pchrDlm );										// Field delimiter
			
			sbDetail.Append ( ByteArrayFormatters.ByteArrayToHexDigitString (
				MyNameIs.GetPublicKeyToken ( ) ) );								// PublicKeyToken
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( pasmSubject.ImageRuntimeVersion );				// RuntimeVersion
			sbDetail.Append ( pchrDlm );										// Field delimiter

			sbDetail.Append ( myVersionInfo.FileVersion );						// VersionString
			sbDetail.Append ( Environment.NewLine );							// Record delimiter

			pswOut.Write ( sbDetail.ToString ( ) );								// Append to stream.
		}	// public static void ListKeyAssemblyProperties


		/// <summary>
		/// List selected properties of any assembly on a console.
		/// </summary>
		/// <param name="pasmSubject">
		/// Pass in a reference to the desired assembly, which may be the
		/// assembly that exports a specified type, the executing assembly, the
		/// calling assembly, the entry assembly, or any other assembly for
		/// which you can obtain a reference.
		/// </param>
		public static void ShowKeyAssemblyProperties ( Assembly pasmSubject )
		{
			AssemblyName MyNameIs = AssemblyName.GetAssemblyName ( pasmSubject.Location );
			System.Diagnostics.FileVersionInfo myVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo ( pasmSubject.Location );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_SELECTED_DLL_PROPS_BEGIN , pasmSubject.FullName, Environment.NewLine );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYFILEBASENAME , Path.GetFileNameWithoutExtension ( pasmSubject.Location ) );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_VERSIONSTRING , myVersionInfo.FileVersion );
            Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYVERSION , MyNameIs.Version );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_CULTURE , MyNameIs.CultureInfo.DisplayName );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_PUBLICKEYTOKEN , ByteArrayFormatters.ByteArrayToHexDigitString ( MyNameIs.GetPublicKeyToken ( ) ) );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_RUNTIME_VERSION , pasmSubject.ImageRuntimeVersion );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYGUIDSTRING , GetAssemblyGuidString ( pasmSubject ) );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_PRODUCTNAME , myVersionInfo.ProductName );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_LEGALCOPYRIGHT , myVersionInfo.LegalCopyright );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_LEGALTRADEMARKS , myVersionInfo.LegalTrademarks );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_COMPANYNAME , myVersionInfo.CompanyName );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_DESCRIPTION , myVersionInfo.FileDescription );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_COMMENTS , myVersionInfo.Comments , Environment.NewLine );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBYDIRNAME , Path.GetDirectoryName ( pasmSubject.Location ) );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYFILENAME , Path.GetFileName ( pasmSubject.Location ) , Environment.NewLine );

			FileInfo fiLibraryFile = new FileInfo ( pasmSubject.Location );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_FILE_CREATION_DATE , fiLibraryFile.CreationTime , fiLibraryFile.CreationTimeUtc );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_FILE_MODIFIED_DATE , fiLibraryFile.LastWriteTime , fiLibraryFile.LastWriteTimeUtc );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_SELECTED_DLL_PROPS_END , Environment.NewLine );
		}   // public static void ShowKeAssemblyProperties method
	}	// public class ReportGenerators
}	// partial namespace WizardWrx.AssemblyUtils