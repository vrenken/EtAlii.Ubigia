namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class PropertyContextStub : IPropertyContext
    {
        public IPropertiesNotificationClient Notifications { get; }

        public IPropertiesDataClient Data { get; }

        public PropertyContextStub()
        {
            Notifications = new PropertiesNotificationClientStub();
            Data = new PropertiesDataClientStub();
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