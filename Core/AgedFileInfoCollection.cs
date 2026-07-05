/*
    ============================================================================

    Module Name:        AgedFileInfo.cs

    Namespace Name:     WizardWrx.Core

    Class Name:         AgedFileInfo

    Synopsis:           This class module implements the AgedFileInfo class, 
						which facilitates sorting of file names by last modified
						date.

    Remarks:            I designed and tested this class as part of my
						CountDownClock general purpose Windows application, and
						exercised it with the source directory empty and filled
						with files. The case that has yet to be tested is the
						other degenerate case, the source directory being absent
						or inaccessible.

    Author:             David A. Gray

    Date Begun:			Wednesday, 28 June 2017

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Synopsis
    ---------- ------- --- -------------------------------------------------
	2017/07/02 4.0     DAG This class makes its first appearance.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.IO;

using WizardWrx;


namespace WizardWrx.Core
{
	/// <summary>
	/// This class hides its data in a List of AgedFileInfo objects, which its
	/// constructors assemble from permutations of the array of FileInfo objects
	/// returned by the GetFiles method on the DirectoryInfo instance that is
	/// fed into it.
	/// </summary>
	/// <remarks>
	/// This class and AgedFileInfo are designed to be used together to
	/// achieve the goal of processing a list of the files that match a file
	/// specification from newest to oldest.
	/// 
	/// Each AgedFileInfo is a FileInfo object, wrapped in a small class that
	/// augments it with a custom IComparable interface implementation that
	/// allows the list to be sorted by LastWriteTimeUTC. Using LastWriteTimeUTC
	/// guarantees that the files are always sorted correctly, even when the set
	/// includes one or more files that were created during the transition hour
	/// that occurs twice annually, on the days when the transitions to and from
	/// Daylight Saving Time occur. LastWriteTimeUTC time stamps created during
	/// this hour are not guaranteed to sort correctly, especially during the
	/// "fall back" transition that occurs in Autumn, when the local time is set
	/// back by 1 hour, so that there are two hours during which the same local
	/// hour is reported.
	/// </remarks>
	public class AgedFileInfoCollection
	{
		#region Constructors, All Public, Except the Pointless Default Constructor
		/// <summary>
		/// To ensure that a populated collection is always created, the default
		/// constructor is hidden.
		/// </summary>
		/// <remarks>
		/// Apart from the constructors, this class is implemented entirely by
		/// its base class.
		/// 
		/// This class and AgedFileInfo are designed to be used together to
		/// achieve the goal of processing a list of the files that match a file
		/// specification from newest to oldest.
		/// </remarks>
		/// <see cref="AgedFileInfo"/>
		private AgedFileInfoCollection ( )
		{
		}	// private AgedFileInfoCollection is the default constructor, which creates the empty collection.


		/// <summary>
		/// The simplest of the three public constructors takes a reference to a
		/// DirectoryInfo collection, which is loaded in its entirety.
		/// </summary>
		/// <param name="pdirInfoCollection">
		/// The DirectoryInfo collection from which to populate the instance
		/// </param>
		public AgedFileInfoCollection (
			DirectoryInfo pdirInfoCollection )
		{
			InitializeAndSortList ( pdirInfoCollection.GetFiles ( ) );
		}	// public AgedFileInfoCollection loads the FileInfo objects returned by a DirectoryInfo collection into the collection for sorting and searching.



		/// <summary>
		/// A slightly more advanced constructor restricts the files to those
		/// that match a standard file name mask.
		/// </summary>
		/// <param name="pdirInfoCollection">
		/// The DirectoryInfo collection from which to populate the instance
		/// </param>
		/// <param name="pstrFileSpec">
		/// The string to employ as a search mask
		/// </param>
		public AgedFileInfoCollection (
			DirectoryInfo pdirInfoCollection ,
			string pstrFileSpec )
		{
			InitializeAndSortList ( pdirInfoCollection.GetFiles ( pstrFileSpec ) );
		}	// public AgedFileInfoCollection loads the FileInfo objects returned by a DirectoryInfo collection into the collection for sorting and searching.


		/// <summary>
		/// The most advanced constructor includes the restriction to a file name mask, to which it adds a SearchOption enumeration member that specifies whether to apply the mask to subdirectories.
		/// </summary>
		/// <param name="pdirInfoCollection">
		/// The DirectoryInfo collection from which to populate the instance
		/// </param>
		/// <param name="pstrFileSpec">
		/// The string to employ as a search mask
		/// </param>
		/// <param name="penmSearchOption">
		/// The SearchOption that determines whether subdirectories are searched or ignored
		/// </param>
		public AgedFileInfoCollection (
			DirectoryInfo pdirInfoCollection ,
			string pstrFileSpec ,
			SearchOption  penmSearchOption )
		{
			InitializeAndSortList ( pdirInfoCollection.GetFiles ( pstrFileSpec , penmSearchOption ) );
		}	// public AgedFileInfoCollection loads the FileInfo objects returned by a DirectoryInfo collection into the collection for sorting and searching.
		#endregion	// Constructors, All Public, Except the Pointless Default Constructor


		#region Public Instance Properties and Methods
		/// <summary>
		/// Read the count off the private list.
		/// </summary>
		public int Count
		{
			get
			{
				return _lstMatchingFiles != null
					? _lstMatchingFiles.Count :
					ListInfo.LIST_IS_EMPTY;
			}	// The public Count property is read only, and returns zero when the list is uninitialized or empty.
		}	// The public Count property is named after the underlying property of the generic List that it reports.


		/// <summary>
		/// Unless the list is empty, return the newest file in it, which is the
		/// first file in the list, since it is reverse sorted by LastWriteTime.
		/// </summary>
		/// <returns>
		/// The returned FileInfo object represents the newest file in the list,
		/// which occupies its first slot, since the list is sorted in reverse
		/// order by LastWriteTimeUTC.
		/// </returns>
		public FileInfo GetFirstFileInfo ( )
		{
			if ( _lstMatchingFiles.Count > ListInfo.LIST_IS_EMPTY )
			{	// Unless the list is empty, there must be a "first" item to return.
				return _lstMatchingFiles [ ArrayInfo.ARRAY_FIRST_ELEMENT ].Details;
			}	// TRUE (anticipated outcome) block, if ( _lstMatchingFiles.Count > ListInfo.LIST_IS_EMPTY )
			else
			{	// Since FileInfo is a nullable type, the degenerate case returns a null reference.
				return null;
			}	// FALSE (unanticipated outcome) block, if ( _lstMatchingFiles.Count > ListInfo.LIST_IS_EMPTY )
		}	// public FileInfo GetFirstFileInfo


		/// <summary>
		/// Unless the list is empty, return the oldest file in it, which is the
		/// last file in the list, since it is reverse sorted by LastWriteTime.
		/// </summary>
		/// <returns>
		/// The returned FileInfo object represents the oldest file in the list,
		/// which occupies its last slot, since the list is sorted in reverse
		/// order by LastWriteTimeUTC.
		/// </returns>
		public FileInfo GetLastFileInfo ( )
		{
			if ( _lstMatchingFiles.Count > ListInfo.LIST_IS_EMPTY )
			{	// Unless the list is empty, there must be a "first" item to return.
				return _lstMatchingFiles [ ArrayInfo.OrdinalFromIndex ( _lstMatchingFiles.Count ) ].Details;
			}	// TRUE (anticipated outcome) block, if ( _lstMatchingFiles.Count > ListInfo.LIST_IS_EMPTY )
			else
			{	// Since FileInfo is a nullable type, the degenerate case returns a null reference.
				return null;
			}	// FALSE (unanticipated outcome) block, if ( _lstMatchingFiles.Count > ListInfo.LIST_IS_EMPTY )
		}	// public FileInfo GetLastFileInfo
		#endregion	// Public Instance Properties and Methods


		#region Private Code and Data
		/// <summary>
		/// Once the array of matching files is filled, all three public constructors do the same thing with it.
		/// </summary>
		/// <param name="pafiMatchingFiles">
		/// Since the array is redundant once it has been loaded into the list, it has method scope.
		/// </param>
		private void InitializeAndSortList ( FileInfo [ ] pafiMatchingFiles )
		{
			_lstMatchingFiles = new List<AgedFileInfo> ( pafiMatchingFiles.Length );

			foreach ( FileInfo file in pafiMatchingFiles )
			{	// Since the collection of FileInfo objects returned by DirectoryInfo.GetFiles must be wrapped in an AgedFileInfo object, the bulk loader can't be used.
				_lstMatchingFiles.Add ( new AgedFileInfo ( file ) );
			}	// foreach ( FileInfo file in pafiMatchingFiles )

			_lstMatchingFiles.Sort ( );
		}	// private void InitializeAndSortLis


		List<AgedFileInfo> _lstMatchingFiles = null;
		#endregion	// Private Code and Data
	}	// public class AgedFileInfoCollection : List<AgedFileInfo>
}	// partial namespace WizardWrx.Core