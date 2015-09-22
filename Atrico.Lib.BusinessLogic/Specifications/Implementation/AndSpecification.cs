using System.Collections.Generic;
using System.Linq;

namespace Atrico.Lib.BusinessLogic.Specifications.Implementation
{
    /// <summary>
    ///     Implementation of AND logic
    /// </summary>
    /// <typeparam name="T">Type of underlying object</typeparam>
    internal sealed class AndSpecification<T> : CompositeSpecification<T>
    {
        public static ISpecification<T> Create(ISpecification<T> lhs, ISpecification<T> rhs)
        {
            if (lhs is TrueSpecification<T>)
            {
                return rhs;
            }
            if (rhs is TrueSpecification<T>)
            {
                return lhs;
            }
            if (lhs is FalseSpecification<T>)
            {
                return lhs;
            }
            if (rhs is FalseSpecification<T>)
            {
                return rhs;
            }
            var specifications = new List<ISpecification<T>>();
            specifications.AddRange(GetSpecifications<AndSpecification<T>>(lhs));
            specifications.AddRange(GetSpecifications<AndSpecification<T>>(rhs));
            return new AndSpecification<T>(specifications);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OrSpecification{T}" /> class.
        /// </summary>
        /// <param name="specifications">The specifications to AND</param>
        private AndSpecification(IEnumerable<ISpecification<T>> specifications)
            : base(specifications)
        {
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return Specifications.All(spec => spec.IsSatisfiedBy(candidate));
        }

        public override string ToString()
        {
            return ToString("AND");
        }
    }
}