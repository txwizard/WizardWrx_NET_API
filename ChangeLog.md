# WizardWrx .NET API Change Log

This file is a running history of fixes and improvements from version 7.0
onwards. Changes are documented for the newest version first. Within each
version, classes are covered in alphabetical order.

# Version 7.15

Following is a summary of changes made in version __7.15__, released Sunday, __28 April 2019__.

## Class WizardWrx.SpecialStrings (defined in WizardWrx.Common.dll)

Define the following single-character strings:

| Name           | Value |
|----------------|-------|
|COLON           | ":"   |
|COMMA           | ","   |
|DOUBLE_QUOTE    | "\""  |
|FULL_STOP       | "."   |
|HYPHEN          | "-"   |
|SEMICOLON       | ";"   |
|SINGLE_QUOTE    | "'"   |
|TAB_CHAR        | "\t"  |
|UNDERSCORE_CHAR | "\_"  |

These are for constructing string constants. Though equivalent character
constants were defined in `WizardWrx.SpecialCharacters` long ago, they are useless
for constructing a string constant, which must be composed entirely of other
string constants. Constants cannot derive their values by calling the `ToString`
method on a character constant.

## Class WizardWrx.SpecialCharacters (defined in WizardWrx.Common.dll)

XML comments attached to the character constants that correspond to the new
string constants listed above get new cross references to the corresponding
string constant. There are no new constants.

## Class WizardWrx.ConsoleStreams.DefaultErrorMessageColors (defined in WizardWrx.ConsoleStreams.dll)

Supplement the `PropsSetFromConfig` property with a `PropsLeftAtDefault` property.

This class also benefits from changes made in its base class,
`WizardWrx.AssemblyLocatorBase`, defined in `WizardWrx.Core.dll`.

## Class WizardWrx.Core.AssemblyLocatorBase (defined in WizardWrx.Core.dll)

1) Replace the `ConfigMessage` string property with the
`RecoveredConfigurationExceptions` list.

2) Replace the properties collection enumeration with the much more efficient
dictionary lookup.

Though it is omitted from the change log, a significant benefit is that these
changes, along with others deeper in the code, eliminated a harmless null
reference exception that was being silently thrown, caught, and handled. That
exception is addressed by adding an overlooked null reference test that prevents
the code that would have executed from doing so.

## Class WizardWrx.Core.PropertyDefaults (defined in WizardWrx.Core.dll)

`EnumerateMissingConfigurationValues` is an instance method that reports
configuration values that are defined, but are missing from the configuration
file. All such properties have hard coded default values. The report is returned
as a formatted string that can be logged to the system console or the event log,
or displayed on a message box.

This class also benefits from changes made in its base class,
`WizardWrx.AssemblyLocatorBase`, defined in `WizardWrx.Core.dll`.

## Class WizardWrx.RecoveredException (defined in WizardWrx.Core.dll)

This new class, derived from `System.Exception`, provides a mechanism for
recording an exception without actually throwing it. The recorded exception is a
reasonbly faithful reproduction of the System.Exception that you would get if
you reported it by throwing.

## WizardWrx.Core.UnconfiguredDLLSettings (defined in WizardWrx.Core.dll)

The UnconfiguredDLLSettings class implements the Singleton pattern, because its
operation relies on there being exactly one instance. It uses a generic
Dictionary keyed by a concatenation of the configuration file name and
configuration key name to store a complete list of configuration settings that
have their hard coded default values, because there are no explicit settings in
the configuration file.

Each `UnconfiguredSetting` object stores the name of the configuration file, the
name of the configuration setting, and its value in three string properties, of
which the first two comprise the index.

The design is not quite ideal, since the key relies on the `ToString` method
override, since there is no separately named computer property to expose it. I
anticipate resolving this inefficiency soon. Meanwhile, it functions correctly,
and correcting this deficiency can be accomplished without breaking anything.

## WizardWrx.DLLConfigurationManager.ExceptionLogger (defined in WizardWrx.DLLConfigurationManager.dll)

Define `s_strSettingsOmittedFromConfigFile` as a static string property that
returns a message that lists the properties that are absent from the DLL
configuration file, along with their hard coded default values.

## Unit Test/Usage Demonstration Program DLLServices2TestStand.exe

As always, the test program has been amended to demonstrate the new features,
including the new string constants and the improved plumbing.

# Version 7.14

Following is a summary of changes made in version __7.14__, released Monday, __24 December 2018__.

## Class WizardWrx.MagicNumbers (defined in WizardWrx.Common.dll)

Define the constants listed in the following table.

| Name                    |             Value |
|-------------------------|-------------------|
| TICKS_PER_1_WEEK        | 6,048,000,000,000 |
| TICKS_PER_1_DAY         |   864,000,000,000 |
| TICKS_PER_23_59_59      |   863,990,000,000 |
| TICKS_PER_23_59_00      |   863,400,000,000 |
| TICKS_PER_1_HOUR        |    36,000,000,000 |
| TICKS_PER_1_MINUTE      |       600,000,000 |
| TICKS_PER_1_SECOND      |        10,000,000 |
| TICKS_PER_1_MILLISECOND |            10,000 |

__BREAKING CHANGE__ `TICKS_PER_SECOND` is correctly described in the XML
comment, but its numerical value was off by a factor of one thousand. This is
corrected by making its value equal to `TICKS_PER_1_SECOND`.

These tick values were computed by a custom class in a lab project that I use to
test ideas and algorithms, and the constants were derived therefrom by Excel
worksheet formulas.

## Class WizardWrx.DLLConfigurationManager (defined in WizardWrx.DLLConfigurationManager.dll)

Add a new `GetTheSingleInstance` overload that takes only the `OptionFlags`
parameter.

## Class WizardWrx.ClassAndMethodDiagnosticInfo (defined in WizardWrx.ClassAndMethodDiagnosticInfo.dll)

This is a new class and a new library that leverages new features in the version
of `System.Runtime.CompilerServices` that ships with version __4.5__ of the Microsoft
.NET Framework to return the name of the calling method without resorting to
Reflection.

For ease of access, the single class, `ClassAndMethodDiagnosticInfo`, is in the
`WizardWrx` namespace. Since it requires a higher version of the framework
than almost everything else, it went into a dedicated library, so that the others
can retain their original target, version __3.5 Client Profile__.

## Class WizardWrx.MoreMath (defined in WizardWrx.MoreMath.dll)

This class is relocated from `WizardWrx.Core.dll` to a dedicated library for two reasons.

1	Since it uses `ClassAndMethodDiagnosticInfo` methods in its exception reports,
its target framework must be at least 4.5.

2	Since its methods perform mathematical operations that can cause arithmetic
overflows that should be caught and reported, it must be compiled with arithmetic
checking enabled.

New functions in this version are a set of `DecimalShift` routines that perform
left and right decimal shift operations. While the required math is technically
trivial, it is easy to get wrong. Hence, in the same spirit that motivated the
`WizardWrx.BitMath` classes, a recent need for a decimal shift motivated creation
of these decimal shift routines.

# Version 7.13

Following is a summary of changes made in version __7.13__, released Monday, __10 December 2018__.

Define `EXACTLY_ONE_HUNDRED_MILLION_LONG`, to meet an immediate requirement, along
with `EXACTLY_ONE_HUNDRED_THOUSAND` and `EXACTLY_ONE_HUNDRED_MILLION`, to more or
less complete the set of powers of ten from two to 9. All powers of ten in that
range sove one (ten million), for which there is no immediate need, are now
covered.

__Change Under Conideration__  Earlier today, I discovered that multiplying a
pair of regular integers (type `System.Int32`) that you expect to yield a long
that is too big to fit in a `System.Int32` yields a large negative number, rather
than throwing an exception. While enabling arithmetic overflow checking in the
advanced compiler settings might elicit exceptions, such an exception would be
an unaacceptable outcome. However, if one of the operands is a long integer (a
`System.Int64`), the compiler generates code that causes the operation to be
performed with long integer operands, yielding the desired outcome. Since
neither outcome of using 32-bit integers in multiplications that yield a 64-bit
product is optimal, it may be worthwhile to define these constants as
long integers, although doing so would almost certainly force arithmatic
operations that would not otherwise be implemented as 64-bit math operations to
be implemented as long integer operations. While this makes little difference in
a 64-bit execution environment, it adds unnecessary complexity to operations
that take place in a 32-bit context. Regardless, scratch storage requirments,
most likely occupying space on the stack, would essentially double for all math
operations that used these constants, and all 64-bit math operations that run in
a 32-bit logical machine require many extra machine instructions, even for the
simplest operations.

For the time being, there are two constants, `EXACTLY_ONE_HUNDRED_MILLION_LONG`
and `EXACTLY_ONE_HUNDRED_MILLION`, which differ only with respect to their types.

# Version 7.12

Following is a summary of changes made in version __7.12__, released Friday, __23 November 2018__.

## Class WizardWrx.MoreMath (defined in WizardWrx.Core.dll)

This class gets a refinement of one of its initial methods, plus two new ones
that are closely related to it.

- `IsEvenlyDivisibleByAnyInteger` prevents the `DivideByZeroException` that would
otherwise arise when a divisor of zero is fed to the second operand of the
modulus operator. To preent it, the divisor is tested, and an `ArugmentException`
exception takes its place. Since the `ArugmentException` arises in user code, the
exception message displays the dividend that was fed into the failed method, to
aid consumers in identifying the source of the exception when the calling code
discards the stack trace.

- `Mod` is the logical companion to `IsEvenlyDivisibleByAnyInteger`, offered as
syntactic sugar, and `Remainder`, analogous to the `IEEERemainder` Math method,
are synonymns.

All three methods share a common message template, which went into the embedded
string resources in the library to facilitate localization.

# Version 7.11

Following is a summary of changes made in version __7.11__, released Saturday, __17 November 2018__.

## Class WizardWrx.MagicNumbers (defined in WizardWrx.Common)

- BREAKING CHANGE  Rename `EXACTLY_ONE_NUNDRED` to `EXACTLY_ONE_HUNDRED` to correct a
misspelling that prevented me finding it.

- Correct the value of `EXACTLY_TEN_THOUSAND`, which I discovered was returning
_one million_.

- Define overlooked constants `EXACTLY_TEN` (10) and `EVENLY_DIVISIBLE` (0), the
latter handy for use with the modulus operator.

## Class WizardWrx.NumericFormats (defined in WizardWrx.Common)

- Define `IntegerToHexStr` overloads that omit the second and third arguments,
substituting common defaults for the missing arguments.

- Change `FormatStatusCode` to use the simplified first overload, shortening its
stack frame and call setup requirments.

## Class WizardWrx.MoreMath (defined in WizardWrx.Core.dll)

This class makes its debut, with the following static methods.

- `IsEvenlyDivisibleByAnyInteger`, defined twice, to accept integer and long
inputs.

- `IsGregorianLeapYear` implements the Gregorian leap year algorithm correctly.

- `IsValidGregorianYear` returns TRUE when given a number is a valid year in the
Gregorian calendar.

## Class WizardWrx.tringExtensions (defined in WizardWrx.Core.dll)

- `RenderEvenWhenNull` represents a null reference as a localizable string literal,
`MSG_OBJECT_REFERENCE_IS_NULL`, defined in `Wizardwrx.Common.Properties.Resources`.

- `EnumFromString` is a generic method that attempts to convert a string to a
member of any enumeration. The enumeration type is inferred from the return
type specified in the method call.

## Namespace  WizardWrx.EmbeddedTextFile (defined in WizardWrx.EmbeddedTextFile.dll)

The documentation of this library is re-phrased so that everything is in active
voice. The code is unchanged.

# Version 7.1

Following is a list of changes made in version __7.1__, released Sunday, __07 October 2018__.

## Class WizardWrx.StringExtensions (defined in WizardWrx.Core.dll)

Incorporate `CapitalizeWords`, which I created and tested as part of the Great
Eastern Energy DataFarmer application.

## Class SpecialStrings (defined in WizardWrx.Common)

Define `SPACE_CHAR` for use when only a string will do, and cross reference the
new constant to its antecedent, `SpecialCharacters.SPACE_CHAR`.

## Class ASCIICharacterDisplayInfo (defined in WizardWrx.ASCIIInfo.dll)

Override `ToString` to render all three representations (printable string,
hexadecimal, then decimal, in that order), and define static method
`DisplayCharacterInfo` to provide that service for an arbitrary character
without instantiating `ASCII_Character_Display_Table`.

## Other Changed Files

Incidental changes included in this commit are as follows.

- __SpecialCharacters.cs__ got a cross reference to `SpecialStrings.SPACE_CHAR`.

- __ProductAssemblyInfo.cs__ reflects the new library version number, 7.1.

- __AssemblyInfo.cs__ in all individual libraries reflects the new library
version number and a build number increment.

- __Resources.resx in DLLServicesTestStand__ has the following new strings
`IDS_ASCII_CHARACTER_INFO`, `IDS_ASCII_TABLE_CHARACTER_PROPERTIES`, and
`IDS_ASCII_TABLE_ENUMERATION`.

- __ClassTestMap.TXT in DLLServicesTestStand__ defines two new unit test method
mappings. Note, too, that the terminal newline is gone. This is by design, to
eliminate 4 bytes from the embedded resource that contribute nothing to its
usability.

- __FormatStringParsing_Drills.cs in DLLServicesTestStand__, which also
exercises `WizardWrx.ASCIIInfo.dll`, which was developed and deployed
concurrently, exercises the new static method for displaying a printable version
of any ASCII character, along with its numerical value, expressed in both
decimal and hexadecimal notation.

- __NewClassTests_20140914.cs in DLLServicesTestStand__ incorporates overlooked
unit tests of the entire `SpecialStrings` class.

- __Program.cs in DLLServicesTestStand__ calls the new methods that went into
class `NewClassTests_20140914`.