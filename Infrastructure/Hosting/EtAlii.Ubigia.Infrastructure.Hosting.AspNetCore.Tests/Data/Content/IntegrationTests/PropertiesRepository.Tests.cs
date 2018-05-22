﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests
{
    using Xunit;


    [Trait("Technology", "AspNetCore")]
    public sealed class PropertiesRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public PropertiesRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void PropertiesRepository_Store_Properties()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var properties = _testContext.TestPropertiesFactory.Create();

            // Act.
            context.Host.Infrastructure.Properties.Store(entry.Id, properties);

            // Assert.
            Assert.True(properties.Stored);
        }

        [Fact]
        public void PropertiesRepository_Retrieve_Properties()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var properties = _testContext.TestPropertiesFactory.CreateComplete();
            context.Host.Infrastructure.Properties.Store(entry.Id, properties);

            // Act.
            var retrievedProperties = context.Host.Infrastructure.Properties.Get(entry.Id);

            // Assert.
            Assert.True(_testContext.PropertyDictionaryComparer.AreEqual(properties, retrievedProperties));
            Assert.True(retrievedProperties.Stored);
            Assert.True(properties.Stored);
        }
    }
}
