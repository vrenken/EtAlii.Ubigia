namespace EtAlii.Ubigia.Api.Management
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public sealed class StorageNotificationClientStub : IStorageNotificationClient
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
