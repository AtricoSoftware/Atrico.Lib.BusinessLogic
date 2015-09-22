using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Implementation.Specifications
{
    /// <summary>
    ///     Specification that is never satisfied, regardless of candidate
    /// </summary>
    /// <typeparam name="T">Candidate type of specification</typeparam>
    internal sealed class FalseSpecification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T candidate)
        {
            return false;
        }
    }
}