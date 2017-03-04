namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class EntryContextStub : IEntryContext
    {
        public IEntryNotificationClient Notifications => _notifications;
        private readonly IEntryNotificationClient _notifications;

        public IEntryDataClient Data => _data;
        private readonly IEntryDataClient _data;

        public EntryContextStub()
        {
            _notifications = new EntryNotificationClientStub();
            _data = new EntryDataClientStub();
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }
    }
}