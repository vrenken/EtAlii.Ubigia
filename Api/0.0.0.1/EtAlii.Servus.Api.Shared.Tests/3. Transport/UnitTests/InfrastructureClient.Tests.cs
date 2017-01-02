namespace EtAlii.Servus.Api.Transport.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InfrastructureClient_UnitTests
    {
        [TestInitialize]
        public void Initialize()
        {
            new DefaultInfrastructureClient(null).AuthenticationToken = null;
        }

        [TestCleanup]
        public void Cleanup()
        {
            new DefaultInfrastructureClient(null).AuthenticationToken = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_New()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var httpClientFactory = new DefaultHttpClientFactory();

            // Act.
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Assert.
            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_New_Has_No_AuthenticationToken()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Act.

            // Assert.
            Assert.IsNull(client.AuthenticationToken);
        }
    }
}
