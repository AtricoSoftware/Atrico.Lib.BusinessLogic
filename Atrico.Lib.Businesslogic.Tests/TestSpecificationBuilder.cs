using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.BusinessLogic.Implementation.Specifications;
using Atrico.Lib.BusinessLogic.Specifications;
using Atrico.Lib.BusinessLogic.Specifications.Builder;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;
using Moq;

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

            // Act
            var builder = new SimpleSpecificationBuilder<T>(() => spec);
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(specification).Is().ReferenceEqualTo(spec), "Correct spec");
        }

        #region And

        [Test]
        public void TestBuildAndEmpty()
        {
            // Arrange

            // Act
            var builder = new SpecificationBuilderAnd<T>();
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(0), "No of Items");
            Assert.That(Value.Of(specification).Is().TypeOf(Specification.False<T>().GetType()), "Spec = False");
        }

        [Test]
        public void TestBuildAndSingleElement()
        {
            // Arrange
            var spec = Specification.True<T>();
            var builder1 = new SimpleSpecificationBuilder<T>(() => spec);

            // Act
            var builder = new SpecificationBuilderAnd<T>()
                .Add(builder1);
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(1), "No of Items");
            Assert.That(Value.Of(specification).Is().ReferenceEqualTo(spec), "Correct spec");
        }

        [Test]
        public void TestBuildAndMultipleElements()
        {
            const int count = 5;
            // Arrange
            var mocks = new List<Mock<ISpecification<T>>>();
            for (var i = 0; i < count; ++i)
            {
                var spec = new Mock<ISpecification<T>>();
                spec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);
                mocks.Add(spec);
            }

            // Act
            var builder = mocks.Aggregate(new SpecificationBuilderAnd<T>() as ISpecificationContainerBuilder<T>, (current, item) => current.Add(new SimpleSpecificationBuilder<T>(() => item.Object)));
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(count), "No of Items");
            Assert.That(Value.Of(specification).Is().TypeOf(typeof(AndSpecification<T>)), "Correct spec type");
            Assert.That(Value.Of(specification.IsSatisfiedBy(RandomValues.Value<T>())).Is().True(), "IsSatisfied = true");
            var idx = 0;
            foreach (var mock in mocks)
            {
                mock.Verify(s => s.IsSatisfiedBy(It.IsAny<T>()), Times.Once(), string.Format("Mock {0} not called", idx++));
            }
        }

        #endregion

        #region Or

        [Test]
        public void TestBuildOrEmpty()
        {
            // Arrange

            // Act
            var builder = new SpecificationBuilderOr<T>();
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(0), "No of Items");
            Assert.That(Value.Of(specification).Is().TypeOf(Specification.False<T>().GetType()), "Spec = False");
        }

        [Test]
        public void TestBuildOrSingleElement()
        {
            // Arrange
            var spec = Specification.True<T>();
            var builder1 = new SimpleSpecificationBuilder<T>(() => spec);

            // Act
            var builder = new SpecificationBuilderOr<T>()
                .Add(builder1);
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(1), "No of Items");
            Assert.That(Value.Of(specification).Is().ReferenceEqualTo(spec), "Correct spec");
        }

        [Test]
        public void TestBuildOrMultipleElements()
        {
            const int count = 5;
            // Arrange
            var mocks = new List<Mock<ISpecification<T>>>();
            for (var i = 0; i < count; ++i)
            {
                var spec = new Mock<ISpecification<T>>();
                spec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(false);
                mocks.Add(spec);
            }

            // Act
            var builder = mocks.Aggregate(new SpecificationBuilderOr<T>() as ISpecificationContainerBuilder<T>, (current, item) => current.Add(new SimpleSpecificationBuilder<T>(() => item.Object)));
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(count), "No of Items");
            Assert.That(Value.Of(specification).Is().TypeOf(typeof(OrSpecification<T>)), "Correct spec type");
            Assert.That(Value.Of(specification.IsSatisfiedBy(RandomValues.Value<T>())).Is().False(), "IsSatisfied = false");
            var idx = 0;
            foreach (var mock in mocks)
            {
                mock.Verify(s => s.IsSatisfiedBy(It.IsAny<T>()), Times.Once(), string.Format("Mock {0} not called", idx++));
            }
        }

        #endregion

        #region Xor

        [Test]
        public void TestBuildXorEmpty()
        {
            // Arrange

            // Act
            var builder = new SpecificationBuilderXor<T>();
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(0), "No of Items");
            Assert.That(Value.Of(specification).Is().TypeOf(Specification.False<T>().GetType()), "Spec = False");
        }

        [Test]
        public void TestBuildXorSingleElement()
        {
            // Arrange
            var spec = Specification.True<T>();
            var builder1 = new SimpleSpecificationBuilder<T>(() => spec);

            // Act
            var builder = new SpecificationBuilderXor<T>()
                .Add(builder1);
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(1), "No of Items");
            Assert.That(Value.Of(specification).Is().ReferenceEqualTo(spec), "Correct spec");
        }

        [Test]
        public void TestBuildXorMultipleElements()
        {
            const int count = 5;
            // Arrange
            var mocks = new List<Mock<ISpecification<T>>>();
            for (var i = 0; i < count; ++i)
            {
                var spec = new Mock<ISpecification<T>>();
                spec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);
                mocks.Add(spec);
            }

            // Act
            var builder = mocks.Aggregate(new SpecificationBuilderXor<T>() as ISpecificationContainerBuilder<T>, (current, item) => current.Add(new SimpleSpecificationBuilder<T>(() => item.Object)));
            var specification = builder.Build();

            // Assert
            Assert.That(Value.Of(builder.Items).Count().Is().EqualTo(count), "No of Items");
            Assert.That(Value.Of(specification).Is().TypeOf(typeof(XorSpecification<T>)), "Correct spec type");
            const bool expected = (count & 0x01) == 0x01;
            Assert.That(Value.Of(specification.IsSatisfiedBy(RandomValues.Value<T>())).Is().EqualTo(expected), "IsSatisfied = {0}", expected);
            var idx = 0;
            foreach (var mock in mocks)
            {
                mock.Verify(s => s.IsSatisfiedBy(It.IsAny<T>()), Times.Once(), string.Format("Mock {0} not called", idx++));
            }
        }

        #endregion
    }
}