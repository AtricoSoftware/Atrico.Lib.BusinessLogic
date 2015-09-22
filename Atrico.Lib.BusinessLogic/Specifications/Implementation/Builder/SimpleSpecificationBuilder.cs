using System;

namespace Atrico.Lib.BusinessLogic.Specifications.Implementation.Builder
{
    internal class SimpleSpecificationBuilder<T> : ISpecificationBuilder<T>
    {
        private readonly Func<ISpecification<T>> _specificationFactory;

        public SimpleSpecificationBuilder(Func<ISpecification<T>> specificationFactory)
        {
            _specificationFactory = specificationFactory;
        }

        public ISpecification<T> Build()
        {
            return _specificationFactory();
        }
    }
}