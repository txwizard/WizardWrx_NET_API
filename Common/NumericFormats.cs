/*
    ============================================================================

	Namespace Name:		WizardWrx

	Class Name:			NumericFormats

 	File Name:			NumericFormats.cs

	Synopsis:			This sealed class exposes custom format strings, based
						primarily on the Standard Numeric Format Strings, as
                        documented for the Microsoft .NET Framework version 2.0.

	Remarks:            This class is one of a constellation of static classes
						that define a wide variety of symbolic constants that I
						use to make my code easier to understand when I need a
						refresher or am about to change it.

						Version 2.47 adds NUMBER_PER_REG_SETTINGS_0D, because I
						discovered that the default NLS format - "N" - works as
						I intended it only because I happen to have the default
						number of decimal places overridden in the Regional and
						Language Settings section of my Windows Control Panel.

						Beginning with version 2.46 of this class, the build and
						revision numbers are controlled by the build engine.

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
							these cross references respect using directives when
							resolving, it appears that this courtesy may exclude
							references to the current assembly (the one being
							built), which makes sense, upon reflection, since it
							is in a state of flux, and has yet to be persisted.

						3)	See and SeeAlso XML tags can refer to Web pages, but
							the correct keyword is not CREF, but HREF.

    License:            Copyright (C) 2009-2019, David A. Gray. 
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

	Date Written:		Saturday, 31 January 2009

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2009/01/31 2.2.0.3 DAG    Add this class, consisting entirely of constants.
                              Most of the initial constants are imported from
                              WizardWrx.CommonConstants.StandardNumericFormats,
                              with new names, to avoid name collisions, should
                              both WiazedWrx.SharedUtl1 and WiazedWrx.SharedUtl2
						      simultaneously be in scope.

    2010/03/22 2.47    DAG    Add NUMBER_PER_REG_SETTINGS_0D, for use when you
                              want to use the NLS settings to format an integer.

    2010/04/04 2.51    DAG    Change the access modifier from sealed to static,
                              which implies sealed, but also signals the C#
                              compiler to flag creation of instance variables as
                              fatal errors.

    2015/06/20 5.5     DAG    Move from WizardWrX.SharedUtl2.dll to
                              WizardWrX.DLLServices2.dll, while retaining the
                              original namespace designation.

    2016/05/20 6.1     DAG    Eliminate pointless "end of" from bracket comments
                              throughout.

    2016/06/05 6.2     DAG    1) IntegerToHexStr distills every imaginable way
                                 of representing an integer in hexadecimal.

                              2) Thoroughly cross reference the constants and
                                 routines in this class.

							  3) Correct typographical errors in the internal
                                 documentation, and append the names to the 
                                 region ending tags.

							  4) Incorporate my three-clause BSD license, which
							     I just noticed was omitted from this module.

    2016/06/07 6.3     DAG    Adjust the internal documentation to correct a few
                              inconsistencies uncovered while preparing this
							  library for publication on GetHub.

	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
                              of classes can be consumed independently.

						      This module moved into WizardWrx.Common.dll, a new
                              dynamic-link library, but stays in the WizardWrx
                              root namespace, and acquired a new convenience
                              method, IntegerPerRegSettings, which behaves like
							  the NumberPerRegSettings method, when it is called
                              with its pintFractionDigits argument set to
                              DECIMAL_DIGITS_NONE (zero).

	2017/07/16 7.0     DAG    Replace references to string.empty, which is not a
                              true constant, with SpecialStrings.EMPTY_STRING,
                              which is one.

	2017/07/18 7.0     DAG    Remove the namespace qualification from the static
                              IntegerToHexStr method.

	2017/08/13 7.0     DAG    Label the closing bracket of IntegerPerRegSettings
                              method.

	2017/09/17 7.0     DAG    Define INTEGER_PER_REG_SETTINGS as an alias for
                              NUMBER_PER_REG_SETTINGS_0D.

	2018/11/10 7.11    DAG    1) Define IntegerToHexStr overloads that omit the
                                 second and third arguments, substituting common
                                 defaults for the missing arguments.

                              2) Change FormatStatusCode to use the simplified
                                 first overload, shortening its stack frame and
                                 call setup requirments.

	2019/05/15 7.17    DAG    Replace the ASCII table in the XML comments with a
                              well-formed XML table.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace WizardWrx
{
    /// <summary>
    /// This class defines standard numeric format strings, for use with the
    /// string.format method and its derivatives and relatives.
    /// </summary>
    public static class NumericFormats
	{
		#region Public Enumerations
		/// <summary>
		/// Use this enumeration with the third argument to the fourth
        /// (coprehensive) overload of IntegerToHexStr or the second argument to
        /// its second overload.
		/// </summary>
		[Flags]
		public enum HexFormatDecoration : byte
		{
			/// <summary>
			/// Just return the hexadecimal glyphs.
			/// </summary>
			None = 0 ,			// 0x00 (binary = 00000000) - Just return the hexadecimal glyphs.

			/// <summary>
			/// Prefix the hexadecimal glyphs with Ox.
			/// </summary>
			Prefix_Ox_LC = 1 ,  // 0x01 (binary = 00000001) - Prefix the hexadecimal glyphs with Ox.

			/// <summary>
			/// Prefix the hexadecimal glyphs with OX.
			/// </summary>
			Prefix_Ox_UC = 2 ,  // 0x02 (binary = 00000010) - Prefix the hexadecimal glyphs with OX.

			/// <summary>
			/// Prefix the hexadecimal glyphs with Oh.
			/// </summary>
			Prefix_Oh_LC = 3 ,  // 0x03 (binary = 00000011) - Prefix the hexadecimal glyphs with Oh.

			/// <summary>
			/// Prefix the hexadecimal glyphs with OH.
			/// </summary>
			Prefix_Oh_UC = 4 ,  // 0x04 (binary = 00000100) - Prefix the hexadecimal glyphs with OH.

			/// <summary>
			/// Suffix the hexadecimal glyphs with lower case h.
			/// </summary>
			Suffix_h_LC = 8 ,	// 0x08 (binary = 00001000) - Suffix the hexadecimal glyphs with lower case h.

			/// <summary>
			/// Suffix the hexadecimal glyphs with upper case h.
			/// </summary>
			Suffix_h_UC = 16 ,  // 0x10 (binary = 00010000) - Suffix the hexadecimal glyphs with upper case h.

			/// <summary>
			/// Use lower case hexadecimal glyphs.
			/// </summary>
			Glyphs_LC = 32 ,	// 0x20 (binary = 00100000) - Use lower case hexadecimal glyphs.

			/// <summary>
			/// Use upper case hexadecimal glyphs.
			/// </summary>
			Glyphs_UC = 64 ,	// 0x40 (binary = 01000000) - Use upper case hexadecimal glyphs.


			/// <summary>
			/// All prefix flags
			/// </summary>
			All_Prefixes = 10 , // 0x0A (binary = 00001010) - All prefix flags

			/// <summary>
			/// All suffix flags
			/// </summary>
			All_Suffixes = 24 , // 0x18 (binary = 00011000) - All suffix flags

			/// <summary>
			/// All glyph flags
			/// </summary>
			All_Glyphs = 96 ,	// 0x60 (binary = 01100000) - All glyph flags

			/// <summary>
			/// All glyph flags
			/// </summary>
			All_Flags = 130 ,	// 0x82 (binary = 10000010) - All flags, period
		}	// HexFormatDecoration
		#endregion	// Public Enumerations


		#region Public Constants
		/// <summary>
        /// Currency, which obeys the Regional Settings for currency, including
        /// the currency symbol and number of decimal places to show.
		/// 
		/// The negative sign comes from the regional settings, and leads the
		/// string, and the precision applies to the number of decimal
		/// (fraction) digits.
		/// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#CFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public const string CURRENCY = @"C";

        /// <summary>
        /// Decimal, which the documentation says is supported only for integral
        /// types. They mean business; if you try to use this type with a
		/// decimal or floating point number, you get an exception.
		/// 
		/// If the number requires fewer than the specified minimum number of
		/// digits, it is padded on the left with zeros, and there are no
		/// thousands separators.
		/// 
		/// The negative sign comes from the regional settings, and leads the
		/// string, and the precision applies to the number of digits to the
		/// left of the implied decimal point.
		/// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#DFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public const string DECIMAL = @"D";

        /// <summary>
        /// This is the minimal fixed point format string. Modifiers can be appended to it to specify significant digits after the decimal point.
		/// 
		/// The negative sign comes from the regional settings, and leads the
		/// string, and the precision applies to the number of decimal
		/// (fraction) digits.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static FixedPointDecimal ( int pintFractionDigits ) method,
		/// which supports any number between zero and ninety-nine, the limits
		/// imposed by the framework.
		/// </summary>
		/// <see cref="FixedPointDecimal()"/>
		/// <see cref="FixedPointDecimal(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#FFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="FIXED_2"/>
		/// <seealso cref="FIXED_3"/>
		public const string FIXED = @"F";

        /// <summary>
        /// This gives fixed point, with 2 places to the right of the decimal
        /// point, and without thousands separators.
        /// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#FFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="FIXED"/>
		/// <seealso cref="FIXED_3"/>
		/// <seealso cref="FixedPointDecimal()"/>
		/// <seealso cref="FixedPointDecimal(int)"/>
		public const string FIXED_2 = @"F2";

        /// <summary>
        /// This gives fixed point, with 3 places to the right of the decimal
		/// point, and without thousands separators.
        /// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#FFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="FIXED"/>
		/// <seealso cref="FIXED_2"/>
		/// <seealso cref="FixedPointDecimal()"/>
		/// <seealso cref="FixedPointDecimal(int)"/>
		public const string FIXED_3 = @"F3";


        /// <summary>
        /// This is the default, and it's pretty minimal. The documentation
        /// states that the number is converted to the most compact format.
        /// 
        /// When the size of the number and the specified precision dictate use
        /// of scientific notation, the exponential symbol is lower case; this
        /// is the only difference between this format and GENERAL_UC.
        /// 
        /// When the specified number of places after the decimal point is zero,
        /// the decimal point is omitted.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        /// <see cref="GENERAL_UC"/>
        /// <see cref="GeneralXPrecision(int)"/>
        /// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#GFormatString"/>
        /// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
        public const string GENERAL_LC = @"g";

        /// <summary>
        /// This is the default, and it's pretty minimal, as it must be, since
        /// its status as the default means that it is the only format that
        /// supports all data types (numbers, dates, times, time spans, and all
        /// the rest. It is also among the few that render output left aligned.
        /// 
        /// The documentation states that the number is converted to the most
        /// compact format.
        /// 
        /// When the size of the number and the specified precision dictate use
        /// of scientific notation, the exponential symbol is UPPER case; this
        /// is the only difference between this format and GENERAL_LC.
        /// 
        /// When the specified number of places after the decimal point is zero,
        /// the decimal point is omitted.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        /// <see cref="GENERAL_LC"/>
        /// <see cref="GeneralXPrecision(int)"/>
        /// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#GFormatString"/>
        /// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
        public const string GENERAL_UC = @"G";


        /// <summary>
        /// Use this string as your argument to the ToString method of any
        /// integral data type to format it in hexadecimal notation, which 
        /// the documentation says is supported only for integral types.
        /// They mean business; if you try to use this type with a decimal or
        /// floating point number, you get an FormatException exception.
        /// 
        /// This format yields a result containing the fewest hexadecimal glyphs
        /// required to represent the number. The difference between this token
        /// and HEXADECIMAL_LC is that this token causes the returned string to
        /// contain UPPER case glyphs, while HEXADECIMAL_LC yields lower case
        /// glyphs.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        /// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#XFormatString"/>
        /// <see cref="HexadecimalInteger()"/>
        /// <see cref="HexadecimalInteger(int)"/>
        /// <seealso cref="HEXADECIMAL_UC"/>
        /// <seealso cref="HEXADECIMAL_2"/>
        /// <seealso cref="HEXADECIMAL_4"/>
        /// <seealso cref="HEXADECIMAL_8"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
        /// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
        public const string HEXADECIMAL_LC = @"x";

        /// <summary>
		/// Use this string as your argument to the ToString method of any
		/// integral data type to format it in hexadecimal notation, which 
		/// the documentation says is supported only for integral types.
		/// They mean business; if you try to use this type with a decimal or
		/// floating point number, you get an FormatException exception.
		/// 
		/// This format yields a result containing the fewest hexadecimal glyphs
		/// required to represent the number. The difference between this token
		/// and HEXADECIMAL_LC is that this token causes the returned string to
		/// contain UPPER case glyphs, while HEXADECIMAL_LC yields lower case
		/// glyphs.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#XFormatString"/>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		/// <seealso cref="HEXADECIMAL_LC"/>
		/// <seealso cref="HEXADECIMAL_2"/>
		/// <seealso cref="HEXADECIMAL_4"/>
		/// <seealso cref="HEXADECIMAL_8"/>
		/// <seealso cref="HEXADECIMAL_16"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public const string HEXADECIMAL_UC = @"X";

        /// <summary>
        /// Use this string as your argument to the ToString method of any
        /// integral data type to format it in hexadecimal notation, which 
        /// the documentation says is supported only for integral types.
        /// They mean business; if you try to use this type with a decimal or
        /// floating point number, you get an FormatException exception.
        /// 
        /// This format yields a result containing a minimum of two hexadecimal
        /// numerals. If the number needs more than two numerals, the returned
        /// string contains the minimum number of hexadecimal numerals required
        /// to represent the integer.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        /// <see cref="HexadecimalInteger()"/>
        /// <see cref="HexadecimalInteger(int)"/>
        /// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
        /// <seealso cref="HEXADECIMAL_UC"/>
        /// <seealso cref="HEXADECIMAL_4"/>
        /// <seealso cref="HEXADECIMAL_8"/>
        /// <seealso cref="HEXADECIMAL_16"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
        /// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
        public const string HEXADECIMAL_2 = @"X2";

        /// <summary>
        /// Hexadecimal, which the documentation says is supported only for
        /// integral types. They mean business; if you try to use this type with
        /// a decimal or floating point number, you get an exception.
        /// 
        /// This format yields a result containing a minimum of four hexadecimal
        /// numerals. If the number needs more than two numerals, the returned
        /// string contains the minimum number of hexadecimal numerals required
        /// to represent the integer.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="HEXADECIMAL_UC"/>
		/// <seealso cref="HEXADECIMAL_2"/>
		/// <seealso cref="HEXADECIMAL_8"/>
		/// <seealso cref="HEXADECIMAL_16"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
		public const string HEXADECIMAL_4 = @"X4";

        /// <summary>
        /// Hexadecimal, which the documentation says is supported only for
        /// integral types. They mean business; if you try to use this type with
        /// a decimal or floating point number, you get an exception.
        /// 
        /// This format yields a result containing a minimum of 8 hexadecimal
        /// numerals. If the number needs more than two numerals, the returned
        /// string contains the minimum number of hexadecimal numerals required
        /// to represent the integer.
        /// 
        /// Use this format to represent result codes returned by Windows DLLs
        /// and other such functions that return things such as HRESULTs.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="HEXADECIMAL_UC"/>
		/// <seealso cref="HEXADECIMAL_2"/>
		/// <seealso cref="HEXADECIMAL_4"/>
		/// <seealso cref="HEXADECIMAL_16"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
		public const string HEXADECIMAL_8 = @"X8";

        /// <summary>
        /// Hexadecimal, which the documentation says is supported only for
        /// integral types. They mean business; if you try to use this type with
        /// a decimal or floating point number, you get an exception.
        /// 
        /// This format yields a result containing a minimum of 16 hexadecimal
        /// numerals. If the number needs more than 16 numerals, the returned
        /// string contains the minimum number of hexadecimal numerals required
        /// to represent the integer.
        /// 
        /// Use this format to represent long integer values, such as file sizes
        /// and 64 bit masks.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        public const string HEXADECIMAL_16 = @"X16";

        /// <summary>
        /// Not strictly a format string, this string is intended for use as a
		/// prefix for the string returned from a call to ToString with any of
        /// the HEXADECIMAL format strings.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		public const string HEXADECIMAL_PREFIX_0H_LC = @"0h";

        /// <summary>
        /// Not strictly a format string, this string is intended for use as a
		/// prefix for the string returned from a call to ToString with any of
        /// the HEXADECIMAL format strings.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		public const string HEXADECIMAL_PREFIX_0H_UC = @"0H";

        /// <summary>
        /// Not strictly a format string, this string is intended for use as a
		/// prefix for the string returned from a call to ToString with any
        /// of the HEXADECIMAL format strings.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		public const string HEXADECIMAL_PREFIX_0X_LC = @"0x";

        /// <summary>
        /// Not strictly a format string, this string is intended for use as a
        /// prefix for the string returned from a call to ToString with any
        /// of the HEXADECIMAL format strings.
		/// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
		/// </summary>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		public const string HEXADECIMAL_PREFIX_0X_UC = @"0X";

        /// <summary>
        /// Not strictly a format string, this string is intended for use as a
        /// suffix for the string returned from a call to ToString with any of
        /// the HEXADECIMAL format strings.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        public const string HEXADECIMAL_SUFFIX_H_LC = @"h";

        /// <summary>
        /// Not strictly a format string, this string is intended for use as a
        /// suffix for the string returned from a call to ToString with any of
        /// the HEXADECIMAL format strings.
        /// 
        /// To easily specify the desired minimum number of hexadecimal glyphs,
        /// along with any of the prefixes and suffixes defined in this set of
        /// HEXADECIMAL_* constants, call static method IntegerToHexStr
        /// ( [T] , int pintTotalDigits , HexFormatDecoration penmDecoration ),
        /// in lieu of calling ToString on the integer.
        /// </summary>
        public const string HEXADECIMAL_SUFFIX_H_UC = @"H";

		/// <summary>
		/// Format an integer per the Regional Settings in the Windows Control
		/// Panel. Sine this string is intended exclusively for formatting an
		/// Integer, it overrides the default regional settings value for Number
		/// of Digits After Decimal. Please see the Remarks for additional
		/// details.
		/// </summary>
		/// <remarks>
		/// Since this string is intended exclusively for formatting an
		/// Integer, it overrides the default regional settings value, which is
		/// the value reported when GetLocaleInfo is called with its LCType
		/// argument set to LOCALE_IDIGITS (0x00000011, per WinNLS.h), which
		/// lives at HKEY_CURRENT_USER\Control Panel\International[iDigits] in
		/// the Windows Registry, and corresponds to the "No. of digits after
		/// decimal" property shown on the Numbers tab displayed by the
		/// Additional Settings button on the Region and Language Windows
		/// Control Panel applet dialog box.
		/// </remarks>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		public const string INTEGER_PER_REG_SETTINGS = NUMBER_PER_REG_SETTINGS_0D;

		/// <summary>
        /// Format a number (any numeric type), using the Regional Settings
		/// (Locale) defaults for thousands separator, decimal symbol, number of
		/// decimal (fraction) places, and number of digits to display between
		/// each thousands separator.
		/// 
		/// Use this string as your argument to the ToString method of any
		/// numeric data type to format it in decimal notation, with zero or
		/// more places after the decimal point.
		/// 
		/// When the specified number of places after the decimal point is zero,
		/// the decimal point is omitted.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static NumberPerRegSettings ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the Base Class Library.
		/// 
		/// A shortcut to get an integer formatted is IntegerPerRegSettings.
		/// </summary>
		/// <see cref="IntegerPerRegSettings"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_2D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_3D"/>
		public const string NUMBER_PER_REG_SETTINGS = @"N";

		/// <summary>
		/// Feed this string to the ToString method of any numeric value to
		/// format it according to the Regional Settings (Locale) defaults for
		/// thousands separator, decimal symbol, and number of digits to display
		/// between each thousands separator.
		/// 
		/// In contrast to NUMBER_PER_REG_SETTINGS, this string overrides the
		/// default decimal places value, and always displays ZERO digits to the
		/// right of the decimal point.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static NumberPerRegSettings ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the Base Class Library.
		/// 
		/// A shortcut to get an integer formatted is IntegerPerRegSettings, and
		/// an alias, which avoids the overhead of a method call, is
		/// INTEGER_PER_REG_SETTINGS.
		/// </summary>
		/// <remarks>
		/// Since this string is intended exclusively for formatting an
		/// Integer, it overrides the default regional settings value, which is
		/// the value reported when GetLocaleInfo is called with its LCType
		/// argument set to LOCALE_IDIGITS (0x00000011, per WinNLS.h), which
		/// lives at HKEY_CURRENT_USER\Control Panel\International[iDigits] in
		/// the Windows Registry, and corresponds to the "No. of digits after
		/// decimal" property shown on the Numbers tab displayed by the
		/// Additional Settings button on the Region and Language Windows
		/// Control Panel applet dialog box.
		/// </remarks>
		/// <see cref="IntegerPerRegSettings"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="INTEGER_PER_REG_SETTINGS"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_2D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_3D"/>
		public const string NUMBER_PER_REG_SETTINGS_0D = @"N0";

        /// <summary>
		/// Feed this string to the ToString method of any numeric value to
		/// format it according to the Regional Settings (Locale) defaults for
		/// thousands separator, decimal symbol, and number of digits to display
		/// between each thousands separator.
		/// 
        /// In contrast to NUMBER_PER_REG_SETTINGS, this string overrides the
        /// default decimal places value, and always displays two digits to the
        /// right of the decimal point.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static NumberPerRegSettings ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the Base Class Library.
		/// 
		/// A shortcut to get an integer formatted is IntegerPerRegSettings.
		/// </summary>
		/// <see cref="IntegerPerRegSettings"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_3D"/>
		public const string NUMBER_PER_REG_SETTINGS_2D = @"N2";

        /// <summary>
		/// Feed this string to the ToString method of any numeric value to
		/// format it according to the Regional Settings (Locale) defaults for
		/// thousands separator, decimal symbol, and number of digits to display
		/// between each thousands separator.
		/// 
        /// In contrast to NUMBER_PER_REG_SETTINGS, this string overrides the
        /// default decimal places value, and always displays three digits to
        /// the right of the decimal point.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static NumberPerRegSettings ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the Base Class Library.
		/// 
		/// A shortcut to get an integer formatted is IntegerPerRegSettings.
		/// </summary>
		/// <see cref="IntegerPerRegSettings"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_2D"/>
		public const string NUMBER_PER_REG_SETTINGS_3D = @"N3";


        /// <summary>
        /// Display a fixed point number as a percentage, using the default
		/// number of decimal places, per the Regional Settings (Locale). The
		/// number is multiplied by 100 before the formatting is applied. Hence,
		/// 0.25 renders as 25%.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static PercentToDecimalPlaces ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the framework.
		/// </summary>
		/// <see cref="PercentToDecimalPlaces()"/>
		/// <see cref="PercentToDecimalPlaces(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#PFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="PERCENT_DIGITS_2"/>
		public const string PERCENT = @"P";

        /// <summary>
        /// Display a fixed point number as a percentage, showing two digits to
        /// the right of the decimal point.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static PercentToDecimalPlaces ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the framework.
		/// </summary>
		/// <see cref="PercentToDecimalPlaces()"/>
		/// <see cref="PercentToDecimalPlaces(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#PFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="PERCENT"/>
		public const string PERCENT_DIGITS_2 = @"P2";


		/// <summary>
		/// Since the formatting engine ignores if for this type, there is no
		/// corresponding format string generator, and only one token, since
		/// there is no documented difference between the upper and lower case
		/// token. Before you use this token, read the documentation cited below
		/// carefully.
		/// </summary>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#RFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#GFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx"/>
		public const string ROUND_TRIP = @"R";


		/// <summary>
        /// Scientific notation, with a lower case "e" for the exponent. Exactly one digit always precedes the decimal point.
		/// 
		/// To specify the number of places to print after the decimal point,
		/// use the static PercentToDecimalPlaces ( int pintFractionDigits ) 
		/// method, which supports any number between zero and ninety-nine, the
		/// limits imposed by the framework.
		/// </summary>
		/// <see cref="ScientificXPrecisionLC(int)"/>
		/// <see cref="ScientificXPrecisionUC(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#EFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public const string SCIENTIFIC_LC = @"e";


        /// <summary>
		/// Scientific notation, with an upper case "E" for the exponent. Exactly one digit always precedes the decimal point.
        /// </summary>
		/// <see cref="ScientificXPrecisionLC(int)"/>
		/// <see cref="ScientificXPrecisionUC(int)"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#EFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public const string SCIENTIFIC_UC = @"E";

        /// <summary>
        /// For some applications, such as the static methods of this class, we
        /// set our own default number of decimal places, rather than relying on
        /// the Regional Settings (Locale), which can be changed, at any time,
        /// by any user, even with restricted permissions.
        /// <list type="table">
        /// <listheader>
        /// <term>Format String</term>
        /// <term>Generator Routine</term>
        /// <term>Width Override</term>
        /// </listheader>
        /// <item>
        /// <term>DECIMAL</term>
        /// <term>FixedWidthInteger</term>
        /// <term>int pintTotalDigits</term>
        /// </item>
        /// <item>
        /// <term>FIXED</term>
        /// <term>FixedPointDecimal</term>
        /// <term>int pintFractionDigits</term>
        /// </item>
        /// <item>
        /// <term>GENERAL</term>
        /// <term>GeneralXPrecision</term>
        /// <term>int pintPrecisionDigits [1]</term>
        /// </item>
        /// <item>
        /// <term>HEXADECIMAL</term>
        /// <term>HexadecimalInteger</term>
        /// <term>int pintTotalDigits</term>
        /// </item>
        /// <item>
        /// <term>NUMBER_PER_REG_SETTINGS</term>
        /// <term>IntegerPerRegSettings</term>
        /// <term>N /A</term>
        /// </item>
        /// <item>
        /// <term>NUMBER_PER_REG_SETTINGS</term>
        /// <term>NumberPerRegSettings</term>
        /// <term>int pintFractionDigits</term>
        /// </item>
        /// <item>
        /// <term>PERCENT</term>
        /// <term>PercentToDecimalPlaces</term>
        /// <term>int pintFractionDigits</term>
        /// </item>
        /// </list>
		/// Note 1: There is only one of these methods, and it accepts the
		/// pintTotalDigits argument.
		/// </summary>
		/// <see cref="FixedWidthInteger()"/>
		/// <see cref="FixedWidthInteger(int)"/>
		/// <see cref="FixedPointDecimal()"/>
		/// <see cref="FixedPointDecimal(int)"/>
		/// <see cref="GeneralXPrecision(int)"/>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see cref="PercentToDecimalPlaces()"/>
		/// <see cref="PercentToDecimalPlaces(int)"/>
		/// <see cref="ScientificXPrecisionLC(int)"/>
		/// <see cref="ScientificXPrecisionLC(int)"/>
		/// <see cref="ScientificXPrecisionUC(int)"/>
		/// <seealso cref="DECIMAL_DIGITS_MIN"/>
		/// <seealso cref="DECIMAL_DIGITS_MAX"/>
		/// <seealso cref="DECIMAL_DIGITS_NONE"/>
		public const int DECIMAL_DIGITS_DEFAULT = 2;

        /// <summary>
        /// The static format string generator methods in this class test the
        /// requested number of decimal points against this lower limit.
        /// 
        /// If the specified number is less than this value, the methods behave
        /// as if the default had been specified.
        /// </summary>
		/// <see cref="FixedWidthInteger()"/>
		/// <see cref="FixedWidthInteger(int)"/>
		/// <see cref="FixedPointDecimal()"/>
		/// <see cref="FixedPointDecimal(int)"/>
		/// <see cref="GeneralXPrecision(int)"/>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see cref="PercentToDecimalPlaces()"/>
		/// <see cref="PercentToDecimalPlaces(int)"/>
		/// <see cref="ScientificXPrecisionLC(int)"/>
		/// <see cref="ScientificXPrecisionUC(int)"/>
		/// <seealso cref="DECIMAL_DIGITS_DEFAULT"/>
		/// <seealso cref="DECIMAL_DIGITS_MAX"/>
		public const int DECIMAL_DIGITS_MIN = 0;


		/// <summary>
        /// The static format string generator methods in this class test the
        /// requested number of decimal points against this upper limit.
        /// 
        /// If the specified number is greater than this value, the methods
        /// behave as if the default had been specified.
        /// </summary>
		/// <see cref="FixedWidthInteger()"/>
		/// <see cref="FixedWidthInteger(int)"/>
		/// <see cref="FixedPointDecimal()"/>
		/// <see cref="FixedPointDecimal(int)"/>
		/// <see cref="GeneralXPrecision(int)"/>
		/// <see cref="HexadecimalInteger()"/>
		/// <see cref="HexadecimalInteger(int)"/>
		/// <see cref="NumberPerRegSettings()"/>
		/// <see cref="NumberPerRegSettings(int)"/>
		/// <see cref="PercentToDecimalPlaces()"/>
		/// <see cref="PercentToDecimalPlaces(int)"/>
		/// <see cref="ScientificXPrecisionLC(int)"/>
		/// <see cref="ScientificXPrecisionUC(int)"/>
		/// <seealso cref="DECIMAL_DIGITS_DEFAULT"/>
		/// <seealso cref="DECIMAL_DIGITS_MIN"/>
		/// <seealso cref="DECIMAL_DIGITS_NONE"/>
		public const int DECIMAL_DIGITS_MAX = 99;

		/// <summary>
		/// This is a synonym of DECIMAL_DIGITS_MIN.
		/// </summary>
		public const int DECIMAL_DIGITS_NONE = DECIMAL_DIGITS_MIN;
		#endregion	// Public Constants


		#region Private Constants
		private const string FORMAT_TEMPLATE = @"{0}{1}";
		private const string INTEGER_AS_HEX_TPL = @"{0:$$TOKEN$$}";
		private const string INTEGER_AS_HEX_GLYPH = @"$$TOKEN$$";
		private const int HEX_GLYPHS_DEFAULT_MINIMUM = 8;
		#endregion	// Private Constants


		#region Static FixedPointDecimal Methods
		/// <summary>
        /// Return a string suitable for formatting a fixed point number.
        /// </summary>
        /// <returns>
        /// Format string for formatting a fixed point number with the default
        /// number of digits to the right of the decimal point.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#FFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="FIXED"/>
		/// <seealso cref="FIXED_2"/>
		/// <seealso cref="FIXED_3"/>
		/// <seealso cref="DECIMAL_DIGITS_DEFAULT"/>
		/// <seealso cref="DECIMAL_DIGITS_MIN"/>
		/// <seealso cref="DECIMAL_DIGITS_MAX"/>
		/// <seealso cref="DECIMAL_DIGITS_NONE"/>
		public static string FixedPointDecimal ( )
		{
			return string.Format (
				FORMAT_TEMPLATE ,
				FIXED ,
				DECIMAL_DIGITS_DEFAULT );
		}   // public static FixedPointDecimal method (1 of 2)


        /// <summary>
        /// Return a string suitable for formatting a fixed point number.
        /// </summary>
        /// <param name="pintFractionDigits">
        /// Number of digits to allow to the right of the decimal point.
        /// </param>
        /// <returns>
        /// Format string for formatting a fixed point number with the specified
        /// number of digits to the right of the decimal point.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#FFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="FIXED"/>
		/// <seealso cref="FIXED_2"/>
		/// <seealso cref="FIXED_3"/>
		/// <seealso cref="DECIMAL_DIGITS_DEFAULT"/>
		/// <seealso cref="DECIMAL_DIGITS_MIN"/>
		/// <seealso cref="DECIMAL_DIGITS_MAX"/>
		/// <seealso cref="DECIMAL_DIGITS_NONE"/>
		public static string FixedPointDecimal ( int pintFractionDigits )
		{
			if ( pintFractionDigits >= DECIMAL_DIGITS_MIN && pintFractionDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					FIXED ,
					pintFractionDigits );
			else
				return string.Format (
					FORMAT_TEMPLATE ,
					FIXED ,
					DECIMAL_DIGITS_DEFAULT );
		}   // public static FixedPointDecimal method (2 of 2)
		#endregion	// Static FixedPointDecimal Methods


		#region Static FixedWidthInteger Methods
		/// <summary>
        /// Return a string suitable for formatting a fixed width integer.
        /// </summary>
        /// <returns>
        /// Format string for formatting a fixed width integer, with the default
        /// number of digits.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#DFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public static string FixedWidthInteger ( )
		{
			return string.Format (
				FORMAT_TEMPLATE ,
				DECIMAL ,
				DECIMAL_DIGITS_DEFAULT );
		}	// public static FixedWidthInteger method (1 of 2)

        /// <summary>
        /// Return a string suitable for formatting a fixed width integer.
        /// </summary>
        /// <param name="pintTotalDigits">
        /// Minimum number of digits to return in the formatted number.
        /// </param>
        /// <returns>
        /// Format string for formatting a fixed width integer, with the specified
        /// number of digits.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#DFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public static string FixedWidthInteger ( int pintTotalDigits )
		{
			if ( pintTotalDigits >= DECIMAL_DIGITS_MIN && pintTotalDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					DECIMAL ,
					pintTotalDigits );
			else
				return string.Format (
					FORMAT_TEMPLATE ,
					DECIMAL ,
					DECIMAL_DIGITS_DEFAULT );
		}   // public static FixedWidthInteger method (2 of 2)
        #endregion // Static FixedWidthInteger Methods


        #region Static FormatStatusCode Methods
        /// <summary>
        /// Return a string representation of a status code, formatted as
        /// hexadecimal, followed by the decimal format in parentheses.
        /// </summary>
        /// <param name="pintStatusCode">
        /// Though intended for use with status codes, any integer will do.
        /// </param>
        /// <returns>
        /// Return a string something like 0x000000ff (255 decimal).
        /// </returns>
        public static string FormatStatusCode ( int pintStatusCode )
        {
            return string.Format (
                Common.Properties.Resources.MSG_STATUS_CODE ,
                IntegerToHexStr ( pintStatusCode ) ,
                pintStatusCode );
        }   // FormatStatusCode
		#endregion // Static FormatStatusCode Methods


		#region Static GeneralXPrecision Methods
		/// <summary>
		/// Return a string suitable for formatting any numeric value.
		/// </summary>
		/// <param name="pintPrecisionDigits">
		/// Maximum number of digits to return in the formatted number.
		/// </param>
		/// <returns>
		/// Format string for formatting any numeric value, with the specified
		/// number of digits.
		/// </returns>
		/// <remarks>
		/// Since the General format differs significantly in intent and
		/// behavior, I chose to provide only one version of its format string
		/// generator, which takes an integer, which specifies the maximum
		/// number of digits to display. If more digits are required, General
		/// format reverts to scientific notation.
		/// </remarks>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#GFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		public static string GeneralXPrecision ( int pintPrecisionDigits )
		{
			if ( pintPrecisionDigits >= DECIMAL_DIGITS_MIN && pintPrecisionDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					GENERAL_UC ,
					pintPrecisionDigits );
			else
				return GENERAL_UC;
		}   // public static GeneralXPrecision method (1 of 1)
		#endregion	// Static GeneralXPrecision Methods


		#region Static HexadecimalInteger Methods
		/// <summary>
        /// Return a string suitable for formatting a hexadecimal representation
        /// of an integer with the default maximum number of numerals.
        /// </summary>
        /// <returns>
        /// Format string for formatting an integer as a hexadecimal number.
        /// </returns>
		/// <see cref="HEXADECIMAL_UC"/>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#XFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
		public static string HexadecimalInteger ( )
		{
			return string.Format (
				FORMAT_TEMPLATE ,
				HEXADECIMAL_UC ,
				DECIMAL_DIGITS_DEFAULT );
		}   // public static HexadecimalInteger method (1 of 2)


        /// <summary>
        /// Return a string suitable for formatting a hexadecimal representation
        /// of an integer with the specified minimum number of numerals.
        /// </summary>
        /// <param name="pintTotalDigits">
        /// Maximum number of digits to return in the formatted number.
        /// </param>
        /// <returns>
        /// Format string for formatting an integer as a hexadecimal number
        /// containing the specified minimum number of numerals.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#XFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0H_UC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_LC"/>
		/// <seealso cref="HEXADECIMAL_PREFIX_0X_UC"/>
		public static string HexadecimalInteger ( 
			int pintTotalDigits )
		{
			if ( pintTotalDigits >= DECIMAL_DIGITS_MIN && pintTotalDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					HEXADECIMAL_UC ,
					pintTotalDigits );
			else
				return string.Format (
					FORMAT_TEMPLATE ,
					HEXADECIMAL_UC ,
					DECIMAL_DIGITS_DEFAULT );
		}   // public static HexadecimalInteger method (2 of 2)


        /// <summary>
        /// Properly formatting an integral type as a hexadecimal string,
        /// including the decorations commonly applied to them as prefix or
        /// suffix, is a bit beyond the reach of a simple ToString format
        /// string.
        /// </summary>
        /// <typeparam name="T">
        /// There are no compiler enforced constraints on this type, because the
        /// Base Class Library provides no mechanism to differentiate integral
        /// types in constraints for generics. See the remarks for a comment about
        /// how we get around this.
        /// </typeparam>
        /// <param name="pintegralValue">
        /// Specify the value to be formatted as hexadecimal. Integral types
        /// only, because that's the only type that may be formatted in this
        /// way.
        /// </param>
        /// <returns>
        /// The returned string consists of the prefix, immediately followed by
        /// the hexadecimal number, itself, and, finally, the suffix. 
        /// 
        /// Theoretically, you can have both a prefix and a suffix, but the idea
        /// is to have one or the other, but not both. You can mix and match
        /// upper and lower case glyphs in the main number string and the
        /// decorations.
        /// </returns>
        public static string IntegerToHexStr<T> ( T pintegralValue )
        {
            return IntegerToHexStr (
                pintegralValue ,
                HEX_GLYPHS_DEFAULT_MINIMUM ,
                HexFormatDecoration.Prefix_Ox_LC );
        }   // IntegerToHexStr (1 of 4)


        /// <summary>
        /// Properly formatting an integral type as a hexadecimal string,
        /// including the decorations commonly applied to them as prefix or
        /// suffix, is a bit beyond the reach of a simple ToString format
        /// string.
        /// </summary>
        /// <typeparam name="T">
        /// There are no compiler enforced constraints on this type, because the
        /// Base Class Library provides no mechanism to differentiate integral
        /// types in constraints for generics. See the remarks for a comment about
        /// how we get around this.
        /// </typeparam>
        /// <param name="pintegralValue">
        /// Specify the value to be formatted as hexadecimal. Integral types
        /// only, because that's the only type that may be formatted in this
        /// way.
        /// </param>
        /// <param name="penmHexDecoration">
        /// The HexFormatDecoration has the Flags attribute set on it, so that
        /// it can be processed as a bit mask, enabling it to specify multiple
        /// items.
        /// </param>
        /// <returns>
        /// The returned string consists of the prefix, immediately followed by
        /// the hexadecimal number, itself, and, finally, the suffix. 
        /// 
        /// Theoretically, you can have both a prefix and a suffix, but the idea
        /// is to have one or the other, but not both. You can mix and match
        /// upper and lower case glyphs in the main number string and the
        /// decorations.
        /// </returns>
        public static string IntegerToHexStr<T> (
            T pintegralValue ,
            HexFormatDecoration penmHexDecoration )
        {
            return IntegerToHexStr (
                pintegralValue ,
                HEX_GLYPHS_DEFAULT_MINIMUM ,
                penmHexDecoration );
        }   // IntegerToHexStr (2 of 4)


        /// <summary>
        /// Properly formatting an integral type as a hexadecimal string,
        /// including the decorations commonly applied to them as prefix or
        /// suffix, is a bit beyond the reach of a simple ToString format
        /// string.
        /// </summary>
        /// <typeparam name="T">
        /// There are no compiler enforced constraints on this type, because the
        /// Base Class Library provides no mechanism to differentiate integral
        /// types in constraints for generics. See the remarks for a comment about
        /// how we get around this.
        /// </typeparam>
        /// <param name="pintegralValue">
        /// Specify the value to be formatted as hexadecimal. Integral types
        /// only, because that's the only type that may be formatted in this
        /// way.
        /// </param>
        /// <param name="pintTotalDigits">
        /// Specify the minimum number of hexadecimal "digits" (glyphs, really)
        /// to render. If the number needs more than the specified number, the
        /// method uses as many as it needs, causing the returned string to be
        /// longer than you expected. If the string needs fewer characters, it
        /// is left padded with zeros.
        /// </param>
        /// <returns>
        /// The returned string consists of the prefix, immediately followed by
        /// the hexadecimal number, itself, and, finally, the suffix. 
        /// 
        /// Theoretically, you can have both a prefix and a suffix, but the idea
        /// is to have one or the other, but not both. You can mix and match
        /// upper and lower case glyphs in the main number string and the
        /// decorations.
        /// </returns>
        /// <remarks>
        /// The available options are overloading the single-argument ToString
        /// method on all fourteen integer types, or crafting one generic method
        /// that takes the integer to format as its first argument. Since it's a
        /// lot less work, I went that route.
        /// 
        /// Although this method uses generics, there is no type constraint,
        /// because the Base Class Library offers no such constraint to filter
        /// integral types, of which there are at least fourteen, not counting
        /// BigInteger. Since the compiler won't enforce a type constraint, I
        /// wrote my own routine that enforces it at run time, by searching a
        /// table of known integral types, identified by their GUID properties.
        /// If the type of pintegralValue matches an entry in the list, the
        /// input is accepted. Otherwise, you get an ArgumentException exception
        /// that clearly explains what happened and why.
        /// </remarks>
        public static string IntegerToHexStr<T> (
            T pintegralValue ,
            int pintTotalDigits )
        {
            return IntegerToHexStr (
                pintegralValue ,
                pintTotalDigits ,
                HexFormatDecoration.Prefix_Ox_LC );
        }   // IntegerToHexStr (3 of 4)


        /// <summary>
        /// Properly formatting an integral type as a hexadecimal string,
        /// including the decorations commonly applied to them as prefix or
        /// suffix, is a bit beyond the reach of a simple ToString format
        /// string.
        /// </summary>
        /// <typeparam name="T">
        /// There are no compiler enforced constraints on this type, because the
        /// Base Class Library provides no mechanism to differentiate integral
        /// types in constraints for generics. See the remarks for a comment about
        /// how we get around this.
        /// </typeparam>
        /// <param name="pintegralValue">
        /// Specify the value to be formatted as hexadecimal. Integral types
        /// only, because that's the only type that may be formatted in this
        /// way.
        /// </param>
        /// <param name="pintTotalDigits">
        /// Specify the minimum number of hexadecimal "digits" (glyphs, really)
        /// to render. If the number needs more than the specified number, the
        /// method uses as many as it needs, causing the returned string to be
        /// longer than you expected. If the string needs fewer characters, it
        /// is left padded with zeros.
        /// </param>
        /// <param name="penmHexDecoration">
        /// The HexFormatDecoration has the Flags attribute set on it, so that
        /// it can be processed as a bit mask, enabling it to specify multiple
        /// items.
        /// </param>
        /// <returns>
        /// The returned string consists of the prefix, immediately followed by
        /// the hexadecimal number, itself, and, finally, the suffix. 
        /// 
        /// Theoretically, you can have both a prefix and a suffix, but the idea
        /// is to have one or the other, but not both. You can mix and match
        /// upper and lower case glyphs in the main number string and the
        /// decorations.
        /// </returns>
        /// <remarks>
        /// The available options are overloading the single-argument ToString
        /// method on all fourteen integer types, or crafting one generic method
        /// that takes the integer to format as its first argument. Since it's a
        /// lot less work, I went that route.
        /// 
        /// Although this method uses generics, there is no type constraint,
        /// because the Base Class Library offers no such constraint to filter
        /// integral types, of which there are at least fourteen, not counting
        /// BigInteger. Since the compiler won't enforce a type constraint, I
        /// wrote my own routine that enforces it at run time, by searching a
        /// table of known integral types, identified by their GUID properties.
        /// If the type of pintegralValue matches an entry in the list, the
        /// input is accepted. Otherwise, you get an ArgumentException exception
        /// that clearly explains what happened and why.
        /// </remarks>
        public static string IntegerToHexStr<T> (
			T pintegralValue ,
			int pintTotalDigits ,
			HexFormatDecoration penmHexDecoration )
		{
			Type typIputType=pintegralValue.GetType ( );											// Save the type, in case we need it when we throw an exception.

			if ( BitHelpers.InfoForIntegralType ( typIputType ) == null )							// Unless the type is some kind of integer, InfoForIntegralType returns NULL.
				throw new ArgumentException (
					string.Format (																	// Message for the exception
						Common.Properties.Resources.ERRMSG_INTEGER2HEXSTR_INVALID_INPUT ,			// Format control string
						pintegralValue ,															// Format Item 0 = string representation of actual value
						typIputType.FullName ,														// Format Item 1 = System.Type of actual value
						Environment.NewLine ) );													// Format Item 2 = Embedded Newline

			int intMinimumDigits = (    pintTotalDigits >= DECIMAL_DIGITS_MIN						// Is value at or above the required minimum?
				                     && pintTotalDigits <= DECIMAL_DIGITS_MAX )						// Is value at or below the required maximum?
										? pintTotalDigits											// Yes, so use it.
										: HEX_GLYPHS_DEFAULT_MINIMUM;								// No, use the hard coded default value.

			string strGlyphToken = null;
			string strPrefix = null;
			string strSuffix = null;

			//	----------------------------------------------------------------
			//	While it might not be quite as sexy as a switch block, I'll bet
			//	these old fashioned If/else if blocks get the job done and are
			//	about as efficient, given the small number of choices in each
			//	group.
			//	----------------------------------------------------------------

			if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Glyphs_LC ) == ( int ) HexFormatDecoration.Glyphs_LC )
				strGlyphToken = HEXADECIMAL_LC;
			else if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Glyphs_UC ) == ( int ) HexFormatDecoration.Glyphs_UC )
				strGlyphToken = HEXADECIMAL_UC;
			else
				strGlyphToken = HEXADECIMAL_UC;

			if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Prefix_Oh_LC ) == ( int ) HexFormatDecoration.Prefix_Oh_LC )
				strPrefix = HEXADECIMAL_PREFIX_0H_LC; 
			else if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Prefix_Oh_UC ) == ( int ) HexFormatDecoration.Prefix_Oh_UC )
				strPrefix = HEXADECIMAL_PREFIX_0H_UC;
			else if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Prefix_Ox_LC ) == ( int ) HexFormatDecoration.Prefix_Ox_LC )
				strPrefix = HEXADECIMAL_PREFIX_0X_LC;
			else if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Prefix_Ox_UC ) == ( int ) HexFormatDecoration.Prefix_Ox_UC )
				strPrefix = HEXADECIMAL_PREFIX_0X_UC;

			if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Suffix_h_LC ) == ( int ) HexFormatDecoration.Suffix_h_LC )
				strSuffix = HEXADECIMAL_SUFFIX_H_LC;
			else if ( ( int ) ( penmHexDecoration & HexFormatDecoration.Suffix_h_UC ) == ( int ) HexFormatDecoration.Suffix_h_UC )
				strSuffix = HEXADECIMAL_SUFFIX_H_UC;

			//	----------------------------------------------------------------
			//	The format string is constructed from FORMAT_TEMPLATE, the same
			//	template that virtually every other routine in this class uses,
			//	but it is coded inline, rather than by working through any of
			//	those routines. The middle part of the final string wraps a
			//	format call around a token substitution.
			//	----------------------------------------------------------------

			string strIntegerFormatString = string.Format (
				FORMAT_TEMPLATE ,							// Format control string
				strGlyphToken ?? HEXADECIMAL_UC ,			// Format item 0 = Identified glyph token, or default if null
				pintTotalDigits );							// Format Item 1 = Minimum number of glyphs to display.

			return string.Concat (
				strPrefix ?? SpecialStrings.EMPTY_STRING ,	// Use strPrefix unless it is null, in which case, substitute the empty string.
				string.Format (								// Use our generated format control string to format the integral value as hex.
					INTEGER_AS_HEX_TPL.Replace (			//		Finish constructing it by replacing
						INTEGER_AS_HEX_GLYPH ,				//		the place holder token that occupies the third part of the composite item
						strIntegerFormatString ) ,			//		with the numeric format string that we just constructed
					pintegralValue ) ,						//		There is a single format item; it gets the integral value.
				strSuffix ?? SpecialStrings.EMPTY_STRING );	// Append strSuffix unless it is null, in which case, substitute the empty string.
		}   // IntegerToHexStr (4 of 4)
		#endregion // Static HexadecimalInteger Methods


		#region Static IntegerPerRegSettings Methods
		/// <summary>
		/// Return a string suitable for formatting an integer per the Regional
		/// Settings, overriding the default number of digits to display to the
		/// right of the decimal point to display zero digits to the right of
		/// the decimal point.
		/// </summary>
		/// <returns>
		/// Format string suitable for formatting an integer
		/// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <see cref="NUMBER_PER_REG_SETTINGS"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_2D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_3D"/>
		public static string IntegerPerRegSettings ( )
		{
			return string.Format (
				FORMAT_TEMPLATE ,
				NUMBER_PER_REG_SETTINGS ,
				DECIMAL_DIGITS_NONE );
		}	// IntegerPerRegSettings
		#endregion	// Static IntegerPerRegSettings Methods


		#region Static NumberPerRegSettings Methods
		/// <summary>
        /// Return a string suitable for formatting an integer or fixed point
        /// number, per the Regional Settings, overriding the default number of
        /// digits to display to the right of the decimal point, if necessary,
        /// to display two digits to the right of the decimal point.
        /// </summary>
        /// <returns>
        /// Format string suitable for formatting an integer or fixed point
        /// number
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <see cref="NUMBER_PER_REG_SETTINGS"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="NumberPerRegSettings()"/>
		/// <seealso cref="NumberPerRegSettings(int)"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_2D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_3D"/>
		public static string NumberPerRegSettings ( )
		{
			return string.Format (
				FORMAT_TEMPLATE ,
				NUMBER_PER_REG_SETTINGS ,
				DECIMAL_DIGITS_DEFAULT );
		}   // public static NumberPerRegSettings method (1 of 2)


        /// <summary>
        /// Return a string suitable for formatting an integer or fixed point
        /// number, per the Regional Settings, overriding the default number of
        /// digits to display to the right of the decimal point, if necessary,
        /// to display a specified number of digits to the right of the decimal
        /// point.
        /// </summary>
        /// <param name="pintFractionDigits">
        /// Maximum number of digits to return in the formatted number.
        /// </param>
        /// <returns>
        /// Format string suitable for formatting an integer or fixed point
        /// number with a specified number of digits to the right of the decimal
        /// point.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#NFormatString"/>
		/// <see cref="NUMBER_PER_REG_SETTINGS"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_0D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_2D"/>
		/// <seealso cref="NUMBER_PER_REG_SETTINGS_3D"/>
		public static string NumberPerRegSettings ( int pintFractionDigits )
		{
			if ( pintFractionDigits >= DECIMAL_DIGITS_MIN && pintFractionDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					NUMBER_PER_REG_SETTINGS ,
					pintFractionDigits );
			else
				return string.Format (
					FORMAT_TEMPLATE ,
					NUMBER_PER_REG_SETTINGS ,
					DECIMAL_DIGITS_DEFAULT );
		}  // public static NumberPerRegSettings method (2 of 2)
		#endregion	// Static NumberPerRegSettings Methods


		#region Static PercentToDecimalPlaces methods
		/// <summary>
        /// Return a string suitable for formatting an integer or fixed point
        /// number as a percentage, with two places to the right of the decimal
        /// point.
        /// </summary>
        /// <returns>
        /// Format string suitable for formatting an integer or fixed point
        /// number as a percentage.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#PFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="PERCENT"/>
		/// <seealso cref="PERCENT_DIGITS_2"/>
		public static string PercentToDecimalPlaces ( )
		{
			return string.Format (
				FORMAT_TEMPLATE ,
				PERCENT ,
				DECIMAL_DIGITS_DEFAULT );
		}   // public static PercentToDecimalPlaces method (1 of 2)


        /// <summary>
        /// Return a string suitable for formatting an integer or fixed point
        /// number as a percentage, with two places to the right of the decimal
        /// point.
        /// </summary>
        /// <param name="pintFractionDigits">
        /// Maximum number of digits to return in the formatted number.
        /// </param>
        /// <returns>
        /// Format string suitable for formatting an integer or fixed point
        /// number as a percentage, displaying a specified number of digits to
        /// the right of the decimal point.
        /// </returns>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#PFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="PERCENT"/>
		/// <seealso cref="PERCENT_DIGITS_2"/>
		public static string PercentToDecimalPlaces ( int pintFractionDigits )
		{
			if ( pintFractionDigits >= DECIMAL_DIGITS_MIN && pintFractionDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					PERCENT ,
					pintFractionDigits );
			else
				return string.Format (
					FORMAT_TEMPLATE ,
					PERCENT ,
					DECIMAL_DIGITS_DEFAULT );
		}   // public static PercentToDecimalPlaces method (2 of 2)
		#endregion	// Static PercentToDecimalPlaces methods


		#region Static ScientificXPrecision Methods
		/// <summary>
        /// Return a string suitable for formatting any numeric value in
        /// scientific notation.
        /// </summary>
        /// <param name="pintFractionDigits">
        /// Maximum number of digits to return in the formatted number.
        /// </param>
        /// <returns>
        /// Format string for formatting any numeric value, with the specified
        /// number of digits, in scientific notation.
        /// 
        /// The exponent is lower case.
        /// </returns>
        /// <remarks>
        /// Since the Scientific format differs significantly in intent and
        /// behavior, I chose to provide only one version of its format string
        /// generator, which takes an integer, which specifies the maximum
        /// number of digits to display.
        /// </remarks>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#EFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="ScientificXPrecisionUC"/>
		/// <seealso cref="SCIENTIFIC_LC"/>
		/// <seealso cref="SCIENTIFIC_UC"/>
		public static string ScientificXPrecisionLC ( int pintFractionDigits )
		{
			if ( pintFractionDigits >= DECIMAL_DIGITS_MIN && pintFractionDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					SCIENTIFIC_LC ,
					pintFractionDigits );
			else
				return SCIENTIFIC_LC;
		}   // public static ScientificXPrecisionLC method (1 of 1)


        /// <summary>
        /// Return a string suitable for formatting any numeric value in
        /// scientific notation.
        /// </summary>
        /// <param name="pintFractionDigits">
        /// Maximum number of digits to return in the formatted number.
        /// </param>
        /// <returns>
        /// Format string for formatting any numeric value, with the specified
        /// number of digits, in scientific notation.
        /// 
        /// The exponent is lower case.
        /// </returns>
        /// <remarks>
        /// Since the Scientific format differs significantly in intent and
        /// behavior, I chose to provide only one version of its format string
        /// generator, which takes an integer, which specifies the maximum
        /// number of digits to display.
        /// </remarks>
		/// <see href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx#EFormatString"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.100).aspx"/>
		/// <seealso cref="ScientificXPrecisionLC"/>
		/// <seealso cref="SCIENTIFIC_LC"/>
		/// <seealso cref="SCIENTIFIC_UC"/>
		public static string ScientificXPrecisionUC ( int pintFractionDigits )
		{
			if ( pintFractionDigits >= DECIMAL_DIGITS_MIN && pintFractionDigits <= DECIMAL_DIGITS_MAX )
				return string.Format (
					FORMAT_TEMPLATE ,
					SCIENTIFIC_UC ,
					pintFractionDigits );
			else
				return SCIENTIFIC_UC;
		}   // public static ScientificXPrecisionUC method (1 of 1)
		#endregion	// Static ScientificXPrecision Methods
	}   // NumericFormats class
}   // WizardWrx namespace