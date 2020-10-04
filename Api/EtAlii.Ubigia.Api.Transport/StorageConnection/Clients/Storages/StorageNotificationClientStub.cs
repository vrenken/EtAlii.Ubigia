namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stub for the <see cref="IStorageNotificationClient"/>.
    /// </summary>
    public sealed class StorageNotificationClientStub : IStorageNotificationClient
    {
        /// <inheritdoc />
        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
