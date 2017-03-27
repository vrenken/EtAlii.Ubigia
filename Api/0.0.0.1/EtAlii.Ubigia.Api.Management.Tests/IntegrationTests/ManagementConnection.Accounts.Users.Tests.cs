namespace EtAlii.Ubigia.Api.Management.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Management.Tests.TestAssembly;

    
    public class ManagementConnection_Accounts_Users_Tests : IClassFixture<NotStartedTransportUnitTestContext>, IDisposable
    {
        private readonly NotStartedTransportUnitTestContext _testContext;

        public ManagementConnection_Accounts_Users_Tests(NotStartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
            var task = Task.Run(async () =>
            {
                await _testContext.TransportTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.TransportTestContext.Stop();
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Single_User()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);

            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Multiple_Users()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User);

                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_Single_User()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);

            account = await connection.Accounts.Get(account.Id);

            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_Multiple_Users()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User);

                account = await connection.Accounts.Get(account.Id);

                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_First_User_Full_Add()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
                accounts.Add(account);
            }

            foreach (var account in accounts)
            {
                var retrievedAccount = await connection.Accounts.Get(account.Id);

                Assert.NotNull(retrievedAccount);
                Assert.Equal(account.Name, retrievedAccount.Name);
                Assert.Equal(account.Password, retrievedAccount.Password);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_No_User()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var retrievedAccounts = await connection.Accounts.GetAll();

            // Assert.
            Assert.NotNull(retrievedAccounts);
            // We have the system and administrator accounts, 
            // so 2 accounts need to be used in the equation.
            Assert.Equal(2, retrievedAccounts.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_All_Users()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
                accounts.Add(account);
            }

            var retrievedAccounts = await connection.Accounts.GetAll();

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_User()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);

            name = Guid.NewGuid().ToString();
            password = Guid.NewGuid().ToString();
            account = await connection.Accounts.Change(account.Id, name, password);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue, account.Created);
            Assert.NotNull(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_User_Roles_01()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(0, account.Roles.Length);

            account.Roles = new[] { "First", "Second", "Third" };
            account = await connection.Accounts.Change(account);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
            Assert.Equal("Third", account.Roles[2]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_User_Roles_02()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(0, account.Roles.Length);

            account.Roles = new[] { "First", "Second", "Third" };
            account = await connection.Accounts.Change(account);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
            Assert.Equal("Third", account.Roles[2]);

            account.Roles = new[] { "First", "Second", };
            account = await connection.Accounts.Change(account);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(2, account.Roles.Length);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(2, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Delete_User()
        {
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            account = await connection.Accounts.Get(account.Id);
            Assert.NotNull(account);

            await connection.Accounts.Remove(account.Id);

            account = await connection.Accounts.Get(account.Id);
            Assert.Null(account);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Delete_Non_Existing_User()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();

            // Act.
            var act = connection.Accounts.Remove(id);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_Non_Existing_User()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var act = connection.Accounts.Change(id, name, password);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Already_Existing_User()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(name, password, AccountTemplate.User);
            Assert.NotNull(account);

            // Act.
            var act = connection.Accounts.Add(name, password, AccountTemplate.User);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_User_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = connection.Accounts.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), AccountTemplate.User);

            // Assert.
            Assert.Null(connection.Accounts);
            //await ExceptionAssert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_User_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = connection.Accounts.Get(Guid.NewGuid());

            // Assert.
            Assert.Null(connection.Accounts);
            //await ExceptionAssert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Delete_User_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = connection.Accounts.Remove(Guid.NewGuid());

            // Assert.
            Assert.Null(connection.Accounts);
            //await ExceptionAssert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_GetAll_Users_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = connection.Accounts.GetAll();

            // Assert.
            Assert.Null(connection.Accounts);
            //await ExceptionAssert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_User_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = connection.Accounts.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(connection.Accounts);
            //await ExceptionAssert.ThrowsAsync<NullReferenceException>(act);
        }
    }
}
