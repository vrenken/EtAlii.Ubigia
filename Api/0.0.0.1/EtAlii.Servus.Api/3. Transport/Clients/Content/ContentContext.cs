namespace EtAlii.Servus.Api.Transport
{
    internal class ContentContext : SpaceClientContextBase<IContentDataClient, IContentNotificationClient>, IContentContext
    {
        public ContentContext(
            IContentNotificationClient notifications, 
            IContentDataClient data) 
            : base(notifications, data)
        {
        }
    }
}