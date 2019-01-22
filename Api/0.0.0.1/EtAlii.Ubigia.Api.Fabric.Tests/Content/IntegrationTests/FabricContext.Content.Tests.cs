namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;

    
    public class FabricContextContentTests : IClassFixture<TransportUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabric;
        private readonly TransportUnitTestContext _testContext;

        public FabricContextContentTests(TransportUnitTestContext testContext)
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
        public async Task FabricContext_Content_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            await _fabric.Content.Store(entry.Id, content);

            // Assert.
            Assert.True(content.Stored); 
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Content_Retrieve_Complete()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);

            var datas = _testContext.TestContentFactory.CreateData(100, 500, 3);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(datas);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            var content = _testContext.TestContentFactory.Create(3);
            var contentParts = _testContext.TestContentFactory.CreateParts(datas);

            // Act.
            await _fabric.Content.Store(entry.Id, content);
            foreach (var contentPart in contentParts)
            {
                await _fabric.Content.Store(entry.Id, contentPart);
            }

            var retrievedContent = await _fabric.Content.Retrieve(entry.Id);

            // Assert.
            Assert.Equal(content.TotalParts, retrievedContent.Summary.TotalParts);
            Assert.True(retrievedContent.Summary.IsComplete);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Content_Retrieve_Incomplete()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var datas = _testContext.TestContentFactory.CreateData(100, 500, 3);
            var contentdefinition = _testContext.TestContentDefinitionFactory.Create(datas);
            await _fabric.Content.StoreDefinition(entry.Id, contentdefinition);
            var content = _testContext.TestContentFactory.Create(3);
            var contentPartFirst = _testContext.TestContentFactory.CreatePart(datas[0], 0);
            var contentPartSecond = _testContext.TestContentFactory.CreatePart(datas[1], 1);
            var contentPartThird = _testContext.TestContentFactory.CreatePart(datas[2], 2);

            // Act.
            await _fabric.Content.Store(entry.Id, content);
            await _fabric.Content.Store(entry.Id, contentPartFirst);
            var retrievedContent = await _fabric.Content.Retrieve(entry.Id);

            // Assert.
            Assert.False(retrievedContent.Summary.IsComplete);

            // Act.
            await _fabric.Content.Store(entry.Id, contentPartSecond);
            retrievedContent = await _fabric.Content.Retrieve(entry.Id);

            // Assert.
            Assert.False(retrievedContent.Summary.IsComplete);

            // Act.
            await _fabric.Content.Store(entry.Id, contentPartThird);
            retrievedContent = await _fabric.Content.Retrieve(entry.Id);

            // Assert.
            Assert.Equal(content.TotalParts, retrievedContent.Summary.TotalParts);
            Assert.True(retrievedContent.Summary.IsComplete);
        }
    }
}
