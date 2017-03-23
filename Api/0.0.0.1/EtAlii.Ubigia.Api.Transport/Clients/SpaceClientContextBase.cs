namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class SpaceClientContextBase<TDataClient, TNotificationClient> : ISpaceClientContext
        where TDataClient: ISpaceTransportClient
        where TNotificationClient: ISpaceTransportClient
    {
        public TNotificationClient Notifications { get; }

        public TDataClient Data { get; }

        public SpaceClientContextBase(
            TNotificationClient notifications,
            TDataClient data)
        {
            Notifications = notifications;
            Data = data;
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Data.Connect(spaceConnection);
            await Notifications.Connect(spaceConnection);
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Notifications.Disconnect(spaceConnection);
            await Data.Disconnect(spaceConnection);
        }
    }
}