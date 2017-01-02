namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class PropertiesStorer : IPropertiesStorer
    {
        private readonly IStorage _storage;

        public PropertiesStorer(IStorage storage)
        {
            _storage = storage;
        }

        public void Store(Identifier identifier, PropertyDictionary properties)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentFabricException("No identifier was specified");
            }
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Properties.Store(containerId, properties);
        }
    }
}