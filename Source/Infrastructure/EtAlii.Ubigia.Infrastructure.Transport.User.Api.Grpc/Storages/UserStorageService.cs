// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using EtAlii.Ubigia.Infrastructure.Functional;
using global::Grpc.Core;

public class UserStorageService : StorageGrpcService.StorageGrpcServiceBase, IUserStorageService
{
    private readonly IStorageRepository _items;

    public UserStorageService(IStorageRepository items)
    {
        _items = items;
    }

    public override async Task<StorageSingleResponse> GetLocal(StorageSingleRequest request, ServerCallContext context)
    {
        var storage = await _items.GetLocal().ConfigureAwait(false);

        var response = new StorageSingleResponse
        {
            Storage = storage.ToWire()
        };
        return response;
    }
}
