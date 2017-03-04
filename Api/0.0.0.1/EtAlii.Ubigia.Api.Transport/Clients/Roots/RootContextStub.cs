namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class RootContextStub : IRootContext
    {
        public IRootNotificationClient Notifications => _notifications;
        private readonly IRootNotificationClient _notifications;

        public IRootDataClient Data => _data;
        private readonly IRootDataClient _data;

        public RootContextStub()
        {
            _notifications = new RootNotificationClientStub();
            _data = new RootDataClientStub();
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