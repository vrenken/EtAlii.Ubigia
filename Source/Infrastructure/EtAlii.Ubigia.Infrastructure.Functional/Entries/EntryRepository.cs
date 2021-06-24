// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class EntryRepository : IEntryRepository
    {
        private readonly ILogicalContext _logicalContext;

        public EntryRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public Task<Entry> Get(Identifier identifier, EntryRelation entryRelations = EntryRelation.None)
        {
            return _logicalContext.Entries.Get(identifier, entryRelations);
        }

        public async IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            foreach (var identifier in identifiers)
            {
                yield return await _logicalContext.Entries.Get(identifier, entryRelations).ConfigureAwait(false);
            }
        }

        public IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            return _logicalContext.Entries.GetRelated(identifier, entriesWithRelation, entryRelations);
        }

        public Task<Entry> Prepare(Guid spaceId)
        {
            return _logicalContext.Entries.Prepare(spaceId);
        }

        public Task<Entry> Prepare(Guid spaceId, Identifier identifier)
        {
            return _logicalContext.Entries.Prepare(spaceId, identifier);
        }

        public Entry Store(IEditableEntry entry)
        {
            var storedEntry = _logicalContext.Entries.Store(entry, out var storedComponents);
            _logicalContext.Entries.Update(storedEntry, storedComponents);
            return storedEntry;
        }

        public Entry Store(Entry entry)
        {
            var storedEntry = _logicalContext.Entries.Store(entry, out var storedComponents);
            _logicalContext.Entries.Update(storedEntry, storedComponents);
            return storedEntry;
        }
    }
}