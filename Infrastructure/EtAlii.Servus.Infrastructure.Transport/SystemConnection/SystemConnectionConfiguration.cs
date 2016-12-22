namespace EtAlii.Servus.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

    public class SystemConnectionConfiguration : ISystemConnectionConfiguration
    {
        public ISystemConnectionExtension[] Extensions { get { return _extensions; } }
        private ISystemConnectionExtension[] _extensions;

        public IStorageTransportProvider TransportProvider { get { return _transportProvider; } }
        private IStorageTransportProvider _transportProvider;

        public Func<ISystemConnection> FactoryExtension { get { return _factoryExtension; } }
        private Func<ISystemConnection> _factoryExtension;

        public IInfrastructure Infrastructure { get { return _infrastructure; } }
        private IInfrastructure _infrastructure;
        

        public SystemConnectionConfiguration()
        {
            _extensions = new ISystemConnectionExtension[0];
        }

        public ISystemConnectionConfiguration Use(ISystemConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public ISystemConnectionConfiguration Use(IStorageTransportProvider transportProvider)
        {
            if (_transportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this SystemConnectionConfiguration", nameof(transportProvider));
            }
            if (transportProvider == null)
            {
                throw new ArgumentNullException(nameof(transportProvider));
            }

            _transportProvider = transportProvider;

            return this;
        }

        public ISystemConnectionConfiguration Use(Func<ISystemConnection> factoryExtension)
        {
            _factoryExtension = factoryExtension;
            return this;
        }

        public ISystemConnectionConfiguration Use(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            return this;
        }
    }
}
