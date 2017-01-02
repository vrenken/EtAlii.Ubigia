namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;

    public class SystemStorageConnectionConfiguration : ISystemStorageConnectionConfiguration
    {
        public ISystemTransport Transport { get { return _transport; } }
        private ISystemTransport _transport;

        public IInfrastructure Infrastructure { get { return _infrastructure; } }
        private IInfrastructure _infrastructure;

        public SystemStorageConnectionConfiguration()
        {
        }

        public ISystemStorageConnectionConfiguration Use(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            return this;
        }

        public ISystemStorageConnectionConfiguration Use(ISystemTransport transport)
        {
            _transport = transport;
            return this;
        }
    }
}
