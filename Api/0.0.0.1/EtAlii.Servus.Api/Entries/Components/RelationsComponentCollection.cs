namespace EtAlii.Servus.Api
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
            : base()
        {
        }

        public void Add(Identifier id)
        {
            base.Add(new TRelationsComponent { Relations = new Relation[] { Relation.NewRelation(id) } });
        }

        internal void Add(IEnumerable<Relation> relations, bool markAsStored)
        {
            base.Add(new TRelationsComponent { Relations = relations, Stored = markAsStored });
        }

        public bool Contains(Identifier id)
        {
            return base.Items.Any(component => component.Relations.Any(c => c.Id == id));
        }
    }
}
