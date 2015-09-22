using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Implementation.Specifications
{
    /// <summary>
    ///     Implementation of OR logic
    /// </summary>
    /// <typeparam name="T">Type of underlying object</typeparam>
    internal sealed class OrSpecification<T> : CompositeSpecification<T>
    {
        public static ISpecification<T> Create(ISpecification<T> lhs, ISpecification<T> rhs)
        {
            if (lhs is TrueSpecification<T>)
            {
                return lhs;
            }
            if (rhs is TrueSpecification<T>)
            {
                return rhs;
            }
            if (lhs is FalseSpecification<T>)
            {
                return rhs;
            }
            if (rhs is FalseSpecification<T>)
            {
                return lhs;
            }
            var specifications = new List<ISpecification<T>>();
            specifications.AddRange(GetSpecifications<OrSpecification<T>>(lhs));
            specifications.AddRange(GetSpecifications<OrSpecification<T>>(rhs));
            return new OrSpecification<T>(specifications);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OrSpecification{T}" /> class.
        /// </summary>
        /// <param name="specifications">The specifications to OR</param>
        private OrSpecification(IEnumerable<ISpecification<T>> specifications)
            : base(specifications)
        {
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return Specifications.Any(spec => spec.IsSatisfiedBy(candidate));
        }

        public override string ToString()
        {
            return ToString("OR");
        }
    }
}