# WizardWrx Common ReadMe

The purpose of this class library is to provide other libraries and assemblies
with easy access to assorted common constants, such as ambiguous chraacters,
e., g., numeric zero versus letter "O", numeric one versus lower case "l", and a
handful of helper functions for converting between subscripts and ordinals when
processing arrays and lists.

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

The easiest way to incorporate the library into your own code is by installing the NuGet package at
[https://www.nuget.org/packages/WizardWrx.Common](https://www.nuget.org/packages/WizardWrx.Common).

## Revision History

**Note:** Going forward, every release will include a new build of every library in the WizardWrx
.NET API, so that all versions stay reasonably well synchronized. _However_, unless the code changes,
the change log will not cite every library for every release. Moreover, the release notes reported by
the NuGet Package Manager will remain static unless there is a noteworthy change that affects that
library.

The objective of this policy change is to attempt to eliminate dependency diamonds that otherwise
arise inevitably because of unavoidable cascading dependencies among the libraries at the bottom of
the call stack, such as WizardWrx.Common.dll, WizardWrx.ASCIIInfo.dll, and others.

At present, the GitHub repository is not updated between releases. However, changes tend to be developed
and released quickly when the need for a change becomes evident.

| Date       | Version | Synopsis                                                                   |
|------------|---------|----------------------------------------------------------------------------|
| 2020/09/20 | 7.23.85 | This build reflects updating of the project-wide copyright year from 2019  |
|            |         | to 2020. The code is otherwise unchanged.                                  |

This file is being released a few days after version 7.15 of the WizardWrx .NET
API, of which it is a component. To track library-wide changes, please see
[https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md](https://github.com/txwizard/WizardWrx_NET_API/blob/master/ChangeLog.md),
which reports changes by namespace and class for each release. At present, the
GitHub repository is not updated between releases.