namespace EtAlii.Servus.Api
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ChildrenComponentCollection : Collection<ChildrenComponent>
    {
        public ChildrenComponentCollection()
            : base()
        {
        }

        public void Add(Identifier id)
        {
            base.Add(new ChildrenComponent(new Relation[] { Relation.NewRelation(id) }));
        }

        internal void Add(IEnumerable<Relation> relations, bool markAsStored)
        {
            base.Add(new ChildrenComponent(relations) { Stored = markAsStored });
        }

        public bool Contains(Identifier id)
        {
            return base.Items.Any(component => component.Children.Any(c => c.Id == id));
        }
    }
}
