/*
    ============================================================================

    Namespace:          DLLServices2TestStand

    Class Name:         MathTests

    File Name:          MathTests.cs

    Synopsis:           This static class exercises the static methods on the
                        new static Math class.

    Remarks:            

    Author:             David A. Gray

	License:            Copyright (C) 2018, David A. Gray. 
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

    Created:            Sunday, 14 September 2014 and Monday, 15 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2018/11/12 7.11    DAG    Initial implementation.
	============================================================================
*/

using System;

using WizardWrx;

namespace DLLServices2TestStand
{
    class MoreMathTests
    {
        static readonly IntegerModulusTestCase [ ] s_aintegerModulusTestCases = new IntegerModulusTestCase [ ]
        {
            new IntegerModulusTestCase(0,10),
            new IntegerModulusTestCase(1,10),
            new IntegerModulusTestCase(9,10),
            new IntegerModulusTestCase(10,10),
            new IntegerModulusTestCase(11,10),
            new IntegerModulusTestCase(19,10),
            new IntegerModulusTestCase(20,10),
            new IntegerModulusTestCase(21,10),

            new IntegerModulusTestCase(1,100),
            new IntegerModulusTestCase(99,100),
            new IntegerModulusTestCase(100,100),
            new IntegerModulusTestCase(101,100),
            new IntegerModulusTestCase(199,100),
            new IntegerModulusTestCase(200,100),
            new IntegerModulusTestCase(201,100),

            new IntegerModulusTestCase(-1,10),
            new IntegerModulusTestCase(-9,10),
            new IntegerModulusTestCase(-10,10),
            new IntegerModulusTestCase(-11,10),
            new IntegerModulusTestCase(-19,10),
            new IntegerModulusTestCase(-20,10),
            new IntegerModulusTestCase(-21,10),

            new IntegerModulusTestCase(-1,-10),
            new IntegerModulusTestCase(-9,-10),
            new IntegerModulusTestCase(-10,-10),
            new IntegerModulusTestCase(-11,-10),
            new IntegerModulusTestCase(-19,-10),
            new IntegerModulusTestCase(-20,-10),
            new IntegerModulusTestCase(-21,-10),

            new IntegerModulusTestCase(-1,100),
            new IntegerModulusTestCase(-99,100),
            new IntegerModulusTestCase(-100,100),
            new IntegerModulusTestCase(-101,100),
            new IntegerModulusTestCase(-199,100),
            new IntegerModulusTestCase(-200,100),
            new IntegerModulusTestCase(-201,100),
        };   //  static readonly IntegerModulusTestCase [ ] s_aintegerModulusTestCases


        static readonly LongModulusTestCase [ ] s_alongModulusTestCases = new LongModulusTestCase [ ]
        {
            new LongModulusTestCase(0,10),
            new LongModulusTestCase(1,10),
            new LongModulusTestCase(9,10),
            new LongModulusTestCase(10,10),
            new LongModulusTestCase(11,10),
            new LongModulusTestCase(19,10),
            new LongModulusTestCase(20,10),
            new LongModulusTestCase(21,10),

            new LongModulusTestCase(0,-10),
            new LongModulusTestCase(1,-10),
            new LongModulusTestCase(9,-10),
            new LongModulusTestCase(10,-10),
            new LongModulusTestCase(11,-10),
            new LongModulusTestCase(19,-10),
            new LongModulusTestCase(20,-10),
            new LongModulusTestCase(21,-10),

            new LongModulusTestCase(-1,-10),
            new LongModulusTestCase(-9,-10),
            new LongModulusTestCase(-10,-10),
            new LongModulusTestCase(-11,-10),
            new LongModulusTestCase(-19,-10),
            new LongModulusTestCase(-20,-10),
            new LongModulusTestCase(-21,-10),

            new LongModulusTestCase(1,100),
            new LongModulusTestCase(99,100),
            new LongModulusTestCase(100,100),
            new LongModulusTestCase(101,100),
            new LongModulusTestCase(199,100),
            new LongModulusTestCase(200,100),
            new LongModulusTestCase(201,100),

            new LongModulusTestCase(0,10000000000),
            new LongModulusTestCase(100000,10000000000),
            new LongModulusTestCase(9999999999,10000000000),
            new LongModulusTestCase(10000000000,10000000000),
            new LongModulusTestCase(10000000001,10000000000),
        };   //  static readonly IntegerModulusTestCase [ ] s_alongModulusTestCases

        static readonly int [ ] s_aintGregorianYearTests = new int [ ]
        {
            1492 ,
            1581 ,
            1582 ,
            1583 ,
            1599 ,
            1600 ,
            1601 ,
            1602 ,
            1603 ,
            1604 ,
            1605 ,
            1606 ,
            1607 ,
            1608 ,
            1609 ,
            1610 ,
            1611 ,
            1699 ,
            1700 ,
            1701 ,
            1702 ,
            1703 ,
            1704 ,
            1705 ,
            1706 ,
            1707 ,
            1708 ,
            1709 ,
            1710 ,
            1711 ,
            1799 ,
            1800 ,
            1801 ,
            1899 ,
            1900 ,
            1901 ,
            1999 ,
            2000 ,
            2001 ,
            2002 ,
            2003 ,
            2004 ,
            2005 ,
            2006 ,
            2007 ,
            2008 ,
            2009 ,
            2010 ,
            2011 ,
            2012 ,
            2013 ,
            2014 ,
            2015 ,
            2016 ,
            2017 ,
            2018 ,
            2019 ,
            2020 ,
            2021 ,
            2022 ,
            2063 ,
            2099 ,
            2100 ,
            2101
        };  // static readonly int [ ] s_aintGregorianYearTests


        public static void Run ( )
        {
            Console.WriteLine ( @"Begin tests of class WizardWrx.MoreMath (defined in WizardWrx.Core.dll):{0}" , Environment.NewLine );
            Console.WriteLine ( @"    Display public constants:{0}" , Environment.NewLine );
            Console.WriteLine ( @"        EXCEPTION_ON_INVALID_INPUT      = {0}" , MoreMath.EXCEPTION_ON_INVALID_INPUT );
            Console.WriteLine ( @"        GRGORIAN_CALENDAR_ADOPTION_YEAR = {0}" , Util.FormatIntegerValue ( MoreMath.GRGORIAN_CALENDAR_ADOPTION_YEAR ) );

            Console.WriteLine ( @"{0}    Test and demonstrate the IsEvenlyDivisibleByAnyInteger method.{0}" , Environment.NewLine );

            TestIntegerModulus ( );

            TestLongIntegerModulus ( );

            Console.WriteLine ( @"{0}    Test and demonstrate the IsGregorianLeapYear method.{0}" , Environment.NewLine );

            Console.WriteLine( @"        Test the default implementation, which throws an ArgumentOutOfRange exception when the input year is invalid.{0}" , Environment.NewLine );

            for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intIndex < s_aintGregorianYearTests.Length ;
                      intIndex++ )
            {   // Since the Gregorian year limit test is instructed to throw on an invalid year, the first iteration causes an exception.
                try
                {
                    Console.WriteLine (
                        "            Case {0,2} of {1,2}: {2,4} {3} a leap year." ,
                        ArrayInfo.OrdinalFromIndex ( intIndex ) ,               // Format Item 0: Case {0,2}
                        s_aintGregorianYearTests.Length ,                       // Format Item 1: of {1,2}:
                        s_aintGregorianYearTests[intIndex] ,                    // Format Item 2: : {2,4}
                        MoreMath.IsGregorianLeapYear (
                            s_aintGregorianYearTests [ intIndex ] )
                                ? "IS" :
                                "IS NOT" );                                     // Format Item 3: {3} a leap year.
                }
                catch ( Exception ex )
                {
                    WizardWrx.DLLConfigurationManager.ExceptionLogger.GetTheSingleInstance ( ).ReportException ( ex );
                }
            }   // for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < s_aintGregorianYearTests.Length ; intIndex++ )


            Console.WriteLine ( @"{0}        Test the implementation that returns FALSE when the input year is invalid.{0}" , Environment.NewLine );

            for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intIndex < s_aintGregorianYearTests.Length ;
                      intIndex++ )
            {   // Since the Gregorian year limit test is instructed to throw on an invalid year, the first iteration causes an exception.
                try
                {
                    Console.WriteLine (
                        "            Case {0,2} of {1,2}: {2,4} {3} a leap year." ,
                        ArrayInfo.OrdinalFromIndex ( intIndex ) ,               // Format Item 0: Case {0,2}
                        s_aintGregorianYearTests.Length ,                       // Format Item 1: of {1,2}:
                        s_aintGregorianYearTests [ intIndex ] ,                    // Format Item 2: : {2,4}
                        MoreMath.IsGregorianLeapYear (
                            s_aintGregorianYearTests [ intIndex ] ,
                            MoreMath.FALSE_ON_INVALID_INPUT )
                                ? "IS" :
                                "IS NOT" );                                     // Format Item 3: {3} a leap year.
                }
                catch ( Exception ex )
                {
                    WizardWrx.DLLConfigurationManager.ExceptionLogger.GetTheSingleInstance ( ).ReportException ( ex );
                }
            }   // for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < s_aintGregorianYearTests.Length ; intIndex++ )

            Console.WriteLine ( @"{0}Tests of class WizardWrx.MoreMath done{0}" , Environment.NewLine );
        }   // public static void Run

        private static void TestIntegerModulus ( )
        {
            Console.WriteLine ( @"        Standard (32 bit) integers:{0}" , Environment.NewLine );

            for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intIndex < s_aintegerModulusTestCases.Length ;
                      intIndex++ )
            {
                ShowAndTell (
                    intIndex ,
                    s_aintegerModulusTestCases [ intIndex ] );
            }   // for ( int intIndex=ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < s_integerModulusTestCases.Length ; intIndex++ )
        }   // private static void TestIntegerModulus

        private static void TestLongIntegerModulus ( )
        {
            Console.WriteLine ( @"{0}        Long (64 bit) integers:{0}" , Environment.NewLine );

            for ( int intIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intIndex < s_alongModulusTestCases.Length ;
                      intIndex++ )
            {
                ShowAndTell (
                    intIndex ,
                    s_alongModulusTestCases [ intIndex ] );
            }   // for ( int intIndex=ArrayInfo.ARRAY_FIRST_ELEMENT ; intIndex < s_longModulusTestCases.Length ; intIndex++ )
        }   // private static void TestLongIntegerModulu

        private static void ShowAndTell (
            int pintIndex ,
            IntegerModulusTestCase pintegerModulusTestCase )
        {
            Console.WriteLine (
                @"            Case {0,2}: Dividend         = {1}{3}                     Divisor          = {2}" ,
                ArrayInfo.OrdinalFromIndex ( pintIndex ) ,                              // Format Item 0: Case {0}:
                Util.FormatIntegerValue ( pintegerModulusTestCase.Dividend ) ,          // Format Item 1: Dividend = {1}
                Util.FormatIntegerValue ( pintegerModulusTestCase.Divisor ) ,           // Format Item 2: Divisor  = {2}
                Environment.NewLine );                                                  // Format Item 3: Platform-dependent newline
            Console.WriteLine (
                @"                     Evenly Divisible = {0}" ,
                MoreMath.IsEvenlyDivisibleByAnyInteger (
                    pintegerModulusTestCase.Dividend ,
                    pintegerModulusTestCase.Divisor ) );
        }   // private static void ShowAndTell (1 of 2)

        private static void ShowAndTell ( int pintIndex , LongModulusTestCase plongIntegerModulusTestCase )
        {
            Console.WriteLine (
                @"            Case {0,2}: Dividend         = {1}{3}                     Divisor          = {2}" ,
                ArrayInfo.OrdinalFromIndex ( pintIndex ) ,                              // Format Item 0: Case {0}:
                Util.FormatIntegerValue ( plongIntegerModulusTestCase.Dividend ) ,      // Format Item 1: Dividend = {1}
                Util.FormatIntegerValue ( plongIntegerModulusTestCase.Divisor ) ,       // Format Item 2: Divisor  = {2}
                Environment.NewLine );                                                  // Format Item 3: Platform-dependent newline
            Console.WriteLine (
                @"                     Evenly Divisible = {0}" ,
                MoreMath.IsEvenlyDivisibleByAnyInteger (
                    plongIntegerModulusTestCase.Dividend ,
                    plongIntegerModulusTestCase.Divisor ) );
        }   // private static void ShowAndTell (2 of 2)


        private class IntegerModulusTestCase
        {
            private IntegerModulusTestCase ( )
            { } // IntegerModulusTestCase (The default constructor is private to force callers to initialize all instances.)

            public IntegerModulusTestCase ( int pintDividend , int pintDivisor )
            {
                Dividend = pintDividend;
                Divisor = pintDivisor;
            }   // IntegerModulusTestCase (The public constructor takes two required arguments to initialize its properties.)

            public int Dividend { get; private set; }
            public int Divisor { get; private set; }
        }   // private class IntegerModulusTestCase

        private class LongModulusTestCase
        {
            private LongModulusTestCase ( )
            { } // LongModulusTestCase (The default constructor is private to force callers to initialize all instances.)

            public LongModulusTestCase ( long plngDividend , long plngDivisor )
            {
                Dividend = plngDividend;
                Divisor = plngDivisor;
            }   // LongModulusTestCase (The public constructor takes two required arguments to initialize its properties.)

            public long Dividend { get; private set; }
            public long Divisor { get; private set; }
        }   // private class LongModulusTestCase
    }   // class MathTests
}   // partial amespace DLLServices2TestStand