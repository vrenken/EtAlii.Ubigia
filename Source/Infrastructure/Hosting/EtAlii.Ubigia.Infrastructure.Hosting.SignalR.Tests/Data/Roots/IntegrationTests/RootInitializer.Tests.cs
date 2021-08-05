// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    [Trait("Technology", "SignalR")]
    public sealed class RootInitializerTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public RootInitializerTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RootInitializer_Initialize()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextOptions = new FabricContextOptions()
                .Use(context.Host.Storage)
                .UseFabricDiagnostics(_testContext.Host.HostConfiguration);
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            var logicalContextOptions = new LogicalContextOptions()
                .Use(fabric)
                .Use(context.HostName, context.ServiceDetails.DataAddress);
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
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextOptions = new FabricContextOptions()
                .Use(context.Host.Storage)
                .UseFabricDiagnostics(_testContext.Host.HostConfiguration);
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            var logicalContextOptions = new LogicalContextOptions()
                .Use(fabric)
                .Use(context.HostName, context.ServiceDetails.DataAddress);
            var logical = new LogicalContextFactory().Create(logicalContextOptions);

            // Act.
            var rootInitializer = new RootInitializer(fabric, logical.Entries);
            await rootInitializer.Initialize(space.Id, root).ConfigureAwait(false);

            // Assert.
            var registeredRoot = await context.Host.Infrastructure.Roots.Get(space.Id, root.Id).ConfigureAwait(false);
            Assert.NotEqual(Identifier.Empty, registeredRoot.Identifier);
            Assert.NotEqual(Guid.Empty, registeredRoot.Id);
        }

        [Fact]
        public async Task RootInitializer_Initialize_Check_Resulting_Entry()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextOptions = new FabricContextOptions()
                .Use(context.Host.Storage)
                .UseFabricDiagnostics(_testContext.Host.HostConfiguration);
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            var logicalContextOptions = new LogicalContextOptions()
                .Use(fabric)
                .Use(context.HostName, context.ServiceDetails.DataAddress);
            var logical = new LogicalContextFactory().Create(logicalContextOptions);

            // Act.
            var rootInitializer = new RootInitializer(fabric, logical.Entries);
            await rootInitializer.Initialize(space.Id, root).ConfigureAwait(false);

            // Assert.
            var registeredRoot = await context.Host.Infrastructure.Roots.Get(space.Id, root.Id).ConfigureAwait(false);
            Assert.NotEqual(registeredRoot.Identifier, Identifier.Empty);
            Assert.NotEqual(registeredRoot.Id, Guid.Empty);
            Assert.Equal(registeredRoot.Identifier, root.Identifier);

            var entry = context.Host.Infrastructure.Entries.Get(registeredRoot.Identifier);
            Assert.NotNull(entry);
        }
    }
}
