using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardWrx.Core
{
	public static class OneOffExceptionReporter
	{
		public static string ReportGenericException (Exception pexAllKinds)
		{
			const string ASM_METHOD_NAME = @"GetTheSingleInstance";
			const string ASM_REPORT_NAME = @"ReportException";

			Type typeOfExceptionLogger = typeof ( WizardWrx.DLLConfigurationManager.ExceptionLogger );
			System.Reflection.AssemblyName assemblyNameOfExceptionLogger = new System.Reflection.AssemblyName (
				System.Reflection.Assembly.GetAssembly (
					typeOfExceptionLogger ).FullName );
			WizardWrx.AssemblyUtils.DependentAssemblies daOfEntryAsm = new WizardWrx.AssemblyUtils.DependentAssemblies ( );
			System.Reflection.Assembly asmDllCfgMgr = daOfEntryAsm.GetDependentAssemblyByName ( assemblyNameOfExceptionLogger );

			if ( !asmDllCfgMgr.ReflectionOnly )
			{   // This is a sanity check. The dependent assembly cannot be loaded into the Reflection-only context.
				System.Reflection.MethodInfo miLoggerrInstance = typeOfExceptionLogger.GetMethod (
					ASM_METHOD_NAME ,
					Type.EmptyTypes );
				object objLogger = miLoggerrInstance.Invoke ( null , null );
				System.Reflection.MethodInfo miReporter = typeOfExceptionLogger.GetMethod (
					ASM_REPORT_NAME ,
					new Type [ ] { typeof ( IOException ) } );
				object objDummy = miReporter.Invoke (
					objLogger ,
					new object [ ] { exIO } );
			}   // if ( !asmDllCfgMgr.ReflectionOnly )

			asmDllCfgMgr = null;
			daOfEntryAsm.DestroyDependents ( );

		}
	}
}
