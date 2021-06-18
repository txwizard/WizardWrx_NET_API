using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//
//	----------------------------------------------------------------------------
//	Revision History
//	----------------------------------------------------------------------------
//
//	Date       By  Comment
//	---------- --- -------------------------------------------------------------
// 	2019/12/01 DAG Adopt SemVer version numbering scheme.
//  2020/09/20 DAG This build reflects updating the package copyright year from
//                 2019 to 2020, which affects all assemblies in the solution.
//  2020/10/17 DAG Implement SemVer version numbering.
// 	============================================================================

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
// The following items are specifieed in ../ProductAssemblyInfo.cs, which is
// linked into every project in the solution of which this project is a part.
//
//		AssemblyCompany
//		AssemblyProduct
//		AssemblyCopyright
//		AssemblyTrademark
//		AssemblyConfiguration

[assembly: AssemblyTitle ( "WizardWrx.EmbeddedTextFile" )]
[assembly: AssemblyDescription ( "Read text files that are embedded in an assembly, and test arbitrary byte arrays for the presence of a Byte Order Mark." )]
[assembly: AssemblyCulture ( "" )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid ( "f760e8dc-4ed4-438a-b728-6f7ded7a6034" )]

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
[assembly: AssemblyVersion ( "8.0.150.0" )]