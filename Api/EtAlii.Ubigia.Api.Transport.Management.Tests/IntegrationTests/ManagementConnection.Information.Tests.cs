namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ManagementConnectionInformationTests : IClassFixture<NotStartedTransportUnitTestContext>, IAsyncLifetime
    {
        private readonly NotStartedTransportUnitTestContext _testContext;

        public ManagementConnectionInformationTests(NotStartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            await _testContext.TransportTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await _testContext.TransportTestContext.Stop();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Information_Get_ConnectivityDetails()
        {
            // Arrange.

            // Act.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            
            // Assert.
            Assert.NotNull(connection.Details.ManagementAddress);
            Assert.Equal(_testContext.TransportTestContext.Context.ServiceDetails.ManagementAddress.Scheme, connection.Details.ManagementAddress.Scheme);
            Assert.Equal(_testContext.TransportTestContext.Context.ServiceDetails.ManagementAddress.Port, connection.Details.ManagementAddress.Port);
            Assert.Equal(_testContext.TransportTestContext.Context.ServiceDetails.ManagementAddress.PathAndQuery, connection.Details.ManagementAddress.PathAndQuery);
            Assert.NotNull(connection.Details.DataAddress);
            Assert.Equal(_testContext.TransportTestContext.Context.ServiceDetails.DataAddress.Scheme, connection.Details.DataAddress.Scheme);
            Assert.Equal(_testContext.TransportTestContext.Context.ServiceDetails.DataAddress.Port, connection.Details.DataAddress.Port);
            Assert.Equal(_testContext.TransportTestContext.Context.ServiceDetails.DataAddress.PathAndQuery, connection.Details.DataAddress.PathAndQuery);
        }
    }
}