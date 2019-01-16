namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class RootContextStub : IRootContext
    {
        public IRootNotificationClient Notifications { get; }

        public IRootDataClient Data { get; }

        public RootContextStub()
        {
            Notifications = new RootNotificationClientStub();
            Data = new RootDataClientStub();
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