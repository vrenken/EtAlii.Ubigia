// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using Xunit;

public class LogicalUnitTestContextTests
{
    private async Task<LogicalOptions> InitializeAsync(LogicalUnitTestContext testContext)
    {
        // Arrange.
        var options = await testContext.Fabric
            .CreateFabricOptions(true)
            .UseLogicalContext()
            .UseDiagnostics()
            .ConfigureAwait(false);

        return options;
    }

    private async Task DisposeAsync(LogicalOptions options)
    {
        await options.FabricContext.Options.Connection
            .Close()
            .ConfigureAwait(false);
    }

    [Fact]
    public async Task LogicalUnitTestContext_Create_Empty()
    {
        // Arrange.
        var context = new LogicalUnitTestContext();
        var start = DateTime.Now;

        // Act.
        try
        {
            await context.InitializeAsync().ConfigureAwait(false);

        }
        finally
        {
            await context.DisposeAsync().ConfigureAwait(false);
        }

        var end = DateTime.Now;
        var duration = end - start;

        // Assert.
        Assert.NotNull(context);
        Assert.True(duration.TotalMinutes < 3);
    }

    [Fact(Skip = "Not working as expected")]
    public async Task LogicalUnitTestContext_Create_Complete()
    {
        // Arrange.
        var context = new LogicalUnitTestContext();
        var start = DateTime.Now;

        // Act.
        try
        {
            await context.InitializeAsync().ConfigureAwait(false);
            var options = await InitializeAsync(context).ConfigureAwait(false);

            await DisposeAsync(options).ConfigureAwait(false);
        }
        finally
        {
            await context.DisposeAsync().ConfigureAwait(false);
        }

        var end = DateTime.Now;
        var duration = end - start;

        // Assert.
        Assert.NotNull(context);
        Assert.True(duration.TotalMinutes < 3);
    }
}
