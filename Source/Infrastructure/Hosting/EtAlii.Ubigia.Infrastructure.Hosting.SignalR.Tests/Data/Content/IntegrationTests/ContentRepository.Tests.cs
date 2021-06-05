namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;

    [Trait("Technology", "SignalR")]
    public sealed class ContentRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public ContentRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ContentRepository_Store_Content()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            await context.Host.Infrastructure.Content.Store(entry.Id, content).ConfigureAwait(false);

            // Assert.
            Assert.True(content.Stored);
        }

        [Fact]
        public async Task ContentRepository_Store_ContentPart()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var data = _testContext.TestContentFactory.CreateData(100, 500);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(data);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            var content = _testContext.TestContentFactory.Create(1);
            var contentPart = _testContext.TestContentFactory.CreatePart(data);

            // Act.
            await context.Host.Infrastructure.Content.Store(entry.Id, content).ConfigureAwait(false);
            await context.Host.Infrastructure.Content.Store(entry.Id, contentPart).ConfigureAwait(false);

            // Assert.
            Assert.True(content.Stored);
            Assert.True(contentPart.Stored);
        }


        [Fact]
        public async Task ContentRepository_Store_ContentPart_Out_Of_Bounds()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create(3);
            var contentPart = _testContext.TestContentFactory.CreatePart(6);
            await context.Host.Infrastructure.Content.Store(entry.Id, content).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await context.Host.Infrastructure.Content.Store(entry.Id, contentPart).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ContentRepositoryException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ContentRepository_Store_ContentPart_Before_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create(1);
            var contentPart = _testContext.TestContentFactory.CreatePart(0);

            // Act.
            var act = new Func<Task>(async () => await context.Host.Infrastructure.Content.Store(entry.Id, contentPart).ConfigureAwait(false));

            // Assert.
            Assert.NotNull(content);
            await Assert.ThrowsAsync<ContentRepositoryException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ContentRepository_Retrieve_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var data = _testContext.TestContentFactory.CreateData(100, 500);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(data);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            var content = _testContext.TestContentFactory.Create(1);
            var contentPart = _testContext.TestContentFactory.CreatePart(data);

            // Act.
            await context.Host.Infrastructure.Content.Store(entry.Id, content).ConfigureAwait(false);
            await context.Host.Infrastructure.Content.Store(entry.Id, contentPart).ConfigureAwait(false);
            var retrievedContentPart = await context.Host.Infrastructure.Content.Get(entry.Id, 0).ConfigureAwait(false);

            // Assert.
            Assert.True(_testContext.ContentComparer.AreEqual(contentPart, retrievedContentPart));
        }

        [Fact]
        public async Task ContentRepository_Store_ContentDefinition_Null_Content()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.Content.Store(entry.Id, (Content) null);
            });

            // Assert.
            Assert.Throws<ContentRepositoryException>(act);
        }

        [Fact]
        public async Task ContentRepository_Store_ContentDefinition_No_Identifier()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
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
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var content = _testContext.TestContentFactory.Create();

            // Act.
            await context.Host.Infrastructure.Content.Store(entry.Id, content).ConfigureAwait(false);
            var retrievedContent = await context.Host.Infrastructure.Content.Get(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.True(_testContext.ContentComparer.AreEqual(content, retrievedContent, true));
        }
    }
}
