// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
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

        public IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
        {
            return _entryGetter.GetRelated(identifier, entriesWithRelation, entryRelations);
        }

        public Task<Entry> Get(Identifier identifier, EntryRelations entryRelations)
        {
            return _entryGetter.Get(identifier, entryRelations);
        }

        public IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations)
        {
            return _entryGetter.Get(identifiers, entryRelations);
        }

        public Entry Store(IEditableEntry entry)
        {
            return _entryStorer.Store(entry);
        }

        public Entry Store(Entry entry)
        {
            return _entryStorer.Store(entry);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _entryStorer.Store(entry, out storedComponents);
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _entryStorer.Store(entry, out storedComponents);
        }

        public Task Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            return _entryUpdater.Update(entry, changedComponents);
        }

        public Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            return _entryUpdater.Update(entry, changedComponents);
        }
    }
}