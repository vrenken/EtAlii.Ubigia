﻿namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalREntryDataClient : SignalRClientBase, IEntryDataClient<ISignalRSpaceTransport> 
    {
        private HubConnection _connection;
        private readonly IHubProxyMethodInvoker _invoker;
        
        public SignalREntryDataClient(
            IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await _invoker.Invoke<Entry>(_connection, SignalRHub.Entry, "Post", Connection.Space.Id);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var result = await _invoker.Invoke<Entry>(_connection, SignalRHub.Entry, "Put", entry);
            scope.Cache.InvalidateEntry(entry.Id);
            // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
            //scope.Cache.InvalidateEntry(result.Id)
            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Get(root.Identifier, scope, entryRelations);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await scope.Cache.GetEntry(entryIdentifier, async () =>
            {
                return await _invoker.Invoke<Entry>(_connection, SignalRHub.Entry, "GetSingle", entryIdentifier, entryRelations);
            });
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            // TODO: this can be improved by using one single Web API call.
            foreach (var entryIdentifier in entryIdentifiers)
            {
                var entry = await scope.Cache.GetEntry(entryIdentifier, async () =>
                {
                    return await _invoker.Invoke<Entry>(_connection, SignalRHub.Entry, "GetSingle", entryIdentifier, entryRelations);
                });
                yield return entry;
            }
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return scope.Cache.GetRelatedEntries(entryIdentifier, entriesWithRelation, () => GetRelated(entryIdentifier, entriesWithRelation, entryRelations));
        }

        private async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            var result = await _invoker.Invoke<IEnumerable<Entry>>(_connection, SignalRHub.Entry, "GetRelated", entryIdentifier, entriesWithRelation, entryRelations);
            foreach (var item in result)
            {
                yield return item;
            }
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

			_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + "/" + SignalRHub.Entry, UriKind.Absolute));
	        await _connection.StartAsync();

        }

        public override async Task Disconnect()
        {
            await base.Disconnect();

            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}