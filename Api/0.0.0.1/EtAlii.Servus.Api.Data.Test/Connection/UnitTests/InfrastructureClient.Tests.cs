namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System.Text;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Newtonsoft.Json;

    [TestClass]
    public class InfrastructureClient_Tests
    {
        [TestInitialize]
        public void Initialize()
        {
            new InfrastructureClient(null, null).AuthenticationToken = null;
        }

        [TestCleanup]
        public void Cleanup()
        {
            new InfrastructureClient(null, null).AuthenticationToken = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_New()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var payloadSerializer = new PayloadSerializer(jsonSerializer);
            var httpClientFactory = new DefaultHttpClientFactory();

            // Act.
            var client = new InfrastructureClient(payloadSerializer, httpClientFactory);

            // Assert.
            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_New_Has_No_AuthenticationToken()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var payloadSerializer = new PayloadSerializer(jsonSerializer);
            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new InfrastructureClient(payloadSerializer, httpClientFactory);

            // Act.

            // Assert.
            Assert.IsNull(client.AuthenticationToken);
        }
    }
}
