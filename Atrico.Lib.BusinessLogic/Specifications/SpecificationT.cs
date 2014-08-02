namespace Atrico.Lib.RulesEngine.Specifications
{
	/// <summary>
	/// Base class for specifications
	/// </summary>
	/// <typeparam name="T">Candidate type</typeparam>
	public abstract class Specification<T>
		: ISpecification<T>
	{
		public abstract bool IsSatisfiedBy(T candidate);

		public virtual ISpecification<T> Not()
		{
			return new SpecificationNot<T>(this);
		}

		public virtual ISpecification<T> And(ISpecification<T> rhs)
		{
			return new SpecificationAnd<T>(this, rhs);
		}

		public virtual ISpecification<T> Or(ISpecification<T> rhs)
		{
			return new SpecificationOr<T>(this, rhs);
		}
	}
}