// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class ContentContext : IContentContext
    {
        private readonly IDataConnection _connection;

        internal ContentContext(IDataConnection connection)
        {
            if (connection == null) return; // In the new setup the LogicalContext and IDataConnection are instantiated at the same time.
            _connection = connection;
            _connection.Content.Notifications.Updated += OnUpdated;
            _connection.Content.Notifications.Stored += OnStored;
        }

        public Task<ContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return _connection.Content.Data.RetrieveDefinition(identifier);
        }

        public Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return _connection.Content.Data.Retrieve(identifier, contentPartId);
        }
        
        public Task<Content> Retrieve(Identifier identifier)
        {
            return _connection.Content.Data.Retrieve(identifier);
        }
        
        public Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            if (contentDefinition == null)
            {
                throw new ArgumentNullException(nameof(contentDefinition));
            }

            return _connection.Content.Data.StoreDefinition(identifier, contentDefinition);
        }

        public Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            if (contentDefinitionPart == null)
            {
                throw new ArgumentNullException(nameof(contentDefinitionPart));
            }

            return _connection.Content.Data.StoreDefinition(identifier, contentDefinitionPart);
        }


        public Task Store(Identifier identifier, Content content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return _connection.Content.Data.Store(identifier, content);
        }

        public Task Store(Identifier identifier, ContentPart contentPart)
        {
            if (contentPart == null)
            {
                throw new ArgumentNullException(nameof(contentPart));
            }

            return _connection.Content.Data.Store(identifier, contentPart);
        }

        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        private void OnUpdated(Identifier identifier)
        {
            Updated(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
