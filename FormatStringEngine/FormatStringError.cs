/*
    ============================================================================

    Namespace:          WizardWrx.FormatStringEngine

    Class Name:         FormatStringError

    File Name:          FormatStringError.cs

    Synopsis:           This class organizes the details of a comprehensive
                        diagnostic message about an error in a format string.
                        The FormatStringParser exposes a List of these through a
                        FormatStringErrors property.

    Remarks:            The only externally visible parts of an instance of this
                        class are its ToString method, which overrides the base
                        class (object) method to return properties and labels as
                        a well formed CSV string, and a dedicated Split method,
                        meant to seriously discourage you from trying in vain to
                        use string.Split() to split it into its components.

    License:            Copyright (C) 2014-2019, David A. Gray.
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

    Created:            Tuesday, 15 July 2014 - Saturday, 19 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2014/07/19 1.0     DAG Initial implementation.

    2016/06/12 3.0     DAG Break the dependency on WizardWrx.SharedUtl2.dll,
                           correct misspelled words flagged by the spelling
                           checker add-in, and incorporate my three-clause BSD
                           license.

                           Reclassify this class as infrastructure, and mark it
                           Internal.

    2017/03/04 3.1     DAG Document the reason that the Split method raises a
                           warning about WizardWrx.AnyCSV.Parser.Parse having a
                           more restrictive build target (x86) than this library
                           (MSIL).

    2017/08/04 7.0     DAG Relocated to the constellation of core libraries that
                           began as WizardWrx.DllServices2.dll.

    2019/12/15 7.23    DAG Allow the tab consistency add-in to replace tabs with
                           spaces. The code is otherwise unchanged, although the
                           new build is required to add a binding redirect, and
                           the version numbering transitions to the SemVer
                           scheme.
    ============================================================================
*/

using System;
using System.Text;

namespace WizardWrx.FormatStringEngine
{
    /// <summary>
    /// The FormatStringParser exposes a List of these objects if errors exist
    /// in its current format string.
    /// </summary>
    /// <remarks>
    /// The only property that has any use outside the defining assembly is the
    /// string representation, which returns the properties in a well formed CSV
    /// string.
    ///
    /// Likewise, only other classes defined in the same assembly are allowed to
    /// create instances of this object, which are immutable.
    /// </remarks>
    public class FormatStringError
    {
        int _intOffset = FormatItem.INVALID_OFFSET;
        char _chrBad = char.MinValue;
        FormatStringParser.State _enmState = FormatStringParser.State.NotStarted;
        string _strMessage = null;

        internal FormatStringError ( )
        { } // The default constructor satisfies a requirement of IList.

        internal FormatStringError (
            int pintOffset ,
            char pchrBad ,
            FormatStringParser.State penmState ,
            string pstrMessage )
        {
            _intOffset = pintOffset;
            _chrBad = pchrBad;
            _enmState = penmState;
            _strMessage = pstrMessage;
        }   // This is the real constructor.

        internal int Offset { get { return _intOffset; } }
        internal char BadCharacter { get { return _chrBad; } }
        internal FormatStringParser.State ParserState { get { return _enmState; } }
        internal string Message { get { return _strMessage; } }


        /// <summary>
        /// This constant is the field delimiter.
        /// </summary>
        public const char FIELD_DELIMITER = ',';            // Both Split and ToString use it, and callers may as well know about it.


        /// <summary>
        /// Since some strings necessarily contain embedded commas, such commas
        /// must be protected. This is accomplished by enclosing the affected
        /// string in double quotation marks before it is appended.
        /// </summary>
        public const char COMMA_GUARD = '"';


        /// <summary>
        /// This constant delimits the field label from its value.
        /// </summary>
        public const char LABEL_VALUE_DELIMITER = '=';      // This is for the benefit of consumers.


        /// <summary>
        /// Split the message into an array of parts, each containing a property
        /// value and its label.
        ///
        /// Please see the remarks.
        /// </summary>
        /// <returns>
        /// The return value is an array of strings.
        ///
        /// Please see the remarks for essential information about this string.
        /// </returns>
        /// <remarks>
        /// This function is provided to facilitate correctly splitting a string,
        /// returned by the ToString method on an instance, into fields, each of
        /// which is a name/value pair that can be further subdivided, using the
        /// built-in string.Split method, along with LABEL_VALUE_DELIMITER, the
        /// correct delimiter to use with string.Split.
        ///
        /// Under the covers, Split passes the string returned by a call to the
        /// instance ToString method to the static WizardWrx.AnyCSV.Parser.Parse
        /// method, which does most of the work.
        ///
        /// IMPORTANT: Since the new version of WizardWrx.AnyCSV.dll has a COM
        /// interface, it had to target the x86 platform. This can eventually be
        /// corrected by building a parallel version that isn't exposed to COM.
        /// </remarks>
        public string [ ] Split ( )
        {
            return WizardWrx.AnyCSV.Parser.Parse (
                this.ToString ( ) ,
                FIELD_DELIMITER );
        }   // public string [ ] Split ( )


        /// <summary>
        /// Format the properties into a comma-delimited string, which may be
        /// used as is or processed by the Split method on this instance.
        ///
        /// Please see the remarks for essential information about this string.
        /// </summary>
        /// <returns>
        /// Please see the remarks for essential information about this string.
        /// </returns>
        /// <remarks>
        /// The standard string.Split method cannot split this string correctly,
        /// because it treats commas embedded in its strings as delimiters, thus
        /// splitting the string into too many pieces, and at the wrong places.
        ///
        /// Use the Split method on this instance, which employs a robust CSV
        /// parsing engine that splits it correctly.
        /// </remarks>
        public override string ToString ( )
        {
            ASCIICharacterDisplayInfo [ ] asciiCharTbl = ASCII_Character_Display_Table.GetTheSingleInstance ( ).AllASCIICharacters;

            StringBuilder sbTheBadChar = new StringBuilder ( );

            sbTheBadChar.Append ( asciiCharTbl [ ( uint ) _chrBad ].DisplayText );

            string strTheMessage;

            if ( _strMessage.IndexOf ( FIELD_DELIMITER ) > FormatItem.INVALID_OFFSET )
                strTheMessage = string.Concat ( new object [ ]
                {
                    COMMA_GUARD ,
                    _strMessage ,
                    COMMA_GUARD
                } );
            else
                strTheMessage = _strMessage;

            return string.Format (
                Properties.Resources.ERRMSG_FORMAT_STRING_ERROR_TPL ,   // Message template
                new object [ ]
                {
                    _intOffset ,                                        // Format Item 0 = Offset
                    sbTheBadChar ,                                      // Format Item 1 = Character
                    _enmState ,                                         // Format Item 2 = Parser State
                    strTheMessage                                       // Format Item 3 = Message
                } );
        }   // public override string ToString
    }   // public FormatStringError
}   // partial namespace WizardWrx.FormatStringEngine