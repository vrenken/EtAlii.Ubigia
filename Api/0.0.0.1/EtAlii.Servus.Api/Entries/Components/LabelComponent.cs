namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class LabelComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal LabelComponent()
        {
        }

        public LabelComponent(string label)
        {
            _label = label;
        }

        public string Label { get { return _label; } }
        private string _label;

        internal override string Name { get { return _name; } }
        private const string _name = "Label";

        internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.LabelComponent = this;
        }
    }
}
