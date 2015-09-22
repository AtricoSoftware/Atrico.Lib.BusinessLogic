using System.Linq;

namespace Atrico.Lib.BusinessLogic.Specifications.Builder
{
    public sealed class SpecificationBuilderXor<T> : SpecificationContainerBuilderBase<T>
    {
        protected override ISpecification<T> BuildImpl()
        {
            var spec1 = Items.First().Build();
            var spec2 = Items.Skip(1).First().Build();
            return Items.Skip(2).Aggregate(spec1.Xor(spec2), (current, item) => current.Xor(item.Build()));
        }
    }
}