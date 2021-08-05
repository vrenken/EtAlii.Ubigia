// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class FabricContextPropertiesTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabric;
        private readonly FabricUnitTestContext _testContext;

        public FabricContextPropertiesTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }
        public async Task InitializeAsync()
        {
            var connection = await _testContext.Transport.CreateDataConnectionToNewSpace().ConfigureAwait(false);
            var fabricContextOptions = new FabricContextOptions(_testContext.ClientConfiguration)
                .Use(connection)
                .UseFabricDiagnostics();
            _fabric = new FabricContextFactory().Create(fabricContextOptions);
        }

        public Task DisposeAsync()
        {
            _fabric.Dispose();
            _fabric = null;
            return Task.CompletedTask;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Properties_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var properties = _testContext.TestPropertiesFactory.Create();

            // Act.
            await _fabric.Properties.Store(entry.Id, properties, scope).ConfigureAwait(false);

            // Assert.
            Assert.True(properties.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Properties_Retrieve()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var properties = _testContext.TestPropertiesFactory.CreateComplete();
            await _fabric.Properties.Store(entry.Id, properties, scope).ConfigureAwait(false);

            // Act.
            var retrievedProperties = await _fabric.Properties.Retrieve(entry.Id, scope).ConfigureAwait(false);

            // Assert.
            Assert.True(_testContext.PropertyDictionaryComparer.AreEqual(properties, retrievedProperties));
            Assert.True(retrievedProperties.Stored);
            Assert.True(properties.Stored);
        }
    }
}
