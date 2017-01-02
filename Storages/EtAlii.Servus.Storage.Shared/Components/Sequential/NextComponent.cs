namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;

    public class NextComponent : NonCompositeComponent
    {
        [JsonConstructor]
        protected NextComponent()
        { 
        }

        public NextComponent(Relation next)
        {
            _next = next;
        }

        public Relation Next { get { return _next; } }
        private Relation _next;

        internal override string Name { get { return _name; } }
        private const string _name = "Next";

        public override void Apply(IEditableEntry entry)
        {
            entry.Next = Next;
        }
    }
}
