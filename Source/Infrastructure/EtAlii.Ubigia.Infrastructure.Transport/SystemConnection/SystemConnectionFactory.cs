// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public sealed class SystemConnectionFactory : ISystemConnectionFactory
    {
        /// <inheritdoc />
        public ISystemConnection Create(ISystemConnectionOptions options)
        {
            var factoryMethod = options.FactoryExtension ?? (() => CreateInternal(options));
            return factoryMethod();
        }

        private ISystemConnection CreateInternal(ISystemConnectionOptions options)
        {
            var hasTransportProvider = options.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating system connection: No TransportProvider provided.");
            }

            if (options.Infrastructure == null)
            {
                throw new NotSupportedException("A Infrastructure is required to construct a SystemConnection instance");
            }

            var container = new Container();

            var serviceDetails = options.Infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

            var transport = options.TransportProvider.GetStorageTransport(serviceDetails.ManagementAddress);
            var scaffoldings = transport
                .CreateScaffolding()
                .Concat(new IScaffolding[]
            {
                new SystemConnectionScaffolding(options),
                new SystemInfrastructureScaffolding(),
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in ((IExtensible)options).Extensions)
            {
                extension.Initialize(container);
            }

            var connection = container.GetInstance<ISystemConnection>();
            return connection;
        }
    }
}
