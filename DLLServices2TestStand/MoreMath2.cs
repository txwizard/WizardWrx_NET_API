/*
    ============================================================================

	Project Name:       DLLServices2TestStand

    File Name:          MoreMath.cs

    Class Name:         MoreMath

    Namespace Name:     DLLServices2TestStand

    Class Synopsis:     This class implements unusual integer math operations
                        that require floating-point arithmetic under the covers.

	Remarks:			This class may eventually make its way into MoreMath.
						Meanwhile, it must be renamed to avoid colliding with a
						like-named class that lives in the same namespace.

    Author:             David A. Gray

    Date Created:       Sunday, 11 July 2021

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version  By  Description
    ---------- -------- --- ----------------------------------------------------
	2021/07/11 8.0.1453 DAG This class makes its debut by way of a system-level
                            file copy from C#Lab, in which it was created and
                            tested.
    ============================================================================
*/

using System;


using WizardWrx;


namespace DLLServices2TestStand
{
    public static class MoreMath2
    {
		public static int MakeEvenlyDivisibleByFour ( int pintBigInteger )
		{
			const double DIVISOR = 4.000;

			const double NEED_1_MORE = 0.75;
			const double NEED_2_MORE = 0.50;
			const double NEED_3_MORE = 0.25;
			const double NEED_0_MORE = 0.00;

			const int PLUS_ONE_MORE = MagicNumbers.PLUS_ONE;
			const int PLUS_TWO_MORE = MagicNumbers.PLUS_TWO;
			const int PLUS_THREE_MORE = MagicNumbers.PLUS_THREE;

			double dblBigInteger = pintBigInteger;
			double dblQuotient = dblBigInteger / DIVISOR;
			double dblFraction = dblQuotient - ( int ) dblQuotient;

			switch ( dblFraction )
			{
				case NEED_0_MORE:
					return ( int ) pintBigInteger;
				case NEED_1_MORE:
					return ( int ) pintBigInteger + PLUS_ONE_MORE;
				case NEED_2_MORE:
					return ( int ) pintBigInteger + PLUS_TWO_MORE;
				case NEED_3_MORE:
					return ( int ) pintBigInteger + PLUS_THREE_MORE;
				default:
					throw new InvalidOperationException ( $"An unexpected exception arose while processing an input value of {pintBigInteger}.{Environment.NewLine}Division by 4 yielded{dblQuotient} and a fractional value of {dblFraction}." );
			}   // switch ( dblFraction )
		}   // private static int MakeEvenlyDivisibleByFour


		public static int FractionalMultiply2Integer ( int pintLength , double pdblMultiplier )
		{
			double dblInput = pintLength;
			double dblOutput = dblInput * pdblMultiplier;
			int intOutput = ( int ) dblOutput;

			if ( dblOutput > ( double ) intOutput )
			{
				return intOutput + MagicNumbers.PLUS_ONE;
			}   // TRUE (There is a fractional part.) block, if ( dblOutput > ( double ) intOutput )
			else
			{
				return intOutput;
			}   // FALSE (The product is already a whole number.) block, if ( dblOutput > ( double ) intOutput )
		}   // private static int FractionalMultiply2Integer
	}   // public static class MoreMath
}   // partial namespace DLLServices2TestStand