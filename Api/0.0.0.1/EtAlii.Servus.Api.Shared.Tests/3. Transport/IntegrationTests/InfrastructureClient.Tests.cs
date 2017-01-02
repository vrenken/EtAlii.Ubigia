namespace EtAlii.Servus.Api.Transport.Tests
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InfrastructureClient_IntegrationTests
    {
        private const string Url = "http://api.openkeyval.org/";

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public async Task InfrastructureClient_Post()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            await client.Post(Url + identifier, testMessage);
        }

        [TestMethod, TestCategory(TestAssembly.Category), Ignore]
        public async Task InfrastructureClient_Get()
        {
            if (IsConnectedToInternet())
            {
                var httpClientFactory = new DefaultHttpClientFactory();
                var client = new DefaultInfrastructureClient(httpClientFactory);

                var result = await client.Get<TestPackage>("http://echo.jsontest.com/first/ping/second/pong/third/42");
                Assert.IsNotNull(result);
                Assert.AreEqual("ping", result.first);
                Assert.AreEqual("pong", result.second);
                Assert.AreEqual(42, result.third);
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        //[TestMethod, TestCategory(TestAssembly.Category)]
        public async Task InfrastructureClient_Post_Get_Result()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            await client.Post(Url + identifier, testMessage);

            //var result = infrastructureClient.Get<TestMessage>(_url + identifier);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.Name, testMessage.Name);
            //Assert.AreEqual(result.Value, testMessage.Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task InfrastructureClient_Get_Null()
        {
            // Arrange.
            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Act.
            var act = client.Get<TestStatus>(null);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
