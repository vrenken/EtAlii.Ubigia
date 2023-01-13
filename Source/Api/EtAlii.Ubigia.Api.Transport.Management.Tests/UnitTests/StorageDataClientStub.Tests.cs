// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class StorageDataClientStubTests
{
    [Fact]
    public void StorageDataClientStub_Create()
    {
        // Arrange.

        // Act.
        var storageDataClientStub = new StorageDataClientStub();

        // Assert.
        Assert.NotNull(storageDataClientStub);
    }

    [Fact]
    public async Task StorageDataClientStub_Add()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        var storage = await storageDataClientStub.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()).ConfigureAwait(false);

        // Assert.
        Assert.Null(storage);
    }

    [Fact]
    public async Task StorageDataClientStub_Change()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        var storage = await storageDataClientStub.Change(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()).ConfigureAwait(false);

        // Assert.
        Assert.Null(storage);
    }

    [Fact]
    public async Task StorageDataClientStub_Connect()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        await storageDataClientStub.Connect(null).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(storageDataClientStub);
    }

    [Fact]
    public async Task StorageDataClientStub_Disconnect()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        await storageDataClientStub.Disconnect(null).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(storageDataClientStub);
    }

    [Fact]
    public async Task StorageDataClientStub_Get_By_Id()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        var storage = await storageDataClientStub.Get(Guid.NewGuid()).ConfigureAwait(false);

        // Assert.
        Assert.Null(storage);
    }

    [Fact]
    public async Task StorageDataClientStub_Get_By_Name()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        var storage = await storageDataClientStub.Get(Guid.NewGuid().ToString()).ConfigureAwait(false);

        // Assert.
        Assert.Null(storage);
    }

    [Fact]
    public async Task StorageDataClientStub_GetAll()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        var storages = await storageDataClientStub
            .GetAll()
            .ToArrayAsync()
            .ConfigureAwait(false);

        // Assert.
        Assert.Empty(storages);
    }

    [Fact]
    public async Task StorageDataClientStub_Remove()
    {
        // Arrange.
        var storageDataClientStub = new StorageDataClientStub();

        // Act.
        await storageDataClientStub.Remove(Guid.NewGuid()).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(storageDataClientStub);
    }
}
