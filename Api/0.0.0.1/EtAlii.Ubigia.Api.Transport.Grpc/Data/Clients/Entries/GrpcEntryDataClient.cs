namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GrpcEntryDataClient : GrpcClientBase, IEntryDataClient<IGrpcSpaceTransport> 
    {
        //private HubConnection _connection;
        //private readonly IHubProxyMethodInvoker _invoker;
        
        //public GrpcEntryDataClient(
        //    IHubProxyMethodInvoker invoker)
        //{
        //    _invoker = invoker;
        //}

        public async Task<IEditableEntry> Prepare()
        {
            // TODO: GRPC
            //return await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "Post", Connection.Space.Id);
            return await Task.FromResult<IEditableEntry>(null);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            // TODO: GRPC
            //var result = await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "Put", entry);
            //scope.Cache.InvalidateEntry(entry.Id);
            // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
            ////scope.Cache.InvalidateEntry(result.Id);
            //return result;
            return await Task.FromResult<IReadOnlyEntry>(null);
        }

        public async Task<IReadOnlyEntry> Get(Api.Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Get(root.Identifier, scope, entryRelations);
        }

        public async Task<IReadOnlyEntry> Get(Api.Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await scope.Cache.GetEntry(entryIdentifier, async () =>
            {
                // TODO: GRPC
                //return await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "GetSingle", entryIdentifier, entryRelations);
                return await Task.FromResult<IReadOnlyEntry>(null);
            });
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Api.Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            // TODO: this can be improved by using one single Web API call.
            var result = new List<IReadOnlyEntry>();
            foreach (var entryIdentifier in entryIdentifiers)
            {
                var entry = await scope.Cache.GetEntry(entryIdentifier, async () =>
                {
                    // TODO: GRPC
                    return await Task.FromResult<IReadOnlyEntry>(null);
                    //return await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "GetSingle", entryIdentifier, entryRelations);
                });
                result.Add(entry);
            }
            return result;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelated(Api.Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await scope.Cache.GetRelatedEntries(entryIdentifier, entriesWithRelation, async () =>
            {
                // TODO: GRPC
                return await Task.FromResult<IEnumerable<IReadOnlyEntry>>(null);
                //return await _invoker.Invoke<IEnumerable<Entry>>(_connection, GrpcHub.Entry, "GetRelated", entryIdentifier, entriesWithRelation, entryRelations);
            });
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
            //_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Entry, UriKind.Absolute));//spaceConnection.Transport.HttpClientHandler);
	        //await _connection.StartAsync();

        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            // TODO: GRPC
            //await _connection.DisposeAsync();
            //_connection = null;
        }
    }
}
