namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class ManagementConnection_Tests : IClassFixture<StartedTransportUnitTestContext>, IDisposable
    {
        private readonly StartedTransportUnitTestContext _testContext;

        public ManagementConnection_Tests(StartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public void Dispose()
        {
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open()
        {
            var configuration = _testContext.TransportTestContext.Context.Host.Infrastructure.Configuration;
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(configuration.Address, configuration.Account, configuration.Password, false);
            await connection.Open();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Invalid_Password()
        {
            // Arrange.
            var configuration = _testContext.TransportTestContext.Context.Host.Infrastructure.Configuration;
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(configuration.Address, configuration.Account, configuration.Password + "BAAD", false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open());

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Invalid_Account()
        {
            // Arrange.
            var configuration = _testContext.TransportTestContext.Context.Host.Infrastructure.Configuration;
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(configuration.Address, configuration.Account + "BAAD", configuration.Password, false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open());

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Invalid_Account_And_Password()
        {
            // Arrange.
            var configuration = _testContext.TransportTestContext.Context.Host.Infrastructure.Configuration;
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(configuration.Address, configuration.Account + "BAAD", configuration.Password + "BAAD", false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open());

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Already_Open()
        {
            // Arrange.
            var configuration = _testContext.TransportTestContext.Context.Host.Infrastructure.Configuration;
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(configuration.Address, configuration.Account, configuration.Password, false);
            await connection.Open();

            // Act.
            var act = new Func<Task>(async () => await connection.Open());

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_And_Close()
        {
            // Act.
            var configuration = _testContext.TransportTestContext.Context.Host.Infrastructure.Configuration;
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(configuration.Address, configuration.Account, configuration.Password, false);

            // Arrange.
            await connection.Open();
            await connection.Close();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Close()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Close());

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User);

            // Act.
            var spaceConnection = await connection.OpenSpace(accountName, SpaceName.Data);

            // Assert.
            Assert.NotNull(spaceConnection);
            Assert.Equal(account.Id, spaceConnection.Account.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace_NonExisting_Space()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User);

            // Act.
            var act = new Func<Task>(async () => await connection.OpenSpace(accountName, Guid.NewGuid().ToString()));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace_NonExisting_Space_And_Account()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User);

            // Act.
            var act = new Func<Task>(async () => await connection.OpenSpace(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace_NonExisting_Account()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User);

            // Act.
            var act = new Func<Task>(async () => await connection.OpenSpace(Guid.NewGuid().ToString(), SpaceName.Data));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }
    }
}
