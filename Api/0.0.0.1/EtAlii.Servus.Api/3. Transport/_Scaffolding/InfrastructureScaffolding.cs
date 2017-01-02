namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureScaffolding : IScaffolding
    {
        private readonly IInfrastructureClient _client;

        public InfrastructureScaffolding(IInfrastructureClient client)
        {
            _client = client;
        }

        public void Register(Container container)
        {
            if (_client != null)
            {
                container.Register<IInfrastructureClient>(() => _client);
            }
            else
            {
                container.Register<IInfrastructureClient, DefaultInfrastructureClient>();
                //container.Register<ISerializer, Serializer>();
                container.Register<ISerializer>(() => new SerializerFactory().Create());
                container.Register<IHttpClientFactory, DefaultHttpClientFactory>();
            }
        }
    }
}
