using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atrico.Lib.BusinessLogic.Specifications
{
	/// <summary>
	///     Helper function for specifications
	/// </summary>
	public static partial class Specification
	{
		/// <summary>
		///     Specification that is always satisfied, regardless of candidate
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		private sealed class TrueSpecification<T> : ISpecification<T>
		{
			public bool IsSatisfiedBy(T candidate)
			{
				return true;
			}
		}

		/// <summary>
		///     Specification that is never satisfied, regardless of candidate
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		private sealed class FalseSpecification<T> : ISpecification<T>
		{
			public bool IsSatisfiedBy(T candidate)
			{
				return false;
			}
		}

		/// <summary>
		///     Represents a NOT specification.
		/// </summary>
		/// <typeparam name="T">The </typeparam>
		/// <remarks>
		///     <para>This class cannot be inherited.</para>
		/// </remarks>
		private sealed class NotSpecification<T> : ISpecification<T>
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

		private abstract class CompositeSpecification<T> : ISpecification<T>
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

		/// <summary>
		///     Implementation of AND logic
		/// </summary>
		/// <typeparam name="T">Type of underlying object</typeparam>
		private sealed class AndSpecification<T> : CompositeSpecification<T>
		{
			public static ISpecification<T> Create(ISpecification<T> lhs, ISpecification<T> rhs)
			{
				if (lhs is TrueSpecification<T>)
				{
					return rhs;
				}
				if (rhs is TrueSpecification<T>)
				{
					return lhs;
				}
				if (lhs is FalseSpecification<T>)
				{
					return lhs;
				}
				if (rhs is FalseSpecification<T>)
				{
					return rhs;
				}
				var specifications = new List<ISpecification<T>>();
				specifications.AddRange(GetSpecifications<AndSpecification<T>>(lhs));
				specifications.AddRange(GetSpecifications<AndSpecification<T>>(rhs));
				return new AndSpecification<T>(specifications);
			}

			/// <summary>
			///     Initializes a new instance of the <see cref="OrSpecification{T}" /> class.
			/// </summary>
			/// <param name="specifications">The specifications to AND</param>
			private AndSpecification(IEnumerable<ISpecification<T>> specifications)
				: base(specifications)
			{
			}

			public override bool IsSatisfiedBy(T candidate)
			{
				return Specifications.All(spec => spec.IsSatisfiedBy(candidate));
			}

			public override string ToString()
			{
				return ToString("AND");
			}
		}

		/// <summary>
		///     Implementation of OR logic
		/// </summary>
		/// <typeparam name="T">Type of underlying object</typeparam>
		private sealed class OrSpecification<T> : CompositeSpecification<T>
		{
			public static ISpecification<T> Create(ISpecification<T> lhs, ISpecification<T> rhs)
			{
				if (lhs is TrueSpecification<T>)
				{
					return lhs;
				}
				if (rhs is TrueSpecification<T>)
				{
					return rhs;
				}
				if (lhs is FalseSpecification<T>)
				{
					return rhs;
				}
				if (rhs is FalseSpecification<T>)
				{
					return lhs;
				}
				var specifications = new List<ISpecification<T>>();
				specifications.AddRange(GetSpecifications<OrSpecification<T>>(lhs));
				specifications.AddRange(GetSpecifications<OrSpecification<T>>(rhs));
				return new OrSpecification<T>(specifications);
			}

			/// <summary>
			///     Initializes a new instance of the <see cref="OrSpecification{T}" /> class.
			/// </summary>
			/// <param name="specifications">The specifications to OR</param>
			private OrSpecification(IEnumerable<ISpecification<T>> specifications)
				: base(specifications)
			{
			}

			public override bool IsSatisfiedBy(T candidate)
			{
				return Specifications.Any(spec => spec.IsSatisfiedBy(candidate));
			}

			public override string ToString()
			{
				return ToString("OR");
			}
		}

		/// <summary>
		///     Implementation of XOR logic
		/// </summary>
		/// <typeparam name="T">Type of underlying object</typeparam>
		private sealed class XorSpecification<T> : CompositeSpecification<T>
		{
			public static ISpecification<T> Create(ISpecification<T> lhs, ISpecification<T> rhs)
			{
				var specifications = new List<ISpecification<T>>();
				specifications.AddRange(GetSpecifications<XorSpecification<T>>(lhs));
				specifications.AddRange(GetSpecifications<XorSpecification<T>>(rhs));
				return new XorSpecification<T>(specifications);
			}

			/// <summary>
			///     Initializes a new instance of the <see cref="XorSpecification{T}" /> class.
			/// </summary>
			/// <param name="specifications">The specifications to XOR</param>
			private XorSpecification(IEnumerable<ISpecification<T>> specifications)
				: base(specifications)
			{
			}

			public override bool IsSatisfiedBy(T candidate)
			{
				// True if odd number of results are true
				var results = Specifications.Select(spec => spec.IsSatisfiedBy(candidate));
				return (results.Count(r => r) & 1) == 1;
			}

			public override string ToString()
			{
				return ToString("XOR");
			}
		}

		/// <summary>
		///     Create specification from a function delegate
		/// </summary>
		/// <typeparam name="T">Type of underlying object</typeparam>
		private sealed class PredicateSpecification<T> : ISpecification<T>
		{
			private readonly Func<T, bool> _predicate;

			/// <summary>
			///     Initializes a new instance of the <see cref="PredicateSpecification{T}" /> class.
			/// </summary>
			/// <param name="predicate">The predicate.</param>
			public PredicateSpecification(Func<T, bool> predicate)
			{
				_predicate = predicate;
			}

			public bool IsSatisfiedBy(T candidate)
			{
				return _predicate(candidate);
			}
		}

		/// <summary>
		///     Class for specifications which compare a simple property to a list of possible values.
		/// </summary>
		private sealed class PropertyIsOneOfSpecification<T, TProp> : ISpecification<T>
		{
			private Func<T, TProp> _getPropertyFunction { get; set; }

			private ISet<TProp> _expectedValues { get; set; }

			/// <summary>
			///     Constructor
			/// </summary>
			/// <param name="getPropertyFunction">Function to return property from candidate</param>
			/// <param name="expectedValues">Values to compare</param>
			public PropertyIsOneOfSpecification(Func<T, TProp> getPropertyFunction, params TProp[] expectedValues)
			{
				_getPropertyFunction = getPropertyFunction;
				_expectedValues = new HashSet<TProp>(expectedValues);
			}

			public bool IsSatisfiedBy(T candidate)
			{
				var value = _getPropertyFunction(candidate);
				return _expectedValues.Contains(value);
			}
		}
	}
}
