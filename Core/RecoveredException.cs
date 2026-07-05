/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:			RecoveredException

	File Name:			RecoveredException.cs

    Synopsis:			Fully emulate an exception as if it had been thrown.

    Remarks:			This class preserves the machine state as if it had been
                        thrown from the point in the code where the constructor
                        is called. Classes that inherit sibling class
                        AssemblyLocatorBase use this method to preserve harmless
                        exceptions that are trapped, but not thrown, to avoid
                        cluttering the debug log with reports of exceptions that
                        are harmless.

    Author:				David A. Gray

    License:            Copyright (C) 2014-2017, David A. Gray. 
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
	2019/04/27 7.15    DAG    This class makes its debut.
    ============================================================================
*/

using System;
using System.Runtime.Serialization;

namespace WizardWrx
{
    /// <summary>
    /// Override the Exception class, so that the Source, StackTrace, and
    /// TargetSite properties can be directly initialized.
    /// </summary>
    public class RecoveredException : Exception
    {
        /// <summary>
        /// The default constructor must be marked as private, so that it cannot
        /// be called.
        /// </summary>
        private RecoveredException ( )
        {
        }   // RecoveredException (default) constructor, 1 of 6


        /// <summary>
        /// The usual constructor that takes a Message must also be marked as
        /// private, so that it cannot be called, either.
        /// </summary>
        /// <param name="message">
        /// The usual message property, available to all Exception consumers
        /// </param>
        private RecoveredException (
            string message )
        {
        }   // RecoveredException (most commonly used) constructor, 2 of 6


        /// <summary>
        /// The usual constructor that takes a <paramref name="message"/> and an
        /// <paramref name="innerException"/> must also be hidden from view.
        /// </summary>
        /// <param name="message">
        /// The usual message property, available to all Exception consumers
        /// </param>
        /// <param name="innerException">
        /// The usual InnerException property, available to all Exception consumers
        /// </param>
        private RecoveredException (
            string message ,
            Exception innerException )
        {
        }   // RecoveredException (next most commonly used) constructor, 3 of 6


        /// <summary>
        /// Call this method when you have only a message to record.
        /// </summary>
        /// <param name="message">
        /// The usual message property, available to all Exception consumers
        /// </param>
        /// <param name="Source"></param>
        /// Gets or sets the name of the application or the object that causes the error
        /// <param name="StackTrace">
        /// Gets a string representation of the immediate frames on the call stack
        /// </param>
        /// <param name="TargetSite">
        /// Gets the method that throws the current exception
        /// </param>
        public RecoveredException (
            string message ,
            string Source ,
            string StackTrace ,
            string TargetSite )
            : base ( message )
        {
            InitializeInstance (
                Source ,
                StackTrace ,
                TargetSite );
        }   // RecoveredException (replacement for most commonly used) constructor, 4 of 6


        /// <summary>
        /// Call this method when you have an InnerException Exception to preserve.
        /// </summary>
        /// <param name="message">
        /// The usual message property, available to all Exception consumers
        /// </param>
        /// <param name="innerException">
        /// The usual InnerException property, available to all Exception consumers
        /// </param>
        /// <param name="Source"></param>
        /// Gets or sets the name of the application or the object that causes the error
        /// <param name="StackTrace">
        /// Gets a string representation of the immediate frames on the call stack
        /// </param>
        /// <param name="TargetSite">
        /// Gets the method that throws the current exception
        /// </param>
        public RecoveredException (
            string message ,
            Exception innerException ,
            string Source ,
            string StackTrace ,
            string TargetSite )
            : base ( message , innerException )
        {
            InitializeInstance (
                Source ,
                StackTrace ,
                TargetSite );
        }   // RecoveredException (replacement for next most commonly used) constructor, 5 of 6


        /// <summary>
        /// Enable serialization by implementing the protected constructor that
        /// can recreate an exception from serialization data.
        /// </summary>
        /// <param name="info">
        /// Specify the SerializationInfo object from which to reconstruct the
        /// RecoveredException.
        /// </param>
        /// <param name="context">
        /// Specify the StreamingContext object from which to reconstruct the
        /// RecoveredException.
        /// </param>
        protected RecoveredException (
            SerializationInfo info ,
            StreamingContext context )
           : base ( info , context )
        {
        }   // RecoveredException protected deserialization constructor, 6 of 6


        /// <summary>
        /// Both public constructors call this little routine to initialize the
        /// three properties that are usually initalized by the runtime system.
        /// </summary>
        /// <param name="source">
        /// Gets or sets the name of the application or the object that causes the error
        /// </param>
        /// <param name="stackTrace">
        /// Gets a string representation of the immediate frames on the call stack
        /// </param>
        /// <param name="targetSite">
        /// Gets the method that throws the current exception
        /// </param>
        private void InitializeInstance (
            string source ,
            string stackTrace ,
            string targetSite )
        {
            Source = source;
            StackTrace = PruneStackTrace ( stackTrace );
            TargetSite = targetSite;
        }   // private void InitializeInstance


        /// <summary>
        /// If the stack trace originated from the System.Environment property,
        /// its first two entries are irrelevant, and should be discarded. This
        /// routine makes that happen.
        /// </summary>
        /// <param name="pstrStackTrace"></param>
        /// This method receives the stack trace that was passed into the
        /// sonctructor.
        /// <returns>
        /// If the stack trace came from the System.Environment.StackTrace
        /// property, its first two entries are discarded. Otherwise, the whole
        /// string is returned.
        /// </returns>
        private string PruneStackTrace ( string pstrStackTrace )
        {
            string strGetStackTraceFromEnviMethodSignarure = Core.Properties.Resources.MSG_STACK_TRACE_IS_FROM_ENVIRONMENT;
            int intPosGetStackTraceFromEnvironment = pstrStackTrace.IndexOf (
                strGetStackTraceFromEnviMethodSignarure );

            if ( intPosGetStackTraceFromEnvironment > ListInfo.INDEXOF_NOT_FOUND )
            {
                return pstrStackTrace.Substring (
                      intPosGetStackTraceFromEnvironment
                    + strGetStackTraceFromEnviMethodSignarure.Length
                    + Environment.NewLine.Length );
            }   // TRUE (The System.Environment singleton provided the stack trace.) block, if ( intPosGetStackTraceFromEnvironment > ListInfo.INDEXOF_NOT_FOUND )
            else
            {
                return pstrStackTrace;
            }   // FALSE (The runtime system generated the stack trace, perhaps because it was copied from a real Exception that was thrown.) block, if ( intPosGetStackTraceFromEnvironment > ListInfo.INDEXOF_NOT_FOUND )
        }   // private string PruneStackTrace 


        /// <summary>
        /// This is a stand-in for the Source property that is usually set
        /// by the runtime environment when an exception is thrown.
        /// </summary>
        public override string Source => base.Source;


        /// <summary>
        /// This is a stand-in for the StackTrace property that is usually set
        /// by the runtime environment when an exception is thrown.
        /// </summary>
        public new string StackTrace
        {
            get; set;
        }   // public new string StackTrace


        /// <summary>
        /// This is a stand-in for the TargetSite property that is usually set
        /// by the runtime environment when an exception is thrown.
        /// </summary>
        public new string TargetSite
        {
            get; set;
        }   // public new string TargetSite
    }   // public RecoveredException class
}   // partial namespace WizardWrx