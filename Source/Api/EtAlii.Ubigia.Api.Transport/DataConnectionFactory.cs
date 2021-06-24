// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    public sealed class DataConnectionFactory
    {
        public IDataConnection Create(IDataConnectionConfiguration configuration)
        {
            var factoryMethod = configuration.FactoryExtension ?? (() => CreateInternal(configuration));
            return factoryMethod();
        }

        private IDataConnection CreateInternal(IDataConnectionConfiguration configuration)
        {
            var hasTransportProvider = configuration.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating data connection: No TransportProvider provided.");
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new DataConnectionScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.GetExtensions<IDataConnectionExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IDataConnection>();
        }
    }
}
