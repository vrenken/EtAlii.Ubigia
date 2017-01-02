namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public class ContentDefinitionNotificationHubProxy : HubProxyBase<ContentDefinitionNotificationHub>
    {
        public ContentDefinitionNotificationHubProxy()
        {
        }

        public void Updated(Identifier identifier)
        {
            Hub.Clients.All.updated(identifier);
        }

        public void Stored(Identifier identifier)
        {
            Hub.Clients.All.stored(identifier);
        }
    }
}
