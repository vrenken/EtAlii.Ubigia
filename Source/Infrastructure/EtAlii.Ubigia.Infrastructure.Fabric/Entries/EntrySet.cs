// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System.Collections.Generic;
using System.Threading.Tasks;

public class EntrySet : IEntrySet
{
    private readonly IEntryGetter _entryGetter;
    private readonly IEntryUpdater _entryUpdater;
    private readonly IEntryStorer _entryStorer;

    public EntrySet(
        IEntryGetter entryGetter,
        IEntryUpdater entryUpdater,
        IEntryStorer entryStorer)
    {
        _entryGetter = entryGetter;
        _entryUpdater = entryUpdater;
        _entryStorer = entryStorer;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
    {
        return _entryGetter.GetRelated(identifier, entriesWithRelation, entryRelations);
    }

    /// <inheritdoc />
    public Task<Entry> Get(Identifier identifier, EntryRelations entryRelations)
    {
        return _entryGetter.Get(identifier, entryRelations);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations)
    {
        return _entryGetter.Get(identifiers, entryRelations);
    }

    /// <inheritdoc />
    public Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(IEditableEntry entry)
    {
        return _entryStorer.Store(entry);
    }

    /// <inheritdoc />
    public Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(Entry entry)
    {
        return _entryStorer.Store(entry);
    }

    /// <inheritdoc />
    public Task Update(Entry entry, IEnumerable<IComponent> changedComponents)
    {
        return _entryUpdater.Update(entry, changedComponents);
    }

    /// <inheritdoc />
    public Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
    {
        return _entryUpdater.Update(entry, changedComponents);
    }
}
