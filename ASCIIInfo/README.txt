WizardWrx ASCIIInfo ReadMe

The purpose of this class library is to provide other libraries and assemblies
with easy access to metadata about the characters that comprise the 256 standard
ASCII characters.

To maximize compatibility with client code, the library targets version 2.0 of
the Microsoft .NET Framework, enabling it to support projects that target that
version, or any later version, of the framework. Since its implementation needs
only core features of the Base Class Library, I have yet to discover an issue in
using it with any of the newer frameworks.

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

In particular:

1) Class ASCII_Character_Display_Table is described at
https://txwizard.github.io/WizardWrx_NET_API/api/WizardWrx.ASCII_Character_Display_Table.html.

2) Class ASCIICharacterDisplayInfo is described at
https://txwizard.github.io/WizardWrx_NET_API/api/WizardWrx.ASCIICharacterDisplayInfo.html.

The easiest way to incorporate the library into your own code is by installing the NuGet package at
https://www.nuget.org/packages/WizardWrx.ASCIIInfo.

Revision History

+---------------------------------------------------------------------------------------------------+
| Date       | Version | Synopsis                                                                   |
|------------|---------|----------------------------------------------------------------------------|
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
|            |         |    words flagged by the spelling checker add-in, and incorporate my        |
|            |         |    three-clause BSD license.                                               |
|            |         | 2) Add a Comment property, to support a like named node in the XML         |
|            |         |    document tree.                                                          |
| 2014/07/19 | 1.0     | Initial implementation.                                                    |
+---------------------------------------------------------------------------------------------------+
