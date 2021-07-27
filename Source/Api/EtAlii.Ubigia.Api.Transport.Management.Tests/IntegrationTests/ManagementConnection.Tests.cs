// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class ManagementConnectionTests : IClassFixture<StartedTransportUnitTestContext>, IDisposable
    {
        private readonly StartedTransportUnitTestContext _testContext;

        public ManagementConnectionTests(StartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public void Dispose()
        {
            // Dispose any relevant resources.
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open()
        {
	        var host = _testContext.Transport.Host;
            var connection = await _testContext.Transport.CreateManagementConnection(host.ServiceDetails.ManagementAddress, host.TestAccountName, host.TestAccountPassword, false).ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Invalid_Password()
        {
			// Arrange.
			var host = _testContext.Transport.Host;
            var connection = await _testContext.Transport.CreateManagementConnection(host.ServiceDetails.ManagementAddress, host.TestAccountName, host.TestAccountPassword + "BAAD", false).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Invalid_Account()
        {
			// Arrange.
			var host = _testContext.Transport.Host;
            var connection = await _testContext.Transport.CreateManagementConnection(host.ServiceDetails.ManagementAddress, host.TestAccountName + "BAAD", host.TestAccountPassword, false).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Invalid_Account_And_Password()
        {
			// Arrange.
			var host = _testContext.Transport.Host;
            var connection = await _testContext.Transport.CreateManagementConnection(host.ServiceDetails.ManagementAddress, host.TestAccountName + "BAAD", host.TestAccountPassword + "BAAD", false).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_Already_Open()
        {
			// Arrange.
			var host = _testContext.Transport.Host;
            var connection = await _testContext.Transport.CreateManagementConnection(host.ServiceDetails.ManagementAddress, host.TestAccountName, host.TestAccountPassword, false).ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Open_And_Close()
        {
			// Act.
			var host = _testContext.Transport.Host;
            var connection = await _testContext.Transport.CreateManagementConnection(host.ServiceDetails.ManagementAddress, host.TestAccountName, host.TestAccountPassword, false).ConfigureAwait(false);

            // Arrange.
            await connection.Open().ConfigureAwait(false);
            await connection.Close().ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Close()
        {
            // Arrange.
            var connection = await _testContext.Transport.CreateManagementConnection(false).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Close().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace()
        {
            // Arrange.
            var connection = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

            // Act.
            var spaceConnection = await connection.OpenSpace(accountName, SpaceName.Data).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(spaceConnection);
            Assert.Equal(account.Id, spaceConnection.Account.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace_NonExisting_Space()
        {
            // Arrange.
            var connection = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.OpenSpace(accountName, Guid.NewGuid().ToString()).ConfigureAwait(false));

            // Assert.
            Assert.NotNull(account);
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace_NonExisting_Space_And_Account()
        {
            // Arrange.
            var connection = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.OpenSpace(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()).ConfigureAwait(false));

            // Assert.
            Assert.NotNull(account);
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_OpenSpace_NonExisting_Account()
        {
            // Arrange.
            var connection = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.OpenSpace(Guid.NewGuid().ToString(), SpaceName.Data).ConfigureAwait(false));

            // Assert.
            Assert.NotNull(account);
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }
    }
}
