using System.Linq;

namespace Atrico.Lib.BusinessLogic.Specifications.Builder
{
    public sealed class SpecificationBuilderOr<T> : SpecificationContainerBuilderBase<T>
    {
        protected override ISpecification<T> BuildImpl()
        {
            return Items.Aggregate(Specification.False<T>(), (current, item) => current.Or(item.Build()));
        }
    }
}