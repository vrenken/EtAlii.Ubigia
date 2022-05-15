// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests
{
    using System;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using EtAlii.xTechnology.Threading;

    [CorrelateUnitTests]
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

        [Fact]
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

        [Fact]
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
