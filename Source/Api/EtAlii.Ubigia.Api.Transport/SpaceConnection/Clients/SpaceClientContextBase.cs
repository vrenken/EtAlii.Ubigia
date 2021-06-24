// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class SpaceClientContextBase<TDataClient, TNotificationClient> : ISpaceClientContext
        where TDataClient: ISpaceTransportClient
        where TNotificationClient: ISpaceTransportClient
    {
        public TNotificationClient Notifications { get; }

        public TDataClient Data { get; }

        protected SpaceClientContextBase(
            TNotificationClient notifications,
            TDataClient data)
        {
            Notifications = notifications;
            Data = data;
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Data.Connect(spaceConnection).ConfigureAwait(false);
            await Notifications.Connect(spaceConnection).ConfigureAwait(false);
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Notifications.Disconnect().ConfigureAwait(false);
            await Data.Disconnect().ConfigureAwait(false);
        }
    }
}
