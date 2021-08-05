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
            var configurationRoot = new ConfigurationBuilder().Build();

            var options = new GraphPathTraverserOptions(configurationRoot).Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var traverser = graphPathTraverserFactory.Create(options);

            // Act.
            var factory = new GraphComposerFactory(traverser);

            // Assert.
            Assert.NotNull(factory );
        }
    }
}
