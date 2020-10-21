namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class SystemEntryDataClient : SystemSpaceClientBase, IEntryDataClient 
    {
        private readonly IInfrastructure _infrastructure;

        public SystemEntryDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public Task<IEditableEntry> Prepare()
        {
            var result = _infrastructure.Entries.Prepare(Connection.Space.Id);
            return Task.FromResult<IEditableEntry>(result);
        }

        public Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var result = _infrastructure.Entries.Store(entry);
            return Task.FromResult<IReadOnlyEntry>(result);
        }

        public Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.Get(root.Identifier);
            return Task.FromResult<IReadOnlyEntry>(result);
        }

        public Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.Get(entryIdentifier, entryRelations);
            return Task.FromResult<IReadOnlyEntry>(result);
        }

        public Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.Get(entryIdentifiers, entryRelations);
            return Task.FromResult<IEnumerable<IReadOnlyEntry>>(result);
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return _infrastructure.Entries.GetRelated(entryIdentifier, entriesWithRelation, entryRelations);
        }
    }
}
