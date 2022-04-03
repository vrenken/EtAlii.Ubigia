// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class ManagementConnectionInformationTests : IClassFixture<NotStartedTransportUnitTestContext>, IAsyncLifetime
    {
        private readonly NotStartedTransportUnitTestContext _testContext;

        public ManagementConnectionInformationTests(NotStartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            await _testContext.Transport.Start().ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await _testContext.Transport.Stop().ConfigureAwait(false);
        }

        [Fact]
        public async Task ManagementConnection_Information_Get_ConnectivityDetails()
        {
            // Arrange.

            // Act.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection.Details.ManagementAddress);
            Assert.Equal(_testContext.Transport.Host.ServiceDetails.ManagementAddress.Scheme, connection.Details.ManagementAddress.Scheme);
            Assert.Equal(_testContext.Transport.Host.ServiceDetails.ManagementAddress.Port, connection.Details.ManagementAddress.Port);
            Assert.Equal(_testContext.Transport.Host.ServiceDetails.ManagementAddress.PathAndQuery, connection.Details.ManagementAddress.PathAndQuery);
            Assert.NotNull(connection.Details.DataAddress);
            Assert.Equal(_testContext.Transport.Host.ServiceDetails.DataAddress.Scheme, connection.Details.DataAddress.Scheme);
            Assert.Equal(_testContext.Transport.Host.ServiceDetails.DataAddress.Port, connection.Details.DataAddress.Port);
            Assert.Equal(_testContext.Transport.Host.ServiceDetails.DataAddress.PathAndQuery, connection.Details.DataAddress.PathAndQuery);
        }
    }
}
