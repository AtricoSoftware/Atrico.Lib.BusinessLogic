using System;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.BusinessLogic.Rules;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;

namespace Atrico.Lib.BusinessLogic.Tests
{
	public class TestRulesFollowOn : TestFixtureBase
	{
		private readonly Action<object> _action = o => { };

		[Test]
		public void TestActionFollowsNoAction()
		{
			// Arrange
			var noAction = Rule.NoAction<object>();
			var action = Rule.Create(_action);

			// Act
			var result = noAction.FollowOn(action);

			// Assert
			Assert.That(Value.Of(result).Is().ReferenceEqualTo(action));
		}

		[Test]
		public void TestNoActionFollowsAction()
		{
			// Arrange
			var noAction = Rule.NoAction<object>();
			var action = Rule.Create(_action);

			// Act
			var result = action.FollowOn(noAction);

			// Assert
			Assert.That(Value.Of(result).Is().ReferenceEqualTo(action));
		}
	}
}
