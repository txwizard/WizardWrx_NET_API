/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:			ClassAndMethodDiagnosticInfo

	File Name:			ClassAndMethodDiagnosticInfo.cs

    Synopsis:			This class is type-safe managed wrappers for kernel32
						routines LoadLibrary and GetProcAddress, published as
                        part of an article on the subject.

    Remarks:			These are 100% independent of System.Reflection, relying
                        instead on compile-time metadata supplied by the
                        System.Runtime.CompilerServices class library that ships
                        with version 4.5 and later of the Microsoft .NET
                        Framework.

    Author:             David A. Gray

    License:            Copyright (C) 2021, David A. Gray. 
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
	2021/06/09 8.0.26  DAG    This class makes its debut.

    2021/06/10 8.0.30  DAG    Rearrange the argument list to put the optional
                              arguments in order most likely to be overrridden.

    2021/06/18 8.0.34  DAG    ListObjectProperties: Right-align the property
                              numbers in the listing, and limit property strings
                              to one line of fewer than 133 characters.
    ============================================================================
*/


using System;
using System.Reflection;


namespace WizardWrx
{
    /// <summary>
    /// This class exposes static methods for enumerating properties of objects.
    /// </summary>
    public static class ObjectPropertyEnumerators
    {
        /// <summary>
        /// Default binding flags bit mask
        /// </summary>
        public const BindingFlags DEFAULT_BINDING_FLAGS = BindingFlags.Instance
                                                          | BindingFlags.Public
                                                          | BindingFlags.NonPublic
                                                          | BindingFlags.FlattenHierarchy;
        /// <summary>
        /// Enumerate the properties of an object as a formatted listing.
        /// </summary>
        /// <param name="pstrNameOfObject">
        /// Name of object as it appears in the calling routine
        /// </param>
        /// <param name="pObjThisOne">
        /// Reference to the object from which to enumerate properties
        /// </param>
        /// <param name="pintLeftPadding">
        /// Optional left padding for the report
        /// </param>
        /// <param name="pstrObjectLabelSuffix">
        /// Optional supplementary label information for the object
        /// </param>
        /// <param name="penmBindingFlags">
        /// Binding flags mask, which determines which properties are enumerated
        /// </param>
        public static void ListObjectProperties (
            string pstrNameOfObject ,
            object pObjThisOne ,
            int pintLeftPadding = ListInfo.EMPTY_STRING_LENGTH ,
            string pstrObjectLabelSuffix = null ,
            BindingFlags penmBindingFlags = DEFAULT_BINDING_FLAGS )
        {
            string strIndentation =
                pintLeftPadding > ListInfo.EMPTY_STRING_LENGTH
                ? StringTricks.StrFill (
                    SpecialCharacters.SPACE_CHAR ,
                    pintLeftPadding ) :
                SpecialStrings.EMPTY_STRING;

            PropertyInfo [ ] apropertyInfos = pObjThisOne.GetType ( ).GetProperties ( penmBindingFlags );
            int intMaxDigits = apropertyInfos.Length.ToString ( ).Length;       // Compute the width of the largest possible ordinal.
            Console.WriteLine (
               Properties.Resources.MSG_PROPERTY_LIST_HEADER ,                  // Format Control String
                strIndentation ,                                                // Format Item 0: {0}Object
                pstrNameOfObject ,                                              // Format Item 1: Object {1}
                pstrObjectLabelSuffix == null                                   // Format Item 2: {2} exposes the
                    ? SpecialStrings.EMPTY_STRING                               // when pstrObjectLabelSuffix == null
                    : $" ({pstrObjectLabelSuffix})" ,                           // when pstrObjectLabelSuffix != null
                apropertyInfos.Length ,                                         // Format Item 3: exposes the following {2}
                Environment.NewLine );                                          // Format Item 4: public properties:{4}

            for ( int intPropertyIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intPropertyIndex < apropertyInfos.Length ;
                      intPropertyIndex++ )
            {
                try
                {
                    Console.WriteLine (
                        Properties.Resources.MSG_PROPERTY_LIST_DETAIL ,         // Format Control String
                        strIndentation ,                                        // Format Item 0: {0}Property
                        NumericFormats.FormatIntegerLeftPadded (                // Format Item 1: Property {1}:
                            ArrayInfo.OrdinalFromIndex (                        // Get the ordinal equivalent to array subscript.
                                intPropertyIndex ),                             // Array subscript for which to get the corresponding ordinal.
                            intMaxDigits ),                                     // Right align to this width.
                        apropertyInfos [ intPropertyIndex ].Name ,              // Format Item 2: : {2} =
                        StringTricks.TruncateValueToOneLine (                   // Format Item 3: = {3}
                            apropertyInfos [ intPropertyIndex ].GetValue (      // Get value from the apropertyInfos in array slot intPropertyIndex.
                            pObjThisOne ,                                       // Object from which to get property value.
                            null ) ) );                                         // The null CultureInfo causes the method to infer the culture of the calling thread.
                }
                catch ( TargetInvocationException ex )
                {
                    Console.WriteLine (
                        Properties.Resources.ERRMSG_PROPERTY_LIST ,             // Format Control String
                        strIndentation ,                                        // Format Item 0: {0}Property
                        ArrayInfo.OrdinalFromIndex ( intPropertyIndex ) ,       // Format Item 1: Property {1}:
                        apropertyInfos [ intPropertyIndex ].Name ,              // Format Item 2: : {2} =
                        ex.Message );                                           // Format Item 3: = {3}
                }
            }   // for ( int intPropertyIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intPropertyIndex < apropertyInfos.Length ; intPropertyIndex++ )

            Console.WriteLine (
                Properties.Resources.MSG_PROPERTY_LIST_FOOTER ,                 // Format Control String
                strIndentation ,                                                // Format Item 0
                Environment.NewLine );                                          // Format Item 1
        }   // ListObjectProperties
    }   // public static class ObjectPropertyEnumerators
}   // partial namespace WizardWrx