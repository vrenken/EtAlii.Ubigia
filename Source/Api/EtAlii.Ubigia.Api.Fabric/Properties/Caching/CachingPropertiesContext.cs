// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    public class CachingPropertiesContext : IPropertiesContext
    {
        private readonly IPropertyCacheRetrieveHandler _retrieveHandler;
        private readonly IPropertyCacheStoreHandler _storeHandler;

        public CachingPropertiesContext(
            IPropertiesCacheContextProvider contextProvider,
            IPropertyCacheRetrieveHandler retrieveHandler,
            IPropertyCacheStoreHandler storeHandler)
        {
            _retrieveHandler = retrieveHandler;
            _storeHandler = storeHandler;

            contextProvider.Context.Stored += OnStored;
        }

        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            await _storeHandler
                .Handle(identifier, properties, scope)
                .ConfigureAwait(false);
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return await _retrieveHandler
                .Handle(identifier, scope)
                .ConfigureAwait(false);
        }

        // TODO: These events should be converted into a true OO oriented pub-sub pattern.
        public event Action<Identifier> Stored = delegate { };

        private void OnStored(Identifier identifier)
        {
            var task = Task.Run(async () =>
            {
                await _storeHandler
                    .Handle(identifier)
                    .ConfigureAwait(false);
            });
            task.Wait();
            Stored(identifier);
        }
    }
}
