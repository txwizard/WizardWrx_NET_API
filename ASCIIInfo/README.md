# WizardWrx ASCIIInfo ReadMe

The purpose of this class library is to provide other libraries and assemblies
with easy access to metadata about the characters that comprise the 256 standard
ASCII characters.

To maximize compatibility with client code, the library targets version 2.0 of
the Microsoft .NET Framework, enabling it to support projects that target that
version, or any later version, of the framework. Since its implementation needs
only core features of the Base Class Library, I have yet to discover an issue in
using it with any of the newer frameworks.

The class belongs to the `WizardWrx` namespace, which I created to organize the
helper libraries that I use in virtually every production assembly, regardless
of what framework version is its target, and whether its surface is a Windows
console, the Windows desktop, or the ASP.NET Web server. To date, I have used
classes and methods in these libraries in all three environments and against
virtually every version of the framework and Base Class Library.

## Using the Library

Since there are no name collisions, you may safely set references to any library
that exposes objects in the the `WizardWrx` namespace in a project, and add a
using directive for any of its subsidiary namespaces to any source module.

This library belongs to the WizardWrx .NET API, which is documented at
[https://txwizard.github.io/WizardWrx_NET_API/](https://txwizard.github.io/WizardWrx_NET_API/).

In particular:

1) Class __ASCII_Character_Display_Table__ is described at
[https://txwizard.github.io/WizardWrx_NET_API/api/WizardWrx.ASCII_Character_Display_Table.html](https://txwizard.github.io/WizardWrx_NET_API/api/WizardWrx.ASCII_Character_Display_Table.html).

2) Class __ASCIICharacterDisplayInfo__ is described at
[https://txwizard.github.io/WizardWrx_NET_API/api/WizardWrx.ASCIICharacterDisplayInfo.html](https://txwizard.github.io/WizardWrx_NET_API/api/WizardWrx.ASCIICharacterDisplayInfo.html).

The easiest way to incorporate the library into your own code is by installing the NuGet package at
[https://www.nuget.org/packages/WizardWrx.ASCIIInfo](https://www.nuget.org/packages/WizardWrx.ASCIIInfo).

# Revision History

**Note:** Going forward, every release will include a new build of every library in the WizardWrx
.NET API, so that all versions stay reasonably well synchronized. _However_, unless the code changes,
the change log will not cite every library for every release. Moreover, the release notes reported by
the NuGet Package Manager will remain static unless there is a noteworthy change that affects that
library.

The objective of this policy change is to attempt to eliminate dependency diamonds that otherwise
arise inevitably because of unavoidable cascading dependencies among the libraries at the bottom of
the call stack, such as WizardWrx.Common.dll, WizardWrx.ASCIIInfo.dll, and others.

| Date       | Version | Synopsis                                                                   |
|------------|---------|----------------------------------------------------------------------------|
| 2021/01/30 | 8.0.87  | Upgrade the API version to 8.0, and align the file versioning with the     |
|            |         | Microsoft convention that the file version remains unchanged between major |
|            |         | version icrements.                                                         |
| 2020/09/20 | 7.23.85 | This build reflects updating of the project-wide copyright year from 2019  |
|            |         | to 2020. The code is otherwise unchanged.                                  |
| 2018/10/07 | 7.23.84 | Adopt the 3-part SemVer version numbering scheme that has become the       |
|            |         | standard for NuGet packages. The code is otherwise unchanged, and the      |
|            |         | fourth part of the version number goes away.                               |
| 2018/10/07 | 7.1.83  | Override ToString to render all three representations (printable string,   |
|            |         | hexadecimal, then decimal, in that order), and define static method        |
|            |         | DisplayCharacterInfo to provide that service for an arbitrary character    |
|            |         | without instantiating ASCII_Character_Display_Table.                       |
| 2017/08/04 | 7.0     | Relocated to the constellation of core libraries that began as             |
|            |         | WizardWrx.DllServices2.dll.                                                |
| 2016/06/12 | 3.0     | 1) Break the dependency on WizardWrx.SharedUtl2.dll, correct misspelled    |
|            |         | words flagged by the spelling checker add-in, and incorporate my           |
|            |         | three-clause BSD license.                                                  |
|            |         | 2) Add a Comment property, to support a like named node in the XML         |
|            |         | document tree.                                                             |
| 2014/07/19 | 1.0     | Initial implementation.                                                    |
