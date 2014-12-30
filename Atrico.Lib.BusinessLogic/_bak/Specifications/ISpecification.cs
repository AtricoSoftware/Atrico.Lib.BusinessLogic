namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Interface for specification
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public interface ISpecification<T>
	{
		/// <summary>
		/// Is this specification satisfied by the given candidate
		/// </summary>
		/// <param name="candidate">Candidate object to test</param>
		/// <returns>True if candidate satisfies the specification</returns>
		bool IsSatisfiedBy(T candidate);

		/// <summary>
		/// Create a specification representing the boolean Not of this specification
		/// </summary>
		/// <returns>New specification</returns>
		ISpecification<T> Not();

		/// <summary>
		/// Create a specification representing the boolean And of this specification and the rhs
		/// </summary>
		/// <param name="rhs">Right hand side of operator</param>
		/// <returns>New specification</returns>
		ISpecification<T> And(ISpecification<T> rhs);

		/// <summary>
		/// Create a specification representing the boolean Or of this specification and the rhs
		/// </summary>
		/// <param name="rhs">Right hand side of operator</param>
		/// <returns>New specification</returns>
		ISpecification<T> Or(ISpecification<T> rhs);
	}
}