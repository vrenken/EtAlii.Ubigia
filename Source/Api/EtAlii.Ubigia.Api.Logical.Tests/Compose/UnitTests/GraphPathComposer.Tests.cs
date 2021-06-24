// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    public class GraphPathComposerUnitTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathComposer_Create()
        {
            // Arrange.

            IFabricContext fabric = null;

            var configuration = new GraphPathTraverserConfiguration();
            var traverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = traverserFactory.Create(configuration);

            var graphChildAdder = new GraphChildAdder(graphPathTraverser, fabric);
            var graphLinkAdder = new GraphLinkAdder(graphChildAdder, graphPathTraverser, fabric);
            var graphUpdater = new GraphUpdater(fabric);
            var graphAdder = new GraphAdder(graphChildAdder, graphLinkAdder, graphUpdater, graphPathTraverser);
            var graphRemover = new GraphRemover(graphChildAdder, graphLinkAdder, graphUpdater, graphPathTraverser);
            var graphLinker = new GraphLinker(graphChildAdder, graphLinkAdder, graphUpdater, graphPathTraverser);
            var graphUnlinker = new GraphUnlinker();//graphChildAdder, graphLinkAdder
            var graphRenamer = new GraphRenamer(graphUpdater, graphPathTraverser);

            // Act.
            var composer = new GraphComposer(graphAdder, graphRemover, graphLinker, graphUnlinker, graphRenamer);

            // Assert.
            Assert.NotNull(composer);
        }
    }
}
