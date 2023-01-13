// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

public static class TypeComponentExtensions
{
    public static TypeComponent ToLocal(this WireProtocol.TypeComponent typeComponent)
    {
        var result = new TypeComponent
        {
            Stored = typeComponent.Stored,
            Type = typeComponent.Type
        };

        return result;
    }

    public static WireProtocol.TypeComponent ToWire(this TypeComponent typeComponent)
    {
        var result = new WireProtocol.TypeComponent
        {
            Stored = typeComponent.Stored,
        };

        if (typeComponent.Type != null)
        {
            result.Type = typeComponent.Type;
        }

        return result;
    }
}
