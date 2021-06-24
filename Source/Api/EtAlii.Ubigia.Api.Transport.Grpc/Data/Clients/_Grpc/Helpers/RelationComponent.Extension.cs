// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RelationComponentExtension
    {
        public static TRelationComponent ToLocal<TRelationComponent>(this WireProtocol.RelationComponent relationComponent)
        where TRelationComponent: RelationComponent, new()
        {
            return new()
            {
                Stored = relationComponent.Stored,
                Relation = relationComponent.Relation.ToLocal(),
            };
        }

        public static WireProtocol.RelationComponent ToWire(this RelationComponent relationComponent)
        {
            return new()
            {
                Stored = relationComponent.Stored,
                Relation = relationComponent.Relation.ToWire(),
            };
        }

        public static IEnumerable<WireProtocol.RelationComponent> ToWire(this IEnumerable<RelationComponent> relationComponents)
        {
            return relationComponents.Select(s => s.ToWire());
        }
    }
}
