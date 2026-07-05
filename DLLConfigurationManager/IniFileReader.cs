/*
    ============================================================================

    Namespace Name:     WizardWrx.DLLConfigurationManager

    Class Name:         IniFileReader

    Module Name:        IniFileReader.cs

    Synopsis:           This class module imports the Win32 Function 
                        "GetPrivateProfileString" from the "Kernel32" class.

    Remarks:            Other than substituting my own namespace and class name,
                        this code is as it appears in the article on The Code
                        Project (See reference # 2.) The first reference gives
                        the P/Invoke setup to do the same thing, but it also
                        refers readers to the article on The Code Project.

                        The internal documentation was supplied by the author.
                        My contributions are limited to reformatting to suit my
                        taste, correcting a minor typographical error or two,
                        and, as of version 6.3, incorporating my standard
						constants in place of his hard coded zeros, ones, and
                        twos.

    Copyright:          Omission of a copyright notice is intentional. Since the
                        code is almost entirely the work of someone else, it is
                        his work, and I won't pass it off as mine. The fact that
                        I didn't find anything to change says what I think about
                        the author's work.

    References:         1)  "getprivateprofilestring (kernel32)"
                            http://www.pinvoke.net/default.aspx/kernel32.getprivateprofilestring

                        2)  "An INI file enumerator class using C#"
                            http://www.codeproject.com/Articles/31597/An-INI-file-enumerator-class-using-C

    Author:             "Hilly_Billy" (The Code Project - See reference # 2.)

	License:			This module is specifically excluded from my BSD license
						and is distributed under the terms of the substantially
						identical Code Project Open License (CPOL).

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ -------------------------------------------------
    2012/09/05 1.0     HB/CPJ Initial implementation.
    
	2012/09/06 2.7     DAG    Install into WizardWrx.ApplicationHelpers.
    
	2014/06/07 5.0     DAG    Major namespace reorganization.
    2014/06/23 5.1     DAG    Documentation corrections.
    
	2015/06/18 5.5     DAG    Move this class from WizardWrx.ApplicationHelpers2
                              to WizardWrx.DLLServices2.

	2016/06/07 6.3     DAG    Correct errors in the internal documentation that
                              surfaced as I was preparing this library for
                              publication on GetHub. The most glaring error was
                              that the heading listed its original namespace;
                              this HAD to be fixed!

	2017/02/24 7.0     DAG    Break this library apart, so that smaller subsets
	                          of classes can be distributed and consumed
                              independently.

                              The only effect of this change on this module is
                              it moves into a new namespace, and the new library
                              has a dependency upon WizardWrx.Common.
    ============================================================================
*/


using System;
using System.Text;

/*  Added by DAG */

using System.Runtime.InteropServices;


namespace WizardWrx.DLLConfigurationManager
{
    /// <summary>
    /// Provide a managed interface to GetPrivateProfileString in the Windows
    /// API, with methods to retrieve the values of individual keys and lists of
    /// the keys in a section or the sections in a file.
    /// </summary>
    public class IniFileReader
    {
		private const int INITIAL_BUFSIZE_250 = 250;
		private const int INITIAL_BUFSIZE_500 = 500;

        //    Imports the Win32 Function "GetPrivateProfileString"
        //    from the "Kernel32" class.
        //
        //    I use 3 methods to gather the information. All have the same name
        //    as defined by the Win32 Function "GetPrivateProfileString"
        //
        //    First Method:     Gathers the Value, as the SectionHeader and
        //                      EntryKey are known.
        //
        //    Second Method:    Gathers a list of EntryKeys for the known
        //                      SectionHeader.
        //
        //    Third Method:     Gathers a list of SectionHeaders.

        // First Method:
        [DllImport ("kernel32")]
        static extern int GetPrivateProfileString
        (
            string Section, 
            string Key, 
            string Value, 
            StringBuilder Result,
            int Size, 
            string FileName
        );

        // Second Method:
        [DllImport ("kernel32")]
        static extern int GetPrivateProfileString
        (
            string Section,
            int Key, 
            string Value,
            [MarshalAs (UnmanagedType.LPArray)] byte[] Result,
            int Size,
            string FileName
        );

        // Third Method:
        [DllImport ("kernel32")]
        static extern int GetPrivateProfileString
        (
            int Section,
            string Key, 
            string Value,
            [MarshalAs (UnmanagedType.LPArray)] byte[] Result, 
            int Size,
            string FileName
        );

        // Set the IniFileName passed from the Main Application.
        /// <summary>
        /// This string holds the fully qualified name of the private profile
        /// file to process, which is passed into the constructor, but can be
        /// changed as needed.
        /// </summary>
        public string path;     // Treat as a Read/Write property.

        /// <summary>
        /// Construct an instance of the class.
        /// </summary>
        /// <param name="INIPath">
        /// This string must be the fully qualified name of a well formed
        /// private profile (INI) file.
        /// </param>
        public IniFileReader ( string INIPath )
        {
            path = INIPath;
        }   // Its only constructor is public, and it requires the name of the INI file.


        /// <summary>
        /// The Function called to obtain the SectionHeaders, and returns them
        /// in an Dynamic Array
        /// </summary>
        /// <returns>
        /// The return value is an array of strings, each of which is the name
        /// of a section.
        /// </returns>
        public string [ ] GetSectionNames ( )
        {
			//    Sets the buffer size to INITIAL_BUFSIZE, if more is
            //    required then doubles the size each time.
			for ( int MaxSize = INITIAL_BUFSIZE_500 ; true ; MaxSize *= MagicNumbers.PLUS_TWO )
            {
                //    Obtains the information in bytes and stores
                //    them in the buffer (Bytes array).
                byte [ ] bytes = new byte [ MaxSize ];
				int size = GetPrivateProfileString ( MagicNumbers.ZERO ,
                                                     SpecialStrings.EMPTY_STRING ,
                                                     SpecialStrings.EMPTY_STRING  ,
                                                     bytes ,
                                                     MaxSize ,
                                                     path );

                // Check the information obtained is not bigger
                // than the allocated buffer - 2 bytes.
                // if it is, then skip over the next section
                // so that the buffer size can be doubled.
                if ( size < MaxSize - MagicNumbers.PLUS_TWO )
                {
                    // Converts the bytes value into an ASCII char. This is one long string.
					string Selected = Encoding.ASCII.GetString ( bytes , MagicNumbers.ZERO ,
											   size - ( size > MagicNumbers.ZERO ? MagicNumbers.PLUS_ONE : MagicNumbers.ZERO ) );
                    // Splits the Long string into an array based on the "\0"
                    // or null (Newline) value and returns the value(s) in an array.
                    return Selected.Split ( new char [ ] { SpecialCharacters.NULL_CHAR } );
				}   // if ( size < MaxSize - MagicNumbers.PLUS_TWO )
			}   // for ( int MaxSize = INITIAL_BUFSIZE ; true ; MaxSize *= MagicNumbers.PLUS_TWO )
        }   // public string[] GetSectionNames


        /// <summary>
        /// The Function called to obtain the EntryKeys from the given
        /// SectionHeader string passed, and returns them in an Dynamic Array
        /// </summary>
        /// <param name="section">
        /// This string must be the name of a section which is expected to be
        /// present, though possibly empty.
        /// </param>
        /// <returns>
        /// The return value is an array of strings, each of which is the name
        /// of a key (entry) in the specified section.
        /// </returns>
        public string [ ] GetEntryNames ( string section )
        {
			//    Sets the MaxSize buffer to INITIAL_BUFSIZE, if the more
            //    is required then doubles the size each time. 
			for ( int MaxSize = INITIAL_BUFSIZE_500 ; true ; MaxSize *= MagicNumbers.PLUS_TWO )
            {
                //    Obtains the EntryKey information in bytes
                //    and stores them in the MaxSize buffer (Bytes array).
                //    Note that the SectionHeader value has been passed.
                byte [ ] bytes = new byte [ MaxSize ];
                int size = GetPrivateProfileString ( section ,
                                                     MagicNumbers.ZERO ,
                                                     SpecialStrings.EMPTY_STRING  ,
                                                     bytes ,
                                                     MaxSize ,
                                                     path );

                // Check the information obtained is not bigger
				// than the allocated MaxSize buffer - MagicNumbers.PLUS_TWO bytes.
                // if it is, then skip over the next section
                // so that the MaxSize buffer can be doubled.
                if ( size < MaxSize - MagicNumbers.PLUS_TWO )
                {
                    // Converts the bytes value into an ASCII char.
                    // This is one long string.
					string entries = Encoding.ASCII.GetString ( bytes , MagicNumbers.ZERO ,
											  size - ( size > MagicNumbers.ZERO ? MagicNumbers.PLUS_ONE : MagicNumbers.ZERO ) );
                    // Splits the Long string into an array based on the "\0"
                    // or null (Newline) value and returns the value(s) in an array
                    return entries.Split ( new char [ ] { SpecialCharacters.NULL_CHAR } );
				}   // if ( size < MaxSize - MagicNumbers.PLUS_TWO )
			}   // for ( int MaxSize = INITIAL_BUFSIZE ; true ; MaxSize *= MagicNumbers.PLUS_TWO )
        }   // public string[] GetEntryNames


        /// <summary>
        /// The Function called to obtain the EntryKey Value from the given
        /// SectionHeader and EntryKey string passed, then returned
        /// </summary>
        /// <param name="section">
        /// This string must be the name of a section that is expected to exist.
        /// </param>
        /// <param name="entry">
        /// This string must be the name of a key (entry) that is expected to
        /// exist, though it might be empty.
        /// </param>
        /// <returns>
        /// The return value is a string, which may be empty.
        /// </returns>
        public string GetEntryValue (
            string section ,
            string entry )
        {
			//    Sets the MaxSize buffer to INITIAL_BUFSIZE_250, if the more
            //    is required then doubles the size each time. 
			for ( int MaxSize = INITIAL_BUFSIZE_250 ; true ; MaxSize *= MagicNumbers.PLUS_TWO )
            {
                //    Obtains the EntryValue information and uses the StringBuilder
                //    Function to and stores them in the MaxSize buffers (result).
                //    Note that the SectionHeader and EntryKey values has been passed.
                StringBuilder result = new StringBuilder ( MaxSize );
                int size = GetPrivateProfileString ( section ,
                                                     entry , 
                                                     SpecialStrings.EMPTY_STRING  ,
                                                     result ,
                                                     MaxSize ,
                                                     path );

				if ( size < MaxSize - MagicNumbers.PLUS_ONE )
                {
                    // Returns the value gathered from the EntryKey.
                    return result.ToString ( );
				}   // if ( size < MaxSize - MagicNumbers.PLUS_ONE )
			}   // for ( int MaxSize = INITIAL_BUFSIZE_250 ; true ; MaxSize *= MagicNumbers.PLUS_TWO )
        }   // public object GetEntryValue
    }   // class IniFileReader
}   // partial namespace WizardWrx.DLLConfigurationManager
