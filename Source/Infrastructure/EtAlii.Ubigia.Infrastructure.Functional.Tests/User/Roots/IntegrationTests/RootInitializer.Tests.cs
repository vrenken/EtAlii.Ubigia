// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public sealed class RootInitializerTests : IClassFixture<FunctionalInfrastructureUnitTestContext>
    {
        private readonly FunctionalInfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public RootInitializerTests(FunctionalInfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RootInitializer_Initialize()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextOptions = new FabricContextOptions(_testContext.Configuration)
                .Use(_testContext.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            var logicalContextOptions = new LogicalContextOptions(_testContext.Configuration)
                .Use(fabric)
                .Use(_testContext.HostName, _testContext.DataAddress);
            var logical = new LogicalContextFactory().Create(logicalContextOptions);

            // Act.
            var rootInitializer = new RootInitializer(fabric, logical.Entries);
            await rootInitializer.Initialize(space.Id, root).ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task RootInitializer_Initialize_Check_Resulting_Root()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextOptions = new FabricContextOptions(_testContext.Configuration)
                .Use(_testContext.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            var logicalContextOptions = new LogicalContextOptions(_testContext.Configuration)
                .Use(fabric)
                .Use(_testContext.HostName, _testContext.DataAddress);
            var logical = new LogicalContextFactory().Create(logicalContextOptions);

            // Act.
            var rootInitializer = new RootInitializer(fabric, logical.Entries);
            await rootInitializer.Initialize(space.Id, root).ConfigureAwait(false);

            // Assert.
            var registeredRoot = await _testContext.Functional.Roots.Get(space.Id, root.Id).ConfigureAwait(false);
            Assert.NotEqual(Identifier.Empty, registeredRoot.Identifier);
            Assert.NotEqual(Guid.Empty, registeredRoot.Id);
        }

        [Fact]
        public async Task RootInitializer_Initialize_Check_Resulting_Entry()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextOptions = new FabricContextOptions(_testContext.Configuration)
                .Use(_testContext.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            var logicalContextOptions = new LogicalContextOptions(_testContext.Configuration)
                .Use(fabric)
                .Use(_testContext.HostName, _testContext.DataAddress);
            var logical = new LogicalContextFactory().Create(logicalContextOptions);

            // Act.
            var rootInitializer = new RootInitializer(fabric, logical.Entries);
            await rootInitializer.Initialize(space.Id, root).ConfigureAwait(false);

            // Assert.
            var registeredRoot = await _testContext.Functional.Roots.Get(space.Id, root.Id).ConfigureAwait(false);
            Assert.NotEqual(registeredRoot.Identifier, Identifier.Empty);
            Assert.NotEqual(registeredRoot.Id, Guid.Empty);
            Assert.Equal(registeredRoot.Identifier, root.Identifier);

            var entry = _testContext.Functional.Entries.Get(registeredRoot.Identifier);
            Assert.NotNull(entry);
        }
    }
}
