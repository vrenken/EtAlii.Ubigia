namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;
    using Content = EtAlii.Ubigia.Api.Content;
    using ContentPart = EtAlii.Ubigia.Api.ContentPart;

    public class UserContentService : EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGrpcService.ContentGrpcServiceBase, IUserContentService
    {
        private readonly IContentRepository _items;

        public UserContentService(IContentRepository items)
        {
            _items = items;
        }

        public override Task<ContentGetResponse> Get(ContentGetRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var content = (Content)_items.Get(entryId);

            var response = new ContentGetResponse
            {
                Content = content.ToWire()
            };
            return Task.FromResult(response);
        }

        public override Task<ContentPartGetResponse> GetPart(ContentPartGetRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentPartId = request.ContentPartId;

            var contentPart = (ContentPart)_items.Get(entryId, contentPartId);
            var response = new ContentPartGetResponse
            {
                ContentPart = contentPart.ToWire()
            };
            return Task.FromResult(response);
        }

        public override Task<ContentPostResponse> Post(ContentPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var content = request.Content.ToLocal();

            _items.Store(entryId, content);
            
            var response = new ContentPostResponse();
            return Task.FromResult(response);
        }

        public override Task<ContentPartPostResponse> PostPart(ContentPartPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var contentPartId = request.ContentPartId;
            var contentPart = request.ContentPart.ToLocal();

            if (contentPartId != contentPart.Id)
            {
                throw new InvalidOperationException("ContentPartId does not match");
            }
        
            _items.Store(entryId, contentPart );
            
            var response = new ContentPartPostResponse();
            return Task.FromResult(response);
        }
    }
}
