namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;
    using Newtonsoft.Json;

    
    public class SerializerFactory_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void SerializerFactory_New()
        {
            // Arrange.

            // Act.
            var factory = new SerializerFactory();

            // Assert.
            Assert.NotNull(factory);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SerializerFactory_Create()
        {
            // Arrange.
            var factory = new SerializerFactory();

            // Act.
            var serializer = factory.Create();

            // Assert.
            Assert.NotNull(serializer);
            Assert.Equal(DefaultValueHandling.IgnoreAndPopulate, serializer.DefaultValueHandling);
            Assert.True(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(UnsignedLongJSonConverter)) != null);
            Assert.True(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(PropertyDictionaryJSonConverter)) != null);
            Assert.True(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(DecimalJSonConverter)) != null);
        }
    }
}
