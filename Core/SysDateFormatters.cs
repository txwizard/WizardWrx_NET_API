/*
    ============================================================================

    Namespace Name:     WizardWrx

    Class Name:         SysDateFormatters

    File Name:          SysDateFormatters.cs

    Synopsis:           This class implements my stalwart ReformatSysDate_P6C
                        date formatting algorithm as 100% managed code, and adds
						some wrapper methods that invoke it with the most common
						format strings.

    Remarks:            Though it is exposed at the root level of the WizardWrx
						namespace, this class is implemented by a separate
						assembly, WizardWrx.Core.dll. Keeping the class in the
						root namespace is for backwards compatibility; there are
						too many references that would break if it were moved.

						This class is implicitly sealed. Instances of it cannot
                        be created, and the class cannot be inherited.

                        If you insist on saving your format strings, store them
                        in its sibling class, TimeDisplayFormatter, and call 
                        these methods through it.

    References:         1)  DayOfWeek Enumeration
                            http://msdn.microsoft.com/en-us/library/system.dayofweek.aspx

                        2)  DateTimeFormatInfo.AbbreviatedDayNames Property 
                            http://msdn.microsoft.com/en-us/library/system.globalization.datetimeformatinfo.abbreviateddaynames.aspx

                        3)  DateTimeFormatInfo Class
                            http://msdn.microsoft.com/en-us/library/system.globalization.datetimeformatinfo.aspx

    Author:             David A. Gray

    License:            Copyright (C) 2012-2021, David A. Gray. 
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
    2012/02/26 1.7     DAG    This class makes its first appearance.

    2012/03/03 1.8     DAG    Comment the public constants.

    2012/09/03 2.6     DAG    Complete the constellation by adding ReformatNow
                              and ReformatUtcNow.

    2013/02/17 2.96    DAG    Add RFD_MM_DD_YY, which defines the archaic short
                              (eight character) date format. The archaic format
                              is required by ZZRdInts, and probably others,
                              sooner or later.

                              For unrelated reasons, this version of the 
                              assembly gets a strong name.
 
    2013/11/24 3.1     DAG    1) Add the following overlooked, but often required
                                 compact date formats.

                                    ----------------------------------
                                    Mask            Symbol
                                    --------------- ------------------
                                    YYYYMMDD_hhmmss RFDYYYYMMDD_HHMMSS
                                    YYYYMMDD        RFDYYYYMMDD
                                    HHMMSS          RFDHHMMSS
                                    ----------------------------------

                              2) Correct a couple of typographical errors in the
                                 internal documentation that I discovered while
                                 scanning for the above-cited formatting masks.

                              The executable code is unaffected.

    2014/06/07 5.0     DAG    Major namespace reorganization.

    2014/06/23 5.1     DAG    Documentation corrections.

    2014/09/14 5.2     DAG    Copy into to WizardWrx.DLLServices2 class
                              library, which is built against the Microsoft .NET
                              Framework, version 3.5, to take advantage of its
                              new TimeZoneInfo class, required by the new Util
                              class, which brings together assorted constants
                              and methods developed for various applications.

	2015/06/06 5.4     DAG    Break completely free from WizardWrx.SharedUtl2.

	2015/06/20 5.5     DAG    Relocate to WizardWrx.DLLServices2 namespace and
                              class library.
 
    2016/04/10 6.0     DAG    Scan for typographical errors flagged by the
							  spelling checker add-in, and correct what I find,
                              and update the formatting and marking of blocks.

	2017/03/03 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

							  In addition to being moved into another DLL, this
							  class acquires three wrappers that once belonged
							  to the DisplayFormats class, which was moved into
							  WizazardWrx.Common.dll, where it retains only the
							  constants. This relocation breaks a circular DLL
                              dependency that would otherwise exist between the
							  two libraries.

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.

    2021/10/13 8.0.252 DAG    Add an optional Boolean flag to GetDisplayTimeZone
                              that causes it to leverage the new TimeZoneInfo
                              extension methods defined in sibling static class
                              TimeZoneInfoExtensions to render abbreviated time
                              zone names.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

/*  Added by DAG */

using System.Globalization;


namespace WizardWrx
{
    /// <summary>
    /// This class implements my stalwart date formatter, ReformatSysDateP6C,
    /// which I created initially as a Windows Interface Language (WIL, 
    /// a. k. a. WinBatch) library function, Reformat_Date_YmdHms_P6C, in
    /// October 2001, although its roots go back much further in my WIL script
    /// development.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <seealso cref="DisplayFormats"/>
	/// <seealso cref="Core.TimeDisplayFormatter"/>
    public static class SysDateFormatters
    {
        #region Public constants define the substitution tokens and a selection of useful format strings.
        /// <summary>
        /// The strings in this array are the substitution tokens supported by
        /// the date formatters in this class.
        /// </summary>
        public static readonly string [ ] RSD_TOKENS =
        {
            @"^MMMM" ,
            @"^MMM" ,
            @"^MM" , 
            @"^DD" ,
            @"^D" ,
            @"^YYYY" ,
            @"^YY" ,
            @"^hh" ,
            @"^h" ,
            @"^mm" ,
            @"^ss" ,
            @"^WWWW" ,
            @"^WWW" ,
            @"^WW" ,
            @"^ttt"
        };  // public static readonly string [ ] RSD_TOKENS

        /// <summary>
        /// Apply the following format to a date: YYYY/MM/DD
        /// 
        /// With respect to the date only, this format confirms to the ISO 8601
        /// standard for time representation.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_YYYY_MM_DD = @"^YYYY/^MM/^DD";

        /// <summary>
        /// Apply the following format to a date: MM/DD/YY
        /// 
        /// This is the standard short format used in the USA.
        /// 
        /// Only the date is returned, including only the year of century, and
        /// the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_MM_DD_YY = @"^MM/^DD/^YY";

        /// <summary>
        /// Apply the following format to a date: MM/DD/YYYY
        /// 
        /// This is the standard format used in the USA.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_MM_DD_YYYY = @"^MM/^DD/^YYYY";

        /// <summary>
        /// Apply the following format to a date: DD/MM/YYYY
        /// 
        /// This is the standard format used in most of the English speaking
        /// world, by all military organizations of which I am aware, Europeans,
        /// and others who take their lead from any of the above groups.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_DD_MM_YYYY = @"^DD/^MM/^YYYY";

        /// <summary>
        /// Apply the following format to a time: hh:mm
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This is a standard format used in most of the English speaking
        /// world, by all military organizations of which I am aware, Europeans,
        /// and others who take their lead from any of the above groups.
        /// 
        /// Only the time is returned, and the hour and minute have leading
        /// zeros if either is less than 10.
        /// </summary>
        public const string RFD_HH_MM = @"^hh:^mm";

        /// <summary>
        /// Apply the following format to a time: hh:mm:ss
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This is a standard format used in most of the English speaking
        /// world, by all military organizations of which I am aware, Europeans,
        /// and others who take their lead from any of the above groups.
        /// 
        /// Only the time is returned, and the hour, minute, and second have 
        /// leading zeros if any of them is less than 10.
        /// </summary>
        public const string RFD_HH_MM_SS = @"^hh:^mm:^ss";

        /// <summary>
        /// Apply the following format to a time: hh:mm:ss.ttt
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// The final token, ttt, is the milliseconds portion of the time,
        /// which is reported with leading zeros.
        /// 
        /// This is an extension of a standard format used in most of the
        /// English speaking world, by all military organizations of which I am
        /// aware, Europeans, and others who take their lead from any of the
        /// above groups.
        /// 
        /// Only the time is returned, and the hour, minute, and second have 
        /// leading zeros if any of them is less than 10.
        /// </summary>
        public const string RFD_HH_MM_SS_TTT = @"^hh:^mm:^ss.^ttt";

        /// <summary>
        /// Apply the following format to a date and time: YYYY/MM/DD hh:mm:ss
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This format conforms fully to the ISO 8601 standard for time
        /// representation.
        /// 
        /// The month, day, hour, minute, and second have leading zeros if any
        /// of them is less than 10.
        /// </summary>
        public const string RFD_YYYY_MM_DD_HH_MM_SS = @"^YYYY/^MM/^DD ^hh:^mm:^ss";

        /// <summary>
        /// Apply the following format to a date and time: YYYY/MM/DD hh:mm:ss.ttt
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This format conforms fully to the ISO 8601 standard for time
        /// representation.
        /// 
        /// The final token, ttt, is the milliseconds portion of the time,
        /// which is reported with leading zeros.
        /// 
        /// The month, day, hour, minute, and second have leading zeros if any
        /// of them is less than 10.
        /// </summary>
        public const string RFD_YYYY_MM_DD_HH_MM_SS_TTT = @"^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt";

        /// <summary>
        /// Apply the following format to a date: WWW DD/MM/YYYY
        /// 
        /// The first token, WWW, represents the three letter abbreviation of
        /// the weekday name, which is derived from the regional settings in the
        /// Windows Control Panel. The returned string conforms to the settings
        /// in the UICulture of the calling thread.
        /// 
        /// This is the standard format used in most of the English speaking
        /// world, by all military organizations of which I am aware, Europeans,
        /// and others who take their lead from any of the above groups.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_WWW_DD_MM_YYYY = @"^WWW ^DD/^MM/^YYYY";

        /// <summary>
        /// Apply the following format to a date: WWW DD/MM/YYYY
        /// 
        /// The first token, WWW, represents the three letter abbreviation of
        /// the weekday name, which is derived from the regional settings in the
        /// Windows Control Panel. The returned string conforms to the settings
        /// in the UICulture of the calling thread.
        /// 
        /// This is the standard format used in the USA.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_WWW_MM_DD_YYYY = @"^WWW ^MM/^DD/^YYYY";

        /// <summary>
        /// Apply the following format to a date: WW DD/MM/YYYY
        /// 
        /// The first token, WW, represents enough of the three letter weekday
        /// name abbreviation, which is derived from the regional settings in
        /// the Windows Control Panel, to uniquely identify the weekday. The
        /// returned string conforms to the settings in the UICulture of the
        /// calling thread.
        /// 
        /// This is the standard format used in most of the English speaking
        /// world, by all military organizations of which I am aware, Europeans,
        /// and others who take their lead from any of the above groups.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_WW_DD_MM_YYYY = @"^WW ^DD/^MM/^YYYY";

        /// <summary>
        /// Apply the following format to a date: WW DD/MM/YYYY
        /// 
        /// The first token, WW, represents enough of the three letter weekday
        /// name abbreviation, which is derived from the regional settings in
        /// the Windows Control Panel, to uniquely identify the weekday. The
        /// returned string conforms to the settings in the UICulture of the
        /// calling thread.
        /// 
        /// This is the standard format used in the USA.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_WW_MM_DD_YYYY = @"^WW ^MM/^DD/^YYYY";

        /// <summary>
        /// Apply the following format to a date: WWWW DD/MM/YYYY
        /// 
        /// The first token, WWWW, represents full name of the weekday, which is
        /// derived from the regional settings in the Windows Control Panel. The
        /// returned string conforms to the settings in the UICulture of the
        /// calling thread.
        /// 
        /// This is the standard format used in most of the English speaking
        /// world, by all military organizations of which I am aware, Europeans,
        /// and others who take their lead from any of the above groups.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_WWWW_DD_MM_YYYY = @"^WWWW, ^DD/^MM/^YYYY";

        /// <summary>
        /// Apply the following format to a date: WWWW DD/MM/YYYY
        /// 
        /// The first token, WWWW, represents full name of the weekday, which is
        /// derived from the regional settings in the Windows Control Panel. The
        /// returned string conforms to the settings in the UICulture of the
        /// calling thread.
        /// 
        /// This is the standard format used in the USA.
        /// 
        /// Only the date is returned, all four digits of the year are included,
        /// and the month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFD_WWWW_MM_DD_YYYY = @"^WWWW, ^MM/^DD/^YYYY";

        /// <summary>
        /// Apply the following format to a date and time: YYYYMMDD_hhmmss
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This format conforms fully to the ISO 8601 standard for time
        /// representation.
        /// 
        /// The month, day, hour, minute, and second have leading zeros if any
        /// of them is less than 10.
        /// </summary>
        public const string RFDYYYYMMDD_HHMMSS = @"^YYYY^MM^DD_^hh^mm^ss";

        /// <summary>
        /// Apply the following format to a date and time: YYYYMMDD
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This format conforms fully to the ISO 8601 standard for time
        /// representation.
        /// 
        /// The month and day have leading zeros if either is less than 10.
        /// </summary>
        public const string RFDYYYYMMDD = @"^YYYY^MM^DD";

        /// <summary>
        /// Apply the following format to a date and time: hhmmss
        /// 
        /// The returned string represents the hours on a 24 hour clock.
        /// 
        /// At present, 12 hour (AM/PM) representation is unsupported.
        /// 
        /// This format conforms fully to the ISO 8601 standard for time
        /// representation.
        /// 
        /// The hour, minute, and second have leading zeros if any of them is
        /// less than 10.
        /// </summary>

        public const string RFDHHMMSS = @"^hh^mm^ss";
		/// <summary>
		/// I use this with my SysDateFormatters class to format a date (sans
		/// time) so that it prints as YYYY/MM/DD.
		/// 
		/// IMPORTANT: This string specifically targets the methods in the
		/// SysDateFormatters class. SysDateFormatters strings are incompatible
		/// with ToString.
		/// </summary>
		/// <example>
		/// 2014/09/04
		/// </example>
		public const string STANDARD_DISPLAY_DATE_FORMAT = SysDateFormatters.RFD_YYYY_MM_DD;

		/// <summary>
		/// I use this with my SysDateFormatters class to format a date and time
		/// so that it prints as YYYY/MM/DD HH:MM:SS.
		/// 
		/// IMPORTANT: This string specifically targets the methods in the
		/// SysDateFormatters class. SysDateFormatters strings are incompatible
		/// with ToString.
		/// </summary>
		/// <example>
		/// 2014/09/04 16:17:30
		/// </example>
		public const string STANDARD_DISPLAY_DATE_TIME_FORMAT = SysDateFormatters.RFD_YYYY_MM_DD_HH_MM_SS;

		/// <summary>
		/// I use this with my SysDateFormatters class to format a time (sans
		/// date) so that it prints as HH:MM:SS.
		/// 
		/// IMPORTANT: This string specifically targets the methods in the
		/// SysDateFormatters class. SysDateFormatters strings are incompatible
		/// with ToString.
		/// </summary>
		/// <example>
		/// 16:17:30
		/// </example>
		public const string STANDARD_DISPLAY_TIME_FORMAT = SysDateFormatters.RFD_HH_MM_SS;

        /// <summary>
        /// Specify this constant as the pfAbbreviateTZName argument value to
        /// method GetDisplayTimeZone to explicitly elicit the default (legacy)
        /// behavior of returning the system-defined (spelled out) time zone
        /// name.
        /// </summary>
        public const bool TZ_NAME_FULL = false;

        /// Specify this constant as the pfAbbreviateTZName argument value to
        /// method GetDisplayTimeZone to elicit the new behavior of returning
        /// the abbreviated time zone name generated from the system-defined
        /// (spelled out) time zone name.
        public const bool TZ_NAME_ABBR = true;
        #endregion	// Public constants define the substitution tokens and a selection of useful format strings.


		#region Although static, this class requires several tables that are initialized at compile time.
		enum FormattingAlgorithm
        {
            ApplyFormatToDateTime ,
            TwoLetterWeekday
        }   // FormattingAlgorithm

        static readonly FormattingAlgorithm [ ] RSD_ALGORITHM =
        {
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.ApplyFormatToDateTime ,
            FormattingAlgorithm.TwoLetterWeekday ,
            FormattingAlgorithm.ApplyFormatToDateTime
        };  // FormattingAlgorithm [ ] RSD_ALGORITHM =


        static readonly string [ ] RSD_BCL_FORMATS =
        {
            @"MMMM" ,
            @"MMM" ,
            @"MM" ,
            @"dd" ,
            @"%d" ,         // See http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx#UsingSingleSpecifiers.
            @"yyyy" ,
            @"yy" ,
            @"HH" ,
            @"%H" ,         // See http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx#UsingSingleSpecifiers.
            @"mm" ,
            @"ss" ,
            @"dddd" ,
            @"ddd" ,
            @"^WW" ,        // There is no custom format string that corresponds to ^WW.
            @"fff"
        };  // string [ ] RSD_BCL_FORMATS
		#endregion	// Although static, this class requires several tables that are initialized at compile time.


		#region In addition to the hard coded tables, this class uses another table, and a scalar which holds the size of the three hard coded tables, which are initialized at run time by the static constructor.
		static readonly string [ ] RSD_WEEKDAY_2LTRS;

        static readonly int _intNDefinedTokens;
		#endregion	// In addition to the hard coded tables, this class uses another table, and a scalar which holds the size of the three hard coded tables, which are initialized at run time by the static constructor.


		#region The static constructor initializes the two-letter weekday table based on the current UI culture.
		static SysDateFormatters ( )
        {
            const int DAYS_IN_WEEK = 7;
            const int ONE_LETTER = ArrayInfo.NEXT_INDEX;
            const int SECOND_LETTER_INDEX = ArrayInfo.NEXT_INDEX;
            
            const string TWO_LETTERS = @"{0}{1}";
            const string UNBALANCED_ERRMSG = @"The static SysDateFormatters constructor has detected unbalanced string arrays.{2}Length of array RSD_TOKENS = {0}{2}Length of array RSD_BCL_FORMATS = {1}";

            //  ----------------------------------------------------------------
            //  Sanity check the sizes of the three arrays that were initialized
            //  at compile time. All three must be the same size.
            //  ----------------------------------------------------------------

            int intRSDTokenArraySize = RSD_TOKENS.Length;
            int intBCLFormatArraySize = RSD_BCL_FORMATS.Length;
            int intAlgorithmArraySize = RSD_ALGORITHM.Length;

            if ( intRSDTokenArraySize == intBCLFormatArraySize )
                if ( intAlgorithmArraySize == intRSDTokenArraySize )
                    _intNDefinedTokens = intRSDTokenArraySize;
                else
                    throw new Exception ( string.Format (
                        UNBALANCED_ERRMSG ,
                        intRSDTokenArraySize ,
                        intBCLFormatArraySize ,
                        Environment.NewLine ) );

            //  ----------------------------------------------------------------
            //  The fourth array, RSD_WEEKDAY_2LTRS, is initialized by this
            //  constructor, because the required values are unknown until this
            //  constructor executes.
            //  ----------------------------------------------------------------

            RSD_WEEKDAY_2LTRS = new string [ DAYS_IN_WEEK ];

            string [ ] astrAbbrWdayNames = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames;
            char [ ] achrFirstLetter = new char [ DAYS_IN_WEEK ];
            Dictionary<char , int> dctTimesUsed = new Dictionary<char , int> ( DAYS_IN_WEEK );
            int intDayIndex = ArrayInfo.ARRAY_INVALID_INDEX;

            //  ----------------------------------------------------------------
            //  Isolate and count the first letter.
            //  ----------------------------------------------------------------

            foreach ( string strWeekdayName in astrAbbrWdayNames )
            {
                char [ ] achrFirstLtrOfWday = strWeekdayName.ToCharArray (
                    ArrayInfo.ARRAY_FIRST_ELEMENT ,
                    ONE_LETTER );

                achrFirstLetter [ ++intDayIndex ] = achrFirstLtrOfWday [ ArrayInfo.ARRAY_FIRST_ELEMENT ];

                if ( dctTimesUsed.ContainsKey ( achrFirstLetter [ intDayIndex ] ) )
                {   // Letter already used at least once.
                    ++dctTimesUsed [ achrFirstLetter [ intDayIndex ] ];
                }   // TRUE block, if ( dctTimesUsed.ContainsKey ( achrFirstLetter [ intDayIndex ] ) )
                else
                {   // First use of this letter.
                    dctTimesUsed [ achrFirstLetter [ intDayIndex ] ] = ONE_LETTER;
                }   // FALSE block, if ( dctTimesUsed.ContainsKey ( achrFirstLetter [ intDayIndex ] ) )
            }   //  foreach ( string strWeekdayName in astrAbbrWdayNames )

            //  ----------------------------------------------------------------
            //  Assemble the two-letter abbreviations, and load them into the
            //  string array. If one letter is enough to uniquely identify a day
            //  of the week, confine the abbreviation to the initial letter. If
            //  a second letter is required, append the second character of the
            //  weekday name.
            //  ----------------------------------------------------------------

            intDayIndex = ArrayInfo.ARRAY_INVALID_INDEX;    // Reset.

            foreach ( string strWeekdayName in astrAbbrWdayNames )
            {
                char chrFirstLetter = achrFirstLetter [ ++intDayIndex ];

                if ( dctTimesUsed [ chrFirstLetter ] == ONE_LETTER )
                {   // Letter is used once only, so it uniquely identifies the weekday.
                    RSD_WEEKDAY_2LTRS [ intDayIndex ] = chrFirstLetter.ToString ( );
                }   // TRUE block, if ( dctTimesUsed [ chrFirstLetter ] == ONE_LETTER )
                else
                {   // Letter is used more than once. Get the second letter.
                    char [ ] achrSecondLetter = strWeekdayName.ToCharArray (
                        SECOND_LETTER_INDEX ,
                        ONE_LETTER );
                    RSD_WEEKDAY_2LTRS [ intDayIndex ] = string.Format (
                        TWO_LETTERS ,
                        chrFirstLetter ,
                        achrSecondLetter [ ArrayInfo.ARRAY_FIRST_ELEMENT ] );
                }   // FALSE block, if ( dctTimesUsed [ chrFirstLetter ] == ONE_LETTER )
            }   // foreach ( string strWeekdayName in astrAbbrWdayNames )
        }   // static SysDateFormatters constructor
		#endregion	// The static constructor initializes the two-letter weekday table based on the current UI culture.


		#region Public static methods are the reason this class exists.
		/// <summary>
		/// Use my standard format string for displaying date stamps in
		/// reports, to format a DateTime structure.
		/// </summary>
		/// <param name="pdtmTestDate">
		/// Specify the populated DateTime to be formatted. Since only the date
		/// goes into the format, the time component MAY be uninitialized.
		/// </param>
		/// <returns>
		/// The return value is a string representation of the date and time,
		/// rendered according to constant STANDARD_DISPLAY_TIME_FORMAT.
		/// </returns>
		public static string FormatDateForShow ( DateTime pdtmTestDate )
		{
			return ReformatSysDate (
				pdtmTestDate ,
				STANDARD_DISPLAY_DATE_FORMAT );
		}   // public static string FormatDateForShow


		/// <summary>
		/// Use my standard format string for displaying date/time stamps in
		/// reports, to format a DateTime structure.
		/// </summary>
		/// <param name="pdtmTestDate">
		/// Specify the populated DateTime to be formatted. Since the date and
		/// time go into the output string, the entire structure must be
		/// initialized.
		/// </param>
		/// <returns>
		/// The return value is a string representation of the date and time,
		/// rendered according to constant STANDARD_DISPLAY_DATE_TIME_FORMAT.
		/// </returns>
		public static string FormatDateTimeForShow ( DateTime pdtmTestDate )
		{
			return ReformatSysDate (
				pdtmTestDate ,
				STANDARD_DISPLAY_DATE_TIME_FORMAT );
		}   // public static string FormatDateTimeForShow


		/// <summary>
		/// Use my standard format string for displaying time stamps in reports,
		/// to format a DateTime structure.
		/// </summary>
		/// <param name="pdtmTestDate">
		/// Specify the populated DateTime to be formatted. Since only the time
		/// goes into the format, the date component MAY be uninitialized.
		/// </param>
		/// <returns>
		/// The return value is a string representation of the date and time,
		/// rendered according to constant STANDARD_DISPLAY_TIME_FORMAT.
		/// </returns>
		public static string FormatTimeForShow ( DateTime pdtmTestDate )
		{
			return ReformatSysDate (
				pdtmTestDate ,
				STANDARD_DISPLAY_TIME_FORMAT );
		}   // public static string FormatTimeForShow


        /// <summary>
        /// Given a DateTime and a system time zone ID string, return the
        /// appropriate text to display, depending on whether the specified time
        /// is standard or Daylight Saving Time.
        /// </summary>
        /// <param name="pdtmTestDate">
        /// Specify the Syatem.DateTime for which the appropriate time zone
        /// string is required. Both DateTime.MinValue and DateTime.MaxValue are
        /// invalid; specifying either elicits the empty string.
        /// </param>
        /// <param name="pstrTimeZoneID">
        /// Specify a valid time zone ID string. Please see the Remarks.
        /// </param>
        /// <param name="pfAbbreviateTZName">
        /// <para>
        /// Specify TZ_NAME_ABBR (Boolean True) to cause the method to return
        /// the abbreviated time zone name that it constructs from the full
        /// (spelled out) name that is the system default.
        /// </para>
        /// <para>
        /// You may also specify TZ_NAME_FULL to explicitly cause the full time
        /// zone name to be returned.
        /// </para>
        /// <para>
        /// If this argument is omitted, the full time zone name is returned, so
        /// that this method is backwards compatible.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the appropriate string
        /// to display for the given time. Otherwise, the empty string is
        /// returned or one of several exceptions is thrown, the most likely of
        /// which is a TimeZoneNotFoundException, which is thrown when the
        /// specified time zone ID string is invalid.
        /// </returns>
        /// <remarks>
        /// if in doubt, use TimeZoneInfo.GetSystemTimeZones to enumerate the
        /// time zones installed on the local machine. Invalid time zone strings
        /// always give rise to one of a number of exceptions, all of which are
        /// fully described in the documentation of a companion function,
        /// GetSystemTimeZoneInfo which this routine uses to get the time zone
        /// information that it needs.
        /// </remarks>
        /// <see cref="GetSystemTimeZoneInfo"/>
        public static string GetDisplayTimeZone (
            DateTime pdtmTestDate ,
            string pstrTimeZoneID ,
            bool pfAbbreviateTZName = false )
        {
            if ( pdtmTestDate == DateTime.MinValue || pdtmTestDate == DateTime.MaxValue )
            {   // Insufficient data available
                return SpecialStrings.EMPTY_STRING;
            }   // TRUE (degenerate case) block, if ( pdtmTestDate == DateTime.MinValue || pdtmTestDate == DateTime.MaxValue || string.IsNullOrEmpty(pstrTimeZoneID) )
            else
            {
                TimeZoneInfo tzinfo = GetSystemTimeZoneInfo ( pstrTimeZoneID );

                if ( pfAbbreviateTZName )
                {   // Use the new TimeZoneInfo extension methods to render abbreviated names.
                    return tzinfo.IsDaylightSavingTime ( pdtmTestDate ) ?
                        tzinfo.AbbreviateDaylightName ( ) :
                        tzinfo.AbbreviatedStandardName ( );
                }   // TRUE (Render abbreviated time zone names.) block, if ( pfAbbreviateTZName )
                else
                {   // Render the default time zone display names, which are spelled out. This is the legacy behavior.
                    return tzinfo.IsDaylightSavingTime ( pdtmTestDate ) ?
                        tzinfo.DaylightName :
                        tzinfo.StandardName;
                }   // TRUE (Render fully spelled out time zone names.) block, if ( pfAbbreviateTZName )
            }   // FALSE (desired outcome) block, if ( pdtmTestDate == DateTime.MinValue || pdtmTestDate == DateTime.MaxValue || string.IsNullOrEmpty(pstrTimeZoneID) )
        }   // public static string GetDisplayTimeZone


        /// <summary>
        /// Given a system time zone ID string, return the corresponding
        /// TimeZoneInfo object if the specified time zone is defined on the
        /// local system.
        /// </summary>
        /// <param name="pstrTimeZoneID">
        /// Specify a valid time zone ID string. There are two special IDs,
        /// LocalTime and UTC, both of which are accessible through static
        /// properties on the TimeZoneInfo class. Although you could use the ID
        /// properties with this method, the most efficient way to handle these
        /// special cases is by reference to the Local property for LocalTime
        /// and the UTC property for UTC time. (This method could take the same
        /// shortcut, but I decided that it wasn't worth the extra code and
        /// testing.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a TimeZoneInfo object,
        /// populated from the Windows Registry. Otherwise, one of the
        /// exceptions listed and described below is thrown.
        /// </returns>
        /// <exception cref="OutOfMemoryException">
        /// You should restart Windows if this happens.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Contact the author of the program. This is something that he or she
        /// must address.
        /// </exception>
        /// <exception cref="TimeZoneNotFoundException">
        /// Contact the author of the program. This is something that he or she
        /// must address.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Contact your system administrator to inquire about why your program
        /// is forbidden to read the regional settings from the Windows
        /// Registry.
        /// </exception>
        /// <exception cref="InvalidTimeZoneException">
        /// Contact your system support group. A corrupted Windows Registry is a
        /// rare, but serious matter.
        /// </exception>
        /// <exception cref="Exception">
        /// Start with your system support group, who may need to request the
        /// assistance of the author of the program.
        /// </exception>
        /// <remarks>
        /// if in doubt, use TimeZoneInfo.GetSystemTimeZones to enumerate the
        /// time zones installed on the local machine.
        /// </remarks>
        public static TimeZoneInfo GetSystemTimeZoneInfo ( string pstrTimeZoneID )
        {
            const string ARGNAME = @"pstrTimeZoneID";

            if ( string.IsNullOrEmpty ( pstrTimeZoneID ) )
            {   // Insufficient data available
                throw new ArgumentNullException ( ARGNAME );
            }   // TRUE (undesired outcome) block, if ( pdtmTestDate == DateTime.MinValue || pdtmTestDate == DateTime.MaxValue || string.IsNullOrEmpty(pstrTimeZoneID) )
            else
            {
                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById ( pstrTimeZoneID );
                }
                catch ( OutOfMemoryException exNoMem )
                {
                    throw new Exception (
						WizardWrx.Core.Properties.Resources.ERRMSG_NO_MEMORY ,
                        exNoMem );
                }
                catch ( ArgumentNullException exNullID )
                {
                    throw new Exception (
						WizardWrx.Core.Properties.Resources.ERRMSG_NULL_TZ_ID ,
                        exNullID );
                }
                catch ( TimeZoneNotFoundException exTZNotFound )
                {
                    throw new Exception (
                        string.Format (
							WizardWrx.Core.Properties.Resources.ERRMSG_TZ_NOT_FOUND ,
                            pstrTimeZoneID ,
                            Environment.NewLine ) ,
                        exTZNotFound );
                }
                catch ( System.Security.SecurityException exSecurity )
                {
                    throw new Exception (
						WizardWrx.Core.Properties.Resources.ERRMSG_SECURITY ,
                        exSecurity );
                }
                catch ( InvalidTimeZoneException exInvTZInfo )
                {
                    throw new Exception (
                        string.Format (
							WizardWrx.Core.Properties.Resources.ERRMSG_INV_TZINFO ,
                            pstrTimeZoneID ,
                            Environment.NewLine ) ,
                        exInvTZInfo );
                }
                catch ( Exception exMisc )
                {
                    throw new Exception (
                        string.Format (
							WizardWrx.Common.Properties.Resources.ERRMSG_RUNTIME ,
                            pstrTimeZoneID ,
                            Environment.NewLine ) ,
                        exMisc );
                }
            }   // FALSE (desired outcome) block, if ( pdtmTestDate == DateTime.MinValue || pdtmTestDate == DateTime.MaxValue || string.IsNullOrEmpty(pstrTimeZoneID) )
        }   // public static string GetSystemTimeZoneInfo


		/// <summary>
        /// This method has a nearly exact analogue in the constellations of WIL
        /// User Defined Functions that gave rise to its immediate predecessor,
        /// a like named function implemented in straight C, with a little help
        /// from the Windows National Language Subsystem, which underlies the
        /// CultureInfo class.
        /// </summary>
        /// <param name="pstrFormat">
        /// This System.String is a combination of tokens and literal text that
        /// governs the formatting of the date.
        /// </param>
        /// <returns>
        /// The return value is a string containing the current date and time,
		/// formatted according to the rules spelled out in format string
		/// pstrFormat.
        /// </returns>
        public static string ReformatNow ( string pstrFormat )
        { return ReformatSysDate ( DateTime.Now , pstrFormat ); }


        /// <summary>
        /// In the original constellation of WinBatch functions and their C
        /// descendants, this function took the form of an optional argument to
        /// ReformatNow. I think I prefer this way.
        /// </summary>
        /// <param name="pstrFormat">
        /// This System.String is a combination of tokens and literal text that
        /// governs the formatting of the date.
        /// </param>
        /// <returns>
        /// The return value is a string containing the current UTC time,
        /// formatted according to the rules spelled out in format string
		/// pstrFormat.
        /// </returns>
        public static string ReformatUtcNow ( string pstrFormat )
        { return ReformatSysDate ( DateTime.UtcNow , pstrFormat ); }


        /// <summary>
        /// ReformatSysDate is the core function of the constellation of
        /// routines that grew from the original WIL script. Substitution tokens
        /// drive construction of a formatted date string.
        /// </summary>
        /// <param name="pdtmToFormat">
        /// This System.DateTime is the time to be formatted.
        /// </param>
        /// <param name="pstrFormat">
        /// This System.String is a combination of tokens and literal text that
        /// governs the formatting of the date.
        /// </param>
        /// <returns>
        /// The return value is a string containing the date and/or time in
		/// argument pdtmToFormat, formatted according to the rules spelled out 
		/// in format string pstrFormat.
        /// </returns>
        public static string ReformatSysDate (
            DateTime pdtmToFormat ,
            string pstrFormat )
        {
			const string ARG_PDTM = @"pdtmToFormat";
            const string ARG_PSTRFORMAT = @"pstrFormat";            

            if ( pdtmToFormat == null )
                throw new ArgumentNullException (
                    ARG_PDTM ,
                    Common.Properties.Resources.ERRMSG_ARG_IS_NULL );

            if ( string.IsNullOrEmpty ( pstrFormat ) )
                throw new ArgumentException (
                    Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY ,
                    ARG_PSTRFORMAT );

            StringBuilder rsbFormattedDate = new StringBuilder (
                pstrFormat ,
                EstimateFinalLength ( ref pstrFormat ) );

            for ( int intCurrToken = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCurrToken < _intNDefinedTokens ; intCurrToken++ )
            {   // Unless a token appears in the format string, everything inside the switch block, and the switch, itself, is wasted effort.
                if ( pstrFormat.IndexOf ( RSD_TOKENS [ intCurrToken ] ) > ArrayInfo.ARRAY_INVALID_INDEX )
                {   // This token is used.
                    switch ( RSD_ALGORITHM [ intCurrToken ] )
                    {
                        case FormattingAlgorithm.ApplyFormatToDateTime:
                            {   // In order to reuse the name, strValue must be confined in a closure.
                                string strValue = pdtmToFormat.ToString ( RSD_BCL_FORMATS [ intCurrToken ] );
                                rsbFormattedDate.Replace (
                                    RSD_TOKENS [ intCurrToken ] ,
                                    strValue );
                            }   // The string, strValue goes out of scope.
                            break;

                        case FormattingAlgorithm.TwoLetterWeekday:
                            {   // In order to reuse the name, strValue must be confined in a block.
                                string strValue = RSD_WEEKDAY_2LTRS [ ( int ) pdtmToFormat.DayOfWeek ];
                                rsbFormattedDate.Replace (
                                    RSD_TOKENS [ intCurrToken ] ,
                                    strValue );
                            }   // The string, strValue goes out of scope.
                            break;

                        default:
                            string strMsg = string.Format (
								WizardWrx.Core.Properties.Resources.ERRMSG_INTERNAL_ERROR_UNSUPP_ALG ,	// Format string
                                ( int ) RSD_ALGORITHM [ intCurrToken ] ,								// Token 0
                                pstrFormat ,															// Token 1
                                Environment.NewLine );													// Token 2
                            throw new Exception ( strMsg );
                    }   // switch ( RSD_ALGORITHM [ intCurrToken ] )
                }   // if ( pstrFormat.IndexOf ( RSD_TOKENS [ intCurrToken ] ) > ArrayInfo.ARRAY_INVALID_INDEX )
            }   // for (int intCurrToken=ArrayInfo.ARRAY_FIRST_ELEMENT;intCurrToken<_intNDefinedTokens;intCurrToken++)

            return rsbFormattedDate.ToString ( );
        }   // ReformatSysDate
		#endregion	// Public static methods are the reason this class exists.


		#region Private methods simplify the public methods.
		private static int EstimateFinalLength ( ref string pstrFormat )
        {
			return pstrFormat.Length * 2;
        }   // private static int EstimateFinalLength
		#endregion	// Private methods simplify the public methods.
	}   // public class SysDateFormatters
}   // partial namespace WizardWrx