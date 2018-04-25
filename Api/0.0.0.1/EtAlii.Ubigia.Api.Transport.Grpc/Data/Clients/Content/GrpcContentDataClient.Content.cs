namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    internal partial class GrpcContentDataClient
    {
        public async Task Store(Api.Identifier identifier, Api.Content content)
        {
            // TODO: GRPC
            //await _invoker.Invoke(_contentConnection, GrpcHub.Content, "Post", identifier, content);

            // TODO: Should this call be replaced by get instead? 
            BlobHelper.SetStored(content, true);
        }

        public async Task Store(Api.Identifier identifier, Api.ContentPart contentPart)
        {
            // TODO: GRPC
            //await _invoker.Invoke(_contentConnection, GrpcHub.Content, "PostPart", identifier, contentPart.Id, contentPart);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Api.Identifier identifier)
        {
            // TODO: GRPC
            return await Task.FromResult<IReadOnlyContent>(null);
            //return await _invoker.Invoke<Content>(_contentConnection, GrpcHub.Content, "Get", identifier);
        }

        public async Task<IReadOnlyContentPart> Retrieve(Api.Identifier identifier, ulong contentPartId)
        {
            // TODO: GRPC
            return await Task.FromResult<IReadOnlyContentPart>(null);
            //return await _invoker.Invoke<ContentPart>(_contentConnection, GrpcHub.Content, "GetPart", identifier, contentPartId);
        }
    }
}
