﻿namespace EtAlii.Servus.Api.Transport
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryDataClient : ISpaceTransportClient
    {
        Task<IEditableEntry> Prepare();
        Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None);
        Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None);
        Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None);

        Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None);
    }

    public interface IEntryDataClient<in TTransport> : IEntryDataClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
