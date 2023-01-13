// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public sealed class ProfilingLogicalRootSet : ILogicalRootSet
{
    private readonly ILogicalRootSet _decoree;

    public ProfilingLogicalRootSet(ILogicalRootSet decoree)
    {
        _decoree = decoree;
    }

    public async Task<Root> Add(string name, RootType rootType)
    {
        return await _decoree.Add(name, rootType).ConfigureAwait(false);
    }

    public async Task Remove(Guid id)
    {
        await _decoree.Remove(id).ConfigureAwait(false);
    }

    public async Task<Root> Change(Guid rootId, string rootName, RootType rootType)
    {
        return await _decoree.Change(rootId, rootName, rootType).ConfigureAwait(false);
    }

    public async Task<Root> Get(string rootName)
    {
        return await _decoree.Get(rootName).ConfigureAwait(false);
    }

    public async Task<Root> Get(Guid rootId)
    {
        return await _decoree.Get(rootId).ConfigureAwait(false);
    }

    public IAsyncEnumerable<Root> GetAll()
    {
        return _decoree.GetAll();
    }
}
