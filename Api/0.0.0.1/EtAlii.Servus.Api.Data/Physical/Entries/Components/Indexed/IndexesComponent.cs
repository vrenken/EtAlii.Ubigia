namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class IndexesComponent : CompositeComponent 
    {
        [JsonConstructor]
        internal IndexesComponent()
        { 
        }

        public IndexesComponent(IEnumerable<Relation> indexes)
        {
            _indexes = indexes;
        }

        public IEnumerable<Relation> Indexes { get { return _indexes; } }
        private IEnumerable<Relation> _indexes = new Relation[] { };

        protected internal override string Name { get { return _name; } }
        private const string _name = "Indexes";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexesComponent.Add(Indexes, markAsStored);
        }
    }
}
