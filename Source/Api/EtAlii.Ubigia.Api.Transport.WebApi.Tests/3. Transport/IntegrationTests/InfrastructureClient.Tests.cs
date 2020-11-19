namespace EtAlii.Ubigia.Api.Transport.WebApi.Tests
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class InfrastructureClientIntegrationTests
    {
        private readonly Uri _url = new Uri("http://api.openkeyval.org/", UriKind.Absolute);

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
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

            await client.Post(new Uri(_url + identifier), testMessage);
        }

        [Fact(Skip="Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public async Task InfrastructureClient_Get()
        {
            if (IsConnectedToInternet())
            {
                var httpClientFactory = new DefaultHttpClientFactory();
                var client = new DefaultInfrastructureClient(httpClientFactory);

                var result = await client.Get<TestPackage>(new Uri("http://echo.jsontest.com/first/ping/second/pong/third/42", UriKind.Absolute));
                Assert.NotNull(result);
                Assert.Equal("ping", result.First);
                Assert.Equal("pong", result.Second);
                Assert.Equal(42, result.Third);
            }
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        //Creating a function that uses the API function...
        private static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out var _, 0);
        }

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
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

            await client.Post(new Uri(_url + identifier), testMessage);

            //var result = infrastructureClient.Get<TestMessage>(_url + identifier)
            //Assert.NotNull(result)
            //Assert.Equal(result.Name, testMessage.Name)
            //Assert.Equal(result.Value, testMessage.Value)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task InfrastructureClient_Get_Null()
        {
            // Arrange.
            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Act.
            var act = new Func<Task>(async () => await client.Get<TestStatus>(null));

            // Assert.
            await Assert.ThrowsAsync<InfrastructureConnectionException>(act);
        }
    }
}
