namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Management;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Storage = EtAlii.Servus.Api.Storage;

    [TestClass]
    public class StorageDataClientStub_Tests
    {
        [TestMethod]
        public void StorageDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var storageDataClientStub = new StorageClientStub();

            // Assert.
        }

        [TestMethod]
        public void StorageDataClientStub_Add()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            var storage = storageDataClientStub.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(storage);
        }

        [TestMethod]
        public void StorageDataClientStub_Change()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            var storage = storageDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(storage);
        }

        [TestMethod]
        public void StorageDataClientStub_Connect()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            storageDataClientStub.Connect();

            // Assert.
        }

        [TestMethod]
        public void StorageDataClientStub_Disconnect()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            storageDataClientStub.Disconnect();

            // Assert.
        }

        [TestMethod]
        public void StorageDataClientStub_Get_By_Id()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            var storage = storageDataClientStub.Get(Guid.NewGuid());

            // Assert.
            Assert.IsNull(storage);
        }

        [TestMethod]
        public void StorageDataClientStub_Get_By_Name()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            var storage = storageDataClientStub.Get(Guid.NewGuid().ToString());

            // Assert.
            Assert.IsNull(storage);
        }

        [TestMethod]
        public void StorageDataClientStub_GetAll()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
            var storages = storageDataClientStub.GetAll();

            // Assert.
            Assert.IsNull(storages);
        }

        [TestMethod]
        public void StorageDataClientStub_Remove()
        {
            // Arrange.
            var storageDataClientStub = new StorageClientStub();

            // Act.
           storageDataClientStub.Remove(Guid.NewGuid());

            // Assert.
        }
    }
}
