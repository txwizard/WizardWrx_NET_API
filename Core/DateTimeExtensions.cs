/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         DateTimeExtensions

    File Name:          DateTimeExtensions.cs

    Synopsis:           This static class implements extension methods on the
                        System.DateTime class that provide nontrivial missing
                        functionality that returns the nearest (most recent)
						instance of a specified weekday (day of the week) to the
						value in the DateTime object that it extends.

    Remarks:            Extension methods are the only way to extend a
                        structure such as a System.DateTime, which cannot be
						inherited.

	License:            Copyright (C) 2021, David A. Gray.
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

    Created:            Sunday, 31 August 2014 - Tuesday, 01 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2022/03/08 8.0.264 DAG    Initial implementation.
    ============================================================================
*/


using System;


namespace WizardWrx
{
	/// <summary>
	/// This class implements two overloads of DateOfMostRecentWeekdayDay, a
	/// static method, implemented as an extension method on the System.DateTime
	/// type. The two overloads accept their one externally visible argment as
	/// either a DayOfWeek enumeration member or its integral value.
	/// </summary>
	public static class DateTimeExtensions
    {
		/// <summary>
		/// Given a date and a day of the week, return the date of the most
		/// recent instance of the specified day of the week.
		/// </summary>
		/// <param name="pdtmThisDate">
		/// Specify the DateTime to serve as the reference point against which
		/// calculate the most recent weekday given by <paramref name="penmDayOfWeek"/>.
		/// </param>
		/// <param name="penmDayOfWeek">
		/// Specify the weekday in terms of the DayOfWeek enumeration for which the
		/// weekday nearest to <paramref name="pdtmThisDate"/> is desired.
		/// </param>
		/// <returns>
		/// The return value is the date, represented as a DateTime object, for
		/// which the DayOfWeek property is the value specified by
		/// <paramref name="penmDayOfWeek"/> that is the most recent instance of
		/// the specified weekday that is on or before the date specified by
		/// <paramref name="pdtmThisDate"/>.
		/// </returns>
		/// <example>
		/// When <paramref name="pdtmThisDate"/> is 03/08/2022 and
		/// <paramref name="penmDayOfWeek"/> is Sunday, the return value is
		/// Sunday, 03/06/2022.
		/// </example>
		/// <remarks>
		/// Since enumerated types can be freely converted to and from integers,
		/// and a program stands a fair chance of having the weekday specified
		/// in terms of either, two versions of this method are offered. Since
		/// integers are easier to manipulate, the overload that accepts the
		/// integer representation of the weekday implements both; the overload
		/// that takes the enumeration casts it to integer and calls the other
		/// overload, through which it returns.
		/// </remarks>
		public static DateTime DateOfMostRecentWeekday (
			this DateTime pdtmThisDate ,
			DayOfWeek penmDayOfWeek )
		{
			return DateOfMostRecentWeekday (
				pdtmThisDate ,
				( int ) penmDayOfWeek );
		}   // public  static DateTime DateOfMostRecentWeekday (1 of 2)


		/// <summary>
		/// Given a date and a day of the week, return the date of the most
		/// recent instance of the specified day of the week.
		/// </summary>
		/// <param name="pdtmThisDate">
		/// Specify the DateTime to serve as the reference point against which
		/// calculate the most recent weekday given by <paramref name="pintDayOfWeekIndex"/>.
		/// </param>
		/// <param name="pintDayOfWeekIndex">
		/// Specify the weekday in terms of the integer that maps to the intended
		/// DayOfWeek enumeration value for which the weekday nearest to
		/// <paramref name="pdtmThisDate"/> is desired.
		/// </param>
		/// <returns>
		/// The return value is the date, represented as a DateTime object, for
		/// which the DayOfWeek property is the value specified as integer
		/// <paramref name="pintDayOfWeekIndex"/> that is the most recent instance of
		/// the specified weekday that is on or before the date specified by
		/// <paramref name="pdtmThisDate"/>.
		/// </returns>
		/// <example>
		/// When <paramref name="pdtmThisDate"/> is 03/08/2022 and
		/// <paramref name="pintDayOfWeekIndex"/> is Sunday, the return value is
		/// Sunday, 03/06/2022.
		/// </example>
		/// <remarks>
		/// Since enumerated types can be freely converted to and from integers,
		/// and a program stands a fair chance of having the weekday specified
		/// in terms of either, two versions of this method are offered. Since
		/// integers are easier to manipulate, the overload that accepts the
		/// integer representation of the weekday implements both; the overload
		/// that takes the enumeration casts it to integer and calls the other
		/// overload, through which it returns.
		/// </remarks>
		public static DateTime DateOfMostRecentWeekday (
			this DateTime pdtmThisDate ,
			int pintDayOfWeekIndex )
		{
			int intWeekDayIndexOfThisDate = ( int ) pdtmThisDate.DayOfWeek;

			if ( intWeekDayIndexOfThisDate == pintDayOfWeekIndex )
			{
				return pdtmThisDate;
			}   // TRUE (The degenerate case is that the input date falls on the specified day of the weeek.) block, if ( intWeekDayIndexOfThisDate == pintDayOfWeekIndex )
			else
			{
				if ( intWeekDayIndexOfThisDate > pintDayOfWeekIndex )
				{
					int intDaysDiff = ( intWeekDayIndexOfThisDate - pintDayOfWeekIndex ) * MagicNumbers.MINUS_ONE;
					return pdtmThisDate.AddDays ( intDaysDiff );
				}   // TRUE (Since the current date is later in the week than the desired weekday, the weekday is given by the difference between the two.) block, if ( intWeekDayIndexOfThisDate > pintDayOfWeekIndex )
				else
				{
					DateTime dtmWeekdayCandidate;
					int intDays2Decrement = MagicNumbers.PLUS_ONE;

					do
					{   // while ( ( int ) dtmWeekdayCandidate.DayOfWeek != pintDayOfWeekIndex );
						dtmWeekdayCandidate = pdtmThisDate.AddDays ( intDays2Decrement * MagicNumbers.MINUS_ONE );
						intDays2Decrement++;
					}
					while ( ( int ) dtmWeekdayCandidate.DayOfWeek != pintDayOfWeekIndex );

					return dtmWeekdayCandidate;
				}   // FALSE (Since the current date precedes the target weekday, we must look backwards.) block, if ( intWeekDayIndexOfThisDate > pintDayOfWeekIndex )
			}   // FALSE (Everything elase requires a bit more math.) block, if ( intWeekDayIndexOfThisDate == pintDayOfWeekIndex )
		}   // public  static DateTime DateOfMostRecentWeekday (2 of 2)
	}   // public static class DateTimeExtensions
}   // partial namespace WizardWrx