using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WizardWrx
{
	/// <summary>
	/// Provides a centralized, thread-safe registry of <see cref="SyncRoot"/>
	/// objects keyed by symbolic names. Each key maps to exactly one
	/// synchronization object for the lifetime of the application domain.
	/// </summary>
	/// <remarks>
	/// The canonical key format for typed overloads is:
	/// 
	///   {AssemblyQualifiedTypeName}.{MethodName}
	///
	/// Use the typed overloads to get strict metadata validation. The raw-key
	/// overload is provided for advanced scenarios where the caller constructs
	/// a collision-proof key manually.
	/// </remarks>
	public static class AppDomainSyncRootRegistry
	{
		/// <summary>
		/// Internal entry that associates a <see cref="SyncRoot"/> with the
		/// metadata used to construct its key.
		/// </summary>
		private sealed class SyncRootEntry
		{
			/// <summary>
			/// This read-only WizardWrx.SyncRoot object reference holds a
			/// reference to the synchronization object itself.
			/// </summary>
			public SyncRoot LockableObject { get; }


			/// <summary>
			/// This read-only System.Type object reference holds a reference to
			/// the Type information supplied in the request to dispense a
			/// SyncRoot object against which to hold a lock.
			/// </summary>
			public Type DeclaringType { get; }


			/// <summary>
			/// This read-only System.String holds a reference to the name of
			/// the method that made the request for a SyncRoot object.
			/// </summary>
			public string MethodName { get; }


			/// <summary>
			/// The default constructor is explicitly defined and marked as
			/// private to document that all objects must be created fully
			/// initialized.
			/// </summary>
			private SyncRootEntry ( ) { }


			/// <summary>
			/// The one and only public constructor creates a fully initialized
			/// instance from the three supplied parameters, which corespond to
			/// its three read-only properties.
			/// </summary>
			/// <param name="pobjSyncRoot">
			/// This WizardWrx.SyncRoot object holds a reference to the SyncRoot
			/// object that goes into the current instance.
			/// </param>
			/// <param name="ptypDeclaringType">
			/// This System.Type object holds the declaring type information
			/// that accompanied the request.
			/// </param>
			/// <param name="pstrMethodName">
			/// This System.String represents the name of the method that made
			/// the request.
			/// </param>
			public SyncRootEntry ( SyncRoot pobjSyncRoot , Type ptypDeclaringType , string pstrMethodName )
			{
				LockableObject = pobjSyncRoot;
				DeclaringType = ptypDeclaringType;
				MethodName = pstrMethodName;
			}   // public SyncRootEntry construcctor
		}   // private sealed class SyncRootEntry


		/// <summary>
		/// This class keeps its own private synchronization object for use by
		/// its methods, so that access to them is serialized.
		/// </summary>
		private static readonly SyncRoot s_syncRoot = new SyncRoot ( @"WizardWrx.AppDomainSyncRootRegistry" );

		/// <summary>
		/// Internal store of synchronization objects, keyed by name.
		/// </summary>
		private static readonly ConcurrentDictionary<string , SyncRootEntry> s_syncRoots =
			new ConcurrentDictionary<string , SyncRootEntry> ( concurrencyLevel: 2 , capacity: 32 );


		/// <summary>
		/// Retrieve the <see cref="SyncRoot"/> associated with the specified
		/// raw key. This overload does not associate explicit type/method
		/// metadata with the key; it is the caller's responsibility to ensure
		/// that the key is collision-free and intention-revealing.
		/// </summary>
		/// <param name="pstrRawKey">
		/// This System.String represents a stable, unique identifier for the 
		/// lock. No metadata validation is performed for this overload.
		/// </param>
		/// <returns>
		/// A reference to the <see cref="SyncRoot"/> associated with the
		/// specified key.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="pstrRawKey"/> is null, empty, or whitespace.
		/// </exception>
		public static SyncRoot GetSyncRoot ( string pstrRawKey )
		{
			lock ( s_syncRoot )
			{
				if ( string.IsNullOrWhiteSpace ( pstrRawKey ) )
				{
					throw new ArgumentException (
						"SyncRoot key cannot be null, empty, or whitespace." ,
						nameof ( pstrRawKey ) );
				}   // if ( string.IsNullOrWhiteSpace ( pstrRawKey ) )

				SyncRootEntry entry = s_syncRoots.GetOrAdd (
					pstrRawKey ,
					_ => new SyncRootEntry ( new SyncRoot ( pstrRawKey ) , ptypDeclaringType: null , pstrMethodName: null ) );

				return entry.LockableObject;
			}   // lock ( s_syncRoot )
		}   // public static SyncRoot GetSyncRoot (1 of 3)


		/// <summary>
		/// Construct a canonical synchronization key from a type and method
		/// name, then retrieve the associated <see cref="SyncRoot"/>. This
		/// overload enforces strict metadata consistency for the key.
		/// </summary>
		/// <param name="ptypDeclaringType">
		/// The declaring <see cref="Type"/> for the method being synchronized.
		/// </param>
		/// <param name="pstrMethodName">
		/// This System.String represents the name of the method whose execution
		/// must be serialized.
		/// </param>
		/// <returns>
		/// If it succeeds, the return value is a reference to the 
		/// <see cref="SyncRoot"/> associated with the constructed key.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// An ArgumentNullException Exception is thrown if 
		/// <paramref name="ptypDeclaringType"/> is null.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// An ArgumentException Exception is thrown if
		/// <paramref name="pstrMethodName"/> is null, empty, or whitespace.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// An InvalidOperationException Exception is thrown if the constructed
		/// key is already associated with a different type or method name than
		/// the one supplied.
		/// </exception>
		public static SyncRoot GetSyncRoot ( Type ptypDeclaringType , string pstrMethodName )
		{
			lock ( s_syncRoot )
			{
				if ( ptypDeclaringType == null )
				{
					throw new ArgumentNullException ( nameof ( ptypDeclaringType ) );
				}   // if ( type == null )

				if ( string.IsNullOrWhiteSpace ( pstrMethodName ) )
				{
					throw new ArgumentException (
						"Method name cannot be null, empty, or whitespace." ,
						nameof ( pstrMethodName ) );
				}   // if ( string.IsNullOrWhiteSpace ( pstrMethodName ) )

				string strUniqueKey = $"{ptypDeclaringType.AssemblyQualifiedName}.{pstrMethodName}";

				SyncRootEntry entry = s_syncRoots.GetOrAdd (
					strUniqueKey ,
					_ => new SyncRootEntry ( new SyncRoot ( strUniqueKey ) , ptypDeclaringType , pstrMethodName ) );

				if ( entry.DeclaringType != null )
				{
					if ( ( !ReferenceEquals ( entry.DeclaringType , ptypDeclaringType ) ) ||
						( !string.Equals ( entry.MethodName , pstrMethodName , StringComparison.Ordinal ) ) )
					{
						throw new InvalidOperationException (
							$"SyncRoot key '{strUniqueKey}' is already associated with " +
							$"{entry.DeclaringType.FullName}.{entry.MethodName}, " +
							$"but was requested for {ptypDeclaringType.FullName}.{pstrMethodName}." );
					}   // if ( ( !ReferenceEquals ( entry.DeclaringType , ptypDeclaringType ) ) || ( !string.Equals ( entry.MethodName , pstrMethodName , StringComparison.Ordinal ) ) )
				}   // if ( entry.DeclaringType != null )

				return entry.LockableObject;
			}   // lock ( s_syncRoot )
		}   // public static SyncRoot GetSyncRoot (2 of 3)


		/// <summary>
		/// Convenience overload that infers the declaring type from the
		/// generic type parameter and the method name from the caller context.
		/// </summary>
		/// <typeparam name="T">
		/// The declaring type for the method being synchronized.
		/// </typeparam>
		/// <param name="pstrCallerMemberName">
		/// The name of the calling member, supplied automatically by the
		/// compiler.
		/// </param>
		/// <returns>
		/// A reference to the <see cref="SyncRoot"/> associated with the
		/// constructed key.
		/// </returns>
		public static SyncRoot GetSyncRoot<T> ( [CallerMemberName] string pstrCallerMemberName = SpecialStrings.EMPTY_STRING )
		{	// Since this routine delegates its request to overload 2 of 3, it needs no lock of its own. Indeed, such a lock would be conterproductive, as it would cause a race condition.
			return GetSyncRoot ( typeof ( T ) , pstrCallerMemberName );
		}   // public static SyncRoot GetSyncRoot<T> (3 of 3)


		/// <summary>
		/// Enumerate all registered synchronization roots and their keys.
		/// </summary>
		/// <remarks>
		/// The returned dictionary is a snapshot view of the current registry
		/// contents. It is intended for diagnostics, logging, and debugging.
		/// </remarks>
		public static IReadOnlyDictionary<string , SyncRoot> RegisteredSyncRoots
		{
			get
			{
				lock ( s_syncRoot )
				{
					Dictionary<string , SyncRoot> snapshot = new Dictionary<string , SyncRoot> ( s_syncRoots.Count );

					foreach ( KeyValuePair<string , SyncRootEntry> kvp in s_syncRoots )
					{
						snapshot [ kvp.Key ] = kvp.Value.LockableObject;
					}   // foreach ( KeyValuePair<string , SyncRootEntry> kvp in s_syncRoots )

					return snapshot;
				}   // lock ( s_syncRoot )
			}   // public static IReadOnlyDictionary<string , SyncRoot> RegisteredSyncRoots property getter method
		}   // public static IReadOnlyDictionary<string , SyncRoot> RegisteredSyncRoots property
	}   // public static class AppDomainSyncRootRegistry
}   // partial namespace WizardWrx