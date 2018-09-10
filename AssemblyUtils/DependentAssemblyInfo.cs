/*
    ============================================================================

    Namespace:			WizardWrx.AssemblyUtils

    Class Name:			DependentAssemblyInfo

	File Name:			DependentAssemblyInfo.cs

    Synopsis:			The primary focus of this class is its properties, which
						store information about an assembly that is a dependent
						of another assembly.

    Remarks:			Though the focus is on its properties, the 3 instance
						methods play essential roles in its lifetime.

						1)	DestroyOwneAppdDomains must be called once on each
							instance, after you are finished with it, and before
							it is destroyed or goes out of scope, to unload any
							assembly that was forcibly loaded into the process,
							by unloading the private AppDomain into which it was
							loaded.

						2)	LoadForInspection creates a private AppDomain, into
							which the assembly named in the FullName property is
							loaded, so that its properties can be queried.

						3)	MarkAsLoaded does as its name suggests, by placing a
							reference to the namesake assembly into a private
							property, from which it is retrieved for reporting.

						To support list sorting, this class explicitly
						implements the IComparable interface. Following common
						practice for classes that implement IComparable, this
						class also overrides the Equals and GetHashCode methods.

						Finally, to make its properties more easily visible in
						watch windows, this class overrides ToString.

    Author:				David A. Gray

	License:            Copyright (C) 2017, David A. Gray.
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
    2017/03/30 7.0     DAG    This class makes its first appearance.

	2017/07/12 7.0	   DAG    Override the ToString method on the base class,
							  object, to display the subsystem type of the
							  process that owns the singleton.
    ============================================================================
*/


using System;
using System.Reflection;
using System.Runtime.Remoting;

namespace WizardWrx.AssemblyUtils
{
	/// <summary>
	/// An instance of this class is created for each assembly listed as a
	/// dependent, and is used to track the assemblies that must be loaded to
	/// query their properties.
	/// </summary>
	public class DependentAssemblyInfo : IComparable<DependentAssemblyInfo>
	{
		#region Public Symbolic Constants
		/// <summary>
		/// The IsLoaded property has this value when the assembly is loaded
		/// when the DependentAssemblies query loop runs.
		/// </summary>
		public const bool IS_LOADED = true;

		/// <summary>
		/// The IsLoaded property has this value when the assembly is unloaded
		/// when the DependentAssemblies query loop runs.
		/// </summary>
		public const bool NOT_LOADED = false;
		#endregion	// Public Symbolic Constants


		#region Constructors
		private DependentAssemblyInfo ( )
		{ }	// DependentAssemblyInfo Constructor 1 of 2 is private.

		/// <summary>
		/// The public constructor requires an AssemblyName from which to
		/// create an initialized instance.
		/// </summary>
		/// <param name="panmAssemblyName">
		/// AssemblyName fully describes an assembly, including properties that
		/// give direct access to the base (simple) name, version, culture, and
		/// public key token.
		/// </param>
		public DependentAssemblyInfo ( AssemblyName panmAssemblyName )
		{
			_anMFullName = panmAssemblyName;
		}	// DependentAssemblyInfo Constructor 2 of 2 is public.
		#endregion	// Constructors


		#region Instance Methods
		/// <summary>
		/// You must call this method once, when you are finished using the
		/// object, but before it goes out of scope, to unload the private
		/// AppDomain, along with the assemblies that were loaded into it.
		/// </summary>
		/// <remarks>
		/// This activity cannot be performed by a destructor, because the
		/// unload fails with HRESULT 0x80131015 when the unload is initiated by
		/// a destructor, or when a destructor is active.
		/// </remarks>
		public void DestroyOwneAppdDomains ( )
		{	// The domain refused to unload because it was invoked within a Finalize method.
			if ( _ad4Reflection != null )
			{	// Destroy the cross-domain references, then discard the domain.
				_asmJar = null;
				AppDomain.Unload ( _ad4Reflection );
			}	// if ( _ad4Reflection != null )
		}	// DestroyOwneAppdDomains Destructor
		/// <summary>
		/// The name of this method reflects its motivation, which was to report
		/// on the assemblies upon which a specified "root" assembly depends.
		/// </summary>
		public void LoadForInspection ( )
		{
			_ad4Reflection = AppDomain.CreateDomain (
				String.Format (
					Properties.Resources.MSG_PRIVATE_DOMAIN_NAME_TEMPLATE ,
					_anMFullName ) );
			ObjectHandle ohAssemblyContainer = _ad4Reflection.CreateInstance (
				Assembly.GetExecutingAssembly ( ).FullName ,
				typeof ( AssemblyContainer ).FullName );
			_asmJar = ( AssemblyContainer ) ohAssemblyContainer.Unwrap ( );
			ohAssemblyContainer = null;
			_asmJar.Store ( _anMFullName );
		}	// LoadForInspection

		/// <summary>
		/// Call this method to mark an assembly as loaded.
		/// </summary>
		public void MarkAsLoaded ( Assembly pasmThis )
		{
			_asm = pasmThis;
		}	// MarkAsLoaded
		#endregion	// Instance Methods


		#region Overridden Base Methods
		/// <summary>
		/// Test a pair of object instances for logical equality.
		/// </summary>
		/// <param name="obj">
		/// Supply a reference to the other object to test for equality with the
		/// calling instance.
		/// </param>
		/// <returns>
		/// This method returns TRUE if the two objects are of the same or
		/// equivalent types and their FullName properties are equal. Otherwise,
		/// the return value is FALSE.
		/// </returns>
		public override bool Equals ( object obj )
		{
			if ( obj == null )
			{
				return false;
			}	// TRUE (degenerate case 1 of 2) block, if ( obj == null )
			else if ( obj.GetType ( ) == typeof ( DependentAssemblyInfo ) )
			{
				DependentAssemblyInfo daiOther = ( DependentAssemblyInfo ) obj;
				return _anMFullName.Equals ( daiOther._anMFullName );
			}	// TRUE (anticipated outcome) block, else if ( obj.GetType ( ) == typeof ( DependentAssemblyInfo ) )
			else
			{
				return false;
			}	// FALSE (degenerate case 2 of 2, unanticipated outcome) block, else if ( obj.GetType ( ) == typeof ( DependentAssemblyInfo ) )
		}	// Equals

		/// <summary>
		/// Return the HashCode property of the FullName property of the instance.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode ( )
		{
			return _anMFullName.GetHashCode ( );
		}	// GetHashCode

		/// <summary>
		/// Return a string representation of the instance.
		/// </summary>
		/// <returns>
		/// The returned string consists of the object type, as it would be
		/// reported by the base ToString method, followed by the FullName and
		/// IsLoaded property values.
		/// </returns>
		public override string ToString ( )
		{
			return string.Format (
				Properties.Resources.DEPENDENT_ASSEMBLYINFO_TOSTRING_TEMPLATE ,	// Format Control String
				this.GetType ( ).FullName ,										// Format Item 0 = Fully qualified type name
				_anMFullName ,													// Format Item 1 = Instance FullName property
				this.IsLoaded );												// Format Item 2 = Instance IsLoaded property
		}	// ToString
		#endregion	// Overridden Base Methods


		#region IComparable<DependentAssemblyInfo> Members
		int IComparable<DependentAssemblyInfo>.CompareTo ( DependentAssemblyInfo other )
		{
			return  _anMFullName.FullName.CompareTo ( other._anMFullName.FullName );
		}	// CompareTo Method
		#endregion	// IComparable<DependentAssemblyInfo> Members


		#region Properties
		/// <summary>
		/// Get the Assembly, itself. If it was already loaded for use, this is
		/// a reference to the live assembly. Otherwise, it is a copy that was
		/// loaded for reflection, and will be unloaded by the destructor.
		/// </summary>
		public Assembly AssemblyDetails
		{
			get
			{
				if ( _ad4Reflection == null )
				{
					return _asm;
				}	// TRUE (Assembly was already loaded.) block, if ( _ad4Reflection == null )
				else
				{
					if ( _asmJar != null)
					{
						return _asmJar.ShowMe ( );
					}	// TRUE (The object is alive.) block, if ( _asmJar != null)
					else
					{
						return null;
					}	// FALSE (The object is destroying itself.) block, if ( _asmJar != null)
				}	// FALSE (Assembly loaded into its own domain.) block, if ( _ad4Reflection == null )
			}	// public Assembly AssemblyDetails Property Getter Method
		}	// public Assembly AssemblyDetails Property

		/// <summary>
		/// Get the FullName of the assembly.
		/// </summary>
		public string FullName { get { return _anMFullName.FullName; } }

		/// <summary>
		/// Get the load state of the assembly.
		/// </summary>
		public bool IsLoaded { get { return _asm != null; } }
		#endregion	// Properties


		#region Private Instance Storage
		AssemblyName _anMFullName;
		Assembly _asm;
		AppDomain _ad4Reflection;
		AssemblyContainer _asmJar;
		#endregion	// Private Instance Storage
	}	// public class DependentAssemblyInfo
}	// partial namespace WizardWrx.AssemblyUtils