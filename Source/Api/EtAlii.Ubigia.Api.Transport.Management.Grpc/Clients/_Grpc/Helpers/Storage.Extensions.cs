// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc;

using System.Collections.Generic;
using System.Linq;

public static class StorageExtension
{
    public static Storage ToLocal(this WireProtocol.Storage storage)
    {
        return new()
        {
            Id = storage.Id.ToLocal(),
            Address = storage.Address,
            Name = storage.Name
        };
    }

    public static IEnumerable<Storage> ToLocal(this IEnumerable<WireProtocol.Storage> storages)
    {
        return storages.Select(s => s.ToLocal());
    }

    public static WireProtocol.Storage ToWire(this Storage storage)
    {
        return new()
        {
            Id = storage.Id.ToWire(),
            Address = storage.Address,
            Name = storage.Name
        };
    }

    public static IEnumerable<WireProtocol.Storage> ToWire(this IEnumerable<Storage> storages)
    {
        return storages.Select(s => s.ToWire());
    }
}
