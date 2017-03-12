namespace EtAlii.Ubigia.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Storage.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;

    
    public sealed class ContentDefinitionRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public ContentDefinitionRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinition()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Assert.
            Assert.True(contentDefinition.Stored);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinition_Including_Parts()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            var contentDefinition = TestContentDefinition.Create(10);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

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
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = (ContentDefinition)null;

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_No_Identifier()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(Identifier.Empty, contentDefinition);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public void ContentDefinitionRepository_Get_ContentDefinition()
        {
            // Act.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinition.Create();
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Arrange.
            var retrievedContentDefinition = _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Get(entry.Id);

            // Assert.
            Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinition.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinition.CreatePart(0);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart);

            // Assert.
            Assert.True(contentDefinitionPart.Stored);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart_Outside_Bounds()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinition.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinition.CreatePart(2);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            
            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }

        [Fact]
        public void ContentDefinitionRepository_Store_ContentDefinitionPart_Twice()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var contentDefinition = TestContentDefinition.Create(0);
            contentDefinition.TotalParts = 1;
            var contentDefinitionPartFirst = TestContentDefinition.CreatePart(0);
            var contentDefinitionPartSecond = TestContentDefinition.CreatePart(0);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinition);
            _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartFirst);

            // Act.
            var act = new Action(() =>
            {
                _testContext.HostTestContext.Host.Infrastructure.ContentDefinition.Store(entry.Id, contentDefinitionPartSecond);
            });

            // Assert.
            ExceptionAssert.Throws<ContentDefinitionRepositoryException>(act);
        }
    }
}
