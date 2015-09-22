using System;
using Atrico.Lib.BusinessLogic.Specifications.Implementation;

namespace Atrico.Lib.BusinessLogic.Specifications
{
	/// <summary>
	///     Helper function for specifications
	/// </summary>
	public static class Specification
	{
		/// <summary>
		///     Creates a new TRUE specification
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <returns>New Specification</returns>
		public static ISpecification<T> True<T>()
		{
			return new TrueSpecification<T>();
		}

		/// <summary>
		///     Creates a new FALSE specification
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <returns>New Specification</returns>
		public static ISpecification<T> False<T>()
		{
			return new FalseSpecification<T>();
		}

		/// <summary>
		///     Create a new specification which is the negation of lhs
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <returns>New specification negating this one</returns>
		/// <returns>New specification representing NOT rhs</returns>
		public static ISpecification<T> Not<T>(this ISpecification<T> rhs)
		{
			if (rhs is TrueSpecification<T>)
			{
				return new FalseSpecification<T>();
			}
			if (rhs is FalseSpecification<T>)
			{
				return True<T>();
			}
			return NotSpecification<T>.Create(rhs);
		}

		/// <summary>
		///     Create a new specification which is the logical AND of 2 specifications
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <param name="lhs">LHS specification</param>
		/// <param name="rhs">RHS specification</param>
		/// <returns>New specification representing lhs AND rhs</returns>
		public static ISpecification<T> And<T>(this ISpecification<T> lhs, ISpecification<T> rhs)
		{
			return AndSpecification<T>.Create(lhs, rhs);
		}

		/// <summary>
		///     Create a new specification which is the logical OR of 2 specifications
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <param name="lhs">LHS specification</param>
		/// <param name="rhs">RHS specification</param>
		/// <returns>New specification representing lhs OR rhs</returns>
		public static ISpecification<T> Or<T>(this ISpecification<T> lhs, ISpecification<T> rhs)
		{
			return OrSpecification<T>.Create(lhs, rhs);
		}

		/// <summary>
		///     Create a new specification which is the logical OR of 2 specifications
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <param name="lhs">LHS specification</param>
		/// <param name="rhs">RHS specification</param>
		/// <returns>New specification representing lhs OR rhs</returns>
		public static ISpecification<T> Xor<T>(this ISpecification<T> lhs, ISpecification<T> rhs)
		{
			return XorSpecification<T>.Create(lhs, rhs);
		}

		/// <summary>
		/// Creates a specification from a predicate.
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <param name="predicate">The predicate.</param>
		/// <returns>New specification</returns>
		public static ISpecification<T> Create<T>(Func<T, bool> predicate)
		{
			return new PredicateSpecification<T>(predicate);
		}

		/// <summary>
		/// Creates a specification to check property is one of
		/// </summary>
		/// <typeparam name="T">Candidate type of specification</typeparam>
		/// <typeparam name="TProp">Type of property</typeparam>
		/// <param name="getPropertyFunction">Function to return property</param>
		/// <param name="expectedValues">Values of property</param>
		/// <returns>New specification</returns>
		public static ISpecification<T> Create<T, TProp>(Func<T, TProp> getPropertyFunction, params TProp[] expectedValues)
		{
			return new PropertyIsOneOfSpecification<T, TProp>(getPropertyFunction, expectedValues);
		}
	}
}
