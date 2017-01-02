namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class AccountRepository_Tests : TestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void AccountRepository_Add()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);
        }

        [TestMethod]
        public void AccountRepository_Get_By_Id()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.AreEqual(addedAccount.Id, fetchedAccount.Id);
            Assert.AreEqual(addedAccount.Name, fetchedAccount.Name);

            Assert.AreEqual(account.Id, fetchedAccount.Id);
            Assert.AreEqual(account.Name, fetchedAccount.Name);
        }

        [TestMethod]
        public void AccountRepository_Get_By_Invalid_Id()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid());
            Assert.IsNull(fetchedAccount);
        }

        [TestMethod]
        public void AccountRepository_Get_By_AccountName()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name);
            Assert.AreEqual(addedAccount.Id, fetchedAccount.Id);
            Assert.AreEqual(addedAccount.Name, fetchedAccount.Name);

            Assert.AreEqual(account.Id, fetchedAccount.Id);
            Assert.AreEqual(account.Name, fetchedAccount.Name);
        }

        [TestMethod]
        public void AccountRepository_Get_By_Invalid_AccountName()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid().ToString());
            Assert.IsNull(fetchedAccount);
        }

        [TestMethod]
        public void AccountRepository_Get_By_AccountName_And_Password()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name, addedAccount.Password);
            Assert.AreEqual(addedAccount.Id, fetchedAccount.Id);
            Assert.AreEqual(addedAccount.Name, fetchedAccount.Name);

            Assert.AreEqual(account.Id, fetchedAccount.Id);
            Assert.AreEqual(account.Name, fetchedAccount.Name);
        }

        [TestMethod]
        public void AccountRepository_Get_By_AccountName_And_Invalid_Password()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Name, Guid.NewGuid().ToString());
            Assert.IsNull(fetchedAccount);
        }

        [TestMethod]
        public void AccountRepository_Get_By_Invalid_AccountName_And_Password()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(Guid.NewGuid().ToString(), addedAccount.Password);
            Assert.IsNull(fetchedAccount);
        }

        [TestMethod]
        public void AccountRepository_Remove_By_Id()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.IsNotNull(fetchedAccount);

            repository.Remove(addedAccount.Id);

            fetchedAccount = repository.Get(addedAccount.Id);
            Assert.IsNull(fetchedAccount);
        }

        [TestMethod]
        public void AccountRepository_Remove_By_Instance()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            Assert.IsNotNull(addedAccount);
            Assert.AreNotEqual(addedAccount.Id, Guid.Empty);

            var fetchedAccount = repository.Get(addedAccount.Id);
            Assert.IsNotNull(fetchedAccount);

            repository.Remove(addedAccount);

            fetchedAccount = repository.Get(addedAccount.Id);
            Assert.IsNull(fetchedAccount);
        }

        [TestMethod]
        public void AccountRepository_Get_Null()
        {
            var repository = Infrastructure.Accounts;
            var account = repository.Get(Guid.NewGuid());
            Assert.IsNull(account);
        }

        [TestMethod]
        public void AccountRepository_GetAll()
        {
            var repository = Infrastructure.Accounts;
            var account = CreateAccount();
            var addedAccount = repository.Add(account);
            account = CreateAccount();
            addedAccount = repository.Add(account);

            var accounts = repository.GetAll();
            Assert.IsNotNull(accounts);
            Assert.IsTrue(accounts.Count() >= 2);
        }

        private Account CreateAccount()
        {
            return new Account
            {
                Name = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
            };
        }
    }
}
