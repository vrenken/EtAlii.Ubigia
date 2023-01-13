// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using global::Grpc.Core;

public partial class UserEntryService
{
    // Update Item by id
    public override async Task<EntrySingleResponse> Put(EntryPutRequest request, ServerCallContext context)
    {

        var entry = request.Entry.ToLocal();
        entry = await _items.Store(entry).ConfigureAwait(false);
        var response = new EntrySingleResponse
        {
            Entry = entry.ToWire()
        };
        return response;
    }
}
