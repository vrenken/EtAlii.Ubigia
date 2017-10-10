namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;

    [RequiresAuthenticationToken]
    public class ContentHubServerProxy : HubProxyBase<ContentHub>
    {
        public ContentHubServerProxy()
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
