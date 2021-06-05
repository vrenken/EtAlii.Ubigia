namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;

    [Trait("Technology", "SignalR")]
    public sealed class ContentDefinitionRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public ContentDefinitionRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_ContentDefinition()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Assert.
            Assert.True(contentDefinition.Stored);
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_ContentDefinition_Including_Parts()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);

            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(10);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            Assert.True(contentDefinition.Stored);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                Assert.True(contentDefinitionPart.Stored);
            }
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_Null()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.ContentDefinition.Store(entry.Id, (ContentDefinition) null);
            });

            // Assert.
            Assert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_No_Identifier()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.ContentDefinition.Store(Identifier.Empty, contentDefinition);
            });

            // Assert.
            Assert.NotNull(entry);
            Assert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public async Task ContentDefinitionRepository_Get_ContentDefinition()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Arrange.
            var retrievedContentDefinition = await context.Host.Infrastructure.ContentDefinition.Get(entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_ContentDefinitionPart()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            Blob.SetTotalParts(contentDefinition, 1);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(0);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Act.
            await context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart).ConfigureAwait(false);

            // Assert.
            Assert.True(contentDefinitionPart.Stored);
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_ContentDefinitionPart_Outside_Bounds()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            Blob.SetTotalParts(contentDefinition, 1);
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(2);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Act.
            var act = new Func<Task>(async () => await context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ContentDefinitionRepositoryException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ContentDefinitionRepository_Store_ContentDefinitionPart_Twice()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            Blob.SetTotalParts(contentDefinition, 1);
            var contentDefinitionPartFirst = _testContext.TestContentDefinitionFactory.CreatePart(0);
            var contentDefinitionPartSecond = _testContext.TestContentDefinitionFactory.CreatePart(0);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            await context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartFirst).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartSecond).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ContentDefinitionRepositoryException>(act).ConfigureAwait(false);
        }
    }
}
