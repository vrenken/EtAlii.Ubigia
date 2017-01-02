namespace EtAlii.Servus.Api.Transport.SignalR
{
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.xTechnology.MicroContainer;

    internal class SignalRSpaceClientsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ISpaceConnection, SignalRSpaceConnection>();

            container.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>();

            // TODO: Replace by SignalRAuthenticationDataClient
            container.Register<IAuthenticationDataClient, WebApiAuthenticationDataClient>();

            container.Register<IEntryDataClient, SignalREntryDataClient>();
            container.Register<IEntryNotificationClient, SignalREntryNotificationClient>();

            container.Register<IRootDataClient, SignalRRootDataClient>();
            container.Register<IRootNotificationClient, SignalRRootNotificationClient>();

            container.Register<IPropertiesDataClient, SignalRPropertiesDataClient>();
            container.Register<IPropertiesNotificationClient, SignalRPropertiesNotificationClient>();

            container.Register<IContentDataClient, SignalRContentDataClient>();
            container.Register<IContentNotificationClient, SignalRContentNotificationClient>();
        }
    }
}
