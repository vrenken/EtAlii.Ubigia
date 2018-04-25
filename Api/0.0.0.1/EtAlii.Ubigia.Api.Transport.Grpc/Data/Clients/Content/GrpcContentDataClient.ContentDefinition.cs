namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    internal partial class GrpcContentDataClient
    {
        public async Task StoreDefinition(Api.Identifier identifier, Api.ContentDefinition contentDefinition)
        {
            // TODO: GRPC
            //await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Post", identifier, contentDefinition);

            MarkAsStored(contentDefinition);
        }

        public async Task StoreDefinition(Api.Identifier identifier, Api.ContentDefinitionPart contentDefinitionPart)
        {
            // TODO: GRPC
            //await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "PostPart", identifier, contentDefinitionPart.Id, contentDefinitionPart);

            MarkAsStored(contentDefinitionPart);
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Api.Identifier identifier)
        {
            // TODO: GRPC
            return await Task.FromResult<IReadOnlyContentDefinition>(null);
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
