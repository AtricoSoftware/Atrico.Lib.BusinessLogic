using System;
using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Creator class for rules
	/// </summary>
	public static partial class Rule
	{
		/// <summary>
		///     Add a follow on rule to be processed after this one
		/// </summary>
		/// <param name="firstRule">First rule in chain</param>
		/// <param name="followOnRule">Rule to process after first one</param>
		/// <returns>New rule representing this follewed by other</returns>
		public static IRule<T> FollowOn<T>(this IRule<T> firstRule, IRule<T> followOnRule)
		{
			return MultipleRules<T>.Create(firstRule, followOnRule);
		}

		/// <summary>
		///     Create a decision node
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="specification">Specification to test</param>
		/// <param name="ifSatisfied">Rule to process if specification is satisfied</param>
		/// <param name="ifNotSatisfied">Rule to process if specification is not satisfied</param>
		/// <returns>New Rule</returns>
		public static IRule<T> Create<T>(ISpecification<T> specification, IRule<T> ifSatisfied, IRule<T> ifNotSatisfied)
		{
			var satisfiedRuleNotNull = ifSatisfied ?? NoAction<T>();
			var unsatisfiedRuleNotNull = ifNotSatisfied ?? NoAction<T>();
			return new DecisionRule<T>(specification, satisfiedRuleNotNull, unsatisfiedRuleNotNull);
		}

		/// <summary>
		///     Create a terminal action node
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="action">Action to carry out</param>
		/// <returns>New Rule</returns>
		public static IRule<T> Create<T>(Action<T> action)
		{
			return action != null ? new ActionRule<T>(action) : NoAction<T>();
		}

		/// <summary>
		///     Create rule that operates on each member of a collection
		/// </summary>
		/// <typeparam name="T">Type of item in collection</typeparam>
		/// <param name="rule">Rule to call for each item</param>
		/// <returns>New Rule</returns>
		public static IRule<IEnumerable<T>> CreateForeach<T>(IRule<T> rule)
		{
			return rule != null ? new ForeachRule<T>(rule) : new NoActionRule<IEnumerable<T>>() as IRule<IEnumerable<T>>;
		}

		/// <summary>
		///     Create rule that acts as a chain of responsibility
		///     The first specification that is satisfied has its rule used, then the chain terminates
		/// </summary>
		/// <typeparam name="T">Type of item in collection</typeparam>
		/// <param name="entries">Specification/rule pairs for chain</param>
		/// <returns>New Rule</returns>
		public static IRule<T> CreateChainOfResponsibility<T>(IEnumerable<Tuple<ISpecification<T>, IRule<T>>> entries)
		{
			return entries.Reverse().Aggregate<Tuple<ISpecification<T>, IRule<T>>, DecisionRule<T>>(null, (current, entry) => new DecisionRule<T>(entry.Item1, entry.Item2, current));
		}

		/// <summary>
		///     Create a null (no action) node
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <returns>New Rule</returns>
		public static IRule<T> NoAction<T>()
		{
			return new NoActionRule<T>();
		}
	}
}
