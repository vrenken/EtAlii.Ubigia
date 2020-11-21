namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class AuthenticationContext : IAuthenticationContext
    {
        public IAuthenticationDataClient Data { get; }

        public AuthenticationContext(IAuthenticationDataClient data)
        {
            Data = data;
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Data.Connect(spaceConnection).ConfigureAwait(false);
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Data.Disconnect().ConfigureAwait(false);
        }

    }

}