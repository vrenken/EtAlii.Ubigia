namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class NextComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal NextComponent()
        { 
        }

        public NextComponent(Relation next)
        {
            _next = next;
        }

        public Relation Next { get { return _next; } }
        private Relation _next;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Next";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.NextComponent = this;
        }
    }
}
