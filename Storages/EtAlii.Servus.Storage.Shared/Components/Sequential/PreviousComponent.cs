namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;

    public class PreviousComponent : NonCompositeComponent
    {
        [JsonConstructor]
        protected PreviousComponent()
        { 
        }

        public PreviousComponent(Relation previous)
        {
            _previous = previous;
        }

        public Relation Previous { get { return _previous; } }
        private Relation _previous;

        internal override string Name { get { return _name; } }
        private const string _name = "Previous";

        public override void Apply(IEditableEntry entry)
        {
            entry.Previous = Previous;
        }
    }
}
