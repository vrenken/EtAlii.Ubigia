namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public sealed class StorageNotificationClientStub : IStorageNotificationClient
    {
        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
