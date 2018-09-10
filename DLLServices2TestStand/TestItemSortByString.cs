/*
    ============================================================================

    Module Name:        TestItemSortByString.cs

    Namespace Name:     DLLServices2TestStand

    Class Name:         TestItemSortByString

    Synopsis:           This module defines the class that models a test input
                        for WizardWrx.ListHelpers.MergeNewItemsIntoArray, a 
                        generic list merge routine.

    Remarks:            Since this is tested code, I started the version number
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

    Created:            Wednesday, 21 November 2012 - Thursday, 22 November 2012

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2012/11/22 2.0     DAG    Application created and tested.

    2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

    2017/08/27 7.0     DAG    Move into WizardWrx.DllServices2_TestStand from
                              WizardWrx.SharedUtl4.dll, to accompany the code
                              that it exercises, which went into WizardWrx.Core.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;


namespace DLLServices2TestStand
{
    class TestItemSortByString : IComparable
    {
		private long _lngKey = WizardWrx.MagicNumbers.ZERO;
        private string _strData = null;
        public TestItemSortByString ( ) { }    // Default constructor (1 of 3)

        /// <summary>
        /// This constructor takes a key, leaving the value uninitialized.
        /// </summary>
        /// <param name="pstrKey">
        /// This string becomes the key.
        /// </param>
        public TestItemSortByString ( long plngKey )
        { this.Key = plngKey; }         // Constructor with key initializer (2 of 3)


        /// <summary>
        /// This constructor fully initializes the object.
        /// </summary>
        /// <param name="pstrKey">
        /// Treat this string as the sort key.
        /// </param>
        /// <param name="pstrData">
        /// Treat this string as data that is presumably related to the specified key.
        /// </param>
        public TestItemSortByString (
			long plngKey ,
			string pstrData )
        {
            this.Key = plngKey;
            this.Data = pstrData;
        }   //  Constructor to fully initialize the object (3 of 3)


        /// <summary>
        /// This string is the value by which items are sorted. For simplicity,
        /// I chose a long integer. Zero is forbidden.
        /// </summary>
        public long Key
        {
            get { return _lngKey; }
            set
            {
                if ( value == WizardWrx.MagicNumbers.ZERO )
                    throw new ArgumentNullException ( );
                else
                    _lngKey = value;
            }   // public string Key set method
        }   // public string Key


        /// <summary>
        /// Likewise, for simplicity, a string represents the data.
        /// </summary>
        public string Data
        {
            get { return _strData; }
            set { _strData = value; }
        }   // public string Data


		/// <summary>
		/// This method implements IComparable for TestItemSortByString objects.
		/// </summary>
		/// <param name="pComparand">
		/// The other TestItemSortByString against which to compare this one.
		/// </param>
		/// <returns>
		/// The return value is specified in the IComparable interface, as it is
		/// implemented by the string class, which carries it out on behalf of
		/// objects of this class.
		/// </returns>
		public int CompareTo ( object pComparand )
		{
			if ( pComparand.GetType ( ) == this.GetType ( ) )
			{
				TestItemSortByString tiComparand = ( TestItemSortByString ) pComparand;
				return _lngKey.CompareTo ( tiComparand.Key );
			}   // TRUE (normal) block, if ( pComparand.GetType ( ) == this.GetType ( ) )
			else
			{
				throw new ArgumentException ( Properties.Resources.IDS_COMPARETO_MISMATCH );
			}   // FALSE (exception) block, if ( pComparand.GetType ( ) == this.GetType ( ) )
		}   // public int CompareTo (implements IComparable)
	}   // class TestItemSortByString
}   // partial namespace DLLServices2TestStand