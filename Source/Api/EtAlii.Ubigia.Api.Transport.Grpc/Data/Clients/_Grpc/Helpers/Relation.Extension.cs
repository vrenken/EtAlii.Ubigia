// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RelationExtension
    {
        public static Relation ToLocal(this WireProtocol.Relation relation)
        {
            var id = relation.Id.ToLocal();
            var moment = relation.Moment;

            return Relation.Create(id, moment);
        }

        public static IEnumerable<Relation> ToLocal(this IEnumerable<WireProtocol.Relation> relations)
        {
            return relations.Select(s => s.ToLocal());
        }

        public static WireProtocol.Relation ToWire(this Relation relation)
        {
            var id = relation.Id.ToWire();
            var moment = relation.Moment;

            return new WireProtocol.Relation
            {
                Id = id,
                Moment = moment,
            };
        }

        public static IEnumerable<WireProtocol.Relation> ToWire(this IEnumerable<Relation> relations)
        {
            return relations.Select(s => s.ToWire());
        }
    }
}
