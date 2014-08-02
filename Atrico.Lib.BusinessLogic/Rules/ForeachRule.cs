using System.Collections.Generic;
using System.Linq;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Rule that performs an action
	/// </summary>
	/// <typeparam name="T">Enumeration item type</typeparam>
	internal class ForeachRule<T> : RuleBase<IEnumerable<T>>
	{
		private readonly IRule<T> _rule;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="rule">Rule to execute for each item</param>
		public ForeachRule(IRule<T> rule)
		{
			_rule = rule;
		}

		public override bool Process(IEnumerable<T> subject)
		{
			return subject.Aggregate(true, (current, item) => current & _rule.Process(item));
		}
	}
}