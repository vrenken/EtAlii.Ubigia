namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SystemStorageConnectionScaffolding : IScaffolding
    {
        private readonly ISystemStorageTransport _transport;

        public SystemStorageConnectionScaffolding(ISystemStorageTransport transport)
        {
            _transport = transport;
        }

        public void Register(Container container)
        {
            container.Register(() => _transport);
        }
    }
}
