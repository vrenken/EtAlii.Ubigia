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

        public async Task Open(IStorageConnection connection)
        {
            await Data.Connect(connection);
        }

        public async Task Close(IStorageConnection connection)
        {
            await Data.Disconnect(connection);
        }

    }

}