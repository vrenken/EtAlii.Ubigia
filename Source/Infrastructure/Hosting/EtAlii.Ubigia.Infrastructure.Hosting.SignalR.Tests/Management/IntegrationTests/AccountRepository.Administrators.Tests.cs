namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Xunit;

	[Trait("Technology", "SignalR")]
    public class AccountRepositoryAdministratorsTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public AccountRepositoryAdministratorsTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task AccountRepository_Add_Administrator()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(DateTime.MinValue, addedAccount.Created);
            Assert.Null(addedAccount.Updated);
            Assert.NotEqual(Guid.Empty, addedAccount.Id);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(Guid.Empty, addedAccount.Id);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Invalid_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid());
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_AccountName()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Invalid_AccountName()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid().ToString());
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_AccountName_And_Password()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name, addedAccount.Password);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_AccountName_And_Invalid_Password()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name, Guid.NewGuid().ToString());
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Invalid_AccountName_And_Password()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid().ToString(), addedAccount.Password);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Remove_Administrator_By_Id()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.NotNull(fetchedAccount);

            repository.Remove(addedAccount.Id);

            fetchedAccount = repository.Get(addedAccount.Id);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Remove_Administrator_By_Instance()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.NotNull(fetchedAccount);

            repository.Remove(addedAccount);

            fetchedAccount = repository.Get(addedAccount.Id);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public void AccountRepository_Get_Administrator_Null()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = repository.Get(Guid.NewGuid());
            Assert.Null(account);
        }

        [Fact]
        public async Task AccountRepository_GetAll_Administrators()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount1 = repository.Add(account, AccountTemplate.Administrator);
            account = CreateAccount();
            var addedAccount2 = repository.Add(account, AccountTemplate.Administrator);

            // Act.
            var accounts = await repository
	            .GetAll()
	            .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedAccount1);
            Assert.NotNull(addedAccount2);
            Assert.NotNull(accounts);
            Assert.True(accounts.Length >= 2);
        }

        private Account CreateAccount()
        {
            return new()
            {
                Name = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
            };
        }
    }
}
