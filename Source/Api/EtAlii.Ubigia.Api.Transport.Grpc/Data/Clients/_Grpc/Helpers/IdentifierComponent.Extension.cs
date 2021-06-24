// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class IdentifierComponentExtension
    {
        public static IdentifierComponent ToLocal(this WireProtocol.IdentifierComponent identifierComponent)
        {
            return new()
            {
                Stored = identifierComponent.Stored,
                Id = identifierComponent.Identifier.ToLocal(),
            };
        }

        public static WireProtocol.IdentifierComponent ToWire(this IdentifierComponent identifierComponent)
        {
            return new()
            {
                Stored = identifierComponent.Stored,
                Identifier = identifierComponent.Id.ToWire(),
            };
        }

        public static IEnumerable<WireProtocol.IdentifierComponent> ToWire(this IEnumerable<IdentifierComponent> identifierComponents)
        {
            return identifierComponents.Select(s => s.ToWire());
        }
    }
}
