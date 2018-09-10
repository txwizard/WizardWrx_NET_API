/*
    ============================================================================

    Namespace:          WizardWrx.ConsoleStreams

    Class Name:         DefaultErrorMessageColors

	Inheritance Tree:	WizardWrx.ConsoleStreams.DefaultErrorMessageColors
						- WizardWrx.Core.PropertyDefaults
						  - WizardWrx.Core.AssemblyLocatorBase
 
    File Name:          DefaultErrorMessageColors.cs

    Synopsis:           Expose the default foreground and background colors kept
                        in a standard Microsoft .NET application configuration
                        file that is linked to a dynamic-link library, rather
                        to an executable.

    Remarks:            This class is used in one of two ways, depending on the
                        circumstances.

						1)	The static constructor of class ErrorMessagesInColor
							loads default values from a configuration file that
							is linked to the DLL that hosts it.

						2)	The ExceptionLogger constructor, which is hosted by
							sibling DLL WizardWrx.DLLConfigurationManager.dll,
                            calls a static method on the ErrorMessagesInColor
							class that calls the overloaded constructor that
							links it to that DLL.

    Author:             David A. Gray

    License:            Copyright (C) 2017, David A. Gray. All rights reserved.

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

    Created:            Sunday, 18 May 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2017/02/26 7.0     DAG    This class makes its debut.

	2017/07/12 7.0     DAG    Override the ToString on the base (object) class.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Reflection;

using WizardWrx;
using WizardWrx.Common;
using WizardWrx.Core;

namespace WizardWrx.ConsoleStreams
{
	/// <summary>
	/// Expose the default fatal and nonfatal exception message colors, which
	/// are defined in a standard application configuration file that is linked
	/// to the assembly that defines this class.
	/// </summary>
	public class DefaultErrorMessageColors : PropertyDefaults
	{
		/// <summary>
		/// The default constructor deviates from the usual pattern by
		/// explicitly calling the base constructor overload that accepts a
		/// reference to the assembly that defines this class, with the
		/// objective of linking it to the application configuration file
		/// that is linked to this DLL.
		/// </summary>
		public DefaultErrorMessageColors ( )
			: base ( typeof ( DefaultErrorMessageColors ).Assembly )
		{
			InitializeInstance ( );
		}	// DefaultErrorMessageColors constructor (1 of 2)

		/// <summary>
		/// The overloaded constructor adheres to the usual pattern by
		/// explicitly calling the base constructor with a reference to the DLL
		/// specified by the caller, with the objective of linking it to the
		/// application configuration file that is linked to aNonther DLL.
		/// </summary>
		/// <param name="pasmLinkedAssembly">
		/// Specify the assembly to which the desired configuration file is
		/// linked.
		/// </param>
		public DefaultErrorMessageColors ( System.Reflection.Assembly pasmLinkedAssembly )
			: base ( pasmLinkedAssembly )
		{
			InitializeInstance ( );
		}	// DefaultErrorMessageColors constructor (2 of 2)

		//	--------------------------------------------------------------------
		//	Properties are public, since there is no obvious way to make them
		//	read/write with different access modifiers.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Get or set the default text color for rendering reports of fatal
		/// exceptions.
		/// </summary>
		public ConsoleColor FatalExceptionTextColor
		{
			get { return _clrFatalExceptionTextColor; }
			set { _clrFatalExceptionTextColor = value; }
		}	// FatalExceptionTextColor property

		/// <summary>
		/// Get or set the default background color for rendering reports of 
		/// fatal exceptions.
		/// </summary>
		public ConsoleColor FatalExceptionBackgroundColor
		{
			get { return _clrFatalExceptionBackgroundColor; }
			set { _clrFatalExceptionBackgroundColor = value; }
		}	// FatalExceptionBackgroundColor property

		/// <summary>
		/// Get or set the default text color for rendering reports of
		/// recoverable exceptions.
		/// </summary>
		public ConsoleColor RecoverableExceptionTextColor
		{
			get { return _clrRecoverableExceptionTextColor; }
			set { _clrRecoverableExceptionTextColor = value; }
		}	// RecoverableExceptionTextColor property

		/// <summary>
		/// Get or set the default background color for rendering reports of
		/// recoverable exceptions.
		/// </summary>
		public ConsoleColor RecoverableExceptionBackgroundColor
		{
			get { return _clrRecoverableExceptionBackgroundColor; }
			set { _clrRecoverableExceptionBackgroundColor = value; }
		}	// RecoverableExceptionBackgroundColor property


		/// <summary>
		/// Get the count of properties that were set from the linked
		/// configuration file.
		/// </summary>
		public int PropsSetFromConfig
		{
			get { return _intPropsSetFromConfig; }
		}	// Read only PropsSetFromConfig property


		/// <summary>
		/// Override the default ToString method on the base class (object) to
		/// show the key properties followed by the fully qualified class name.
		/// </summary>
		/// <returns>
		/// Return a string similar to the following.
		/// 
		/// Template: {Fatal: Text = FatalExceptionTextColor, FatalExceptionBackgroundColor = BackgroundColor; Recoverable: Text = RecoverableExceptionTextColor, Background = RecoverableExceptionBackgroundColor} WizardWrx.ConsoleStreams.DefaultErrorMessageColors
		/// Template: {{WizardWrx.ConsoleStreams.DefaultErrorMessageColors (Fatal: Text (Foreground) = FatalExceptionTextColor, Background = FatalExceptionBackgroundColor; Recoverable: Text (Foreground) = RecoverableExceptionTextColor, Background = RecoverableExceptionBackgroundColor)}}
		/// </returns>
		public override string ToString ( )
		{
			return string.Format (
				Properties.Resources.DEFAULTERRORMESSAGECOLORS_TOSTRING_TEMPLATE ,
				new object [ ]													// Template: {{{0} (Fatal: Text (Foreground) = {1}, Background = {2}; Recoverable: Text (Foreground) = {3}, Background = {4})}}
				{
					this.GetType ( ).FullName ,									// Format Item 0 = Fully qualified name of object
					_clrFatalExceptionTextColor ,								// Format Item 1 = Text (foreground) color of fatal exception message text
					_clrFatalExceptionBackgroundColor ,							// Format Item 2 = Background color of fatal exception message text
					_clrRecoverableExceptionTextColor ,							// Format Item 3 = Text (foreground) color of recoverable (nonfatal) exception message text
					_clrRecoverableExceptionBackgroundColor ,					// Format Item 4 = Background color of recoverable (nonfatal) exception message text
																
				} );
		}	// public override string ToString


		//	--------------------------------------------------------------------
		//	Storage is private, as is the instance initializer.
		//	--------------------------------------------------------------------

		/// <summary>
		/// Since both constructors perform identical initialization tasks, the
		/// initializer is factored into a dedicated routine, to decrease the
		/// maintenance burden.
		/// </summary>
		private void InitializeInstance ( )
		{	// This one-statement initializer is future-proofing. In its present form, the optimizer should inline it.
			_intPropsSetFromConfig = base.SetPropertiesFromDLLConfiguration ( typeof ( DefaultErrorMessageColors ) );
		}	// InitializeInstance

		private ConsoleColor _clrFatalExceptionTextColor;
		private ConsoleColor _clrFatalExceptionBackgroundColor;

		private ConsoleColor _clrRecoverableExceptionTextColor;
		private ConsoleColor _clrRecoverableExceptionBackgroundColor;

		private int _intPropsSetFromConfig = MagicNumbers.ZERO;
	}	// public class DefaultErrorMessageColors
}	// partial namespace WizardWrx.ConsoleStreams