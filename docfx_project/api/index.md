# WizardWrx .NET API

The following sections summarize the classes in each namespace. Unless indicated
otherwise, each class is exposed by a library of the same name as the namespace.
To use them, add a reference to the indicated library, and add a using directive
that specifies the corresponding namespace to any module that references it.

Use the links in the table of contents along the left side of this page to view
the documentation of a namespace.

## WizardWrx: The Root Namespace

These classes are defined in `WizardWrx.Common.dll`.

*	__ArrayInfo__ defines constants and static utility methods for working with
arrays.

*	__ClassAndMethodDiagnosticInfo__, defined in a new class library,
`WizardWrx.ClassAndMethodDiagnosticInfo.dll`, that requires version 4.5 or
higher of the Microsoft .NET Framework, offers a drop-in replacement for
`System.Reflection.MethodBase.GetCurrentMethod ( ).Name` that leverages new
features in the version of `System.Runtime.CompilerServices` that ships with
version __4.5__ of the Microsoft .NET Framework to return the name of the
calling method without resorting to Reflection. Other methods report the source
file name and line number where the call originated. All three methods leverage
data that is emitted by the compiler, and becomes the value of an optional
parameter that compiler essentially supplies, *at compile time*. This is a huge
improvement, as it supports a pattern similar to one that I implemented, in
COBOL, in 1980.

*	__CSVFileInfo__ defines constants for working with CSV style files.

*	__DisplayFormats__  defines conostants that express standard format string for
use with the ToString method on most objects and string.Format, along with a set
of static methods for formatting dates, times, and time spans.

*	__FileIOFlags__ defines constants that clarify the intent of the flags that
modify the behavior of stream I/O methods.

*	__ListInfo__ exposes handy constants for working with buffers, lists, and
substrings.

*	__MagicBooleans__ defines frequently used Boolean values whose correct values
are easier to remember with the help of a mnemonic. The constants defined herein
are pairs. The first part of the name of each pair associates it with the method
or constructor with which it is intended to be used. The remainder of the name
identifies the behavior elicited from the object or method by specifying this
value.

*	__MagicNumbers__ defines constants for commonly used magic numbers. Others
are defined in companion class `ArrayInfo`, while `SpecialCharacters` defines
character representations of the visually ambiguous numbers and letters, for use
in place of literals.

   Version 7.14 defines the constants listed in the following table.

   | Name                    |             Value |
   |-------------------------|------------------:|
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

*   __MoreMath__ (defined in `WizardWrx.MoreMath.dll`) defines mathematical
operations that are mechanically simple, but tricky to get right.

   1	`DecimalShiftLeft` and `DecimalShiftRight` are defined for all numeric types.

   2	`IsEvenlyDivisibleByAnyInteger` prevents the `DivideByZeroException` that
would otherwise arise when a divisor of zero is fed to the second operand of the
modulus operator. To preent it, the divisor is tested, and an `ArugmentException`
exception takes its place. Since the `ArugmentException` arises in user code, the
exception message displays the dividend that was fed into the failed method, to
aid consumers in identifying the source of the exception when the calling code
discards the stack trace.

   3	`Mod` is the logical companion to `IsEvenlyDivisibleByAnyInteger`, offered as
syntactic sugar, and `Remainder`, analogous to the `IEEERemainder` method in the
system math library, are synonymns.

*	__NumericFormats__ defines standard numeric format strings, for use with the
`string.Format` method and its derivatives and relatives.

*	__PathPositions__ defines constants to document path string parsing.

*	__RegExpSupport__ defines constants, some built from others by static methods,
to expedite common tasks that use regular expressions.

*	__SpecialCharacters__ defines constants to use when you want or need your
listings to be crystal clear about certain ambiguous character literals, such as
commas, periods, asterisks, etc.

*	__SpecialStrings__ defines special purpose strings that are either difficult
to get right in the first place, or display ambiguously in text editors and
printed source code listings.

*	__StringTricks__ defines static methods for performing common tasks not
provided by the `System.String` class. Most, but not all, have been superseded
by extension methods, but they remain for backwards compatibility with internal
consumers that have yet to be updated.

## WizardWrx.ConsoleStreams Namespace

*	__DefaultErrorMessageColors__ expose the default fatal and nonfatal exception
message colors, which are defined in a standard application configuration file
that is linked to the assembly that defines this class.

*	__DigestFile__ exposes static methods for computing message digests for files,
using the most common algorithms. Unlike the BCL methods that do the real work,
these methods require just one call to process an entire file. (This class is
defined in `WizardWrx.Core.dll`.)

*	__DigestString__ exposes static methods for computing message digests for
strings, using the most common algorithms. Unlike the BCL methods that do the
real work, these methods require just one call to process the entire string.
(This class is defined in `WizardWrx.Core.dll`.)

*	__FileInfoExtensionMethods__ extends the `System.IO.FileInfo` class with methods
for testing, setting, and clearing file attribute flags, including saving and
restoring flags to their initial states. (This class is defined in
`WizardWrx.Core.dll`.)

*	__FileNameTricks__ exposes numerous static methods for manipulating file
names. Unlike the objects in the `System.IO.File` namespace, these methods don't
need a real file object. All work on strings that represent file names. (This
class is defined in `WizardWrx.Core.dll`.)

*	__GenericSingletonBase<T>__ is a complete implementation of the Singleton
design pattern that takes advantage of a guarantee made by the framework that it
won't bother to call a static constructor on a class until its first use, and
that a static constructor is never called more than once, no matter how many
subsequent references to the class occur. For backwards compatibility, there is
a GetTheSingleInstance method that takes no arguments; it just returns a
reference to the static instance. (This class is defined in
`WizardWrx.Core.dll`.)

*	__ListHelpers__ exposes methods for merging sorted lists of items, and to
simplify working with the values returned through the __IComparable__ interface.
(This class is defined in `WizardWrx.Core.dll`.)

*	__ErrorMessagesInColor__ wrap calls to `Console.Error.Write` and
`Console.Error.WriteLine` with code that lets them write in living color. Use
instance methods as drop-in replacements for `Console.Error.Write` and
`Console.Error.WriteLine`. A full set of companion static methods exists for
writing one-off colored messages; these take two extra parameters that specify
the text (foreground) and background colors.

*	__MessageInColor__ expose a combination of static and instance methods that
reduce writing console messages in any supported combination of foreground and
background colors to method calls that mirror the `Console.Write` and
`Console.WriteLine` methods that you already know and use.

*	__StandardHandleInfo__ is a static class that provides type-safe managed
methods to determine the true redirection state of a standard console handle,
including learning the name of the file to which it is redirected.

*	__NumberFormatters__ exposes methods to simplify formatting of integers,
floating point (single precision real numbers) and double precision real
numbers. For integers, these methods cover both decimal and the most frequently
used hexadecimal representations, spanning from two through sixteen hexadecimal
glyphs. (This class is defined in `WizardWrx.Core.dll`.)

*	__ReportDetail__ instances represent generic report details, with labels and
formats for printing them. (This class is defined in `WizardWrx.Core.dll`.)

*	__ReportHelpers__ exposes methods to help prepare strings for use on reports.
(This class is defined in `WizardWrx.Core.dll`.)

*	__StringExtensions__ is a class of extension methods for performing common
tasks not provided by the `System.String` class. All but the four Pad methods
are derived from long established routines in companion class `StringTricks`.
(This class is defined in `WizardWrx.Core.dll`.)

*	__SyncRoot__ provide classes that must be made thread-safe with locks over
which the class has complete control. It has a Label property that can be used
to disambiguate instances. (This class is defined in `WizardWrx.Core.dll`.)

*	__SysDateFormatters__ implements my stalwart date and time formatter,
`ReformatSysDateP6C`, which I created initially as a Windows Interface Language
(WIL, a. k. a. WinBatch) library function, `Reformat_Date_YmdHms_P6C`, in
October 2001, although its roots go back much further in my WIL script
development. (This class is defined in `WizardWrx.Core.dll`.)

*	__TextBlocks__ exposes methods for creating and manipulating test blocks.
(This class is defined in `WizardWrx.Core.dll`.)

## WizardWrx.Core Namespace

*	__AgedFileInfo__ extends a `FileInfo` object with a custom `IComparable`
interface implementation. This class and `AgedFileInfoCollection` are designed
to be used together to process a list of the files that match a file
specification from newest to oldest. These objects are not intended for
independent use, since they must go into a collection that can be sorted to
leverage the `IComparable` interface implementation that permits them to be
sorted in reverse order by `LastWriteTimeUTC`. The choice of `LastWriteTimeUTC`
is by design, because UTC times are unambiguous and immune to changes in DST
transition dates. You are still free to use `LastWriteTime` on your reports and
as data, safe in the knowledge that the processing order is predictable and
correct.

*	__AgedFileInfoCollection__ hides its data in a List of `AgedFileInfo`
objects, which its constructors assemble from permutations of the array of
`FileInfo` objects returned by the `GetFiles` method on the `DirectoryInfo`
instance that is fed into it.

*	__AssemblyLocatorBase__ is an abstract class that can be used to get the fully
qualified name of the file from which the assembly in which the derived class is
defined was loaded. Given the location from which an assembly was loaded, you
can learn almost anything else you need to know about that file, such as its size,
age, and directory. Given the directory, you can locate satellite files, such as
custom configuration files. Utility methods on the base class support substituting
the name of the directory from the assembly location into file name strings to
derive the absolute name of a satellite file.

*	__BasicSystemInfoDisplayMessages__ exposes static methods that simplify
displaying information about the processor on which the calling process is
executing that is otherwise hard to determine accurately.

*	__ByteArrayFormatters__ exposes static methods that format byte arrays into
strings of hexadecimal numerals for display on reports and in windows.

*	__CmdLneArgsBasic__ efficiently processes command line switches,
named arguments, and positional arguments, in such a way that they are easily
accessible. The constructor parses the command line. Switches, named arguments,
and positional arguments may be freely mixed in any way; your users aren't
confined to specifying all switches and/or named arguments first, last, or in
any other order. While it is fairly trivial to reverse engineer the underlying
Dictionary object and read the arguments directly, the supported method of
reading them is through its methods: `GetArgByName`, `GetArgByPosition`, and
`GetSwitchByName`.

*	__PropertyDefaults__ Expose the `AppSettingsSection` associated with the
executing DLL, as opposed to settings associated with the entry assembly. The
settings exposed by this class inhabit a configuration file that is associated
with the DLL, itself, as opposed to the application configuration. Though its
format is similar to that of the application configuration file, the DLL
configuration lives in its own configuration file that has the same name as the
DLL, with an additional suffix of `.config`. The rationale behind segregating
these settings is that the affected application properties are ones that you
want to keep consistent, or nearly so, across groups of applications. Keeping
them in a dedicated configuration file that travels with the DLL that implements
them eliminates the thankless task of adding them to every application
configuration file.

*	__RegistryValues__ exposes static methods for efficiently testing for the
presence of named values in a Registry key that behave more like the `Item`
property of a collection, and retrieving Registry value types that require a
transformation of one sort or another to get them into the appropriate native
type.

*	__TimeDisplayFormatter__ exposes instance methods that return dates and times,
uniformly formatted by rules set by way of its properties.

*	__TraceLogger__ exposes methods that support every conceivable combination
of local and UTC time stamps for trace logging. Since trace listeners are
configured in the `app.config` file, there is no specific support for doing so.

*	__UnmanagedLibrary__ is a managed wrapper over the native `LoadLibrary`,
`GetProcAddress`, and `FreeLibrary` Win32 API calls. Though it was created for
internal consumption, it may as well be public, so that you or I never need to
work out the Platform Invoke call again.

## WizardWrx.DLLConfigurationManager Namespace

*	__ExceptionLogger__ exposes methods for formatting data from instances of the
`System.Exception` class, and commonly used derived classes, and displaying the
formatted data on a console (strictly speaking, on STDOUT, which can be
redirected to a file), and recording them in a Windows event log.

*	__IniFileReader__ provides a managed interface to `GetPrivateProfileString`
in the Windows API, with methods to retrieve the values of individual keys and
lists of the keys in a section or the sections in a file.

*	__StateManager__ is a Singleton that maintains run-time information about the
entry assembly, assumed to be a desktop application, that calls it into being.
Since it implements the Singleton design pattern, there is never more than one
instance.

## WizardWrx.EmbeddedTextFile Namespace

*	__ByteOrderMark__ evaluates arbitrary byte arrays for the presence of a Byte
Order Mark. This required for reading `MemoryStream` streams that are returned
as byte arrays, even when they represent text.

*	__Readers__ exposes methods for loading text from custom resources that are
embedded in an assembly. This class uses `ByteOrderMark` to safely dispose of
a leading Byte Order Mark that may appear at the beginning of the text stream.

## WizardWrx.FormatStringEngine Namespace

*	__FormatItem__ is an infrastructure class; instances represent a Format Item
found in the format string associated with the `FormatStringParser` that owns
it. The static methods of this class are public, and are handy for creating
compact summary reports to display on consoles.

*	__FormatItemsCollection__ holds the collection of `FormatItems` found in a
Format String. While the class is marked Public, everything else about it is
marked as Internal, so that only instances of classes defined in this assembly
can create a collection or add items to it, while instances of the
`FormatStringParser` class expose it as a read only property.

*	__FormatStringError__ exposes detailed information about errors in format
strings detected by a `FormatStringParser` constructor. The constructed
instance exposes a collection of these if it finds errors in the input string.

*	__FormatStringParser__ parses format control strings that you intend to use
with `string.Format` or one of the `Write` or `WriteLine` methods of a stream
object, such as a `Console` or `TextWriter`. Properties and methods report on
its attributes, including its `FormaItems`, and errors flagged by the parser.
The class is useful for evaluating format strings that are either dynamically
generated or constructed from user input.