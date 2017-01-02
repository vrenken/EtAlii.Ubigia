namespace EtAlii.Servus.Api.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Storage = EtAlii.Servus.Client.Model.Storage;

    [TestClass, ExcludeFromCodeCoverage]
    public class StorageConnectionStorages_Tests : StorageConnection_Tests
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
        public void StorageConnection_Storages_Add()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = connection.Storages.Add(name, address);

            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);
        }

        [TestMethod]
        public void StorageConnection_Storages_Add_Multiple()
        {
            var connection = CreateStorageConnection();

            for(int i=0; i<10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var address = Guid.NewGuid().ToString();

                var storage = connection.Storages.Add(name, address);
            
                Assert.IsNotNull(storage);
                Assert.AreEqual(name, storage.Name);
                Assert.AreEqual(address, storage.Address);
            }
        }

        [TestMethod]
        public void StorageConnection_Storages_Get()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = connection.Storages.Add(name, address);

            storage = connection.Storages.Get(storage.Id);

            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);
        }

        [TestMethod]
        public void StorageConnection_Storages_Get_Multiple()
        {
            var connection = CreateStorageConnection();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var address = Guid.NewGuid().ToString();

                var storage = connection.Storages.Add(name, address);

                storage = connection.Storages.Get(storage.Id);

                Assert.IsNotNull(storage);
                Assert.AreEqual(name, storage.Name);
                Assert.AreEqual(address, storage.Address);
            }
        }

        [TestMethod]
        public void StorageConnection_Storages_Get_First_Full_Add()
        {
            var connection = CreateStorageConnection();

            var storages = new List<Storage>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var address = Guid.NewGuid().ToString();

                var storage = connection.Storages.Add(name, address);
                Assert.IsNotNull(storage);
                Assert.AreEqual(name, storage.Name);
                Assert.AreEqual(address, storage.Address);
                storages.Add(storage);
            }

            foreach (var storage in storages)
            {
                var retrievedStorage = connection.Storages.Get(storage.Id);

                Assert.IsNotNull(retrievedStorage);
                Assert.AreEqual(storage.Name, retrievedStorage.Name);
                Assert.AreEqual(storage.Address, retrievedStorage.Address);
            }
        }


        //[TestMethod]
        //public void StorageConnection_Storages_Get_None()
        //{
        //    var connection = CreateStorageConnection();
        //    var retrievedStorages = connection.Storages.GetAll();
        //    Assert.IsNotNull(retrievedStorages);
        //    Assert.AreEqual(1, retrievedStorages.Count());
        //    Assert.AreEqual(Configuration.Address, retrievedStorages.First().Address);
        //    Assert.AreEqual(Configuration.Name, retrievedStorages.First().Name);
        //}

        //[TestMethod]
        //public void StorageConnection_Storages_Get_All()
        //{
        //    var connection = CreateStorageConnection();

        //    var storages = new List<Storage>(); 

        //    for (int i = 0; i < 10; i++)
        //    {
        //        var name = Guid.NewGuid().ToString();
        //        var address = Guid.NewGuid().ToString();

        //        var storage = connection.Storages.Add(name, address);
        //        Assert.IsNotNull(storage);
        //        Assert.AreEqual(name, storage.Name);
        //        Assert.AreEqual(address, storage.Address);
        //        storages.Add(storage);
        //    }

        //    var retrievedStorages = connection.Storages.GetAll();

        //    Assert.AreEqual(storages.Count + 1, retrievedStorages.Count());

        //    foreach (var storage in storages)
        //    {
        //        var matchingStorage = retrievedStorages.Single(s => s.Id == storage.Id);
        //        Assert.IsNotNull(matchingStorage);
        //        Assert.AreEqual(storage.Name, matchingStorage.Name);
        //        Assert.AreEqual(storage.Address, matchingStorage.Address);
        //    }
        //}

        [TestMethod]
        public void StorageConnection_Storages_Change()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = connection.Storages.Add(name, address);
            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);

            storage = connection.Storages.Get(storage.Id);
            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);

            name = Guid.NewGuid().ToString();
            address = Guid.NewGuid().ToString();
            storage = connection.Storages.Change(storage.Id, name, address);
            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);

            storage = connection.Storages.Get(storage.Id);
            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);
        }

        [TestMethod]
        public void StorageConnection_Storages_Delete()
        {
            var connection = CreateStorageConnection();

            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = connection.Storages.Add(name, address);
            Assert.IsNotNull(storage);
            Assert.AreEqual(name, storage.Name);
            Assert.AreEqual(address, storage.Address);

            storage = connection.Storages.Get(storage.Id);
            Assert.IsNotNull(storage);

            connection.Storages.Remove(storage.Id);

            storage = connection.Storages.Get(storage.Id);
            Assert.IsNull(storage);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Delete_Non_Existing()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();

            connection.Storages.Remove(id);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Change_Non_Existing()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            connection.Storages.Change(id, name, address);
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Add_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Storages.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Get_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Storages.Get(Guid.NewGuid());
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Remove_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Storages.Remove(Guid.NewGuid());
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Get_All_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Storages.GetAll();
        }

        [TestMethod, ExpectedException(typeof(InvalidInfrastructureOperationException))]
        public void StorageConnection_Storages_Change_With_Closed_Connection()
        {
            var connection = CreateStorageConnection(false);
            connection.Storages.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }


        [TestMethod]
        public void StorageConnection_Add_Already_Existing_Storage()
        {
            var connection = CreateStorageConnection();

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();

            var storage = connection.Storages.Add(name, address);
            Assert.IsNotNull(storage);

            storage = connection.Storages.Add(name, address);
            Assert.IsNull(storage);
        }
    }
}
