namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;

    [RequiresAuthenticationToken]
    public class EntryHubServerProxy : HubProxyBase<EntryHub>
    {
        public EntryHubServerProxy()
        {
        }

        public void Prepared(Identifier identifier)
        {
            Hub.Clients.All.prepared(identifier);

            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<EntryHub>();
            //hubContext.Clients.All.prepared(identifier);

            //Hub.Clients.Group(identifier.Space.ToString()).Prepared(identifier);
        }

        public void Stored(Identifier identifier)
        {
            Hub.Clients.All.stored(identifier);
            //Hub.Clients.Group(identifier.Space.ToString()).Stored(identifier);
        }
    }
}
