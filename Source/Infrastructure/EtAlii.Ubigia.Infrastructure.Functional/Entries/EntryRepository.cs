// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class EntryRepository : IEntryRepository
{
    private readonly ILogicalContext _logicalContext;
    private readonly ILocalStorageGetter _localStorageGetter;

    public EntryRepository(
        ILogicalContext logicalContext,
        ILocalStorageGetter localStorageGetter)
    {
        _logicalContext = logicalContext;
        _localStorageGetter = localStorageGetter;
    }

    /// <inheritdoc />
    public Task<Entry> Get(Identifier identifier, EntryRelations entryRelations = EntryRelations.None)
    {
        return _logicalContext.Entries.Get(identifier, entryRelations);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations = EntryRelations.None)
    {
        foreach (var identifier in identifiers)
        {
            yield return await _logicalContext.Entries.Get(identifier, entryRelations).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations = EntryRelations.None)
    {
        return _logicalContext.Entries.GetRelated(identifier, entriesWithRelation, entryRelations);
    }

    /// <inheritdoc />
    public Task<Entry> Prepare(Guid spaceId)
    {
        var localStorage = _localStorageGetter.GetLocal();
        return _logicalContext.Entries.Prepare(localStorage.Id, spaceId);
    }

    /// <inheritdoc />
    public Task<Entry> Prepare(Guid spaceId, Identifier identifier)
    {
        return _logicalContext.Entries.Prepare(identifier);
    }

    /// <inheritdoc />
    public async Task<Entry> Store(IEditableEntry entry)
    {
        var (storedEntry, storedComponents) = await _logicalContext.Entries.Store(entry).ConfigureAwait(false);
        await _logicalContext.Entries.Update(storedEntry, storedComponents).ConfigureAwait(false);
        return storedEntry;
    }

    /// <inheritdoc />
    public async Task<Entry> Store(Entry entry)
    {
        var (storedEntry, storedComponents) = await _logicalContext.Entries.Store(entry).ConfigureAwait(false);
        await _logicalContext.Entries.Update(storedEntry, storedComponents).ConfigureAwait(false);
        return storedEntry;
    }
}
