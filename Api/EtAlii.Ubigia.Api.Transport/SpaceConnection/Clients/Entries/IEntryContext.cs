namespace EtAlii.Ubigia.Api.Transport
{
    public interface IEntryContext : ISpaceClientContext
    {
        IEntryNotificationClient Notifications { get; }
        IEntryDataClient Data { get; }
    }
}
