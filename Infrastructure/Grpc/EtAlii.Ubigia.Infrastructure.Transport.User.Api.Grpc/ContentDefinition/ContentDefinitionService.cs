namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using ContentDefinition = EtAlii.Ubigia.Api.ContentDefinition;

    using global::Grpc.Core;

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
        
        
        public override Task<ContentDefinitionGetResponse> Get(ContentDefinitionGetRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentDefinition = (ContentDefinition)_items.Get(entryId);

            var response = new ContentDefinitionGetResponse
            {
                ContentDefinition = contentDefinition.ToWire()
            };
            return Task.FromResult(response);
        }

        public override Task<ContentDefinitionPostResponse> Post(ContentDefinitionPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentDefinition = request.ContentDefinition.ToLocal();
            
            _items.Store(entryId, contentDefinition);

            var response = new ContentDefinitionPostResponse();
            return Task.FromResult(response);
        }

        public override Task<ContentDefinitionPartPostResponse> PostPart(ContentDefinitionPartPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentDefinitionPart = request.ContentDefinitionPart.ToLocal();
            var contentDefinitionPartId = request.ContentDefinitionPartId;
            
                if (contentDefinitionPartId != contentDefinitionPart.Id)
                {
                    throw new InvalidOperationException("ContentDefinitionPartId does not match");
                }

            _items.Store(entryId, contentDefinitionPart);

            var response = new ContentDefinitionPartPostResponse();
            return Task.FromResult(response);
        }
    }
}
