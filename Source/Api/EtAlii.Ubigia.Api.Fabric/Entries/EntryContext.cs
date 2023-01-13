// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;

internal class EntryContext : IEntryContext
{
    private readonly IDataConnection _connection;

    public EntryContext(IDataConnection connection)
    {
        if (connection == null) return; // In the new setup the LogicalContext and IDataConnection are instantiated at the same time.
        _connection = connection;
    }

    public async Task<IEditableEntry> Prepare()
    {
        return await _connection.Entries.Data.Prepare().ConfigureAwait(false);
    }

    public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
    {
        return await _connection.Entries.Data.Change(entry, scope).ConfigureAwait(false);
    }

    public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
    {
        return await _connection.Entries.Data.Get(root, scope, EntryRelations.All).ConfigureAwait(false);
    }

    public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
    {
        return await _connection.Entries.Data.Get(identifier, scope, EntryRelations.All).ConfigureAwait(false);
    }

    public IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
    {
        return _connection.Entries.Data.Get(identifiers, scope, EntryRelations.All);
    }

    public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relations, ExecutionScope scope)
    {
        return _connection.Entries.Data.GetRelated(identifier, relations, scope, EntryRelations.All);
    }
}
