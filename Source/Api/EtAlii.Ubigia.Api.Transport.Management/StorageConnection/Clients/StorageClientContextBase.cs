// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
            await Data.Connect(storageConnection).ConfigureAwait(false);
            await Notifications.Connect(storageConnection).ConfigureAwait(false);

            Connection = storageConnection;
        }

        public async Task Close(IStorageConnection storageConnection)
        {
            await Notifications.Disconnect(storageConnection).ConfigureAwait(false);
            await Data.Disconnect(storageConnection).ConfigureAwait(false);

            Connection = null;
        }
    }
}