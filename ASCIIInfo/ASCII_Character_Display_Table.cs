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
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        { InitialzeInstance ( ); }

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
            const int ARRAY_INVALID_INDEX = -1;
            const string ASCII_CODE_NODE_NAME = @"Code";

            //  ----------------------------------------------------------------
            //  The file is named ASCII_Character_Display_Table.xml in the file
            //  system, but the default namespace name, in this case, WizardWrx,
            //  prepends the name when the bound resource is created.
            //  ----------------------------------------------------------------

            const string ASCII_TABLE_SOURCE = @"WizardWrx.ASCII_Character_Display_Table.xml";

            const int NODE_COUNT_CODE_BY_ITSELF = 1;
            const int NODE_COUNT_CODE_WITH_ALTERNATE_OR_COMMENT = 2;
			const int NODE_COUNT_CODE_WITH_ALTERNATE_AND_COMMENT = 3;
            const int NODE_INDEX_OF_CODE = 0;
            const int NODE_INDEX_OF_DETAIL_ITEM_1 = 1;
			const int NODE_INDEX_OF_DETAIL_ITEM_2 = 2;

            const int REAL_ROOT_NODE_INDEX = 2;

            XmlDocument xmlASCIITable = new XmlDocument ( );
            xmlASCIITable.Load ( System.Reflection.Assembly.GetExecutingAssembly ( ).GetManifestResourceStream ( ASCII_TABLE_SOURCE ) );
            XmlNode xmlRealParent = xmlASCIITable.ChildNodes [ REAL_ROOT_NODE_INDEX ];
            int intNextSlot = ARRAY_INVALID_INDEX;
            _asciiTable = new ASCIICharacterDisplayInfo [ xmlRealParent.ChildNodes.Count ];

            foreach ( XmlNode xmlCharacter in xmlRealParent.ChildNodes )
            {
                int intGrandChildren = xmlCharacter.ChildNodes.Count;

                ++intNextSlot;

                XmlNode xmlCode = xmlCharacter.ChildNodes [ NODE_INDEX_OF_CODE ];

                if ( xmlCode.Name != ASCII_CODE_NODE_NAME )
                    throw new InvalidOperationException (
                        Properties.Resources.ERRMSG_INVALID_NODE_IN_ASCII_TABLE +
                        xmlCharacter.InnerXml );

                uint uintNodeCode;

                if ( uint.TryParse ( xmlCode.InnerXml , out uintNodeCode ) )
                {   // It's a valid integer.
                    switch ( intGrandChildren )
                    {
                        case NODE_COUNT_CODE_BY_ITSELF:
                            _asciiTable [ intNextSlot ] = new ASCIICharacterDisplayInfo ( 
                                uintNodeCode );
                            break;  // case NODE_COUNT_CODE_BY_ITSELF

                        case NODE_COUNT_CODE_WITH_ALTERNATE_OR_COMMENT:
							ParseDetailItem ( intNextSlot , xmlCharacter , uintNodeCode , NODE_INDEX_OF_DETAIL_ITEM_1 );
                            break;  // case NODE_COUNT_CODE_WITH_ALTERNATE

						case NODE_COUNT_CODE_WITH_ALTERNATE_AND_COMMENT:
							for ( int intNodeIndex = NODE_INDEX_OF_DETAIL_ITEM_1 ;
								      intNodeIndex < NODE_INDEX_OF_DETAIL_ITEM_2 ;
									  intNodeIndex++ )
								ParseDetailItem (
									intNextSlot ,
									xmlCharacter ,
									uintNodeCode ,
									intNodeIndex );
							break;	// case NODE_COUNT_CODE_WITH_ALTERNATE_AND_COMMENT
                    }   // switch ( intGrandChildren )
                }   // TRUE (expected outcome) block, if ( uint.TryParse ( xmlCode.InnerXml , out uintNodeCode ) )
                else
                {   // If I can't use this node, I am unprepared to trest the rest of them.
                    throw new InvalidOperationException (
                        Properties.Resources.ERRMSG_INVALID_NODE_IN_ASCII_TABLE +
                        xmlCharacter.InnerXml );
                }   // FALSE (UNexpected outcome) block, if ( uint.TryParse ( xmlCode.InnerXml , out uintNodeCode ) )
            }   // foreach ( XmlNode xmlCharacter in xmlRealParent.ChildNodes )
        }   // InitialzeInstance


		/// <summary>
		/// Parse the detail items, of which two are currently defined, into the
		/// properties of a new ASCIICharacterDisplayInfo instance, which can be
		/// fully initialized by any of its three public constructors, depending
		/// on what properties have values.
		/// </summary>
		/// <param name="pintNextSlot">
		/// Argument pintNextSlot is the subscript of the _asciiTable element to
		/// store the current character.
		/// 
		/// The _asciiTable array contains 256 elements, which happens to be the
		/// number of ASCII characters. Since characters are numbered from zero
		/// through 255, the ASCII code is the obvious index for the array.
		/// 
		/// Instance member _asciiTable is ab array of ASCIICharacterDisplayInfo
		/// objects that is initialized with the details read from the XML 
		/// document in which they are stored. The XML document is stored in the
		/// DLL as n custom resource. 
		/// </param>
		/// <param name="pxmlCharacterInfo">
		/// Each character is represented as a XmlNode; this method processes the
		/// detail items on one such node.
		/// </param>
		/// <param name="puintNodeCode">
		/// This is the ASCII code, which the calling routine derives by parsing
		/// its first child node, which is required to store the ASCII code.
		/// 
		/// Since this routine processes an embedded XML document, we can afford
		/// to impose a rigid schema.
		/// </param>
		/// <param name="pintChildRank">
		/// Each invocation of this method processes one child node on the XmlNode
		/// supplied as its pxmlCharacterInfo argument. The calling routine keeps
		/// track of the number of children, and calls it once for each child.
		/// </param>
		private void ParseDetailItem (
			int pintNextSlot ,
			XmlNode pxmlCharacterInfo ,
			uint puintNodeCode ,
			int pintChildRank )
		{
			const string ASCII_DISPLAY_ALTERNATIVE_NODE_NAME = @"Display";
			const string ASCII_DISPLAY_COMMENT = @"Comment";

			XmlNode xmlDetailItem = pxmlCharacterInfo.ChildNodes [ pintChildRank ];

			if ( xmlDetailItem.Name == ASCII_DISPLAY_ALTERNATIVE_NODE_NAME )
				_asciiTable [ pintNextSlot ] = new ASCIICharacterDisplayInfo (
					puintNodeCode ,
					xmlDetailItem.InnerXml );
			else if ( xmlDetailItem.Name == ASCII_DISPLAY_COMMENT )
				_asciiTable [ pintNextSlot ] = new ASCIICharacterDisplayInfo (
					puintNodeCode ,
					null ,
					xmlDetailItem.InnerXml );
			else
				throw new InvalidOperationException (
					Properties.Resources.ERRMSG_INVALID_NODE_IN_ASCII_TABLE +
					pxmlCharacterInfo.InnerXml );
		}	// ParseDetailItem
		#endregion	// Private Instance Methods


		#region Private Instance Storage
		/// <summary>
        /// It all comes down to this little array.
        /// </summary>
        ASCIICharacterDisplayInfo [ ] _asciiTable;
		#endregion	// Private Instance Storage
	}   // class ASCII_Character_Display_Table
}   // partial namespace WizardWrx