namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    internal partial class GrpcContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            await _invoker.Invoke(_contentConnection, GrpcHub.Content, "Post", identifier, content);

            // TODO: Should this call be replaced by get instead? 
            BlobHelper.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await _invoker.Invoke(_contentConnection, GrpcHub.Content, "PostPart", identifier, contentPart.Id, contentPart);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            return await _invoker.Invoke<Content>(_contentConnection, GrpcHub.Content, "Get", identifier);
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return await _invoker.Invoke<ContentPart>(_contentConnection, GrpcHub.Content, "GetPart", identifier, contentPartId);
        }
    }
}
