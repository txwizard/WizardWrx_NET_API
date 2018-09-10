/*
    ============================================================================

    Module Name:        ReportDetail.cs

    Namespace Name:     WizardWrx

    Class Name:         ReportDetail

    Synopsis:           This class module defines an object used to manage one
                        of a set of detail items for a report.

    Remarks:            Since the details on a typical report are a mixture of
                        types, the default data field is cast to Object, but an
                        alternative version can be kept in a string.

                        Since this is tested code, I started the version number
                        at 2.0.

    License:            Copyright (C) 2012-2017, David A. Gray. 
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
    2012/11/19 2.0     DAG    This class is new, though the concept isn't.

    2014/06/29 2.3     DAG    1) Add GetPaddedLabel method.

                              2) Allow exceptions to be thrown when FormatDetail
                                 is called prematurely.

                              3) Move all messages from embedded constants to
                                 library resource strings.

    2014/07/19 2.5     DAG    1) Add DisplayFormat as an instance property that
                                 defaults to the current static DetailFormat.

                              2) Add DisplayOrder as an instance property that
                                 defaults to zero. To make DisplayOrder useful,
                                 implement the IComparable interface.

                              3) Add SupplementaryDetails property and a static
                                 companion, DetailFormatItems, and change the
                                 FormatDetail method to use it, if set and
                                 needed, to construct a report item.

                              4) Add a FormatDetail overload that uses an
                                 unsigned integer, such as the Count property of
                                 a populated ReportDetails collection, and the
                                 GetPaddedLabel method to render a neatly
                                 aligned list when the collection is enumerated.

                              5) BREAKING CHANGE ALERT: 1) GetPaddedLabel Method
                                                        2) GetPaddedValue Method

                                 Replace ReportDetails pdtlColl with unsigned
                                 integer (uint) puintWidthOfWidestValue.

                                 This change affects only one assembly outside
                                 the library source code tree. The ProgramParams
                                 class of PolyCat.exe is the only application to
                                 date of either method, which classified as For
                                 Internal Use Only. Although I used it to solve
                                 a problem for Kevin Cox, I ran it here, and
                                 sent him the report.

    2014/07/21 2.6     DAG    1) Reinstate the versions of GetPaddedLabel
                                 and GetPaddedValue that expect signed integers,
                                 to eliminate pointless casts back and forth.

                              2) To differentiate it from unsigned integers as
                                 report item values, create an ItemDisplayOrder
                                 structure, and cast the DisplayOrder property
                                 as one of these. The structure has one member,
                                 an unsigned integer.

                                 Since DisplayOrder was added in version 2.5,
                                 which I released just a couple of days ago, and
                                 hasn't been integrated into production code, it
                                 doesn't qualify as a breaking change.
 
	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

	2017/08/12 7.0     DAG    Move this class into WizardWrx.Core.dll, alongside
                              its siblings, so that WizardWrx.SharedUtl4.dll can
                              be retired.

	2017/09/07 7.0     DAG    Implement ItemDisplayOrder as a signe integer.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using WizardWrx;
using WizardWrx.FormatStringEngine;


namespace WizardWrx
{
    /// <summary>
    /// Instances of this class are generic report details, with labels and
    /// formats for printing them.
    /// </summary>
    public class ReportDetail : System.IComparable
    {
        #region Public Constants, Enumerations, and Structures
        /// <summary>
        /// This one-member structure prevents the Common Language Runtime from
        /// misdirecting the signed integer Value argument of a constructor,
        /// which might have a valid value of zero, into a constructor that
        /// expects a DisplayOrder value, which is prohibited from being zero.
        /// </summary>
        public struct ItemDisplayOrder
        {
            /// <summary>
            /// With some help from an IComparable implementation, this governs
            /// the order of appearance of items in the report.
            /// </summary>
            private int Order;

            /// <summary>
            /// Hide the display order value, a signed integer, from the CLR.
            /// </summary>
            /// <param name="itemdisplayorder">
            /// Specify the unsigned integer display order value to hide inside
            /// this structure.
            /// </param>
            public ItemDisplayOrder ( int itemdisplayorder )
            {
                Order = itemdisplayorder;
            }   // public ItemDisplayOrder constructor


            /// <summary>
            /// Implicitly convert an ItemDisplayOrder to its true type, which
            /// is signed integer.
            /// </summary>
            /// <param name="itemdisplayorder">
            /// Specify the ItemDisplayOrder to be implicitly cast to unsigned
            /// integer.
            /// </param>
            /// <returns>
            /// The return value is the signed integer that is wrapped inside
            /// this structure.
            /// </returns>
            static public implicit operator int ( ItemDisplayOrder itemdisplayorder )
            {
                return itemdisplayorder.Order;
            }   // static public implicit operator uint

            
            /// <summary>
            /// Explicitly convert an unsigned integer to the ItemDisplayOrder
            /// type that is intended to protect it from receiving signed
            /// integers that are intended to be treated as report values.
            /// </summary>
            /// <param name="pintItemdisplayorder">
            /// The return value is the input value, wrapped in a new
            /// ItemDisplayOrder structure.
            /// </param>
            /// <returns>
			/// The return value is an ItemDisplayOrder structure wrapped around
			/// the specified integer.
			/// </returns>
			/// <remarks>
			/// This operator must be explicit to coerce use of an explicit cast
			/// to steer the CLR to bind to the desired ReportDetail constructor.
			/// </remarks>
            static public explicit operator ItemDisplayOrder ( int pintItemdisplayorder )
            {
                return new ItemDisplayOrder ( pintItemdisplayorder );
            }   // static public implicit operator ItemDisplayOrder


            /// <summary>
            /// This method implements iComparable for instances of the 
            /// ItemDisplayOrder structure.
            /// </summary>
            /// <param name="itemDisplayOrder">
            /// Specify the ItemDisplayOrder to be compared against the current
            /// instance.
            /// </param>
            /// <returns>
            /// The return value is a standard CompareTo result, applied to the
            /// integers wrapped inside the two operands.
            /// </returns>
            internal int CompareTo ( ItemDisplayOrder itemDisplayOrder )
            {
                return this.Order.CompareTo ( itemDisplayOrder.Order );
            }   // internal int CompareTo
        }   // public ItemDisplayOrder structure


        /// <summary>
        /// This enumeration keeps track of the state of the instance.
        /// </summary>
        [Flags]
        public enum State : byte
        {
            /// <summary>
            /// The instance is empty.
            /// </summary>
            Empty = 0x00 ,

            /// <summary>
            /// The Label property has been set.
            /// </summary>
            HaveLabel = 0x01 ,

            /// <summary>
            /// The Value property has been set.
            /// </summary>
            HaveValueObject = 0x02 ,

            /// <summary>
            /// The DisplayValue property has been set.
            /// </summary>
            HaveValueString = 0x04 ,

            /// <summary>
            /// The ListOrder property has been set.
            /// </summary>
            HaveListOrder = 0x08 ,

            /// <summary>
            /// Either the SupplementaryDetails property is set, or it can 
            /// inherit an array from the static DetailFormatItems property.
            /// </summary>
            HaveFormatItems = 0x10 ,

            /// <summary>
            /// One or both of the DsiplayValue and Value properties has been set.
            /// </summary>
            HaveValue = HaveValueObject | HaveValueString ,

            /// <summary>
            /// The Label property has been set, as has either or both of the Value properties.
            /// </summary>
            HaveLabelAndValue = HaveLabel | HaveValue
        }   // State


        /// <summary>
        /// Use this format for reports, unless the caller overrides it.
        /// </summary>
        public const string DEFAULT_FORMAT = @"{0} = {1}";


        /// <summary>
        /// When the FormatDetail method is called before the object is fully
        /// initialized, throw an System.InvalidOperationException exception.
        /// </summary>
        public const bool THROW_ON_INVALID_STATE = true;


        /// <summary>
        /// When the FormatDetail method is called before the object is fully
        /// initialized, return an error message. Since the returned value is
        /// expected to go into a file of some kind (which may be a print file),
        /// this is a much cheaper way of handling the exception. However, as is
        /// almost always true, there is a cost, because throwing an exception
        /// typically causes the application to return a nonzero exit code, but
        /// writing it into a file yields an exit code of zero.
        /// </summary>
        public const bool RETURN_ON_INVALID_STATE = false;
        #endregion  // Public Constants, Enumerations, and Structures


        #region Public Storage Shared by All Instances
        /// <summary>
        /// The state of this flag determines what happens when FormatDetail is
        /// called when the object is partially initialized.
        /// 
        /// The default action, False (RETURN_ON_INVALID_STATE) is the behavior
        /// originally programmed into it.
        /// 
        /// When set to True, (THROW_ON_INVALID_STATE), the message that would
        /// have been returned becomes the Message property of an 
        /// System.InvalidOperationException exception.
        /// </summary>
        public static bool ThrowOnInvalidState { get; set; }
        #endregion  // Public Storage Shared by All Instances


        #region Private Storage Shared by All Instances
        private static string s_strFormat = DEFAULT_FORMAT;
        private static object [ ] s_aobjDefaultSupplementaryDetails = null;
        #endregion  // Private Storage Shared by All Instances


        #region Private Storage for Class Instance
        private State _enmState = State.Empty;
        private string _strLabel = null;
        private object _objValue = null;
        private string _strDisplayValue = null;
        private string _strDisplayFormat = null;
        private ItemDisplayOrder _utptDisplayOrder;
        private object [ ] _aobjSupplementaryDetails = null;
        #endregion  // Private Storage for Class Instance


        #region LabelChanged Event Infrastructure
        /// <summary>
        /// The LabelChanged event needs to pass two integers to the event sink.
        /// </summary>
        public class LabelChangedEventArgs : EventArgs
        {
            private readonly int _intNewLength;
            private readonly int _intOldLength;


            /// <summary>
            /// LabelChangedEventArgs has one public constructor, which creates
            /// and fully initializes the instance. 
            /// </summary>
            /// <param name="pintNewLength">
            /// The new length can't simply be added.
            /// </param>
            /// <param name="pintOldLength">
            /// The old length can bear any relation to the new length.
            /// </param>
            public LabelChangedEventArgs ( 
                int pintNewLength ,
                int pintOldLength )
            {
                _intNewLength = pintNewLength;
                _intOldLength = pintOldLength;
            }   // public LabelChangedEventArgs

            /// <summary>
            /// The new length is added to the accumulated total.
            /// </summary>
            public int NewLength { get { return _intNewLength; } }

            /// <summary>
            /// The old length is subtracted from the total.
            /// </summary>
            public int OldLength { get { return _intOldLength; } }
        }   // public class LabelChangedEventArgs : EventArgs


        /// <summary>
        /// Delegates register here.
        /// </summary>
        public event EventHandler LabelChanged;


        /// <summary>
        /// Raise this event to signal listeners that the length of the label
        /// changed.
        /// </summary>
        /// <param name="e">
        /// The LabelChangedEventArgs method is populated with the new and old
        /// label length, so that only the difference is added to the total.
        /// </param>
        protected virtual void OnLabelChanged ( LabelChangedEventArgs e )
        {   // Check for registered objects. If there are any, notify them.
            if ( LabelChanged != null )
                LabelChanged ( this , e );
        }   // protected virtual void OnLabelChanged
        #endregion  // LabelChanged Event Infrastructure


        #region Constructors, All Public
        /// <summary>
        /// The default constructor creates an empty ReportDetail.
        /// </summary>
		public ReportDetail ( )
		{
			if ( s_aobjDefaultSupplementaryDetails != null )
				_enmState = _enmState | State.HaveFormatItems;
		} // Default constructor (1 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label property.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        public ReportDetail ( string label )
        {   // Let the property initializer validate it.
            this.Label = label;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (2 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label and DisplayOrder
        /// properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        public ReportDetail (
            string label ,
            ItemDisplayOrder itemdisplayorder )
        {
            this.Label = label;
            this.DisplayOrder = itemdisplayorder;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (3 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label, DisplayOrder, and 
        /// DisplayFormat, and SupplementaryDetails properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        public ReportDetail (
            string label ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat )
        {
            this.Label = label;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (4 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label, DisplayOrder, 
        /// DisplayFormat properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        /// <param name="supplementarydetails">
        /// Override the default SupplementaryDetails property.
        /// </param>
        public ReportDetail (
            string label ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat ,
            object [ ] supplementarydetails )
        {
            this.Label = label;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;
            this.SupplementaryDetails = supplementarydetails;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (5 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label and Value properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="pobjValue">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        public ReportDetail (
            string label ,
            object pobjValue )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.Value = pobjValue;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (6 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label and Value properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="value">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        public ReportDetail (
            string label ,
            object value ,
            ItemDisplayOrder itemdisplayorder )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.Value = value;
            this.DisplayOrder = itemdisplayorder;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (7 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label, Value, DisplayOrder, and
        /// DisplayFormat properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="value">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        public ReportDetail (
            string label ,
            object value ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.Value = value;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (8 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label, Value, DisplayOrder,
        /// DisplayFormat, and SupplementaryDetails properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="value">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        /// <param name="supplementarydetails">
        /// Override the default SupplementaryDetails property.
        /// </param>
        public ReportDetail (
            string label ,
            object value ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat ,
            object [ ] supplementarydetails )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.Value = value;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;
            this.SupplementaryDetails = supplementarydetails;
        }   // public ReportDetail (9 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label and DisplayValue properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        public ReportDetail (
            string label , 
            string displayvalue )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.DisplayValue = displayvalue;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (10 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label and DisplayValue properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        public ReportDetail (
            string label ,
            string displayvalue ,
            ItemDisplayOrder itemdisplayorder )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.DisplayValue = displayvalue;
            this.DisplayOrder = itemdisplayorder;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (11 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label, DisplayValue,
        /// DisplayOrder, and DisplayFormat properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        public ReportDetail (
            string label ,
            string displayvalue ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.DisplayValue = displayvalue;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (12 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label, DisplayValue,
        /// DisplayOrder, DisplayFormat, and SupplementaryDetails properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        /// <param name="supplementarydetails">
        /// Override the default SupplementaryDetails property.
        /// </param>
        public ReportDetail (
            string label ,
            string displayvalue ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat ,
            object [ ] supplementarydetails )
        {   // Let the property initializers validate them.
            this.Label = label;
            this.DisplayValue = displayvalue;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;
            this.SupplementaryDetails = supplementarydetails;
        }   // public ReportDetail (13 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label. native value, and
        /// DisplayValue properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="pobjValue">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        public ReportDetail (
            string label ,
            object pobjValue ,
            string displayvalue )
        {
            this.Label = label;
            this.Value = pobjValue;
            this.DisplayValue = displayvalue;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (14 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label. native value, 
        /// DisplayValue, and DisplayOrder properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="pobjValue">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        public ReportDetail (
            string label ,
            object pobjValue ,
            string displayvalue ,
            ItemDisplayOrder itemdisplayorder )
        {
            this.Label = label;
            this.Value = pobjValue;
            this.DisplayValue = displayvalue;
            this.DisplayOrder = itemdisplayorder;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (15 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label. native value, 
        /// DisplayValue, DisplayOrder, and DisplayFormat properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="pobjValue">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        public ReportDetail (
            string label ,
            object pobjValue ,
            string displayvalue ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat )
        {
            this.Label = label;
            this.Value = pobjValue;
            this.DisplayValue = displayvalue;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;

            if ( s_aobjDefaultSupplementaryDetails != null )
                _enmState = _enmState | State.HaveFormatItems;
        }   // public ReportDetail (16 of 17)


        /// <summary>
        /// Create a ReportDetail, and set its Label. native value, 
        /// DisplayValue, DisplayOrder, DisplayFormat, and SupplementaryDetails
        /// properties.
        /// </summary>
        /// <param name="label">
        /// Initial value for label, which can be neither null, nor empty.
        /// </param>
        /// <param name="pobjValue">
        /// Initial data value, as an Object, which MAY be a null reference.
        /// </param>
        /// <param name="displayvalue">
        /// Initial string representation of data value, which MAY be a null
        /// reference or an empty string.
        /// </param>
        /// <param name="itemdisplayorder">
        /// DisplayOrder must be greater than zero.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// </param>
        /// <param name="supplementarydetails">
        /// Override the default SupplementaryDetails property.
        /// </param>
        public ReportDetail (
            string label ,
            object pobjValue ,
            string displayvalue ,
            ItemDisplayOrder itemdisplayorder ,
            string displayformat ,
            object [ ] supplementarydetails )
        {
            this.Label = label;
            this.Value = pobjValue;
            this.DisplayValue = displayvalue;
            this.DisplayOrder = itemdisplayorder;
            this.DisplayFormat = displayformat;
            this.SupplementaryDetails = supplementarydetails;
        }   // public ReportDetail (17 of 17)
        #endregion  // Constructors, All Public


        #region Properties, Read/Write, Save One
        /// <summary>
        /// Report on the state of the instance, for evaluating its readiness
        /// for use on a report.
        /// </summary>
        public State DetailState { get { return _enmState; } }


        /// <summary>
        /// Get or set the default format string to use with the label and value
        /// to display an item on a report. See remarks for critical
        /// information.
        /// </summary>
        /// <remarks>
        /// The first three format items, {0}, {1}, and {2} are reserved for the
        /// label, value, and item number. Additional format items, if any, are
        /// populated from the objects in the SupplementaryDetails array.
        /// 
        /// The FormatDetail method creates a new object array with enough room
        /// to hold the SupplementaryDetails array, plus three, fills it by
        /// inserting the label, value, and item number into the first three
        /// slots, and appending the SupplementaryDetails array, if it exists,
        /// and passes the whole array to string.Format.
        /// 
        /// Unless its value differs from the current static (default) value,
        /// this property stays NULL. When callers query this property, the
        /// static property is returned if the instance property is null. Hence,
        /// the property behaves like a instance property, without wasting space
        /// to store duplicates of the default value. Hence, output routines
        /// need not check both properties.
        /// </remarks>
        public string DisplayFormat
        {
            get
            {
                if ( _strDisplayFormat == null )
                    return s_strFormat;
                else
                    return _strDisplayFormat;
            }   // public string DisplayFormat property Get method

            set
            {
                if ( !string.IsNullOrEmpty ( value ) )
                    if ( value != s_strFormat )
                        _strDisplayFormat = value;
            }   // public string DisplayFormat property Set method
        }   // public string DisplayFormat property


        /// <summary>
        /// This property gives read/write access to the SupplementaryDetails
        /// property, an array of Objects for use with the DisplayFormat
        /// property. See remarks for critical information.
        /// </summary>
        /// <seealso cref="DisplayFormat"/>
        public object [ ] SupplementaryDetails
        {
            get
            {
                if ( _aobjSupplementaryDetails == null )
                    return s_aobjDefaultSupplementaryDetails;
                else
                    return _aobjSupplementaryDetails;
            }   // public object [ ] SupplementaryDetails property Get method

            set
            {
                _aobjSupplementaryDetails = value;

                // HACK: I know this should be one statement, but I can't find a recent example to refresh my memory.
                if ( _aobjSupplementaryDetails == null )
                {   // Cast _enmState to a BitArray32, and use its BitOff method to clear the HaveFormatItems bit.
                    BitArray32 b32State = ( WizardWrx.BitArray32 ) ( ( uint ) _enmState );
                    b32State.BitOff ( ( int ) State.HaveFormatItems );
                }   // TRUE block, if ( _aobjSupplementaryDetails == null )
                else
                {   // Turning it ON is straightforward.
                    _enmState = _enmState | State.HaveFormatItems;
                }   // FALSE block , if ( _aobjSupplementaryDetails == null )
            }   // // public object [ ] SupplementaryDetails property Set method
        }   // public object [ ] SupplementaryDetails property


        /// <summary>
        /// Get or set the unsigned integer that determines the order in which
        /// this item is returned from a sorted collection.
        /// </summary>
        public ItemDisplayOrder DisplayOrder
        {
            get { return _utptDisplayOrder; }

            set
            {
                if ( ( int ) value == MagicNumbers.ZERO )
                    throw new ArgumentOutOfRangeException (
                        Core.Properties.Resources.ERRMSG_DISPLAY_ORDER_CANNOT_BE_ZERO );

                _utptDisplayOrder = value;
                _enmState = _enmState | State.HaveListOrder;
            }   // public uint DisplayOrder setter method
        }   // public uint DisplayOrder


        /// <summary>
        /// Get or set the string representation of data value, which MAY be a 
        /// null reference or an empty string.
        /// </summary>
        public string DisplayValue
        {
            get { return _strDisplayValue; }

            set
            {
                _strDisplayValue = value;
                _enmState = _enmState | State.HaveValueString;
            }   // public string DisplayValue property Set method
        }   // public string DisplayValue property


        /// <summary>
        /// New value for label, which can be neither null, nor empty.
        /// </summary>
        public string Label
        {
            get { return _strLabel; }

            set
            {
                if ( string.IsNullOrEmpty ( value ) )
                {   // Reject null or empty labels.
                    throw new ArgumentNullException (
                        Core.Properties.Resources.ERRMSG_SPECIFIED_LABEL_IS_NULL_OR_BLANK );
                }   // TRUE (UNexpected outcome) block, if ( string.IsNullOrEmpty ( value ) )
                else
                {   // Decide what to send with the event.
                    if ( string.IsNullOrEmpty ( _strLabel ) )
                    {   // Tell delegate to count the whole length.
                        OnLabelChanged ( new LabelChangedEventArgs (
                            value.Length ,
                            MagicNumbers.ZERO ) );
                    }   // TRUE (value being set for first time) block, if ( string.IsNullOrEmpty ( _strLabel ) )
                    else
                    {   // Tell delegate to count only the difference between the old and new lengths, which can be negative if the label shrank.
                        OnLabelChanged ( new LabelChangedEventArgs (
                            value.Length ,
                            _strLabel.Length ) );
                    }   // FALSE (value being changed) block, if ( string.IsNullOrEmpty ( _strLabel ) )

                    _strLabel = value;
                    _enmState = _enmState | State.HaveLabel;
                }   // FALSE (expected outcome) block, if ( string.IsNullOrEmpty ( value ) )
            }   // public string Label property set method
        }   // public string Label property


        /// <summary>
        /// Gets or sets the data value, as an Object, which MAY be a null
        /// reference.
        /// 
        /// Please see Remarks for important details.
        /// </summary>
        /// <remarks>
        /// To make the class a tad more robust, if the rhe property is null,
        /// the DisplayValue is returned, unless it is also null. This gets
        /// around an ambiguity that causes the constructor to put a String into
        /// the DisplayValue property, unless it is downcast to Object.
        /// </remarks>
        public object Value
        {
            get
            {
                if ( _objValue == null )
                    if ( _strDisplayValue == null )
                        return _objValue;
                    else
                        return _strDisplayValue;
                else
                    return _objValue;
            }   // public object Value property get method

            set
            {
                _objValue = value;
                _enmState = _enmState | State.HaveValueObject;
            }   // public object Value property set method
        }   // public object Value property
        #endregion  // Properties, Read/Write, Save One


        #region Instance Methods
        /// <summary>
        /// Return a formatted detail record, ready to print.
        /// </summary>
        /// <returns>
        /// The return value is a formatted string, if a label and value are
        /// present. Otherwise, an error message is returned for printing.
        /// </returns>
        public string FormatDetail ( )
        {
            if ( TestState ( State.HaveLabel ) )
                return FormatLabelAndValue (
                    ListInfo.EMPTY_STRING_LENGTH ,					// Width of longest label is unknown.
                    MagicNumbers.ZERO );										// Total number of items in list is unknown.
            else
                return ReportMissingLabel ( );
        }   // public string FormatDetail ( ) (1 of 3)


        /// <summary>
        /// Return a formatted detail record, ready to print.
        /// </summary>
        /// <param name="puintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <returns>
        /// The return value is a string that can be fed directly to a WriteLine
        /// method.
        /// </returns>
        public string FormatDetail ( uint puintWidthOfWidestLabel )
        {
            if ( TestState ( State.HaveLabel ) )
                return FormatLabelAndValue (
                    ( int ) puintWidthOfWidestLabel ,
                    MagicNumbers.ZERO );										// Total number of items in list is unknown.
            else
                return ReportMissingLabel ( );
        }   // public string FormatDetail ( ) (2 of 3)


        /// <summary>
        /// Return a formatted detail record, ready to print.
        /// </summary>
        /// <param name="puintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <param name="puintTotalitems">
        /// Specify the total number of items in the list, which becomes part of
        /// the third format item, {2}, in the detail item format string.
        /// </param>
        /// <returns>
        /// The return value is a string that can be fed directly to a WriteLine
        /// method.
        /// </returns>
        public string FormatDetail (
            uint puintWidthOfWidestLabel ,
            uint puintTotalitems )
        {
            if ( TestState ( State.HaveLabel ) )
                return FormatLabelAndValue (
                    ( int ) puintWidthOfWidestLabel ,
                    MagicNumbers.ZERO );										// Total number of items in list is unknown.
            else
                return ReportMissingLabel ( );
        }   // public string FormatDetail ( ) (3 of 3)


        /// <summary>
        /// Return a formatted detail record, ready to print.
        /// </summary>
        /// <param name="pintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <returns>
        /// The return value is a string that can be fed directly to a WriteLine
        /// method.
        /// </returns>
        public string FormatDetail ( int pintWidthOfWidestLabel )
        {
            if ( TestState ( State.HaveLabel ) )
                return FormatLabelAndValue (
                    pintWidthOfWidestLabel ,
                    MagicNumbers.ZERO );
            else
                return ReportMissingLabel ( );
        }   // public string FormatDetail ( ) (4 of 5)


        /// <summary>
        /// Return a formatted detail record, ready to print.
        /// </summary>
        /// <param name="pintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <param name="puintTotalitems">
        /// Specify the total number of items in the list, which becomes part of
        /// the third format item, {2}, in the detail item format string.
        /// </param>
        /// <returns>
        /// The return value is a string that can be fed directly to a WriteLine
        /// method.
        /// </returns>
        public string FormatDetail (
            int pintWidthOfWidestLabel ,
            uint puintTotalitems )
        {
            if ( TestState ( State.HaveLabel ) )
                return FormatLabelAndValue (
                    pintWidthOfWidestLabel ,
                    MagicNumbers.ZERO );										// Total number of items in list is unknown.
            else
                return ReportMissingLabel ( );
        }   // public string FormatDetail ( ) (5 of 5)
        

        /// <summary>
        /// Return a padded label string.
        /// </summary>
        /// <param name="pintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <returns>
        /// If this instance has a label, pad it as indicated, otherwise, behave
        /// as if FormatDetail got the call.
        /// </returns>
        public string GetPaddedLabel ( int pintWidthOfWidestLabel )
        {
            return FormatLabelWithPadding ( pintWidthOfWidestLabel );
        }   // public string GetPaddedLabel (1 of 2)


        /// <summary>
        /// Return a padded label string.
        /// </summary>
        /// <param name="puintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <returns>
        /// If this instance has a label, pad it as indicated, otherwise, behave
        /// as if FormatDetail got the call.
        /// </returns>
        public string GetPaddedLabel ( uint puintWidthOfWidestLabel )
        {
            return FormatLabelWithPadding ( ( int ) puintWidthOfWidestLabel );
        }   // public string GetPaddedLabel (2 of 2)


        /// <summary>
        /// Return an aligned field item string.
        /// </summary>
        /// <param name="pintWidthOfWidestValue">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestValue property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <param name="penmAlignment">
        /// Specify Left or Right. Format items, per se, don't support center
        /// alignment.
        /// </param>
        /// <param name="pstrFormatString">
        /// Specify a Standard Numeric, Date, or Text format string or a custom
        /// string composed around a standard string.
        /// </param>
        /// <returns>
        /// The returned string is ready to insert into a complete format string
        /// for use with string.Format, Console.WriteLine, and their numerous
        /// cousins.
        /// </returns>
        public string GetPaddedValue (
            int pintWidthOfWidestValue ,
            FormatItem.Alignment penmAlignment ,
            string pstrFormatString )
        {
            return FormatValueWithPadding (
                pintWidthOfWidestValue ,
                penmAlignment ,
                pstrFormatString );
        }   // public string GetPaddedValue (1 of 2)


        /// <summary>
        /// Return an aligned field item string.
        /// </summary>
        /// <param name="puintWidthOfWidestValue">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestValue property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <param name="penmAlignment">
        /// Specify Left or Right. Format items don't support center alignment.
        /// </param>
        /// <param name="pstrFormatString">
        /// Specify a Standard Numeric, Date, or Text format string or a custom
        /// string composed around a standard string.
        /// </param>
        /// <returns>
        /// The returned string is ready to insert into a complete format string
        /// for use with string.Format, Console.WriteLine, and their numerous
        /// cousins.
        /// </returns>
        public string GetPaddedValue (
            uint puintWidthOfWidestValue ,
            FormatItem.Alignment penmAlignment ,
            string pstrFormatString )
        {
            return FormatValueWithPadding (
                ( int ) puintWidthOfWidestValue ,
                penmAlignment ,
                pstrFormatString );
        }   // public string GetPaddedValue (2 of 2)


        /// <summary>
        /// Determine whether the condition represented by a member of the State
        /// enumeration is true.
        /// </summary>
        /// <param name="penmState">
        /// The member of the State enumeration to evaluate.
        /// </param>
        /// <returns>
        /// True if State is true, else False.
        /// </returns>
        public bool TestState ( State penmState )
        {
            return ( _enmState & penmState ) == penmState;
        }   // public bool TestState
        #endregion  // Instance Methods


        #region Static Properties
        /// <summary>
        /// This property gives read/write access to the DetailFormat property,
        /// a string that is shared by all instances. See remarks for critical
        /// information.
        /// </summary>
        /// <remarks>
        /// The first two format items, {0} and {1}, are reserved for the label
        /// and value properties. Additional format items, if any, may be filled
        /// from the DetailFormatItems array.
        /// </remarks>
        public static string DetailFormat
        {
            get { return s_strFormat; }
            set
            {
                if ( string.IsNullOrEmpty ( value ) )
                    throw new ArgumentNullException (
                        Core.Properties.Resources.ERRMSG_DETAIL_FORMAT_CANNOT_BE_NULL_OR_BLANK );
                else
                    s_strFormat = value;
            }   // public static string DetailFormat property Set method
        }   // public static string DetailFormat property


        /// <summary>
        /// This property gives read/write access to the DetailFormatItems
        /// property, an array of Objects that is shared by all instances. See 
        /// remarks for critical information.
        /// </summary>
        /// <remarks>
        /// The first two format items, {0} and {1}, are reserved for the label
        /// and value properties. Additional format items, if any, may be filled
        /// from this array.
        /// 
        /// The instance message formatter takes items from this array as needed
        /// to fill the remaining format items.
        /// </remarks>
        public static object [ ] DetailFormatItems
        {
            get { return s_aobjDefaultSupplementaryDetails; }
            set { s_aobjDefaultSupplementaryDetails = value; }
        }   // public static object [ ] DetailFormatItems
        #endregion  // Static Properties


        #region Private Instance Methods
        /// <summary>
        /// This method encapsulates the code that runs in both FormatDetail
        /// methods unless the label property is uninitialized.
        /// </summary>
        /// <param name="pintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <param name="puintTotalitems">
        /// Specify the total number of items in the list, which becomes part of
        /// the third format item, {2}, in the detail item format string.
        /// </param>
        /// <returns>
        /// The returned string can be passed directly to any WriteLine method.
        /// </returns>
        private string FormatLabelAndValue (
            int pintWidthOfWidestLabel ,
            uint puintTotalitems )
        {
            if ( TestState ( State.HaveValueString ) )
                return string.Format (
                    this.DisplayFormat ,                            // Format string (template)
                    GatherFormatItemValues (                        // Gather everything into one array of Objects.
                        SelectLabel ( pintWidthOfWidestLabel ) ,    // Format Item 0 = Item Label string
                        _strDisplayValue ,                          // Format Item 1 = Item Value
                        puintTotalitems ) );                        // Format Item 2 = Item ordinal (of puintTotalitems, if puintTotalitems > 0)
            else if ( TestState ( State.HaveValueObject ) )
                return string.Format (
                    this.DisplayFormat ,                            // Format string (template)
                    GatherFormatItemValues (                        // Gather everything into one array of Objects.
                        SelectLabel ( pintWidthOfWidestLabel ) ,    // Format Item 0 = Item Label string
                        _objValue.ToString ( ) ,                    // Format Item 1 = Item Value
                        puintTotalitems ) );                        // Format Item 2 = Item ordinal
            else
                if ( ReportDetail.ThrowOnInvalidState )
                    throw new System.InvalidOperationException (
                        Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_NULL );
                else
                    return Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_NULL;
        }   // private string FormatLabelAndValue


        /// <summary>
        /// This routine implements both overloads of GetPaddedLabel.
        /// </summary>
        /// <param name="pintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <returns>
        /// If this instance has a label, pad it as indicated, otherwise, behave
        /// as if FormatDetail got the call.
        /// </returns>
        private string FormatLabelWithPadding ( int pintWidthOfWidestLabel )
        {
            if ( TestState ( State.HaveLabel ) )
                return _strLabel.PadRight ( pintWidthOfWidestLabel );
            else
                if ( ReportDetail.ThrowOnInvalidState )
                    throw new System.InvalidOperationException (
                        Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_UNLABELED );
                else
                    return Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_UNLABELED;
        }   // private string FormatLabelWithPadding


        /// <summary>
        /// This private method implements both overloads of GetPaddedValue.
        /// </summary>
        /// <param name="pintWidthOfWidestValue">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestValue property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <param name="penmAlignment">
        /// Specify Left or Right. Format items, per se, don't support center
        /// alignment.
        /// </param>
        /// <param name="pstrFormatString">
        /// Specify a Standard Numeric, Date, or Text format string or a custom
        /// string composed around a standard string.
        /// </param>
        /// <returns>
        /// The returned string is ready to insert into a complete format string
        /// for use with string.Format, Console.WriteLine, and their numerous
        /// cousins.
        /// </returns>
        private string FormatValueWithPadding (
            int pintWidthOfWidestValue ,
            FormatItem.Alignment penmAlignment ,
            string pstrFormatString )
        {
            if ( TestState ( State.HaveValueString ) )
                return string.Format (
                    FormatItem.AdjustToMaximumWidth (
                        ArrayInfo.ARRAY_FIRST_ELEMENT ,
                        pintWidthOfWidestValue ,
                        penmAlignment ,
                        SpecialStrings.EMPTY_STRING ) ,
                    _strDisplayValue );
            else if ( TestState ( State.HaveValueObject ) )
                return string.Format (
                    FormatItem.AdjustToMaximumWidth (
                        ArrayInfo.ARRAY_FIRST_ELEMENT ,
                        pintWidthOfWidestValue ,
                        penmAlignment ,
                        pstrFormatString ) ,
                    _objValue );
            else
                return Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_NULL;
        }   // private string FormatValueWithPadding


        /// <summary>
        /// Gather the label, value, item number, and extra items, if any, into
        /// one array of objects.
        /// </summary>
        /// <param name="label">
        /// Specify the field label, which replaces the first format item, {0}.
        /// </param>
        /// <param name="displayvalue">
        /// Specify the field value, which replaces the second format item, {1}.
        /// </param>
        /// <param name="puintTotalitems">
        /// Specify the total number of items in the list, or zero to suppress
        /// its inclusion in the report details.
        /// </param>
        /// <returns>
        /// The return value is an array of generic Object variables, ready to
        /// pass into the static string.Format method, or any of the others that
        /// have the same signature.
        /// </returns>
        private object [ ] GatherFormatItemValues (
            string label ,
            string displayvalue ,
            uint puintTotalitems )
        {
            const int ITEM_FOR_LABEL = ArrayInfo.ARRAY_FIRST_ELEMENT;
            const int ITEM_FOR_VALUE = MagicNumbers.PLUS_ONE;
            const int ITEM_FOR_ORDINAL = MagicNumbers.PLUS_TWO;
			const int RESERVE_FOR_STANDARD_FIELDS = ITEM_FOR_ORDINAL + ArrayInfo.ORDINAL_FROM_INDEX;
                
            //  ----------------------------------------------------------------
            //  Obscure Syntax Alert:
            //
            //  The following obscure expression replaces an IF statement.
            //
            //      ( ( _aobjSupplementaryDetails == null ) ? 0 : _aobjSupplementaryDetails.Length )
            //
            //  If _aobjSupplementaryDetails is null (uninitialized), the value
            //  of the expression is zero. Otherwise, its value is the length of
            //  array _aobjSupplementaryDetails.
            //  ----------------------------------------------------------------

            object [ ] raobjEverything = new object [ RESERVE_FOR_STANDARD_FIELDS + ( ( _aobjSupplementaryDetails == null ) ? 0 : _aobjSupplementaryDetails.Length ) ];

            raobjEverything [ ITEM_FOR_LABEL ] = label;
            raobjEverything [ ITEM_FOR_VALUE ] = displayvalue;
            raobjEverything [ ITEM_FOR_ORDINAL ] = FormatItem.XofY (
                ( int ) _utptDisplayOrder ,
                puintTotalitems );

            if ( _aobjSupplementaryDetails != null )
                _aobjSupplementaryDetails.CopyTo (
                    raobjEverything ,
                    RESERVE_FOR_STANDARD_FIELDS );

            return raobjEverything;
        }   // private string GatherFormatItemValues


        /// <summary>
        /// If the ReportDetails is initialized (not null), return a padded
        /// label. Otherwise, return a unpadded label.
        /// </summary>
        /// <param name="pintWidthOfWidestLabel">
        /// Specify the length, in characters, of the longest string 
        /// representation of all items in a collection. The intent is that the
        /// details for a report go into a ReportDetails collection, which has a
        /// WidthOfWidestLabel property that was originally acquired indirectly,
        /// through a reference to the collection that originally occupied this
        /// slot in the argument list.
        /// </param>
        /// <returns>
        /// if puintWidthOfWidestLabel is greater than zero, GetPaddedLabel is
        /// called on this instance, passing the puintWidthOfWidestLabel value
        /// for its use. Otherwise, the label stored in the instance is returned
        /// as is.
        /// </returns>
        private string SelectLabel ( int pintWidthOfWidestLabel )
        {
			if ( pintWidthOfWidestLabel == ListInfo.EMPTY_STRING_LENGTH )
				return _strLabel;
			else
				return this.GetPaddedLabel ( pintWidthOfWidestLabel );
        }   // private string SelectLabel
        #endregion  // Private Instance Methods


        #region Private Static Methods
        /// <summary>
        /// Either return a message, or put it into an System.InvalidOperationException
        /// exception if the ThrowOnInvalidState property is set to TRUE. 
        /// 
        /// Please see the associated Remarks topic.
        /// </summary>
        /// <returns>
        /// Unless ThrowOnInvalidState is true, return a message that takes the
        /// place of the expected message on the report.
        /// 
        /// Please see the associated Remarks topic.
        /// </returns>
        /// <remarks>
        /// When this happens, look for your error in the routine that created
        /// the instance that reported the error. 
        /// 
        /// If  the static ThrowOnInvalidState property is True, your program
        /// throws an System.InvalidOperationException exception. Otherwise, the same
        /// message that would have gone into the exception report is recorded
        /// on the report.
        /// </remarks>
        private static string ReportMissingLabel ( )
        {
            if ( ReportDetail.ThrowOnInvalidState )
                throw new System.InvalidOperationException (
                    Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_UNLABELED );
            else
                return Core.Properties.Resources.ERRMSG_REPORT_ITEM_IS_UNLABELED;
        }   // private static string ReportMissingLabel
        #endregion  // Private Static Methods


        #region IComparable Members
        int IComparable.CompareTo ( object pobjComparand )
        {
            if ( pobjComparand.GetType ( ) == typeof ( ReportDetail ) )
            {
                ReportDetail dtl = ( ReportDetail ) pobjComparand;
                return _utptDisplayOrder.CompareTo ( dtl._utptDisplayOrder );
            }   // TRUE (normal) block, if ( pobjComparand.GetType ( ) == typeof ( ReportDetail ) )
            else
            {
                string strMsg = string.Format (
                    Core.Properties.Resources.ERRMSG_COMPARAND_IS_WRONG_TYPE ,
                    pobjComparand.GetType ( ) ,
                    typeof ( ReportDetail ) ,
                    Environment.NewLine );
                throw new System.InvalidOperationException ( strMsg );
            }   // FALSE (exception) block, if ( pobjComparand.GetType ( ) == typeof ( ReportDetail ) )
        }   // int IComparable.CompareTo
        #endregion  // IComparable Members
    }   // public class ReportDetail
}   // partial namespace WizardWrx