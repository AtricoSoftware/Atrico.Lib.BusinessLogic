using System.Collections.Generic;
using System.Linq;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.BusinessLogic.Specifications;
using Atrico.Lib.BusinessLogic.Specifications.Builder;
using Atrico.Lib.BusinessLogic.zzImplementation.Specifications;
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
    
        [Test]
        public void TestConvertContainers()
        {
            const int count = 5;
            // Arrange
            var specs = new List<ISpecificationBuilder<T>>();
            for (var i = 0; i < count; ++i)
            {
                specs.Add(new SimpleSpecificationBuilder<T>(Specification.True<T>));
            }

            // Act
            var builder = specs.Aggregate(new SpecificationBuilderAnd<T>() as ISpecificationContainerBuilder<T>, (current, item) => current.Add(item));
            var builderAndAnd = builder.Convert<SpecificationBuilderAnd<T>>();           
            var builderAndOr = builder.Convert<SpecificationBuilderOr<T>>();
            var builderAndXor = builder.Convert<SpecificationBuilderXor<T>>();
            var builderOrAnd = builderAndOr.Convert<SpecificationBuilderAnd<T>>();
            var builderOrOr = builderAndOr.Convert<SpecificationBuilderOr<T>>();
            var builderOrXor = builderAndXor.Convert<SpecificationBuilderXor<T>>();
            var builderXorAnd = builderAndXor.Convert<SpecificationBuilderAnd<T>>();
            var builderXorOr = builderAndXor.Convert<SpecificationBuilderOr<T>>();
            var builderXorXor = builderAndXor.Convert<SpecificationBuilderXor<T>>();
            
            // Assert
            Assert.That(Value.Of(builderAndAnd.Items).Count().Is().EqualTo(count), "No of Items (and-and)");
            Assert.That(Value.Of(builderAndAnd).Is().TypeOf(typeof(SpecificationBuilderAnd<T>)), "Correct builder type (and-and)");
            Assert.That(Value.Of(builderAndOr.Items).Count().Is().EqualTo(count), "No of Items (and-or)");
            Assert.That(Value.Of(builderAndOr).Is().TypeOf(typeof(SpecificationBuilderOr<T>)), "Correct builder type (and-or)");
            Assert.That(Value.Of(builderAndXor.Items).Count().Is().EqualTo(count), "No of Items (and-xor)");
            Assert.That(Value.Of(builderAndXor).Is().TypeOf(typeof(SpecificationBuilderXor<T>)), "Correct builder type (and-xor)");
            Assert.That(Value.Of(builderOrAnd.Items).Count().Is().EqualTo(count), "No of Items (or-and)");
            Assert.That(Value.Of(builderOrAnd).Is().TypeOf(typeof(SpecificationBuilderAnd<T>)), "Correct builder type (or-and)");
            Assert.That(Value.Of(builderOrOr.Items).Count().Is().EqualTo(count), "No of Items (or-or)");
            Assert.That(Value.Of(builderOrOr).Is().TypeOf(typeof(SpecificationBuilderOr<T>)), "Correct builder type (or-or)");
            Assert.That(Value.Of(builderOrXor.Items).Count().Is().EqualTo(count), "No of Items (or-xor)");
            Assert.That(Value.Of(builderOrXor).Is().TypeOf(typeof(SpecificationBuilderXor<T>)), "Correct builder type (or-xor)");
            Assert.That(Value.Of(builderXorAnd.Items).Count().Is().EqualTo(count), "No of Items (xor-and)");
            Assert.That(Value.Of(builderXorAnd).Is().TypeOf(typeof(SpecificationBuilderAnd<T>)), "Correct builder type (xor-and)");
            Assert.That(Value.Of(builderXorOr.Items).Count().Is().EqualTo(count), "No of Items (xor-or)");
            Assert.That(Value.Of(builderXorOr).Is().TypeOf(typeof(SpecificationBuilderOr<T>)), "Correct builder type (xor-or)");
            Assert.That(Value.Of(builderXorXor.Items).Count().Is().EqualTo(count), "No of Items (xor-xor)");
            Assert.That(Value.Of(builderXorXor).Is().TypeOf(typeof(SpecificationBuilderXor<T>)), "Correct builder type (xor-xor)");
        }
        
    }
}