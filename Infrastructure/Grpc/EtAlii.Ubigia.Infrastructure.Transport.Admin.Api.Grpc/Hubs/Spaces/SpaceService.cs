namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
	using EtAlii.Ubigia.Infrastructure.Functional;

	public class SpaceService : EtAlii.Ubigia.Api.Transport.Management.Grpc.SpaceGrpcService.SpaceGrpcServiceBase
    {
		private readonly ISpaceRepository _items;
		private readonly IAccountRepository _accountItems;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public SpaceService(
			ISpaceRepository items,
			IAccountRepository accountItems,
			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IAuthenticationTokenConverter authenticationTokenConverter)
		{
			_items = items;
			_accountItems = accountItems;
		    _authenticationTokenVerifier = authenticationTokenVerifier;
		    _authenticationTokenConverter = authenticationTokenConverter;
		}
    }
}
