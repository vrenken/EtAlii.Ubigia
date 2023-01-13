// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests;

using System.Threading.Tasks;
using Xunit;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;

[CorrelateUnitTests]
public class FabricContextUseFabricContextTests : IClassFixture<FabricUnitTestContext>
{
    private readonly FabricUnitTestContext _testContext;

    public FabricContextUseFabricContextTests(FabricUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task FabricOptions_Create_01()
    {
        // Arrange.

        // Act.
        var fabricOptions = await _testContext.Transport
            .CreateDataConnectionToNewSpace()
            .UseFabricContext()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(fabricOptions);

        // Assure.
        var fabricContext = Factory.Create<IFabricContext>(fabricOptions);
        await fabricContext.DisposeAsync().ConfigureAwait(false);
    }

    [Fact]
    public async Task FabricOptions_Create_02()
    {
        // Arrange.
        var kvp = await _testContext.Transport
            .CreateDataConnectionToNewSpace()
            .ConfigureAwait(false);
        var dataConnection = kvp.Item2;

        // Act.
        var fabricOptions = dataConnection.UseFabricContext();

        // Assert.
        Assert.NotNull(fabricOptions);

        // Assure.
        var fabricContext = Factory.Create<IFabricContext>(fabricOptions);
        await fabricContext.DisposeAsync().ConfigureAwait(false);
    }
}
