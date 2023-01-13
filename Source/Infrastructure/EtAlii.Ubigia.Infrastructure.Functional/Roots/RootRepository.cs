// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class RootRepository : IRootRepository
{
    private readonly ILogicalContext _logicalContext;
    private readonly ILocalStorageGetter _localStorageGetter;

    public RootRepository(
        ILogicalContext logicalContext,
        ILocalStorageGetter localStorageGetter)
    {
        _logicalContext = logicalContext;
        _localStorageGetter = localStorageGetter;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Root> GetAll(Guid spaceId)
    {
        return _logicalContext.Roots.GetAll(spaceId);
    }

    /// <inheritdoc />
    public Task<Root> Get(Guid spaceId, Guid rootId)
    {
        return _logicalContext.Roots.Get(spaceId, rootId);
    }

    /// <inheritdoc />
    public Task<Root> Get(Guid spaceId, string name)
    {
        return _logicalContext.Roots.Get(spaceId, name);
    }

    /// <inheritdoc />
    public Task<Root> Add(Guid spaceId, Root root)
    {
        var storage = _localStorageGetter.GetLocal();
        return _logicalContext.Roots.Add(storage.Id, spaceId, root);
    }

    /// <inheritdoc />
    public Task Remove(Guid spaceId, Guid rootId)
    {
        return _logicalContext.Roots.Remove(spaceId, rootId);
    }

    /// <inheritdoc />
    public Task Remove(Guid spaceId, Root root)
    {
        return _logicalContext.Roots.Remove(spaceId, root);
    }

    /// <inheritdoc />
    public Task<Root> Update(Guid spaceId, Guid rootId, Root root)
    {
        return _logicalContext.Roots.Update(spaceId, rootId, root);
    }
}
