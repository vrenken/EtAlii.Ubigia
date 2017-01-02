namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class TransportScaffolding : ITransportScaffolding
    {
        private readonly ITransport _transport;

        public TransportScaffolding(ITransport transport)
        {
            _transport = transport;
        }

        public void Register(Container container)
        {
            container.Register<ITransport, ITransport>(() => _transport);
        }
    }
}
