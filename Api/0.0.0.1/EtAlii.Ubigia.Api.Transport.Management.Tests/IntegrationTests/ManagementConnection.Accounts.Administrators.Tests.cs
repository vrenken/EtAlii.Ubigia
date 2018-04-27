namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    
    public class ManagementConnectionAccountsAdministratorsTests : IClassFixture<NotStartedTransportUnitTestContext>, IDisposable
    {
        private readonly NotStartedTransportUnitTestContext _testContext;

        public ManagementConnectionAccountsAdministratorsTests(NotStartedTransportUnitTestContext testContext)
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
        public async Task ManagementConnection_Accounts_InitializeAndCleanupOnly_01()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var account = await connection.Accounts.Add("JohnDoe", "123", AccountTemplate.Administrator);
            account = await connection.Accounts.Get(account.Id);
            await connection.Close();

            // Assert.
            Assert.NotNull(account);
            Assert.Equal("JohnDoe",account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_InitializeAndCleanupOnly_02()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var account = await connection.Accounts.Add("JohnDoe", "123", AccountTemplate.Administrator);
            account = await connection.Accounts.Get(account.Id);
            await connection.Close();

            // Assert.
            Assert.NotNull(account);
            Assert.Equal("JohnDoe", account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_InitializeAndCleanupOnly_03()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var account = await connection.Accounts.Add("JohnDoe", "123", AccountTemplate.Administrator);
            account = await connection.Accounts.Get(account.Id);
            await connection.Close();

            // Assert.
            Assert.NotNull(account);
            Assert.Equal("JohnDoe", account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_InitializeAndCleanupOnly_04()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var account = await connection.Accounts.Add("JohnDoe", "123", AccountTemplate.Administrator);
            account = await connection.Accounts.Get(account.Id);
            await connection.Close();

            // Assert.
            Assert.NotNull(account);
            Assert.Equal("JohnDoe", account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_InitializeAndCleanupOnly_05()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var account = await connection.Accounts.Add("JohnDoe", "123", AccountTemplate.Administrator);
            account = await connection.Accounts.Get(account.Id);
            await connection.Close();

            // Assert.
            Assert.NotNull(account);
            Assert.Equal("JohnDoe", account.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Single_Administrator()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            // Act.
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Multiple_Administrators()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            for (int i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                // Act.
                var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

                // Assert.
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
            }

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_Single_Administrator()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_Multiple_Administrators()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();

            for (int i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                // Act.
                var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);
                account = await connection.Accounts.Get(account.Id);

                // Assert.
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
            }

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_First_Administrator_Full_Add()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                // Act.
                var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

                // Assert.
                Assert.NotNull(account);
                Assert.Equal(name, account.Name);
                Assert.Equal(password, account.Password);
                accounts.Add(account);
            }

            foreach (var account in accounts)
            {
                // Act.
                var retrievedAccount = await connection.Accounts.Get(account.Id);

                // Assert.
                Assert.NotNull(retrievedAccount);
                Assert.Equal(account.Name, retrievedAccount.Name);
                Assert.Equal(account.Password, retrievedAccount.Password);
            }

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_No_Administrator()
        {
            // Arrange.

            // Act.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var retrievedAccounts = await connection.Accounts.GetAll();
            
            // Assert.
            Assert.NotNull(retrievedAccounts);
            // We have the system and administrator accounts, 
            // so 2 accounts need to be used in the equation.
            Assert.Equal(2, retrievedAccounts.Count());

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_All_Administrators()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                // Act.
                var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

                // Arrange.
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

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_Administrator()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue.ToUniversalTime(), account.Created);
            Assert.Null(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);

            // Arrange.
            name = Guid.NewGuid().ToString();
            password = Guid.NewGuid().ToString();

            // Act.
            account = await connection.Accounts.Change(account.Id, name, password);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.NotEqual(DateTime.MinValue, account.Created);
            Assert.NotNull(account.Updated);
            Assert.NotEqual(Guid.Empty, account.Id);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_Administrator_Roles_01()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
	        Assert.Equal(2, account.Roles.Length);
	        Assert.Contains(Role.Admin, account.Roles);
	        Assert.Contains(Role.User, account.Roles);
	        Assert.DoesNotContain("", account.Roles);
	        Assert.DoesNotContain(null, account.Roles);

			// Act.
			account.Roles = new[] { "First", "Second", "Third" };
            account = await connection.Accounts.Change(account);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
            Assert.Equal("Third", account.Roles[2]);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_Administrator_Roles_02()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
	        Assert.Equal(2, account.Roles.Length);
	        Assert.Contains(Role.Admin, account.Roles);
	        Assert.Contains(Role.User, account.Roles);
	        Assert.DoesNotContain("", account.Roles);
	        Assert.DoesNotContain(null, account.Roles);
			// Act.
			account.Roles = new[] { "First", "Second", "Third" };
            account = await connection.Accounts.Change(account);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(3, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);
            Assert.Equal("Third", account.Roles[2]);

            // Act.
            account.Roles = new[] { "First", "Second", };
            account = await connection.Accounts.Change(account);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(2, account.Roles.Length);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Equal(2, account.Roles.Length);
            Assert.Equal("First", account.Roles[0]);
            Assert.Equal("Second", account.Roles[1]);

            // Assure.
            await connection.Close();
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Delete_Administrator()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);

            // Act.
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.NotNull(account);

            // Act.
            await connection.Accounts.Remove(account.Id);
            account = await connection.Accounts.Get(account.Id);

            // Assert.
            Assert.Null(account);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Delete_Non_Existing_Administrator()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();

            // Act.
            var act = new Func<Task>(async () => await connection.Accounts.Remove(id));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_Non_Existing_Administrator()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var act = new Func<Task>(async () => await connection.Accounts.Change(id, name, password));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Already_Existing_Administrator()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var account = await connection.Accounts.Add(name, password, AccountTemplate.Administrator);
            Assert.NotNull(account);

            // Act.
            var act = new Func<Task>(async () => await connection.Accounts.Add(name, password, AccountTemplate.Administrator));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);

            // Assure.
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Add_Administrator_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), AccountTemplate.Administrator));

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Get_Administrator_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Get(Guid.NewGuid()));

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Delete_Administrator_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Remove(Guid.NewGuid()));

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_GetAll_Administrators_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.GetAll());

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Accounts_Change_Administrator_With_Closed_Connection()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection(false);

            // Act.
            //var act = new Func<Task>(async () => await connection.Accounts.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            // Assert.
            Assert.Null(connection.Accounts);
            //await Assert.ThrowsAsync<NullReferenceException>(act);
        }
    }
}
