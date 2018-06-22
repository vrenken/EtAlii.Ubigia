namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class AuthenticationManagementDataClientStub : IAuthenticationManagementDataClient
    {
        public async Task Authenticate(IStorageConnection connection)
        {
            await Task.Run(() => { });
        }

        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            return await Task.FromResult<Storage>(null);
        }
        
        public async Task Connect(IStorageConnection connection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(IStorageConnection connection)
        {
            await Task.Run(() => { });
        }
    }
}
