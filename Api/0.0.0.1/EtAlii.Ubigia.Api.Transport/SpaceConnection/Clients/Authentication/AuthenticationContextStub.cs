namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class AuthenticationContextStub : IAuthenticationContext
    {
        /// <inheritdoc />
        public IAuthenticationDataClient Data { get; }

        /// <summary>
        /// Create a new <see cref="AuthenticationContextStub"/> instance.
        /// </summary>
        public AuthenticationContextStub()
        {
            Data = new AuthenticationDataClientStub();
        }

        /// <inheritdoc />
        public Task Open(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Close(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

    }
}