namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class PayloadSerializer_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PayloadSerializer_New()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();

            // Act.
            var serializer = new PayloadSerializer(jsonSerializer);

            // Assert.
            Assert.IsNotNull(serializer);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PayloadSerializer_Serialize_String()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var serializer = new PayloadSerializer(jsonSerializer);
            const string source = "TwentyFourSeven";

            // Act.
            var bytes = serializer.Serialize(source);
            var result = serializer.Deserialize<string>(bytes);

            // Assert.
            Assert.AreEqual(source, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PayloadSerializer_Serialize_Integer()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var serializer = new PayloadSerializer(jsonSerializer);
            var source = new Random().Next(100, 100);

            // Act.
            var bytes = serializer.Serialize(source);
            var result = serializer.Deserialize<int>(bytes);

            // Assert.
            Assert.AreEqual(source, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PayloadSerializer_Serialize_UnsignedLongInteger()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var serializer = new PayloadSerializer(jsonSerializer);
            var source = (ulong)new Random().Next(100, 100);

            // Act.
            var bytes = serializer.Serialize(source);
            var result = serializer.Deserialize<ulong>(bytes);

            // Assert.
            Assert.AreEqual(source, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PayloadSerializer_Serialize_UnsignedLongInteger_Max()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var serializer = new PayloadSerializer(jsonSerializer);
            const ulong source = ulong.MaxValue - 1;

            // Act.
            var bytes = serializer.Serialize(source);
            var result = serializer.Deserialize<ulong>(bytes);

            // Assert.
            Assert.AreEqual(source, result);
        }
    }
}
