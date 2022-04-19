/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:			TraceLogger

	File Name:			TraceLogger.cs

    Synopsis:			This class defines static methods for writing time 
						stamped trace log messages.

    Remarks:			Two of these routines use one string that would usually
						go into a string table. However, at least for now, they
						are defined as private constants.

						For all practical purposes, Trace.WriteLine is one
						method, which accepts an Object, with one overload, that
						takes a String. To date, this is the only WriteLine that
						I have encountered that delegates all formatting to the
						calling routine.

    Author:				David A. Gray

    License:            Copyright (C) 2017-2022, David A. Gray. 
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
 `	2017/03/31 1.0	   DAG    Class created, tested, and deployed as part of
                              VSHostingProcess_Demo.

	2017/04/01 1.1     DAG    Tweak the output a bit, and substantially improve
                              the internal documentation throughout.

    2017/04/03 7.0     DAG    Incorporate into WizaerdWrx.Core.dll, and the root
                              WizaerdWrx namespace.

    2022/04/18 8.0.280 DAG    Add an optional StreamWriter argument to all four
                              methods, so that they can be used withut wriing up
                              a Trace Listener and incurring its overhead.
    ============================================================================
*/


using System;
using System.Diagnostics;
using System.IO;


namespace WizardWrx.Core
{
	/// <summary>
	/// This static class exposes methods that support every conceivable
	/// combination of local and UTC time stamps for trace logging.
	/// </summary>
	/// <remarks>
	/// The methods that record both local and UTC time capture the current UTC
	/// time, then convert it into local time. This guarantees that both times
	/// are truly equal, and that local and UTC times are unambiguous.
	/// </remarks>
	public static class TraceLogger
	{
		const string TPL_LABELED_BOTH_TIMES = @"{0} ({1}) {2} ({3}): {4}";
		const string TPL_LABELED_LOCAL_TIME = @"{0} {1}: {2}";
		const string TPL_LABELED_UTC_TIME = @"{0} {1}: {2}";

		const string TPL_UNLABELED_TIME = @"{0}: {1}";
		const string TPL_UNLABELED_BOTH_TIMES = @"{0} ({1}): {2}";

		const string TRACE_LOG_NEWLINE_REPLACEMENT = @" - ";


		/// <summary>
		/// Write a time stamped trace log message, using the local and UTC 
		/// machine times as its time stamp prefix, local first, followed by
		/// UTC in parentheses.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithBothTimesLabeledLocalFirst (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			DateTime dtmUtcNow = DateTime.UtcNow;
			DateTime dtmLocal = dtmUtcNow.ToLocalTime ( );

			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_LABELED_BOTH_TIMES ,                                // Format Control String, followed by implicit parameter array
						dtmLocal ,                                              // Format Item 0 = Current local machine time value
						Properties.Resources.TIME_LABEL_LOCAL_SHORT ,           // Format Item 1 = Current local machine time label
						dtmUtcNow ,                                             // Format Item 2 = Current UTC machine time value
						Properties.Resources.TIME_LABEL_UTC ,                   // Format Item 3 = Current UTC machine time label
						ReplaceNewlines ( pstrMessage ) ) );					// Format Item 4 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_LABELED_BOTH_TIMES ,                                // Format Control String, followed by implicit parameter array
						dtmLocal ,                                              // Format Item 0 = Current local machine time value
						Properties.Resources.TIME_LABEL_LOCAL_SHORT ,           // Format Item 1 = Current local machine time label
						dtmUtcNow ,                                             // Format Item 2 = Current UTC machine time value
						Properties.Resources.TIME_LABEL_UTC ,                   // Format Item 3 = Current UTC machine time label
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 4 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithBothTimesLabeledLocalFirst


		/// <summary>
		/// Write a time stamped trace log message, using the local and UTC 
		/// machine times as its time stamp prefix, UTC first, followed by
		/// local in parentheses.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithBothTimesLabeledUTCFirst (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			DateTime dtmUtcNow = DateTime.UtcNow;
			DateTime dtmLocal = dtmUtcNow.ToLocalTime ( );

			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_LABELED_BOTH_TIMES ,                                // Format Control String
						new object [ ]                                          // This parameter array is explicit, and is built dynamically on the stack.
						{
							dtmUtcNow ,											// Format Item 0 = Current UTC machine time value
							Properties.Resources.TIME_LABEL_UTC ,				// Format Item 1 = Current UTC machine time label
							dtmLocal ,											// Format Item 2 = Current local machine time value
							Properties.Resources.TIME_LABEL_LOCAL_SHORT ,		// Format Item 3 = Current local machine time label
							ReplaceNewlines(pstrMessage )                       // Format Item 4 = Message
						} ) );
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_LABELED_BOTH_TIMES ,                                // Format Control String
						new object [ ]                                          // This parameter array is explicit, and is built dynamically on the stack.
						{
							dtmUtcNow ,											// Format Item 0 = Current UTC machine time value
							Properties.Resources.TIME_LABEL_UTC ,				// Format Item 1 = Current UTC machine time label
							dtmLocal ,											// Format Item 2 = Current local machine time value
							Properties.Resources.TIME_LABEL_LOCAL_SHORT ,		// Format Item 3 = Current local machine time label
							ReplaceNewlines(pstrMessage )                       // Format Item 4 = Message
						} ) );
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithBothTimesLabeledUTCFirst


		/// <summary>
		/// Write a time stamped trace log message, using the local and UTC 
		/// machine times as its time stamp prefix, local first, followed by
		/// UTC in parentheses.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithBothTimesUnlabeledLocalFirst (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			DateTime dtmUtcNow = DateTime.UtcNow;
			DateTime dtmLocal = dtmUtcNow.ToLocalTime ( );

			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_UNLABELED_BOTH_TIMES ,                              // Format Control String
						dtmLocal ,                                              // Format Item 0 = Current local machine time value
						dtmUtcNow ,                                             // Format Item 1 = Current UTC machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 4 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_UNLABELED_BOTH_TIMES ,                              // Format Control String
						dtmLocal ,                                              // Format Item 0 = Current local machine time value
						dtmUtcNow ,                                             // Format Item 1 = Current UTC machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 4 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithBothTimesUnlabeledLocalFirst


		/// <summary>
		/// Write a time stamped trace log message, using the local and UTC 
		/// machine times as its time stamp prefix, UTC first, followed by
		/// local in parentheses.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithBothTimesUnlabeledUTCFirst (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			DateTime dtmUtcNow = DateTime.UtcNow;
			DateTime dtmLocal = dtmUtcNow.ToLocalTime ( );

			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_UNLABELED_BOTH_TIMES ,                              // Format Control String
						dtmUtcNow ,                                             // Format Item 0 = Current UTC machine time value
						dtmLocal ,                                              // Format Item 1 = Current local machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 2 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_UNLABELED_BOTH_TIMES ,                              // Format Control String
						dtmUtcNow ,                                             // Format Item 0 = Current UTC machine time value
						dtmLocal ,                                              // Format Item 1 = Current local machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 2 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithBothTimesUnlabeledUTCFirst


		/// <summary>
		/// Write a time stamped trace log message, using the local machine time
		/// as its time stamp prefix.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithLabeledLocalTime (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_LABELED_LOCAL_TIME ,                                // Format Control String
						DateTime.Now ,                                          // Format Item 0 = Current local machine time value
						Properties.Resources.TIME_LABEL_LOCAL_SHORT ,           // Format Item 1 = Short label for local time stamp
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 2 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_LABELED_LOCAL_TIME ,                                // Format Control String
						DateTime.Now ,                                          // Format Item 0 = Current local machine time value
						Properties.Resources.TIME_LABEL_LOCAL_SHORT ,           // Format Item 1 = Short label for local time stamp
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 2 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithLabeledLocalTime


		/// <summary>
		/// Write a time stamped trace log message, using the local machine time
		/// as its time stamp prefix.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithUnlabeledLocalTime (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_UNLABELED_TIME ,                                    // Format Control String
						DateTime.Now ,                                          // Format Item 0 = Current local machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 1 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_UNLABELED_TIME ,                                    // Format Control String
						DateTime.Now ,                                          // Format Item 0 = Current local machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 1 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithUnlabeledLocalTime


		/// <summary>
		/// Write a time stamped trace log message, using the UTC machine time
		/// as its time stamp prefix.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithLabeledUTCTime (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_LABELED_UTC_TIME ,                                  // Format Control String
						DateTime.UtcNow ,                                       // Format Item 0 = Current local machine time value
						Properties.Resources.TIME_LABEL_UTC ,                   // Format Item 1 = Label for UTC time, of which there is but one.
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 2 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_LABELED_UTC_TIME ,                                  // Format Control String
						DateTime.UtcNow ,                                       // Format Item 0 = Current local machine time value
						Properties.Resources.TIME_LABEL_UTC ,                   // Format Item 1 = Label for UTC time, of which there is but one.
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 2 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // WriteWithLabeledUTCTime


		/// <summary>
		/// Write a time stamped trace log message, using the UTC machine time
		/// as its time stamp prefix.
		/// </summary>
		/// <param name="pstrMessage">
		/// The specified string is written verbatim, immediately after the time
		/// stamp.
		/// </param>
		/// <param name="psw">
		/// When supplied, this StreamWriter takes the place of the Trace Listener.
		/// </param>
		public static void WriteWithUnlabeledUTCTime (
			string pstrMessage ,
			StreamWriter psw = null )
		{
			if ( psw == null )
			{
				Trace.WriteLine (
					string.Format (
						TPL_UNLABELED_TIME ,                                    // Format Control String
						DateTime.UtcNow ,                                       // Format Item 0 = Current local machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 1 = Message
			}   // TRUE, and backward compatible, block, if ( psw == null )
			else
			{
				psw.WriteLine (
					string.Format (
						TPL_UNLABELED_TIME ,                                    // Format Control String
						DateTime.UtcNow ,                                       // Format Item 0 = Current local machine time value
						ReplaceNewlines ( pstrMessage ) ) );                    // Format Item 1 = Message
			}   // FALSE, dispensing with the Trace Listener, block, if ( psw == null )
		}   // public static void WriteWithUnlabeledUTCTime


		/// <summary>
		/// This private method replaces newlines embedded in the message with a
		/// string that maintains separation of the text, while keeping it on a
		/// single logical output line.
		/// </summary>
		/// <param name="pstrMessage">
		/// This argument specifies the string that needs its embedded newlines replaced.
		/// </param>
		/// <returns>
		/// In the current implementation, the replacement string is a privately defined
		/// constant.
		/// </returns>
		private static string ReplaceNewlines ( string pstrMessage )
		{
			return pstrMessage.Replace (
				Environment.NewLine ,
				TRACE_LOG_NEWLINE_REPLACEMENT );
		}   // private static string ReplaceNewlines
	}   // public static class TraceLogger
}	// partial namespace WizardWrx.Core