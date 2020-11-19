namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;
    using ContentDefinition = EtAlii.Ubigia.ContentDefinition;

    public class UserContentDefinitionService : ContentDefinitionGrpcService.ContentDefinitionGrpcServiceBase, IUserContentDefinitionService
    {
        private readonly IContentDefinitionRepository _items;

        public UserContentDefinitionService(IContentDefinitionRepository items)
        {
            _items = items;
        }
        
        
        public override async Task<ContentDefinitionGetResponse> Get(ContentDefinitionGetRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentDefinition = (ContentDefinition)await _items
                .Get(entryId)
                .ConfigureAwait(false);

            var response = new ContentDefinitionGetResponse
            {
                ContentDefinition = contentDefinition.ToWire()
            };
            return response;
        }

        public override Task<ContentDefinitionPostResponse> Post(ContentDefinitionPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentDefinition = request.ContentDefinition.ToLocal();
            
            _items.Store(entryId, contentDefinition);

            var response = new ContentDefinitionPostResponse();
            return Task.FromResult(response);
        }

        public override async Task<ContentDefinitionPartPostResponse> PostPart(ContentDefinitionPartPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentDefinitionPart = request.ContentDefinitionPart.ToLocal();
            var contentDefinitionPartId = request.ContentDefinitionPartId;
            
                if (contentDefinitionPartId != contentDefinitionPart.Id)
                {
                    throw new InvalidOperationException("ContentDefinitionPartId does not match");
                }

            await _items
                .Store(entryId, contentDefinitionPart)
                .ConfigureAwait(false);

            var response = new ContentDefinitionPartPostResponse();
            return response;
        }
    }
}
