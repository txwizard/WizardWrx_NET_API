/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         MagicNumbers

    File Name:          MagicNumbers.cs

    Synopsis:           Define a handful of frequently used numeric values that
                        are difficult to correctly differentiate in a source
						code listing, in print or in a text editor window, or
                        whose values can be a challenge to keep straight.

	Remarks:            This class is one of a constellation of static classes
						that define a wide variety of symbolic constants that I
						use to make my code easier to understand when I need a
						refresher or am about to change it.

						This class implements a subset of the numeric constants
                        defined in WizardWrx.MagicNumbers and exposed by
						class library WizardWrx.SharetUtl2. Other constants, 
						such as those intended mainly for use with arrays and
                        lists, have moved into sibling classes ArrayInfo, 
						ListInfo, and PathPositions, all of which are defined in
						this library.

						The XML documentation in this class incorporates cross
						references to classes defined in other modules that are
						compiled into the same assembly. A couple of hours has
						yielded the following lessons.

						1)	You can refer to any other class in the assembly, if
							you fully qualify it with respect to the namespace
							declared at the top of the same source module.

						2)	All references are subject to the above rule:

								- Argument types used to disambiguate overloads
								- Method names
								- Argument types used to disambiguate overloads
								- Named constants

							Examples:

								These don't work.

									GetReservedErrorMessage(ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(ExceptionLogger.ErrorExitOptions)
									ExceptionLogger.GetReservedErrorMessage(DLLServices2.ExceptionLogger.ErrorExitOptions)

								This works.

									DLLServices2.ExceptionLogger.GetReservedErrorMessage(DLLServices2.ExceptionLogger.ErrorExitOptions)

							Notwithstanding assertions in the documentation that
							these cross references respect using irectiv4es when
							resolving, it appears that this courtesy may exclude
							references to the current assembly (the one being
							built), which makes sense, upon reflection, since it
							is in a state of flux, and has yet to be persisted.

						3)	See and SeeAlso XML tags can refer to Web pages, but
							the correct keyword is not CREF, but HREF.

    Author:             David A. Gray

    License:            Copyright (C) 2014-2020, David A. Gray
						All rights reserved

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
	2015/06/20 5.5     DAG    Relocate to WizardWrx.DLLServices2 class library, 
                              promote to the root WizardWrx namespace, add
                              special characters that I originally defined in
                              class WizardWrx.SharedUt2.MagicNumbers, and
                              cross reference related constants defined in other
                              classes.

	2015/09/01 5.6     DAG    Correct syntax errors and other issues in the XML
                              documentation.

    2015/09/01 5.7     DAG    Correct spelling errors flagged by the spelling
                              checker add-in that I recently installed into the
                              Visual Studio 2013 IDE, along with a documentation
                              XML error that I overlooked.

	2015/10/09 5.8     DAG    Define APPLICATION_ERROR_MASK, for use with 
                              routines that would typically call SetLastError if
                              they were implemented as native code.

	2016/05/12 6.1     DAG    Cross reference related routines in this library
                              and companion ConsoleAppAids2.

	2016/06/04 6.2     DAG    Resolve ambiguous and unreachable cross references
							  tags in the XML documentation.
 
    2016/06/07 6.3     DAG    Correct grammatical errors uncovered while
                              preparing this library for publication on GitHub.

	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

	2017/06/24 7.0     DAG    Define the constants listed in the following table.

									----------------------------------------
									Name                               Value
									-------------------------- -------------
									ERROR_INVALID_CMD_LNE_ARGS             2

									EXACTLY_ONE_HUNDRED		             100
									EXACTLY_ONE_THOUSAND   	           1,000
                                    EXACTLY_TEN_THOUSAND   	          10,000
									EXACTLY_ONE_MILLION		       1,000,000
									EXACTLY_ONE_BILLION		   1,000,000,000

									MILLISECONDS_PER_SECOND	           1,000
									TICKS_PER_SECOND              10,000,000
									----------------------------------------

	                          The EXACTLY_* groups is defined where anything
                              with that prefix would appear alphabetically, but
                              the individual items are defined in order of
                              relative magnitude.

	2017/09/17 7.0     DAG    Define NUMBER_BASE_DECIMAL and 
                              NUMBER_BASE_HEXADECIMAL.

    2018/11/10 7.11    DAG    BREAKING CHANGE: Rename EXACTLY_ONE_NUNDRED to
                                               EXACTLY_ONE_HUNDRED, correcting a
                                               misspelling that prevented me
                                               finding it yesterday.

                              Correct the value of EXACTLY_TEN_THOUSAND, which I
                              discovered was returning one million.

                              Define overlooked constants EXACTLY_TEN and
                              EVENLY_DIVISIBLE.

	2017/09/17 7.13    DAG    Define EXACTLY_ONE_HUNDRED_MILLION_LONG, to meet an
                              immediate requirement, along with
                              EXACTLY_ONE_HUNDRED_MILLION and 
                              EXACTLY_ONE_HUNDRED_THOUSAND to more or less
                              complete the set of powers of ten from two to 9.

	2018/12/22 7.14    DAG    Define the constants listed in the following table.

									--------------------------------------------
									Name                                   Value
									-------------------------- -----------------
                                    TICKS_PER_1_WEEK           6,048,000,000,000
                                    TICKS_PER_1_DAY              864,000,000,000
                                    TICKS_PER_23_59_59           863,990,000,000
                                    TICKS_PER_23_59_00           863,400,000,000
                                    TICKS_PER_1_HOUR              36,000,000,000
                                    TICKS_PER_1_MINUTE               600,000,000
                                    TICKS_PER_1_SECOND                10,000,000
                                    TICKS_PER_1_MILLISECOND               10,000
									--------------------------------------------

                              BREAKING CHANGE: TICKS_PER_SECOND is correctly
                                               described in the XML comment, but
                                               its numerical value was off by a
                                               factor of one thousand. This is
                                               corrected by making its value
                                               equal to TICKS_PER_1_SECOND.

                              These tick values were computed by a custom class,
                              and the constants were derived therefrom by Excel
                              worksheet formulas.

	2020/12/21 7.24    DAG    Define the constants listed below.

                                CAPACITY_4
                                CAPACITY_8
                                CAPACITY_10
                                CAPACITY_16

                                CAPACITY_128
                                CAPACITY_255
                                CAPACITY_256

		                        PLUS_THREE
		                        PLUS_FOUR
		                        PLUS_FIVE
		                        PLUS_SIX
		                        PLUS_SEVEN
		                        PLUS_EIGHT
		                        PLUS_NINE
                                MSO_COLLECTION_FIRST_ITEM

                              Observant code reviewers may notice the absence of
                              the new constants from the unit test. Since the
                              "test" consists of listing their values, I beg you
                              to overlook this oversight, which came about due
                              to their addition being made to meet a pressing
                              need.
	============================================================================
*/


namespace WizardWrx
{
	/// <summary>
	/// This class defines constants for commonly used magic numbers. Others are
	/// defined in companion class ArrayInfo, while SpecialCharacters defines
	/// character representations of the visually ambiguous numbers and letters,
	/// for use in place of literals.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	/// <remarks>
	/// For ease of access, I promoted the classes that expose only constants to
	/// the root of the WizardWrx namespace.
	/// </remarks>
	/// <seealso cref="ArrayInfo"/>
	/// <seealso cref="ListInfo"/>
	/// <seealso cref="MagicBooleans"/>
	/// <seealso cref="PathPositions"/>
	/// <seealso cref="SpecialCharacters"/>
	public static class MagicNumbers
	{
		/// <summary>
		/// This constant defines the standard APPLICATION_ERROR_MASK bit, which
		/// distinguishes application errors from system errors in the status
		/// codes returned by Marshal.GetLastWin32Error.
		/// </summary>
		public const uint APPLICATION_ERROR_MASK = 0x20000000;

        /// <summary>
        /// Use this to set a small capacity on empty List and Dictionary collections.
        /// </summary>
        /// <seealso cref="CAPACITY_8"/>
        /// <seealso cref="CAPACITY_10"/>
        /// <seealso cref="CAPACITY_16"/>
        public const int CAPACITY_4 = 4;

        /// <summary>
        /// Use this to set a small capacity on empty List and Dictionary collections.
        /// </summary>
        /// <seealso cref="CAPACITY_4"/>
        /// <seealso cref="CAPACITY_10"/>
        /// <seealso cref="CAPACITY_16"/>
        public const int CAPACITY_8 = 8;

        /// <summary>
        /// Use this to set a small capacity on empty List and Dictionary collections.
        /// </summary>
        /// <seealso cref="CAPACITY_4"/>
        /// <seealso cref="CAPACITY_8"/>
        /// <seealso cref="CAPACITY_16"/>
        public const int CAPACITY_10 = 10;

        /// <summary>
        /// Use this to set a small capacity on empty List and Dictionary collections.
        /// </summary>
        /// <seealso cref="CAPACITY_4"/>
        /// <seealso cref="CAPACITY_8"/>
        public const int CAPACITY_16 = 16;

        /// <summary>
        /// Constant for sizing a 128 unit StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_128 = 128;

        /// <summary>
        /// Constant for sizing a 255 unit StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_255 = 255;

        /// <summary>
        /// Constant for sizing a 256 unit StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_256 = 256;

        /// <summary>
        /// Constant for sizing a 1 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_01KB = 1024;

        /// <summary>
        /// Constant for sizing a 2 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_02KB	= 2048;

        /// <summary>
        /// Constant for sizing a 4 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_04KB	= 4096;

        /// <summary>
        /// Constant for sizing a 8 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_08KB = 8192;

        /// <summary>
        /// Constant for sizing a 16 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_16KB = 16384;

        /// <summary>
        /// Constant for sizing a 32 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_64KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_32KB = 32768;

        /// <summary>
        /// Constant for sizing a 64 KB StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        /// <seealso cref="CAPACITY_MAX_PATH"/>
        public const int CAPACITY_64KB = 65536;

        /// <summary>
        /// Constant for sizing a MAX_PATH StringBuilder, array, or buffer.
        /// </summary>
        /// <seealso cref="CAPACITY_128"/>
        /// <seealso cref="CAPACITY_255"/>
        /// <seealso cref="CAPACITY_256"/>
        /// <seealso cref="CAPACITY_01KB"/>
        /// <seealso cref="CAPACITY_02KB"/>
        /// <seealso cref="CAPACITY_04KB"/>
        /// <seealso cref="CAPACITY_08KB"/>
        /// <seealso cref="CAPACITY_16KB"/>
        /// <seealso cref="CAPACITY_32KB"/>
        public const int CAPACITY_MAX_PATH = 260;

        /// <summary>
        /// Use this integer to start a loop over a one-based collection of
        /// Microsoft Office object collections, such as cells in an Excel
        /// worksheet.
        /// </summary>
        public const int MSO_COLLECTION_FIRST_ITEM = PLUS_ONE;

		/// <summary>
		/// Use this constant to document when zero stands for the length of the
		/// empty string, or is the lower limit of a string length argument.
		/// </summary>
		/// <seealso cref="ArrayInfo.ARRAY_FIRST_ELEMENT"/>
		/// <seealso cref="ArrayInfo.ARRAY_IS_EMPTY"/>
		/// <seealso cref="ArrayInfo.ARRAY_INVALID_INDEX"/>
		/// <seealso cref="ArrayInfo.ARRAY_SECOND_ELEMENT"/>
		/// <seealso cref="ArrayInfo.INDEX_FROM_ORDINAL"/>
		/// <seealso cref="ArrayInfo.NEXT_INDEX"/>
		/// <seealso cref="ArrayInfo.ORDINAL_FROM_INDEX"/>
		/// <seealso cref="SpecialStrings.EMPTY_STRING"/>
		public const int EMPTY_STRING_LENGTH = ZERO;

		/// <summary>
		/// This exit code is reserved for reporting invalid command line
		/// arguments.
		/// </summary>
		/// <seealso cref="ERROR_SUCCESS"/>
		/// <seealso cref="PLUS_ONE"/>
		public const int ERROR_INVALID_CMD_LNE_ARGS = PLUS_TWO;

		/// <summary>
		/// This exit code is reserved for reporting runtime exceptions.
		/// </summary>
		/// <seealso cref="ERROR_SUCCESS"/>
		/// <seealso cref="ERROR_INVALID_CMD_LNE_ARGS"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="SpecialStrings.ERRMSG_SUCCESS_PLACEHOLDER"/>
		public const int ERROR_RUNTIME = PLUS_ONE;

		/// <summary>
		/// Use this with environment.exit() and other situations when you want
		/// to report the default "success" exit code of zero.
		/// </summary>
		/// <seealso cref="ERROR_RUNTIME"/>
		/// <seealso cref="ERROR_INVALID_CMD_LNE_ARGS"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialStrings.ERRMSG_SUCCESS_PLACEHOLDER"/>
		public const int ERROR_SUCCESS = ZERO;

        /// <summary>
        /// Use this constant with the modulus operator to evaluate whether one
        /// integer is evenly divisible by a smaller integer.
        /// </summary>
        /// <example>
        /// The following example uses the modulus operator to evaluate whether
        /// a counter is an even multiple of one hundred, updating a frozen line
        /// on the program's console with the new count.
        /// <code>
        /// 
        ///     if ( intTotalRows % MagicNumbers.EXACTLY_ONE_HUNDRED == MagicNumbers.EVENLY_DIVISIBLE )
        ///     {
        ///         fixedConsoleWriter.Write(
        ///             @"{0} of {1} records processed" ,
        ///             intTotalRows.ToString(NumericFormats.IntegerPerRegSettings ( ) ) ,
        ///             intRowsInTable.ToString(NumericFormats.IntegerPerRegSettings ( ) ) );
        ///     }   // if ( intTotalRows % MagicNumbers.EXACTLY_ONE_NUNDRED == MagicNumbers.ZERO )
        /// </code>
        /// </example>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EVENLY_DIVISIBLE = 0;

        /// <summary>
        /// Use this constant when you need a literal value of exactly ten.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EXACTLY_TEN = 10;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one hundred.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EXACTLY_ONE_HUNDRED = 100;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one hundred million.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EXACTLY_ONE_HUNDRED_MILLION = 100000000;

        /// <summary>
        /// Use this constant when you need a long integer literal value of exactly one hundred million.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_BILLION"/>
        public const long EXACTLY_ONE_HUNDRED_MILLION_LONG = 100000000;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one hundred thousand.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EXACTLY_ONE_HUNDRED_THOUSAND = 1000000;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one thousand.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        ///	<seealso cref="EXACTLY_ONE_BILLION"/>
        ///	<seealso cref="MILLISECONDS_PER_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_SECOND"/>
        public const int EXACTLY_ONE_THOUSAND = 1000;

        /// <summary>
        /// Use this constant when you need a literal value of exactly ten thousand.
        /// </summary>
        /// <remarks>
        /// This internal-use literal may as well be public.
        /// </remarks>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        ///	<seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EXACTLY_TEN_THOUSAND = 10000;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one million.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_BILLION"/>
        public const int EXACTLY_ONE_MILLION = 1000000;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one billion.
        /// </summary>
        /// <seealso cref="EVENLY_DIVISIBLE"/>
        /// <seealso cref="EXACTLY_TEN"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_THOUSAND"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION"/>
        /// <seealso cref="EXACTLY_ONE_HUNDRED_MILLION_LONG"/>
        ///	<seealso cref="EXACTLY_TEN_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="EXACTLY_ONE_MILLION"/>
        public const int EXACTLY_ONE_BILLION = 1000000000;

        /// <summary>
        /// Use this constant when you need a literal value of exactly one second worth of milliseconds..
        /// </summary>
        /// <see cref="EXACTLY_ONE_THOUSAND"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_SECOND"/>
        public const int MILLISECONDS_PER_SECOND = EXACTLY_ONE_THOUSAND;

		/// <summary>
		/// Use this constant to disambiguate a negative 1 from a positive 1
		/// immediately following a minus sign that is intended to be an
		/// operator.
		/// </summary>
		/// <remarks>
		/// This constant is especially useful in an expression that follows any
		/// algebraic operator with an operand value of -1.
		/// </remarks>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_TWO"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
		public const int MINUS_ONE = -1;

		/// <summary>
		/// Use this value with Math.Pow to raise ten to a specified power.
		/// </summary>
		/// <seealso cref="NUMBER_BASE_HEXADECIMAL"/>
		public const int NUMBER_BASE_DECIMAL = 10;

		/// <summary>
		/// Use this value with Math.Pow to raise sixteen to a specified power.
		/// </summary>
		/// <seealso cref="NUMBER_BASE_DECIMAL"/>
		public const int NUMBER_BASE_HEXADECIMAL = 16;

		/// <summary>
		/// Use this constant to disambiguate a numeric value of 1 from a
		/// literal upper case I or lower case L, either of which is a legal
		/// variable name, and all of which are almost impossible to reiaably
		/// distinguish visually in source code.
		/// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_TWO"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
		public const int PLUS_ONE = 1;

		/// <summary>
		/// Number 2, to clearly distinguish it from a letter Z and a numeral 7,
		/// which can look a lot like it.
		/// 
		/// Compare to character constants CHAR_LC_Z and CHAR_UC_Z.
		/// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
		public const int PLUS_TWO = 2;


        /// <summary>
        /// Thres makes a crowd.
        /// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
        public const int PLUS_THREE = 3;


        /// <summary>
        /// Two times two equals four, the first integral power of two.
        /// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
        public const int PLUS_FOUR = 4;


        /// <summary>
        /// Five fingers has a normal, fully-equipped human hand.
        /// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
        public const int PLUS_FIVE = 5;


        /// <summary>
        /// Six fills the gap between five and seven.
        /// </summary>
        /// <seealso cref="MINUS_ONE"/>
        /// <seealso cref="PLUS_ONE"/>
        /// <seealso cref="PLUS_SEVEN"/>
        /// <seealso cref="ZERO"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
        /// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
        /// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
        /// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
        public const int PLUS_SIX = 6;

		/// <summary>
		/// Number 7, to clearly distinguish it from a letter Z and a numeral 2,
		/// which can look a lot like it.
		/// 
		/// Compare to character constants CHAR_LC_Z and CHAR_UC_Z.
		/// 												
		/// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_TWO"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
		public const int PLUS_SEVEN = 7;


        /// <summary>
        /// Eight is the cube of two.
        /// </summary>
        /// <seealso cref="MINUS_ONE"/>
        /// <seealso cref="PLUS_ONE"/>
        /// <seealso cref="PLUS_SEVEN"/>
        /// <seealso cref="ZERO"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
        /// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
        /// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
        /// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
        /// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
        /// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
        public const int PLUS_EIGHT = 8;


        /// <summary>
        /// Number Nine, number nine, number nine . . .
        /// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_TWO"/>
		/// <seealso cref="PLUS_THREE"/>
		/// <seealso cref="PLUS_FOUR"/>
		/// <seealso cref="PLUS_FIVE"/>
		/// <seealso cref="PLUS_SIX"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="PLUS_EIGHT"/>
		/// <seealso cref="PLUS_NINE"/>
		/// <seealso cref="ZERO"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
        public const int PLUS_NINE = 9;

		/// <summary>
		/// Value returned by the IndexOf methods, e. g., of the String class,
		/// if the object sought is not found.
		/// </summary>
		/// <seealso cref="MINUS_ONE"/>
		public const int STRING_INDEXOF_NOT_FOUND = MINUS_ONE;

		/// <summary>
		/// Use with the Substring method of the String class to denote the
		/// first character in a string.
		/// </summary>
		/// <seealso cref="ZERO"/>
		public const int STRING_SUBSTR_BEGINNING = ZERO;

        /// <summary>
        /// There are ten million ticks of one hundred nanoseconds each in one second.
        /// 
        /// BREAKING CHANGE: Prior to version 7.14, this value was understated
        /// by a factor of one thousand.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<see cref="EXACTLY_TEN_THOUSAND"/>
        ///	<see cref="TICKS_PER_1_SECOND"/>
        /// <seealso cref="MILLISECONDS_PER_SECOND"/>
        public const int TICKS_PER_SECOND = ( int ) TICKS_PER_1_SECOND;

        /// <summary>
        /// There are 6,048,000,000,000 ticks in one calendar week.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        public const long TICKS_PER_1_WEEK = 6048000000000;

        /// <summary>
        /// There are 864,000,000,000 ticks in one calendar day.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        public const long TICKS_PER_1_DAY = 864000000000;

        /// <summary>
        /// There are 863,990,000,000 ticks in one calendar day less one second.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        public const long TICKS_PER_23_59_59 = 863990000000;

        /// <summary>
        /// There are 863,400,000,000 ticks in one calendar day less one minute. 
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        public const long TICKS_PER_23_59_00 = 863400000000;

        /// <summary>
        /// There are 36,000,000,000 ticks in one hour.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        public const long TICKS_PER_1_HOUR = 36000000000;

        /// <summary>
        /// There are 600,000,000 ticks in one minute.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        public const long TICKS_PER_1_MINUTE = 600000000;

        /// <summary>
        /// There are 10,000,000 ticks in one second.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_MILLISECOND"/>
        ///	<seealso cref="TICKS_PER_SECOND"/>
        ///	<seealso cref="MILLISECONDS_PER_SECOND"/>
        public const long TICKS_PER_1_SECOND = 10000000;

        /// <summary>
        /// There are 10,000 ticks in one millisecond.
        /// 
        /// Use this constant to construct a TimeSpan of the corresponding
        /// length (value) by way of its one-argument constructor, or to
        /// increase or decrease the value of a time span by the corresponding
        /// amount by way of its one-argument method.
        /// </summary>
        ///	<seealso cref="TICKS_PER_1_WEEK"/>
        ///	<seealso cref="TICKS_PER_1_DAY"/>
        ///	<seealso cref="TICKS_PER_23_59_59"/>
        ///	<seealso cref="TICKS_PER_23_59_00"/>
        ///	<seealso cref="TICKS_PER_1_HOUR"/>
        ///	<seealso cref="TICKS_PER_1_MINUTE"/>
        ///	<seealso cref="TICKS_PER_1_SECOND"/>
        public const long TICKS_PER_1_MILLISECOND = 10000;


        /// <summary>
        /// A UNC prefix starts here at this character position.
        /// </summary>
        /// <seealso cref="MINUS_ONE"/>
        public const int UNC_PREFIX_START_POS = PLUS_ONE;

		/// <summary>
		/// A UNC prefix has this many characters.
		/// </summary>
		/// <seealso cref="PLUS_TWO"/>
		public const int UNC_PREFIX_START_LEN = PLUS_TWO;

		/// <summary>
		/// Constant equivalent to integer value of zero, to disambiguate zero
		/// literal values in code.
		/// 
		/// Compare to character constants CHAR_LC_O and CHAR_UC_O.
		/// </summary>
		/// <seealso cref="MINUS_ONE"/>
		/// <seealso cref="PLUS_ONE"/>
		/// <seealso cref="PLUS_TWO"/>
		/// <seealso cref="PLUS_SEVEN"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_I"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_L"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_O"/>
		/// <seealso cref="SpecialCharacters.CHAR_LC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_UC_Z"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_0"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_1"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_2"/>
		/// <seealso cref="SpecialCharacters.CHAR_NUMERAL_7"/>
		public const int ZERO = 0;
	}	// public sealed class MagicNumbers
}	// partial namespace WizardWrx