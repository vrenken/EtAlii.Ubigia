// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;
using System.Threading.Tasks;
using EtAlii.xTechnology.Diagnostics;

public class ProfilingItemStorage : IItemStorage
{
    private readonly IItemStorage _storage;
    private readonly IProfiler _profiler;

    private const string StoreCounter = "ItemStorage.Store";
    private const string RetrieveCounter = "ItemStorage.Retrieve";
    private const string RemoveCounter = "ItemStorage.Remove";
    private const string GetCounter = "ItemStorage.Get";
    private const string HasCounter = "ItemStorage.Has";

    public ProfilingItemStorage(
        IItemStorage storage,
        IProfiler profiler)
    {
        _storage = storage;
        _profiler = profiler;

        profiler.Register(StoreCounter, SamplingType.RawCount, "Milliseconds", "Store item", "The time it takes for the Store method to execute");
        profiler.Register(RetrieveCounter, SamplingType.RawCount, "Milliseconds", "Retrieve item", "The time it takes for the Retrieve method to execute");
        profiler.Register(RemoveCounter, SamplingType.RawCount, "Milliseconds", "Remove item", "The time it takes for the Remove method to execute");
        profiler.Register(GetCounter, SamplingType.RawCount, "Milliseconds", "Get item", "The time it takes for the Get method to execute");
        profiler.Register(HasCounter, SamplingType.RawCount, "Milliseconds", "Has item", "The time it takes for the Has method to execute");

    }

    public void Store<T>(T item, Guid id, ContainerIdentifier container) where T : class
    {
        var startTicks = Environment.TickCount;
        _storage.Store(item, id, container);
        var endTicks = Environment.TickCount;
        _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
    }

    public async Task<T> Retrieve<T>(Guid id, ContainerIdentifier container)
        where T : class
    {
        var startTicks = Environment.TickCount;
        var result = await _storage.Retrieve<T>(id, container).ConfigureAwait(false);
        var endTicks = Environment.TickCount;
        _profiler.WriteSample(RetrieveCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        return result;
    }

    public void Remove(Guid id, ContainerIdentifier container)
    {
        var startTicks = Environment.TickCount;
        _storage.Remove(id, container);
        var endTicks = Environment.TickCount;
        _profiler.WriteSample(RemoveCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
    }

    public Guid[] Get(ContainerIdentifier container)
    {
        var startTicks = Environment.TickCount;
        var result = _storage.Get(container);
        var endTicks = Environment.TickCount;
        _profiler.WriteSample(GetCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        return result;
    }

    public bool Has(Guid id, ContainerIdentifier container)
    {
        var startTicks = Environment.TickCount;
        var result = _storage.Has(id, container);
        var endTicks = Environment.TickCount;
        _profiler.WriteSample(HasCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        return result;
    }
}
