namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Content
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class UserContentService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGrpcService.ContentGrpcServiceBase, IUserContentService
    {
        private readonly IContentRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public UserContentService(
            IContentRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

    }
}
