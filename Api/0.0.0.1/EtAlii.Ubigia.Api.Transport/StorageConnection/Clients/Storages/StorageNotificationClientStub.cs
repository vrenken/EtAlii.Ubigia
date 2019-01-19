namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public sealed class StorageNotificationClientStub : IStorageNotificationClient
    {
        public async Task Connect(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }
    }
}
