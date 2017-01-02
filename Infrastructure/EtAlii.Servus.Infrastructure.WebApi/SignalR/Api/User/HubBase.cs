namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;

    public class HubBase : Hub
    {
        private readonly ISignalRAuthenticationTokenVerifier _authenticationTokenVerifier;

        public HubBase(ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        public override Task OnConnected()
        {
            var authenticationToken = this.Context.Headers.Get("Authentication-Token");
            _authenticationTokenVerifier.Verify(authenticationToken, null);

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var authenticationToken = this.Context.Headers.Get("Authentication-Token");
            _authenticationTokenVerifier.Verify(authenticationToken, null);

            return base.OnReconnected();
        }
    }
}