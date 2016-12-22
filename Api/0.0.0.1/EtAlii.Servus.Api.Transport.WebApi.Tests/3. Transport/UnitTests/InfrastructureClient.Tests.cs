namespace EtAlii.Servus.Api.Transport.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Api.Transport.WebApi;
    using Xunit;

    
    public class InfrastructureClient_UnitTests : IDisposable
    {
        public InfrastructureClient_UnitTests()
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
            var jsonSerializer = new SerializerFactory().Create();
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
            var jsonSerializer = new SerializerFactory().Create();
            var httpClientFactory = new DefaultHttpClientFactory();
            var client = new DefaultInfrastructureClient(httpClientFactory);

            // Act.

            // Assert.
            Assert.Null(client.AuthenticationToken);
        }
    }
}
