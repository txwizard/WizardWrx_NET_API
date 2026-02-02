using System;
using System.Linq;
using System.Reflection;

using WizardWrx.Core;   // for ReflectionInvoker
using WizardWrx;        // for SpecialStrings, SpecialCharacters, and ConsoleSymbols

/// <summary>
/// Demonstration harness for ReflectionInvoker.
/// Enumerates overloads of CSVParseEngine.Parse, scores them,
/// attempts invocation, and prints a summary report.
/// </summary>
class ReflectionInvokerDemo
{
	static void Main ( )
	{
		// Ensure encoding and glyphs are ready before any output
		ConsoleSymbols.Initialize ( );

		// Executing assembly version info
		Assembly exeAssembly = Assembly.GetExecutingAssembly ( );
		Version identityVersion = exeAssembly.GetName ( ).Version;
		var fileVersionAttr = exeAssembly.GetCustomAttribute<AssemblyFileVersionAttribute> ( );
		string fileVersion = fileVersionAttr?.Version ?? "n/a";

		Console.WriteLine ( "=== ReflectionInvoker Overload Resolution Demo ===" );
		Console.WriteLine ( $"Executing Assembly: {exeAssembly.GetName ( ).Name}.exe" );
		Console.WriteLine ( $"Assembly Identity Version: {identityVersion}" );
		Console.WriteLine ( $"File Version: {fileVersion}" );
		Console.WriteLine ( );

		string strAnyCSVRelativePath = @"..\AnyCSV\AnyCSV\bin\Release\WizardWrx.AnyCSV.dll";
		string strTypeName = @"WizardWrx.AnyCSV.ICSVParser";
		string strMethodName = @"Parse";
		string strTestCSV = @"alpha,beta,gamma,delta";

		Assembly asm = Assembly.LoadFrom ( strAnyCSVRelativePath );
		Type engineType = asm.GetType ( strTypeName , throwOnError: true );
		string [ ] astrParsed = ReflectionInvoker.InvokeInstanceMethod<string [ ]> (
			strAnyCSVRelativePath ,                         // string pstrAssemblyPath
			strTypeName ,                                   // string pstrFullyQualifiedTypeName
			strMethodName ,                                 // string pstrMethodName
			engineType ,                                    // object pobjInstance
			new object [ ] { strTestCSV } );				// object [ ] paobjParameters

		// Static overloads live on the abstract base class.
		//var staticOverloads = engineType.BaseType
		//	.GetMethods ( BindingFlags.Public | BindingFlags.Static )
		//	.Where ( m => m.Name == strMethodName )
		//	.ToArray ( );

		//// Instance overloads are inherited into Parser.
		//var instanceOverloads = engineType
		//	.GetMethods ( BindingFlags.Public | BindingFlags.Instance )
		//	.Where ( m => m.Name == strMethodName )
		//	.ToArray ( );

		//foreach ( var mi in staticOverloads )
		//{
		//	ProcessOverload ( mi , strAnyCSVRelativePath , engineType.BaseType.FullName , strMethodName , strTestCSV , isStatic: true );
		//}

		//foreach ( var mi in instanceOverloads )
		//{
		//	ProcessOverload ( mi , strAnyCSVRelativePath , strTypeName , strMethodName , strTestCSV , isStatic: false );
		//}
	}   // static void Main


	private static void ProcessOverload (
		MethodInfo mi ,
		string strAnyCSVRelativePath ,
		string strTypeName ,
		string strMethodName ,
		string strTestCSV ,
		bool isStatic )
	{
		object [ ] args = BuildArgumentsFor ( mi , strTestCSV );
		Type [ ] argTypes = args.Select ( a => a?.GetType ( ) ).ToArray ( );

		int score = SpecificityScore ( mi , argTypes );
		bool match = ParametersMatch ( mi.GetParameters ( ) , argTypes );

		Console.WriteLine ( $"Candidate overload: {mi}" );
		Console.WriteLine ( "  Argument types:" );
		
		for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ;
			      i < argTypes.Length ;
				  i++ )
		{
			string typeNameDisplay = argTypes [ i ] == null ? "[null]" : argTypes [ i ].FullName;
			Console.WriteLine ( $"    Arg {i + 1} of {argTypes.Length}: {typeNameDisplay}" );
		}   // for ( int i = ArrayInfo.ARRAY_FIRST_ELEMENT ; i < argTypes.Length ; i++ )

		Console.WriteLine ( $"  Specificity score: {score}" );
		Console.WriteLine ( $"  Match result: {( match ? ConsoleSymbols.Check + " Match" : ConsoleSymbols.Cross + " No match" )}" );

		if ( match )
		{
			try
			{
				object result;
				
				if ( isStatic )
				{
					result = ReflectionInvoker.InvokeStaticMethod<object> (
						strAnyCSVRelativePath ,
						strTypeName ,
						strMethodName ,
						args );
				}   // TRUE (The matched method is marked as static.) block, if ( isStatic )
				else
				{
					object instance = Activator.CreateInstance ( mi.DeclaringType );
					result = ReflectionInvoker.InvokeInstanceMethod<object> (
						strAnyCSVRelativePath ,
						strTypeName ,
						strMethodName ,
						instance ,
						args );
				}   // FALSE (The matched method is marked as instance.) block, if ( isStatic )

				if ( result is string [ ] arr )
				{
					Console.WriteLine ( "  Result (string[]):" );

					for ( int intResultIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
						      intResultIndex < arr.Length ;
							  intResultIndex++ )
					{
						Console.WriteLine ( $"    Substring {ArrayInfo.OrdinalFromIndex( intResultIndex)} of {arr.Length}: {arr [ intResultIndex ]}" );
					}   // for ( int intResultIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intResultIndex < arr.Length ; intResultIndex++ )
				}   // TRUE (anticipated outcome) block, if ( result is string [ ] arr )
				else
				{
					Console.WriteLine ( $"  Result: {result ?? "null"}" );
				}   // FALSE (unanticipated outcome) block, if ( result is string [ ] arr )
			}
			catch ( Exception ex )
			{
				Console.WriteLine ( $"  Invocation failed: {ex.Message}" );
			}
		}   // if ( match )

		Console.WriteLine ( );

		// Summary line
		Console.WriteLine ( $"{( isStatic ? "[static]" : "[instance]" )} {mi} {SpecialStrings.RIGHT_ARROW} Score {score}, Match {( match ? ConsoleSymbols.Check : ConsoleSymbols.Cross )}" );
	}   // private static void ProcessOverload


	private static object [ ] BuildArgumentsFor ( MethodInfo mi , string strTestCSV )
	{
		var ps = mi.GetParameters ( );
		object [ ] args = new object [ ps.Length ];

		for ( int intParamIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
			      intParamIndex < ps.Length ;
				  intParamIndex++ )
		{
			Type pt = ps [ intParamIndex ].ParameterType;
			bool fHasDefaultValue = ps [ intParamIndex ].HasDefaultValue;
			object objDefaultValue = ps [ intParamIndex ].DefaultValue;

			if ( pt == typeof ( string ) )
			{
				args [ intParamIndex ] = strTestCSV;
			}
			else if ( pt == typeof ( char ) )
			{
				args [ intParamIndex ] = fHasDefaultValue ? objDefaultValue : null;
			}
			else if ( pt == typeof ( bool ) )
			{
				args [ intParamIndex ] = fHasDefaultValue ? objDefaultValue : null;
			}
			else
			{
				args [ intParamIndex ] = fHasDefaultValue ? objDefaultValue : null;
			}
		}   // for ( int intParamIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intParamIndex < ps.Length ; intParamIndex++ )

		return args;
	}   // private static object [ ] BuildArgumentsFor


	private static bool ParametersMatch ( ParameterInfo [ ] parameters , Type [ ] argTypes )
	{
		if ( parameters.Length != argTypes.Length ) return false;

		for ( int intParameterInfoIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
			      intParameterInfoIndex < parameters.Length ;
				  intParameterInfoIndex++ )
		{
			Type paramType = parameters [ intParameterInfoIndex ].ParameterType;
			Type argType = argTypes [ intParameterInfoIndex ];

			if ( argType == null )
			{
				if ( !AllowsNull ( paramType ) )
				{
					return false;
				}
			}
			else if ( !paramType.IsAssignableFrom ( argType ) )
			{
				return false;
			}
		}   // for ( int intParameterInfoIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intParameterInfoIndex < parameters.Length ; intParameterInfoIndex++ )

		return true;
	}   // private static bool ParametersMatch


	/// <summary>
	/// Computes a specificity score for a candidate overload,
	/// based on how closely its parameters match the argument types.
	/// 
	/// Scoring rules:
	/// - Exact type match: +3
	/// - Base type match (assignable but not exact): +2
	/// - Null argument accepted by parameter: +1
	/// - Argument type not assignable: -5 (penalty)
	/// 
	/// These constants are defined locally since they are only
	/// used within this method.
	/// </summary>
	private static int SpecificityScore ( MethodInfo mi , Type [ ] argTypes )
	{
		const int SCORE_EXACT_TYPE_MATCH = 3;
		const int SCORE_BASE_TYPE_MATCH = 2;
		const int SCORE_NULLABLE_MATCH = 1;
		const int SCORE_MISMATCH_PENALTY = -5;

		var ps = mi.GetParameters ( );
		int score = 0;

		for ( int intParameterIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ;
	 			  intParameterIndex < ps.Length ;
				  intParameterIndex++ )
		{
			Type paramType = ps [ intParameterIndex ].ParameterType;
			Type argType = argTypes [ intParameterIndex ];

			if ( argType == null )
			{
				score += AllowsNull ( paramType ) ? SCORE_NULLABLE_MATCH : 0;
			}
			else if ( argType == paramType )
			{
				score += SCORE_EXACT_TYPE_MATCH;
			}
			else if ( paramType.IsAssignableFrom ( argType ) )
			{
				score += SCORE_BASE_TYPE_MATCH;
			}
			else
			{
				score += SCORE_MISMATCH_PENALTY;
			}
		}   // for ( int intParameterIndex = ArrayInfo.ARRAY_FIRST_ELEMENT ; intParameterIndex < ps.Length ; intParameterIndex++ )

		return score;
	}   // private static int SpecificityScore


	private static bool AllowsNull ( Type t ) => !t.IsValueType || Nullable.GetUnderlyingType ( t ) != null;
}   // class ReflectionInvokerDemo