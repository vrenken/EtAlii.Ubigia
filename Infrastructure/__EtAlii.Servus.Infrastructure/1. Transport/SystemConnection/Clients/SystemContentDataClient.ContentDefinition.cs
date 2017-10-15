namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;

    internal partial class SystemContentDataClient
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            await Task.Run(() => { _infrastructure.ContentDefinition.Store(identifier, contentDefinition); });
            MarkAsStored(contentDefinition);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            //await _client.Post(address, contentDefinition);

            //MarkAsStored(contentDefinition);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            await Task.Run(() => { _infrastructure.ContentDefinition.Store(identifier, contentDefinitionPart); });
            MarkAsStored(contentDefinitionPart);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString());
            //await _client.Post(address, contentDefinitionPart);

            //MarkAsStored(contentDefinitionPart);
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            var result = _infrastructure.ContentDefinition.Get(identifier);
            return await Task.FromResult(result);

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
