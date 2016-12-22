namespace EtAlii.Servus.Api.Transport
{
    internal class PropertyContext : SpaceClientContextBase<IPropertiesDataClient, IPropertiesNotificationClient>, IPropertyContext
    {
        public PropertyContext(
            IPropertiesNotificationClient notifications, 
            IPropertiesDataClient data) 
            : base(notifications, data)
        {
        }
    }
}