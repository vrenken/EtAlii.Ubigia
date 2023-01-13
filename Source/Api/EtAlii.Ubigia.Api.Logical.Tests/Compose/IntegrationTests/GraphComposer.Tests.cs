// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using Xunit;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;

[CorrelateUnitTests]
public partial class GraphComposerIntegrationTests : IClassFixture<LogicalUnitTestContext>
{
    private readonly LogicalUnitTestContext _testContext;

    public GraphComposerIntegrationTests(LogicalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task GraphComposer_Create()
    {
        // Arrange.
        var logicalOptions = await _testContext.Fabric
            .CreateFabricOptions(true)
            .UseLogicalContext()
            .UseDiagnostics()
            .ConfigureAwait(false);
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        // Act.
        var composer = new GraphComposerFactory(traverser).Create(fabricContext);

        // Assert.
        Assert.NotNull(composer);
    }
}
