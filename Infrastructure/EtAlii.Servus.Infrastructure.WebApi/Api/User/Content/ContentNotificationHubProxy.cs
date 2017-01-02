namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public class ContentNotificationHubProxy : HubProxyBase<ContentNotificationHub>
    {
        public ContentNotificationHubProxy()
        {
        }

        public void Updated(Identifier identifier)
        {
            Hub.Clients.All.updated(identifier);
        }
    }
}
