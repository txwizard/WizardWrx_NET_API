/*
    ============================================================================

    Namespace:          WizardWrx.FormatStringEngine

    Class Name:         FormatItem

    File Name:          FormatItem.cs

    Synopsis:           Members of this class represent individual Format Items
                        found by the FormatStringParser in its format string.

    Remarks:            In addition to the three parts of a format item, this
                        class maintains an index, which is managed by a List<T>,
                        which is created and maintained by the FormatStringParser
                        class.

    License:            Copyright (C) 2014-2017, David A. Gray.
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

    Created:            Monday, 14 July 2014 - Saturday, 19 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2014/07/19 1.0     DAG Initial implementation.

    2014/08/13 1.1     DAG Add static method AdjustItemNumberWidthPerMaxValue
                           with four overloads taking combinations of signed and
                           unsigned integer values.

    2014/09/23 1.2     DAG 1) Align the behavior of AdjustToMaximumWidth (ALL 4
                              overloads) with the syntax of a FormatItem, in
                              which the formatString is optional.

                           2) Add two new overloads of UpgradeFormatItem, to
                              cover use cases identified while developing the
                              proofs of the improved AdjustToMaximumWidth
                              routines.

    2014/09/26 1.3     DAG Add a UpgradeFormatItem method that takes a generic
                           list for pastrValueArray.

    2016/06/12 3.0     DAG Break the dependency on WizardWrx.SharedUtl2.dll,
                           correct misspelled words flagged by the spelling
                           checker add-in, and incorporate my three-clause BSD
                           license.
 
	2017/03/05 3.1     DAG Add the AdjustItemNumberWidthPerMaxValue overload
                           that I created in ProductVersionSetter.exe, in
                           which it was proven before I moved it into this
                           class, then discovered an existing overload that is
                           actually better suited to its use therein, though I
                           shall keep the new overload, at least for now.

	2017/08/28 7.0     DAG Relocated to the constellation of core libraries that
                           began as WizardWrx.DllServices2.dll.

	2017/10/22 7.0     DAG 1) Correct the regression error, exposed by the lists
                              generated by EnumEnvironmentStrings.

                           2) Simplify the first 3 of the 4 AdjustToMaximumWidth
                              overloads so that the fourth overload does the
                              work, since the only difference among the four is
                              the types of the first two arguments, all of which
                              can be cast to signed integer.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

using WizardWrx;


namespace WizardWrx.FormatStringEngine
{
	/// <summary>
	/// This is an infrastructure class; instances represent a FormatItem found
	/// in the FormatString associated with the FormatStringParser that owns it.
	///
	/// The static methods of this class are public, and are very handy for
	/// creating compact summary reports.
	/// 
	/// Use them to construct composite format items that have their
	/// characteristics, especially their widths, established at runtime.
	/// Setting the width dynamically permits summary totals and their labels,
	/// all of which may have widely varying widths that are unknown in advance
	/// to be set in such a way that a set of numeric values and their labels
	/// can be printed with labels on the left, and a minimum amount of white
	/// space between the longest label and the numbers.
	/// </summary>
	/// <remarks>
	/// While the class, itself, is marked Public, everything else about it is
	/// marked as Internal, so that only instances of classes defined in this
	/// assembly can create instances, while instances of the FormatStringParser
	/// class expose a collection of them through its FormatItems property, a
	/// FormatItemsCollection.
	/// </remarks>
	public class FormatItem
	{
		#region Public Enumerations and Constants
		/// <summary>
		/// If specified, the Width property supports two alignments, Left and
		/// Right.
		/// </summary>
		public enum Alignment
		{
			/// <summary>
			/// Unless the item has an Alignment, this property is unspecified,
			/// and algnment is moot, since no extra characters are reserved for
			/// padding.
			/// </summary>
			Unspecified ,

			/// <summary>
			/// Left align the text within the alloted width.
			/// </summary>
			Left ,

			/// <summary>
			/// Right align the text within the alloted width.
			///
			/// If the item has an Alignment, its default is right.
			/// </summary>
			Right
		}   // Alignment


		/// <summary>
		/// The ApperanceOrder property evaluating to this value means that the
		/// property is uninitialized.
		/// </summary>
		public const uint APPEARANCE_ORDER_NOT_SET = 0;


		/// <summary>
		/// Make the lower bound of an index visible to class consumers.
		/// </summary>
		public const int INVALID_INDEX = -1;


		/// <summary>
		/// Make the lower bound of a string offset visible to class consumers.
		/// </summary>
		public const int INVALID_OFFSET = -1;


		/// <summary>
		/// Make the lower bound of the RawLength property visible to class
		/// consumers.
		/// </summary>
		public const int INVALID_RAW_LENGTH = 0;


		/// <summary>
		/// Make the lower bound of the MinimumWidth property visible to class
		/// consumers.
		/// </summary>
		public const int INVALID_WIDTH = 0;


		/// <summary>
		/// Like all good array subscripts, format item indexes start at zero.
		/// </summary>
		public const int MINIMUM_VALID_FORMAT_ITEM_INDEX = 0;


		/// <summary>
		/// Like all good array subscripts, format item indexes start at zero.
		/// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <see cref="WizardWrx.NumericFormats.NUMBER_PER_REG_SETTINGS"/>
		public const string INTEGER_PER_REG_SETTINGS = NumericFormats.NUMBER_PER_REG_SETTINGS_0D;
		#endregion  // Public Enumerations and Constants


		#region Private Constants
		const string ARGNAME_ALIGNMENT = @"penmAlignment";
		const string ARGNAME_FORMAT = @"pstrFormat";
		const string ARGNAME_STRUPGRADE = @"pstrUpgrade";

		const int FLIP_SIGN = -1;               // Sign inversion = multiplication by minus one

		const char DLM_ITEM_BEGIN = '{';        // Left French Brace begins a Format Item.
		const char DLM_ITEM_FORMAT = ':';       // Colon separates the alignment token from the format string.
		const char DLM_ITEM_ALIGNMENT = ',';    // Comma separates the index token from the alignment token.
		const char DLM_ITEM_END = '}';          // Right French Brace terminates a Format Item.

		const int LENGTH_IF_EMPTY = 0;

		const string TOKEN_ITEM_NUMBER = @"$$NBR$$";
		const string TOKEN_ITEM_NUMBER_FINDER_TPL = @"{$$NBR$$}";
		const string TOKEN_ITEM_NUMBER_VALIDATOR_TPL = @"{$$NBR$$";
		#endregion  // Private Constants


		#region Private Storage for Instance
		uint _uintAppearanceOrder = APPEARANCE_ORDER_NOT_SET;
		int _intStartOffset = INVALID_OFFSET;
		int _intRawLength = INVALID_RAW_LENGTH;

		int _intItemIndex = INVALID_INDEX;
		Alignment _enmAlignment = Alignment.Unspecified;
		int _intMinimumWidth = INVALID_WIDTH;
		string _strItemFormat = null;
		#endregion  // Private Storage for Instance


		#region Constructors
		/// <summary>
		/// The default constructor satisfies a requirement of the IList
		/// interface, required for storing a collection of these in a List that
		/// is created and maintained by instances of the FormatStringParser
		/// class.
		/// </summary>
		internal FormatItem ( )
		{ } // internal FormatItem constructor (1 of 2)


		/// <summary>
		/// This constructor initializes its StartOffset and Index properties.
		///
		/// Please see the remarks for important information about these values.
		/// </summary>
		/// <param name="pintStartOffset">
		/// By defining the starting offset as a signed integer, its backing
		/// store can be initialized to minus one, an invalid value.
		///
		/// Please see the remarks for important information about these values.
		/// </param>
		/// <param name="pintIndex">
		/// By defining the index as a signed integer, its backing store can be
		/// initialized to minus one, an invalid value.
		///
		/// Please see the remarks for important information about these values.
		/// </param>
		/// <remarks>
		/// The StartOffset property is immutable, while the Index property may
		/// be subsequently amended by calling the UpdateIndex method on the
		/// instance. The synopsis of the UpdateIndex method explains in a bit
		/// greater detail.
		/// </remarks>
		internal FormatItem (
			int pintStartOffset ,
			int pintIndex )
		{
			if ( pintStartOffset > INVALID_OFFSET )
			{   // Value passed the smell test. Store it.
				_intStartOffset = pintStartOffset;
			}   // TRUE (default) block, if ( pintStartOffset > INVALID_OFFSET )
			else
			{   // This should never happen in the real world.
				throw new ArgumentOutOfRangeException (
					Properties.Resources.FORMAT_ITEM_INDEX_PROPERTY ,
					pintIndex ,
					Properties.Resources.ERRMSG_INVALID_INDEX_FOR_FORMAT_ITEM );
			}   // FALSE (exception) block, if ( pintStartOffset > INVALID_OFFSET )

			if ( pintIndex > INVALID_INDEX )
			{   // Value passed the smell test. Store it.
				_intItemIndex = pintIndex;
			}   // TRUE (default) block, if ( pintIndex > INVALID_INDEX )
			else
			{   // This should never happen in the real world.
				throw new ArgumentOutOfRangeException (
					Properties.Resources.FORMAT_ITEM_INDEX_PROPERTY ,
					pintIndex ,
					Properties.Resources.ERRMSG_INVALID_INDEX_FOR_FORMAT_ITEM );
			}   // FALSE (exception) block, if ( pintIndex > INVALID_INDEX )
		}   // internal FormatItem constructor (2 of 2)
		#endregion  // Constructors


		#region Public Properties, Most Read/Write
		/// <summary>
		/// Gets the AppearanceOrder property, which is the order, counting from
		/// 1, of its appearance in the format string.
		/// </summary>
		/// <remarks>
		/// This property is set when the Add method of a FormatItemsCollection
		/// object calls the SetAppearanceOrder on the instance just added. This
		/// is achieved by overriding the Add method of its base class.
		/// </remarks>
		public uint AppearanceOrder { get { return _uintAppearanceOrder; } }


		/// <summary>
		/// Gets the Index property of the item, which is the first, or only,
		/// number found within the item string.
		///
		/// A return value of minus one means that the Index property is
		/// uninitialized, which should be true only when the entire object is
		/// uninitialized.
		/// </summary>
		public int Index { get { return _intItemIndex; } }


		/// <summary>
		/// Gets or sets the optional ItemFormat property of the item.
		///
		/// A return value of null (Nothing in Visual Basic) means that the
		/// ItemFormat property is undefined, which is permissible, since it is
		/// optional.
		/// </summary>
		public string ItemFormat
		{
			get { return _strItemFormat; }
			set
			{
				if ( value != null )
					if ( value.Length > LENGTH_IF_EMPTY )
						_strItemFormat = value;
					else
						throw new ArgumentException (
							Properties.Resources.ERRMSG_FORMAT_ITEM_ITEMFORMAT_SUBSTRING ,
							Properties.Resources.FORMAT_ITEM_ITEMFORMAT_SUBSTRING );
				else
					throw new ArgumentNullException (
						Properties.Resources.FORMAT_ITEM_ITEMFORMAT_SUBSTRING );
			}   // public string ItemFormat Set method
		}   // public string ItemFormat Property


		/// <summary>
		/// Gets or sets the optional MinimumWidth property of the item.
		///
		/// A return value of zero means that the MinimumWidth property is
		/// undefined, which is permissible, since it is optional.
		/// </summary>
		/// <remarks>
		/// Once initialized, this property may subsequently be updated by
		/// calling the UpdateMinimumWidth method on the instance. The
		/// synopsis of the UpdateMinimumWidth method explains in greater
		/// detail.
		/// </remarks>
		public int MinimumWidth
		{
			get { return _intMinimumWidth; }
			set
			{
				if ( _intMinimumWidth > INVALID_WIDTH )
				{   // Look for an error in the Parse method.
					throw new InvalidOperationException (
						Properties.Resources.ERRMSG_MINIMUM_WIDTH_ALREADY_INITIALIZED );
				}   // if ( _intMinimumWidth > INVALID_WIDTH )
				else if ( value > INVALID_WIDTH )
				{   // Store it.
					_intMinimumWidth = value;
				}   // TRUE (expected outcome) block, if ( value > INVALID_WIDTH )
				else
				{   // Carp about bad data.
					throw new ArgumentOutOfRangeException (
						Properties.Resources.FORMAT_ITEM_MINIMUM_WIDTH_PROPERTY ,
						value ,
						Properties.Resources.ERRMSG_INVALID_WIDTH_FORMAT_ITEM );
				}   // FALSE (UNexpected outcome) block, if ( _intMinimumWidth > INVALID_WIDTH ) AND if ( value > INVALID_WIDTH )
			}   // public int MinimumWidth Property Set Method
		}   // public int MinimumWidth Property


		/// <summary>
		/// Gets the RawLength property, the length, in characters, of the raw
		/// format item in the format string.
		///
		/// A return value of zero means that the StartOffset property is
		/// uninitialized or partially initialized.
		/// </summary>
		/// <remarks>
		/// Instance method SetRawLength is called when the closing French brace
		/// is found to set this property.
		/// </remarks>
		public int RawLength
		{ get { return _intRawLength; } }


		/// <summary>
		/// Gets the required StartOffset property, the offset, counting from 0,
		/// where the format item appears in the format string.
		///
		/// A return value of minus one means that the StartOffset property is
		/// uninitialized, which should be true only when the entire object is
		/// uninitialized.
		/// </summary>
		/// <remarks>
		/// This property is immutable.
		/// </remarks>
		public int StartOffset { get { return _intStartOffset; } }


		/// <summary>
		/// Gets or sets the Alignment property, if any, associated with the
		/// item.
		/// </summary>
		public Alignment TextAlignment
		{
			get { return _enmAlignment; }
			set { _enmAlignment = value; }
		}   // public Alignment TextAlignment Property
		#endregion  // Public Properties, Most Read/Write


		#region Public Static Methods
		/// <summary>
		/// Assemble a composite format item from its constituents.
		/// </summary>
		/// <param name="puintItemIndex">
		/// The index is a standard zero based array subscript which corresponds
		/// to the position of an item in a list of objects. The list happens to
		/// be the items that correspond to the format items in a format string.
		/// </param>
		/// <param name="puintMaximumWidth">
		/// The maximum width is a positive integer that specifies the desired
		/// width of the item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a Standard Numeric, Date, or Text format string or a custom
		/// string composed around a standard string.
		/// </param>
		/// <returns>
		/// The returned string is ready to insert into a complete format string
		/// for use with string.Format, Console.WriteLine, and their numerous
		/// cousins.
		///
		/// The safest way to insert it into a format string is by calling a
		/// companion method, UpgradeFormatItem, which expects a format string,
		/// puintItemIndex, and the string returned by this method.
		/// </returns>
		/// <see cref="WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem(string, uint, string)"/>
		public static string AdjustToMaximumWidth (
			uint puintItemIndex ,
			uint puintMaximumWidth ,
			Alignment penmAlignment ,
			string pstrFormatString )
		{
			return AdjustToMaximumWidth (
				( int ) puintItemIndex ,
				( int ) puintMaximumWidth ,
				penmAlignment ,
				pstrFormatString );
		}   // public static string AdjustToMaximumWidth (1 of 4)


		/// <summary>
		/// Assemble a composite format item from its constituents.
		/// </summary>
		/// <param name="pintItemIndex">
		/// The index is a standard zero based array subscript which corresponds
		/// to the position of an item in a list of objects. The list happens to
		/// be the items that correspond to the format items in a format string.
		/// </param>
		/// <param name="puintMaximumWidth">
		/// The maximum width is a positive integer that specifies the desired
		/// width of the item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a Standard Numeric, Date, or Text format string or a custom
		/// string composed around a standard string.
		/// </param>
		/// <returns>
		/// The returned string is ready to insert into a complete format string
		/// for use with string.Format, Console.WriteLine, and their numerous
		/// cousins.
		///
		/// The safest way to insert it into a format string is by calling a
		/// companion method, UpgradeFormatItem, which expects a format string,
		/// puintItemIndex, and the string returned by this method.
		/// </returns>
		/// <see cref="WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem(string, uint, string)"/>
		public static string AdjustToMaximumWidth (
			int pintItemIndex ,
			uint puintMaximumWidth ,
			Alignment penmAlignment ,
			string pstrFormatString )
		{
			return AdjustToMaximumWidth (
				( int ) pintItemIndex ,
				( int ) puintMaximumWidth ,
				penmAlignment ,
				pstrFormatString );
		}   // public static string AdjustToMaximumWidth (2 of 4)


		/// <summary>
		/// Assemble a composite format item from its constituents.
		/// </summary>
		/// <param name="puintItemIndex">
		/// The index is a standard zero based array subscript which corresponds
		/// to the position of an item in a list of objects. The list happens to
		/// be the items that correspond to the format items in a format string.
		/// </param>
		/// <param name="pintMaximumWidth">
		/// The maximum width is a positive integer that specifies the desired
		/// width of the item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a Standard Numeric, Date, or Text format string or a custom
		/// string composed around a standard string.
		/// </param>
		/// <returns>
		/// The returned string is ready to insert into a complete format string
		/// for use with string.Format, Console.WriteLine, and their numerous
		/// cousins.
		///
		/// The safest way to insert it into a format string is by calling a
		/// companion method, UpgradeFormatItem, which expects a format string,
		/// puintItemIndex, and the string returned by this method.
		/// </returns>
		/// <see cref="WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem(string, uint, string)"/>
		public static string AdjustToMaximumWidth (
			uint puintItemIndex ,
			int pintMaximumWidth ,
			Alignment penmAlignment ,
			string pstrFormatString )
		{
			return AdjustToMaximumWidth (
				( int ) puintItemIndex ,
				( int ) pintMaximumWidth ,
				penmAlignment ,
				pstrFormatString );
		}   // public static string AdjustToMaximumWidth (3 of 4)


		/// <summary>
		/// Assemble a composite format item from its constituents.
		/// </summary>
		/// <param name="pintItemIndex">
		/// The index is a standard zero based array subscript which corresponds
		/// to the position of an item in a list of objects. The list happens to
		/// be the items that correspond to the format items in a format string.
		/// </param>
		/// <param name="pintMaximumWidth">
		/// The maximum width is a positive integer that specifies the desired
		/// width of the item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a Standard Numeric, Date, or Text format string or a custom
		/// string composed around a standard string.
		/// </param>
		/// <returns>
		/// The returned string is ready to insert into a complete format string
		/// for use with string.Format, Console.WriteLine, and their numerous
		/// cousins.
		///
		/// The safest way to insert it into a format string is by calling a
		/// companion method, UpgradeFormatItem, which expects a format string,
		/// puintItemIndex, and the string returned by this method.
		/// </returns>
		/// <see cref="WizardWrx.FormatStringEngine.FormatItem.UpgradeFormatItem(string, uint, string)"/>
		public static string AdjustToMaximumWidth (
			int pintItemIndex ,
			int pintMaximumWidth ,
			Alignment penmAlignment ,
			string pstrFormatString )
		{
			StringBuilder rsb = new StringBuilder ( );

			rsb.Append ( DLM_ITEM_BEGIN );
			rsb.Append ( pintItemIndex );
			rsb.Append ( DLM_ITEM_ALIGNMENT );

			switch ( penmAlignment )
			{
				case FormatItem.Alignment.Left:
					rsb.Append ( pintMaximumWidth * FLIP_SIGN );
					break;
				case FormatItem.Alignment.Right:
					rsb.Append ( pintMaximumWidth );
					break;
				default:
					throw new System.ComponentModel.InvalidEnumArgumentException (
						ARGNAME_ALIGNMENT ,
						( int ) penmAlignment ,
						typeof ( FormatItem.Alignment ) );
			}   // switch ( penmAlignment )

			return FinishFormatItem (
				pstrFormatString ,
				rsb );
		}   // public static string AdjustToMaximumWidth (4 of 4)


		/// <summary>
		/// Replace a basic format item of the form "{n}" where "n" is the item
		/// number, with a composite item that right aligns a numeric value in a
		/// screen width that will accommodate the maximum value that will be
		/// substituted for it in the current instance.
		/// </summary>
		/// <param name="pstrInputFormatString">
		/// Specify the format string into which to substitute the composite
		/// format item.
		/// </param>
		/// <param name="pintItemIndex">
		/// Specify the index number of the item to be replaced. The string must
		/// contain a format item "{pintItemIndex}" which is replaced.
		/// </param>
		/// <param name="pintHighestValue">
		/// Specify the highest value that you expect to substitute for format
		/// item {pintItemIndex} such as the Count property of an array that
		/// holds items to be listed.
		/// </param>
		/// <returns>
		/// A new string is returned, with format item {pintItemIndex} replaced
		/// with {pintItemIndex,m:N0}, where m = the minimum width required to
		/// display pintHighestValue using the numeric formatting defined by the
		/// current Region and Language settings in the Windows Control Panel,
		/// overriding the decimal places value with a value of zero.
		/// </returns>
		public static string AdjustItemNumberWidthPerMaxValue (
			string pstrInputFormatString ,
			int pintItemIndex ,
			int pintHighestValue )
		{
			//  ----------------------------------------------------------------
			//  This routine was designed and tested in WWEnumEnvironmentStrings
			//  which needs it in two places. Since it is essentially a wrapper
			//  for two other static routines in this class, I'll defer adding a
			//  test routine to the test stand program.
			//  ----------------------------------------------------------------

			return UpgradeFormatItem (
				pstrInputFormatString ,						// string	 pstrFormat
				pintItemIndex ,								// uint		 puintItemIndex
				AdjustToMaximumWidth (						// string	 pstrUpgrade
					pintItemIndex ,                         // uint      puintItemIndex
					pintHighestValue.ToString ( ).Length ,  // uint      puintMaximumWidth
					Alignment.Right ,                       // Alignment penmAlignment
					INTEGER_PER_REG_SETTINGS ) );           // string    pstrFormatString
		}   // public static string AdjustItemNumberWidthPerMaxValue (1 of 5)


		/// <summary>
		/// Replace a basic format item of the form "{n}" where "n" is the item
		/// number, with a composite item that right aligns a numeric value in a
		/// screen width that will accommodate the maximum value that will be
		/// substituted for it in the current instance.
		/// </summary>
		/// <param name="pstrInputFormatString">
		/// Specify the format string into which to substitute the composite
		/// format item.
		/// </param>
		/// <param name="puintItemIndex">
		/// Specify the index number of the item to be replaced. The string must
		/// contain a format item "{pintItemIndex}" which is replaced.
		/// </param>
		/// <param name="pintHighestValue">
		/// Specify the highest value that you expect to substitute for format
		/// item {pintItemIndex} such as the Count property of an array that
		/// holds items to be listed.
		/// </param>
		/// <returns>
		/// A new string is returned, with format item {pintItemIndex} replaced
		/// with {pintItemIndex,m:N0}, where m = the minimum width required to
		/// display pintHighestValue using the numeric formatting defined by the
		/// current Region and Language settings in the Windows Control Panel,
		/// overriding the decimal places value with a value of zero.
		/// </returns>
		public static string AdjustItemNumberWidthPerMaxValue (
			string pstrInputFormatString ,
			uint puintItemIndex ,
			int pintHighestValue )
		{
			//  ----------------------------------------------------------------
			//  See the note under overload 1 about the origin of these routines
			//  and why it justifies omitting a dedicated test routine.
			//  ----------------------------------------------------------------

			return FormatItem.UpgradeFormatItem (
				pstrInputFormatString ,						// string    pstrFormat
				puintItemIndex ,							// uint      puintItemIndex
				FormatItem.AdjustToMaximumWidth (			// string    pstrUpgrade
					puintItemIndex ,						// uint      puintItemIndex
					pintHighestValue.ToString ( ).Length ,	// uint      puintMaximumWidth
					FormatItem.Alignment.Right ,			// Alignment penmAlignment
					INTEGER_PER_REG_SETTINGS ) );			// string    pstrFormatString
		}   // public static string AdjustItemNumberWidthPerMaxValue (2 of 5)


		/// <summary>
		/// Replace a basic format item of the form "{n}" where "n" is the item
		/// number, with a composite item that right aligns a numeric value in a
		/// screen width that will accommodate the maximum value that will be
		/// substituted for it in the current instance.
		/// </summary>
		/// <param name="pstrInputFormatString">
		/// Specify the format string into which to substitute the composite
		/// format item.
		/// </param>
		/// <param name="pintItemIndex">
		/// Specify the index number of the item to be replaced. The string must
		/// contain a format item "{pintItemIndex}" which is replaced.
		/// </param>
		/// <param name="puintHighestValue">
		/// Specify the highest value that you expect to substitute for format
		/// item {pintItemIndex} such as the Count property of an array that
		/// holds items to be listed.
		/// </param>
		/// <returns>
		/// A new string is returned, with format item {pintItemIndex} replaced
		/// with {pintItemIndex,m:N0}, where m = the minimum width required to
		/// display pintHighestValue using the numeric formatting defined by the
		/// current Region and Language settings in the Windows Control Panel,
		/// overriding the decimal places value with a value of zero.
		/// </returns>
		public static string AdjustItemNumberWidthPerMaxValue (
			string pstrInputFormatString ,
			int pintItemIndex ,
			uint puintHighestValue )
		{
			//  ----------------------------------------------------------------
			//  See the note under overload 1 about the origin of these routines
			//  and why it justifies omitting a dedicated test routine.
			//  ----------------------------------------------------------------

			return FormatItem.UpgradeFormatItem (
				pstrInputFormatString ,						// string    pstrFormat
				pintItemIndex ,								// uint      puintItemIndex
				FormatItem.AdjustToMaximumWidth (			// string    pstrUpgrade
					pintItemIndex ,							// uint      puintItemIndex
					puintHighestValue.ToString ( ).Length ,	// uint      puintMaximumWidth
					FormatItem.Alignment.Right ,			// Alignment penmAlignment
					INTEGER_PER_REG_SETTINGS ) );			// string    pstrFormatString
		}   // public static string AdjustItemNumberWidthPerMaxValue (3 of 5)


		/// <summary>
		/// Replace a basic format item of the form "{n}" where "n" is the item
		/// number, with a composite item that right aligns a numeric value in a
		/// screen width that will accommodate the maximum value that will be
		/// substituted for it in the current instance.
		/// </summary>
		/// <param name="pstrInputFormatString">
		/// Specify the format string into which to substitute the composite
		/// format item.
		/// </param>
		/// <param name="puintItemIndex">
		/// Specify the index number of the item to be replaced. The string must
		/// contain a format item "{pintItemIndex}" which is replaced.
		/// </param>
		/// <param name="puintHighestValue">
		/// Specify the highest value that you expect to substitute for format
		/// item {pintItemIndex} such as the Count property of an array that
		/// holds items to be listed.
		/// </param>
		/// <returns>
		/// A new string is returned, with format item {pintItemIndex} replaced
		/// with {pintItemIndex,m:N0}, where m = the minimum width required to
		/// display pintHighestValue using the numeric formatting defined by the
		/// current Region and Language settings in the Windows Control Panel,
		/// overriding the decimal places value with a value of zero.
		/// </returns>
		public static string AdjustItemNumberWidthPerMaxValue (
			string pstrInputFormatString ,
			uint puintItemIndex ,
			uint puintHighestValue )
		{
			//  ----------------------------------------------------------------
			//  See the note under overload 1 about the origin of these routines
			//  and why it justifies omitting a dedicated test routine.
			//  ----------------------------------------------------------------

			return FormatItem.UpgradeFormatItem (
				pstrInputFormatString ,						// string    pstrFormat
				puintItemIndex ,							// uint      puintItemIndex
				FormatItem.AdjustToMaximumWidth (			// string    pstrUpgrade
					puintItemIndex ,						// uint      puintItemIndex
					puintHighestValue.ToString ( ).Length ,	// uint      puintMaximumWidth
					FormatItem.Alignment.Right ,			// Alignment penmAlignment
					INTEGER_PER_REG_SETTINGS ) );			// string    pstrFormatString
		}   // public static string AdjustItemNumberWidthPerMaxValue (4 of 5)


		/// <summary>
		/// Replace a basic format item of the form "{n}" where "n" is the item
		/// number, with a composite item that right aligns a numeric value in a
		/// screen width that will accommodate the maximum value that will be
		/// substituted for it in the current instance.
		/// </summary>
		/// <param name="pstrFormat">
		/// Specify a valid format string to upgrade. The string must contain a
		/// token of the form {n}, where n is equal to pintItemIndex.
		/// </param>
		/// <param name="pintItemIndex">
		/// Specify the index of the item to be upgraded. The integer must be
		/// positive, and pstrFormat must contain at least once instance of a
		/// correspondingly numbered format item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pintMaximumWidth">
		/// The maximum width is a positive integer that specifies the desired
		/// width of the item.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a Standard Numeric, Date, or Text format string or a custom
		/// string composed around a standard string.
		/// </param>
		/// <returns>
		/// The return value is a new format control string, with the original
		/// vanilla format item at the specified index replaced with a new one
		/// that has its width adjusted precisely to accommodate the widest
		/// expected value.
		/// </returns>
		public static string AdjustItemNumberWidthPerMaxValue (
			string pstrFormat ,
			int pintItemIndex ,
			FormatItem.Alignment penmAlignment ,
			int pintMaximumWidth ,
			string pstrFormatString )
		{
			return FormatItem.UpgradeFormatItem (
				pstrFormat ,								// pstrFormat: 			Specify a valid format string to upgrade. The string must contain a token of the form {n}, where n is equal to pintItemIndex.
				pintItemIndex ,								// pintItemIndex: 		Specify the index of the item to be upgraded. The integer must be positive, and pstrFormat must contain at least once instance of a correspondingly numbered format item.
				FormatItem.AdjustToMaximumWidth (			// pstrUpgrade: Specify the upgraded format item, such as the string returned by the static AdjustToMaximumWidth method of this class.  Contrast this with pstrFormatString, which expects you to supply a standard or custom Numeric or DateTime format string, from which it constructs a complete, well formed Format Item.
					pintItemIndex ,							// pintItemIndex: 		Specify the index of the item to be upgraded. The integer must be positive, and pstrFormat must contain at least once instance of a correspondingly numbered format item.
					pintMaximumWidth ,						// pintMaximumWidth: 	The maximum width is a positive integer that specifies the desired width of the item.
					FormatItem.Alignment.Right ,			// penmAlignment:	 	Specify Left or Right. Center alignment is unsupported, although it could be achieved with custom code.
					pstrFormatString ) );					// pstrFormatString: 	Specify a Standard Numeric, Date, or Text format string or a custom string composed around a standard string.
		}	// public static string AdjustItemNumberWidthPerMaxValue (5 of 5)


		/// <summary>
		/// Upgrade a plain vanilla format item of the form {n}, where n is the
		/// number of a format item that exists somewhere in a format string to
		/// a composite string that specifies a width that is computed at run
		/// time to be appropriate to the current data set.
		/// </summary>
		/// <param name="pstrFormat">
		/// Specify a valid format string to upgrade. The string must contain a
		/// token of the form {n}, where n is equal to pintItemIndex.
		/// </param>
		/// <param name="pintItemIndex">
		/// Specify the index of the item to be upgraded. The integer must be
		/// positive, and pstrFormat must contain at least once instance of a
		/// correspondingly numbered format item.
		/// </param>
		/// <param name="pstrUpgrade">
		/// Specify the upgraded format item, such as the string returned by the
		/// static AdjustToMaximumWidth method of this class.
		///
		/// Contrast this with pstrFormatString, which expects you to supply a
		/// standard or custom Numeric or DateTime format string, from which it
		/// constructs a complete, well formed Format Item.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the upgraded format
		/// string, in which the indicated plain vanilla format item has been
		/// replaced with a dynamically generated composite format item.
		/// </returns>
		/// <remarks>
		/// This routine expends a good deal of effort to validate its inputs,
		/// so that it can be safely used to process user input, such as data
		/// read into the program from a configuration file.
		/// </remarks>
		/// <see cref="WizardWrx.FormatStringEngine.FormatItem.AdjustToMaximumWidth(uint, uint, WizardWrx.FormatStringEngine.FormatItem.Alignment, string)"/>
		public static string UpgradeFormatItem (
			string pstrFormat ,
			int pintItemIndex ,
			string pstrUpgrade )
		{
			if ( string.IsNullOrEmpty ( pstrFormat ) )
				if ( pstrFormat == null )
					throw new ArgumentNullException ( ARGNAME_FORMAT );
				else
					throw new ArgumentException (
						WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
						ARGNAME_FORMAT );

			if ( string.IsNullOrEmpty ( pstrUpgrade ) )
				if ( pstrUpgrade == null )
					throw new ArgumentNullException ( ARGNAME_STRUPGRADE );
				else
					throw new ArgumentException (
						WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
						ARGNAME_STRUPGRADE );

			string strTokenValidator = TOKEN_ITEM_NUMBER_VALIDATOR_TPL.Replace (
				TOKEN_ITEM_NUMBER ,
				pintItemIndex.ToString ( ) );

			if ( pstrUpgrade.Substring ( ArrayInfo.ARRAY_FIRST_ELEMENT , strTokenValidator.Length ) != strTokenValidator )
				throw new InvalidOperationException (
					DeclineUpgrade (
						Properties.Resources.ERRMSG_INVALID_FORMAT_ITEM ,
						pstrFormat ,
						pintItemIndex ,
						pstrUpgrade ) );

			string strTokenFinder = TOKEN_ITEM_NUMBER_FINDER_TPL.Replace (
				TOKEN_ITEM_NUMBER ,
				pintItemIndex.ToString ( ) );

			if ( pstrFormat.Contains ( strTokenFinder ) )
				return pstrFormat.Replace ( strTokenFinder , pstrUpgrade );
			else
				throw new InvalidOperationException (
					DeclineUpgrade (
						Properties.Resources.ERRMSG_MISSING_FORMAT_ITEM ,
						pstrFormat ,
						pintItemIndex ,
						pstrUpgrade ) );
		}   // public static string UpgradeFormatItem (1 of 5)


		/// <summary>
		/// Upgrade a plain vanilla format item of the form {n}, where n is the
		/// number of a format item that exists somewhere in a format string to
		/// a composite string that specifies a width that is computed at run
		/// time to be appropriate to the current data set.
		/// </summary>
		/// <param name="pstrFormat">
		/// Specify a valid format string to upgrade. The string must contain a
		/// token of the form {n}, where n is equal to puintItemIndex.
		/// </param>
		/// <param name="puintItemIndex">
		/// Specify the index of the item to be upgraded. The integer must be
		/// positive, and pstrFormat must contain at least once instance of a
		/// correspondingly numbered format item.
		/// </param>
		/// <param name="pstrUpgrade">
		/// Specify the upgraded format item, such as the string returned by the
		/// static AdjustToMaximumWidth method of this class.
		///
		/// Contrast this with pstrFormatString, which expects you to supply a
		/// standard or custom Numeric or DateTime format string, from which it
		/// constructs a complete, well formed Format Item.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the upgraded format
		/// string, in which the indicated plain vanilla format item has been
		/// replaced with a dynamically generated composite format item.
		/// </returns>
		/// <remarks>
		/// This routine expends a good deal of effort to validate its inputs,
		/// so that it can be safely used to process user input, such as data
		/// read into the program from a configuration file.
		/// </remarks>
		/// <see cref="WizardWrx.FormatStringEngine.FormatItem.AdjustToMaximumWidth(uint, uint, WizardWrx.FormatStringEngine.FormatItem.Alignment, string)"/>
		public static string UpgradeFormatItem (
			string pstrFormat ,
			uint puintItemIndex ,
			string pstrUpgrade )
		{
			if ( string.IsNullOrEmpty ( pstrFormat ) )
				if ( pstrFormat == null )
					throw new ArgumentNullException ( ARGNAME_FORMAT );
				else
					throw new ArgumentException (
						WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
						ARGNAME_FORMAT );

			if ( string.IsNullOrEmpty ( pstrUpgrade ) )
				if ( pstrUpgrade == null )
					throw new ArgumentNullException ( ARGNAME_STRUPGRADE );
				else
					throw new ArgumentException (
						WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
						ARGNAME_STRUPGRADE );

			string strTokenValidator = TOKEN_ITEM_NUMBER_VALIDATOR_TPL.Replace (
				TOKEN_ITEM_NUMBER ,
				puintItemIndex.ToString ( ) );

			if ( pstrUpgrade.Substring ( ArrayInfo.ARRAY_FIRST_ELEMENT , strTokenValidator.Length ) != strTokenValidator )
				throw new InvalidOperationException (
					DeclineUpgrade (
						Properties.Resources.ERRMSG_INVALID_FORMAT_ITEM ,
						pstrFormat ,
						puintItemIndex ,
						pstrUpgrade ) );

			string strTokenFinder = TOKEN_ITEM_NUMBER_FINDER_TPL.Replace (
				TOKEN_ITEM_NUMBER ,
				puintItemIndex.ToString ( ) );

			if ( pstrFormat.Contains ( strTokenFinder ) )
				return pstrFormat.Replace ( strTokenFinder , pstrUpgrade );
			else
				throw new InvalidOperationException (
					DeclineUpgrade (
						Properties.Resources.ERRMSG_MISSING_FORMAT_ITEM ,
						pstrFormat ,
						puintItemIndex ,
						pstrUpgrade ) );
		}   // public static string UpgradeFormatItem (2 of 5)


		/// <summary>
		/// Upgrade a plain vanilla format item of the form {n}, where n is the
		/// number of a format item that exists somewhere in a format string to
		/// a composite string that specifies a width that is computed at run
		/// time to be appropriate to the current data set.
		/// </summary>
		/// <param name="pstrFormat">
		/// Specify a valid format string to upgrade. The string must contain a
		/// token of the form {n}, where n is equal to pintItemIndex.
		/// </param>
		/// <param name="pintItemIndex">
		/// Specify the index of the item to be upgraded. The integer must be
		/// positive, and pstrFormat must contain at least once instance of a
		/// correspondingly numbered format item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a standard or custom Numeric or DateTime format string.
		///
		/// Contrast this with pstrUpgrade, which expects you to supply the
		/// entire format item, ready for substitution.
		/// </param>
		/// <param name="pastrValueArray">
		/// Specify an array of strings, all of which are values intended to be
		/// formatted by the upgraded format item. This routine determines the
		/// length of the longest string, which becomes the basis of the
		/// alignment parameter inserted into the upgraded format item.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the upgraded format
		/// string, in which the indicated plain vanilla format item has been
		/// replaced with a dynamically generated composite format item.
		/// </returns>
		public static string UpgradeFormatItem (
			string pstrFormat ,
			int pintItemIndex ,
			Alignment penmAlignment ,
			string pstrFormatString ,
			string [ ] pastrValueArray )
		{
			return FormatItem.UpgradeFormatItem (
				pstrFormat ,                                // string               pstrFormat        = Prototype format template string
				pintItemIndex ,                             // int                  pintItemIndex     = Index (zero based) of format item to upgrade
				FormatItem.AdjustToMaximumWidth (           // string               pstrUpgrade       = Replacement format item to substitute for {pintItemIndex}
					pintItemIndex ,                         // int                  pintItemIndex     = Index (zero based) of format item to upgrade
					StringTricks.LengthOfLongestString (	// int                  pintMaximumWidth  = Width required to accommodate longest widest field value
						pastrValueArray ) ,                 // string [ ]           pastrTheseStrings = Format must accommodate longest of these.
					penmAlignment ,                         // FormatItem.Alignment penmAlignment     = Format Items may be aligned either Left or Right. Choose.
					pstrFormatString ) );                   // string               pstrFormatString  = Unless null (or empty) this becomes the 3rd part of the FormatItem.
		}   // public static string UpgradeFormatItem (3 of 5)


		/// <summary>
		/// Upgrade a plain vanilla format item of the form {n}, where n is the
		/// number of a format item that exists somewhere in a format string to
		/// a composite string that specifies a width that is computed at run
		/// time to be appropriate to the current data set.
		/// </summary>
		/// <param name="pstrFormat">
		/// Specify a valid format string to upgrade. The string must contain a
		/// token of the form {n}, where n is equal to pintItemIndex.
		/// </param>
		/// <param name="puintItemIndex">
		/// Specify the index of the item to be upgraded. The integer must be
		/// positive, and pstrFormat must contain at least once instance of a
		/// correspondingly numbered format item.
		/// </param>
		/// <param name="penmAlignment">
		/// Specify Left or Right. Center alignment is unsupported, although it
		/// could be achieved with custom code.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify a standard or custom Numeric or DateTime format string.
		///
		/// Contrast this with pstrUpgrade, which expects you to supply the
		/// entire format item, ready for substitution.
		/// </param>
		/// <param name="pastrValueArray">
		/// Specify an array of strings, all of which are values intended to be
		/// formatted by the upgraded format item. This routine determines the
		/// length of the longest string, which becomes the basis of the
		/// alignment parameter inserted into the upgraded format item.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the upgraded format
		/// string, in which the indicated plain vanilla format item has been
		/// replaced with a dynamically generated composite format item.
		/// </returns>
		public static string UpgradeFormatItem (
			string pstrFormat ,
			uint puintItemIndex ,
			Alignment penmAlignment ,
			string pstrFormatString ,
			List<string> pastrValueArray )
		{
			return FormatItem.UpgradeFormatItem (
				pstrFormat ,                                // string               pstrFormat        = Prototype format template string
				puintItemIndex ,                            // uint                 pintItemIndex     = Index (zero based) of format item to upgrade
				FormatItem.AdjustToMaximumWidth (           // string               pstrUpgrade       = Replacement format item to substitute for {pintItemIndex}
					puintItemIndex ,                        // uint                 pintItemIndex     = Index (zero based) of format item to upgrade
					MaxStringLength (                       // uint                 pintMaximumWidth  = Width required to accommodate longest widest field value
						pastrValueArray ) ,                 // string [ ]           pastrTheseStrings = Format must accommodate longest of these.
					penmAlignment ,                         // FormatItem.Alignment penmAlignment     = Format Items may be aligned either Left or Right. Choose.
					pstrFormatString ) );                   // string               pstrFormatString  = Unless null (or empty) this becomes the 3rd part of the FormatItem.
		}   // public static string UpgradeFormatItem (4 of 5)


		/// <summary>
		/// Given two integral values X and Y, where X is less than or equal to
		/// Y, return a string of the form "X of Y" for use on a report.
		/// </summary>
		/// <param name="puintX">
		/// X is the ordinal number of the item in the list, expressed as an
		/// unsigned integer.
		/// </param>
		/// <param name="puintY">
		/// Y is the total number of items in the list, expressed as an unsigned
		/// integer.
		/// </param>
		/// <returns>
		/// The returned string is literally "X of Y," where X and Y are the two
		/// integers representing, respectively, the current item number and the
		/// total number of items in the set. The formatting of X and Y is such
		/// that a list is guaranteed to be vertically aligned, because X is set
		/// in a right aligned field whose width is the same as that of a string
		/// representation of Y.
		/// </returns>
		public static string XofY (
			uint puintX ,
			uint puintY )
		{
			if ( puintY == MagicNumbers.ZERO )
				return puintX.ToString ( INTEGER_PER_REG_SETTINGS );
			else
				return string.Format (
					XFormatFixup ( puintY.ToString ( INTEGER_PER_REG_SETTINGS ) ) ,     // Template (format control string)
					puintX ,                                                            // Format Item 0 = X value
					puintY );                                                           // Format Item 1 = Y value
		}   // public static string XofY (1 of 4)


		/// <summary>
		/// Given two integral values X and Y, where X is less than or equal to
		/// Y, return a string of the form "X of Y" for use on a report.
		/// </summary>
		/// <param name="pintX">
		/// X is the ordinal number of the item in the list, expressed as a
		/// signed integer.
		/// </param>
		/// <param name="puintY">
		/// Y is the total number of items in the list, expressed as an unsigned
		/// integer.
		/// </param>
		/// <returns>
		/// The returned string is literally "X of Y," where X and Y are the two
		/// integers representing, respectively, the current item number and the
		/// total number of items in the set. The formatting of X and Y is such
		/// that a list is guaranteed to be vertically aligned, because X is set
		/// in a right aligned field whose width is the same as that of a string
		/// representation of Y.
		/// </returns>
		public static string XofY (
			int pintX ,
			uint puintY )
		{
			if ( puintY == MagicNumbers.ZERO )
				return pintX.ToString ( INTEGER_PER_REG_SETTINGS );
			else
				return string.Format (
					XFormatFixup ( puintY.ToString ( INTEGER_PER_REG_SETTINGS ) ) ,     // Template (format control string)
					pintX ,                                                             // Format Item 0 = X value
					puintY );                                                           // Format Item 1 = Y value
		}   // public static string XofY (2 of 4)


		/// <summary>
		/// Given two integral values X and Y, where X is less than or equal to
		/// Y, return a string of the form "X of Y" for use on a report.
		/// </summary>
		/// <param name="puintX">
		/// X is the ordinal number of the item in the list, expressed as an
		/// unsigned integer.
		/// </param>
		/// <param name="pintY">
		/// Y is the total number of items in the list, expressed as a signed
		/// integer.
		/// </param>
		/// <returns>
		/// The returned string is literally "X of Y," where X and Y are the two
		/// integers representing, respectively, the current item number and the
		/// total number of items in the set. The formatting of X and Y is such
		/// that a list is guaranteed to be vertically aligned, because X is set
		/// in a right aligned field whose width is the same as that of a string
		/// representation of Y.
		/// </returns>
		public static string XofY (
			uint puintX ,
			int pintY )
		{
			if ( pintY == MagicNumbers.ZERO )
				return puintX.ToString ( INTEGER_PER_REG_SETTINGS );
			else
				return string.Format (
					XFormatFixup ( pintY.ToString ( INTEGER_PER_REG_SETTINGS ) ) ,      // Template (format control string)
					puintX ,                                                            // Format Item 0 = X value
					pintY );                                                            // Format Item 1 = Y value
		}   // public static string XofY (3 of 4)


		/// <summary>
		/// Given two integral values X and Y, where X is less than or equal to
		/// Y, return a string of the form "X of Y" for use on a report.
		/// </summary>
		/// <param name="pintX">
		/// X is the ordinal number of the item in the list, expressed as a
		/// signed integer.
		/// </param>
		/// <param name="pintY">
		/// Y is the total number of items in the list, expressed as a signed
		/// integer.
		/// </param>
		/// <returns>
		/// The returned string is literally "X of Y," where X and Y are the two
		/// integers representing, respectively, the current item number and the
		/// total number of items in the set. The formatting of X and Y is such
		/// that a list is guaranteed to be vertically aligned, because X is set
		/// in a right aligned field whose width is the same as that of a string
		/// representation of Y.
		/// </returns>
		public static string XofY (
			int pintX ,
			int pintY )
		{
			if ( pintY == MagicNumbers.ZERO )
				return Integer ( pintX );
			else
				return string.Format (
					XFormatFixup ( Integer ( pintY ) ) ,	// Template (format control string)
					pintX ,                                 // Format Item 0 = X value
					pintY );                                // Format Item 1 = Y value
		}   // public static string XofY (4 of 4)
		#endregion


		#region Internal Methods
		/// <summary>
		/// Sets the Alignment property associated with the item.
		/// </summary>
		/// <remarks>
		/// This property is set by the overridden Add method of the
		/// FormatItemsCollection. Since that class isn't in the
		/// inheritance chain of this one, and the property must be
		/// protected from tampering by consumers, the property is
		/// assigned Internal scope.
		/// </remarks>
		internal void SetAppearanceOrder ( uint puintAppearanceOrder )
		{
			if ( puintAppearanceOrder > APPEARANCE_ORDER_NOT_SET )
				_uintAppearanceOrder = puintAppearanceOrder;
			else
				throw new ArgumentOutOfRangeException (
					Properties.Resources.FORMAT_ITEM_APPEARANCE_ORDER ,         // string paramName
					puintAppearanceOrder ,                                      // object actualValue
					Properties.Resources.ERRMSG_INVALID_APPEARANCE_ORDER );     // string message
		}   // internal void SetAppearanceOrder


		/// <summary>
		/// Calculate the raw length of the format item from the character
		/// position (zero based offset) where the closing French brace was
		/// found and the offset where the opening French brace occurred. The
		/// opening position is supplied to the constructor, and saved in the
		/// immutable StartOffset property.
		/// </summary>
		/// <param name="pintPosClosingBrace">
		/// The FormatStringParser passes in the current character position.
		/// Both pintPosClosingBrace and _intStartOffset are zero based string
		/// offsets, to which we add 1 to derive a length.
		/// </param>
		internal void SetRawLength ( int pintPosClosingBrace )
		{
			_intRawLength = pintPosClosingBrace - _intStartOffset + 1;
		}   // internal void SetRawLength


		/// <summary>
		/// Call this method to add a digit to the index. Although the index is
		/// an integer and the digit arrives as a character, it is converted to
		/// an integer, then added to the current index value times ten,
		/// shifting the decimal place to make room for it.
		/// </summary>
		/// <param name="pchrNextDigit">
		/// The next digit is passed into the method without conversion, so that
		/// one routine can service both Index and MininumWidth properties.
		/// </param>
		internal void UpdateIndex ( char pchrNextDigit )
		{
			_intItemIndex = AddDigitToInteger (
				pchrNextDigit ,
				_intItemIndex );
		}   // internal void UpdateIndex


		/// <summary>
		/// Call this method to add a digit to the minimum width attribute of an
		/// Alignment. Although the width is an integer and the digit arrives as
		/// a character, it is converted to an integer, then added to the
		/// current width value times ten, shifting the decimal place to make
		/// room for it.
		/// </summary>
		/// <param name="pchrNextDigit">
		/// The next digit is passed into the method without conversion, so that
		/// one routine can service both Index and MininumWidth properties.
		/// </param>
		internal void UpdateMinimumWidth ( char pchrNextDigit )
		{
			_intMinimumWidth = AddDigitToInteger (
				pchrNextDigit ,
				_intMinimumWidth );
		}   // internal void UpdateMinimumWidth
		#endregion  // Internal Methods


		#region Private Static Utility Methods
		/// <summary>
		/// Instance methods UpdateIndex and UpdateMinimumWidth call this method
		/// to add a digit to the index or minimum width, respectively. Although
		/// both values are integers and the digit arrives as a character, it is
		/// converted to an integer, then added to the supplied value times ten,
		/// shifting the decimal place to make room for it.
		/// </summary>
		/// <param name="pchrNextDigit">
		/// The next digit is passed into the method without conversion, so that
		/// one routine can service both Index and MininumWidth properties.
		/// </param>
		/// <param name="pintCurrentValue">
		/// The current value to be converted is passed into this method from a
		/// calling instance method.
		/// </param>
		/// <returns>
		/// The new value is the sum of pintCurrentValue times ten and a single
		/// digit integer representation of pchrNextDigit, which replaces
		/// pintCurrentValue in the instance.
		/// </returns>
		private static int AddDigitToInteger (
			char pchrNextDigit ,
			int pintCurrentValue )
		{
			const int DECIMAL_SHIFT = 10;
			return ( pintCurrentValue * DECIMAL_SHIFT )
				+ ( int ) char.GetNumericValue ( pchrNextDigit );
		}   // private static int AddDigitToInteger


		/// <summary>
		/// Assemble a message to accompany the InvalidOperationException
		/// exception that UpgradeFormatItem is about to throw.
		/// </summary>
		/// <param name="pstrMainMessage">
		/// The caller selects a message from the collection of resource strings
		/// to state the reason the upgrade is being rejected.
		/// </param>
		/// <param name="pstrFormat">
		/// The caller hands off the format string that it was asked to upgrade.
		/// </param>
		/// <param name="puintItemIndex">
		/// The caller hands off the index number of the format item that it was
		/// asked to upgrade.
		/// </param>
		/// <param name="pstrUpgrade">
		/// The caller hands off the format string that it was asked to
		/// substitute for the plain vanilla format item.
		/// </param>
		/// <returns>
		/// The caller hands off the returned string to a new
		/// InvalidOperationException exception.
		/// </returns>
		/// <remarks>
		/// The reason for handing off the string, rather than creating the
		/// exception and returning it is that it makes the origin of the
		/// exception unambiguous.
		/// </remarks>
		private static string DeclineUpgrade (
			string pstrMainMessage ,
			string pstrFormat ,
			uint puintItemIndex ,
			string pstrUpgrade )
		{
			return string.Format (
				pstrMainMessage ,                                       // Template for main message, to which the argument list is appended
				string.Format (                                         // Format Item0 - Formatted argument list
					Properties.Resources.ERRMSG_FORMAT_ITEM_DETAILS ,   // Template (Format String)
					new string [ ]
                    {
                        pstrFormat ,                                    // Format Item0 = Format string that is missing the reqoured format item
                        puintItemIndex.ToString ( ) ,                   // Format Item1 = Specified Item number (an array index), missing from pstrFormat
                        pstrUpgrade ,                                   // Format Item2 = Upgraded format item that would have been inserted
                        Environment.NewLine                             // Format Item3 = Newline
                    } ) );
		}   // private static string DeclineUpgrade (1 of 2)


		/// <summary>
		/// Assemble a message to accompany the InvalidOperationException
		/// exception that UpgradeFormatItem is about to throw.
		/// </summary>
		/// <param name="pstrMainMessage">
		/// The caller selects a message from the collection of resource strings
		/// to state the reason the upgrade is being rejected.
		/// </param>
		/// <param name="pstrFormat">
		/// The caller hands off the format string that it was asked to upgrade.
		/// </param>
		/// <param name="pintItemIndex">
		/// The caller hands off the index number of the format item that it was
		/// asked to upgrade.
		/// </param>
		/// <param name="pstrUpgrade">
		/// The caller hands off the format string that it was asked to
		/// substitute for the plain vanilla format item.
		/// </param>
		/// <returns>
		/// The caller hands off the returned string to a new
		/// InvalidOperationException exception.
		/// </returns>
		/// <remarks>
		/// The reason for handing off the string, rather than creating the
		/// exception and returning it is that it makes the origin of the
		/// exception unambiguous.
		/// </remarks>
		private static string DeclineUpgrade (
			string pstrMainMessage ,
			string pstrFormat ,
			int pintItemIndex ,
			string pstrUpgrade )
		{
			return string.Format (
				pstrMainMessage ,                                       // Template for main message, to which the argument list is appended
				string.Format (                                         // Format Item0 - Formatted argument list
					Properties.Resources.ERRMSG_FORMAT_ITEM_DETAILS ,   // Template (Format String)
					new string [ ]
                    {
                        pstrFormat ,                                    // Format Item0 = Format string that is missing the reqoured format item
                        pintItemIndex.ToString ( ) ,                    // Format Item1 = Specified Item number (an array index), missing from pstrFormat
                        pstrUpgrade ,                                   // Format Item2 = Upgraded format item that would have been inserted
                        Environment.NewLine                             // Format Item3 = Newline
                    } ) );
		}   // private static string DeclineUpgrade (2 of 2)


		/// <summary>
		/// Evaluate the third argument to AdjustToMaximumWidth, and append it
		/// to the new format item, unless it is a null reference (Nothing in
		/// Visual Basic) or the empty string.
		/// </summary>
		/// <param name="pstrFormatString">
		/// This string is the optional format string to append to the item.
		/// </param>
		/// <param name="prsb">
		/// The calling AdjustToMaximumWidth overload hands off a StringBuilder
		/// that it populated with the first two items, both required in the
		/// context in which this method is used.
		/// </param>
		/// <returns>
		/// This function constructs a string containing the completed format
		/// item that was assembled in the StringBuilder that
		/// AdjustToMaximumWidth handed off.
		/// </returns>
		/// <remarks>
		/// This piece was originally coded inline, trading a tad of code bloat
		/// for efficiency. The additional complexity significantly tips the
		/// balance in favor of refactoring.
		/// </remarks>
		private static string FinishFormatItem (
			string pstrFormatString ,
			StringBuilder prsb )
		{
			if ( !string.IsNullOrEmpty ( pstrFormatString ) )
			{   // Along with the format string, its delimiter is optional.
				prsb.Append ( DLM_ITEM_FORMAT );
				prsb.Append ( pstrFormatString );
			}   // if ( !string.IsNullOrEmpty ( pstrFormatString ) )

			prsb.Append ( DLM_ITEM_END );

			return prsb.ToString ( );
		}   // private static string FinishFormatItem


		/// <summary>
		/// Format any integer per the Regional Settings, overriding the digits
		/// past the decimal point to render an integer.
		/// </summary>
		/// <param name="pintAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		private static string Integer ( int pintAnyInteger )
		{
			return pintAnyInteger.ToString ( IntegerPerRegSettings ( ) );
		}	// private static string Integer

		/// <summary>
		/// Return a string suitable for formatting an integer per the Regional
		/// Settings, overriding the default number of digits to display to the
		/// right of the decimal point to display zero digits to the right of
		/// the decimal point.
		/// </summary>
		/// <returns>
		/// Format string suitable for formatting an integer
		/// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <see cref="WizardWrx.NumericFormats.NUMBER_PER_REG_SETTINGS"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="WizardWrx.NumericFormats.NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="WizardWrx.NumericFormats.NUMBER_PER_REG_SETTINGS_2D"/>
		/// <seealso cref="WizardWrx.NumericFormats.NUMBER_PER_REG_SETTINGS_3D"/>
		public static string IntegerPerRegSettings ( )
		{
			const int DECIMAL_DIGITS_MIN = 0;
			const int DECIMAL_DIGITS_NONE = DECIMAL_DIGITS_MIN;

			const string FORMAT_TEMPLATE = @"{0}{1}";
			const string NUMBER_PER_REG_SETTINGS = @"N";

			return string.Format (
				FORMAT_TEMPLATE ,
				NUMBER_PER_REG_SETTINGS ,
				DECIMAL_DIGITS_NONE );
		}	// IntegerPerRegSettings

		/// <summary>
		/// Given an array of objects of any type, return the length of the
		/// longest string made from them. See Remarks.
		/// </summary>
		/// <param name="plstObjs">
		/// This argument expects a generic List of strings.
		/// </param>
		/// <returns>
		/// The return value is the length of the longest string made from the
		/// objects in the input array. Since it is intended for use with the
		/// PadRight method on a string, it is cast to int. See Remarks.
		/// </returns>
		/// <remarks>
		/// This specialized implementation exists to decouple this class from
		/// class library WizardWrx.SharedUtl4.dll, since I want the option of
		/// distributing this class library separately.
		///
		/// As a bonus, this implementation is slightly more efficient, since it
		/// dispenses with a redundant ToString call on each member of the
		/// collection.
		/// </remarks>
		private static int MaxStringLength ( List<string> plstObjs )
		{   // Treat a null reference gracefully, as a degenerate case that returns zero.
			int rintMaxLength = MagicNumbers.ZERO;

			if ( plstObjs != null )
			{
				foreach ( string objCurrent in plstObjs )
				{   // Convert each object, in turn, to a string.
					if ( objCurrent.Length > rintMaxLength )
					{   // Update return value if string is longest so far.
						rintMaxLength = objCurrent.Length;
					}   // if ( strObjectAsString.Length > rintMaxLength )
				}   // foreach ( string objCurrent in plstObjs )
			}   // if ( plstObjs != null )

			return rintMaxLength;
		}   // private static int MaxStringLength


		/// <summary>
		/// Return a format string suitable for displaying a string of the form,
		/// "X of Y," where X and Y are integers, such that X is less than or
		/// equal to Y.
		/// </summary>
		/// <param name="pstrOfY">
		/// Since Y is the larger of the two integers, formatting it as a string
		/// determines the maximum number of characters needed to display X with
		/// the same standard numeric format string, N0.
		/// </param>
		/// <returns>
		/// Use the returned string to format a string "X of Y" for display in
		/// a report. The outcome is a report in which a list of items, all of
		/// which begin with "X of Y," will align vertically.
		/// </returns>
		private static string XFormatFixup ( string pstrOfY )
		{
			string strX = AdjustToMaximumWidth (            // Construct a composite Format Item.
				MINIMUM_VALID_FORMAT_ITEM_INDEX ,           // The string is for the 1st format item.
				pstrOfY.Length ,                            // Use this length in the Alignment component.
				Alignment.Right ,                           // Align text to the right. This is the default.
				INTEGER_PER_REG_SETTINGS );                 // Use this string for the Format component.
			return UpgradeFormatItem (                      // Upgrade the basic format item with the composite.
				Properties.Resources.X_OF_Y_FIXED_TEXT ,    // The format item is in this string.
				MINIMUM_VALID_FORMAT_ITEM_INDEX ,           // The item to replace is the item whose index is zero.
				strX );                                     // Use this composite string.
		}   // private static string XFormatFixup
		#endregion  // Private Static Utility Methods
	}   // public class FormatItem
}   // partial namespace WizardWrx.FormatStringEngine