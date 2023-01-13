// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Fabric.Diagnostics;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using Xunit;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

[CorrelateUnitTests]
public class GraphPathComposerUnitTests
{
    [Fact]
    public void GraphPathComposer_Create()
    {
        // Arrange.
        var configurationRoot = new ConfigurationBuilder().Build();

        var logicalOptions = new FabricOptions(configurationRoot)
            .UseDiagnostics()
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);

        var fabricContext = logicalOptions.FabricContext;
        var graphChildAdder = new GraphChildAdder(traverser, fabricContext);
        var graphLinkAdder = new GraphLinkAdder(graphChildAdder, traverser, fabricContext);
        var graphUpdater = new GraphUpdater(fabricContext);
        var graphAdder = new GraphAdder(graphChildAdder, graphLinkAdder, graphUpdater, traverser);
        var graphRemover = new GraphRemover(graphChildAdder, graphLinkAdder, graphUpdater, traverser);
        var graphLinker = new GraphLinker(graphChildAdder, graphLinkAdder, graphUpdater, traverser);
        var graphUnlinker = new GraphUnlinker();
        var graphRenamer = new GraphRenamer(graphUpdater, traverser);

        // Act.
        var composer = new GraphComposer(graphAdder, graphRemover, graphLinker, graphUnlinker, graphRenamer);

        // Assert.
        Assert.NotNull(composer);
    }
}
