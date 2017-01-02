namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;

    [RequiresAuthenticationToken]
    internal class SpaceHubServerProxy : HubProxyBase<SpaceHub>
    {
        public SpaceHubServerProxy()
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
