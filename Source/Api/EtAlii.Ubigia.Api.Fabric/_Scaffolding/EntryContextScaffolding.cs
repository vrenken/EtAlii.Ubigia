// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class EntryContextScaffolding : IScaffolding
    {
        private readonly bool _enableCaching;

        public EntryContextScaffolding(bool enableCaching)
        {
            _enableCaching = enableCaching;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_enableCaching)
            {
                // Caching enabled.
                container.Register<IEntryCacheChangeHandler, EntryCacheChangeHandler>();
                container.Register<IEntryCacheGetHandler, EntryCacheGetHandler>();
                container.Register<IEntryCacheGetRelatedHandler, EntryCacheGetRelatedHandler>();
                container.Register<IEntryCacheStoreHandler, EntryCacheStoreHandler>();
                container.Register<IEntryCacheReconnectOnStartupHandler, EntryCacheReconnectOnStartupHandler>();

                container.Register<IEntryCacheProvider, EntryCacheProvider>();
                container.Register<IEntryCacheHelper, EntryCacheHelper>();
                container.Register(CreateEntryCacheContextProvider);
                container.Register<IEntryContext, CachingEntryContext>();
            }
            else
            {
                // Caching disabled.
                container.Register<IEntryContext, EntryContext>();
            }

        }

        private IEntryCacheContextProvider CreateEntryCacheContextProvider(IServiceCollection services)
        {
            var connection = services.GetInstance<IDataConnection>();
            var context = new EntryContext(connection); // we need to instantiate the original EntryContext and use it in the EntryCacheContextProvider.
            return new EntryCacheContextProvider(context);
        }
    }
}
