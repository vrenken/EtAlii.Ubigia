namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class PropertyDictionaryCreateTests
    {
        [Fact]
        public void PropertyDictionary_Create()
        {
            // Arrange.

            // Act.
            var properties = new PropertyDictionary();

            // Assert.
            Assert.NotNull(properties);
            Assert.Equal(string.Empty, properties.ToString());
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public void PropertyDictionary_Create_With_Double()
        {
            // Arrange.
            var properties = new PropertyDictionary();
            var value = new Random().NextDouble();

            // Act.
            properties["Key"] = value;

            // Assert.
            Assert.Equal(value, properties["Key"]);
            Assert.Equal($"Key: \"{value}\"", properties.ToString());
        }


        [Fact]
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
            Assert.Equal($"Key1: \"{value1}\" - Key2: \"{value2}\"", properties.ToString());
        }


        [Fact]
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

        [Fact]
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

        [Fact]
        public void PropertyDictionary_Create_With_Two_Strings_From_Other_Dictionary()
        {
            // Arrange.
            var properties1 = new PropertyDictionary();
            var value1 = Guid.NewGuid().ToString();
            var value2 = Guid.NewGuid().ToString();
            properties1["Key1"] = value1;
            properties1["Key2"] = value2;

            // Act.
            var properties2 = new PropertyDictionary(properties1);

            // Assert.
            Assert.Equal(value1, properties2["Key1"]);
            Assert.Equal(value2, properties2["Key2"]);
        }
    }
}
