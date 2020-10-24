﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class UserEntryService
    {
        // Update Item by id
        public override Task<EntrySingleResponse> Put(EntryPutRequest request, ServerCallContext context)
        {

            var entry = request.Entry.ToLocal();
            entry = _items.Store(entry);
            var response = new EntrySingleResponse
            {
                Entry = entry.ToWire()
            };
            return Task.FromResult(response);
        }
    }
}