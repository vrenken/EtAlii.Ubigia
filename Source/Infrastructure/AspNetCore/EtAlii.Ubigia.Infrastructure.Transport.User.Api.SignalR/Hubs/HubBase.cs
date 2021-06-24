// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
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
            _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System);

            return base.OnConnectedAsync();
        }

        //public override Task OnReconnectedAsync()
        //[
        //    var authenticationToken = Context.Headers.Get("Authentication-Token")
        //    _authenticationTokenVerifier.Verify(authenticationToken, null)

        //    return base.OnReconnected()
        //]
    }
}