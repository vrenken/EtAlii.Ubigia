namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    public class CachingPropertiesContext : IPropertiesContext
    {
        private readonly IPropertyCacheRetrieveHandler _retrieveHandler;
        private readonly IPropertyCacheStoreHandler _storeHandler;
        private readonly IPropertyCacheContextProvider _contextProvider;

        internal CachingPropertiesContext(
            IPropertyCacheContextProvider contextProvider,
            IPropertyCacheRetrieveHandler retrieveHandler,
            IPropertyCacheStoreHandler storeHandler)
        {
            _retrieveHandler = retrieveHandler;
            _storeHandler = storeHandler;

            _contextProvider = contextProvider;
            _contextProvider.Context.Stored += OnStored;
        }

        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            await _storeHandler.Handle(identifier, properties, scope);
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return await _retrieveHandler.Handle(identifier, scope);
        }

        public event Action<Identifier> Stored = delegate { };

        private void OnStored(Identifier identifier)
        {
            var task = Task.Run(async () =>
            {
                await _storeHandler.Handle(identifier);
            });
            task.Wait();
            Stored(identifier);
        }
    }
}
