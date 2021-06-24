// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RelationsComponentExtension
    {
        public static TRelationsComponent ToLocal<TRelationsComponent>(this WireProtocol.RelationsComponent relationsComponent)
        where TRelationsComponent: RelationsComponent, new()
        {
            return new()
            {
                Stored = relationsComponent.Stored,
                Relations = relationsComponent.Relations.ToLocal(),
            };
        }

        public static WireProtocol.RelationsComponent ToWire(this RelationsComponent relationsComponent)
        {
            var result = new WireProtocol.RelationsComponent();
            result.Stored = relationsComponent.Stored;
            result.Relations.AddRange(relationsComponent.Relations.ToWire());
            return result;
        }

        public static IEnumerable<WireProtocol.RelationsComponent> ToWire(this IEnumerable<RelationsComponent> relationsComponents)
        {
            return relationsComponents.Select(s => s.ToWire());
        }
    }
}
