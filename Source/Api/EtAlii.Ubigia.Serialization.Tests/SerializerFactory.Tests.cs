// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests
{
    using System.Linq;
    using Newtonsoft.Json;
    using Xunit;

    public class SerializerFactoryTests
    {
        [Fact]
        public void SerializerFactory_New()
        {
            // Arrange.

            // Act.
            var factory = new SerializerFactory();

            // Assert.
            Assert.NotNull(factory);
        }

        [Fact]
        public void SerializerFactory_Create()
        {
            // Arrange.
            var factory = new SerializerFactory();

            // Act.
            var serializer = factory.Create() as Serializer;

            // Assert.
            Assert.NotNull(serializer);
            Assert.Equal(DefaultValueHandling.IgnoreAndPopulate, serializer.DefaultValueHandling);
            Assert.True(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(UnsignedLongJSonConverter)) != null);
            Assert.True(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(PropertyDictionaryJSonConverter)) != null);
            Assert.True(serializer.Converters.SingleOrDefault(c => c.GetType() == typeof(DecimalJSonConverter)) != null);
        }
    }
}