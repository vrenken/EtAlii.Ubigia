namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionScaffolding : IScaffolding
    {
        private readonly ITransport _transport;

        public SpaceConnectionScaffolding(ITransport transport)
        {
            _transport = transport;
        }

        public void Register(Container container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<IClientContext, ClientContext>();
            container.Register<ITransport>(() => _transport);
            container.Register<ISpaceConnection, SpaceConnection>();
            container.RegisterInitializer<ISpaceConnection>(connection =>
            {
                var transport = container.GetInstance<ITransport>();
                transport.Initialize(container);
            });
        }
    }
}
