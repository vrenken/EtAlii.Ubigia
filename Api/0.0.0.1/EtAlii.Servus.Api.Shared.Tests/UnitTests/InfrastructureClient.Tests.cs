namespace EtAlii.Servus.Api.Tests.UnitTests
{
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Newtonsoft.Json;

    [TestClass]
    public class InfrastructureClient_Tests
    {
        [TestInitialize]
        public void Initialize()
        {
            new InfrastructureClient(null).AuthenticationToken = null;
        }

        [TestCleanup]
        public void Cleanup()
        {
            new InfrastructureClient(null).AuthenticationToken = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_New()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var payloadSerializer = new PayloadSerializer(jsonSerializer);

            // Act.
            var client = new InfrastructureClient(payloadSerializer);

            // Assert.
            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_New_Has_No_AuthenticationToken()
        {
            // Arrange.
            var jsonSerializer = new SerializerFactory().Create();
            var payloadSerializer = new PayloadSerializer(jsonSerializer);
            var client = new InfrastructureClient(payloadSerializer);

            // Act.

            // Assert.
            Assert.IsNull(client.AuthenticationToken);
        }
    }
}
