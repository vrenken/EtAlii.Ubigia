// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal sealed class SignalREntryDataClient : SignalRClientBase, IEntryDataClient<ISignalRSpaceTransport>
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
            return await _invoker.Invoke<Entry>(_connection, SignalRHub.Entry, "Post", Connection.Space.Id).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var result = await _invoker.Invoke<Entry>(_connection, SignalRHub.Entry, "Put", entry).ConfigureAwait(false);
            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            return await Get(root.Identifier, scope, entryRelations).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            return await _invoker
                .Invoke<Entry>(_connection, SignalRHub.Entry, "GetSingle", entryIdentifier, entryRelations)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            // Is it possible to improved this by using one single Web API call?
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/85

            foreach (var entryIdentifier in entryIdentifiers)
            {
                yield return await _invoker
                    .Invoke<Entry>(_connection, SignalRHub.Entry, "GetSingle", entryIdentifier, entryRelations)
                    .ConfigureAwait(false);
            }
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            return GetRelated(entryIdentifier, entriesWithRelation, entryRelations);
        }

        private async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
        {
            var items = _invoker
                .Stream<Entry>(_connection, SignalRHub.Entry, "GetRelated", entryIdentifier, entriesWithRelation, entryRelations)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection).ConfigureAwait(false);

			_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Entry, UriKind.Absolute));
	        await _connection.StartAsync().ConfigureAwait(false);

        }

        public override async Task Disconnect()
        {
            await base.Disconnect().ConfigureAwait(false);

            await _connection.DisposeAsync().ConfigureAwait(false);
            _connection = null;
        }
    }
}
