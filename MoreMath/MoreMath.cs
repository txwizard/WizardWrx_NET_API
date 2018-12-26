/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         MoreMath

    File Name:          MoreMath.cs

    Synopsis:           This sealed class is a container for utility routines to
                        perform seldom-needed and somewhat obscure mathematical
                        operations.

    Remarks:            To disambiguate its name, this class is called MoreMath,
                        because the System namespace in the Base Class Library
                        defines a class called Math.

                        All but one of the initial members of this class are
                        ports of routines that I originally implemented as part
                        of static class PureDate, which belonged to deprecated
                        assembly WizardWrx.DateMath.dll. This port incorporates
                        lessons learned about library design and documentation
                        learned in the ensuing decade.

                        Since this class is pure C#, and uses only fundamental
                        value types, it is devoid of using directives other than
                        the ubiqutous System namespace, which includes the core
                        Exception classes.

    Author:             David A. Gray

    License:            Copyright (C) 2008-2018, David A. Gray. 
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

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2018/11/12 7.11    DAG    This class makes its debut, with the following:
                              
                              1) IsEvenlyDivisibleByAnyInteger, defined twice,
                                 to accept integer and long inputs.

                              2) IsGregorianLeapYear implements the Gregorian
                                 leap year algorithm.

                              3) IsValidGregorianYear returns TRUE if given a
                                 number that is a valid year in the Gregorian
                                 calendar.

	2018/11/23 7.12    DAG    Define the Mod function, the logical companion to
                              IsEvenlyDivisibleByAnyInteger, as syntactic sugar,
                              and Remainder, analogous to the IEEERemainder Math
                              method, and change IsEvenlyDivisibleByAnyInteger
                              to raise an ArgumentException when called with its
                              divisor set to zero. The Remainder synonymn covers
                              for users who recognize IEEERemainder in the Math
                              class in the BCL.

	2018/12/24 7.14    DAG    Define the DecimalShift function, syntactic sugar
                              that performs decimal shift operations, and move
                              this class into a dedicated library.
    ============================================================================
*/

using System;

using WizardWrx.Properties;

namespace WizardWrx
{
    /// <summary>
    /// This static class exposes methods that perform a variety of infrequently
    /// used, but technically obscure or deceptively tricky mathematical
    /// computations.
    /// </summary>
    public static class MoreMath
    {
        #region Public Constants for use with IsGregorianLeapYear method
        /// <summary>
        /// Use with IsGregorianLeapYear and IsValidGregorianYear to cause them
        /// to throw an exception when either is fed an invalid Gregorian year,
        /// rather than return FALSE.
        /// </summary>
        public const bool EXCEPTION_ON_INVALID_INPUT = true;

        /// <summary>
        /// Use with IsGregorianLeapYear and IsValidGregorianYear to cause them
        /// to suppress the ArgumentOutOfRange exception when either is fed an
        /// invalid Gregorian year; instead, both return FALSE.
        /// </summary>
        public const bool FALSE_ON_INVALID_INPUT = false;


        /// <summary>
        /// Year values must be greater than this value. Callers may use this
        /// constant to perform their own validations, or for other
        /// purposes.
        /// 
        /// The Gregorian calendar was adopted in 1582. To be on the safe side,
        /// this class rejects years prior to the following year, since, in its
        /// present form, it cannot correctly process dates on the Julian
        /// calendar that was in use before 1583.
        /// </summary>
        public const int GRGORIAN_CALENDAR_ADOPTION_YEAR = 1582;
        #endregion  // Public Constants for use with IsGregorianLeapYear method


        #region Private Constants for use in exception reports
        private const string ARG_NAME_SHIFT_THIS = @"ShiftThis";
        private const string ARG_NAME_YEAR = @"pintYear";

        private const string INT_ARG_NAME_DIVIDEND = @"pintDividend";
        private const string INT_ARG_NAME_DIVISOR = @"pintDivisor";

        private const string LONG_ARG_NAME_DIVIDEND = @"plngDividend";
        private const string LONG_ARG_NAME_DIVISOR = @"pintDivisor";
        #endregion  // Private Constants for use in exception reports


        #region DecimalShiftLeft Methods
        /// <summary>
        /// Shift the decimal point of an integer value left by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits">
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// </param>
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static int DecimalShiftLeft (
            int ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                int intShiftFactor = MagicNumbers.PLUS_ONE;

                try
                {
                    for ( int intShiftDigits = MagicNumbers.ZERO ;
                              intShiftDigits < NDigits ;
                              intShiftDigits++ )
                    {
                        intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                    }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                    return ShiftThis * intShiftFactor;
                }
                catch ( Exception exAll )
                {   // ClassAndMethodDiagnosticInfo  @"An {0} exception arose while computing the value of {1}.{5}Input parameters were as follows:{5}{5}    ShiftThis = {2}{5}    NDigits   = {3}{5}{5}Details are in the inner exception: {4}{5}"
                    throw new InvalidOperationException (
                        GenerateDecimalShiftExceptionMessage (
                            ShiftThis ,
                            NDigits ,
                            ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                            exAll ) ,
                        exAll );
                }
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static int DecimalShiftLeft


        /// <summary>
        /// Shift the decimal point of an integer value left by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits">
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// </param>
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static long DecimalShiftLeft (
            long ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                int intShiftFactor = MagicNumbers.PLUS_ONE;

                try
                {
                    for ( int intShiftDigits = MagicNumbers.ZERO ;
                              intShiftDigits < NDigits ;
                              intShiftDigits++ )
                    {
                        intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                    }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                    return ShiftThis * intShiftFactor;
                }
                catch ( Exception exAll )
                {   // ClassAndMethodDiagnosticInfo  @"An {0} exception arose while computing the value of {1}.{5}Input parameters were as follows:{5}{5}    ShiftThis = {2}{5}    NDigits   = {3}{5}{5}Details are in the inner exception: {4}{5}"
                    throw new InvalidOperationException (
                        GenerateDecimalShiftExceptionMessage (
                            ShiftThis ,
                            NDigits ,
                            ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                            exAll ) ,
                        exAll );
                }
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static long DecimalShiftLeft


        /// <summary>
        /// Shift the decimal point of an integer value left by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits">
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// </param>
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static uint DecimalShiftLeft (
            uint ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                //return pintShiftThis * ( int ) Math.Pow ( MagicNumbers.EXACTLY_TEN , pintNDigits );

                uint intShiftFactor = MagicNumbers.PLUS_ONE;

                try
                {
                    for ( int intShiftDigits = MagicNumbers.ZERO ;
                              intShiftDigits < NDigits ;
                              intShiftDigits++ )
                    {
                        intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                    }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                    return ShiftThis * intShiftFactor;
                }
                catch ( Exception exAll )
                {   // ClassAndMethodDiagnosticInfo  @"An {0} exception arose while computing the value of {1}.{5}Input parameters were as follows:{5}{5}    ShiftThis = {2}{5}    NDigits   = {3}{5}{5}Details are in the inner exception: {4}{5}"
                    throw new InvalidOperationException (
                        GenerateDecimalShiftExceptionMessage (
                            ShiftThis ,
                            NDigits ,
                            ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                            exAll ) ,
                        exAll );
                }
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static int DecimalShiftLeft


        /// <summary>
        /// Shift the decimal point of an integer value left by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits">
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// </param>
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static ulong DecimalShiftLeft (
            ulong ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                ulong intShiftFactor = MagicNumbers.PLUS_ONE;

                try
                {
                    for ( int intShiftDigits = MagicNumbers.ZERO ;
                              intShiftDigits < NDigits ;
                              intShiftDigits++ )
                    {
                        intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                    }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                    return ShiftThis * intShiftFactor;
                }
                catch ( Exception exAll )
                {   // ClassAndMethodDiagnosticInfo  @"An {0} exception arose while computing the value of {1}.{5}Input parameters were as follows:{5}{5}    ShiftThis = {2}{5}    NDigits   = {3}{5}{5}Details are in the inner exception: {4}{5}"
                    throw new InvalidOperationException (
                        GenerateDecimalShiftExceptionMessage (
                            ShiftThis ,
                            NDigits ,
                            ClassAndMethodDiagnosticInfo.GetMyMethodName ( ) ,
                            exAll ) ,
                        exAll );
                }
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static long DecimalShiftLeft
        #endregion  // DecimalShiftLeft Methods


        #region DecimalShiftRight Methods
        /// <summary>
        /// Shift the decimal point of an integer value right by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits"></param>
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static int DecimalShiftRight (
            int ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                int intShiftFactor = MagicNumbers.PLUS_ONE;

                for ( int intShiftDigits = MagicNumbers.ZERO ;
                          intShiftDigits < NDigits ;
                          intShiftDigits++ )
                {
                    intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                return ShiftThis / intShiftFactor;
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static int DecimalShiftRight


        /// <summary>
        /// Shift the decimal point of a float value right by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits"></param>
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static float DecimalShiftRight (
            float ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                int intShiftFactor = MagicNumbers.PLUS_ONE;

                for ( int intShiftDigits = MagicNumbers.ZERO ;
                          intShiftDigits < NDigits ;
                          intShiftDigits++ )
                {
                    intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                return ShiftThis / intShiftFactor;
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static float DecimalShiftRight


        /// <summary>
        /// Shift the decimal point of a decimal value right by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits"></param>
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static decimal DecimalShiftRight (
            decimal ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                int intShiftFactor = MagicNumbers.PLUS_ONE;

                for ( int intShiftDigits = MagicNumbers.ZERO ;
                          intShiftDigits < NDigits ;
                          intShiftDigits++ )
                {
                    intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                return ShiftThis / intShiftFactor;
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static decimal DecimalShiftRight


        /// <summary>
        /// Shift the decimal point of a double value right by a poisitive
        /// number of digits.
        /// </summary>
        /// <param name="ShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="NDigits"/>.
        /// </param>
        /// <param name="NDigits"></param>
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="ShiftThis"/>.
        /// <returns>
        /// Return the value of <paramref name="ShiftThis"/> multiplied by
        /// a power of ten that is one less than the value specified by
        /// <paramref name="NDigits"/>.
        /// </returns>
        public static double DecimalShiftRight (
            double ShiftThis ,
            int NDigits )
        {
            if ( NDigits > MagicNumbers.ZERO )
            {
                int intShiftFactor = MagicNumbers.PLUS_ONE;

                for ( int intShiftDigits = MagicNumbers.ZERO ;
                          intShiftDigits < NDigits ;
                          intShiftDigits++ )
                {
                    intShiftFactor = intShiftFactor * MagicNumbers.EXACTLY_TEN;
                }   // for ( int intShiftDigits = MagicNumbers.ZERO ; intShiftDigits < pintNDigits ; intShiftDigits++ )

                return ShiftThis / intShiftFactor;
            }   // TRUE (anticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
            else
            {
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_SHIFT_THIS ,
                    NDigits ,
                    Resources.ERRMSG_DECIMAL_SHIFT_DIGITS_VALUE );
            }   // FALSE (unanticipated outcome) block, if ( pintNDigits > MagicNumbers.ZERO )
        }   // public static double DecimalShiftRight
        #endregion  // DecimalShiftRight Methods


        #region IsEvenlyDivisibleByAnyInteger Methods
        /// <summary>
        /// Evaluate whether integer <paramref name="pintDividend"/> is evenly
        /// divisible by integer <paramref name="pintDivisor"/>.
        /// </summary>
        /// <param name="pintDividend">
        /// Specify the integer to evaluate for even divisibility against
        /// <paramref name="pintDivisor"/>.
        /// </param>
        /// <param name="pintDivisor">
        /// Specify the integer to determine whether integer
        /// <paramref name="pintDividend"/> is evenly divisible.
        /// </param>
        /// <returns>
        /// Return TRUE if integer <paramref name="pintDividend"/> is evenly
        /// divisible by <paramref name="pintDivisor"/>.
        /// 
        /// Otherwise, return FALSE.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when
        /// <paramref name="pintDivisor"/> is equal to zero, an illegal value,
        /// since the modulus operator is implemented as an integer division,
        /// and division by zero is an illegal operation, which raises an
        /// DivideByZeroException exception. Rather than report the unhelpful
        /// DivideByZeroException exception, <paramref name="pintDivisor"/> is
        /// evaluated, and the ArgumentException is raised in its place, so that
        /// the dividend passed into the method can be reported.
        /// </exception>
        /// <seealso cref="Mod(int,int)"/>
        /// <seealso cref="Remainder(int, int)"/>
        public static bool IsEvenlyDivisibleByAnyInteger (
        int pintDividend ,
        int pintDivisor )
        {
            if ( pintDivisor != MagicNumbers.ZERO )
                return pintDividend % pintDivisor == MagicNumbers.EVENLY_DIVISIBLE;
            else
                throw new ArgumentException (
                    string.Format (                                             // string message
                        Resources.ERRMSG_DIVISOR_CANNOT_BE_ZERO ,               // Format control string
                        INT_ARG_NAME_DIVISOR ,                                  // Format Item 0: the value of argument {0}
                        INT_ARG_NAME_DIVIDEND ,                                 // Format Item 1: its value canot be zero. The {1} value
                        pintDividend ) ,                                        // Format Item 2: was {2}, which MAY be zero.
                    INT_ARG_NAME_DIVISOR );                                     // string paramname
        }   // IsEvenlyDivisibleByAnyInteger (1 of 2)


        /// <summary>
        /// Evaluate whether long integer <paramref name="plngDividend"/> is
        /// evenly divisible by long integer <paramref name="plngDivisor"/>.
        /// </summary>
        /// <param name="plngDividend">
        /// Specify the long integer to evaluate for even divisibility against
        /// <paramref name="plngDivisor"/>.
        /// </param>
        /// <param name="plngDivisor">
        /// Specify the long integer to determine whether long integer
        /// <paramref name="plngDividend"/> is evenly divisible.
        /// </param>
        /// <returns>
        /// Return TRUE if long integer <paramref name="plngDividend"/> is
        /// evenly divisible by <paramref name="plngDivisor"/>. therwise, return
        /// FALSE.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when
        /// <paramref name="plngDivisor"/> is equal to zero, an illegal value,
        /// since the modulus operator is implemented as an integer division,
        /// and division by zero is an illegal operation, which raises an
        /// DivideByZeroException exception. Rather than report the unhelpful
        /// DivideByZeroException exception, <paramref name="plngDivisor"/> is
        /// evaluated, and the ArgumentException is raised in its place, so that
        /// the dividend passed into the method can be reported.
        /// </exception>
        /// <seealso cref="Mod(long,long)"/>
        /// <seealso cref="Remainder(long, long)"/>
        public static bool IsEvenlyDivisibleByAnyInteger (
            long plngDividend ,
            long plngDivisor )
        {
            if ( plngDivisor != MagicNumbers.ZERO )
                return plngDividend % plngDivisor == MagicNumbers.EVENLY_DIVISIBLE;
            else
                throw new ArgumentException (
                    string.Format (                                             // string message
                        Resources.ERRMSG_DIVISOR_CANNOT_BE_ZERO ,               // Format control string
                        LONG_ARG_NAME_DIVISOR ,                                 // Format Item 0: the value of argument {0}
                        LONG_ARG_NAME_DIVIDEND ,                                // Format Item 1: its value canot be zero. The {1} value
                        plngDividend ) ,                                        // Format Item 2: was {2}, which MAY be zero.
                    LONG_ARG_NAME_DIVISOR );                                    // string paramname
        }   // IsEvenlyDivisibleByAnyInteger (2 of 2)
        #endregion  // IsEvenlyDivisibleByAnyInteger Methods


        #region IsGregorianLeapYear Methods
        /// <summary>
        /// Given a valid year, return True if the year is a leap year, else
        /// return False.
        /// 
        /// If the input year is invalid, an ArgumentOutOfRange exception is
        /// thrown.
        /// 
        /// According to the first reference cited below, the Grgorian
        /// calendar was adopted in 1582. Hence, this formula is invalid for
        /// years before 1583. Consequently, any year before 1583 is treated
        /// as invalid, and an ArgumentOutOfRange exception is thrown, which is
        /// enforced by the IsValidGregorianYear method.
        /// </summary>
        /// <param name="pintYear">
        /// Gregorian year number, greater than 1582, to evaluate
        /// </param>
        /// <param name="pfThrowError">
        /// Specify FALSE_ON_INVALID_INPUT to suppress the default behavior,
        /// which is for IsValidGregorianYear, when called upon to validate
        /// <paramref name="pintYear"/>, to throw an ArgumentOutOfRangeException
        /// exception. The default value is EXCEPTION_ON_INVALID_INPUT (true).
        /// </param>
        /// <returns>
        /// TRUE if <paramref name="pintYear"/> is a leap year, else FALSE.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// An ArgumentOutOfRangeException exception arises when 
        /// <paramref name="pintYear"/> is an invalid Gregorian year, unless
        /// <paramref name="pfThrowError"/> is EXCEPTION_ON_INVALID_INPUT
        /// (false), in which case, the method return FALSE.
        /// </exception>
        /// <see href="https://www.tondering.dk/claus/cal/christian.php"/>
        /// <see href="https://support.microsoft.com/en-us/help/214019/method-to-determine-whether-a-year-is-a-leap-year"/>
        public static bool IsGregorianLeapYear (
            int pintYear ,
            bool pfThrowError = EXCEPTION_ON_INVALID_INPUT )
        {
            //  I am astounded that this isn't a native property of the
            //  System.DateTime class.

            // References:  https://www.tondering.dk/claus/cal/christian.php
            //              The Calendar FAQ: What is the Gregorian calendar?
            //
            //              https://support.microsoft.com/en-us/help/214019/method-to-determine-whether-a-year-is-a-leap-year
            //              XL: Method to Determine Whether a Year Is a Leap Year

            const int CENTURY_YEAR_DIVISOR = 100;
            const int LEAP_YEAR_SKIPPED_CENTURY_YEAR = 400;                     // The Gregorian calendar leap year algorithm excludes century years unless they are evenly divisible by 400.
            const int LEAP_YEAR_DIVISOR = 4;                                    // The Gregorian calendar leap year algorithm largely preserves the leap year algorithm that was introduced by the Julian calendar.

            const bool IS_LEAP_YEAR = true;
            const bool IS_NORMAL_YEAR = false;

            const bool NO_THROW_ON_ERROR = false;                               // Use this as the value of IsValidGregorianYear argument pfThrowError.

            if ( IsValidGregorianYear ( pintYear , NO_THROW_ON_ERROR ) )
            {   // Perform the new Gregorian leap year test.
                if ( pintYear % LEAP_YEAR_SKIPPED_CENTURY_YEAR == MagicNumbers.EVENLY_DIVISIBLE )
                {
                    return IS_LEAP_YEAR;                    // Century years that are divisible by 400 are leap years.
                }   // TRUE block, if ( pintYear % LEAP_YEAR_SKIPPED_CENTURY_YEAR == MagicNumbers.EVENLY_DIVISIBLE )
                else
                {   // Perform the original Julian leap year test.
                    if ( pintYear % LEAP_YEAR_DIVISOR == MagicNumbers.EVENLY_DIVISIBLE )
                    {
                        if ( pintYear % CENTURY_YEAR_DIVISOR == MagicNumbers.EVENLY_DIVISIBLE )
                        {
                            return IS_NORMAL_YEAR;          // Exclude century years unless divisible by 400.
                        }   // TRUE block, if ( pintYear % CENTURY_YEAR_DIVISOR == MagicNumbers.EVENLY_DIVISIBLE )
                        else
                        {
                            return IS_LEAP_YEAR;            // Except for century years, all other years divisible by 4 leap.
                        }   // FALSE block, if ( pintYear % CENTURY_YEAR_DIVISOR == MagicNumbers.EVENLY_DIVISIBLE )
                    }   // TRUE block, if ( pintYear % LEAP_YEAR_DIVISOR == MagicNumbers.EVENLY_DIVISIBLE )
                    else
                    {
                        return IS_NORMAL_YEAR;              // All other years don't leap.
                    }   // FALSE block, if ( pintYear % LEAP_YEAR_DIVISOR == MagicNumbers.EVENLY_DIVISIBLE )
                }   // FALSE block, if ( pintYear % LEAP_YEAR_SKIPPED_CENTURY_YEAR == MagicNumbers.EVENLY_DIVISIBLE )
            }   // TRUE (anticipated outcome) block, if ( IsValidGregorianYear ( pintYear , NO_THROW_ON_ERROR ) )
            else
            {
                if ( pfThrowError )
                {
                    throw new ArgumentOutOfRangeException (
                        ARG_NAME_YEAR ,
                        CreateYearOutOfRangeMsg ( pintYear ) );
                }   // TRUE (anticipated outcome) block, if ( pfThrowError )
                else
                {   // Return FALSE, since we don't support the Julian calendar that covers the first 1581 years of the Common Era.
                    return IS_NORMAL_YEAR;
                }   // FALSE (unanticipated outcome) block, if ( pfThrowError )
            }   // FALSE (unanticipated outcome) block, if ( IsValidGregorianYear ( pintYear , NO_THROW_ON_ERROR ) )
        }   // public static bool IsGregorianLeapYear


        /// <summary>
        /// This method returns True if the year is valid for the Gregorian
        /// calendar.
        /// 
        /// The lower limit is exposed as a public constant,
        /// GRGORIAN_CALENDAR_ADOPTION_YEAR.
        /// </summary>
        /// /// <param name="pintYear">
		/// Gregorian year number, greater than 1582, to evaluate
		/// </param>
        /// <param name="pfThrowError">
		/// TRUE if caller wants an invalid input to provoke an exception.
		/// 
		/// To inprove the quality of your internal documentation, You may use
		/// public constant EXCEPTION_ON_INVALID_INPUT in lieu of TRUE, and
		/// RETURN_ON_INVALID_INPUT in lieu of FALSE.
		/// </param>
        /// <returns>
		/// TRUE if the input is a valid Gregorian year.
		/// </returns>
        public static bool IsValidGregorianYear (
            int pintYear ,
            bool pfThrowError )
        {
            if ( pintYear > GRGORIAN_CALENDAR_ADOPTION_YEAR && pintYear < DateTime.MaxValue.Year )
                return true;

            if ( pfThrowError )
                throw new ArgumentOutOfRangeException (
                    ARG_NAME_YEAR ,
                    CreateYearOutOfRangeMsg ( pintYear ) );

            return false;
        }   // public static bool IsValidGregorianYear
        #endregion  // IsGregorianLeapYear Methods


        #region Mod and Remainder methods, which are interchangeable
        /// <summary>
        /// Return the modulus, which is the remainder from dividing one integer
        /// by another.
        /// </summary>
        /// <param name="pintDividend">
        /// Specify the number into which <paramref name="pintDivisor"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <param name="pintDivisor">
        /// Specify the number by which <paramref name="pintDividend"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <returns>
        /// The return value is the integer result of dividing
        /// <paramref name="pintDividend"/> by <paramref name="pintDivisor"/>.
        /// </returns>
        /// <remarks>
        /// The Mod and Remainder methods are synonymns. Their simplicity lends
        /// them to inline implementation.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when
        /// <paramref name="pintDivisor"/> is equal to zero, an illegal value,
        /// since the modulus operator is implemented as an integer division,
        /// and division by zero is an illegal operation, which raises an
        /// DivideByZeroException exception. Rather than report the unhelpful
        /// DivideByZeroException exception, <paramref name="pintDivisor"/> is
        /// evaluated, and the ArgumentException is raised in its place, so that
        /// the dividend passed into the method can be reported.
        /// </exception>
        /// <seealso cref="Remainder(int,int)"/>
        /// <seealso cref="IsEvenlyDivisibleByAnyInteger(int,int)"/>
        public static int Mod (
            int pintDividend ,
            int pintDivisor )
        {
            if ( pintDivisor != MagicNumbers.ZERO )
                return pintDividend % pintDivisor;
            else
                throw new ArgumentException (
                    string.Format (                                             // string message
                        Resources.ERRMSG_DIVISOR_CANNOT_BE_ZERO ,               // Format control string
                        INT_ARG_NAME_DIVISOR ,                                  // Format Item 0: the value of argument {0}
                        INT_ARG_NAME_DIVIDEND ,                                 // Format Item 1: its value canot be zero. The {1} value
                        pintDividend ) ,                                        // Format Item 2: was {2}, which MAY be zero.
                    INT_ARG_NAME_DIVISOR );                                     // string paramname
        }   // public static int Mod (1 of 2)


        /// <summary>
        /// Return the modulus, which is the remainder from dividing one integer
        /// by another.
        /// </summary>
        /// <param name="plngDividend">
        /// Specify the number into which <paramref name="plngDivisor"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <param name="plngDivisor">
        /// Specify the number by which <paramref name="plngDividend"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <returns>
        /// The return value is the long integer result of dividing
        /// <paramref name="plngDividend"/> by <paramref name="plngDivisor"/>.
        /// </returns>
        /// <remarks>
        /// The Mod and Remainder methods are synonymns. Their simplicity lends
        /// them to inline implementation.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when
        /// <paramref name="plngDivisor"/> is equal to zero, an illegal value,
        /// since the modulus operator is implemented as an integer division,
        /// and division by zero is an illegal operation, which raises an
        /// DivideByZeroException exception. Rather than report the unhelpful
        /// DivideByZeroException exception, <paramref name="plngDivisor"/> is
        /// evaluated, and the ArgumentException is raised in its place, so that
        /// the dividend passed into the method can be reported.
        /// </exception>
        /// <seealso cref="Remainder(long,long)"/>
        /// <seealso cref="IsEvenlyDivisibleByAnyInteger(int,int)"/>
        public static long Mod (
            long plngDividend ,
            long plngDivisor )
        {
            if ( plngDivisor != MagicNumbers.ZERO )
                return plngDividend % plngDivisor;
            else
                throw new ArgumentException (
                    string.Format (                                             // string message
                        Resources.ERRMSG_DIVISOR_CANNOT_BE_ZERO ,               // Format control string
                        LONG_ARG_NAME_DIVISOR ,                                 // Format Item 0: the value of argument {0}
                        LONG_ARG_NAME_DIVIDEND ,                                // Format Item 1: its value canot be zero. The {1} value
                        plngDividend ) ,                                        // Format Item 2: was {2}, which MAY be zero.
                    LONG_ARG_NAME_DIVISOR );                                    // string paramname
        }   // public static long Mod (1 of 2)


        /// <summary>
        /// Return the modulus, which is the remainder from dividing one integer
        /// by another.
        /// </summary>
        /// <param name="pintDividend">
        /// Specify the number into which <paramref name="pintDivisor"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <param name="pintDivisor">
        /// Specify the number by which <paramref name="pintDividend"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <returns>
        /// The return value is the integer result of dividing
        /// <paramref name="pintDividend"/> by <paramref name="pintDivisor"/>.
        /// </returns>
        /// <remarks>
        /// The Mod and Remainder methods are synonymns. Their simplicity lends
        /// them to inline implementation.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when
        /// <paramref name="pintDivisor"/> is equal to zero, an illegal value,
        /// since the modulus operator is implemented as an integer division,
        /// and division by zero is an illegal operation, which raises an
        /// DivideByZeroException exception. Rather than report the unhelpful
        /// DivideByZeroException exception, <paramref name="pintDivisor"/> is
        /// evaluated, and the ArgumentException is raised in its place, so that
        /// the dividend passed into the method can be reported.
        /// </exception>
        /// <seealso cref="Mod(int,int)"/>
        /// <seealso cref="IsEvenlyDivisibleByAnyInteger(int,int)"/>
        public static int Remainder (
            int pintDividend ,
            int pintDivisor )
        {
            if ( pintDivisor != MagicNumbers.ZERO )
                return pintDividend % pintDivisor;
            else
                throw new ArgumentException (
                    string.Format (                                             // string message
                        Resources.ERRMSG_DIVISOR_CANNOT_BE_ZERO ,               // Format control string
                        INT_ARG_NAME_DIVISOR ,                                  // Format Item 0: the value of argument {0}
                        INT_ARG_NAME_DIVIDEND ,                                 // Format Item 1: its value canot be zero. The {1} value
                        pintDividend ) ,                                        // Format Item 2: was {2}, which MAY be zero.
                    INT_ARG_NAME_DIVISOR );                                     // string paramname
        }   // public static int Remainder (1 of 2)


        /// <summary>
        /// Return the modulus, which is the remainder from dividing one integer
        /// by another.
        /// </summary>
        /// <param name="plngDividend">
        /// Specify the number into which <paramref name="plngDivisor"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <param name="plngDivisor">
        /// Specify the number by which <paramref name="plngDividend"/> should
        /// be divided to obtain a remainder.
        /// </param>
        /// <returns>
        /// The return value is the integer result of dividing
        /// <paramref name="plngDividend"/> by <paramref name="plngDivisor"/>.
        /// </returns>
        /// <remarks>
        /// The Mod and Remainder methods are synonymns. Their simplicity lends
        /// them to inline implementation.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// An ArgumentException exception arises when
        /// <paramref name="plngDivisor"/> is equal to zero, an illegal value,
        /// since the modulus operator is implemented as an integer division,
        /// and division by zero is an illegal operation, which raises an
        /// DivideByZeroException exception. Rather than report the unhelpful
        /// DivideByZeroException exception, <paramref name="plngDivisor"/> is
        /// evaluated, and the ArgumentException is raised in its place, so that
        /// the dividend passed into the method can be reported.
        /// </exception>
        /// <seealso cref="Mod(long,long)"/>
        /// <seealso cref="IsEvenlyDivisibleByAnyInteger(long,long)"/>
        public static long Remainder (
            long plngDividend ,
            long plngDivisor )
        {
            if ( plngDivisor != MagicNumbers.ZERO )
                return plngDividend % plngDivisor;
            else
                throw new ArgumentException (
                    string.Format (                                             // string message
                        Resources.ERRMSG_DIVISOR_CANNOT_BE_ZERO ,               // Format control string
                        LONG_ARG_NAME_DIVISOR ,                                 // Format Item 0: the value of argument {0}
                        LONG_ARG_NAME_DIVIDEND ,                                // Format Item 1: its value canot be zero. The {1} value
                        plngDividend ) ,                                        // Format Item 2: was {2}, which MAY be zero.
                    LONG_ARG_NAME_DIVISOR );                                    // string paramname
        }   // public static long Remainder 2 of 2
        #endregion  // Mod and Remainder methods, which are interchangeable


        #region Private Static Helper Methods
        /// <summary>
        /// This method returns a string that contains the value, and the
        /// upper and lower limits that it must meet, according to the rules
        /// against which it was evaluated.
        /// </summary>
        /// <param name="pintBadYear">
        /// Invalid Gregorian year that provoked the error.
        /// </param>
        /// <returns>
        /// The returned string is a message, suitable for use as the Message
        /// argument to an ArgumentOutOfRange exception constructor.
        /// </returns>
        private static string CreateYearOutOfRangeMsg ( int pintBadYear )
        {
            return string.Format (
                Common.Properties.Resources.ERRMSG_ARG_OUT_OF_RANGE ,
                pintBadYear ,
                GRGORIAN_CALENDAR_ADOPTION_YEAR ,
                DateTime.MaxValue.Year );
        }   // CreateYearOutOfRangeMsg


        /// <summary>
        /// Construct a detailed exception report.
        /// </summary>
        /// <typeparam name="T">
        /// Although generic parameter type T is unconstrained, it is expected
        /// to be numeric, either integral or floating point of some precision.
        /// Implementing this method as a generic allows one method to construct
        /// messages for all DecimalShiftLeft and DecimalShiftRight methods.
        /// </typeparam>
        /// <param name="pnbrShiftThis">
        /// Specify the integer value to be shifted left by the number of digits
        /// specified by <paramref name="pintNDigits"/>.
        /// </param>
        /// <param name="pintNDigits">
        /// Specify the number of digits to the left by which to shift the
        /// value specified by <paramref name="pnbrShiftThis"/>.
        /// </param>
        /// <param name="pstrFailingMethodName">
        /// Pass in a reference to a string that contains the name of the method
        /// that failed. The most reliable way to get this value is by calling
        /// ClassAndMethodDiagnosticInfo.GetMyMethodName, which obtains the info
        /// from the compiler through one of three optional arguments that the
        /// C# compiler supplies at run time.
        /// </param>
        /// <param name="pexAll">
        /// Pass in a reference to the exception being handled, from which the
        /// Message property is appended to the returned string.
        /// </param>
        /// <returns>
        /// The return value is a string that summarizes the parameter values
        /// that gave rise to the exception reported herein, concluding with a
        /// copy of the message from the exception being handled by the calling
        /// code block.
        /// </returns>
        private static string GenerateDecimalShiftExceptionMessage<T> (
            T pnbrShiftThis ,
            int pintNDigits ,
            string pstrFailingMethodName ,
            Exception pexAll )
        {
            return string.Format (
                Resources.ERRMSG_MATH_EXCEPTION ,
                new object [ ]
                {
                            pexAll.GetType ( ).FullName ,                        // Format Item 0: An {0} exception arose
                            pstrFailingMethodName ,  // Format item 1: while computing the value of {1}.
                            pnbrShiftThis ,                                         // Format Item 2: ShiftThis = {2}
                            pintNDigits ,                                           // Format Item 3: NDigits   = {3}
                            pexAll.Message ,                                     // Format Item 4: Details are in the inner exception: {4}
                            Environment.NewLine                                 // Format Item 5: Platform-dependent newline
                } );
        }   // private static string GenerateDecimalShiftExceptionMessage<T>
        #endregion  // Private Static Helper Methods
    }   // public static class MoreMath
}   // namespace WizardWrx