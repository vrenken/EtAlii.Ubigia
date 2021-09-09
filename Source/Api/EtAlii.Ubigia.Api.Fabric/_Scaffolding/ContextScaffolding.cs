// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly FabricOptions _options;

        public ContextScaffolding(FabricOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IFabricContext>(serviceCollection =>
            {
                var entryContext = serviceCollection.GetInstance<IEntryContext>();
                var rootContext = serviceCollection.GetInstance<IRootContext>();
                var contentContext = serviceCollection.GetInstance<IContentContext>();
                var dataConnection = serviceCollection.GetInstance<IDataConnection>();
                var propertiesContext = serviceCollection.GetInstance<IPropertiesContext>();

                return new FabricContext(_options, entryContext, rootContext, contentContext, dataConnection, propertiesContext);
            });
            container.Register(() => _options.ConfigurationRoot);

            // We want to be able to instantiate parts of the DI hierarchy also without a connection.
            container.Register(() => _options.Connection ?? new DataConnectionStub());
        }
    }
}
