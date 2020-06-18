namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemTransportProvider : IStorageTransportProvider
    {
        private readonly IInfrastructure _infrastructure;

        private SystemTransportProvider(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public static SystemTransportProvider Create(IInfrastructure infrastructure)
        {
            return new SystemTransportProvider(infrastructure);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new SystemSpaceTransport(address, _infrastructure);
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new SystemStorageTransport(address, _infrastructure);
        }
        
        public IStorageTransport GetStorageTransport()
        {
            return new SystemStorageTransport(_infrastructure.Configuration.ManagementAddress, _infrastructure);
        }
    }
}