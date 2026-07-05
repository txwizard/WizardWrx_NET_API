/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:			UnmanagedLibrary

	File Name:			UnmanagedLibrary.cs

    Synopsis:			This class is type-safe managed wrappers for kernel32
						routines LoadLibrary and GetProcAddress, published as
                        part of an article on the subject.

    Remarks:			These are 100% managed, if you count a handful of calls
						into the Windows API via Platform Invoke as managed.

	Reference:			Type-safe Managed wrappers for kernel32!GetProcAddress
						Mike Stall - MSFT
						January 6, 2007
						https://blogs.msdn.microsoft.com/jmstall/2007/01/06/type-safe-managed-wrappers-for-kernel32getprocaddress/

    Author:				Mike Stall - MSFT (Microsoft Corporation)
						Please see the Reference for complete details.

    License:            Copyright (C) 2007, Mike Stall - MSFT.
						All rights reserved.

						Though I made a few cosmetic changes, and corrected one
						incorrect cross reference, this class is the work of
                        Mike Stall, working on behalf of Microsoft Corporation,
						and he deserves full credit for the work.Except for the
						first entry below, which bears no version number, the
						history entries reflect my inclusion of the code into my
						code.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    20017/01/06        MStall Mike Stall published this code as part of the MSDN
                              article cited above.
	2017/07/09 1.0	   DAG    This class makes its debut in ConsoleStreamsLab.
	2017/07/11 7.0     DAG    This class takes its rightful place in library
                              WizardWrx.Core, having been proved in my
                              ConsoleStreamsLab test assembly, where I left the
                              original, at least for the present, and probably
                              indefinitely.
    ============================================================================
*/


using System;

using Microsoft.Win32.SafeHandles;

using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace WizardWrx.Core
{
	/// <summary>
    /// Utility class to wrap an unmanaged DLL and be responsible for freeing it
    /// </summary>
    /// <remarks>
	/// This is a managed wrapper over the native LoadLibrary, GetProcAddress,
	/// and FreeLibrary calls.
	/// 
	/// I didn't immediately notice the fact that this class is sealed, but not
	/// static; hence, public UnmanagedLibrary ( string fileName ) is its one
	/// and only public constructor. As insurance against accidentally creating
	/// a useless, uninitialized instance, I added a default constructor, marked
	/// as private.
    /// </remarks>
    public sealed class UnmanagedLibrary : IDisposable
    {
        #region Safe Handles and Native imports
        // See http://msdn.microsoft.com/msdnmag/issues/05/10/Reliability/ for more about safe handles.
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
			private SafeLibraryHandle ( ) : base ( true ) { }

			protected override bool ReleaseHandle ( )
			{
				return NativeMethods.FreeLibrary ( handle );
			}	// ReleaseHandle
        }	// class SafeLibraryHandle

        static class NativeMethods
        {
			const string KERNEL32 = "kernel32";
            [DllImport(KERNEL32, CharSet = CharSet.Auto, BestFitMapping = false, SetLastError = true)]
            public static extern SafeLibraryHandle LoadLibrary(string fileName);

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport(KERNEL32, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport(KERNEL32)]
            public static extern IntPtr GetProcAddress(SafeLibraryHandle hModule, String procname);
        }	// class NativeMethods
        #endregion // Safe Handles and Native imports

		/// <summary>
		/// Since Mike didn't provide a default constructor, and there is no
		/// provision for initializing an object after the fact, I took the
		/// precaution of hiding the default constructor.
		/// </summary>
		private UnmanagedLibrary ( )
		{
		}	// The default constructor is marked as private, since an uninit8ialied object is pretty useless.

        /// <summary>
        /// Constructor to load a DLL and be responsible for freeing it
        /// </summary>
		/// <param name="fileName">
		/// full path name of DLL to load
		/// </param>
		/// <exception cref="System.IO.FileNotFoundException">
		/// if fileName cannot be found
		/// </exception>
        /// <remarks>
		/// Throws exceptions on failure. Most common failure would be file-not-found, or
        /// that the file is not a loadable image.
		/// </remarks>
		public UnmanagedLibrary ( string fileName )
        {
			m_hLibrary = NativeMethods.LoadLibrary ( fileName );

			if ( m_hLibrary.IsInvalid )
			{
				Marshal.ThrowExceptionForHR ( Marshal.GetHRForLastWin32Error ( ) );
			}	// if ( m_hLibrary.IsInvalid )
        }	// UnmanagedLibrary


        /// <summary>
        /// Dynamically lookup a function via kernel32!GetProcAddress.
        /// </summary>
		/// <param name="functionName">
		/// raw name of the function in the export table
		/// </param>
        /// <returns>
		/// null if function is not found, else a delegate to the unmanaged
		/// function
        /// </returns>
        /// <remarks>
		/// GetProcAddress results are valid as long as the DLL is loaded. This
		/// is very, very dangerous to use since you need to ensure that the DLL
		/// is not unloaded until after you’re done with any objects implemented
		/// by the DLL. For example, if you get a delegate that then gets an
		/// IUnknown implemented by this DLL, you can not dispose this library
		/// until that IUnknown is collected. Else, you may free the library, and
		/// then the CLR may call release on that IUnknown, leading to a crash.
		/// 
		/// Declare your delegate pretty much as you would in C or C++; if you
		/// happen to have a working typedef, use it as the pattern for your
		/// delegate declaration, which must have class cope.
		/// 
		/// Once you have your delegate declared, instantiate it by assigning it
		/// the value returned by calling this method on an instance that was
		/// constructed using the name of the DLL that exports the desired Win32
		/// (or Win64) function.
		/// 
		/// Since the Win64 API generally uses the same names, signatures, and
		/// DLL names (e. g., it's still Kernel32.dll, and GetProcAddress is
		/// still GetProcAddress), most of your existing Windows API calls work
		/// unchanged in Win64. The magic that makes this possible is that Win32
		/// code looks in %SystemRoot%\SysWOW64, while Win64 code looks in
		/// %SystemRoot%\System32 for system libraries. The clear goal of this
		/// initially confusing scheme was to make porting code to 64 bits as
		/// painless as possible.
		/// </remarks>
		public TDelegate GetUnmanagedFunction<TDelegate> ( string functionName ) where TDelegate : class
		{
			IntPtr p = NativeMethods.GetProcAddress ( m_hLibrary , functionName );

			// Failure is a common case, especially for adaptive code.

			if ( p == IntPtr.Zero )
			{
				return null;
			}	// if ( p == IntPtr.Zero )

			Delegate function = Marshal.GetDelegateForFunctionPointer ( p , typeof ( TDelegate ) );

			//	--------------------------------------------------------------------
			//	Ideally, we’d just make the constraint on TDelegate be 
			//	System.Delegate, but compiler error CS0702 (constrained can’t be
			//	System.Delegate) prevents that. So we make the constraint
			//	system.object and do the cast from object–>TDelegate.
			//	--------------------------------------------------------------------

			object o = function;

			return ( TDelegate ) o;
		}	// TDelegate

        #region IDisposable Members
        /// <summary>
        /// Call FreeLibrary on the unmanaged DLL. All function pointers
        /// handed out from this class become invalid after this.
        /// </summary>
        /// <remarks>
		/// This is very dangerous because it suddenly invalidate
        /// everything retrieved from this DLL. This includes any functions
        /// handed out via GetProcAddress, and potentially any objects returned
        /// from those functions (which may have an implementation in the DLL).
        /// </remarks>
		public void Dispose ( )
		{
			if ( !m_hLibrary.IsClosed )
			{
				m_hLibrary.Close ( );
			}	// if ( !m_hLibrary.IsClosed )
		}	// Dispose method

		//	--------------------------------------------------------------------
		//	Unmanaged resource. CLR will ensure SafeHandles get freed, without
		//	requiring a finalize method on this class.
		//	--------------------------------------------------------------------

        SafeLibraryHandle m_hLibrary;
        #endregion	// IDisposable Members
    } // UnmanagedLibrary
}	// partial namespace WizardWrx.Core