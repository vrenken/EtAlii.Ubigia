namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stub for the <see cref="IInformationDataClient"/>.
    /// </summary>
    public sealed class InformationDataClientStub : IInformationDataClient 
    {
        /// <inheritdoc />
        public Task Connect(IStorageConnection connection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect(IStorageConnection connection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            return Task.FromResult<Storage>(null);
        }

        /// <inheritdoc />
        public Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
        {
            return Task.FromResult<ConnectivityDetails>(null);
        }
    }
}
