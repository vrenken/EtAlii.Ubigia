﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;

    [Trait("Technology", "Grpc")]
    public sealed class PropertiesRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public PropertiesRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task PropertiesRepository_Store_Properties()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
            var properties = _testContext.TestPropertiesFactory.Create();

            // Act.
            context.Host.Infrastructure.Properties.Store(entry.Id, properties);

            // Assert.
            Assert.True(properties.Stored);
        }

        [Fact]
        public async Task PropertiesRepository_Retrieve_Properties()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var entry = await context.Host.Infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
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
