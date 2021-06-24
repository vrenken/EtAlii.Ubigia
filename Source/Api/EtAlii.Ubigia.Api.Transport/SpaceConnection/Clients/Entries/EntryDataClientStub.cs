// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return AsyncEnumerable.Empty<IReadOnlyEntry>();
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return AsyncEnumerable.Empty<IReadOnlyEntry>();
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
