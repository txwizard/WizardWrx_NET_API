/*
	============================================================================

	Namespace Name:     WizardWrx.DLLConfigurationManager

	Class Name:         ExceptionLogger

	File Name:          ExceptionLogger.cs

	Synopsis:           This module contains the entire definition of a utility
						class, ExceptionLogger, originally called
						ThreadSafeExceptionReporting, and is intended to provide
						thread-safe exception reporting for all applications
						(Console, Windows Forms, ASP.NET, etc).

						Since each thread has its own stack frame, and all data
						is either contained in an argument or allocated locally
						(with its address kept in the stack frame), all methods
						should be thread safe.

	Remarks:            To ensure that the entire exception report is kept
						together, even if another thread throws the same kind of
						exception, messages are sent to the console in a single
						string, with one synchronized stream I/O operation.

						Though it is almost certainly overkill, as extra
						insurance, access to the AppEventSourceID property is
						synchronized for both reading and writing.

						Although I have confirmed that EventLog.WriteEntry can
						include both event ID and event Category values, I shall
						defer adding the necessary overloads to support it until
						another day.

	References:         1)  "Statics & Thread Safety: Part II," K. Scott Allen
							http://odetocode.com/Articles/314.aspx

						2)  The original ThreadSafeExceptionReporter class is
							defined in the following source file.

							%USERPROFILE%\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\TimeStampGenerator\TimeStampGenerator\ThreadSafeExceptionReporters.cs

	Author:             David A. Gray

	License:            Copyright (C) 2010-2019, David A. Gray. All rights reserved.

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

	Contact:            dagray@p6c.com

	Date Written:       Tuesday, 23 March 2010 and Wednesday, 24 March 2010

	----------------------------------------------------------------------------
	Revision History
	----------------------------------------------------------------------------

	Date       Version Author Synopsis
	---------- ------- ------ --------------------------------------------------
	2010/03/24 1.0     DAG    Initial version.

	2010/10/22 2.52    DAG    1) Move this class from TimeStampGenerator, the
								 project in which it was created and tested, so
								 that other projects that write to the console
								 can use it.

							  2) Remove the lock dependency by replacing each
								 series of console writes with appends to a
								 StringBuilder, followed by a synchronized
								 console write.

								 This changes takes advantage of the recently
								 noticed AppendFormat method of the workhorse
								 StringBuilder class.

	2011/12/02 2.65    DAG    Add reporting of inner exceptions by recursively
							  calling private method AddCommonElements.

	2011/11/12 2.8     DAG    Move ThreadSafeExceptionReporting from SharedUtl2,
							  rename it ExceptionLogger, and change methods from
							  void to string, so that the return values can be
							  recycled as event log records. To support CUI or
							  GUI applications from one code base, it becomes
							  a private singleton, which is exposed as a read
							  only property of the ApplicationInstance object.

	2014/02/05 3.2     DAG    Add support for the new MessageInColors class, for
							  use in character mode applications.

	2014/03/24 3.3     DAG    1) Most overloads of ReportException were ignoring
								 the AppStackTraceDisposition property setting,
								 and blindly including the stack trace in every
								 message.

							  2) If event logging is enabled, see to it that the
								 message that goes into the event log always has
								 the stack trace attached to it.

							  3) Replace string constants that become part of a
								 message with named resource strings.

	2014/03/28 4.0     DAG    Enable greater control over the content and format
							  of the string displayed on the console when an
							  instance is configured to run in Console mode, and
							  display its own messages.

	2014/05/19 4.1     DAG    Use my spiffy new IP6CUtilLib1.GetProcessSubsystem
							  method to identify the subsystem and set a default
							  value for the Subsystem property.

							  This class is moving! This morning, it occurred to
							  me that it belongs in this library, which is at a
							  lower architectural level that allows me to break
							  the tight bond between the ApplicationHelpers and
							  ConsoleAppAids2 namespace.

							  CLASS RELOCATION NOTICE

							  It finally occurred to me that I already have a
							  class that is an ideal candidate to house these
							  classes that were the source of the tight coupling
							  between the WizardWrx.ApplicationHelpers and
							  WizardWrx.ConsoleAppAids2. Moving the tightly
							  coupled classes into this class, which was created
							  to house a single abstract base class decouples
							  both libraries from each other, in exchange fro a
							  common coupling to this class library, yielding a
							  much more robust architecture that will be easier
							  to maintain.

	2014/06/06 5.0     DAG    1) Fix the code associated with saving and
								 restoring ErrorMessageInColor settings in
								 configuration files, and add synchronization of
								 old and new flag values to all read/write
								 properties.

							  2) Correct typographical errors in the
								 IntelliSense documentation that I discovered
								 while chasing down the source of a type
								 conversion exception that was thrown at run
								 time by a class that consumes this class and
								 its converters.

							  3) Relocate this class to a new DLLServices2
								 assembly, which is not signed with a strong
								 name. Like the other classes that I moved here,
								 this one keeps its place in the namespace
								 hierarchy.

	2014/07/20 5.1     DAG    1) Correct an oversight that left this class in
								 the old WizardWrx.ApplicationHelpers namespace.
								 Since this change affects only two other DLLs
								 and, at most one user program, I took advantage
								 of the opportunity to promote the DLLServices2
								 namespace to the first rank under the overall
								 WizardWrx namespace.

							  2) Add methods for saving and restoring color
								 settings, to permit the default settings stored
								 in the configuration file to be overridden at
								 run time.

							  3) Account for the renaming of the methods in the
								 color message writers for STDOUT and STDERR.

	2015/06/05 5.4     DAG    Insert missing XML documentation for argument
							  penmOutputOptions of method AddCommonElements.

	2015/06/20 5.5     DAG    Protect the static arrays of constants against
							  accidental or malicious changes.

	2015/09/01 5.7     DAG    Correct spelling errors flagged by the spelling
							  checker add-in that I recently installed into the
							  Visual Studio 2013 IDE, along with a documentation
							  XML error that I overlooked.

	2016/04/06 6.0     DAG    ExceptionLogger BREAKING CHANGE ALERT:
							  --------------------------------------

							  Simplify the ReportException interface to get the
							  name of the failing method from the TargetSite
							  property.

							  The break eliminates the second argument from all
							  method signatures.

							  To prevent breaking existing code, the old methods
							  are left, marked as obsolete, to elicit a compiler
							  warning the next time a dependent program is
							  compiled.

	2016/04/12 6.0     DAG    Replace the generic (C++ oriented) implementation
							  with an implementation optimized for the .NET
							  Framework, which eliminates the need for double-
							  checked locking in the constructor.

	2016/05/10 6.0     DAG    Undefine preprocessor symbol SHOW_TYPE_HANDLES, and
							  reinstate the message type name, which isn't there,
							  since the new table is unfinished, though it works
							  for looking up the prefixes.

	2016/05/12 6.1     DAG    Define instance and static methods that return the
							  strings reserved for reporting a run-time error
							  through console mode exception reporting routine
							  ConsoleAppAids2.ConsoleAppStateManager.ErrorExit.

							  While the static method must rely on the caller to
							  specify which of the two messages to use to report
							  a run-time exception, the instance methods can use
							  properties of the instance to make that decision,
							  subject to overriding by the calling assembly.

	2016/06/06 6.3     DAG    1) SetDefaultOptions was harmlessly setting the
								 Method bit twice.

							  2) ReportAsDirected was printing twice on standard
								 output unless that stream was redirected. To
								 prevent a disastrous recursive calling loop, a
								 new instance method, GetStandardHandleStates,
								 gets the standard handle states from the
								 StateManager, which calls this method once both
								 objects have been constructed.

	2017/02/27 7.0     DAG    Break this library apart, so that smaller subsets
							  of classes can be distributed and consumed
							  independently.

							  The only direct effect of this change on this
							  module is that a static method that was moved out
							  of its DLL must be more fully qualified.
  
							  The other effect of this change on this module is
							  it moves into a new namespace, and the new library
							  depends upon WizardWrx.Common, WizardWrx.Core, and
							  WizardWrx.ConsoleStreams, the other three class
							  libraries spawned by the subdivision.

							  Since this is, for all practical purposes, a new
							  library, I removed the deprecated properties and
							  methods. Since the obsolete ReportException
							  methods occupied an entire region, they were easy
							  to safely remove.

							  Finally, using Marshal.GetHRForException() in 
							  System.Runtime.InteropServices eliminated the
							  dependency on the NewtonSoft JSON library to 
							  subclass the I/O exception and obtain its HRESULT.

	2017/02/24 7.0     DAG    Use the AppDomain.CurrentDomain.DomainManager to
							  reliably set the subsystem property, regardless of
							  how the singleton springs into existence.

	2017/04/03 7.0     DAG    Adapt to renaming of PESubsystemInfo property 
                              ProcessSubsystmID to DefaultAppDomainSubsystem,
                              and list all dependent assemblies in the Using
                              directives.

	2017/06/27 7.0     DAG    Change the AppEventSourceID property get method so
                              that it calls GetDefaultEventSourceID if its value
                              is uninitialized, as happens if the property is
                              queried before any exceptions are logged.

	2017/07/11 7.0     DAG    Adjust to support the improved 100% managed code
                              that processes standard console handles.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.
 
	2017/09/10 7.0     DAG    1) Substitute or supplement newlines with a 
                                 NOLBREAKING_SPACE (ASCII code 160), depending
                                 on the settings of two new flags.
 
                              2) Add an intelligent ScrollUp method that handles
                                 whichever of the console streams is receiving
                                 messages, taking into account the same flags
                                 that ReportException uses.
 
	2017/09/16 7.0     DAG    Correct an error that caused subsequent lines of a
                              generic Exception message to truncated.
 
	2017/10/20 7.0     DAG    Handle the expected null reference exception that
                              arises when the DLL configuration file is missing
                              by generating and reporting a new exception that
                              describes the problem precisely.
 
	2018/09/09 7.0     DAG    Eliminate obsolete references to obsolete method
	                          IP6CUtilLib1.GetExeSubsystemID, which I replaced
							  with a 100% managed implementation, and adapt to
							  support partially initialized Exception objects.

	2018/12/24 7.14    DAG    Add a new GetTheSingleInstance overload that takes
                              only the OptionFlags parameter.

    2019/02/18 7.15    DAG    Define s_strSettingsOmittedFromConfigFile as a
                              static string property that returns a message that
                              lists the properties that are absent from the DLL
                              configuration file, along with their hard coded
                              default values.

	2020/09/20 7.23    DAG    Eliminate redundant assembly reference to Common.

	2020/12/21 7.24    DAG    1) GetDefaultEventSourceID becomes an instance
                                 method (like it matters for a singleton) to
                                 make the subsystem ID visible to it, so that it
                                 can skip everything related to console streams
                                 unless the execution environment is character
                                 mode.

                              2) ReportAsDirected gets the same treatment unless
                                 its execution context is character mode.
	============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using WizardWrx;
using WizardWrx.AssemblyUtils;
using WizardWrx.Core;
using WizardWrx.ConsoleStreams;


namespace WizardWrx.DLLConfigurationManager
{
	/// <summary>
	/// This Singleton class exposes methods for formatting data from instances
	/// of the System.Exception class, and commonly used derived classes, and
	/// displaying the formatted data on a console (strictly speaking, on
	/// STDOUT, which can be redirected to a file), and recording them in a
	/// Windows event log.
	///
	/// Unlike most of the classes defined in this library, this class may be
	/// inherited.
	///
	/// All methods of this class are thread-safe. See Remarks.
	/// </summary>
	/// <remarks>
	/// This class was developed to report exceptions in a multi threaded console
	/// application. Its methods were designed to report messages in a manner
	/// that is thread-safe, yet keeps each message intact.
	///
	/// Both objectives are achieved fairly easily.
	///
	/// 1) All methods use only local variables. The only class level static
	/// data is a handful of constants, which are, by definition, read only, and
	/// the private SyncRoot object used to synchronize access to the object and
	/// its properties. This satisfies the first condition of thread safety,
	/// because all methods have exclusive access to all of their data.
	///
	/// 2) Each message is built up, line by line, by appending to an instance
	/// of a StringBuilder class, using its AppendFormat method, which behaves
	/// like the static Format method of the System.String class. Although the
	/// AppendFormat method is an instance method, since the associated instance
	/// is local, thread safety remains intact. See Reference 1.
	///
	/// 2) A single call to TextWriter.Synchronized ( Console.Out ).WriteLine
	/// or TextWriter.Synchronized ( Console.Error ).WriteLine writes the
	/// message, all at once, onto the console, preserving its integrity. Since
	/// the only event that uses a shared resource is called once only, and that
	/// call is synchronized, the message display is intact.
	///
	/// Since each method uses the shared resource, access to the console
	/// Standard Error (STDERR) stream, once only, and does so in a synchronized
	/// (thread-safe) way, the entire method is thread-safe.
	///
	/// As a reminder to include WizardWrx.DLLServices2.dll in your projects, I
	/// left this class in the WizardWrx.DLLServices2 namespace. Only symbolic
	/// constants got promoted to the root namespace.
	/// </remarks>
	/// <seealso cref="PropertyDefaults"/>
	public class ExceptionLogger : GenericSingletonBase<ExceptionLogger>
	{
		#region Public Enumerations and Constants
		/// <summary>
		/// Use these flags to control the output. There is no flag for the
		/// Message property, which always becomes part of the message.
		/// </summary>
		/// <remarks>
		/// DiscardNewlines and NBSpaceForNewlines arose to resolve an issue
		/// that came to light when a desire arose to reconstruct an exception
		/// report from the message posted into a Microsoft Excel workbook that
		/// was populated from the output of a run of PsLogList.exe. To cause it
		/// to keep each message on a single logical line, so that it would
		/// import into a single worksheet row,  PsLogList.exe is invoked with
		/// its -s switch, causing PsLogList to print (output) Event Log records
		/// one-per-line, with comma delimited fields.
		/// 
		/// To preserve the newlines in the event log file and have some
		/// indication of their original positions when PsLogList strips
		/// them, set the NBSpaceForNewlines flag to TRUE. This causes each
		/// embedded newline to be preceded in the output by a NOLBREAKING_SPACE
		/// (ASCII character code 160, A0h, Unicode code point U+0A0) character.
		/// 
		/// To substitute a NOLBREAKING_SPACE for each newline, set this flag
		/// AND DiscardNewlines to TRUE.
		/// </remarks>
		/// <seealso cref="ErrorExitOptions"/>
		[Flags]
		public enum OutputOptions : byte
		{
			/// <summary>
			/// Use this member to evaluate whether all flags are OFF.
			/// </summary>
			NoFlags = 0x00 ,

			/// <summary>
			/// Show Method Name if TRUE.
			///
			/// If the EventLog flag is also set, the method name is always
			/// written there.
			/// </summary>
			Method = 0x01 ,

			/// <summary>
			/// Show Source (Assembly) Name if TRUE.
			///
			/// If the EventLog flag is also set, the originating assembly name
			/// is always written there.
			/// </summary>
			Source = 0x02 ,

			/// <summary>
			/// Show Stack Trace if TRUE.
			///
			/// If the EventLog flag is also set, the stack trace is always
			/// written there.
			/// </summary>
			Stack = 0x04 ,

			/// <summary>
			/// Post to associated event log if TRUE.
			///
			/// The value of the event source associated with the current
			/// ExceptionLogger instance determines which event log gets the
			/// message.
			///
			/// WARNING: Unless your event source string is already registered,
			/// the application MUST run, one time only, with full administrator
			/// privileges AND use the event source to write a message into the
			/// Windows event log in order for it to be registered with Windows,
			/// which maps it to an event log. Alternatively, you may register
			/// the event source by any number of other means, such as by way of
			/// an installation script.
			///
			/// Once the event source string is registered, the application can
			/// use it to post messages to the event log in any Windows security
			/// context.
			/// </summary>
			EventLog = 0x08 ,

			/// <summary>
			/// Write message on STDOUT if TRUE and if the application has a
			/// working console.
			/// 
			/// The setting of this flag also governs the behavior of the
			/// ScrollUp method.
			///
			/// CAUTION: Although it is technically legal to set both
			/// StandardOutput and StandardError to TRUE, this can have the
			/// unwanted consequence of generating TWO copies of the message,
			/// unless STDOUT and/or STDERR is directed to a file or if both are
			/// redirected to the SAME file.
			/// </summary>
			StandardOutput = 0x10 ,

			/// <summary>
			/// Write message on STDERR if TRUE and if the application has a
			/// working console.
			/// 
			/// The setting of this flag also governs the behavior of the
			/// ScrollUp method.
			///
			/// CAUTION: Although it is technically legal to set both
			/// StandardOutput and StandardError to TRUE, this can have the
			/// unwanted consequence of generating TWO copies of the message,
			/// unless STDOUT and/or STDERR is directed to a file or if both are
			/// redirected to the SAME file.
			/// </summary>
			StandardError = 0x20 ,

			/// <summary>
			/// Discard newlines entirely, unless NBSpaceForNewlines is also
			/// true, in which case each newline is retained, preceded by a
			/// NOLBREAKING_SPACE (ASCII character code 160, A0h, U+0A0) 
			/// character.
			/// 
			/// Please see the Remarks on the enumeration for important details.
			/// </summary>
			DiscardNewlines = 0x40 ,

			/// <summary>
			/// Precede each newline with a NOLBREAKING_SPACE (ASCII character 
			/// code 160, A0h, Unicode code point U+0A0) character, unless
			/// DiscardNewlines is also true, in which case a NOLBREAKING_SPACE
			/// precedes the newline, which is preserved.
			/// 
			/// Please see the Remarks on the enumeration for important details.
			/// </summary>
			NBSpaceForNewlines = 0x80 ,

			/// <summary>
			/// Deploy this flag combination to substitute a NOLBREAKING_SPACE
			/// (ASCII character code 160, A0h, Unicode code point U+0A0) for
			/// every platform-dependent newline. On a Windows platform, the
			/// outcome is a slightly shorter message string, since each space
			/// replaces two characters. In all cases, the NOLBREAKING_SPACE
			/// guarantees that each event record occupies one line when output
			/// as text, while providing a means of restoring the newlines by
			/// reversing the replacement.
			/// </summary>
			ReplaceNewlines = DiscardNewlines | NBSpaceForNewlines ,

			/// <summary>
			/// Use this bit mask to include all auxiliary properties of the
			/// Exception (StackTrace, TargetSite, Source, DiscardNewlines, and
			/// NBSpaceForNewlines) in the report, or to strip them form a set
			/// of flags.
			/// </summary>
			ActiveProperties = Method | Source | Stack | DiscardNewlines | NBSpaceForNewlines ,

			/// <summary>
			/// Use this bit mask to include all auxiliary properties of the
			/// Exception (StackTrace, TargetSite, and Source) in the report,
			/// and to send the report to a Windows event log.
			/// </summary>
			ActivePropsToEventLog = ActiveProperties | EventLog ,

			/// <summary>
			/// Use this bit mask to include all auxiliary properties of the
			/// Exception (StackTrace, TargetSite, and Source) in the report,
			/// and to send the report to the standard error stream.
			/// </summary>
			ActivePropsToStdErr = ActiveProperties | StandardError ,

			/// <summary>
			/// Use this bit mask to include all auxiliary properties of the
			/// Exception (StackTrace, TargetSite, and Source) in the report,
			/// and to send the report to the standard output stream.
			/// </summary>
			ActivePropsToStdOut = ActiveProperties | StandardOutput ,

			/// <summary>
			/// Use this bit mask to include all auxiliary properties of the
			/// Exception (StackTrace, TargetSite, and Source) in the report,
			/// and to send the report to a Windows event log and both the
			/// standard output and standard error streams. This is useful when
			/// you know that the standard output is redirected, and you want to
			/// preserve the report in the output file, in addition to having a
			/// copy displayed on the console.
			/// </summary>
			ActivePropsEverywhere = ActiveProperties | AllDestiations ,

			/// <summary>
			/// Use this flag to include all auxiliary properties of the 
			/// Exception (StackTrace, TargetSite, Source, DiscardNewlines, and
			/// NBSpaceForNewlines) in the report, or to strip them form a set
			/// of flags.
			/// </summary>
			AllProperties = ActiveProperties | AllDestiations ,

			/// <summary>
			/// Use this flag to include all destinations for output, or as a
			/// mask to strip them from a set of flags.
			/// </summary>
			AllDestiations = EventLog | StandardOutput | StandardError ,

			/// <summary>
			/// This flag turns on everything, and is of no practical use, but
			/// is defined for reference, to document that every bit in the byte
			/// is accounted for. Now that all bits have assignments, this flag
			/// and AllProperties resolve to the same value.
			/// </summary>
			AllFlags = 0xFF
		}  // OutputOptions


		/// <summary>
		/// Use with the static GetSpecifiedReservedErrorMessage method or with
		/// the overloaded instance GetReservedErrorMessage method that allows
		/// callers to override the behavior dictated by its properties.
		/// </summary>
		/// <remarks>
		/// The correspondence between the naming and numbering of the members
		/// of this enumeration is by design, since the two work hand in hand
		/// internally, and the consistency should simplify writing calls to the
		/// GetReservedErrorMessage methods.
		/// </remarks>
		/// <see cref="GetSpecifiedReservedErrorMessage"/>
		/// <see cref="GetSpecifiedReservedErrorMessage(ExceptionLogger.ErrorExitOptions)"/>
		/// <seealso cref="OutputOptions"/>
		[Flags]
		public enum ErrorExitOptions : byte
		{
			/// <summary>
			/// Execution succeeded; return the ERROR_SUCCESS placeholder for
			/// the table of error messages.
			/// </summary>
			Succeeded = 00 ,

			/// <summary>
			/// Details of the run-time exception were reported in a Windows
			/// Event Log.
			/// </summary>
			RecordedInEventLog = 0x08 ,

			/// <summary>
			/// Details of the run-time exception were listed on STDOUT if the
			/// application has a working console.
			/// </summary>
			RecordedInStandardOutput = 0x10 ,

			/// <summary>
			/// Details of the run-time exception were listed on STDERR if the
			/// application has a working console.
			/// </summary>
			RecordedInStandardError = 0x20 ,
		}	// ErrorExitOptions


		/// <summary>
		/// As a convenience, the ScrollUp method reports the result through a
		/// bit-mapped enumeration.
		/// </summary>
		[Flags]
		public enum ScrollUpResult
		{
			/// <summary>
			/// Nothing happened, most likely because the calling process runs
			/// in the Windows (graphical) User Interface subsystem, which is
			/// unsupported, though not treated as an outright failure.
			/// </summary>
			NoAction = 0x00 ,

			/// <summary>
			/// The standard error console stream was scrolled up one line.
			/// </summary>
			StandardError = 0x01 ,

			/// <summary>
			/// The standard output console stream was scrolled up one line.
			/// </summary>
			StandardOutput = 0x02 ,

			/// <summary>
			/// Both console streams were scrolled up one line, which happens
			/// only when the standard output stream is redirected AND messages
			/// are being reported on both streams.
			/// </summary>
			Both = StandardError | StandardOutput
		}	// ScrollUpResult


		/// <summary>
		/// This string defines a default event source ID, WizardWrx, which I
		/// register on behalf of my own applications.
		/// </summary>
		public const string WIZARDWRX_EVENT_SOURCE_ID = @"WizardWrx";
		#endregion  // Public Enumerations and Constants


		#region Static Data
		//	--------------------------------------------------------------------
		//	The initial implementation of this table had a Windows newline pair 
		//	as the truncation point for several messages, causing subsequent
		//	lines to be discarded. The error was exposed when I noticed that the
		//	second line of a two-line message wasn't being output. Since the
		//	routine that looks up exceptions by type in this table is expected
		//	to return a null reference for unlisted exception types, all that is
		//	required to resolve this issue is to leave those exceptions in the
		//	table, and set the corresponding values to NULL.
		//	--------------------------------------------------------------------
		static Dictionary<System.Guid , string> s_dctKnowExceptionTypes = new Dictionary<System.Guid , string> ( )
		{  // Exception Subtype                                    Suffix to Trim
			{ typeof ( System.Exception ).GUID                   , null                   } ,
			{ typeof ( System.ArgumentException ).GUID           , "\r\nParameter name: " } ,
			{ typeof ( System.ArgumentOutOfRangeException ).GUID , "\r\nParameter name: " } ,
			{ typeof ( System.ArgumentNullException ).GUID       , "\r\nParameter name: " } ,
			{ typeof ( System.ObjectDisposedException ).GUID     , "\r\nObject name: "    } ,
			{ typeof ( System.IO.IOException ).GUID              , null                   } ,
			{ typeof ( System.FormatException ).GUID             , null                   }
		};	// s_dctKnowExceptionTypes

		#if SHOW_TYPE_HANDLES
		static Type [ ] s_atypKnowExceptionTypes = new Type [ ] 
		{
			typeof ( System.Exception ) ,
			typeof ( System.ArgumentException ) ,
			typeof ( System.ArgumentOutOfRangeException ) ,
			typeof ( System.ArgumentNullException ) ,
			typeof ( System.ObjectDisposedException ) ,
			typeof ( System.IO.IOException ) ,
			typeof ( System.FormatException )
		};	// s_atypKnowExceptionTypes
		#endif	// SHOW_TYPE_HANDLES


		//  --------------------------------------------------------------------
		//  Overloads turn this off before they call the default
		//  GetTheSingleInstance method.
		//
		//  s_strDefaultEventSource is initialized if, and when, private static
		//  method GetDefaultEventSourceID is called upon to retrieve a default
		//  event source ID string from the DLL's private configuration file.
		//  --------------------------------------------------------------------

		private static bool s_fSynchronizeFlagsNow = true;
		private static string s_strDefaultEventSource = null;
		private static SyncRoot s_srCriticalSection = new SyncRoot ( typeof ( ExceptionLogger ).ToString ( ) );
		#endregion  // Static Data


		#region Instance Data Storage
		private string _strEventSource;                                         // Leave uninitialized until it is set through its property setter.

		private MessageInColor _emsgColorsStdOut;                               // Defer initialization until requested by the property setter.

		private ErrorMessagesInColor _emsgColorsStdErr;                         // Ditto, and they are created in tandem, using the same colors.
		private ErrorMessagesInColor _emsgSpecialColorsSave;

		private OutputOptions _enmOutputOptions = SetDefaultOptions ( );
		private OutputOptions _enmSavedOptions;

		private PESubsystemInfo.PESubsystemID _enmProcessSubsystem;
	
		//	--------------------------------------------------------------------
		//	These three flags were implemented to enable the private processing
		//	methods to avoid the overhead of repeatedly calling across the
		//	managed/unmanaged context boundary to gather information that never
		//	changes once a process gets underway. Though the calls have moved
		//	out of the custom native DLLs from which they were imported, there
		//	are still a handful of P/Invoke calls directly into the Windows API.
		//	--------------------------------------------------------------------

		private bool _fInstanceIsInitialized = false;
		private bool _fStdOutIsRedirected = false;
		private bool _fStdErrIsRedirected = false;
		#endregion  // Instance Data Storage


		#region Public Properties
		/// <summary>
		/// Along with the EventLoggingState property, this property governs
		/// recording of events in the Windows Application Event Log or in a
		/// custom event log of your choice.
		///
		/// The value of this property is the event source ID string to use. To
		/// simplify applications, this property has a default value, defined by
		/// WIZARDWRX_EVENT_SOURCE_ID.
		///
		/// IMPORTANT: Each event source ID string is machine specific, and each
		/// maps to one, and only one, event log, which is designated when the
		/// source is registered, as it must be before its first use.
		/// </summary>
		public string AppEventSourceID
		{
			get
			{
				lock ( s_srCriticalSection )
					return string.IsNullOrEmpty ( _strEventSource )
						? GetDefaultEventSourceID ( )
						: _strEventSource;
			}   // AppEventSourceID get method

			set
			{
				lock ( s_srCriticalSection )
					_strEventSource = value;
			}   // AppEventSourceID set method
		}   // AppEventSourceID (Read/Write)


		/// <summary>
		/// The value of this property is the actual subsystem ID reported by a
		/// fully managed function that yields the same result as the usual
		/// native (unmanaged) method.
		/// </summary>
		/// <remarks>
		/// This property uses GetExeSubsystem in lieu of GetProcessSubsystem to
		/// circumvent an anomaly in the Visual Studio debugging engine, which
		/// causes that function to return 2 (Windows GUI subsystem) when you
		/// use the Visual Studio Hosting Process, which runs in the Windows
		/// subsystem. The cause of this behavior is that the Visual Studio
		/// Hosting Process trades places with the process under study, becoming
		/// the first executable file loaded into the active process.
		/// </remarks>
		public PESubsystemInfo.PESubsystemID ApplicationSubsystem
		{
			get { return _enmProcessSubsystem; }
		}   // ApplicationSubsystem (Read only)


		/// <summary>
		/// Set this property to cause error messages to be displayed in a
		/// distinctive pair of foreground and background colors.
		///
		/// Unless the AppSubsystem property is Console or CUI, this property is
		/// meaningless.
		///
		/// Unlike other properties, ErrorMessageColors must be set directly,
		/// and it may be changed at any time.
		/// </summary>
		/// <remarks>
		/// A hidden MessageInColor is maintained in tandem with this property,
		/// for use with STDERR. In this way, messages written to either STDOUT
		/// or STDERR use the same color scheme.
		/// </remarks>
		public ErrorMessagesInColor ErrorMessageColors
		{
			get { return _emsgColorsStdErr; }      // A null reference is OK, and operates as a degenerate case.

			set
			{   //  ------------------------------------------------------------
				//  The two console output channels, standard output (STDOUT)
				//  and standard error (STDERR) have functionally identical sets
				//  of output routines that write to the appropriate channel.
				//  Since the active output channel is subject to change without
				//  notice, the simplest way to implement color output is to
				//  maintain an object of each class, ready for use if needed.
				//  ------------------------------------------------------------

				if ( value != null )
				{
					if ( value.MessageForegroundColor == value.MessageBackgroundColor )
					{   // Turn alternate colors off if both are same.
						_emsgColorsStdErr = null;
						_emsgColorsStdOut = null;
					}   // TRUE block, if ( value.MessageForegroundColor == value.MessageBackgroundColor )
					else
					{
						_emsgColorsStdErr = value;

						_emsgColorsStdOut = new MessageInColor (
							_emsgColorsStdErr.MessageForegroundColor ,
							_emsgColorsStdErr.MessageBackgroundColor );
					}   // FALSE block, if ( value.MessageForegroundColor == value.MessageBackgroundColor )
				}   //  TRUE block, if ( value != null )
				else
				{   // Set both color pairs to null.
					_emsgColorsStdErr = null;
					_emsgColorsStdOut = null;
				}   // FALSE block, if ( value != null )
			}     // Allow object to be destroyed by setting to NULL.
		}   // ErrorMessageColors (Read/Write)


		/// <summary>
		/// Gets or sets the new OutputOptions enumeration, which supersedes the
		/// obsolete individual flags.
		/// </summary>
		public OutputOptions OptionFlags
		{
			get
			{
				return _enmOutputOptions;
			}   // The initial value causes the console output to include everything but the stack.

			set
			{
				_enmOutputOptions = value;
			}  // The setter takes the input at face value, and keeps the legacy enumerations in sync.
		}   // OptionFlags (Read/Write)


		/// <summary>
		/// Return TRUE if the Standard Error console stream was redirected by
		/// the shell. Otherwise, the return value is FALSE.
		/// 
		/// This flag is meaningless unless the ApplicationSubsystem property
		/// value is equal to ProcessSubsystem.Console, indicating that the
		/// calling process is running in the Windows Character Mode (CUI)
		/// subsystem.
		/// </summary>
		public bool StdErrIsRedirected
		{
			get { return _fStdErrIsRedirected; }
		}	// StdErrIsRedirected (Read only)

		/// <summary>
		/// Return TRUE if the Standard Output console stream was redirected by
		/// the shell. Otherwise, the return value is FALSE.
		/// 
		/// This flag is meaningless unless the ApplicationSubsystem property
		/// value is equal to ProcessSubsystem.Console, indicating that the
		/// calling process is running in the Windows Character Mode (CUI)
		/// subsystem.
		/// </summary>
		public bool StdOutIsRedirected
		{
			get { return _fStdOutIsRedirected; }
		}   // StdOutIsRedirected (Read only)
        #endregion  // Public Properties


        #region Singleton Access Methods
        /// <summary>
        /// Call this static method from anywhere to get a reference to the
        /// ExceptionLogger singleton.
        /// </summary>
        /// <returns>
        /// The return value is a reference to the singleton, which is created
        /// the first time the method is called. Subsequent calls return a
        /// reference to the singleton.
        /// </returns>
        /// <remarks>
        /// All overloads call this method, caching the returned reference in a
        /// local variable, before they override one or more of its default
        /// property values. When all overrides have been processed, the cached
        /// reference is returned through the overload that took the call.
        ///
        /// This roundabout procedure is necessary because the properties cannot
        /// be set until the object has been created. The most straightforward
        /// way to do this is to call the default method, which performs a task
        /// usually performed by a default constructor in this implementation of
        /// the singleton design pattern.
        /// </remarks>
        public static new ExceptionLogger GetTheSingleInstance ( )
		{
			if ( s_fSynchronizeFlagsNow )
				InitializeInstance ( s_genTheOnlyInstance ,
									 PESubsystemInfo.GetTheSingleInstance ( ).DefaultAppDomainSubsystem );
	
			s_fSynchronizeFlagsNow = true;
			return s_genTheOnlyInstance;
		}   // GetTheSingleInstance (1 of 5)


        /// <summary>
		/// Call this static method from anywhere to get a reference to the
		/// ExceptionLogger singleton and set its OptionFlags property.
        /// </summary>
		/// <param name="penmOutputOptions">
		/// The OutputOptions enumeration is organized for use as a bit mask.
		/// Set its flags as desired to control the format and content of output
		/// generated by the ReportException methods.
		/// </param>
        /// <returns>
		/// The return value is a reference to the singleton, which is created
		/// the first time the method is called. Subsequent calls return a
		/// reference to the singleton.
        /// </returns>
        public static ExceptionLogger GetTheSingleInstance (
            OutputOptions penmOutputOptions )
        {
            s_fSynchronizeFlagsNow = false;                 // Defer the call to SyncNewFlagsWithOld, so that it can accept the Subsystem.
            ExceptionLogger rTheInstance = GetTheSingleInstance ( );

            InitializeInstance ( rTheInstance ,
                                 PESubsystemInfo.s_genTheOnlyInstance.DefaultAppDomainSubsystem );

            rTheInstance.OptionFlags = penmOutputOptions;
            return rTheInstance;
        }   // GetTheSingleInstance (2 of 5)


		/// <summary>
		/// Call this static method from anywhere to get a reference to the
		/// ExceptionLogger singleton and set its ProcessSubsystem,
		/// OptionFlags, and AppEventSourceID properties.
		/// </summary>
		/// <param name="penmProcessSubsystem">
		/// Use this member of the ProcessSubsystem enumeration to modify the
		/// default behavior of the exception logging methods, by enabling
		/// console output if the calling application has one.
		/// </param>
		/// <param name="penmOutputOptions">
		/// The OutputOptions enumeration is organized for use as a bit mask.
		/// Set its flags as desired to control the format and content of output
		/// generated by the ReportException methods.
		/// </param>
		/// <param name="pstrEventSourceID">
		/// Use this string to override the default event source ID,  which is
		/// WIZARDWRX_EVENT_SOURCE_ID.
		/// </param>
		/// <returns>
		/// The return value is a reference to the singleton, which is created
		/// the first time the method is called. Subsequent calls return a
		/// reference to the singleton.
		/// </returns>
		/// <remarks>
		/// This method looks entirely forward, which means that it doesn't
		/// bother with the obsolete properties.
		/// </remarks>
		public static ExceptionLogger GetTheSingleInstance (
			PESubsystemInfo.PESubsystemID penmProcessSubsystem ,
			OutputOptions penmOutputOptions ,
			string pstrEventSourceID )
		{
			s_fSynchronizeFlagsNow = false;	// Defer the call to SyncNewFlagsWithOld, so that it can accept the Subsystem.
			ExceptionLogger rTheInstance = GetTheSingleInstance ( );

			InitializeInstance ( rTheInstance ,
								 penmProcessSubsystem );
			rTheInstance._strEventSource = pstrEventSourceID;

			return rTheInstance;
		}   // GetTheSingleInstance (3 of 3)


		/// <summary>
		/// Call this static method from anywhere to get a reference to the
		/// ExceptionLogger singleton and set its ProcessSubsystem,
		/// OptionFlags, and AppEventSourceID properties.
		/// </summary>
		/// <param name="penmOutputOptions">
		/// The OutputOptions enumeration is organized for use as a bit mask.
		/// Set its flags as desired to control the format and content of output
		/// generated by the ReportException methods.
		/// </param>
		/// <param name="pstrEventSourceID">
		/// Use this string to override the default event source ID,  which is
		/// WIZARDWRX_EVENT_SOURCE_ID.
		/// </param>
		/// <returns>
		/// The return value is a reference to the singleton, which is created
		/// the first time the method is called. Subsequent calls return a
		/// reference to the singleton.
		/// </returns>
		/// <remarks>
		/// This method looks entirely forward, which means that it doesn't
		/// bother with the obsolete properties.
		/// </remarks>
		public static ExceptionLogger GetTheSingleInstance (
			OutputOptions penmOutputOptions ,
			string pstrEventSourceID )
		{
			s_fSynchronizeFlagsNow = false; // Defer the call to SyncNewFlagsWithOld, so that it can accept the Subsystem.
			ExceptionLogger rTheInstance = GetTheSingleInstance ( );

			InitializeInstance ( rTheInstance ,
								 PESubsystemInfo.s_genTheOnlyInstance.DefaultAppDomainSubsystem );
			rTheInstance._strEventSource = pstrEventSourceID;

			return rTheInstance;
		}   // GetTheSingleInstance (4 of 5)


        /// <summary>
        /// Call this static method from anywhere to get a reference to the
        /// ExceptionLogger singleton and set its AppSubsystem
        /// property.
        /// </summary>
        /// <param name="penmProcessSubsystem">
        /// Use this member of the ProcessSubsystem enumeration to modify the
        /// default behavior of the exception logging methods, by enabling
        /// console output if the calling application has one.
        /// </param>
        /// <returns>
        /// The return value is a reference to the singleton, which is created
        /// the first time the method is called. Subsequent calls return a
        /// reference to the singleton.
        /// </returns>
        [Obsolete ( "Using the property on the StateManager instance makes this use case redundant." )]
        public static ExceptionLogger GetTheSingleInstance (
            PESubsystemInfo.PESubsystemID penmProcessSubsystem )
        {
            s_fSynchronizeFlagsNow = false; // Defer the call to SyncNewFlagsWithOld, so that it can accept the Subsystem. 
            ExceptionLogger rTheInstance = GetTheSingleInstance ( );
            InitializeInstance ( rTheInstance ,
                                 penmProcessSubsystem );
            return rTheInstance;
        }   // GetTheSingleInstance (5 of 5)


        #if SHOW_TYPE_HANDLES
		/// <summary>
		/// The purpose of this static constructor is to force the enclosed code to run once only.
		/// </summary>
		static ExceptionLogger ( )
		{
			foreach ( Type typThisException in s_atypKnowExceptionTypes )
			{
				Console.WriteLine ( "{0}Exception type, as seen by static ExceptionLogger constructor:{0}" , Environment.NewLine );
				Console.WriteLine ( "    FullName   = {0}" , typThisException.FullName );
				Console.WriteLine ( "    Name       = {0}" , typThisException.Name );
				Console.WriteLine ( "    GUID       = {0}" , typThisException.GUID );
				Console.WriteLine ( "    TypeHandle = {0}" , typThisException.TypeHandle.Value.ToString ( NumericFormats.HEXADECIMAL_8 ) );
			}	// foreach ( Type typThisException in s_atypKnowExceptionTypes )
		}	// static ExceptionLogger
        #endif // SHOW_TYPE_HANDLES
        #endregion  // Singleton Access Methods


        #region Public Instance Utility Methods
        /// <summary>
        /// Restore the default exception message colors.
        /// </summary>
        /// <param name="pfWipeSavedColors">
        /// To have the colors saved by the last call to SaveCurrentColors
        /// discarded, set this argument to TRUE. Otherwise, the saved colors
        /// are preserved.
        /// </param>
        /// <returns>
        /// The return value is the reinstated default exception message colors,
        /// which may be NULL if the color scheme is invalid (both colors set to
        /// the same value) or missing (no color scheme is configured).
        /// </returns>
        public ErrorMessagesInColor RestoreDefaultColors ( bool pfWipeSavedColors )
		{
			_emsgColorsStdErr = null;
			_emsgColorsStdOut = null;

			if ( pfWipeSavedColors )
				_emsgSpecialColorsSave = null;

			SetMessageColors ( );
			return _emsgColorsStdErr;
		}   // RestoreDefaultColors


		/// <summary>
		/// Return a message suitable for display on a console to accompany a
		/// status code of ERROR_SUCCESS (zero) or ERROR_RUNTIME (one), both
		/// defined in the MagicNumbers class of standard constant definitions.
		/// </summary>
		/// <returns>
		/// Since its operation is self contained, this method should always
		/// succeed in returning an appropriate message.
		/// </returns>
		/// <seealso cref="GetSpecifiedReservedErrorMessage"/>
		public string GetReservedErrorMessage ( )
		{	// The purpose of this substitution is to discard the bits that govern what gets included in the message, leaving only the destination flags.
			return GetSpecifiedReservedErrorMessage ( ( ErrorExitOptions ) ( this._enmOutputOptions & OutputOptions.AllDestiations ) );
		}	// GetReservedErrorMessage (1 of 2)


		/// <summary>
		/// Return a message suitable for display on a console to accompany a
		/// status code of ERROR_SUCCESS (zero) or ERROR_RUNTIME (one), both
		/// defined in the MagicNumbers class of standard constant definitions.
		/// </summary>
		/// <param name="penmErrorExitOptions">
		/// A member of the ErrorExitOptions specifies the desired action. This
		/// value overrides the corresponding bits in the OptionFlags bit mask.
		/// 
		/// If an invalid value is specified, the returned string is an error 
		/// message that starts with "An internal error has occurred." If this
		/// happens, it should be treated as a coding error.
		/// </param>
		/// <returns>
		/// If the function succeeds, the returned message is ready to use; 
		/// appropriate substitutions have already been made. Otherwise, the
		/// return value is the error message described in the documentation
		/// of argument penmErrorExitOptions.
		/// </returns>
		public string GetReservedErrorMessage (
			ErrorExitOptions penmErrorExitOptions )
		{	// The static method does all of the work.
			return GetSpecifiedReservedErrorMessage ( penmErrorExitOptions );
		}	// GetReservedErrorMessage (2 of 2)


		/// <summary>
		/// Return a labeled string representation of the current OptionFlags,
		/// along with decimal and hexadecimal representations of the bit mask.
		/// </summary>
		/// <param name="pstrLabel">
		/// Specify a label to be inserted into the message. This may be the
		/// empty string, or even a null reference, to omit the label.
		/// </param>
		/// <returns>
		/// The returned string is ready to display via Console.WriteLine or
		/// MessageBox.Show.
		/// </returns>
		public string OutputOptionsDisplay ( string pstrLabel )
		{
			return string.Format (
				Properties.Resources.OUTPUT_OPTIONS_DISPLAY_FORMAT ,								// Format control string.
				new object [ ] {
					string.IsNullOrEmpty ( pstrLabel )												// Test for null or empty pstrLabel
						? SpecialStrings.EMPTY_STRING :												// If so, Format Item 0 = the empty string
						string.Concat ( pstrLabel ,													// Otherwise, Format Item 0 = descriptive text, followed by a single space
										SpecialCharacters.SPACE_CHAR ) ,							// Format Item 0 = Optional descriptive text, which may be the empty string or a null reference, which is treated the empty string
					( ( int ) _enmOutputOptions ).ToString ( NumericFormats.HEXADECIMAL_2 ) ,		// Format Item 1 = hexadecimal representation of the value
					( int ) _enmOutputOptions ,														// Format Item 2 = decimal representation of the value
					_enmOutputOptions.ToString ( )													// Format Item 3 = enumeration representation
				} );
		}	// OutputOptionsDisplay


		/// <summary>
		/// Turn the specified bit in the OutputOptions bit mask OFF.
		/// </summary>
		/// <param name="penmOutputOptions">
		/// Specify the member of the OutputOptions enumerated type to turn OFF.
		/// </param>
		/// <returns>
		/// The return value is the updated OutputOptions enumerated type, which
		/// is organized as a bit mask.
		/// </returns>
		public OutputOptions OutputOptionTurnOff ( OutputOptions penmOutputOptions )
		{
			_enmOutputOptions = _enmOutputOptions & ( ~penmOutputOptions );
			return _enmOutputOptions;
		}	// OutputOptionTurnOff


		/// <summary>
		/// Turn the specified bit in the OutputOptions bit mask ON.
		/// </summary>
		/// <param name="penmOutputOptions">
		/// Specify the member of the OutputOptions enumerated type to turn ON.
		/// </param>
		/// <returns>
		/// The return value is the updated OutputOptions enumerated type, which
		/// is organized as a bit mask.
		/// </returns>
		public OutputOptions OutputOptionTurnOn ( OutputOptions penmOutputOptions )
		{
			_enmOutputOptions = _enmOutputOptions | penmOutputOptions;
			return _enmOutputOptions;
		}	// OutputOptionTurnOn


		/// <summary>
		/// Restore the state of the OutputOptions flags to their initial
		/// (default) values.
		/// </summary>
		/// <returns>
		/// The return value is the reinstated property value.
		/// </returns>
		/// <remarks>
		/// This routine calls the same static SetDefaultOptions method used by
		/// the static initializer, so that the defaults can be changed by
		/// visiting just one routine.
		/// </remarks>
		public OutputOptions RestoreDefaultOptions ( )
		{
			_enmOutputOptions = SetDefaultOptions ( );
			return _enmOutputOptions;
		}   // RestoreDefaultOptions


		/// <summary>
		/// Restore the state of the OutputOptions flags from the copy saved by
		/// the last SaveCurrentOptions method call.
		/// </summary>
		/// <returns>
		/// This method returns the options that were just restored, so that
		/// callers can sanity check them against the expected settings.
		/// </returns>
		/// <remarks>
		/// CAUTION: Unless this method is preceded by a call to
		/// SaveCurrentOptions, this call clears all flags, not just back to
		/// their initial state, but truly clear - all flags OFF.
		/// </remarks>
		public OutputOptions RestoreSavedOptions ( )
		{
			_enmOutputOptions = _enmSavedOptions;
			return _enmOutputOptions;
		}   // RestoreSavedOptions


		/// <summary>
		/// Restore the ErrorMessageColors from the copy saved by the last
		/// SaveCurrentColors method call.
		/// </summary>
		/// <returns>
		/// This method returns the restored message colors, so that callers may
		/// sanity check them against the expected values.
		/// </returns>
		/// <remarks>
		/// CAUTION: Unless this method is preceded by a call to
		/// SaveCurrentColors, this call completely disables color error
		/// messages, unless the static initializer set default colors from a
		/// configuration file.
		/// </remarks>
		public ErrorMessagesInColor RestoreSavedColors ( )
		{   // Let the property setter do the work.
			ErrorMessageColors = _emsgSpecialColorsSave;
			return _emsgSpecialColorsSave;
		}   // RestoreSavedColors


		/// <summary>
		/// Save a copy of the current colors defined by the ErrorMessageColors
		/// property into a private area reserved for the purpose.
		/// </summary>
		/// <returns>
		/// The current settings (the settings just saved) are returned.
		/// </returns>
		public ErrorMessagesInColor SaveCurrentColors ( )
		{
			_emsgSpecialColorsSave = _emsgColorsStdErr;
			return _emsgColorsStdErr;
		}   // SaveCurrentColors


		/// <summary>
		/// Save a copy of the current state of the OutputOptions flags into a
		/// private area reserved for the purpose.
		/// </summary>
		/// <returns>
		/// The current settings (the settings just saved) are returned.
		/// </returns>
		public OutputOptions SaveCurrentOptions ( )
		{
			_enmSavedOptions = _enmOutputOptions;
			return _enmOutputOptions;
		}   // SaveCurrentOptions
		#endregion  // Public Instance Utility Methods


		#region Public Static Utility Methods
		/// <summary>
		/// Return a message suitable for display on a console to accompany a
		/// status code of ERROR_SUCCESS (zero) or ERROR_RUNTIME (one), both
		/// defined in the MagicNumbers class of standard constant definitions.
		/// 
		/// Call this method with penmErrorExitOptions equal to Succeeded to get
		/// the ERROR_SUCCESS placeholder string for your error message table.
		/// </summary>
		/// <param name="penmErrorExitOptions">
		/// A member of the ErrorExitOptions specifies the desired action. Since
		/// this is a static method, and doesn't have access to the instance
		/// properties, this value substitutes for the corresponding bits in the
		/// OptionFlags bit mask.
		/// 
		/// If an invalid value is specified, the returned string is an error 
		/// message that starts with "An internal error has occurred." If this
		/// happens, it should be treated as a coding error.
		/// </param>
		/// <returns>
		/// If the function succeeds, the returned message is ready to use; 
		/// appropriate substitutions have already been made. Otherwise, the
		/// return value is the error message described in the documentation
		/// of argument penmErrorExitOptions.
		/// </returns>
		public static string GetSpecifiedReservedErrorMessage (
			ErrorExitOptions penmErrorExitOptions )
		{
			switch ( penmErrorExitOptions )
			{ 
				case ErrorExitOptions.Succeeded:
					return SpecialStrings.ERRMSG_SUCCESS_PLACEHOLDER;
				case ErrorExitOptions.RecordedInEventLog:
					return Properties.Resources.ERRMSG_RUNTIME_SEE_EVENT_LOG;

				case ErrorExitOptions.RecordedInStandardError:
				case ErrorExitOptions.RecordedInStandardOutput:
					return string.Format (
						Properties.Resources.ERRMSG_RUNTIME_SEE_MESSAGE_ABOVE ,	// Format Control String
						Environment.NewLine );									// Format Item 0 = Embedded Newline

				default:
					return string.Format (
						Properties.Resources.ERRMSG_INVALID_ERROREXITOPTIONS ,	// Format Control String
						( int ) penmErrorExitOptions ,							// Format Item 0 = Integer value of penmErrorExitOptions
						Environment.NewLine );									// Format Item 1 = Embedded Newline
			}	// switch ( penmErrorExitOptions )
		}	// public static string GetSpecifiedReservedErrorMessage


		/// <summary>
		/// Append a message to a standard ISO-8601 time stamp.
		/// </summary>
		/// <param name="pstrMessage">
		/// Specify the message to record.
		/// </param>
		/// <remarks>
		/// Though written ostensibly for internal use, I marked this method as
		/// public because it will quickly find employment outside this library.
		/// </remarks>
		public static void TimeStampedTraceWrite ( string pstrMessage )
		{
			System.Diagnostics.Trace.WriteLine (
				string.Format (
					"{0}: {1}" ,
					WizardWrx.SysDateFormatters.FormatDateTimeForShow ( DateTime.Now ) ,
				pstrMessage ) );
			System.Diagnostics.Trace.Flush ( );
		}   // public static void TimeStampedTraceWrite
		#endregion  // Public Static Utility Methods


		#region Public ReportException Methods
		/// <summary>
		/// Format and report the properties of a generic Exception on a console
		/// in a thread-safe manner.
		/// </summary>
		/// <param name="perrAny">
		/// The instance of the base Exception class to process. See Remarks.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// or, 
		/// </returns>
		/// <remarks>
		/// This can be ANY exception type, although the intent is to limit its
		/// use to reporting exceptions thrown by the base class,
		/// System.Exception.
		///
		/// Other overloads exist for reporting exceptions thrown by types
		/// derived from System.Exception.
		///
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException ( Exception perrAny )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = CreateForEventLog ( _enmOutputOptions & OutputOptions.EventLog );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrAny ,
					StringTricks.DisplayNullSafely ( perrAny.TargetSite ) ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );

			if ( UseEventLog ( _enmOutputOptions ) )
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrAny ,
						StringTricks.DisplayNullSafely ( perrAny.TargetSite ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );

			return AddCommonElementsReportAndReturn (
				perrAny ,
				sbMsg , 
				sbLogMsg );
		}   // ReportException method (1 of 7 - Exception)


		/// <summary>
		/// Format and report the properties of an ArgumentException exception on
		/// a console in a thread-safe manner.
		/// </summary>
		/// <param name="perrBadArg">
		/// The instance of the ArgumentException exception to process. See
		/// Remarks.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// or, 
		/// </returns>
		/// <remarks>
		/// Although this method can process objects of ANY class which derives
		/// from ArgumentException, other methods of this class specialize in
		/// processing objects of the commonly used ArgumentOutOfRangeException
		/// and ArgumentNullException derived classes.
		///
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException ( ArgumentException perrBadArg )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = CreateForEventLog ( _enmOutputOptions & OutputOptions.EventLog );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrBadArg ,
					perrBadArg.TargetSite.Name ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );
			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_ARGNAME_TPL ,								// Format String
				perrBadArg.ParamName ,                                                  // Token Index 0
				Environment.NewLine );                                                  // Token Index 1

			if ( UseEventLog ( _enmOutputOptions ) )
			{
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrBadArg ,
						StringTricks.DisplayNullSafely ( perrBadArg.TargetSite.Name ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_ARGNAME_TPL ,							// Format String
					perrBadArg.ParamName ,                                              // Token Index 0
					Environment.NewLine );                                              // Token Index 1
			}   // if ( UseEventLog ( _enmOutputOptions ) )

			return AddCommonElementsReportAndReturn (
				perrBadArg ,
				sbMsg ,
				sbLogMsg );
		}   // ReportException method (2 of 7 - Exception)


		/// <summary>
		/// Format and report the properties of an ArgumentOutOfRangeException
		/// exception on a console in a thread-safe manner.
		/// </summary>
		/// <param name="perrBadArg">
		/// The instance of the ArgumentOutOfRangeException class to process.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// or, 
		/// </returns>
		/// <remarks>
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException ( ArgumentOutOfRangeException perrBadArg )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = CreateForEventLog ( _enmOutputOptions & OutputOptions.EventLog );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrBadArg ,
					perrBadArg.TargetSite.Name ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );
			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_ARGNAME_TPL ,								// Format String
				perrBadArg.ParamName ,                                                  // Token Index 0
				Environment.NewLine );                                                  // Token Index 1
			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_ARGVALUE_TPL ,								// Format String
				perrBadArg.ActualValue ,                                                // Token Index 0
				Environment.NewLine );                                                  // Taken Index 1

			if ( UseEventLog ( _enmOutputOptions ) )
			{
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrBadArg ,
						StringTricks.DisplayNullSafely ( perrBadArg.TargetSite.Name ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_ARGNAME_TPL ,							// Format String
					perrBadArg.ParamName ,                                              // Token Index 0
					Environment.NewLine );                                              // Token Index 1
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_ARGVALUE_TPL ,							// Format String
					perrBadArg.ActualValue ,                                            // Token Index 0
					Environment.NewLine );                                              // Taken Index 1
			}   // if ( UseEventLog ( _enmOutputOptions ) )

			return AddCommonElementsReportAndReturn (
				perrBadArg ,
				sbMsg ,
				sbLogMsg );
		}   // ReportException method (3 of 7 - ArgumentOutOfRangeException)


		/// <summary>
		/// Format and report the properties of an ArgumentNullException
		/// exception on a console in a thread-safe manner. See Remarks.
		/// </summary>
		/// <param name="perrNullArg">
		/// The instance of an ArgumentNullException exception to process.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// or, 
		/// </returns>
		/// <remarks>
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException ( ArgumentNullException perrNullArg )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrNullArg ,
					perrNullArg.TargetSite.Name ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );
			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_ARGNAME_TPL ,						// Format String
				perrNullArg.ParamName ,                                         // Token Index 0
				Environment.NewLine );                                          // Token Index 1

			if ( UseEventLog ( _enmOutputOptions ) )
			{   // Generate the separate message for the event log, which always includes the stack trace.
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrNullArg ,
						StringTricks.DisplayNullSafely ( perrNullArg.TargetSite.Name ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_ARGNAME_TPL ,					// Format String
					perrNullArg.ParamName ,                                     // Token Index 0
					Environment.NewLine );                                      // Token Index 1
			}   // if ( UseEventLog ( _enmOutputOptions ) )

			return AddCommonElementsReportAndReturn (
				perrNullArg ,
				sbMsg ,
				sbLogMsg );
		}   // ReportException method (4 of 7 - ArgumentNullException)


		/// <summary>
		/// Format and report the properties of an ObjectDisposedException
		/// exception on a console in a thread-safe manner. See Remarks.
		/// </summary>
		/// <param name="perrDisposed">
		/// The instance of the ObjectDisposedException Exception class to
		/// process.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// or, 
		/// </returns>
		/// <remarks>
		/// Any process that throws an ObjectDisposedException exception is in
		/// serious trouble, and deserves to crash, and be investigated, because
		/// this type of exception is almost invariably caused by a programming
		/// logic error.
		///
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException ( ObjectDisposedException perrDisposed )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrDisposed ,
					StringTricks.DisplayNullSafely ( perrDisposed.TargetSite.Name ) ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );
			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_OBJNAME_TPL ,
				perrDisposed.ObjectName ,
				Environment.NewLine );

			if ( UseEventLog ( _enmOutputOptions ) )
			{   // Generate the separate message for the event log, which always includes the stack trace.
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrDisposed ,
						StringTricks.DisplayNullSafely ( perrDisposed.TargetSite.Name ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_OBJNAME_TPL ,
					perrDisposed.ObjectName ,
					Environment.NewLine );
			}   // if ( UseEventLog ( _enmOutputOptions ) )

			return AddCommonElementsReportAndReturn (
				perrDisposed ,
				sbMsg ,
				sbLogMsg );
		}   // ReportException method (5 of 7 - ObjectDisposedException)


		/// <summary>
		/// Format and report the properties of an IOException exception on a
		/// console in a thread-safe manner.
		/// </summary>
		/// <param name="perrIOError">
		/// The instance of the IOException class to process. See Remarks.
		/// </param>
		/// <param name="pfi">
		/// The FileInfo object makes available much more than the file name,
		/// allowing for the possibility of an override to deliver more detailed
		/// information about the file being processed.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// </returns>
		/// <remarks>
		/// This routine processes ANY exception of the IOException class and
		/// its derivatives. Methods for parsing published derived classes are
		/// somewhere on my ToDo list.
		///
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException (
			IOException perrIOError ,
			FileInfo pfi )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrIOError ,
					perrIOError.TargetSite.Name ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );

			{	// Use Marshal.GetHRForException() in System.Runtime.InteropServices.
				// Source:	 "Getting the Exceptions HResult value in .net 4 or earlier"
				//			 https://www.experts-exchange.com/questions/28620045/Getting-the-Exceptions-HResult-value-in-net-4-or-earlier.html
				// See also: "Marshal.GetHRForException Method (Exception)"
				//			 https://msdn.microsoft.com/en-us/library/system.runtime.interopservices.marshal.gethrforexception(v=vs.110).aspx

				int hResult = System.Runtime.InteropServices.Marshal.GetHRForException ( perrIOError );
				sbMsg.AppendFormat (
					Properties.Resources.ERRMSG_HRESULT ,						// This message template displays hexadecimal and decimal representations of an HRESULT.
					hResult.ToString ( NumericFormats.HEXADECIMAL_8 ) ,			// Format Item 0 renders a hexadecimal representation of the HRESULT.
					hResult.ToString ( NumericFormats.DECIMAL ) , 				// Format Item 1 renders a decimal representation of the HRESULT.
					Environment.NewLine );										// My method of rendering a newline keeps it out of the managed resources.
			}	// Let the enhIOException go out of scope.

			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_FILENAME_TPL ,
				pfi.Name ,
				Environment.NewLine );

			if ( UseEventLog ( _enmOutputOptions ) )
			{   // Generate the separate message for the event log, which always includes the stack trace.
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrIOError ,
						StringTricks.DisplayNullSafely ( perrIOError.TargetSite.Name ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_FILENAME_TPL ,
					pfi.FullName ,
					Environment.NewLine );
			}   // if ( UseEventLog ( _enmOutputOptions ) )

			return AddCommonElementsReportAndReturn (
				perrIOError ,
				sbMsg ,
				sbLogMsg );
		}   // ReportException method (6 of 7 - IOException)


		/// <summary>
		/// Format and report the properties of an FormatException exception on
		/// a console in a thread-safe manner.
		/// </summary>
		/// <param name="perrrBadFormat">
		/// The instance of the FormatException class to process.
		/// </param>
		/// <param name="pstrFormatString">
		/// This should be the format string which caused the exception. There
		/// should be a way to feed this to the exception, or recover it, but I
		/// have yet to find it.
		/// </param>
		/// <returns>
		/// The return value is the string that was written onto the standard
		/// error or standard output stream (or both, depending on the flags).
		/// 
		/// NOTE: The returned string is terminated by a single newline. Since
		/// it is appended to the stream by a Console.Write, the carriage is
		/// returned to the next line, but no additional line feeds follow.
		/// Hence, if you want your error message to be followed by a blank
		/// line, you must follow this call with an empty Console.WriteLine
		/// or, 
		/// </returns>
		/// <remarks>
		/// The TargetSite property, exposed by descendants of System.Exception,
		/// is the name of the method in which the exception was thrown.
		/// </remarks>
		public string ReportException (
			FormatException perrrBadFormat ,
			string pstrFormatString )
		{
			StringBuilder sbMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			StringBuilder sbLogMsg = new StringBuilder ( MagicNumbers.CAPACITY_08KB );

			sbMsg.Append (
				ReformatExceptionMessage (
					perrrBadFormat ,
					perrrBadFormat.TargetSite.Name ,
					Properties.Resources.ERRMSG_EX_MSG_TPL ) );
			sbMsg.AppendFormat (
				Properties.Resources.ERRMSG_FORMATSTRING_TPL ,
				perrrBadFormat.TargetSite.Name ,
				Environment.NewLine );

			if ( UseEventLog ( _enmOutputOptions ) )
			{   // Generate the separate message for the event log, which always includes the stack trace.
				sbLogMsg.Append (
					ReformatExceptionMessage (
						perrrBadFormat ,
						StringTricks.DisplayNullSafely ( perrrBadFormat.TargetSite.Name ) ,
						Properties.Resources.ERRMSG_EX_EVTMSG_TPL ) );
				sbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_FORMATSTRING_TPL ,
					perrrBadFormat.TargetSite.Name ,
					Environment.NewLine );
			}   // if ( UseEventLog ( _enmOutputOptions ) )

			return AddCommonElementsReportAndReturn (
				perrrBadFormat ,
				sbMsg ,
				sbLogMsg );
		}   // ReportException method (7 of 7 - FormatException)
        #endregion // Public ReportException Methods


        #region Public Static Properties
        /// <summary>
        /// Once an instance has spring into being, this static property returns
        /// the configuration values that are missing a setting in the
        /// configuration file.
        /// </summary>
        public static string s_strSettingsOmittedFromConfigFile { get; private set; }
        #endregion  // Public Static Properties


        /// <summary>
        /// The StateManager calls this method once, immediately after both it
        /// and the ExceptionLogger exist.
        /// </summary>
        /// <param name="psmOfThisApp">
        /// To simplify matters a bit, the state manager passes a reference to
        /// itself.
        /// </param>
        internal void GetStandardHandleStates ( StateManager psmOfThisApp )
		{
			_fStdOutIsRedirected = ( psmOfThisApp.StandardHandleState ( StandardHandleInfo.StandardConsoleHandle.StandardOutput ) == StandardHandleInfo.StandardHandleState.Redirected );
			_fStdErrIsRedirected = ( psmOfThisApp.StandardHandleState ( StandardHandleInfo.StandardConsoleHandle.StandardError ) == StandardHandleInfo.StandardHandleState.Redirected );
		}	// GetStandardHandleStates


		#region Private Instance Methods
		private ExceptionLogger ( )
		{ }	// To prevent uncontrolled instantiation, the default constructor must be private.

		/// <summary>
		/// Test whether an OutputOptions is ON or OFF.
		/// </summary>
		/// <param name="penmTestThisOption">
		/// Specify the OutputOptions enumeration member to test.
		/// </param>
		/// <returns>
		/// Return TRUE if the specified option is ON; otherwise, return FALSE.
		/// </returns>

		//  --------------------------------------------------------------------
		//  This routine simplifies simultaneously evaluating combinations of
		//  two or more OutputOptions. The following example illustrates my
		//  point.
		//
		//  With this routine:
		//
		//     if ( OptionIsOn ( OutputOptions.StandardError ) || OptionIsOn ( OutputOptions.StandardOutput ) )
		//
		//  Without it:
		//
		//     if ( ( ( _enmOutputOptions & OutputOptions.StandardError ) == OutputOptions.StandardError ) || ( ( _enmOutputOptions & OutputOptions.StandardOutput ) == OutputOptions.StandardOutput ) )
		//
		//  Which statement would you rather figure out a year or two after you
		//  wrote it?
		//
		//  As such, it is syntactic sugar, which I expect to be optimized away
		//  by in-lining; since I have to understand the C# code at a glance, but
		//  I may never look at the optimized IL, it doesn't matter nearly as
		//  much that it is easy to decipher.
		//  --------------------------------------------------------------------

		private bool OptionIsOn ( OutputOptions penmTestThisOption )
		{   // Return TRUE if the penmTestThisOption flag is ON.
			return ( _enmOutputOptions & penmTestThisOption ) == penmTestThisOption;
		}   // OptionIsOn


		/// <summary>
		/// Report as indicated by the flags stored in the _enmOutputOptions bit
		/// mask.
		/// </summary>
		/// <param name="pstrMsg">
		/// The message string to return to the caller.
		/// </param>
		/// <param name="pstrLogMsg">
		/// To correctly report inner exceptions, the messages for the user and
		/// the event log must be segregated and built concurrently.
		/// </param>
		/// <returns>
		/// The message to report.
		/// </returns>
		/// <remarks>
		/// This is the only private instance method. Making it static would
		/// require four additional arguments into it. I'd rather save those 128
		/// bytes of stack frame for when I really need it.
		///
		/// The same message is recorded on the console, if so indicated, and in
		/// the application event log, EXCEPT that the copy that goes into the
		/// event log ALWAYS gets a stack trace attached for the exception and
		/// each inner exception, if any. Finally, the text is returned, so that
		/// the caller can use it, for example, in a message box.
		/// </remarks>
		private string ReportAsDirected (
			string pstrMsg ,
			string pstrLogMsg )
		{
			#if ECHO_COLOURS
				Console.WriteLine ( "Default colors for fatal exception message, per ErrorMessagesInColor: Foreground = {0}" , ErrorMessagesInColor.FatalExceptionTextColor );
				Console.WriteLine ( "                                                                      Background = {0}" , ErrorMessagesInColor.FatalExceptionBackgroundColor );

				if ( _emsgColorsStdErr == null )
				{   // Console.WriteLine can handle a property being null, but a null object throws a NullReferenceException exception.
					Console.WriteLine ( "Current colors for fatal exception message, per ExceptionLogger:      Foreground = NULL" );
					Console.WriteLine ( "                                                                      Background = NULL" );
				}	// TRUE (anticipated outcome) block, if ( _emsgColorsStdErr == null )
				else
				{
					Console.WriteLine ( "Current colors for fatal exception message, per ExceptionLogger:      Foreground = {0}" , _emsgColorsStdErr.MessageForegroundColor );
					Console.WriteLine ( "                                                                      Background = {0}" , _emsgColorsStdErr.MessageBackgroundColor );
				}   // FALSE (UNexpected outcome) block, if ( _emsgColorsStdErr == null )
			#endif  // #if ECHO_COLOURS

			if ( UseConsole ( _enmOutputOptions ) )
			{	// The client wants errors written onto one or both of the standard console output streams.
				SetMessageColors ( );
			}	// if ( UseConsole ( _enmOutputOptions ) )

			//	----------------------------------------------------------------
			//	Since a user might want error messages to be displayed on the
			//	console via the standard error stream and preserved in a
			//	redirected standard output stream, this method supports this use
			//	case. However, when standard output is feeding the console, this
			//	produces duplicate error messages. Hence, the second printing is
			//	suppressed when it has already gone to standard error AND the
			//	standard output stream is redirected.
			//	----------------------------------------------------------------

			bool fWroteOnStandardError = false;

			if ( _enmProcessSubsystem == PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI )
			{	// None of this matters unless we are in Character Mode.
				if ( ( _enmOutputOptions & OutputOptions.StandardError ) == OutputOptions.StandardError )
				{   // The client assembly wants errors written onto the standard error stream.
					fWroteOnStandardError = true;

					if ( _emsgColorsStdOut == null )
					{   // Write directly to the standard error stream.
						Console.Error.WriteLine ( pstrMsg );                    // Use the current screen colors.
					}
					else
					{   // Write indirectly to the standard error stream through the ErrorMessagesInColor class.
						_emsgColorsStdErr.Write ( pstrMsg );                    // Use the custom screen colors.
					}   // if ( _emsgColorsStdOut == null )
				}   // if ( ( _enmOutputOptions & OutputOptions.StandardError ) == OutputOptions.StandardError )

				if ( ( _enmOutputOptions & OutputOptions.StandardOutput ) == OutputOptions.StandardOutput )
				{   // The client assembly wants errors written onto the standard output stream.
					if ( fWroteOnStandardError )
					{   // Suppress if this is a duplicate, which is true if standard error was written and standard output is attached to the console.
						if ( _fStdOutIsRedirected )
						{   // Although the message was written on standard error, since standard output is redirected, it gets a copy.
							if ( _emsgColorsStdOut == null )
							{   // Write directly to the standard output stream.
								Console.WriteLine ( pstrMsg );                  // Use the current screen colors.
							}
							else
							{   // Write indirectly to the standard output stream through the MessageInColor class.
								_emsgColorsStdOut.Write ( pstrMsg );            // Use the custom screen colors.
							}   // if ( _emsgColorsStdOut == null )
						}   // if ( _fStdOutIsRedirected )
					}   // TRUE (The message was written on the Standard Error stream.) block, if ( fWroteOnStandardError )
					else
					{   // If it didn't go on Standard Error, the redirection state of Standard Output is moot.
						if ( _emsgColorsStdOut == null )
						{   // Write directly to the standard output stream.
							Console.WriteLine ( pstrMsg );                      // Use the current screen colors.
						}
						else
						{   // Write indirectly to the standard output stream through the MessageInColor class.
							_emsgColorsStdOut.Write ( pstrMsg );                // Use the custom screen colors.
						}   // if ( _emsgColorsStdOut == null )
					}   // FALSE (The message was NOT written on the Standard Error stream.) block, if ( fWroteOnStandardError )
				}   // if ( ( _enmOutputOptions & OutputOptions.StandardOutput ) == OutputOptions.StandardOutput )

				ScrollUp ( );                                                   // Compensate for some methods that do write, rather than writeLine, so that visible behavior is consistent.
			}   // if (_enmProcessSubsystem== PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI)

			if ( UseEventLog ( _enmOutputOptions ) )
				System.Diagnostics.EventLog.WriteEntry (
					string.IsNullOrEmpty ( _strEventSource )
						? GetDefaultEventSourceID ( )
						: _strEventSource ,
					pstrLogMsg ,
					System.Diagnostics.EventLogEntryType.Error );

			return pstrMsg;														// Return a copy to the caller.
		}   // ReportAsDirected method


		/// <summary>
		/// Scroll up one line upon request, unless the system already generated
		/// a blank line by passing the message to a WriteLine method, which
		/// appends a second newline following the one that's already there, since
		/// every message ends with a newline.
		/// </summary>
		/// <returns>
		/// The return value is a member of the ScrollUpResult bit mapped
		/// enumeration, which indicates which console stream(s) got scrolled. A
		/// message that goes out by way of a WriteLine method doesn't get one,
		/// since it's already scrolled up.
		/// </returns>
		private ScrollUpResult ScrollUp ( )
		{
			ScrollUpResult enmScrollUpResult = ScrollUpResult.NoAction;

			//	----------------------------------------------------------------
			//	Since a user might want error messages to be displayed on the
			//	console via the standard error stream and preserved in a
			//	redirected standard output stream, this method supports this use
			//	case. However, when standard output is feeding the console, this
			//	produces duplicate error messages. Hence, the second printing is
			//	suppressed when it has already gone to standard error AND the
			//	standard output stream is redirected.
			//	----------------------------------------------------------------

			bool fWroteOnStandardError = false;

			if ( ( _enmOutputOptions & OutputOptions.StandardError ) == OutputOptions.StandardError )
			{
				fWroteOnStandardError = true;

				if ( _emsgColorsStdOut != null )
				{	// If the message was rendered in default console colors, it went out by way of a WriteLine, and another is redundant.
					Console.Error.WriteLine ( );
					enmScrollUpResult = enmScrollUpResult | ScrollUpResult.StandardError;
				}	// if ( _emsgColorsStdOut != null )
			}	// if ( ( _enmOutputOptions & OutputOptions.StandardError ) == OutputOptions.StandardError )

			if ( ( _enmOutputOptions & OutputOptions.StandardOutput ) == OutputOptions.StandardOutput )
			{	// The client assembly wants errors written onto the standard output stream.
				if ( fWroteOnStandardError )
				{	// Suppress if this is a duplicate, which is true if standard error was written and standard output is attached to the console.
					if ( _fStdOutIsRedirected )
					{	// Although the message was written on standard error, since standard output is redirected, it gets a copy.
						if ( _emsgColorsStdOut != null )
						{	// If the message was rendered in default console colors, it went out by way of a WriteLine, and another is redundant.
							Console.Out.WriteLine ( );
							enmScrollUpResult = enmScrollUpResult | ScrollUpResult.StandardOutput;
						}	// if ( _emsgColorsStdOut == null )
					}	// if ( _fStdOutIsRedirected )
				}	// TRUE (The message was written on the Standard Error stream.) block, if ( fWroteOnStandardError )
				else
				{	// If it didn't go on Standard Error, the redirection state of Standard Output is moot.
					if ( _emsgColorsStdOut != null )
					{	// If the message was rendered in default console colors, it went out by way of a WriteLine, and another is redundant.
						Console.Out.WriteLine ( );
						enmScrollUpResult = enmScrollUpResult | ScrollUpResult.StandardOutput;
					}	// if ( _emsgColorsStdOut == null )
				}	// FALSE (The message was NOT written on the Standard Error stream.) block, if ( fWroteOnStandardError )
			}	// if ( ( _enmOutputOptions & OutputOptions.StandardOutput ) == OutputOptions.StandardOutput )

			return enmScrollUpResult;
		}	// ScrollUp method


		/// <summary>
		/// This method attends to default message colors, setting or
		/// reinstating them as needed.
		/// </summary>
		private void SetMessageColors ( )
		{
			#if ECHO_COLOURS
				if ( _emsgColorsStdErr == null )
					Console.WriteLine ( "On entry to SetMessageColors, _emsgColorsStdErr is NULL." );
				else
					Console.WriteLine (
						"On entry to SetMessageColors, _emsgColorsStdErr is {0} text on a {1} background." ,
						_emsgColorsStdErr.MessageForegroundColor ,
						_emsgColorsStdErr.MessageBackgroundColor );

				Console.WriteLine (
					"On entry to SetMessageColors, ErrorMessagesInColor specifies {0} text on a {1} background." ,
					ErrorMessagesInColor.FatalExceptionTextColor ,
					ErrorMessagesInColor.FatalExceptionBackgroundColor );

				Console.WriteLine (
					"On entry to SetMessageColors, the call stack is as follows:{1}{0}{1}END OF CALL STACK{1}" ,
					Environment.StackTrace ,
					Environment.NewLine );
			#endif  // #if ECHO_COLOURS

			if ( _emsgColorsStdErr != null )
				return; // Leave it.

			if ( ErrorMessagesInColor.FatalExceptionTextColor == ErrorMessagesInColor.FatalExceptionBackgroundColor )
				return; // Can't use. Treat as if null, so messages stay visible.

			_emsgColorsStdErr = new ErrorMessagesInColor (
				ErrorMessagesInColor.FatalExceptionTextColor ,
				ErrorMessagesInColor.FatalExceptionBackgroundColor );
			_emsgColorsStdOut = new MessageInColor (
				ErrorMessagesInColor.FatalExceptionTextColor ,
				ErrorMessagesInColor.FatalExceptionBackgroundColor );
		}   // SetMessageColors method
		#endregion  // Private Instance Methods


		#region Private Static Methods
		/// <summary>
		/// The last two blocks of every ReportException method are identical,
		/// and are extracted to reduce the code size.
		/// </summary>
		/// <param name="perrAny">
		/// Pass in a reference to the exception being reported, from which
		/// private method AddCommonElements, which may get folded into it,
		/// extracts the TargetSite, StackTrace, and other common properties,
		/// depending on the current state of the option flags.
		/// </param>
		/// <param name="psbMsg">
		/// Pass in a reference to the partially constructed message, which has
		/// the raw or parsed message, along with other properties that vary by
		/// exception type.
		/// 
		/// This StringBuilder is eventually sent to the console if the option
		/// flags so indicate, and becomes the value returned by the method.
		/// </param>
		/// <param name="psbLogMsg">
		/// Pass in a reference to the partially constructed message, which has
		/// the raw or parsed message, along with other properties that vary by
		/// exception type.
		/// 
		/// This StringBuilder is eventually sent to a Windows Event Log, if the
		/// option flags so indicate; otherwise, it is discarded.
		/// </param>
		/// <returns>
		/// The completed sbLogMsg is always returned to the calling routine,
		/// which may dispose of it as it sees fit, and usually discards it if
		/// the calling routine is a console program, or displays it in a
		/// message box if the program is running in the graphical subsystem.
		/// </returns>
		private string AddCommonElementsReportAndReturn (
			Exception perrAny ,
			StringBuilder psbMsg ,
			StringBuilder psbLogMsg )
		{
			AddCommonElements (
				psbMsg ,
				psbLogMsg ,
				perrAny ,
				_enmOutputOptions );

			return ReportAsDirected (
				psbMsg.ToString ( ) ,
				UseEventLog ( _enmOutputOptions )
					? PrepareMessageForEventLog ( psbLogMsg )
					: SpecialStrings.EMPTY_STRING );
		}	// AddCommonElementsReportAndReturn


		/// <summary>
		/// Prepare the log message for the event log by replacing newlines with
		/// a non-breaking space followed by a newline, a non-breaking space, by
		/// itself, or nothing, depending on the values of the DiscardNewlines
		/// and NBSpaceForNewlines bits on the _enmOutputOptions bit mask.
		/// </summary>
		/// <param name="psbLogMsg">
		/// The input is the string constructed from the components of the 
		/// exception, which usually contains numerous newlines, since the event
		/// log always gets a complete report.
		/// </param>
		/// <returns>
		/// The returned string has newlines either preserved or replaced, ready
		/// to go into the event log.
		/// </returns>
		private string PrepareMessageForEventLog ( StringBuilder psbLogMsg )
		{
			StringBuilder sbToKeep = new StringBuilder ( );

			if ( ( _enmOutputOptions & OutputOptions.DiscardNewlines ) != OutputOptions.DiscardNewlines )
				sbToKeep.Append ( Environment.NewLine );

			if ( ( _enmOutputOptions & OutputOptions.NBSpaceForNewlines ) == OutputOptions.NBSpaceForNewlines )
				sbToKeep.Append ( SpecialCharacters.NONBREAKING_SPACE_CHAR );

			return psbLogMsg.Replace (
				Environment.NewLine ,
				sbToKeep.ToString ( ) ).ToString ( );
		}	// PrepareForEventLog

		
		/// <summary>
		/// Add the Source, TargetSite, and StackTrace properties to the
		/// exception report. See Remarks.
		/// </summary>
		/// <param name="psbMsg">
		/// Append the report items to this StringBuilder.
		/// </param>
		/// <param name="psbLogMsg">
		/// Since the stack trace is always included, the message for the event
		/// log must be assembled separately. If event logging is disabled, this
		/// argument is a null reference, so we don't waste effort if it would
		/// be discarded.
		/// </param>
		/// <param name="perrAnyKind">
		/// This is an instance of the System.Exception class, or one of its
		/// derivatives. See Remarks.
		/// </param>
		/// <param name="penmOutputOptions">
		/// Combine members of the OutputOptions enumeration to specify items to
		/// include in the report, and how to log the error. (The enumeration is
		/// a bit mask.)
		/// </param>
		/// <remarks>
		/// This method is called recursively to process inner exceptions.
		///
		/// By default, all exceptions which derive from System.Exception expose
		/// these three properties, and any of them can be cast to this type.
		///
		/// The TargetSite string contains the name of the method that threw the
		/// exception.
		///
		/// The Source string contains the name of the class to which the method
		/// named in the TargetSite string belongs.
		/// </remarks>
		private static void AddCommonElements (
			StringBuilder psbMsg ,
			StringBuilder psbLogMsg ,
			Exception perrAnyKind ,
			OutputOptions penmOutputOptions )
		{
			if ( ( penmOutputOptions & OutputOptions.Method ) == OutputOptions.Method )
				psbMsg.AppendFormat (
					Properties.Resources.ERRMSG_METHOD ,                        // Format String
					StringTricks.DisplayNullSafely ( perrAnyKind.TargetSite ) , // Token Index 0
					Environment.NewLine );                                      // Token Index 1

			if ( ( penmOutputOptions & OutputOptions.Source ) == OutputOptions.Source )
				psbMsg.AppendFormat (
					Properties.Resources.ERRMSG_SOURCE ,                        // Format String
					StringTricks.DisplayNullSafely ( perrAnyKind.Source ) ,     // Token Index 0
					Environment.NewLine );										// Token Index 1

			string strPrettyStackTrace = FormatStackTrace ( perrAnyKind );

			if ( ( penmOutputOptions & OutputOptions.Stack ) == OutputOptions.Stack )
				psbMsg.Append ( strPrettyStackTrace );

			if ( psbLogMsg != null )
			{   // The event log always gets a copy of the stack trace.
				psbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_METHOD ,                        // Format String
					StringTricks.DisplayNullSafely ( perrAnyKind.TargetSite ) , // Token Index 0
					Environment.NewLine );                                      // Token Index 1
				psbLogMsg.AppendFormat (
					Properties.Resources.ERRMSG_SOURCE ,                        // Format String
					StringTricks.DisplayNullSafely ( perrAnyKind.Source ) ,     // Token Index 0
					Environment.NewLine );										// Token Index 1
				psbLogMsg.Append ( strPrettyStackTrace );
			}   // if ( psbLogMsg != null )

			//  ----------------------------------------------------------------
			//  Check for an inner exception. If there is none, say so and exit.
			//  Otherwise, do the following.
			//
			//  1)  Add the message from the InnerException to the report.
			//
			//  2)  Call AddCommonElements recursively, passing the inner
			//      exception.
			//  ----------------------------------------------------------------

			if ( perrAnyKind.InnerException == null )
			{   // Either the original exception had none, or this is the end of the chain.
				psbMsg.AppendFormat (
					Properties.Resources.THIS_IS_THE_LAST ,						// Format String
					Environment.NewLine );										// Token Index 0

				if ( psbLogMsg != null )
				{   // Since event logging is enabled, it gets a copy.
					psbLogMsg.AppendFormat (
						Properties.Resources.THIS_IS_THE_LAST ,					// Format String
						Environment.NewLine );                      			// Token Index 0
				}   // if ( psbLogMsg != null )
			}   // TRUE block, if ( perrAnyKind.InnerException == null )
			else
			{   // There is at least one inner exception. Process them recursively.
				psbMsg.AppendFormat (
					Properties.Resources.ERRMSG_INNER ,							// Format String
					perrAnyKind.InnerException.Message ,            			// Token Index 0
					Environment.NewLine );                          			// Token Index 1
				AddCommonElements (
					psbMsg ,
					psbLogMsg ,
					perrAnyKind.InnerException ,
					penmOutputOptions );
			}   // FALSE block, if ( perrAnyKind.InnerException == null )
		}   // AddCommonElements


		/// <summary>
		/// Return a new empty StringBuilder if event logging is enabled.
		/// Otherwise, return a null reference, which signals the exception
		/// reporting routines to skip building a message for it.
		/// </summary>
		/// <param name="penmOutputOptions">
		/// Since some of the methods with which it works are static because
		/// they are called recursively, this routine must also be static, and
		/// it must receive a copy of the OutputOptions bit mask.
		/// </param>
		/// <returns>
		/// If a copy of the report is bound for a Windows event log, it is
		/// constructed in the StringBuilder returned by this method. Otherwise,
		/// the null reference signals the message formatters not to bother.
		/// </returns>
		private static StringBuilder CreateForEventLog ( OutputOptions penmOutputOptions )
		{
			if ( UseEventLog ( penmOutputOptions ) )
				return new StringBuilder ( MagicNumbers.CAPACITY_08KB );
			else
				return null;
		}   // CreateForeventLog


		/// <summary>
		/// Format the stack trace to make it (hopefully) a tad easier to read.
		/// </summary>
		/// <param name="perrAnyKind">
		/// A reference to the entire exception is passed into the method, from
		/// which this routine extracts the stack trace.
		/// </param>
		/// <returns>
		/// The returned string contains the formatted stack trace.
		/// </returns>
		private static string FormatStackTrace ( Exception perrAnyKind )
		{
			if ( perrAnyKind == null )
				return SpecialStrings.EMPTY_STRING;
			else
				return string.Format (
					Properties.Resources.STACKTRACE_TPL ,                       // Format String
					StringTricks.DisplayNullSafely ( perrAnyKind.StackTrace ) , // Token index 0
					Environment.NewLine );                                      // Token index 1
		}   // FormatStackTrace


		/// <summary>
		/// Read the default event source ID string from the DLL configuration.
		/// </summary>
		/// <returns>
		/// If the function succeeds, the return value is the event source ID
		/// string stored in the configuration file that comes along for the
		/// ride whenever this DLL is imported into a project. Otherwise, the
		/// default event source ID defined in WIZARDWRX_EVENT_SOURCE_ID is
		/// returned.
		///
		/// To save trips to the disk or its cache, once read, the event source
		/// ID is cached in static string s_strDefaultEventSource.
		/// </returns>
		private string GetDefaultEventSourceID ( )
		{
			if ( string.IsNullOrEmpty ( s_strDefaultEventSource ) )
			{   // The first request initializes s_strDefaultEventSource.
				lock ( s_srCriticalSection )
				{
					try
					{
						PropertyDefaults DLLProperties = new PropertyDefaults ( System.Reflection.Assembly.GetExecutingAssembly ( ) );
                        s_strSettingsOmittedFromConfigFile = DLLProperties.EnumerateMissingConfigurationValues ( );

						if ( _enmProcessSubsystem == PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI )
						{	// Don't try to write to the console that doesn't exist.
							if ( string.IsNullOrEmpty ( s_strSettingsOmittedFromConfigFile ) )
							{
								Console.WriteLine ( @"All DLL configuration settings have explicit values." );
							}   // TRUE (All settings have explicit values.) block, if ( string.IsNullOrEmpty ( s_strSettingsOmittedFromConfigFile ) )
							else
							{
								Console.WriteLine ( s_strSettingsOmittedFromConfigFile );
							}   // FALSE (One or more settings took its default value.) block, if ( string.IsNullOrEmpty ( s_strSettingsOmittedFromConfigFile ) )
						}   // if ( _enmProcessSubsystem == PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI )

						System.Configuration.KeyValueConfigurationCollection DLLConfigurationSettings = DLLProperties.ValuesCollection;
						s_strDefaultEventSource = DLLConfigurationSettings [ Properties.Resources.DEFAULT_EVENT_SOURCE_ID ].Value;
					}
					catch ( Exception exAll )
					{   // Tell on yourself in a way that avoids creating a recursion loop that never converges.
						ExceptionLogger TattleTale = ExceptionLogger.GetTheSingleInstance ( );          // Get a local reference to the singleton, just like everybody else does.
						TattleTale.AppEventSourceID = WIZARDWRX_EVENT_SOURCE_ID;                        // To prevent a recursion loop, set the event source property to the hard coded default.

						if ( exAll.Message == Properties.Resources.EXCEPTION_MESSAGE_NULL_REFERENCE )
						{	// Generate a new exception, and substitute it for the exception that was just raised.
							try
							{	// To initialize the remaining properties of an Exception, it must be thrown. That being the case, the only thing in this try block is the throw statement.
								throw new Exception (
									string.Format (
										Properties.Resources.EXCEPTION_DLL_CONFIG_IS_MISSING ,
										System.Reflection.Assembly.GetExecutingAssembly ( ).Location ) );
							}
							catch ( Exception exExpected )
							{
								TimeStampedTraceWrite ( TattleTale.ReportException ( exExpected ) );
							}
						}	// TRUE (anticipated outcome) block, if ( exAll.Message == Properties.Resources.EXCEPTION_MESSAGE_NULL_REFERENCE )
						else
						{	// This exception came out of left field, and must be reported.
							TimeStampedTraceWrite ( TattleTale.ReportException ( exAll ) );
						}	// FALSE (unanticipated outcome) block, if ( exAll.Message == Properties.Resources.EXCEPTION_MESSAGE_NULL_REFERENCE )

						s_strDefaultEventSource = WIZARDWRX_EVENT_SOURCE_ID;                            // Implement the fail-safe default event source ID.
					}
				}   // lock ( s_srCriticalSection )
			}   // TRUE (first time in the lifetime of this application) block, if ( string.IsNullOrEmpty ( s_strDefaultEventSource ) )

			return s_strDefaultEventSource;
		}   // GetDefaultEventSourceID


		/// <summary>
		/// Look up the exception in the list of known exceptions and, if found,
		/// return a string that marks the point where the displayed message is
		/// to be truncated.
		/// </summary>
		/// <param name="pguidExceptionTypeName">
		/// This string contains the fully qualified type name of the exception,
		/// which is the key to an public Dictionary of strings that mark the
		/// point where the message should be truncated for display. Please see
		/// the Remarks section.
		/// </param>
		/// <returns>
		/// The return value, which may be the empty string, is text, such as a
		/// fixed label, that marks a point where the message supplied by the
		/// exception is truncated.
		/// </returns>
		/// <remarks>
		/// Typically, a message is truncated because we present the information
		/// in a more visually appealing and/or accessible format. Of the myriad
		/// exceptions exposed by the Base Class Library, not to mention custom
		/// exceptions derived from System.Exception, only a handful are "known"
		/// types that require attention.
		///
		/// Messages from types that are unknown to this class (i. e., they have
		/// no entry in the s_dctKnowExceptionTypes dictionary, are preserved.
		///
		/// This private method hides the processing required to cover for the
		/// unknown exception type.
		/// </remarks>
		private static string GetMessageTruncationStart ( Guid pguidExceptionTypeName )
		{
			if ( s_dctKnowExceptionTypes == null )
				return null;

			string rstrTruncateFromThisString = null;

			if ( s_dctKnowExceptionTypes.TryGetValue ( pguidExceptionTypeName , out rstrTruncateFromThisString ) )
				return rstrTruncateFromThisString;
			else
				return null;
		}   // GetMessageTruncationStart


		/// <summary>
		/// This private method beautifies the format of invalid argument
		/// exception reports.
		/// </summary>
		/// <param name="pexAnyKind">
		/// Reference to exception from which to extract and format its Message
		/// property.
		/// </param>
		/// <param name="pstrRoutineLabel">
		/// This string identifies the place in the source code where the
		/// exception was thrown.
		/// </param>
		/// <param name="pstrMessageTemplate">
		/// Format string, suitable for use with String.Format, from which the
		/// beautified message is constructed.
		/// </param>
		/// <returns>
		/// Beautified string, suitable for presentation on a console.
		/// </returns>
		private static string ReformatExceptionMessage (
			Exception pexAnyKind ,
			string pstrRoutineLabel ,
			string pstrMessageTemplate )
		{
			const string MESSAGE_PADDING = @"{0}               ";

            #if SHOW_TYPE_HANDLES
				Type typThisException = pexAnyKind.GetType ( );
				Console.WriteLine ( "{0}Exception type, as seen by ReformatExceptionMessage:{0}" , Environment.NewLine);
				Console.WriteLine ( "    Type FullName   = {0}" , typThisException.FullName );
				Console.WriteLine ( "    Type Name       = {0}" , typThisException.Name );
				Console.WriteLine ( "    Type GUID       = {0}" , typThisException.GUID );
				Console.WriteLine ( "    TypeHandle      = {0}" , typThisException.TypeHandle.Value.ToString ( NumericFormats.HEXADECIMAL_8 ) );
            #endif	// SHOW_TYPE_HANDLES

			string strRawMsg = pexAnyKind.Message;

			Guid guidExceptionTypeName = pexAnyKind.GetType ( ).GUID;
			string strTrimStartPoint = GetMessageTruncationStart ( guidExceptionTypeName );
			string strTrimmedMsg = null;

			if ( string.IsNullOrEmpty ( strTrimStartPoint ) )
				strTrimmedMsg = strRawMsg;
			else
				if ( strRawMsg.IndexOf ( strTrimStartPoint ) == MagicNumbers.STRING_INDEXOF_NOT_FOUND )
					strTrimmedMsg = strRawMsg;
				else
					strTrimmedMsg = strRawMsg.Substring (
						MagicNumbers.STRING_SUBSTR_BEGINNING ,
						strRawMsg.IndexOf ( strTrimStartPoint ) );

			return string.Format (
				pstrMessageTemplate ,
				new string [ ]
					{
						pexAnyKind.GetType ( ).FullName ,
						pstrRoutineLabel ,
						strTrimmedMsg.Replace (
							Environment.NewLine ,
							string.Format (
								MESSAGE_PADDING ,
								Environment.NewLine ) ) ,
						Environment.NewLine
					} );
		}   // ReformatExceptionMessage


		/// <summary>
		/// The purpose of this routine is to keep the code that sets the
		/// default option flags in one place only.
		/// </summary>
		/// <returns>
		/// The return value is the OutputOptions bit mask with all flags set to
		/// their initial default values.
		/// </returns>
		/// <remarks>
		/// I expect this one-line syntactic candy to be optimized away in the
		/// Release build.
		/// </remarks>
		private static OutputOptions SetDefaultOptions ( )
		{
			return OutputOptions.Method | OutputOptions.Source;
		}   // SetDefaultOptions


		/// <summary>
		/// Synchronize old and new flags and set default message colors if
		/// necessary.
		/// </summary>
		/// <param name="pTheInstance">
		/// Since this method must be static, a reference to the ExceptionLogger
		/// singleton must be passed into it.
		/// </param>
		/// <param name="penmProcessSubsystem">
		/// Use this member of the ProcessSubsystem enumeration to modify the
		/// default behavior of the exception logging methods, by enabling
		/// console output if the calling application has one.
		/// </param>
		private static void InitializeInstance (
			ExceptionLogger pTheInstance ,
			PESubsystemInfo.PESubsystemID penmProcessSubsystem )
		{
			if ( !pTheInstance._fInstanceIsInitialized )
			{	// Since this method gets called for every trip through the singleton access method, subsequent calls short circuit.
				pTheInstance._enmProcessSubsystem = penmProcessSubsystem;

				//  ----------------------------------------------------------------
				//  These are the flags that will go forward.
				//  ----------------------------------------------------------------

				switch ( penmProcessSubsystem )
				{
					case PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI:
						pTheInstance._enmOutputOptions = pTheInstance._enmOutputOptions | OutputOptions.StandardError;
						break;

					case PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI:
					case PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN:
					default:
						pTheInstance._enmOutputOptions = pTheInstance._enmOutputOptions & ( ~OutputOptions.StandardError );
						break;
				}   // switch ( penmSubsystem )

				if ( UseConsole ( pTheInstance._enmOutputOptions ) )
				{	// Both of these tasks are pointless unless logging to the console is enabled.
					ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile (
						System.Reflection.Assembly.GetExecutingAssembly ( ) );
					pTheInstance.SetMessageColors ( );
				}	// if ( UseConsole ( pTheInstance._enmOutputOptions ) )

				pTheInstance._fInstanceIsInitialized = true;
			}	// if ( !pTheInstance._fInstanceIsInitialized )
		}   // InitializeInstance


		private static bool UseConsole ( OutputOptions penmOutputOptions )
		{
			bool fStdErrEnabled = ( penmOutputOptions & OutputOptions.StandardError ) == OutputOptions.StandardError;
			bool fStdOutEnabled = ( penmOutputOptions & OutputOptions.StandardOutput ) == OutputOptions.StandardOutput;

			return fStdErrEnabled | fStdOutEnabled;
		}   // UseConsole


		/// <summary>
		/// Hide the complexity of bit mask testing from cursory scanning of the
		/// code, so that the reader doesn't feel compelled to slow down to
		/// study the bit test. At compile time, this routine is optimized away,
		/// replaced by inline code.
		/// </summary>
		/// <param name="penmOutputOptions">
		/// The OutputOptions enumeration is organized for use as a bit mask.
		/// Set its flags as desired to control the format and content of output
		/// generated by the ReportException methods.
		/// </param>
		/// <returns>
		/// Return TRUE if event logging is enabled.
		/// </returns>
		private static bool UseEventLog ( OutputOptions penmOutputOptions )
		{
			return ( penmOutputOptions & OutputOptions.EventLog ) == OutputOptions.EventLog ;
		}   // UseEventLog
		#endregion  // Private Static Methods
	}   // public sealed class ExceptionLogger
}   // partial namespace WizardWrx.DLLConfigurationManager