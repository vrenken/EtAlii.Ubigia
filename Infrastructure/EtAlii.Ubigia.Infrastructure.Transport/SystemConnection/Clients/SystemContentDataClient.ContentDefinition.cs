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

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            //await _client.Post(address, contentDefinition);

            //MarkAsStored(contentDefinition);
            return Task.CompletedTask;
        }

        public Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            _infrastructure.ContentDefinition.Store(identifier, contentDefinitionPart); 
            MarkAsStored(contentDefinitionPart);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString());
            //await _client.Post(address, contentDefinitionPart);

            //MarkAsStored(contentDefinitionPart);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            var result = _infrastructure.ContentDefinition.Get(identifier);
            return Task.FromResult(result);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            //var contentDefinition = await _client.Get<ContentDefinition>(address);
            //return contentDefinition;
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
