namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;

    public class ParentComponent : NonCompositeComponent
    {
        [JsonConstructor]
        protected ParentComponent()
        { 
        }

        public ParentComponent(Relation parent)
        {
            _parent = parent;
        }

        public Relation Parent { get { return _parent; } }
        private Relation _parent;

        internal override string Name { get { return _name; } }
        private const string _name = "Parent";

        public override void Apply(IEditableEntry entry)
        {
            entry.Parent = Parent;
        }
    }
}
