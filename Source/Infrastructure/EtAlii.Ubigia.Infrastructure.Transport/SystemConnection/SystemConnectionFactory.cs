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
        public ISystemConnection Create(ISystemConnectionConfiguration configuration)
        {
            var factoryMethod = configuration.FactoryExtension ?? (() => CreateInternal(configuration));
            return factoryMethod();
        }

        private ISystemConnection CreateInternal(ISystemConnectionConfiguration configuration)
        {
            var hasTransportProvider = configuration.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating system connection: No TransportProvider provided.");
            }

            if (configuration.Infrastructure == null)
            {
                throw new NotSupportedException("A Infrastructure is required to construct a SystemConnection instance");
            }

            var container = new Container();

            var serviceDetails = configuration.Infrastructure.Configuration.ServiceDetails.Single(sd => sd.IsSystemService);

            var transport = configuration.TransportProvider.GetStorageTransport(serviceDetails.ManagementAddress);
            var scaffoldings = transport
                .CreateScaffolding()
                .Concat(new IScaffolding[]
            {
                new SystemConnectionScaffolding(configuration),
                new SystemInfrastructureScaffolding(), 
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.GetExtensions<ISystemConnectionExtension>())
            {
                extension.Initialize(container);
            }

            var connection = container.GetInstance<ISystemConnection>();
            return connection;
        }
    }
}
