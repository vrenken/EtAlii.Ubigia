namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    internal partial class GrpcContentDataClient
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Post", identifier, contentDefinition);

            MarkAsStored(contentDefinition);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            await _invoker.Invoke(_contentDefinitionConnection, GrpcHub.ContentDefinition, "PostPart", identifier, contentDefinitionPart.Id, contentDefinitionPart);

            MarkAsStored(contentDefinitionPart);
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return await _invoker.Invoke<ContentDefinition>(_contentDefinitionConnection, GrpcHub.ContentDefinition, "Get", identifier);
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
