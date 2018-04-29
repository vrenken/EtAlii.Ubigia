namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;

	public class AccountService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountGrpcService.AccountGrpcServiceBase
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
