namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public sealed class ContentRepository_Tests : TestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void ContentRepository_Store_Content()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var content = TestContentFactory.Create();

            // Act.
            Infrastructure.Content.Store(entry.Id, content);

            // Assert.
            Assert.IsTrue(content.Stored);
        }

        [TestMethod]
        public void ContentRepository_Store_ContentPart()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var data = TestContentFactory.CreateData(100, 500);
            var contentDefinition = TestContentDefinitionFactory.Create(data);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            var content = TestContentFactory.Create(1);
            var contentPart = TestContentFactory.CreatePart(data);

            // Act.
            Infrastructure.Content.Store(entry.Id, content);
            Infrastructure.Content.Store(entry.Id, contentPart);

            // Assert.
            Assert.IsTrue(content.Stored);
            Assert.IsTrue(contentPart.Stored);
        }


        [TestMethod]
        public void ContentRepository_Store_ContentPart_Out_Of_Bounds()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var content = TestContentFactory.Create(3);
            var contentPart = TestContentFactory.CreatePart(6);
            Infrastructure.Content.Store(entry.Id, content);

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.Content.Store(entry.Id, contentPart);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [TestMethod]
        public void ContentRepository_Store_ContentPart_Before_Content()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var content = TestContentFactory.Create(1);
            var contentPart = TestContentFactory.CreatePart(0);

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.Content.Store(entry.Id, contentPart);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [TestMethod]
        public void ContentRepository_Retrieve_Content()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var data = TestContentFactory.CreateData(100, 500);
            var contentDefinition = TestContentDefinitionFactory.Create(data);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            var content = TestContentFactory.Create(1);
            var contentPart = TestContentFactory.CreatePart(data);

            // Act.
            Infrastructure.Content.Store(entry.Id, content);
            Infrastructure.Content.Store(entry.Id, contentPart);
            var retrievedContentPart = Infrastructure.Content.Get(entry.Id, 0);

            // Assert.
            AssertData.AreEqual(contentPart, retrievedContentPart);
        }

        [TestMethod]
        public void ContentRepository_Store_ContentDefinition_Null_Content()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var content = (Content)null;

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.Content.Store(entry.Id, content);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [TestMethod]
        public void ContentRepository_Store_ContentDefinition_No_Identifier()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var content = TestContentFactory.Create();

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.Content.Store(Identifier.Empty, content);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [TestMethod]
        public void ContentRepository_Get_Content()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var content = TestContentFactory.Create();

            // Act.
            Infrastructure.Content.Store(entry.Id, content);
            var retrievedContent = Infrastructure.Content.Get(entry.Id);

            // Assert.
            AssertData.AreEqual(content, retrievedContent, true);
        }
    }
}
