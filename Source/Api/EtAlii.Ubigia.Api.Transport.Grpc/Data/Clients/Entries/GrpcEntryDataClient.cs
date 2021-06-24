// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using EntryRelation = EtAlii.Ubigia.EntryRelation;
    using Identifier = EtAlii.Ubigia.Identifier;
    using Root = EtAlii.Ubigia.Root;

    internal class GrpcEntryDataClient : GrpcClientBase, IEntryDataClient<IGrpcSpaceTransport>
    {
        private EntryGrpcService.EntryGrpcServiceClient _client;
        private IGrpcSpaceTransport _transport;

        public async Task<IEditableEntry> Prepare()
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new EntryPostRequest { SpaceId = Connection.Space.Id.ToWire() };
                var response = await _client.PostAsync(request, metadata);
                return response.Entry.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.Prepare()", e);
            }
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new EntryPutRequest { Entry = ((IComponentEditableEntry)entry).ToWire() };
                var response = await _client.PutAsync(request, metadata);

                scope.Cache.InvalidateEntry(entry.Id);
                // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
                //scope.Cache.InvalidateEntry(result.Id)
                return response.Entry.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.Change()", e);
            }
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Get(root.Identifier, scope, entryRelations).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            try
            {
                return await scope.Cache.GetEntry(entryIdentifier, async () =>
                {
                    var metadata = new Metadata { _transport.AuthenticationHeader };
                    var request = new EntrySingleRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire() };
                    var response = await _client.GetSingleAsync(request, metadata);
                    return response.Entry.ToLocal();
                }).ConfigureAwait(false);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            // TODO: this can be improved by using one single Web API call.

            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator.
            using var enumerator = entryIdentifiers.GetEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                IReadOnlyEntry item;
                try
                {
                    hasResult = enumerator.MoveNext();

                    if (hasResult)
                    {
                        var current = enumerator.Current;
                        item = await scope.Cache.GetEntry(current, async () =>
                        {
                            var metadata = new Metadata { _transport.AuthenticationHeader };
                            var request = new EntrySingleRequest {EntryId = current.ToWire(), EntryRelations = entryRelations.ToWire()};
                            var response = await _client.GetSingleAsync(request, metadata);
                            return response.Entry.ToLocal();
                        }).ConfigureAwait(false);
                    }
                    else
                    {
                        item = null;
                    }

                }
                catch (RpcException e)
                {
                    throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.Get()", e);
                }

                if (item != null)
                {
                    yield return item;
                }
            }
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            try
            {
                return scope.Cache.GetRelatedEntries(entryIdentifier, entriesWithRelation, () => GetRelated(entryIdentifier, entriesWithRelation, entryRelations));
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.GetRelated()", e);
            }
        }

        private async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            var metadata = new Metadata { _transport.AuthenticationHeader };
            var request = new EntryRelatedRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire(), EntriesWithRelation = entriesWithRelation.ToWire() };
            var call = _client.GetRelated(request, metadata);
            await foreach (var response in call.ResponseStream.ReadAllAsync().ConfigureAwait(false))
            {
                yield return response.Entry.ToLocal();
            }
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection).ConfigureAwait(false);

            _transport = ((IGrpcSpaceConnection)spaceConnection).Transport;
            _client = new EntryGrpcService.EntryGrpcServiceClient(_transport.CallInvoker);
        }

        public override async Task Disconnect()
        {
            await base.Disconnect().ConfigureAwait(false);
            _transport = null;
            _client = null;
        }
    }
}
