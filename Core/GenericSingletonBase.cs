/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:			GenericSingletonBase<T>

	File Name:			GenericSingletonBase.cs

    Synopsis:			Abstract class GenericSingletonBase is a complete 
						implementation of the Singleton design pattern that
						takes full advantage of the Microsoft .NET Framework.
						Please see the Remarks for further details.

    Remarks:			Sibling classes ExceptionLogger and StateManager are the
						first two concrete classes to inherit from this class.
						ConsoleAppStateManager, the singleton that motivated the
						research mentioned in the next paragraph, is the first
						class defined in a dependent assembly to be derived from
						this class.

						I've thought several times over the last few years that
						the Singleton design pattern probably lends itself to an
						abstract base class, though it had not yet occurred to
						me to make it a Generic. Then, while I was investigating
						whether it was practical to inherit from sibling class
						StateManager, I read the first article cited below.

						Though the article that inspired me has vanished, a new
						article, by Boris Brock, dated 05 April 2013, covers the
						same ground. The only difference between the two
						implemntations is that Mr. Brock used Lazy<T>, which
						made its debut in version 4.0 of the Microsoft .NET
						Framework. I see no reason to upgrade solid, functioning
						code for its sake.

						The revisions of Sunday, 01 August 2021 arose because
						reading the fourth article prompted a review of this
						class to see whether it uses the double-checked locking
						mechanism mentioned therein as the gold standard of lazy
						singleton instantiation implementations.

						As I reviewed my code, I was reminded that this code can
						dispense with double-checked locking because the static
						constructor initializes the object. Since the framework
						guarantees that the static constructor is called exactly
						once, the very first time the object is referenced, it
						can safely dispense with the double-checked locking
						because static constructors are intrinsically thread-safe.

	References:			1)	Base class for Singleton Pattern with Generics
							http://geekswithblogs.net/NewThingsILearned/archive/2009/07/24/base-class-for-singleton-pattern-with-generics.aspx
							
							As of Sunday, 01 August 2021, this article is no
							longer avaliable, and is superseded by the next
							reference.

						2)	A Reusable Base Class for the Singleton Pattern in C#
							https://www.codeproject.com/articles/572263/a-reusable-base-class-for-the-singleton-pattern-in

							Since Lazy<T> didn't arrive until the next version
							of the Microsoft .NET Framework, this implementation
							makes do without it.

						3)	CreateInstance(Type, Boolean)
							https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance?view=netframework-3.5#System_Activator_CreateInstance_System_Type_System_Boolean_

							Though it doesn't explicitly say so, this document
							implies that this method is intended for use by
							compilers, rather than user code. Nevertheless, the
							method is publicly documented, and the public
							documentation is devoid of outright restrictions on
							use in user code.

						4)	5 Ways to Implement the Singleton Design Anti-Pattern in C#
							https://levelup.gitconnected.com/5-ways-to-implement-the-singleton-design-anti-pattern-in-c-68bb664c31f2

							Reading this article prompted me to review the
							implemnentation of this class to remind myself how
							and why it dispenses with the usual double-checked
							locking mechanism that seems to be the gold standard
							for lazy singleton instantiation.

	Author:				David A. Gray

    License:            Copyright (C) 2016-2021, David A. Gray. 
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

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2016/04/09 6.0     DAG    This class makes its debut.

	2017/02/25 7.0     DAG    This class is promoted to the root WizardWrx
                              namespace, and moved to WizardWrx.Core.dll.

	2018/09/06 7.0     DAG    Replace leftover documentation that came along for
	                          the ride when I copied another file to create this
							  class module, and add a GetTheSingleInstance
							  method.

	2021/08/01 8.0.248 DAG    Update the references to account for the article
                              that inspired the original implementation ceased
							  to exist. Thankfully, I found a newer article that
                              covers the same ground with one difference; its
                              example uses the Lazy<T> class, which arrived with
                              version 4 of the Microsoft .NET Framework.

							  This revision is a documentation revision. Other
                              than eliminating two unused using directives and
							  hiding PRIVATE_CTOR_OK in the method that uses it,
							  the code is unchanged.
    ============================================================================
*/


using System;


namespace WizardWrx
{
	/// <summary>
	/// Abstract class GenericSingletonBase is a complete implementation of the
	/// Singleton design pattern that takes full advantage of the Microsoft .NET
	/// Framework. Please see the Remarks for further details.
	/// </summary>
	/// <typeparam name="T">
	/// This class uses a recursive constraint on T, to require it to be derived
	/// from this base class.
	/// </typeparam>
	/// <remarks>
	/// The optimizations in this implementation take advantage of a guarantee
	/// made by the framework that it won't bother to call a static constructor
	/// on a class until its first use. Moreover, a static constructor is never
	/// called more than once, no matter how many subsequent references to the
	/// class occur.
	/// 
	/// Taking advantage of these features of the framework eliminates the need
	/// for synchronization, and replaces a method call with a direct reference
	/// to the static read only property that returns a reference to the one and
	/// only instance.
	/// </remarks>
	public abstract class GenericSingletonBase<T> where T : GenericSingletonBase<T>
	{
		/// <summary>
		/// The private constructor has no real work to do, but it must exist to
		/// prevent the framework from generating a public constructor, which
		/// would violate a critical constraint of the Singleton design pattern.
		/// </summary>
		/// <remarks>
		/// This property is marked protected to give derived classes direct
		/// access to it, which their static members require. For example, in
		/// ExceptionLogger, the static initializer has more work to do that it
		/// cannot start until an instance exists.
		/// </remarks>
		protected GenericSingletonBase ( )
		{
		}   // protected GenericSingletonBase, to suppress generation of a public parameterless constructor


		/// <summary>
		/// The static constructor initializes the private static 
		/// _genTheOnlyInstance member that holds the reference to the one and
		/// only instance of the derived class.
		/// </summary>
		/// <remarks>
		/// I had to stop and think for a minute about why the first token in
		/// the r-value is the upper case T, enclosed in parentheses. Then, I
		/// realized that it is an explicit cast.
		/// 
		/// More obvious, due to recent experience adding a static constructor
		/// to another class, is that the static constructor cannot have an
		/// access modifier, and is, by definition, private. This simplification
		/// of the Singleton design pattern takes advantage of the fact that the
		/// framework won't run the static constructor until the first reference
		/// to the class arises.
		/// </remarks>
		static GenericSingletonBase ( )
		{
			// 	Use this as the second argument to Activator.CreateInstance, to
			// 	tell it that a private constructor is acceptable, which meets a
			// 	requirement of the Singleton design pattern.

			const bool PRIVATE_CTOR_OK = true;

			s_genTheOnlyInstance = ( T ) Activator.CreateInstance ( typeof ( T ) , PRIVATE_CTOR_OK );
		}	// GenericSingletonBase


		/// <summary>
		/// This static member holds the reference to the one and only instance
		/// of the derived class that is permitted.
		/// </summary>
		/// <remarks>
		/// This property is marked protected to give derived classes direct
		/// access to it, which their static members require. For example, in
		/// ExceptionLogger, the static initializer has more work to do that it
		/// cannot start until an instance exists.
		/// </remarks>
		protected static T s_genTheOnlyInstance;


		/// <summary>
		/// This implementation simplifies access to the single instance by way
		/// of this static read-only property that returns the reference to the
		/// instance stored in its one and only private static member.
		/// </summary>
		/// <remarks>
		/// The sweet thing about this implementation is that your code doesn't
		/// need a copy of the reference, since a tail call on the static
		/// property is sufficient.
		/// </remarks>
		public static T TheOnlyInstance
		{
			get
			{
				return s_genTheOnlyInstance;
			}	// public static T TheOnlyInstance get method
		}   // public static T TheOnlyInstance, read only static property


		/// <summary>
		/// Implement the traditional GetTheSingleInstance method for obtaining
		/// a reference to a Singleton object.
		/// </summary>
		/// <returns>
		/// This method should always succeed by returning a reference to the
		/// one and only instance of the derived class.
		/// </returns>
		public static T GetTheSingleInstance ( )
		{
			return s_genTheOnlyInstance;
		}   // public static T GetTheSingleInstance method
	}	// public abstract class GenericSingletonBase<T>
}	// namespace WizardWrx