﻿namespace EtAlii.Servus.Api.Logical.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Diagnostics.Tests;
    using EtAlii.Servus.Api.Fabric.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    
    public partial class GraphComposer_IntegrationTests : IClassFixture<FabricUnitTestContext>
    {
        private readonly FabricUnitTestContext _testContext;

        public GraphComposer_IntegrationTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task GraphComposer_Create()
        {
            // Arrange.
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();

            // Act.
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            // Assert.
            Assert.NotNull(composer);
        }
    }
}