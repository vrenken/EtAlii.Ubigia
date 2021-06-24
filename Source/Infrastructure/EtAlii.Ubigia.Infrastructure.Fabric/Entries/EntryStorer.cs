// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Persistence;

    internal class EntryStorer : IEntryStorer
    {
        private readonly IStorage _storage;

        public EntryStorer(IStorage storage)
        {
            _storage = storage;
        }

        public Entry Store(IEditableEntry entry)
        {
            return Store((Entry)entry, out _);
        }

        public Entry Store(Entry entry)
        {
            return Store(entry, out _);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            return Store((Entry)entry, out storedComponents);
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(entry.Id);

            var components = EntryHelper.Decompose(entry);
            storedComponents = components.Where(component => !component.Stored)
                                         .ToArray();

            _storage.Components.StoreAll(containerId, storedComponents);

            return entry;
        }
    }
}