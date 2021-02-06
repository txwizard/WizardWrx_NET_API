/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         ASCII_Character_Display_Table

    File Name:          ASCII_Character_Display_Table.cs

    Synopsis:           This class fills a frequent need for unambiguous
                        identification of characters displayed by computer
                        programs, especially system utilities.

    Remarks:            Some characters that would be nice to render in an
                        unambiguous way are simply not amendable to such.

                        The table is initialized from data stored in an XML
                        document that is persisted as a custom resource that is
                        embedded in the DLL that exposes this class.

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

    Created:            Saturday, 19 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Synopsis
    ---------- ------- --- -----------------------------------------------------
    2014/07/19 1.0     DAG Initial implementation.

    2016/06/12 3.0     DAG 1) Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

                           2) Add a Comment property, to support a like named
                              node in the XML document tree.

    2017/08/04 7.0     DAG Relocated to the constellation of core libraries that
                           began as WizardWrx.DllServices2.dll.

	2021/02/05 8.0     DAG At long last, this class exposes HTML entities and
                           URL encoding strings.
    ============================================================================
*/

using System;

namespace WizardWrx
{
    /// <summary>
    /// Provide read only access to a table of ASCII characters and text to
    /// display for selected special characters.
    /// </summary>
    /// <example>
    /// The ASCII code for a space is 32. ASCIICharacterDisplayInfo[32], for C#,
    /// or ASCIICharacterDisplayInfo(32), for Visual Basic, returns the item for
    /// the space character.
    /// 
    /// Likewise, the ASCII code for a horizontal TAB character is 9. Hence, the
    /// C# expression ASCIICharacterDisplayInfo[9] evaluates to the information
    /// about the TAB character. Likewise, ASCIICharacterDisplayInfo(9) does the
    /// same thing in Visual Basic.
    /// 
    /// The following example comes from production code in the class library
    /// that motivated me to create this library.
    /// 
    ///     ASCIICharacterDisplayInfo [ ] asciiCharTbl = ASCII_Character_Display_Table.GetTheSingleInstance ( ).AllASCIICharacters;
    ///     StringBuilder sbTheBadChar = new StringBuilder ( );
    ///     sbTheBadChar.Append ( asciiCharTbl [ ( uint ) _chrBad ].DisplayText );
    /// 
    /// Obviously, more things go into the message before it is returned to the
    /// calling routine.
    /// </example>
    /// <seealso cref="ASCIICharacterDisplayInfo"/>
    public class ASCII_Character_Display_Table
    {
        #region Singleton Infrastructure
        private static readonly string sro_lwlMaster = @"{0E5626A2-9765-44d1-B2FD-9E38443F5B62}";

        private static ASCII_Character_Display_Table s_theOneAndOnly = null;

        private ASCII_Character_Display_Table ( )
        {
            InitialzeInstance ( );
        }   // private ASCII_Character_Display_Table


        private static void CreateOnFirstUse (
            string plwlMaster ,
            ref ASCII_Character_Display_Table ptsfThis )
        {
            lock ( plwlMaster )
                if ( ptsfThis == null )
                    ptsfThis = new ASCII_Character_Display_Table ( );
        }   // private static void CreateOnFirstUse
        #endregion  // Singleton Infrastructure


        #region Singleton Access Methods
        /// <summary>
        /// Gets a reference to the single ASCII_Character_Display_Table
        /// instance.
        /// </summary>
        /// <returns>
        /// The return value is a reference to the single instance of this class
        /// that is created in response to the first call to this method. Please
        /// see the remarks.
        /// </returns>
        /// <remarks>
        /// The example given under the help topic for this class shows you that
        /// you need not actually allocate storage for the instance, since what
        /// you really need is a copy of the ASCIICharacterDisplayInfo table,
        /// available through the read only AllASCIICharacters property, which
        /// can be assigned directly to an AllASCIICharacters array.
        /// 
        /// To preserve its independence, this class uses the archaic Singleton
        /// implementation, rather than inherit from the abstract base class in
        /// WizardWrx.DllServices2.dll, although I could certainly fix that by
        /// linking the source code into this assembly. Since that creates an
        /// even more awkward dependency, and I don't want to put an actual copy
        /// in this source tree, I'll leave it alone. After all, this class is
        /// not exactly a high traffic property.
        /// </remarks>
        public static ASCII_Character_Display_Table GetTheSingleInstance ( )
        {
            CreateOnFirstUse (
                sro_lwlMaster ,
                ref s_theOneAndOnly );
            return s_theOneAndOnly;
        }   // GetTheSingleInstance
        #endregion  // Singleton Access Methods


        #region Instance Properties
        /// <summary>
        /// Gets the populated ASCIICharacterDisplayInfo array that is the sole
        /// public property of this class, which exists to ensure that exactly
        /// one instance of this table exists.
        /// </summary>
        public ASCIICharacterDisplayInfo [ ] AllASCIICharacters
        {
            get { return _asciiTable; }
        }   // AllASCIICharacters
        #endregion	// Instance Properties


        #region Private Instance Methods
        /// <summary>
        /// Since the class is a singleton, I separated the initializer from the
        /// constructor, as has been my custom.
        /// </summary>
        private void InitialzeInstance ( )
        {
            const int ASCII_CHARACTER_COUNT = 256;
            const string ASCII_TABLE_SOURCE = @"ASCII_Character_Display_Table.TSV";

            //  ----------------------------------------------------------------
            //  The expected row count is one greater than the number of ASCII
            //  characters because the first row in the table contains column
            //  labels.
            //
            //  As a sanity check, the label row is compared against a constant,
            //  LABEL_ROW.
            //  ----------------------------------------------------------------

            const int EXPECTED_ROW_COUNT = ASCII_CHARACTER_COUNT + ArrayInfo.ORDINAL_FROM_INDEX;

            //const int COL_CODE = 0;
            const int COL_CHARTYPE = 1;
            const int COL_SUBTYPE = 2;
            const int COL_CHAR = 3;
            const int COL_DESCRIPTION = 4;
            const int COL_HTML_NAME = 5;
            const int COL_DISPLAY = 6;
            const int COL_COMMENT = 7;
            const int COL_EXPECTED_COUNT = 8;

            const string LABEL_ROW = "Code\tCharType\tSubtype\tCHAR\tDESCRIPTION\tHTML Name\tDisplay\tComment";

            string [ ] astrASCIITable = Readers.LoadTextFileFromCallingAssembly ( ASCII_TABLE_SOURCE );

            if ( astrASCIITable.Length == EXPECTED_ROW_COUNT )
            {
                _asciiTable = new ASCIICharacterDisplayInfo [ ASCII_CHARACTER_COUNT ];

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < EXPECTED_ROW_COUNT ;
                          intJ++ )
                {
                    if ( intJ == ArrayInfo.ARRAY_FIRST_ELEMENT )
                    {
                        if ( astrASCIITable [ ArrayInfo.ARRAY_FIRST_ELEMENT ] != LABEL_ROW )
                        {   // Verify that the label row is as expected.
                            throw new Exception (
                                string.Format (
                                    Properties.Resources.ERRMSG_BAD_LABEL_ROW ,                     // Format Control String: Internal Table Error: The label row is invalid.{2}Expected label row = {0}{2}Actaul label row = {1}
                                    LABEL_ROW ,                                                     // Format Item 0: Expected label row = {0}
                                    astrASCIITable [ ArrayInfo.ARRAY_FIRST_ELEMENT ] ,              // Format Item 1: Actaul label row   = {1}
                                    Environment.NewLine ) );                                        // Format Item 2: Line break between message parts
                        }   // if ( astrASCIITable [ ArrayInfo.ARRAY_FIRST_ELEMENT ] != LABEL_ROW )
                    }   // TRUE (This is the label row.) block, if ( intJ == ArrayInfo.ARRAY_FIRST_ELEMENT )
                    else
                    {
                        string [ ] astrFields = astrASCIITable [ intJ ].Split ( SpecialCharacters.TAB_CHAR );

                        if ( astrFields.Length == COL_EXPECTED_COUNT )
                        {
                            int intCharacterCode = ArrayInfo.IndexFromOrdinal ( intJ );
                            _asciiTable [ intCharacterCode ] = new ASCIICharacterDisplayInfo (
                                ( uint ) intCharacterCode ,                     // uint puintCode
                                ( char ) intCharacterCode ,                     // char pchrCharacter
                                ( ASCIICharacterDisplayInfo.CharacterType ) Enum.Parse (                  // CharacterType penmCharacterType
                                    typeof ( ASCIICharacterDisplayInfo.CharacterType ) ,                  // Type enumType
                                    astrFields [ COL_CHARTYPE ] ,               // string value
                                    true ) ,                                    // bool ignoreCase
                                ( ASCIICharacterDisplayInfo.CharacterSubtype ) Enum.Parse (               // CharacterType penmCharacterType
                                    typeof ( ASCIICharacterDisplayInfo.CharacterSubtype ) ,               // Type enumType
                                    astrFields [ COL_SUBTYPE ] ,                // string value
                                    true ) ,                                    // bool ignoreCase
                                astrFields [ COL_CHAR ] ,                       // string pstrCHAR
                                astrFields [ COL_DESCRIPTION ] ,                // string pstrDescription
                                astrFields [ COL_HTML_NAME ] ,                  // string pstrHTMLName
                                astrFields [ COL_DISPLAY ] ,                    // string pstrAlternateText
                                astrFields [ COL_COMMENT ] );                   // string pstrComment
                        }   // TRUE (anticipated outcome) block, if ( astrFields.Length == COL_EXPECTED_COUNT )
                        else
                        {
                            throw new Exception (
                                string.Format (
                                    Properties.Resources.ERRMSG_BAD_DETAIL_ROW ,// Format Control String: Internal Table Error: Detaill row {0} is invalid.{3}Expected field count = {1}{3}Actaul field count = {2}
                                    intJ ,                                      // Format Item 0: Detaill row {0} is invalid.
                                    COL_EXPECTED_COUNT ,                        // Format Item 1: Expected field count = {1}
                                    astrFields.Length ,                         // Format Item 2: Actaul field count   = {2}
                                    Environment.NewLine ) );                    // Format Item 3: Line Break
                        }   // FALSE (unanticipated outcome) block, if ( astrFields.Length == COL_EXPECTED_COUNT )
                    }   // FALSE (This is a detail row.) block, if ( intJ == ArrayInfo.ARRAY_FIRST_ELEMENT )
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < EXPECTED_ROW_COUNT ; intJ++ )
            }   // TRUE (anticipated outcome) block, if ( astrASCIITable.Length == EXPECTED_ROW_COUNT )
            else
            {
                throw new Exception (
                    string.Format (
                        Properties.Resources.ERRMSG_UNEXPECTED_ROW_COUNT ,      // Format Control String: Internal Table Error: The ASCII Table should contain {0} rows. Instead, it contains {1} rows.
                        EXPECTED_ROW_COUNT ,                                    // Format Item 0: The ASCII Table should contain {0} rows.
                        astrASCIITable.Length ) );                              // Format Item 1: Instead, it contains {1} rows.
            }   // FALSE (unanticipated outcome) block, if ( astrASCIITable.Length == EXPECTED_ROW_COUNT )
        }   // InitialzeInstance
        #endregion // Private Instance Methods


        #region Private Instance Storage
        /// <summary>
        /// It all comes down to this little array.
        /// </summary>
        ASCIICharacterDisplayInfo [ ] _asciiTable;
        #endregion	// Private Instance Storage
    }   // class ASCII_Character_Display_Table
}   // partial namespace WizardWrx