namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class AuthenticationManagementContextStub : IAuthenticationManagementContext
    {
        public IAuthenticationManagementDataClient Data { get; }

        public AuthenticationManagementContextStub()
        {
            Data = new AuthenticationManagementDataClientStub();
        }

        public async Task Open(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Close(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }

    }
}