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

        public Task Open(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Close(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }
    }
}