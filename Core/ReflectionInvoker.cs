using System;
using System.Linq;
using System.Reflection;

namespace WizardWrx.Core
{
	/// <summary>
	/// Provides generic, overload-resolving reflection helpers for invoking
	/// static and instance methods with defensive error handling.
	/// </summary>
	public static class ReflectionInvoker
	{
		/// <summary>
		/// Invoke a static method by name, resolving overloads based on argument types,
		/// and return its result as the specified generic type.
		/// </summary>
		/// <typeparam name="T">Expected return type.</typeparam>
		/// <param name="pstrAssemblyPath">Absolute path to the DLL containing the type.</param>
		/// <param name="pstrFullyQualifiedTypeName">Namespace-qualified type name.</param>
		/// <param name="pstrMethodName">Name of the static method to invoke.</param>
		/// <param name="paobjParameters">Arguments to pass to the method.</param>
		/// <param name="penmFlagsBitMask">Binding penmFlagsBitMask (defaults to Public | Static | BindingFlags.FlattenHierarchy).</param>
		/// <returns>Result of the method call, cast to <typeparamref name="T"/>.</returns>
		public static T InvokeStaticMethod<T> (
			string pstrAssemblyPath ,
			string pstrFullyQualifiedTypeName ,
			string pstrMethodName ,
			object [ ] paobjParameters ,
			BindingFlags penmFlagsBitMask = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy )
		{
			object result = InvokeMethodInternal (
				pstrAssemblyPath ,
				pstrFullyQualifiedTypeName ,
				pstrMethodName ,
				paobjParameters ,
				penmFlagsBitMask ,
				pobjInstance: null );

			if ( result is T typed ) return typed;

			throw new InvalidCastException (
				$"Expected return type {typeof ( T ).FullName}, but got {( result == null ? "null" : result.GetType ( ).FullName )}." );
		}   // public static T InvokeStaticMethod<T>


		/// <summary>
		/// Invoke an instance method by name, resolving overloads based on argument types,
		/// and return its result as the specified generic type.
		/// </summary>
		/// <typeparam name="T">Expected return type.</typeparam>
		/// <param name="pstrAssemblyPath">Absolute path to the DLL containing the type.</param>
		/// <param name="pstrFullyQualifiedTypeName">Namespace-qualified type name.</param>
		/// <param name="pstrMethodName">Name of the instance method to invoke.</param>
		/// <param name="pobjInstance">Instance of the type on which to invoke the method.</param>
		/// <param name="paobjParameters">Arguments to pass to the method.</param>
		/// <param name="penmFlagsBitMask">Binding penmFlagsBitMask (defaults to Public | Instance).</param>
		/// <returns>Result of the method call, cast to <typeparamref name="T"/>.</returns>
		public static T InvokeInstanceMethod<T> (
			string pstrAssemblyPath ,
			string pstrFullyQualifiedTypeName ,
			string pstrMethodName ,
			object pobjInstance ,
			object [ ] paobjParameters ,
			BindingFlags penmFlagsBitMask = BindingFlags.Public | BindingFlags.Instance )
		{
			object result = InvokeMethodInternal (
				pstrAssemblyPath ,
				pstrFullyQualifiedTypeName ,
				pstrMethodName ,
				paobjParameters ,
				penmFlagsBitMask ,
				pobjInstance );

			if ( result is T typed ) return typed;

			throw new InvalidCastException (
				$"Expected return type {typeof ( T ).FullName}, but got {( result == null ? "null" : result.GetType ( ).FullName )}." );
		}   // public static T InvokeInstanceMethod<T>


		/// <summary>
		/// Internal overload resolver that selects the best matching method
		/// based on argument types and invokes it.
		/// </summary>
		private static object InvokeMethodInternal (
			string pstrAssemblyPath ,
			string pstrFullyQualifiedTypeName ,
			string pstrMethodName ,
			object [ ] paobjParameters ,
			BindingFlags penmFlagsBitMask ,
			object pobjInstance )
		{
			Assembly asm = Assembly.LoadFrom ( pstrAssemblyPath );
			Type targetType = asm.GetType ( pstrFullyQualifiedTypeName , throwOnError: true );

			object [ ] args = paobjParameters ?? Array.Empty<object> ( );

			return InvokeMethod (
				targetType ,                                // object            pobjTarget
				pstrMethodName ,                            // string            pstrMethodName
				BindingFlags.Public                         // BindingFlags      penmBindingFlagsBitMask
				| BindingFlags.Instance 
				| BindingFlags.DeclaredOnly ,
				args );                                     // params object [ ] paobjParameters

			//Type [ ] argTypes = args.Select ( a => a?.GetType ( ) ).ToArray ( );

			//MethodInfo mi = targetType.GetMethod ( pstrMethodName , penmFlagsBitMask , null , argTypes , null );

			//if ( mi == null )
			//{
			//	// Fallback: manually resolve overloads
			//	var candidates = targetType.GetMethods ( penmFlagsBitMask ).Where ( m => m.Name == pstrMethodName );
			//	mi = candidates.FirstOrDefault ( m => ParametersMatch ( m.GetParameters ( ) , argTypes ) );
			//}   // if ( mi == null )

			//if ( mi == null )
			//{
			//	throw new MissingMethodException (
			//		$"No overload of {pstrMethodName} matched the provided arguments." );
			//}   // if ( mi == null )

			//return mi.Invoke ( pobjInstance , args );
		}   // private static object InvokeMethodInternal


		/// <summary>
		/// Invokes a method (virtual, non-virtual, or overridden) on the given target object.
		/// </summary>
		/// <param name="pobjTarget">
		/// This required generic object represents the instance on which to invoke
		/// method <paramref name="pstrMethodName"/>
		/// </param>
		/// <param name="pstrMethodName">
		/// This string represents the name of the method on object
		/// <paramref name="pobjTarget"/> to invoke.
		/// </param>
		/// <param name="penmBindingFlagsBitMask">
		/// This bit mask represents one or more members of the BindingFlags
		/// enumeration.
		/// </param>
		/// <param name="paobjParameters">
		/// This optional array of generic objects is the parameters for the
		/// method, if any. Since it is a parameter array, this argument is, and
		/// must be, last.
		/// </param>
		/// <returns>
		/// This generic object is the return value of the method, or null if the
		/// method returns void.
		/// </returns>
		public static object InvokeMethod ( object pobjTarget , string pstrMethodName , BindingFlags penmBindingFlagsBitMask , params object [ ] paobjParameters )
		{
			if ( pobjTarget == null ) throw new ArgumentNullException ( nameof ( pobjTarget ) , @"Target object cannot be null." );
			if ( string.IsNullOrWhiteSpace ( pstrMethodName ) ) throw new ArgumentException ( @"Method name cannot be null or whitespace." , nameof ( pstrMethodName ) );

			// Get the runtime type (important for abstract references).
			Type runtimeType = pobjTarget.GetType ( );

			// Find the method (public or non-public instance).
			MethodInfo method = runtimeType.GetMethod (
				pstrMethodName ,
				penmBindingFlagsBitMask );

			if ( method == null ) throw new MissingMethodException ( $"Method '{pstrMethodName}' not found on type '{runtimeType.FullName}'." );

			try
			{
				return method.Invoke ( pobjTarget , paobjParameters );
			}
			catch ( TargetInvocationException ex )
			{
				// Unwrap the inner exception for clarity.
				throw ex.InnerException ?? ex;
			}
		}   // public static object InvokeMethod


		/// <summary>
		/// Determines whether the supplied argument types are assignable to the given paobjParameters.
		/// </summary>
		private static bool ParametersMatch ( ParameterInfo [ ] paobjParameters , Type [ ] argTypes )
		{
			if ( paobjParameters.Length != argTypes.Length ) return false;

			for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ;
				      i < paobjParameters.Length ;
					  i++ )
			{
				Type paramType = paobjParameters [ i ].ParameterType;
				Type argType = argTypes [ i ];

				if ( argType == null )
				{
					if ( !AllowsNull ( paramType ) ) return false;
				}   // if ( argType == null )
				else if ( !paramType.IsAssignableFrom ( argType ) )
				{
					return false;
				}   // else if ( !paramType.IsAssignableFrom ( argType ) )
			}   // for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ; i < paobjParameters.Length ; i++ )

			return true;
		}   // private static bool ParametersMatch

		private static bool AllowsNull ( Type t ) => !t.IsValueType || Nullable.GetUnderlyingType ( t ) != null;
	}   // public static class ReflectionInvoker
}   // partial namespace WizardWrx.Core