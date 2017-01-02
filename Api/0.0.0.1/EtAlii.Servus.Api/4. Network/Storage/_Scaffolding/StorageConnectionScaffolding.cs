namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionScaffolding : IScaffolding
    {
        private readonly ITransport _transport;

        public StorageConnectionScaffolding(ITransport transport)
        {
            _transport = transport;
        }

        public void Register(Container container)
        {
            container.Register<ITransport>(() => _transport);
            container.Register<IStorageConnection, StorageConnection>();
            container.Register<IAddressFactory, AddressFactory>();
        }
    }
}
