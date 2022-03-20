// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
	using System;
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.AspNetCore.SignalR;

	public class AccountHub : HubBase
    {
        private readonly IAccountRepository _items;
		private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public AccountHub(
            IAccountRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IAuthenticationTokenConverter authenticationTokenConverter)
            : base(authenticationTokenVerifier)
        {
            _items = items;
			_authenticationTokenConverter = authenticationTokenConverter;
		}

		public Account GetForAuthenticationToken()
		{
			Account response;
			try
			{
				var httpContext = Context.GetHttpContext();
				httpContext!.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
				var authenticationTokenAsString = stringValues.Single();
				var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

				response = _items.Get(authenticationToken.Name);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException("Unable to serve a Account GET client request", e);
			}
			return response;
		}
    }
}
