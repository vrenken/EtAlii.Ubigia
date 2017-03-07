namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using System;
    using Xunit;

    public class PropertyDictionary_Create_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create()
        {
            // Arrange.

            // Act.
            var properties = new PropertyDictionary();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create_With_String()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value = Guid.NewGuid().ToString();

            // Act.
            properties["Key"] = value;

            // Assert.
            Assert.Equal(value, properties["Key"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create_With_Integer()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value = new Random().Next();

            // Act.
            properties["Key"] = value;

            // Assert.
            Assert.Equal(value, properties["Key"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create_With_Double()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value = new Random().NextDouble();

            // Act.
            properties["Key"] = value;

            // Assert.
            Assert.Equal(value, properties["Key"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create_With_Two_Doubles()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value1 = new Random().NextDouble();
            var value2 = new Random().NextDouble();

            // Act.
            properties["Key1"] = value1;
            properties["Key2"] = value2;

            // Assert.
            Assert.Equal(value1, properties["Key1"]);
            Assert.Equal(value2, properties["Key2"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create_With_Two_Integers()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value1 = new Random().Next();
            var value2 = new Random().Next();

            // Act.
            properties["Key1"] = value1;
            properties["Key2"] = value2;

            // Assert.
            Assert.Equal(value1, properties["Key1"]);
            Assert.Equal(value2, properties["Key2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Create_With_Two_Strings()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value1 = Guid.NewGuid().ToString();
            var value2 = Guid.NewGuid().ToString();

            // Act.
            properties["Key1"] = value1;
            properties["Key2"] = value2;

            // Assert.
            Assert.Equal(value1, properties["Key1"]);
            Assert.Equal(value2, properties["Key2"]);
        }
    }
}
