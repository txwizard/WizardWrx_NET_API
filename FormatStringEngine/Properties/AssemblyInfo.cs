using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//
//  ----------------------------------------------------------------------------
//  Project Revision History
//  ----------------------------------------------------------------------------
//  Date       By  Remark
//  ---------- --- -------------------------------------------------------------
//  2019/07/05 DAG Re-build to resolve diamond dependencies on WizardWrx.Common
//                 and WizardWrx.AnyCSV. This build also falls back to wild card
//                 revision number assignment, which conflicts with the new
//                 Deterministic project build setting, which I already
//                 disabled. See DiagnosticInfo.csproj for the commented-out
//                 setting.
//
//  2019/11/17 DAG Re-build to resolve binding conflicts involving signed
//                 assemblies.
//
//  2019/12/01 DAG Rebuild without reference to any NuGet package that is part
//                 of the same Visual Studio solution.
//
//  2020/09/20 DAG This build reflects updating the package copyright year from
//                 2019 to 2020, which affects all assemblies in the solution.
// =============================================================================

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
// The following items are specified in ../ProductAssemblyInfo.cs, which is
// linked into every project in the solution of which this project is a part.
//
//		AssemblyCompany
//		AssemblyProduct
//		AssemblyCopyright
//		AssemblyTrademark
//		AssemblyConfiguration

[assembly: AssemblyTitle ( "WizardWrx Format String Parsing Engine" )]
[assembly: AssemblyDescription ( "Composite Format String Processing" )]
[assembly: AssemblyCulture ( "" )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid ( "f7e968d4-764a-454c-bebc-3831c0fa88fc" )]

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
[assembly: AssemblyVersion ( "8.0.234.0" )]