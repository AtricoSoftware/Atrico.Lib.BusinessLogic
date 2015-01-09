using System;
using System.Collections.Generic;
using System.Text;
using Atrico.Lib.BusinessLogic.Rules;
using Atrico.Lib.BusinessLogic.Specifications;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.Mocks;
using Atrico.Lib.Testing.NUnitAttributes;
using Moq;

namespace Atrico.Lib.BusinessLogic.Tests
{
	public class TestRules<T> : TestPODTypes<T>
	{
		[Test]
		public void TestAction()
		{
			// Arrange
			var subject = RandomValues.Value<T>();
			var mockAction = new Mock<IInvokeDelegate>();
			var rule = Rule.Create<T>(mockAction.Object.Action);

			// Act
			rule.Process(subject);

			// Assert
			mockAction.Verify(a => a.Action(subject), Times.Once());
		}

		[Test]
		public void TestFollowOnAction()
		{
			// Arrange
			var subject = RandomValues.Value<T>();
			var mockAction2 = new Mock<IInvokeDelegate>();
			var rule2 = Rule.Create<T>(mockAction2.Object.Action);
			var mockAction1 = new Mock<IInvokeDelegate>();
			var rule1 = Rule.Create<T>(mockAction1.Object.Action).FollowOn(rule2);

			// Act
			rule1.Process(subject);

			// Assert
			mockAction1.Verify(a => a.Action(subject), Times.Once());
			mockAction2.Verify(a => a.Action(subject), Times.Once());
		}

		[Test]
		public void TestDecisionSatisfied()
		{
			// Arrange
			var subject = RandomValues.Value<T>();
			var mockActionSatisfied = new Mock<IInvokeDelegate>();
			var ruleSatisfied = Rule.Create<T>(mockActionSatisfied.Object.Action);
			var mockActionUnsatisfied = new Mock<IInvokeDelegate>();
			var ruleUnsatisfied = Rule.Create<T>(mockActionUnsatisfied.Object.Action);
			var mockSpecification = new Mock<ISpecification<T>>();
			mockSpecification.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);
			var rule = Rule.Create(mockSpecification.Object, ruleSatisfied, ruleUnsatisfied);

			// Act
			rule.Process(subject);

			// Assert
			mockActionSatisfied.Verify(a => a.Action(subject), Times.Once());
			mockActionUnsatisfied.Verify(a => a.Action(subject), Times.Never());
		}

		[Test]
		public void TestDecisionUnsatisfied()
		{
			// Arrange
			var subject = RandomValues.Value<T>();
			var mockActionSatisfied = new Mock<IInvokeDelegate>();
			var ruleSatisfied = Rule.Create<T>(mockActionSatisfied.Object.Action);
			var mockActionUnsatisfied = new Mock<IInvokeDelegate>();
			var ruleUnsatisfied = Rule.Create<T>(mockActionUnsatisfied.Object.Action);
			var mockSpecification = new Mock<ISpecification<T>>();
			mockSpecification.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(false);
			var rule = Rule.Create(mockSpecification.Object, ruleSatisfied, ruleUnsatisfied);

			// Act
			rule.Process(subject);

			// Assert
			mockActionSatisfied.Verify(a => a.Action(subject), Times.Never());
			mockActionUnsatisfied.Verify(a => a.Action(subject), Times.Once());
		}

		[Test]
		public void TestForeachRule()
		{
			// Arrange
			const int count = 10;
			var subject = new HashSet<T>(RandomValues.Values<T>(count));
			var mockAction = new Mock<IInvokeDelegate>();
			var rule = Rule.CreateForeach(Rule.Create<T>(mockAction.Object.Action));

			// Act
			rule.Process(subject);

			// Assert
			foreach (var item in subject)
			{
				var item1 = item;
				mockAction.Verify(a => a.Action(item1), Times.Once());
			}
		}
		[Test]
		public void TestChainOfResponsibilityRule()
		{
			// Arrange
			var subject = RandomValues.Value<T>();
			var mockActionTrue = new Mock<IInvokeDelegate>();
			var ruleTrue = Rule.Create<T>(mockActionTrue.Object.Action);
			var mockActionFalse = new Mock<IInvokeDelegate>();
			var ruleFalse = Rule.Create<T>(mockActionFalse.Object.Action);
			var rule = Rule.CreateChainOfResponsibility(new []
			{
				Tuple.Create(Specification.False<T>(), ruleFalse),
				Tuple.Create(Specification.False<T>(), ruleFalse),
				Tuple.Create(Specification.True<T>(), ruleTrue),
				Tuple.Create(Specification.True<T>(), ruleFalse)
			});

			// Act
			rule.Process(subject);

			// Assert
				mockActionTrue.Verify(a => a.Action(subject), Times.Once());
				mockActionFalse.Verify(a => a.Action(It.IsAny<T>()), Times.Never);
		}
	}
}
