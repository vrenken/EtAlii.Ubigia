namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.Servus.Client.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [TestClass, ExcludeFromCodeCoverage]
    public class StorageConnection_Tests : TestBase
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
        public void StorageConnection_Open()
        {
            var connection = CreateStorageConnection(false);
            connection.Open(Configuration.Address, Configuration.Account, Configuration.Password);
        }

        [TestMethod, ExpectedException(typeof(UnauthorizedInfrastructureOperationException))]
        public void StorageConnection_Open_Invalid_Password()
        {
            var connection = CreateStorageConnection(false);
            connection.Open(Configuration.Address, Configuration.Account, Configuration.Password + "BAAD");
        }

        [TestMethod, ExpectedException(typeof(UnauthorizedInfrastructureOperationException))]
        public void StorageConnection_Open_Invalid_Account()
        {
            var connection = CreateStorageConnection(false);
            connection.Open(Configuration.Address, Configuration.Account + "BAAD", Configuration.Password);
        }

        [TestMethod, ExpectedException(typeof(UnauthorizedInfrastructureOperationException))]
        public void StorageConnection_Open_Invalid_Account_And_Password()
        {
            var connection = CreateStorageConnection(false);
            connection.Open(Configuration.Address, Configuration.Account + "BAAD", Configuration.Password + "BAAD");
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Open_Already_Open()
        {
            var connection = CreateStorageConnection(false);
            connection.Open(Configuration.Address, Configuration.Account, Configuration.Password);
            connection.Open(Configuration.Address, Configuration.Account, Configuration.Password);
        }

        [TestMethod]
        public void StorageConnection_Open_And_Close()
        {
            var connection = CreateStorageConnection(false);
            connection.Open(Configuration.Address, Configuration.Account, Configuration.Password);
            connection.Close();
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Close()
        {
            var connection = CreateStorageConnection(false);
            connection.Close();
        }

        public StorageConnection CreateStorageConnection(bool openOnCreation = true)
        {
            var connection = new StorageConnection();
            if (openOnCreation)
            {
                connection.Open(Configuration.Address, Configuration.Account, Configuration.Password);
            }
            return connection;
        }

        public Account CreateAccount(StorageConnection connection)
        {
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            return connection.Accounts.Add(name, password);
        }
    }
}
