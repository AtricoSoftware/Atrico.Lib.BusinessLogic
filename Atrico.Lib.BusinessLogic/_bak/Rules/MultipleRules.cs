using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Atrico.Lib.BusinessLogic.Rules
{
	[DebuggerDisplay("MultipleRules Count = {_rules.Count}")]
	public class MultipleRules<T> : RuleBase<T>
	{
		private readonly IList<IRule<T>> _rules;

		public MultipleRules(IRule<T> firstRule, params IRule<T>[] followOnRules)
		{
			var rules = new List<IRule<T>>();
			if (firstRule != null && !(firstRule is NoActionRule<T>)) rules.Add(firstRule);
			rules.AddRange(followOnRules.Where(rule => rule != null && !(rule is NoActionRule<T>)));
			_rules = rules.ToArray();
		}

		public override bool Process(T subject)
		{
			return _rules.Aggregate(true, (current, rule) => current & rule.Process(subject));
		}
	}
}