namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;

    public class Result 
    {
        public string Id => _id;
        private readonly string _id;

        public string Label => _label;
        private readonly string _label;

        public string PropertiesAsString => _properties?.ToString();

        public IPropertyDictionary Properties => _properties;
        private readonly IPropertyDictionary _properties;

        public object Data => _data;
        private readonly object _data;

        public object Group => _group;
        private readonly object _group;

        public Result(
            string id, 
            string label,
            IPropertyDictionary properties,
            object group)
        {
            _id = id;
            _label = label;
            _properties = properties;
            _group = group;
            _data = null;
        }
    }
}
