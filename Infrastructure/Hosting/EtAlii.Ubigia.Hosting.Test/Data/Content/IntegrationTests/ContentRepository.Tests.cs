namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure;

    public sealed class ContentRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public ContentRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void ContentRepository_Store_Content()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = TestContent.Create();

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, content);

            // Assert.
            Assert.True(content.Stored);
        }

        [Fact]
        public void ContentRepository_Store_ContentPart()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var data = TestContent.CreateData(100, 500);
            var contentDefinition = TestContentDefinition.Create(data);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            var content = TestContent.Create(1);
            var contentPart = TestContent.CreatePart(data);

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, content);
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, contentPart);

            // Assert.
            Assert.True(content.Stored);
            Assert.True(contentPart.Stored);
        }


        [Fact]
        public void ContentRepository_Store_ContentPart_Out_Of_Bounds()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = TestContent.Create(3);
            var contentPart = TestContent.CreatePart(6);
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, content);

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, contentPart);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public void ContentRepository_Store_ContentPart_Before_Content()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = TestContent.Create(1);
            var contentPart = TestContent.CreatePart(0);

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, contentPart);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public void ContentRepository_Retrieve_Content()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var data = TestContent.CreateData(100, 500);
            var contentDefinition = TestContentDefinition.Create(data);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            var content = TestContent.Create(1);
            var contentPart = TestContent.CreatePart(data);

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, content);
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, contentPart);
            var retrievedContentPart = _testContext.HostTestContext.Host.Infrastructure.Content.Get(entry.Id, 0);

            // Assert.
            AssertData.AreEqual(contentPart, retrievedContentPart);
        }

        [Fact]
        public void ContentRepository_Store_ContentDefinition_Null_Content()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = (Content)null;

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, content);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public void ContentRepository_Store_ContentDefinition_No_Identifier()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = TestContent.Create();

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.Content.Store(Identifier.Empty, content);
            });

            // Assert.
            ExceptionAssert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public void ContentRepository_Get_Content()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = TestContent.Create();

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.Content.Store(entry.Id, content);
            var retrievedContent = _testContext.HostTestContext.Host.Infrastructure.Content.Get(entry.Id);

            // Assert.
            AssertData.AreEqual(content, retrievedContent, true);
        }
    }
}
