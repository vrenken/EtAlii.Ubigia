// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests;

using System;
using System.Collections.Generic;
using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Transport;
using Xunit;
using EtAlii.Ubigia.Tests;
using Microsoft.Extensions.Configuration;

[CorrelateUnitTests]
public class FabricOptionsTests
{
    [Fact]
    public void FabricOptions_Create()
    {
        // Arrange.
        var settings = new Dictionary<string, string>
        {
            {"Service1:url", "http://somewhere"},
            {"Service1:port", "123"}
        };
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddInMemoryCollection(settings);
        var configurationRoot = configurationBuilder.Build();

        // Act.
        var options = new FabricOptions(configurationRoot);

        // Assert.
        Assert.NotNull(options);
    }

    [Fact]
    public void FabricOptions_UseCaching()
    {
        // Arrange.
        var settings = new Dictionary<string, string>
        {
            {"Service1:url", "http://somewhere"},
            {"Service1:port", "123"}
        };
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddInMemoryCollection(settings);
        var configurationRoot = configurationBuilder.Build();
        var options = new FabricOptions(configurationRoot);

        // Act.
        options.UseCaching(true);

        // Assert.
        Assert.True(options.CachingEnabled);
    }


    [Fact]
    public void FabricOptions_UseDataConnectionOptions()
    {
        // Arrange.
        var settings = new Dictionary<string, string>
        {
            {"Service1:url", "http://somewhere"},
            {"Service1:port", "123"}
        };
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddInMemoryCollection(settings);
        var configurationRoot = configurationBuilder.Build();
        var options = new FabricOptions(configurationRoot);

        // Act.
        var act  = new Action(() => options.Use((DataConnectionOptions)null));

        // Assert.
        Assert.Throws<ArgumentNullException>(act);
    }

}
