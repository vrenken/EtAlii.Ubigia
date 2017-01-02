namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UpdatesComponent : CompositeComponent
    {
        [JsonConstructor]
        internal UpdatesComponent()
        {
        }

        public UpdatesComponent(IEnumerable<Relation> updates)
        {
            _updates = updates;
        }

        public IEnumerable<Relation> Updates { get { return _updates; } }
        private IEnumerable<Relation> _updates = new Relation[] { };

        protected internal override string Name { get { return _name; } }
        private const string _name = "Updates";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.UpdatesComponent.Add(Updates, markAsStored);
        }
    }
}
