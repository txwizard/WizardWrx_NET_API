/*
    ============================================================================

    Namespace:			WizardWrx.Core

    Class Name:			AssemblyLocatorBase

    File Name:			AssemblyLocatorBase.cs

    Synopsis:			Derive a class from this when you need the location from
						which the assembly in which it is defined was loaded.

    Important:			I have discovered that any assembly that defines a class
						derived from AssemblyLocatorBase depends upon this
						assembly, although the build engine may not make it so,
						especially when the dependency is indirect.

						Likewise, I have yet to find a way to force the build 
						engine to bring along a configuration file that is 
						paired with a DLL.

						However, since I have discovered how easy it is to embed
						a text file in an assembly and read it at run time, some
						of the situations that I once thought required a
						mechanism for finding a related text file can be solved
						by embedding the text file in the assembly, so long as
						its data is truly static.

    Remarks:			The motivation for this class was a need to initialize a
						static property from information stored in a
						configuration file that is associated with, and 
						accompanies, a DLL.

						Since much of its desired behavior must be evaluated in
						the context of a working DLL, its initial test stand was
						the DataEase DLL that motivated its creation.

						Not surprisingly, the DataEase DLL that motivated the
						creation of this abstract base class was among the first
						beneficiaries of resources stored as text files that are
						embedded into it as custom resources.

    References:			1)  "XML Reserved Characters"
							http://msdn.microsoft.com/en-us/library/ms145315(v=sql.90).aspx

						2)  "Naming Files, Paths, and Namespaces (Windows)"
							http://msdn.microsoft.com/en-us/library/windows/desktop/aa365247(v=vs.85).aspx

    Author:				David A. Gray

    License:            Copyright (C) 2012-2020, David A. Gray. 
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
    2012/12/26 2.91   DAG     This class makes its first appearance.

    2012/12/26 2.92   DAG     Extend this class with virtual methods for reading
                              its associated configuration file.

    2013/02/21 2.96   DAG     Move the class into a new subsidiary namespace of
                              WizardWrx.ApplicationHelpers, DLLServices, which
                              is housed in a new DLL.

                              Make the following improvements, identified during
                              use in class library DataEase.dll.

                              1) Exposing the name of the directory from which
                                 the DLL loaded expedites creating a fully
                                 qualified name from the unqualified name of a
                                 file that is expected to inhabit the same
                                 directory.

                              2) If the configuration reader supported a token
                                 for the AssemblyDataPath, it could fix up a
                                 configuration value before returning it.

                              3) To accommodate assemblies loaded from the GAC,
                                 it should interrogate its meta-data, and
                                 substitute the name of the directory from which
                                 the first assembly loaded into the process was
                                 loaded.

                              4) At a minimum, the assembly should provide a
                                 simple means of reporting that its 
                                 configuration file is missing.

                              The text above came, verbatim, from notes that I
                              made in preparation for this update.

                              Demote the DLLConfiguration and DLLettingsSection
                              properties to protected access.

                              To test its ability to find the application when 
							  it is loaded from the GAC, the assembly that 
						      houses it requires a strong name. I've put this
                              off for as long as I could.

    2014/06/08 5.1    DAG     BREAKING CHANGE

                              Promote the DLLServices namespace to the first 
                              rank of namespaces defined under the WizardWrx
                              namespace.

                              Since there are only about three assemblies that
                              refer in any way to this library, its impact is
                              pretty limited.

    2014/09/08 5.2    DAG     1) While I was cleaning up the XML documentation, 
                                 I noticed that two methods, GetAssemblyVersion
                                 and GetAssemblyVersionString, were calling
                                 GetEntryAssembly, which returns a reference to
                                 the assembly that contains the application
                                 entry point, rather than GetExecutingAssembly,
                                 which gets the assembly in which the calling
                                 code is executing.

                              2) This update began as a cosmetic cleanup of the
                                 XML documentation.

    2014/10/14 5.3    DAG     Promote to WizardWrx.DLLServices2, so that I can
                              break the link with WizardWrx.DLLServices and
                              break free of its strong name and associated
                              version.

	2015/06/20 5.5    DAG     Incorporate my three-clause BSD license.

	2016/04/02 6.0    DAG     Correct typographical errors, and mark the ends of
                              the conditional compilation blocks.

	2017/02/26 7.0    DAG     Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

                              The only effect of this change on this module is
                              it moves into a new namespace, and the new library
                              has a dependency upon WizardWrx.Common.

                              Subsequent events prompted me to add a constructor
                              that takes a System.Reflection.Assembly reference,
                              so that it can be bound to any arbitrary assembly.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.

    2019/04/28 7.15    DAG    1) Replace the ConfigMessage string property with
                                 the RecoveredConfigurationExceptions list.

                              2) Replace the properties collection enumeration
                                 with the much more efficient dictionary lookup.

    2020/12/20 7.24    DAG    Replace the reference to the single instance of
                              UnconfiguredDLLSettings MissingConfigSettings with
                              a call to its static GetTheSingleInstance method.
    ============================================================================
*/


using System;
using System.Configuration;

using System.IO;
using System.Reflection;


namespace WizardWrx.Core
{
    /// <summary>
    /// Use a class derived from this class to get the fully qualified name of
    /// the file from which the assembly in which the derived class is defined
    /// was loaded. See Remarks.
    /// </summary>
    /// <remarks>
    /// Given the location from which an assembly was loaded, you can learn
    /// almost anything else you need to know about that file, such as its size,
    /// age, version, and directory. Given the directory, you can locate
    /// satellite files, such as configuration files that contain settings that
    /// it uses.
    /// </remarks>
	/// <seealso cref="PropertyDefaults"/>
    public abstract class AssemblyLocatorBase
    {
        #region Public Constants
        //  --------------------------------------------------------------------
        //  The following table lists the four XML Reserved Characters and the
        //  entity reference with which they must be replaced.
        //
        //      --------------------------------------------
        //      Character   Meaning         Entity reference
        //      ---------   ------------    ----------------
        //      >           Greater than    &gt;
        //      <           Less than       &lt;
        //      &           Ampersand       &amp;
        //      %           Percent         &#37;
        //      --------------------------------------------
        //
        //  Rather than use these entity references in my substitution tokens, I
        //  selected a different token delimiter.
        //
        //  See reference 1.
        //
        //  The following characters, which are reserved by either the command
        //  line parser or the file mask parser, are also best avoided.
        //
        //      < (less than)
        //      > (greater than)
        //      : (colon)
        //      " (double quote)
        //      / (forward slash)
        //      \ (backslash)
        //      | (vertical bar or pipe)
        //      ? (question mark)
        //      * (asterisk)
        //
        //  Astute observers will recognize some overlap in the above two lists.
        //  Although not listed in the table of XML reserved characters, I
        //  suspect that a forward slash in the InnerText of an element is also
        //  problematic.
        //
        //  See reference 2.
        //  --------------------------------------------------------------------

        /// <summary>
        /// Use this token in file names stored in DLL configuration files to
        /// explicitly state that the file is expected to inhabit the directory
        /// from which the assembly is loaded, unless the assembly was loaded
        /// from the Global Assembly Cache (GAC). In that case, substitute the
        /// application directory.
        /// </summary>
        public const string ASSEMBLYDATAPATH_TOKEN = @"$$AssemblyDataPath$$";
        #endregion  // Public Constants


        #region Publicly Visible Data Structures
        /// <summary>
        /// SetPropertiesFromDLLConfiguration fills and returns this structure,
        /// to account for all defined properties, whether or not their values
        /// are configured.
        /// </summary>
        public struct PropertySourceCounts
        {
            /// <summary>
            /// Count of properties read from the configuration file.
            /// </summary>
            public int SpecifiedInConfiguration;

            /// <summary>
            /// Count of properties omitted from the configuration file, and
            /// left at their default values.
            /// </summary>
            public int Defaulted;
        }   // public struct PropertySourceCounts
        #endregion  // Publicly Visible Data Structures


        #region Private Constants and Instance Storage
        const int ALL_VERSION_PARTS = 4;

        #if DEBUG_MESSAGES_WW
            const string DEBUG_MESSAGES_WW_TPL = @"DEBUG_MESSAGES_WW, in class {0} (derived from AssemblyLocatorBase): {1} = {2}";
        #endif	// #if DEBUG_MESSAGES_WW

		const string APPSETTINGS = @"appSettings";

        const string DLL_CONFIG_FILE_NAME_TEMPLATE = @"{0}{1}";
        const string DLL_CONFIG_FILE_SUFFIX = @".config";

        const int EMPTY_CONFIG = 0;

        const bool FOR_SHOW = true;
        const bool FOR_USE = false;

        /// <summary>
        /// Once the energy required to gather the location has been expended,
        /// save it for future use.
        /// </summary>
        protected string _strAssemblyLocation = null;

        /// <summary>
        /// Likewise, hang onto the AssemblyDataPath.
        /// </summary>
        string _strAssemblyDataPath = null;

        /// <summary>
        /// The assembly configuration file has the same name as does the
		/// assembly, with an extension of .config appended. However, if the DLL
		/// loaded from the GAC, its configuration file must live in the
		/// application directory.
        /// </summary>
        string _strAssemblyConfigPath = null;
        string _strStartupAssemblyLocation = null;
        bool _fLoadedFromGAC = false;
        #endregion  // Private Constants and Instance Storage


        #region Constructors
        /// <summary>
        /// Initialize the one and only property of this class, which holds the
        /// fully qualified path from which the containing assembly was loaded.
		/// 
		/// Use this constructor to link the configuration file to the assembly
		/// that defines this class.
        /// </summary>
		/// <remarks>
		/// IMPORTANT: If the assembly loads from the Global Assembly Cache, its
		/// configuration file must be stored in the application directory. Be
		/// aware that if an assembly exists in the Global Assembly Cache, it
		/// loads from there, even if there is a copy in the application
		/// directory.
		/// </remarks>
		public AssemblyLocatorBase ( )
        {
			//	The original implementation derives its location from the
			//	assembly in which its derived type is defined.
            //	_strAssemblyLocation = this.GetType ( ).Assembly.Location;
			//	The improved implementation derives its location from that of 
			//	the calling assembly. To be effective, however, the top of the
			//  inheritance hierarchy MUST be defined in the assembly with which
			//	the configuration file is linked.
			_strAssemblyLocation = Assembly.GetCallingAssembly ( ).Location;
			InitializeInstance ( );
        }   // public AssemblyLocatorBase (constructor 1 of 2)


		/// <summary>
		/// Initialize the one and only property of this class, which holds the
		/// fully qualified path from which the containing assembly was loaded.
		/// 
		/// Use this constructor to link the configuration file to the assembly
		/// that defines this class.
		/// </summary>
		/// <param name="pasmLinkedAssembly">
		/// Pass in a reference to the assembly to which the configuration file
		/// is linked. For example, you can use the executing assembly of the
		/// object at the top of the inheritance tree.
		/// </param>
		/// <remarks>
		/// IMPORTANT: If the assembly loads from the Global Assembly Cache, its
		/// configuration file must be stored in the application directory. Be
		/// aware that if an assembly exists in the Global Assembly Cache, it
		/// loads from there, even if there is a copy in the application
		/// directory.
		/// </remarks>
		public AssemblyLocatorBase ( Assembly pasmLinkedAssembly )
		{
			_strAssemblyLocation = pasmLinkedAssembly.Location;
			InitializeInstance ( );
		}   // public AssemblyLocatorBase (constructor 2 of 2)
        #endregion  // Constructors


        #region Protected and Public ReadOnly Properties
        /// <summary>
        /// Gets a string containing the fully qualified path of the directory
        /// from which the assembly was loaded, unless it was loaded from the
        /// Global Assembly Cache (GAC). In that case, the return value is the
        /// fully qualified name of the directory from which the first assembly
        /// was loaded into the current process. See Remarks.
        /// </summary>
        /// <remarks>
        /// So far as I know, assemblies must load from one of two locations.
        /// Unsigned assemblies MUST load from the application directory. If the
        /// assembly is signed with a strong name, it MAY  be loaded from either
        /// the application directory or the Global Assembly Cache. If a signed
        /// assembly is in the local GAC, it loads from there, even if there is
		/// a copy in the application directory.
        /// </remarks>
        public string AssemblyDataPath { get { return _strAssemblyDataPath; } }


        /// <summary>
        /// Gets a string containing the fully qualified file name from which
        /// the assembly in which the derived class is defined was loaded.
        /// </summary>
        public string AssemblyLocation
        { get { return _strAssemblyLocation; } }


        /// <summary>
        /// This read only property returns a generic List of Exceptions that
        /// arose during the initialization phase of an instance, and were
        /// silently recovered, so that they can be reported for investigation.
        /// </summary>
        /// <remarks>
        /// This property supersedes ConfigMessage, which returned the list of
        /// exceptions as one long string, discarding their all-important stack
        /// traces.
        /// </remarks>
        public System.Collections.Generic.List<RecoveredException> RecoveredConfigurationExceptions { get; private set; }


        /// <summary>
        /// This read-only property returns a generic List of string, each of
        /// which is a message that names a property that was omitted from the
        /// configuration file, along with its default value.
        /// </summary>
        public UnconfiguredDLLSettings MissingConfigSettings { get; protected set; }


        /// <summary>
        /// Gets a reference to the entire Configuration object tied to the
        /// assembly in which the derived class is defined.
        /// </summary>
        protected Configuration DLLConfiguration
        {
            get
            {
                string strAssemblyConfigFleFQFN = FullyQualifiedDLLConfigFileName ( FOR_USE );

                #if DEBUG_MESSAGES_WW
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "strAssemblyConfigFleFQFN" ,
                        strAssemblyConfigFleFQFN );
                #endif	// #if DEBUG_MESSAGES_WW

				if ( File.Exists ( strAssemblyConfigFleFQFN ) )
                {   // Configuration file found. Load it.
                    return ConfigurationManager.OpenExeConfiguration ( strAssemblyConfigFleFQFN );
                }   // TRUE (normal) block, if ( File.Exists ( strAssemblyConfigFleFQFN ) )
                else
				{   // The configuration file is missing. Report by way of the ConfigMessage property, and return a null reference.
                    if ( RecoveredConfigurationExceptions == null)
                    {   // Creat as and when needed.
                        RecoveredConfigurationExceptions = new System.Collections.Generic.List<RecoveredException> ( );
                    }   // if ( RecoveredConfigurationExceptions == null)

                    RecoveredConfigurationExceptions.Add (
                        new RecoveredException (
                            string.Format (
                                Properties.Resources.ERRMSG_MISSING_CONFIG_FILE ,
                                strAssemblyConfigFleFQFN ) ,
                            MethodBase.GetCurrentMethod ( ).Module.Name ,
                            Environment.StackTrace ,
                            MethodBase.GetCurrentMethod ( ).Name ) );
                    return null;
                }   // FALSE (missing configuration file) block, if ( File.Exists ( strAssemblyConfigFleFQFN ) )
            }   // protected Configuration DLLConfiguration Get method
        }   // protected Configuration DLLConfiguration property


        /// <summary>
        /// Gets a reference to the entire AppSettingsSection object tied to
        /// the assembly in which the derived class is defined.
        /// </summary>
        /// <remarks>
        /// Since this property starts from the ConfigurationManager object
        /// returned by its DLLConfiguration sibling, it requires only a single
        /// statement, with a little help from an explicit cast.
        /// </remarks>
        protected AppSettingsSection DLLSettingsSection
        {
            get
            {
                Configuration cfgSection = this.DLLConfiguration;

                if ( cfgSection != null )
                {
                    AppSettingsSection rcfgAppSettings = ( AppSettingsSection ) cfgSection.GetSection ( APPSETTINGS );

                    if ( rcfgAppSettings.Settings.Count == EMPTY_CONFIG )
                    {
                        if ( RecoveredConfigurationExceptions == null )
                        {   // Create as and when needed.
                            RecoveredConfigurationExceptions = new System.Collections.Generic.List<RecoveredException> ( );
                        }   // if ( RecoveredConfigurationExceptions == null)

                        string strMessage = string.Format (
                            Properties.Resources.ERRMSG_CONFIG_FILE_IS_EMPTY ,
                            FullyQualifiedDLLConfigFileName ( FOR_SHOW ) );
                        TraceLogger.WriteWithBothTimesLabeledLocalFirst ( strMessage );
                        RecoveredConfigurationExceptions.Add ( new RecoveredException (
                            string.Format (
                                Properties.Resources.ERRMSG_FROM_THROWN_EXCEPTION ,
                                strMessage ) ,
                            MethodBase.GetCurrentMethod ( ).Module.Name ,
                            Environment.StackTrace ,
                            MethodBase.GetCurrentMethod ( ).Name ) );

                        #if DEBUG_MESSAGES_WW
                            Console.WriteLine (
                                DEBUG_MESSAGES_WW_TPL ,
                                System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                                "_strConfigMessage" ,
                                _strConfigMessage );
                        #endif   // #if DEBUG_MESSAGES_WW
                    }   // Notify the caller if the file is empty.

                    return rcfgAppSettings;
                }   // TRUE (normal case) block, if ( cfgSection != null )
                else
                {   // The absent file has already been noted.
                    return null;
                }   // FALSE (null case) block, if ( cfgSection != null )
            }   // protected AppSettingsSection DLLettingsSection Get method
        }   // protected AppSettingsSection DLLettingsSection property


        /// <summary>
        /// Gets the DLL Settings section as a KeyValueConfigurationCollection.
        /// </summary>
        protected KeyValueConfigurationCollection DLLSettings
        {
            get
            {
                if ( this.DLLSettingsSection != null )
                    return this.DLLSettingsSection.Settings;
                else
                    return null;
            }   // // protected KeyValueConfigurationCollection DLLSettings Get method
        }   // protected KeyValueConfigurationCollection DLLSettings


		/// <summary>
		/// Protected method SetPropertiesFromDLLConfiguration calls this
		/// routine to assemble an error message for appending to a string, for
		/// subsequent review by callers of derived classes.
		/// </summary>
		/// <param name="pderivedType">
		/// The derived type is listed in the report.
		/// </param>
		/// <param name="pstrPropertyName">
		/// The property name is listed in the report.
		/// </param>
		/// <param name="pexAllKinds">
		/// The message, presumably reporting a missing configuration item,
		/// completes the message.
		/// </param>
		/// <returns>
		/// The return value is a string, ready to be appended to the
		/// ConfigMessage string.
		/// </returns>
		private Exception SaveErrorReport (
			Type pderivedType ,
			string pstrPropertyName ,
			Exception pexAllKinds )
		{
            TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                string.Format (
                    @"At the top of method {0}, {1}.{2} = {3}" ,
                    new string [ ]
                    {
                        MethodBase.GetCurrentMethod().Name ,                    // Format Item 0: At the top of method {0}
                        nameof ( Environment ) ,                                // Format Item 0: , {1}
                        nameof ( Environment.StackTrace ) ,                     // Format Item 0: .{2}
                        Environment.StackTrace                                  // Format Item 0: = {3}
                    } ) );
            string strMessage = string.Format (
                Properties.Resources.ERRMSG_CONFIG_PROP_NOT_FOUND ,             // Format Control String
                pstrPropertyName ,                                              // Format Item 0 = while processing the {0} property
                pderivedType.FullName ,                                         // Format Item 1 = on a {1} object
                pexAllKinds.Message );				    						// Format Item 2 = Details are as follows: {2}
            TraceLogger.WriteWithBothTimesLabeledLocalFirst ( strMessage );
            return new Exception (
                strMessage ,
                pexAllKinds );
		}	// SaveErrorReport
        #endregion  // Protected and Public ReadOnly Properties


        #region Protected Methods
        /// <summary>
        /// Return the LastWriteTime of the file that contains the executing
        /// assembly. For all practical purposes, that is the date on which the
        /// assembly was built.
        /// </summary>
        /// <param name="pdtmKind">
        /// This DateTimeKind enumeration member specifies whether to report the
        /// LastWriteTime or the LastWriteTimeUtc.
        /// </param>
        /// <returns>
        /// The return value is a fully initialized DateTime structure, which
        /// contains the requested LastWriteTime (Local or UTC) of the file that
        /// contains the code of the executing assembly.
        /// </returns>
        protected DateTime GetAssemblyBuildDate ( DateTimeKind pdtmKind )
        {
            FileInfo fiThisAssembly = new FileInfo ( _strAssemblyLocation );

            switch ( pdtmKind )
            {
                case DateTimeKind.Unspecified:
                case DateTimeKind.Local:
                    return fiThisAssembly.LastWriteTime;
                case DateTimeKind.Utc:
                    return fiThisAssembly.LastWriteTimeUtc;
                default:
                    return fiThisAssembly.LastWriteTime;
            }   // switch ( pdtmKind )
        }   // protected DateTime GetAssemblyBuildDate


        /// <summary>
        /// Return the Version structure, to expedite parsing its parts.
        /// </summary>
        /// <returns>
        /// The return value is the version component of the fully qualified
        /// assembly name.
        /// </returns>
        protected Version GetAssemblyVersion ( )
        {
            return Assembly.GetExecutingAssembly ( ).GetName ( ).Version;
        }   // protected Version GetAssemblyVersion


        /// <summary>
        /// Return the complete version of the executing assembly.
        /// </summary>
        /// <returns>
        /// The return value is a string representation of all version number
        /// "octets" - Major, Minor, Build, and Revision.
        /// </returns>
        protected string GetAssemblyVersionString ( )
        {
            return Assembly.GetExecutingAssembly ( ).GetName ( ).Version.ToString ( ALL_VERSION_PARTS );
        }   // protected string GetAssemblyVersionString
        #endregion  // Protected Methods


        #region Public Methods
        /// <summary>
        /// Return the specified setting value, as a string.
        /// </summary>
        /// <param name="pstrSettingsKey">
        /// This string is the name (key) of the desired setting.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the value stored in
        /// the named key.
        /// </returns>
        public string GetDLLSetting ( string pstrSettingsKey )
        {
            if ( string.IsNullOrEmpty ( pstrSettingsKey ) )
                return SpecialStrings.EMPTY_STRING;
            else
                return this.DLLSettings [ pstrSettingsKey ].Value.Replace (
                    ASSEMBLYDATAPATH_TOKEN ,
                    _strAssemblyDataPath );
        }   // public string GetDLLSetting


		/// <summary>
		/// Set the like named properties from the linked configuration file.
		/// </summary>
		/// <param name="pderivedType">
		/// When the derived class constructor calls this method, it must pass in a
		/// reference to its own Type property.
		/// </param>
		/// <returns>
		/// The return value is the count of properties that were set.
		/// </returns>
		/// <remarks>
		/// This can almost certainly be simplified by enumerating the settings,
		/// but either way risks a NOT FOUND exception.
		/// 
		/// This method uses some fairly tricky Reflection gymnastics to map the
		/// key names in a configuration file to property names on an object.
		/// </remarks>
		protected PropertySourceCounts SetPropertiesFromDLLConfiguration ( Type pderivedType )
		{
			const BindingFlags BASELINE_BINDING_FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            PropertySourceCounts rutpCounts = new PropertySourceCounts ( );

            rutpCounts.Defaulted = MagicNumbers.ZERO;
            rutpCounts.SpecifiedInConfiguration = MagicNumbers.ZERO;

            string [ ] astrDefaultErrorMessageColors = DLLSettings.AllKeys;

			for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
					  intJ < astrDefaultErrorMessageColors.Length ;
					  intJ++ )
			{
				string strPropertyName = null;                                  // This must be visible to the catch block.

                try
                {
                    strPropertyName = astrDefaultErrorMessageColors [ intJ ];
                    string strConfigValueString = DLLSettings [ strPropertyName ].Value;
                    TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            @"At iteration {0} in {1}: {2} = {3}" ,             // Format control string
                            new object [ ]                                      // Array of values to substitute for tokens in format control string
                            {
                                ArrayInfo.OrdinalFromIndex ( intJ ) ,           // Format Item 0: At iteration {0}
                                MethodBase.GetCurrentMethod ( ).Name ,          // Format Item 1: in {1}
                                strPropertyName ,                               // Format Item 2: : {2}
                                strConfigValueString                            // Format Item 3: = {3}
                            } ) );
                    PropertyInfo piThisProperty = this.GetType ( ).GetProperty (
                        strPropertyName ,                                       // string      name         = The string containing the name of the property to get
                        BASELINE_BINDING_FLAGS );                               // bindingAttr BindingFlags = A bitmask comprised of one or more BindingFlags that specify how the search is conducted

                    if ( piThisProperty != null )
                    {
                        piThisProperty.SetValue (
                            this ,                                                  // Object      obj          = Object to which to apply setting
                            FromString (
                                piThisProperty ,
                                strConfigValueString ) ,                            // Object      value        = Value to assign to the property to which piThisProperty refers
                            BASELINE_BINDING_FLAGS | BindingFlags.SetProperty ,     // invokeAttr  invokeAtt	= A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, reateInstance, Static, GetField, SetField, GetProperty, or SetProperty. You must specify a suitable invocation attribute.
                            null ,                                                  // Binder      binder		= An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects through reflection. If binder is null, the default binder is used.
                            null ,                                                  // Object[]    index		= Optional index values for indexed properties. This value should be null for non-indexed properties.
                            null );                                                 // CultureInfo culture		= The culture for which the resource is to be localized. If the resource is not localized for this culture, the Parent property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the CurrentUICulture property.
                        rutpCounts.SpecifiedInConfiguration++;                      // Count properties successfully set; the final value is returned as the function value.
                    }   // TRUE (The configuration file has a value for this property.) block, if ( piThisProperty != null )
                    else
                    {
                        MissingConfigSettings = MissingConfigSettings ?? UnconfiguredDLLSettings.GetTheSingleInstance ( );

                        MissingConfigSettings.Add (
                            Path.GetFileName ( _strAssemblyLocation ) ,
                                strPropertyName ,                               // Format Item 0: property {1} default value
                                strConfigValueString );                         // Format Item 1: default value of {2} accepted
                        rutpCounts.Defaulted++;                                 // Count properties that retained their hard coded default values.
                    }   // FALSE (A value for this property is absent from the configuration file.) block, if ( piThisProperty != null )
                }
                catch ( Exception exAllKinds )
                {	// Since the catch block is within the body of the For loop, processing advances to the next item.
                    TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            @"{0}.{1} = {2}" ,
                            nameof ( Environment ) ,
                            nameof ( Environment.StackTrace ) ,
                            Environment.StackTrace ) );
                    if ( RecoveredConfigurationExceptions == null )
                    {   // Create as and when needed.
                        RecoveredConfigurationExceptions = new System.Collections.Generic.List<RecoveredException> ( );
                    }   // if ( RecoveredConfigurationExceptions == null )

                    RecoveredConfigurationExceptions.Add (
                       new RecoveredException (
                           string.Format (
                               Properties.Resources.ERRMSG_EXCEPTION_NOT_THROWN ,
                               SaveErrorReport (
                                   this.GetType ( ) ,
                                   strPropertyName ,
                                   exAllKinds ).Message ) ,
                           exAllKinds.Source ,
                           exAllKinds.StackTrace ,
                           exAllKinds.TargetSite.Name ) );
                    Exception exWrapped = RecoveredConfigurationExceptions [ ArrayInfo.IndexFromOrdinal ( RecoveredConfigurationExceptions.Count ) ];
                    TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_1 ,
                            exWrapped.Message ) );
                    TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_2 ,
                            exWrapped.Source ?? Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );
                    TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_3 ,
                            exWrapped.TargetSite != null
                                ? exWrapped.TargetSite.Name
                                : Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );
                    TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_4 ,
                            exWrapped.StackTrace ?? Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );

                    //  --------------------------------------------------------
                    //  Since SaveErrorReport appends the inner exception, this
                    //  test is technically redundant, but I chose to leave it,
                    //  as a reminder that new exceptions are not necessarily
                    //  so decordated. Hence, with the test, this code is a bit
                    //  more portable.
                    //  --------------------------------------------------------

                    if ( exWrapped.InnerException != null )
                    {
                        TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                        string.Format (
                            Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_5 ,
                            exWrapped.InnerException.Message ?? Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );
                        TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                            string.Format (
                                Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_6 ,
                                exWrapped.InnerException.Source ?? Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );
                        TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                            string.Format (
                                Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_7 ,
                                exWrapped.InnerException.TargetSite.Name ?? Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );
                        TraceLogger.WriteWithBothTimesLabeledLocalFirst (
                            string.Format (
                                Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_8 ,
                                exWrapped.InnerException.StackTrace ?? Common.Properties.Resources.MSG_OBJECT_REFERENCE_IS_NULL ) );
                    }   // TRUE (anticipated outcome) block, if ( exWrapped.InnerException != null )
                    else
                    {
                        TraceLogger.WriteWithBothTimesLabeledLocalFirst ( Properties.Resources.TRACEMSG_EXCEPTION_NOT_THROWN_9 );
                    }   // FALSE (unanticipated outcome) block, if ( exWrapped.InnerException != null )
                }   // catch ( Exception exAllKinds )
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < astrDefaultErrorMessageColors.Length ; intJ++ )

            return rutpCounts;
		}	// SetPropertiesFromDLLConfiguration
		#endregion  // Public Methods


        #region Private Instance Methods
		/// <summary>
		/// Create an object of the correct type from a string. Please see the
		/// Remarks section for important information.
		/// </summary>
		/// <param name="piThisProperty">
		/// The PropertyInfo identifies the name and type of the property to be
		/// converted. Please see the Remarks section for important information.
		/// </param>
		/// <param name="strConfigValueString">
		/// Since they are essentially untyped, the inputs from application
		/// configuration files always arrive as a string.
		/// </param>
		/// <returns>
		/// The return value is a new object of the desired type, containing
		/// enough type information to permit it to be implicitly cast to the
		/// desired ultimate type.
		/// </returns>
		/// <remarks>
		/// For conversion purposes, objects fall into two groups: enumerations,
		/// and everything else.
		/// 
		/// Segregating inputs into the correct group is done by evaluating
		/// the PropertyType.BaseType property of the input PropertyInfo object,
		/// since enumerations derive from a distinct base type, System.Enum.
		/// 
		/// Everything else derives from System.Object, and its conversion is
		/// handled by the SetValue method on the supplied PropertyInfo object.
		/// </remarks>
		private object FromString (
			PropertyInfo piThisProperty ,
			string strConfigValueString )
		{
			//	----------------------------------------------------------------
			//	Conversion of enumerations is handled by the static Parse method
			//	on he System.Enum type, using the PropertyType property of the
			//	input PropertyInfo object to designate the expected output type.
			//	The object returned by Enum.Parse is a generic Object of the
			//	specified  enumerated type, which any subsequent operation
			//	recognizes as if it were cast to that type in the first place.
			//
			//	This routine should have a catch block, because Enum.Parse
			//	throws if it gets anything that it doesn't like. However, since
			//	the caller has one, which the exception will find when the stack
			//	unwinds, this method runs under its protection.
			//	----------------------------------------------------------------

			object robjNew = new object ( );

			if ( piThisProperty.PropertyType.BaseType == typeof ( Enum ) )
			{
				robjNew = Enum.Parse (
					piThisProperty.PropertyType ,
					strConfigValueString );
			}	// TRUE (The object of interest is an enumeration.) block, if ( piThisProperty.GetType ( ).BaseType == typeof ( Enum ) )
			else
			{
				piThisProperty.SetValue (
					robjNew ,
					strConfigValueString ,
					BindingFlags.SetProperty ,
					null ,
					null ,
					null );
			}	// FALSE (The object of interest is some other type.) block, if ( piThisProperty.GetType ( ).BaseType == typeof ( Enum ) )

			return robjNew;
		}	// FromString
		

		private string FullyQualifiedDLLConfigFileName ( bool pfIsForShow )
        {
            if ( pfIsForShow )
                return string.Format (
                    DLL_CONFIG_FILE_NAME_TEMPLATE ,
                    _strAssemblyConfigPath ,
                    DLL_CONFIG_FILE_SUFFIX );
            else
                if ( _fLoadedFromGAC )
                    return _strStartupAssemblyLocation;
                else
                    return _strAssemblyConfigPath;
        }   // private string FullyQualifiedDLLConfigFileName


		/// <summary>
		/// Both constructors call this method to finish initializing the
		/// instance. Private string member _strAssemblyLocation must be
		/// initialized before the constructor calls this method.
		/// </summary>
		private void InitializeInstance ( )
		{

            #if DEBUG_MESSAGES_WW
                Console.WriteLine (
                    DEBUG_MESSAGES_WW_TPL ,
                    System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                    "_strAssemblyLocation" ,
                    _strAssemblyLocation );
            #endif	// #if DEBUG_MESSAGES_WW

			if ( this.GetType ( ).Module.Assembly.GlobalAssemblyCache )
			{   // Assembly loaded from GAC. Use application path.
				Assembly asmStartup = Assembly.GetEntryAssembly ( );
				_strStartupAssemblyLocation = asmStartup.Location;
				_strAssemblyDataPath = Path.GetDirectoryName ( _strStartupAssemblyLocation );
				_strAssemblyConfigPath = Path.Combine (
					_strAssemblyDataPath ,
					Path.GetFileName ( _strAssemblyLocation ) );
				_fLoadedFromGAC = true;      // Remember the answer.

                #if DEBUG_MESSAGES_WW
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_strStartupAssemblyLocation" ,
                        _strStartupAssemblyLocation );
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_strAssemblyDataPath" ,
                        _strAssemblyDataPath );
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_strAssemblyConfigPath" ,
                        _strAssemblyConfigPath );
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_fLoadedFromGAC" ,
                        _fLoadedFromGAC );
                #endif	// #if DEBUG_MESSAGES_WW
			}   // TRUE block, if ( this.GetType ( ).Module.Assembly.GlobalAssemblyCache )
			else
			{   // Assembly loaded from application directory. Use its path.
				_strAssemblyDataPath = Path.GetDirectoryName ( _strAssemblyLocation );
				_strAssemblyConfigPath = _strAssemblyLocation;
				_fLoadedFromGAC = false;     // Set it anyway, to be clear.

                #if DEBUG_MESSAGES_WW
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_strAssemblyDataPath" ,
                        _strAssemblyDataPath );
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_strAssemblyConfigPath" ,
                        _strAssemblyConfigPath );
                    Console.WriteLine (
                        DEBUG_MESSAGES_WW_TPL ,
                        System.Reflection.MethodBase.GetCurrentMethod ( ).Name ,
                        "_fLoadedFromGAC" ,
                        _fLoadedFromGAC );
                #endif	// #if DEBUG_MESSAGES_WW
			}   // FALSE block, if ( this.GetType ( ).Module.Assembly.GlobalAssemblyCache )
		}	// InitializeInstance
		#endregion  // Private Instance Methods
    }   // public abstract class AssemblyLocatorBase
}   // namespace WizardWrx.Core