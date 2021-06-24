// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EntryRelationExtension
    {
        public static EntryRelation ToLocal(this WireProtocol.EntryRelation entryRelation)
        {
            return (EntryRelation)entryRelation;
        }

        public static WireProtocol.EntryRelation ToWire(this EntryRelation entryRelation)
        {
            return (WireProtocol.EntryRelation) entryRelation;
        }

        public static IEnumerable<WireProtocol.EntryRelation> ToWire(this IEnumerable<EntryRelation> entryRelations)
        {
            return entryRelations.Select(s => s.ToWire());
        }
    }
}
