/*
    ============================================================================

    Namespace:          WizardWrx.AssemblyUtils

    Class Name:         AssemblyAttributeHelpers

    File Name:          AssemblyAttributeHelpers.cs

    Synopsis:           Provide static methods for obtaining AssemblyAttributes
                        for various uses, such as identifying programs in their
                        user interfaces.

    Remarks:            This class transforms property defaults from hard coded
                        bits in the assembly to something that a user can change
						to suit.

    Author:             David A. Gray

    License:            Copyright (C) 2020-2021, David A. Gray.
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

    Created:            Sunday, 08 June 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2021/01/31 8.0     DAG    This class makes its debut.
    ============================================================================
*/

using System.Collections.Specialized;
using System.Reflection;


namespace WizardWrx.AssemblyUtils
{
    /// <summary>
    /// Provide static methods for obtaining AssemblyAttributes for various
    /// uses, such as identifying programs in their user interfaces.
    /// </summary>
    public static class AssemblyAttributeHelpers
    {
        /// <summary>
        /// This enumeration maps strings developed for the original, and used
        /// internally as keys for a NameValueCollection of selected Assembly
        /// Attributes, to safe, fast integers, which serve as the input of
        /// static method GetStartupAssemblyVersionInfo.
        /// </summary>
        public enum AttributeFriendlyName
        {
            /// <summary>
            /// This maps to the Company (long name AssemblyCompanyAttribute).
            /// </summary>
            Company,

            /// <summary>
            /// This maps to the Copyright (long name AssemblyCopyrightAttribute).
            /// </summary>
            Copyright,

            /// <summary>
            /// This maps to the Culture (long name AssemblyCultureAttribute).
            /// </summary>
            Culture,

            /// <summary>
            /// This maps to the Description (long name AssemblyDescriptionAttribute).
            /// </summary>
            Description,

            /// <summary>
            /// This maps to the FileVersion (long name AssemblyFileVersionAttribute).
            /// </summary>
            FileVersion,

            /// <summary>
            /// This maps to the Product (long name AssemblyProductAttribute).
            /// </summary>
            Product,

            /// <summary>
            /// This maps to the Title (long name AssemblyTitleAttribute).
            /// </summary>
            Title,

            /// <summary>
            /// This maps to the TradeMark (long name AssemblyTrademarkAttribute).
            /// </summary>
            Trademark,

            /// <summary>
            /// This maps to the Version (long name AssemblyVersionAttribute).
            /// </summary>
            Version
        }   // public enum AttributeFriendlyName


        /// <summary>
        /// Get common version attributes from the entry assembly.
        /// </summary>
        /// <param name="penmAttributeFriendlyName">
        /// A member of the AttributeFriendlyName describes the desired version
        /// attribute.
        /// </param>
        /// <param name="pasm">
        /// Pass in a reference to the Assembly for which assembly attributes are
        /// wanted, or pass NULL to get the metadata from the entry assembly.
        /// </param>
        /// <returns>
        /// The string returned by this method is the string representation of
        /// the value of the attribute specified by
        /// <paramref name="penmAttributeFriendlyName"/> as it was recorded in
        /// AssemblyInfo.cs of the assembly specified by <paramref name="pasm"/>.
        /// </returns>
        public static string GetAssemblyVersionInfo (
            AttributeFriendlyName penmAttributeFriendlyName ,
            Assembly pasm = null )
        {
            Assembly assembly = pasm == null ? Assembly.GetEntryAssembly ( ) : pasm;
            NameValueCollection asmAttribs = GetAssemblyAttribs ( assembly );

            return asmAttribs [ penmAttributeFriendlyName.ToString ( ) ];
        }   // public static string GetAssemblyVersionInfo


        /// <summary>
        /// Get the set of commonly requested "custom" assembly attributes from
        /// the specified assembly.
        /// </summary>
        /// <param name="pasm">
        /// Pass in a reference to the Assembly for which assembly attributes are
        /// wanted, or pass NULL to get the metadata from the entry assembly.
        /// </param>
        /// <returns>
        /// The return value of this private static method is a small
        /// NameValueCollection, keyed by friendly names, of the "custom"
        /// attributes bound to the assembly specified by <paramref name="pasm"/>.
        /// </returns>
        private static NameValueCollection GetAssemblyAttribs ( Assembly pasm = null )
        {
            NameValueCollection rnvc = new NameValueCollection ( );

            string strAttribNameLong;
            string strAttribValue;

            object [ ] attribs = pasm != null ? pasm.GetCustomAttributes ( false ) : Assembly.GetEntryAssembly ( ).GetCustomAttributes ( false );
            string strAttribNameShort = SpecialStrings.EMPTY_STRING;

            foreach ( object attrib in attribs )
            {
                strAttribNameLong = attrib.GetType ( ).ToString ( );
                strAttribValue = SpecialStrings.EMPTY_STRING;

                switch ( strAttribNameLong )
                {
                    case @"System.Reflection.AssemblyTrademarkAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_TRADEMARK;
                        strAttribValue = ( ( AssemblyTrademarkAttribute ) attrib ).Trademark.ToString ( );
                        break;

                    case @"System.Reflection.AssemblyProductAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_PRODUCT;
                        strAttribValue = ( ( AssemblyProductAttribute ) attrib ).Product.ToString ( );
                        break;

                    case @"System.Reflection.AssemblyCopyrightAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_COPYRIGHT;
                        strAttribValue = ( ( AssemblyCopyrightAttribute ) attrib ).Copyright.ToString ( );
                        break;

                    case @"System.Reflection.AssemblyCompanyAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_COMPANY;
                        strAttribValue = ( ( AssemblyCompanyAttribute ) attrib ).Company.ToString ( );
                        break;

                    case @"System.Reflection.AssemblyTitleAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_TITLE;
                        strAttribValue = ( ( AssemblyTitleAttribute ) attrib ).Title.ToString ( );
                        break;

                    case @"System.Reflection.AssemblyDescriptionAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_DESCRIPTION;
                        strAttribValue = ( ( AssemblyDescriptionAttribute ) attrib ).Description.ToString ( );
                        break;
                    case @"System.Reflection.AssemblyFileVersionAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_FILEVERSION;
                        strAttribValue = ( ( AssemblyFileVersionAttribute ) attrib ).Version.ToString ( );
                        break;
                    case @"System.Reflection.AssemblyVersionAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_VERSION;
                        strAttribValue = ( ( AssemblyVersionAttribute ) attrib ).Version.ToString ( );
                        break;
                    case @"System.Reflection.AssemblyCultureAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_CULTURE;
                        strAttribValue = ( ( AssemblyCultureAttribute ) attrib ).Culture.ToString ( );
                        break;
                    default:
                        break;
                }   // switch ( Name )

                if ( strAttribValue != SpecialStrings.EMPTY_STRING )
                {
                    if ( strAttribNameShort != SpecialStrings.EMPTY_STRING )
                    {
                        rnvc.Add (
                            strAttribNameShort ,
                            strAttribValue );
                    }   // if ( AttribName != SpecialStrings.EMPTY_STRING )
                }   // if ( AttribValue != SpecialStrings.EMPTY_STRING )
            }   // foreach ( object attrib in attribs )

            return rnvc;
        }   // NameValueCollection GetAssemblyAttribs
    }   // public static class AssemblyAttributeHelpers
}   // partial namespace WizardWrx.AssemblyUtils