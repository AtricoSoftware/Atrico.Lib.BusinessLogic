using System;
using System.Collections.Generic;
using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Implementation.Specifications
{
    /// <summary>
    ///     Class for specifications which compare a simple property to a list of possible values.
    /// </summary>
    internal sealed class PropertyIsOneOfSpecification<T, TProp> : ISpecification<T>
    {
        private Func<T, TProp> _getPropertyFunction { get; set; }

        private ISet<TProp> _expectedValues { get; set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="getPropertyFunction">Function to return property from candidate</param>
        /// <param name="expectedValues">Values to compare</param>
        public PropertyIsOneOfSpecification(Func<T, TProp> getPropertyFunction, params TProp[] expectedValues)
        {
            _getPropertyFunction = getPropertyFunction;
            _expectedValues = new HashSet<TProp>(expectedValues);
        }

        public bool IsSatisfiedBy(T candidate)
        {
            var value = _getPropertyFunction(candidate);
            return _expectedValues.Contains(value);
        }
    }
}