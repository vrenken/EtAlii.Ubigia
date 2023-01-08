// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalEntrySet : ILogicalEntrySet
    {
        private readonly IFabricContext _fabricContext;
        private readonly IEntryPreparer _entryPreparer;

        public LogicalEntrySet(
            IFabricContext fabricContext,
            IEntryPreparer entryPreparer)
        {
            _fabricContext = fabricContext;
            _entryPreparer = entryPreparer;
        }

        /// <inheritdoc />
        public Task<Entry> Prepare(Guid storageId, Guid spaceId)
        {
            return _entryPreparer.Prepare(storageId, spaceId);
        }

        /// <inheritdoc />
        public Task<Entry> Prepare(Identifier id)
        {
            return _entryPreparer.Prepare(id);
        }

        /// <inheritdoc />
        public Task<Entry> Get(Identifier identifier, EntryRelations entryRelations)
        {
            return _fabricContext.Entries.Get(identifier, entryRelations);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations)
        {
            return _fabricContext.Entries.Get(identifiers, entryRelations);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
        {
            return _fabricContext.Entries.GetRelated(identifier, entriesWithRelation, entryRelations);
        }

        /// <inheritdoc />
        public Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(IEditableEntry entry)
        {
            return _fabricContext.Entries.Store(entry);
        }

        /// <inheritdoc />
        public Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(Entry entry)
        {
            return _fabricContext.Entries.Store(entry);
        }

        /// <inheritdoc />
        public Task Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            return _fabricContext.Entries.Update(entry, changedComponents);
        }

        /// <inheritdoc />
        public Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            return _fabricContext.Entries.Update(entry, changedComponents);
        }
    }
}
