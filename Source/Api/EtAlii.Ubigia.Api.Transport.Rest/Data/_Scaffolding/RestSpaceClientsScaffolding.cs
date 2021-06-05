namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class RestSpaceClientsScaffolding : IScaffolding
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public RestSpaceClientsScaffolding(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public void Register(Container container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<ISpaceConnection, RestSpaceConnection>();

            container.Register<IAuthenticationDataClient, RestAuthenticationDataClient>();

            container.Register<IEntryDataClient, RestEntryDataClient>();
            container.Register<IRootDataClient, RestRootDataClient>();
            container.Register<IContentDataClient, RestContentDataClient>();
            container.Register<IPropertiesDataClient, RestPropertiesDataClient>();

            // Web API does not support notifications (yet).
            container.Register<IEntryNotificationClient, EntryNotificationClientStub>();
            container.Register<IRootNotificationClient, RootNotificationClientStub>();
            container.Register<IContentNotificationClient, ContentNotificationClientStub>();
            container.Register<IPropertiesNotificationClient, PropertiesNotificationClientStub>();

            if (_infrastructureClient != null)
            {
                container.Register(() => _infrastructureClient);
            }
            else
            {
                container.Register<IInfrastructureClient, DefaultInfrastructureClient>();
                container.Register(() => new SerializerFactory().Create());
                container.Register<IHttpClientFactory, DefaultHttpClientFactory>();
            }
        }
    }
}
