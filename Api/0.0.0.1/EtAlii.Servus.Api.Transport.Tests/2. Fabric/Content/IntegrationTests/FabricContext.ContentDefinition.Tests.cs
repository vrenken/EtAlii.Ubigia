namespace EtAlii.Servus.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.Tests;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    
    public class FabricContext_ContentDefinition_Tests : IClassFixture<TransportUnitTestContext>, IDisposable
    {
        private IFabricContext _fabric;
        private readonly TransportUnitTestContext _testContext;

        public FabricContext_ContentDefinition_Tests(TransportUnitTestContext testContext)
        {
            _testContext = testContext;

            var task = Task.Run(async () =>
            {
                var connection = await _testContext.TransportTestContext.CreateDataConnection();
                var fabricContextConfiguration = new FabricContextConfiguration()
                    .Use(connection);
                _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _fabric.Dispose();
                _fabric = null;
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create();

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
            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<ArgumentNullException>(act);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create(0);
            contentDefinition.TotalParts = 3;
            var contentDefinitionPart = TestContentDefinition.CreatePart(0);
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
            var contentDefinition = TestContentDefinition.Create();
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinition.CreatePart(2);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_At_Bounds()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create();
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinition.CreatePart(1);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Before_ContentDefinition()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create();
            var contentDefinitionPart = TestContentDefinition.CreatePart(0);
            //connection.Content.StoreDefinition(entry.Id, contentDefinition);

            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Existing_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create(10);
            var contentDefinitionPart = TestContentDefinition.CreatePart(5);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            //await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Invalid_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create(10);
            var contentDefinitionPart = TestContentDefinition.CreatePart(15);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Null()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create();
            var contentDefinitionPart = (ContentDefinitionPart)null;
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            await ExceptionAssert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id);

            // Assert.
            AssertData.AreEqual(contentDefinition, retrievedContentDefinition, false);
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
            var contentDefinition = TestContentDefinition.Create(0);
            contentDefinition.TotalParts = 2;
            var contentDefinitionPart = TestContentDefinition.CreatePart(1);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.Equal(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.False(retrievedContentDefinition.Summary.IsComplete);
            Assert.Equal(1, retrievedContentDefinition.Summary.AvailableParts.Length);
            Assert.Equal((UInt64)1, retrievedContentDefinition.Summary.AvailableParts.First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve_Incomplete_2()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var contentDefinition = TestContentDefinition.Create(0);
            contentDefinition.TotalParts = 3;
            var contentDefinitionPart = TestContentDefinition.CreatePart(2);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.Equal(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.False(retrievedContentDefinition.Summary.IsComplete);
            Assert.Equal(1, retrievedContentDefinition.Summary.AvailableParts.Length);
            Assert.Equal((UInt64)2, retrievedContentDefinition.Summary.AvailableParts.First());
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_ContentDefinition_Store_And_Retrieve_Check_Size()
        //{
        //    var connection = CreateFabricContext();

        //    var root = await connection.Roots.Get("Hierarchy");
        //    var entry = await connection.Entries.Get(root.Identifier);

        //    var contentDefinition = Create();
        //    await connection.Content.StoreDefinition(entry.Id, contentDefinition);

        //    var retrievedContentDefinition = await connection.Content.RetrieveDefinition(entry.Id);

        //    Assert.Equal(contentDefinition.Size, retrievedContentDefinition.Size);
        //}

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Checksum()
        //{
        //    var connection = CreateFabricContext();

        //    var root = await connection.Roots.Get("Hierarchy");
        //    var entry = await connection.Entries.Get(root.Identifier);

        //    var contentDefinition = Create();
        //    await connection.Content.StoreDefinition(entry.Id, contentDefinition);

        //    var retrievedContentDefinition = await connection.Content.RetrieveDefinition(entry.Id);

        //    Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        //}


        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Parts()
        //{
        //    var connection = CreateFabricContext();

        //    var root = await connection.Roots.Get("Hierarchy");
        //    var entry = await connection.Entries.Get(root.Identifier);

        //    var contentDefinition = Create();
        //    await connection.Content.StoreDefinition(entry.Id, contentDefinition);

        //    var retrievedContentDefinition = await connection.Content.RetrieveDefinition(entry.Id);

        //    Assert.Equal(contentDefinition.Parts.Count, retrievedContentDefinition.Parts.Count());
        //    for (int i = 0; i < contentDefinition.Parts.Count; i++)
        //    {
        //        Assert.Equal(contentDefinition.Parts[i].Checksum, retrievedContentDefinition.Parts.ElementAt(i).Checksum);
        //        Assert.Equal(contentDefinition.Parts[i].Size, retrievedContentDefinition.Parts.ElementAt(i).Size);
        //    }
        //}
    }
}
