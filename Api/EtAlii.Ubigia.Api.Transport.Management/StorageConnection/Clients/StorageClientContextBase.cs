namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System.Threading.Tasks;

    public class StorageClientContextBase<TDataClient, TNotificationClient> : IStorageClientContext
        where TDataClient: IStorageTransportClient
        where TNotificationClient: IStorageTransportClient
    {
        public TNotificationClient Notifications { get; }

        public TDataClient Data { get; }

        protected IStorageConnection Connection { get; private set; }

        public StorageClientContextBase(
            TNotificationClient notifications,
            TDataClient data)
        {
            Notifications = notifications;
            Data = data;
        }

        public async Task Open(IStorageConnection storageConnection)
        {
            await Data.Connect(storageConnection);
            await Notifications.Connect(storageConnection);

            Connection = storageConnection;
        }

        public async Task Close(IStorageConnection storageConnection)
        {
            await Notifications.Disconnect(storageConnection);
            await Data.Disconnect(storageConnection);

            Connection = null;
        }
    }
}