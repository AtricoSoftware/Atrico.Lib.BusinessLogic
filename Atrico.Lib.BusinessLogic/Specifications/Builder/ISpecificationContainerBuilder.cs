using System.Collections.Generic;

namespace Atrico.Lib.BusinessLogic.Specifications.Builder
{
    public interface ISpecificationContainerBuilder<T> : ISpecificationBuilder<T>
    {
        IEnumerable<ISpecificationBuilder<T>> Items { get; }
        ISpecificationContainerBuilder<T> Add(ISpecificationBuilder<T> specification);
        ISpecificationContainerBuilder<T> Convert<TConv>() where TConv : ISpecificationContainerBuilder<T>, new();
    }
}