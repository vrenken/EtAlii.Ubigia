namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Transport;
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

            var transport = configuration.TransportProvider.GetStorageTransport();
            var scaffoldings = transport
                .CreateScaffolding()
                .Concat(new EtAlii.xTechnology.MicroContainer.IScaffolding[]
            {
                new SystemConnectionScaffolding(configuration),
                new SystemInfrastructureScaffolding(), 
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            var connection = container.GetInstance<ISystemConnection>();
            return connection;
        }
    }
}
