using System.Linq;

namespace Atrico.Lib.BusinessLogic.Specifications.Builder
{
    public sealed class SpecificationBuilderAnd<T> : SpecificationContainerBuilderBase<T>
    {
        protected override ISpecification<T> BuildImpl()
        {
            return Items.Aggregate(Specification.True<T>(), (current, item) => current.And(item.Build()));
        }
    }
}