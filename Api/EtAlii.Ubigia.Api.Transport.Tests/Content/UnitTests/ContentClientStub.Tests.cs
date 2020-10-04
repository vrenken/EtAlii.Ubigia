﻿namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ContentDataClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var contentDataClientStub = new ContentDataClientStub();

            // Assert.
            Assert.NotNull(contentDataClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Connect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.Connect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Disconnect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.Disconnect();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Retrieve()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            var result = await contentDataClientStub.RetrieveDefinition(Identifier.Empty);

            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Store_ContentPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.Store(Identifier.Empty, (ContentPart)null);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Store_ContentDefinition()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinition)null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Store_ContentDefinitionPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinitionPart)null);

            // Assert.
        }
    }
}