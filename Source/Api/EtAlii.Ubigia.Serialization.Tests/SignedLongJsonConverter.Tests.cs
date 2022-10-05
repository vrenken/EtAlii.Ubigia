// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
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

            // Act.
            var bytes = WriteBytes(startValue);
            var resultValue = ReadBytes<long>(bytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Binary_Random()
        {
            // Arrange.
            var expectedBytes = new byte[8];
            new Random().NextBytes(expectedBytes);
            var startValue = BitConverter.ToInt64(expectedBytes, 0);

            // Act.
            var bytes = WriteBytes(startValue);
            var resultValue = ReadBytes<long>(bytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Binary_Max()
        {
            // Arrange.
            const long startValue = long.MaxValue - 1;

            // Act.
            var bytes = WriteBytes(startValue);
            var resultValue = ReadBytes<long>(bytes);

            // Assert.
            Assert.Equal(startValue, resultValue);
        }

        [Fact]
        public void SignedLongJsonConverter_Convert_Binary_Min()
        {
            // Arrange.
            const long startValue = long.MinValue + 1;

            // Act.
            var bytes = WriteBytes(startValue);
            var resultValue = ReadBytes<long>(bytes);

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

        private byte[] WriteBytes<T>(T value)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);

            var package = new TestPackage<T> { Value = value };

            writer.Write<TestPackage<T>>(package);
            return stream.ToArray();
        }

        private T ReadBytes<T>(byte[] jsonAsBytes)
        {
            using var stream = new MemoryStream(jsonAsBytes);
            using var reader = new BinaryReader(stream, Encoding.UTF8);

            var package = reader.Read<TestPackage<T>>();
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
