namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
{
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Tests;
    using Xunit;


    public sealed class PropertiesRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public PropertiesRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void PropertiesRepository_Store_Properties()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var properties = _testContext.TestPropertiesFactory.Create();

            // Act.
            _testContext.HostTestContext.Host.Infrastructure.Properties.Store(entry.Id, properties);

            // Assert.
            Assert.True(properties.Stored);
        }

        [Fact]
        public void PropertiesRepository_Retrieve_Properties()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var properties = _testContext.TestPropertiesFactory.CreateComplete();
            _testContext.HostTestContext.Host.Infrastructure.Properties.Store(entry.Id, properties);

            // Act.
            var retrievedProperties = _testContext.HostTestContext.Host.Infrastructure.Properties.Get(entry.Id);

            // Assert.
            Assert.True(_testContext.PropertyDictionaryComparer.AreEqual(properties, retrievedProperties));
            Assert.True(retrievedProperties.Stored);
            Assert.True(properties.Stored);
        }
    }
}
