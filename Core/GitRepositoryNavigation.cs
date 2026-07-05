using System;

namespace WizardWrx.Core
{
	/// <summary>
	/// This static class presently exposes a single method that discovers the
	/// name of the root directory of the Git repository that contains the
	/// current working directory or the directory specified by the caller.
	/// </summary>
	public class GitRepositoryNavigation
	{
		/// <summary>
		/// Call this method to discover the name of the root directory of the
		/// Git repository that contains the current working directory or the
		/// directory specified by the caller.
		/// </summary>
		/// <param name="pstrStartDirectoryName">
		/// This optional string identifies the directory from which to start
		/// the search. If null, the starting point is either the value of the
		/// WWREBASEPATHS_REPOROOT environment variable or the current working
		/// directory.
		/// </param>
		/// <returns>
		/// If it succeeds, the returnd string represents the name of the root
		/// directory of the current Git repository. Otherwise, the method
		/// raises an InvalidOperationException.
		/// </returns>
		/// <summary>
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method wraps the internal shared implementation and provides
		/// localized error messages using this assembly’s resource set.
		/// </para>
		/// <para>
		/// If the repository root cannot be determined, this method throws an
		/// <see cref="InvalidOperationException"/> whose message is formatted using
		/// <c>Properties.Resources.IDS_REPOSITORY_ROOT_NOT_FOUND</c>.
		/// </para>
		/// </remarks>
		public static string DiscoverRepoRoot ( string pstrStartDirectoryName )
		{
			try
			{
				return Shared.Common.DiscoverRepoRoot ( pstrStartDirectoryName );
			}
			catch ( InvalidOperationException ex )
			{
				throw new InvalidOperationException (
					string.Format (
						Properties.Resources.IDS_REPOSITORY_ROOT_NOT_FOUND ,
						ex.Message ) ,
					ex );
			}
		}   // public static string DiscoverRepoRoot
	}   // public class GitRepositoryNavigation
}   // partial namespace WizardWrx.Core