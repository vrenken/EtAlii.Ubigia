namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class StorageDataClientStub_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void StorageDataClientStub_Create()
        {
            // Arrange.

            // Act.
            var storageDataClientStub = new StorageDataClientStub();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Add()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            var storage = await storageDataClientStub.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Change()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            var storage = await storageDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Connect()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            await storageDataClientStub.Connect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Disconnect()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            await storageDataClientStub.Disconnect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Get_By_Id()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            var storage = await storageDataClientStub.Get(Guid.NewGuid());

            // Assert.
            Assert.Null(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Get_By_Name()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            var storage = await storageDataClientStub.Get(Guid.NewGuid().ToString());

            // Assert.
            Assert.Null(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_GetAll()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
            var storages = await storageDataClientStub.GetAll();

            // Assert.
            Assert.Null(storages);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task StorageDataClientStub_Remove()
        {
            // Arrange.
            var storageDataClientStub = new StorageDataClientStub();

            // Act.
           await storageDataClientStub.Remove(Guid.NewGuid());

            // Assert.
        }
    }
}
