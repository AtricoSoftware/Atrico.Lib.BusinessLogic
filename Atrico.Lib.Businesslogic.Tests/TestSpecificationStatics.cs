using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.BusinessLogic.Specifications;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;

namespace Atrico.Lib.BusinessLogic.Tests
{
	public class TestSpecificationStatics<T> : TestPODTypes<T>
	{
		[Test]
		public void TestSpecificationNot([Values(false, true)] bool operand)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var innerSpecification = Specification.Create<T>(__ => operand);
			var specification = innerSpecification.Not();

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = !operand;
			Assert.That(Value.Of(isSatisfied).Is().EqualTo(expected), string.Format("Not {0}", operand));
		}

		[Test]
		public void TestSpecificationAnd([Values(false, true)] bool lhs, [Values(false, true)] bool rhs)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var lhsSpecification = Specification.Create<T>(__ => lhs);
			var rhsSpecification = Specification.Create<T>(__ => rhs);
			var specification = lhsSpecification.And(rhsSpecification);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = lhs && rhs;
			Assert.That(Value.Of(isSatisfied).Is().EqualTo(expected), string.Format("{0} And {1}", lhs, rhs));
		}

		[Test]
		public void TestSpecificationOr([Values(false, true)] bool lhs, [Values(false, true)] bool rhs)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var lhsSpecification = Specification.Create<T>(__ => lhs);
			var rhsSpecification = Specification.Create<T>(__ => rhs);
			var specification = lhsSpecification.Or(rhsSpecification);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = lhs || rhs;
			Assert.That(Value.Of(isSatisfied).Is().EqualTo(expected), string.Format("{0} Or {1}", lhs, rhs));
		}
	}
}
