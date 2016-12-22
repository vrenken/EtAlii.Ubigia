namespace EtAlii.Servus.Api.Transport
{
    public class RootContext : SpaceClientContextBase<IRootDataClient, IRootNotificationClient>, IRootContext
    {
        public RootContext(
            IRootNotificationClient notifications, 
            IRootDataClient data) 
            : base(notifications, data)
        {
        }
    }
}