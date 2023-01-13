// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using global::Grpc.Core;
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
            var response = await _client.PostAsync(request, metadata).ConfigureAwait(false);
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
            var response = await _client.PutAsync(request, metadata).ConfigureAwait(false);
            var result = response.Entry.ToLocal();
            return result;
        }
        catch (RpcException e)
        {
            throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.Change()", e);
        }
    }

    public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
    {
        return await Get(root.Identifier, scope, entryRelations).ConfigureAwait(false);
    }

    public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
    {
        try
        {
            var metadata = new Metadata { _transport.AuthenticationHeader };
            var request = new EntrySingleRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire() };
            var response = await _client.GetSingleAsync(request, metadata).ConfigureAwait(false);
            return response.Entry.ToLocal();
        }
        catch (RpcException e)
        {
            throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.Get()", e);
        }
    }

    public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
    {
        // Is it possible to improved this by using one single Web API call?
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/85

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
                    var metadata = new Metadata { _transport.AuthenticationHeader };
                    var request = new EntrySingleRequest {EntryId = current.ToWire(), EntryRelations = entryRelations.ToWire()};
                    var response = await _client.GetSingleAsync(request, metadata);
                    item = response.Entry.ToLocal();
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

    public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
    {
        try
        {
            return GetRelated(entryIdentifier, entriesWithRelation, entryRelations);
        }
        catch (RpcException e)
        {
            throw new InvalidInfrastructureOperationException($"{nameof(GrpcEntryDataClient)}.GetRelated()", e);
        }
    }

    private async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
    {
        var metadata = new Metadata { _transport.AuthenticationHeader };
        var request = new EntryRelatedRequest { EntryId = entryIdentifier.ToWire(), EntryRelations = entryRelations.ToWire(), EntriesWithRelation = entriesWithRelation.ToWire() };
        var call = _client.GetRelated(request, metadata);
        var responses = call.ResponseStream
            .ReadAllAsync()
            .ConfigureAwait(false);
        await foreach (var response in responses)
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
