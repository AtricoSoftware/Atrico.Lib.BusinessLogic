namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Helper class for specifications
	/// </summary>
	public static class Specification
	{
		/// <summary>
		/// Create a specification representing the boolean Not of the specification
		/// </summary>
		/// <param name="operand">Operand for Not</param>
		/// <returns>New specification</returns>
		public static ISpecification<T> Not<T>(ISpecification<T> operand)
		{
			return operand.Not();
		}

		/// <summary>
		/// Create a specification representing the boolean And of the specifications
		/// </summary>
		/// <param name="lhs">lhs for operation</param>
		/// <param name="rhs">rhs for operation</param>
		/// <returns>New specification</returns>
		public static ISpecification<T> And<T>(ISpecification<T> lhs, ISpecification<T> rhs)
		{
			return lhs.And(rhs);
		}

		/// <summary>
		/// Create a specification representing the boolean Or of the specifications
		/// </summary>
		/// <param name="lhs">lhs for operation</param>
		/// <param name="rhs">rhs for operation</param>
		/// <returns>New specification</returns>
		public static ISpecification<T> Or<T>(ISpecification<T> lhs, ISpecification<T> rhs)
		{
			return lhs.Or(rhs);
		}
	}
}