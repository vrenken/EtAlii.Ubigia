namespace EtAlii.Ubigia.Api.Transport
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EntryDataClientStub : IEntryDataClient 
    {
        public Task<IEditableEntry> Prepare()
        {
            return Task.FromResult<IEditableEntry>(null);
        }

        public Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return Task.FromResult<IReadOnlyEntry>(null);
        }

        public Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return Task.FromResult<IReadOnlyEntry>(null);
        }

        public Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return Task.FromResult<IReadOnlyEntry>(null);
        }

        public Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return Task.FromResult<IEnumerable<IReadOnlyEntry>>(new IReadOnlyEntry[] { });
        }

        public Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return Task.FromResult<IEnumerable<IReadOnlyEntry>>(null);
        }

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
