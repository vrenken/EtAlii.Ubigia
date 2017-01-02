namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class AuthenticationDataClientStub : IAuthenticationDataClient
    {
        public async Task Authenticate(ISpaceConnection connection)
        {
            await Task.Run(() => { });
        }

        public async Task Authenticate(IStorageConnection connection)
        {
            await Task.Run(() => { });
        }

        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            return await Task.FromResult<Storage>(null);
        }

        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            return await Task.FromResult<Storage>(null);
        }

        public async Task<Account> GetAccount(ISpaceConnection connection)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }
    }
}
