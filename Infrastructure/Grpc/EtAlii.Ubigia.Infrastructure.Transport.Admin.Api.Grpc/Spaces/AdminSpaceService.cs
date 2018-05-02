namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Spaces
{
	using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
	using EtAlii.Ubigia.Infrastructure.Functional;

	public class AdminSpaceService : SpaceGrpcService.SpaceGrpcServiceBase, IAdminSpaceService
    {
		private readonly ISpaceRepository _items;
		private readonly IAccountRepository _accountItems;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public AdminSpaceService(
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
