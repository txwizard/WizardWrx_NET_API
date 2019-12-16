# WizardWrx FormatStringEngine ReadMe

The purpose of this class library is to provide other libraries and assemblies
with tools for parsing and validating composite formatting strings intended for
use with string.Format, Console.WriteLine, and their relatives.

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

## Version 7.23.167, released 2019/12/15

The only difference between this version and its immediate predecessor is that
it adheres to the SemVer version numbering scheme that is generally used for
NuGet packages.

## Using the Library

Since there are no name collisions, you may safely set references to any library
that exposes objects in the the `WizardWrx` namespace in a project, and add a
using directive for any of its subsidiary namespaces to any source module.

This library belongs to the WizardWrx .NET API, which is documented at
[https://txwizard.github.io/WizardWrx_NET_API/](https://txwizard.github.io/WizardWrx_NET_API/).

The easiest way to incorporate the library into your own code is by installing the NuGet package at
[https://www.nuget.org/packages/WizardWrx.FormatStringEngine](https://www.nuget.org/packages/WizardWrx.FormatStringEngine).

# Revision History

This file is being released a few days after version 7.15 of the WizardWrx .NET
API, of which it is a component. To track changes, please see
[https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md](https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md),
which reports changes by namespace and class for each release. At present, the
GitHub repository is not updated between releases.