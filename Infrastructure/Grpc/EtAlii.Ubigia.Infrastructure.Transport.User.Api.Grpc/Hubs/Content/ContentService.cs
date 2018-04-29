namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class ContentService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGrpcService.ContentGrpcServiceBase
    {
        private readonly IContentRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public ContentService(
            IContentRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

    }
}
