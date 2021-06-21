﻿/*
    ============================================================================

    Namespace:			WizardWrx.AssemblyUtils

    Class Name:			DependentAssemblies

    File Name:			DependentAssemblies.cs

    Synopsis:			Instances of this class gather, sort, and process lists
                        of the dependents of an assembly.

    Remarks:			The Microsoft .NET Framework defers loading an assembly
                        until one of its public classes is referenced. However, 
                        once loaded, an assembly is never unloaded from the
                        AppDomain into which it was loaded. 

                        Hence, to generate a realistic report that reflects the
                        assemblies that would actually load into a process, the
                        dependent assemblies that have yet to see action must be
                        explicitly loaded into an AppDomain.

                        Since an assembly cannot be unloaded for the remaining
                        lifetime of the AppDomain into which it is loaded, the
                        simplest solution is to load each assembly into its own
                        private AppDomain. However, this is not straightforward,
                        because a default assembly load puts copies in both the
                        target AppDomain and the primary AppDomain. Since the
                        Assembly class doesn't derive from MarshalByRefObject,
                        this is accomplished by creating such an object, and
                        letting it own the assembly reference.

                        Finally, the MarshalByRefObject handle is unwrapped in
                        the primary AppDomain, which gets access to the assembly
                        through a factory method on the MarshalByRefObject.

    Algorithm:			Both constructors call a private initialization routine
                        that calls GetReferencedAssemblies on the assembly that
                        was stored into a private Assembly member. The output of
                        this method call is an array of AssemblyName objects
                        that fully describe the dependent assemblies. Next, a
                        generic List of DependentAssemblyInfo objects that has
                        room for the same number of items is constructed and
                        populated by enumeration. Finally, the list is sorted,
                        AppDomain.CurrentDomain.GetAssemblies is called to get a
                        list of the assemblies currently loaded into the primary
                        AppDomain (Every process has a default AppDomain.), and
                        the resulting array is matched against the list of
                        DependentAssemblyInfo objects to identify assemblies not
                        currently loaded.

                        Each assembly that is not already loaded is loaded into
                        a private AppDomain, and a reference to it is returned
                        through a method and stored in the DependentAssemblyInfo
                        list.

                        To report the properties of the dependent assemblies,
                        the DependentAssemblyInfo is enumerated once more, and
                        the properties of each assembly are queried through the
                        reference returned through one of its properties.						

    Author:				David A. Gray

    License:            Copyright (C) 2017-2021, David A. Gray.
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

    Date       Version  Author Synopsis
    ---------- -------- ------ --------------------------------------------------
    2017/03/30 7.0      DAG    This class makes its first appearance.

    2017/04/04 7.0      DAG    Account for null DomainManager in the default
                               AppDomain, which doesn't exist unless the default
                               application domain is created by the Visual
                               Studio hosting process, or another mechanism that
                               creates a domain manager.

    2018/09/09 7.0      DAG    GetDependentAssemblyByName: Correctly account for
                               the subject assembly being already loaded into
                               the current domain.

    2019/12/14 7.23.120 DAG    Correct internal documentation errors as I
                               prepare to create a freestanding routine that
                               walks the dependency tree of an assembly. The
                               new method is GetDependentAssemblyInfos.

    2019/12/22 7.23.121 DAG    Replace the foreach loops in DisplayProperties,
                               EnumerateDependents, and InitializeInstance with
                               more efficient old-school FOR loops, and feed the
                               loop control variables to the report writers.

    2021/06/21 8.0.187  DAG    Make the list of dependents visible through a
                               read-only property, NamesOfDependentAssemblies.

                               Owing to its simplicity, I dispensed with a test.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace WizardWrx.AssemblyUtils
{
    /// <summary>
    /// Use instances of this class to enumerate the dependents of an assembly.
    /// </summary>
    public class DependentAssemblies
    {
        #region Constructors
        /// <summary>
        /// Create an instance that has the calling assembly as its root.
        /// </summary>
        public DependentAssemblies ( )
        {
            //	----------------------------------------------------------------
            //	If the application is started by the Visual Studio hosting
            //	process, its default AppDomain has a DomainManager property that
            //	returns the name of the assembly that is under test, while the
            //	Assembly.GetEntryAssembly method would return the assembly that
            //	owns the hosting process, which is of no particular interest.
            //
            //	There are probably other cases in which a default Application
            //	Domain has a Domain Manager, but I suspect that you would still
            //	rather see the name of the "real" assembly, which would be
            //	hidden for the same reason that it is when the default AppDomain
            //	is created by the Visual Studio hosting process.
            //
            //	Incidentally, I start with the null-coalescing operator, which
            //	cannot be used here, because the parent object (e. g., the
            //	DomainManager) must exist.
            //	----------------------------------------------------------------

            _asmRoot = AppDomain.CurrentDomain.DomainManager == null
                ? Assembly.GetEntryAssembly ( )									// Default application domains usually come without a domain manager.
                : AppDomain.CurrentDomain.DomainManager.EntryAssembly;			// If it has one, though, we'll use its EntryAssembly property.
            InitializeInstance ( );
        }	// DependentAssemblies Constructor (1 of 2)


        /// <summary>
        /// Create an instance that has a specified assembly as its root.
        /// </summary>
        /// <param name="pasmTopLevel">
        /// Specify the assembly to establish as the top level reference assembly.
        /// </param>
        public DependentAssemblies ( Assembly pasmTopLevel )
        {
            _asmRoot = pasmTopLevel;
            InitializeInstance ( );
        }	// DependentAssemblies Constructor (2 of 2)
        #endregion	// Constructors


        #region Public Methods
        /// <summary>
        /// Return TRUE if the root assembly of the instance depends upon the
        /// assembly named in its argument.
        /// </summary>
        /// <param name="panMaybeDependent">
        /// Specify the AssemblyName property, preferably fully qualified.
        /// </param>
        /// <returns>
        /// If the assembly specified as the root when the instance was created
        /// depends upon the assembly named in the argument, the return value is
        /// TRUE. Otherwise, the return value is FALSE.
        /// </returns>
        public bool AssemblyDependsUpon ( AssemblyName panMaybeDependent )
        {
            return _lstNamesOfDependentAssemblies.BinarySearch ( new DependentAssemblyInfo ( panMaybeDependent ) ) > ListInfo.BINARY_SEARCH_NOT_FOUND;
        }	// AssemblyDependsUpon


        /// <summary>
        /// Return TRUE if the root assembly of the instance depends upon the
        /// assembly named in its argument AND that assembly  is loaded.
        /// </summary>
        /// <param name="panMaybeDependent">
        /// Specify the AssemblyName property, preferably fully qualified.
        /// </param>
        /// <returns>
        /// If the assembly specified as the root when the instance was created
        /// depends upon the assembly named in the argument, and the named
        /// assembly is loaded into the default Application Domain, the return
        /// value is TRUE. Otherwise, the return value is FALSE.
        /// </returns>
        public bool DependentAssemblyIsLoaded ( AssemblyName panMaybeDependent )
        {
            int intSlot = _lstNamesOfDependentAssemblies.BinarySearch (
                new DependentAssemblyInfo (
                    panMaybeDependent ) );

            if ( intSlot > ListInfo.BINARY_SEARCH_NOT_FOUND )
            {	// Since the named assembly is a dependent, evaluate its IsLoaded property.
                return _lstNamesOfDependentAssemblies [ intSlot ].IsLoaded;
            }	// TRUE (The named assembly is a dependent of the root assembly.) block, if ( intSlot > ListInfo.BINARY_SEARCH_NOT_FOUND )
            else
            {	// Since the named assembly is not a dependent, whether it is loaded is irrelevant.
                return false;
            }	// FALSE (The named assembly is not a dependent of the root assembly.) block, if ( intSlot > ListInfo.BINARY_SEARCH_NOT_FOUND )
        }	// DependentAssemblyIsLoaded


        /// <summary>
        /// This object gets an explicitly implemented destructor, because it
        /// may acquire a secondary AppDomain that should be destroyed before
        /// the main processing routine progresses further.
        /// </summary>
        public void DestroyDependents ( )
        {
            int intNDependencies = _lstNamesOfDependentAssemblies.Count;

            for ( int intJ = ArrayInfo.ARRAY_IS_EMPTY ;
                      intJ < intNDependencies ;
                      intJ++ )
            {
                _lstNamesOfDependentAssemblies [ intJ ].DestroyOwneAppdDomains ( );
            }   // for ( int intJ = ArrayInfo.ARRAY_IS_EMPTY ; intJ < intNDependencies ; intJ++ )
        }	// DestroyDependents


        /// <summary>
        /// List the properties of each dependent assembly.
        /// </summary>
        /// <param name="pswOut">
        /// Specify the optional output StreamWriter onto which the dependent
        /// assembly details are to be written. The default value is NULL, which
        /// suppresses output.
        /// </param>
        /// <param name="pchrDelimiter">
        /// Specify the optional field delimiter. The default value is a comma.
        /// </param>
        public void DisplayProperties (
            StreamWriter pswOut = null ,
            char pchrDelimiter = SpecialCharacters.COMMA )
        {
            Console.WriteLine (
                Properties.Resources.MSG_ASM_DEPENDENTS_DETAILS_HEAD ,			// Format Control String
                Environment.NewLine );											// Format Item 2 = Embedded Newline

            //	----------------------------------------------------------------
            //	Load any dependents that haven't already loaded in due course.
            //	Each of these DLLs loads into a private Application Domain, so
            //	that it can be unloaded before the collection goes out scope.
            //
            //	Deferring this operation until this routine executes means that
            //	DestroyDependents need not be called unless this method runs.
            //	----------------------------------------------------------------

            if ( pswOut != null )
            {	// If present, the stream is expected to be open for writing.
                ReportGenerators.LabelKeyAssemblyProperties (
                    pswOut ,
                    pchrDelimiter );
            }	// if ( pswOut != null )

            int intNDependents = _lstNamesOfDependentAssemblies.Count;

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < intNDependents ;
                      intJ++ )
            {	// Load dependents that haven't already been loaded.
                DependentAssemblyInfo daiCurrent = _lstNamesOfDependentAssemblies [ intJ ];

                if ( !daiCurrent.IsLoaded )
                {	// Load the assembly into a dedicated AppDomain, so that it can be unloaded.
                    daiCurrent.LoadForInspection ( );
                }	// FALSE (The application created a custom domain, into which it loaded the assembly.) block, if ( daiCurrent.IsLoaded )

                ReportGenerators.ShowKeyAssemblyProperties (
                    daiCurrent.AssemblyDetails ,
                    intJ ,
                    intNDependents );

                if ( pswOut != null )
                {	// If present, the stream is expected to be open for writing.
                    ReportGenerators.ListKeyAssemblyProperties (
                        daiCurrent.AssemblyDetails ,
                        pswOut ,
                        pchrDelimiter );
                }	// if ( pswOut != null )
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNDependents ; intJ++ )

            Console.WriteLine (
                Properties.Resources.MSG_ASM_DEPENDENTS_DETAILS_TAIL ,			// Format Control String
                _asmRoot.FullName ,												// Format Item 0 = Assembly Full Name
                Environment.NewLine );											// Format Item 1 = Embedded Newline
            DestroyDependents ( );
        }	// DisplayProperties method


        /// <summary>
        /// Enumerate the dependent assemblies.
        /// </summary>
        public void EnumerateDependents ( )
        {
            string strDispCount = NumberFormatters.Integer ( _lstNamesOfDependentAssemblies.Count );
            int intMaxWidth = strDispCount.Length;
            string strDtlFmt = string.Concat ( "    {0,", intMaxWidth , "}: {1}" );
            Console.WriteLine (
                Properties.Resources.MSG_ASM_DEPENDENTS_LIST_HEADER ,			// Format Control String
                _asmRoot.FullName ,												// Format Item 0 = Assembly Full Name
                strDispCount ,													// Format Item 1 = Dependent Count
                Environment.NewLine );											// Format Item 2 = Embedded Newline

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < _lstNamesOfDependentAssemblies.Count ;
                      intJ++ )
            {
                Console.WriteLine (
                    strDtlFmt ,													// Format Control String
                    NumberFormatters.Integer (                                  // Format Item 0 = Item number
                        ArrayInfo.OrdinalFromIndex (                            // int pintAnyInteger
                            intJ ) ) ,					                        // int pintIndex
                    _lstNamesOfDependentAssemblies [ intJ ].FullName );			// Format Item 1 = Assembly Full Name
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < _lstNamesOfDependentAssemblies.Count ; intJ++ )

            Console.WriteLine (
                Properties.Resources.MSG_ASM_DEPENDENTS_LIST_TAIL ,				// Format Control String
                _asmRoot.FullName ,												// Format Item 0 = Assembly Full Name
                Environment.NewLine );											// Format Item 1 = Embedded Newline
        }	// EnumerateDependents


        /// <summary>
        /// When called on an instance, this method returns a sorted list of the
        /// dependent assemblies of the assembly that was passed into its
        /// constructor.
        /// </summary>
        /// <returns>
        /// If the method succeeds, the return value is a generic list of
        /// DependentAssemblyInfo objects, sorted by name.
        /// </returns>
        public List<DependentAssemblyInfo> GetDependentAssemblyInfos ( )
        {
            return _lstNamesOfDependentAssemblies;
        }   // public List<DependentAssemblyInfo> GetDependentAssemblyInfos


        /// <summary>
        /// Get a reference to the named dependent assembly.
        /// </summary>
        /// <param name="panMaybeDependent">
        /// Specify the AssemblyName property, preferably fully qualified.
        /// </param>
        /// <param name="pfDynamicLoadingPermitted">
        /// Set this flag to TRUE to permit an assembly to be loaded to satisfy
        /// the request. The default is FALSE, so that a request is unsatisfied
        /// unless the required assembly is already loaded.
        /// </param>
        /// <returns>
        /// If the named assembly is a dependent, and it is successfully loaded,
        /// the return value is a reference to the assembly. If the assembly was
        /// already loaded into the default application domain, the reference is
        /// to that assembly. Otherwise, the reference is to the assembly that
        /// was loaded into the private application domain.
        /// </returns>
        public Assembly GetDependentAssemblyByName (
            AssemblyName panMaybeDependent ,
            bool pfDynamicLoadingPermitted = false )
        {
            int intSlot = _lstNamesOfDependentAssemblies.BinarySearch (
                new DependentAssemblyInfo (
                    panMaybeDependent ) );

            if ( intSlot > ListInfo.BINARY_SEARCH_NOT_FOUND )
            {   // Since the named assembly is a dependent, evaluate its IsLoaded property.
                if ( _lstNamesOfDependentAssemblies [ intSlot ].IsLoaded )
                {
                    return _lstNamesOfDependentAssemblies [ intSlot ].AssemblyDetails;
                }   // TRUE (degenerate case) block, if ( _lstNamesOfDependentAssemblies [ intSlot ].IsLoaded )
                else
                {
                    if ( pfDynamicLoadingPermitted )
                    {   // Permission is granted to create a private application domain into which to load the assembly.
                        if ( !_lstNamesOfDependentAssemblies [ intSlot ].IsLoaded )
                        {   // If it isn't loaded into the default application domain, load it into a private domain, so that it can be unloaded.
                            _lstNamesOfDependentAssemblies [ intSlot ].LoadForInspection ( );
                        }   // if ( !_lstNamesOfDependentAssemblies [ intSlot ].IsLoaded )

                        return _lstNamesOfDependentAssemblies [ intSlot ].AssemblyDetails;
                    }   // TRUE (Dynamic loading is permitted.) block, if ( pfDynamicLoadingPermitted )
                    else
                    {   // Since the assembly isn't loaded, its use is prohibited.
                        return null;
                    }   // FALSE (Dynamic loading is forbidden.) block, if ( pfDynamicLoadingPermitted )
                }   // FALSE (standard case) block, if ( _lstNamesOfDependentAssemblies [ intSlot ].IsLoaded )
            }	// TRUE (The named assembly is a dependent of the root assembly.) block, if ( intSlot > ListInfo.BINARY_SEARCH_NOT_FOUND )
            else
            {	// Since the named assembly is not a dependent, whether it is loaded is irrelevant, and we won't try.
                return null;
            }	// FALSE (The named assembly is not a dependent of the root assembly.) block, if ( intSlot > ListInfo.BINARY_SEARCH_NOT_FOUND )
        }	// GetDependentAssemblyByName
        #endregion	// Public Methods


        #region Public Properties
        /// <summary>
        /// Return a reference to the DependentAssemblyInfo List.
        /// </summary>
        public List<DependentAssemblyInfo> NamesOfDependentAssemblies
        {
            get
            {
                return _lstNamesOfDependentAssemblies;
            }   // public NamesOfDependentAssemblies getter method
        }   // public NamesOfDependentAssemblies property


        /// <summary>
        /// Get the root assembly around which the instance was constructed.
        /// </summary>
        public Assembly Root
        {
            get
            {
                return _asmRoot;
            }	// public Root property getter method
        }	// public Root property
        #endregion	// Public Properties


        #region Private Methods
        private void InitializeInstance ( )
        {
            AssemblyName [ ] aasmNames = _asmRoot.GetReferencedAssemblies ( );
            _lstNamesOfDependentAssemblies = new List<DependentAssemblyInfo> ( aasmNames.Length );

            {   // Build a scope fence around loop index intNDependents.
                int intNDependents = aasmNames.Length;

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < intNDependents ;
                          intJ++ )
                {
                    _lstNamesOfDependentAssemblies.Add ( new DependentAssemblyInfo ( aasmNames [ intJ ] ) );
                }	// for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNDependents ; intJ++ )
            }   // Let intNDependents go out of scope.

            _lstNamesOfDependentAssemblies.Sort ( );		// Sorting is a prerequisite of the binary search algorithm.

            //	----------------------------------------------------------------
            //	Identify and mark dependents that are currently loaded.
            //	----------------------------------------------------------------

            Assembly [ ] asmAlreadyLoaded = AppDomain.CurrentDomain.GetAssemblies ( );

            {   // Build a scope fence around loop index intNLoadedDependents.
                int intNLoadedDependents = asmAlreadyLoaded.Length;

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < intNLoadedDependents ;
                          intJ++ )
                {
                    DependentAssemblyInfo daiCurrent = new DependentAssemblyInfo ( asmAlreadyLoaded [ intJ ].GetName ( ) );
                    int intMatch = _lstNamesOfDependentAssemblies.BinarySearch ( daiCurrent );

                    if ( intMatch > ListInfo.BINARY_SEARCH_NOT_FOUND )
                    {
                        _lstNamesOfDependentAssemblies [ intMatch ].MarkAsLoaded ( asmAlreadyLoaded [ intJ ] );
                    }   // if ( intMatch > ListInfo.BINARY_SEARCH_NOT_FOUND )
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNLoadedDependents ; intJ++ )
            }   // Let intNLoadedDependents go out of scope. (This is insurance against new code being added below that extends the scope of the method.)
        }	// InitializeInstance
        #endregion	// Private Methods


        #region Private Instance Storage
        private Assembly _asmRoot;
        private List<DependentAssemblyInfo> _lstNamesOfDependentAssemblies;
        #endregion	// Private Instance Storage
    }	// public class DependentAssemblies
}	// partial namespace WizardWrx.AssemblyUtils