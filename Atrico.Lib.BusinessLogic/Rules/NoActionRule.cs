namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Action node
	/// </summary>
	/// <typeparam name="T">Underlying object type</typeparam>
	internal class NoActionRule<T> : IRule<T>
	{
		public bool Process(T subject)
		{
			// Nothing to do
			return true;
		}

		public IRule<T> FollowOn(IRule<T> followOnRule)
		{
			return followOnRule;
		}

		public override string ToString()
		{
			return "NoAction";
		}
	}
}