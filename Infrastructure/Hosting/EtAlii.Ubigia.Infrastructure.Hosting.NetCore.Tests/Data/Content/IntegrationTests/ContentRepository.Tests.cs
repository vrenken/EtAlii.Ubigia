namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Xunit;

    [Trait("Technology", "NetCore")]
    public sealed class ContentRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public ContentRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ContentRepository_Store_Content()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            context.Host.Infrastructure.Content.Store(entry.Id, content);

            // Assert.
            Assert.True(content.Stored);
        }

        [Fact]
        public async Task ContentRepository_Store_ContentPart()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var data = _testContext.TestContentFactory.CreateData(100, 500);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(data);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            var content = _testContext.TestContentFactory.Create(1);
            var contentPart = _testContext.TestContentFactory.CreatePart(data);

            // Act.
            context.Host.Infrastructure.Content.Store(entry.Id, content);
            context.Host.Infrastructure.Content.Store(entry.Id, contentPart);

            // Assert.
            Assert.True(content.Stored);
            Assert.True(contentPart.Stored);
        }


        [Fact]
        public async Task ContentRepository_Store_ContentPart_Out_Of_Bounds()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = _testContext.TestContentFactory.Create(3);
            var contentPart = _testContext.TestContentFactory.CreatePart(6);
            context.Host.Infrastructure.Content.Store(entry.Id, content);

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.Content.Store(entry.Id, contentPart);
            });

            // Assert.
            Assert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public async Task ContentRepository_Store_ContentPart_Before_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = _testContext.TestContentFactory.Create(1);
            var contentPart = _testContext.TestContentFactory.CreatePart(0);

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.Content.Store(entry.Id, contentPart);
            });

            // Assert.
            Assert.NotNull(content);
            Assert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public async Task ContentRepository_Retrieve_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var data = _testContext.TestContentFactory.CreateData(100, 500);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(data);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            var content = _testContext.TestContentFactory.Create(1);
            var contentPart = _testContext.TestContentFactory.CreatePart(data);

            // Act.
            context.Host.Infrastructure.Content.Store(entry.Id, content);
            context.Host.Infrastructure.Content.Store(entry.Id, contentPart);
            var retrievedContentPart = context.Host.Infrastructure.Content.Get(entry.Id, 0);

            // Assert.
            Assert.True(_testContext.ContentComparer.AreEqual(contentPart, retrievedContentPart));
        }

        [Fact]
        public async Task ContentRepository_Store_ContentDefinition_Null_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = (Content)null;

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.Content.Store(entry.Id, content);
            });

            // Assert.
            Assert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public async Task ContentRepository_Store_ContentDefinition_No_Identifier()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.Content.Store(Identifier.Empty, content);
            });

            // Assert.
            Assert.NotNull(entry);
            Assert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public async Task ContentRepository_Get_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            context.Host.Infrastructure.Content.Store(entry.Id, content);
            var retrievedContent = context.Host.Infrastructure.Content.Get(entry.Id);

            // Assert.
            Assert.True(_testContext.ContentComparer.AreEqual(content, retrievedContent, true));
        }
    }
}
