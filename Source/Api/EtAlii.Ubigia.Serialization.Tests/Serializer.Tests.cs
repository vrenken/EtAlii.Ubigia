// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests;

using System.Linq;
using Newtonsoft.Json;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class SerializerTests
{
    [Fact]
    public void Serializer_Default()
    {
        // Arrange.

        // Act.
        var serializer = Serializer.Default as Serializer;

        // Assert.
        Assert.NotNull(serializer);
        Assert.Equal(DefaultValueHandling.IgnoreAndPopulate, serializer.DefaultValueHandling);
        Assert.True(serializer.Converters.SingleOrDefault(c => c is UnsignedLongJSonConverter) != null);
        Assert.True(serializer.Converters.SingleOrDefault(c => c is PropertyDictionaryJSonConverter) != null);
        Assert.True(serializer.Converters.SingleOrDefault(c => c is DecimalJSonConverter) != null);
    }
}
