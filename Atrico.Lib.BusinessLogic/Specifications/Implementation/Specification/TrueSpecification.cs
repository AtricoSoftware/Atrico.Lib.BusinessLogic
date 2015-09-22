namespace Atrico.Lib.BusinessLogic.Specifications.Implementation.Specification
{
    /// <summary>
    ///     Specification that is always satisfied, regardless of candidate
    /// </summary>
    /// <typeparam name="T">Candidate type of specification</typeparam>
    internal sealed class TrueSpecification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T candidate)
        {
            return true;
        }
    }
}