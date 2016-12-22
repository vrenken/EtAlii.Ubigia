﻿namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Logical;

    internal class EntryRepository : IEntryRepository
    {
        private readonly ILogicalContext _logicalContext;

        public EntryRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations = EntryRelation.None)
        {
            return _logicalContext.Entries.Get(identifier, entryRelations);
        }

        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            return _logicalContext.Entries.GetRelated(identifier, entriesWithRelation, entryRelations);
        }


        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            return identifiers.Select(identifier => _logicalContext.Entries.Get(identifier, entryRelations));
        }

        public Entry Prepare(Guid spaceId)
        {
            return _logicalContext.Entries.Prepare(spaceId);
        }

        public Entry Prepare(Guid spaceId, Identifier id)
        {
            return _logicalContext.Entries.Prepare(spaceId, id);
        }

        public Entry Store(IEditableEntry entry)
        {
            var storedComponents = (IEnumerable<IComponent>)null;
            var storedEntry = _logicalContext.Entries.Store(entry, out storedComponents);
            _logicalContext.Entries.Update(storedEntry, storedComponents);
            return storedEntry;
        }

        public Entry Store(Entry entry)
        {
            var storedComponents = (IEnumerable<IComponent>)null;
            var storedEntry = _logicalContext.Entries.Store(entry, out storedComponents);
            _logicalContext.Entries.Update(storedEntry, storedComponents);
            return storedEntry;
        }
    }
}