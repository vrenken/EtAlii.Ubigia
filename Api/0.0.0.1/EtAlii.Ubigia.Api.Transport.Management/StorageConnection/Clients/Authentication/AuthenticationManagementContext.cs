namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System.Threading.Tasks;

    public class AuthenticationManagementContext : IAuthenticationManagementContext
    {
        public IAuthenticationManagementDataClient Data { get; }

        public AuthenticationManagementContext(IAuthenticationManagementDataClient data)
        {
            Data = data;
        }

        public async Task Open(IStorageConnection storageConnection)
        {
            await Data.Connect(storageConnection);
        }

        public async Task Close(IStorageConnection storageConnection)
        {
            await Data.Disconnect(storageConnection);
        }

    }

}