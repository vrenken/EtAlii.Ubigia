namespace EtAlii.Ubigia.Api
{
    using Newtonsoft.Json;

    public class TypeComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal TypeComponent()
        {
        }

        public string Type { get { return _type; } internal set { _type = value; } }
        private string _type;

        protected internal override string Name => _name;
        private const string _name = "Type";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TypeComponent = this;
        }
    }
}
