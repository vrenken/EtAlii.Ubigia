namespace EtAlii.Servus.Api
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class IndexesComponentCollection : Collection<IndexesComponent>
    {
        public IndexesComponentCollection()
            : base()
        {
        }

        public void Add(Identifier id)
        {
            base.Add(new IndexesComponent(new Relation[] { Relation.NewRelation(id) }));
        }

        internal void Add(IEnumerable<Relation> relations, bool markAsStored)
        {
            base.Add(new IndexesComponent(relations) {Stored = markAsStored});
        }

        public bool Contains(Identifier id)
        {
            return base.Items.Any(component => component.Indexes.Any(i => i.Id == id));
        }

    }
}
