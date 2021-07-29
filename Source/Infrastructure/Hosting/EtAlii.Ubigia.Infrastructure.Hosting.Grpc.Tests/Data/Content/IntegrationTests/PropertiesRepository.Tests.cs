// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;

    [Trait("Technology", "Grpc")]
    public sealed class PropertiesRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public PropertiesRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task PropertiesRepository_Store_Properties()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
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
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
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
