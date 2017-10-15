namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;

    [RequiresAuthenticationToken]
    internal class SpaceNotificationHubProxy : HubProxyBase<SpaceNotificationHub>
    {
        public SpaceNotificationHubProxy()
        {
        }

        public void Subscribe(Guid spaceId)
        {
            //Hub.Groups.Add(spaceId.ToString());
        }

        public void Unsubscribe(Guid spaceId)
        {
            //Hub.Groups.Remove(spaceId.ToString());
        }

        public void Removed(Guid spaceId, Guid rootId)
        {
            Hub.Clients.Group(spaceId.ToString()).Removed(rootId);
        }
    }
}
