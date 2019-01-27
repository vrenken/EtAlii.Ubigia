namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    internal partial class GrpcContentDataClient
    {
        public async Task Store(Api.Identifier identifier, Api.Content content)
        {
            try
            {
                var request = new ContentPostRequest {EntryId = identifier.ToWire(), Content = content.ToWire()};
                await _contentClient.PostAsync(request, _transport.AuthenticationHeaders);
                
                // TODO: Should this call be replaced by get instead? 
                BlobHelper.SetStored(content, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }

        }

        public async Task Store(Api.Identifier identifier, Api.ContentPart contentPart)
        {
            try
            {
                var request = new ContentPartPostRequest {EntryId = identifier.ToWire(), ContentPart = contentPart.ToWire(), ContentPartId = contentPart.Id };
                await _contentClient.PostPartAsync(request, _transport.AuthenticationHeaders);
                
                BlobPartHelper.SetStored(contentPart, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<IReadOnlyContent> Retrieve(Api.Identifier identifier)
        {
            try
            {
                var request = new ContentGetRequest { EntryId = identifier.ToWire() };
                var response = await _contentClient.GetAsync(request, _transport.AuthenticationHeaders);
                return response.Content.ToLocal();
                //return await _invoker.Invoke<Content>(_contentConnection, GrpcHub.Content, "Get", identifier);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<IReadOnlyContentPart> Retrieve(Api.Identifier identifier, ulong contentPartId)
        {
            try
            {
                var request = new ContentPartGetRequest { EntryId = identifier.ToWire(), ContentPartId = contentPartId};
                var response = await _contentClient.GetPartAsync(request, _transport.AuthenticationHeaders);
                return response.ContentPart.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }
    }
}
