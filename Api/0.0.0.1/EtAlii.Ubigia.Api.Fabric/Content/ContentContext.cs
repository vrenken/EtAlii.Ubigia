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
            _connection = connection;
            _connection.Content.Notifications.Updated += OnUpdated;
            _connection.Content.Notifications.Stored += OnStored;
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return await _connection.Content.Data.RetrieveDefinition(identifier);
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, UInt64 contentPartId)
        {
            return await _connection.Content.Data.Retrieve(identifier, contentPartId);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            if (contentDefinition == null)
            {
                throw new ArgumentNullException("contentDefinition");
            }

            await _connection.Content.Data.StoreDefinition(identifier, contentDefinition);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            if (contentDefinitionPart == null)
            {
                throw new ArgumentNullException("contentDefinitionPart");
            }

            await _connection.Content.Data.StoreDefinition(identifier, contentDefinitionPart);
        }


        public async Task Store(Identifier identifier, Content content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            await _connection.Content.Data.Store(identifier, content);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            if (contentPart == null)
            {
                throw new ArgumentNullException("contentPart");
            }

            await _connection.Content.Data.Store(identifier, contentPart);
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            return await _connection.Content.Data.Retrieve(identifier);
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
