/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:         ByteArrayFormatters

    File Name:          ByteArrayFormatters.cs

    Synopsis:           Use the routines in this static class to format byte
                        arrays as hexadecimal strings for display on reports.

    Remarks:            These routines moved up the food chain to make them
                        more readily accessible to sibling libraries.

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

    Created:            Friday, 29 August 2014 - Wednesday, 03 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/09/03 1.0     DAG    Initial implementation.

    2014/09/12 1.0     DAG    Moved from DllUtl Class in EZXFER_Cloud_Manager and
                              merged FormatDateForShow from TimeZoneLab.

    2014/09/15 5.2     DAG    Permanently move to WizardWrx.ApplicationHelpers3
                              class library.

    2015/06/06 5.4     DAG    Correct the logic error in IsLastForIterationLE,
                              and add IsLastForIterationLT, which puts the bad
                              logic that I put into IsLastForIterationLE to some
                              productive use. Give MoreForIterationsToComeLE the
                              same treatment.

    2015/06/20 5.5     DAG    Move ByteArrayToHexDigitString from SharedUtl4,
                              for use by routines that must display a REG_BINARY
                              value.
 
	2017/03/27 7.0     DAG    This class makes its debut.
    ============================================================================
*/


using System;
using System.Text;

namespace WizardWrx.Core
{
	/// <summary>
	/// The static methods in this class format byte arrays into strings of
	/// hexadecimal numerals for display on reports and in windows.
	/// </summary>
	public static class ByteArrayFormatters
	{
		#region Public Constants
		/// <summary>
		/// Use this to set ByteArrayToHexDigitString argument puintGroupSize to
		/// insert a space between every 4th byte.
		/// </summary>
		public const uint BYTES_TO_STRING_BLOCK_OF_4 = 4;

		/// <summary>
		/// Use this to set ByteArrayToHexDigitString argument puintGroupSize to
		/// insert a space between every 8th byte.
		/// </summary>
		public const uint BYTES_TO_STRING_BLOCK_OF_8 = 8;

		/// <summary>
		/// Use this to set ByteArrayToHexDigitString argument puintGroupSize to
		/// format the string without any spaces.
		/// </summary>
		/// <remarks>
		/// This constant is intended primarily for internal use by the first
		/// overload, which omits the second argument, to call the second
		/// overload, which does the work.
		/// </remarks>
		public const uint BYTES_TO_STRING_NO_SPACING = 0;


		#endregion  // Public Constants
		/// <summary>
		/// Convert a byte array into a printable hexadecimal representation.
		/// </summary>
		/// <param name="pbytInputData">
		/// Specify the byte array to be formatted. Any byte array will do.
		/// </param>
		/// <returns>
		/// The return value is a string that contains two characters for each
		/// byte in the array.
		/// </returns>
		public static string ByteArrayToHexDigitString ( byte [ ] pbytInputData )
		{
			return ByteArrayToHexDigitString (
				pbytInputData ,
				BYTES_TO_STRING_NO_SPACING );
		}   // ByteArrayToHexDigitString (1 of 2)

		/// <summary>
		/// Convert a byte array into a printable hexadecimal representation.
		/// </summary>
		/// <param name="pbytInputData">
		/// Specify the byte array to be formatted. Any byte array will do.
		/// </param>
		/// <param name="puintGroupSize">
		/// Specify the number of bytes to display as a group.
		/// </param>
		/// <returns>
		/// The return value is a string that contains two characters for each
		/// byte in the array, plus one space between every puintGroupSizeth
		/// byte.
		/// </returns>
		public static string ByteArrayToHexDigitString (
			byte [ ] pbytInputData ,
			uint puintGroupSize )
		{
			StringBuilder sbOutput = new StringBuilder ( pbytInputData.Length );

			//  ----------------------------------------------------------------
			//  Loop through each byte of the hashed data, and format each one
			//  as a hexadecimal string. Although this For loop will never
			//  contain more than one statement, I left the braces to separate
			//  that statement from the third line of the For statement, which I
			//  spread across three lines because of its length.
			//  ----------------------------------------------------------------

			for ( int intOffset = ArrayInfo.ARRAY_FIRST_ELEMENT ;
				  intOffset < pbytInputData.Length ;
				  intOffset++ )
			{
				if ( puintGroupSize > 0 && intOffset > 0 && intOffset % puintGroupSize == 0 )
					sbOutput.Append ( SpecialCharacters.SPACE_CHAR );

				sbOutput.Append ( pbytInputData [ intOffset ].ToString ( DisplayFormats.HEXADECIMAL_2 ).ToLowerInvariant ( ) );
			}   //  for ( int intOffset = MagicNumbers.ARRAY_FIRST_ELEMENT ; ...

			return sbOutput.ToString ( );       //  Return the hexadecimal string.
		}   // ByteArrayToHexDigitString (2 of 2)
	}	// public static class ByteArrayFormatters
}	// partial namespace WizardWrx.Core