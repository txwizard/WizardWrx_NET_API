/*
    ============================================================================

    Namespace:          WizardWrx.FormatStringEngine

    Class Name:         FormatItemsCollection

    File Name:          FormatItemsCollection.cs

    Synopsis:           This class models the collection of FormatItems that
                        appear within the FormatString represented by the
                        FormatStringParser instance that owns it.

    Remarks:            This class inherits from the generic List<T> class,
                        which doesn't support synchronization. Although probably
                        overkill for the ways in which this class is likely to
                        be used, it provides its own synchronization mechanism,
                        which is always employed when items are being added.
                        Since removal of items is unlikely, the Remove method is
                        left to the base class.

                        The constructors and Add method have internal (assembly
                        level) scope, to restrict creation and modification of
                        instances to members of the assembly in which it is
                        defined.

                        The base class Add method is overridden for three
                        reasons.

                        1)  This class demotes it from Public to Internal scope.

                        2)  The Add method is the logical place to call the
                            SetAppearanceOrder method on each object as it is
                            added.

                        3)  This class provides its own synchronization.

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

    Created:            Monday, 14 July 2014 - Wednesday, 16 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2014/07/16 1.0     DAG Initial implementation.

    2016/06/12 3.0     DAG Break the dependency on WizardWrx.SharedUtl2.dll,
                           correct misspelled words flagged by the spelling
                           checker add-in, and incorporate my three-clause BSD
                           license.

	2017/08/13 7.0     DAG Relocated to the constellation of core libraries that
                           began as WizardWrx.DllServices2.dll.
    ============================================================================
*/


using System;
using System.Collections.Generic;


namespace WizardWrx.FormatStringEngine
{
    /// <summary>
    /// This generic List derivative holds the collection of FormatItems found
    /// in a FormatString.
    /// 
    /// While the class, itself, is marked Public, everything else about it is
    /// marked as Internal, so that only instances of classes defined in this
    /// assembly can create a collection or add items to it, while instances of
    /// the FormatStringParser class expose it as a read only property.
    /// </summary>
    public class FormatItemsCollection : List<FormatItem>
    {
        #region Private Static Storage
        static readonly string s_objMyLock = "{CC2AE43E-29DE-4d40-AE12-0419B2B0CACA}";  // GUID courtesy of GuidGEN, running on host ZAPHOD42.
        static bool s_fIsLocked = false;    // Set when the constructor is adding the members of an ICollection, so that the overridden Add method doesn't try to do so.
        #endregion  // Private Static Storage


        #region Constructors
        /// <summary>
        /// The default constructor creates the collection, leaving it empty and
        /// with its capacity unspecified.
        /// </summary>
        internal FormatItemsCollection ( )
            : base ( )
        { } // FormatItemsCollection constructor (1 of 3)


        /// <summary>
        /// Create the empty collection, and specify its capacity.
        /// </summary>
        /// <param name="pintCapacity">
        /// Set aside memory to hold this many items.
        /// </param>
        internal FormatItemsCollection ( int pintCapacity )
            : base ( pintCapacity )
        { } // FormatItemsCollection constructor (2 of 3)


        /// <summary>
        /// Create the collection, and load an ICollection into it.
        /// </summary>
        /// <param name="pICollection">
        /// Specify the ICollection to load into the object. Since the base
        /// constructor already ran, the class is locked before the collection
        /// is loaded into it.
        /// </param>
        internal FormatItemsCollection ( ICollection<FormatItem> pICollection )
            : base ( pICollection.Count )
        {
            try
            {
                lock ( s_objMyLock )
                {   // Make thread-safe.
                    s_fIsLocked = true;

                    foreach ( object objItem in pICollection )
                    {   // Process each item in turn.
                        if ( objItem.GetType ( ) == typeof ( FormatItem ) )
                        {   // It passed the smell test.
                            FormatItem fi = ( FormatItem ) objItem;
                            this.Add ( fi );
                        }   // if ( objItem.GetType ( ) == typeof ( FormatItem ) )
                    }   // foreach ( object objItem in pICollection )
                }   // lock ( s_objMyLock )
            }
            catch ( Exception ex )
            {
                throw ex;
            }
            finally
            {
                s_fIsLocked = false;
            }
        }    // FormatItemsCollection constructor (3 of 3)
        #endregion  // Constructors


        #region Internal Methods
        /// <summary>
        /// Add a new item to the collection, setting its AppearanceOrder
        /// property equal to the Count of this collection, which already
        /// reflects its addtion, since it is added before the property is
        /// set.
        /// </summary>
        /// <param name="pfi">
        /// Specify the FormatItem to add to the collection.
        /// </param>
        internal new void Add ( FormatItem pfi )
        {
            if ( s_fIsLocked )
                base.Add ( pfi );
            else
                lock ( s_objMyLock )
                    base.Add ( pfi );

            //  ----------------------------------------------------------------
            //  By setting the property last, the count is the correct value. We
            //  let the base class do the math that it does when the item goes
            //  into the collection.
            //  ----------------------------------------------------------------

            pfi.SetAppearanceOrder ( ( uint ) base.Count );
        }   // internal new void Add
        #endregion  // Internal Methods
    }   // class FormatItemsCollection
}   // partial namespace WizardWrx.FormatStringEngine