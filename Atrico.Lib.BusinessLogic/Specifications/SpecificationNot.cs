using System;

namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Implementation of Not specification
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public class SpecificationNot<T>
		: UnarySpecification<T>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="operand">Operand to Not operation</param>
		public SpecificationNot(ISpecification<T> operand)
			: base(operand)
		{
		}

		protected override bool Operate(Func<bool> operand)
		{
			return !operand();
		}
	}
}