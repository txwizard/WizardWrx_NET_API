# WizardWrx .NET API ReadMe

## Release_Notes_7.11.TXT

Fixes and improvements (mostly the latter) are listed in the cumulative history,
`ChangeLog.md` and `Release_Notes_7.11.TXT`. Since the release notes were copied
from the revision histories of the source files, I put them into a text file
instead of a Markdown file. Nevertheless, you can read it directly in the GitHub
repository without explictly downloading it.

The most significant improvement is the addition of `EnumFromString` and
`RenderEvenWhenNull` to the `WizardWrx.StringExtensions` classs, which defines
extension methods on the `System.string` class.

Another significant improvement is the addition of `IsGregorianLeapYear` defined
in the new `WizardWrx.MoreMath` class, defined in `WizardWrx.Core.dll`, which
faithfully implements the Gregorian year algorithm. This addition came about due
to an anticipated need for it in my current paying project, and is a port of code
that I wrote in ANSI C, and thoroughly tested about a decade ago.

## Purpose of These Libraries

The purpose of these class libraries is to expedite development of applications
that target any version of the Microsoft .NET Framework, from version 2.0 up.
The classes in these libraries define numerous constants, most assigned to the
base WizardWrx namespace, and utility classes, organized into subsidiary
namespaces.

## Using These Libraries

Since there are no name collisions, you may safely set references to all 6
namespaces in the same source module.

Detailed API documentation is at <https://txwizard.github.io/WizardWrx_NET_API/>.

For those who just want to use them, debug and release builds of the libraries
and the unit test program are available as archives off the project root
directory.

*	`WizardWrx_NET_API_Binaries_Debug.7z` is the debug build of the binaries.

*	`WizardWrx_NET_API_Binaries_Release.7z` is the release build of the binaries.

There is a DLL, PDB, and XML file for each library. To derive maximum benefit,
including support for the Visual Studio managed code debugger and IntelliSense
in the text editor, take all three.

__Important__: All but a couple of the libraries depend on one or more others;
if in doubt, check the manifest of the DLL(s) you plan to use. You can use
`ILDASM.exe`, which ships with the Microsoft .NET Framework SDKs.

## Change Log

See `ChangeLog.md` for a cumulative history of changes, listed from newest to
oldest, beginning with version 7.1. Previous changes are meticulously documented
in the top of each source file.

## Road Map

For the most part, this constellation of class libraries evolves to acommodate
needs as they arise in my development work. Nevertheless, I have a road map, and
it has a few well-marked stops.

1.	__NuGet Packages__ are a certainty, once I decide whether there should be
one package or seven (1 for each library, plus a rollup that contains all six),
and find time to create them.

2.	The classes that process `FormatItems` and `FormatStrings` could stand
to be made more usable.

3.	`DigestString` and `DigestFile` support only MD5 and the SHAx message
digest algorithms. While that covers the two most commonly used algorithms, I
would like to cover others, and add HMAC digest authentication support. I have a
native library, implemented in ANSI C, that supports HMAC, but I have yet to
investigate what it would take to convert it.

4.	`AgedFileInfo` and its companion `AgedFileInfoCollection` could stand a
companions that support other orderings of file lists, such as by size, type, or
name.

5.	`ExceptionLogger` should define an interface to simplify replacing its
`ReportException` methods. Formatting of `ReportException` output could also
stand to be a bit more flexible.

6.	`ReportDetail` and `ReportHelpers` were intended to be the foundation of
an ambitious report generator for character-mode programs of the sort that are
the lifeblood of systems software.

7.	Devise a simple, automated mechanism to have the configuration file
associated with a DLL that defines a class that derives from`PropertyDefaults`
follow it, as do the associated XML documentation and program data base (`.pdb`)
files.

## Contributing

Though I created this library to meet my individual development needs, I have
put a good bit of thought and care into its design. Moreover, since I will not
live forever, and I want these libraries to outlive me, I would be honored to
add contributions from others to it. My expectations are few, simple, easy to
meet, and intended to maintain the consistency of the code base and its API.

1.	__Naming Conventions__: I use Hungarian notation. Some claim that it has
outlived its usefulness. I think it remains useful because it encodes data
about the objects to which the names are applied that follows them wherever they
go, and convey it without help from IntelliSense.

2.	__Coding Style__: I have my editor set to leave spaces around every token.
This spacing has helped me quicly spot misplaced puncuation, such as the right
bracket that closes an array initializer that is in the wrong place, and it
makes mathematical expressions easier to read and mentally parse.

3.	__Comments__: I comment liberally and very deliberately. Of particular
importance are the comments that I append to the bracket that closes a block. It
does either or both of two things: link it to the opening statement, and
document which of two paths an __if__ statement is expected to follow most of
the time. When blocks get nested two, three, or four deep, they really earn
their keep.

4.	__Negative Conditions__: I do my best to avoid them, because they almost
always cause confusion. Astute observers will notice that they occur from time
to time, because there are _a few cases_ where they happen to be less confusing.

5.	__Array Initializers__: Arrays that have more than a very few initializers,
or that are initialized to long strings, are laid out as lists, with line
comments, if necessary, that describe the intent of each item.

6.	__Format Item Lists__: Lists of items that are paired with format items in
calls to `string.Format`, `Console.WriteLine`, and their relatives, are laid out
as arrays, even if there are too few to warrant one, and the comments show the
corresponding format item in context. This helps ensure that the items are
listed in the correct order, and that all format items are covered.

7.	__Symbolic Constants__: I use symbolic constants to document what a literal
value means in the context in which it appears, and to disambiguate tokens that
are easy to confuse, suzh as `1` and `l` (lower case L), `0` and `o` (lower case O),
literal spaces (1 and 2 spaces are common), underscores, the number `-1`, and so
forth. Literals that are widely applicable are defined in a set of classes that
comprise the majority of the root `WizardWrx` namespace.

8.	__Argument Lists__: I treat argument lists as arrays, and often comment each
argument with the name of the paramter that it represents. These lists help
guaranteeing that a long list of positional arguments is specified in the
correct order, especially when several are of the same type (e. g., two or more
integer arguments).

9.	__Triple-slash Comments__: These go on _everything_, even private members and
methods, so that everything supports IntelliSense, and it's easy to apply a tool
(I use DocFX.) to generate reference documentation.

With respect to the above items, you can expect me to be a nazi, though I shall
endeavor to give contributors a fair hearing when they have a good case.
Otherwise, please exercise your imagination, and submit your pull requests. When
I get NuGet packages implemented, I'll take care of rolling the contributions
into the appropriate packages, and contributors will get prominent credit on the
package page in the official public repository. If you skim the headnotes of the
code, you'll see that I take great pains to give others credit when I icorporate
their code into my projects, even to the point of disclaiming copyright or
leaving their copyright notice intact. Along the same lines, the comments are
liberally sprinkled with references to articles and Stack Overflow discussions
that contributed to the work.