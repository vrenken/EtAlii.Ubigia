namespace EtAlii.Servus.Api.Transport
{
    internal partial class WebApiPropertyDataClient
    {
        public void Store(Identifier identifier, IPropertyDictionary properties)
        {
            var address = _addressFactory.Create(Connection.Storage, RelativeUri.Properties, UriParameter.EntryId, identifier.ToString());
            _client.Post(address, properties);
        }

        public IPropertyDictionary Retrieve(Identifier identifier)
        {
            var address = _addressFactory.Create(Connection.Storage, RelativeUri.Properties, UriParameter.EntryId, identifier.ToString());
            var properties = _client.Get<IPropertyDictionary>(address);
            return properties;
        }
    }
}
