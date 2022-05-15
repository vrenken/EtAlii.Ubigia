// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using EtAlii.xTechnology.Threading;

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class InfrastructureClientIntegrationTests
    {
        [Fact]
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
