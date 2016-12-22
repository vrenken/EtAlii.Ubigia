namespace EtAlii.Servus.Api.Transport
{
    public interface IPropertyContext : ISpaceClientContext
    {
        IPropertiesNotificationClient Notifications { get; }
        IPropertiesDataClient Data { get; }
    }
}