WizardWrx .NET API ReadMe
=========================

The purpose of these class libraries is to expedite development of new
applications that target any version of the Microsoft .NET Framework, from
version 2.0 up. The classes in these libraries define numerous constants, most
assigned to the base WizardWrx namespace, and utility classes, organized into
subsidiary namespaces.

The following sections summarize the classes in each namespace. Unless indicated
otherwise, each class is exposed by a library of the same name as the namespace.
To use them, add a reference to the indicated library, and add a using directive
that specifies the corresponding namespace.

Since there are no name collisions, you may safely set references to all 6
namespaces in the same source module.

WizardWrx: The Root Namespace
-----------------------------

These classes are defined in `WizardWrx.Common.dll`.

*	 __CSVFileInfo__ defines constants for working with CSV style files.

*	 __DisplayFormats__  defines conostants that express standard format string for
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

*	__MagicNumbers__ defines constants for commonly used magic numbers. Others are
defined in companion class ArrayInfo, while SpecialCharacters defines character
representations of the visually ambiguous numbers and letters for use in place
of literals.

*	__NumericFormats__ defines standard numeric format strings, for use with the
string.format method and its derivatives and relatives.

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
provided by the System.String class. Most, but not all, have been superseded by
extension methods, but they remain for backwards compatibility with internal
consumers that have yet to be updated.

WizardWrx.ConsoleStreams Namespace
----------------------------------

*	__DefaultErrorMessageColors__ expose the default fatal and nonfatal exception
message colors, which are defined in a standard application configuration file
that is linked to the assembly that defines this class.

*	__DigestFile__ exposes static methods for computing message digests for files,
using the most common algorithms. (This class is defined in `WizardWrx.Core__.dll`.)

*	__DigestString__ exposes static methods for computing message digests for
strings, using the most common algorithms. (This class is defined in
`WizardWrx.Core__.dll`.)

*	__FileInfoExtensionMethods__ extends the System.IO.FileInfo class with methods
for testing, setting, and clearing file attribute flags, including saving and
restoring flags to their initial states. (This class is defined in
`WizardWrx.Core__.dll`.)

*	__FileNameTricks__ exposes numerous static methods for manipulating file names.
Unlike the objects in the System.File namespace, these methods don't need a real
file object. All work on strings that represent file names. (This class is
defined in `WizardWrx.Core__.dll`.)

*	__GenericSingletonBase<T>__ is a complete implementation of the Singleton
design pattern that takes advantage of a guarantee made by the framework that it
won't bother to call a static constructor on a class until its first use, and
that a static constructor is never called more than once, no matter how many
subsequent references to the class occur. (This class is defined in
`WizardWrx.Core__.dll`.)

*	__ListHelpers__ exposes methods for merging sorted lists of items, and to
simplify working with the values returned through the IComparable interface.
(This class is defined in `WizardWrx.Core__.dll`.)

*	__ErrorMessagesInColor__ exposes Console.Error.Write and
Console.Error.WriteLine methods that write in living color. Use instance
methods as drop-in replacements for Console.Error.Write and
Console.Error.WriteLine.

*	__MessageInColor__ expose a combination of static and instance methods that
reduce writing console messages in any supported combination of foreground and
background colors to method calls that mirror the Console.Write and
Console.WriteLine methods that you already know and use.

*	__StandardHandleInfo__ is a static class that provides type-safe managed
methods to determine the true redirection state of a standard console handle,
including, if applicable, learning the name of the file to which it is
redirected.

*	__NumberFormatters__ exposes methods to further simplify formatting of
integers, floating point (single precision real numbers) and double precision
real numbers. For integers, these methods cover both decimal and the most
frequently used hexadecimal representations, spanning from two through sixteen
hexadecimal glyphs. (This class is defined in `WizardWrx.Core__.dll`.)

*	__ReportDetail__ instances represent generic report details, with labels and
formats for printing them. (This class is defined in `WizardWrx.Core__.dll`.)

*	__ReportHelpers__ exposes methods to help prepare strings for use on reports.
(This class is defined in `WizardWrx.Core__.dll`.)

*	__StringExtensions__ is a class of extension methods for performing common
tasks not provided by the System.String class. All but the four Pad methods are
derived from long established routines in companion class StringTricks.
(This class is defined in `WizardWrx.Core__.dll`.)

*	__SyncRoot__ provide classes that must be made thread-safe with locks over
which the class has complete control. (This class is defined in
`WizardWrx.Core__.dll`.)

*	__SysDateFormatters__ implements my stalwart date and time formatter,
ReformatSysDateP6C, which I created initially as a Windows Interface Language
(WIL, a. k. a. WinBatch) library function, Reformat_Date_YmdHms_P6C, in October
2001, although its roots go back much further in my WIL script development.
(This class is defined in `WizardWrx.Core__.dll`.)

*	__TextBlocks__ exposes methods for creating and manipulating test blocks.
(This class is defined in `WizardWrx.Core__.dll`.)

WizardWrx.Core Namespace
------------------------

*	__AgedFileInfo__ extends a FileInfo object with a custom IComparable interface
implementation. This class and AgedFileInfoCollection are designed to be used
together to achieve the goal of processing a list of the files that match a file
specification from newest to oldest. These objects are not intended for
independent use, since they must go into a collection that can be sorted to
leverage the IComparable interface implementation that permits them to be sorted
in reverse order by LastWriteTimeUTC.

*	__AgedFileInfoCollection__ hides its data in a List of AgedFileInfo objects,
which its constructors assemble from permutations of the array of FileInfo
objects returned by the GetFiles method on the DirectoryInfo instance that is
fed into it.

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
reading them is through its methods: GetArgByName, GetArgByPosition, and
GetSwitchByName.

*	__PropertyDefaults__ Expose the AppSettingsSection associated with this DLL,
as opposed to settings associated with the entry assembly. The settings exposed
by this class inhabit a configuration file that is associated with the DLL,
itself, as opposed to the application configuration. Though its format is
similar to that of the application configuration file, the DLL configuration
lives in its own configuration file that has the same name as the DLL, with an
additional suffix of .config. The rationale behind segregating these settings is
that the affected application properties are ones that you want to keep
consistent, or nearly so, across large groups of applications. Keeping them in a
dedicated configuration file that travels with the DLL that implements them
eliminates the thankless task of adding them to every application configuration
file.

*	__RegistryValues__ exposes static methods for efficiently testing for the
presence of named values in a Registry key that behave more like the Item
property of a collection, and retrieving Registry value types that require a
transformation of one sort or another to get them into the appropriate native
type.

*	__TimeDisplayFormatter__ exposes instance methods that return dates and times,
uniformly formatted by rules set by way of its properties.

*	__TraceLogger__ exposes methods that support every conceivable combination of
local and UTC time stamps for trace logging.

*	__UnmanagedLibrary__ is a managed wrapper over the native LoadLibrary,
GetProcAddress, and FreeLibrary calls.

WizardWrx.DLLConfigurationManager Namespace
-------------------------------------------

*	*ExceptionLogger*exposes methods for formatting data from instances of the
System.Exception class, and commonly used derived classes, and displaying the
formatted data on a console (strictly speaking, on STDOUT, which can be
redirected to a file), and recording them in a Windows event log.

*	__IniFileReader__ provides a managed interface to GetPrivateProfileString in
the Windows API, with methods to retrieve the values of individual keys and
lists of the keys in a section or the sections in a file.

*	__StateManager__ is a Singleton that maintains run-time information about the
executing assembly, assumed to be a desktop application, that calls it into
being. Since it implements the Singleton design pattern, there is never more
than one instance.

WizardWrx.EmbeddedTextFile Namespace
------------------------------------

*	__ByteOrderMark__ evaluates arbitrary byte arrays for the presence of a Byte
Order Mark.

*	__Readers__ exposes methods for loading text from custom resources that are
embedded in an assembly. This class uses __ByteOrderMark__  to safely dispose of
a leading Byte Order Mark that may open the text stream.

WizardWrx.FormatStringEngine Namespace
--------------------------------------

*	__FormatItem__ is an infrastructure class; instances represent a FormatItem
found in the FormatString associated with the FormatStringParser that owns it.
The static methods of this class are public, and are very handy for creating
compact summary reports.

*	__FormatItemsCollection__ holds the collection of FormatItems found in a
FormatString. While the class, itself, is marked Public, everything else about
it is marked as Internal, so that only instances of classes defined in this
assembly can create a collection or add items to it, while instances of the
FormatStringParser class expose it as a read only property.

*	__FormatStringError__ exposes detailed information about errors in format
strings detected by a __FormatStringParser__  constructor. The constructed
instance exposes a collection of these if it finds errors in the input string.

*	__FormatStringParser__ parses format control strings that you intend to use
with string.Format or one of the Write or WriteLine methods of a stream object,
such as a Console or TextWriter. Properties and methods report on its
attributes, including its FormaItems, and errors flagged by the parser.