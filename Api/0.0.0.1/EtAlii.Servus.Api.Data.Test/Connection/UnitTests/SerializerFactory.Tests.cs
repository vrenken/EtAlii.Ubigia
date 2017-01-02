namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System.Linq;
    using System.Text;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Newtonsoft.Json;

    [TestClass]
    public class SerializerFactory_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SerializerFactory_New()
        {
            // Arrange.

            // Act.
            var factory = new SerializerFactory();

            // Assert.
            Assert.IsNotNull(factory);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SerializerFactory_Create()
        {
            // Arrange.
            var factory = new SerializerFactory();

            // Act.
            var serializer = factory.Create();

            // Assert.
            Assert.IsNotNull(serializer);
            Assert.AreEqual(DefaultValueHandling.IgnoreAndPopulate, serializer.DefaultValueHandling);
            Assert.IsTrue(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(UnsignedLongJSonConverter)) != null);
        }
    }
}
