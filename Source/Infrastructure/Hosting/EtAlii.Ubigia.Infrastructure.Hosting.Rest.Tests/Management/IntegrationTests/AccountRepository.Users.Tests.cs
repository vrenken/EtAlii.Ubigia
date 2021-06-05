namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Xunit;

	[Trait("Technology", "NetCore")]
    public class AccountRepositoryUserTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public AccountRepositoryUserTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task AccountRepository_Add_User()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(DateTime.MinValue, addedAccount.Created);
            Assert.Null(addedAccount.Updated);
            Assert.NotEqual(Guid.Empty, addedAccount.Id);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_Id()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(Guid.Empty, addedAccount.Id);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_Invalid_Id()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid());
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_AccountName()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_Invalid_AccountName()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid().ToString());
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_AccountName_And_Password()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name, addedAccount.Password);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_AccountName_And_Invalid_Password()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name, Guid.NewGuid().ToString());
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_User_By_Invalid_AccountName_And_Password()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid().ToString(), addedAccount.Password);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Remove_User_By_Id()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.NotNull(fetchedAccount);

            repository.Remove(addedAccount.Id);

            fetchedAccount = repository.Get(addedAccount.Id);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Remove_User_By_Instance()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.NotNull(fetchedAccount);

            repository.Remove(addedAccount);

            fetchedAccount = repository.Get(addedAccount.Id);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public void AccountRepository_Get_User_Null()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = repository.Get(Guid.NewGuid());
            Assert.Null(account);
        }

        [Fact]
        public async Task AccountRepository_GetAll_Users()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount1 = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);
            account = CreateAccount();
            var addedAccount2 = await repository.Add(account, AccountTemplate.User).ConfigureAwait(false);

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
