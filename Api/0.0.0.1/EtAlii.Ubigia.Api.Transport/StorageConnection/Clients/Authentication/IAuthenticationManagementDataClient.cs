namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IAuthenticationManagementDataClient : IStorageTransportClient
    {
        Task Authenticate(IStorageConnection storageConnection, string accountName, string password);

        Task<Storage> GetConnectedStorage(IStorageConnection storageConnection);

    }

    public interface IAuthenticationManagementDataClient<in TTransport> : IAuthenticationManagementDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
