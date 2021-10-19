/*
    ============================================================================

    Namespace Name:         WizardWrx.Core

    Class Name:             EnvTokenExpander

    Source Module Name:     EnvTokenExpander.cs

    Synopsis:               Provide methods for performing various token
                            substitutions.

    Renarks:                I took the proof shown on the Web page cited below,
                            in Reference 1, step further, and documented how the
                            properties of two instances have the same values,
                            even though only one of the two was actually used.
                            The proof, including extra read only properties,
                            used to expose the private variables that gave rise
                            to my desire to implement the class as a singleton,
                            rather than a class composed of static methods, is
                            in archive wwBldNbrMgr_Singleton_Proof.ZIP, which
                            I'll leave in the project directory indefinitely,
                            as documentation.

    References:             1)  "Singleton Design Pattern in C# and VB.NET," at
                                http://www.dofactory.com/Patterns/PatternSingleton.aspx

                            2)  "lock Statement (C# Reference)"
                                http://msdn.microsoft.com/en-us/library/c5kehkcz(VS.80).aspx
 
    Author:             David A. Gray

    License:            Copyright (C) 2009-2021, David A. Gray. 
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

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2009/04/20 1.0.0.0 DAG Initial version.

	2017/12/10 2.3     DAG Amend to support AssemblyFileVersion, and bind to the
                           successors of the two class libraries that were
                           retired 5 years ago.

    2021/10/19 8.0.259 DAG Move this class from the wwBldNbrMgr assembly, for
                           which it was created, into WizardWrx.Core, a library.
    ============================================================================
*/

using System;


namespace WizardWrx.Core
{
    /// <summary>
    /// This class is implemented as a very lean Singleton.
    /// </summary>
    public class EnvTokenExpander
    {
        /// <summary>
        /// For reference, the character that delminits an environment string
        /// token is exposed as a public constant.
        /// </summary>
        public const char ENV_STR_DLM = '%';


        #region Singleton Pattern Implementation
        //  --------------------------------------------------------------------
        //  This private variable anchors the single instance by calling its own 
        //  class constructor, which is marked private, so that outsiders cannot
        //  call it.
        //
        //  The first time this happens, the single instance is created, and its
        //  private storage is allocated. Subsequent calls, by the same or other
        //  classes in the same application, are given a reference to the single
        //  instance.
        //  --------------------------------------------------------------------

        private static readonly EnvTokenExpander _TheInstance = new EnvTokenExpander ( );

        //  --------------------------------------------------------------------
        //  Since all of my private class variables have static initializers,
        //  the constructor doesn't have anything to do, except return the
        //  object reference. The example cited in the reference had work for
        //  its constructor to do, which was the only "extra" content in that
        //  example.
        //  --------------------------------------------------------------------

        private EnvTokenExpander ( ) { }                     // Prohibit public instances.

        /// <summary>
        /// All classes that want to call methods on this object must first call
        /// this method.
        /// </summary>
        /// <returns>
        /// A reference to the single instance of this object, which all
        /// consumers share.
        /// </returns>
        /// <remarks>
        /// This pseudo-constructor must be declared static. Its only real job
        /// is to return a reference to itself, personified as _TheInstance. 
        /// </remarks>
        public static EnvTokenExpander GetTokenExpander ( ) { return _TheInstance; }
        #endregion  // Singleton Pattern Implementation


        #region Public Methods
        /// <summary>
        /// Rather than have the caller lock the object, I provide this pair of
        /// methods that create a similar effect, by causing a competing caller
        /// of an object in a Frozen state to told to wait its turn.
        /// </summary>
        /// <returns>
        /// TRUE, unless the object is already frozen by another thread.
        /// </returns>
        public bool Acquire ( )
        {
            lock ( _objLock )
            {
                if ( _fBusy )
                {
                    return false;       // I'm busy with another thread.
                }
                else
                {
                    _fBusy = true;
                    return true;        // I'm all yours. Please thaw me when you are done.
                }
            }   // Release the object lock.
        }   // Acquire method


        /// <summary>
        /// Returns TRUE if the input string contains at least one environment
        /// string token.
        /// </summary>
        /// <param name="pstrIn">
        /// String to evaluate for presence of environment string tokens.
        /// </param>
        /// <returns>
        /// TRUE if the string contains at least one environment string token.
        /// Otherwise return FALSE.
        /// </returns>
        /// <remarks>
        /// An environment string token is defined as a substring delimited
        /// by '%' charagers, which are symbolically represented by
        /// ENV_STR_DLM in this class.
        /// 
        /// Because this is a public method of a singleton, it cannot be marked
        /// Static. If it were so marked, it would lose access to the private
        /// storage that motivated me to finally sit down and figure out how to
        /// implement the Singleton pattern in C#.
        /// </remarks>
        public bool ContainsEnvToken ( string pstrIn )
        {
            if ( string.IsNullOrEmpty ( pstrIn ) )
                return false;           // Null or empty string is the most degenerate case.

            if ( _fTokenChecked )
                throw new Exception ( Properties.Resources.ERRMSG_ILLEGAL_METHOD_CALL );

            _intPosLeftToken = pstrIn.IndexOf ( ENV_STR_DLM );
            if ( _intPosLeftToken == ArrayInfo.ARRAY_INVALID_INDEX )
                return false;           // It takes two to tango.

            _intPosRightToken = pstrIn.IndexOf (
                ENV_STR_DLM ,
                _intPosLeftToken + MagicNumbers.PLUS_ONE );

            if ( _intPosRightToken == ArrayInfo.ARRAY_INVALID_INDEX )
                return CheckStateAndReturnResult ( false ); // It takes two to tango.
            else
                return CheckStateAndReturnResult ( true );  // And we have a pair.
        }   // ContainsEnvToken method


        /// <summary>
        /// If the input string contains an enviroment token, extract and return
        /// it.
        /// </summary>
        /// <param name="pstrIn">
        /// String from which to extract an environment variable token.
        /// </param>
        /// <returns>
        /// String containing the token, minus its delimiters.
        /// </returns>
        /// <remarks>
        /// An environment string token is defined as a substring delimited
        /// by '%' charagers, which are symbolically represented by
        /// ENV_STR_DLM in this class.
        /// 
        /// Since this method returns the string without its delimiters, pass it
        /// to static method TokenizeEnvVarName to convert it to a string that
        /// can be passed into a Replace method, to replace the token with its
        /// value, which you got by calling Environment.GetEnvironmentVariable.
        /// 
        /// Because this is a public method of a singleton, it cannot be marked
        /// Static. If it were so marked, it would lose access to the private
        /// storage that motivated me to finally sit down and figure out how to
        /// implement the Singleton pattern in C#.
        /// </remarks>
        public string ExtractEnvToken ( string pstrIn )
        {
            if ( ( _fTokenChecked && _intPosRightToken > MagicNumbers.ZERO ) || ContainsEnvToken ( pstrIn ) )
            {   // Clearing this flag allows the place markers to stay frozen.
                _fTokenChecked = false;
                return pstrIn.Substring (
                    _intPosLeftToken
                    + MagicNumbers.PLUS_ONE ,
                    ( _intPosRightToken
                    - _intPosLeftToken )
                    - MagicNumbers.PLUS_ONE );
            }   // TRUE (anticipated outcome) block, if ( ( _fTokenChecked && _intPosRightToken > MagicNumbers.ZERO ) || ContainsEnvToken ( ref pstrIn ) )
            else
            {
                _fTokenChecked = false;                     // By clearing this flag, we can stay frozen.
                return SpecialStrings.EMPTY_STRING;         // The string is devoid of tokens.
            }   // FALSE (unanticipated outcome) block, if ( ( _fTokenChecked && _intPosRightToken > MagicNumbers.ZERO ) || ContainsEnvToken ( ref pstrIn ) )
        }   // ExtractEnvToken method


        /// <summary>
        /// Rather than have the caller lock the object, I provide this pair of
        /// methods that create a similar effect, by causing a competing caller
        /// of an object in a Frozen state to be thrown and exception.
        /// </summary>
        /// <returns>
        /// Always True.
        /// </returns>
        public bool Free ( )
        {
            lock ( _objLock )
            {
                _fBusy = false;
                return true;
            }   // Release the object lock.
        }   // Free method
        #endregion  // Public Methods


        #region Public Properties
        /// <summary>
        /// Returns TRUE if the EnvTokenExpander is Busy, which means that a thread
        /// has acquired it, by calling its Acquire method.
        /// </summary>
        /// <remarks>
        /// By acquiring the EnvTokenExpander, a thread can shave a tad off the
        /// processing needed to find the first occurrance of a token, in order
        /// to extract it, if it intends to call ContainsEnvToken before calling
        /// ExtractEnvToken.
        /// </remarks>
        public bool Busy { get { return _fBusy; } }
        #endregion  // Public Properties


        #region Public Static Methods
        /// <summary>
        /// Given a string that contains one or more environment string tokens,
        /// expand the tokens, returning the resulting string.
        /// </summary>
        /// <param name="pstrIn">
        /// String to parse and expand environment string tokens.
        /// </param>
        /// <param name="pstrDefault">
        /// Optional string to return as default if an undefined (empty)
        /// environment variable is encountered in input string
        /// <paramref name="pstrIn"/>.
        /// </param>
        /// <returns>
        /// The return value is string <paramref name="pstrIn"/> with its
        /// environment tokens expanded.
        /// </returns>
        public static string ExpandEnvironmentTokens (
            string pstrIn ,
            string pstrDefault = null )
        {
            //  ------------------------------------------------------------
            //  The quick and dirty way to do this would be to call
            //  GetEnvironmentVariables, once only, but I chose otherwise
            //  for two reasons.
            //
            //  1)  GetEnvironmentVariables uses COM Interop to get the
            //      environment block.
            //
            //  2)  The result wouldn't give me as much information about
            //      any missing string. Without doing the work done here, my
            //      error message would be confined to saying that there is
            //      an enviornment variable, somewhere in this string, that
            //      has no value in the current context.
            //
            //  I'd rather do the extra work.
            //  ------------------------------------------------------------

            string rstrExpanded = pstrIn;

            EnvTokenExpander te = GetTokenExpander ( );

            if ( te.Acquire ( ) )
            {
                while ( te.ContainsEnvToken ( rstrExpanded ) )
                {
                    string strEnvToken = te.ExtractEnvToken ( pstrIn );
                    string strEnvString = Environment.GetEnvironmentVariable (
                        strEnvToken ,
                        EnvironmentVariableTarget.Process );

                    if ( string.IsNullOrEmpty ( strEnvString ) && pstrDefault == null )
                    {   // Release the EnvTokenExpander now. The exception bypasses the end of the loop, where it would be cleared.
                        te.Free ( );
                        throw new Exception (
                            string.Format (
                                Properties.Resources.ERRMSG_ENVIRONMENT_TOKEN_UNDEFINED ,
                                strEnvToken ,
                                pstrIn ) );
                    }   // if ( string.IsNullOrEmpty ( strEnvString ) && pstrDefault == null )

                    rstrExpanded = rstrExpanded.Replace (
                        TokenizeEnvVarName (
                            strEnvToken ) ,
                        strEnvString == null
                            ? pstrDefault
                            : strEnvString );
                }   // while ( te.ContainsEnvToken ( rstrExpanded ) ) [enviornment string expansions]

                te.Free ( );

                return rstrExpanded;
            }   // TRUE (expected outcome) block, if ( te.Acquire ( ) )
            else
            {   // This should never happen in this single threaded application.
                throw new Exception ( Properties.Resources.ERRMSG_ENVIRONMENT_UNAVAILABLE );
            }   // FALSE (unexpected outcome) block, if ( te.Acquire ( ) )
        }   // ExpandEnvironmentTokens method


        /// <summary>
        /// Given a string, assumed to be the name of an environment string,
        /// return it with the expected escape ("quote") characters appended to
        /// both ends.
        /// </summary>
        /// <param name="pstrIn">
        /// String to turn into an environment variable token.
        /// </param>
        /// <returns>
        /// String pstrIn, with the escape characters (%) appended to both ends.
        /// </returns>
        /// <remarks>
        /// Use this method to turn the output of ExtractEnvToken into a string
        /// that can be fed to a Replace method to substitute the value of the
        /// environment variable for its token.
        /// </remarks>
        public static string TokenizeEnvVarName ( string pstrIn )
        {
            const string ENV_TOKEN_TPL = @"{1}{0}{1}";

            if ( string.IsNullOrEmpty ( pstrIn ) )
                return string.Empty;
            else
                return string.Format (
                    ENV_TOKEN_TPL ,
                    pstrIn ,
                    ENV_STR_DLM );
        }   // TokenizeEnvVarName method
        #endregion  // Public Static Methods


        #region Private Code
        private bool CheckStateAndReturnResult ( bool pfResult )
        {
            if ( _fBusy )                                   // Did somebody freeze me?
                _fTokenChecked = true;                      // Yes, so freeze my token positions.
            else
                _fTokenChecked = false;                     // No, so ensure that flag is clear.

            return pfResult;                                // Return result from ContainsEnvToken. 
        }   // CheckStateAndReturnResult method
        #endregion  // Private Code


        #region Private Storage
        //  --------------------------------------------------------------------
        //  By giving these class scope, ContainsEnvToken does double duty by
        //  saving the start and end positions of the token, so that companion
        //  method ExtractEnvToken can use them.
        //  --------------------------------------------------------------------

        private bool _fBusy = false;          // Set and cleared by Acquire and Free methods.

        //  --------------------------------------------------------------------
        //  Flag _fTokenChecked is cleared by Acquire AND Free, 
        //
        //  ContainsEnvToken checks it, won't scan a string if it's been set,
        //  and sets it if the object is frozen.
        //
        //  ExtractEnvToken checks it, and, if true, skips ContainsEnvToken,
        //  assuming it's safe to use the current token positions, saved by the
        //  last call on ContainsEnvToken.
        //  --------------------------------------------------------------------

        private bool _fTokenChecked = false;
        private int _intPosLeftToken = ArrayInfo.ARRAY_INVALID_INDEX;
        private int _intPosRightToken = ArrayInfo.ARRAY_INVALID_INDEX;
        private static object _objLock = new object ( );  // See Reference 2.
        #endregion    // Private Storage
    }   // class EnvTokenExpander
}   // partial namespace WizardWrx.Core