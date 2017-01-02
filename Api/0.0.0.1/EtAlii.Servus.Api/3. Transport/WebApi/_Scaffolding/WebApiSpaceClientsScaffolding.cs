namespace EtAlii.Servus.Api.Transport.WebApi
{
    using EtAlii.xTechnology.MicroContainer;

    internal class WebApiSpaceClientsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
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
        }
    }
}
