using Atrico.Lib.Assertions;
using Atrico.Lib.BusinessLogic.Specifications;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.Mocks;
using Atrico.Lib.Testing.NUnitAttributes;
using Moq;

namespace Atrico.Lib.BusinessLogic.Tests
{
	public class TestSpecifications<T> : TestPODTypes<T>
	{
		[Test]
		public void TestSpecification()
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var mockSpecification = new Mock<ISpecification<T>>();
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
			var specification = Specification.Create<T>(mockAction.Object.Predicate);

			// Act
			specification.IsSatisfiedBy(candidate);

			// Assert
			mockAction.Verify(a => a.Predicate(candidate), Times.Once());
		}

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
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("Not {0}", operand));
		}

		[Test]
		public void TestSpecificationAnd([Values(false, true)] bool p1, [Values(false, true)] bool p2, [Values(false, true)] bool p3)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var specification1 = Specification.Create<T>(__ => p1);
			var specification2 = Specification.Create<T>(__ => p2);
			var specification3 = Specification.Create<T>(__ => p3);
			var specification = specification1.And(specification2).And(specification3);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = p1 && p2 && p3;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} AND {1} AND {2}", p1, p2, p3));
		}

		[Test]
		public void TestSpecificationAndConstant()
		{
			// Arrange
			var specificationT = Specification.Create<T>(__ => true);
			var specificationF = Specification.Create<T>(__ => false);
			var specificationC = Specification.False<T>();

			// Act
			var specification = specificationT.And(specificationC).And(specificationF);

			// Assert
			Assert.That(specification, Is.TypeOf(specificationC.GetType()));
		}

		[Test]
		public void TestSpecificationOr([Values(false, true)] bool p1, [Values(false, true)] bool p2, [Values(false, true)] bool p3)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var specification1 = Specification.Create<T>(__ => p1);
			var specification2 = Specification.Create<T>(__ => p2);
			var specification3 = Specification.Create<T>(__ => p3);
			var specification = specification1.Or(specification2).Or(specification3);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = p1 || p2 || p3;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} OR {1} OR {2}", p1, p2, p3));
		}

		[Test]
		public void TestSpecificationOrConstant()
		{
			// Arrange
			var specificationT = Specification.Create<T>(__ => true);
			var specificationF = Specification.Create<T>(__ => false);
			var specificationC = Specification.True<T>();

			// Act
			var specification = specificationT.Or(specificationC).Or(specificationF);

			// Assert
			Assert.That(specification, Is.TypeOf(specificationC.GetType()));
		}

		[Test]
		public void TestSpecificationXor([Values(false, true)] bool p1, [Values(false, true)] bool p2, [Values(false, true)] bool p3)
		{
			// Arrange
			var candidate = RandomValues.Value<T>();
			var specification1 = Specification.Create<T>(__ => p1);
			var specification2 = Specification.Create<T>(__ => p2);
			var specification3 = Specification.Create<T>(__ => p3);
			var specification = specification1.Xor(specification2).Xor(specification3);

			// Act
			var isSatisfied = specification.IsSatisfiedBy(candidate);

			// Assert
			var expected = p1 ^ p2 ^ p3;
			Assert.That(isSatisfied, Is.EqualTo(expected), string.Format("{0} XOR {1} XOR {2}", p1, p2, p3));
		}
	}
}
