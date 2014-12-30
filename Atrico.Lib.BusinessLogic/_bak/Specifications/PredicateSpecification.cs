using System;

namespace Atrico.Lib.RulesEngine.Specifications
{
	public class PredicateSpecification<T>
		: Specification<T>
	{
		private readonly Func<T, bool> _predicate;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="predicate">Predicate to check for satisfaction</param>
		public PredicateSpecification(Func<T, bool> predicate)
		{
			_predicate = predicate;
		}

		public override bool IsSatisfiedBy(T candidate)
		{
			return _predicate(candidate);
		}
	}
}