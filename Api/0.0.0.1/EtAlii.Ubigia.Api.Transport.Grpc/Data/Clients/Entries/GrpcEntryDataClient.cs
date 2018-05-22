namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EntryRelation = EtAlii.Ubigia.Api.EntryRelation;

    internal class GrpcEntryDataClient : GrpcClientBase, IEntryDataClient<IGrpcSpaceTransport> 
    {
        private EntryGrpcService.EntryGrpcServiceClient _client;
        private IGrpcSpaceConnection _connection;

        public async Task<IEditableEntry> Prepare()
        {
            var request = new EntryPutRequest { SpaceId = Connection.Space.Id.ToWire() };
            var response = await _client.PutAsync(request, _connection.Transport.AuthenticationHeaders);
            //return await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "Post", Connection.Space.Id);
            return response.Entry.ToLocal();
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var request = new EntryPostRequest {Entry = ((IComponentEditableEntry)entry).ToWire()};
            var response = await _client.PostAsync(request, _connection.Transport.AuthenticationHeaders);
            //var result = await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "Put", entry);

            scope.Cache.InvalidateEntry(entry.Id);
            // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
            //scope.Cache.InvalidateEntry(result.Id);
            return response.Entry.ToLocal();
        }

        public async Task<IReadOnlyEntry> Get(Api.Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Get(root.Identifier, scope, entryRelations);
        }

        public async Task<IReadOnlyEntry> Get(Api.Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await scope.Cache.GetEntry(entryIdentifier, async () =>
            {
                var request = new EntrySingleRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire()};
                var response = await _client.GetSingleAsync(request, _connection.Transport.AuthenticationHeaders);
                //return await _invoker.Invoke<Entry>(_connection, GrpcHub.Entry, "GetSingle", entryIdentifier, entryRelations);
                return response.Entry.ToLocal();
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
                    var request = new EntrySingleRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire()};
                    var response = await _client.GetSingleAsync(request, _connection.Transport.AuthenticationHeaders);
                    return response.Entry.ToLocal();
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
                var request = new EntryRelatedRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire(), EntriesWithRelation = entriesWithRelation.ToWire()};
                var response = await _client.GetRelatedAsync(request, _connection.Transport.AuthenticationHeaders);
                return response.Entries.ToLocal();
                //return await _invoker.Invoke<IEnumerable<Entry>>(_connection, GrpcHub.Entry, "GetRelated", entryIdentifier, entriesWithRelation, entryRelations);
            });
        }

        public override Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            var channel = spaceConnection.Transport.Channel;
            _connection = (IGrpcSpaceConnection)spaceConnection;
            _client = new EntryGrpcService.EntryGrpcServiceClient(channel);
            return Task.CompletedTask;

        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _connection = null;
            _client = null;
            return Task.CompletedTask;
        }
    }
}
