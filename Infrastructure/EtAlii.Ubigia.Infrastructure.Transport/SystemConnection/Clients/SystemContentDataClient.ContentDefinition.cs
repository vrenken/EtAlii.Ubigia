namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    internal partial class SystemContentDataClient
    {
        public Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            _infrastructure.ContentDefinition.Store(identifier, contentDefinition); 
            MarkAsStored(contentDefinition);

            return Task.CompletedTask;
        }

        public Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            _infrastructure.ContentDefinition.Store(identifier, contentDefinitionPart); 
            MarkAsStored(contentDefinitionPart);

            return Task.CompletedTask;
        }

        public Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            var result = _infrastructure.ContentDefinition.Get(identifier);
            return Task.FromResult(result);
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
