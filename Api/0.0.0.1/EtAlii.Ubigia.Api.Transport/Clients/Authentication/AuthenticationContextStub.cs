namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class AuthenticationContextStub : IAuthenticationContext
    {
        public IAuthenticationDataClient Data { get { return _data; } }
        private readonly IAuthenticationDataClient _data;

        public AuthenticationContextStub()
        {
            _data = new AuthenticationDataClientStub();
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

    }
}