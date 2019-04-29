/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:         PropertyDefaults

    File Name:          PropertyDefaults.cs

    Synopsis:           Provide the infrastructure to allow default property
                        values to be stored in a configuration file that belongs
                        to the DLL, and travels with it.

    Remarks:            This class transforms property defaults from hard coded
                        bits in the assembly to something that a user can change
						to suit.

						This class derives from AssemblyLocatorBase. Since
						abstract base classes create an implicit dependency, I
                        moved its definition from WizardWrx.DLLServices, a
						single-purpose class library that exposes this class,
						and contains nothing else. WizardWrx.DLLServices was
						created to test a scenario in which the derived class is
						installed into the Global Assembly Cache. Since any
						library that goes into the GAC must depend entirely on
						assemblies that are signed with a strong name, I needed
						a dedicated class library that I could sign and install
						into the GAC alongside it.

						Though I still have WizardWrx.DLLServices.dll, I moved
						its source code tree to my _Deprecated_Libraries
						directory, and I didn't install it into the GAC on my
						new development machine, code named ENIGMA.

    References:         "DLL with configuration file," By fmsalmeida, 22 May 2011,
                        http://www.codeproject.com/Tips/199441/DLL-with-configuration-file
                        Retrieved and read Sunday, 08 June 2014.

    Author:             David A. Gray

    License:            Copyright (C) 2015-2017, David A. Gray. All rights reserved.

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
    2014/06/09 1.0     DAG    Initial implementation.

    2014/10/14 5.3     DAG    Take advantage of the relocation of its base class
                              to this library, breaking its link with 
                              WizardWrx.DLLServices and its strong name and the
                              thorny issues that arise when such an assembly is
							  updated, but is installed side by side, rather
							  than in the Global Assembly Cache.

	2015/06/20 5.5     DAG    1) Promote access from internal to public.

                              2) Incorporate my three-clause BSD license.

    2016/06/07 6.3     DAG    Adjust the internal documentation to correct a few
                              inconsistencies uncovered while preparing this
							  library for publication on GetHub.

	2017/02/27 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

                              The only effect of this change on this module is
                              it moves into a new namespace, and the new library
                              has a dependency upon WizardWrx.Common.

	2019/04/28 7.15    DAG    EnumerateMissingConfigurationValues is an instance
                              method that reports configuration values that are
                              defined, but are missing from the configuration
                              file.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Configuration;


namespace WizardWrx.Core
{
    /// <summary>
    /// Expose the AppSettingsSection associated with this DLL. 
	/// 
	/// The configuration settings come from the base class. The configuration
	/// settings come from the base class, AssemblyLocatorBase; hence, this
	/// class also serves as the concrete example that is recommended to
	/// accompany distribution of any abstract base class.
	/// 
	/// The settings exposed by this class inhabit a configuration file that is
	/// associated with the DLL, itself, as opposed to the application 
	/// configuration. Though its format is similar to that of the application
	/// configuration file, the DLL configuration lives in its own configuration
	/// file that has the same name as the DLL, with an additional suffix of
	/// .config.
    /// </summary>
	/// <remarks>
	/// The rationale behind segregating these settings is that the affected
	/// application properties are ones that you want to keep consistent, or
	/// nearly so, across large groups of applications. Keeping them in a
	/// dedicated configuration file that travels with the DLL that implements
	/// them eliminates the thankless task of adding them to every application
	/// configuration file.
	/// </remarks>
    public class PropertyDefaults : AssemblyLocatorBase
    {
        /// <summary>
        /// Keep a copy of the AppSettingsSection here.
        /// </summary>
		private KeyValueConfigurationCollection _DllConfigSettings = null;


        /// <summary>
        /// Use this constructor when the class instance is a sibling (defined
		/// in the same assembly).
        /// </summary>
		public PropertyDefaults ( )
			: base ( )
		{
			InitializeInstance ( );
		}   // public PropertyDefaults constructor (1 of 2)


		/// <summary>
		/// Use this constructor when the class instance and base class are
		/// defined in different assemblies.
		/// </summary>
		/// <param name="pasmLinkedAssembly"></param>
		public PropertyDefaults ( System.Reflection.Assembly pasmLinkedAssembly )
			: base ( pasmLinkedAssembly )
		{
			InitializeInstance ( );
		}	//    // public PropertyDefaults constructor (2 of 2)


        /// <summary>
        /// Return the KeyValueConfigurationCollection from the
        /// AppSettingsSection section of the DLL configuration file.
        /// </summary>
        public KeyValueConfigurationCollection ValuesCollection
        { get { return _DllConfigSettings; } }


        /// <summary>
        /// Get the date on which the assembly was built.
        /// </summary>
        /// <param name="pdtmKind">
        /// Specify whether to return local or UTC time. Altough all three types
        /// are nominally supported, Unspecified is treated as Local.
        /// </param>
        /// <returns>
        /// The return value is the System.DateTime when the assembly was built.
        /// </returns>
        public new DateTime GetAssemblyBuildDate ( DateTimeKind pdtmKind )
        { return base.GetAssemblyBuildDate ( pdtmKind ); }


        /// <summary>
        /// Get a version string, suitable for display in reports.
        /// </summary>
        /// <returns>
        /// The fully qualified version (i. e., all four parts) is returned.
        /// </returns>
        public new string GetAssemblyVersionString ( )
        { return base.GetAssemblyVersionString ( ); }


		/// <summary>
		/// Both constructors call this method to initialize their properties.
		/// </summary>
		private void InitializeInstance ( )
		{
			const string SECT_APPSETTINGS = @"appSettings";

            Configuration cfgForDLLClasses = ConfigurationManager.OpenExeConfiguration ( AssemblyLocation );
			AppSettingsSection SettingsSection = ( AppSettingsSection ) cfgForDLLClasses.GetSection ( SECT_APPSETTINGS );
			_DllConfigSettings = SettingsSection.Settings;
		}	// InitializeInstance

        /// <summary>
        /// Enumeate missing configuration values, if any.
        /// </summary>
        /// <returns>
        /// This method returns a message suitable for display on a console or a
        /// Windows message box. The returned message summarises the state of
        /// affairs, even when all defined values are represented in the
        /// configuration file.
        /// </returns>
        public string EnumerateMissingConfigurationValues ( )
        {
            MissingConfigSettings = MissingConfigSettings ?? UnconfiguredDLLSettings.TheOnlyInstance;
            List<UnconfiguredDLLSettings.UnconfiguredSetting> missing = MissingConfigSettings.GetMissingPropsForFile (
                System.IO.Path.GetFileName (
                    _strAssemblyLocation ) );

            if ( missing.Count > ListInfo.LIST_IS_EMPTY )
            {
                System.Text.StringBuilder sbMessages = new System.Text.StringBuilder ( MagicNumbers.CAPACITY_04KB );

                //  ------------------------------------------------------------
                //  Start with a heading.
                //  ------------------------------------------------------------

                sbMessages.AppendFormat (
                    Properties.Resources.MSG_SOME_CONFIG_SETTINGS_OMITTED ,
                    MissingConfigSettings.Count ,
                    _DllConfigSettings.Count ,
                    AssemblyLocation ,
                    Environment.NewLine );

                //  ------------------------------------------------------------
                //  List each item in turn.
                //  ------------------------------------------------------------

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < MissingConfigSettings.Count ;
                          intJ++ )
                {
                    sbMessages.AppendFormat (
                        Properties.Resources.MSG_ACCEPTED_DEFAULT_VALUE ,       // Format Control String: {0} of {1}: {2} = {3}{4}
                        ArrayInfo.OrdinalFromIndex ( intJ ) ,                   // Format Item 0: {0} of
                        MissingConfigSettings.Count ,                           // Format Item 1: of {1}
                        missing [ intJ ].PropName ,                             // Format Item 2: : {2}
                        missing [ intJ ].PropValue ,                            // Format Item 3: = {3}
                        Environment.NewLine );                                  // Format Item 4: newline
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < MissingConfigSettings.Count ; intJ++ )

                //  ------------------------------------------------------------
                //  End with a footing.
                //  ------------------------------------------------------------

                sbMessages.AppendFormat (
                    Properties.Resources.MSG_ACCEPTED_LIST_END ,
                    Environment.NewLine );

                return sbMessages.ToString ( );
            }   // TRUE (One or more settings is missing from the configuration file.) block, if ( base.MissingConfigSettings.Count > ListInfo.LIST_IS_EMPTY )
            else
            {
                return string.Format (
                    Properties.Resources.MSG_ALL_CONFIG_SETTINGS_COVERED ,
                    _DllConfigSettings.Count ,
                    AssemblyLocation );
            }   // FALSE (All configuration settings are covered by the configuration file.) block, if ( base.MissingConfigSettings.Count > ListInfo.LIST_IS_EMPTY )
        }   // public string EnumerateMissingConfigurationValues
    }   // public class PropertyDefaults
}   // partial namespace WizardWrx.Core