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

    2021/02/03 8.0     DAG    1) Replace the public NameValueCollection with a
                                 Dictionary, and replace the bland Object array
                                 with an array of Attribute objects.

                              2) Export the Assembly's GUID.

	2021/05/19 8.0.167 DAG    New Methods: GetAssemblyCompanyNameSnakeCased
                                           GetAssemblyAppDataDirectoryName

    2024/02/05 9.0.275 DAG    New Methods: GetAssemblyGUID
                                           GetAssemblyTargetFramework
    ============================================================================
*/

using System;

using System.Collections.Generic;
using System.Reflection;

using WizardWrx.Core;


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
        /// publicly as keys for a NameValueCollection of selected Assembly
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
            /// <remarks>
            /// It appears that executable assemblies always have an AssemblyFileAttribute,
            /// which corresponds to this enumeration value, but they may not possess a
            /// Version attribute.
            /// </remarks>
            FileVersion,

            /// <summary>
            /// This maps to the System.Runtime.InteropServices.GuidAttribute attribute.
            /// </summary>
            /// <remarks>
            /// Though all assemblies sport one of these, their most common use is for
            /// exposing them to COM.
            /// </remarks>
            GUID,

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
            /// <remarks>
            /// Version maps to AssemblyVersion, and it appears that, at least
            /// in the case of executable assemblies (.EXE files), assemblies
            /// have an AssemblyFileAttribute (enumeration value FileVersion),
            /// but the AssemblyAttribute custom attribute may be absent.
            /// </remarks>
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
            Dictionary<string , string> dctAttribs = GetAssemblyAttribs ( assembly );
            string strAttributeNameString = penmAttributeFriendlyName.ToString ( );

            if ( dctAttribs.ContainsKey ( strAttributeNameString ) )
            {   // The requested value exists on the specified assembly. Return it.
                return dctAttribs [ strAttributeNameString ];
            }   // TRUE (The specified assembly has the requested attribute.) block, if ( dctAttribs.ContainsKey ( strAttributeNameString ) )
            else
            {   // The requested value is absent from the specified assembly. Return the empty string.
                return SpecialStrings.EMPTY_STRING;
            }   // FALSE (The specified assembly has no such attribute.) block, if ( dctAttribs.ContainsKey ( strAttributeNameString ) )
        }   // public static string GetAssemblyVersionInfo


        /// <summary>
        /// Get the absolute (fully qualified) name of the Application Data
        /// directory that is set aside for use by assemblies published by the
        /// company named in the AssemblyCompany property of the entry assembly.
        /// </summary>
        /// <param name="pfCreate">
        /// If TRUE, the specified directory is created if necessary.
        /// </param>
        /// <returns>
        /// The return value is the absolute (fully qualified) name of the
        /// Application Data directory that is set aside for use by assemblies
        /// published by the company named in the AssemblyCompany property of
        /// the entry assembly. If <paramref name="pfCreate"/> is TRUE, the
        /// directory named therein is guaranteed to exist. Otherwise, its
        /// existence is not guaranteed.
        /// </returns>
        public static string GetAssemblyAppDataDirectoryName ( bool pfCreate )
        {   // See the first example shown in https://docs.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=netframework-3.5&f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Environment.SpecialFolder.ApplicationData);k(TargetFrameworkMoniker-.NETFramework,Version%253Dv3.5);k(DevLang-csharp)%26rd%3Dtrue.
            string rstrAssemblyAppDataDirectoryName = System.IO.Path.Combine (
                Environment.GetFolderPath ( Environment.SpecialFolder.ApplicationData ) ,
                GetAssemblyCompanyNameSnakeCased ( null ) );

            if ( pfCreate )
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo ( rstrAssemblyAppDataDirectoryName );

                if ( !directoryInfo.Exists )
                {
                    directoryInfo = System.IO.Directory.CreateDirectory ( rstrAssemblyAppDataDirectoryName );

                    if ( !directoryInfo.Exists )
                    {
                        throw new InvalidOperationException (
                            string.Format (                                                         // string message
                                Properties.Resources.ERRMSG_CREATE_DIRECTORY_FAILED ,               // Format Control String
                                MethodBase.GetCurrentMethod ( ).Name ,                              // Format Item 0: Library method {0}
                                rstrAssemblyAppDataDirectoryName ) );                               // Format Item 1: was unable to create directory {1}.
                    }   // if ( !directoryInfo.Exists )
                }   // if ( !directoryInfo.Exists )
            }   // if ( pfCreate )

            return rstrAssemblyAppDataDirectoryName;
        }   // public static string GetAssemblyAppDataDirectoryName


        /// <summary>
        /// Call this method to get the AssemblyGUID that is embedded in the
        /// assembly's metadata (See its AssemblyInfo.cs.).
        /// </summary>
        /// <param name="pasm">
        /// Specify a reference to the System.Reflection.Assembly object, which
        /// must be loaded "for real" as opposed to ReflectionOnly.
        /// </param>
        /// <returns>
        /// The return value is the System.Runtime.InteropServices.GuidAttribute
        /// assembly attribute applied to the assembly via its AssemblyInfo.cs.
        /// </returns>
        public static System.Runtime.InteropServices.GuidAttribute GetAssemblyGUID ( Assembly pasm )
        {
            if ( pasm != null )
            {
                object [ ] objAttribs = pasm.GetCustomAttributes (
                    typeof (System.Runtime.InteropServices.GuidAttribute) ,
                    false );

                if ( objAttribs.Length > ArrayInfo.ARRAY_IS_EMPTY )
                {
                    return (System.Runtime.InteropServices.GuidAttribute) objAttribs [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
                }   // TRUE (anticipated outcome) block, if ( objAttribs.Length > ArrayInfo.ARRAY_IS_EMPTY )
                else
                {
                    return null;
                }   // FALSE (unanticipated outcome) block, if ( objAttribs.Length > ArrayInfo.ARRAY_IS_EMPTY )
            }   // TRUE (anticipated outcome) block, if ( pasm != null )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( pasm != null )
        }   // public static GuidAttribute GetAssemblyGUID method


        /// <summary>
        /// Get the Target Framework attribute from the <paramref name="pasm"/>
        /// assembly.
        /// </summary>
        /// <param name="pasm">
        /// Specify a reference to the System.Reflection.Assembly object, which
        /// must be loaded "for real" as opposed to ReflectionOnly.
        /// </param>
        /// <returns>
        /// The return value is the System.Runtime.Versioning.TargetFrameworkAttribute
        /// attribute applied to the assembly by the build engine.
        /// </returns>
        /// <remarks>
        /// Be advised that this routine truly supports only target frameworks 
        /// 4.0 and above. Versions 2.5, 3.0, and 3.5 are reported as version
        /// 2.5, and versions 1.0 and 1.1 will return a null reference.
        /// </remarks>
        public static System.Runtime.Versioning.TargetFrameworkAttribute GetAssemblyTargetFramework ( Assembly pasm )
        {
            if ( pasm != null )
            {
                if ( pasm.ImageRuntimeVersion == @"v2.0.50727" )
                {   // Since the TargetFrameworkAttribute first appeared on version 4.0 assemblies, previous verions must adapt.
                    System.Runtime.Versioning.TargetFrameworkAttribute targetFrameworkAttribute = new System.Runtime.Versioning.TargetFrameworkAttribute ( pasm.ImageRuntimeVersion );
                    targetFrameworkAttribute.FrameworkDisplayName = @".NET Framework 2.5";
                    return targetFrameworkAttribute;
                }   // TRUE (The target framework version is older than 4.0.) block, if ( pasm.ImageRuntimeVersion == @"v2.0.50727" )
                else
                {
                    object [ ] objAttribs = pasm.GetCustomAttributes (
                        typeof ( System.Runtime.Versioning.TargetFrameworkAttribute ) ,
                        false );

                    if ( objAttribs.Length > ArrayInfo.ARRAY_IS_EMPTY )
                    {
                        return ( System.Runtime.Versioning.TargetFrameworkAttribute ) objAttribs [ ArrayInfo.ARRAY_FIRST_ELEMENT ];
                    }   // TRUE (anticipated outcome) block, if ( objAttribs.Length > ArrayInfo.ARRAY_IS_EMPTY )
                    else
                    {
                        return null;
                    }   // FALSE (unanticipated outcome) block, if ( objAttribs.Length > ArrayInfo.ARRAY_IS_EMPTY )
                }   // FALSE (The target framework is applied to the assembly as an attribute.) block, if ( pasm.ImageRuntimeVersion == @"v2.0.50727" )
            }   // TRUE (anticipated outcome) block, if ( pasm != null )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( pasm != null )
        }   // public static TargetFrameworkAttribute GetAssemblyTargetFramework


        /// <summary>
        /// Get the AssemblyCompany property, which usually contains spaces, and
        /// often commas and other invalid characters, and either remove them or
        /// replace them with underscores.
        /// </summary>
        /// Pass in a reference to the Assembly for which assembly attributes are
        /// wanted, or pass NULL to get the metadata from the entry assembly.
        /// <returns>
        /// The string returned by this method is the string representation of
        /// the value of AttributeFriendlyName.Company as it was recorded in
        /// AssemblyInfo.cs of the assembly specified by <paramref name="pasm"/>,
        /// with commas, underscores, single quotes, hyphens, and spaces replaced
        /// by underscores, and sequential underscores reduced to one.
        /// </returns>
        public static string GetAssemblyCompanyNameSnakeCased ( Assembly pasm = null )
        {
            string strAssemblyCompany = GetAssemblyVersionInfo (
                AttributeFriendlyName.Company ,             // AttributeFriendlyName penmAttributeFriendlyName 
                pasm );                                     // Assembly pasm = null
            StringFixups fixups = new StringFixups ( s_fixups );

            return fixups.ApplyFixups ( strAssemblyCompany );
        }   // public static string GetAssemblyCompanyNameSnakeCased


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
        private static Dictionary<string , string> GetAssemblyAttribs ( Assembly pasm = null )
        {
            string strAttribNameLong;
            string strAttribValue;

            object [ ] aobjAttributesAsObjects = pasm != null ? pasm.GetCustomAttributes ( false ) : Assembly.GetEntryAssembly ( ).GetCustomAttributes ( false );
            Dictionary<string , string> rdctMatchedAttributes = new Dictionary<string , string> ( aobjAttributesAsObjects.Length );

            string strAttribNameShort = SpecialStrings.EMPTY_STRING;

            foreach ( object attrib in aobjAttributesAsObjects )
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

                    case @"System.Runtime.InteropServices.GuidAttribute":
                        strAttribNameShort = Properties.Resources.ATTRIBUTE_SHORT_NAME_ASSEMBLY_GUID;
                        strAttribValue = ( ( System.Runtime.InteropServices.GuidAttribute ) attrib ).Value.ToString ( );
                        break;
                    case @"System.Runtime.InteropServices.AssemblyInformationalVersionAttribute":

                    default:
                        break;
                }   // switch ( Name )

                if ( strAttribValue != SpecialStrings.EMPTY_STRING )
                {
                    if ( strAttribNameShort != SpecialStrings.EMPTY_STRING )
                    {
                        rdctMatchedAttributes.Add (
                            strAttribNameShort ,
                            strAttribValue );
                    }   // if ( AttribName != SpecialStrings.EMPTY_STRING )
                }   // if ( AttribValue != SpecialStrings.EMPTY_STRING )
            }   // foreach ( object attrib in attribs )

            return rdctMatchedAttributes;
        }   // private static Dictionary<string , string> GetAssemblyAttribs


        /// <summary>
        /// This private static read-only array of StringFixups objects is used
        /// by public static method GetAssemblyCompanyNameSnakeCased.
        /// </summary>
        private static readonly StringFixups.StringFixup [ ] s_fixups =
        {
            new StringFixups.StringFixup (
                SpecialStrings.COMMA,
                SpecialStrings.UNDERSCORE_CHAR ),
            new StringFixups.StringFixup (
                SpecialStrings.FULL_STOP,
                SpecialStrings.UNDERSCORE_CHAR ),
            new StringFixups.StringFixup (
                SpecialStrings.SINGLE_QUOTE,
                SpecialStrings.UNDERSCORE_CHAR ),
            new StringFixups.StringFixup (
                SpecialStrings.SPACE_CHAR,
                SpecialStrings.UNDERSCORE_CHAR ),
            new StringFixups.StringFixup (
                string.Concat (
                    SpecialCharacters.UNDERSCORE_CHAR,
                    SpecialCharacters.UNDERSCORE_CHAR ),
                SpecialStrings.UNDERSCORE_CHAR )
        };  // private static readonly StringFixups.StringFixup s_fixups
    }   // public static class AssemblyAttributeHelpers
}   // partial namespace WizardWrx.AssemblyUtils