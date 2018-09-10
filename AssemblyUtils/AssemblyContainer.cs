/*
    ============================================================================

    Namespace:			WizardWrx.AssemblyUtils

    Class Name:			AssemblyContainer

	File Name:			AssemblyContainer.cs

    Synopsis:			The methods

    Remarks:			

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
    2017/03/27 7.0     DAG    This class makes its first appearance.
    ============================================================================
*/


using System;
using System.Reflection;

namespace WizardWrx.AssemblyUtils
{
	/// <summary>
	/// Use this class to hold a reference to an assembly that you want to
	/// confine to a separate AppDomain, so that the assembly can be unloaded by
	/// discarding its domain.
	/// </summary>
	public class AssemblyContainer : MarshalByRefObject
	{
		/// <summary>
		/// The public constructor creates an empty container.
		/// </summary>
		public AssemblyContainer ( ) { }

		/// <summary>
		/// Call this method to load an assembly into the container.
		/// </summary>
		/// <param name="panThis">
		/// Designate the assembly to load by its AssemblyName.
		/// </param>
		public void Store ( AssemblyName panThis )
		{
			_pvtAsm = Assembly.Load ( panThis );
		}	// public void Store Method

		/// <summary>
		/// Get a transparent reference to the assembly stored in the container.
		/// </summary>
		/// <returns>
		/// The reference is returned through a transparent proxy, and the main
		/// AppDomain can treat it as if it were local. Hence, it can be used to
		/// instantiate objects, query their properties, and call their methods.
		/// </returns>
		public Assembly ShowMe ( ) { return _pvtAsm; }

		/// <summary>
		/// The real assembly reference is hidden in this property, so that it
		/// isn't created simultaneously in both AppDomains.
		/// </summary>
		private Assembly _pvtAsm;
	}	// class AssemblyContainer
}	// partial namespace WizardWrx.AssemblyUtils