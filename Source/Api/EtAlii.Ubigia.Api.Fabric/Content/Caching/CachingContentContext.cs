// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    internal class CachingContentContext : IContentContext
    {
        private readonly IContentCacheContextProvider _contextProvider;

        private readonly IContentCacheRetrieveDefinitionHandler _retrieveDefinitionHandler;
        private readonly IContentCacheStoreDefinitionHandler _storeDefinitionHandler;

        private readonly IContentCacheRetrieveHandler _retrieveHandler;
        private readonly IContentCacheRetrievePartHandler _retrievePartHandler;
        private readonly IContentCacheStoreHandler _storeHandler;
        private readonly IContentCacheStorePartHandler _storePartHandler;

        public CachingContentContext(
            IContentCacheContextProvider contextProvider,
            IContentCacheRetrieveDefinitionHandler retrieveDefinitionHandler,
            IContentCacheStoreDefinitionHandler storeDefinitionHandler,
            IContentCacheRetrieveHandler retrieveHandler,
            IContentCacheRetrievePartHandler retrievePartHandler,
            IContentCacheStoreHandler storeHandler,
            IContentCacheStorePartHandler storePartHandler)
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

        public async Task<ContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return await _retrieveDefinitionHandler.Handle(identifier).ConfigureAwait(false);
        }

        public async Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return await _retrievePartHandler.Handle(identifier, contentPartId).ConfigureAwait(false);
        }

        public async Task<Content> Retrieve(Identifier identifier)
        {
            // TODO: IMPORTANT ISSUE WITH CACHING.
            return await _retrieveHandler.Handle(identifier).ConfigureAwait(false);
            //return await _contextProvider.Context.Retrieve(identifier)
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            await _storeDefinitionHandler.Handle(identifier, contentDefinition).ConfigureAwait(false);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            await _contextProvider.Context.StoreDefinition(identifier, contentDefinitionPart).ConfigureAwait(false);
        }


        public async Task Store(Identifier identifier, Content content)
        {
            await _storeHandler.Handle(identifier, content).ConfigureAwait(false);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await _storePartHandler.Handle(identifier, contentPart).ConfigureAwait(false);
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
