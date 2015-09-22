namespace Atrico.Lib.BusinessLogic.Specifications.Builder
{
    public interface ISpecificationBuilder<in T>
    {
        ISpecification<T> Build();
    }
}