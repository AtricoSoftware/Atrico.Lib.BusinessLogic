using System;
using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.RulesEngine.Specifications;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Creator class for rules
	/// </summary>
	public class Rule
	{
		/// <summary>
		///	Create No Action rule
		/// </summary>
		/// <typeparam name="T">Type of action</typeparam>
		/// <returns>Rule that performs no action</returns>
		public static IRule<T> NoAction<T>()
		{
			return new NoActionRule<T>();
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
			var satisfiedRuleNotNull = ifSatisfied ?? new NoActionRule<T>();
			var unsatisfiedRuleNotNull = ifNotSatisfied ?? new NoActionRule<T>();
			return new DecisionRule<T>(specification, satisfiedRuleNotNull, unsatisfiedRuleNotNull);
		}

		/// <summary>
		///     Create a decision node (using actions rather than rules)
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="specification">Specification to test</param>
		/// <param name="ifSatisfied">Action to process if specification is satisfied</param>
		/// <param name="ifNotSatisfied">Action to process if specification is not satisfied</param>
		/// <returns>New Rule</returns>
		public static IRule<T> Create<T>(ISpecification<T> specification, Action<T> ifSatisfied, Action<T> ifNotSatisfied)
		{
			return Create(specification, Create(ifSatisfied), Create(ifNotSatisfied));
		}

		/// <summary>
		///     Create a decision node (mix of actions and rules)
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="specification">Specification to test</param>
		/// <param name="ifSatisfied">Action to process if specification is satisfied</param>
		/// <param name="ifNotSatisfied">Rule to process if specification is not satisfied</param>
		/// <returns>New Rule</returns>
		public static IRule<T> Create<T>(ISpecification<T> specification, Action<T> ifSatisfied, IRule<T> ifNotSatisfied)
		{
			return Create(specification, Create(ifSatisfied), ifNotSatisfied);
		}

		/// <summary>
		///     Create a decision node (mix of actions and rules)
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="specification">Specification to test</param>
		/// <param name="ifSatisfied">Rule to process if specification is satisfied</param>
		/// <param name="ifNotSatisfied">Action to process if specification is not satisfied</param>
		/// <returns>New Rule</returns>
		public static IRule<T> Create<T>(ISpecification<T> specification, IRule<T> ifSatisfied, Action<T> ifNotSatisfied)
		{
			return Create(specification, ifSatisfied, Create(ifNotSatisfied));
		}

		/// <summary>
		///     Create a terminal action node
		/// </summary>
		/// <typeparam name="T">Underlying type</typeparam>
		/// <param name="action">Action to carry out</param>
		/// <returns>New Rule</returns>
		public static IRule<T> Create<T>(Action<T> action = null)
		{
			return action != null ? new ActionRule<T>(action) : new NoActionRule<T>() as IRule<T>;
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
		///     Create rule that operates on each member of a collection
		/// </summary>
		/// <typeparam name="T">Type of item in collection</typeparam>
		/// <param name="action">Action to perform for each item</param>
		/// <returns>New Rule</returns>
		public static IRule<IEnumerable<T>> CreateForeach<T>(Action<T> action = null)
		{
			return action != null ? new ForeachRule<T>(new ActionRule<T>(action)) : new NoActionRule<IEnumerable<T>>() as IRule<IEnumerable<T>>;
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
			var firstRule = Create<T>(null);
			DecisionRule<T> lastRule = null;
			foreach (
				var rule in entries.Select(entry => new DecisionRule<T>(entry.Item1, entry.Item2)))
			{
				// Add to chain
				if (lastRule == null)
					firstRule = rule;
				else
					lastRule.UnsatisfiedRule = rule;
				lastRule = rule;
			}
			return firstRule;
		}
	}
}