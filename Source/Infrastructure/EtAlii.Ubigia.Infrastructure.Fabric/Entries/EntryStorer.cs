// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence;

internal class EntryStorer : IEntryStorer
{
    private readonly IStorage _storage;

    public EntryStorer(IStorage storage)
    {
        _storage = storage;
    }

    /// <inheritdoc />
    public async Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(IEditableEntry entry)
    {
        return await Store((Entry)entry).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(Entry entry)
    {
        var containerId = _storage.ContainerProvider.FromIdentifier(entry.Id);

        var components = EntryHelper.Decompose(entry);
        var storedComponents = components
            .Where(component => !component.Stored)
            .ToArray();

        await _storage.Components
            .StoreAll(containerId, storedComponents)
            .ConfigureAwait(false);

        return (entry, storedComponents);
    }
}
