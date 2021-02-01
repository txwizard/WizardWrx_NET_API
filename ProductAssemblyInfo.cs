using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// =============================================================================
//
// File Name:			ProductAssemblyInfo.cs
//
// File Goal:			This file, which has solution scope, and lives in the
//						root directory of the solution, specifies attributes
//						that are intended to be common to all libraries in the
//						solution.
//
// File Author:			David A. Gray
//
// File Remarks:		This file contains a subset of the attributes usually
//						specified in AssemblyInfo.cs. That subset consists of
//						the attributes that apply to all assemblies in the
//						solution.
//
// -----------------------------------------------------------------------------
// Revision History
// -----------------------------------------------------------------------------
//
// Date       By  Change Note
// ---------- --- --------------------------------------------------------------
// 2020/09/20 DAG Update the copyright year.
//
// 2020/10/17 DAG Update the product build number to match the highest library
//                build number.
//
// 2020/11/02 DAG Update the product version number for the next set of point
//                releases. Hence, 7.23 becomes 7.24.
//
// 2021/01/30 DAG Update the product version number for the next major version.
//                releases. Hence, 7.24 becomes 8.0. Going forward, the
//                AssemblyFileVersion shall remain unchanged unless the API
//                undergoes another major revision, which seems unlikely.
//                However, the AssemblyVersion attribute of each library shall
//                continue to inch upwards. All build numbers will increment
//                from the last valuses they had in the previous major version,
//                and each release will include each library, regardless of
//                whether the build includes relevant changes. If README.MD and
//                the NuGet package file are unchanged, you are safe in assuming
//                that the code is unchanged, and the new library is backwards
//                compatible with its predecessors.
// =============================================================================

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

// The following properties are specified in AssemblyInfo.cs in individual
// projects:
//
//		AssemblyTitle
//		AssemblyDescription
//		Guid

[assembly: AssemblyConfiguration ( "" )]
[assembly: AssemblyCompany ( "David A. Gray" )]
[assembly: AssemblyProduct ( "Common Classes for the Microsoft .NET Framework 3.5 and Above" )]
[assembly: AssemblyCopyright ( "Copyright © 2014-2021, David A. Gray" )]
[assembly: AssemblyTrademark ( "This library is distributed under a three-clause BSD license." )]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible ( false )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion ( "8.0.0.0" )]