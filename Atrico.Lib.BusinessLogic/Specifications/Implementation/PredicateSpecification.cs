using System;

namespace Atrico.Lib.BusinessLogic.Specifications.Implementation
{
    /// <summary>
    ///     Create specification from a function delegate
    /// </summary>
    /// <typeparam name="T">Type of underlying object</typeparam>
    internal sealed class PredicateSpecification<T> : ISpecification<T>
    {
        private readonly Func<T, bool> _predicate;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PredicateSpecification{T}" /> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public PredicateSpecification(Func<T, bool> predicate)
        {
            _predicate = predicate;
        }

        public bool IsSatisfiedBy(T candidate)
        {
            return _predicate(candidate);
        }
    }
}