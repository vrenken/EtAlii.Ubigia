namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class PropertiesGetter : IPropertiesGetter
    {
        private readonly IStorage _storage;

        public PropertiesGetter(IStorage storage)
        {
            _storage = storage;
        }

        public PropertyDictionary Get(Identifier identifier)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var properties = _storage.Properties.Retrieve(containerId);
            return properties;
        }
    }
}