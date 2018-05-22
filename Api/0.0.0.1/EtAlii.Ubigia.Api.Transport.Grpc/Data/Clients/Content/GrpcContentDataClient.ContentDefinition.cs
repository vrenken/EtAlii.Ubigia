namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    internal partial class GrpcContentDataClient
    {
        public async Task StoreDefinition(Api.Identifier identifier, Api.ContentDefinition contentDefinition)
        {
            var request = new ContentDefinitionPostRequest {EntryId = identifier.ToWire(), ContentDefinition = contentDefinition.ToWire()};
            await _contentDefinitionClient.PostAsync(request, _connection.Transport.AuthenticationHeaders);
            //await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Post", identifier, contentDefinition);

            MarkAsStored(contentDefinition);
        }

        public async Task StoreDefinition(Api.Identifier identifier, Api.ContentDefinitionPart contentDefinitionPart)
        {
            var request = new ContentDefinitionPartPostRequest {EntryId = identifier.ToWire(), ContentDefinitionPart = contentDefinitionPart.ToWire(), ContentDefinitionPartId = contentDefinitionPart.Id };
            await _contentDefinitionClient.PostPartAsync(request, _connection.Transport.AuthenticationHeaders);
            //await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "PostPart", identifier, contentDefinitionPart.Id, contentDefinitionPart);

            MarkAsStored(contentDefinitionPart);
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Api.Identifier identifier)
        {
            var request = new ContentDefinitionGetRequest { EntryId = identifier.ToWire() };
            var response =  await _contentDefinitionClient.GetAsync(request);
            return response.ContentDefinition.ToLocal();
            //return await _invoker.Invoke<ContentDefinition>(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Get", identifier);
        }

        private void MarkAsStored(Api.ContentDefinition contentDefinition)
        {
            BlobHelper.SetStored(contentDefinition, true);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                MarkAsStored(contentDefinitionPart);
            }
        }

        private void MarkAsStored(Api.ContentDefinitionPart contentDefinitionPart)
        {
            BlobPartHelper.SetStored(contentDefinitionPart, true);
        }
    }
}
