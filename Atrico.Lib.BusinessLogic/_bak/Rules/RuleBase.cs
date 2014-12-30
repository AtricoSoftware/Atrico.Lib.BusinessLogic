namespace Atrico.Lib.BusinessLogic.Rules
{
	public abstract class RuleBase<T> : IRule<T>
	{
		public abstract bool Process(T subject);

		public IRule<T> FollowOn(IRule<T> followOnRule)
		{
			if (followOnRule == null || followOnRule is NoActionRule<T>) return this;
			return new MultipleRules<T>(this, followOnRule);
		}
	}
}