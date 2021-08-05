// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;

    [CorrelateUnitTests]
    public class GraphComposerFactoryTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphComposerFactory_New()
        {
            // Arrange.
            IFabricContext fabric = null;
            IConfiguration configurationRoot = null;

            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration(configurationRoot).Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);

            // Act.
            var factory = new GraphComposerFactory(graphPathTraverser);

            // Assert.
            Assert.NotNull(factory );
        }
    }
}
