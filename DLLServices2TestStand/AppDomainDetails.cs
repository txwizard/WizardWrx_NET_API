/*
    ============================================================================

    Namespace:          DLLServices2TestStand

    Class Name:			AppDomainDetails

	File Name:			AppDomainDetails.cs

    Synopsis:			This class defines two subroutines that I created in
						the module that defines the entry point routine, and
						moved here because I expect to grab this whole module,
						and move it into one of the libraries in the DllServices
						constellation.

    Remarks:			These routines use numerous string resources that must 
						be copied into the library into which I move it. Surely,
						it must be possible to segregate sets of resource
						strings, to simplify incorporating them into another
						project!

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
 `	2017/03/31 1.0	   DAG    Class created, tested, and deployed.

	2017/04/02 1.1     DAG    Tweak the output a bit, and substantially improve
                              the internal documentation throughout.
	2017/04/04 7.0     DAG    Import into DllServicesTestStand.

	2017/08/26 7.0     DAG    Conform the format to my standards for production.
    ============================================================================
*/

using System;

using WizardWrx.AssemblyUtils;


namespace DLLServices2TestStand
{
	class AppDomainDetails
	{
		/// <summary>
		/// Report selected properties of the default Application Domain.
		/// </summary>
		public static void DisplayDefaultAppDomainProperties ( )
		{
			AppDomain adDefault = AppDomain.CurrentDomain;

			Console.WriteLine (
				Properties.Resources.MSG_DFLT_DOMAIN_PROPS_HEADER ,				// Format Control String
				Environment.NewLine );											// Format Item 0 = Embedded Newline

			if ( adDefault.ActivationContext != null )
			{	// Get the ApplicationIdentity from the ActivateionContext property.
				Console.WriteLine (
					Properties.Resources.MSG_DFLT_DOMAIN_IDENTITY_FULLNAME ,	// Format Control String
					adDefault.ActivationContext.Identity.FullName );			// Format Item 0 = FullName property of ApplicationIdentity
				Console.WriteLine (
					Properties.Resources.MSG_DFLT_DOMAIN_IDENTITY_CODEBASE ,	// Format Control String
					adDefault.ActivationContext.Identity.CodeBase );			// Format Item 0 = CodeBase property of ApplicationIdentity
			}	// TRUE (anticipated outcome) block, if ( adDefault.ActivationContext != null )
			else
			{	// Report that the ActivateionContext property is null.
				Console.WriteLine ( Properties.Resources.MSG_NULL_ACTIVATION_CONTEXT );
			}	// FALSE (unanticipated outcome) block, if ( adDefault.ActivationContext != null )

			if ( adDefault.ApplicationIdentity != null )
			{	// Get the ApplicationIdentity from the AppDomain, itself.
				Console.WriteLine (
					Properties.Resources.MSG_DFLT_DOMAIN_IDENTITY_FULLNAME ,	// Format Control String
					adDefault.ApplicationIdentity.FullName );					// Format Item 0 = FullName property of ApplicationIdentity
				Console.WriteLine (
					Properties.Resources.MSG_DFLT_DOMAIN_IDENTITY_CODEBASE ,	// Format Control String
					adDefault.ApplicationIdentity.CodeBase );					// Format Item 0 = CodeBase property of ApplicationIdentity
			}	// TRUE (anticipated outcome) block, if ( adDefault.ApplicationIdentity != null )
			else
			{	// Report that the ApplicationIdentity property is null.
				Console.WriteLine ( Properties.Resources.MSG_DFLT_APP_IDENTITY_NULL );
			}	// FALSE (unanticipated outcome) block, if ( adDefault.ApplicationIdentity != null )

			Console.WriteLine (
				Properties.Resources.MSG_BASE_DIRECTORY_NAME ,					// Format Control String
				adDefault.BaseDirectory );										// Format Item 0 = BaseDirectory property of ApplicationDomain

			if ( adDefault.DomainManager != null )
			{	// Get the AssemblyName and Location properties from the DomainManager property of the Application Domain.
				Console.WriteLine (
					Properties.Resources.MSG_ENTRY_ASM_FULLNAME ,				// Format Control String
					adDefault.DomainManager.EntryAssembly.FullName );			// Format Item 0 = FullName property of EntryAssembly on the DomainManager property
				Console.WriteLine (
					Properties.Resources.MSG_ENTRY_ASM_LOCATION ,				// Format Control String
					adDefault.DomainManager.EntryAssembly.Location );			// Format Item 0 = Location property of EntryAssembly on the DomainManager property
			}	// TRUE (outcome when the Visual Studio Hosting Process is present) block, if ( adDefault.DomainManager != null )
			else
			{	// Report that the DomainManager property is null.
				Console.WriteLine ( Properties.Resources.MSG_NO_DOMAIN_MANAGER );
			}	// FALSE (outcome when the Visual Studio Hosting Process is absent) block, if ( adDefault.DomainManager != null )

			Console.WriteLine (
				Properties.Resources.MSG_DOMAIN_MGR_FRIENDLY_NAME ,				// Format Control String
				adDefault.FriendlyName );										// Format Item 0 = FriendlyName of the AppDomain, by itself, shows whether the hosting process is active
			Console.WriteLine (
				Properties.Resources.MSG_DOMAIN_MGR_ID ,						// Format Control String
				adDefault.Id );													// Format Item 0 = AppDomain ID, which is probably always 1 for the default application domain

			if ( adDefault.SetupInformation != null )
			{	// List a couple of interesting properties of the SetupInformation property.
				Console.WriteLine (
					Properties.Resources.MSG_DOMAIN_MGR_APP_NAME ,				// Format Control String
					adDefault.SetupInformation.ApplicationName );				// Format Item 0 = ApplicationName property on SetupInformation
				Console.WriteLine (
					Properties.Resources.MSG_DOMAIN_MGR_APP_BASE ,				// Format Control String
					adDefault.SetupInformation.ApplicationBase );				// Format Item 0 = ApplicationBase (initial working directory name) property on SetupInformation
			}	// TRUE (anticipated outcome) block, if ( adDefault.SetupInformation != null )
			else
			{	// Report that the SetupInformation property is null.
				Console.WriteLine ( Properties.Resources.MSG_DFLT_SETUP_INFO_NULL );
			}	// FALSE (unanticipated outcome) block, if ( adDefault.SetupInformation != null )

			PESubsystemInfo peAppDomainInfo = PESubsystemInfo.GetTheSingleInstance ( );

			Console.WriteLine (
				Properties.Resources.MSG_APP_DOMAIN_ENTRY_ASM_NAME ,			// Format Control String
				peAppDomainInfo.DefaultDomainEntryAssemblyName ,				// Format Item 0 = Always the real entry assembly (the one that matters to us!)
				Environment.NewLine );											// Format Item 1 = Embedded Newline
			Console.WriteLine (
				Properties.Resources.MSG_APP_DOMAIN_ENTRY_ASM_LOCATION ,		// Format Control String
				peAppDomainInfo.DomainEntryAssemblyLocation );					// Format Item 0 = The location (absolute file name) from which the real entry assembly loaded
			Console.WriteLine (
				Properties.Resources.MSG_APP_DOMAIN_SUBSYSTEM ,					// Format Control String
				new object [ ]													// Since there are more than three format items, the call must pass an array, which is constructed on the stack.
				{
					peAppDomainInfo.DefaultAppDomainSubsystemID ,				// Format Item 0 = Windows subsystem ID of the entry assembly identified just above
					peAppDomainInfo.DefaultAppDomainSubsystem ,					// Format Item 1 = PESubsystemID enumeration mapping of the DefaultAppDomainSubsystemID
					PESubsystemInfo.GetPESubsystemDescription (					// Format Item 2 = Long (verbose) description of the Windows subsystem in which the assembly executes
						peAppDomainInfo.DefaultAppDomainSubsystem ,				//		PESubsystemID penmSubsystemID designates the Windows subsystem in which the assembly executes
						PESubsystemInfo.SubsystemDescription.Long ) ,			//		SubsystemDescription penmSubsystemDescription identifies which of two descriptions (short or long) is wanted
					Environment.NewLine											// Format Item 3 = Embedded Newline
				} );

			Console.WriteLine (
				Properties.Resources.MSG_DFLT_DOMAIN_PROPS_FOOTER ,				// Format Control String
				Environment.NewLine );											// Format Item 0 = Embedded Newline
		}	// DisplayDefaultAppDomainProperties


		/// <summary>
		/// Report selected properties of the process and its main assemblies.
		/// </summary>
		public static void DisplayProcessProperties ( DateTime dtmStartedUtc )
		{
			System.Diagnostics.Process procCurrent = System.Diagnostics.Process.GetCurrentProcess ( );

			Console.WriteLine (
				Properties.Resources.MSG_PROCESS_PROPERTIES_HEADER ,			// Format Control String
				Environment.NewLine );											// Format Item 0 = Embedded Newline

			Console.WriteLine (
				Properties.Resources.MSG_PROCESS_ENTRY_ASM_NAME ,				// Format Control String
				procCurrent.MainModule.FileName );								// Format Item 0 = FileName property of the MainModule property of the Process object
			Int16 intPSSubsystemID = PESubsystemInfo.GetPESubsystemID (
				procCurrent.MainModule.FileName );
			Console.WriteLine (
				Properties.Resources.MSG_PROCESS_ENTRY_ASM_SUBSYSTEM ,			// Format Control String
				new object [ ]													// Since there are more than three format items, the call must pass an array, which is constructed on the stack.
				{
					intPSSubsystemID ,											// Format Item 0 = Windows subsystem in which the MainModule assembly executes
					( PESubsystemInfo.PESubsystemID ) intPSSubsystemID ,		// Format Item 1 = PESubsystemID enumeration mapping of the Windows subsystem in which the MainModule assembly executes
					PESubsystemInfo.GetPESubsystemDescription (					// Format Item 2 = Long (verbose) description of the Windows subsystem in which the assembly executes
						intPSSubsystemID ,										//		PESubsystemID penmSubsystemID designates the Windows subsystem in which the assembly executes
						PESubsystemInfo.SubsystemDescription.Long ) ,			//		SubsystemDescription penmSubsystemDescription identifies which of two descriptions (short or long) is wanted
					Environment.NewLine											// Format Item 3 = Embedded Newline
				} );
			Console.WriteLine (
				Properties.Resources.MSG_PROCESS_ID ,							// Format Control String
				procCurrent.Id ,												// Format Item 0 = Process ID, hexadecimal representation
				procCurrent.Id ,												// Format Item 1 = Process ID, decimal representation
				Environment.NewLine );											// Format Item 2 = Embedded Newline
			Console.WriteLine (
				Properties.Resources.MSG_PROCESS_STARTUP_TIME ,					// Format Control String
				procCurrent.StartTime ,											// Format Item 0 = Process Start Time is reported as local machine time
				procCurrent.StartTime.ToUniversalTime ( ) );					// Format Item 1 = Process Start Time, converted to UTC time
			Console.WriteLine (
				Properties.Resources.MSG_CURRENT_MACHINE_TIME ,					// Format Control String
				dtmStartedUtc.ToLocalTime ( ) ,									// Format Item 0 = dtmStartedUtc, initialized with UTC time when the initializer ran
				dtmStartedUtc );												// Format Item 1 = dtmStartedUtc, converted to UTC time

			Console.WriteLine (
				Properties.Resources.MSG_PROCESS_PROPERTIES_FOOTER ,			// Format Control String
				Environment.NewLine );											// Format Item 0 = Embedded Newline
		}	// public static void DisplayProcessProperties
	}	// class AppDomainDetails
}	// partial namespace DLLServices2TestStand