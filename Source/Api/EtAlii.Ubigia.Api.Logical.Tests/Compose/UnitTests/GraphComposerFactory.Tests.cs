// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    [CorrelateUnitTests]
    public class GraphComposerFactoryTests
    {
        [Fact]
        public void GraphComposerFactory_New()
        {
            // Arrange.
            var configurationRoot = new ConfigurationBuilder().Build();

            var logicalOptions = new FabricOptions(configurationRoot)
                .UseDiagnostics()
                .UseLogicalContext()
                .UseDiagnostics();
            var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);

            // Act.
            var factory = new GraphComposerFactory(traverser);

            // Assert.
            Assert.NotNull(factory );
        }
    }
}
