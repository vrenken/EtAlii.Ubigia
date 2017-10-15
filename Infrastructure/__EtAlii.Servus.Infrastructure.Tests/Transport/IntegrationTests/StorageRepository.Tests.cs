namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public sealed class StorageRepository_Tests : TestBase
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
        public void StorageRepository_Add()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.IsNotNull(addedStorage);
            Assert.AreNotEqual(addedStorage.Id, Guid.Empty);
        }

        [TestMethod]
        public void StorageRepository_Get()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.IsNotNull(addedStorage);
            Assert.AreNotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.AreEqual(addedStorage.Id, fetchedStorage.Id);
            Assert.AreEqual(addedStorage.Name, fetchedStorage.Name);
            Assert.AreEqual(addedStorage.Address, fetchedStorage.Address);

            Assert.AreEqual(storage.Id, fetchedStorage.Id);
            Assert.AreEqual(storage.Name, fetchedStorage.Name);
            Assert.AreEqual(storage.Address, fetchedStorage.Address);
        }

        [TestMethod]
        public void StorageRepository_Remove_By_Id()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.IsNotNull(addedStorage);
            Assert.AreNotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.IsNotNull(fetchedStorage);

            repository.Remove(addedStorage.Id);

            fetchedStorage = repository.Get(addedStorage.Id);
            Assert.IsNull(fetchedStorage);
        }

        [TestMethod]
        public void StorageRepository_Remove_By_Instance()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.IsNotNull(addedStorage);
            Assert.AreNotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.IsNotNull(fetchedStorage);

            repository.Remove(addedStorage);

            fetchedStorage = repository.Get(addedStorage.Id);
            Assert.IsNull(fetchedStorage);
        }

        [TestMethod]
        public void StorageRepository_Get_Null()
        {
            var repository = Infrastructure.Storages;
            var storage = repository.Get(Guid.NewGuid());
            Assert.IsNull(storage);
        }

        [TestMethod]
        public void StorageRepository_GetAll()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            storage = CreateStorage();
            addedStorage = repository.Add(storage);

            var storages = repository.GetAll();
            Assert.IsNotNull(storages);
            Assert.IsTrue(storages.Count() >= 2);
        }


        [TestMethod]
        public void StorageRepository_Get_By_Name()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.IsNotNull(addedStorage);
            Assert.AreNotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Name);
            Assert.AreEqual(addedStorage.Id, fetchedStorage.Id);
            Assert.AreEqual(addedStorage.Name, fetchedStorage.Name);

            Assert.AreEqual(storage.Id, fetchedStorage.Id);
            Assert.AreEqual(storage.Name, fetchedStorage.Name);
        }

        [TestMethod]
        public void StorageRepository_Get_By_Invalid_Name()
        {
            var repository = Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.IsNotNull(addedStorage);
            Assert.AreNotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(Guid.NewGuid().ToString());
            Assert.IsNull(fetchedStorage);
        }


        private Storage CreateStorage()
        {
            return new Storage
            {
                Name = Guid.NewGuid().ToString(),
                Address = Guid.NewGuid().ToString(),
            };
        }
    }
}
