// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;

    [CorrelateUnitTests]
    public class GraphPathTraverserFactoryTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathTraverserFactory_New()
        {
            // Arrange.

            // Act.
            var factory = new GraphPathTraverserFactory();

            // Assert.
            Assert.NotNull(factory);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathTraverserFactory_Create()
        {
            // Arrange.
            var factory = new GraphPathTraverserFactory();
            IConfiguration configurationRoot = null;
            var configuration = new GraphPathTraverserConfiguration(configurationRoot);

            // Act.
            var traverser = factory.Create(configuration);

            // Assert.
            Assert.NotNull(traverser);
        }
    }
}
