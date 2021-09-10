// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class ManagementConnectionAccountsUsersTests : IClassFixture<NotStartedTransportUnitTestContext>, IAsyncLifetime
    {
        private readonly NotStartedTransportUnitTestContext _testContext;

        public ManagementConnectionAccountsUsersTests(NotStartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            await _testContext.Transport.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await _testContext.Transport.Stop().ConfigureAwait(false);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Add_Single_User()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);

            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Add_Multiple_Users()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);

                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
            }
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Get_Single_User()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);

            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Get_Multiple_Users()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);

                account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);

                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
            }
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Get_First_User_Full_Add()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var accounts = new List<Account>();

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
                accounts.Add(account);
            }

            foreach (var account in accounts)
            {
                var retrievedAccount = await connection.Accounts.Get(account.Id).ConfigureAwait(false);

                Assert.NotNull(retrievedAccount);
                Assert.Equal(account.Name, retrievedAccount.Name);
                Assert.Equal(account.Password, retrievedAccount.Password);
            }
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Get_No_User()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            // Act.
            var retrievedAccounts = await connection.Accounts
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedAccounts);
            // We have the system and administrator accounts,
            // so 2 accounts need to be used in the equation.
            Assert.Equal(2, retrievedAccounts.Count());
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Get_All_Users()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var accounts = new List<Account>();

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
                accounts.Add(account);
            }

            var retrievedAccounts = await connection.Accounts.GetAll().ToArrayAsync().ConfigureAwait(false);

            // We have the system and administrator accounts,
            // so 2 additional accounts need to be used in the equation.
            Assert.Equal(accounts.Count + 2, retrievedAccounts.Count());

            foreach (var account in accounts)
            {
                var matchingAccount = retrievedAccounts.Single(a => a.Id == account.Id);
                Assert.NotNull(matchingAccount);
                Assert.Equal(account.Name, matchingAccount.Name);
                Assert.Equal(account.Password, matchingAccount.Password);
            }
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Change_User()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);

            name = Guid.NewGuid().ToString();
            password = Guid.NewGuid().ToString();
            account = await connection.Accounts.Change(account.Id, name, password).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue, account.Created);
            Assert.NotNull(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Change_User_Roles_01()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
	        Assert.Single(account.Roles);
	        Assert.Contains(Role.User, account.Roles);
	        Assert.DoesNotContain("", account.Roles);
	        Assert.DoesNotContain(null, account.Roles);

			account.Roles = new[] { "First", "Second", "Third" };
            account = await connection.Accounts.Change(account).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
            Assert.Equal("Third", account.Roles[2]);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Change_User_Roles_02()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
	        Assert.Single(account.Roles);
	        Assert.Contains(Role.User, account.Roles);
	        Assert.DoesNotContain("", account.Roles);
	        Assert.DoesNotContain(null, account.Roles);

			account.Roles = new[] { "First", "Second", "Third" };
            account = await connection.Accounts.Change(account).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
            Assert.Equal("Third", account.Roles[2]);

            account.Roles = new[] { "First", "Second", };
            account = await connection.Accounts.Change(account).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(2, account.Roles.Length);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(2, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
        }


        [Fact]
        public async Task ManagementConnection_Accounts_Delete_User()
        {
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.NotNull(account);

            await connection.Accounts.Remove(account.Id).ConfigureAwait(false);

            account = await connection.Accounts.Get(account.Id).ConfigureAwait(false);
            Assert.Null(account);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Delete_Non_Existing_User()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var id = Guid.NewGuid();

            // Act.
            var act = new Func<Task>(async () => await connection.Accounts.Remove(id).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Change_Non_Existing_User()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var act = new Func<Task>(async () => await connection.Accounts.Change(id, name, password).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Add_Already_Existing_User()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(account);

            // Act.
            var act = new Func<Task>(async () => await connection.Accounts.Add(name, password, AccountTemplate.User).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Add_User_With_Closed_Connection()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection(false).ConfigureAwait(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), AccountTemplate.User))

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act)
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Get_User_With_Closed_Connection()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection(false).ConfigureAwait(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Get(Guid.NewGuid()))

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act)
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Delete_User_With_Closed_Connection()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection(false).ConfigureAwait(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Remove(Guid.NewGuid()))

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act)
        }

        [Fact]
        public async Task ManagementConnection_Accounts_GetAll_Users_With_Closed_Connection()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection(false).ConfigureAwait(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.GetAll())

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act)
        }

        [Fact]
        public async Task ManagementConnection_Accounts_Change_User_With_Closed_Connection()
        {
            // TODO: Check these tests.
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection(false).ConfigureAwait(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()))

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act)
        }
    }
}
