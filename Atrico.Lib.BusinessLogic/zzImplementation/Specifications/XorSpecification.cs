using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.zzImplementation.Specifications
{
    /// <summary>
    ///     Implementation of XOR logic
    /// </summary>
    /// <typeparam name="T">Type of underlying object</typeparam>
    internal sealed class XorSpecification<T> : CompositeSpecification<T>
    {
        public static ISpecification<T> Create(ISpecification<T> lhs, ISpecification<T> rhs)
        {
            var specifications = new List<ISpecification<T>>();
            specifications.AddRange(GetSpecifications<XorSpecification<T>>(lhs));
            specifications.AddRange(GetSpecifications<XorSpecification<T>>(rhs));
            return new XorSpecification<T>(specifications);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XorSpecification{T}" /> class.
        /// </summary>
        /// <param name="specifications">The specifications to XOR</param>
        private XorSpecification(IEnumerable<ISpecification<T>> specifications)
            : base(specifications)
        {
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            // True if odd number of results are true
            var results = Specifications.Select(spec => spec.IsSatisfiedBy(candidate));
            return (results.Count(r => r) & 1) == 1;
        }

        public override string ToString()
        {
            return ToString("XOR");
        }
    }
}