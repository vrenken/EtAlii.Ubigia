namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class IdentifierComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal IdentifierComponent()
        { 
        }

        public Identifier Id { get { return _id; } internal set { _id = value; } }
        private Identifier _id;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Identifier";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IdComponent = this;
        }
    }
}
