// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System;

public static class GuidExtension
{
    public static Guid ToLocal(this WireProtocol.Guid id)
    {
        var bytes = new byte[16];

        BitConverter.GetBytes(id.Data1).CopyTo(bytes, 0);
        BitConverter.GetBytes(id.Data2).CopyTo(bytes, 8);

        return new Guid(bytes);
    }

    public static WireProtocol.Guid ToWire(this Guid id)
    {
        var bytes = id.ToByteArray();
        return new WireProtocol.Guid
        {
            Data1 = BitConverter.ToUInt64(bytes, 0),
            Data2 = BitConverter.ToUInt64(bytes, 8),
        };
    }
}
