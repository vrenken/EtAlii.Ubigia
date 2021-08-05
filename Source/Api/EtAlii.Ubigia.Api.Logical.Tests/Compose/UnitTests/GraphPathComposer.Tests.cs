// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;

    [CorrelateUnitTests]
    public class GraphPathComposerUnitTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathComposer_Create()
        {
            // Arrange.

            IFabricContext fabric = null;
            var configurationRoot = new ConfigurationBuilder().Build();

            var options = new GraphPathTraverserOptions(configurationRoot);
            var traverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = traverserFactory.Create(options);

            var graphChildAdder = new GraphChildAdder(graphPathTraverser, fabric);
            var graphLinkAdder = new GraphLinkAdder(graphChildAdder, graphPathTraverser, fabric);
            var graphUpdater = new GraphUpdater(fabric);
            var graphAdder = new GraphAdder(graphChildAdder, graphLinkAdder, graphUpdater, graphPathTraverser);
            var graphRemover = new GraphRemover(graphChildAdder, graphLinkAdder, graphUpdater, graphPathTraverser);
            var graphLinker = new GraphLinker(graphChildAdder, graphLinkAdder, graphUpdater, graphPathTraverser);
            var graphUnlinker = new GraphUnlinker();
            var graphRenamer = new GraphRenamer(graphUpdater, graphPathTraverser);

            // Act.
            var composer = new GraphComposer(graphAdder, graphRemover, graphLinker, graphUnlinker, graphRenamer);

            // Assert.
            Assert.NotNull(composer);
        }
    }
}
