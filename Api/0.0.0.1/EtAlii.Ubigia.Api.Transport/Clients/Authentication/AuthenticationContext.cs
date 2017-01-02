namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class AuthenticationContext : IAuthenticationContext
    {
        public IAuthenticationDataClient Data { get { return _data; } }
        private readonly IAuthenticationDataClient _data;

        public AuthenticationContext(IAuthenticationDataClient data)
        {
            _data = data;
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await _data.Connect(spaceConnection);
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await _data.Disconnect(spaceConnection);
        }

    }

}