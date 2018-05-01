namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class UserSpaceService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.SpaceGrpcService.SpaceGrpcServiceBase, IUserSpaceService
    {
        private readonly ISpaceRepository _items;
        private readonly IAccountRepository _accountItems;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public UserSpaceService(
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
