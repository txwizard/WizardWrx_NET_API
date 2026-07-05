/*
    ============================================================================

	Project Name:       DLLServices2TestStand

    File Name:          Base64RoundTripExperiments.cs

    Class Name:         Base64RoundTripExperiments

    Namespace Name:     DLLServices2TestStand

	Project Synopsis:   This class is part of a Visual C# laboratory project,
	                    which I use to develop and test new coding techniques
						and classes.

    Class Synopsis:     This class implements Base64 round trip test routines.

	Class Remarks:		I moved this code here so that I can guarantee that the
						required using directives are present before it becomes
						part of a new library module.

    Author:             David A. Gray

    Date Created:       Sunday, 11 July 2021

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
	2021/07/11 8.0.1453 DAG This class makes its debut by way of a system-level
                            file copy from C#Lab, in which it was created and
                            tested.
    ============================================================================
*/


using System;

using System.IO;


using WizardWrx;


namespace DLLServices2TestStand
{
    internal static class Base64RoundTripExperiments
    {
		internal static void Exercise_Base64_ToAndFrom ( )
		{
			string strInputFileName = Properties.Settings.Default.Base64EncodingSource;
			string strOutputFileName = Properties.Settings.Default.Base64EncodingRoundTrip;

			Console.WriteLine (
				@"{0}Exercise_Base64_ToAndFrom Begin:{0}" ,
				Environment.NewLine );

			//	--------------------------------------------------------------------------
			//	List input and output file names, both of which are absolute.
			//	--------------------------------------------------------------------------

			Console.WriteLine ( 
				@"    Absolute Input FileName                = {0}" , 
				strInputFileName );
			Console.WriteLine (
				@"    Absolute Output FileName               = {0}" ,
				strOutputFileName );

			try
			{
				FileInfo fiInputFile = new FileInfo ( strInputFileName );

				if ( fiInputFile.Exists )
                {
                    //	--------------------------------------------------------
                    //	List the siza and last modified date of the input file.
                    //	--------------------------------------------------------

                    Console.WriteLine (
                        @"{1}    Input File Size (bytes)                = {0}" ,
                        NumberFormatters.Integer ( fiInputFile.Length ) ,
                        Environment.NewLine );
                    Console.WriteLine (
                        @"    Input File Last Modified               = {0} ({1} UTC){2}" ,
                        SysDateFormatters.FormatDateTimeForShow ( fiInputFile.LastWriteTime ) ,
                        SysDateFormatters.FormatDateTimeForShow ( fiInputFile.CreationTimeUtc ) ,
                        Environment.NewLine );

                    //	--------------------------------------------------------
                    //	Read the input file into a byte array from which the
                    //	Base64 character array can be constructed, then report
                    //	its size, which should be equal to the length of the
                    //	input file reported above.
                    //	--------------------------------------------------------

                    byte [ ] abytInputBytes = File.ReadAllBytes ( strInputFileName );
                    Console.WriteLine (
                        @"    Input Byte Array Size                  = {0}" ,
                        NumberFormatters.Integer ( abytInputBytes.Length ) );

                    if ( fiInputFile.Length == abytInputBytes.Length )
                    {
                        Console.WriteLine ( $"{Environment.NewLine}    The input file and byte array lengths are equal.{Environment.NewLine}" );
                    }   // TRUE (expected outcome) block, if ( fiInputFile.Length == abytInputBytes.Length )
                    else
                    {
                        Console.WriteLine ( $"{Environment.NewLine}    The input file and byte array lengths DIFFER.{Environment.NewLine}" );
                    }   // FALSE (unexpected outcome) block, if ( fiInputFile.Length == abytInputBytes.Length )

                    //	--------------------------------------------------------
                    //	Construct a character array 4/3 as large as the input
                    //	array to hold the Base64 encoded characters created from
                    //	it, then call the Base64 encoder to create it.
                    //	--------------------------------------------------------

                    char [ ] achrOutputBytes = new char [ MoreMath2.MakeEvenlyDivisibleByFour ( MoreMath2.FractionalMultiply2Integer (
                        abytInputBytes.Length ,
                        ( double ) 4 / 3 ) ) ];
                    int intOutCount = Convert.ToBase64CharArray (
                        abytInputBytes ,
                        ArrayInfo.ARRAY_FIRST_ELEMENT ,
                        abytInputBytes.Length ,
                        achrOutputBytes ,
                        ArrayInfo.ARRAY_FIRST_ELEMENT );

                    //	--------------------------------------------------------
                    //	Creeate a Base64 encoded string from the same input, and
                    //	convert the character array into another string.
                    //	--------------------------------------------------------

                    string strBase64String = Convert.ToBase64String ( abytInputBytes );
                    string strBase64Array2String = new string ( achrOutputBytes );

                    //	--------------------------------------------------------
                    //	Show the lengths of both output strings, then report the
                    //	result of comparing them, which should report that they
                    //	are identical.
                    //	--------------------------------------------------------

                    Console.WriteLine (
                        @"    Output Byte Array Size                 = {0}" ,
                        NumberFormatters.Integer ( achrOutputBytes.Length ) );
                    Console.WriteLine (
                        @"    Convert.ToBase64CharArray Return Value = {0}" ,
                        NumberFormatters.Integer ( intOutCount ) );
                    Console.WriteLine (
                        @"    Convert.ToBase64String String Length   = {0}" ,
                        NumberFormatters.Integer ( strBase64String.Length ) );
                    Console.WriteLine (
                        @"    strBase64Array2String String Length    = {0}" ,
                        NumberFormatters.Integer ( strBase64Array2String.Length ) );

                    if ( strBase64String.Equals ( strBase64Array2String ) )
                    {
                        Console.WriteLine ( $"{Environment.NewLine}    Strings strBase64String and strBase64Array2String match.{Environment.NewLine}" );
                    }   // TRUE (expected outcome) block, if ( strBase64String.Equals ( strBase64Array2String ) )
                    else
                    {
                        Console.WriteLine ( $"{Environment.NewLine}    Strings strBase64String and strBase64Array2String DIFFER.{Environment.NewLine}" );
                    }   // FALSE (unexpected outcome) block, if ( strBase64String.Equals ( strBase64Array2String ) )

                    //	--------------------------------------------------------
                    //	Decodee the Base64 character array, which creates a new
                    //	array dynamically, and compare it byte for byte against
                    //	the input array. Both should match exactly.
                    //	--------------------------------------------------------

                    byte [ ] abytRoundTripBytes = Convert.FromBase64CharArray (
                        achrOutputBytes ,
                        ArrayInfo.ARRAY_FIRST_ELEMENT ,
                        achrOutputBytes.Length );

                    Console.WriteLine (
                        @"    Convert.FromBase64CharArray Array Size = {0}" ,
                        NumberFormatters.Integer ( abytRoundTripBytes.Length ) );

                    Compare2ArraysByte4Byte (
                        abytInputBytes ,
                        abytRoundTripBytes );

                    File.WriteAllBytes (
                        strOutputFileName ,
                        abytRoundTripBytes );
                    FileInfo fiRoundTripFile = new FileInfo ( strOutputFileName );
                    Console.WriteLine (
                        @"    Round Trip Output File Size            = {0}" ,
                        NumberFormatters.Integer ( fiRoundTripFile.Length ) );

                    if ( fiInputFile.Length == fiRoundTripFile.Length )
                    {
                        Console.WriteLine ( $"{Environment.NewLine}    Input and output file sizes match." );
                    }   // TRUE (expected outcome) block, if ( fiInputFile.Length == fiRoundTripFile.Length )
                    else
                    {
                        Console.WriteLine ( $"{Environment.NewLine}    Input and output file sizes DIFFER." );
                    }   // FALSE (unexpected outcome) block, if ( fiInputFile.Length == fiRoundTripFile.Length )

                    //	--------------------------------------------------------
                    //	Exercise new Base64 round trip converters.
                    //	--------------------------------------------------------

                    byte [ ] abytBase64Characters = WizardWrx.Core.ByteArrayBase64Converters.Base64EncodeBinaryFile ( strInputFileName );
                    byte [ ] abytDecodedBase64Characters = WizardWrx.Core.ByteArrayBase64Converters.Base64DecodeByteArray ( abytBase64Characters );

                    Compare2ArraysByte4Byte (
                        abytInputBytes ,
                        abytDecodedBase64Characters );
                }   // TRUE (anticipated outcome) block, if ( fiInputFile.Exists )
                else
				{
					Console.WriteLine ( @"FATAL ERROR: The specified input file cannot be found." );
				}   // FALSE (unanticipated outcome) block, if ( fiInputFile.Exists )
			}
			catch ( Exception exAllKinds )
			{
				Program.s_smTheApp.AppExceptionLogger.ReportException ( exAllKinds );
			}

			Console.WriteLine (
				@"{0}Exercise_Base64_ToAndFrom Done{0}" ,
				Environment.NewLine );
		}   // internal static void Exercise_Base64_ToAndFrom


        private static void Compare2ArraysByte4Byte ( byte [ ] pabytInputBytes , byte [ ] pabytRoundTripBytes )
        {
            if ( pabytInputBytes.Length == pabytRoundTripBytes.Length )
            {
                Console.WriteLine ( $"{Environment.NewLine}    Input and output array sizes match." );

                int intMismatches = MagicNumbers.ZERO;

                for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                          intJ < pabytInputBytes.Length ;
                          intJ++ )
                {
                    if ( pabytInputBytes [ intJ ] != pabytRoundTripBytes [ intJ ] )
                    {
                        Console.WriteLine ( $"    Bytes at offset {intJ} DIFFER - abytInputBytes [ intJ ] = {pabytInputBytes [ intJ ]} while abytRoundTripBytes [ intJ ] = {pabytRoundTripBytes [ intJ ]}" );
                        intMismatches++;
                    }   // TRUE (unanticipated outcome) block, if ( abytInputBytes [ intJ ] == abytRoundTripBytes [ intJ ] )
                }   // for ( int intJ = ArrayInfo.ARRAY_FIRST_ELEMENT ; intJ < abytInputBytes.Length ; intJ++ )

                if ( intMismatches == MagicNumbers.ZERO )
                {
                    Console.WriteLine ( $"    Input and Round Trip byte arrays match.{Environment.NewLine}" );
                }   // TRUE (expected outcome) block, if ( intMismatches == MagicNumbers.ZERO )
                else
                {
                    Console.WriteLine ( $"    Input and Round Trip byte arrays DIFFER - {intMismatches} mismatches found{Environment.NewLine}" );
                }   // FALSE (unexpected outcome) block, if ( intMismatches == MagicNumbers.ZERO )
            }   // TRUE (expected outcome) block, if ( abytInputBytes.Length == abytRoundTripBytes.Length)
            else
            {
                Console.WriteLine ( $"{Environment.NewLine}    Input and output array sizes DIFFER.{Environment.NewLine}" );
            }   // FALSE (unexpected outcome) block, if ( abytInputBytes.Length == abytRoundTripBytes.Length)
        }   // private static void Compare2ArraysByte4Byte
    }   // internal static class Base64RoundTripExperiments
}   // partial namespace DLLServices2TestStand