namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class PropertyContextStub : IPropertyContext
    {
        public IPropertiesNotificationClient Notifications { get { return _notifications; } }
        private readonly IPropertiesNotificationClient _notifications;

        public IPropertiesDataClient Data { get { return _data; } }
        private readonly IPropertiesDataClient _data;

        public PropertyContextStub()
        {
            _notifications = new PropertiesNotificationClientStub();
            _data = new PropertiesDataClientStub();
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