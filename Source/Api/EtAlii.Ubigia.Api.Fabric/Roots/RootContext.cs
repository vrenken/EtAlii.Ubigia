// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;

internal class RootContext : IRootContext
{
    private readonly IDataConnection _connection;

    public RootContext(IDataConnection connection)
    {
        if (connection == null) return; // In the new setup the LogicalContext and IDataConnection are instantiated at the same time.
        _connection = connection;
    }

    public async Task<Root> Add(string rootName, RootType rootType)
    {
        // RCI2022: We want to make roots case insensitive.
        rootName = rootName.ToUpper();
        return await _connection.Roots.Data.Add(rootName, rootType).ConfigureAwait(false);
    }

    public async Task Remove(Guid id)
    {
        await _connection.Roots.Data.Remove(id).ConfigureAwait(false);
    }

    public async Task<Root> Change(Guid rootId, string rootName)
    {
        // RCI2022: We want to make roots case insensitive.
        rootName = rootName.ToUpper();
        return await _connection.Roots.Data.Change(rootId, rootName).ConfigureAwait(false);
    }

    public async Task<Root> Get(string rootName)
    {
        // RCI2022: We want to make roots case insensitive.
        rootName = rootName.ToUpper();
        return await _connection.Roots.Data.Get(rootName).ConfigureAwait(false);
    }

    public async Task<Root> Get(Guid rootId)
    {
        return await _connection.Roots.Data.Get(rootId).ConfigureAwait(false);
    }

    public IAsyncEnumerable<Root> GetAll()
    {
        return _connection.Roots.Data.GetAll();
    }
}
