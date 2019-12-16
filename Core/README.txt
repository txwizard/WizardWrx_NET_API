WizardWrx Core ReadMe

Version 7.23

The code is unchanged.

This update implements two changes in the infrastructure of the WizardWrx .NET
API libraries.

1. These libraries abandon NuGet packages as their means of consuming updates
to sibling dependencies WizardWrx.Common and WizardWrx.FormatStringEngine in
favor of old-fashioned project references. Though consuming NuGet packages looks
great on paper, it proved awkward in practice because everything stayed one
release behind.

2. These libraries trade traditional 4-part version numbers for the industry
standard 3-part SemVer numbering adopted for the NuGet Repository. This scheme
improves version control by eliminating the time stamp generated for the fourth
part by the asterisk, thereby making builds detrministic.

Version 7.20

For the benefit of NuGet package subscribers, following is a summary of the
changes in this library, which are confined to the FileInfoExtensionMethods
class.

ShowFileDetails: Previous testing overlooked the case when the specified
file does not exist. This is an issue because I overlooked the fact that the
Length property on the FileInfo object throws a FileNotFoundException
Exception when the associated file does not exist, although other properties
return values of some kind, regardless of whether the file is present or
absent. Size zero is a legitimate, and fairly commonplace, length (size) of a
file, this method reports -1, which is not. This is consistent with the behavior
of the time stamp properties, which report 1600/01/01 00:00:00 when the file is
absent.

Everything else in the library is unchanged, since no other issues have
surfaced in daily development use.

Version 7.19

For the benefit of NuGet package subscribers, following is a summary of the
changes in this library, which are confined to the StringExtensions class.

|Method Name        |Method Goal                                     |
|-------------------|------------------------------------------------|
|UnixLineEndings    |Replace CR/LF pairs and bare CRs with bare LFs. |
|WindowsLineEndings |Replace bare LFs with CR/LF pairs.              |
|OldMacLineEndings  |Replace CR/LF pairs and bare LFs with bare CRs. |

Overview

The purpose of this class library is to provide other libraries and assemblies
with easy access to classes that implement core capabilities, such as a robust,
yet efficient implementation of the Singleton design pattern, efficient
generation of message digests, advanced number and date/time formatting,
registry key and value processing, extension methods for the System.String and
System.IO.FileInfo classes, settings that travel with the DLLs that use them,
advanced trace logging, etc.

To maximize compatibility with client code, the library targets version 3.5 of
the Microsoft .NET Framework, enabling it to support projects that target that
version, or any later version, of the framework. Since its implementation needs
only core features of the Base Class Library, apart from the time zone features
that pushed it to version 3.5, I have yet to discover an issue in using it with
any of the newer frameworks.

The class belongs to the WizardWrx namespace, which I created to organize the
helper libraries that I use in virtually every production assembly, regardless
of what framework version is its target, and whether its surface is a Windows
console, the Windows desktop, or the ASP.NET Web server. To date, I have used
classes and methods in these libraries in all three environments and against
virtually every version of the framework and Base Class Library.

Using the Library

Since there are no name collisions, you may safely set references to any library
that exposes objects in the the WizardWrx namespace in a project, and add a
using directive for any of its subsidiary namespaces to any source module.

This library belongs to the WizardWrx .NET API, which is documented at
https://txwizard.github.io/WizardWrx_NET_API/.

The easiest way to incorporate the library into your own code is by installing the NuGet package at
https://www.nuget.org/packages/WizardWrx.Core.

Revision History

This file is being released a few days after version 7.15 of the WizardWrx .NET
API, of which it is a component. To track changes, please see
https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md,
which reports changes by namespace and class for each release.

At present, the GitHub repository is not updated between releases.