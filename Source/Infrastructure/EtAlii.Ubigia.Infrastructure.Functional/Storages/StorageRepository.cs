// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class StorageRepository :  IStorageRepository
{
    private readonly ILogicalContext _logicalContext;
    private readonly IStorageInitializer _storageInitializer;
    private readonly ILocalStorageGetter _localStorageGetter;

    public StorageRepository(
        ILogicalContext logicalContext,
        IStorageInitializer storageInitializer,
        ILocalStorageGetter localStorageGetter)
    {
        _logicalContext = logicalContext;
        _storageInitializer = storageInitializer;
        _localStorageGetter = localStorageGetter;
    }

    /// <inheritdoc />
    public Task<Storage> GetLocal()
    {
        var storage = _localStorageGetter.GetLocal();
        return Task.FromResult(storage);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Storage> GetAll()
    {
        return _logicalContext.Storages.GetAll();
    }

    /// <inheritdoc />
    public Task<Storage> Get(string name)
    {
        return _logicalContext.Storages.Get(name);
    }

    /// <inheritdoc />
    public Task<Storage> Get(Guid itemId)
    {
        return _logicalContext.Storages.Get(itemId);
    }

    /// <inheritdoc />
    public Task<Storage> Update(Guid itemId, Storage item)
    {
        return _logicalContext.Storages.Update(itemId, item);
    }

    /// <inheritdoc />
    public async Task<Storage> Add(Storage item)
    {
        var storage = await _logicalContext.Storages
            .Add(item)
            .ConfigureAwait(false);

        if (storage != null)
        {
            await _storageInitializer
                .Initialize(storage)
                .ConfigureAwait(false);
        }

        return storage;
    }

    /// <inheritdoc />
    public Task Remove(Guid itemId)
    {
        return _logicalContext.Storages.Remove(itemId);
    }

    /// <inheritdoc />
    public Task Remove(Storage item)
    {
        return _logicalContext.Storages.Remove(item);
    }
}
