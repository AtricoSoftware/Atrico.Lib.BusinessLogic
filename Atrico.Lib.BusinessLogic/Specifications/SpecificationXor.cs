using System;

namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Implementation of Xor specification
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public class SpecificationXor<T>
		: BinarySpecification<T>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lhs">lhs to operation</param>
		/// <param name="rhs">rhs to operation</param>
		public SpecificationXor(ISpecification<T> lhs, ISpecification<T> rhs)
			: base(lhs, rhs)
		{
		}

		protected override bool Operate(Func<bool> lhs, Func<bool> rhs)
		{
			return lhs() ^ rhs();
		}
	}
}