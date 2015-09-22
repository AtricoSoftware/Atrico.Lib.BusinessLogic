using System.Collections.Generic;
using System.Linq;

namespace Atrico.Lib.BusinessLogic.Specifications.Builder
{
    public abstract class SpecificationContainerBuilderBase<T> : ISpecificationContainerBuilder<T>
    {
        private readonly IList<ISpecificationBuilder<T>> _items = new List<ISpecificationBuilder<T>>();

        public ISpecification<T> Build()
        {
            if (!_items.Any())
            {
                return Specification.False<T>();
            }
            if (_items.Count == 1)
            {
                return _items.First().Build();
            }
            return BuildImpl();
        }

        protected abstract ISpecification<T> BuildImpl();

        public IEnumerable<ISpecificationBuilder<T>> Items
        {
            get { return _items; }
        }

        public ISpecificationContainerBuilder<T> Add(ISpecificationBuilder<T> specification)
        {
            _items.Add(specification);
            return this;
        }

        public ISpecificationContainerBuilder<T> Convert<TConv>() where TConv : ISpecificationContainerBuilder<T>, new()
        {
            return Items.Aggregate(new TConv() as ISpecificationContainerBuilder<T>, (current, item) => current.Add(item));
        }
    }
}