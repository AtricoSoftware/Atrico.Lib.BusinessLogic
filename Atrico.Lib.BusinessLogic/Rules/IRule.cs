﻿namespace Atrico.Lib.BusinessLogic.Rules
{
	/// <summary>
	///     Interface to typed rule to be used in Rule Engine
	/// </summary>
	/// <typeparam name="T">Type of underlying object</typeparam>
	public interface IRule<in T>
	{
		/// <summary>
		///     Process this rule for the given object
		/// </summary>
		/// <param name="subject">Object for which to process rule</param>
		void Process(T subject);
	}
}
