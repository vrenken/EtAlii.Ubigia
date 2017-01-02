namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IAuthenticationDataClient : ISpaceTransportClient
    {
        Task Authenticate(ISpaceConnection connection);
        Task Authenticate(IStorageConnection connection);

        Task<Storage> GetConnectedStorage(IStorageConnection connection);
        Task<Storage> GetConnectedStorage(ISpaceConnection connection);

        Task<Account> GetAccount(ISpaceConnection connection);
        Task<Space> GetSpace(ISpaceConnection connection);
    }

    public interface IAuthenticationDataClient<in TTransport> : IAuthenticationDataClient, ISpaceTransportClient<TTransport>
        where TTransport : ISpaceTransport
    {
    }
}
