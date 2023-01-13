// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RootSet : IRootSet
{
    private readonly IRootAdder _rootAdder;
    private readonly IRootGetter _rootGetter;
    private readonly IRootRemover _rootRemover;
    private readonly IRootUpdater _rootUpdater;

    public RootSet(
        IRootAdder rootAdder,
        IRootGetter rootGetter,
        IRootRemover rootRemover,
        IRootUpdater rootUpdater)
    {
        _rootAdder = rootAdder;
        _rootGetter = rootGetter;
        _rootRemover = rootRemover;
        _rootUpdater = rootUpdater;
    }

    /// <inheritdoc />
    public Task<Root> Add(Guid spaceId, Root root)
    {
        return _rootAdder.Add(spaceId, root);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Root> GetAll(Guid spaceId)
    {
        return _rootGetter.GetAll(spaceId);
    }

    /// <inheritdoc />
    public Task<Root> Get(Guid spaceId, Guid rootId)
    {
        return _rootGetter.Get(spaceId, rootId);
    }

    /// <inheritdoc />
    public Task<Root> Get(Guid spaceId, string name)
    {
        return _rootGetter.Get(spaceId, name);
    }

    /// <inheritdoc />
    public Task Remove(Guid spaceId, Guid rootId)
    {
        return _rootRemover.Remove(spaceId, rootId);
    }

    /// <inheritdoc />
    public Task Remove(Guid spaceId, Root root)
    {
        return _rootRemover.Remove(spaceId, root);
    }

    /// <inheritdoc />
    public Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot)
    {
        return _rootUpdater.Update(spaceId, rootId, updatedRoot);
    }
}
