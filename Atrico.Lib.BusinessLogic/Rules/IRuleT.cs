namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Interface to typed rule to be used in Rule Engine
	/// </summary>
	/// <typeparam name="T">Type of underlying object</typeparam>
	public interface IRule<T>
	{
		/// <summary>
		///     Process this rule for the given object
		/// </summary>
		/// <param name="subject">Object for which to process rule</param>
		bool Process(T subject);

		/// <summary>
		///     Add a follow on rule to be processed after this one
		/// </summary>
		/// <param name="followOnRule">Rule to process after this one</param>
		/// <returns>New rule representing this follewed by other</returns>
		IRule<T> FollowOn(IRule<T> followOnRule);
	}
}