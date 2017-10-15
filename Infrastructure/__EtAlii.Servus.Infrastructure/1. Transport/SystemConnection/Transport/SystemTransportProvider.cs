namespace EtAlii.Servus.Infrastructure.Transport
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

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

        public ISpaceTransport GetSpaceTransport()
        {
            return new SystemSpaceTransport(_infrastructure);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new SystemStorageTransport(_infrastructure);
        }
    }
}