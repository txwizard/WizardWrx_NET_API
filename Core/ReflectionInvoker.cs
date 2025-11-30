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
				instance: null );

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
		/// <param name="instance">Instance of the type on which to invoke the method.</param>
		/// <param name="paobjParameters">Arguments to pass to the method.</param>
		/// <param name="penmFlagsBitMask">Binding penmFlagsBitMask (defaults to Public | Instance).</param>
		/// <returns>Result of the method call, cast to <typeparamref name="T"/>.</returns>
		public static T InvokeInstanceMethod<T> (
			string pstrAssemblyPath ,
			string pstrFullyQualifiedTypeName ,
			string pstrMethodName ,
			object instance ,
			object [ ] paobjParameters ,
			BindingFlags penmFlagsBitMask = BindingFlags.Public | BindingFlags.Instance )
		{
			object result = InvokeMethodInternal (
				pstrAssemblyPath ,
				pstrFullyQualifiedTypeName ,
				pstrMethodName ,
				paobjParameters ,
				penmFlagsBitMask ,
				instance );

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
			object instance )
		{
			Assembly asm = Assembly.LoadFrom ( pstrAssemblyPath );
			Type targetType = asm.GetType ( pstrFullyQualifiedTypeName , throwOnError: true );

			object [ ] args = paobjParameters ?? Array.Empty<object> ( );
			Type [ ] argTypes = args.Select ( a => a?.GetType ( ) ).ToArray ( );

			MethodInfo mi = targetType.GetMethod ( pstrMethodName , penmFlagsBitMask , null , argTypes , null );
			if ( mi == null )
			{
				// Fallback: manually resolve overloads
				var candidates = targetType.GetMethods ( penmFlagsBitMask ).Where ( m => m.Name == pstrMethodName );
				mi = candidates.FirstOrDefault ( m => ParametersMatch ( m.GetParameters ( ) , argTypes ) );
			}

			if ( mi == null )
				throw new MissingMethodException (
					$"No overload of {pstrMethodName} matched the provided arguments." );

			return mi.Invoke ( instance , args );
		}   // private static object InvokeMethodInternal


		/// <summary>
		/// Determines whether the supplied argument types are assignable to the given paobjParameters.
		/// </summary>
		private static bool ParametersMatch ( ParameterInfo [ ] paobjParameters , Type [ ] argTypes )
		{
			if ( paobjParameters.Length != argTypes.Length ) return false;

			for ( int i = 0 ;
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
			}   // for ( int i = 0 ; i < paobjParameters.Length ; i++ )

			return true;
		}   // private static bool ParametersMatch

		private static bool AllowsNull ( Type t ) => !t.IsValueType || Nullable.GetUnderlyingType ( t ) != null;
	}   // public static class ReflectionInvoker
}   // partial namespace WizardWrx.Core