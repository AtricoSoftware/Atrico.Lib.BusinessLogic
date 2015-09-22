using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.BusinessLogic.Specifications;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;

namespace Atrico.Lib.BusinessLogic.Tests
{
    [TestFixture]
    public class TestSpecificationBuilder<T> : TestPODTypes<T>
    {
        [Test]
        public void TestBuildSimpleSpec()
        {
            // Arrange
            var spec = Specification.True<T>();
            var builder = SpecificationBuilder.Create(spec);

            // Act
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(specification).Is().ReferenceEqualTo(spec));
        }

        [Test]
        public void TestBuildSimpleSpecFromFactory()
        {
            // Arrange
            var builder = SpecificationBuilder.Create(Specification.False<T>);

            // Act
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(specification).Is().TypeOf(Specification.False<T>().GetType()));
        }
    }
}