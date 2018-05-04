namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class UserContentDefinitionService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentDefinitionGrpcService.ContentDefinitionGrpcServiceBase, IUserContentDefinitionService
    {
        private readonly IContentDefinitionRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public UserContentDefinitionService(
            IContentDefinitionRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }
    }
}
