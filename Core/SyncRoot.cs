/*
    ============================================================================

	Namespace Name:		WizardWrx

	Class Name:			SyncRoot

	File Name:			SyncRoot.cs

	Synopsis:			This module contains the complete definition of a
						SyncRoot class, which provides a way to document a
						lock by giving it a displayable label.

	Dependencies:		This class uses only two base classes from the Microsoft
						.NET Framework Base Class Library, Object and String.

	Remarks:			1)	The default constructor generates an unlabeled lock
							object.

						2)	This class was defined and tested in the course of
							creating the TimeStampFactory class, which has since
							been moved to the WizardWrx.DateMath library, along
							with a private copy of this class.

							Putting a private copy of this class in library
							WizardWrx.DateMath avoids coupling that library to
							this one.

						3)	The label is made available as a public read-only
							property, but it can be set only by the constructor.

						For all practical purposes, this class is LockWithLabel,
						with a new name.

	References:			1)	"Choosing What To Lock On"
							http://www.yoda.arachsys.com/csharp/threads/lockchoice.shtml

						2)	"Implementing the Singleton Pattern in C#"
							http://www.yoda.arachsys.com/csharp/singleton.html

						3)	"C Sharp Programming/Keywords/lock"
							http://en.wikibooks.org/wiki/C_Sharp_Programming/Keywords/lock

						4)	"Building Multithreaded Applications—Synchronization
							Using the C# lock Keyword"
							http://en.csharp-online.net/Building_Multithreaded_Applications%E2%80%94Synchronization_Using_the_CSharp_lock_Keyword

	Author:				David A. Gray

	License:            Copyright (C) 2010-2015, David A. Gray. 
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

	Contact:			dgray@wizardwrx.com

	Date Written:		Saturday, 20 March 2010

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Author Synopsis
    ---------- ------ ----------------------------------------------------------
    2010/03/20 DAG    Initial version.
	2010/03/31 DAG    Merge into this library, and mark class public.
	2010/04/04 DAG    Supply missing XML documentation.
	2011/04/04 DAG    Unseal the class, so that other classes can inherit it.
	2015/06/20 DAG    Re-seal the class, having concluded that there is no good
                      reason that another class should be able to inherit it,
                      and leave the label uninitialized.
	2016/06/07 DAG    Adjust the internal documentation to correct a few
                      inconsistencies uncovered while preparing this library for
                      publication on GetHub.
	2017/02/19 DAG    This class is promoted to the root WizardWrx namespace, 
                      and moved to WizardWrx.Core.dll.
	2017/07/12 DAG    Override the ToString method to display the label.
    ============================================================================
*/

namespace WizardWrx
{
	/// <summary>
	/// Use instances of this class to provide classes that must be made
	/// thread-safe with locks over which the class has complete control.
	/// </summary>
	/// <remarks>
	/// Use labeled instances when you expect to have multiple locks active,
	/// especially during the lifetime of a single method, or across calls to
	/// two or more related methods.
	/// 
	/// This class cannot be inherited.
	/// </remarks>
	public sealed class SyncRoot
	{
        /// <summary>
        /// This string provides internal storage for the optional object label.
        /// </summary>
		private string _strLabel = null;

		/// <summary>
		/// Create an unlabeled instance.
		/// </summary>
		public SyncRoot ( ) { }	// Constructor (1 of 2)

		/// <summary>
		/// Create a labeled lockable object.
		/// </summary>
		/// <param name="pstrLabel">
		/// Label to assign to the instance.
		/// </param>
		public SyncRoot ( string pstrLabel )
		{
			_strLabel = pstrLabel;
		}	// Constructor (2 of 2)

		/// <summary>
		/// Return the label assigned to this instance. Labels are read only.
		/// You must use the overloaded constructor to create a labeled
		/// instance.
		/// </summary>
		public string Label { get { return _strLabel; } }

		/// <summary>
		/// Display the label inside French braces, followed by the fully
		/// qualified class name, similar to the way many BCL classes render in
		/// debugger watch windows.
		/// </summary>
		/// <returns>
		/// The returned string is the label, followed by the fully qualified
		/// class name.
		/// </returns>
		public override string ToString ( )
		{	// The resources must be fully qualified because this class lives in the root namespace, while they live in a subsidiary namespace.
			return string.Format (
				WizardWrx.Core.Properties.Resources.SYNCROOT_TOSTRING_TEMPLATE ,// Format Control String
				this.GetType ( ).FullName ,										// Format Item 0 = Fully Qualified Class Name
				_strLabel );													// Format Item 1 = Label property
		}	// public override string ToString
	}	// public sealed class SyncRoot
}	// Partial namespace WizardWrx