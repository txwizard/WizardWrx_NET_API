using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//
//  ----------------------------------------------------------------------------
//  Project Revision History
//  ----------------------------------------------------------------------------
//
//  Date       By  Remark
//  ---------- --- -------------------------------------------------------------
//  2019/07/04 DAG Re-build to resolve diamond dependencies on WizardWrx.Common
//                 and WizardWrx.AnyCSV. This build also falls back to wild card
//                 revision number assignment, which conflicts with the new
//                 Deterministic project build setting, which I already disabled.
//                 See DiagnosticInfo.csproj for the commented-out setting.
//
// 	2019/12/01 DAG Adopt SemVer version numbering scheme.
//
//  2020/09/20 DAG This build reflects updating the package copyright year from
//                 2019 to 2020, which affects all assemblies in the solution.
// 	============================================================================

// =============================================================================

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle ( "WizardWrx.DiagnosticInfo" )]
[assembly: AssemblyDescription ( "Helper Classes to supply runtime diagnostic information about calling routines without using Reflection" )]
[assembly: AssemblyCulture ( "" )]


// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid ( "a248e5a4-b42b-4b10-8f52-e58b06a0bc18" )]

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
[assembly: AssemblyVersion ( "8.0.32.0" )]