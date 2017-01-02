namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class Parent2Component : NonCompositeComponent
    {
        [JsonConstructor]
        internal Parent2Component()
        { 
        }

        public Parent2Component(Relation parent2)
        {
            _parent2 = parent2;
        }

        public Relation Parent2 { get { return _parent2; } }
        private Relation _parent2;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Parent2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Parent2Component = this;
        }
    }
}
