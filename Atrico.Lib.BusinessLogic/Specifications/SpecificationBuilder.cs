using System;
using Atrico.Lib.BusinessLogic.Specifications.Implementation.Builder;

namespace Atrico.Lib.BusinessLogic.Specifications
{
    public static class SpecificationBuilder
    {
        /// <summary>
        /// Simple builder that returns the given Specification
        /// </summary>
        /// <typeparam name="T">Type of specification</typeparam>
        /// <param name="specification">Specification to "create"</param>
        /// <returns>Specification</returns>
        public static ISpecificationBuilder<T> Create<T>(ISpecification<T> specification)
        {
            return Create(() => specification);
        }

        /// <summary>
        /// Simple builder that creates a specification using the given factory
        /// </summary>
        /// <typeparam name="T">Type of specification</typeparam>
        /// <param name="specificationFactory">Factory to create specification</param>
        /// <returns>Specification</returns>
        public static ISpecificationBuilder<T> Create<T>(Func<ISpecification<T>> specificationFactory)
        {
            return new SimpleSpecificationBuilder<T>(specificationFactory);
        }
    }
}