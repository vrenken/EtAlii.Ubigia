// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public sealed class StorageRepositoryTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public StorageRepositoryTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task StorageRepository_Add()
    {
        // Arrange.
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();

        // Act.
        var addedStorage = await repository.Add(storage).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(addedStorage);
        Assert.NotEqual(addedStorage.Id, Guid.Empty);
    }

    [Fact]
    public async Task StorageRepository_Get()
    {
        // Arrange.
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();
        var addedStorage = await repository.Add(storage).ConfigureAwait(false);
        Assert.NotNull(addedStorage);
        Assert.NotEqual(addedStorage.Id, Guid.Empty);

        // Act.
        var fetchedStorage = await repository.Get(addedStorage.Id).ConfigureAwait(false);

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
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();
        var addedStorage = await repository.Add(storage).ConfigureAwait(false);
        Assert.NotNull(addedStorage);
        Assert.NotEqual(addedStorage.Id, Guid.Empty);

        var fetchedStorage = await repository.Get(addedStorage.Id).ConfigureAwait(false);
        Assert.NotNull(fetchedStorage);

        await repository.Remove(addedStorage.Id).ConfigureAwait(false);

        // Act.
        fetchedStorage = await repository.Get(addedStorage.Id).ConfigureAwait(false);

        // Assert.
        Assert.Null(fetchedStorage);
    }

    [Fact]
    public async Task StorageRepository_Remove_By_Instance()
    {
        // Arrange.
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();
        var addedStorage = await repository.Add(storage).ConfigureAwait(false);
        Assert.NotNull(addedStorage);
        Assert.NotEqual(addedStorage.Id, Guid.Empty);

        var fetchedStorage = await repository.Get(addedStorage.Id).ConfigureAwait(false);
        Assert.NotNull(fetchedStorage);

        // Act.
        await repository.Remove(addedStorage).ConfigureAwait(false);
        fetchedStorage = await repository.Get(addedStorage.Id).ConfigureAwait(false);

        // Assert.
        Assert.Null(fetchedStorage);
    }

    [Fact]
    public async Task StorageRepository_Get_Null()
    {
        // Arrange.
        var repository = _testContext.Functional.Storages;

        // Act.
        var storage = await repository.Get(Guid.NewGuid()).ConfigureAwait(false);

        // Assert.
        Assert.Null(storage);
    }

    [Fact]
    public async Task StorageRepository_GetAll()
    {
        // Arrange.
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();
        var addedStorage1 = await repository.Add(storage).ConfigureAwait(false);
        storage = CreateStorage();
        var addedStorage2 = await repository.Add(storage).ConfigureAwait(false);

        // Act.
        var storages = await repository
            .GetAll()
            .ToArrayAsync()
            .ConfigureAwait(false);

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
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();
        var addedStorage = await repository.Add(storage).ConfigureAwait(false);
        Assert.NotNull(addedStorage);
        Assert.NotEqual(addedStorage.Id, Guid.Empty);

        // Act.
        var fetchedStorage = await repository.Get(addedStorage.Name).ConfigureAwait(false);

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
        var repository = _testContext.Functional.Storages;
        var storage = CreateStorage();
        var addedStorage = await repository.Add(storage).ConfigureAwait(false);
        Assert.NotNull(addedStorage);
        Assert.NotEqual(addedStorage.Id, Guid.Empty);

        // Act.
        var fetchedStorage = await repository.Get(Guid.NewGuid().ToString()).ConfigureAwait(false);

        // Assert.
        Assert.Null(fetchedStorage);
    }


    private Storage CreateStorage()
    {
        return new()
        {
            Name = Guid.NewGuid().ToString(),
            Address = Guid.NewGuid().ToString(),
        };
    }
}
