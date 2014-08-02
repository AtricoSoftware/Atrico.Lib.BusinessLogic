using Atrico.Lib.RulesEngine.Specifications;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.NUnitAttributes;
using Assert = Atrico.Lib.Assertions.Assert;
using Is = Atrico.Lib.Assertions.Is;

namespace Atrico.Lib.BusinessLogic.Tests
{
	public class TestSpecificationStatics<T> : TestPODTypes<T>
	{
		[Test]
		public void TestSpecificationNot([Values(false, true)]bool operand)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var innerSpecification = new PredicateSpecification<T>(__ => operand);
			var specification = Specification.Not(innerSpecification);

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
			var specification = Specification.And(lhsSpecification, rhsSpecification);

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
			var specification = Specification.Or(lhsSpecification, rhsSpecification);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = lhs || rhs;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} Or {1}", lhs, rhs));
		}
	}
}