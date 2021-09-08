// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;

    [CorrelateUnitTests]
    public class GraphPathTraverserFactoryTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathTraverser_Create()
        {
            // Arrange.
            var configurationRoot = new ConfigurationBuilder().Build();
            var fabricOptions = new FabricOptions(configurationRoot)
                .UseDiagnostics();
            using var fabricContext = Factory.Create<IFabricContext>(fabricOptions);

            var logicalOptions = new LogicalOptions(configurationRoot)
                .UseFabricContext(fabricContext)
                .UseDiagnostics();

            // Act.
            var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);

            // Assert.
            Assert.NotNull(traverser);
        }
    }
}
