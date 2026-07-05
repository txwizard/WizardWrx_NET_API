/*
    ============================================================================

    Module Name:        ListHelpers.cs

    Namespace Name:     WizardWrx

    Class Name:         ListHelpers

    Synopsis:           This class module defines some static utility methods
                        to simplify comparing objects of like kinds and merging
                        lists, stored in arrays, of like kind objects.

    Remarks:            Since the inputs and internal variables are generics,
                        the CompareTwoOfAKind and MergeNewItemsIntoArray methods
                        can process objects of all types, although the outcome 
                        depends on the behavior of their CompareTo method.

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

    Created:            Monday, 19 November 2012 - Wednesday, 21 November 2012

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2012/11/17 1.7     DAG    This class makes its first appearance, in program
                              DEDocTR_Listing_Splitter.

    2012/11/18 1.71    DAG    Optimize MergeNewestIntoArray, have it return a
                              new array of <T>, and change its name to
                              MergeNewItemsIntoArray.

    2012/11/21 2.0     DAG    Extract utility code from DEDocTR_Listing_Splitter
                              into two general purpose utility classes.

	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

    2017/08/03 7.0     DAG    Move this class from WizardWrx.SharedUtl4.dll into
                              WizardWrx.Core.dll.
    ============================================================================
*/


using System;
using System.Collections;
using System.Collections.Generic;

namespace WizardWrx
{
    /// <summary>
    /// This class exposes methods for merging sorted lists of items, and to
    /// simplify working with the values returned through the IComparable 
    /// interface.
    /// </summary>
    public static class ListHelpers
    {
        /// <summary>
        /// The CompareTwoOfAKind method returns a member of this enumeration,
        /// providing for the CompareTo method of the IComparable interface that
        /// it encapsulates with a tad of syntactic sugar.
        /// </summary>
        public enum CompareResult
        {
            /// <summary>
            /// Second argument, pComparand, is less than the first argument,
            /// pReference.
            /// </summary>
            LessThan ,

            /// <summary>
            /// Both arguments are equal.
            /// </summary>
            EqualTo ,

            /// <summary>
            /// Second argument, pComparand, is greater than the first argument,
            /// pReference.
            /// </summary>
            GreaterThan
        } // CompareResult


        /// <summary>
        /// This enumeration is used internally by the MergeNewItemsIntoArray
        /// method, where its use simplifies management of its internal state.
        /// </summary>
        enum MergeSource
        {
            /// <summary>
            /// This is the state at the beginning of the first iteration, until
            /// method CompareTwoOfAKind evaluates the first item in each list.
            /// </summary>
            Undetermined ,

            /// <summary>
            /// The outcome of the comparison is a decision to add the current
            /// item in the master list, which comes before the current item in
            /// the list of new items.
            /// </summary>
            AddItemFromMasterList ,

            /// <summary>
            /// The outcome of the comparison is a decision to add the current
            /// item in the list of new items, which is either a newer version
            /// of the current item in the master list, or a completely new item
            /// that comes next in the sort order defined by the class CompareTo
            /// method.
            /// </summary>
            AddItemFromNewList ,

            /// <summary>
            /// All items in the list of new items have been merged. Finish by
            /// appending the remaining items in the master list, all of which
            /// follow the last item in the list of new items in the sort order
            /// defined by the class CompareTo method.
            /// </summary>
            FinishFromMasterList ,

            /// <summary>
            /// All items in the master list have been merged. Fihish by
            /// appending the remaining items in the list of new items, all of
            /// which follow the last item in the list of new items in the sort
            /// order defined by the class CompareTo method.
            /// </summary>
            FinishFromNewItemsList
        }   // enum MergeSource


        /// <summary>
        /// Compare two objects of a kind. See Remarks.
        /// </summary>
        /// <typeparam name="T">
        /// Both comparands must implement the IComparable interface.
        /// </typeparam>
        /// <param name="pReference">
        /// The object against which to make the comparison.
        /// </param>
        /// <param name="pComparand">
        /// A second object of the same type against which to compare.
        /// </param>
        /// <returns>
        /// The return value is a member of the CompareResult enumeration, which
        /// reduces evaluation of results returned by the CompareTo method to a
        /// three-case switch statement.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// A NullReferenceException exception is thrown when pReference is null.
        /// The CompareTo method of the object's IComparable interface is
        /// expected to return a LessThan result when pComparand is null. See
        /// Remarks.
        /// </exception>
        /// <remarks>
        /// This method encapsulates the CompareTo method of a class T,
        /// returning a member of the CompareResult enumeration in place of the
        /// arbitrary zero or positive or negative integer specified in the
        /// IComparable interface. This syntactic sugar enables its return value
        /// to be processed by a switch block, rather than a nested IF block.
        /// 
        /// Whether or not I wrote it into the this, calling CompareTo on a null
        /// pReference object would elicit a NullReference exception. Having the
        /// application throw the exception permits it to supply a more
        /// informative message than the one that the CLR would have generated.
        /// For a two-argument function, the generic message is ambiguous.
        /// </remarks>
        public static CompareResult CompareTwoOfAKind<T> (
            T pReference ,
            T pComparand )
            where T : IComparable
        {
            const int EQUAL = MagicNumbers.ZERO;

            if ( pReference == null )
                throw new NullReferenceException ( "The pReference argument is a null reference." );

            int intOutcome = pReference.CompareTo ( pComparand );

            if ( intOutcome == EQUAL )
                return CompareResult.EqualTo;
            else
                if ( intOutcome < EQUAL )
                    return CompareResult.LessThan;
                else
                    return CompareResult.GreaterThan;
        }   // public static CompareResult CompareTwoOfAKind<T>
        

        /// <summary>
        /// Merge two sorted lists, returning a new sorted list containg the new
        /// or updated items from a second list. Please see Remarks.
        /// </summary>
        /// <typeparam name="T">
        /// All three lists (both inputs, paMasterList and paNewItems, and the
        /// returned list) must contain objects of the same type, and that type
        /// must implement the IComparable interface and have a parameterless
        /// default constructor.
        /// </typeparam>
        /// <param name="paMasterList">
        /// This array is the master list. Items without matching items in list
        /// paNewItems are preserved. Please see Remarks.
        /// </param>
        /// <param name="paNewItems">
        /// An item that matches an item in list paMasterList replaces it. An
        /// item that doesn't match any existing item in list paMasterList is
        /// merged into it. Please see Remarks.
        /// </param>
        /// <returns>
        /// The returned list contains everything in list paNewItems, and
        /// everything in list paMasterList that has no matching item in list
        /// paMasterList. Please see Remarks. Since both input lists are sorted,
        /// the new list is also sorted.
        /// </returns>
        /// <remarks>
        /// The goal of this routine is to merge two lists, the first of which
        /// is treated as a master list, into which new and updated items from
        /// from the second list are merged.
        /// 
        /// Merging is based on comparing items from both lists based on the
        /// values returned by their respective CompareTo methods. Values that
        /// return zero (equality) are merged by replacing the value from the
        /// first list, represented by the first argument (paMasterList) with
        /// that from the second list, represented by the second argument
        /// (paNewItems).
        /// 
        /// This algorithm imposes four requirements on its inputs.
        /// 
        /// 1) Both input arrays must be composed of objects of the same type.
        /// 
        /// 2) That type must implement the IComparable interface.
        /// 
        /// 3) That type must have a default constructor.
        /// 
        /// 4) Both input arrays must be sorted.
        /// 
        /// In return, it makes the following four guarantees.
        /// 
        /// 1) Every item in array paNewItems will become part of the new list.
        /// 
        /// 2) Every item in array paMasterList that has no matching value in
        /// array paNewItems will become part of the new list.
        /// 
        /// 3) Every item in array paNewItems that matches an item in array
        /// paMasterList replaces that matching item.
        /// 
        /// 4) Every item in array paNewItems that doesn't match any item in
        /// paMasterList is added to the list.
        /// 
        /// On input, both lists must be sorted, which is the first reason that
        /// the objects in the arrays must implement IComparable. The second
        /// reason is that this routine must compare the two lists in order to
        /// merge them correctly. The comparison happens in CompareTwoOfAKind, a
        /// companion routine that also takes generics meeting the first of the
        /// two specified constraints.
        /// </remarks>
        public static T [ ] MergeNewItemsIntoArray<T> (
            T [ ] paMasterList ,
            T [ ] paNewItems )
            where T : IComparable , new ( )
        {
            const string ARG_NAME_OLD = @"paMasterList";
            const string ARG_NAME_NEW = @"paNewItems";

            const long EMPTY = MagicNumbers.ZERO;

            //  ----------------------------------------------------------------
            //  First things first: Sanity check both inputs.
            //  ----------------------------------------------------------------

            if ( paMasterList == null )
            {
                throw new ArgumentNullException (
                    ARG_NAME_OLD ,
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL );
			}   // if ( paMasterList == null )

            if ( paNewItems == null )
            {
                throw new ArgumentNullException (
                    ARG_NAME_NEW ,
					WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL );
			}   // if ( paNewItems == null )

            long lngMasterItemsCount = paMasterList.LongLength;
            long lngNewItemsCount = paNewItems.LongLength;

            ArrayList alMerged = new ArrayList ( );

            //  ----------------------------------------------------------------
            //  Next, address the three degenerate cases of one or both arrays
            //  being empty.
            //  ----------------------------------------------------------------

            if ( lngMasterItemsCount == EMPTY || lngNewItemsCount == EMPTY )
            {   // One or both arrays are empty.
                if ( lngMasterItemsCount == EMPTY )
                {   // Old array is empty. Use new array, unless it is also empty.
                    if ( lngNewItemsCount > EMPTY )
                    {   // New array is populated. Fill the output array from it.
                        alMerged.AddRange ( paNewItems );
                    }   // if ( panew.Length > EMPTY )
                }   // TRUE block, if ( paold.Length == EMPTY )
                else
                {   // Fill the output array from the old array. The new one is, a priori, empty.
                    alMerged.AddRange ( paMasterList );
                }   // FALSE block, if ( paold.Length == EMPTY )
            }   // if ( paold.Length == EMPTY || panew.Length == EMPTY )

            //  ----------------------------------------------------------------
            //  The general case prevails; both arrays contain data.
            //  ----------------------------------------------------------------

            bool fMoreData = true;
            long lngIndexMaster = ArrayInfo.ARRAY_FIRST_ELEMENT;
            long lngIndexNewItems = ArrayInfo.ARRAY_FIRST_ELEMENT;

            MergeSource enmMergeSource = MergeSource.Undetermined;

            while ( fMoreData )
            {
                switch ( enmMergeSource )
                {
                    case MergeSource.FinishFromNewItemsList:
                        while ( lngIndexNewItems < lngNewItemsCount )
                        {
                            alMerged.Add ( paNewItems [ lngIndexNewItems ] );
                            lngIndexNewItems++;
                        }   // while ( lngIndexNewItems < lngNewItemsCount )

                        fMoreData = false;
                        break;  // case MergeSource.FinishFronNew

                    case MergeSource.FinishFromMasterList:
                        while ( lngIndexMaster < lngMasterItemsCount )
                        {
                            alMerged.Add ( paMasterList [ lngIndexMaster ] );
                            lngIndexMaster++;
                        }   // while ( lngIndexMaster < lngMasterItemsCount )

                        fMoreData = false;
                        break;  // case MergeSource.FinishFromOld

                    default:

                        //  ----------------------------------------------------
                        //  The designs of this routine and CompareTwoOfAKind
                        //  ensure that CompareTwoOfAKind succeeds, and that its
                        //  return value is always one of the three cases
                        //  written into this switch block.
                        //  ----------------------------------------------------

                        switch ( CompareTwoOfAKind ( paMasterList [ lngIndexMaster ] , paNewItems [ lngIndexNewItems ] ) )
                        {
                            case CompareResult.EqualTo:
                                enmMergeSource = MergeSource.AddItemFromNewList;
                                alMerged.Add ( paNewItems [ lngIndexNewItems ] );
                                lngIndexNewItems++;
                                lngIndexMaster++;
                                break;  // case CompareResult.EqualTo

                            case CompareResult.LessThan:
                                enmMergeSource = MergeSource.AddItemFromMasterList;
                                alMerged.Add ( paMasterList [ lngIndexMaster ] );
                                lngIndexMaster++;
                                break;  // case CompareResult.LessThan

                            case CompareResult.GreaterThan:
                                enmMergeSource = MergeSource.AddItemFromNewList;
                                alMerged.Add ( paNewItems [ lngIndexNewItems ] );
                                lngIndexNewItems++;
                                break;  // case CompareResult.GreaterThan
                        }   // switch ( CompareTwoOfAKind ( panew [ lngIndexNewItems ] , paold [ lngIndexMaster ] ) )

                        //  ----------------------------------------------------
                        //  After each iteration, see whether either or both
                        //  lists are exhausted.
                        //  ----------------------------------------------------

                        if ( lngIndexNewItems >= lngNewItemsCount && lngIndexMaster >= lngMasterItemsCount )
                        {   // The merge is finished when both lists are exhausted.
                            fMoreData = false;
                        }
                        else
                        {   // Check for the special case of one list being exhausted.
                            if ( lngIndexNewItems >= lngNewItemsCount )
                            {
                                enmMergeSource = MergeSource.FinishFromMasterList;
                            }   // if ( lngIndexNewItems > panew.Length )
                            else if ( lngIndexMaster >= lngMasterItemsCount )
                            {
                                enmMergeSource = MergeSource.FinishFromNewItemsList;
                            }   // else if ( lngIndexMaster >= lngMasterItemsCount )
                        }   // if ( lngIndexNewItems > panew.Length && lngIndexMaster > paold.Length )

                        break;  // Default case
                }   // switch ( enmMergeSource )
            }   // while ( fMoreData )

            T [ ] raMerged = ( T [ ] ) alMerged.ToArray ( typeof ( T ) );
            return raMerged;
        }   // public static T [] MergeNewItemsIntoArray<T>
    }   // static class ListHelpers
}   // partial namespace WizardWrx