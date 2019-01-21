namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemConnectionConfiguration : ISystemConnectionConfiguration
    {
        public ISystemConnectionExtension[] Extensions { get; private set; }

        public IStorageTransportProvider TransportProvider { get; private set; }

        public Func<ISystemConnection> FactoryExtension { get; private set; }

        public IInfrastructure Infrastructure { get; private set; }


        public SystemConnectionConfiguration()
        {
            Extensions = new ISystemConnectionExtension[0];
        }

        public ISystemConnectionConfiguration Use(ISystemConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public ISystemConnectionConfiguration Use(IStorageTransportProvider transportProvider)
        {
            if (TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this SystemConnectionConfiguration", nameof(transportProvider));
            }

            TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return this;
        }

        public ISystemConnectionConfiguration Use(Func<ISystemConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        public ISystemConnectionConfiguration Use(IInfrastructure infrastructure)
        {
            Infrastructure = infrastructure;
            return this;
        }
    }
}
