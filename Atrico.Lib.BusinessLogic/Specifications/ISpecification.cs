namespace Atrico.Lib.BusinessLogic.Specifications
{
    /// <summary>
    ///     Interface to a specification filtering typed objects
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public interface ISpecification<in T>
    {
        /// <summary>
        ///     Is this specification satisfied by this object?
        /// </summary>
        /// <param name="candidate">Candidate object to check</param>
        /// <returns>Result: True if candidate satisfies the specification with success/failure message</returns>
        bool IsSatisfiedBy(T candidate);
    }
}