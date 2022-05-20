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
//  License:            Copyright ( C ) 2011 - 2022, David A.Gray.
//                      All rights reserved.
//
//                      Redistribution and use in source and binary forms , with
//                      or without modification , are permitted provided that
//                      the following conditions are met:
//
//                      *   Redistributions of source code must retain the above
//                          copyright notice , this list of conditions and the
//                          following disclaimer.
//
//                      *   Redistributions in binary form must reproduce the
//                          above copyright notice , this list of conditions and
//                          the following disclaimer in the documentation and/or
//                          other materials provided with the distribution.
//
//                      *   Neither the name of David A.Gray, nor the names of
//                          his contributors may be used to endorse or promote
//                          products derived from this software without specific
//                          prior written permission.
//
//                      THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
//                      CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
//                      WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//                      WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
//                      PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
//                      David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
//                      INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//                      (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
//                      SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//                      PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
//                      ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
//                      LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//                      ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
//                      IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
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
//                Hence, 7.24 becomes 8.0. Going forward, AssemblyFileVersion
//                shall remain unchanged unless the API undergoes another major
//                revision, which seems unlikely.
//
//                However, the AssemblyVersion attribute of each library shall
//                continue to inch upwards. All build numbers will increment
//                from the last valuses they had in the previous major version,
//                and each release will include each library, regardless of
//                whether the build includes relevant changes. If README.MD and
//                the NuGet package file are unchanged, you are safe in assuming
//                that the code is unchanged, and the new library is backwards
//                compatible with its predecessors.
//
// 2021/03/21 DAG 1) Correct typographical errors in these comments, and add a
//                   note immediately above the AssemblyFileVersion key to
//                   remind myself that its value is frozen.
//
//                2) Incorporate my three-clause BSD license into this file.
//
// 2022/05/19 DAG Update the terminal copyright year.
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
[assembly: AssemblyCopyright ( "Copyright © 2014-2022, David A. Gray" )]
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
//
// IMPORTANT MAINTENANCE NOTE: Henceforth, the AssemblyFileVersion property is
//                             frozen at its current value, 8.0.0.0.
//
//                             AssemblyVersion shall continue to track build
//                             numbers, and all major+minor assembly version
//                             numbers shall be at least 8.0.
[assembly: AssemblyFileVersion ( "8.0.0.0" )]