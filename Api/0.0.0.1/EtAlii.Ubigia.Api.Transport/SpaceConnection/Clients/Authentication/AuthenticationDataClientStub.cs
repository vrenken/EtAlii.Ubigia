namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class AuthenticationDataClientStub : IAuthenticationDataClient
    {
        public async Task Authenticate(ISpaceConnection connection, string accountName, string password)
        {
            await Task.CompletedTask;
        }
        

        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            return await Task.FromResult<Storage>(null);
        }
        
        public async Task<Account> GetAccount(ISpaceConnection connection, string accountName)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }
    }
}
