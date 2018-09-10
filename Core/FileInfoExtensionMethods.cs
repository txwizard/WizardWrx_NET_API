/*
    ============================================================================

    Namespace:          WizardWrx
 
    Class:              FileInfoExtensionMethods

	File Name:			FileInfoExtensionMethods.cs
 
    Synopsis            Extend the FileInfo class with additional methods that
						simplify common tasks that, while feasible, require some
						advanced coding techniques which are tricky to get right.
 
    Remarks:            Version 7.0 adds a set of extension methods to the
						the FileInfo class that are intended to replace the old
						FileInfoExtension class, which was a workaround intended
						to support version 2.0 of the .NET Framework, which did
						not support extension methods.

						Since querying the file attributes is rather obtuse,
						because they are a bit mask, version 2.66 adds a set of
                        read only properties that return Boolean values.

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
	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

	2018/09/06 7.0     DAG    Add this headnote, which I discovered was missing
	                          when I rewrote the ReadMe file.
    ============================================================================
*/

using System.IO;

namespace WizardWrx
{
	/// <summary>
	/// The new extension methods that supersede the instance methods on the
	/// companion FileInfoExtension class go into a dedicated class, because a
	/// static class must expose them.
	/// </summary>
	public static class FileInfoExtensionMethods
	{
		#region Extension Methods for the Archive Attribute
		/// <summary>
		/// Clear the Archive FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the Archive attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeArchiveClear ( this FileInfo pfi )
		{ return FileAttributeClear ( pfi , FileAttributes.Archive ); }


		/// <summary>
		/// Reinstate the Archive flag on the specified file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <param name="penmInitialStatus">
		/// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
		/// FileAttributeArchiveClear.
		/// </param>
		/// <returns>
		/// If the Archive attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeArchiveReinstate (
			this FileInfo pfi ,
			FileInfoExtension.enmInitialStatus penmInitialStatus )
		{
			if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
			{
				return FileInfoExtension.enmInitialStatus.WasSet;
			}	// TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
			else
			{
				return FileAttributeSet ( pfi , FileAttributes.Archive );
			}	// FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
		}	// FileAttributeArchiveReinstate


		/// <summary>
		/// Set the Archive FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the Archive attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeArchiveSet ( this FileInfo pfi )
		{ return FileAttributeSet ( pfi , FileAttributes.Archive ); }
		#endregion	// Extension Methods for the Archive Attribute


		#region Extension Methods for the Hidden Attribute
		/// <summary>
		/// Clear the Hidden FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the Hidden attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeHiddenClear ( this FileInfo pfi )
		{ return FileAttributeClear ( pfi , FileAttributes.Hidden ); }


		/// <summary>
		/// Reinstate the Hidden flag on the specified file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <param name="penmInitialStatus">
		/// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
		/// FileAttributeArchiveClear.
		/// </param>
		/// <returns>
		/// If the Hidden attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeHiddeneReinstate (
			this FileInfo pfi ,
			FileInfoExtension.enmInitialStatus penmInitialStatus )
		{
			if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
			{
				return FileInfoExtension.enmInitialStatus.WasSet;
			}	// TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
			else
			{
				return FileAttributeSet ( pfi , FileAttributes.Hidden );
			}	// FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
		}	// FileAttributeHiddeneReinstate


		/// <summary>
		/// Set the Hidden FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the Hidden attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeHiddenSet ( this FileInfo pfi )
		{ return FileAttributeSet ( pfi , FileAttributes.Hidden ); }
		#endregion	// Extension Methods for the Hidden Attribute


		#region Extension Methods for the ReadOnly Attribute
		/// <summary>
		/// Clear the ReadOnly FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the ReadOnly attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeReadOnlyClear ( this FileInfo pfi )
		{ return FileAttributeClear ( pfi , FileAttributes.ReadOnly ); }


		/// <summary>
		/// Reinstate the ReadOnly flag on the specified file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <param name="penmInitialStatus">
		/// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
		/// FileAttributeArchiveClear.
		/// </param>
		/// <returns>
		/// If the ReadOnly attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeReadOnlyReinstate (
			this FileInfo pfi ,
			FileInfoExtension.enmInitialStatus penmInitialStatus )
		{
			if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
			{
				return FileInfoExtension.enmInitialStatus.WasSet;
			}	// TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
			else
			{
				return FileAttributeSet ( pfi , FileAttributes.ReadOnly );
			}	// FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
		}	// FileAttributeReadOnlyReinstate


		/// <summary>
		/// Set the ReadOnly FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the ReadOnly attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeReadOnlySet ( this FileInfo pfi )
		{ return FileAttributeSet ( pfi , FileAttributes.ReadOnly ); }
		#endregion	// Extension Methods for the ReadOnly Attribute


		#region Extension Methods for the System Attribute
		/// <summary>
		/// Clear the System FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the specified bit (It's usually just one.) is set on entry, the
		/// return value is WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeSystemClear ( this FileInfo pfi )
		{ return FileAttributeClear ( pfi , FileAttributes.System ); }


		/// <summary>
		/// Reinstate the System flag on the specified file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <param name="penmInitialStatus">
		/// Specify the FileInfoExtension.enmInitialStatus value returned by the last call to 
		/// FileAttributeArchiveClear.
		/// </param>
		/// <returns>
		/// If the System attribute is set on entry, the return value is
		/// WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeSystemReinstate (
			this FileInfo pfi ,
			FileInfoExtension.enmInitialStatus penmInitialStatus )
		{
			if ( penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet )
			{
				return FileInfoExtension.enmInitialStatus.WasSet;
			}	// TRUE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
			else
			{
				return FileAttributeSet ( pfi , FileAttributes.System );
			}	// FALSE block, if (penmInitialStatus == FileInfoExtension.enmInitialStatus.WasSet)
		}	// FileAttributeSystemReinstate


		/// <summary>
		/// Set the System FileAttributes on a file.
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <returns>
		/// If the specified bit (It's usually just one.) is set on entry, the
		/// return value is WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeSystemSet ( this FileInfo pfi )
		{ return FileAttributeSet ( pfi , FileAttributes.System ); }
		#endregion	// Extension Methods for the System Attribute


		#region Extension Methods for Any File Attribute
		/// <summary>
		/// Clear the specified FileAttributes bit(s).
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <param name="penmFileAttributes">
		/// This argument specifies the member(s) of the FileAttributes bitwise
		/// enumeration to clear.
		/// </param>
		/// <returns>
		/// If the specified bit (It's usually just one.) is set on entry, the
		/// return value is WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeClear (
			this FileInfo pfi ,
			FileAttributes penmFileAttributes )
		{
			if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
			{	// The specified attribute is set; clear it, and report that it was set.
				pfi.Attributes = pfi.Attributes & ( ~penmFileAttributes );
				return FileInfoExtension.enmInitialStatus.WasSet;
			}	// TRUE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
			else
			{	// The specified attribute is cleared; return without taking any action beyond reporting.
				return FileInfoExtension.enmInitialStatus.WasCleared;
			}	// FALSE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
		}	// FileAttributeClear


		/// <summary>
		/// Set the specified FileAttributes bit(s).
		/// </summary>
		/// <param name="pfi">
		/// As an extension method, the first argument must be a reference to
		/// the instance that invoked it, which is used in much the same way as
		/// the implicit this parameter that leads the list of parameters for a
		/// call to any C=+ instance method.
		/// </param>
		/// <param name="penmFileAttributes">
		/// This argument specifies the member(s) of the FileAttributes bitwise
		/// enumeration to clear.
		/// </param>
		/// <returns>
		/// If the specified bit (It's usually just one.) is set on entry, the
		/// return value is WasSet; otherwise, it is WasCleared.
		/// </returns>
		public static FileInfoExtension.enmInitialStatus FileAttributeSet (
			this FileInfo pfi ,
			FileAttributes penmFileAttributes )
		{
			if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
			{	// The specified bit(s) are set; return without taking any action.
				return FileInfoExtension.enmInitialStatus.WasSet;
			}	// TRUE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
			else
			{	// At least one of the specified bits is cleared; set it, then return.
				pfi.Attributes = pfi.Attributes | penmFileAttributes;
				return FileInfoExtension.enmInitialStatus.WasCleared;
			}	// FALSE block, if ( ( pfi.Attributes & penmFileAttributes ) == penmFileAttributes )
		}	// FileInfoExtension.enmInitialStatus
		#endregion	// Extension Methods for Any File Attribute
	}   // public static class FileInfoExtensionMethods
}   // partial namespace WizardWrx