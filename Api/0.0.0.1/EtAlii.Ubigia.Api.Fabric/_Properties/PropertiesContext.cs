namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class PropertiesContext : IPropertiesContext
    {
        private readonly IDataConnection _connection;

        internal PropertiesContext(IDataConnection connection)
        {
            _connection = connection;
            _connection.Properties.Notifications.Stored += OnStored;
        }

        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            await _connection.Properties.Data.Store(identifier, properties, scope);
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return await _connection.Properties.Data.Retrieve(identifier, scope);
        }

        public event Action<Identifier> Stored = delegate { };

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
