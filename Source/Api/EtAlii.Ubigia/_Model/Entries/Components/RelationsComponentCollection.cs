// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public abstract class RelationsComponentCollection<TRelationsComponent> : Collection<TRelationsComponent>
        where TRelationsComponent : RelationsComponent, new()
    {
        private protected RelationsComponentCollection()
        {
        }

        public void Add(in Identifier id)
        {
            base.Add(new TRelationsComponent { Relations = new[] { Relation.NewRelation(id) } });
        }

        internal void Add(Relation[] relations, bool markAsStored)
        {
            base.Add(new TRelationsComponent { Relations = relations, Stored = markAsStored });
        }

        public bool Contains(Identifier id)
        {
            return Items.Any(component => component.Relations.Any(c => c.Id == id));
        }
    }
}
