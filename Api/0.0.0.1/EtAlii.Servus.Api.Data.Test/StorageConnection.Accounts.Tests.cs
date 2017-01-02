namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.Servus.Client.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [TestClass, ExcludeFromCodeCoverage]
    public class StorageConnectionAccounts_Tests : StorageConnection_Tests
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
        public void StorageConnection_Accounts_Add_Single()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = connection.Accounts.Add(name, password);

            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);
        }

        [TestMethod]
        public void StorageConnection_Accounts_Add_Multiple()
        {
            var connection = CreateStorageConnection();

            for(int i=0; i<10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = connection.Accounts.Add(name, password);
            
                Assert.IsNotNull(account);
                Assert.AreEqual(name, account.Name);
                Assert.AreEqual(password, account.Password);
            }
        }

        [TestMethod]
        public void StorageConnection_Accounts_Get_Single()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = connection.Accounts.Add(name, password);

            account = connection.Accounts.Get(account.Id);

            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);
        }

        [TestMethod]
        public void StorageConnection_Accounts_Get_Multiple()
        {
            var connection = CreateStorageConnection();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = connection.Accounts.Add(name, password);

                account = connection.Accounts.Get(account.Id);

                Assert.IsNotNull(account);
                Assert.AreEqual(name, account.Name);
                Assert.AreEqual(password, account.Password);
            }
        }


        [TestMethod]
        public void StorageConnection_Accounts_Get_First_Full_Add()
        {
            var connection = CreateStorageConnection();

            var accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = connection.Accounts.Add(name, password);
                Assert.IsNotNull(account);
                Assert.AreEqual(name, account.Name);
                Assert.AreEqual(password, account.Password);
                accounts.Add(account);
            }

            foreach (var account in accounts)
            {
                var retrievedAccount = connection.Accounts.Get(account.Id);

                Assert.IsNotNull(retrievedAccount);
                Assert.AreEqual(account.Name, retrievedAccount.Name);
                Assert.AreEqual(account.Password, retrievedAccount.Password);
            }
        }

        [TestMethod]
        public void StorageConnection_Accounts_Get_None()
        {
            var connection = CreateStorageConnection();
            var retrievedAccounts = connection.Accounts.GetAll();
            Assert.IsNotNull(retrievedAccounts);
            Assert.AreEqual(0, retrievedAccounts.Count());
        }

        [TestMethod]
        public void StorageConnection_Accounts_Get_All()
        {
            var connection = CreateStorageConnection();

            var accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var password = Guid.NewGuid().ToString();

                var account = connection.Accounts.Add(name, password);
                Assert.IsNotNull(account);
                Assert.AreEqual(name, account.Name);
                Assert.AreEqual(password, account.Password);
                accounts.Add(account);
            }

            var retrievedAccounts = connection.Accounts.GetAll();

            Assert.AreEqual(accounts.Count, retrievedAccounts.Count());

            foreach (var account in accounts)
            {
                var matchingAccount = retrievedAccounts.Single(a => a.Id == account.Id);
                Assert.IsNotNull(matchingAccount);
                Assert.AreEqual(account.Name, matchingAccount.Name);
                Assert.AreEqual(account.Password, matchingAccount.Password);
            }
        }

        [TestMethod]
        public void StorageConnection_Accounts_Change()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = connection.Accounts.Add(name, password);
            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);

            account = connection.Accounts.Get(account.Id);
            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);

            name = Guid.NewGuid().ToString();
            password = Guid.NewGuid().ToString();
            account = connection.Accounts.Change(account.Id, name, password);
            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);

            account = connection.Accounts.Get(account.Id);
            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);
        }


        [TestMethod]
        public void StorageConnection_Delete_Account()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = connection.Accounts.Add(name, password);
            Assert.IsNotNull(account);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(password, account.Password);

            account = connection.Accounts.Get(account.Id);
            Assert.IsNotNull(account);

            connection.Accounts.Remove(account.Id);

            account = connection.Accounts.Get(account.Id);
            Assert.IsNull(account);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Delete_Non_Existing_Account()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();

            connection.Accounts.Remove(id);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Change_Non_Existing_Account()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            connection.Accounts.Change(id, name, password);
        }

        [TestMethod]
        public void StorageConnection_Add_Already_Existing_Account()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var account = connection.Accounts.Add(name, password);
            Assert.IsNotNull(account);

            account = connection.Accounts.Add(name, password);
            Assert.IsNull(account);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Accounts_Add_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Accounts.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Accounts_Get_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Accounts.Get(Guid.NewGuid());
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Accounts_Delete_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Accounts.Remove(Guid.NewGuid());
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Accounts_GetAll_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Accounts.GetAll();
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Accounts_Change_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Accounts.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    }
}
