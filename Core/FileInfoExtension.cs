/*
    ============================================================================

    Namespace:          WizardWrx
 
    Class:              FileInfoExtension

	File Name:			FileInfoExtension.cs
 
    Synopsis            Extend the FileInfo class with additional methods that
						simplify common tasks that, while feasible, require some
						advanced coding techniques which are tricky to get right.
 
    Remarks:            Version 7.0 adds a set of extension methods to the
						the FileInfo class that are intended to replace the old
						FileInfoExtension class, which was a workaround intended
						to support version 2.0 of the .NET Framework, which did
						not support extension methods.

						1)	Since FileInfo is a sealed (not inheritable) class,
							the constructor does the next best thing, which is
							to create a FileInfo instance, for internal use, and
							expose it as a Read Only property.

						2)	Like its "parent" class, this one has one public
							constructor, which requires a path string.

						3)	Since querying the file attributes is rather obtuse,
							because they are a bit mask, version 2.66 adds a set
                            of read only properties that return Boolean values.

                            For obvious reasons, the Directory and ReparsePoint
                            attributes are omitted.

    References:         None.

    Author:             David A. Gray

    License:            Copyright (C) 2009-2017, David A. Gray. 
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
    2009/05/03         DAG    Initial version, in version 1.0.133.27105 of
                              wwBldNbrMgr.exe.

	2009/08/10 2.4     DAG    Add this class into WizardWrx.SharedUtl2, and
							  extend coverage to the Archive, Hidden, and System
							  attributes, so that the class covers all four of
                              the legacy MS-DOS file attributes, which remain
                              the most frequently used.

    2011/12/03 2.66    DAG    Add properties that return the current state of a
                              file attribute as a Boolean.

    2016/05/19 6.1     DAG    Move to WizardWrx.DllServices2 namespace and class
                              library, making its debut in version 6.1, keeping
                              its spot in the root of the WizardWrx hierarchy, 
                              so that existing code "just works" when bound to
                              this library. Relocation also brings this module
                              under my three-clause BSD license.

	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

							  In addition to being moved into another DLL, some
							  of its methods become extensions of the FileInfo
							  class by way of a static sibling class,
                              FileInfoExtensionMethods.
    ============================================================================
*/


using System;

/* Added by DAG */

using System.IO;


namespace WizardWrx
{
	/// <summary>
	/// Extend the System.IO.FileInfo class with methods for testing, setting,
	/// and clearing file attribute flags, including the ability to save and
	/// restore flags to their initial states.
	/// </summary>
	public class FileInfoExtension
	{
		/// <summary>
		/// Define the three possible initial states.
		/// </summary>
		public enum enmInitialStatus
		{
			/// <value >
			/// This is the initial state, before the flag is tested.
			/// </value>
			Unknown,

			/// <value>
			/// The flag was initially in the Cleared state.
			/// </value>
			WasCleared ,

			/// <value>
			/// The flag was initially in the Set state.
			/// </value>
			WasSet
		}   // enmInitialStatus


		#region Constructors
		private FileInfoExtension ( ) { }		// Hide the default constructor.


		/// <summary>
		/// We insist on a file name.
		/// </summary>
		/// <param name="pstrFileName">
		/// String containing the name of a file to process. This string must
		/// point to an existing file.
		/// </param>
		/// <remarks>
		/// Because of its intended use, this class is designed without a public
		/// default constructor. I believe that it is extremely unlikely that a
		/// user would need to serialize an instance of this class.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public FileInfoExtension ( string pstrFileName )
		{	//	Initialize the underlying FileInfo object. The remaining private
			//	storage, all scalars, initializes itself.
			_FileInfo = new FileInfo ( pstrFileName );
		}	// public FileInfoExtension constructor
		#endregion	// Constructors


		#region Public Methods for the Archive Attribute
		/// <summary>
		/// Clear the Archive flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Archive flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasArchive property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus ArchiveClear ( )
		{
			//	Save the initial state of the Archive flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.Archive ) == FileAttributes.Archive )
				_fWasArchive = enmInitialStatus.WasSet;
			else
				_fWasArchive = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes & ( ~FileAttributes.Archive );
			return _fWasArchive;
		}	// ArchiveClear method


		/// <summary>
		/// Restore the initial state of the Archive flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Archive flag.
		/// </returns>
		/// <remarks>
		/// Since the return value is strictly informational, callers may safely
		/// ignore or discard it.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus ArchiveReinstate ( )
		{
			//	----------------------------------------------------------------
			//	Test internal variable _fWasArchive to determine both the
			//	return value, and whether to set the Archive attribute.
			//
			//	Once the test has been completed, the internal variable
			//	is reset, and the appropriate value from the enmInitialStatus
			//	enumeration is returned.
			//	----------------------------------------------------------------

			if ( _fWasArchive == enmInitialStatus.WasSet )
			{	//	File was Archive when the ArchiveClear or ArchiveSet was last called.
				_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.Archive;
				_fWasArchive = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasSet;
			}
			else
			{	//	File was NOT Archive when the ArchiveClear or ArchiveSet waslast called.
				_fWasArchive = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasCleared;
			}
		}	// ArchiveReinstate method


		/// <summary>
		/// Set the Archive flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Archive flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasArchive property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus ArchiveSet ( )
		{
			//	Save the initial state of the Archive flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.Archive ) == FileAttributes.Archive )
				_fWasArchive = enmInitialStatus.WasSet;
			else
				_fWasArchive = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.Archive;
			return _fWasArchive;
		}	// ArchiveSet method
		#endregion	// Public Methods for the Archive Attribute


		#region Public Methods for the Hidden Attribute
		/// <summary>
		/// Clear the Hidden flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Hidden flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasHidden property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus HiddenClear ( )
		{
			//	Save the initial state of the Hidden flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden )
				_fWasHidden = enmInitialStatus.WasSet;
			else
				_fWasHidden = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes & ( ~FileAttributes.Hidden );
			return _fWasHidden;
		}	// HiddenClear method


		/// <summary>
		/// Restore the initial state of the Hidden flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Hidden flag.
		/// </returns>
		/// <remarks>
		/// Since the return value is strictly informational, callers may safely
		/// ignore or discard it.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus HiddenReinstate ( )
		{
			//	----------------------------------------------------------------
			//	Test internal variable _fWasHidden to determine both the
			//	return value, and whether to set the Hidden attribute.
			//
			//	Once the test has been completed, the internal variable
			//	is reset, and the appropriate value from the enmInitialStatus
			//	enumeration is returned.
			//	----------------------------------------------------------------

			if ( _fWasHidden == enmInitialStatus.WasSet )
			{	//	File was hidden when the HiddenClear or HiddenSet was last called.
				_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.Hidden;
				_fWasHidden = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasSet;
			}
			else
			{	//	File was NOT hidden when the HiddenClear or HiddenSet waslast called.
				_fWasHidden = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasCleared;
			}
		}	// HiddenReinstate method


		/// <summary>
		/// Set the Hidden flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Hidden flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasHidden property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus HiddenSet ( )
		{
			//	Save the initial state of the Hidden flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden )
				_fWasHidden = enmInitialStatus.WasSet;
			else
				_fWasHidden = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.Hidden;
			return _fWasHidden;
		}	// HiddenSet method
		#endregion	// Public Methods for the Hidden Attribute


		#region Public Methods for the ReadOnly Attribute
		/// <summary>
		/// Clear the Read ONly flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Read ONly flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasReadOnly property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus ReadOnlyClear ( )
		{
			//	Save the initial state of the Read Only flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
				_fWasReadOnly = enmInitialStatus.WasSet;
			else
				_fWasReadOnly = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes & ( ~FileAttributes.ReadOnly );
			return _fWasReadOnly;
		}	// ReadOnlyClear method


		/// <summary>
		/// Restore the initial state of the Read Only flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Read ONly flag.
		/// </returns>
		/// <remarks>
		/// Since the return value is strictly informational, callers may safely
		/// ignore or discard it.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus ReadOnlyReinstate ( )
		{
			//	----------------------------------------------------------------
			//	Test internal variable _fWasReadOnly to determine both the
			//	return value, and whether to set the ReadOnly attribute.
			//
			//	Once the test has been completed, the internal variable
			//	is reset, and the appropriate value from the enmInitialStatus
			//	enumeration is returned.
			//	----------------------------------------------------------------

			if ( _fWasReadOnly == enmInitialStatus.WasSet )
			{	//	File was RO when the ReadOnlyClear or ReadOnlySet was last called.
				_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.ReadOnly;
				_fWasReadOnly = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasSet;
			}
			else
			{	//	File was RW when the ReadOnlyClear or ReadOnlySet was last called.
				_fWasReadOnly = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasCleared;
			}
		}	// ReadOnlyReinstate method


		/// <summary>
		/// Set the Read Only flag.
		/// </summary>
		/// <returns>
		/// Previous state of the Read ONly flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasReadOnly property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus ReadOnlySet ( )
		{
			//	Save the initial state of the Read Only flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
				_fWasReadOnly = enmInitialStatus.WasSet;
			else
				_fWasReadOnly = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.ReadOnly;
			return _fWasReadOnly;
		}	// ReadOnlySet method
		#endregion	// Public Methods for the ReadOnly Attribute


		#region Public Methods for the System Attribute
		/// <summary>
		/// Clear the System flag.
		/// </summary>
		/// <returns>
		/// Previous state of the System flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasSystem property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus SystemClear ( )
		{
			//	Save the initial state of the System flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.System ) == FileAttributes.System )
				_fWasSystem = enmInitialStatus.WasSet;
			else
				_fWasSystem = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes & ( ~FileAttributes.System );
			return _fWasSystem;
		}	// SystemClear method


		/// <summary>
		/// Restore the initial state of the System flag.
		/// </summary>
		/// <returns>
		/// Previous state of the System flag.
		/// </returns>
		/// <remarks>
		/// Since the return value is strictly informational, callers may safely
		/// ignore or discard it.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus SystemReinstate ( )
		{
			//	----------------------------------------------------------------
			//	Test internal variable _fWasSystem to determine both the
			//	return value, and whether to set the System attribute.
			//
			//	Once the test has been completed, the internal variable
			//	is reset, and the appropriate value from the enmInitialStatus
			//	enumeration is returned.
			//	----------------------------------------------------------------

			if ( _fWasSystem == enmInitialStatus.WasSet )
			{	//	File was System when the SystemClear or SystemSet was last called.
				_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.System;
				_fWasSystem = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasSet;
			}
			else
			{	//	File was NOT System when the SystemClear or SystemSet waslast called.
				_fWasSystem = enmInitialStatus.Unknown;	// Revert to "Unknown" state.
				return enmInitialStatus.WasCleared;
			}
		}	// SystemReinstate method


		/// <summary>
		/// Set the System flag.
		/// </summary>
		/// <returns>
		/// Previous state of the System flag.
		/// </returns>
		/// <remarks>
		/// Since the initial state of the flag is preserved internally by the
		/// class, and can be read from the WasSystem property, callers may
		/// safely ignore or discard the return value.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed." )]
		public enmInitialStatus SystemSet ( )
		{
			//	Save the initial state of the System flag.
			//
			//	I intentionally chose the concise Block IF construct, because
			//	it never needs more than a single statement in each branch.

			if ( ( _FileInfo.Attributes & FileAttributes.System ) == FileAttributes.System )
				_fWasSystem = enmInitialStatus.WasSet;
			else
				_fWasSystem = enmInitialStatus.WasCleared;

			//	The next statement is the tricky bit that inspired me to create
			//	this class.

			_FileInfo.Attributes = _FileInfo.Attributes | FileAttributes.System;
			return _fWasSystem;
		}	// SystemSet method
		#endregion	// Public Methods for the System Attribute


		#region Public Properties
		/// <summary>
		/// Return the underlying FileInfo object, which is initialized by the
		/// constructor.
		/// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public FileInfo TheFile { get { return _FileInfo; } }


        /// <summary>
        /// This property returns the current state of the Archive attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool Archive { get { return ( _FileInfo.Attributes & FileAttributes.Archive ) == FileAttributes.Archive; } }


        /// <summary>
        /// This property returns the current state of the Compressed attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool Compressed { get { return ( _FileInfo.Attributes & FileAttributes.Compressed ) == FileAttributes.Compressed; } }


        /// <summary>
        /// This property returns the current state of the Encrypted attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool Encrypted { get { return ( _FileInfo.Attributes & FileAttributes.Encrypted ) == FileAttributes.Encrypted; } }


        /// <summary>
        /// This property returns the current state of the Hidden attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool Hidden { get { return ( _FileInfo.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden; } }


        /// <summary>
        /// This property returns the current state of the NotContentIndexed attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool NotContentIndexed { get { return ( _FileInfo.Attributes & FileAttributes.NotContentIndexed ) == FileAttributes.NotContentIndexed; } }


        /// <summary>
        /// This property returns the current state of the Offline attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool Offline { get { return ( _FileInfo.Attributes & FileAttributes.Offline ) == FileAttributes.Offline; } }


        /// <summary>
        /// This property returns the current state of the ReadOnly attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool ReadOnly { get { return ( _FileInfo.Attributes & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly; } }


        /// <summary>
        /// This property returns the current state of the System attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool System { get { return ( _FileInfo.Attributes & FileAttributes.System ) == FileAttributes.System; } }


        /// <summary>
        /// This property returns the current state of the Temporary attribute.
        /// </summary>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public bool Temporary { get { return ( _FileInfo.Attributes & FileAttributes.Temporary ) == FileAttributes.Temporary; } }


		/// <summary>
		/// Return the initial state of the Archive flag.
		/// </summary>
		/// <remarks>
		/// The value of this property is meaningless after the
		/// ArchiveReinstate method is called, and before either ArchiveClear
		/// or ArchiveSet has been called on an instance.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public enmInitialStatus WasArchive { get { return _fWasArchive; } }


		/// <summary>
		/// Return the initial state of the ReadOnly flag.
		/// </summary>
		/// <remarks>
		/// The value of this property is meaningless after the
		/// ReadOnlyReinstate method is called, and before either ReadOnlyClear
		/// or ReadOnlySet has been called on an instance.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public enmInitialStatus WasReadOnly { get { return _fWasReadOnly; } }


		/// <summary>
		/// Return the initial state of the Hidden flag.
		/// </summary>
		/// <remarks>
		/// The value of this property is meaningless after the
		/// HiddenReinstate method is called, and before either HiddenClear
		/// or HiddenSet has been called on an instance.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public enmInitialStatus WasHidden { get { return _fWasHidden; } }


		/// <summary>
		/// Return the initial state of the System flag.
		/// </summary>
		/// <remarks>
		/// The value of this property is meaningless after the
		/// SystemReinstate method is called, and before either SystemClear
		/// or SystemSet has been called on an instance.
		/// </remarks>
		[Obsolete ( "The FileInfoExtension instance methods that modify FileAttributes are being retired in favor of the extension methods on FileInfo declared herein. These methods will eventually be removed, as will the instance properties and storage." )]
		public enmInitialStatus WasSystem { get { return _fWasSystem; } }
		#endregion	// Public Properties


		#region Private Storage for Class Instance Properties
		private FileInfo _FileInfo;		// This is the underlying object.

		private enmInitialStatus _fWasArchive = enmInitialStatus.Unknown;
		private enmInitialStatus _fWasHidden = enmInitialStatus.Unknown;
		private enmInitialStatus _fWasReadOnly = enmInitialStatus.Unknown;
		private enmInitialStatus _fWasSystem = enmInitialStatus.Unknown;
		#endregion	// Private Storage for Class Instance Properties
	}	// public class FileInfoExtension
}	// partial namespace WizardWrx