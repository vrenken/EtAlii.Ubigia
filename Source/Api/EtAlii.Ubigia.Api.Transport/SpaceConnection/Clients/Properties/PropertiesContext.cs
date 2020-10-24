namespace EtAlii.Ubigia.Api.Transport
{
    internal class PropertiesContext : SpaceClientContextBase<IPropertiesDataClient, IPropertiesNotificationClient>, IPropertiesContext
    {
        public PropertiesContext(
            IPropertiesNotificationClient notifications, 
            IPropertiesDataClient data) 
            : base(notifications, data)
        {
        }
    }
}