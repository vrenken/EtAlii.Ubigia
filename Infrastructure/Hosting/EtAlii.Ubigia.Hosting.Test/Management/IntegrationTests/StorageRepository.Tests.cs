namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using Xunit;
    using System;
    using System.Linq;

    
    public sealed class StorageRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public StorageRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void StorageRepository_Add()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);
        }

        [Fact]
        public void StorageRepository_Get()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.Equal(addedStorage.Id, fetchedStorage.Id);
            Assert.Equal(addedStorage.Name, fetchedStorage.Name);
            Assert.Equal(addedStorage.Address, fetchedStorage.Address);

            Assert.Equal(storage.Id, fetchedStorage.Id);
            Assert.Equal(storage.Name, fetchedStorage.Name);
            Assert.Equal(storage.Address, fetchedStorage.Address);
        }

        [Fact]
        public void StorageRepository_Remove_By_Id()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.NotNull(fetchedStorage);

            repository.Remove(addedStorage.Id);

            fetchedStorage = repository.Get(addedStorage.Id);
            Assert.Null(fetchedStorage);
        }

        [Fact]
        public void StorageRepository_Remove_By_Instance()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.NotNull(fetchedStorage);

            repository.Remove(addedStorage);

            fetchedStorage = repository.Get(addedStorage.Id);
            Assert.Null(fetchedStorage);
        }

        [Fact]
        public void StorageRepository_Get_Null()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = repository.Get(Guid.NewGuid());
            Assert.Null(storage);
        }

        [Fact]
        public void StorageRepository_GetAll()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            storage = CreateStorage();
            addedStorage = repository.Add(storage);

            var storages = repository.GetAll();
            Assert.NotNull(storages);
            Assert.True(storages.Count() >= 2);
        }


        [Fact]
        public void StorageRepository_Get_By_Name()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Name);
            Assert.Equal(addedStorage.Id, fetchedStorage.Id);
            Assert.Equal(addedStorage.Name, fetchedStorage.Name);

            Assert.Equal(storage.Id, fetchedStorage.Id);
            Assert.Equal(storage.Name, fetchedStorage.Name);
        }

        [Fact]
        public void StorageRepository_Get_By_Invalid_Name()
        {
            var repository = _testContext.HostTestContext.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(Guid.NewGuid().ToString());
            Assert.Null(fetchedStorage);
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
