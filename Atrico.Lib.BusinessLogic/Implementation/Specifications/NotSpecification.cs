using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Implementation.Specifications
{
    /// <summary>
    ///     Represents a NOT specification.
    /// </summary>
    /// <typeparam name="T">The </typeparam>
    /// <remarks>
    ///     <para>This class cannot be inherited.</para>
    /// </remarks>
    internal sealed class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _operand;

        public static ISpecification<T> Create(ISpecification<T> rhs)
        {
            var not = rhs as NotSpecification<T>;
            return !ReferenceEquals(not, null) ? not._operand : new NotSpecification<T>(rhs);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotSpecification{T}" /> class.
        /// </summary>
        /// <param name="operand">The specification operand to negate.</param>
        private NotSpecification(ISpecification<T> operand)
        {
            _operand = operand;
        }

        public bool IsSatisfiedBy(T candidate)
        {
            return !_operand.IsSatisfiedBy(candidate);
        }

        public override string ToString()
        {
            return string.Format("Not {0}", _operand);
        }
    }
}