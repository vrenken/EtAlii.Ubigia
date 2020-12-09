namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using Content = EtAlii.Ubigia.Content;
    using ContentPart = EtAlii.Ubigia.ContentPart;
    using Identifier = EtAlii.Ubigia.Identifier;

    internal partial class GrpcContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            try
            {
                var request = new ContentPostRequest {EntryId = identifier.ToWire(), Content = content.ToWire()};
                await _contentClient.PostAsync(request, _transport.AuthenticationHeaders);
                //await _invoker.Invoke(_contentConnection, GrpcHub.Content, "Post", identifier, content)
    
                // TODO: Should this call be replaced by get instead? 
                Blob.SetStored(content, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }

        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            try
            {
                var request = new ContentPartPostRequest {EntryId = identifier.ToWire(), ContentPart = contentPart.ToWire(), ContentPartId = contentPart.Id };
                await _contentClient.PostPartAsync(request, _transport.AuthenticationHeaders);
                //await _invoker.Invoke(_contentConnection, GrpcHub.Content, "PostPart", identifier, contentPart.Id, contentPart)
    
                BlobPart.SetStored(contentPart, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<Content> Retrieve(Identifier identifier)
        {
            try
            {
                var request = new ContentGetRequest { EntryId = identifier.ToWire() };
                var response = await _contentClient.GetAsync(request, _transport.AuthenticationHeaders);
                return response.Content.ToLocal();
                //return await _invoker.Invoke<Content>(_contentConnection, GrpcHub.Content, "Get", identifier)
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            try
            {
                var request = new ContentPartGetRequest { EntryId = identifier.ToWire(), ContentPartId = contentPartId};
                var response = await _contentClient.GetPartAsync(request, _transport.AuthenticationHeaders);
                return response.ContentPart.ToLocal();
                //return await _invoker.Invoke<ContentPart>(_contentConnection, GrpcHub.Content, "GetPart", identifier, contentPartId)
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }
    }
}
