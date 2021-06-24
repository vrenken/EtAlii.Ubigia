// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Xunit;

    [Trait("Technology", "Grpc")]
    public class SystemConnectionDataConnectionTests : IClassFixture<InfrastructureUnitTestContext>, IAsyncLifetime
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private string _accountName;
        private string _password;
        private string[] _spaceNames;
        private ISystemConnection _systemConnection;

        public SystemConnectionDataConnectionTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _accountName = Guid.NewGuid().ToString();
            _password = Guid.NewGuid().ToString();
            _spaceNames = new[]
            {
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            };
            _systemConnection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            await _testContext.HostTestContext.AddUserAccountAndSpaces(_systemConnection, _accountName, _password, _spaceNames ).ConfigureAwait(false);
        }

        public Task DisposeAsync()
        {
            _systemConnection = null;
            _accountName = null;
            _spaceNames = null;
            _password = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_DataConnection_Open()
        {
            // Arrange.

            // Act.
            var connection = await _systemConnection.OpenSpace(_accountName, _spaceNames[0]).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
            Assert.NotNull(connection.Storage);//, "connection.Storage")
            Assert.NotNull(connection.Account);//, "connection.Account")
            Assert.Equal(_accountName, connection.Account.Name);
            Assert.NotNull(connection.Space);//, "connection.Space")
            Assert.Equal(_spaceNames[0], connection.Space.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_DataConnection_Open_And_Close_01()
        {
            // Arrange.
            var connection = await _systemConnection.OpenSpace(_accountName, _spaceNames[0]).ConfigureAwait(false);

            // Act.
            await connection.Close().ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
            Assert.Null(connection.Storage);//, "connection.Storage")
            Assert.Null(connection.Account);//, "connection.Account")
            Assert.Null(connection.Space);//, "connection.Space")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_DataConnection_Open_And_Close_And_Open_01()
        {
            // Arrange.
            var connection = await _systemConnection.OpenSpace(_accountName, _spaceNames[0]).ConfigureAwait(false);

            // Act.
            await connection.Close().ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
            Assert.Null(connection.Storage);//, "connection.Storage")
            Assert.Null(connection.Account);//, "connection.Account")
            Assert.Null(connection.Space);//, "connection.Space")

            // Act.
            await connection.Open().ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
            Assert.NotNull(connection.Storage);//, "connection.Storage")
            Assert.NotNull(connection.Account);//, "connection.Account")
            Assert.NotNull(connection.Space);//, "connection.Space")
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);

            // Act.
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);

            // Assert.
            Assert.NotNull(managementConnection);
            Assert.True(managementConnection.IsConnected);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection_Add_User_Account()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(account);
            Assert.True(account.Id != Guid.Empty);
            Assert.Equal(accountName, account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection_Add_Administrator_Account()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.Administrator).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(account);
            Assert.True(account.Id != Guid.Empty);
            Assert.Equal(accountName, account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection_Add_User_Account_And_Data_Space()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

            // Act.
            var space = await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(space);
            Assert.True(space.Id != Guid.Empty);
            Assert.Equal(spaceName, space.Name);
            Assert.Equal(account.Id, space.AccountId);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection_Add_Administrator_Account_And_Data_Space()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.Administrator).ConfigureAwait(false);

            // Act.
            var space = await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(space);
            Assert.True(space.Id != Guid.Empty);
            Assert.Equal(spaceName, space.Name);
            Assert.Equal(account.Id, space.AccountId);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection_Add_User_Account_And_Data_Space_And_Open_Space()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            var space = await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data).ConfigureAwait(false);

            // Act.
            var spaceConnection = await connection.OpenSpace(accountName, space.Name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(spaceConnection);
            Assert.Equal(account.Id, spaceConnection.Account.Id);
            Assert.Equal(space.Id, spaceConnection.Space.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SystemConnection_OpenManagementConnection_Add_Administrator_Account_And_Data_Space_And_Open_Space()
        {
            // Arrange.
            var connection = await _testContext.HostTestContext.CreateSystemConnection().ConfigureAwait(false);
            var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.Administrator).ConfigureAwait(false);
            var space = await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data).ConfigureAwait(false);

            // Act.
            var spaceConnection = await connection.OpenSpace(accountName, space.Name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(spaceConnection);
            Assert.Equal(account.Id, spaceConnection.Account.Id);
            Assert.Equal(space.Id, spaceConnection.Space.Id);
        }
    }
}
