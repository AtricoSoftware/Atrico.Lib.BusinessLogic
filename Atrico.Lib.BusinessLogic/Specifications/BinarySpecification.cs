using System;

namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Implementation of binary specification
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public abstract class BinarySpecification<T>
		: Specification<T>
	{
		private readonly ISpecification<T> _lhs;
		private readonly ISpecification<T> _rhs;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lhs">lhs to operation</param>
		/// <param name="rhs">rhs to operation</param>
		protected BinarySpecification(ISpecification<T> lhs, ISpecification<T> rhs)
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public override bool IsSatisfiedBy(T candidate)
		{
			return Operate(() => _lhs.IsSatisfiedBy(candidate), () => _rhs.IsSatisfiedBy(candidate));
		}

		protected abstract bool Operate(Func<bool> lhs, Func<bool> rhs);
	}
}