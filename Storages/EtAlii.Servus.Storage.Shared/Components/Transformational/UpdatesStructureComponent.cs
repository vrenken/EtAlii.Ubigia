namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UpdatesComponent : CompositeComponent
    {
        [JsonConstructor]
        protected UpdatesComponent()
        { 
        }

        public UpdatesComponent(IEnumerable<Relation> updates)
        {
            _updates = updates;
        }

        public IEnumerable<Relation> Updates { get { return _updates; } }
        private IEnumerable<Relation> _updates = new Relation[] { };

        internal override string Name { get { return _name; } }
        private const string _name = "Updates";

        public override void Apply(IEditableEntry entry)
        {
            entry.Updates.AddRange(Updates);
        }
    }
}
