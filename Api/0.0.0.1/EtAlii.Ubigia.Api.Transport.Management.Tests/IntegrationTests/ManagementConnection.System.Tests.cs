namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ManagementConnection_System_Tests : IClassFixture<StartedTransportUnitTestContext>, IDisposable
    {
        private readonly StartedTransportUnitTestContext _testContext;

        public ManagementConnection_System_Tests(StartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;

        }

        public void Dispose()
        {

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_System_Account()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var systemAccount = await connection.Accounts.Get("System");

            // Assert.
            Assert.NotNull(systemAccount);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_System_Spaces()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var systemAccount = await connection.Accounts.Get("System");

            // Act.
            var spaces = await connection.Spaces.GetAll(systemAccount.Id);

            // Assert.
            // Each user is initialized with at least a configuration and a data space. so we need to expect two spaces .
            Assert.Equal(2, spaces.Count());
            Assert.True(spaces.SingleOrDefault(s => s.Name == "System") != null, "System space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Metrics") != null, "Metrics space not found");
        }
        [Fact, Trait("Category", TestAssembly.Category)]

        public async Task ManagementConnection_Administrator_Spaces()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var administratorAccount = await connection.Accounts.Get("Administrator");

            // Act.
            var spaces = await connection.Spaces.GetAll(administratorAccount.Id);

            // Assert.
            Assert.Equal(2, spaces.Count());
            //Assert.True(spaces.SingleOrDefault(s => s.Name == "System") != null, "System space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Configuration") != null, "Configuration space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Data") != null, "Data space not found");
        }
    }
}
