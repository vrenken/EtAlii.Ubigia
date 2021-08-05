// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using EtAlii.xTechnology.MicroContainer;

    public sealed class ManagementConnectionFactory : IManagementConnectionFactory
    {
        /// <inheritdoc />
        public IManagementConnection Create(IManagementConnectionOptions options)
        {
            var factoryMethod = options.FactoryExtension ?? (() => CreateInternal(options));
            return factoryMethod();
        }

        private IManagementConnection CreateInternal(IManagementConnectionOptions options)
        {
            var hasTransportProvider = options.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating management connection: No TransportProvider provided.");
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionScaffolding(options)
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in options.GetExtensions<IManagementConnectionExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IManagementConnection>();
        }
    }
}
