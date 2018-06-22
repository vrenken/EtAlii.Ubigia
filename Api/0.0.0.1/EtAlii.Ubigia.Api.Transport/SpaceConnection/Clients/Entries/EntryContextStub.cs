namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class EntryContextStub : IEntryContext
    {
        public IEntryNotificationClient Notifications { get; }

        public IEntryDataClient Data { get; }

        public EntryContextStub()
        {
            Notifications = new EntryNotificationClientStub();
            Data = new EntryDataClientStub();
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