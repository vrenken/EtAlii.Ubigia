namespace EtAlii.Servus.Api
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class UpdatesComponentCollection : Collection<UpdatesComponent>
    {
        public UpdatesComponentCollection()
            : base()
        {
        }

        public void Add(Identifier id)
        {
            base.Add(new UpdatesComponent(new Relation[] { Relation.NewRelation(id) }));
        }

        public void Add(IEnumerable<Relation> relations, bool markAsStored)
        {
            base.Add(new UpdatesComponent(relations) {Stored = markAsStored});
        }

        public bool Contains(Identifier id)
        {
            return base.Items.Any(component => component.Updates.Any(u => u.Id == id));
        }
    }
}
