namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class InfrastructureClient_Tests
    {
        private const string Url = "http://api.openkeyval.org/";

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_Post()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var httpClientFactory = new DefaultHttpClientFactory();
            var payloadSerializer = new PayloadSerializer(new SerializerFactory().Create());
            var client = new InfrastructureClient(payloadSerializer, httpClientFactory);

            client.Post(Url + identifier, testMessage);
        }

        [TestMethod, TestCategory(TestAssembly.Category), Ignore]
        public void InfrastructureClient_Get()
        {
            var hasNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (hasNetwork)
            {
                var httpClientFactory = new DefaultHttpClientFactory();
                var payloadSerializer = new PayloadSerializer(new SerializerFactory().Create());
                var client = new InfrastructureClient(payloadSerializer, httpClientFactory);

                var result = client.Get<TestPackage>("http://echo.jsontest.com/first/ping/second/pong/third/42");
                Assert.IsNotNull(result);
                Assert.AreEqual("ping", result.first);
                Assert.AreEqual("pong", result.second);
                Assert.AreEqual(42, result.third);
            }
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_Post_Get_Result()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var httpClientFactory = new DefaultHttpClientFactory();
            var payloadSerializer = new PayloadSerializer(new SerializerFactory().Create());
            var client = new InfrastructureClient(payloadSerializer, httpClientFactory);

            client.Post(Url + identifier, testMessage);

            //var result = infrastructureClient.Get<TestMessage>(_url + identifier);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.Name, testMessage.Name);
            //Assert.AreEqual(result.Value, testMessage.Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void InfrastructureClient_Get_Null()
        {
            // Arrange.
            var httpClientFactory = new DefaultHttpClientFactory();
            var payloadSerializer = new PayloadSerializer(new SerializerFactory().Create());
            var client = new InfrastructureClient(payloadSerializer, httpClientFactory);

            // Act.
            var act = new Action(() => client.Get<TestStatus>(null));

            // Assert.
            ExceptionAssert.Throws<InvalidOperationException>(act);
        }
    }
}
