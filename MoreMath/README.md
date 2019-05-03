# WizardWrx MoreMath ReadMe

The purpose of this class library is to provide other libraries and assemblies
methods that perform a variety of infrequently used, but technically obscure or
deceptively tricky mathematical computations.

To maximize compatibility with client code, the library targets version 4.5 of
the Microsoft .NET Framework, enabling it to support projects that target that
version, or any later version, of the framework. Since its implementation needs
only core features of the Base Class Library, apart from the
System.Runtime.CompilerServices features that pushed it to version 4.5, I have
yet to discover an issue in using it with any of the newer frameworks.  Though
this library doesn't directly use System.Runtime.CompilerServices, it relies
upon WizardWrx.DiagnosticInfo, which does.

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

The easiest way to incorporate the library into your own code is by installing the NuGet package at
[https://www.nuget.org/packages/WizardWrx.MoreMath](https://www.nuget.org/packages/WizardWrx.MoreMath).

## Revision History

This file is being released a few days after version 7.15 of the WizardWrx .NET
API, of which it is a component. To track changes, please see
[https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md](https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md),
which reports changes by namespace and class for each release.

At present, the GitHub repository is not updated between releases.