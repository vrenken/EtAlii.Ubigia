namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Storage = EtAlii.Servus.Api.Storage;

    [TestClass]
    public class ContentDataClientStub_Tests
    {
        [TestMethod]
        public void ContentDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var contentDataClientStub = new ContentDataClientStub();

            // Assert.
        }

        [TestMethod]
        public void ContentDataClientStub_Connect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.Connect();

            // Assert.
        }

        [TestMethod]
        public void ContentDataClientStub_Disconnect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.Disconnect();

            // Assert.
        }

        [TestMethod]
        public void ContentDataClientStub_Retrieve()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            var result = contentDataClientStub.RetrieveDefinition(Identifier.Empty);

            // Assert.
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ContentDataClientStub_Store_ContentPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.Store(Identifier.Empty, (ContentPart)null);
        }

        [TestMethod]
        public void ContentDataClientStub_Store_ContentDefinition()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinition)null);

            // Assert.
        }

        [TestMethod]
        public void ContentDataClientStub_Store_ContentDefinitionPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinitionPart)null);

            // Assert.
        }
    }
}
