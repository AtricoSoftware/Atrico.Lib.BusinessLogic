using System.Collections;
using Atrico.Lib.RulesEngine.Specifications;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.Mocks;
using Moq;
using NUnit.Framework;
using Assert = Atrico.Lib.Assertions.Assert;
using Is = Atrico.Lib.Assertions.Is;

namespace Atrico.Lib.BusinessLogic.Tests
{
	public class TestSpecifications<T> : TestPODTypes<T>
	{
		[Test]
		public void TestSpecification()
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var mockSpecification = new Mock<Specification<T>>();
			var specification = mockSpecification.Object;

			// Act
			specification.IsSatisfiedBy(candidate);

			// Assert
			mockSpecification.Verify(s => s.IsSatisfiedBy(candidate), Times.Once());
		}

		[Test]
		public void TestSpecificationPredicate()
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var mockAction = new Mock<IInvokeDelegate>();
			var specification = new PredicateSpecification<T>(mockAction.Object.Predicate);

			// Act
			specification.IsSatisfiedBy(candidate);

			// Assert
			mockAction.Verify(a => a.Predicate(candidate), Times.Once());
		}

		[Test]
		public void TestSpecificationNot([Values(false, true)]bool operand)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var innerSpecification = new PredicateSpecification<T>(__ => operand);
			var specification = innerSpecification.Not();

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = !operand;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("Not {0}", operand));
		}

		[Test]
		public void TestSpecificationAnd([Values(false, true)]bool lhs, [Values(false, true)]bool rhs)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var lhsSpecification = new PredicateSpecification<T>(__ => lhs);
			var rhsSpecification = new PredicateSpecification<T>(__ => rhs);
			var specification = lhsSpecification.And(rhsSpecification);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = lhs && rhs;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} And {1}", lhs, rhs));
		}

		[Test]
		public void TestSpecificationOr([Values(false, true)]bool lhs, [Values(false, true)]bool rhs)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var lhsSpecification = new PredicateSpecification<T>(__ => lhs);
			var rhsSpecification = new PredicateSpecification<T>(__ => rhs);
			var specification = lhsSpecification.Or(rhsSpecification);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = lhs || rhs;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} Or {1}", lhs, rhs));
		}

		[Test]
		public void TestSpecificationXor([Values(false, true)]bool lhs, [Values(false, true)]bool rhs)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var lhsSpecification = new PredicateSpecification<T>(__ => lhs);
			var rhsSpecification = new PredicateSpecification<T>(__ => rhs);
			var specification = new SpecificationXor<T>(lhsSpecification, rhsSpecification);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = lhs ^ rhs;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} Xor {1}", lhs, rhs));
		}
	}
}