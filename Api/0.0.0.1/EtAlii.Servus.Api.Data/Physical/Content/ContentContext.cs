namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;

    public class ContentContext : IContentContext
    {
        private readonly IContentNotificationClient _notificationClient;
        private readonly IContentDataClient _dataClient;
        private readonly IConnectionStatusProvider _connectionStatusProvider;

        internal ContentContext(
            IContentNotificationClient notificationClient, 
            IContentDataClient dataClient,
            IConnectionStatusProvider connectionStatusProvider)
        {
            _notificationClient = notificationClient;
            _notificationClient.Updated += OnUpdated;
            _notificationClient.Stored += OnStored;
            _dataClient = dataClient;
            _connectionStatusProvider = connectionStatusProvider;
        }

        public IReadOnlyContentDefinition RetrieveDefinition(Identifier identifier)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.RetrieveDefinition(identifier);
        }

        public IReadOnlyContentPart Retrieve(Identifier identifier, UInt64 contentPartId)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Retrieve(identifier, contentPartId);
        }

        public void StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            if (contentDefinition == null)
            {
                throw new ArgumentNullException("contentDefinition");
            }

            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            _dataClient.StoreDefinition(identifier, contentDefinition);
        }

        public void StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            if (contentDefinitionPart == null)
            {
                throw new ArgumentNullException("contentDefinitionPart");
            }

            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            _dataClient.StoreDefinition(identifier, contentDefinitionPart);
        }


        public void Store(Identifier identifier, EtAlii.Servus.Api.Content content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            _dataClient.Store(identifier, content);
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            if (contentPart == null)
            {
                throw new ArgumentNullException("contentPart");
            }

            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            _dataClient.Store(identifier, contentPart);
        }

        public IReadOnlyContent Retrieve(Identifier identifier)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Retrieve(identifier);
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
