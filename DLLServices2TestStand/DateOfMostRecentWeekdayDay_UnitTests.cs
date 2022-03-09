/*
    ============================================================================

    Namespace:			DLLServices2TestStand

    Class Name:			DateOfMostRecentWeekdayDay_UnitTests

    File Name:			DateOfMostRecentWeekdayDay_UnitTests.cs

    Synopsis:			This module encapsulates the unit tests for a new static
                        method, DateOfMostRecentWeekday, which was developed
						in CSharp_Lab as DateOfMostRecentWeekdayDay, a standard
						static method. By contrast, DateOfMostRecentWeekday is
						implemented as an extension method on the System.DateTime
						type.

    Remarks:			This class holds together two static read-only arrays,
						s_astrDateStrings and s_aenmDayOfWeek, that are inputs
						to static method TestFindingMostRecentWeekday, its one
						and only visible attribute.

    Author:				David A. Gray

    License:            Copyright (C) 2022, David A. Gray.
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

    Date       Version  By  Synopsis
    ---------- -------- --- ----------------------------------------------------
    2022/03/08 8.0.1483 DAG This class makes its first appearance.
    ============================================================================
*/


using System;

using WizardWrx;


namespace DLLServices2TestStand
{
    internal static class DateOfMostRecentWeekdayDay_UnitTests
    {
		//	--------------------------------------------------------------------
		//	Theese two lists are test cases for TestFindingMostRecentWeekday.
		//	--------------------------------------------------------------------

		static readonly string [ ] s_astrDateStrings =
		{
			@"2022/03/07 11:59:41",
			@"2022/03/08 11:59:41",
			@"2022/03/09 11:59:41",
			@"2022/03/10 11:59:41",
			@"2022/03/11 11:59:41",
			@"2022/03/12 11:59:41",
			@"2022/03/13 11:59:41",
			@"2022/03/14 11:59:41",
			@"2022/03/15 11:59:41",
			@"2022/03/16 11:59:41",
			@"2022/03/17 11:59:41",
			@"2022/03/18 11:59:41",
			@"2022/03/19 11:59:41",
			@"2022/03/20 11:59:41",
			@"2022/03/21 11:59:41",
			@"2022/03/22 11:59:41",
			@"2022/03/23 11:59:41",
			@"2022/03/24 11:59:41",
			@"2022/03/25 11:59:41",
			@"2022/03/26 11:59:41",
			@"2022/03/27 11:59:41",
			@"2022/03/28 11:59:41",
			@"2022/03/29 11:59:41",
			@"2022/03/30 11:59:41",
			@"2022/03/31 11:59:41",
			@"2022/04/01 11:59:41",
			@"2022/04/02 11:59:41",
			@"2022/04/03 11:59:41",
			@"2022/04/04 11:59:41"
		};  // static readonly string [ ] s_astrDateStrings

		static readonly DayOfWeek [ ] s_aenmDayOfWeek =
		{
			DayOfWeek.Sunday,
			DayOfWeek.Monday,
			DayOfWeek.Tuesday,
			DayOfWeek.Wednesday,
			DayOfWeek.Thursday,
			DayOfWeek.Friday,
			DayOfWeek.Saturday
		};  // static readonly DayOfWeek [ ] s_aenmDayOfWeek


		internal static void TestFindingMostRecentWeekday ( )
		{
			int intNTestDates = s_astrDateStrings.Length;
			Console.WriteLine (
				@"TestFindingMostRecentWeekday Begin for {0} test cases:" ,
				intNTestDates );

			for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
					  intJ < intNTestDates ;
					  intJ++ )
			{
				if ( DateTime.TryParse ( s_astrDateStrings [ intJ ] , out DateTime dtmCurrentCase ) )
				{
					Console.WriteLine (
						@"{3}    Test Case {0} of {1}: {2:yyyy/MM/dd hh:mm:ss}{3}" ,
						ArrayInfo.OrdinalFromIndex ( intJ ) ,                   // Format Item 0: Test Case {0}
						intNTestDates ,                                         // Format Item 1: of {1}
						dtmCurrentCase ,                                        // Format Item 2: : {2}
						Environment.NewLine );                                  // Format Item 3: Extra line break
					Console.WriteLine (
						@"        Weekday         = {0}" ,
						dtmCurrentCase.DayOfWeek );
					Console.WriteLine (
						@"        WeekdayDayIndex = {0}{1}" ,
						( int ) dtmCurrentCase.DayOfWeek ,
						Environment.NewLine );

					int intNDaysOfWeek2Test = s_aenmDayOfWeek.Length;

					for ( int intK = ArrayInfo.ARRAY_FIRST_ELEMENT ;
							  intK < intNDaysOfWeek2Test ;
							  intK++ )
					{
						Console.WriteLine (
							@"            Testing scenario {0} of {1}: Weekday Name = {2}, Enum value = {3}, Return value = {4:dddd, yyyy/MM/dd hh:mm:ss}" ,
							ArrayInfo.OrdinalFromIndex ( intK ) ,               // Format Item 0:Testing scenario {0}
							intNDaysOfWeek2Test ,                               // Format Item 1: of {1}
							s_aenmDayOfWeek [ intK ] ,                          // Format Item 2: Weekday Name = {2}
							( int ) s_aenmDayOfWeek [ intK ] ,                  // Format Item 3: Enum value = {3}
							dtmCurrentCase.DateOfMostRecentWeekday (			// Format Item 4: Return value = {4}
								s_aenmDayOfWeek [ intK ] ) );                   // DayOfWeek penmDayOfWeek							
					}   // for ( int intK = ArrayInfo.ARRAY_FIRST_ELEMENT ; intK < intNDaysOfWeek2Test ; intK++ )
				}   // TRUE (anticipated outcome) block, if ( DateTime.TryParse ( s_astrDateStrings [ intJ ] , out DateTime dtmCurrentCase ) )
				else
				{
					Console.WriteLine (
						@"            Test Case {0} of {1}: {2} cannot be parsed as a System.DateTime object - case SKIPPED.{3}" ,
						ArrayInfo.OrdinalFromIndex ( intJ ) ,
						intNTestDates ,
						s_astrDateStrings [ intJ ] ,
						Environment.NewLine );
				}   // FALSE (unanticipated outcome) block, if ( DateTime.TryParse ( s_astrDateStrings [ intJ ] , out DateTime dtmCurrentCase ) )
			}   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNTestDates ; intJ++ )

			Console.WriteLine (
				@"{0}TestFindingMostRecentWeekday Done{0}" ,
				Environment.NewLine );
		}   // internal static void TestFindingMostRecentWeekday
	}   // internal static class DateOfMostRecentWeekdayDay_UnitTests
}   // partial namespace DLLServices2TestStand