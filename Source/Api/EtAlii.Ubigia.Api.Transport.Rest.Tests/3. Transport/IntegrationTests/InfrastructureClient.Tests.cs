// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using EtAlii.xTechnology.Threading;

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class InfrastructureClientIntegrationTests
    {
        private readonly Uri _url = new("http://api.openkeyval.org/", UriKind.Absolute);

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
        public async Task InfrastructureClient_Post()
        {
            // Arrange.
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var contextCorrelator = new ContextCorrelator();
            var httpClientFactory = new RestHttpClientFactory(contextCorrelator);
            var client = new RestInfrastructureClient(httpClientFactory);

            // Act.
            await client.Post(new Uri(_url + identifier), testMessage).ConfigureAwait(false);

            // Assert.
        }

        [Fact(Skip="Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public async Task InfrastructureClient_Get()
        {
            if (IsConnectedToInternet())
            {
                // Arrange.
                var contextCorrelator = new ContextCorrelator();
                var httpClientFactory = new RestHttpClientFactory(contextCorrelator);
                var client = new RestInfrastructureClient(httpClientFactory);

                // Act.
                var result = await client.Get<TestPackage>(new Uri("https://echo.jsontest.com/first/ping/second/pong/third/42", UriKind.Absolute)).ConfigureAwait(false);

                // Assert.
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
            // Arrange.
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var contextCorrelator = new ContextCorrelator();
            var httpClientFactory = new RestHttpClientFactory(contextCorrelator);
            var client = new RestInfrastructureClient(httpClientFactory);

            // Act.
            await client.Post(new Uri(_url + identifier), testMessage).ConfigureAwait(false);

            // Assert.
            //var result = infrastructureClient.Get<TestMessage>(_url + identifier)
            //Assert.NotNull(result)
            //Assert.Equal(result.Name, testMessage.Name)
            //Assert.Equal(result.Value, testMessage.Value)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task InfrastructureClient_Get_Null()
        {
            // Arrange.
            var contextCorrelator = new ContextCorrelator();
            var httpClientFactory = new RestHttpClientFactory(contextCorrelator);
            var client = new RestInfrastructureClient(httpClientFactory);

            // Act.
            var act = new Func<Task>(async () => await client.Get<TestStatus>(null).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InfrastructureConnectionException>(act).ConfigureAwait(false);
        }
    }
}
