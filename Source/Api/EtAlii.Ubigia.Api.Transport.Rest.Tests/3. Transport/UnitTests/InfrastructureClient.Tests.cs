// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using EtAlii.xTechnology.Threading;

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;


    public class InfrastructureClientUnitTests : IDisposable
    {
        public InfrastructureClientUnitTests()
        {
            new RestInfrastructureClient(null).AuthenticationToken = null;
        }

        public void Dispose()
        {
            new RestInfrastructureClient(null).AuthenticationToken = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void InfrastructureClient_New()
        {
            // Arrange.
            var contextCorrelator = new ContextCorrelator();
            var httpClientFactory = new RestHttpClientFactory(contextCorrelator);

            // Act.
            var client = new RestInfrastructureClient(httpClientFactory);

            // Assert.
            Assert.NotNull(client);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void InfrastructureClient_New_Has_No_AuthenticationToken()
        {
            // Arrange.
            var contextCorrelator = new ContextCorrelator();
            var httpClientFactory = new RestHttpClientFactory(contextCorrelator);
            var client = new RestInfrastructureClient(httpClientFactory);

            // Act.

            // Assert.
            Assert.Null(client.AuthenticationToken);
        }
    }
}
