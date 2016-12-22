namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public class SpaceClientContextBase<TDataClient, TNotificationClient> : ISpaceClientContext
        where TDataClient: ISpaceTransportClient
        where TNotificationClient: ISpaceTransportClient
    {
        public TNotificationClient Notifications { get { return _notifications; } }
        private readonly TNotificationClient _notifications;

        public TDataClient Data { get { return _data; } }
        private readonly TDataClient _data;

        public SpaceClientContextBase(
            TNotificationClient notifications,
            TDataClient data)
        {
            _notifications = notifications;
            _data = data;
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await _data.Connect(spaceConnection);
            await _notifications.Connect(spaceConnection);
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await _notifications.Disconnect(spaceConnection);
            await _data.Disconnect(spaceConnection);
        }
    }
}