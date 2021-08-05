// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    public sealed class DataConnectionFactory
    {
        public IDataConnection Create(IDataConnectionOptions options)
        {
            var factoryMethod = options.FactoryExtension ?? (() => CreateInternal(options));
            return factoryMethod();
        }

        private IDataConnection CreateInternal(IDataConnectionOptions options)
        {
            var hasTransportProvider = options.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating data connection: No TransportProvider provided.");
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new DataConnectionScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in options.GetExtensions<IDataConnectionExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IDataConnection>();
        }
    }
}
