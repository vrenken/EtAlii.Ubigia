namespace EtAlii.Ubigia.Api.Management
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public class StorageClientContextBase<TDataClient, TNotificationClient> : IStorageClientContext
        where TDataClient: IStorageTransportClient
        where TNotificationClient: IStorageTransportClient
    {
        public TNotificationClient Notifications { get { return _notifications; } }
        private readonly TNotificationClient _notifications;

        public TDataClient Data { get { return _data; } }
        private readonly TDataClient _data;

        protected IStorageConnection Connection { get { return _connection; } }
        private IStorageConnection _connection;

        public StorageClientContextBase(
            TNotificationClient notifications,
            TDataClient data)
        {
            _notifications = notifications;
            _data = data;
        }

        public async Task Open(IStorageConnection storageConnection)
        {
            await _data.Connect(storageConnection);
            await _notifications.Connect(storageConnection);

            _connection = storageConnection;
        }

        public async Task Close(IStorageConnection spaceConnection)
        {
            await _notifications.Disconnect(spaceConnection);
            await _data.Disconnect(spaceConnection);

            _connection = null;
        }
    }
}