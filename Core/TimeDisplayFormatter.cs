/*
    ============================================================================

    Namespace Name:		WizardWrx.Core

    Class Name:			TimeDisplayFormatter

    File Name:			TimeDisplayFormatter.cs

    Synopsis:			This class is composed entirely of instance members, 
                        which are called as needed, using parameters defined as
                        read/write properties, to format the local time.

    Remarks:			All instance method come in pairs, one of which takes a
                        System.DateTime to be processed, and aonther which is
                        called without arguments, and returns a string derived
                        from the current time.

                        This class was not designed with thread safety in mind.
                        Nevertheless, if the main thread initializes the
                        properties, and all other threads confine themselves to
                        calling the instance methods, it should behave itself,
                        since the real work is done by private static members.

    Author:				David A. Gray

    License:            Copyright (C) 2011-2017, David A. Gray. 
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

    Date       Version By  Synopsis
    ---------- ------- --- --------------------------------------------------
    2011/07/11 1.2     DAG This class makes its first appearance.
 
    2011/07/13 1.4     DAG Correct a data error in the YYMMDD calendar date
                           formatting template.

    2011/07/16 1.5     DAG Change namespace name from ConsoleHelpers to
                           ApplicationHelpers, to reflect its broadened range of
                           applications (pun intended).

    2014/06/08 5.1     DAG Correct an oversight that left this class in the old
                           WizardWrx.ApplicationHelpers namespace. Since this
                           change affects only two other DLLs, and, at most one
                           user program, I took advantage of the opportunity to
                           hoist the DLLServices2 namespace to the first rank
                           under the overall WizardWrx namespace.

    2015/06/20 5.5     DAG Incorporate my three-clause BSD license.
 
    2016/04/10 6.0     DAG Scan for typographical errors flagged by the
                           spelling checker add-in, and correct what I find, and
                           update the formatting and marking of blocks.

    2016/05/20 6.1     DAG Eliminate pointless "end of" from bracket comments
                           throughout.

    2016/06/07 6.3     DAG 1) Adjust the internal documentation to correct a few
                              inconsistencies uncovered while preparing this
                              library for publication on GetHub.

                           2) Move hard coded message strings into managed
                              string resources.

    2017/02/21 7.0     DAG Break this library apart, so that smaller subsets of
                           classes can be distributed independently.

                           This module moved into WizardWrx.Core, a new
                           namespace, which is exported by a like named
                           dynamic-link library.

    2017/03/28 7.0     DAG Define a new PrepareLocalAndUTCTimes method.

    2017/07/16 7.0     DAG Replace references to string.empty, which is not a
                           true constant, with SpecialStrings.EMPTY_STRING,
                           which is one.

    2019/12/16 7.23    DAG Allow the tab consistency add-in to replace tabs with
                           spaces. The code is otherwise unchanged, although the
                           new build is required to add a binding redirect, and
                           the version numbering transitions to the SemVer
                           scheme.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;


namespace WizardWrx.Core
{
    /// <summary>
    /// Use instances of this class to return dates and times, uniformly
    /// formatted by rules set by way of its properties.
    /// 
    /// This class is sealed, and cannot be inherited.
    /// </summary>
    /// <seealso cref="DisplayFormats"/>
    /// <seealso cref="SysDateFormatters"/>
    public sealed class TimeDisplayFormatter
    {
        #region Public Constants and Enumerations
        /// <summary>
        /// Indicate the format to use for displaying the calendar date.
        /// </summary>
        public enum DateFieldOrder
        {
            /// <summary>
            /// Use the ShortDate format defined in the current CultureInfo.
            /// </summary>
            CultureInfoShortDate ,

            /// <summary>
            /// Display Month, Day, and Year, showing all four digits of the year.
            /// </summary>
            MMDDYYYY ,

            /// <summary>
            /// Display Year, Month, and Day, showing all four digits of the year.
            /// </summary>
            YYYYMMDD ,

            /// <summary>
            /// Display Day, Month, and Year, showing all four digits of the year.
            /// </summary>
            DDMMYYYY ,

            /// <summary>
            /// Display Month, Day, and Year, discarding the century from the year.
            /// </summary>
            MMDDYY ,

            /// <summary>
            /// Display Year, Month, and Day, discarding the century from the year.
            /// </summary>
            YYMMDD ,

            /// <summary>
            /// Display Day, Month, and Year, discarding the century from the year.
            /// </summary>
            DDMMYY
        }   // DateFieldOrder


        /// <summary>
        /// Indicate time format to use (CultureInfoShortTime, Civilian, or
        /// Military).
        /// </summary>
        public enum HoursFormatType
        {
            /// <summary>
            /// Use the ShortTime format defined in the current CultureInfo.
            /// </summary>
            CultureInfoShortTime ,

            /// <summary>
            /// Use civilian (12 hour) display format.
            /// </summary>
            Civilian ,

            /// <summary>
            /// Use Military (24 hour) display format.
            /// </summary>
            Military
        }   // HoursFormatType


        /// <summary>
        /// Indicate the precision to use for the time display. The class
        /// default is HMST.
        /// </summary>
        public enum TimePrecisionType
        {
            /// <summary>
            /// Display only hours and minutes (HH:MM).
            /// </summary>
            HM,

            /// <summary>
            /// Display hours, minutes, and seconds (HH:MM:SS).
            /// </summary>
            HMS,

            /// <summary>
            /// Display hours, minutes, seconds, and milliseconds.
            /// </summary>
            HMST,

            /// <summary>
            /// Display hours, minutes, seconds, milliseconds, and ticks.
            /// </summary>
            HMSTN
        }   // TimePrecisionType
        #endregion	// Public Constants and Enumerations


        #region Private Constants and Enumerations
        const string ARG_NAME_HRSFMTTYP = @"penmHoursFormatType";
        const string ARG_NAME_DATETIMEKIND = @"pdtmTheTime.Kind";

        const string CIVILIAN_AM_LC = @"am";
        const string CIVILIAN_PM_LC = @"pm";
        const string CIVILIAN_AM_UC = @"AN";
        const string CIVILIAN_PM_UC = @"PM";

        const int DIGITS_MDHMS = 2;
        const int DIGITS_MILLISECONDS = 3;
        const int DIGITS_TICKS = 18;
        const int DIGITS_COMPLETE_YEAR = 4;

        const string DT_FMT_TPL_CULTUREINFOSHORTDATE = FMT_UNUSED;
        const string DT_FMT_TPL_MMDDYYYY = @"{0}/{1}/{2}{3}";
        const string DT_FMT_TPL_YYYYMMDD = @"{2}{3}/{0}/{1}";
        const string DT_FMT_TPL_DDMMYYYY = @"{1}/{0}/{2}{3}";
        const string DT_FMT_TPL_MMDDYY = @"{0}/{1}/{2}";
        const string DT_FMT_TPL_YYMMDD = @"{3}/{0}/{1}";
        const string DT_FMT_TPL_DDMMYY = @"{1}/{0}/{3}";

        const string FMT_UNUSED = SpecialStrings.EMPTY_STRING;

        const string FMT_DATE_AND_TIME = @"{0}{1} {2}{3}";

        const string FMT_TOD_CULTUREINFOSHORTTIME = FMT_UNUSED;
        const string FMT_TOD_CIV = @"{0}:{1}{2}{5}";
        const string FMT_TOD_MIL = @"{0}:{1}{2}{3}{4}";

        const string FMT_AMPM = @" {0}";
        const string FMT_MDHM = @"{0}";
        const string FMT_SECS = @":{0}";
        const string FMT_MS = @".{0}";
        const string FMT_TICKS = @" ({0} Ticks)";
        const string FMT_TIMEZONE = @" {0}";
        const string FMT_WEEKDAY = @"{0}, ";
        const string FMT_YEAR = @"{0}";

        const int HOUR_NOON = 12;

        const string TZ_IS_UTC = @"UTC";
        const string TZ_IS_UNSPECIFIED = @"Unspecified";
        #endregion	// Private Constants and Enumerations


        #region Private Static Storage for All Instances
        //  --------------------------------------------------------------------
        //  Using the CalendarDateFormat as a subscript into this array, the
        //  FormatCalendarDate method returns the desired string representation
        //  of the date portion of a System.DateTime. Since CalendarDateFormat
        //  is a member of the DateFieldOrder enumeration, it is cast to an int
        //  for use as the subscript.
        //
        //  Each string has substitution tokens {0}, {1}, {2}, and {3} arranged
        //  in the desired order so that the same array of momth, day, century,
        //  and year of century values can be used to render any of the desired
        //  formats.
        //  --------------------------------------------------------------------

        static readonly string [ ] _astrCalDateFormats =
        {
            DT_FMT_TPL_CULTUREINFOSHORTDATE,
            DT_FMT_TPL_MMDDYYYY,
            DT_FMT_TPL_YYYYMMDD,
            DT_FMT_TPL_DDMMYYYY,
            DT_FMT_TPL_MMDDYY,
            DT_FMT_TPL_YYMMDD,
            DT_FMT_TPL_DDMMYY
        };  // string [ ] _astrCalDateFormats


        //  --------------------------------------------------------------------
        //  Using the TimePrecision as a subscript into this array, the
        //  FormatTimeTokenMilliseconds method returns the desired string
        //  representation of the Millisecond portion of a System.DateTime.
        //  Since TimePrecision is a member of the TimePrecisionType
        //  enumeration, it is cast to an int for use as the subscript.
        //  --------------------------------------------------------------------

        static readonly string [ ] _astrMillisecondsFormat =
        {
            FMT_UNUSED,
            FMT_UNUSED,
            FMT_MS,
            FMT_MS
        };  // string [ ] _astrMillisecondsFormat


        //  --------------------------------------------------------------------
        //  Using the TimePrecision as a subscript into this array, the
        //  FormatTimeTokenSeconds method returns the desired string
        //  representation of the Second portion of a System.DateTime. Since
        //  TimePrecision is a member of the TimePrecisionType enumeration, it
        //  is cast to an int for use as the subscript.
        //  --------------------------------------------------------------------

        static readonly string [ ] _astrSecondsFormat =
        {
            FMT_UNUSED,
            FMT_SECS,
            FMT_SECS,
            FMT_SECS
        };  // string [ ] _astrSecondsFormat


        //  --------------------------------------------------------------------
        //  Using the TimePrecision as a subscript into this array, the
        //  FromatTimeTokenTicks method returns the desired string
        //  representation of the Ticks portion of a System.DateTime. Since
        //  TimePrecision is a member of the TimePrecisionType enumeration, it
        //  is cast to an int for use as the subscript.
        //  --------------------------------------------------------------------

        static readonly string [ ] _astrTicksFormat =
        {
            FMT_UNUSED,
            FMT_UNUSED,
            FMT_UNUSED,
            FMT_TICKS
        };  // string [ ] _astrTicksFormat


        //  --------------------------------------------------------------------
        //  Using the HoursFormat as a subscript into this array, the
        //  FormatTimeOfDay method returns the desired string representation of
        //  the TimeOfDay portion of a System.DateTime. Since HoursFormat is a
        //  member of the HoursFormatType enumeration, it is cast to an int for
        //  use as the subscript.
        //  --------------------------------------------------------------------

        static readonly string [ ] _astrTimeOfDayFormat =
        {
            FMT_TOD_CULTUREINFOSHORTTIME,
            FMT_TOD_CIV,
            FMT_TOD_MIL
        };  // string [ ] _astrTimeOfDayFormat


        //  --------------------------------------------------------------------
        //  Formatting the numerical parts of the date must be separated from
        //  formatting of the date and time string, because the ToString method
        //  of an integer uses specialized format strings, and are confined to
        //  formatting numbers.
        //
        //  The arrays of tokens shown above insert the formatted numbers into a
        //  standard System.String.Format template after System.int.ToString has
        //  done its part. These read-only strings are initialized by a static
        //  method, WizardWrx.NumericFormats.FixedWidthInteger, which constructs
        //  a format string that yields a string of a desired number of digits.
        //  --------------------------------------------------------------------

        static readonly string TOSTRING_MDHMS = WizardWrx.NumericFormats.FixedWidthInteger ( DIGITS_MDHMS );
        static readonly string TOSTRING_MS = WizardWrx.NumericFormats.FixedWidthInteger ( DIGITS_MILLISECONDS );
        static readonly string TOSTRING_TICKS = WizardWrx.NumericFormats.FixedWidthInteger ( DIGITS_TICKS );
        static readonly string TOSTRING_YEAR = WizardWrx.NumericFormats.FixedWidthInteger ( DIGITS_COMPLETE_YEAR );

        //  --------------------------------------------------------------------
        //  Among other things, the CurrentTimeZone property of the globally
        //  static TimeZone object returns a string representation of the
        //  correct time zone name, based on the state of Standard or Daylight
        //  Saving time for a specified System.DateTime.
        //  --------------------------------------------------------------------

        static readonly TimeZone _tzMachine = TimeZone.CurrentTimeZone;
        #endregion	// Private Static Storage for All Instances


        #region Private Storage for Class Instance
        DateFieldOrder _enmDateFieldOrder = DateFieldOrder.CultureInfoShortDate;
        HoursFormatType _enmHoursFormatType = HoursFormatType.Military;
        TimePrecisionType _enmTimePrecisionType = TimePrecisionType.HMST;

        bool _fShowAmPmAsUC = false;
        bool _fShowTimeZone = false;
        bool _fShowWeekday = false;
        #endregion	// Private Storage for Class Instance


        #region Properties
        /// <summary>
        /// Set this property to override the default formatting of calendar
        /// dates, which is governed by the active CultureInfo settings, which
        /// are, themselves, governed by the active Regional Settings of the
        /// operating system.
        /// </summary>
        public DateFieldOrder CalendarDateFormat
        {
            get { return _enmDateFieldOrder; }
            set
            {
                const string PROPERTY_NAME = @"CalendarDateFormat";

                switch ( value )
                {
                    case DateFieldOrder.CultureInfoShortDate:
                    case DateFieldOrder.DDMMYY:
                    case DateFieldOrder.DDMMYYYY:
                    case DateFieldOrder.MMDDYY:
                    case DateFieldOrder.MMDDYYYY:
                    case DateFieldOrder.YYMMDD:
                    case DateFieldOrder.YYYYMMDD:
                        _enmDateFieldOrder = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException (
                            PROPERTY_NAME ,
                            InvalidEnumValueMessage (
                                PROPERTY_NAME ,
                                ( int ) value ,
                            ( int ) _enmHoursFormatType ) );
                }   // End switch ( value )
            }   // CalendarDateFormat Set method
        }   // CalendarDateFormat property


        /// <summary>
        /// Set the display format for hours. See the HoursFormatType enumeration
        /// for more details.
        /// 
        /// When the value is DateFieldOrder.CultureInfoShortDate, the
        /// HoursFormat and TimePrecision properties are ignored.
        ///
        /// The default format is Military, and the value must be a member of
        /// the HoursFormatType enumeration. If an invalid value is specified, a
        /// System.ArgumentOutOfRangeException exception is thrown, and the
        /// property value is unchanged.
        ///
        /// When the value of this property is CultureInfoShortTime, the
        /// TimePrecision property is ignored.
        /// </summary>
        public HoursFormatType HoursFormat
        {
            get
            {
                return _enmHoursFormatType;
            }   // HoursFormatType HoursFormatType Get method

            set
            {
                const string PROPERTY_NAME = @"HoursFormat";

                switch ( value )
                {
                    case HoursFormatType.Civilian:
                    case HoursFormatType.CultureInfoShortTime:
                    case HoursFormatType.Military:
                        _enmHoursFormatType = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException (
                            PROPERTY_NAME ,
                            InvalidEnumValueMessage (
                                PROPERTY_NAME ,
                                ( int ) value ,
                                ( int ) _enmHoursFormatType ) );
                }   // switch ( value )
            }   // HoursFormatType HoursFormatType Set method
        }   // HoursFormatType HoursFormatType property


        /// <summary>
        /// This static property provides convenience access to the
        /// CurrentTimeZone property maintained in a static TimeZone instance
        /// for its own use.
        /// </summary>
        public static TimeZone MachineTimeZone { get { return _tzMachine; } }


        /// <summary>
        /// Set this property to TRUE to have the AM and PM tokens displayed in
        /// upper case.
        ///
        /// Unless the HoursFormat property is HoursFormatType.Civilian, this
        /// property is ignored.
        /// </summary>
        public bool ShowAmPmAsUC
        {
            get { return _fShowAmPmAsUC; }
            set { _fShowAmPmAsUC = value; }
        }   // ShowAmPmAsUC property


        /// <summary>
        /// Set this property to TRUE to have the local time zone displayed with
        /// the time. By default, the time zone is omitted.
        ///
        /// This property is applied independently of the HoursFormatType and
        /// Precision properties.
        /// </summary>
        public bool ShowTimeZone
        {
            get { return _fShowTimeZone; }
            set { _fShowTimeZone = value; }
        }   // ShowTimeZone property


        /// <summary>
        /// Set this property to TRUE to have the local weekday name displayed
        /// with the time. By default, the weekday name is omitted.
        ///
        /// This property is applied independently of the HoursFormatType and
        /// Precision properties.
        /// </summary>
        public bool ShowWeekday
        {
            get { return _fShowWeekday; }
            set { _fShowWeekday = value; }
        }   // ShowWeekday property


        /// <summary>
        /// This property governs the precision used for time displays. The
        /// class default is HMST.
        ///
        /// When the value of the HoursFormat property is CultureInfoShortTime,
        /// the value of this property is ignored.
        ///
        /// For additional information, see the TimePrecisionType enumeration.
        /// </summary>
        public TimePrecisionType TimePrecision
        {
            get { return _enmTimePrecisionType; }
            set
            {
                const string PROPERTY_NAME = "@TimePrecision";

                switch ( value )
                {
                    case TimePrecisionType.HM:
                    case TimePrecisionType.HMS:
                    case TimePrecisionType.HMST:
                    case TimePrecisionType.HMSTN:
                        _enmTimePrecisionType = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException (
                            PROPERTY_NAME,
                            InvalidEnumValueMessage (
                                PROPERTY_NAME ,
                                ( int ) value ,
                                ( int ) _enmHoursFormatType ) );
                }   // End switch ( value )
            }   // TimePrecisionType Precision Set method
        }   // TimePrecisionType Precision property
        #endregion	// Properties


        #region Constructors
        /// <summary>
        /// The default constructor initializes an instance that follows the
        /// default preferences built into the class. Hence, methods called on
        /// objects created by it are guaranteed to yield a usable string unless
        /// the DateTime value supplied to the method is, itself, invalid.
        /// </summary>
        /// <remarks>
        /// Rather than attempt to create the numerous constructors that would
        /// be required to cover every possible combination of properties, there
        /// is just one constructor. All properties are read-write. Use them to
        /// change the operating parameters.
        ///
        /// If you always use the same combination of settings, you can wrap
        /// your own class around an instance of this one.
        /// </remarks>
        public TimeDisplayFormatter ( ) { }
        #endregion	// Constructors


        #region Public Methods
        /// <summary>
        /// Format the specified time.
        /// </summary>
        /// <param name="pdtmThisTime">
        /// The DateTime instance to format.
        /// </param>
        /// <returns>
        /// The input time, formatted as dictated by the current settings of the
        /// properties of the class instance.
        /// </returns>
        public string FormatThisTime ( DateTime pdtmThisTime )
        {
            return FormatTheTime (
                pdtmThisTime ,
                _enmDateFieldOrder ,
                _enmHoursFormatType ,
                _enmTimePrecisionType ,
                _fShowTimeZone ,
                _fShowWeekday ,
                _fShowAmPmAsUC );
        }   // FormatThisTime


        /// <summary>
        /// Format the current time.
        /// </summary>
        /// <returns>
        /// The current time, formatted as dictated by the current settings of
        /// the properties of the class instance.
        /// </returns>
        public string FormatCurrentTime ( )
        {
            return FormatTheTime (
                DateTime.Now ,
                _enmDateFieldOrder ,
                _enmHoursFormatType ,
                _enmTimePrecisionType ,
                _fShowTimeZone ,
                _fShowWeekday ,
                _fShowAmPmAsUC );
        }   // FormatCurrentTime


        /// <summary>
        /// Return the time zone name that corresponds to a local time.
        /// </summary>
        /// <param name="pdtmTheTime">
        /// Specify the System.DateTime for which the correct time zone string
        /// is required.
        /// </param>
        /// <returns>
        /// If the DateTimeKind of argument pdtmTheTime is Local, the return
        /// value is a string representation of the spelled out time zone name.
        /// Otherwise, an ArgumentException is thrown.
        /// </returns>
        public static string GetTimeZoneForTime ( DateTime pdtmTheTime )
        {
            switch ( pdtmTheTime.Kind )
            {
                case DateTimeKind.Local:
                    if ( _tzMachine.IsDaylightSavingTime ( pdtmTheTime ) )
                        return _tzMachine.DaylightName;
                    else
                        return _tzMachine.StandardName;

                case DateTimeKind.Utc:
                    return TZ_IS_UTC;

                case DateTimeKind.Unspecified:
                    return TZ_IS_UNSPECIFIED;

                default:
                    return SpecialStrings.EMPTY_STRING;
            }   // switch ( pdtmTheTime.Kind )
        }   // GetTimeZoneForTime
        

        /// <summary>
        /// Given a pair of DateTime structures, return them in a standardized
        /// format.
        /// </summary>
        /// <param name="dtmTimeLocal">
        /// Specify the local time to include in the standardized message.
        /// </param>
        /// <param name="dtmTimeUtc">
        /// Specify the UTC time to include in the standardized message.
        /// </param>
        /// <returns>
        /// The returned string is of the format {0} ({1} UTC), where {0} is
        /// replaced with the local time, while {1} is replaced with the
        /// corresponding UTC time.
        /// </returns>
        /// <example>
        /// 2017/03/27 18:18:58 (2017/03/27 23:18:58 UTC)
        /// </example>
        public static string PrepareLocalAndUTCTimes (
            DateTime dtmTimeLocal ,
            DateTime dtmTimeUtc )
        {
            return string.Format (
                Properties.Resources.LOCAL_AND_UTC_TIME_TEMPLATE ,				// Format control string, e. g., {0} ({1} UTC)
                dtmTimeLocal ,													// Format Item 0 = Local Time
                dtmTimeUtc );													// Format Item 1 = Corresponding UTC Time
        }	// PrepareLocalAndUTCTimes
        #endregion	// Public Methods


        #region Private Static Methods
        private static string FormatTheTime (
            DateTime pdtmTheTime ,
            DateFieldOrder penmDateFieldOrder ,
            HoursFormatType penmHoursFormatType ,
            TimePrecisionType penmTimePrecisionType ,
            bool pfShowTimeZone ,
            bool pfShowWeekdayName ,
            bool pfShowAmPmAsUC )
        {
            string strFormattedCalendarDate = FormatCalendarDate (
                pdtmTheTime ,
                penmDateFieldOrder );
            string strFormattedTimeOfDay = FormatTimeOfDay (
                pdtmTheTime ,
                penmHoursFormatType ,
                penmTimePrecisionType ,
                pfShowAmPmAsUC );
            return string.Format (
                FMT_DATE_AND_TIME ,             // Message Template
                new string [ ]{
                    FormatWeekdayNameDisplay(
                        pdtmTheTime ,
                        pfShowWeekdayName ) ,   // Token 0
                    strFormattedCalendarDate ,  // Token 1
                    strFormattedTimeOfDay,      // Token 2
                    FormatTimeZoneDisplay(
                        pdtmTheTime ,
                        pfShowTimeZone)} );     // Token 3
        }   // EFormatTheTime


        private static string FormatCalendarDate (
            DateTime pdtmTheTime ,
            DateFieldOrder penmDateFieldOrder )
        {
            if ( penmDateFieldOrder == DateFieldOrder.CultureInfoShortDate )
                return pdtmTheTime.ToShortDateString ( );
            else
                return string.Format (
                    _astrCalDateFormats [ ( int ) penmDateFieldOrder ] ,                            // Message Template
                    new string [ ] {
                        pdtmTheTime.Month.ToString ( TOSTRING_MDHMS ) ,                             // Token 0
                        pdtmTheTime.Day.ToString ( TOSTRING_MDHMS ) ,                               // Token 1
                        ( ( int ) ( ( pdtmTheTime.Year ) / 100 ) ).ToString ( TOSTRING_MDHMS) ,     // Token 2
                        ( ( int ) ( ( pdtmTheTime.Year ) % 100 ) ).ToString ( TOSTRING_MDHMS ) } ); // Token 3
        }   // FormatCalendarDate


        private static string FormatAMPMToken (
            DateTime pdtmTheTime ,
            HoursFormatType penmHoursFormatType ,
            bool pfShowAmPmAsUC )
        {
            switch ( penmHoursFormatType )
            {
                case HoursFormatType.Civilian:
                    if ( pdtmTheTime.Hour < HOUR_NOON )
                        if ( pfShowAmPmAsUC )
                            return string.Format (
                                FMT_AMPM ,
                                CIVILIAN_AM_UC );
                        else
                            return string.Format (
                                FMT_AMPM ,
                                CIVILIAN_AM_LC );
                    else
                        if ( pfShowAmPmAsUC )
                            return string.Format (
                                FMT_AMPM ,
                                CIVILIAN_PM_UC );
                        else
                            return string.Format (
                                FMT_AMPM ,
                                CIVILIAN_PM_LC );

                case HoursFormatType.CultureInfoShortTime:
                case HoursFormatType.Military:
                    return SpecialStrings.EMPTY_STRING;

                default:
                    throw new ArgumentOutOfRangeException (
                        ARG_NAME_HRSFMTTYP ,
                        InvalidEnumArgValueMessage (
                            ARG_NAME_HRSFMTTYP ,
                            ( int ) penmHoursFormatType ) );
            }   // switch ( penmHoursFormatType )
        }   // FormatAMPMToken


        private static string FormatTimeOfDay (
            DateTime pdtmTheTime ,
            HoursFormatType penmHoursFormatType ,
            TimePrecisionType penmTimePrecisionType ,
            bool pfShowAmPmAsUC )
        {
            if ( penmHoursFormatType == HoursFormatType.CultureInfoShortTime )
                return pdtmTheTime.ToShortTimeString ( );
            else
                return string.Format (
                    _astrTimeOfDayFormat [ ( int ) penmHoursFormatType ] ,		// Message Template
                    new string [ ] {
                        FormatTimeTokenHour(
                            pdtmTheTime ,
                            penmHoursFormatType ) ,								// Token 0
                        pdtmTheTime.Minute.ToString ( TOSTRING_MDHMS ) ,		// Token 1
                        FormatTimeTokenSeconds (
                            pdtmTheTime ,
                            penmTimePrecisionType ) ,							// Token 2
                        FormatTimeTokenMilliseconds (
                            pdtmTheTime ,
                            penmTimePrecisionType ) ,							// Token 3
                        FromatTimeTokenTicks (
                            pdtmTheTime ,
                            penmTimePrecisionType ) ,							// Token 4
                        FormatAMPMToken (
                            pdtmTheTime ,
                            penmHoursFormatType,
                            pfShowAmPmAsUC)} );									// Token 5
        }   // FormatTimeOfDay


        private static string FormatTimeTokenHour (
            DateTime pdtmTheTime ,
            HoursFormatType penmHoursFormatType )
        {
            switch ( penmHoursFormatType )
            {
                case HoursFormatType.Civilian:
                    if ( pdtmTheTime.Hour < HOUR_NOON )
                        return pdtmTheTime.Hour.ToString ( TOSTRING_MDHMS );
                    else
                        return ( pdtmTheTime.Hour - HOUR_NOON ).ToString ( TOSTRING_MDHMS );

                case HoursFormatType.CultureInfoShortTime:
                    return SpecialStrings.EMPTY_STRING;

                case HoursFormatType.Military:
                    return pdtmTheTime.Hour.ToString ( TOSTRING_MDHMS );

                default:
                    throw new ArgumentOutOfRangeException (
                        ARG_NAME_HRSFMTTYP ,
                        InvalidEnumArgValueMessage (
                            ARG_NAME_HRSFMTTYP ,
                            ( int ) penmHoursFormatType ) );
            }   // switch ( penmHoursFormatType )
        }   // FormatTimeTokenHour


        private static string FormatTimeTokenMilliseconds (
            DateTime pdtmTheTime ,
            TimePrecisionType penmTimePrecisionType )
        {
            if ( _astrMillisecondsFormat [ ( int ) penmTimePrecisionType ].Length > MagicNumbers.EMPTY_STRING_LENGTH )
                return string.Format (
                    _astrMillisecondsFormat [ ( int ) penmTimePrecisionType ] ,
                    pdtmTheTime.Millisecond.ToString ( TOSTRING_MS ) );
            else
                return SpecialStrings.EMPTY_STRING;
        }   // FormatTimeTokenMilliseconds


        private static string FormatTimeTokenSeconds (
            DateTime pdtmTheTime ,
            TimePrecisionType penmTimePrecisionType )
        {
            if ( _astrSecondsFormat [ ( int ) penmTimePrecisionType ].Length > MagicNumbers.EMPTY_STRING_LENGTH )
                return string.Format (
                    _astrSecondsFormat [ ( int ) penmTimePrecisionType ] ,
                    pdtmTheTime.Second.ToString ( TOSTRING_MDHMS ) );
            else
                return SpecialStrings.EMPTY_STRING;
        }   // FormatTimeTokenSeconds


        private static string FromatTimeTokenTicks (
            DateTime pdtmTheTime ,
            TimePrecisionType penmTimePrecisionType )
        {
            if ( _astrTicksFormat [ ( int ) penmTimePrecisionType ].Length > MagicNumbers.EMPTY_STRING_LENGTH )
                return string.Format (
                    _astrTicksFormat [ ( int ) penmTimePrecisionType ] ,
                    pdtmTheTime.Ticks.ToString ( TOSTRING_TICKS ) );
            else
                return SpecialStrings.EMPTY_STRING;
        }   // FromatTimeTokenTicks


        private static string FormatTimeZoneDisplay (
            DateTime pdtmTheTime ,
            bool pfShowTimeZone )
        {
            if ( pfShowTimeZone )
                return string.Format (
                    FMT_TIMEZONE ,
                    GetTimeZoneForTime ( pdtmTheTime ) );
            else
                return SpecialStrings.EMPTY_STRING;
        }   // FormatTimeZoneDisplay


        private static string FormatWeekdayNameDisplay (
            DateTime pdtmTheTime ,
            bool pfShowWeekdayName )
        {
            if ( pfShowWeekdayName )
                return string.Format (
                    FMT_WEEKDAY ,
                    pdtmTheTime.DayOfWeek.ToString ( ) );
            else
                return SpecialStrings.EMPTY_STRING;
        }   // FormatWeekdayNameDisplay


        private static string InvalidEnumArgValueMessage (
            string pstrName ,
            int pintSpecifiedValue )
        {
            return string.Format (
                Properties.Resources.TDF_INVALID_ARG_VALUE ,					// Format control string (Message Template)
                pstrName ,														// Format Item (Token) 0 = Argument Name
                pintSpecifiedValue );											// Format Item (Token) 1 = Argument Value
        }   // InvalidEnumArgValueMessage


        private static string InvalidEnumValueMessage (
            string pstrName,
            int pintSpecifiedValue ,
            int pintCurrentValue )
        {
            return string.Format (
                Properties.Resources.TDF_INVALID_ENUM_VALUE ,					// Format control string (Message Template)
                new object[]
                {
                    pstrName,													// Format Item (Token) 0 = Argument Name
                    pintSpecifiedValue ,										// Format Item (Token) 1 = Argument Name
                    pintCurrentValue ,											// Format Item (Token) 2 = Current Property Value
                    Environment.NewLine} );										// Format Item (Token) 3 = Embedded Newline
        }   // InvalidEnumValueMessage
        #endregion	//  Private Static Methods
    }   // public class TimeDisplayFormatter
}   // partial namespace WizardWrx.Core