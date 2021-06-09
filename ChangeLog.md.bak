# WizardWrx .NET API Change Log

## 2021-05-19

`WizardWrx.AssemblyUtils`, Version 8.0.167, adds two newr static methods that
build on `GetAssemblyVersionInfo`, added to version 8.0.156, to render the
`AssemblyCompany` string as something suitable for use in a path string, such
that the path is devoid of embedded spaces, and can be freely used without
quoting it.

The new methods are:

1. `GetAssemblyCompanyNameSnakeCased` gets the `AssemblyCompany` attribute of an
assemly (by default, the entry assembly), replaces embedded commas, periods,
single quote marks, hyphens, and spaces with underscores, then makes another
pass to replace consecutive underscores with one of them.

2. `GetAssemblyAppDataDirectoryName` gets the name of the directory in the
user's APPDATA directory that corresponds to its company name, optionally
creating the directory unless it exists.

## 05/02/2021 03:53:04

The NuGet packages of all libraries now include a copy of this change log.

`WizardWrx.AssemblyInfo` gets a copy of its DLL configuration file, which
includes a binding redirect that you might need for WizardWrx.AnyCSV, which has
a strong name because it is exposed to COM.

Finally, `WizardWrx.DLLConfigurationManager` also has a DLL configuration file,
but its NuSpec had an error that prevented its deployment.

## 2021-04-18

### WizardWrx.EmbeddedTextFile, version 8.0.129

Static class `Readers` gets a new method, `NameValueCollectionFromEmbbededList`,
that returns a `System.Collections.Specialized.NameValueCollection` populated
from an embedded binary resource consisting of a tab-delimited text file.

In addition to the foregoing classes that incorporate new features, everything else
gets a general refresh, in which all libraries are built against the latest versions
of their dependents. Likewise, the corresponding NuGet packages are kept current with
respect to their dependencies.

## 2021-03-22

The libraries are unchanged. The only change in this release is that the unit
test and demonstration assembly, DLLServices2TestStand.exe version 8.0.1388.0,
fully covers the last batch of constants defined in WizardWrx.Common.dll,
Version 7.24.128.

Since I updated ProductAssemblyInfo.cs, every library got a refresh with a build
number increment or three.

## 2021-02-06

In addition to the following classes that incorporate new features, everything else
gets a general refresh, in which all libraries are built against the latest versions
of their dependents. Likewise, the corresponding NuGet packages are kept current with
respect to their dependencies.

### WizardWrx.ASCIIInfo.dll, Version 8.0.113

The ASCII table entries expose many new properties, of which the most significant
are:

1. `DisplayText`, which always offers a presentable rendering of the character
2. `HTMLName`, which returns a HTML entity string
3. `URLEncoding`, which returns a value that can be used to URL encode any character

### WizardWrx.AssemblyUtils, Version 8.0.156

This release adds a single static class, `AssemblyAttributeHelpers`, which exposes
one equally static method, `GetAssemblyVersionInfo`, that queries any assembly for
its major Custom Attribute values, most of which revolve around versioning and
identification.

## 2021-01-30

This release updates the product version number for the next major version, 8.0.
Hence, 7.24 becomes 8.0.

Going forward, the AssemblyFileVersion shall remain unchanged unless the API
undergoes another major revision, which seems unlikely.

However, the AssemblyVersion attribute of each library shall continue to inch
upwards. Build numbers will increment from the last values they had in the
previous major version, and each release will include each library, regardless
of whether the build includes relevant changes.

If README.MD and the NuGet package file are unchanged, you are safe in assuming
that the code is unchanged, and the new library is backwards compatible with its
predecessors.

## 2020-12-31

Now that I again have a working API key, the NuGet packages are up to date!

I wish a happy New Year to everyone.

## WizardWrx.Common.dll, Version 7.24.128, Released 2020-12-23

This release adds more overlooked symbolic constants that I use from time to
time. The character constants are great for use in switch blocks, which the C#
compiler transforms into very efficient jump tables, as do the optimizing C and
C++ compilers.

|Name                      |Value |Class               |
|--------------------------|------|--------------------|
|MSO_COLLECTION_FIRST_ITEM |     1|ArrayInfo           |
|ALARM                     |     7|SpecialCharacters   |
|BEL                       |     7|SpecialCharacters   |
|BELL                      |     7|SpecialCharacters   |
|BACKSPACE                 |     8|SpecialCharacters   |
|END_OF_FILE               |    26|SpecialCharacters   |
|EOF                       |    26|SpecialCharacters   |
|ESCAPE_CHAR               |    27|SpecialCharacters   |

At present, the special characters have no counterparts in the SpecialStrings
list.

## WizardWrx.Core.dll, Version 7.24.185.0, Released 2020-12-21

This update corrects some internal issues that surfaced when dependent library
`WizardWrx.DLLConfigurationManager.dll` was called upon for the first time to
use a completely populated configuration file. The result was an uninitialized
dictionary owned by the `PropertyDefaults` class exposed by the core library.

Since virtually everything else depends on this library, this is a complete
release, although the only other change, also internal and of no significance to
most users, affects the `ExceptionLogger` singleton exposed by
`WizardWrx.DLLConfigurationManager.dll`.

Whether or not it depends on `WizardWrx.Core.dll` (which all but 3 do),
everything got a minor version bump and a build bump.

## WizardWrx.Common.dll, Version 7.24.127, Released 2020-12-20

This release adds some overlooked symbolic constants that I use regularly.

|Name                      |Value |Class               |
|--------------------------|------|--------------------|
|CAPACITY_4                |     4|MagicNumbers        |
|CAPACITY_8                |     8|MagicNumbers        |
|CAPACITY_10               |    10|MagicNumbers        |
|CAPACITY_16               |    16|MagicNumbers        |
|CAPACITY_128              |   128|MagicNumbers        |
|CAPACITY_255              |   255|MagicNumbers        |
|CAPACITY_256              |   256|MagicNumbers        |
|PLUS_THREE                |     3|MagicNumbers        |
|PLUS_FOUR                 |     4|MagicNumbers        |
|PLUS_FIVE                 |     5|MagicNumbers        |
|PLUS_SIX                  |     6|MagicNumbers        |
|PLUS_SEVEN                |     7|MagicNumbers        |
|PLUS_EIGHT                |     8|MagicNumbers        |
|PLUS_NINE                 |     9|MagicNumbers        |
|MSO_COLLECTION_FIRST_ITEM |     1|MagicNumbers        |
|ARRAY_NEXT_INDEX          |     1|ArrayInfo           |

## WizardWrx.Common.dll, Version 7.24.126, Released 2020-11-02

This update adds a handful of new locale-sensitive string constants and another
handful of string representations of character constants that are frequently
needed as strings, too.

|String Name                      |String Value          |Class               |
|---------------------------------|----------------------|--------------------|
|MSG_DUMMY                        |Dummy                 |Properties.Resources|
|MSG_DUMMY_VALUE                  |DummyValue            |Properties.Resources|
|ASTERISK                         |*                     |SpecialStrings      |
|BRACE_LEFT                       |{                     |SpecialStrings      |
|BRACE_RIGHT                      |}                     |SpecialStrings      |
|BRACKET_LEFT                     |\[                    |SpecialStrings      |
|BRACKET_RIGHT                    |\]                    |SpecialStrings      |
|ASTERISK_CHAR                    |*                     |SpecialStrings      |
|AT_SIGN                          |@                     |SpecialStrings      |
|CARRIAGE_RETURN                  |\r                    |SpecialStrings      |
|CHAR_NUMERAL_0                   |0                     |SpecialStrings      |
|CHAR_NUMERAL_1                   |1                     |SpecialStrings      |
|CHAR_NUMERAL_2                   |2                     |SpecialStrings      |
|CHAR_NUMERAL_7                   |7                     |SpecialStrings      |
|CHAR_LC_I                        |i                     |SpecialStrings      |
|CHAR_UC_I                        |I                     |SpecialStrings      |
|CHAR_LC_L                        |l                     |SpecialStrings      |
|CHAR_UC_L                        |L                     |SpecialStrings      |
|CHAR_LC_O                        |o                     |SpecialStrings      |
|CHAR_UC_O                        |O                     |SpecialStrings      |
|CHAR_LC_Z                        |z                     |SpecialStrings      |
|CHAR_UC_Z                        |Z                     |SpecialStrings      |
|CHECK_MARK_CHAR                  |\xFB                  |SpecialStrings      |
|EQUALS_SIGN                      |=                     |SpecialStrings      |
|HASH_TAG                         |#                     |SpecialStrings      |
|LAST_ASCII_CHAR                  |\xFF                  |SpecialStrings      |
|LINEFEED                         |\n                    |SpecialStrings      |
|NONBREAKING_SPACE_CHAR           |\0xA0                 |SpecialStrings      |
|PARENTHESIS_LEFT                 |(                     |SpecialStrings      |
|PARENTHESIS_RIGHT                |)                     |SpecialStrings      |
|PIPE_CHAR                        |\|                    |SpecialStrings      |
|QUESTION_MARK                    |?                     |SpecialStrings      |

As of this version, essentially everything that has a character representation
has an analogous string representation.

## WizardWrx.AssemblyUtils 7.23.121, Released 2019/12/25

This release corrects an omission from the overloaded `DependentAssemblies`
constructor that rendered it inoperable.

## WizardWrx.AssemblyUtils 7.23.119, Released 2019/12/17

This release adds a new instance method, `GetDependentAssemblyInfos`, to class
`DependentAssemblies`, which returns a sorted list of dependent assemblies.
Though this could be implemted as a read-only property, I chose to implement it
as a method.

## WizardWrx.Core 7.23.182, Released 2019/12/16

This update adopts the generally accepted SemVer version numbering scheme.

The code is otherwise unchanged from the previous release.

## WizardWrx.Common.dll, Version 7.23.123, Released 2019/12/15

This update adds static method FormatIntegerLeftPadded to the NumericFormats
class. This update also adopts the generally accepted SemVer version numbering
scheme.

## WizardWrx.ASCIIInfo 7.23.84, Released 2019/12/15

This update adopts the generally accepted SemVer version numbering scheme.

The code is otherwise unchanged from the previous release.

## WizardWrx.FormatStringEngine 7.23.167, Released 2019/12/15

This update adopts the generally accepted SemVer version numbering scheme.

The code is otherwise unchanged from the previous release.

## WizardWrx.DiagnosticInfo.dll, version 7.22.5, Released 2019/10/06

This release corrects a typographical error found when I skimmed its ReadMe.md
file following a dogfood run that incorporated it into a package that is under
active development, initially for internal use. The only reason for the build is
to satisfy the NuGet package publishing engine.

## WizardWrx Common.dll, version 7.22.122, Released 2019/10/02

The public string resource set has the 18 new strings listed in the following
table.

|String Name                      |String Value          |
|---------------------------------|----------------------|
|ANSWER_IS_FALSE                  |false                 |
|ANSWER_IS_NO                     |no                    |
|ANSWER_IS_TRUE                   |true                  |
|ANSWER_IS_YES                    |yes                   |
|MSG_BLANK_CAPS                   |BLANK                 |
|MSG_INTEGER_EVENLY_DIVISIBLE_BY  |is evenly divisible by|
|MSG_INTEGER_IS_EVEN              |is even               |
|MSG_INTEGER_IS_ODD               |is odd                |
|MSG_OBJECT_DOES_NOT_EXIST        |does not exist        |
|MSG_IS                           |is                    |
|MSG_IS_NOT                       |is not                |
|MSG_OBJECT_EXISTS                |exists                |
|MSG_OBJECT_IS_ABSENT             |is absent             |
|MSG_OBJECT_IS_MISSING            |is missing            |
|MSG_OBJECT_IS_PRESENT            |is present            |
|MSG_OBJECT_REFERENCE_IS_NULL_CAPS|NULL                  |
|MSG_VALUE_IS_FALSE               |false                 |
|MSG_VALUE_IS_TRUE                |true                  |

Since the meaning and use of these strings is self-evident, I dispensed with
comments in the .RESX source file and herein.

All other libraries in this constellation are unchanged.

## WizardWrx Common.dll, version 7.21.121, Released 2019/07/18

The public string resource set has the new strings listed in the following table.

|String Name                   |String Value                                            |Source Code Comment in the .RESX File                                                                                                                                                                                                                        |
|------------------------------|--------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|ERRMSG_ARRAY_IS_EMPTY         |Array {0} ie empty; it exists, but contains no elements.|Use this message to report that an array argument or program variable exists, but is empty. Use ERRMSG_REFERENCE_IS_NULL to report a null reference. Use the nameof() pseudo-function to identify the array by name.                                         |
|ERRMSG_REFERENCE_IS_NULL      |Object {0} is a null reference.                         |Use this message to report a null reference. Unlike its predecessor, ERRMSG_ARG_IS_NULL, this string has a substitution token through which to report the object name. Use the nameof() pseudo-function to get the name of the argument or internal variable.|
|MSG_STRING_IS_EMPTY           |\[empty\]                                               |Use this token to represent the empty string as the word "empty" in brackets.                                                                                                                                                                                |
|MSG_BLANK_STRING              |\[blank\]                                               |Use this token to represent the empty string as the word "blank" in brackets.                                                                                                                                                                                |

Many other strings get improved comments, which can presently be seen only by
viewing the .RESX file in an editor.

## WizardWrx.MoreMath, version 7.21.3, Released 2019/07/18

`IncrementAndReturnNewValue` methods, with four overloads, covering the four
generic integral types (int, uint, long, ulong), increment the value of the
integer to which the reference points, returning the incremented value as
their own return value.. All four take a reference to an integer and increment
it, returning the new value. The goal is to maintain a static (Shared in Visual
Basic) integer, while making the new value available for local use with a single
method call.

__Note:__ Testing the `IncrementAndReturnNewValue` methods exposed a coding
error caused by moving the report output directory up a level in the directory
tree that prevented the automated test scripts from covering every unit test.

## WizardWrx Common.dll, version 7.21.117, Released 2019/07/09

The public string resources has a new string, `STACK_TRACE_ENTRY_PREFIX`.

## WizardWrx Common.dll version 7.20.116, Released 2019/07/04

The public string resources have two sets of new strings, named
`REGISTRY\_VALUE\_TYPE\_*` and `VERSION\_STRING\_PART\_*` listed in the following
table.

|String Name                   |String Value |
|------------------------------|-------------|
|REGISTRY_VALUE_TYPE_BINARY    |REG_BINARY   |
|REGISTRY_VALUE_TYPE_DWORD     |REG_DWORD    |
|REGISTRY_VALUE_TYPE_EXPAND    |REG_EXPAND_SZ|
|REGISTRY_VALUE_TYPE_MULTI     |REG_MULTI_SZ |
|REGISTRY_VALUE_TYPE_QWORD     |REG_QWORD    |
|VERSION_STRING_PART_BUILD_ABBR|Bld.         |
|VERSION_STRING_PART_BUILD_LONG|Build Number |
|VERSION_STRING_PART_BUILD_SHORT|Build       |
|VERSION_STRING_PART_MAJOR_ABBR|Maj.         |
|VERSION_STRING_PART_MAJOR_LONG|Major Version|
|VERSION_STRING_PART_MAJOR_SHORT|Major       |
|VERSION_STRING_PART_MINOR_ABBR|Min.         |
|VERSION_STRING_PART_MINOR_LONG|Minor Version|
|VERSION_STRING_PART_MINOR_SHORT|Minor       |
|VERSION_STRING_PART_REVNO_ABBR|Rev.         |
|VERSION_STRING_PART_REVNO_LONG|Revision     |
|VERSION_STRING_PART_REVNO_SHORT|Revision    |

### Other Libraries

New builds of the libraries listed in the following table address diamond
dependencies on libraries WizardWrx.AnyCSV.dll and WizardWrx.Common.dll. In this
case, the diamond dependencies are harmless, but their resolution paves the way
for a forthcoming version numbering rationalization exercise.

|Library Name                    |NuGet Package Name          |
|--------------------------------|----------------------------|
|WizardWrx.DiagnosticInfo.dll    |WizardWrx.DiagnosticInfo    |
|WizardWrx.FormatStringEngine.dll|WizardWrx.FormatStringEngine|

## Release Notes, 2019/06/11, WizardWrx AssemblyUtils.dll, Version 7.20.117

This build is a bug fix. I discovered that the iten number written into the
optional tab-delimited output file generated by
`SortableManagedResourceItem.ListResourcesInAssemblyByName` was always the
total item count, rather than the actual item number.

Although MSBuild created a new version of the test stand assembly, its code is
unchanged.

## Version 7.20

Only two of the ten libraries in this constellation have changes in this minor
update that was driven primarily by the need to fix a bug.

### WizardWrx.Core

The static class that defines the `FileInfo` extension methods is the beneficiary
of the bug fix.

`ShowFileDetails`: Previous testing overlooked the case when the specified
file does not exist. This is an issue because I overlooked the fact that the
`Length` property on the `FileInfo` object throws a `FileNotFoundException`
Exception when the associated file does not exist, although other properties
return values of some kind, regardless of whether the file is present or
absent. Size zero is a legitimate, and fairly commonplace, length (size) of a
file, this method reports -1, which is not. This is consistent with the behavior
of the time stamp properties, which report 1600/01/01 00:00:00 when the file is
absent.

Everything else in the library is unchanged, since no other issues have
surfaced in daily development use.

### WizardWrx AssemblyUtils

* `SortableManagedResourceItem.ListResourcesInAssemblyByName` gets an optional
`StreamWriter` argument that causes it to create a tab delimited list of the
managed string resources in an assembly.

* `ReportGenerators.ListKeyAssemblyPropertiess` had lost the summary paragraph
of its XML documentation, which is restored.

## Version 7.19

This upgrade affects only one library, `WizardWrx.Core.dll`; although others
are updated with the new product version number, they remain otherwise
unchanged. The changes in this library are confined to the `StringExtensions`
class, and consist of the following three new methods.

|Method Name        |Method Goal                                     |
|-------------------|------------------------------------------------|
|UnixLineEndings    |Replace CR/LF pairs and bare CRs with bare LFs. |
|WindowsLineEndings |Replace bare LFs with CR/LF pairs.              |
|OldMacLineEndings  |Replace CR/LF pairs and bare LFs with bare CRs. |

There are literally no other changes in this update.

## Version 7.18

Version 7.18 is a maintenance release, which affects only one library,
`WizardWrx.Core.dll`, which got rushed into production without sufficient
testing. This release got the substantially more careful testing that I prefer
to give to everything. The companion NuGet package went out when I built the
release configuration of the whole library set.

## Version 7.17

With this release, the two most actively updated libraries, `WizardWrx.Core.dll`
and `WizardWrx.Common.dll`, automatically update their respective NuGet packages
when a new release build is created. Other libraries will eventually get the
same treatment, but these two went first, because I needed to update them and
immediately pull both into another project, and they receive by far the most
frequent updates. At present, these are the only two projects that have NuGet
package generation and publication built into their `.csproj` files.

### Class WizardWrx.AssemblyUtils.ReportGenerators (defined in WizardWrx.AssemblyUtils.dll)

Since its inception, this class has always listed the file version reported by
the FileVersion object in the System.Diagnostics namespace. Beginning with this
version, the assembly version reported by the Assembly class in the
System.Reflection namespace is also given, and each is labeled with its source.

### Class WizardWrx.StringFixups (defined in WizardWrx.Core.dll)

Override the default `ToString` method, so that it returns a formatted string
containing the values of its two members (properties), so that they appear in a
watch window without requring the object to be expanded.

### Class WizardWrx.StringExtensions (defined in WizardWrx.Core.dll)

A new `ReplaceEscapedTabsInResourceString` extension method undoes the escaped TAB
characters that go into a string that contains TAB characters when it is pasted
into the string resource editor to become part of a .resx file.

### Class WizardWrx.FileInfoExtensionMethods (defined in WizardWrx.Core.dll)

`ShowFileDetails` is a new FileInfo extension method that returns a formatted
string containing user-selected properties of a file.

### Class WizardWrx.ReportHelpers (defined in WizardWrx.Core.dll)

Significantly simplify `CreateFormatString` and `CreateLastToken` by way of a much
more efficient algorithm that consumes much less memory and CPU time.

### Class WizardWrx.NumericFormats (defined in WizardWrx.Common.dll)

The XML comments attached to integer constant `DECIMAL_DIGITS_DEFAULT` contained
a plain ASCII table that caused it to display incorrectly in the DocFX page that
was generated from it. Though this change affected the source code, forcing a
new build, the generated assembly is otherwise identical to its predecessor.

## Version 7.16

The additions revolve around a new method to apply pairs of strings, each
composed of an original and a replacement value, to a string. Two versions
exist, one which provides a place to stash the array, while the other is an
extension method on the System.String class. The two new resource strings are
incidental, though both are employed in the unlikely event that you feed an
invalid array to the new methods.

### Class WizardWrx.Common.Properties.Resources (defined in WizardWrx.Common.dll)

Since they are intended to be used everywhere, the string resources are marked
as Public. This release demonstrates the value of that, with additions as
follows.

|Name                   |Value   |Comment                                                            |
|-----------------------|--------|-------------------------------------------------------------------|
|MSG_VALUE_IS_INVALID   |invalid |Use this string to report that the value of a variable is invalid. |
|MSG_VALUE_IS_VALID     |valid   |Use this string to report that the value of a variable is valid.   |

### Class WizardWrx.Core.StringFixups (defined in WizardWrx.Core.dll)

This new class exposes one method, `ApplyFixups`, which applies pairs of strings
comprised of an original and its replacement to a string. The array of string
pairs is stored in the instance for reuse.

### Class WizardWrx.StringExtensions.cs (defined in WizardWrx.Core.dll)

This established string extension class gets a new method, `ApplyFixups`, which
applies pairs of strings comprised of an original and its replacement to a
string. Unlike the StringFixups class, which has a place to store them, this
method takes the array as its argument.

## 2019/05/03

Only the __product__ build number changed, from 210 to 211, to account for the
migration of all subsidiary projects to NuGet packages.

Each of the 10 libraries is in its own like-named NuGet package. For example,
the Nuget package that contains `WizardWrx.Core.dll` is `WizardWrx.Core`. The
following table lists the packages, along with their version numbers.

|Library                               |Package Name                       |Version        |
|--------------------------------------|-----------------------------------|---------------|
|WizardWrx.ASCIIInfo.dll               |WizardWrx.ASCIIInfo                |7.1.83.29298   |
|WizardWrx.Common.dll                  |WizardWrx.Common                   |7.15.102.41891 |
|WizardWrx.FormatStringEngine.dll      |WizardWrx.FormatStringEngine       |7.15.162.826   |
|WizardWrx.Core.dll                    |WizardWrx.Core                     |7.15.153.2428  |
|WizardWrx.AssemblyUtils.dll           |WizardWrx.AssemblyUtils            |7.15.114.34743 |
|WizardWrx.DiagnosticInfo.dll          |WizardWrx.DiagnosticInfo           |7.15.1         |
|WizardWrx.MoreMath.dll                |WizardWrx.MoreMath                 |7.15.1.36803   |
|WizardWrx.ConsoleStreams.dll          |WizardWrx.ConsoleStreams           |7.15.191.37663 |
|WizardWrx.EmbeddedTextFile.dll        |WizardWrx.EmbeddedTextFile         |7.15.90.42197  |
|WizardWrx.DLLConfigurationManager.dll |WizardWrx.DLLConfigurationManager  |7.15.211       |

In addition to substituting NuGet packages throughout, a handful of errata in
the XML documentation got fixed. Otherwise, the code is unchanged from the code
that was first marked as version 7.15.

## Version 7.15

Following is a summary of changes made in version __7.15__, released Sunday, __28 April 2019__.

### Class WizardWrx.SpecialStrings (defined in WizardWrx.Common.dll)

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

### Class WizardWrx.SpecialCharacters (defined in WizardWrx.Common.dll)

XML comments attached to the character constants that correspond to the new
string constants listed above get new cross references to the corresponding
string constant. There are no new constants.

### Class WizardWrx.ConsoleStreams.DefaultErrorMessageColors (defined in WizardWrx.ConsoleStreams.dll)

Supplement the `PropsSetFromConfig` property with a `PropsLeftAtDefault` property.

This class also benefits from changes made in its base class,
`WizardWrx.AssemblyLocatorBase`, defined in `WizardWrx.Core.dll`.

### Class WizardWrx.Core.AssemblyLocatorBase (defined in WizardWrx.Core.dll)

1) Replace the `ConfigMessage` string property with the
`RecoveredConfigurationExceptions` list.

2) Replace the properties collection enumeration with the much more efficient
dictionary lookup.

Though it is omitted from the change log, a significant benefit is that these
changes, along with others deeper in the code, eliminated a harmless null
reference exception that was being silently thrown, caught, and handled. That
exception is addressed by adding an overlooked null reference test that prevents
the code that would have executed from doing so.

### Class WizardWrx.Core.PropertyDefaults (defined in WizardWrx.Core.dll)

`EnumerateMissingConfigurationValues` is an instance method that reports
configuration values that are defined, but are missing from the configuration
file. All such properties have hard coded default values. The report is returned
as a formatted string that can be logged to the system console or the event log,
or displayed on a message box.

This class also benefits from changes made in its base class,
`WizardWrx.AssemblyLocatorBase`, defined in `WizardWrx.Core.dll`.

### Class WizardWrx.RecoveredException (defined in WizardWrx.Core.dll)

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

## Version 7.14

Following is a summary of changes made in version __7.14__, released Monday, __24 December 2018__.

### Class WizardWrx.MagicNumbers (defined in WizardWrx.Common.dll)

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

### Class WizardWrx.DLLConfigurationManager (defined in WizardWrx.DLLConfigurationManager.dll)

Add a new `GetTheSingleInstance` overload that takes only the `OptionFlags`
parameter.

### Class WizardWrx.ClassAndMethodDiagnosticInfo (defined in WizardWrx.ClassAndMethodDiagnosticInfo.dll)

This is a new class and a new library that leverages new features in the version
of `System.Runtime.CompilerServices` that ships with version __4.5__ of the Microsoft
.NET Framework to return the name of the calling method without resorting to
Reflection.

For ease of access, the single class, `ClassAndMethodDiagnosticInfo`, is in the
`WizardWrx` namespace. Since it requires a higher version of the framework
than almost everything else, it went into a dedicated library, so that the others
can retain their original target, version __3.5 Client Profile__.

### Class WizardWrx.MoreMath (defined in WizardWrx.MoreMath.dll)

This class is relocated from `WizardWrx.Core.dll` to a dedicated library for two reasons.

1   Since it uses `ClassAndMethodDiagnosticInfo` methods in its exception reports,
its target framework must be at least 4.5.

2   Since its methods perform mathematical operations that can cause arithmetic
overflows that should be caught and reported, it must be compiled with arithmetic
checking enabled.

New functions in this version are a set of `DecimalShift` routines that perform
left and right decimal shift operations. While the required math is technically
trivial, it is easy to get wrong. Hence, in the same spirit that motivated the
`WizardWrx.BitMath` classes, a recent need for a decimal shift motivated creation
of these decimal shift routines.

## Version 7.13

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

## Version 7.12

Following is a summary of changes made in version __7.12__, released Friday, __23 November 2018__.

### Class WizardWrx.MoreMath (defined in WizardWrx.Core.dll)

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

## Version 7.11

Following is a summary of changes made in version __7.11__, released Saturday, __17 November 2018__.

### Class WizardWrx.MagicNumbers (defined in WizardWrx.Common)

- BREAKING CHANGE  Rename `EXACTLY_ONE_NUNDRED` to `EXACTLY_ONE_HUNDRED` to correct a
misspelling that prevented me finding it.

- Correct the value of `EXACTLY_TEN_THOUSAND`, which I discovered was returning
_one million_.

- Define overlooked constants `EXACTLY_TEN` (10) and `EVENLY_DIVISIBLE` (0), the
latter handy for use with the modulus operator.

### Class WizardWrx.NumericFormats (defined in WizardWrx.Common)

- Define `IntegerToHexStr` overloads that omit the second and third arguments,
substituting common defaults for the missing arguments.

- Change `FormatStatusCode` to use the simplified first overload, shortening its
stack frame and call setup requirments.

### Class WizardWrx.MoreMath (defined in WizardWrx.Core.dll)

This class makes its debut, with the following static methods.

- `IsEvenlyDivisibleByAnyInteger`, defined twice, to accept integer and long
inputs.

- `IsGregorianLeapYear` implements the Gregorian leap year algorithm correctly.

- `IsValidGregorianYear` returns TRUE when given a number is a valid year in the
Gregorian calendar.

### Class WizardWrx.tringExtensions (defined in WizardWrx.Core.dll)

- `RenderEvenWhenNull` represents a null reference as a localizable string literal,
`MSG_OBJECT_REFERENCE_IS_NULL`, defined in `Wizardwrx.Common.Properties.Resources`.

- `EnumFromString` is a generic method that attempts to convert a string to a
member of any enumeration. The enumeration type is inferred from the return
type specified in the method call.

## Namespace  WizardWrx.EmbeddedTextFile (defined in WizardWrx.EmbeddedTextFile.dll)

The documentation of this library is re-phrased so that everything is in active
voice. The code is unchanged.

## Version 7.1

Following is a list of changes made in version __7.1__, released Sunday, __07 October 2018__.

### Class WizardWrx.StringExtensions (defined in WizardWrx.Core.dll)

Incorporate `CapitalizeWords`, which I created and tested as part of the Great
Eastern Energy DataFarmer application.

### Class SpecialStrings (defined in WizardWrx.Common)

Define `SPACE_CHAR` for use when only a string will do, and cross reference the
new constant to its antecedent, `SpecialCharacters.SPACE_CHAR`.

### Class ASCIICharacterDisplayInfo (defined in WizardWrx.ASCIIInfo.dll)

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