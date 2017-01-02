namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class IndexedComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal IndexedComponent()
        { 
        }

        public IndexedComponent(Relation indexed)
        {
            _indexed = indexed;
        }

        public Relation Indexed { get { return _indexed; } }
        private Relation _indexed;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Indexed";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexedComponent = this;
        }
    }
}
