namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class ContentContextStub : IContentContext
    {
        public IContentNotificationClient Notifications { get; }

        public IContentDataClient Data { get; }

        public ContentContextStub()
        {
            Notifications = new ContentNotificationClientStub();
            Data = new ContentDataClientStub();
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