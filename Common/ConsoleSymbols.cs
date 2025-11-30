using System;
using System.Text;

namespace WizardWrx
{
	/// <summary>
	/// Provides standardized symbols for console output,
	/// with automatic fallback to ASCII if Unicode is unsupported.
	/// </summary>
	/// <remarks>
	/// <para>
	/// In character-mode (console) applications that use them, the Unicode
	/// glyphs, BULLET, CHECK_MARK, CROSS_MARK, and RIGHT_ARROW, require a
	/// console that supports UTF-8. Use <see cref="ConsoleSymbols.Initialize"/>
	/// to coerce the console to use a UTF-8 code page that supports them.
	/// Additionally, to properly display these glyphs, your console must render
	/// in a font face that supports these Unicode glyphs. Examples are Consolas,
	/// Cascadia Code, and Segoe UI Emoji.
	/// </para>
	/// <para>
	/// For ease of access, I promoted the classes that expose only constants to
	/// the root of the WizardWrx namespace.
	/// </para>
	/// </remarks>
	public static class ConsoleSymbols
	{
		/// <summary>
		/// Unicode check mark (✔) or ASCII fallback ("[OK]").
		/// </summary>
		public static readonly string Check;

		/// <summary>
		/// Unicode cross mark (✘) or ASCII fallback ("[X]").
		/// </summary>
		public static readonly string Cross;

		/// <summary>
		/// Initializes static fields by testing console encoding.
		/// </summary>
		static ConsoleSymbols ( )
		{
			try
			{
				// Force UTF-8 output encoding if possible
				Console.OutputEncoding = Encoding.UTF8;

				// If encoding is UTF-8, use Unicode glyphs
				if ( Console.OutputEncoding.Equals ( Encoding.UTF8 ) )
				{
					Check = "\u2714"; // ✔
					Cross = "\u2718"; // ✘
				}   // TRUE (anticipated outcome, console encoding is UTF-8) block, if ( Console.OutputEncoding.Equals ( Encoding.UTF8 ) )
				else
				{
					// Fallback to ASCII
					Check = "[OK]";
					Cross = "[X]";
				}   // FALSE (unanticipated outcome, console encoding is OEM) block, if ( Console.OutputEncoding.Equals ( Encoding.UTF8 ) )
			}
			catch
			{
				// Defensive fallback if encoding assignment fails
				Check = "[OK]";
				Cross = "[X]";
			}
		}   // static ConsoleSymbols constructor


		/// <summary>
		/// Forces static constructor execution early,
		/// ensuring encoding is set before first output.
		/// </summary>
		public static void Initialize ( )
		{
			// Touch a static field to trigger the constructor.
			_ = Check;
		}   // public static void Initialize
	}   // public static class ConsoleSymbols
}   // partial namespace WizardWrx