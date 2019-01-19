namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class PropertiesContextStub : IPropertiesContext
    {
        public IPropertiesNotificationClient Notifications { get; }

        public IPropertiesDataClient Data { get; }

        public PropertiesContextStub()
        {
            Notifications = new PropertiesNotificationClientStub();
            Data = new PropertiesDataClientStub();
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

    }
}