namespace EtAlii.Ubigia.Api.Transport.WebApi.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;

    
    public class InfrastructureClientUnitTests : IDisposable
    {
        public InfrastructureClientUnitTests()
        {
            new DefaultInfrastructureClient(null).AuthenticationToken = null;
        }

        public void Dispose()
        {
            new DefaultInfrastructureClient(null).AuthenticationToken = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void InfrastructureClient_New()
        {
            // Arrange.
            var httpClientFactory = new DefaultHttpClientFactory();

            // Act.
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Assert.
            Assert.NotNull(client);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void InfrastructureClient_New_Has_No_AuthenticationToken()
        {
            // Arrange.
            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Act.

            // Assert.
            Assert.Null(client.AuthenticationToken);
        }
    }
}
