/*
    ============================================================================

    Namespace:          WizardWrx.ConsoleStreams

    Class Name:			StandardHandleInfo

	File Name:			StandardHandleInfo.cs

    Synopsis:			This class is the managed implementation of the two
                        DllServices2 methods that still rely on unmanaged Win32
                        API wrappers.

    Remarks:			These are 100% managed, if you count a handful of calls
						into the Windows API via Platform Invoke as managed.

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

	Date Begun:			Friday, 07 July 2017

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
	2017/07/09 1.0	   DAG    This class makes its debut in ConsoleStreamsLab.

	2017/07/11 7.0     DAG    This class takes its rightful place in library
                              WizardWrx.ConsoleStreams, having been proved in my
                              ConsoleStreamsLab test assembly, where I left the
                              original, at least for the present, and probably
                              indefinitely.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.
    ============================================================================
*/


using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

using WizardWrx;
using WizardWrx.Core;

namespace WizardWrx.ConsoleStreams
{
	/// <summary>
	/// This static class provides type-safe managed methods to determine the
	/// true redirection state of a standard console handle, including, if
	/// applicable, learning the name of the file to which it is redirected.
	/// </summary>
	/// <remarks>
	/// The standard properties on the Process (all versions) and Console
	/// (version 4.5 and newer) objects report based solely on redirections
	/// brought about by the Base Class Library. They completely ignore redirection
	/// that happens in the shell, before the process started. Hence, to learn the
	/// true redirection state requires a couple of Platform Invoke calls into the
	/// Windows API, which this class provides through type-safe wrappers.
	/// </remarks>
	[SuppressUnmanagedCodeSecurityAttribute]
	public static class StandardHandleInfo
	{
		#region Public Constants and Enumerations
		//	--------------------------------------------------------------------
		//  The Input Mode flags returned by GetConsoleMode are transformed into
		//	a bit mapped enumeration.
		//	--------------------------------------------------------------------

		/// <summary>
		///	Use these flags to test the values returned by GetConsoleMode.
		/// </summary>
		/// <remarks>
		/// In wincon.h, these are defined as symbolic constants, although there
		/// is ample justification for rolling them into a bit-mapped
		/// enumeration. I suspect the reason they aren't is that the change
		/// would break too much legacy code.
		/// 
		/// For the present, GetConsoleMode is called, but only for a side
		/// effect; its return value is discarded.
		/// 
		/// Though currently unused, the enumeration is reserved for future use.
		/// 
		/// The summaries shown here for all but the last member of this
		/// enumeration are quoted verbatim from the MSDN manual page of the
		/// SetConsoleMode function cited below.
		/// </remarks>
		/// <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms686033(v=vs.85).aspx"/>
		[Flags]
		public enum ConsoleModes
		{
			/// <summary>
			/// Characters written by the WriteFile or WriteConsole function or
			/// echoed by the ReadFile or ReadConsole function are examined for
			/// ASCII control sequences and the correct action is performed.
			/// Backspace, tab, bell, carriage return, and line feed characters
			/// are processed.
			/// </summary>
			ENABLE_PROCESSED_INPUT = 0x0001 ,									// #define ENABLE_PROCESSED_INPUT  0x0001, per wincon.h

			/// <summary>
			/// The ReadFile or ReadConsole function returns only when a
			/// carriage return character is read. If this mode is disabled, the
			/// functions return when one or more characters are available.
			/// </summary>
			ENABLE_LINE_INPUT = 0x0002 ,										// #define ENABLE_LINE_INPUT       0x0002, per wincon.h

			/// <summary>
			/// Characters read by the ReadFile or ReadConsole function are
			/// written to the active screen buffer as they are read. This mode
			/// can be used only if the ENABLE_LINE_INPUT mode is also enabled.
			/// </summary>
			ENABLE_ECHO_INPUT = 0x0004 ,										// #define ENABLE_ECHO_INPUT       0x0004, per wincon.h

			/// <summary>
			/// User interactions that change the size of the console screen
			/// buffer are reported in the console's input buffer. Information
			/// about these events can be read from the input buffer by
			/// applications using the ReadConsoleInput function, but not by
			/// those using ReadFile or ReadConsole.
			/// </summary>
			ENABLE_WINDOW_INPUT = 0x0008 ,										// #define ENABLE_WINDOW_INPUT     0x0008, per wincon.h

			/// <summary>
			/// If the mouse pointer is within the borders of the console window
			/// and the window has the keyboard focus, mouse events generated by
			/// mouse movement and button presses are placed in the input buffer.
			/// These events are discarded by ReadFile or ReadConsole, even when
			/// this mode is enabled.
			/// </summary>
			ENABLE_MOUSE_INPUT = 0x0010 ,										// #define ENABLE_MOUSE_INPUT      0x0010, per wincon.h

			/// <summary>
			/// When enabled, text entered in a console window will be inserted
			/// at the current cursor location and all text following that
			/// location will not be overwritten. When disabled, all following
			/// text will be overwritten.
			/// 
			/// To enable this mode, use ENABLE_INSERT_MODE | ENABLE_EXTENDED_FLAGS.
			/// 
			/// To disable this mode, use ENABLE_EXTENDED_FLAGS without this flag.
			/// </summary>
			ENABLE_INSERT_MODE = 0x0020 ,										// #define ENABLE_INSERT_MODE      0x0020, per wincon.h

			/// <summary>
			/// This flag enables the user to use the mouse to select and edit
			/// text.
			/// 
			/// To enable this mode, use ENABLE_QUICK_EDIT_MODE | ENABLE_EXTENDED_FLAGS.
			/// 
			/// To disable this mode, use ENABLE_EXTENDED_FLAGS without this flag.
			/// </summary>
			ENABLE_QUICK_EDIT_MODE = 0x0040 ,									// #define ENABLE_QUICK_EDIT_MODE  0x0040, per wincon.h

			/// <summary>
			/// Required to enable or disable extended flags.
			/// 
			/// See ENABLE_INSERT_MODE and ENABLE_QUICK_EDIT_MODE.
			/// </summary>
			ENABLE_EXTENDED_FLAGS = 0x0080 ,									// #define ENABLE_EXTENDED_FLAGS   0x0080, per wincon.h

			/// <summary>
			/// Though defined in WinCon.h, this flag is otherwise undocumented.
			/// 
			/// My initial best guess is that it is related to the "Let system
			/// position window" check box on the property sheet of a CMD.EXE
			/// window, though I have yet to test this theory.
			/// </summary>
			ENABLE_AUTO_POSITION = 0x0100 ,										// #define ENABLE_AUTO_POSITION    0x0100, per wincon.h
		}	// ConsoleModes


		//	--------------------------------------------------------------------
		//	The Output Mode flags used with GetConsoleMode are transformed into
		//	a bit mapped enumeration.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Use these flags with SetConsoleMode to control how long output lines
		/// are handled.
		/// </summary>
		/// <remarks>
		/// In wincon.h, these are defined as symbolic constants, although there
		/// is ample justification for rolling them into a bit-mapped
		/// enumeration. I suspect the reason they aren't is that the change
		/// would break too much legacy code.
		/// 
		/// For the present, SetConsoleMode is not called.
		/// 
		/// Though currently unused, the enumeration is reserved for future use.
		/// 
		/// The summaries shown here for all but the last member of this
		/// enumeration are quoted verbatim from the MSDN manual page of the
		/// SetConsoleMode function cited below.
		/// </remarks>
		/// <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms686033(v=vs.85).aspx"/>
		[Flags]
		public enum ConsoleOutputModes
		{
			/// <summary>
			/// Characters written by the WriteFile or WriteConsole function or
			/// echoed by the ReadFile or ReadConsole function are examined for
			/// ASCII control sequences and the correct action is performed.
			/// Backspace, tab, bell, carriage return, and line feed characters
			/// are processed.
			/// </summary>
			ENABLE_PROCESSED_OUTPUT = 0x0001 ,									// #define ENABLE_PROCESSED_OUTPUT    0x0001, per wincon.h

			/// <summary>
			/// When writing with WriteFile or WriteConsole or echoing with
			/// ReadFile or ReadConsole, the cursor moves to the beginning of
			/// the next row when it reaches the end of the current row. This
			/// causes the rows displayed in the console window to scroll up
			/// automatically when the cursor advances beyond the last row in
			/// the window. It also causes the contents of the console screen
			/// buffer to scroll up (discarding the top row of the console
			/// screen buffer) when the cursor advances beyond the last row in
			/// the console screen buffer. If this mode is disabled, the last
			/// character in the row is overwritten with any subsequent
			/// characters.
			/// </summary>
			ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002									// #define ENABLE_WRAP_AT_EOL_OUTPUT  0x0002, per wincon.h
		}	// ConsoleOutputModes

		//	--------------------------------------------------------------------
		//	This enumeration was ported from StandardHandleState.H.
		//	--------------------------------------------------------------------

		/// <summary>
		/// This enumeration defines the three valid values for an enumeration,
		/// StandardConsoleHandle, the input argument type of methods
		/// StandardHandleState and GetRedirectedFileName, both public methods,
		/// and GetStandardHandle, currently private, but under consideration
		/// for a promotion.
		/// 
		/// The upper and lower values are for efficient boundary testing.
		/// </summary>
		/// <remarks>
		/// Although the standard handles have well-known fixed values, each
		/// OF WHICH HAS a corresponding symbolic constant defined in winbase.h,
		/// they are nonstandard handle values (all three less than zero), which
		/// would usually be interpreted as invalid handles. These values would
		/// be unusual, though perfectly legal, enumeration values.
		/// 
		/// Instead, I defined this enumeration, and provided a private method,
		/// GetStandardHandle, that maps the enumeration to the corresponding 
		/// standard handle constant by way of a simple lookup table that uses
		/// the enumeration value, cast to integer, as its index. The returned
		/// Windows handle value is a 32 or 64 bit positive integer, which that
		/// function passes into GetStdHandle (via Platform Invoke), through
		/// which it returns.
		/// 
		/// This is one of two public enumerations that were ported from
		/// StandardHandleState.H, the C/C++ header that defines the interfaces
		/// that this class supersedes.
		/// </remarks>
		public enum StandardConsoleHandle
		{
			/// <summary>
			/// This member is both the uninitialized value of a variable of its
			/// type and the lower limit of the range of valid values.
			/// </summary>
			Undefined ,															// Value = 0, which is reserved for indicating that the value is uninitialized.

			/// <summary>
			/// This member signifies a request to perform an operation on the
			/// Standard Input stream, STDOUT, which corresponds to the Console.In
			/// and System.Diagnostics.Process.StandardInput properties.
			/// </summary>
			StandardInput ,														// Value = 1, which corresponds to STD_INPUT_HANDLE

			/// <summary>
			/// This member signifies a request to perform an operation on the
			/// Standard Output stream, STDIN, which corresponds to the Console.Out
			/// and System.Diagnostics.Process.StandardOutput properties.
			/// </summary>
			StandardOutput ,													// Value = 2, which corresponds to STD_OUTPUT_HANDLE

			/// <summary>
			/// This member signifies a request to perform an operation on the
			/// Standard Output stream, STDERR, which corresponds to the Console.Error
			/// and System.Diagnostics.Process.StandardError properties.
			/// </summary>
			StandardError ,														// Value = 3, which corresponds to STD_ERROR_HANDLE

			/// <summary>
			/// This member is intended for bounds-checking values that are cast
			/// to an integral type.
			/// </summary>
			OutOfRange															// Value = 4, which is reserved for indicating that the value is out of range.
		}	// StandardConsoleHandle

		//	--------------------------------------------------------------------
		//	This enumeration was ported from StandardHandleState.H.
		//	--------------------------------------------------------------------

		/// <summary>
		/// This enumeration defines the two valid states and two defined error
		/// states that may be returned by the StandardHandleState method, along
		/// with a pair of values that may be returned to indicate one of two
		/// error conditions.
		/// </summary>
		/// <remarks>
		/// This is one of two public enumerations that were ported from
		/// StandardHandleState.H, the C/C++ header that defines the interfaces
		/// that this class supersedes.
		/// </remarks>
		public enum StandardHandleState
		{
			/// <summary>
			/// This member is returned when the supplied StandardConsoleHandle
			/// member is out of range.
			/// </summary>
			Undetermined ,														// Value = 0, indicating that StandardConsoleHandle is either Undetermined or out of range

			/// <summary>
			/// This member is returned when console stream that corresponds to
			/// the specified StandardConsoleHandle member is attached to its
			/// console.
			/// </summary>
			Attached ,															// Value = 1, indicating that the handle corresponding to the value of StandardConsoleHandle is attached to its console
			
			/// <summary>
			/// This member is returned when console stream that corresponds to
			/// the specified StandardConsoleHandle member is redirected to a
			/// file.
			/// </summary>
			Redirected ,														// Value = 2, indicating that the handle corresponding to the value of StandardConsoleHandle is redirected

			/// <summary>
			/// This member is returned when attempting to get the state of the
			/// specified StandardConsoleHandle raised an exception.
			/// </summary>
			STATE_SYSTEM_ERROR													// Value = 3, indicating that an internal error occurred. Call GetLastError to learn why.
		}	// StandardHandleState

		//	--------------------------------------------------------------------
		//	Since these are treated as constants, and must be visible to 
		//	calling routines, these static read-only IntPtr values belong in 
		//	this section.
		//	--------------------------------------------------------------------

		/// <summary>
		/// When a routine, such as GetStandardHandle cannot return a usable
		/// file handle, this is the return value, cast to IntPtr, as is the
		/// expected file handle.
		/// </summary>
		/// <remarks>
		/// Were it not that IntPtr is a structure, INVALID_HANDLE_VALUE would
		/// be defined as a public constant.
		/// </remarks>
		public static readonly IntPtr INVALID_HANDLE_VALUE = ( IntPtr ) ( -1 );	// INVALID_HANDLE_VALUE is defined in the Windows Platform SDK, but I've forgotten in which header. Since its type is IntPtr, a structure type, it cannot be a constant.

		/// <summary>
		/// When a routine, such as GetModuleHandle cannot return a usable
		/// module handle, this is the return value, cast to IntPtr, as is the
		/// expected module handle.
		/// </summary>
		/// <remarks>
		/// Were it not that IntPtr is a structure, INVALID_HANDLE_VALUE would
		/// be defined as a public constant.
		/// </remarks>
		public static readonly IntPtr INVALID_MODULE_HANDLE = IntPtr.Zero;		// INVALID_MODULE_HANDLE must also be defined as a static read-only variable for the same reason as does INVALID_HANDLE_VALUE.
		#endregion	// Public Constants and Enumerations


		#region Private Constants and Enumerations
		//	--------------------------------------------------------------------
		//	The following constants were ported from WinBase.h. They could have
		//	been built up into a bit-mapped enumeration, but for this one-off
		//	application, I decided that it wasn't worth the effort.
		//	--------------------------------------------------------------------

		const UInt32 VOLUME_NAME_DOS = 0x0;										// #define VOLUME_NAME_DOS 0x0, per WinBase.h (default)
		const UInt32 VOLUME_NAME_GUID = 0x1;									// #define VOLUME_NAME_GUID 0x1, per WinBase.h
		const UInt32 VOLUME_NAME_NT = 0x2;										// #define VOLUME_NAME_NT   0x2, per WinBase.h
		const UInt32 VOLUME_NAME_NONE = 0x4;									// #define VOLUME_NAME_NONE 0x4, per WinBase.h

		const UInt32 FILE_NAME_NORMALIZED = 0x0;								// #define FILE_NAME_NORMALIZED 0x0, per WinBase.h (default)
		const UInt32 FILE_NAME_OPENED = 0x8;									// #define FILE_NAME_OPENED     0x8, per WinBase.h
		
		//	--------------------------------------------------------------------
		//	This enumeration was ported from StandardHandleState.H.
		//	--------------------------------------------------------------------

		enum SHS_HANDLE_LABELS
		{
			SHS_HANDLE_SHORT_LABEL ,											// Short Label
			SHS_HANDLE_LONG_LABEL ,												// Long Label
			SHS_HANDLE_CONSTANT_NAME 											// Standard Handle Symbolic Constant Name
		}	// SHS_HANDLE_LABELS

		//	--------------------------------------------------------------------
		//	This array was ported from SHS_StandardHandleState.C.
		//
		//  The standard handle values accepted by GetStdHandle are values that
		//	would be invalid if returned as Windows handles of any kind.
		//	--------------------------------------------------------------------

		const int STD_INPUT_HANDLE = -10;										// #define STD_INPUT_HANDLE    ((DWORD)-10), per winbase.h
		const int STD_OUTPUT_HANDLE = -11;										// #define STD_OUTPUT_HANDLE   ((DWORD)-11), per winbase.h
		const int STD_ERROR_HANDLE = -12;										// #define STD_ERROR_HANDLE    ((DWORD)-12), per winbase.h

		const int ERROR_INVALID_HANDLE = 0x00000006;							// #define ERROR_INVALID_HANDLE          6L, per winerror.h
		#endregion	// Private Constants and Enumerations


		#region Private static Data
		/// <summary>
		/// This constant is a place holder to occupy the first slot in the 
		/// standard handle mapping table, s_adwStdConsoleHandleIDs, which
		/// corresponds to an uninitialized value.
		/// </summary>
		const int STD_HANDLE_UNDEFINED = MagicNumbers.ZERO;

		static readonly Int32 [ ] s_adwStdConsoleHandleIDs =
		{
			STD_HANDLE_UNDEFINED ,												// This value is intentionally invalid, and corresponds to SHS_UNDEFINED.
			STD_INPUT_HANDLE ,													// SHS_STDIN
			STD_OUTPUT_HANDLE ,													// SHS_STDOUT
			STD_ERROR_HANDLE 													// SHS_STERR
		};	// dwStdConsoleHandleIDs

		static int s_intLastWin32Error = MagicNumbers.ERROR_SUCCESS;
		#endregion	// Private static Data


		#region Platform Invoke Declarations for Windows Platform SDK Routines
		[DllImport ( "kernel32.dll" ,
					 SetLastError = true )]
		static extern bool GetConsoleMode
			(
				IntPtr hConsoleHandle ,
				out uint lpMode
			);
		[DllImport ( "kernel32.dll" ,
			         SetLastError = true )]
		static extern IntPtr GetStdHandle ( int nStdHandle );

		// typedef DWORD ( WINAPI* tGetFinalPathNameByHandle )( HANDLE , LPTSTR , DWORD , DWORD );
		delegate Int32 GetFinalPathNameByHandle ( IntPtr hFile , IntPtr lpszFilePath , UInt32 cchFilePath , UInt32 dwFlags );
		#endregion	// Platform Invoke Declarations for Windows Platform SDK Routines


		#region Public Static Methods
		//	--------------------------------------------------------------------
		//	This method was ported from SHS_StandardHandleState.C. Portions of
		//	it were subsequently re-factored into GetStandardHandle, to vastly
		//	simplify porting SHS_GetStdHandleFNCLI to GetRedirectedFileName.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Get the redirection state of a specified standard console stream.
		/// </summary>
		/// <param name="penmStdHandleID">
		/// Use a member of the StandardConsoleHandle enumeration to designate
		/// the desired standard console stream.
		/// </param>
		/// <returns>
		/// The return value is one of the following.
		/// 
		/// StandardHandleState.Attached means that the specified stream is
		/// attached to its console.
		/// 
		/// StandardHandleState.Redirected means that the specified stream is
		/// redirected.
		/// 
		/// Call GetRedirectedFileName to get the fully qualified name of the
		/// file to which it is redirected.
		/// </returns>
		/// <remarks>
		/// In all versions of the framework, the following properties report
		/// inaccurately on the true state of the console stream.
		/// 
		/// - System.Diagnostics.Process.StartInfo.RedirectStandardInput ,
		/// - System.Diagnostics.Process.StartInfo.RedirectStandardOutput ,
		/// - System.Diagnostics.Process.StartInfo.RedirectStandardError ,
		///
		/// The following properties, added in version 4.5, also report incorrectly.
		/// 
		/// - Console.IsErrorRedirected
		/// - Console.IsInputRedirected
		/// - Console.IsOutputRedirected
		/// 
		/// The above properties report inaccurately because their report is
		/// based entirely on changes made by calls to methods on the Console
		/// class. They exhibit no knowledge, whatsoever, of what the underlying
		/// operating system does in response to shell commands to redirect one
		/// or more of the standard console streams.
		/// 
		/// On the other hand, since StandardHandleState gets its information 
		/// from GetConsoleMode, a native Windows API routine, its report takes 
		/// into account console stream redirections that are the result of
		/// shell commands.
		/// </remarks>
		public static StandardHandleState GetStandardHandleState ( StandardConsoleHandle penmStdHandleID )
		{
			IntPtr hThis = INVALID_MODULE_HANDLE;								// In the ANSI C implementation, the first use of this variable initializes it. As a concession to the C# compiler, this version employs a static initializer.
			UInt32 dwModde = ( uint ) INVALID_MODULE_HANDLE;					// The first use of this variable initializes it, AND it's a throwaway. As a concession to the C# compiler, this version employs a static initializer.

			if ( ( hThis = GetStandardHandle ( penmStdHandleID ) ) != INVALID_HANDLE_VALUE )
			{	// Handle acquired. Since GetStdHandle leaves the reference count unchanged, hThis needn't, and shouldn't, be closed.
				if ( GetConsoleMode ( hThis , out dwModde ) )
				{	// Handles that are attached to a console have a ConsoleMode.
					return StandardHandleState.Attached;
				}	// TRUE (Handle is attached to its console.) block, if ( GetConsoleMode ( hThis , &dwModde ) )
				else
				{	// When a console stream is redirected, its handle loses its ConsoleMode.
					s_intLastWin32Error = Marshal.GetLastWin32Error ( );

					if ( s_intLastWin32Error == ERROR_INVALID_HANDLE )
					{	// Since LastError is ERROR_INVALID_HANDLE, the cause of the failure is that the handle is redirected.
						return StandardHandleState.Redirected;
					}	// TRUE (anticipated outcome) block, if ( intLastWin32Error == ERROR_INVALID_HANDLE )
					else
					{	// Something besides redirection caused GetConsoleMode to fail.
						return StandardHandleState.STATE_SYSTEM_ERROR;
					}	// FALSE (unanticipated outcome) block, if ( intLastWin32Error == ERROR_INVALID_HANDLE )
				}	// FALSE (Handle is redirected from its console into a file or pipe.) block, if ( GetConsoleMode ( hThis , &dwModde ) )
			}	// TRUE (anticipated outcome) block, if ( ( hThis = GetStdHandle ( dwStdConsoleHandleIDs [ ( int ) penmStdHandleID ] ) ) != ( IntPtr ) INVALID_HANDLE_VALUE )
			else
			{	// Windows declined the request. Report that GetStdHandle failed.
				s_intLastWin32Error = Marshal.GetLastWin32Error ( );
				return penmStdHandleID == StandardConsoleHandle.Undefined
					? StandardHandleState.Undetermined
					: StandardHandleState.STATE_SYSTEM_ERROR;
			}	// FALSE (unanticipated outcome) block, if ( ( hThis = GetStdHandle ( dwStdConsoleHandleIDs [ ( int ) penmStdHandleID ] ) ) != ( IntPtr ) INVALID_HANDLE_VALUE )
		}	// SHS_StandardHandleState

		//	--------------------------------------------------------------------
		//	This method is a consolidated port of  SHS_GetStdHandleFNCLI and 
		//	SHS_GetStdHandleFNW, which are rolled into one method.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Get the name of the file to which a specified standard handle is
		/// redirected.
		/// </summary>
		/// <param name="penmStdHandleID">
		/// Use a member of the StandardConsoleHandle enumeration to designate
		/// the desired standard console stream.
		/// </param>
		/// <returns>
		/// If the file is redirected, the returned string contains the fully
		/// qualified name of the file to which it is redirected. If the handle
		/// is attached to its console (that is, not redirected), the return
		/// value is the empty string.
		/// </returns>
		public static string GetRedirectedFileName ( StandardConsoleHandle penmStdHandleID )
		{
			if ( GetStandardHandleState ( penmStdHandleID ) != StandardHandleState.Redirected )
			{	// A stream that is attached to its console has no file.
				return SpecialStrings.EMPTY_STRING;
			}	// if ( StandardHandleState ( penmStdHandleID ) != StandardHandleState.Redirected )

			if ( BasicSystemInfoDisplayMessages.WWKW_OSIsVistaOrNewer ( ) )
			{	// This API requires Windows Vista or newer.
				using ( WizardWrx.Core.UnmanagedLibrary lib = new WizardWrx.Core.UnmanagedLibrary ( "kernel32" ) )		// becomes call to LoadLibrary
				{	// Since it employs unmanaged resources, the UnmanagedLibrary instance implements IDisposable; hence, the using block.
					GetFinalPathNameByHandle fnGetFinalPathNameByHandle = lib.GetUnmanagedFunction<GetFinalPathNameByHandle> ( "GetFinalPathNameByHandleW" );
					
					char [ ] achrManagedBuffer = new char [ MagicNumbers.CAPACITY_32KB ];	// Reserve 32K characters.
					GCHandle gchPinnedArray = GCHandle.Alloc (
						achrManagedBuffer ,													// Now that it is allocated, keep the garbage collector away from it.		
						GCHandleType.Pinned );
					IntPtr lpManagedBuffer = gchPinnedArray.AddrOfPinnedObject ( );			// Since IntPtr is a structure, this operation cannot be nested.

					//	--------------------------------------------------------
					//	Since the return value, intFQFNLength, is the last 
					//	parameter of the string constructor, which must be first
					//	onto the stack, this function call cannot be nested.
					//	--------------------------------------------------------

					Int32 intFQFNLength = fnGetFinalPathNameByHandle (
						GetStandardHandle ( penmStdHandleID ) ,								// IntPtr hFile
						lpManagedBuffer ,													// IntPtr lpszFilePath
						MagicNumbers.CAPACITY_32KB ,										// UInt32 cchFilePath
						FILE_NAME_NORMALIZED | VOLUME_NAME_DOS );							// UInt32 dwFlags

					return new string (														// Array of characters in, string out.
						achrManagedBuffer ,													//		Array from which to copy characters
						ArrayInfo.ARRAY_FIRST_ELEMENT ,										//		Start copying at this index into the array.
						intFQFNLength );													//		Stop when this many characters have been copied.
				}	// implicit call to lib.Dispose, which calls FreeLibrary.
			}	// TRUE (anticipated outcome) block, if ( WWKW_OSIsVistaOrNewer ( ) )
			else
			{	// Windows versions before Vista are unsupported.
				throw new InvalidOperationException (
					string.Format (
						Properties.Resources.ERRMSG_UNSUPPORTED_WINDOWS_VERSION ,			// Format Control String
						new object [ ]														// Make the parameter array explicit,
						{																	//		and the ToString calls implicit.
							Environment.OSVersion.Version.Major ,							// Format Item 0 = installed version of Microsoft Windows, version {0}
							Environment.OSVersion.Version.Minor ,							// Format Item 1 = {1}.
							BasicSystemInfoDisplayMessages.WWKW_GETOSVERSIONINFO_VISTA ,	// Format Item 2 = minimum supported version is {2}.
							BasicSystemInfoDisplayMessages.WWKW_GETOSVERSIONINFO_BASE ,		// Format Item 3 = .{3}.
							Environment.NewLine												// Format Item 4 = Newline embedded: .{4}The minimum supported
						} ) );
			}	// FALSE (unanticipated outcome) block, if ( WWKW_OSIsVistaOrNewer ( ) )
		}	// GetRedirectedFileName


		/// <summary>
		/// Get the last Win32 status code logged by Marshal.GetLastWin32Error.
		/// </summary>
		/// <returns>
		/// The return value is the value returned by the last call to
		/// Marshal.GetLastWin32Error.
		/// </returns>
		/// <remarks>
		/// As is true of the other methods defined by this class, this method
		/// is not yet thread-safe.
		/// </remarks>
		public static Int32 GetWin32StatusCode ( )
		{
			return s_intLastWin32Error;
		}	// GetWin32StatusCode
		#endregion	// Public Static Methods


		#region Private Static Methods
		/// <summary>
		/// Though perhaps a tad less efficient than calling the like named
		/// Windows API function, this method is a 100% managed approach to the
		/// task of getting the instance handle (base address) of a module.
		/// </summary>
		/// <param name="pstrModuleName">
		/// Specify the base name of the desired module. Omit the .DLL suffix.
		/// </param>
		/// <returns>
		/// If a like named module exists in the collection of loaded modules,
		/// its instance handle, which happens also to be its base address, is
		/// returned. Otherwise, the return value is INVALID_MODULE_HANDLE.
		/// </returns>
		/// <see href="https://blogs.msdn.microsoft.com/oldnewthing/20041025-00/?p=37483"/>
		private static IntPtr GetModuleHandle ( string pstrModuleName )
		{
			string strModuleNameMatch = pstrModuleName.ToLower ( );

			foreach ( System.Diagnostics.ProcessModule thisModule in System.Diagnostics.Process.GetCurrentProcess ( ).Modules )
			{
				if ( thisModule.ModuleName.ToLower ( ) == strModuleNameMatch )
				{	// Save the base address, so that the module can be disposed.
					IntPtr rHINSTHandle =  thisModule.BaseAddress;
					thisModule.Dispose ( );
					return rHINSTHandle;
				}	// TRUE (The desired module is loaded, and has been found in the collection.) block, if ( thisModule.ModuleName.ToLower ( ) == strModuleNameMatch )
				else
				{	// Modules have unmanaged resources, and implement IDisposable. Do so.
					thisModule.Dispose ( );
				}	// FALSE (The current module isn't the one that was sought.) block, if ( thisModule.ModuleName.ToLower ( ) == strModuleNameMatch )
			}	// foreach ( System.Diagnostics.ProcessModule thisModule in System.Diagnostics.Process.GetCurrentProcess ( ).Modules )

			return INVALID_MODULE_HANDLE;
		}	// GetModuleHandle


		private static IntPtr GetStandardHandle ( StandardConsoleHandle penmStdHandleID )
		{
			IntPtr hThis = INVALID_HANDLE_VALUE;											// In the ANSI C implementation, the first use of this variable initializes it. As a concession to the C# compiler, this version employs a static initializer.

			switch ( penmStdHandleID )
			{
				case StandardConsoleHandle.StandardInput:
				case StandardConsoleHandle.StandardOutput:
				case StandardConsoleHandle.StandardError:
					return GetStdHandle (
						s_adwStdConsoleHandleIDs [ ( int ) penmStdHandleID ] );				// An invalid argument elicits a return value of INVALID_HANDLE_VALUE, for which the caller must test. 

				case StandardConsoleHandle.Undefined:										// Argument penmStdHandleID is uninitialized.
				case StandardConsoleHandle.OutOfRange:										// Argument penmStdHandleID is out of range.
				default:																	
					return INVALID_HANDLE_VALUE ;
			}	// switch ( penmStdHandleID )
		}	// GetStandardHandle
		#endregion	// Private Static Methods
	}	// public static class StandardHandleInfo
}	// partial namespace WizardWrx.ConsoleStreams