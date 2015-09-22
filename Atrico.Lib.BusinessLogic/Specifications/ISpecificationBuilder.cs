namespace Atrico.Lib.BusinessLogic.Specifications
{
    public interface ISpecificationBuilder<T>
    {
        ISpecification<T> Build();
    }
}