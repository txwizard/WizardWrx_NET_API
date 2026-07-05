/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         DisplayFormats

    File Name:          DisplayFormats.cs

    Synopsis:           Pass these constants to the ToString method on an object
                        of the appropriate type to render the object for display
                        or printing.

                        Use these service methods to facilitate using the
                        Date/Time formatting constants, which cannot be fed to
                        ToString, except, perhaps, with a custom formatting
                        engine.

    Remarks:            The comment associated with each constant identifies the
                        types for which it is appropriate.

                        Use these service methods, or call the ReformatSysDate
                        function, which also belongs to this library, directly.

                        The time formatting strings and routines in this class
                        are time zone agnostic. If you want or need the time
                        zone, use the companion method, GetDisplayTimeZone,
                        defined in sibling class Util (This API is available 
						only in libraries that target .NET Framework 3.5 or
						later.

						As of version 6.2, the numeric formatting strings are
						referred to a dedicated sibling class, NumericFormats.
						The date and time formatting strings already refer to
						another dedicated sibling class, SysDateFormatters. This
						change (at version 6.2) means that formatting string
						definition maintenance happens in the dedicated classes.
						However, rather than deprecate these, I am leaving them,
						because they transform this class into a single point of
						access to all of the standard formatting strings defined
						in this library.

    Author:             David A. Gray

    License:            Copyright (C) 2014-2017, David A. Gray. 
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

    Created:            Sunday, 14 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/14 5.2     DAG    Initial implementation.

	2015/06/20 5.5     DAG    Relocate to WizardWrx namespace and
                              class library.

	2016/06/04 6.2     DAG    To lighten the maintenance burden, forward the
                              numeric format strings to sibling NumericFormats.


	2017/03/25 7.0     DAG    Break this library apart, so that smaller subsets
                              of classes can be consumed independently.

						      This module moved into WizardWrx.Common.dll, a new
                              dynamic-link library, but stays in the WizardWrx
                              root namespace.

                              Add a mapping for the 16 numeral hexadecimal long
                              integer format string.

	2017/09/17 7.0     DAG    Define INTEGER_PER_REG_SETTINGS as an alias for
                              NUMBER_PER_REG_SETTINGS_0D, both actually defined
                              by the much richer NumericFormats class.
    ============================================================================
*/


using System;

namespace WizardWrx
{
    /// <summary>
    /// Pass these constants to the ToString method on an object of the
    /// appropriate type to render the object for printing.
    /// 
    /// The comment associated with each constant identifies the types for
    /// which it is appropriate.
    /// 
    /// There are service methods to facilitate using the Date/Time formatting
    /// constants, which cannot be fed to ToString, except, perhaps, with a
    /// custom formatting engine. Use these service methods, or call the
    /// ReformatSysDate function, which also belongs to this library, directly.
    /// 
    /// NOTE: The time formatting strings and routines in this class are time
    /// zone agnostic. If you want or need the time zone, use the companion
    /// method, GetDisplayTimeZone, defined in sibling class Util.
    /// </summary>

	public static class DisplayFormats
    {
        #region Convenience Constants
		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as an arbitrary string of hexadecimal digits, using lower
		/// case glyphs..
		/// </summary>
		public const string HEXADECIMAL_LC = NumericFormats.HEXADECIMAL_LC;

        /// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as an arbitrary string of hexadecimal digits, using upper
		/// case glyphs..
		/// </summary>
		public const string HEXADECIMAL_UC = NumericFormats.HEXADECIMAL_UC;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it as a string of 2 hexadecimal digits.
        /// </summary>
		public const string HEXADECIMAL_2 = NumericFormats.HEXADECIMAL_2;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it as a string of 4 hexadecimal digits.
        /// </summary>
		public const string HEXADECIMAL_4 = NumericFormats.HEXADECIMAL_4;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it as a string of 8 hexadecimal digits.
        /// </summary>
		public const string HEXADECIMAL_8 = NumericFormats.HEXADECIMAL_8;

		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as a string of 16 hexadecimal digits.
		/// </summary>
		public const string HEXADECIMAL_16 = NumericFormats.HEXADECIMAL_16;

        /// <summary>
        /// Substitute this into a format string as a prefix to a hexadecimal
        /// number display. This string renders exactly as shown, 0h.
        /// </summary>
		public const string HEXADECIMAL_PREFIX_0H_LC = NumericFormats.HEXADECIMAL_PREFIX_0H_LC;

        /// <summary>
        /// Substitute this into a format string as a prefix to a hexadecimal
        /// number display. This string renders exactly as shown, 0H.
        /// </summary>
		public const string HEXADECIMAL_PREFIX_0H_UC = NumericFormats.HEXADECIMAL_PREFIX_0H_UC;

        /// <summary>
        /// Substitute this into a format string as a prefix to a hexadecimal
        /// number display. This string renders exactly as shown, 0x.
        /// </summary>
		public const string HEXADECIMAL_PREFIX_0X_LC = NumericFormats.HEXADECIMAL_PREFIX_0X_LC;

        /// <summary>
        /// Substitute this into a format string as a prefix to a hexadecimal
        /// number display. This string renders exactly as shown, 0X.
        /// </summary>
		public const string HEXADECIMAL_PREFIX_0X_UC = NumericFormats.HEXADECIMAL_PREFIX_0X_UC;

		/// <summary>
		/// Use this string to format an integer per the Regional Settings
		/// applet in the Windows Control Panel, overriding its default setting
		/// for digits after the decimal point, which is meaningless for an
		/// integer.
		/// </summary>
		public const string INTEGER_PER_REG_SETTINGS = NumericFormats.NUMBER_PER_REG_SETTINGS_0D;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it according to the current settings in the Regional Settings
        /// part of the Windows Control Panel.
        /// 
        /// This format string causes the formatting engine to obey ALL of the
        /// settings, including the number of digits to display after the
        /// decimal point for a whole number.
        /// </summary>
		public const string NUMBER_PER_REG_SETTINGS = NumericFormats.NUMBER_PER_REG_SETTINGS;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it according to the current settings in the Regional Settings
        /// part of the Windows Control Panel.
        /// 
        /// This format string causes the formatting engine to obey all of the
        /// settings, EXCEPT the number of digits to display after the decimal
        /// point for a whole number.
        /// 
        /// This format string overrides the digits after decimal value
        /// specified by the iDigits value of Windows Registry key
        /// HKCU\Control Panel\International, causing it to behave as if it had
        /// been set to 0.
        /// 
        /// The override applies only to the instance ToString method being
        /// called; the Registry is unchanged.
        /// 
        /// See http://technet.microsoft.com/en-us/library/cc978638.aspx.
        /// </summary>
		public const string NUMBER_PER_REG_SETTINGS_0D = NumericFormats.NUMBER_PER_REG_SETTINGS_0D;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it according to the current settings in the Regional Settings
        /// part of the Windows Control Panel.
        /// 
        /// This format string causes the formatting engine to obey all of the
        /// settings, EXCEPT the number of digits to display after the decimal
        /// point for a whole number.
        /// 
        /// This format string overrides the digits after decimal value
        /// specified by the iDigits value of Windows Registry key
        /// HKCU\Control Panel\International, causing it to behave as if it had
        /// been set to 2, which happens to be the default for a US
        /// installation. Nevertheless, uses this value if changes made by the
        /// user would mess up your work.
        /// 
        /// The override applies only to the instance ToString method being
        /// called; the Registry is unchanged.
        /// 
        /// See http://technet.microsoft.com/en-us/library/cc978638.aspx.
        /// </summary>
		public const string NUMBER_PER_REG_SETTINGS_2D = NumericFormats.NUMBER_PER_REG_SETTINGS_2D;

        /// <summary>
        /// Pass this constant to the ToString method on any integral type to
        /// format it according to the current settings in the Regional Settings
        /// part of the Windows Control Panel.
        /// 
        /// This format string causes the formatting engine to obey all of the
        /// settings, EXCEPT the number of digits to display after the decimal
        /// point for a whole number.
        /// 
        /// This format string overrides the digits after decimal value
        /// specified by the iDigits value of Windows Registry key
        /// HKCU\Control Panel\International, causing it to behave as if it had
        /// been set to 3.
        /// 
        /// The override applies only to the instance ToString method being
        /// called; the Registry is unchanged.
        /// 
        /// See http://technet.microsoft.com/en-us/library/cc978638.aspx.
        /// </summary>
		public const string NUMBER_PER_REG_SETTINGS_3D = NumericFormats.NUMBER_PER_REG_SETTINGS_3D;

        /// <summary>
        /// Pass this constant to the ToString method on a single or double
        /// precision floating point number to be displayed as an integral
        /// percentage.
        /// </summary>
		public const string PERCENT = NumericFormats.PERCENT;

        /// <summary>
        /// Pass this constant to the ToString method on a single or double
        /// precision floating point number to be displayed as a fixed point
        /// percentage, accurate to two decimal places.
        /// </summary>
		public const string PERCENT_DIGITS_2 = NumericFormats.PERCENT_DIGITS_2;
        #endregion  // Convenience Constants
    }   // public static class DisplayFormats
}   // partial namespace WizardWrx