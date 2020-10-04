﻿namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class PropertiesContext : IPropertiesContext
    {
        private readonly IDataConnection _connection;

        internal PropertiesContext(IDataConnection connection)
        {
            if (connection == null) return; // In the new setup the LogicalContext and IDataConnection are instantiated at the same time.
            _connection = connection;
            _connection.Properties.Notifications.Stored += OnStored;
        }

        public Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return _connection.Properties.Data.Store(identifier, properties, scope);
        }

        public Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return _connection.Properties.Data.Retrieve(identifier, scope);
        }

        public event Action<Identifier> Stored = delegate { };

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
