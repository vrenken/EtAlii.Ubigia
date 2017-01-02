namespace EtAlii.Ubigia.Api.Transport
{
    public class EntryContext : SpaceClientContextBase<IEntryDataClient, IEntryNotificationClient>, IEntryContext
    {
        public EntryContext(
            IEntryNotificationClient notifications, 
            IEntryDataClient data) 
            : base(notifications, data)
        {
        }
    }
}
