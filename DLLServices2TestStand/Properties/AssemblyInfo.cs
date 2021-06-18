using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

[assembly: AssemblyTitle ( "DLLServicesTestStand" )]
[assembly: AssemblyDescription ( "This is the test program for the product." )]
[assembly: AssemblyCulture ( "" )]

// The following GUID is for the ID of the type library if this project is exposed to COM.
[assembly: Guid ( "b381d490-4cf9-42b8-b14e-79bd07e248a0" )]

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
//
//	============================================================================
//
//  ----------------------------------------------------------------------------
//  Revision History
//  ----------------------------------------------------------------------------
//
//  2015/06/04 5.4 Incorporate P6CUtilLib1.dll into the DLL project, marked as
//                 Content to be copied if newer, so that any project that
//                 consumes this class is guaranteed access to it. Since the
//                 effects of this change are confined to the configuration of
//                 the DLL project, this is the only source file of this project
//                 that is changed.
//
//  2016/10/29 7.0 Break this library apart, so that smaller subsets of classes
//                 can be distributed and consumed independently.
//	============================================================================
[assembly: AssemblyVersion ( "8.0.1435.0" )]