namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.Extensions.Primitives;

	public class AccountService : EtAlii.Ubigia.Api.Transport.Management.Grpc.AccountGrpcService.AccountGrpcServiceBase
    {
		private readonly IAccountRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public AccountService(
			IAccountRepository items,
			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IAuthenticationTokenConverter authenticationTokenConverter)
		{
			_items = items;
		    _authenticationTokenVerifier = authenticationTokenVerifier;
		    _authenticationTokenConverter = authenticationTokenConverter;
		}
    }
}
