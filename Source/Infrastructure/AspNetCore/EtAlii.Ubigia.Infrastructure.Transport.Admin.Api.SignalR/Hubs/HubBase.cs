namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class HubBase : Hub
    {
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        protected HubBase(ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        public override Task OnConnectedAsync()
        {
            Context.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
            var authenticationToken = stringValues.Single();
            _authenticationTokenVerifier.Verify(authenticationToken, Role.Admin, Role.System);

            return base.OnConnectedAsync();
        }
    }
}
