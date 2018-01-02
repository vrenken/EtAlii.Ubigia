namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class HubBase : Hub
    {
        private readonly ISignalRAuthenticationTokenVerifier _authenticationTokenVerifier;

        public HubBase(ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        public override Task OnConnected()
        {
            var authenticationToken = Context.Headers.Get("Authentication-Token");
            _authenticationTokenVerifier.Verify(authenticationToken, null);

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var authenticationToken = Context.Headers.Get("Authentication-Token");
            _authenticationTokenVerifier.Verify(authenticationToken, null);

            return base.OnReconnected();
        }
    }
}