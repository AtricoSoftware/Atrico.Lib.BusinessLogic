using System.Collections.Generic;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Represents the interface for rules engines.
	/// </summary>
	public interface IRulesEngine<T> : IRule<T>
	{
		/// <summary>
		///     Inspect the rules
		/// </summary>
		/// <param name="subjects">List of candidates to pass through engine</param>
		bool Inspect(IEnumerable<T> subjects);
	}
}