namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Accounts
{
	using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
	using EtAlii.Ubigia.Infrastructure.Functional;

	public class AccountService : AccountGrpcService.AccountGrpcServiceBase
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
