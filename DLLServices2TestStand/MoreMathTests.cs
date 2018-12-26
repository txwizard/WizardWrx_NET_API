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

    2018/11/23 7.12    DAG    Extend to cover the new Mod and Remainder methods.

	2018/12/24 7.14    DAG    Define the DecimalShift function, syntactic sugar
                              that performs decimal shift operations on integers
                              and floating point numbers of various sizes.
	============================================================================
*/

using System;

using WizardWrx;

namespace DLLServices2TestStand
{
    static class MoreMathTests
    {
        static readonly WizardWrx.DLLConfigurationManager.ExceptionLogger s_exceptionLogger = WizardWrx.DLLConfigurationManager.ExceptionLogger.GetTheSingleInstance (
            WizardWrx.DLLConfigurationManager.ExceptionLogger.OutputOptions.AllFlags );


        #region DecimalShiftLeft Test Case Arrays
        static readonly IntegerDecimalShiftLeftTestCase [ ] s_aintegerDecimalShifttLeftTestCases = new IntegerDecimalShiftLeftTestCase [ ]
        {
            new IntegerDecimalShiftLeftTestCase(123,2,12300),
            new IntegerDecimalShiftLeftTestCase(123,1,1230),
            new IntegerDecimalShiftLeftTestCase(123,4,1230000),
            new IntegerDecimalShiftLeftTestCase(123,6,123000000),
        };  // static readonly IntegerDecimalShifttLeftTestCase [ ] s_aintegerDecimalShifttLeftTestCases

        static readonly UnsignedIntegerDecimalShiftLeftTestCase [ ] s_auintegerDecimalShifttLeftTestCases = new UnsignedIntegerDecimalShiftLeftTestCase [ ]
        {
            new UnsignedIntegerDecimalShiftLeftTestCase(123,1,1230),
            new UnsignedIntegerDecimalShiftLeftTestCase(123,2,12300),
            new UnsignedIntegerDecimalShiftLeftTestCase(123,3,123000),
            new UnsignedIntegerDecimalShiftLeftTestCase(123,4,1230000),
            new UnsignedIntegerDecimalShiftLeftTestCase(123,5,12300000),
            new UnsignedIntegerDecimalShiftLeftTestCase(123,6,123000000),
            new UnsignedIntegerDecimalShiftLeftTestCase(123,7,1230000000),
        };  // static readonly UnsignedIntegerDecimalShifttLeftTestCase [ ] s_auintegerDecimalShifttLeftTestCases

        static readonly LongIntegerDecimalShiftLeftTestCase [ ] s_alongDecimalShifttLeftTestCases = new LongIntegerDecimalShiftLeftTestCase [ ]
        {
            new LongIntegerDecimalShiftLeftTestCase(123,1,1230),
            new LongIntegerDecimalShiftLeftTestCase(123,2,12300),
            new LongIntegerDecimalShiftLeftTestCase(123,3,123000),
            new LongIntegerDecimalShiftLeftTestCase(123,4,1230000),
            new LongIntegerDecimalShiftLeftTestCase(123,5,12300000),
            new LongIntegerDecimalShiftLeftTestCase(123,6,123000000),
            new LongIntegerDecimalShiftLeftTestCase(123,7,1230000000),
            new LongIntegerDecimalShiftLeftTestCase(123,12,123000000000000),
            new LongIntegerDecimalShiftLeftTestCase(123,14,12300000000000000),
        };  // static readonly LongIntegerDecimalShifttLeftTestCase [ ] s_alongDecimalShifttLeftTestCases

        static readonly UnsignedLongIntegerDecimalShiftLeftTestCase [ ] s_aulongDecimalShifttLeftTestCases = new UnsignedLongIntegerDecimalShiftLeftTestCase [ ]
        {
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,1,1230),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,2,12300),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,3,123000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,4,1230000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,5,12300000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,6,123000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,7,1230000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,8,12300000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,9,123000000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,10,1230000000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,11,12300000000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,12,123000000000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,13,1230000000000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,14,12300000000000000),
            new UnsignedLongIntegerDecimalShiftLeftTestCase(123,15,123000000000000000),
        };  // static readonly UnsignedLongIntegerDecimalShifttLeftTestCase [ ] s_aulongDecimalShifttLeftTestCases
        #endregion  // DecimalShiftLeft Test Case Arrays


        #region DecimalShiftRight Test Case Arrays
        static readonly IntegerDecimalShiftRightTestCase [ ] s_aintDecimalShiftRightTestCases = new IntegerDecimalShiftRightTestCase [ ]
        {
           new IntegerDecimalShiftRightTestCase(12345000,2,123450),
           new IntegerDecimalShiftRightTestCase(12345000,3,12345),
           new IntegerDecimalShiftRightTestCase(12345000,4,1234),
        };  // static readonly IntegerDecimalShiftRightTestCase [ ] s_aintDecimalShiftRightTestCases
        #endregion  // DecimalShiftRight Test Case Arrays



        #region IntegerModulus Test Case Arrays
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

            new IntegerModulusTestCase(25,0),
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

            new LongModulusTestCase(25,0),
        };   //  static readonly IntegerModulusTestCase [ ] s_alongModulusTestCases
        #endregion  // IntegerModulus Test Case Arrays


        #region Gregorian Year Test Case Arrays
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
        #endregion  // Gregorian Year Test Case Arrays


        #region Public Static Test Runner
        public static void Run ( )
        {
            Console.WriteLine (
                @"Begin tests of class WizardWrx.MoreMath (defined in WizardWrx.Core.dll):{0}" ,
                Environment.NewLine );
            Console.WriteLine (
                @"    Display public constants:{0}" ,
                Environment.NewLine );
            Console.WriteLine (
                @"        EXCEPTION_ON_INVALID_INPUT      = {0}" ,
                MoreMath.EXCEPTION_ON_INVALID_INPUT );
            Console.WriteLine (
                @"        GRGORIAN_CALENDAR_ADOPTION_YEAR = {0}" ,
                Util.FormatIntegerValue (
                    MoreMath.GRGORIAN_CALENDAR_ADOPTION_YEAR ) );

            TestDecimalShift ( );

            TestIntegerModulus ( );

            TestLongIntegerModulus ( );

            Console.WriteLine (
                @"{0}    Test and demonstrate the IsGregorianLeapYear method.{0}" ,
                Environment.NewLine );

            Console.WriteLine
                ( @"        Test the default implementation, which throws an ArgumentOutOfRange exception when the input year is invalid.{0}" ,
                Environment.NewLine );

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
                        s_aintGregorianYearTests [ intIndex ] ,                 // Format Item 2: : {2,4}
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

            Console.WriteLine (
                @"{0}Tests of class WizardWrx.MoreMath done{0}" ,
                Environment.NewLine );
        }   // public static void Run
        #endregion  // Public Static Test Runner


        #region DecimalShift Test Routines
        private static void TestDecimalShift ( )
        {
            Console.WriteLine (
                @"{0}    Test and demonstrate the DecimalShiftLeft method.{0}" ,
                Environment.NewLine );

            {   // Constrain the scope of IntegerDecimalShifttLeftTestCase variable dummy.
                IntegerDecimalShiftLeftTestCase dummy = IntegerDecimalShiftLeftTestCase.GetDummyInstance ( );
                dummy.UnitTests ( s_aintegerDecimalShifttLeftTestCases );
            }   // Let dummy go out of scope.

            {   // Constrain the scope of UnsignedIntegerDecimalShifttLeftTestCase variable dummy.
                UnsignedIntegerDecimalShiftLeftTestCase dummy = UnsignedIntegerDecimalShiftLeftTestCase.GetDummyInstance ( );
                dummy.UnitTests ( s_auintegerDecimalShifttLeftTestCases );
            }   // Let dummy go out of scope.

            {   // Constrain the scope of LongIntegerDecimalShifttLeftTestCase variable dummy.
                LongIntegerDecimalShiftLeftTestCase dummy = LongIntegerDecimalShiftLeftTestCase.GetDummyInstance ( );
                dummy.UnitTests ( s_alongDecimalShifttLeftTestCases );
            }   // Let dummy go out of scope.

            {   // Constrain the scope of UnsignedLongIntegerDecimalShifttLeftTestCase variable dummy.
                UnsignedLongIntegerDecimalShiftLeftTestCase dummy = UnsignedLongIntegerDecimalShiftLeftTestCase.GetDummyInstance ( );
                dummy.UnitTests ( s_aulongDecimalShifttLeftTestCases );
            }   // Let dummy go out of scope.

            {   // Constrain the scope of UnsignedLongIntegerDecimalShifttLeftTestCase variable dummy.
                IntegerDecimalShiftRightTestCase dummy = IntegerDecimalShiftRightTestCase.GetDummyInstance ( );
                dummy.UnitTests ( s_aintDecimalShiftRightTestCases );
            }   // Let dummy go out of scope.

            Console.WriteLine (
                @"    DecimalShiftLeft method testing completed.{0}" ,
                Environment.NewLine );
        }   // private static void TestDecimalShift
        #endregion  // DecimalShift Test Routines


        #region IntegerModulus Test Routines
        private static void TestIntegerModulus ( )
        {
            Console.WriteLine (
                @"        Standard (32 bit) integers:{0}" ,
                Environment.NewLine );

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
        }   // private static void TestLongIntegerModulus


        private static void ShowAndTell (
            int pintIndex ,
            IntegerModulusTestCase pintegerModulusTestCase )
        {
            Console.WriteLine (
                @"            Case {0,2}: Dividend            = {1}{3}                     Divisor             = {2}" ,
                ArrayInfo.OrdinalFromIndex ( pintIndex ) ,                              // Format Item 0: Case {0}:
                Util.FormatIntegerValue ( pintegerModulusTestCase.Dividend ) ,          // Format Item 1: Dividend = {1}
                Util.FormatIntegerValue ( pintegerModulusTestCase.Divisor ) ,           // Format Item 2: Divisor  = {2}
                Environment.NewLine );                                                  // Format Item 3: Platform-dependent newline

            try
            {
                Console.WriteLine (
                    @"                     Evenly Divisible    = {0}" ,
                    MoreMath.IsEvenlyDivisibleByAnyInteger (
                        pintegerModulusTestCase.Dividend ,
                        pintegerModulusTestCase.Divisor ) );
            }
            catch ( ArgumentException exBadArg )
            {
                s_exceptionLogger.ReportException ( exBadArg );
            }

            try
            {
                Console.WriteLine (
                    @"                     Mod (Modulus)       = {0}" ,
                    MoreMath.Mod (
                        pintegerModulusTestCase.Dividend ,
                        pintegerModulusTestCase.Divisor ) );
            }
            catch ( ArgumentException exBadArg )
            {
                s_exceptionLogger.ReportException ( exBadArg );
            }

            try
            {
                Console.WriteLine (
                    @"                     Remainder (Modulus) = {0}" ,
                    MoreMath.Remainder (
                        pintegerModulusTestCase.Dividend ,
                        pintegerModulusTestCase.Divisor ) );
            }
            catch ( ArgumentException exBadArg )
            {
                s_exceptionLogger.ReportException ( exBadArg );
            }

            Console.WriteLine (
                @"            Case {0,2} Done{1}" ,
                ArrayInfo.OrdinalFromIndex ( pintIndex ) ,  // Format Item 0: Case {0}:
                Environment.NewLine );                      // Format Item 1: Platform-dependent newline
        }   // private static void ShowAndTell (1 of 2)


        private static void ShowAndTell (
            int pintIndex ,
            LongModulusTestCase plongIntegerModulusTestCase )
        {
            Console.WriteLine (
                @"            Case {0,2}: Dividend            = {1}{3}                     Divisor             = {2}" ,
                ArrayInfo.OrdinalFromIndex ( pintIndex ) ,                              // Format Item 0: Case {0}:
                Util.FormatIntegerValue ( plongIntegerModulusTestCase.Dividend ) ,      // Format Item 1: Dividend = {1}
                Util.FormatIntegerValue ( plongIntegerModulusTestCase.Divisor ) ,       // Format Item 2: Divisor  = {2}
                Environment.NewLine );                                                  // Format Item 3: Platform-dependent newline

            try
            {
                Console.WriteLine (
                    @"                     Evenly Divisible    = {0}" ,
                    MoreMath.IsEvenlyDivisibleByAnyInteger (
                        plongIntegerModulusTestCase.Dividend ,
                        plongIntegerModulusTestCase.Divisor ) );
            }
            catch ( ArgumentException exBadArg )
            {
                s_exceptionLogger.ReportException ( exBadArg );
            }

            try
            {
                Console.WriteLine (
                    @"                     Mod (Modulus)       = {0}" ,
                    MoreMath.Mod (
                        plongIntegerModulusTestCase.Dividend ,
                        plongIntegerModulusTestCase.Divisor ) );
            }
            catch ( ArgumentException exBadArg )
            {
                s_exceptionLogger.ReportException ( exBadArg );
            }

            try
            {
                Console.WriteLine (
                    @"                     Remainder (Modulus) = {0}" ,
                    MoreMath.Remainder (
                        plongIntegerModulusTestCase.Dividend ,
                        plongIntegerModulusTestCase.Divisor ) );
            }
            catch ( ArgumentException exBadArg )
            {
                s_exceptionLogger.ReportException ( exBadArg );
            }

            Console.WriteLine (
                @"            Case {0,2} Done{1}" ,
                ArrayInfo.OrdinalFromIndex ( pintIndex ) ,  // Format Item 0: Case {0}:
                Environment.NewLine );                      // Format Item 1: Platform-dependent newline
        }   // private static void ShowAndTell (2 of 2)
        #endregion  // IntegerModulus Test Routines


        #region DecimalShiftTestCase Abstraction (Template) Class
        private abstract class DecimalShiftTestCase<T>
        {
            protected DecimalShiftTestCase ( )
            {
            }   // private DecimalShiftLeftTestCase constructor (The default constructor is private to force callers to initialize all instances.)

            public DecimalShiftTestCase ( T inputValue , int shiftValue , T expectedOutcome )
            {
                InputValue = inputValue;
                ShiftValue = shiftValue;
                ExpectedOutcome = expectedOutcome;
            }   // // public DecimalShiftLeftTestCase constructor (The public constructor takes two required arguments to initialize its properties.)

            public T InputValue { get; private set; }
            public int ShiftValue { get; private set; }

            public T ExpectedOutcome { get; private set; }


            /// <summary>
            /// Exercise one of the DecimalShiftLeft methods.
            /// </summary>
            /// <param name="pautpDecimalShifttLeftTestCases">
            /// Pass in a reference to an array of DecimalShiftTestCase 
            /// instances of type T, each of which is a pair of input parameters
            /// and an expected outcome.
            /// </param>
            /// <typeparam name="T">
            /// Strictly speaking, the type is unconstrained, although I could
            /// construct something along the lines of the runtime sanity tests
            /// that I did for the hexadecimal integer formatter.
            /// </typeparam>
            public abstract void UnitTests ( DecimalShiftTestCase<T> [ ] pautpDecimalShiftTestCases );
        }   // private class DecimalShiftLeftTestCase
        #endregion  // DecimalShiftTestCase Abstraction (Template) Class


        #region DecimalShifttLeftTestCase Classes
        private class IntegerDecimalShiftLeftTestCase : DecimalShiftTestCase<int>
        {
            private IntegerDecimalShiftLeftTestCase ( )
            { }   // private IntegerDecimalShiftLeftTestCase constructor (The default constructor is private to force callers to initialize all instances.)

            public IntegerDecimalShiftLeftTestCase (
                int inputValue ,
                int shiftValue ,
                int expectedOutcome )
               : base (
                     inputValue ,
                     shiftValue ,
                     expectedOutcome )
            { } // Classes don't inherit overloaded constructors.

            /// <summary>
            /// Exercise one of the DecimalShiftLeft methods.
            /// </summary>
            /// <param name="pautpDecimalShiftLeftTestCases">
            /// Pass in a reference to an array of DecimalShifttLeftTestCase
            /// instances of type int, each of which is a pair of input
            /// parameters and an expected outcome.
            /// </param>
            public override void UnitTests ( DecimalShiftTestCase<int> [ ] pautpDecimalShiftLeftTestCases )
            {
                Console.WriteLine (
                    @"    Exercise MoreMath.DecimalShiftLeft method for inputs of type {0}.{1}" ,
                    this.InputValue.GetType().FullName ,
                    Environment.NewLine );

                for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intCaseIndex < pautpDecimalShiftLeftTestCases.Length ;
                          intCaseIndex++ )
                {
                    Console.WriteLine ( @"        Case {0,2}: Input Value      = {1}" , ArrayInfo.OrdinalFromIndex ( intCaseIndex ) , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ) );
                    Console.WriteLine ( @"                 Shift Value      = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue ) );
                    Console.WriteLine ( @"                 Expected Outcome = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome ) );

                    int intShifted = MagicNumbers.ZERO;

                    try
                    {
                        intShifted = MoreMath.DecimalShiftLeft (
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ,
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue );
                    }
                    catch ( Exception exAll )
                    {
                        s_exceptionLogger.ReportException ( exAll );
                    }
                    finally
                    {
                        Console.WriteLine (
                            @"                 Case {0,2} done" ,
                            ArrayInfo.OrdinalFromIndex ( intCaseIndex ) );
                    }

                    string strOutcomeMessage =
                        intShifted.Equals ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                        ? @"Passed"
                        : @"Failed";

                    Console.WriteLine (
                        @"                 Actual Outcome   = {0}{1}{2}" ,
                        strOutcomeMessage ,                 // Format Item 0
                        intShifted.Equals (                 // Format Item 1
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                            ? SpecialStrings.EMPTY_STRING
                            : string.Format (
                                @" [actual outcome = {0}]" ,
                                Util.FormatIntegerValue (
                                    intShifted ) ) ,
                        Environment.NewLine );              // Format Item 2
                }   // for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCaseIndex < pautpDecimalShiftLeftTestCases.Length ; intCaseIndex++ )
            }   // public override void UnitTests

            /// <summary>
            /// Grant controlled access to a dummy instance, not to be used for
            /// testing, but to provide access to overridden abstract methods.
            /// </summary>
            /// <returns>
            /// The return value is an uninitialized DecimalShifttLeftTestCase of
            /// type int, suitable only for gaining access to its methods.
            /// </returns>
            public static IntegerDecimalShiftLeftTestCase GetDummyInstance ( )
            {
                return new IntegerDecimalShiftLeftTestCase ( );
            }   // public override DecimalShifttLeftTestCase<int> GetDummyInstance
        }   // private class IntegerDecimalShiftLeftTestCase


        private class UnsignedIntegerDecimalShiftLeftTestCase : DecimalShiftTestCase<uint>
        {
            private UnsignedIntegerDecimalShiftLeftTestCase ( )
            { }   // private UnsignedIntegerDecimalShifttLeftTestCase constructor (The default constructor is private to force callers to initialize all instances.)

            public UnsignedIntegerDecimalShiftLeftTestCase (
                uint inputValue ,
                int shiftValue ,
                uint expectedOutcome )
               : base (
                     inputValue ,
                     shiftValue ,
                     expectedOutcome )
            { }   // public UnsignedIntegerDecimalShifttLeftTestCase constructor (The public constructor takes two required arguments to initialize its properties.)

            /// <summary>
            /// Exercise one of the DecimalShiftLeft methods.
            /// </summary>
            /// <param name="pautpDecimalShiftLeftTestCases">
            /// Pass in a reference to an array of DecimalShifttLeftTestCase instances
            /// of type uint, each of which is a pair of input parameters and an
            /// expected outcome.
            /// </param>
            public override void UnitTests ( DecimalShiftTestCase<uint> [ ] pautpDecimalShiftLeftTestCases )
            {
                Console.WriteLine (
                    @"    Exercise MoreMath.DecimalShiftLeft method for inputs of type {0}.{1}" ,
                    this.InputValue.GetType ( ).FullName ,
                    Environment.NewLine );

                for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intCaseIndex < pautpDecimalShiftLeftTestCases.Length ;
                          intCaseIndex++ )
                {
                    Console.WriteLine ( @"        Case {0,2}: Input Value      = {1}" , ArrayInfo.OrdinalFromIndex ( intCaseIndex ) , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ) );
                    Console.WriteLine ( @"                 Shift Value      = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue ) );
                    Console.WriteLine ( @"                 Expected Outcome = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome ) );

                    uint intShifted = MagicNumbers.ZERO;

                    try
                    {
                        intShifted = MoreMath.DecimalShiftLeft (
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ,
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue );
                    }
                    catch ( Exception exAll )
                    {
                        s_exceptionLogger.ReportException ( exAll );
                    }
                    finally
                    {
                        Console.WriteLine (
                            @"                 Case {0,2} done" ,
                            ArrayInfo.OrdinalFromIndex ( intCaseIndex ) );
                    }

                    string strOutcomeMessage =
                        intShifted.Equals ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                        ? @"Passed"
                        : @"Failed";

                    Console.WriteLine (
                        @"                 Actual Outcome   = {0}{1}{2}" ,
                        strOutcomeMessage ,                 // Format Item 0
                        intShifted.Equals (                 // Format Item 1
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                            ? SpecialStrings.EMPTY_STRING
                            : string.Format (
                                @" [actual outcome = {0}]" ,
                                Util.FormatIntegerValue (
                                    intShifted ) ) ,
                        Environment.NewLine );              // Format Item 2
                }   // for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCaseIndex < pautpDecimalShiftLeftTestCases.Length ; intCaseIndex++ )
            }   // public override void UnitTests

            /// <summary>
            /// Grant controlled access to a dummy instance, not to be used for
            /// testing, but to provide access to overridden abstract methods.
            /// </summary>
            /// <returns>
            /// The return value is an uninitialized DecimalShifttLeftTestCase of
            /// type uint, suitable only for gaining access to its methods.
            /// </returns>
            public static UnsignedIntegerDecimalShiftLeftTestCase GetDummyInstance ( )
            {
                return new UnsignedIntegerDecimalShiftLeftTestCase ( );
            }   // public override DecimalShifttLeftTestCase<int> GetDummyInstance
        }   // private class UnsignedIntegerDecimalShiftLeftTestCase


        private class LongIntegerDecimalShiftLeftTestCase : DecimalShiftTestCase<long>
        {
            private LongIntegerDecimalShiftLeftTestCase ( )
            { } // private LongIntegerDecimalShiftLeftTestCase constructor (The default constructor is private to force callers to initialize all instances.)

            public LongIntegerDecimalShiftLeftTestCase (
                long inputValue ,
                int shiftValue ,
                long expectedOutcome )
               : base (
                     inputValue ,
                     shiftValue ,
                     expectedOutcome )
            { }   // public LongIntegerDecimalShiftLeftTestCase constructor (The public constructor takes two required arguments to initialize its properties.)

            /// <summary>
            /// Exercise one of the DecimalShiftLeft methods.
            /// </summary>
            /// <param name="pautpDecimalShiftLeftTestCases">
            /// Pass in a reference to an array of DecimalShifttLeftTestCase instances
            /// of type long, each of which is a pair of input parameters and an
            /// expected outcome.
            /// </param>
            public override void UnitTests ( DecimalShiftTestCase<long> [ ] pautpDecimalShiftLeftTestCases )
            {
                Console.WriteLine (
                    @"    Exercise MoreMath.DecimalShiftLeft method for inputs of type {0}.{1}" ,
                    this.InputValue.GetType ( ).FullName ,
                    Environment.NewLine );

                for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intCaseIndex < pautpDecimalShiftLeftTestCases.Length ;
                          intCaseIndex++ )
                {
                    Console.WriteLine ( @"        Case {0,2}: Input Value      = {1}" , ArrayInfo.OrdinalFromIndex ( intCaseIndex ) , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ) );
                    Console.WriteLine ( @"                 Shift Value      = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue ) );
                    Console.WriteLine ( @"                 Expected Outcome = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome ) );

                    long intShifted = MagicNumbers.ZERO;

                    try
                    {
                        intShifted = MoreMath.DecimalShiftLeft (
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ,
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue );
                    }
                    catch ( Exception exAll )
                    {
                        s_exceptionLogger.ReportException ( exAll );
                    }
                    finally
                    {
                        Console.WriteLine (
                            @"                 Case {0,2} done" ,
                            ArrayInfo.OrdinalFromIndex ( intCaseIndex ) );
                    }

                    string strOutcomeMessage =
                        intShifted.Equals ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                        ? @"Passed"
                        : @"Failed";

                    Console.WriteLine (
                        @"                 Actual Outcome   = {0}{1}{2}" ,
                        strOutcomeMessage ,                 // Format Item 0
                        intShifted.Equals (                 // Format Item 1
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                            ? SpecialStrings.EMPTY_STRING
                            : string.Format (
                                @" [actual outcome = {0}]" ,
                                Util.FormatIntegerValue (
                                    intShifted ) ) ,
                        Environment.NewLine );              // Format Item 2
                }   // for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCaseIndex < pautpDecimalShiftLeftTestCases.Length ; intCaseIndex++ )
            }   // public override void UnitTests

            /// <summary>
            /// Grant controlled access to a dummy instance, not to be used for
            /// testing, but to provide access to overridden abstract methods.
            /// </summary>
            /// <returns>
            /// The return value is an uninitialized DecimalShifttLeftTestCase of
            /// type long, suitable only for gaining access to its methods.
            /// </returns>
            public static LongIntegerDecimalShiftLeftTestCase GetDummyInstance ( )
            {
                return new LongIntegerDecimalShiftLeftTestCase ( );
            }   // public override DecimalShiftLeftTestCase<int> GetDummyInstance
        }   // private class LongIntegerDecimalShiftLeftTestCase


        private class UnsignedLongIntegerDecimalShiftLeftTestCase : DecimalShiftTestCase<ulong>
        {
            private UnsignedLongIntegerDecimalShiftLeftTestCase ( )
            { } // private LongIntegerDecimalShifttLeftTestCase constructor (The default constructor is private to force callers to initialize all instances.)

            public UnsignedLongIntegerDecimalShiftLeftTestCase (
                ulong inputValue ,
                int shiftValue ,
                ulong expectedOutcome )
               : base (
                     inputValue ,
                     shiftValue ,
                     expectedOutcome )
            { }   // public UnsignedLongIntegerDecimalShifttLeftTestCase constructor (The public constructor takes two required arguments to initialize its properties.)

            /// <summary>
            /// Exercise one of the DecimalShiftLeft methods.
            /// </summary>
            /// <param name="pautpDecimalShiftLeftTestCases">
            /// Pass in a reference to an array of DecimalShifttLeftTestCase instances
            /// of type ulong, each of which is a pair of input parameters and an
            /// expected outcome.
            /// </param>
            public override void UnitTests ( DecimalShiftTestCase<ulong> [ ] pautpDecimalShiftLeftTestCases )
            {
                Console.WriteLine (
                    @"    Exercise MoreMath.DecimalShiftLeft method for inputs of type {0}.{1}" ,
                    this.InputValue.GetType ( ).FullName ,
                    Environment.NewLine );

                for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intCaseIndex < pautpDecimalShiftLeftTestCases.Length ;
                          intCaseIndex++ )
                {
                    Console.WriteLine ( @"        Case {0,2}: Input Value      = {1}" , ArrayInfo.OrdinalFromIndex ( intCaseIndex ) , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ) );
                    Console.WriteLine ( @"                 Shift Value      = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue ) );
                    Console.WriteLine ( @"                 Expected Outcome = {0}" , Util.FormatIntegerValue ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome ) );

                    ulong intShifted = MagicNumbers.ZERO;

                    try
                    {
                        intShifted = MoreMath.DecimalShiftLeft (
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].InputValue ,
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ShiftValue );
                    }
                    catch ( Exception exAll )
                    {
                        s_exceptionLogger.ReportException ( exAll );
                    }
                    finally
                    {
                        Console.WriteLine (
                            @"                 Case {0,2} done" ,
                            ArrayInfo.OrdinalFromIndex ( intCaseIndex ) );
                    }

                    string strOutcomeMessage =
                        intShifted.Equals ( pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                        ? @"Passed"
                        : @"Failed";

                    Console.WriteLine (
                        @"                 Actual Outcome   = {0}{1}{2}" ,
                        strOutcomeMessage ,                 // Format Item 0
                        intShifted.Equals (                 // Format Item 1
                            pautpDecimalShiftLeftTestCases [ intCaseIndex ].ExpectedOutcome )
                            ? SpecialStrings.EMPTY_STRING
                            : string.Format (
                                @" [actual outcome = {0}]" ,
                                Util.FormatIntegerValue (
                                    intShifted ) ) ,
                        Environment.NewLine );              // Format Item 2
                }   // for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCaseIndex < pautpDecimalShiftLeftTestCases.Length ; intCaseIndex++ )
            }   // public override void UnitTests

            /// <summary>
            /// Grant controlled access to a dummy instance, not to be used for
            /// testing, but to provide access to overridden abstract methods.
            /// </summary>
            /// <returns>
            /// The return value is an uninitialized DecimalShifttLeftTestCase of
            /// type ulong, suitable only for gaining access to its methods.
            /// </returns>
            public static UnsignedLongIntegerDecimalShiftLeftTestCase GetDummyInstance ( )
            {
                return new UnsignedLongIntegerDecimalShiftLeftTestCase ( );
            }   // public override DecimalShifttLeftTestCase<int> GetDummyInstance
        }   // private class UnsignedLongIntegerDecimalShifttLeftTestCase
        #endregion  // DecimalShiftLeftTestCase Classes


        #region DecimalShiftRightTestCase Classes
        // ToDo: Duplicate these for uint, long, ulong, float, double, and decimal.
        private class IntegerDecimalShiftRightTestCase : DecimalShiftTestCase<int>
        {
            private IntegerDecimalShiftRightTestCase ( )
            { }   // private IntegerDecimalShifttLeftTestCase constructor (The default constructor is private to force callers to initialize all instances.)

            public IntegerDecimalShiftRightTestCase (
                int inputValue ,
                int shiftValue ,
                int expectedOutcome )
               : base (
                     inputValue ,
                     shiftValue ,
                     expectedOutcome )
            { } // Classes don't inherit overloaded constructors.

            /// <summary>
            /// Exercise one of the DecimalShiftRight methods.
            /// </summary>
            /// <param name="pautpDecimalShiftLeftTestCases">
            /// Pass in a reference to an array of DecimalShiftTestCase instances
            /// of type int, each of which is a pair of input parameters and an
            /// expected outcome.
            /// </param>
            public override void UnitTests ( DecimalShiftTestCase<int> [ ] pautpDecimalShifRightTestCases )
            {
                Console.WriteLine (
                    @"    Exercise MoreMath.DecimalShiftRight method for inputs of type {0}.{1}" ,
                    this.InputValue.GetType ( ).FullName ,
                    Environment.NewLine );

                for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intCaseIndex < pautpDecimalShifRightTestCases.Length ;
                          intCaseIndex++ )
                {
                    Console.WriteLine ( @"        Case {0,2}: Input Value      = {1}" , ArrayInfo.OrdinalFromIndex ( intCaseIndex ) , Util.FormatIntegerValue ( pautpDecimalShifRightTestCases [ intCaseIndex ].InputValue ) );
                    Console.WriteLine ( @"                 Shift Value      = {0}" , Util.FormatIntegerValue ( pautpDecimalShifRightTestCases [ intCaseIndex ].ShiftValue ) );
                    Console.WriteLine ( @"                 Expected Outcome = {0}" , Util.FormatIntegerValue ( pautpDecimalShifRightTestCases [ intCaseIndex ].ExpectedOutcome ) );

                    int intShifted = MagicNumbers.ZERO;

                    try
                    {
                        intShifted = MoreMath.DecimalShiftRight (
                            pautpDecimalShifRightTestCases [ intCaseIndex ].InputValue ,
                            pautpDecimalShifRightTestCases [ intCaseIndex ].ShiftValue );
                    }
                    catch ( Exception exAll )
                    {
                        s_exceptionLogger.ReportException ( exAll );
                    }
                    finally
                    {
                        Console.WriteLine (
                            @"                 Case {0,2} done" ,
                            ArrayInfo.OrdinalFromIndex ( intCaseIndex ) );
                    }

                    string strOutcomeMessage =
                        intShifted.Equals ( pautpDecimalShifRightTestCases [ intCaseIndex ].ExpectedOutcome )
                        ? @"Passed"
                        : @"Failed";

                    Console.WriteLine (
                        @"                 Actual Outcome   = {0}{1}{2}" ,
                        strOutcomeMessage ,                 // Format Item 0
                        intShifted.Equals (                 // Format Item 1
                            pautpDecimalShifRightTestCases [ intCaseIndex ].ExpectedOutcome )
                            ? SpecialStrings.EMPTY_STRING
                            : string.Format (
                                @" [actual outcome = {0}]" ,
                                Util.FormatIntegerValue (
                                    intShifted ) ) ,
                        Environment.NewLine );              // Format Item 2
                }   // for ( int intCaseIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intCaseIndex < pautpDecimalShiftLeftTestCases.Length ; intCaseIndex++ )
            }   // public override void UnitTests

            /// <summary>
            /// Grant controlled access to a dummy instance, not to be used for
            /// testing, but to provide access to overridden abstract methods.
            /// </summary>
            /// <returns>
            /// The return value is an uninitialized DecimalShiftTestCase of
            /// type int, suitable only for gaining access to its methods.
            /// </returns>
            public static IntegerDecimalShiftRightTestCase GetDummyInstance ( )
            {
                return new IntegerDecimalShiftRightTestCase ( );
            }   // public override IntegerDecimalShiftRightTestCase<int> GetDummyInstance
        }   // private class IntegerDecimalShiftRightTestCase
        #endregion  // DecimalShiftRightTestCase Classes


        #region IntegerModulusTestCase Classes
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
        #endregion  // IntegerModulusTestCase Classes
    }   // class MathTests
}   // partial amespace DLLServices2TestStand