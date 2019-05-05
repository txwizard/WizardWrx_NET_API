/*
    ============================================================================

    Namespace:			WizardWrx

    Class Name:			StringFixups

	File Name:			StringFixups.cs

    Synopsis:			This class exposes one instance method, ApplyFixups,
						that applies an array that contains one or more
                        StringFixup structures to a string to substitute valid
                        field names for the invalid names returned by a REST API
                        endpoint.

    Remarks:			Since StringFixup are intended only for use by
                        instances of a StringFixups class, the structure 
                        is defined herein, marked public because it is an input
                        to the ApplyFixups method.

    Author:				David A. Gray

	License:            Copyright (C) 2018, Great Eastern Energy.
						All rights reserved.

                        This source code is proprietary to the copyright holder.
						It must be treated as a trade secret.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       By Synopsis
    ---------- -- --------------------------------------------------------------
	2018/11/07 DG Initial commit.

	2019/05/04 DG Adapt for use with the RestClient class that I incorporated
                  from the same project from whence came this class.
	============================================================================
*/


using System;

namespace WizardWrx.Core
{
    /// <summary>
    /// Instances of this class represent pairs of fixup strings to apply as
    /// replacement pairs to amend a string, such as the JSON response from a
    /// REST endpoint that motivated its creation.
    /// </summary>
    public class StringFixups
    {
        /// <summary>
        /// A fixup pair is a pair of strings.
        /// </summary>
        public struct StringFixup
        {
            /// <summary>
            /// Specify the string to be replaced with OutputValue when found in
            /// an input string.
            /// </summary>
            public string InputValue;

            /// <summary>
            /// Specify the string to substitute for InputValue when found in an
            /// input string.
            /// </summary>
            public string OutputValue;


            /// <summary>
            /// This constructor permits initializing static read-only arrays of
            /// these structures.
            /// </summary>
            /// <param name="pstrInputValue">
            /// String to assign to the InputValue property
            /// </param>
            /// <param name="pstroutputValue">
            /// String to assign to the OutpuValue property
            /// </param>
            public StringFixup ( string pstrInputValue , string pstroutputValue )
            {
                this.InputValue = pstrInputValue;
                this.OutputValue = pstroutputValue;
            }   // public StringFixup structure
        };  // public struct StringFixup


        /// <summary>
        /// The default constructor is marked as private to force all instances
        /// to be initialized.
        /// </summary>
        private StringFixups ( )
        {
        } // StringFixups constructor (1 of 2) is kept private to force callers to pass in an array of fixups.


        /// <summary>
        /// The public constructor is overloaded, to guarantee that instances
        /// are properly initialized.
        /// </summary>
        /// <param name="pafixupPair">
        /// The array of StringFixup string pairs to apply to string fed into
        /// the ApplyFixups method
        /// </param>
        public StringFixups ( StringFixup [ ] pafixupPair )
        {
            _afixupPairs = ValidateFixupPairArray ( pafixupPair );
        }   // public StringFixups constructor (2 of 2) must, of course, be public.


        /// <summary>
        /// Call this method to apply the StringFixup array that was passed into
        /// the constructor.
        /// </summary>
        /// <param name="pstrInputString">
        /// String to transform by applying the StringFixup array that was passed into
        /// the constructor
        /// </param>
        /// <returns>
        /// The <paramref name="pstrInputString"/>, transformed by applying each
        /// StringFixup to it in turn
        /// </returns>
        public string ApplyFixups ( string pstrInputString )
        {   // Our ApplyFixups method delegates the work to the extension method.
            return pstrInputString.ApplyFixups ( _afixupPairs );
        }   // public string ApplyFixups




        /// <summary>
        /// The input array is sequestered here.
        /// </summary>
        private StringFixup [ ] _afixupPairs;


        /// <summary>
        /// Unless sumething is wrong with it, return the input <paramref name="pafixupPairs"/>
        /// to the calling routine.
        /// </summary>
        /// <param name="pafixupPairs">
        /// Array of StringFixup structures to validate
        /// </param>
        /// <returns>
        /// If the method succeeds, the return value is a reference to the input <paramref name="pafixupPairs"/>.
        /// Otherwise, the method throws an ArgumentNullException exception or
        /// an ArgumentException exception, depending on the circumstances.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException exception arises when the entire <paramref name="pafixupPairs"/>
        /// array is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when the entire array is empty
        /// or one of its InputValue members is a null reference or the empty
        /// string.
        /// </exception>
        internal static StringFixup [ ] ValidateFixupPairArray ( StringFixup [ ] pafixupPairs )
        {
            if ( pafixupPairs == null )
            {
                throw new ArgumentNullException ( nameof ( pafixupPairs ) );
            }   // TRUE (unanticipated outcome) block, if ( pafixupPairs == null )
            else
            {
                if ( pafixupPairs.Length == ArrayInfo.ARRAY_IS_EMPTY )
                {
                    throw new ArgumentException (
                        Core.Properties.Resources.ERRMSG_ARRAY_IS_EMPTY ,
                        nameof ( pafixupPairs ) );
                }   // TRUE (unanticipated outcome) block, if ( pafixupPairs.Length == ArrayInfo.ARRAY_IS_EMPTY )
                else
                {
                    for ( int intK = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                              intK < pafixupPairs.Length ;
                              intK++ )
                    {
                        if ( string.IsNullOrEmpty ( pafixupPairs [ intK ].InputValue ) )
                        {
                            throw new ArgumentException (
                                string.Format (
                                    Core.Properties.Resources.ERRMSG_STRING_FIXUP_PAIR_IS_INVALID ,
                                    new object [ ]
                                    {
                                        ArrayInfo.OrdinalFromIndex ( intK ),    // Format Item 0: at ordinal position {0} (
                                        intK ,                                  // Format Item 1: (subscript = {1})
                                        pafixupPairs [ intK ].OutputValue ,     // Format Item 2: The OutputValue at that position is {2},
                                        pafixupPairs [ intK ].OutputValue == null   // Format Item 3: which is {3}.
                                            ? Common.Properties.Resources.MSG_VALUE_IS_INVALID
                                            : Common.Properties.Resources.MSG_VALUE_IS_VALID ,
                                        Environment.NewLine                     // Format Item 4: is a null reference or the empty string.{4}
                                    } ) ,
                                nameof ( pafixupPairs ) );
                        }   // if ( string.IsNullOrEmpty ( pafixupPairs [ intK ].InputValue ) )
                    }   // for ( int intK = ArrayInfo.ARRAY_FIRST_ELEMENT ; intK < pafixupPairs.Length ; intK++ )

                    return pafixupPairs;
                }   // FALSE (anticipated outcome) block, if ( pafixupPairs.Length == ArrayInfo.ARRAY_IS_EMPTY )
            }   // FALSE (anticipated outcome) block, if ( pafixupPairs == null )
        }   // internal static StringFixup [ ] ValidateFixupPairArray
    }   // public class StringFixups
}   // partial namespace WizardWrx.Core