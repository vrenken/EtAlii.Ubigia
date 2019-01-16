namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;

    public class Result 
    {
        public string Id { get; }

        public string Label { get; }

        public string PropertiesAsString => Properties?.ToString();

        public IPropertyDictionary Properties { get; }

        public object Data { get; }

        public object Group { get; }

        public Result(
            string id, 
            string label,
            IPropertyDictionary properties,
            object group)
        {
            Id = id;
            Label = label;
            Properties = properties;
            Group = group;
            Data = null;
        }
    }
}
