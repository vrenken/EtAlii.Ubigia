namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;

    public class HubBase : Hub
    {
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public HubBase(ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
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