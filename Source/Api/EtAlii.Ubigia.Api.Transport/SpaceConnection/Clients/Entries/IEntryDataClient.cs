// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEntryDataClient : ISpaceTransportClient
{
    Task<IEditableEntry> Prepare();
    Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope);
    Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None);
    Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None);
    IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None);

    IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None);
}

public interface IEntryDataClient<in TTransport> : IEntryDataClient, ISpaceTransportClient<TTransport>
    where TTransport: ISpaceTransport
{
}
