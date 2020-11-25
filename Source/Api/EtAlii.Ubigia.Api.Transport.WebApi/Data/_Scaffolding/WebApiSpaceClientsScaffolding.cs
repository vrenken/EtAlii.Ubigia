namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class WebApiSpaceClientsScaffolding : IScaffolding
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public WebApiSpaceClientsScaffolding(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public void Register(Container container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<ISpaceConnection, WebApiSpaceConnection>();

            container.Register<IAuthenticationDataClient, WebApiAuthenticationDataClient>();

            container.Register<IEntryDataClient, WebApiEntryDataClient>();
            container.Register<IRootDataClient, WebApiRootDataClient>();
            container.Register<IContentDataClient, WebApiContentDataClient>();
            container.Register<IPropertiesDataClient, WebApiPropertiesDataClient>();

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
