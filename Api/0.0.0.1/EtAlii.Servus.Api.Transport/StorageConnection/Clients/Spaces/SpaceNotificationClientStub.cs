namespace EtAlii.Servus.Api.Management
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;

    public sealed class SpaceNotificationClientStub : ISpaceNotificationClient 
    {
        public async Task Connect(IStorageConnection storageConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.Run(() => { });
        }
    }
}
