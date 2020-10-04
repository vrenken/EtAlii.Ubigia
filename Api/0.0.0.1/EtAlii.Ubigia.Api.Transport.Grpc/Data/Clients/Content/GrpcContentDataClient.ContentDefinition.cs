namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using ContentDefinition = EtAlii.Ubigia.ContentDefinition;
    using ContentDefinitionPart = EtAlii.Ubigia.ContentDefinitionPart;
    using Identifier = EtAlii.Ubigia.Identifier;

    internal partial class GrpcContentDataClient
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            try
            {
                var request = new ContentDefinitionPostRequest {EntryId = identifier.ToWire(), ContentDefinition = contentDefinition.ToWire()};
                await _contentDefinitionClient.PostAsync(request, _transport.AuthenticationHeaders);
                //await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Post", identifier, contentDefinition)
    
                MarkAsStored(contentDefinition);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            try
            {
                var request = new ContentDefinitionPartPostRequest {EntryId = identifier.ToWire(), ContentDefinitionPart = contentDefinitionPart.ToWire(), ContentDefinitionPartId = contentDefinitionPart.Id };
                await _contentDefinitionClient.PostPartAsync(request, _transport.AuthenticationHeaders);
                //await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "PostPart", identifier, contentDefinitionPart.Id, contentDefinitionPart)
    
                MarkAsStored(contentDefinitionPart);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            try
            {
                var request = new ContentDefinitionGetRequest { EntryId = identifier.ToWire() };
                var response = await _contentDefinitionClient.GetAsync(request, _transport.AuthenticationHeaders);
                return response.ContentDefinition.ToLocal();
                //return await _invoker.Invoke<ContentDefinition>(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Get", identifier)
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.RetrieveDefinition()", e);
            }
        }

        private void MarkAsStored(ContentDefinition contentDefinition)
        {
            BlobHelper.SetStored(contentDefinition, true);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                MarkAsStored(contentDefinitionPart);
            }
        }

        private void MarkAsStored(ContentDefinitionPart contentDefinitionPart)
        {
            BlobPartHelper.SetStored(contentDefinitionPart, true);
        }
    }
}
