/*
    ============================================================================

    Module Name:        MaxStringLength_Tester.cs

    Namespace Name:     DLLServices2TestStand

    Class Name:         MaxStringLength_Tester

    Synopsis:           This class module defines the class that exercises the
                        MaxStringLength methods.

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

    Created:            Monday, 19 November 2012 - Wednesday, 21 November 2012

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2012/11/21 2.0     DAG    Application created and tested.
    
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
    class MaxStringLength_Tester
    {
        enum DataSource
        {
            LinesInTextFile ,
            ArrayOfStrings ,
        }   // DataSource

        enum DetailItem
        {
            DataSource ,
            SourceFQFN ,
            ModDate ,
            FileSize ,
            LineCount ,
            LongestLine
        }   // enum DetailItem

        static readonly DetailItem [ ] s_enmDetailItem =
        {
            DetailItem.DataSource ,
            DetailItem.SourceFQFN ,
            DetailItem.ModDate ,
            DetailItem.FileSize ,
            DetailItem.LineCount , 
            DetailItem.LongestLine
        };  // static readonly DetailItem [ ] s_enmDetailItem

        static readonly string [ ] s_asLabels =
        {
            DLLServices2TestStand.Properties.Resources.IDS_DATA_SOURCE ,
            DLLServices2TestStand.Properties.Resources.IDS_DATAFILE_FQFN ,
            DLLServices2TestStand.Properties.Resources.IDS_DATAFILE_MODDATE ,
            DLLServices2TestStand.Properties.Resources.IDS_DATAFILE_SIZE ,
            DLLServices2TestStand.Properties.Resources.IDS_DATAFILE_LINES ,
            DLLServices2TestStand.Properties.Resources.IDS_LENGTH_OF_LONGEST
        };  // static readonly string [ ] s_asLabels

        static readonly string [ ] s_astrPaddedLbels = MakePaddedLabels ( s_asLabels );

        DataSource _enmDataSource = DataSource.LinesInTextFile;

		string _strTestDataFileName = Program.AbsolutePathStringFromSettings ( Properties.Settings.Default.MaxStringLength_Input );

        string [ ] _astrTestStrings;

        public MaxStringLength_Tester ( ) { }
        public MaxStringLength_Tester ( string pstrTestDataFileName ) { _strTestDataFileName = pstrTestDataFileName; }
        public MaxStringLength_Tester ( string [ ] pastrTestStrings ) { _astrTestStrings = pastrTestStrings; }

        public void Go ( )
        {
            switch ( _enmDataSource )
            {
                case DataSource.ArrayOfStrings:
                    UseInputFromStringArray ( );
                    break;  // case DataSource.ArrayOfStrings

                case DataSource.LinesInTextFile:
                    ReportDetails rpt = UseInputFromFile ( );
                    
                    foreach ( ReportDetail dtl in rpt )
                    {
                        Console.WriteLine ( dtl.FormatDetail ( ) );
                    }   // foreach ( ReportDetail dtl in rpt )
                    break;  // case DataSource.LinesInTextFile

                default:
                    Console.WriteLine (
                        DLLServices2TestStand.Properties.Resources.IDS_UNSUPPORTED_DATASOURCE ,
                        ( int ) _enmDataSource );
                    break;  // Default (else) case
            }   // switch ( _enmDataSource )
        }   // public void Go


        private static string [ ] MakePaddedLabels ( string [ ] pastrRawLabels )
        {
            int intLongest = WizardWrx.ReportHelpers.MaxStringLength ( new List<string> ( pastrRawLabels ) );

            long lngCount = pastrRawLabels.LongLength;   // Used multiple times.
            
            string [ ] rastrPaddedLabels = new string [ lngCount ];

            for ( long lngCurrent = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                       lngCurrent < lngCount ;
                       lngCurrent++ )
            {
                rastrPaddedLabels [ lngCurrent ] = pastrRawLabels [ lngCurrent ].PadRight ( intLongest );
            }   // for ( int lngCurrent = ArrayInfo.ARRAY_FIRST_ELEMENT ; ...

            return rastrPaddedLabels;
        }   // private static string [ ] MakePaddedLabels


        private ReportDetails UseInputFromFile ( )
        {
            ReportDetails rptDetails = new ReportDetails ( s_astrPaddedLbels.Length );
            int intItemIndex = ArrayInfo.ARRAY_INVALID_INDEX;
            string strInputFQFN = Path.Combine (
                Environment.CurrentDirectory ,
                _strTestDataFileName );
            FileInfo fiInputFile = new FileInfo ( strInputFQFN );
            string [ ] astrLinesFromFile = File.ReadAllLines ( strInputFQFN );
            int intLogestLine = WizardWrx.ReportHelpers.MaxStringLength ( new List<string> ( astrLinesFromFile ) );

            foreach ( string strLabel in s_astrPaddedLbels )
            {
                switch ( s_enmDetailItem [ ++intItemIndex ] )
                {
                    case DetailItem.DataSource:
                        rptDetails.Add ( new ReportDetail ( 
                            strLabel ,
                            _enmDataSource.ToString ( ) ) );
                        break;  // case DetailItem.DataSource

                    case DetailItem.SourceFQFN:
                        if ( fiInputFile.DirectoryName == Environment.CurrentDirectory )
                            rptDetails.Add ( new ReportDetail (
                                strLabel ,
                                fiInputFile.Name ) );
                        else
                            rptDetails.Add ( new ReportDetail (
                                strLabel ,
                                fiInputFile.FullName ) );

                        break;  // case DetailItem.SourceFQFN

                    case DetailItem.ModDate:
                        rptDetails.Add ( new ReportDetail (
                            strLabel ,
                            SysDateFormatters.ReformatSysDate (
                                fiInputFile.LastWriteTime , 
                                SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS ) ) );
                        break;  // case DetailItem.ModDate

                    case DetailItem.FileSize:
                        rptDetails.Add ( new ReportDetail (
                            strLabel , 
                            fiInputFile.Length.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) ) );
                        break;  // case DetailItem.FileSize

                    case DetailItem.LineCount:
                        rptDetails.Add ( new ReportDetail (
                            strLabel ,
                            astrLinesFromFile.LongLength.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) ) );
                        break;  // case DetailItem.LineCount

                    case DetailItem.LongestLine:
                        rptDetails.Add ( new ReportDetail (
                            strLabel ,
                            intLogestLine.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) ) );
                        break;  // case DetailItem.LongestLine
                }   // switch ( s_enmDetailItem [ ++intItemIndex ] )                
            }   // foreach ( string strLabel in s_astrPaddedLbels )

            return rptDetails;
        }   // private void UseInputFromFile


        private void UseInputFromStringArray ( )
        {
        }   // private void UseInputFromStringArray ( )
    }   // class MaxStringLength_Tester
}   // partial namespace DLLServices2TestStand