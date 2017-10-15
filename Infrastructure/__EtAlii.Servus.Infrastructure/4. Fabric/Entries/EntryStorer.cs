namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class EntryStorer : IEntryStorer
    {
        private readonly IStorage _storage;

        public EntryStorer(IStorage storage)
        {
            _storage = storage;
        }

        public Entry Store(IEditableEntry entry)
        {
            var storedComponents = (IEnumerable<IComponent>)null;
            return Store((Entry)entry, out storedComponents);
        }

        public Entry Store(Entry entry)
        {
            var storedComponents = (IEnumerable<IComponent>)null;
            return Store(entry, out storedComponents);
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