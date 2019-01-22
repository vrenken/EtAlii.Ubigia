namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;

    
    public class FabricContextContentDefinitionTests : IClassFixture<TransportUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabric;
        private readonly TransportUnitTestContext _testContext;

        public FabricContextContentDefinitionTests(TransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var connection = await _testContext.TransportTestContext.CreateDataConnection();
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
        public async Task FabricContext_ContentDefinition_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Assert.
            Assert.True(contentDefinition.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Null()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinitionPart = (ContentDefinitionPart)null;

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 3;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(0);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            Assert.True(contentDefinitionPart.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Outside_Bounds()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(2);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_At_Bounds()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(1);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Before_ContentDefinition()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(0);

            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            Assert.NotNull(contentDefinition);
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Existing_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(10);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(5);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Invalid_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(10);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(15);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Null()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            var contentDefinitionPart = (ContentDefinitionPart)null;
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart));

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.True(_testContext.ContentComparer.AreEqual(contentDefinition, retrievedContentDefinition, false));
            Assert.Equal((UInt64)contentDefinition.Parts.Count, retrievedContentDefinition.Summary.TotalParts);
            Assert.True(retrievedContentDefinition.Summary.IsComplete);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve_Incomplete_1()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 2;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(1);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.Equal(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.False(retrievedContentDefinition.Summary.IsComplete);
            Assert.Single(retrievedContentDefinition.Summary.AvailableParts);
            Assert.Equal((UInt64)1, retrievedContentDefinition.Summary.AvailableParts.First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve_Incomplete_2()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 3;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(2);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.Equal(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.False(retrievedContentDefinition.Summary.IsComplete);
            Assert.Single(retrievedContentDefinition.Summary.AvailableParts);
            Assert.Equal((UInt64)2, retrievedContentDefinition.Summary.AvailableParts.First());
        }
    }
}
