/*
    ============================================================================

    Namespace:          WizardWrx.Core

    Class Name:         RegistryValues

    File Name:          RegistryValues.cs

    Synopsis:           This sealed class is a container for utility routines
                        that efficiently test a Registry key for the presence of 
                        a value, and return the more complex types as strongly
                        typed variables.

    Remarks:            This class was created around GetRegMultiStringValue, a
                        utility routine for gracefully handling REG_MULTI_SZ
                        values stored in Windows Registry keys; the rest came
                        slightly later, as the need for them arose.

    Author:             David A. Gray

    License:            Copyright (C) 2015-2017, David A. Gray. All rights reserved.

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

    Created:            Wednesday, 10 June 2015

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2015/06/20 5.5     DAG    Create class by moving the functions that read the
                              values from a Windows Registry key into this class
                              from the Util class.
 
    2016/04/10 6.0     DAG    Scan for typographical errors flagged by the
							  spelling checker add-in, and correct what I find,
                              and update the formatting and marking of blocks.

	2017/02/21 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

						      This module moved into WizardWrx.Core, a new
                              namespace, which is exported by a like named
							  dynamic-link library.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using Microsoft.Win32;


namespace WizardWrx.Core
{
	/// <summary>
	/// This sealed class exposes static methods for efficiently testing for the
	/// presence of named values in a Registry key that behave more like the
	/// Item property of a collection, and retrieving Registry value types that
	/// require a transformation of one sort or another to get them into the
	/// appropriate native type.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	public static class RegistryValues
	{
		/// <summary>
		/// Specify this constant as the value of RegQueryValue argument pfNullIsOK if
		/// your routine expects a null reference when the requested value is absent
		/// or the wrong type.
		/// </summary>
		public const bool REG_BINARY_NULL_FOR_ABSENT        = true;


		/// <summary>
		/// Specify this constant as the value of RegQueryValue argument pfNullIsOK if
		/// your routine expects an array of some kind, even when the requested value
		/// is absent or the wrong type.
		/// </summary>
		public const bool REG_BIANRY_EMPTY_ARRAY_FOR_ABSENT = false;


		/// <summary>
		/// Get a REG_BINARY value from the Windows Registry.
		/// </summary>
		/// <param name="phRegKey">
		/// Specify the handle of the Registry key where the value is expected
		/// to exist. This must be the key that holds the value, since neither
		/// RegistryKey.GetValueKind, nor RegistryKey.GetValue supports paths.
		/// </param>
		/// <param name="pstrValueName">
		/// Specify the name, sans square brackets, which aren't needed, since
		/// the string is the unqualified name of a value.
		/// </param>
		/// <param name="pfNullIsOK">
		/// Specify True if your code tests the return value for a null
		/// reference. Otherwise, specify False, and expect an uninitialized
		/// one-element byte array.
		/// 
		/// This is a specific instance in which I believe that a null reference
		/// is the best solution to the problem of what to use as a default for
		/// a missing REG_BINARY value.
		/// </param>
		/// <returns>
		/// If the specified value exists and its type is REG_BINARY, the return
		/// value is a byte array containing the value read from the Registry.
		/// Otherwise, the return value is null (Nothing in Visual Basic).
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If phRegKey is a null reference (Nothing in Visual Basic), when this
		/// method calls companion method ValueExists to query the Registry for
		/// the specified value, that method throws an ArgumentNullException
		/// exception, listing this method as its calling method in its stack
		/// trace.
		/// </exception>
		public static byte [ ] RegQueryValue (
			RegistryKey phRegKey ,
			string pstrValueName ,
			bool pfNullIsOK )
		{
			if ( ValueExists ( phRegKey , pstrValueName ) )
			{   // Value exists in key. See if it's the right type.
				if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.Binary )
				{   // Good! It's a REG_BINARY.
					return ( byte [ ] ) phRegKey.GetValue ( pstrValueName );
				}   // TRUE (expected outcome) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
				else
				{   // BAD! It's some other kind.
					return pfNullIsOK ? ( byte [ ] ) null : new byte [ 1 ];
				}   // FALSE (UNexpected outcome) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
			}   // TRUE (expected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
			else
			{   // The specified value doesn't exist.
				return pfNullIsOK ? ( byte [ ] ) null : new byte [ 1 ];
			}   // FALSE (UNexpected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
		}	// public static byte [ ] RegQueryValue (1 of 5)


		/// <summary>
		/// Get a REG_DWORD value from the Windows Registry.
		/// </summary>
		/// <param name="phRegKey">
		/// Specify the handle of the Registry key where the value is expected
		/// to exist. This must be the key that holds the value, since neither
		/// RegistryKey.GetValueKind, nor RegistryKey.GetValue supports paths.
		/// </param>
		/// <param name="pstrValueName">
		/// Specify the name, sans square brackets, which aren't needed, since
		/// the string is the unqualified name of a value.
		/// </param>
		/// <param name="pintDefault">
		/// Although a DWORD seems logically more like an unsigned integer, I am
		/// somewhat surprised that the framework casts it to int, which carries
		/// a sign. Nevertheless, since the return is cast to int, so is its
		/// default, which is returned if the specified value is absent, or if
		/// it exists, but has a different type (ValueKind) property.
		/// </param>
		/// <returns>
		/// If the named value exists in the specified key and its type is 
		/// REG_DWORD, it is returned, as a SIGNED integer. Otherwise, the value
		/// specified in pintDefault is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If phRegKey is a null reference (Nothing in Visual Basic), when this
		/// method calls companion method ValueExists to query the Registry for
		/// the specified value, that method throws an ArgumentNullException
		/// exception, listing this method as its calling method in its stack
		/// trace.
		/// </exception>
		public static int RegQueryValue (
			RegistryKey phRegKey ,
			string pstrValueName ,
			int pintDefault )
		{
			if ( ValueExists ( phRegKey , pstrValueName ) )
			{   // Value exists in key. See if it's the right type.
				if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
				{   // Good! It's a REG_DWORD.
					return ( int ) phRegKey.GetValue ( pstrValueName );
				}   // TRUE (expected outcome) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
				else
				{   // BAD! It's some other kind.
					return pintDefault;
				}   // FALSE (UNexpected outcome) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
			}   // TRUE (expected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
			else
			{   // The specified value doesn't exist.
				return pintDefault;
			}   // FALSE (UNexpected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
		}   // public static int RegQueryValue (2 of 5)


		/// <summary>
		/// Get a REG_QWORD value from the Windows Registry.
		/// </summary>
		/// <param name="phRegKey">
		/// Specify the handle of the Registry key where the value is expected
		/// to exist. This must be the key that holds the value, since neither
		/// RegistryKey.GetValueKind, nor RegistryKey.GetValue supports paths.
		/// </param>
		/// <param name="pstrValueName">
		/// Specify the name, sans square brackets, which aren't needed, since
		/// the string is the unqualified name of a value.
		/// </param>
		/// <param name="plngDefault">
		/// Although a QWORD seems logically more like an unsigned long, I am
		/// somewhat surprised that the framework casts it to long, which
		/// carries a sign. Nevertheless, since the return is cast to long, so
		/// is its default, which is returned if the specified value is absent,
		/// or if it exists, but has a different type (ValueKind) property.
		/// </param>
		/// <returns>
		/// If the named value exists in the specified key and its type is
		/// REG_QWORD, it is returned, as a SIGNED long integer. Otherwise, the
		/// value specified in plngDefault is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If phRegKey is a null reference (Nothing in Visual Basic), this
		/// method throws an ArgumentNullException exception.
		/// </exception>
		public static long RegQueryValue (
			RegistryKey phRegKey ,
			string pstrValueName ,
			long plngDefault )
		{
			if ( ValueExists ( phRegKey , pstrValueName ) )
			{   // Value exists in key. See if it's the right type.
				if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.QWord )
				{   // Good! It's a REG_QWORD.
					return ( long ) phRegKey.GetValue ( pstrValueName );
				}   // TRUE (expected outcome) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
				else
				{   // BAD! It's some other kind.
					return plngDefault;
				}   // FALSE (UNexpected outcome) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.DWord )
			}   // TRUE (expected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
			else
			{   // The specified value doesn't exist.
				return plngDefault;
			}   // FALSE (UNexpected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
		}   // public static long RegQueryValue (3 of 5)


		/// <summary>
		/// Return the single string representation of a REG_SZ or 
		/// REG_EXPAND_SZ Registry value.
		/// </summary>
		/// <param name="phRegKey">
		/// Pass in a handle to the Registry sub-key that is supposed to contain
		/// the required REG_SZ or REG_EXPAND_SZ value.
		/// </param>
		/// <param name="pstrValueName">
		/// Pass in a string that contains the name of the value to be queried
		/// and returned if present. See notes in the Return Value comment.
		/// </param>
		/// <param name="pstrDefault">
		/// Specify the string to return if the value is missing or of the wrong type.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is a string containing
		/// the value. If the value type is REG_EXPAND_SZ, environment variables
		/// are expanded.
		/// 
		/// If the value specified by pstrValueName is missing entirely, the
		/// return value is pstrDefault.
		/// 
		/// If the requested string is something other than REG_SZ or 
		/// REG_EXPAND_SZ, the return value is pstrDefault.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If phRegKey is a null reference (Nothing in Visual Basic), when this
		/// method calls companion method ValueExists to query the Registry for
		/// the specified value, that method throws an ArgumentNullException
		/// exception, listing this method as its calling method in its stack
		/// trace.
		/// </exception>
		public static string RegQueryValue (
			RegistryKey phRegKey ,
			string pstrValueName ,
			string pstrDefault )
		{
			if ( ValueExists ( phRegKey , pstrValueName ) )
			{   // Value exists in key. See if it's the right type.
				switch ( phRegKey.GetValueKind ( pstrValueName ) )
				{
					case RegistryValueKind.String:
					case RegistryValueKind.ExpandString:
						return ( string ) phRegKey.GetValue ( pstrValueName ) ;
					default:
						return pstrDefault;
				}   // switch ( phRegKey.GetValueKind ( pstrValueName ) )
			}   // TRUE (expected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
			else
			{   // Specified value doesn't exist in the specified key.
				return pstrDefault;
			}   // FALSE (UNexpected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
		}   // public static string RegQueryValue (4 of 5)


		/// <summary>
		/// Return the multiple values stored in a REG_MULTI_SZ Registry value.
		/// </summary>
		/// <param name="phRegKey">
		/// Pass in a handle to the Registry sub-key that is supposed to contain
		/// the required REG_MULTI_SZ value.
		/// </param>
		/// <param name="pstrValueName">
		/// Pass in a string that contains the name of the value to be queried
		/// and returned if present. See notes in the Return Value comment.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is an array of strings
		/// that contains one string for each substring in the REG_MULTI_SZ
		/// value.
		/// 
		/// If the value specified by pstrValueName is missing entirely, an
		/// empty array is returned.
		/// 
		/// If the requested string is something other than REG_MULTI_SZ, this
		/// method treats the call as a degenerate case, returning an array of
		/// one element if the value is either REG_SZ or REG_EXPAND_SZ.
		/// 
		/// ALL other types return empty arrays.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If phRegKey is a null reference (Nothing in Visual Basic), when this
		/// method calls companion method ValueExists to query the Registry for
		/// the specified value, that method throws an ArgumentNullException
		/// exception, listing this method as its calling method in its stack
		/// trace.
		/// </exception>
		public static string [ ] RegQueryValue (
			RegistryKey phRegKey ,
			string pstrValueName )
		{
			if ( ValueExists ( phRegKey , pstrValueName ) )
			{   // Value exists in key. See if it's the right type.
				if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.MultiString )
				{
					return ( string [ ] ) phRegKey.GetValue ( pstrValueName );
				}	// TRUE (Value is a compatible type.) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.MultiString )
				else
				{
					return new string [ ] { };
				}   // FALSE (The type of this value is incompatible with the specified type.) block, if ( phRegKey.GetValueKind ( pstrValueName ) == RegistryValueKind.MultiString )
			}   // TRUE (expected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
			else
			{   // Specified value doesn't exist in the specified key.
				return new string [ ] { };
			}   // FALSE (UNexpected outcome) block, if ( RegistryKeyContainsValue ( phRegKey , pstrValueName ) )
		}   // public static string [ ] RegQueryValue (5 of 5)


		/// <summary>
		/// Return TRUE if a specified value exists on a specified key of the
		/// Windows Registry.
		/// </summary>
		/// <param name="phRegKey">
		/// Pass in a handle to the Registry sub-key that is supposed to contain
		/// the desired value.
		/// </param>
		/// <param name="pstrValueName">
		/// Pass in a string that contains the name of the value to be queried
		/// and returned if present. See notes in the Return Value comment.
		/// </param>
		/// <returns>
		/// The function returns true if the key contains a value with the 
		/// specified name, or if pstrValueName is the empty string or a null
		/// reference, either of which stands for the default value, which is
		/// always present, though seldom set. Otherwise, the return value is
		/// false.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If phRegKey is a null reference, an ArgumentNullException exception
		/// is thrown.
		/// </exception>
		/// <remarks>
		/// This was written as a service routine for companion methods
		/// GetRegDwordValue and GetRegMultiStringValue. However, since it is
		/// almost certain to find use on its own, I marked it public.
		/// </remarks>
		public static bool ValueExists (
			RegistryKey phRegKey ,
			string pstrValueName )
		{
			const string ARGNAME_KEY = @"RegistryKey phRegKey";

			if ( phRegKey == null )
				throw new ArgumentNullException ( ARGNAME_KEY );

			if ( string.IsNullOrEmpty ( pstrValueName ) )
				return true;

			//  ----------------------------------------------------------------
			//  Although GetValueNames returns an array of strings, since the
			//  generic List has a constructor that loads a list from any
			//  ICollection, and the array qualifies, I loaded it into a List,
			//  which can be sorted and searched using built-in methods. This 
			//  reduces determining whether a specific value name exists to two
			//  method calls that beat the pants off the brute force search that
			//  would otherwise be necessary. Treating the empty list as a
			//  degenerate case eliminates a call that would try to sort a null
			//  list, followed by a BinarySearch that would always return a NOT
			//  FOUND result.
			//  ----------------------------------------------------------------

			List<string> lstValueNames = new List<string> ( phRegKey.GetValueNames ( ) );

			if ( lstValueNames.Count > ListInfo.LIST_IS_EMPTY )
			{   // The key contains at least one value. Sort it for fast searching.
				lstValueNames.Sort ( );
				return ( lstValueNames.BinarySearch ( pstrValueName ) > ListInfo.BINARY_SEARCH_NOT_FOUND );
			}   // TRUE (expected outcome) block, if ( lstValueNames.Count > LIST_IS_EMPTY )
			else
			{   // The key contains no values of its own.
				return false;
			}   // FALSE (UNexpected outcome) block, if ( lstValueNames.Count > LIST_IS_EMPTY )
		}   // ValueExists
	}	// public sealed class RegistryValues
}	// partial namespace WizardWrx.Core