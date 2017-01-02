namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;

    [RequiresAuthenticationToken]
    public class RootNotificationHubProxy : HubProxyBase<RootNotificationHub>
    {
        public RootNotificationHubProxy()
        {
        }

        public void Added(Guid spaceId, Guid rootId)
        {
            Hub.Clients.All.added(rootId);
            //Hub.Clients.Group(spaceId.ToString()).added(rootId);
        }

        public void Changed(Guid spaceId, Guid rootId)
        {
            Hub.Clients.All.changed(rootId);
            //Hub.Clients.Group(spaceId.ToString()).changed(rootId);
        }

        public void Removed(Guid spaceId, Guid rootId)
        {
            Hub.Clients.All.removed(rootId);
            //Hub.Clients.Group(spaceId.ToString()).removed(rootId);
        }
    }
}
