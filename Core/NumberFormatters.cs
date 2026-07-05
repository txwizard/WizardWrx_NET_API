/*
    ============================================================================

    Namespace Name:     WizardWrx

    Class Name:         NumberFormatters

    File Name:          NumberFormatters.cs

    Synopsis:           This class implements my stalwart ReformatSysDate_P6C
                        date formatting algorithm as 100% managed code, and adds
						some wrapper methods that invoke it with the most common
						format strings.

    Remarks:            Though it is exposed at the root level of the WizardWrx
						namespace, this class is implemented by a separate
						assembly, WizardWrx.Core.dll. Keeping the class in the
						root namespace puts it in the same namespace as its date
                        formatter sibling, SysDateFormatters, and NumericFormats
                        (which defines the format strings that it consumes).

						This class is implicitly sealed. Instances of it cannot
                        be created, and the class cannot be inherited.

    License:            Copyright (C) 2017, David A. Gray. 
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
    2017/03/25 7.0     DAG    This class makes its first appearance.
    ============================================================================
*/

using System;

namespace WizardWrx
{
	/// <summary>
	/// This static class exposes methods to further simplify formatting of
	/// integers, floating point (single precision real numbers) and double
	/// precision real numbers. For integers, these methods cover both decimal
	/// and the most frequently used hexadecimal representations, spanning from
	/// two through sixteen hexadecimal glyphs.
	/// </summary>
	/// <remarks>
	/// Since everything returns a string, the values can be appended to arrays
	/// of format items to use as inputs to the most open-ended overload of the
	/// string.Format method.
	/// </remarks>
	public static class NumberFormatters
	{
		#region DecimalNumber Formatters
		/// <summary>
		/// Format any double precision number per the Regional Settings,
		/// overriding the digits past the decimal point to render a specified
		/// number of significant digits to the right of the decimal point.
		/// </summary>
		/// <param name="pdblDecimalNumber">
		/// Floating point value to be formatted
		/// </param>
		/// <param name="pintSignificantDigits">
		/// Number of significant digits to display
		/// </param>
		/// <returns>
		/// Floating point value, ready to display
		/// </returns>
		public static string DecimalNumber ( double pdblDecimalNumber , int pintSignificantDigits )
		{
			return pdblDecimalNumber.ToString ( NumericFormats.NumberPerRegSettings ( pintSignificantDigits ) );
		}	// public static string DecimalNumber

		/// <summary>
		/// Format any floating point number per the Regional Settings, 
		/// overriding the digits past the decimal point to render a specified
		/// number of significant digits to the right of the decimal point.
		/// </summary>
		/// <param name="pfltDecimalNumber">
		/// Floating point value to be formatted
		/// </param>
		/// <param name="pintSignificantDigits">
		/// Number of significant digits to display
		/// </param>
		/// <returns>
		/// Floating point value, ready to display
		/// </returns>
		public static string DecimalNumber ( float pfltDecimalNumber , int pintSignificantDigits )
		{
			return pfltDecimalNumber.ToString ( NumericFormats.NumberPerRegSettings ( pintSignificantDigits ) );
		}	// public static string DecimalNumber
		#endregion	// DecimalNumber Formatters


		#region DollarsAndCents Formatters
		/// <summary>
		/// Format any double precision number per the Regional Settings, 
		/// overriding the digits past the decimal point to render dollars and
		/// cents.
		/// </summary>
		/// <param name="pdblDollarsAndCents">
		/// Floating point value to be formatted
		/// </param>
		/// <returns>
		/// Floating point value, ready to display
		/// </returns>
		public static string DollarsAndCents ( double pdblDollarsAndCents )
		{
			return pdblDollarsAndCents.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_2D );
		}	// public static string DollarsAndCents

		/// <summary>
		/// Format any floating point number per the Regional Settings, 
		/// overriding the digits past the decimal point to render dollars and
		/// cents.
		/// </summary>
		/// <param name="pfltDollarsAndCents">
		/// Floating point value to be formatted
		/// </param>
		/// <returns>
		/// Floating point value, ready to display
		/// </returns>
		public static string DollarsAndCents ( float pfltDollarsAndCents )
		{
			return pfltDollarsAndCents.ToString ( NumericFormats.NUMBER_PER_REG_SETTINGS_2D );
		}	// public static string DollarsAndCents
		#endregion	// DollarsAndCents Formatters


		#region Hexadecimal Integer Formatters
		/// <summary>
		/// Format any integer to display as a minimum of two hexadecimal
		/// "digits."
		/// </summary>
		/// <param name="pintAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		public static string Hexadecimal2 ( int pintAnyInteger )
		{
			return string.Concat (
				NumericFormats.HEXADECIMAL_PREFIX_0X_LC ,
				pintAnyInteger.ToString ( NumericFormats.HEXADECIMAL_2 ) );
		}	// public static string Hexadecimal2

		/// <summary>
		/// Format any integer to display as a minimum of four hexadecimal
		/// "digits."
		/// </summary>
		/// <param name="pintAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		public static string Hexadecimal4 ( int pintAnyInteger )
		{
			return string.Concat (
				NumericFormats.HEXADECIMAL_PREFIX_0X_LC ,
				pintAnyInteger.ToString ( NumericFormats.HEXADECIMAL_4 ) );
		}	// public static string Hexadecimal4

		/// <summary>
		/// Format any integer to display as a minimum of eight hexadecimal
		/// "digits."
		/// </summary>
		/// <param name="pintAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		public static string Hexadecimal8 ( int pintAnyInteger )
		{
			return string.Concat (
				NumericFormats.HEXADECIMAL_PREFIX_0X_LC ,
				pintAnyInteger.ToString ( NumericFormats.HEXADECIMAL_8 ) );
		}	// public static string Hexadecimal8

		/// <summary>
		/// Format any long integer to display as a minimum of sixteen hexadecimal
		/// "digits."
		/// </summary>
		/// <param name="plngAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		public static string Hexadecimal16 ( long plngAnyInteger )
		{
			return string.Concat (
				NumericFormats.HEXADECIMAL_PREFIX_0X_LC ,
				plngAnyInteger.ToString ( NumericFormats.HEXADECIMAL_16 ) );
		}	// public static string Hexadecimal16
		#endregion	// Hexadecimal Integer Formatters


		#region Integer Formatters
		/// <summary>
		/// Format any integer per the Regional Settings, overriding the digits
		/// past the decimal point to render an integer.
		/// </summary>
		/// <param name="pintAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		public static string Integer ( int pintAnyInteger )
		{
			return pintAnyInteger.ToString ( NumericFormats.IntegerPerRegSettings ( ) );
		}	// public static string Integer

		/// <summary>
		/// Format any long integer per the Regional Settings, overriding the
		/// digits past the decimal point to render an long integer.
		/// </summary>
		/// <param name="plngAnyInteger">
		/// Integer to be formatted
		/// </param>
		/// <returns>
		/// Formatted integer, ready to display
		/// </returns>
		public static string Integer ( long plngAnyInteger )
		{
			return plngAnyInteger.ToString ( NumericFormats.IntegerPerRegSettings ( ) );
		}	// public static string Integer
		#endregion	// Integer Formatters
	}	// public static class NumberFormatters
}	// partial namespace WizardWrx