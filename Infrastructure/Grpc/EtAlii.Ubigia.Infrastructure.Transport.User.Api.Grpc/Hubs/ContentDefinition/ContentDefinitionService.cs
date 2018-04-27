namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class ContentDefinitionService : EtAlii.Ubigia.Api.Transport.Grpc.ContentDefinitionGrpcService.ContentDefinitionGrpcServiceBase
    {
        private readonly IContentDefinitionRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public ContentDefinitionService(
            IContentDefinitionRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }
    }
}
