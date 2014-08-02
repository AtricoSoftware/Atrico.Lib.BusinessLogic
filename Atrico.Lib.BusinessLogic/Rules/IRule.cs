namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Interface to typed rule to be used in Rule Engine
	/// </summary>
	public interface IRule
	{
		/// <summary>
		///     Process this rule for the given object
		/// </summary>
		bool Process();

		/// <summary>
		///     Add a follow on rule to be processed after this one
		/// </summary>
		/// <param name="followOnRule">Rule to process after this one</param>
		/// <returns>New rule representing this followed by other</returns>
		IRule FollowOn(IRule followOnRule);
	}
}