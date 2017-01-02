namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public class CachingContentContext : IContentContext
    {
        private readonly ContentCacheContextProvider _contextProvider;
        private readonly object _lockObject = new object();

        private readonly ContentCacheRetrieveDefinitionHandler _retrieveDefinitionHandler;
        private readonly ContentCacheStoreDefinitionHandler _storeDefinitionHandler;

        private readonly ContentCacheRetrieveHandler _retrieveHandler;
        private readonly ContentCacheRetrievePartHandler _retrievePartHandler;
        private readonly ContentCacheStoreHandler _storeHandler;
        private readonly ContentCacheStorePartHandler _storePartHandler;

        internal CachingContentContext(
            ContentCacheContextProvider contextProvider,
            ContentCacheRetrieveDefinitionHandler retrieveDefinitionHandler,
            ContentCacheStoreDefinitionHandler storeDefinitionHandler,
            ContentCacheRetrieveHandler retrieveHandler,
            ContentCacheRetrievePartHandler retrievePartHandler,
            ContentCacheStoreHandler storeHandler,
            ContentCacheStorePartHandler storePartHandler)
        {
            _storeDefinitionHandler = storeDefinitionHandler;
            _retrieveDefinitionHandler = retrieveDefinitionHandler;

            _retrieveHandler = retrieveHandler;
            _retrievePartHandler = retrievePartHandler;
            _storeHandler = storeHandler;
            _storePartHandler = storePartHandler;

            _contextProvider = contextProvider;
            _contextProvider.Context.Updated += OnUpdated;
            _contextProvider.Context.Stored += OnStored;
        }

        public IReadOnlyContentDefinition RetrieveDefinition(Identifier identifier)
        {
            lock (_lockObject)
            {
                return _retrieveDefinitionHandler.Handle(identifier);
            }
        }

        public IReadOnlyContentPart Retrieve(Identifier identifier, UInt64 contentPartId)
        {
            lock (_lockObject)
            {
                return _retrievePartHandler.Handle(identifier, contentPartId);
            }
        }

        public void StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            lock (_lockObject)
            {
                _storeDefinitionHandler.Handle(identifier, contentDefinition);
            }
        }

        public void StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            lock (_lockObject)
            {
                _contextProvider.Context.StoreDefinition(identifier, contentDefinitionPart);
            }
        }


        public void Store(Identifier identifier, EtAlii.Servus.Api.Content content)
        {
            lock (_lockObject)
            {
                _storeHandler.Handle(identifier, content);
            }
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            lock (_lockObject)
            {
                _storePartHandler.Handle(identifier, contentPart);
            }
        }

        public IReadOnlyContent Retrieve(Identifier identifier)
        {
            lock (_lockObject)
            {
                return _contextProvider.Context.Retrieve(identifier);
            }
        }

        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        private void OnUpdated(Identifier identifier)
        {
            lock (_lockObject)
            {
                Updated(identifier);
            }
        }

        private void OnStored(Identifier identifier)
        {
            lock (_lockObject)
            {
                Stored(identifier);
            }
        }
    }
}
