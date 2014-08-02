using System;

namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Implementation of unary specification
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public abstract class UnarySpecification<T>
		: Specification<T>
	{
		private readonly ISpecification<T> _operand;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="operand">operand to operation</param>
		protected UnarySpecification(ISpecification<T> operand)
		{
			_operand = operand;
		}

		public override bool IsSatisfiedBy(T candidate)
		{
			return Operate(() => _operand.IsSatisfiedBy(candidate));
		}

		protected abstract bool Operate(Func<bool> operand);
	}
}