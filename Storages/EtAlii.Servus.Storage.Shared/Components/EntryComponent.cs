namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;

    public class EntryComponent : NonCompositeComponent
    {
        [JsonConstructor]
        protected EntryComponent()
        { 
        }

        public EntryComponent(Identifier identifier)
        {
            _id = identifier;
        }

        public Identifier Id { get { return _id; } }
        private Identifier _id;


        internal override string Name { get { return _name; } }
        private const string _name = "Entity";

        public override void Apply(IEditableEntry entry)
        {
            entry.Id = Id;
        }
    }
}
