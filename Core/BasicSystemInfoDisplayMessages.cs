/*
    ============================================================================

	Namespace Name:		WizardWrx.Core

	Class Name:			BasicSystemInfoDisplayMessages

	File Name:			BasicSystemInfoDisplayMessages.cs

	Synopsis:			This class exposes static methods to display messages
						that describe the processor architecture and subsystem
						in which a process is executing.

	Dependencies:		This class uses only two base classes from the Microsoft
						.NET Framework Base Class Library, Object and String.

	Remarks:			

	Author:				David A. Gray

	License:            Copyright (C) 2017, David A. Gray. 
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

	Date Written:		Saturday, 20 March 2010

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Author Synopsis
    ---------- ------ ----------------------------------------------------------
    2017/07/12 DAG    This class makes its debut.
    ============================================================================
*/

using System;
using System.Diagnostics;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Security;

using WizardWrx;

namespace WizardWrx.Core
{
	/// <summary>
	/// The static methods exposed by this class simplify displaying information
	/// about the processor on which the calling process is executing that is
	/// otherwise hard to determine accurately.
	/// </summary>
	[SuppressUnmanagedCodeSecurityAttribute]
	public static class BasicSystemInfoDisplayMessages
	{
		#region Public Constants and Structures
		//	--------------------------------------------------------------------
		//	The following constants are ported from WWKernelLibWrapper.H. The
		//	line comment shows the original definition as it appeared therein.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Use as argument pintMajorMinimum to WWKW_OSIsMinimumVersionMaj or WWKW_OSIsMinimumVersionMin to test for Windows 2000 or newer.
		/// </summary>
		public const int WWKV_GETOSVERSIONINFO_WIN2K = 0x00000005;				// #define WWKV_GETOSVERSIONINFO_WIN2K         0x00000005L

		/// <summary>
		/// Use as argument pintMajorMinimum to WWKW_OSIsMinimumVersionMaj or WWKW_OSIsMinimumVersionMin to test for Windows Vista or newer.
		/// </summary>
		public const int WWKW_GETOSVERSIONINFO_VISTA = 0x00000006;				// #define WWKW_GETOSVERSIONINFO_VISTA         0x00000006L

		/// <summary>
		/// Use as argument pintMajorMinimum to WWKW_OSIsMinimumVersionMaj or WWKW_OSIsMinimumVersionMin to test for Windows 8 or newer.
		/// </summary>
		public const int WWKW_GETOSVERSIONINFO_WIN8 = 0x00000008;				// #define WWKW_GETOSVERSIONINFO_WIN8          0x00000008L

		/// <summary>
		/// Use as argument pintMajorMinimum to WWKW_OSIsMinimumVersionMaj or WWKW_OSIsMinimumVersionMin to test for Windows 10 or newer.
		/// </summary>
		public const int WWKW_GETOSVERSIONINFO_WIN10 = 0x0000000A;				// #define WWKW_GETOSVERSIONINFO_WIN10         0x0000000AL

		/// <summary>
		/// Use as argument pintOSVersionMinorinimum to WWKW_OSIsMinimumVersionMin to test for even numbered (.0) versions of Windows (e. g., Win28, Vista, 8.0, 10.0).
		/// </summary>
		public const int WWKW_GETOSVERSIONINFO_BASE = 0x00000000;				// #define WWKW_GETOSVERSIONINFO_BASE          0x00000000L     // Windows 2000, with WWKV_GETOSVERSIONINFO_WIN2K, or Windows Vista, with WWKW_GETOSVERSIONINFO_VISTA

		/// <summary>
		/// Use as argument pintOSVersionMinorinimum to WWKW_OSIsMinimumVersionMin to test for point versions of Windows (e. g., Windows XP (5.1), Windows 7 (which has an internal version number of 6.1), etc.
		/// </summary>
		public const int WWKW_GETOSVERSIONINFO_MINOR_1 = 0x00000001;			// #define WWKW_GETOSVERSIONINFO_PONT_1        0x00000001L     // Windows XP, with WWKV_GETOSVERSIONINFO_WIN2K, or Windows 7, with WWKW_GETOSVERSIONINFO_VISTA

		/// <summary>
		/// Use as argument pintOSVersionMinorinimum to WWKW_OSIsMinimumVersionMin to test for point two versions of Windows (e. g., Windows Server 2003, 2008, 2012).
		/// </summary>
		public const int WWKW_GETOSVERSIONINFO_MINOR_2 = 0x00000002;			// #define WWKW_GETOSVERSIONINFO_PONT_2        0x00000002L
		#endregion	// Public Constants and Structures


		#region Private Constants, Structures, and Platform Invoke Declarations
		const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
		const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
		const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
		const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

		const int INTPTR_BYTES = 4;

		[StructLayout ( LayoutKind.Sequential )]
		struct SYSTEM_INFO
		{
			public ushort wProcessorArchitecture;
			public ushort wReserved;
			public uint dwPageSize;
			public IntPtr lpMinimumApplicationAddress;
			public IntPtr lpMaximumApplicationAddress;
			public UIntPtr dwActiveProcessorMask;
			public uint dwNumberOfProcessors;
			public uint dwProcessorType;
			public uint dwAllocationGranularity;
			public ushort wProcessorLevel;
			public ushort wProcessorRevision;
		};	// SYSTEM_INFO structure

		[DllImport ( "kernel32.dll" )]
		static extern void GetNativeSystemInfo ( ref SYSTEM_INFO lpSystemInfo );

		[DllImport ( "kernel32.dll" )]
		static extern void GetSystemInfo ( ref SYSTEM_INFO lpSystemInfo );
		#endregion	// Private Constants, Structures, and Platform Invoke Declarations


		#region Public Methods
		/// <summary>
		/// Get a ProcessorArchitecture enumeration member that specifies the
		/// processor architecture.
		/// </summary>
		/// <returns>
		/// The return value is a member of the ProcessorArchitecture
		/// enumeration that specifies the type of processor installed on the
		/// machine.
		/// </returns>
		public static ProcessorArchitecture GetProcessorArchitecture ( )
		{
			SYSTEM_INFO sysInfo = new SYSTEM_INFO ( );

			if ( WWKW_OSIsWindowsXPOrNewer ( ) )
			{	// Use the new GetNativeSystemInfo, which supersedes GetSystemInfo function.
				GetNativeSystemInfo ( ref sysInfo );
			}	// True block, if ( WWKW_OSIsWindowsXPOrNewer ( ) ) 
			else
			{	// Use the deprecated GetSystemInfo function.
				GetSystemInfo ( ref sysInfo );
			}	// False block, if ( WWKW_OSIsWindowsXPOrNewer ( ) )

			switch ( sysInfo.wProcessorArchitecture )
			{
				case PROCESSOR_ARCHITECTURE_IA64:
					return ProcessorArchitecture.IA64;

				case PROCESSOR_ARCHITECTURE_AMD64:
					return ProcessorArchitecture.Amd64;

				case PROCESSOR_ARCHITECTURE_INTEL:
					return ProcessorArchitecture.X86;

				default:
					return ProcessorArchitecture.None;
			}	// switch ( sysInfo.wProcessorArchitecture )
		}	// GetProcessorArchitecture

		/// <summary>
		/// Get a string that describes the processor architecture and the size,
		/// in bits, of the machine word of the subsystem in which the calling
		/// process is executing.
		/// </summary>
		/// <returns>
		/// The return value is a string of the following form.
		/// 
		/// Processor Architecture = ProcessorArchitecture
		/// Process Machine Word   = NN bits
		/// </returns>
		public static string DisplayProcessorArchitecture ( )
		{
			return string.Format (
				Properties.Resources.PROCESSOR_AND_PROCESS_ARCHITECTURES ,		// Format Control String
				GetProcessorArchitecture ( ) ,									// Format Item 0: Processor Architecture = {0}
				IntPtr.Size == INTPTR_BYTES ? 32 : 64 ,							// Format Item 1: Process Machine Word   = {1} bits
				Environment.NewLine );											// Format Item 2: Embedded newlines all around
		}	// DisplayProcessorArchitecture

		//	--------------------------------------------------------------------
		//	The following methods are implemented as preprocessor macros in
		//	WKernelLibWrapper.H. However, since C# doesn't support macros, they
		//	are implemented as one-line static methods that we fervently hope to
		//	see substituted with code that is generated inline. Otherwise, they
		//	are cheap enough as out-of-line functions.
		//
		//	Only the first, WWKW_OSIsVistaOrNewer, is actually required by this
		//	class, but I decided that I may as well port all four, and be done
		//	with it. Eventually, they'll probably find a home in another library
		//	module.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Evaluate whether the operating system is at least Windows Vista.
		/// </summary>
		/// <returns>
		/// Return TRUE if the calling process is executing on Windows Vista or
		/// newer. Otherwise, return FALSE.
		/// </returns>
		public static bool WWKW_OSIsVistaOrNewer ( )
		{
			return Environment.OSVersion.Version.Major >= WWKW_GETOSVERSIONINFO_VISTA;
		}	// WWKW_OSIsVistaOrNewer

		/// <summary>
		/// Evaluate whether the operating system is at least Windows XP.
		/// </summary>
		/// <returns>
		/// Return TRUE if the calling process is executing on Windows XP or
		/// newer. Otherwise, return FALSE.
		/// </returns>
		public static bool WWKW_OSIsWindowsXPOrNewer ( )
		{
			return ( Environment.OSVersion.Version.Major > WWKV_GETOSVERSIONINFO_WIN2K ||
					( Environment.OSVersion.Version.Major == WWKV_GETOSVERSIONINFO_WIN2K && Environment.OSVersion.Version.Minor >= WWKW_GETOSVERSIONINFO_MINOR_1 ) );
		}	// WWKW_OSIsWindowsXPOrNewer

		/// <summary>
		/// Evaluate whether the operating system is at least Windows 7.
		/// </summary>
		/// <returns>
		/// Return TRUE if the calling process is executing on Windows 7 or
		/// newer. Otherwise, return FALSE.
		/// </returns>
		public static bool WWKW_OSIsWindows7OrNewer ( )
		{
			return Environment.OSVersion.Version.Major >= WWKW_GETOSVERSIONINFO_VISTA && Environment.OSVersion.Version.Minor >= WWKW_GETOSVERSIONINFO_MINOR_1;
		}	// WWKW_OSIsWindows7OrNewer

		/// <summary>
		/// Evaluate whether the operating system is at least equal to the
		/// specified major version number.
		/// </summary>
		/// <returns>
		/// Return TRUE if the calling process is executing on the specified
		/// major version number of Windows or newer. Otherwise, return FALSE.
		/// </returns>
		public static bool WWKW_OSIsMinimumVersionMaj ( int pintMajorMinimum )
		{
			return Environment.OSVersion.Version.Major >= pintMajorMinimum;
		}	// WWKW_OSIsMinimumVersionMaj

		/// <summary>
		/// Evaluate whether the operating system is at least equal to the
		/// specified major and minor version number.
		/// </summary>
		/// <returns>
		/// Return TRUE if the calling process is executing on the specified
		/// major and minor version number of Windows or newer. Otherwise,
		/// return FALSE.
		/// </returns>
		public static bool WWKW_OSIsMinimumVersionMin ( int pintMajorMinimum , int pintOSVersionMinorinimum )
		{
			return Environment.OSVersion.Version.Major >= pintMajorMinimum && Environment.OSVersion.Version.Minor >= pintOSVersionMinorinimum;
		}	// WWKW_OSIsMinimumVersionMin
		#endregion	// Public Methods
	}	// public static class BasicSystemInfoDisplayMessages
}	// partial namespace WizardWrx.Core