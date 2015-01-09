using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Atrico.Lib.BusinessLogic.Specifications;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Rule implementations for creators
	/// </summary>
	public static partial class Rule
	{
		/// <summary>
		///     Action node
		/// </summary>
		/// <typeparam name="T">Underlying object type</typeparam>
		private sealed class ActionRule<T> : IRule<T>
		{
			private Action<T> _action { get; set; }

			/// <summary>
			///     Constructor - Non Terminal rule
			/// </summary>
			/// <param name="action">Action to carry out</param>
			public ActionRule(Action<T> action)
			{
				_action = action;
			}

			public void Process(T subject)
			{
				_action(subject);
			}

			public override string ToString()
			{
				return GetType().Name;
			}
		}

		/// <summary>
		///     Decision node
		/// </summary>
		/// <typeparam name="T">Underlying object type</typeparam>
		private sealed class DecisionRule<T> : IRule<T>
		{
			private readonly ISpecification<T> _specification;
			private readonly IRule<T> _satisfiedRule;
			private readonly IRule<T> _unsatisfiedRule;

			/// <summary>
			///     Constructor
			/// </summary>
			/// <typeparam name="T">Underlying type</typeparam>
			/// <param name="specification">Specification to test</param>
			/// <param name="satisfiedRule">Rule to process if specification is satisfied</param>
			/// <param name="unsatisfiedRule">Rule to process if specification is not satisfied</param>
			public DecisionRule(ISpecification<T> specification, IRule<T> satisfiedRule = null, IRule<T> unsatisfiedRule = null)
			{
				_specification = specification;
				_satisfiedRule = satisfiedRule ?? NoAction<T>();
				_unsatisfiedRule = unsatisfiedRule ?? NoAction<T>();
			}

			public void Process(T subject)
			{
				if (_specification.IsSatisfiedBy(subject))
				{
					_satisfiedRule.Process(subject);
				}
				else
				{
					_unsatisfiedRule.Process(subject);
				}
			}

			public override string ToString()
			{
				return string.Format("({0}) ? ({1}) : ({2})", _specification, _satisfiedRule, _unsatisfiedRule);
			}
		}

		/// <summary>
		///     Rule that performs an action
		/// </summary>
		/// <typeparam name="T">Enumeration item type</typeparam>
		private sealed class ForeachRule<T> : IRule<IEnumerable<T>>
		{
			private readonly IRule<T> _rule;

			/// <summary>
			///     Constructor
			/// </summary>
			/// <param name="rule">Rule to execute for each item</param>
			public ForeachRule(IRule<T> rule)
			{
				_rule = rule;
			}

			public void Process(IEnumerable<T> subjects)
			{
				foreach (var subject in subjects)
				{
					_rule.Process(subject);
				}
			}
		}

		[DebuggerDisplay("MultipleRules Count = {_rules.Count}")]
		private sealed class MultipleRules<T> : IRule<T>
		{
			private IEnumerable<IRule<T>> _rules { get; set; }

			internal static IRule<T> Create(IRule<T> lhs, IRule<T> rhs)
			{
				var rules = new List<IRule<T>>();
				rules.AddRange(GetRules(lhs));
				rules.AddRange(GetRules(rhs));
				switch (rules.Count)
				{
					case 0:
						return NoAction<T>();

					case 1:
						return rules.First();

					default:
						return new MultipleRules<T>(rules);
				}
			}

			private MultipleRules(IEnumerable<IRule<T>> rules)
			{
				_rules = rules;
			}

			public void Process(T subject)
			{
				foreach (var rule in _rules)
				{
					rule.Process(subject);
				}
			}

			private static IEnumerable<IRule<T>> GetRules(IRule<T> rule)
			{
				if (rule == null || rule is NoActionRule<T>)
				{
					return new IRule<T>[] {};
				}
				var group = rule as MultipleRules<T>;
				return !ReferenceEquals(group, null) ? group._rules : new[] {rule};
			}
		}

		private sealed class NoActionRule<T> : IRule<T>
		{
			public void Process(T subject)
			{
				// Do nothing
			}
		}
	}
}
