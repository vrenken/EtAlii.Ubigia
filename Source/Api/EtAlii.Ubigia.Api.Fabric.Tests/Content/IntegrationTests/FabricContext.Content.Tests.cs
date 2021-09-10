// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class FabricContextContentTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabricContext;
        private readonly FabricUnitTestContext _testContext;

        public FabricContextContentTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var fabricOptions = await _testContext.Transport
                .CreateDataConnectionToNewSpace()
                .UseFabricContext()
                .UseDiagnostics()
                .ConfigureAwait(false);

            _fabricContext = Factory.Create<IFabricContext>(fabricOptions);
        }

        public Task DisposeAsync()
        {
            _fabricContext.Dispose();
            _fabricContext = null;
            return Task.CompletedTask;
        }

        [Fact]
        public async Task FabricContext_Content_Store()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var root = await _fabricContext.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabricContext.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            await _fabricContext.Content.Store(entry.Id, content).ConfigureAwait(false);

            // Assert.
            Assert.True(content.Stored);
        }

        [Fact]
        public async Task FabricContext_Content_Retrieve_Complete()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var root = await _fabricContext.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabricContext.Entries.Get(root.Identifier, scope).ConfigureAwait(false);

            var datas = _testContext.TestContentFactory.CreateData(100, 500, 3);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(datas);
            await _fabricContext.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create(3);
            var contentParts = _testContext.TestContentFactory.CreateParts(datas);

            // Act.
            await _fabricContext.Content.Store(entry.Id, content).ConfigureAwait(false);
            foreach (var contentPart in contentParts)
            {
                await _fabricContext.Content.Store(entry.Id, contentPart).ConfigureAwait(false);
            }

            var retrievedContent = await _fabricContext.Content.Retrieve(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(content.TotalParts, retrievedContent.Summary.TotalParts);
            Assert.True(retrievedContent.Summary.IsComplete);
        }

        [Fact]
        public async Task FabricContext_Content_Retrieve_Incomplete()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var root = await _fabricContext.Roots.Get("Hierarchy").ConfigureAwait(false);
            var entry = await _fabricContext.Entries.Get(root.Identifier, scope).ConfigureAwait(false);
            var datas = _testContext.TestContentFactory.CreateData(100, 500, 3);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(datas);
            await _fabricContext.Content.StoreDefinition(entry.Id, contentDefinition).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create(3);
            var contentPartFirst = _testContext.TestContentFactory.CreatePart(datas[0]);
            var contentPartSecond = _testContext.TestContentFactory.CreatePart(datas[1], 1);
            var contentPartThird = _testContext.TestContentFactory.CreatePart(datas[2], 2);

            // Act.
            await _fabricContext.Content.Store(entry.Id, content).ConfigureAwait(false);
            await _fabricContext.Content.Store(entry.Id, contentPartFirst).ConfigureAwait(false);
            var retrievedContent = await _fabricContext.Content.Retrieve(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.False(retrievedContent.Summary.IsComplete);

            // Act.
            await _fabricContext.Content.Store(entry.Id, contentPartSecond).ConfigureAwait(false);
            retrievedContent = await _fabricContext.Content.Retrieve(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.False(retrievedContent.Summary.IsComplete);

            // Act.
            await _fabricContext.Content.Store(entry.Id, contentPartThird).ConfigureAwait(false);
            retrievedContent = await _fabricContext.Content.Retrieve(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(content.TotalParts, retrievedContent.Summary.TotalParts);
            Assert.True(retrievedContent.Summary.IsComplete);
        }

        //[Fact]
        //public void FabricContext_ContentDefinition_Store_And_Retrieve_Check_Size()
        //[
        //    var connection = CreateFabricContext()

        //    var root = connection.Roots.Get("Hierarchy")
        //    var entry = connection.Entries.Get(root.Identifier)

        //    var contentDefinition = Create()
        //    connection.Content.StoreDefinition(entry.Id, contentDefinition)

        //    var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id)

        //    Assert.Equal(contentDefinition.Size, retrievedContentDefinition.Size)
        //]
        //[Fact]
        //public void FabricContext_ContentDefinition_Store_And_Retrieve_Check_Checksum()
        //[
        //    var connection = CreateFabricContext()

        //    var root = connection.Roots.Get("Hierarchy")
        //    var entry = connection.Entries.Get(root.Identifier)

        //    var contentDefinition = Create()
        //    connection.Content.StoreDefinition(entry.Id, contentDefinition)

        //    var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id)

        //    Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum)
        //]
        //[Fact]
        //public void FabricContext_ContentDefinition_Store_And_Retrieve_Check_Parts()
        //[
        //    var connection = CreateFabricContext()

        //    var root = connection.Roots.Get("Hierarchy")
        //    var entry = connection.Entries.Get(root.Identifier)

        //    var contentDefinition = Create()
        //    connection.Content.StoreDefinition(entry.Id, contentDefinition)

        //    var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id)

        //    Assert.Equal(contentDefinition.Parts.Count, retrievedContentDefinition.Parts.Count())
        //    for (int i = 0; i < contentDefinition.Parts.Count; i++)
        //    [
        //        Assert.Equal(contentDefinition.Parts[i].Checksum, retrievedContentDefinition.Parts.ElementAt(i).Checksum)
        //        Assert.Equal(contentDefinition.Parts[i].Size, retrievedContentDefinition.Parts.ElementAt(i).Size)
        //    ]
        //]
    }
}
