namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class SignalREntryDataClient : SignalRClientBase, IEntryDataClient<ISignalRSpaceTransport> 
    {
        private IHubProxy _proxy;
        private readonly IHubProxyMethodInvoker _invoker;
        
        public SignalREntryDataClient(
            IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await _invoker.Invoke<Entry>(_proxy, SignalRHub.Entry, "Post", Connection.Space.Id);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var result = await _invoker.Invoke<Entry>(_proxy, SignalRHub.Entry, "Put", entry);
            scope.Cache.InvalidateEntry(entry.Id);
            // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
            //scope.Cache.InvalidateEntry(result.Id);
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
                return await _invoker.Invoke<Entry>(_proxy, SignalRHub.Entry, "GetSingle", entryIdentifier, entryRelations);
            });
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            // TODO: this can be improved by using one single Web API call.
            var result = new List<IReadOnlyEntry>();
            foreach (var entryIdentifier in entryIdentifiers)
            {
                var entry = await scope.Cache.GetEntry(entryIdentifier, async () =>
                {
                    return await _invoker.Invoke<Entry>(_proxy, SignalRHub.Entry, "GetSingle", entryIdentifier, entryRelations);
                });
                result.Add(entry);
            }
            return result;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await scope.Cache.GetRelatedEntries(entryIdentifier, entriesWithRelation, async () =>
            {
                return await _invoker.Invoke<IEnumerable<Entry>>(_proxy, SignalRHub.Entry, "GetRelated", entryIdentifier, entriesWithRelation, entryRelations);
            });
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = Connection.Transport.HubConnection.CreateHubProxy(SignalRHub.Entry);
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = null;
            });
        }
    }
}
