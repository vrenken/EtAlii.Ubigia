namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class AuthenticationManagementDataClientStub : IAuthenticationManagementDataClient
    {
        public Task Authenticate(IStorageConnection connection, string accountName, string password)
        {
            return Task.CompletedTask;
        }

        public Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            return Task.FromResult<Storage>(null);
        }
        
        public Task Connect(IStorageConnection connection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection connection)
        {
            return Task.CompletedTask;
        }
    }
}
