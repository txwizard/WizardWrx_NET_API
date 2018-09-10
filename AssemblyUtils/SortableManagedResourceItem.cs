/*
    ============================================================================

    Namespace:          WizardWrx.AssemblyUtils

    Class Name:         SortableManagedResourceItem

    File Name:          SortableManagedResourceItem.cs

    Synopsis:           This small class extends the item returned by a managed
                        resource reader with an implementation of IComparable,
						so that it can be sorted.

    Remarks:            This class is required because the Resource Manager
						must be enumerated the hard way, and its underlying
						dictionary is opaque.
 
						This class originated as a proof of concept in the
						test stand program.

    Author:             David A. Gray

	License:            Copyright (C) 2017, David A. Gray. 
						All rights reserved.

                        Redistribution and use in source and binary forms, with
                        or without modification, are permitted provided that the
                        following conditions are met:

                        *   Redistributions of source code must retain the above
                            copyright notice, this list of conditions and the
                            following disclaimer.

                        *   Redistributions in binary form must reproduce the
                            above copyright notice, this list of conditions and
                            the following disclaimer in the documentation and/or
                            other materials provided with the distribution.

                        *   Neither the name of David A. Gray, nor the names of
                            his contributors may be used to endorse or promote
                            products derived from this software without specific
                            prior written permission.

                        THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
                        CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
                        WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
                        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
                        PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
                        David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
                        INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
                        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
                        SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
                        PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
                        ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
                        LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
                        ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
                        IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    Created:            Sunday, 14 September 2014 and Monday, 15 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2017/07/19 7.0     DAG    This class makes its debut.
    ============================================================================
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;


namespace WizardWrx.AssemblyUtils
{
	/// <summary>
	/// Instances of this class represent arbitrary managed resource items.
	/// </summary>
	/// <remarks>
	/// This class is necessary because the public dictionary is opaque, so a
	/// consumer has no control over the order in which they are returned. Since
	/// this wrapper implements the IComparable interface, collections of these
	/// objects can be sorted.
	/// </remarks>
	public class SortableManagedResourceItem : IComparable<SortableManagedResourceItem>
	{
		#region Constructors
		/// <summary>
		/// Other than satisfying the requirements of the IList interface, the
		/// uninitialized object is useless.
		/// </summary>
		public SortableManagedResourceItem ( )
		{
		}	// SortableManagedResourceItem constructor (1 of 2)


		/// <summary>
		/// This constructor creates a fully initialized instance from the data
		/// in the Current property of the IDictionaryEnumerator object returned
		/// by a ResourceSet instance.
		/// </summary>
		/// <param name="pstrName">
		/// Set the Name to the string returned by the Key property of the
		/// enumerator.
		/// </param>
		/// <param name="pobjValue">
		/// Set the Value to the System.object returned by the Value property of
		/// the enumerator.
		/// </param>
		public SortableManagedResourceItem (
			string pstrName ,
			object pobjValue )
		{
			if ( string.IsNullOrEmpty ( pstrName ) )
			{
				throw new ArgumentNullException (
					"pstrName" ,
					WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL_OR_EMPTY );
			}	// TRUE (unanticipated outcome) block, if ( string.IsNullOrEmpty ( pstrName ) )
			else if ( pobjValue == null )
			{
				throw new ArgumentNullException (
					"pobjValue" ,
					WizardWrx.Common.Properties.Resources.ERRMSG_ARG_IS_NULL );
			}	// TRUE (unanticipated outcome) block, else if ( pobjValue == null )
			else
			{
				_strName = pstrName;
				_objValue = pobjValue;
			}	// FALSE (anticipated outcome) block, else if ( pobjValue == null )
		}	// SortableManagedResourceItem constructor (2 of 2)
		#endregion	// Constructors


		#region public Read-Only Properties
		/// <summary>
		/// The Name is the Name shown in the property sheet grid into which 
		/// garden variety managed string resources are input.
		/// </summary>
		public string Name
		{
			get { return _strName; }
		}	// Name property


		/// <summary>
		/// The ResourceValue property is the Value property shown in the
		/// property sheet grid into which garden variety managed string
		/// resources are input, and its type is usually System.string.
		/// </summary>
		public object Value
		{
			get { return _objValue; }
		}	// Value property


		/// <summary>
		/// Since the full type specification is accessible through the Value
		/// property, and the string representation of the FullName is its most
		/// useful property, it gets its own read only property, which also
		/// returns just "string" for the common case, for which the prefix is
		/// redundant.
		/// </summary>
		public string TypeName
		{
			get
			{
				string ValueTypeName = _objValue.GetType ( ).FullName;

				if ( ValueTypeName == "System.String" )
					return "String";
				else
					return ValueTypeName;
			}	// TypeName property get method
		}	// TypeName property
		#endregion	// public Read-Only Properties


		#region Instance Storage
		string _strName;
		object _objValue;
		#endregion	// Instance Storage


		#region IComparable<SortableManagedResourceItem> Members
		/// <summary>
		/// Making the comparison based on the Name property permits sorting the
		/// resources in the most logical order.
		/// </summary>
		/// <param name="other">
		/// The explicit implementation puts the burden of enforcing type safety
		/// on the runtime system.
		/// </param>
		/// <returns>
		/// Since it is a pass-through of another CompareTo method, the return
		/// type is guaranteed to adhere to the interface specification.
		/// </returns>
		int IComparable<SortableManagedResourceItem>.CompareTo (
			SortableManagedResourceItem other )
		{
			return _strName.CompareTo ( other._strName );
		}	// CompareTo method
		#endregion	// IComparable<SortableManagedResourceItem> Members


		#region Static Methods
		/// <summary>
		/// Use the list of Manifest Resource Names returned by method
		/// GetManifestResourceNames on a specified assembly. Each of several
		/// methods employs a different mechanism to identify the assembly of
		/// interest.
		/// </summary>
		/// <param name="pstrResourceName">
		/// Specify the name of the file from which the embedded resource was
		/// created. Typically, this will be the local name of the file in the
		/// source code tree.
		/// </param>
		/// <param name="pasmSource">
		/// Pass a reference to the Assembly that is supposed to contain the
		/// desired resource.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the internal name of
		/// the requested resource, which is fed to GetManifestResourceStream on
		/// the same assembly, which returns a read-only Stream backed by the
		/// embedded resource. If the specified resource is not found, it
		/// returns null.
		/// </returns>
		/// <remarks>
		/// Since I cannot imagine any use for this method beyond its
		/// infrastructure role in this class, I marked it private.
		/// </remarks>
		public static string GetInternalResourceName (
			string pstrResourceName ,
			Assembly pasmSource )
		{
			foreach ( string strManifestResourceName in pasmSource.GetManifestResourceNames ( ) )
				if ( strManifestResourceName.EndsWith ( pstrResourceName ) )
					return strManifestResourceName;

			return null;
		}   // private static string GetInternalResourceName

		/// <summary>
		/// Call this static method from a console program to list the resources
		/// defined in an assembly alphabetically by name.
		/// </summary>
		/// <param name="pasmInWhichEmbedded">
		/// Specify the assembly that contains the resources to be enumerated.
		/// </param>
		/// <remarks>
		/// This class creates and consumes a generic List of instances of the
		/// class that hosts it, and uses string padding to vertically align the
		/// list without resorting to composite format items.
		/// </remarks>
		public static void ListResourcesInAssemblyByName (
			Assembly pasmInWhichEmbedded )
		{
			const string DEFAULT_STRING_RESOURCE_NAME = @"resources";

			Console.WriteLine (
				Properties.Resources.MSG_RESOURCE_LIST_FQNAME ,
				pasmInWhichEmbedded.FullName ,
				Environment.NewLine );
			Console.WriteLine (
				Properties.Resources.MSG_RESOURCE_LIST_BASENAME ,
				pasmInWhichEmbedded.Location );

			//	----------------------------------------------------------------
			//	Since they own unmanaged resources, ResourceSet are disposable.
			//	Moreover, they must be enumerated (ForEach is unsupported.), and
			//	they can't be sorted in place. Thankfully, it's fairly easy to
			//	make copies of the parts that matter to us, sort them, and show
			//	them.
			//	----------------------------------------------------------------

			int intMaxNameLength = MagicNumbers.ZERO;
			int intMaxTypeNameLength = MagicNumbers.ZERO;

			List<SortableManagedResourceItem> lstResources = new List<SortableManagedResourceItem> ( );

			//using ( ResourceReader resourceReader = System.Reflection.Assembly.GetAssembly( presAssemblyResources.GetType()).GetManifestResourceStream(presAssemblyResources.GetType()))
			//	CultureInfo.InvariantCulture ,
			//	LOAD_IF_NECESSARY ,
			//	TRY_PARENTS_IF_NECESSARY ) )
			//{
			//	IDictionaryEnumerator resourceEnumerator = resourceReader.GetEnumerator ( );

			//	while ( resourceEnumerator.MoveNext ( ) )
			//	{
			//		string strName = resourceEnumerator.Key.ToString ( );

			//		if ( strName.Length > intMaxNameLength )
			//		{	// Update length if longer than any yet seen.
			//			intMaxNameLength = strName.Length;
			//		}	// if ( strName.Length > intMaxNameLength )

			//		SortableManagedResourceItem resourceItem = new SortableManagedResourceItem (
			//			strName ,
			//			resourceEnumerator.Value );

			//		if ( resourceItem.TypeName.Length > intMaxTypeNameLength )
			//		{
			//			intMaxTypeNameLength = resourceItem.TypeName.Length;
			//		}	// if ( item.TypeName.Length > intMaxTypeNameLength )

			//		lstResources.Add ( resourceItem );
			//	}	// while ( resourceEnumerator.MoveNext ( ) )
			//}	// using ( System.Resources.ResourceSet resourceSet = presAssemblyResources.GetResourceSet ( System.Globalization.CultureInfo.InvariantCulture , LOAD_IF_NECESSARY , LOAD_IF_NECESSARY ) )

			//	----------------------------------------------------------------
			//	The list of SortableManagedResourceItem items is complete. Get a
			//	count, initialize an item counter, and sort the list, then use a
			//	standard ForEach loop to display them in order by name. The next
			//	improvement is to apply dynamic formatting to the item numbers
			//	and names.
			//	----------------------------------------------------------------

			string [ ] astrNamedResources = pasmInWhichEmbedded.GetManifestResourceNames ( );

			Console.WriteLine (
				Properties.Resources.MSG_RESOURCE_LIST_ITEM_COUNT ,									// Format control string
				astrNamedResources.Length ,															// Format Item 0 = Item count
				Environment.NewLine );																// Format Item 1 = Extra newline

			int intItemNumber = ListInfo.LIST_IS_EMPTY;
			int intItemNumberWidth = astrNamedResources.Length.ToString ( ).Length;

			foreach ( string strName in astrNamedResources )
			{	// Though most assemblies contain but a single resource, named "resources," they may contain an unlimited number of them. I have at least one assembly that contains three.
				Console.WriteLine (
					Properties.Resources.MSG_RESOURCE_LIST_NAMED_ITEM ,
					( ++intItemNumber ).ToString ( ).PadRight ( intItemNumberWidth ) ,
					strName );
			}	// foreach ( string strName in astrNamedResources)

			Console.WriteLine (
				Properties.Resources.MSG_RESOURCE_LIST_NAMES_END ,
				Environment.NewLine );

			if ( astrNamedResources.Length > ListInfo.LIST_IS_EMPTY )
			{
				using ( Stream strOfResources = pasmInWhichEmbedded.GetManifestResourceStream ( GetInternalResourceName ( DEFAULT_STRING_RESOURCE_NAME , pasmInWhichEmbedded ) ) )
				{
					if ( strOfResources != null )
					{
						using ( ResourceReader resReader4Embedded = new ResourceReader ( strOfResources ) )
						{
							IDictionaryEnumerator resourceEnumerator = resReader4Embedded.GetEnumerator ( );

							while ( resourceEnumerator.MoveNext ( ) )
							{
								string strName = resourceEnumerator.Key.ToString ( );

								if ( strName.Length > intMaxNameLength )
								{	// Update length if longer than any yet seen.
									intMaxNameLength = strName.Length;
								}	// if ( strName.Length > intMaxNameLength )

								SortableManagedResourceItem resourceItem = new SortableManagedResourceItem (
									strName ,
									resourceEnumerator.Value );

								if ( resourceItem.TypeName.Length > intMaxTypeNameLength )
								{
									intMaxTypeNameLength = resourceItem.TypeName.Length;
								}	// if ( item.TypeName.Length > intMaxTypeNameLength )

								lstResources.Add ( resourceItem );
							}	// while ( resourceEnumerator.MoveNext ( ) )
						}	// using ( ResourceReader resReader4Embedded = new ResourceReader ( strOfResources ) )
					}	// TRUE (anticipated outcome) block, if ( strOfResources != null )
					else
					{	// There is nothing to report about string resources, because there aren't any.
						Console.WriteLine ( Properties.Resources.MSG_RESOURCE_LIST_NO_STRINGS );
					}	// FALSE (unanticipated outcome) block, if ( strOfResources != null )
				}	// using ( Stream strOfResources = pasmInWhichEmbedded.GetManifestResourceStream ( GetInternalResourceName ( DEFAULT_STRING_RESOURCE_NAME , pasmInWhichEmbedded ) ) )
			}	// TRUE (anticipated outcome) block, if ( astrNamedResources.Length > ListInfo.LIST_IS_EMPTY )
			else
			{	// The assembly is completely devoid of embedded resources of any kind, except the obligatory manifest.
				Console.WriteLine ( Properties.Resources.MSG_RESOURCE_LIST_NONE );
			}	// FALSE (unanticipated outcome) block, if ( astrNamedResources.Length > ListInfo.LIST_IS_EMPTY )

			int intItemCount = lstResources.Count;

			intItemNumberWidth = intItemCount.ToString ( ).Length;
			intItemNumber = ListInfo.LIST_IS_EMPTY;

			lstResources.Sort ( );

			foreach ( SortableManagedResourceItem resourceItem in lstResources )
			{	// Padding to maximum width eliminates the need for composite format items.
				Console.WriteLine (
					Properties.Resources.MSG_RESOURCE_LIST_ITEM_DETAIL ,							// Format control string
					new string [ ]
					{
						resourceItem.TypeName.PadRight(intMaxTypeNameLength) ,						// Format Item 0 = Item type
						( ++intItemNumber ).ToString ( ).PadLeft ( intItemNumberWidth ) ,			// Format Item 1 = Item number, left padded
						resourceItem.Name.PadRight ( intMaxNameLength ) ,							// Format Item 2 = Name, right padded
						resourceItem.Value.ToString ( ) ,											// Format Item 3 = Value, as is
					} );
			}	// foreach ( SortableManagedResourceItem resourceItem in lstResources )
		}	// ListResourcesInAssemblyByName
		#endregion	// Static Methods
	}	// public class SortableManagedResourceItem
}	// partial namespace WizardWrx.AssemblyUtils