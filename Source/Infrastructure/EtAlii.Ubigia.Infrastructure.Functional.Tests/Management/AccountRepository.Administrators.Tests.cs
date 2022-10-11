// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class AccountRepositoryAdministratorsTests : IClassFixture<FunctionalInfrastructureUnitTestContext>
    {
        private readonly FunctionalInfrastructureUnitTestContext _testContext;

        public AccountRepositoryAdministratorsTests(FunctionalInfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task AccountRepository_Add_Administrator()
        {
	        // Arrange.
            var repository = _testContext.Functional.Accounts;
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
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(Guid.Empty, addedAccount.Id);

            var fetchedAccount = await repository.Get(addedAccount.Id).ConfigureAwait(false);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Invalid_Id()
        {
	        // Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = await repository.Get(Guid.NewGuid()).ConfigureAwait(false);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_AccountName()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = await repository.Get(addedAccount.Name).ConfigureAwait(false);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Invalid_AccountName()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = await repository.Get(Guid.NewGuid().ToString()).ConfigureAwait(false);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_AccountName_And_Password()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = await repository.Get(addedAccount.Name, addedAccount.Password).ConfigureAwait(false);
            Assert.Equal(addedAccount.Id, fetchedAccount.Id);
            Assert.Equal(addedAccount.Name, fetchedAccount.Name);

            Assert.Equal(account.Id, fetchedAccount.Id);
            Assert.Equal(account.Name, fetchedAccount.Name);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_AccountName_And_Invalid_Password()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            // Act.
            var fetchedAccount = await repository.Get(addedAccount.Name, Guid.NewGuid().ToString()).ConfigureAwait(false);

            // Assert.
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_By_Invalid_AccountName_And_Password()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            // Act.
            var fetchedAccount = await repository.Get(Guid.NewGuid().ToString(), addedAccount.Password).ConfigureAwait(false);

            // Assert.
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Remove_Administrator_By_Id()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);
            var fetchedAccount = await repository.Get(addedAccount.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedAccount);

            // Act.
            await repository.Remove(addedAccount.Id).ConfigureAwait(false);

            // Assert.
            fetchedAccount = await repository.Get(addedAccount.Id).ConfigureAwait(false);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Remove_Administrator_By_Instance()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            Assert.NotNull(addedAccount);
            Assert.NotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = await repository.Get(addedAccount.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedAccount);

            // Act.
            await repository.Remove(addedAccount).ConfigureAwait(false);

            // Assert.
            fetchedAccount = await repository.Get(addedAccount.Id).ConfigureAwait(false);
            Assert.Null(fetchedAccount);
        }

        [Fact]
        public async Task AccountRepository_Get_Administrator_Null()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;

            // Act.
            var account = await repository.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
            Assert.Null(account);
        }

        [Fact]
        public async Task AccountRepository_GetAll_Administrators()
        {
			// Arrange.
            var repository = _testContext.Functional.Accounts;
            var account = CreateAccount();
            var addedAccount1 = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);
            account = CreateAccount();
            var addedAccount2 = await repository.Add(account, AccountTemplate.Administrator).ConfigureAwait(false);

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
