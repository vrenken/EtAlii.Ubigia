namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class ParentComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal ParentComponent()
        { 
        }

        public ParentComponent(Relation parent)
        {
            _parent = parent;
        }

        public Relation Parent { get { return _parent; } }
        private Relation _parent;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Parent";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ParentComponent = this;
        }
    }
}
