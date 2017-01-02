namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public class PropertiesNotificationHubProxy : HubProxyBase<PropertiesNotificationHub>
    {
        public PropertiesNotificationHubProxy()
        {
        }

        public void Stored(Identifier identifier)
        {
            Hub.Clients.All.stored(identifier);
        }
    }
}
