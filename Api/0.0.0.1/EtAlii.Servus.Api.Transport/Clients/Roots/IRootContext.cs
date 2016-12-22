namespace EtAlii.Servus.Api.Transport
{
    public interface IRootContext : ISpaceClientContext
    {
        IRootNotificationClient Notifications { get; }
        IRootDataClient Data { get; }
    }
}