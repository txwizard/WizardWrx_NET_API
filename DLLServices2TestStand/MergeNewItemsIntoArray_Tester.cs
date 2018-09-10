/*
    ============================================================================

    Module Name:        MaxStringLength_Tester.cs

    Namespace Name:     DLLServices2TestStand

    Class Name:         MaxStringLength_Tester

    Synopsis:           This module defines a class for testing the ListHelpers
                        class of the WizardWrx.SharedUtl4 class library.

    Remarks:            Since this is tested code, I started the version number
                        at 2.0.

    License:            Copyright (C) 2012-2017, David A. Gray. 
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

    Created:            Monday, 19 November 2012 - Thursday, 22 November 2012

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2012/11/22 2.0     DAG    Application created and tested.
    
	2012/01/05 2.1     DAG    Extend to test the overloaded detail label 
                              generator.

	2014/07/08 2.4     DAG    Upgrade the support libraries to the new series 2
                              set.
 
	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license, and document why I suppressed warning
                              CS0618. Please see item 2 in Remarks (above).

    2017/08/26 7.0     DAG    Move the classes exercised by this class from 
                              WizardWrx.SharedUtl4.dll into WizardWrx.Core.dll,
                              and bring this class into DllServices2TestStand.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using WizardWrx;


namespace DLLServices2TestStand
{
    internal class MergeNewItemsIntoArray_Tester
    {
        #region Private Stuff
		/// <summary>
		/// Tab characters, as they must be entered into resource (.RESX) strings.
		/// </summary>
		public const string EMBEDDED_TAB = "\\t";

		/// <summary>
		/// Tab characters, as they must appear in the string before it can be
		/// used.
		/// </summary>
		public const string OUTPUT_TAB = "\t";
		private static readonly string sr_strReportLabels = Properties.Resources.IDS_MERGENEWITEMSINTOARRAY_LABELS.Replace (
            EMBEDDED_TAB ,
            OUTPUT_TAB );
        private static readonly string sr_strReportDetail = ReportHelpers.DetailTemplateFromLabels ( sr_strReportLabels );
		private static readonly string sr_strDefaultDirName = Environment.CurrentDirectory;
        private static string s_Labels = null;

        private string _strMasterFileName = null;
        private string _strNewItemsFileName = null;
        private string _strOutputFileName = null;
        private List<string> _lstReportDetails = null;
        private string _strReportDetailTemplate = null;
		#endregion	// Private Stuff


		#region Constructors
		private MergeNewItemsIntoArray_Tester ( ) { }   // Hide the default constructor.


        public MergeNewItemsIntoArray_Tester (
            string pstrMasterFileName , 
            string pstrNewItemsFileName ,
            string pstrOutputFileName ,
            List<string> plstReportDetails ,
            string pstrReportDetailTemplate )
        {
            _strMasterFileName = pstrMasterFileName;
            _strNewItemsFileName = pstrNewItemsFileName;
            _strOutputFileName = pstrOutputFileName;
            _lstReportDetails = plstReportDetails;
            _strReportDetailTemplate = pstrReportDetailTemplate;
        }   // Force callers to use this one, which yields a ready-to-use object.
		#endregion	// Constructors


		#region Public ReadOnly Properties
		public static string ReportLabels { get { return sr_strReportLabels; } }
		#endregion	// Public ReadOnly Properties


		#region Public Instance Methods
		public void Go ( )
        {
            Console.WriteLine ( sr_strReportLabels );
            string [ ] astrMasterFile = LoadInputList (
                Properties.Resources.IDS_DESCR_MASTER_FILE ,
                _strMasterFileName );

            string [ ] astrNewItems = LoadInputList (
                Properties.Resources.IDS_DESCR_NEWITEMS_FILE ,
                _strNewItemsFileName );

            if ( astrMasterFile.LongLength > MagicNumbers.ZERO && astrNewItems.LongLength > MagicNumbers.ZERO )
            {
                TestItemSortByString [ ] atiMasterList = LoadList ( astrMasterFile );
                TestItemSortByString [ ] atiNewItems = LoadList ( astrNewItems );
                TestItemSortByString [ ] atiMergedList = WizardWrx.ListHelpers.MergeNewItemsIntoArray (
                    atiMasterList ,
                    atiNewItems );

                string [ ] astrMergedList = new string [ atiMergedList.LongLength + ArrayInfo.ORDINAL_FROM_INDEX ];
                long lngCurrentSlot = ArrayInfo.ARRAY_FIRST_ELEMENT;
                astrMergedList [ lngCurrentSlot ] = s_Labels;

                foreach ( TestItemSortByString tiCurrentItem in atiMergedList )
                {
                    astrMergedList [ ++lngCurrentSlot ] = string.Join (
                        SpecialCharacters.TAB_CHAR.ToString ( ) ,
                        new string [ ]
                        {
                            tiCurrentItem.Key.ToString ( NumericFormats.GENERAL_UC ) ,
                            tiCurrentItem.Data
                        } );
                }   // foreach ( TestItemSortByString tiCurrentItem in atiMergedList )

                File.WriteAllLines ( 
                    _strOutputFileName ,
                    astrMergedList );
                FileInfo fiOutputFile = new FileInfo ( _strOutputFileName );

                //  ------------------------------------------------------------
                //  Stash this for the other test.
                //  ------------------------------------------------------------
                string [ ] astrDetails = new string [ ]
                {
                    Properties.Resources.IDS_DESCR_OUTPUT_FILE ,
                    FileNameToDisplay ( fiOutputFile ) ,
                    SysDateFormatters.ReformatSysDate (
                        fiOutputFile.LastWriteTime ,
                        SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS ) ,
                    fiOutputFile.Length.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) ,
                    atiMergedList.LongLength.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D )
                };

                Console.WriteLine (
                    sr_strReportDetail ,
                    astrDetails );
                _lstReportDetails.Add ( 
                    string.Format (
                        _strReportDetailTemplate ,
                        astrDetails ) );
            }   // TRUE (normal) block, if ( astrMasterFile.LongLength > MagicNumbers.ZERO && astrNewItems.LongLength > MagicNumbers.ZERO )
            else
            {
                Console.WriteLine ( @"Error!" );
            }   // FALSE (exception) block, if ( astrMasterFile.LongLength > MagicNumbers.ZERO && astrNewItems.LongLength > MagicNumbers.ZERO )
        }   // public void Go
		#endregion	// Public Instance Methods


		#region Private Static Methods
		private static string FileNameToDisplay ( FileInfo pfiForThisFile )
        {
            if ( pfiForThisFile.DirectoryName == sr_strDefaultDirName )
                return pfiForThisFile.Name;
            else
                return pfiForThisFile.FullName;
        }   // private static string FileNameToDisplay


        private static string FileNameToDisplay ( string pstrForThisFile )
        {
            if ( pstrForThisFile.Contains ( Path.DirectorySeparatorChar.ToString ( ) ) )
                return pstrForThisFile.Substring (
                    pstrForThisFile.LastIndexOf ( Path.DirectorySeparatorChar )
                        + ArrayInfo.INDEX_FROM_ORDINAL );
            else
                return pstrForThisFile;
        }   // private static string FileNameToDisplay


        private static long LineCountToDisplay ( string [ ] pastrLines )
        {   // Exclude the label row, so that the reported count is the number of detail lines.
            return pastrLines.LongLength - ArrayInfo.INDEX_FROM_ORDINAL;
        }   // private static long LineCountToDisplay


        private string [ ] LoadInputList (
            string pstrLabel ,
            string pstrInputFileName )
        {
            if ( File.Exists ( pstrInputFileName ) )
            {
                FileInfo fiMasterFile = new FileInfo ( pstrInputFileName );
                string [ ] rastrMasterFile = File.ReadAllLines ( pstrInputFileName );
                string [ ] astrReportDetails = new string [ ]
                {
                    pstrLabel ,
                    FileNameToDisplay ( fiMasterFile ) ,
                    SysDateFormatters.ReformatSysDate
                    (
                        fiMasterFile.LastWriteTime ,
                        SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS
                    ) ,
                    fiMasterFile.Length.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) ,
                    LineCountToDisplay ( rastrMasterFile ).ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D )
                };

                _lstReportDetails.Add (
                    string.Format
                        (
                            _strReportDetailTemplate ,
                            astrReportDetails
                        )
                    );
                Console.WriteLine (
                    sr_strReportDetail ,
                    astrReportDetails );

                return rastrMasterFile;
            }   // TRUE (normal) block, if ( File.Exists ( pstrInputFileName ) )
            else
            {
                Console.WriteLine (
                    sr_strReportDetail ,
                    new string [ ]
                    {
                        pstrLabel , 
                        FileNameToDisplay ( pstrInputFileName ) ,
                        Properties.Resources.IDS_MSG_FILE_NOT_FOUND ,
                        string.Empty ,
                        string.Empty
                    } );

                return null;
            }   // FALSE (exception) block, if ( File.Exists ( pstrInputFileName ) )
        }   //private string [ ] LoadInputList


        private static TestItemSortByString [ ] LoadList ( string [ ] pastrInputList )
        {
			const long KEY_FIELD = ArrayInfo.ARRAY_FIRST_ELEMENT;
			const long VALUE_FIELD = KEY_FIELD + ArrayInfo.NEXT_INDEX;
			const long EXPECTED_FIELD_COUNT = VALUE_FIELD + ArrayInfo.NEXT_INDEX;

            // On entry, pastrMasterFile is known to be populated.
            long lngLastItem = pastrInputList.LongLength + ArrayInfo.INDEX_FROM_ORDINAL;
			long lngKey = KEY_FIELD;

			//	----------------------------------------------------------------
			//	Since the label row is separated from the data, the array that
			//	stores the data needs one fewer elements than are in the input
			//	array. Moreover, the code in the ELSE block of the outermost of
			//	the IF blocks within the following FOR loop must adjust the
			//	subscript into which it inserts each element down by one, to
			//	account for the discarded label.
			//	----------------------------------------------------------------

			TestItemSortByString [ ] ratiItems = new TestItemSortByString [ lngLastItem ];
            
            for ( long lngCurrentItem = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                       lngCurrentItem <= lngLastItem ;
                       lngCurrentItem++ )
            {
                string [ ] astrFields = pastrInputList [ lngCurrentItem ].Split ( new char [ ] { SpecialCharacters.TAB_CHAR } );

                if ( lngCurrentItem == ArrayInfo.ARRAY_FIRST_ELEMENT )
                {   // The first element contains labels.
                    if ( s_Labels == null )
                    {   // Save to compare against the next list.
                        if ( astrFields.Length == EXPECTED_FIELD_COUNT )
                        {   // Label appears to be OK.
                            s_Labels = pastrInputList [ lngCurrentItem ];
                        }   // TRUE (normal) block, if ( astrTest.Length == EXPECTED_FIELD_COUNT )
                        else
                        {   // Compose a message and throw an exception.
                            string strMsg = string.Format (
                                Properties.Resources.IDS_BAD_MASTER_LABEL_ROW ,
                                new object [ ]
                                {
                                    EXPECTED_FIELD_COUNT ,
                                    astrFields.Length ,
                                    pastrInputList [ lngCurrentItem ] ,
                                    Environment.NewLine
                                } );
                            throw new Exception ( strMsg );
                        }   // FALSE (exception) block, if ( astrTest.Length == EXPECTED_FIELD_COUNT )
                    }   // TRUE (first time) block, if ( s_Labels == null )
                    else
                    {   // Both must match, or we die.
                        if ( pastrInputList [ lngCurrentItem ] != s_Labels )
                        {   // Compose a message and throw an exception.
                            string strMsg = string.Format (
                                Properties.Resources.IDS_INCONSISTENT_INPUTS ,
                                s_Labels ,
                                pastrInputList [ lngCurrentItem ] ,
                                Environment.NewLine );
                            throw new Exception ( strMsg );
                        }   // if ( pastrInputList [ lngCurrentItem ] != s_Labels )
                    }   // FALSE (subsequent calls) block, if ( s_Labels == null )
                }   // TRUE (one-off) block, if ( lngCurrentItem == ArrayInfo.ARRAY_FIRST_ELEMENT )
                else
                {
                    if ( long.TryParse ( astrFields [ KEY_FIELD ] , out lngKey ) )
                    {
						ratiItems [ lngCurrentItem + ArrayInfo.INDEX_FROM_ORDINAL ] = new TestItemSortByString (
                            lngKey ,
                            astrFields [ VALUE_FIELD ] );
                    }   // TRUE (normal) block, if ( long.TryParse ( astrFields [ KEY_FIELD ] , out lngKey ) )
                    else
                    {
                        string strMsg = string.Format (
                            Properties.Resources.IDS_INVALID_KEY ,
                            astrFields [ KEY_FIELD ] ,
                            pastrInputList [ lngCurrentItem ] );
                        throw new Exception ( strMsg );
                    }   // FALSE (exception) block, if ( long.TryParse ( astrFields [ KEY_FIELD ] , out lngKey ) )
                }   // FALSE block, if ( lngCurrentItem == ArrayInfo.ARRAY_FIRST_ELEMENT )
            }   // for ( long lngCurrentItem = ArrayInfo.ARRAY_FIRST_ELEMENT ; lngCurrentItem <= lngLastItem ; lngCurrentItem++ )

            return ratiItems;
        }   // private static TestItemSortByString [ ] LoadList
		#endregion	// Private Static Methods
	}   // class MergeNewItemsIntoArray_Tester
}   // partial namespace DLLServices2TestStand