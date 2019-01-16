namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using EtAlii.Ubigia.Api;
    using Xunit;
    using System;
    using System.Linq;

    
	[Trait("Technology", "Grpc")]
    public sealed class StorageRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public StorageRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void StorageRepository_Add()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();

			// Act.
	        var addedStorage = repository.Add(storage);

			// Assert.
	        Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);
        }

        [Fact]
        public void StorageRepository_Get()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

	        // Act.
            var fetchedStorage = repository.Get(addedStorage.Id);

	        // Assert.
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
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.NotNull(fetchedStorage);

            repository.Remove(addedStorage.Id);

			// Act.
            fetchedStorage = repository.Get(addedStorage.Id);

			// Assert.
	        Assert.Null(fetchedStorage);
        }

        [Fact]
        public void StorageRepository_Remove_By_Instance()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

            var fetchedStorage = repository.Get(addedStorage.Id);
            Assert.NotNull(fetchedStorage);

			// Act.
            repository.Remove(addedStorage);
            fetchedStorage = repository.Get(addedStorage.Id);

	        // Assert.
	        Assert.Null(fetchedStorage);
        }

        [Fact]
        public void StorageRepository_Get_Null()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;

			// Act.
	        var storage = repository.Get(Guid.NewGuid());

	        // Assert.
	        Assert.Null(storage);
        }

        [Fact]
        public void StorageRepository_GetAll()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            storage = CreateStorage();
            addedStorage = repository.Add(storage);

			// Act.
            var storages = repository.GetAll();

	        // Assert.
	        Assert.NotNull(storages);
            Assert.True(storages.Count() >= 2);
        }


        [Fact]
        public void StorageRepository_Get_By_Name()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

			// Act.
            var fetchedStorage = repository.Get(addedStorage.Name);

	        // Assert.
	        Assert.Equal(addedStorage.Id, fetchedStorage.Id);
            Assert.Equal(addedStorage.Name, fetchedStorage.Name);
            Assert.Equal(storage.Id, fetchedStorage.Id);
            Assert.Equal(storage.Name, fetchedStorage.Name);
        }

        [Fact]
        public void StorageRepository_Get_By_Invalid_Name()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = repository.Add(storage);
            Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);

			// Act.
            var fetchedStorage = repository.Get(Guid.NewGuid().ToString());

	        // Assert.
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
