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

	Reference:			Convert a positive number to negative in C#
						https://stackoverflow.com/questions/1348080/convert-a-positive-number-to-negative-in-c-sharp

    Author:             David A. Gray

    Date Begun:			Wednesday, 28 June 2017

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Synopsis
    ---------- ------- --- -------------------------------------------------
	2017/07/02 4.0     DAG This class makes its first appearance.
  	2017/07/12 7.0     DAG Override the ToString method to display the key
                           properties.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.IO;

using WizardWrx;


namespace WizardWrx.Core
{
	/// <summary>
	/// This class extends a FileInfo object with a custom IComparable interface
	/// implementation.
	/// </summary>
	/// <remarks>
	/// This class and AgedFileInfoCollection are designed to be used together 
	/// to achieve the goal of processing a list of the files that match a file
	/// specification from newest to oldest.
	/// 
	/// These objects are not intended for independent use, since they must go
	/// into a collection that can be sorted to leverage the IComparable
	/// interface implementation that permits them to be sorted in reverse order
	/// by LastWriteTimeUTC.
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
	/// 
	/// In the course of investigating an unexpected exception, I again read the
	/// recommendation from Microsoft (published in the MSDN library) about
	/// overriding other methods (Equals and GetHashCode), implementing other
	/// interfaces (IEquatable and IEqualityComparer), and overloading the
	/// equality and inequality operators, all of which I went ahead and did,
	/// even though doing so is almost certainly overkill for this class.
	/// </remarks>
	/// <see cref="AgedFileInfoCollection"/>
	public class AgedFileInfo : IComparable<AgedFileInfo>
	{
		#region Public Members
		/// <summary>
		/// The default constructor would be private, except that I believe that
		/// it must be marked as Public to satisfy the IComparable and IList
		/// interfaces.
		/// </summary>
		public AgedFileInfo ( )
		{
		}	// The default constructor creates a null instance that is pretty much useless, apart from satisfying a requirement of the IList interface.

		/// <summary>
		/// The single overloaded constructor stashes a reference to a FileInfo,
		/// which this class augments with a custom implementation of the
		/// IComparable interface that supports inverse ordering.
		/// </summary>
		/// <param name="pfileInfo">
		/// Pass in a reference to the FileInfo to be stored in the instance.
		/// </param>
		public AgedFileInfo ( FileInfo pfileInfo )
		{
			_fileInfo = pfileInfo;
		}	// The real constructor takes a reference to FileInfo instance, which it tucks away in a private member, so that collections of instances can be reverse sorted.

		/// <summary>
		/// Return a reference to the FileInfo that was loaded into the
		/// constructor.
		/// </summary>
		/// <remarks>
		/// While this protects the reference against destruction, it doesn't
		/// prevent callers from changing read/write properties. For that, each
		/// property must be individually exposed as a read-only property.
		/// </remarks>
		public FileInfo Details
		{
			get
			{
				return _fileInfo;
			}	// public FileInfo Details property get method protects the data against accidental destruction.
		}	// public FileInfo Details is a read-only property that returns a reference to the FileInfo that was fed into the constructor.
		#endregion	// Public Members


		#region Private Members
		FileInfo _fileInfo = null;
		#endregion	// Private Members


		#region Overridden Methods of Base Class (Object) - All of them
		/// <summary>
		/// Override the base class ToString method, so that it renders the most
		/// critical properties of the underlying file.
		/// </summary>
		/// <returns>
		/// The returned string lists the name of the exporting class, as usual,
		/// but followed immediately by the key properties that a debugger will
		/// most likely need.
		/// </returns>
		public override string ToString ( )
		{
			return string.Format (
				Properties.Resources.AGED_FILES_INFO_TOSTRING_TEMPLATE ,
				new string [ ]
				{
					this.GetType ( ).FullName,						// Format Item 0 = Fully qualified class name
					_fileInfo.Name ,								// Format Item 1 = Base Name        = {1},
					SysDateFormatters.FormatDateTimeForShow (		// Format Item 2 = LastWriteTimeUTC = {2}
						_fileInfo.LastAccessTimeUtc ) ,					// LastAccessTimeUtc is a System.DateTime structure, which FormatDateTimeForShow formats for display to carbon units.
					NumberFormatters.Integer (						// Format Item 3 = ({3 Ticks}),LastWriteTime
						_fileInfo.LastAccessTimeUtc.Ticks ) ,			// LastAccessTimeUtc.Ticks is a long (64 bit) integer, which counts upwards from 01/01/0001 at Midnight UTC.
					SysDateFormatters.FormatDateTimeForShow (		// Format Item 4 = LastWriteTime    = {4}
						_fileInfo.LastAccessTime ) ,					// LastAccessTime, which reports the LastWriteTime, expressed as Local time is a System.DateTime structure, which FormatDateTimeForShow formats for display to carbon units.
					NumberFormatters.Integer (						// Format Item 5 = ({5 Ticks})
						_fileInfo.LastAccessTime.Ticks )				// LastAccessTime.Ticks is a long (64 bit) integer, which counts upwards from 01/01/0001 at Midnight local time,
				} );
		}	// public override string ToString ( )


		/// <summary>
		/// Override the base imp0lementation of the Equals method to return a
		/// result that is consistent with the algorithm implemented by the
		/// IComparable interface.
		/// </summary>
		/// <param name="obj">
		/// The object against which to compare must be another AgedFileInfo, or
		/// the return value is False.
		/// </param>
		/// <returns>
		/// The returned Boolean value is that returned by comparing the two
		/// _fileInfo.LastAccessTimeUtc.Ticks property values.
		/// </returns>
		public override bool Equals ( object obj )
		{	// I remember reading years ago that if you implement IComparable, you should also override the Equals operator.
			if ( obj.GetType ( ) == this.GetType ( ) )
			{	// Cast it to type, so that we can pick it apart.
				AgedFileInfo afiTheOther = ( AgedFileInfo ) obj;
				return _fileInfo.LastAccessTimeUtc.Ticks.Equals ( afiTheOther._fileInfo.LastAccessTimeUtc.Ticks );
			}	// TRUE (The anticipated use case is a valid comparand.) block, if ( obj.GetType ( ) == this.GetType ( ) )
			else
			{	// Objects of different types are inherently unequal.
				return false;
			}	// FALSE (The unanticipated use case is an invalid comparand.) block, if ( obj.GetType ( ) == this.GetType ( ) )
		}	// public override bool Equals

		
		/// <summary>
		/// Override the base GetHashCode method to return a result that is
		/// consistent with the algorithm implemented by the IComparable
		/// interface.
		/// </summary>
		/// <returns>
		/// The return value is the HashCode of the LastWriteTimeUTC.Ticks
		/// property of the underlying FileInfo object.
		/// </returns>
		public override int GetHashCode ( )
		{
			return _fileInfo.LastAccessTimeUtc.Ticks.GetHashCode ( );
		}	// public override int GetHashCode
		#endregion	// Overridden Methods of Base Class (Object)


		#region IComparable<AgedFileInfo> Members
		int IComparable<AgedFileInfo>.CompareTo ( AgedFileInfo other )
		{	// The unary minus operator reverses the sort order. The parentheses
			// are syntactic sugar that makes the unary minus stand out. See Joe
			// White's answer to the Stack Overflow question 
			// at https://stackoverflow.com/questions/1348080/convert-a-positive-number-to-negative-in-c-sharp.

			if ( other != null )
			{
				return -( this._fileInfo.LastWriteTimeUtc.Ticks.CompareTo ( other._fileInfo.LastWriteTimeUtc.Ticks ) );
			}	// TRUE (The comparand is a valid reference.) block, if ( other != null )
			else
			{
				return -( this._fileInfo.LastWriteTimeUtc.Ticks.CompareTo ( DateTime.MinValue.Ticks ) );
			}	// FALSE (The comparand is a null reference.) block, if ( other != null )
		}	// int IComparable<DateTime>.CompareTo
		#endregion	// IComparable<DateTime> Members
	}	// public class AgedFileInfo
}	// partial namespace WizardWrx.Core