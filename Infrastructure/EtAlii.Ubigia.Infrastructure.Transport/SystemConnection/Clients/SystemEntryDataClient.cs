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

        public async Task<IEditableEntry> Prepare()
        {
            var result = _infrastructure.Entries.Prepare(Connection.Space.Id);
            return await Task.FromResult(result);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var result = _infrastructure.Entries.Store(entry);
            return await Task.FromResult(result);
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.Get(root.Identifier);
            return await Task.FromResult(result);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.Get(entryIdentifier, entryRelations);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.Get(entryIdentifiers, entryRelations);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = _infrastructure.Entries.GetRelated(entryIdentifier, entriesWithRelation, entryRelations);
            return await Task.FromResult(result);
        }
    }
}
