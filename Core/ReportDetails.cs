/*
    ============================================================================

    Module Name:        ReportDetails.cs

    Namespace Name:     WizardWrx

    Class Name:         ReportDetails

    Synopsis:           This class module defines an object used to manage one
                        of a set of detail items for a report.

    Remarks:            Since the details on a typical report are a mixture of
                        types, the default data field is cast to Object, but an
                        alternative version can be kept in a string.

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

    2014/06/29 2.3     DAG    Add the following methods.

                              1) ComputeWidthofWidestLabel

                              2) ComputeWidthOfWidestValue

    2014/07/17 2.5     DAG    1) Inherit from List<T>, rather than ArrayList, 
                                 override the Add method on the base class to 
                                 automatically increment a newDisplayOrder
                                 property on each ReportDetail as it is added to
                                 the collection, and add a static Read/Write 
                                 property to store a default increment for it.

                              2) Rename the ComputeWidthofWidestLabel method to
                                 WidthOfWidestLabel, convert it from a method
                                 to a read only property, and change its return
                                 type from int to uint.

                              3) Rename the ComputeWidthOfWidestValue method to
                                 WidthOfWidestValue, convert it from a method
                                 to a read only property, and change its return
                                 type from int to uint.

                              4) Add ListAllItems and ListAllItemsInArray 
                                 methods.

                                 a) ListAllItems is overloaded. Called without
                                    arguments, it lists items on the system
                                    console. Called with a reference to a
                                    TextWriter, it calls its WriteLine method to
                                    append the report to it.

                                 b) ListAllItemsInArray resembles ListAllItems,
                                    except that it transforms the items into an
                                    array of strings, which it returns to the
                                    calling routine for further processing.

    2014/07/21 2.6     DAG    Add overloads of properties WidthOfWidestLabel and
                              WidthOfWidestValue that return signed integers,
                              leaving the originals that return unsigned values
                              for backward compatibility. This decision is based
                              on the realization that, since length is reported
                              as a signed integer, I am wasting quite a few cast
                              operations, each of which involves a 4 byte memory
                              copy, via a register.

                              Of greater significance is stashing a copy of a
                              property value that may be used twice in some or
                              all iterations of a loop.

	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

	2017/08/12 7.0     DAG    Move this class into WizardWrx.Core.dll, alongside
                              its siblings, so that WizardWrx.SharedUtl4.dll can
                              be retired.

	2017/09/05 7.0     DAG    Correct an error that caused a display order of -1
                              to be accepted at face value, rather than replaced
                              by the automatic increment algorithm.

	2017/09/07 7.0     DAG    Implement ItemDisplayOrder as a signe integer.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace WizardWrx
{
    /// <summary>
    /// This class holds the details for a report. Static object locks are used
    /// throughout to ensure synchronized access.
    /// </summary>
    public class ReportDetails : List<ReportDetail>
    {
        #region Constants and Other Static Entities
        /// <summary>
        /// The default increment value is 100. With automatic incrementation,
        /// this is almost certainly overkill. On the other hand, the
        /// applications that I anticipate for this class should have plenty of
        /// headroom in the provided unsigned integer.
        /// </summary>
        public const int DEFAULT_DISPLAY_ORDER_INCREMENT = 100;


		static SyncRoot s_lwlPrivate = new SyncRoot ( typeof ( ReportDetails ).GUID.ToString ( ) );
        #endregion  // Constants and Other Static Entities


        #region Overridden Methods of Base Class
        /// <summary>
        /// Override the New method in the base class, because it's lots cheaper
        /// than using an event.
        /// </summary>
        /// <param name="prDtl">
        /// The ReportDetail item to add to the collection.
        /// </param>
		public new void Add ( ReportDetail prDtl )
		{
			lock ( s_lwlPrivate )
			{   // Make safe for threads.
				base.Add ( prDtl );

				//  ------------------------------------------------------------
				//  My initial implementation set the DisplayOrder on the new
				//  ReportDetail before it was appended to the collection, which
				//  meant that it wasn't yet reflected in its Count property. By
				//  deferring the update until after the item is appended, it is
				//  included in the count. This simple adjustment eliminates the
				//  addition from the formula.
				//  ------------------------------------------------------------

				if ( prDtl.DisplayOrder <= MagicNumbers.ZERO )
				{   // If it arrives with a nonzero DisplayOrder, leave it.
					prDtl.DisplayOrder = ( ReportDetail.ItemDisplayOrder ) ( this.Count * s_uintIncrement );
				}   // if ( ( uint ) prDtl.DisplayOrder <= MagicNumbers.ZERO )
			}   // lock ( s_lwlPrivate )
		}   // public new void Add
        #endregion  // Overridden Methods of Base Class


        #region Constructors
        /// <summary>
        /// Construct an empty list.
        /// </summary>
        public ReportDetails ( )
            : base ( )
        { } // The default constructor is 1 of 3.


        /// <summary>
        /// Construct a list with an initial capacity sufficient to hold the
        /// whole ICollection, but check them in one by one.
        /// </summary>
        /// <param name="pICollection">
        /// The collection from which to construct the list. Any member that is
        /// a ReportDetail is added. Others are discarded, so that the finished
        /// collection is homogeneous.
        /// </param>
        public ReportDetails ( ICollection<ReportDetail> pICollection )
            : base ( pICollection.Count )
        {
            lock ( s_lwlPrivate )
            {   // Make safe for threads.
                foreach ( object objItem in pICollection )
                {
                    if ( objItem.GetType ( ) == typeof ( ReportDetail ) )
                    {
                        ReportDetail rptItem = ( ReportDetail ) objItem;
                        this.Add ( rptItem );
                    }   // if ( objItem.GetType ( ) == typeof ( ReportDetail ) )
                }   // foreach ( object objItem in pICollection )
            }   // lock ( s_lwlPrivate )
        }   // Set the initial capacity, but gate the ICollection (2 of 3).


        /// <summary>
        /// Construct an empty list, with a specified initial capacity.
        /// </summary>
        /// <param name="pintCapacity">
        /// Set the initial capacity of the list to this value.
        /// </param>
        public ReportDetails ( int pintCapacity )
            : base ( pintCapacity )
        { }   // Set the initial capacity of the list (3 of 3).
        #endregion  // Constructors


        #region Properties
        /// <summary>
        /// Gets or sets the value (contents) of the GroupDetails array of
        /// generic object variables intended for inclusion in reports.
        /// </summary>
        /// <remarks>
        /// Use this array to store objects intended to appear in multiple lines
        /// of a report, such as a description of their source, creation date,
        /// or similar attributes that apply to the collection as a whole.
        /// </remarks>
        public object [ ] GroupDetails
        {
            get { return s_aobjGroupDetails; }
            set { s_aobjGroupDetails = value; }
        }   // public static object [ ] GroupDetails


        /// <summary>
        /// Unless the object being added has one of its own, the local Add
        /// method multiplies the count, plus 1, by this increment to set its
        /// DisplayOrder property.
        /// </summary>
        public int Increment
        {
            get { return s_uintIncrement; }
            set
            {
                if ( value > MagicNumbers.ZERO )
                    lock ( s_lwlPrivate )
                        s_uintIncrement = value;
                else
                    throw new ArgumentOutOfRangeException (
                        Core.Properties.Resources.INCREMENT_PROPERTY_VALUE ,
                        value ,
                        Core.Properties.Resources.INCREMENT_CANNOT_BE_ZERO );
            }   // public static uint Increment property setter method
        }   // public uint Increment property


        /// <summary>
        /// Gets the width, in characters, of the widest label.
        /// 
        /// If the class is empty, the return value is zero.
        /// </summary>
        public int WidthOfWidestLabel
        { get { return GetLongestLabel ( ); } }


        /// <summary>
        /// Gets the width, in characters, of the widest label, as an unsigned 
        /// integer.
        /// 
        /// If the class is empty, the return value is zero.
        /// </summary>
        public uint WidthOfWidestLabelUnsigned
        { get { return ( uint ) GetLongestLabel ( ); } }



        /// <summary>
        /// Gets the width, in characters, of the widest value in the
        /// collection.
        /// </summary>
        public int WidthOfWidestValue
        { get { return GetLongestDisplayValue ( ); } }


        /// <summary>
        /// Gets the width, in characters, of the widest value in the
        /// collection, as an unsigned integer.
        /// </summary>
        public uint WidthOfWidestValueUnsigned
        { get { return ( uint ) GetLongestDisplayValue ( ); } }
        #endregion  // Properties


        #region Regular Instance Methods
        /// <summary>
        /// List each item in a new line on the system console.
        /// </summary>
        public void ListAllItems ( )
        {
            ListAllItems ( Console.Out );
        }   // public void ListAllItems (1 of 2)


        /// <summary>
        /// List each item in a new line on the specified TextWriter.
        /// </summary>
        /// <param name="pswOut">
        /// Specify the open TextWriter on which to write the report.
        /// </param>
        public void ListAllItems ( TextWriter pswOut )
        {
            int intWidthOfWidestLabel = GetLongestLabel ( );

            //  ----------------------------------------------------------------
            //  Use the overloaded FormatDetail method that returns padded
            //  labels.
            //  ----------------------------------------------------------------

            foreach ( ReportDetail dtl in this )
                pswOut.WriteLine ( dtl.FormatDetail ( intWidthOfWidestLabel ) );
        }   // public void ListAllItems (2 of 2)


        /// <summary>
        /// Fill an array of strings with report items.
        /// </summary>
        /// <returns>
        /// The return value is an array of strings, one per item in the
        /// collection. Each string is a report item, ready to send to a text
        /// file or for further modification, since the lines aren't terminated.
        /// </returns>
        public string [ ] ListAllItemsInArray ( )
        {
            uint uintWidthOfWidestLabel = ( uint ) GetLongestLabel ( );
            int intNextSlot = ArrayInfo.ARRAY_INVALID_INDEX;

            string [ ] rastrDetails = new string [ this.Count ];

            foreach ( ReportDetail dtl in this )
                rastrDetails [ ++intNextSlot ] = dtl.FormatDetail ( uintWidthOfWidestLabel );

            return rastrDetails;
        }   // public string [ ] ListAllItemsInArray
        #endregion  // Regular Instance Methods

        
        #region Private Instance Methods
        private int GetLongestDisplayValue ( )
        {
            int rintLongest = MagicNumbers.ZERO;

            lock ( s_lwlPrivate )
            {   // Make safe for threads.
                foreach ( ReportDetail rd in this )
                {   // Run through the collection.
                    lock ( rd )
                    {   // Lock each member while we look at it.
                        if ( rd.TestState ( ReportDetail.State.HaveValueString ) )
                        {
                            int intTempLength = rd.DisplayValue.Length;

                            if ( intTempLength > rintLongest )
                            {   // This value is longer than any seen so far.
                                rintLongest = intTempLength;
                            }   // if ( intTempWidth > rintLongest )
                        }   // if ( rd.TestState ( ReportDetail.State.HaveValueString ) )
                        else if ( rd.TestState ( ReportDetail.State.HaveValueObject ) )
                        {   // Compute the length of the string representation of the object, once only, in case it's expensive.
                            int intTempLength = rd.Value.ToString ( ).Length;

                            if ( intTempLength > rintLongest )
                            {   // This value is longer than any seen so far.
                                rintLongest = intTempLength;
                            }   // if ( intTempLength > rintLongest )
                        }   // else if ( rd.TestState ( ReportDetail.State.HaveValueObject ) )
                        else
                        {   // The object and its display value are both null.
                            if ( rd.TestState ( ReportDetail.State.HaveLabel ) )
                            {   // It has a label, though.
                                throw new InvalidOperationException (
                                    string.Format (
                                        Core.Properties.Resources.ERRMSG_THIS_REPORTITEM_IS_NULL ,
                                        rd.Label ) );
                            }   // TRUE (more likely outcome) block, if ( rd.TestState ( ReportDetail.State.HaveLabel ) )
                            else
                            {   // It isn't even labeled!
                                throw new InvalidOperationException (
                                    string.Format (
                                        Core.Properties.Resources.ERRMSG_THIS_REPORTITEM_IS_NULL ,
                                        Core.Properties.Resources.ERRMSG_UNLABELED ) );
                            }   // FALSE (less likely outcome) block, if ( rd.TestState ( ReportDetail.State.HaveLabel ) )
                        }   // neither ( rd.TestState ( ReportDetail.State.HaveValueString ) ), nor ( rd.TestState ( ReportDetail.State.HaveValueObject ) )
                    }   // lock ( rd )
                }   // foreach ( ReportDetail rd in this )
            }   // lock ( lwlPrivate )

            return rintLongest;
        }   // private int GetLongestDisplayValue
        
        
        private int GetLongestLabel ( )
        {
            int rintLongest = MagicNumbers.ZERO;

            lock ( s_lwlPrivate )
            {   // Make safe for threads.
                foreach ( ReportDetail rd in this )
                {   // Run through the collection.
                    lock ( rd )
                    {   // Lock each member while we look at it.
                        int intTempLength = rd.Label.Length;

                        if ( intTempLength > rintLongest )
                        {   // This label is longer than any seen so far.
                            rintLongest = intTempLength;
                        }   // if ( intTempLength > rintLongest )
                    }   // lock ( rd )
                }   // foreach ( ReportDetail rd in this )
            }   // lock ( lwlPrivate )

            return rintLongest;
        }   // private int GetLongestLabel ( )
        #endregion  // Private Instance Methods


        #region Instance Storage
        int s_uintIncrement = DEFAULT_DISPLAY_ORDER_INCREMENT;
        object [ ] s_aobjGroupDetails = null;
        #endregion  // Instance Storage
    }   // public class ReportDetails
}   // partial namespace WizardWrx