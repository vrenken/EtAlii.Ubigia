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

        public Task Open(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Close(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

    }
}