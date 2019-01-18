namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class AuthenticationContextStub : IAuthenticationContext
    {
        public IAuthenticationDataClient Data { get; }

        public AuthenticationContextStub()
        {
            Data = new AuthenticationDataClientStub();
        }

        public async Task Open(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Close(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

    }
}