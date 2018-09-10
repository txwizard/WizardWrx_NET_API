/*
    ============================================================================

    Namespace:          WizardWrx.AssemblyUtils

    Class Name:         PESubsystemInfo

    File Name:          PESubsystemInfo.cs

    Synopsis:           Define static methods for obtaining the subsystem ID of
                        any Windows Portable Executable file, and to translate
                        such IDs into standard short and long descriptions.

    Remarks:            This class implements the algorithm that I perfected in
						ProcessInfo32.dll, a native Win32 dynamic-link library.
						The objective of this rewrite is to replace it with a
						100% managed implementation that targets any CPU that
						the Microsoft .NET Base Class Library supports.

    Author:             David A. Gray
-
    License:            Copyright (C) 2016-2017, David A. Gray. 
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

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2016/05/17 1.0     DAG    Initial implementation.

	2017/02/24 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

                              The only effect of this change on this module is
                              it moves into a new namespace, and the new library
                              has a dependency upon WizardWrx.Common.

	2017/02/24 7.0     DAG    Use the AppDomain.CurrentDomain.DomainManager to
                              reliably set the subsystem property, regardless of
                              how the singleton springs into existence.

	2017/06/24 7.0	   DAG    To prevent accidental corruption, mark the static
                              string arrays that are used as lookup tables read
                              only, and to permit it to behave correctly when a
                              form loads the library, fail gracefully if the
                              entry assembly is unidentified.

	2017/07/12 7.0	   DAG    Override the ToString method on the base class,
							  object, to display the subsystem type of the
							  process that owns the singleton.

	2018/09/06 7.0	   DAG    Eliiminate the GetTheSingleInstance method, which
	                          is now provided by the base class.
    ============================================================================
*/


using System;
using System.IO;
using System.Reflection;

namespace WizardWrx.AssemblyUtils
{
	/// <summary>
	/// This class exposes methods for obtaining the subsystem ID encoded into
	/// the NT header of a Windows Portable Executable (PE) file. Such files
	/// include, but are not limited to, character mode and graphical mode
	/// programs implemented in both native or managed programming languages,
	/// dynamic link libraries, and device drivers.
	/// </summary>
	public class PESubsystemInfo : GenericSingletonBase<PESubsystemInfo>
	{
		#region Public Enumerations
		/// <summary>
		/// Map the unsigned integer returned by GetExeSubsystem onto an
		/// enumerated type that conveys its correct interpretation.
		/// </summary>
		/// <see cref="GetPESubsystemID"/>
		/// <see cref="GetPESubsystemDescription(Int16,SubsystemDescription)"/>
		/// <see cref="GetPESubsystemDescription(PESubsystemID,SubsystemDescription)"/>
		public enum PESubsystemID : uint
		{
			/// <summary>
			/// Unknown subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_UNKNOWN ,

			/// <summary>
			/// Image doesn't require a subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_NATIVE ,

			/// <summary>
			/// Image runs in the Windows GUI subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_WINDOWS_GUI ,

			/// <summary>
			/// Image runs in the Windows character subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_WINDOWS_CUI ,

			/// <summary>
			/// Image runs in the OS/2 character subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_OS2_CUI ,

			/// <summary>
			/// Image runs in the Posix character subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_POSIX_CUI ,

			/// <summary>
			/// Image is a native Win9x driver.
			/// </summary>
			IMAGE_SUBSYSTEM_NATIVE_WINDOWS ,

			/// <summary>
			/// Image runs in the Windows CE subsystem.
			/// </summary>
			IMAGE_SUBSYSTEM_WINDOWS_CE_GUI ,

			/// <summary>
			/// Image is an EFI Application.
			/// </summary>
			IMAGE_SUBSYSTEM_EFI_APPLICATION ,

			/// <summary>
			/// Image is a EFI Boot Service Driver.
			/// </summary>
			IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER ,

			/// <summary>
			/// Image is a EFI Runtime Driver.
			/// </summary>
			IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER ,

			/// <summary>
			/// Image runs from a EFI ROM.
			/// </summary>
			IMAGE_SUBSYSTEM_EFI_ROM ,

			/// <summary>
			/// Image runs on XBOX.
			/// </summary>
			IMAGE_SUBSYSTEM_XBOX ,
		};  // PESubsystemID


		/// <summary>
		/// Each subsystem ID has both a short and long description. Use this
		/// enumeration as the second argument, penmSubsystemDescription, to
		/// static method GetPESubsystemDescription.
		/// </summary>
		public enum SubsystemDescription
		{
			/// <summary>
			/// This value is invalid as input to GetPESubsystemDescription, and
			/// is defined to require the parameter to be explicitly set, so
			/// that there is no default value for penmSubsystemDescription.
			/// </summary>
			Unspecified ,

			/// <summary>
			/// Return the short (one and two word) description.
			/// </summary>
			Short ,

			/// <summary>
			/// Return the long (complete sentence) description.
			/// </summary>
			Long
		}	// SubsystemDescription
		#endregion	// Public Enumerations


		#region Public Constants (These define the raw subsystem ID values returned by the operating system.)
		/// <summary>
		/// The ProcessSubsystmID property returns this value until the private
		/// constructor initializes it.
		/// </summary>
		public const Int16 IMAGE_SUBSYSTEM_UNKNOWN = 0;


		/// <summary>
		/// The ProcessSubsystmID property returns this value when queried on an
		/// PESubsystemInfo singleton instance that was initialized by a Windows
		/// (graphical mode) entry assembly.
		/// 
		/// Static method GetPESubsystemID returns this value when queried about
		/// the subsystem ID of a Windows (graphical mode) assembly or compiled
		/// native program.
		/// </summary>
		public const Int16 IMAGE_SUBSYSTEM_WINDOWS_GUI = 2;


		/// <summary>
		/// The ProcessSubsystmID property returns this value when queried on an
		/// PESubsystemInfo singleton instance that was initialized by a console
		/// (character mode) entry assembly.
		/// 
		/// Static method GetPESubsystemID returns this value when queried about
		/// the subsystem ID of a console (character mode) assembly or compiled
		/// native program.
		/// </summary>
		public const Int16 IMAGE_SUBSYSTEM_WINDOWS_CUI = 3;
		#endregion	// Public Constants (These define the raw subsystem ID values returned by the operating system.)


		#region Private Static Lookup Tables
		/// <summary>
		/// This table maps the short description resource strings to the 
		/// subsystem ID that they describe. Since subsystem IDs 4 and 6 are
		/// undefined, the corresponding elements are null references. If one is
		/// actually referenced, the resulting null reference exception must be
		/// caught, reported, and investigated.
		/// </summary>
		static readonly string [ ] s_astrShortNames =
		{
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_UNKNOWN ,					// IMAGE_SUBSYSTEM_UNKNOWN (0)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_NATIVE ,					// IMAGE_SUBSYSTEM_NATIVE (1)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_WINDOWS_GUI ,				// IMAGE_SUBSYSTEM_WINDOWS_GUI (2)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_WINDOWS_CUI ,				// IMAGE_SUBSYSTEM_WINDOWS_CUI (3)
			null ,																// Unassigned (4)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_OS2_CUI ,					// IMAGE_SUBSYSTEM_OS2_CUI (5)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_POSIX_CUI ,				// IMAGE_SUBSYSTEM_POSIX_CUI (6)
			null ,																// Unassigned (7)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_NATIVE_WINDOWS ,			// IMAGE_SUBSYSTEM_NATIVE_WINDOWS (8)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_WINDOWS_CE_GUI				// IMAGE_SUBSYSTEM_WINDOWS_CE_GUI (9)
		};	// static readonly string [ ] s_astrShortNames


		/// This table maps the long description resource strings to the 
		/// subsystem ID that they describe. Since subsystem IDs 4 and 6 are
		/// undefined, the corresponding elements are null references. If one is
		/// actually referenced, the resulting null reference exception must be
		/// caught, reported, and investigated.
		static readonly string [ ] s_astrLongNames =
		{
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_UNKNOWN_LONG ,				// IMAGE_SUBSYSTEM_UNKNOWN (0)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_NATIVE_LONG ,				// IMAGE_SUBSYSTEM_NATIVE (1)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_WINDOWS_GUI_LONG ,			// IMAGE_SUBSYSTEM_WINDOWS_GUI (2)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_WINDOWS_CUI_LONG ,			// IMAGE_SUBSYSTEM_WINDOWS_CUI (3)
			null ,																// Unassigned (4)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_OS2_CUI_LONG ,				// IMAGE_SUBSYSTEM_OS2_CUI (5)
			null ,																// Unassigned (6)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_POSIX_CUI_LONG ,			// IMAGE_SUBSYSTEM_POSIX_CUI (7)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_NATIVE_WINDOWS_LONG ,		// IMAGE_SUBSYSTEM_NATIVE_WINDOWS (8)
			Properties.Resources.MSG_IMAGE_SUBSYSTEM_WINDOWS_CE_GUI_LONG		// IMAGE_SUBSYSTEM_WINDOWS_CE_GUI (9)
		};	// static readonly string [ ] s_astrLongNames
		#endregion	// Private Static Lookup Tables


		#region Private Instance Storage
		private Int16 _intDefaultAppDomainSubsystemID = IMAGE_SUBSYSTEM_UNKNOWN;
		private AssemblyName _asmDefaultDomainEntryAssemblyName;
		private string _strDomainEntryAssemblyLocation;
		#endregion	// Private Instance Storage


		#region Public Read-Only Instance Properties
		/// <summary>
		/// Get the subsystem ID of the default application domain.
		/// </summary>
		/// <seealso cref="DefaultAppDomainSubsystem"/>
		public Int16 DefaultAppDomainSubsystemID
		{
			get
			{
				return _intDefaultAppDomainSubsystemID;
			}	// DefaultAppDomainSubsystemID property getter
		}	// DefaultAppDomainSubsystemID property


		/// <summary>
		/// Get the subsystem in which the default application domain executes.
		/// </summary>
		/// <seealso cref="DefaultAppDomainSubsystemID"/>
		public PESubsystemID DefaultAppDomainSubsystem
		{
			get
			{
				return ( PESubsystemID ) _intDefaultAppDomainSubsystemID;
			}	// DefaultAppDomainSubsystem property getter
		}	// DefaultAppDomainSubsystem property


		/// <summary>
		/// Get the fully qualified name of the file from which the first
		/// assembly that was loaded into the default application domain was
		/// read.
		/// </summary>
		public string DomainEntryAssemblyLocation
		{
			get
			{
				return _strDomainEntryAssemblyLocation;
			}	// DomainEntryAssemblyLocation property getter
		}	// DomainEntryAssemblyLocation property


		/// <summary>
		/// Get the fully qualified AssemblyName of the first assembly that was
		/// loaded into the default application domain.
		/// </summary>
		/// <remarks>
		/// AssemblyName has properties that expose the parts of an assembly
		/// name, its simple name, version, culture, and public key token.
		/// </remarks>
		public AssemblyName DefaultDomainEntryAssemblyName
		{
			get
			{
				return _asmDefaultDomainEntryAssemblyName;
			}	// DefaultDomainEntryAssemblyName property getter
		}	// DefaultDomainEntryAssemblyName property
		#endregion	// Public Read-Only Instance Properties


		#region Public Static Methods
		/// <summary>
		/// Call this method with the name of a file to get its subsystem ID.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string must be the name of a file that can be found in the
		/// current security context. Names may be either relative or fully
		/// qualified. Relative file names are resolved relative to the current
		/// working directory.
		/// </param>
		/// <returns>
		/// If the specified file exists and is a valid Windows Portable
		/// Executable (PE) file, its subsystem ID is returned. Since the ID is
		/// represented internally as a 16 bit signed integer, the return type
		/// of Int16 is guaranteed to be correct, regardless of the machine 
		/// architecture. Debuggers and stack traces may represent this type as
		/// a short, a common alias for Int16.
		/// </returns>
		/// <see cref="GetPESubsystemDescription(Int16,SubsystemDescription)"/>
		/// <see cref="GetPESubsystemDescription(PESubsystemID,SubsystemDescription)"/>
		/// <exception cref="ArgumentException">
		/// The following conditions elicit an ArgumentException exception.
		/// 
		/// 1) String argument pstrFileName is a null reference or points to the
		/// empty string.
		/// 
		/// 2) String argument pstrFileName specifies a file that cannot be
		/// found in the current security context.
		/// 
		/// 3) String argument pstrFileName specifies a file that is too small
		/// to contain a valid PE header, let alone its associated code and'
		/// data.
		/// </exception>
		/// <exception cref="Exception">
		/// The following conditions elicit an Exception (the garden variety
		/// exception class) exception.
		/// 
		/// 1) The file read returned, indicating that fewer than the expected
		/// number of bytes was read. This is probably the result of an internal
		/// programming error, and is unlikely to arise in practice, since this
		/// type of error should elicit an I/O exception.
		/// 
		/// 2) The Int16 (16 bit signed integer) that marks the start of a valid
		/// DOS file header is missing.
		/// 
		/// 3) The DWORD (32 bit unsigned integer) in the DOS header that should
		/// point to the start of the NT header is either null, or it points to
		/// a location beyond the first 1024 bytes of the file.
		/// 
		/// 4) The magic Int32 (32 bit signed integer) that marks the start of
		/// the NT header is not where the pointer in the DOS header says it
		/// should be.
		/// 
		/// 5) The I/O subsystem threw an exception. A new garden variety
		/// exception object is created, the I/O exception is attached as its
		/// InnerException property, and the new exception is thrown up the call
		/// stack. 
		/// 
		/// Wrapping the I/O exception in a garden variety exception lets the
		/// final exception report include the name of the file that was being
		/// processed when the exception arose, which may provide useful clues
		/// about its root cause.
		/// 
		/// 6) A completely unexpected event gave rise to an exception. A new
		/// garden variety exception object is created, the original exception
		/// is attached as its InnerException property, and the new exception is
		/// thrown up the call stack.
		/// 
		/// Wrapping the original I/O exception in a new exception lets the 
		/// final exception report include the name of the file that was being
		/// processed when the exception arose, which may provide useful clues
		/// about its root cause.
		/// </exception>
		/// <seealso cref="GetPESubsystem"/>
		/// <seealso cref="GetPESubsystemDescription(PESubsystemID,SubsystemDescription)"/>
		/// <seealso cref="DefaultAppDomainSubsystemID"/>
		public static Int16 GetPESubsystemID (
			string pstrFileName )
		{
			//	----------------------------------------------------------------
			//	Everything that remotely smacks of being a magic number is
			//	defined as a constant, everything that is the object of an
			//	integer comparison is the same size as the integer against which
			//	it is compared, and every integer that is involved in a compare
			//	against data read from the file has its size specified as an
			//	integer of the appropriate number of bits (16 for the EXE magic
			//	and the subsystem ID, and 32 for the NT header magic. The goal
			//	of this much precision is to enable the program to perform
			//	correctly on both 32 and 64 bit processors.
			//	----------------------------------------------------------------

			const int BEGINNING_OF_BUFFER = 0;
			const int INVALID_POINTER = 0;
			const int MINIMUM_FILE_LENGTH = 384;
			const int NOTHING_READ = 0;
			const int PE_HEADER_BUFFER = 1024;

			const int PE_HDR_OFFSET_E_LFANEW = 60;
			const int PE_HDR_OFFSET_SUBSYSTEM = 92;

			const Int16 IMAGE_DOS_SIGNATURE = 23117;
			const Int32 IMAGE_NT_SIGNATURE = 17744;

			const char QUOTE_CHAR = '"';

			//	----------------------------------------------------------------
			//	Verify that the string that is expected to contain the file name
			//	meets a few minimum requirements before we incur the overhead of
			//	opening a binary stream.
			//	----------------------------------------------------------------

			if ( string.IsNullOrEmpty ( pstrFileName ) )
			{	// The ternary expression differentiates between the null reference and the empty string.
				throw new ArgumentException (
					pstrFileName == null															// Differentiate between null reference and empty string.
						? Properties.Resources.MSG_GETSUBSYST_NULL_FILENAME_POINTER					// Display this message if the pointer is null.
						: Properties.Resources.MSG_GETSUBSYST_FILENAME_POINTER_EMPTY_STRING );		// Display this message if the pointer is the empty string.
			}	// if ( string.IsNullOrEmpty ( pstrFileName ) )

			FileInfo fiCandidate = null;															// This needs method scope, but allocating it is wasted if the argument is patently invalid.

			if ( File.Exists ( pstrFileName ) )
			{	// File exists. Check its length.
				fiCandidate = new FileInfo ( pstrFileName );

				if ( fiCandidate.Length < MINIMUM_FILE_LENGTH )
				{	// File is too small to contain a complete PE header.
					throw new ArgumentException (
						string.Format (
							Properties.Resources.MSG_GETSUBSYST_FILE_TOO_SMALL ,					// Format control string
							new object [ ]
							{
								pstrFileName ,														// Format Item 0 = File name, as fed into the method
								QUOTE_CHAR ,														// Format Item 1 = Double Quote character to enclose file name
								fiCandidate.Length ,												// Format Item 2 = Actual length (size) of file
								MINIMUM_FILE_LENGTH	,												// Format Item 3 = Minimum file length
								Environment.NewLine													// Format Item 4 = Embedded Newline
							} ) );											
				}	// if ( fiCandidate.Length < MINIMUM_FILE_LENGTH )
			}	// TRUE (anticipated outcome) block, if ( File.Exists ( pstrFileName ) )
			else
			{	// The specified file cannot be found in the current security context.
				throw new ArgumentException (
					string.Format (
						Properties.Resources.MSG_GETSUBSYST_FILE_NOT_FOUND ,						// Format control string
						pstrFileName ,																// Format Item 0 = File name, as fed into the method
						QUOTE_CHAR ) );																// Format Item 1 = Double Quote character to enclose file name
			}	// FALSE (UNanticipated outcome) block, if ( File.Exists ( pstrFileName ) )

			//	----------------------------------------------------------------
			//	Since the file name string passed the smell test, open the file.
			//	read up to the first kilobyte into memory, and search for the
			//	magic flags.
			//	----------------------------------------------------------------

			Int16 rintSubystemID = IMAGE_SUBSYSTEM_UNKNOWN;

			try
			{
				int intBytesToRead =
					( fiCandidate.Length >= PE_HEADER_BUFFER )
					? PE_HEADER_BUFFER
					: ( int ) fiCandidate.Length;
				int intBytesRead = NOTHING_READ;

				//	------------------------------------------------------------
				//	Since the file I/O happens within a Using block guarded by a
				//	try/catch block, proper disposal of its unmanaged resources
				//	is guaranteed by the runtime engine.
				//
				//	Before the buffer is processed, the number of bytes actually
				//	read is compared against the expected count, which is the
				//	lesser of 1024 (1 KB) or the length of the file.
				//	------------------------------------------------------------

				using ( FileStream fsCandidate = new FileStream (
					pstrFileName ,
					FileMode.Open ,
					FileAccess.Read ,
					FileShare.Read ) )
				{
					byte [ ] abytPeHeaderBuf = new byte [ intBytesToRead ];

					intBytesRead = fsCandidate.Read ( abytPeHeaderBuf ,								// Store bytes read into this array.
													  BEGINNING_OF_BUFFER ,							// Start copying at this offset in the buffer, which happens to be its beginning.
													  intBytesToRead );								// Store up to this many bytes, which happens to be the size of array abytPeHeaderBuf.

					if ( intBytesRead < intBytesToRead )
					{	// An I/O error occurred while reading input file {0}{3}Only {1} of the expected {2} bytes were read.
						throw new Exception ( string.Format (
							Properties.Resources.MSG_GETSUBSYST_FILE_READ_SHORT ,					// Format Control String
							new object [ ]
							{
								pstrFileName ,														// Format Item 0 = File Name, as tendered for processing
								intBytesRead ,														// Format Item 1 = Count of bytes actually read from file
								intBytesToRead ,													// Format Item 2 = Count of bytes expected to be read
								Environment.NewLine													// Format Item 3 = Embedded newline
							} ) );
					}	// if ( intBytesRead < intBytesToRead )

					//	--------------------------------------------------------
					//	Though it could be moved outside the using block, or the
					//	enclosing try block, for that matter, I chose to leave
					//	the testing inline, which makes the program flow very
					//	clean. Since it's all over in a matter of nanoseconds,
					//	leaving the file open won't make that much difference.
					//	--------------------------------------------------------

					Int16 intPEMagic = BitConverter.ToInt16 ( abytPeHeaderBuf , BEGINNING_OF_BUFFER );

					if ( intPEMagic == IMAGE_DOS_SIGNATURE)
					{	// Checking for the presence of the magic WORD is the very first task.
						Int32 intPEOffsetNTHeader = BitConverter.ToInt32 (
							abytPeHeaderBuf ,
							PE_HDR_OFFSET_E_LFANEW );

						if ( intPEOffsetNTHeader > INVALID_POINTER && intPEOffsetNTHeader <= intBytesToRead )
						{	// The location of the NT header is variable, but the DOS header has a pointer, relative to its own start, at a fixed location.
							Int32 intNTHeaderMagic = BitConverter.ToInt32 (
								abytPeHeaderBuf ,
								intPEOffsetNTHeader );

							if ( intNTHeaderMagic == IMAGE_NT_SIGNATURE )
							{	// Though the Subsystem is at a fixed offset within the NT header, the location of the start of said header is variable, but known.
								rintSubystemID = BitConverter.ToInt16 (
									abytPeHeaderBuf ,
									intPEOffsetNTHeader + PE_HDR_OFFSET_SUBSYSTEM );
							}	// TRUE (anticipated outcome) block, if ( intNTHeaderMagic == IMAGE_NT_SIGNATURE )
							else
							{	// The NT header magic DWORD is missing.
								throw new Exception (
									string.Format (
									Properties.Resources.MSG_GETSUBSYST_NO_NT_MAGIC ,				// Format control string
									pstrFileName ) );												// Format Item 0 = File Name as submitted
							}	// FALSE (unanticipated outcome) block, if ( intNTHeaderMagic == IMAGE_NT_SIGNATURE )
						}	// TRUE (anticipated outcome) if ( intPEOffsetNTHeader > INVALID_POINTER && intPEOffsetNTHeader <= intBytesToRead )
						else
						{	// The pointer to the NT header is missing.
							throw new Exception (
								string.Format (
									Properties.Resources.MSG_GETSUBSYST_NO_NT_SIGNATURE ,			// Format control string
									pstrFileName ,													// Format Item 0 = File Name as submitted
									intPEOffsetNTHeader ,											// Format Item 1 = Offset of PE header
									Environment.NewLine ) );										// Format Item 2 = Embedded newline
						}	// FALSE (unanticipated outcome) block, if ( intPEOffsetNTHeader > INVALID_POINTER )
					}	// TRUE (anticipated outcome) block, if ( intPEOffsetNTHeader > INVALID_POINTER && intPEOffsetNTHeader <= intBytesToRead )
					else
					{	// The PE header magic WORD is absent.
						throw new Exception ( 
							string.Format (
								Properties.Resources.MSG_GETSUBSYST_NO_MAGIC ,						// Format control string
								pstrFileName ) );													// Format Item 0 = File Name as submitted								
					}	// FALSE (unanticipated outcome) block, if ( intPEMagic == IMAGE_DOS_SIGNATURE)
				}	// using ( FileStream Candidate = new FileStream ( pstrFileName , FileMode.Open , FileAccess.Read , FileShare.Read ) )
			}	// The normal flow of control falls through to the return statement at the very end of the function block.
			catch ( IOException exIO )
			{
				throw new Exception (
					string.Format (
						Properties.Resources.MSG_GETSUBSYST_FILE_READ_ERROR ,						// Format control string
						exIO.GetType ( ).FullName ,													// Format Item 0 = Fully qualified Type of the exception
						pstrFileName ,																// Format Item 1 = File name, as fed into the method
						Environment.NewLine ) ,														// Format Item 2 = Embedded Newline
					exIO );																			// The original exception gets the InnerException seat.
			}
			catch ( Exception exMisc )
			{	// If the exception is our own, it contains the file name; pass it up the call stack. Otherwise, wrap a new exception around it that can pass the file name up the call stack.
				if ( exMisc.TargetSite.Name == System.Reflection.MethodBase.GetCurrentMethod ( ).Name )
					throw;
				else
					throw new Exception (
						string.Format (
							Properties.Resources.MSG_GETSUBSYST_GENERAL_EXCEPTION ,					// Format control string
							exMisc.GetType ( ).FullName ,											// Format Item 0 = Fully qualified Type of the exception
							pstrFileName ,															// Format Item 1 = File name, as fed into the method
							Environment.NewLine ) ,													// Format Item 2 = Embedded Newline
						exMisc );																	// The original exception gets the InnerException seat.
			}

			//	----------------------------------------------------------------
			//	Leaving the return here, and letting execution fall through is
			//	easier than arguing with the compiler about whether all code
			//	paths return a value.
			//	----------------------------------------------------------------

			return rintSubystemID;
		}	// public static Int16 GetPESubsystemID


		/// <summary>
		/// Use this wrapper to get the PESubsystemID enumeration member
		/// equivalent to the integer returned by GetPESubsystemID.
		/// </summary>
		/// <param name="pstrFileName">
		/// This string must be the name of a file that can be found in the
		/// current security context. Names may be either relative or fully
		/// qualified. Relative file names are resolved relative to the current
		/// working directory.
		/// </param>
		/// <returns>
		/// If the function succeeds, its return value is a PESubsystemID member
		/// that corresponds to the 16-bit integer returned by GetPESubsystemID,
		/// which it calls internally.
		/// </returns>
		/// <seealso cref="GetPESubsystemID"/>
		public static PESubsystemID GetPESubsystem ( string pstrFileName )
		{
			return ( PESubsystemID ) GetPESubsystemID ( pstrFileName );
		}	// public static PESubsystemID GetPESubsystem


		/// <summary>
		/// Get the short (one or two word) or long (complete, grammatically
		/// correct sentence) description that corresponds to a Portable
		/// Executable (PE) subsystem ID, such as the value returned by passing
		/// a file name to GetPESubsystemID.
		/// </summary>
		/// <param name="pintSubsystemID">
		/// Specify the subsystem ID for which the corresponding short or long
		/// description is wanted. Suitable inputs include the signed integer
		/// returned by GetPESubsystemID, which may be called as a nested method
		/// if you have no further use for the subsystem ID.
		/// </param>
		/// <seealso cref="GetPESubsystemID"/>
		/// <param name="penmSubsystemDescription">
		/// A member of the SubsystemDescription specifies whether to return the
		/// short (one or two word) description or the long (complete sentence)
		/// description that corresponds to the specified subsystem ID.
		/// </param>
		/// <see cref="SubsystemDescription"/>
		/// <returns>
		/// If the function succeeds, the return value is a string containing a
		/// short or long description that corresponds to a specified subsystem
		/// ID.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// If the subsystem ID specified by argument pintSubsystemID is either
		/// negative or greater than the largest valid subsystem ID (9, at
		/// present, though future editions of the Microsoft Platform SDK might
		/// define additional IDs), an ArgumentOutOfRangeException exception is
		/// thrown, which reports the
		/// </exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
		/// An System.ComponentModel.InvalidEnumArgumentException exception is
		/// thrown when the second argument, penmSubsystemDescription, is either
		/// SubsystemDescription.Unspecified or is not a valid member of the
		/// SubsystemDescription enumeration. Unspecified is defined, but marked
		/// as invalid to ensure that if penmSubsystemDescription is a variable,
		/// it is initialized.
		/// </exception>
		/// <see cref="GetPESubsystemID"/>
		/// <seealso cref="GetPESubsystemDescription(PESubsystemID,SubsystemDescription)"/>
		public static string GetPESubsystemDescription (
			Int16 pintSubsystemID , 
			SubsystemDescription penmSubsystemDescription )
		{
			const string ARG_SUBSYST = "pintSubsystemID";
			const string ARG_ENUM_NAME = "penmSubsystemDescription";

			if ( pintSubsystemID >= IMAGE_SUBSYSTEM_UNKNOWN && pintSubsystemID < s_astrLongNames.Length )
			{
				switch ( penmSubsystemDescription )
				{
					case SubsystemDescription.Short:
						return s_astrShortNames [ pintSubsystemID ];
					case SubsystemDescription.Long:
						return s_astrLongNames [ pintSubsystemID ];
					default:
						throw new System.ComponentModel.InvalidEnumArgumentException (
							ARG_ENUM_NAME ,										// Parameter (Argument) Name
							( int ) penmSubsystemDescription ,					// Parameter (Argument) Value, cast to Integer
							penmSubsystemDescription.GetType ( ) );				// Parameter (Enumeration) Type
				}	// switch ( penmSubsystemDescription )
			}	// TRUE (anticipated outcome) block, if ( pintSubsystemID > IMAGE_SUBSYSTEM_UNKNOWN && pintSubsystemID < s_astrLongNames.Length )
			else
			{
				throw new ArgumentOutOfRangeException (
					ARG_SUBSYST ,												// Parameter (Argument) Name
					pintSubsystemID ,											// Value passed in as a subsystem ID
					Properties.Resources.MSG_XLATE_SUBSYST_INVALID_ID );		// Explanatory text
			}	// FALSE (unanticipated outcome) block, if ( pintSubsystemID > IMAGE_SUBSYSTEM_UNKNOWN && pintSubsystemID < s_astrLongNames.Length )
		}	// public static string GetPESubsystemDescription (1 of 2)


		/// <summary>
		/// Get the short (one or two word) or long (complete, grammatically
		/// correct sentence) description that corresponds to a Portable
		/// Executable (PE) subsystem ID, such as the value returned by passing
		/// a file name to GetPESubsystemID.
		/// </summary>
		/// <param name="penmSubsystemID">
		/// Specify the PESubsystemID enumeration value for which the
		/// corresponding string is needed. Suitable inputs include the value
		/// returned by GetPESubsystem or the DefaultDomainSubsystem property
		/// returned by the singleton instance.
		/// </param>
		/// <param name="penmSubsystemDescription">
		/// A member of the SubsystemDescription specifies whether to return the
		/// short (one or two word) description or the long (complete sentence)
		/// description that corresponds to the specified subsystem ID.
		/// </param>
		/// <see cref="SubsystemDescription"/>
		/// <returns>
		/// If the function succeeds, the return value is a string containing a
		/// short or long description that corresponds to a specified subsystem
		/// ID.
		/// </returns>
		/// <remarks>
		/// This method casts penmSubsystemID to Int16, and feeds it to
		/// GetPESubsystemDescription, since the cast wound be necessary, sooner
		/// or later, to use the lookup tables that contain the descriptions.
		/// </remarks>
		/// <see cref="GetPESubsystem"/>
		/// <seealso cref="GetPESubsystemID"/>
		/// <seealso cref="GetPESubsystemDescription(Int16,SubsystemDescription)"/>
		public static string GetPESubsystemDescription (
			PESubsystemID penmSubsystemID ,
			SubsystemDescription penmSubsystemDescription )
		{
			return GetPESubsystemDescription (
				( Int16 ) penmSubsystemID ,
				penmSubsystemDescription );
		}	// public static string GetPESubsystemDescription (2 of 2)
		#endregion	// Public Static Methods


		#region Private Instance Methods
		private PESubsystemInfo ( )
		{
			//	----------------------------------------------------------------
			//	A default Application Domain doesn't necessarily have a Domain
			//	Manager bound to it. However, without one, it's probably safe to
			//	use the entry assembly returned by the Assembly.GetEntryAssembly
			//	method.
			//	----------------------------------------------------------------

			#if DBG_PE_SUBSYSTEM_INFO
				string strDomainManagerAssemblyName = AppDomain.CurrentDomain.DomainManager != null ? AppDomain.CurrentDomain.DomainManager.EntryAssembly.FullName : "** UNDEFINED **";
				string strEntryAssemblyName = Assembly.GetEntryAssembly ( ).FullName;
				string strEventMessage = string.Format (
					"PESubsystemInfo constructor: Assembly FullName, per Domain Manager = {0}{2}                             Assembly FullName, per EmtryAssembly  = {1}" ,
					strDomainManagerAssemblyName ,
					strEntryAssemblyName ,
					Environment.NewLine );
				System.Diagnostics.EventLog.WriteEntry (
					"WizardWrx" ,
					strEventMessage );
			#endif	// DBG_PE_SUBSYSTEM_INFO
			Assembly asmDomainEntryAssembly = AppDomain.CurrentDomain.DomainManager != null
											  ? AppDomain.CurrentDomain.DomainManager.EntryAssembly
											  : Assembly.GetEntryAssembly ( );

			if ( asmDomainEntryAssembly == null )
			{	// This hack assumes that Assembly.GetEntryAssembly ( ) always returns a usable value.
				Assembly.GetEntryAssembly ( );

				#if DBG_PE_SUBSYSTEM_INFO
					strEventMessage = string.Format (
						"PESubsystemInfo constructor: Assembly FullName taken as Entry Assembly = {0}" ,
						asmDomainEntryAssembly.FullName );
					System.Diagnostics.EventLog.WriteEntry (
						"WizardWrx" ,
						strEventMessage );
				#endif	// DBG_PE_SUBSYSTEM_INFO
			}	// if ( asmDomainEntryAssembly == null )

			_asmDefaultDomainEntryAssemblyName = asmDomainEntryAssembly.GetName ( );
			_strDomainEntryAssemblyLocation = asmDomainEntryAssembly.Location;
			_intDefaultAppDomainSubsystemID = GetPESubsystemID ( _strDomainEntryAssemblyLocation );
		}	// The default constructor is marked as private, to meet the requirements of the Singleton design pattern.
		#endregion	// Private Instance Methods


		#region Base Class Overrides
		/// <summary>
		/// Override the default ToString method to display the subsystem type.
		/// </summary>
		/// <returns>
		/// The returned string resembles the following example.
		/// 
		/// {{Subsystem ID = Console (3)}} WizardWrx.AssemblyUtils.PESubsystemInfo
		/// </returns>
		public override string ToString ( )
		{
			return string.Format (
				Properties.Resources.PESUBSYSTEMINFO_TOSTRING_TEMPLATE ,		// Format Control String
				this.GetType ( ).FullName ,										// Format Item 0 = Fully qualified type
				this.DefaultAppDomainSubsystem ,								// Format Item 1 = Subsystem type as enumeration
				this.DefaultAppDomainSubsystemID );								// Format Item 2 = Subsystem type as integer (raw value)
		}	// ToString
		#endregion	// Base Class Overrides
	}	// public class PESubsystemInfo
}	// partial namespace WizardWrx.AssemblyUtils