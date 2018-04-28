namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;

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
