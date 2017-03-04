namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class ContentContextStub : IContentContext
    {
        public IContentNotificationClient Notifications => _notifications;
        private readonly IContentNotificationClient _notifications;

        public IContentDataClient Data => _data;
        private readonly IContentDataClient _data;

        public ContentContextStub()
        {
            _notifications = new ContentNotificationClientStub();
            _data = new ContentDataClientStub();
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