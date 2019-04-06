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