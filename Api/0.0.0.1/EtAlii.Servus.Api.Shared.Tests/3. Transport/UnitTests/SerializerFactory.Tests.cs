namespace EtAlii.Servus.Api.Transport.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.IsTrue(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(PropertyDictionaryJSonConverter)) != null);
            Assert.IsTrue(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(DecimalJSonConverter)) != null);
        }
    }
}
