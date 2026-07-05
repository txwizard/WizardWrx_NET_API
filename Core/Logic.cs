/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         Logic

    File Name:          Logic.cs

    Synopsis:           This sealed class is a container for utility routines to
                        perform various logic tests that are painfully easy to
                        get wrong.

    Remarks:            You can still get them wrong, by invoking the wrong
                        method. However, if you request a specific test, you can
                        trust these routines will deliver the correct result.

						Since this class is pure C#, and uses only fundamental
                        value types, it is devoid of using directives.

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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2015/06/15 5.5     DAG    Create this dedicated class from routines brought
                              from the sealed Util class, when it belonged to
                              the WizawrdWrx.ApplicationHelpers namespaces, and
                              several new routines that I added to complete the
                              set.

	2017/02/19 7.0     DAG    This class is promoted to the root WizardWrx
                              namespace, and moved to WizardWrx.Core.dll.
    ============================================================================
*/

namespace WizardWrx
{
	/// <summary>
	/// This sealed class exposes methods that encapsulate many common, tricky
	/// loop state tests.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
	public static class Logic
	{
		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on its first
		/// iteration.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintInitialValue">
		/// Specify the integer initial value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index of a FOR loop is at its
		/// initial value, indicating the first iteration of the loop.
		/// </returns>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool IsFirstForIteration (
			int pintLoopIndex ,
			int pintInitialValue )
		{
			return pintLoopIndex == pintInitialValue;
		}   // public static bool IsFirstForIteration


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on its last
		/// iteration, given that the limit criterion is pintLoopIndex is
		/// greater than pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the next increment of the loop index
		/// would set one less than pintLimit, stopping the loop without another
		/// iteration.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool IsLastForIterationEQ (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex == pintLimit;
		}   // public static bool IsLastForIterationEQ


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on its last
		/// iteration, given that the limit criterion is pintLoopIndex is
		/// greater than or equal to pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the next increment of the loop index
		/// would set equal to pintLimit, stopping the loop without another
		/// iteration.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		public static bool IsLastForIterationGE (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex == pintLimit;
		}   // public static bool IsLastForIterationGE


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on its last
		/// iteration, given that the limit criterion is pintLoopIndex is
		/// greater than pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the next increment of the loop index
		/// would set one less than pintLimit, stopping the loop without another
		/// iteration.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		public static bool IsLastForIterationGT (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex - ArrayInfo.NEXT_INDEX == pintLimit;
		}   // public static bool IsLastForIterationGT


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on its last
		/// iteration, given that the limit criterion is pintLoopIndex is less
		/// than or equal to pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the next increment of the loop index
		/// would set it equal to pintLimit, stopping the loop without another
		/// iteration.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool IsLastForIterationLE (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex == pintLimit;
		}   // public static bool IsLastForIterationLE


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on its last
		/// iteration, given that the limit criterion is pintLoopIndex is less
		/// than pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the next increment of the loop index
		/// would set it equal to pintLimit, stopping the loop without another
		/// iteration.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		public static bool IsLastForIterationLT (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex + ArrayInfo.NEXT_INDEX == pintLimit;
		}   // public static bool IsLastForIterationLT

	
		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex is on a
		/// subsequent iteration.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintInitialValue">
		/// Specify the integer initial value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index of a FOR loop has passed its
		/// initial value, indicating that it is on a subsequent iteration of
		/// the loop.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		public static bool IsNextForIteration (
			int pintLoopIndex ,
			int pintInitialValue )
		{
			return pintLoopIndex != pintInitialValue;
		}   // public static bool IsNextForIteration


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex has one or more
		/// iteration to go, given that the limit criterion is pintLoopIndex is
		/// equal to pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index and limit indicate that one
		/// or more iterations remain.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool MoreForIterationsToComeEQ (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex != pintLimit;
		}   // public static bool MoreForIterationsToComeEQ


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex has one or more
		/// iteration to go, given that the limit criterion is pintLoopIndex is
		/// greater than or equal to pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index and limit indicate that one
		/// or more iterations remain.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool MoreForIterationsToComeGE (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex - ArrayInfo.NEXT_INDEX >= pintLimit;
		}   // public static bool MoreForIterationsToComeGE


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex has one or more
		/// iteration to go, given that the limit criterion is pintLoopIndex is
		/// greater than pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index and limit indicate that one
		/// or more iterations remain.
		/// </returns>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationEQ"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool MoreForIterationsToComeGT (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex - ArrayInfo.NEXT_INDEX > pintLimit;
		}   // public static bool MoreForIterationsToComeGT


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex has one or more
		/// iteration to go, given that the limit criterion is pintLoopIndex is less
		/// than or equal to pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index and limit indicate that one
		/// or more iterations remain.
		/// </returns>
		/// <remarks>
		/// Sometimes, it is more sensible to test whether there are iterations
		/// remaining.
		/// </remarks>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLT"/>
		public static bool MoreForIterationsToComeLE (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex + ArrayInfo.NEXT_INDEX <= pintLimit;
		}   // public static bool MoreForIterationsToComeLE


		/// <summary>
		/// Return TRUE if the FOR loop driven by pintLoopIndex has one or more
		/// iteration to go, given that the limit criterion is pintLoopIndex is less
		/// than pintLimit.
		/// </summary>
		/// <param name="pintLoopIndex">
		/// Specify the integer loop index.
		/// </param>
		/// <param name="pintLimit">
		/// Specify the integer limit value.
		/// </param>
		/// <returns>
		/// This function returns TRUE if the index and limit indicate that one
		/// or more iterations remain.
		/// </returns>
		/// <remarks>
		/// Sometimes, it is more sensible to test whether there are iterations
		/// remaining.
		/// </remarks>
		/// <seealso cref="IsFirstForIteration"/>
		/// <seealso cref="IsLastForIterationGE"/>
		/// <seealso cref="IsLastForIterationGT"/>
		/// <seealso cref="IsLastForIterationLE"/>
		/// <seealso cref="IsLastForIterationLT"/>
		/// <seealso cref="IsNextForIteration"/>
		/// <seealso cref="MoreForIterationsToComeEQ"/>
		/// <seealso cref="MoreForIterationsToComeGE"/>
		/// <seealso cref="MoreForIterationsToComeGT"/>
		/// <seealso cref="MoreForIterationsToComeLE"/>
		public static bool MoreForIterationsToComeLT (
			int pintLoopIndex ,
			int pintLimit )
		{
			return pintLoopIndex + ArrayInfo.NEXT_INDEX < pintLimit;
		}   // public static bool MoreForIterationsToComeLT


		/// <summary>
		/// Return the inverse of the truth value of an expression.
		/// </summary>
		/// <param name="pfUnlessWhat">
		/// Specify the expression to invert.
		/// </param>
		/// <returns>
		/// Return TRUE if pfUnlessWhat is FALSE, and vice vers.
		/// </returns>
		/// <remarks>
		/// I shamelessly borrowed this idiom from Perl, with full credit to
		/// Larry Wall, and implemented it as a macro for the C and C++ source
		/// code preprocessors about a decade ago. I don't know why it's taken
		/// me this long to realize that it is equally easy to implement in C#,
		/// and the implementation is sufficiently small that the code optimizer
		/// will almost certainly inline it, so that its performance impact is
		/// equivalent to the C/C++ macro.
		/// 
		/// The C# implementation is not quite as neat as the C/C++ macro.
		/// </remarks>
		public static bool Unless ( bool pfUnlessWhat )
		{
			return !pfUnlessWhat;
		}	// public static bool Unless
	}	// public sealed class Logic
}	// partial namespace WizardWrx