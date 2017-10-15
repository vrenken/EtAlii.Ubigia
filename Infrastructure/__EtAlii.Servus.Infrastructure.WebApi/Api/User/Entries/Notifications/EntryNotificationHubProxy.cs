namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public class EntryNotificationHubProxy : HubProxyBase<EntryNotificationHub>
    {
        public EntryNotificationHubProxy()
        {
        }

        public void Prepared(Identifier identifier)
        {
            Hub.Clients.All.prepared(identifier);

            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<EntryNotificationHub>();
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
