// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class FabricContextContentDefinitionTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabric;
        private readonly FabricUnitTestContext _testContext;

        public FabricContextContentDefinitionTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }
        public async Task InitializeAsync()
        {
            var connection = await _testContext.Transport.CreateDataConnectionToNewSpace().ConfigureAwait(false);
            var fabricContextConfiguration = new FabricContextConfiguration(_testContext.ClientConfiguration)
                .Use(connection)
                .UseFabricDiagnostics();
            _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
        }

        public Task DisposeAsync()
        {
            _fabric.Dispose();
            _fabric = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Assert.
            Assert.True(contentDefinition.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Null()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, (ContentDefinitionPart)null).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            Blob.SetTotalParts(contentDefinition, 3);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(0);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false);

            // Assert.
            Assert.True(contentDefinitionPart.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Outside_Bounds()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            Blob.SetTotalParts(contentDefinition, 1);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(2);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_At_Bounds()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            Blob.SetTotalParts(contentDefinition, 1);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(1);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Before_ContentDefinition()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(0);
            //connection.Content.StoreDefinition(entry.Id, contentDefinition)

            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false));

            // Assert.
            Assert.NotNull(contentDefinition);
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Existing_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(10);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(5);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Act.
            //await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart)
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Invalid_Part()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(10);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(15);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Store_Part_Null()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Content.StoreDefinition(entry.Id, (ContentDefinitionPart)null).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve() // Last exception 2019-04-06.
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.True(_testContext.ContentComparer.AreEqual(contentDefinition, retrievedContentDefinition, false));
            Assert.Equal((ulong)contentDefinition.Parts.Length, retrievedContentDefinition.Summary.TotalParts);
            Assert.True(retrievedContentDefinition.Summary.IsComplete);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve_Incomplete_1()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            Blob.SetTotalParts(contentDefinition, 2);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(1);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.False(retrievedContentDefinition.Summary.IsComplete);
            Assert.Single(retrievedContentDefinition.Summary.AvailableParts);
            Assert.Equal((ulong)1, retrievedContentDefinition.Summary.AvailableParts.First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_ContentDefinition_Retrieve_Incomplete_2()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabric.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            Blob.SetTotalParts(contentDefinition, 3);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(2);

            // Act.
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);
            await _fabric.Content.StoreDefinition(entry.Id, contentDefinitionPart).ConfigureAwait(false);
            var retrievedContentDefinition = await _fabric.Content.RetrieveDefinition(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.False(retrievedContentDefinition.Summary.IsComplete);
            Assert.Single(retrievedContentDefinition.Summary.AvailableParts);
            Assert.Equal((ulong)2, retrievedContentDefinition.Summary.AvailableParts.First());
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_ContentDefinition_Store_And_Retrieve_Check_Size()
        //[
        //    var connection = CreateFabricContext()

        //    var root = await connection.Roots.Get("Hierarchy")
        //    var entry = await connection.Entries.Get(root.Identifier)

        //    var contentDefinition = Create()
        //    await connection.Content.StoreDefinition(entry.Id, contentDefinition)

        //    var retrievedContentDefinition = await connection.Content.RetrieveDefinition(entry.Id)

        //    Assert.Equal(contentDefinition.Size, retrievedContentDefinition.Size)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Checksum()
        //[
        //    var connection = CreateFabricContext()

        //    var root = await connection.Roots.Get("Hierarchy")
        //    var entry = await connection.Entries.Get(root.Identifier)

        //    var contentDefinition = Create()
        //    await connection.Content.StoreDefinition(entry.Id, contentDefinition)

        //    var retrievedContentDefinition = await connection.Content.RetrieveDefinition(entry.Id)

        //    Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Parts()
        //[
        //    var connection = CreateFabricContext()

        //    var root = await connection.Roots.Get("Hierarchy")
        //    var entry = await connection.Entries.Get(root.Identifier)

        //    var contentDefinition = Create()
        //    await connection.Content.StoreDefinition(entry.Id, contentDefinition)

        //    var retrievedContentDefinition = await connection.Content.RetrieveDefinition(entry.Id)

        //    Assert.Equal(contentDefinition.Parts.Count, retrievedContentDefinition.Parts.Count())
        //    for (int i = 0; i < contentDefinition.Parts.Count; i++)
        //    [
        //        Assert.Equal(contentDefinition.Parts[i].Checksum, retrievedContentDefinition.Parts.ElementAt(i).Checksum)
        //        Assert.Equal(contentDefinition.Parts[i].Size, retrievedContentDefinition.Parts.ElementAt(i).Size)
        //    ]
        //]
    }
}
