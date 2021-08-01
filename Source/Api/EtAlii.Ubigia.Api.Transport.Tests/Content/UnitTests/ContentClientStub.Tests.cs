// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
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
            await contentDataClientStub.Connect(null).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Disconnect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.Disconnect().ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Retrieve()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            var result = await contentDataClientStub.RetrieveDefinition(Identifier.Empty).ConfigureAwait(false);

            // Assert.
            Assert.Null(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Store_ContentPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.Store(Identifier.Empty, (ContentPart)null).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Store_ContentDefinition()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinition)null).ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDataClientStub_Store_ContentDefinitionPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            await contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinitionPart)null).ConfigureAwait(false);

            // Assert.
        }
    }
}
