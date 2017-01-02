namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.Servus.Client.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [TestClass, ExcludeFromCodeCoverage]
    public class StorageConnectionSpaces_Tests : StorageConnection_Tests
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
        public void StorageConnection_Spaces_Add_Single()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var name = Guid.NewGuid().ToString();

            var space = connection.Spaces.Add(account.Id, name);

            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);
        }

        [TestMethod]
        public void StorageConnection_Spaces_Add_Multiple()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var space = connection.Spaces.Add(account.Id, name);

                Assert.IsNotNull(space);
                Assert.AreEqual(name, space.Name);
                Assert.AreEqual(account.Id, space.AccountId);
            }
        }

        [TestMethod]
        public void StorageConnection_Spaces_Get_Single()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var name = Guid.NewGuid().ToString();

            var space = connection.Spaces.Add(account.Id, name);

            space = connection.Spaces.Get(space.Id);

            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);
        }

        [TestMethod]
        public void StorageConnection_Spaces_Get_Multiple()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var space = connection.Spaces.Add(account.Id, name);

                space = connection.Spaces.Get(space.Id);

                Assert.IsNotNull(space);
                Assert.AreEqual(name, space.Name);
                Assert.AreEqual(account.Id, space.AccountId);
            }
        }

        [TestMethod]
        public void StorageConnection_Spaces_Get_First_Full_Add()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var spaces = new List<Space>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var space = connection.Spaces.Add(account.Id, name);
                Assert.IsNotNull(space);
                Assert.AreEqual(name, space.Name);
                Assert.AreEqual(account.Id, space.AccountId);
                spaces.Add(space);
            }

            foreach (var space in spaces)
            {
                var retrievedSpace = connection.Spaces.Get(space.Id);

                Assert.IsNotNull(retrievedSpace);
                Assert.AreEqual(space.Name, retrievedSpace.Name);
                Assert.AreEqual(account.Id, retrievedSpace.AccountId);
            }
        }

        [TestMethod]
        public void StorageConnection_Spaces_Get_None()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var retrievedSpaces = connection.Spaces.GetAll(account.Id);
            Assert.IsNotNull(retrievedSpaces);
            Assert.AreEqual(0, retrievedSpaces.Count());
        }

        [TestMethod]
        public void StorageConnection_Spaces_Get_All()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var spaces = new List<Space>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var space = connection.Spaces.Add(account.Id, name);
                Assert.IsNotNull(space);
                Assert.AreEqual(name, space.Name);
                Assert.AreEqual(account.Id, space.AccountId);
                spaces.Add(space);
            }

            var retrievedSpaces = connection.Spaces.GetAll(account.Id);

            Assert.AreEqual(spaces.Count, retrievedSpaces.Count());

            foreach (var space in spaces)
            {
                var matchingSpace = retrievedSpaces.Single(s => s.Id == space.Id);
                Assert.IsNotNull(matchingSpace);
                Assert.AreEqual(space.Name, matchingSpace.Name);
                Assert.AreEqual(account.Id, matchingSpace.AccountId);
            }
        }

        [TestMethod]
        public void StorageConnection_Spaces_Change()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var name = Guid.NewGuid().ToString();

            var space = connection.Spaces.Add(account.Id, name);
            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);

            space = connection.Spaces.Get(space.Id);
            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);

            name = Guid.NewGuid().ToString();

            space = connection.Spaces.Change(space.Id, name);
            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);

            space = connection.Spaces.Get(space.Id);
            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);
        }


        [TestMethod]
        public void StorageConnection_Spaces_Delete()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var name = Guid.NewGuid().ToString();

            var space = connection.Spaces.Add(account.Id, name);
            Assert.IsNotNull(space);
            Assert.AreEqual(name, space.Name);
            Assert.AreEqual(account.Id, space.AccountId);

            space = connection.Spaces.Get(space.Id);
            Assert.IsNotNull(space);

            connection.Spaces.Remove(space.Id);

            space = connection.Spaces.Get(space.Id);
            Assert.IsNull(space);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Spaces_Delete_Non_Existing()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();

            connection.Spaces.Remove(id);
        }



        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Spaces_Change_Non_Existing()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            connection.Spaces.Change(id, name);
        }

        [TestMethod]
        public void StorageConnection_Spaces_Add_Already_Existing()
        {
            var connection = CreateStorageConnection();
            var account = CreateAccount(connection);

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            var space = connection.Spaces.Add(account.Id, name);
            Assert.IsNotNull(space);

            space = connection.Spaces.Add(account.Id, name);
            Assert.IsNull(space);
        }
    }
}
