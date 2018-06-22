namespace EtAlii.Ubigia.Api.Transport
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EntryDataClientStub : IEntryDataClient 
    {
        public async Task<IEditableEntry> Prepare()
        {
            return await Task.FromResult<IEditableEntry>(null);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return await Task.FromResult<IReadOnlyEntry>(null);
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Task.FromResult<IReadOnlyEntry>(null);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Task.FromResult<IReadOnlyEntry>(null);
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Task.FromResult<IEnumerable<IReadOnlyEntry>>(new IReadOnlyEntry[] { });
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Task.FromResult<IEnumerable<IReadOnlyEntry>>(null);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }
    }
}
