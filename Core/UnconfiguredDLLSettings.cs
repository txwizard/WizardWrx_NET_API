/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:			UnconfiguredDLLSettings

	File Name:			UnconfiguredDLLSettings.cs

    Synopsis:			Organize missing DLL configuration settings by library.

    Remarks:			This class implements the Singleton pattern, and hides
                        the dictionary that serves as its data store.

    Author:				David A. Gray

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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
	2019/04/28 7.15    DAG    This class makes its debut.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardWrx.Core
{
    /// <summary>
    /// Organize the DLL configuration values that were omitted from the
    /// associated configuration file. Since multiple classes can and do share a
    /// DLL configuration file, this class must be a Singleton.
    /// </summary>
    public class UnconfiguredDLLSettings : GenericSingletonBase<UnconfiguredDLLSettings>
    {
        /// <summary>
        /// Add a new unconfigured setting.
        /// </summary>
        /// <param name="configFileName">
        /// Identify the affected configuration file.
        /// </param>
        /// <param name="propName">
        /// Identify the name of the missing property.
        /// </param>
        /// <param name="propValue">
        /// Recird its default value.
        /// </param>
        public void Add ( string configFileName , string propName , string propValue )
        {
            UnconfiguredSetting unconfiguredSetting = new UnconfiguredSetting (
                configFileName ,
                propName ,
                propValue );
            string strKey = unconfiguredSetting.ToString ( );
            _valuePairs = _valuePairs ?? new Dictionary<string , UnconfiguredSetting> ( );

            if ( !_valuePairs.ContainsKey ( strKey ) )
            {
                _valuePairs.Add (
                    strKey ,
                    unconfiguredSetting );
            }   // TRUE (anticipated outcome) block, if ( !_valuePairs.ContainsKey ( strKey ) )
            else
            {
                throw new InvalidOperationException (
                    string.Format (
                        Properties.Resources.ERRMSG_DUPLICATE_CONFIG_PROP_NAME ,
                        propName ,
                        configFileName ) );
            }   // FALSE (unanticipated outcome) block, if ( !_valuePairs.ContainsKey ( strKey ) )
        }   // public void Add method


        /// <summary>
        /// Return the list of parameters that are missing from the specified
        /// configuration file.
        /// </summary>
        /// <param name="pstrConfigFileName">
        /// Specify the name of the file for which the list is wanted.
        /// </param>
        /// <returns>
        /// The return value is a list of UnconfiguredSetting objects, which may
        /// be empty.
        /// </returns>
        public List<UnconfiguredSetting> GetMissingPropsForFile ( string pstrConfigFileName )
        {
            List<UnconfiguredSetting> rmissing = new List<UnconfiguredSetting> ( _valuePairs.Count );

            if ( _valuePairs.Count > ListInfo.LIST_IS_EMPTY )
            {
                foreach ( string strKey in _valuePairs.Keys )
                {
                    if ( strKey.StartsWith ( pstrConfigFileName ) )
                    {
                        rmissing.Add ( _valuePairs [ strKey ] );
                    }   // if ( strKey.StartsWith ( pstrConfigFileName ) )
                }   // foreach ( string strKey in _valuePairs.Keys )
            }   // if ( _valuePairs.Count > ListInfo.LIST_IS_EMPTY )

            return rmissing;
        }   // public List<UnconfiguredSetting> GetMissingPropsForFile


        /// <summary>
        /// Expose the Count property of the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return _valuePairs.Count;
            }   // public int Count property getter method
        }   // public int Count property (read-only)


        private Dictionary<string , UnconfiguredSetting> _valuePairs = null;
        /// <summary>
        /// Settings are organized into a private collection that belongs to the
        /// singleton.
        /// </summary>
        public class UnconfiguredSetting : IComparable<UnconfiguredSetting>
        {
            /// <summary>
            /// The default constructor is intended to remain unused.
            /// </summary>
            private UnconfiguredSetting ( )
            {
            }   // private UnconfiguredSetting constructor


            /// <summary>
            /// All interaction is expected to be with this constructor.
            /// </summary>
            /// <param name="configFileName">
            /// Settings are associated with a named configuration file. Only
            /// its base name matters, however.
            /// </param>
            /// <param name="propertyName">
            /// Each property name must be unique within the scope of its
            /// configuration file.
            /// </param>
            /// <param name="propertyValue">
            /// The default property value is recorded herein for reference.
            /// </param>
            public UnconfiguredSetting (
                string configFileName ,
                string propertyName ,
                string propertyValue )
            {
                ConfigFileName = configFileName;
                PropName = propertyName;
                PropValue = propertyValue;
            }   // public UnconfiguredSetting constructor


            /// <summary>
            /// Gets or sets the configuration file name
            /// </summary>
            public string ConfigFileName
            {
                get; set;
            }   // public string ConfigFileName


            /// <summary>
            /// Gets or sets the property name
            /// </summary>
            public string PropName
            {
                get; set;
            }   // public string PropName


            /// <summary>
            /// Gets or sets the default value assigned to the property
            /// </summary>
            public string PropValue
            {
                get; set;
            }   // public string PropValue


            int IComparable<UnconfiguredSetting>.CompareTo ( UnconfiguredSetting other )
            {
                return ToString ( ).CompareTo ( other.ToString ( ) );
            }   // int IComparable<UnconfiguredSetting>.CompareTo


            /// <summary>
            /// Override the Equals method on the base class to give a value
            /// that contains the absolute (fully qualified) configuration value
            /// name, composed of the configuration file name and the key name.
            /// </summary>
            /// <param name="obj">
            /// Comparand
            /// </param>
            /// <returns>
            /// True if this instance and the other refer to the same
            /// configuration file key.
            /// </returns>
            public override bool Equals ( object obj )
            {
                UnconfiguredSetting otherUnconfiguredSetting = obj as UnconfiguredSetting;
                return ToString ( ).Equals ( otherUnconfiguredSetting.ToString ( ) );
            }   // public override bool Equals


            /// <summary>
            /// Return a hash code based on the equality value.
            /// </summary>
            /// <returns>
            /// The return value is the hash code that corresponds to the
            /// equality value.
            /// </returns>
            public override int GetHashCode ( )
            {
                return this.ToString ( ).GetHashCode ( );
            }   // public override int GetHashCode


            /// <summary>
            /// Return a concatenated string composed of the name of the
            /// configuration file followed by the configuration value name.
            /// </summary>
            /// <returns>
            /// The return value is the configuration file name and key name,
            /// separated by an underscore character.
            /// </returns>
            public override string ToString ( )
            {
                const string UNIQUE_ID = @"{0}_{1}";

                return string.Format (
                    UNIQUE_ID ,
                    ConfigFileName ,
                    PropName );
            }   // public override string ToString
        }
    }   // public class UnconfiguredDLLSettings
}   // partial namespace WizardWrx.Core