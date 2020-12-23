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
// 2020/10/17 DAG Update the product build number to match the highest library
//                build number.
// 2020/11/02 DAG Update the product version number for the next set of point
//                releases. Hence, 7.23 becomes 7.24.
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
[assembly: AssemblyCopyright ( "Copyright © 2014-2020, David A. Gray" )]
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
[assembly: AssemblyFileVersion ( "7.24.215.0" )]