namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public sealed class ContentDefinitionRepository_Tests : TestBase
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
        public void ContentDefinitionRepository_Store_ContentDefinition()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinitionFactory.Create();

            // Act.
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Assert.
            Assert.IsTrue(contentDefinition.Stored);
        }

        [TestMethod]
        public void ContentDefinitionRepository_Store_ContentDefinition_Including_Parts()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);

            var contentDefinition = TestContentDefinitionFactory.Create(10);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            Assert.IsTrue(contentDefinition.Stored);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                Assert.IsTrue(contentDefinitionPart.Stored);
            }
        }

        [TestMethod]
        public void ContentDefinitionRepository_Store_Null()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = (ContentDefinition)null;

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [TestMethod]
        public void ContentDefinitionRepository_Store_No_Identifier()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinitionFactory.Create();

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.ContentDefinition.Store(Identifier.Empty, contentDefinition);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [TestMethod]
        public void ContentDefinitionRepository_Get_ContentDefinition()
        {
            // Act.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinitionFactory.Create();
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Arrange.
            var retrievedContentDefinition = Infrastructure.ContentDefinition.Get(entry.Id);

            // Assert.
            Assert.AreEqual(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        }

        [TestMethod]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(0);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Act.
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart);

            // Assert.
            Assert.IsTrue(contentDefinitionPart.Stored);
        }

        [TestMethod]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart_Outside_Bounds()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(2);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            
            // Act.
            var act = new Action(() =>
            {
                Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [TestMethod]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart_Twice()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPartFirst = TestContentDefinitionFactory.CreatePart(0);
            var contentDefinitionPartSecond = TestContentDefinitionFactory.CreatePart(0);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartFirst);

            // Act.
            var act = new Action(() =>
            {
                Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartSecond);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }
    }
}
