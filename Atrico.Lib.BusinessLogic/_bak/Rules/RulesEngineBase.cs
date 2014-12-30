using System.Collections.Generic;
using System.Linq;

namespace Atrico.Lib.BusinessLogic.Rules
{
	public abstract class RulesEngineBase<T> : RuleBase<T>, IRulesEngine<T>
	{
		public bool Inspect(IEnumerable<T> subjects)
		{
			return subjects.Aggregate(true, (current, subject) => current & Process(subject));
		}
	}
}