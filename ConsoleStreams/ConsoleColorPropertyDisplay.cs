/*
    ============================================================================

    Namespace:          WizardWrx.ConsoleStreams

    Class Name:         ConsoleColorPropertyDisplay

    File Name:          ConsoleColorPropertyDisplay.cs

    Synopsis:           This static class exposes one static method that
						implements the ToString override of the MessageInColor
						and ErrorMessageInColor classes to display the text
						(foreground) and background color properties, followed
						by the fully qualified name of the calling class.

    Remarks:            Though it implements an output that resembles one used
						by many classes of the Base Class Library, I didn't look
						at their source to figure out how they did it, since it
						isn't exactly rocket science.
 
						This class exposes one method, MessageColorsToString,
						which the ToString overrides on the MessageInColor and
						ErrorMessageInColor classes invoke to implement their
						ToString method overrides. Since both implementations
	`					are identical, it made sense to share the code.

    Author:             David A. Gray

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

    Created:            Tuesday, 11 July 2017

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2017/07/12 7.0     DAG    Initial implementation.
    ============================================================================
*/

using System;

namespace WizardWrx.ConsoleStreams
{
	internal static class ConsoleColorPropertyDisplay
	{
		internal static string MessageColorsToString ( ConsoleColor pclrText , ConsoleColor pclrBackground , Type ptypMessageType )
		{
			return string.Format (
				Properties.Resources.CONSOLE_COLORS_TOSTRING_TEMPLATE ,			// Format Control String
				ptypMessageType.FullName ,										// Format Item 0: }} {2}
				pclrText ,														// Format Item 1: Text (Foreground) = {0},
				pclrBackground );												// Format Item 2: Background = {1}
		}	// internal static string MessageColorsToString
	}	// internal static class ConsoleColorPropertyDisplay
}	// partial namespace WizardWrx.ConsoleStreams