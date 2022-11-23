/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         SortableManagedResourceItem

    File Name:          SortableManagedResourceItem.cs

    Synopsis:           This small class extends the item returned by a managed
                        resource reader with an implementation of IComparable,
                        so that it can be sorted.

    Remarks:            This class is required because the Resource Manager
                        must be enumerated the hard way, and its underlying
                        dictionary is opaque.
 
                        This class originated as a proof of concept in the
                        test stand program.

    Author:             David A. Gray

    License:            Copyright (C) 2017-2022, David A. Gray. 
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

    Created:            Sunday, 14 September 2014 and Monday, 15 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- --------------------------------------------------
    2017/07/19 7.0     DAG This class makes its debut.

    2019/06/07 7.19    DAG ListResourcesInAssemblyByName gets an optional
                           StreamWriter, which, if specified, causes it to
                           supplement the console report with a tab-delimited
                           list rendered in a text file.

    2019/12/16 7.23    DAG Allow the tab consistency add-in to replace tabs with
                           spaces. The code is otherwise unchanged, although the
                           new build is required to add a binding redirect, and
                           the version numbering transitions to the SemVer
                           scheme. This version also eliminates an unreferenced
                           using directive.

	2022/11/22 9.0.246 DAG    Move this class from WizardWrx.EmbeddedTextFile to
                              WizardWrx.Common, trading the dedicated namespace
                              for the root WizardWrx namespace. 
    ============================================================================
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;


namespace WizardWrx
{
    /// <summary>
    /// Instances of this class represent arbitrary managed resource items.
    /// </summary>
    /// <remarks>
    /// This class is necessary because the public dictionary is opaque, so a
    /// consumer has no control over the order in which they are returned. Since
    /// this wrapper implements the IComparable interface, collections of these
    /// objects can be sorted.
    /// </remarks>
    public class SortableManagedResourceItem : IComparable<SortableManagedResourceItem>
    {
        /// <summary>
        /// Other than satisfying the requirements of the IList interface, the
        /// uninitialized object is useless.
        /// </summary>
        public SortableManagedResourceItem ( )
        {
        }	// SortableManagedResourceItem constructor (1 of 2)


        /// <summary>
        /// This constructor creates a fully initialized instance from the data
        /// in the Current property of the IDictionaryEnumerator object returned
        /// by a ResourceSet instance.
        /// </summary>
        /// <param name="pstrName">
        /// Set the Name to the string returned by the Key property of the
        /// enumerator.
        /// </param>
        /// <param name="pobjValue">
        /// Set the Value to the System.object returned by the Value property of
        /// the enumerator.
        /// </param>
        public SortableManagedResourceItem (
            string pstrName ,
            object pobjValue )
        {
            if ( string.IsNullOrEmpty ( pstrName ) )
            {
                throw new ArgumentNullException (
                    "pstrName" ,
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY );
            }	// TRUE (unanticipated outcome) block, if ( string.IsNullOrEmpty ( pstrName ) )
            else if ( pobjValue == null )
            {
                throw new ArgumentNullException (
                    "pobjValue" ,
                    WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL );
            }	// TRUE (unanticipated outcome) block, else if ( pobjValue == null )
            else
            {
                _strName = pstrName;
                _objValue = pobjValue;
            }	// FALSE (anticipated outcome) block, else if ( pobjValue == null )
        }	// SortableManagedResourceItem constructor (2 of 2)


        /// <summary>
        /// The Name is the Name shown in the property sheet grid into which 
        /// garden variety managed string resources are input.
        /// </summary>
        public string Name
        {
            get { return _strName; }
        }	// Name property


        /// <summary>
        /// The ResourceValue property is the Value property shown in the
        /// property sheet grid into which garden variety managed string
        /// resources are input, and its type is usually System.string.
        /// </summary>
        public object Value
        {
            get { return _objValue; }
        }	// Value property


        /// <summary>
        /// Since the full type specification is accessible through the Value
        /// property, and the string representation of the FullName is its most
        /// useful property, it gets its own read only property, which also
        /// returns just "string" for the common case, for which the prefix is
        /// redundant.
        /// </summary>
        public string TypeName
        {
            get
            {
                string ValueTypeName = _objValue.GetType ( ).FullName;

                if ( ValueTypeName == "System.String" )
                    return "String";
                else
                    return ValueTypeName;
            }	// TypeName property get method
        }	// TypeName property


        string _strName;
        object _objValue;


        /// <summary>
        /// Making the comparison based on the Name property permits sorting the
        /// resources in the most logical order.
        /// </summary>
        /// <param name="other">
        /// The explicit implementation puts the burden of enforcing type safety
        /// on the runtime system.
        /// </param>
        /// <returns>
        /// Since it is a pass-through of another CompareTo method, the return
        /// type is guaranteed to adhere to the interface specification.
        /// </returns>
        int IComparable<SortableManagedResourceItem>.CompareTo (
            SortableManagedResourceItem other )
        {
            return _strName.CompareTo ( other._strName );
        }	// CompareTo method


        /// <summary>
        /// Use the list of Manifest Resource Names returned by method
        /// GetManifestResourceNames on a specified assembly. Each of several
        /// methods employs a different mechanism to identify the assembly of
        /// interest.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the name of the file from which the embedded resource was
        /// created. Typically, this will be the local name of the file in the
        /// source code tree.
        /// </param>
        /// <param name="pasmSource">
        /// Pass a reference to the Assembly that is supposed to contain the
        /// desired resource.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the internal name of
        /// the requested resource, which is fed to GetManifestResourceStream on
        /// the same assembly, which returns a read-only Stream backed by the
        /// embedded resource. If the specified resource is not found, it
        /// returns null.
        /// </returns>
        /// <remarks>
        /// Since I cannot imagine any use for this method beyond its
        /// infrastructure role in this class, I marked it private.
        /// </remarks>
        public static string GetInternalResourceName (
            string pstrResourceName ,
            Assembly pasmSource )
        {
            foreach ( string strManifestResourceName in pasmSource.GetManifestResourceNames ( ) )
            {
                if ( strManifestResourceName.EndsWith ( pstrResourceName ) )
                {
                    return strManifestResourceName;
                }   // if ( strManifestResourceName.EndsWith ( pstrResourceName ) )
            }   // foreach ( string strManifestResourceName in pasmSource.GetManifestResourceNames ( ) )

            return null;
        }   // private static string GetInternalResourceName
    }	// public class SortableManagedResourceItem
}	// partial namespace WizardWrx