using Atrico.Lib.RulesEngine.Specifications;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Decision node
	/// </summary>
	/// <typeparam name="T">Underlying object type</typeparam>
	public class DecisionRule<T> : RuleBase<T>
	{
		internal ISpecification<T> Specification { get; private set; }
		internal IRule<T> SatisfiedRule { get; private set; }
		internal IRule<T> UnsatisfiedRule { get; set; }

		/// <summary>
		///     Constructor
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="specification">Specification to test</param>
		/// <param name="satisfiedRule">Rule to process if specification is satisfied</param>
		/// <param name="unsatisfiedRule">Rule to process if specification is not satisfied</param>
		public DecisionRule(ISpecification<T> specification, IRule<T> satisfiedRule = null, IRule<T> unsatisfiedRule = null)
		{
			Specification = specification;
			SatisfiedRule = satisfiedRule ?? new NoActionRule<T>();
			UnsatisfiedRule = unsatisfiedRule ?? new NoActionRule<T>();
		}

		public override bool Process(T subject)
		{
			return Specification.IsSatisfiedBy(subject) ? SatisfiedRule.Process(subject) : UnsatisfiedRule.Process(subject);
		}

		public override string ToString()
		{
			return string.Format("({0}) ? ({1}) : ({2})", Specification, SatisfiedRule, UnsatisfiedRule);
		}
	}
}