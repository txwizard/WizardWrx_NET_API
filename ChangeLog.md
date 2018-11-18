# WizardWrx .NET API Change Log

This file is a running history of fixes and improvements from version 7.0
onwards. Changes are documented for the newest version first. Within each
version, classes are listed alphabetically.

# Version 7.11

Following is a summary of changes made in version 7.11, released Saturday, 17 November 2018.

##Class: WizardWrx.MagicNumbers (defined in WizardWrx.Common)

- BREAKING CHANGE: Rename `EXACTLY_ONE_NUNDRED` to `EXACTLY_ONE_HUNDRED` to correct a
misspelling that prevented me finding it.

- Correct the value of `EXACTLY_TEN_THOUSAND`, which I discovered was returning
_one million_.

- Define overlooked constants `EXACTLY_TEN` (10) and `EVENLY_DIVISIBLE` (0), the
latter handy for use with the modulus operator.

##Class: WizardWrx.NumericFormats (defined in WizardWrx.Common)

- Define `IntegerToHexStr` overloads that omit the second and third arguments,
substituting common defaults for the missing arguments.

- Change `FormatStatusCode` to use the simplified first overload, shortening its
stack frame and call setup requirments.

##Class: WizardWrx.MoreMath (defined in WizardWrx.Core.dll)

This class makes its debut, with the following static methods:

- `IsEvenlyDivisibleByAnyInteger`, defined twice, to accept integer and long
inputs.

- `IsGregorianLeapYear` implements the Gregorian leap year algorithm correctly.

- `IsValidGregorianYear` returns TRUE when given a number is a valid year in the
Gregorian calendar.

##Class: WizardWrx.tringExtensions (defined in WizardWrx.Core.dll)

- `RenderEvenWhenNull` represents a null reference as a localizable string literal,
`MSG_OBJECT_REFERENCE_IS_NULL`, defined in `Wizardwrx.Common.Properties.Resources`.

- `EnumFromString` is a generic method that attempts to convert a string to a
member of any enumeration. The enumeration type is inferred from the return
type specified in the method call.

##Namespace: WizardWrx.EmbeddedTextFile (defined in WizardWrx.EmbeddedTextFile.dll)

The documentation of this library is re-phrased so that everything is in active
voice. The code is unchanged.

# Version 7.1

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