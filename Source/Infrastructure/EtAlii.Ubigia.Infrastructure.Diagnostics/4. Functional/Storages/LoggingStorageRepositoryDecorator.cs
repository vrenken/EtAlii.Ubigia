﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using Serilog;

internal class LoggingStorageRepositoryDecorator : IStorageRepository
{
    private readonly IStorageRepository _repository;
    private readonly ILogger _logger = Log.ForContext<IStorageRepository>();

    public LoggingStorageRepositoryDecorator(
        IStorageRepository storageRepository
        //IProfiler profiler
    )
    {
        _repository = storageRepository;
    }

    public async Task<Storage> GetLocal()
    {
        _logger.Information("Getting local storage");
        var start = Environment.TickCount;

        var storage = await _repository.GetLocal().ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Got local storage (Duration: {Duration}ms)", duration);

        return storage;
    }

    public async IAsyncEnumerable<Storage> GetAll()
    {
        _logger.Information("Getting all storages");
        var start = Environment.TickCount;

        var items = _repository
            .GetAll()
            .ConfigureAwait(false);
        await foreach (var item in items)
        {
            yield return item;
        }

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Got all storages (Duration: {Duration}ms)", duration);
    }

    public async Task<Storage> Get(string name)
    {
        _logger.Information("Getting storage (Name: {StorageName})", name);
        var start = Environment.TickCount;

        var storage = await _repository.Get(name).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Got storage (Name: {StorageName} Duration: {Duration}ms)", name, duration);

        return storage;
    }

    public async Task<Storage> Get(Guid itemId)
    {
        _logger.Information("Getting storage (Id: {StorageId})", itemId);
        var start = Environment.TickCount;

        var storage = await _repository.Get(itemId).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Got storage (Id: {StorageId} Duration: {Duration}ms)", itemId, duration);

        return storage;
    }

    public async Task<Storage> Add(Storage item)
    {
        _logger.Information("Adding storage (Id: {StorageId})", item.Id);
        var start = Environment.TickCount;

        item = await _repository.Add(item).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Added storage (Id: {StorageId} Duration: {Duration}ms)", item.Id, duration);

        return item;
    }

    public async Task Remove(Guid itemId)
    {
        _logger.Information("Removing storage (Id: {StorageId})", itemId);
        var start = Environment.TickCount;

        await _repository.Remove(itemId).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Removed storage (Id: {StorageId} Duration: {Duration}ms)", itemId, duration);
    }

    public async Task Remove(Storage item)
    {
        _logger.Information("Removing storage (Id: {StorageId})", item.Id);
        var start = Environment.TickCount;

        await _repository.Remove(item).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Removed storage (Id: {StorageId} Duration: {Duration}ms)", item.Id, duration);
    }

    public async Task<Storage> Update(Guid itemId, Storage item)
    {
        _logger.Information("Updating storage (Id: {StorageId})", itemId);
        var start = Environment.TickCount;

        item = await _repository.Update(itemId, item).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Information("Updated storage (Id: {StorageId} Duration: {Duration}ms)", itemId, duration);

        return item;
    }
}
