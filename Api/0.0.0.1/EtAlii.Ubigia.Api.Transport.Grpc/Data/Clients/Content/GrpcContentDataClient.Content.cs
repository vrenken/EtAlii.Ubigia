namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    internal partial class GrpcContentDataClient
    {
        public async Task Store(Api.Identifier identifier, Api.Content content)
        {
            var request = new ContentPostRequest {EntryId = identifier.ToWire(), Content = content.ToWire()};
            await _contentClient.PostAsync(request, _transport.AuthenticationHeaders);
            //await _invoker.Invoke(_contentConnection, GrpcHub.Content, "Post", identifier, content);

            // TODO: Should this call be replaced by get instead? 
            BlobHelper.SetStored(content, true);
        }

        public async Task Store(Api.Identifier identifier, Api.ContentPart contentPart)
        {
            var request = new ContentPartPostRequest {EntryId = identifier.ToWire(), ContentPart = contentPart.ToWire(), ContentPartId = contentPart.Id };
            await _contentClient.PostPartAsync(request, _transport.AuthenticationHeaders);
            //await _invoker.Invoke(_contentConnection, GrpcHub.Content, "PostPart", identifier, contentPart.Id, contentPart);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Api.Identifier identifier)
        {
            var request = new ContentGetRequest { EntryId = identifier.ToWire() };
            var response =  await _contentClient.GetAsync(request);
            return response.Content.ToLocal();
            //return await _invoker.Invoke<Content>(_contentConnection, GrpcHub.Content, "Get", identifier);
        }

        public async Task<IReadOnlyContentPart> Retrieve(Api.Identifier identifier, ulong contentPartId)
        {
            var request = new ContentPartGetRequest { EntryId = identifier.ToWire(), ContentPartId = contentPartId};
            var response =  await _contentClient.GetPartAsync(request);
            return response.ContentPart.ToLocal();
            //return await _invoker.Invoke<ContentPart>(_contentConnection, GrpcHub.Content, "GetPart", identifier, contentPartId);
        }
    }
}
