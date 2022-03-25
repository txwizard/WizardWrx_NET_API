/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         TimeZoneInfoExtensions

    File Name:          TimeZoneInfoExtensions.cs

    Synopsis:           This static class implements extension methods on the
                        TimeZoneInfo class that provide abbreviated renderings
                        of the time zone name strings.

    Remarks:            Extension methods are the only way to extend a
                        TimeZoneInfo object.

	License:            Copyright (C) 2021-2022, David A. Gray.
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

    Created:            Sunday, 31 August 2014 - Tuesday, 01 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2021/10/12 3.0     DAG    Initial implementation.

    2021/10/13 8.0.252 DAG    Migrate from TimeZoneLab, where the class was
                              perfected.

    2022/03/24 8.0.269 DAG    Implement a new method, GetCurrentTimeZoneName,
                              that takes a DateTime structure, along with the
                              TimeZoneInfo instance that it extends, and returns
                              the correct display time zone as either the
                              appropriate instance property or the extended
                              abbreviated property.
    ============================================================================
*/


using System;
using System.Text;


namespace WizardWrx
{
    /// <summary>
    /// Extend the sealed TimeZoneInfo class to support abbreviated time zone
    /// names.
    /// </summary>
    public static class TimeZoneInfoExtensions
    {
        #region Public Extension Method
        /// <summary>
        /// Get the abbreviated time zone Daylight Name.
        /// </summary>
        /// <param name="ptzi">
        /// Specify the TimeZoneInfo object to process. This implicit paramteer
        /// is supplied by the runtime when the extension method is called.
        /// </param>
        /// <returns>
        /// The abbreviated name is constructed from the name specified on the
        /// DaylightName property on the TimeZoneInfo object by extracting from
        /// it the first letter of each word.
        /// </returns>
        public static string AbbreviateDaylightName ( this TimeZoneInfo ptzi )
        {
            return AbbreviateAnyTZName ( ptzi.DaylightName );
        }   // public static string AbbreviateDaylightName


        /// <summary>
        /// Get the abbreviated time zone Display Name.
        /// </summary>
        /// <param name="ptzi">
        /// Specify the TimeZoneInfo object to process. This implicit paramteer
        /// is supplied by the runtime when the extension method is called.
        /// </param>
        /// <returns>
        /// <para>
        /// The abbreviated name is constructed from the name specified on the
        /// DisplayName property on the TimeZoneInfo object by extracting from
        /// it the first letter of each word.
        /// </para>
        /// <para>
        /// The Display Name sees much less use because it includes the UTC
        /// offset, which confuses users who are unfamiliar with the details of
        /// UTC in particular and time zones in general.
        /// </para>
        /// </returns>
        public static string AbbreviateDisplayName ( this TimeZoneInfo ptzi )
        {
            return AbbreviateAnyTZName ( ptzi.DisplayName );
        }   // public static string AbbreviateDisplayName


        /// <summary>
        /// Get the abbreviated time zone Standard Name.
        /// </summary>
        /// <param name="ptzi">
        /// Specify the TimeZoneInfo object to process. This implicit paramteer
        /// is supplied by the runtime when the extension method is called.
        /// </param>
        /// <returns>
        /// The abbreviated name is constructed from the name specified on the
        /// StandardName property on the TimeZoneInfo object by extracting from
        /// it the first letter of each word.
        /// </returns>
        public static string AbbreviatedStandardName ( this TimeZoneInfo ptzi )
        {
            return AbbreviateAnyTZName ( ptzi.StandardName );
        }   // public static string AbbreviatedStandardName


        /// <summary>
        /// Given a DateTime object to establish whether to display the Stanadrd
        /// Name or the Daylight Name, return the appropriate string for the
        /// part of the year that it represents.
        /// </summary>
        /// <param name="ptzi">
        /// Specify the TimeZoneInfo object to process. This implicit paramteer
        /// is supplied by the runtime when the extension method is called.
        /// </param>
        /// <param name="pdtmDateTime">
        /// The IsDaylightSavingTime method on the <paramref name="ptzi"/>
        /// instance establishes the correct string to return for the time that
        /// it represents, assuming that its DateTimeKind property is Local or
        /// that it is implicitly interpreted as such.
        /// </param>
        /// <param name="pfAbbreviate">
        /// When True, this flag causes the abbreviated time zone name to be
        /// displayed. Otherwise, the appropriate string from the extended
        /// TimeZoneInfo instance is returned.
        /// </param>
        /// <returns>
        /// The return value is the standard or daylight time zone name string
        /// or abbreviation that represents the time specified by argument
        /// <paramref name="pdtmDateTime"/> as directed by argument
        /// <paramref name="pfAbbreviate"/>.
        /// </returns>
        public static string GetCurrentTimeZoneName ( this TimeZoneInfo ptzi , DateTime pdtmDateTime , bool pfAbbreviate )
        {
            if ( ptzi.IsDaylightSavingTime ( pdtmDateTime ) )
            {
                if ( pfAbbreviate )
                {   // At the cost of theoretical correctness, shorten the call stack by bypassing the extension method.
                    return AbbreviateAnyTZName ( ptzi.DaylightName );
                }   // TRUE (The caller wants the abbreviated time zone name.) block, if ( pfAbbreviate )
                else
                {   // Return the full DaylightName string.
                    return ptzi.DaylightName;
                }   // FALSE (The caller wants the regular time zone name.) block, if ( pfAbbreviate )
            }   // TRUE (The current time is in the Daylight Saving Time portion of the year in the current time zone.) block, if ( ptzi.IsDaylightSavingTime ( pdtmDateTime ) )
            else
            {
                if ( pfAbbreviate )
                {   // At the cost of theoretical correctness, shorten the call stack by bypassing the extension method.
                    return AbbreviateAnyTZName ( ptzi.StandardName );
                }   // TRUE (The caller wants the abbreviated time zone name.) block, if ( pfAbbreviate )
                else
                {   // Return the full StandardName string.
                    return ptzi.StandardName;
                }   // FALSE (The caller wants the regular time zone name.) block, if ( pfAbbreviate )
            }   // FALSE (The current time is in the Standard Time portion of the year in the current time zone.) block, if ( ptzi.IsDaylightSavingTime ( pdtmDateTime ) )
        }   // public static string GetCurrentTimeZoneName
        #endregion  // Public Extension Method


        #region Private Helper Methods
        /// <summary>
        /// Construct the abbreviated time zone name.
        /// </summary>
        /// <param name="pstrTimeZoneName">
        /// This string specifies one of the three time zone name properties on
        /// the TimeZoneInfo object that was passed into the method that called
        /// it.
        /// </param>
        /// <returns>
        /// <para>
        /// The abbreviated name is constructed from the name specified on the
        /// StandardName property on the TimeZoneInfo object by extracting from
        /// it the first letter of each word.
        /// </para>
        /// <para>
        /// Two special cases exist.
        /// </para>
        /// <list type="number">
        /// <item>
        /// Regional time zones (e .g., Mexico) display the region in
        /// parentheses. The abbreviations enclose the first character of the
        /// region name in parentheses.
        /// </item>
        /// <item>
        /// Time zones that are known only by their UTC offsets have names like
        /// "UTC+11" that are abbreviated as "U+11" by taking the plus character
        /// and everything that follows it.
        /// </item>
        /// </list>
        /// </returns>
        private static string AbbreviateAnyTZName ( string pstrTimeZoneName )
        {
            int intNameLength = pstrTimeZoneName.Length;
            bool fIsFirstCharacterOfWord = true;
            bool fTakeRemainingCharacters = false;

            StringBuilder rsbAbbreviatedName = new StringBuilder ( intNameLength );

            for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                      intJ < intNameLength ;
                      intJ++ )
            {
                char chrCurrent = pstrTimeZoneName [ intJ ];

                if ( fTakeRemainingCharacters )
                {
                    rsbAbbreviatedName.Append ( chrCurrent );
                }
                else if ( chrCurrent == SpecialCharacters.SPACE_CHAR )
                {
                    fIsFirstCharacterOfWord = true;
                }   // TRUE block, if ( chrCurrent == SpecialCharacters.SPACE_CHAR )
                else
                {
                    if ( fIsFirstCharacterOfWord )
                    {
                        rsbAbbreviatedName.Append ( chrCurrent );

                        if ( chrCurrent == SpecialCharacters.PARENTHESIS_LEFT )
                        {
                            //  Do nothing.
                        }
                        else if ( chrCurrent == SpecialCharacters.PARENTHESIS_RIGHT )
                        {
                            rsbAbbreviatedName.Append ( chrCurrent );
                        }
                        else
                        {
                            fIsFirstCharacterOfWord = false;
                        }
                    }   // TRUE block, if ( fIsFirstCharacterOfWord )
                    else if ( chrCurrent == SpecialCharacters.PARENTHESIS_RIGHT )
                    {
                        rsbAbbreviatedName.Append ( chrCurrent );
                    }   // FALSE block, if ( fIsFirstCharacterOfWord )
                    else if ( chrCurrent == SpecialCharacters.MINUS || chrCurrent == SpecialCharacters.PLUS )
                    {
                        rsbAbbreviatedName.Append ( chrCurrent );
                        fTakeRemainingCharacters = true;
                    }   // FALSE block, else if ( chrCurrent == SpecialCharacters.PARENTHESIS_RIGHT )
                }   // FALSE block, if ( chrCurrent == SpecialCharacters.SPACE_CHAR )
            }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < intNameLength ; intJ++ )

            return rsbAbbreviatedName.ToString ( );
        }   // private static string AbbreviateAnyTZName
        #endregion  // Private Helper Methods
    }   // public static class TimeZoneInfoExtensions
}   // partial namespace WizardWrx