﻿namespace EtAlii.Ubigia
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public class RelationsComponentCollection<TRelationsComponent> : Collection<TRelationsComponent>
        where TRelationsComponent : RelationsComponent, new()
    {
        internal RelationsComponentCollection()
        {
        }

        public void Add(in Identifier id)
        {
            base.Add(new TRelationsComponent { Relations = new[] { Relation.NewRelation(id) } });
        }

        internal void Add(IEnumerable<Relation> relations, bool markAsStored)
        {
            base.Add(new TRelationsComponent { Relations = relations, Stored = markAsStored });
        }

        public bool Contains(Identifier id)
        {
            return Items.Any(component => component.Relations.Any(c => c.Id == id));
        }
    }
}
