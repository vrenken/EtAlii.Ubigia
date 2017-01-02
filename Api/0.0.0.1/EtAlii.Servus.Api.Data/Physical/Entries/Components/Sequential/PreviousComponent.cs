namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class PreviousComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal PreviousComponent()
        { 
        }

        public PreviousComponent(Relation previous)
        {
            _previous = previous;
        }

        public Relation Previous { get { return _previous; } }
        private Relation _previous;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Previous";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.PreviousComponent = this;
        }
    }
}
