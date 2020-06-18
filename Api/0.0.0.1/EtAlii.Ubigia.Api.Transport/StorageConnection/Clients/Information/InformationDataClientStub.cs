namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public sealed class InformationDataClientStub : IInformationDataClient 
    {
        public Task Connect(IStorageConnection connection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection connection)
        {
            return Task.CompletedTask;
        }

        public Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            return Task.FromResult<Storage>(null);
        }

        public Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
        {
            return Task.FromResult<ConnectivityDetails>(null);
        }
    }
}
