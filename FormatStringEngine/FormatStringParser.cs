/*
    ============================================================================

    Namespace:          WizardWrx.FormatStringEngine

    Class Name:         FormatStringParser

    File Name:          FormatStringParser.cs

    Synopsis:           This class implements a robust engine for parsing format
                        strings used with string.Format and the numerous methods
                        derived from it.

    Remarks:            After two days of intensive work, I concluded that it is
                        impractical, maybe impossible, to correctly parse format
                        strings with regular expressions.

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

    Created:            Monday, 14 July 2014 - Saturday, 19 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2014/07/19 1.0     DAG Initial implementation.

    2016/06/12 3.0     DAG Break the dependency on WizardWrx.SharedUtl2.dll,
                           correct misspelled words flagged by the spelling
                           checker add-in, and incorporate my three-clause
                           BSD license.

    2017/08/13 7.0     DAG Relocated to the constellation of core libraries that
                           began as WizardWrx.DllServices2.dll.

    2019/12/15 7.23    DAG Allow the tab consistency add-in to replace tabs with
                           spaces. The code is otherwise unchanged, although the
                           new build is required to add a binding redirect, and
                           the version numbering transitions to the SemVer
                           scheme.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WizardWrx.FormatStringEngine
{
    /// <summary>
    /// Use this class to parse format control strings that you intend to use
    /// with string.Format or one of the Write or WriteLine methods of a stream
    /// object, such as a Console or TextWriter. Properties and methods report
    /// on its attributes, including its FormaItems, and errors flagged by the
    /// parser.
    /// </summary>
    public class FormatStringParser
    {
        #region Public Constants
        /// <summary>
        /// To include a literal opening brace (that is, one that you want to
        /// appear in the completed string), it must be escaped, which is
        /// accomplished by including a pair of them where you want one to
        /// appear.
        /// </summary>
        public const string ESCAPED_ITEM_BEGIN = @"{{";


        /// <summary>
        /// To include a literal closing brace (that is, one that you want to
        /// appear in the completed string), it must be escaped, which is
        /// accomplished by including a pair of them where you want one to
        /// appear.
        /// </summary>
        public const string ESCAPED_ITEM_END = @"}}";


        /// <summary>
        /// One left French brace is assumed to be the beginning of a new format
        /// item.
        /// </summary>
        /// <remarks>
        /// To disable this interpretation, and have the left French brace
        /// appear in the output, use a ESCAPED_ITEM_BEGIN string.
        /// </remarks>
        /// <see cref="ESCAPED_ITEM_BEGIN"/>
        public const char ITEM_BEGIN = '{';


        /// <summary>
        /// One right French brace is assumed to be the end of the format item.
        /// </summary>
        /// <remarks>
        /// To disable this interpretation, and have the right French brace
        /// appear in the output, use a ESCAPED_ITEM_END string.
        /// </remarks>
        /// <see cref="ESCAPED_ITEM_END"/>
        public const char ITEM_END = '}';


        /// <summary>
        /// When one appears within French braces that are not escaped, a comma
        /// indicates an alignment and minimum width are specified. The
        /// documentation of a composite format string refers to this as the
        /// Alignment component.
        /// </summary>
        public const char ITEM_HAS_ALIGNMENT_ATTRIBUTE = ',';


        /// <summary>
        /// Since a negative number signifies left alignment, the presence of a
        /// minus sign immediately following the comma acts as a switch.
        /// </summary>
        public const char ITEM_IS_LEFT_ALIGNED = '-';


        /// <summary>
        /// Since a negative number signifies left alignment, the presence of a
        /// plus sign immediately following the comma acts as a dummy switch.
        /// </summary>
        public const char ITEM_IS_RIGHT_ALIGNED = '+';


        /// <summary>
        /// When one appears within French braces, a colon indicates a format
        /// string, to be applied to the item is specified. The documentation
        /// of a composite format string refers to this as the Format String
        /// component.
        /// </summary>
        public const char ITEM_HAS_FORMAT_STRING_ATTRIBUTE = ':';


        /// <summary>
        /// The FormatStringErrorCount returns this value when the
        /// FormatStringErrors collection is null. The collection remains so
        /// unless at least one error is found in the string.
        /// </summary>
        public const int NO_ERRORS = 0;
        #endregion  // Public Constants


        #region Internal Enumerations
        /// <summary>
        /// The Parse method defines a local State variable and uses it to keep
        /// track of its internal state.
        /// </summary>
        /// <remarks>
        /// While the beginning of a format string is represented in the State
        /// enumeration by NotStarted, no value is reserved for the end, because
        /// the main loop of the parser is a loop that processes the format
        /// string character by character, making such a state moot.
        /// </remarks>
        internal enum State
        {
            /// <summary>
            /// The Parse method has not processed anything. This is the initial
            /// value of the internal state variable, which ends when the first
            /// character is processed.
            /// </summary>
            NotStarted ,

            /// <summary>
            /// The last character seen by the parser is an ordinary literal or
            /// an escaped French brace.
            /// </summary>
            ProcessingLiterals ,

            /// <summary>
            /// The last character seen by the parser is the unescaped opening
            /// French brace that marks the beginning of a new Format Item.
            /// </summary>
            ItemBegin ,

            /// <summary>
            /// The last character seen by the parser is a digit of a Index
            /// component.
            /// </summary>
            IndexComponet ,

            /// <summary>
            /// The last character seen by the parser is the comma that delimits
            /// the Alignment component.
            /// </summary>
            AlignmentBegin ,

            /// <summary>
            /// The last character seen by the parser is a sign, immediately
            /// preceded by a comma, both of which follow a French brace.
            /// </summary>
            AlignmentFlag ,

            /// <summary>
            /// The last character seen by the parser is either a minus sign or
            /// a decimal digit, both preceded by a comma.
            /// </summary>
            AlignmentComponent ,

            /// <summary>
            /// The last character seen by the parser is the colon that delimits
            /// the Format String component.
            /// </summary>
            FormatBegin ,

            /// <summary>
            /// The last character seen by the parser follows a colon and
            /// precedes a closing French brace.
            /// </summary>
            FormatString ,
        };  // State
        #endregion  // Internal Enumerations


        #region Instance Storage
        string _strFormatString;
        FormatItemsCollection _ficoll;
        int _intHighestFormatItemIndex;
        List<FormatStringError> _lstFormatStringErrors;
        #endregion  // Instance Storage


        #region Constructors
        /// <summary>
        /// Create the empty parser.
        /// </summary>
        public FormatStringParser ( )
        { }   // FormatStringParser constructor (1 of 2)


        /// <summary>
        /// Create the parser and set the initial value of its FormatString
        /// property.
        /// </summary>
        /// <param name="pstrFormatString">
        /// Pass a reference to the format control string that you want parsed.
        /// Since the constructor parses the string, the properties are set, and
        /// all that remains for you to do is evaluate them.
        /// </param>
        public FormatStringParser
            ( string pstrFormatString )
        {
            _strFormatString = pstrFormatString;
            Parse ( );
        }   // FormatStringParser constructor (2 of 2)
        #endregion  // Constructors


        #region Properties
        /// <summary>
        /// Gets the collection of FormatItems found in the string.
        /// </summary>
        public FormatItemsCollection FormatItems
        { get { return _ficoll; } }


        /// <summary>
        /// Get or set the value of the FormatString property, from which all
        /// other properties are derived.
        /// </summary>
        public string FormatString
        {
            get { return _strFormatString; }    // public string FormatString getter

            set
            {
                if ( string.IsNullOrEmpty ( value ) )
                {
                    throw new ArgumentNullException (
                        Properties.Resources.PARSER_INPUT_STRING );
                }   // TRUE (exception case) block, if ( string.IsNullOrEmpty ( value ) )
                else if ( value != _strFormatString )
                {
                    _strFormatString = value;
                    Parse ( );
                }   // FALSE (normal case) block, if ( string.IsNullOrEmpty ( value ) )
            }   // public string FormatString property setter
        }   // public string FormatString property


        /// <summary>
        /// Gets the count of errors found in the format string.
        /// </summary>
        /// <remarks>
        /// This convenience property exists because the FormatStringErrors
        /// collection is null unless at least one error was found in the format
        /// string.
        /// </remarks>
        public int FormatStringErrorCount
        {
            get
            {
                if ( _lstFormatStringErrors == null )
                    return NO_ERRORS;
                else
                    return _lstFormatStringErrors.Count;
            }   // public int FormatStringErrorCount Get method
        }   // public int FormatStringErrorCount Property

        /// <summary>
        /// Gets the list of errors, which may be null.
        /// </summary>
        public List<FormatStringError> FormatStringErrors
        { get { return _lstFormatStringErrors; } }


        /// <summary>
        /// Gets the higest format item index number.
        /// </summary>
        /// <remarks>
        /// The array of format items must contains at least this many items,
        /// plus one.
        /// </remarks>
        public int HighestFormatItemIndex { get { return _intHighestFormatItemIndex; } }
        #endregion  // Properties


        #region Private Instance Methods
        private void Parse ( )
        {
            const int GO_BACK_ONE_CHARACTER = 1;

            _intHighestFormatItemIndex = FormatItem.INVALID_INDEX;
            State enmState = State.NotStarted;
            int intCharPos = FormatItem.INVALID_OFFSET;
            FormatItem fiCurrent = null;
            _ficoll = null;
            StringBuilder sbFormatComponent = null;

            if ( _lstFormatStringErrors != null )
            {   // Object is being recycled, and a previous use foud at least one error.
                _lstFormatStringErrors.Clear ( );
            }   // if ( _lstFormatStringErrors != null )

            foreach ( char chrThisCharacter in _strFormatString )
            {
                intCharPos++;

                switch ( enmState )
                {
                    case State.NotStarted:
                        if ( chrThisCharacter == ITEM_BEGIN )
                        {   // The first character is a left French brace.
                            enmState = State.ItemBegin;
                        }   // TRUE block, if ( chrThisCharacter == ITEM_BEGIN )
                        else
                        {   // The first character is something else.
                            enmState = State.ProcessingLiterals;
                        }   // FALSE block, if ( chrThisCharacter == ITEM_BEGIN )

                        break;  // case State.NotStarted

                    case State.ProcessingLiterals:
                        if ( chrThisCharacter == ITEM_BEGIN )
                        {   // Found a left French brace.
                            enmState = State.ItemBegin;
                        }   // if ( chrThisCharacter == ITEM_BEGIN )

                        break;  // case State.ProcessingLiterals

                    case State.ItemBegin:
                        //  ----------------------------------------------------
                        //  If the current character is a left French brace and
                        //  the last was an escape. Disregard both, and resume
                        //  scanning for left braces.
                        //
                        //  The item cannot officially begin until the first
                        //  character immediately following the left brace is
                        //  evaluated, because it is unknown until then whether
                        //  the left brace is escaped, and is to be treated as a
                        //  literal. As a consequence, the reported starting
                        //  position must allow for the fact that the carat is
                        //  already on the next character.
                        //  ----------------------------------------------------

                        if ( chrThisCharacter == ITEM_BEGIN )
                        {   // Left brace is escaped. Revert to scanning.
                            enmState = State.ProcessingLiterals;
                        }   // if ( chrThisCharacter == ITEM_BEGIN )
                        else if ( char.IsDigit ( chrThisCharacter ) )
                        {   // Found a digit. This is expected.
                            enmState = State.IndexComponet;
                            fiCurrent = new FormatItem (
                                ( intCharPos - GO_BACK_ONE_CHARACTER ) ,
                                ( int ) Char.GetNumericValue ( chrThisCharacter ) );
                        }   // else if ( char.IsDigit ( chrThisCharacter ) )
                        else
                        {   // Character is not a digit. Report the error and stop processing this format item.
                            ReportNewError (
                                intCharPos ,
                                chrThisCharacter ,
                                enmState ,
                                Properties.Resources.ERRMSG_NONDIGIT_AFTER_LEFTBRACE );
                            enmState = State.ProcessingLiterals;
                        }   // FALSE block, if ( chrThisCharacter == ITEM_BEGIN ) AND else if ( char.IsDigit ( chrThisCharacter ) )

                        break;  // case State.ItemBegin

                    case State.IndexComponet:
                        if ( chrThisCharacter == ITEM_END )
                        {   // The unmodified single-digit format item is the simplest case.
                            SaveFormatItem (
                                ref enmState ,          // On return, this is always State.ProcessingLiterals.
                                intCharPos ,            // On return, this is always unchanged.
                                ref fiCurrent );        // On return, this is always null.
                        }   // TRUE block, if ( chrThisCharacter == ITEM_END )
                        else if ( char.IsDigit ( chrThisCharacter ) )
                        {   // Item index has more digits.
                            fiCurrent.UpdateIndex ( chrThisCharacter );
                        }   // else if ( char.IsDigit ( chrThisCharacter ) )
                        else if ( chrThisCharacter == ITEM_HAS_ALIGNMENT_ATTRIBUTE )
                        {   // This is a flag, whose only effect is to cause a state change.
                            enmState = State.AlignmentBegin;
                        }   // else if ( chrThisCharacter == ITEM_HAS_ALIGNMENT_ATTRIBUTE )
                        else if ( chrThisCharacter == ITEM_HAS_FORMAT_STRING_ATTRIBUTE )
                        {   // This is a flag, whose only effect is to cause a state change.
                            enmState = State.FormatBegin;
                        }   // else if ( chrThisCharacter == ITEM_HAS_FORMAT_STRING_ATTRIBUTE )
                        else
                        {   // Format item is invalid. Report the error and stop processing this format item.
                            ReportNewError (
                                intCharPos ,
                                chrThisCharacter ,
                                enmState ,
                                Properties.Resources.ERRMSG_NONDIGIT_AFTER_LEFTBRACE );
                            enmState = State.ProcessingLiterals;
                        }   // ELSE block for all of the above.

                        break;  // case State.IndexComponet

                    case State.AlignmentBegin:
                        if ( char.IsDigit ( chrThisCharacter ) )
                        {   // The Alignment component delimiter is most likely to be followed by one or more digits.
                            fiCurrent.UpdateMinimumWidth ( chrThisCharacter );
                            fiCurrent.TextAlignment = FormatItem.Alignment.Right;
                            enmState = State.AlignmentComponent;
                        }   // if ( char.IsDigit ( chrThisCharacter ) )
                        else if ( chrThisCharacter == ITEM_IS_LEFT_ALIGNED )
                        {   // Item is left aligned.
                            fiCurrent.TextAlignment = FormatItem.Alignment.Left;
                            enmState = State.AlignmentFlag;
                        }   // else if ( chrThisCharacter == ITEM_IS_LEFT_ALIGNED )
                        else if ( chrThisCharacter == ITEM_IS_RIGHT_ALIGNED )
                        {   // Item is right aligned.
                            fiCurrent.TextAlignment = FormatItem.Alignment.Right;
                            enmState = State.AlignmentFlag;
                        }   // else if ( chrThisCharacter == ITEM_IS_RIGHT_ALIGNED )
                        else
                        {   // Format item is invalid. Report the error and stop processing this format item.
                            ReportNewError (
                                intCharPos ,
                                chrThisCharacter ,
                                enmState ,
                                Properties.Resources.ERRMSG_NONDIGIT_AFTER_LEFTBRACE );
                            enmState = State.ProcessingLiterals;
                        }   // ELSE block for all of the above.

                        break;  // case State.AlignmentBegin

                    case State.AlignmentFlag:
                        if ( char.IsDigit ( chrThisCharacter ) )
                        {   // The alignment flag (+/-) must be followed by at least one digit.
                            fiCurrent.MinimumWidth = ( int ) char.GetNumericValue ( chrThisCharacter );
                            enmState = State.AlignmentComponent;
                        }   // TRUE (normal outcome) block, if ( char.IsDigit ( chrThisCharacter ) )
                        else
                        {   // Format item is invalid. Report the error and stop processing this format item.
                            ReportNewError (
                                intCharPos ,
                                chrThisCharacter ,
                                enmState ,
                                Properties.Resources.ERRMSG_NONDIGIT_AFTER_LEFTBRACE );
                            enmState = State.ProcessingLiterals;
                        }   // FALSE (exception outcome) block, if ( char.IsDigit ( chrThisCharacter ) )

                        break;  // case State.AlignmentFlag

                    case State.AlignmentComponent:
                        if ( char.IsDigit ( chrThisCharacter ) )
                        {   // Update the MinimumWidth property.
                            fiCurrent.UpdateMinimumWidth ( chrThisCharacter );
                        }   // if ( char.IsDigit ( chrThisCharacter ) )
                        else if ( chrThisCharacter == ITEM_HAS_FORMAT_STRING_ATTRIBUTE )
                        {   // This is a flag, whose only effect is to cause a state change.
                            enmState = State.FormatBegin;
                        }   // else if ( chrThisCharacter == ITEM_HAS_FORMAT_STRING_ATTRIBUTE )
                        else if ( chrThisCharacter == ITEM_END )
                        {   // Add the fully populated item to the collection, change the State flag, and discard the FormatItem.
                            SaveFormatItem (
                                ref enmState ,          // On return, this is always State.ProcessingLiterals.
                                intCharPos ,            // On return, this is always unchanged.
                                ref fiCurrent );        // On return, this is always null.
                        }   // else if ( chrThisCharacter == ITEM_END )
                        else
                        {   // Format item is invalid. Report the error and stop processing this format item.
                            ReportNewError (
                                intCharPos ,
                                chrThisCharacter ,
                                enmState ,
                                Properties.Resources.ERRMSG_NONDIGIT_AFTER_LEFTBRACE );
                            enmState = State.ProcessingLiterals;
                        }   // FALSE (exception outcome) block, if ( char.IsDigit ( chrThisCharacter ) )

                        break;  // case State.AlignmentComponent

                    case State.FormatBegin:
                        if ( chrThisCharacter != ITEM_END )
                        {   // Unless it's a closing French brace, use the current character to start a new StringBuilder.
                            sbFormatComponent = new StringBuilder ( chrThisCharacter.ToString ( ) );
                            enmState = State.FormatString;
                        }   // TRUE (expected outcome) block, if ( chrThisCharacter != ITEM_END )
                        else
                        {   // Format item is invalid. Report the error and stop processing this format item.
                            ReportNewError (
                                intCharPos ,
                                chrThisCharacter ,
                                enmState ,
                                Properties.Resources.ERRMSG_FORMAT_COMPONENT_IS_EMPTY );
                            enmState = State.ProcessingLiterals;
                        }   // FALSE (UNexpected outcome) block, if ( chrThisCharacter != ITEM_END )

                        break;  // case State.FormatBegin

                    case State.FormatString:
                        if ( chrThisCharacter == ITEM_END )
                        {   // The format component is complete. Save it, discard the StringBuilder, and save the completed FormatItem.
                            fiCurrent.ItemFormat = sbFormatComponent.ToString ( );
                            sbFormatComponent = null;
                            SaveFormatItem (
                                ref enmState ,          // On return, this is always State.ProcessingLiterals.
                                intCharPos ,            // On return, this is always unchanged.
                                ref fiCurrent );        // On return, this is always null.
                        }   // TRUE block, if ( chrThisCharacter == ITEM_END )
                        else
                        {   // Append to string.
                            sbFormatComponent.Append ( chrThisCharacter );
                        }   // FALSE block, if ( chrThisCharacter == ITEM_END )

                        break;  // case State.FormatString
                }   // switch ( enmState )
            }   // foreach ( char chrThisCharacter in _strFormatString )

            if ( enmState != State.ProcessingLiterals )
            {
                ReportNewError (
                    intCharPos ,
                    _strFormatString [ _strFormatString.Length ] ,
                    enmState ,
                    Properties.Resources.ERRMSG_LAST_ITEM_INCOMPLETE );
                enmState = State.ProcessingLiterals;
            }   // if ( enmState != State.ProcessingLiterals )
        }   // private void Parse


        /// <summary>
        /// Errors are reported by appending them to a List that is exposed
        /// through the FormatStringErrors property.
        /// </summary>
        /// <param name="pintCharPos">
        /// The current character position is a zero based offset from the start
        /// of the format string.
        /// </param>
        /// <param name="pchrCurrent">
        /// The character found at offset pintCharPos is included as further
        /// hint.
        /// </param>
        /// <param name="penmState">
        /// The current value of the State enumeration tells what the parser is
        /// expecting to find at this point in the string.
        /// </param>
        /// <param name="pstrMessage">
        /// The message offers a human readable explanation of what happened.
        /// </param>
        private void ReportNewError (
            int pintCharPos ,
            char pchrCurrent ,
            State penmState ,
            string pstrMessage )
        {
            const int RESERVE_ROOM_FOR_ONE = 1;

            if ( _lstFormatStringErrors == null )
            {   // List creation is lazy.
                _lstFormatStringErrors = new List<FormatStringError> ( RESERVE_ROOM_FOR_ONE );
            }   // if ( _lstFormatStringErrors == null )

            _lstFormatStringErrors.Add (
                new FormatStringError (
                    pintCharPos ,
                    pchrCurrent ,
                    penmState ,
                    pstrMessage ) );
        }   // private void ReportNewError


        /// <summary>
        /// Although its name, SaveFormatItem, suggests that its sole task is to
        /// save the populated FormatItem into the collection, it performs some
        /// related tasks, each of which is noted in the argument comments.
        /// </summary>
        /// <param name="prenmState">
        /// The State enumeration, which governs the behavior of the parsing
        /// engine, is set to ProcessingLiterals to indicate that processing is
        /// between format items.
        ///
        /// Since this method changes its value, and it is a value type (Any
        /// enumeration is really an integer in handcuffs.), it is passed by
        /// reference, so that the change is reflected in the caller's address
        /// space.
        /// </param>
        /// <param name="pintCharPos">
        /// Since this integer is used, but not changed, it can be safely passed
        /// by value.
        /// </param>
        /// <param name="prfiCurrent">
        /// After the FormatItem is copied into a FormatItemsCollection object,
        /// the locally scoped copy is discarded. Sooner or later, this would
        /// happen on its own, but I like to discard objects as soon as I know
        /// that I am finished with them, especially if their lingering presence
        /// could cause confusion about the current state of affairs.
        /// </param>
        private void SaveFormatItem (
            ref State prenmState ,
            int pintCharPos ,
            ref FormatItem prfiCurrent )
        {
            prfiCurrent.SetRawLength ( pintCharPos );

            if ( _ficoll == null )
            {   // Create collection on first use.
                _ficoll = new FormatItemsCollection ( );
            }   // if ( _ficoll == null )

            if ( prfiCurrent.Index > _intHighestFormatItemIndex )
            {   // This is the ideal place to update the ighestFormatItemIndex.
                _intHighestFormatItemIndex = prfiCurrent.Index;
            }   // if ( prfiCurrent.Index > _intHighestFormatItemIndex )

            _ficoll.Add ( prfiCurrent );
            prfiCurrent = null;
            prenmState = State.ProcessingLiterals;
        }   // private void SaveFormatItem
        #endregion  // Private Instance Methods
    }   // public class FormatStringParser
}   // partial namespace WizardWrx.FormatStringEngine