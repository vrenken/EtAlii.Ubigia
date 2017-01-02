namespace EtAlii.Servus.Api
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Children2ComponentCollection : Collection<Children2Component>
    {
        public Children2ComponentCollection()
            : base()
        {
        }

        public void Add(Identifier id)
        {
            base.Add(new Children2Component(new Relation[] { Relation.NewRelation(id) }));
        }

        internal void Add(IEnumerable<Relation> children2, bool markAsStored)
        {
            base.Add(new Children2Component(children2) { Stored = markAsStored });
        }

        public bool Contains(Identifier id)
        {
            return base.Items.Any(component => component.Children2.Any(c => c.Id == id));
        }
    }
}
