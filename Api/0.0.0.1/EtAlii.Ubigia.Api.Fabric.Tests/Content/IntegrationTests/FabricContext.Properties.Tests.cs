namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;

    
    public class FabricContextPropertiesTests : IClassFixture<TransportUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabric;
        private readonly TransportUnitTestContext _testContext;

        public FabricContextPropertiesTests(TransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var connection = await _testContext.TransportTestContext.CreateDataConnection(true);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
        }

        public async Task DisposeAsync()
        {
            _fabric.Dispose();
            _fabric = null;

            await Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Properties_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var properties = _testContext.TestPropertiesFactory.Create();

            // Act.
            await _fabric.Properties.Store(entry.Id, properties, scope);

            // Assert.
            Assert.True(properties.Stored); 
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Properties_Retrieve()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var properties = _testContext.TestPropertiesFactory.CreateComplete();
            await _fabric.Properties.Store(entry.Id, properties, scope);

            // Act.
            var retrievedProperties = await _fabric.Properties.Retrieve(entry.Id, scope);

            // Assert.
            Assert.True(_testContext.PropertyDictionaryComparer.AreEqual(properties, retrievedProperties));
            Assert.True(retrievedProperties.Stored);
            Assert.True(properties.Stored);
        }
    }
}
