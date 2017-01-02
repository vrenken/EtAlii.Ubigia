namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    
    public class InfrastructureClient_IntegrationTests
    {
        private const string Url = "http://api.openkeyval.org/";

        //[Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact(Skip="Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public async Task InfrastructureClient_Get()
        {
            if (IsConnectedToInternet())
            {
                var httpClientFactory = new DefaultHttpClientFactory();
                var client = new DefaultInfrastructureClient(httpClientFactory);

                var result = await client.Get<TestPackage>("http://echo.jsontest.com/first/ping/second/pong/third/42");
                Assert.NotNull(result);
                Assert.Equal("ping", result.first);
                Assert.Equal("pong", result.second);
                Assert.Equal(42, result.third);
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
        //[Fact, Trait("Category", TestAssembly.Category)]
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
            //Assert.NotNull(result);
            //Assert.Equal(result.Name, testMessage.Name);
            //Assert.Equal(result.Value, testMessage.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
