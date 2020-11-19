﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Xunit;

	[Trait("Technology", "Grpc")]
    public sealed class StorageRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public StorageRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task StorageRepository_Add()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();

			// Act.
	        var addedStorage = await repository.Add(storage);

			// Assert.
	        Assert.NotNull(addedStorage);
            Assert.NotEqual(addedStorage.Id, Guid.Empty);
        }

        [Fact]
        public async Task StorageRepository_Get()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = await repository.Add(storage);
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
        public async Task StorageRepository_Remove_By_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = await repository.Add(storage);
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
        public async Task StorageRepository_Remove_By_Instance()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = await repository.Add(storage);
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
        public async Task StorageRepository_GetAll()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage1 = await repository.Add(storage);
            storage = CreateStorage();
            var addedStorage2 = await repository.Add(storage);

			// Act.
            var storages = await repository
	            .GetAll()
	            .ToArrayAsync();

	        // Assert.
	        Assert.NotNull(addedStorage1);
	        Assert.NotNull(addedStorage2);
	        Assert.NotNull(storages);
            Assert.True(storages.Length >= 2);
        }


        [Fact]
        public async Task StorageRepository_Get_By_Name()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = await repository.Add(storage);
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
        public async Task StorageRepository_Get_By_Invalid_Name()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var repository = context.Host.Infrastructure.Storages;
            var storage = CreateStorage();
            var addedStorage = await repository.Add(storage);
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
