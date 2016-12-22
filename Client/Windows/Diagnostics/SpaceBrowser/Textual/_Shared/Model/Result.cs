namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;

    public class Result 
    {
        public string Id { get { return _id; } }
        private readonly string _id;

        public string Label { get { return _label; } }
        private readonly string _label;

        public string PropertiesAsString { get { return _properties?.ToString(); } }

        public IPropertyDictionary Properties { get { return _properties; } }
        private readonly IPropertyDictionary _properties;

        public object Data { get { return _data; } }
        private readonly object _data;

        public object Group { get { return _group; } }
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
