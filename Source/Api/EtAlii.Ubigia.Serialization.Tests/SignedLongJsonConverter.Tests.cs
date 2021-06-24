// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.Ubigia.Tests;
    using Newtonsoft.Json.Bson;
    using Xunit;

    public class SignedLongJsonConverterTests
    {
        [Fact]
        public void SignedLongJsonConverter_Convert_Json()
        {
            // Arrange.
            const long startValue = 123456;
            var serializer = CreateSerializer();

            // Act.
            var jsonAsString = WriteString(startValue, serializer);
            var resultValue = ReadString<long>(serializer, jsonAsString);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Json_Random()
        {
            // Arrange.
            var valueAsBytes = new byte[8];
            new Random().NextBytes(valueAsBytes);
            var startValue = BitConverter.ToInt64(valueAsBytes, 0);
            var serializer = CreateSerializer();

            // Act.
            var jsonAsString = WriteString(startValue, serializer);
            var resultValue = ReadString<long>(serializer, jsonAsString);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Json_Max()
        {
            // Arrange.
            const long startValue = long.MaxValue - 1;
            var serializer = CreateSerializer();

            // Act.
            var jsonAsString = WriteString(startValue, serializer);
            var resultValue = ReadString<long>(serializer, jsonAsString);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Json_Min()
        {
            // Arrange.
            const long startValue = long.MinValue + 1;
            var serializer = CreateSerializer();

            // Act.
            var jsonAsString = WriteString(startValue, serializer);
            var resultValue = ReadString<long>(serializer, jsonAsString);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert()
        {
            // Arrange.
            const long startValue = 12345;
            var serializer = CreateSerializer();

            // Act.
            var jsonAsBytes = WriteBytes(startValue, serializer);
            var resultValue = ReadBytes<long>(serializer, jsonAsBytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Bson_Random()
        {
            // Arrange.
            var valueAsBytes = new byte[8];
            new Random().NextBytes(valueAsBytes);
            var startValue = BitConverter.ToInt64(valueAsBytes, 0);
            var serializer = CreateSerializer();

            // Act.
            var jsonAsBytes = WriteBytes(startValue, serializer);
            var resultValue = ReadBytes<long>(serializer, jsonAsBytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Bson_Max()
        {
            // Arrange.
            const long startValue = long.MaxValue - 1;
            var serializer = CreateSerializer();

            // Act.
            var jsonAsBytes = WriteBytes(startValue, serializer);
            var resultValue = ReadBytes<long>(serializer, jsonAsBytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Bson_Min()
        {
            // Arrange.
            const long startValue = long.MinValue + 1;
            var serializer = CreateSerializer();

            // Act.
            var jsonAsBytes = WriteBytes(startValue, serializer);
            var resultValue = ReadBytes<long>(serializer, jsonAsBytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        private string WriteString<T>(T value, ISerializer serializer)
        {
            using var writer = new StringWriter();

            var package = new TestPackage<T> { Value = value };
            serializer.Serialize(writer, package);
            return writer.ToString();
        }

        private T ReadString<T>(ISerializer serializer, string jsonAsString)
        {
            using var reader = new StringReader(jsonAsString);
            using var jsonReader = new Newtonsoft.Json.JsonTextReader(reader);

            var package = serializer.Deserialize<TestPackage<T>>(jsonReader);
            return package.Value;
        }

        private byte[] WriteBytes<T>(T value, ISerializer serializer)
        {
            using var stream = new MemoryStream();
            using var writer = new BsonDataWriter(stream);

            var package = new TestPackage<T> { Value = value };
            serializer.Serialize(writer, package);
            return stream.ToArray();
        }

        private T ReadBytes<T>(ISerializer serializer, byte[] jsonAsBytes)
        {
            using var reader = new MemoryStream(jsonAsBytes);
            using var jsonReader = new BsonDataReader(reader);

            var package = serializer.Deserialize<TestPackage<T>>(jsonReader);
            return package.Value;
        }

        private ISerializer CreateSerializer()
        {
            var serializer = new Serializer();
            serializer.Converters.Add(new UnsignedLongJSonConverter());
            return serializer;
        }
    }
}
