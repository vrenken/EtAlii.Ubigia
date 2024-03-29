﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using System.Reflection;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Fabric.Diagnostics;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using Microsoft.Extensions.Configuration;
using Xunit;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;

[CorrelateUnitTests]
public class ProfilingLogicalContextTests : IClassFixture<LogicalUnitTestContext>
{
    private readonly LogicalUnitTestContext _testContext;

    public ProfilingLogicalContextTests(LogicalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task ProfilingLogicalContext_Create_01()
    {
        // Arrange.
        var clientConfiguration = await GetProfilingClientConfiguration().ConfigureAwait(false);

        var logicalOptions = await new FabricOptions(clientConfiguration)
            .UseDiagnostics()
            .UseDataConnectionToNewSpace(_testContext, true)
            .UseLogicalContext()
            .UseDiagnostics()
            .UseProfiling()
            .ConfigureAwait(false);

        // Act.
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var context = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        // Assert.
        Assert.NotNull(context);
    }

    [Fact]
    public async Task ProfilingLogicalContext_Create_02()
    {
        // Arrange.
        var clientConfiguration = await GetProfilingClientConfiguration().ConfigureAwait(false);

        var logicalOptions = await new FabricOptions(clientConfiguration)
            .UseDiagnostics()
            .UseDataConnectionToNewSpace(_testContext, true)
            .UseLogicalContext()
            .UseDiagnostics()
            .ConfigureAwait(false);

        // Act.
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var context = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        // Assert.
        Assert.NotNull(context);
    }

    [Fact]
    public async Task ProfilingLogicalContext_Create_03()
    {
        // Arrange.
        var clientConfiguration = await GetProfilingClientConfiguration().ConfigureAwait(false);

        var logicalOptions = await new FabricOptions(clientConfiguration)
            .UseDiagnostics()
            .UseDataConnectionToNewSpace(_testContext, true)
            .UseLogicalContext()
            .UseDiagnostics()
            .ConfigureAwait(false);

        // Act.
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        // Assert.
        Assert.NotNull(logicalContext);
    }


    private async Task<IConfigurationRoot> GetProfilingClientConfiguration()
    {
        var type = typeof(LogicalUnitTestContext);
        // Get the current executing assembly (in this case it's the test dll)
        var assembly = Assembly.GetAssembly(type);
        // Get the stream (embedded resource) - be sure to wrap in a using block
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var stream = assembly!.GetManifestResourceStream($"{type.Namespace}.LogicalProfilingSettings.json");
#pragma warning restore CA2007

        var clientConfiguration = new ConfigurationBuilder()
            .AddConfiguration(_testContext.ClientConfiguration)
            .AddJsonStream(stream)
            .Build();
        return clientConfiguration;
    }
}
