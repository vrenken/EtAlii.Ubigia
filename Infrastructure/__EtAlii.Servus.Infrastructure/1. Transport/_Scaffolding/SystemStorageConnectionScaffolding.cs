﻿namespace EtAlii.Servus.Infrastructure.Transport
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class SystemStorageConnectionScaffolding : xTechnology.MicroContainer.IScaffolding
    {
        private readonly ISystemStorageTransport _transport;

        public SystemStorageConnectionScaffolding(ISystemStorageTransport transport)
        {
            _transport = transport;
        }

        public void Register(Container container)
        {
            container.Register<ISystemStorageTransport>(() => _transport);
        }
    }
}
