namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class TransportScaffolding<TTransport> : ITransportScaffolding
        where TTransport : ITransport, new()
    {
        public void Register(Container container)
        {
            container.Register<ITransport, TTransport>();
        }
    }
}
