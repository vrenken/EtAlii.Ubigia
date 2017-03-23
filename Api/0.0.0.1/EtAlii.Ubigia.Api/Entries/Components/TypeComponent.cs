namespace EtAlii.Ubigia.Api
{
    using Newtonsoft.Json;

    public class TypeComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal TypeComponent()
        {
        }

        public string Type { get; internal set; }

        protected internal override string Name => _name;
        private const string _name = "Type";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TypeComponent = this;
        }
    }
}
