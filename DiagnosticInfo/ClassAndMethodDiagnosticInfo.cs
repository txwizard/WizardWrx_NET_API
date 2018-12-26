/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:			ClassAndMethodDiagnosticInfo

	File Name:			ClassAndMethodDiagnosticInfo.cs

    Synopsis:			This class is type-safe managed wrappers for kernel32
						routines LoadLibrary and GetProcAddress, published as
                        part of an article on the subject.

    Remarks:			These are 100% independent of System.Reflection, relying
                        instead on compile-time metadata supplied by the
                        System.Runtime.CompilerServices class library that ships
                        with version 4.5 and later of the Microsoft .NET
                        Framework.

    Author:             David A. Gray

    License:            Copyright (C) 2018, David A. Gray. 
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
	2018/12/24 7.14	   DAG    This class makes its debut.
    ============================================================================
*/


using System.Runtime.CompilerServices;


namespace WizardWrx
{
    /// <summary>
    /// The static methods in this class expose the internal name, source code
    /// file name, and source file line number from which calls to them
    /// originated.
    /// </summary>
    public static class ClassAndMethodDiagnosticInfo
    {
        /// <summary>
        /// Get the unqualified name of the calling method.
        /// </summary>
        /// <param name="pstrMemberName">
        /// This parameter is configured as optional, and is set by the compiler
        /// to the unqualified name of the method that called it.
        /// </param>
        /// <returns>
        /// The return value is the unqualified compile-time name of the method
        /// that called it
        /// </returns>
        public static string GetMyMethodName ( [CallerMemberName] string pstrMemberName = SpecialStrings.EMPTY_STRING )
        {
            return pstrMemberName;
        }   // public static string GetMyMethodName


        /// <summary>
        /// Get the absolute (fully qualified) name of the source file in which
        /// the calling method is defined.
        /// </summary>
        /// <param name="pstrCallerFilePath">
        /// This parameter is configured as optional, and is set by the compiler
        /// to the absolute (fully qualified) name of the source file in which
        /// the method that called it is defined.
        /// </param>
        /// <returns>
        /// The return value is the absolute (fully qualified) name of the file,
        /// indicated by <paramref name="pstrCallerFilePath"/>, in which the
        /// call originated.
        /// </returns>
        public static string GetMySourceFileName ( [CallerFilePath] string pstrCallerFilePath = SpecialStrings.EMPTY_STRING )
        {
            return pstrCallerFilePath;
        }   // public static string GetMySourceFileName


        /// <summary>
        /// Get the line number in the source file at which the call arose.
        /// </summary>
        /// <param name="pintCallerLineNumber">
        /// This parameter is configured as optional, and is set by the compiler
        /// to the line number in the source file where the call originated.
        /// </param>
        /// <returns>
        /// The return value is the line number indicated by integer
        /// <paramref name="pintCallerLineNumber"/> where the call arose.
        /// </returns>
        public static int GetMySourceLineNumber ( [CallerLineNumber] int pintCallerLineNumber = MagicNumbers.ZERO )
        {
            return pintCallerLineNumber;
        }   // public static string GetMySourceLineNumber
    }   // public static class ClassAndMethodDiagnosticInfo
}   // partial namespace WizardWrx