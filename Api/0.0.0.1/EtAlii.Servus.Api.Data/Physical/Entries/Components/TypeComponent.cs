namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class TypeComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal TypeComponent()
        {
        }

        public TypeComponent(string type)
        {
            _type = type;
        }

        public string Type { get { return _type; } }
        private string _type;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Type";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TypeComponent = this;
        }
    }
}
