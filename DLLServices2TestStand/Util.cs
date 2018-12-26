/*
    ============================================================================

    Namespace:          DLLServices2TestStand

    Class Name:         Util

    File Name:          Util.cs

    Synopsis:           This static class exposes utility methods for use by the
                        DLLServices2TestStand project.

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

    2018/12/23 7.14    DAG    Add coverage for unsigned 32 and 64 bit integers.
	============================================================================
*/

using System;
using WizardWrx;

namespace DLLServices2TestStand
{
    internal class Util
    {
        internal static string FormatIntegerValue ( int pintToShow )
        {
            return string.Format (
                @"{0} (Formatted: {1}, Hexadecimal: 0x{2})" ,                           // Format Control String
                pintToShow ,                                                            // Format Item 0: Default rendering
                pintToShow.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) ,       // Format Item 1: Formatted with thousands separator
                pintToShow.ToString ( NumericFormats.HEXADECIMAL_8 ) );                 // Formatted as hexadecimal.
        }   // internal static string DisplayIntegerValue (1 of 4)

        internal static string FormatIntegerValue ( uint pintToShow )
        {
            return string.Format (
                @"{0} (Formatted: {1}, Hexadecimal: 0x{2})" ,                           // Format Control String
                pintToShow ,                                                            // Format Item 0: Default rendering
                pintToShow.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) ,       // Format Item 1: Formatted with thousands separator
                pintToShow.ToString ( NumericFormats.HEXADECIMAL_8 ) );                 // Formatted as hexadecimal.
        }   // internal static string DisplayIntegerValue (3 of 4)

        internal static string FormatIntegerValue ( long plngToShow )
        {
            return string.Format (
                @"{0} (Formatted: {1}, Hexadecimal: 0x{2})" ,                           // Format Control String
                plngToShow ,                                                            // Format Item 0: Default rendering
                plngToShow.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) ,       // Format Item 1: Formatted with thousands separator
                plngToShow.ToString ( NumericFormats.HEXADECIMAL_16 ) );                // Formatted as hexadecimal.
        }   // internal static string DisplayIntegerValue (2 of 4)

        internal static string FormatIntegerValue ( ulong plngToShow )
        {
            return string.Format (
                @"{0} (Formatted: {1}, Hexadecimal: 0x{2})" ,                           // Format Control String
                plngToShow ,                                                            // Format Item 0: Default rendering
                plngToShow.ToString ( NumericFormats.INTEGER_PER_REG_SETTINGS ) ,       // Format Item 1: Formatted with thousands separator
                plngToShow.ToString ( NumericFormats.HEXADECIMAL_16 ) );                // Formatted as hexadecimal.
        }   // internal static string DisplayIntegerValue (4 of 4)
    }   // internal class Util
}   // partial namespace DLLServices2TestStand