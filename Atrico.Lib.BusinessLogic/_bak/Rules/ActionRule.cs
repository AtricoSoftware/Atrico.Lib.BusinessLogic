using System;

namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Action node
	/// </summary>
	/// <typeparam name="T">Underlying object type</typeparam>
	public class ActionRule<T> : RuleBase<T>
	{
		private Func<T, bool> _action { get; set; }

		/// <summary>
		///     Constructor - Non Terminal rule
		/// </summary>
		/// <param name="action">Action to carry out</param>
		public ActionRule(Action<T> action)
			: this(s =>
			       {
				       action(s);
				       return true;
			       })
		{
		}

		/// <summary>
		///     Constructor - Non Terminal rule
		/// </summary>
		/// <param name="action">Action to carry out</param>
		public ActionRule(Func<T, bool> action)
		{
			_action = action;
		}

		public override bool Process(T subject)
		{
			return _action.Invoke(subject);
		}

		public override string ToString()
		{
			return GetType().Name;
		}
	}
}