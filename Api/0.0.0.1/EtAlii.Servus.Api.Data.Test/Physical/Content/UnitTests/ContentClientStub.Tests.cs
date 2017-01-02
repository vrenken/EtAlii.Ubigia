namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentDataClientStub_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var contentDataClientStub = new ContentDataClientStub();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentDataClientStub_Connect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.Connect();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentDataClientStub_Disconnect()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.Disconnect();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentDataClientStub_Retrieve()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            var result = contentDataClientStub.RetrieveDefinition(Identifier.Empty);

            // Assert.
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentDataClientStub_Store_ContentPart()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.Store(Identifier.Empty, (ContentPart)null);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentDataClientStub_Store_ContentDefinition()
        {
            // Arrange.
            var contentDataClientStub = new ContentDataClientStub();

            // Act.
            contentDataClientStub.StoreDefinition(Identifier.Empty, (ContentDefinition)null);

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
