# WizardWrx .NET API Change Log

Following is a list of changes made in version 7.1, released Sunday, 07 October 2018.

##Class: WizardWrx.StringExtensions (defined in WizardWrx.Core.dll)

Incorporate CapitalizeWords, which I created and tested as part of the Great
Eastern Energy DataFarmer application.

##Class: SpecialStrings (defined in WizardWrx.Common)

Define SPACE_CHAR for use when only a string will do, and cross reference the
new constant to its antecedent, SpecialCharacters.SPACE_CHAR.

##Class: ASCIICharacterDisplayInfo (defined in WizardWrx.ASCIIInfo.dll)

Override ToString to render all three representations (printable string,
hexadecimal, then decimal, in that order), and define static method
DisplayCharacterInfo to provide that service for an arbitrary character, without
instantiating ASCII_Character_Display_Table.

##Other Changed Files

Incidental changes included in this commit are as follows.

- __SpecialCharacters.cs__ got a cross reference to SpecialStrings.SPACE_CHAR.

- __ProductAssemblyInfo.cs__ reflects the new library version number, 7.1.

- __AssemblyInfo.cs__ in all individual libraries reflects the new library
version number and a build number increment.

- __Resources.resx in DLLServicesTestStand__ has the following new strings:
IDS_ASCII_CHARACTER_INFO, IDS_ASCII_TABLE_CHARACTER_PROPERTIES, and
IDS_ASCII_TABLE_ENUMERATION.

- __ClassTestMap.TXT in DLLServicesTestStand__ defines two new unit test method
mappings. Note, too, that the terminal newline is gone. This is by design, to
eliminate 4 bytes from the embedded resource that contribute nothing to its
usability.

- __FormatStringParsing_Drills.cs in DLLServicesTestStand__, which also
exercises WizardWrx.ASCIIInfo.dll, which was developed and deployed
concurrently, exercises the new static method for displaying a printable version
of any ASCII character, along with its numerical value, expressed in both
decimal and hexadecimal notation.

- __NewClassTests_20140914.cs in DLLServicesTestStand__ incorporates overlooked
unit tests of the entire SpecialStrings class.

- __Program.cs in DLLServicesTestStand__ calls the new methods that went into
class NewClassTests_20140914.