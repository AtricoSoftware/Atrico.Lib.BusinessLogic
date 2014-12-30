using System;

namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Implementation of Or specification
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public class SpecificationOr<T>
		: BinarySpecification<T>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lhs">lhs to operation</param>
		/// <param name="rhs">rhs to operation</param>
		public SpecificationOr(ISpecification<T> lhs, ISpecification<T> rhs)
			: base(lhs, rhs)
		{
		}

		protected override bool Operate(Func<bool> lhs, Func<bool> rhs)
		{
			return lhs() || rhs();
		}
	}
}