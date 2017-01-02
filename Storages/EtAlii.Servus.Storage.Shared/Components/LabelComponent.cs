namespace EtAlii.Servus.Storage
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    public class LabelComponent : NonCompositeComponent
    {
        [JsonConstructor]
        protected LabelComponent()
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

        public override void Apply(Client.Model.IEditableEntry entry)
        {
            entry.Label = Label;
        }
    }
}
