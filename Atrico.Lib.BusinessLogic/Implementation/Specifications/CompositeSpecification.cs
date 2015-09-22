using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Implementation.Specifications
{
    internal abstract class CompositeSpecification<T> : ISpecification<T>
    {
        protected IEnumerable<ISpecification<T>> Specifications { get; private set; }

        protected CompositeSpecification(IEnumerable<ISpecification<T>> specifications)
        {
            Specifications = specifications;
        }

        public abstract bool IsSatisfiedBy(T candidate);

        protected string ToString(string @operator)
        {
            if (!Specifications.Any())
            {
                return "";
            }
            var text = new StringBuilder();
            var first = true;
            foreach (var spec in Specifications)
            {
                if (!first)
                {
                    text.AppendFormat(" {0} ", @operator);
                }
                else
                {
                    first = false;
                }
                text.Append(spec);
            }
            return text.ToString();
        }

        protected static IEnumerable<ISpecification<T>> GetSpecifications<TGroup>(ISpecification<T> specification) where TGroup : CompositeSpecification<T>
        {
            if (specification == null)
            {
                return new ISpecification<T>[] {};
            }
            var group = specification as TGroup;
            return !ReferenceEquals(group, null) ? group.Specifications : new[] {specification};
        }
    }
}