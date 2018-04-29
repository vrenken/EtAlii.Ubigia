namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using EtAlii.Ubigia.Api;
    using Xunit;
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public sealed class ContentDefinitionRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public ContentDefinitionRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinition()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Assert.
            Assert.True(contentDefinition.Stored);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinition_Including_Parts()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);

            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(10);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            Assert.True(contentDefinition.Stored);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                Assert.True(contentDefinitionPart.Stored);
            }
        }

        [Fact]
        public void ContentDefinitionRepository_Store_Null()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = (ContentDefinition)null;

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            });

            // Assert.
            Assert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_No_Identifier()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
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
        public void ContentDefinitionRepository_Get_ContentDefinition()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Arrange.
            var retrievedContentDefinition = context.Host.Infrastructure.ContentDefinition.Get(entry.Id);

            // Assert.
            Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(0);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Act.
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart);

            // Assert.
            Assert.True(contentDefinitionPart.Stored);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart_Outside_Bounds()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart(2);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            
            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart);
            });

            // Assert.
            Assert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart_Twice()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPartFirst = _testContext.TestContentDefinitionFactory.CreatePart(0);
            var contentDefinitionPartSecond = _testContext.TestContentDefinitionFactory.CreatePart(0);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartFirst);

            // Act.
            var act = new Action(() =>
            {
                context.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartSecond);
            });

            // Assert.
            Assert.Throws<ContentDefinitionRepositoryException>(act);
        }
    }
}
