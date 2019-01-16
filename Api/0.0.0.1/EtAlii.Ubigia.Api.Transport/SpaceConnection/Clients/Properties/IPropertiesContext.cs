namespace EtAlii.Ubigia.Api.Transport
{
    public interface IPropertiesContext : ISpaceClientContext
    {
        IPropertiesNotificationClient Notifications { get; }
        IPropertiesDataClient Data { get; }
    }
}