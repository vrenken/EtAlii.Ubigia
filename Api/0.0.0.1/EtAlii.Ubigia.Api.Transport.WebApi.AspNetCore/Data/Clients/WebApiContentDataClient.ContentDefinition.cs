namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    internal partial class WebApiContentDataClient
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            await Connection.Client.Post(address, contentDefinition);

            MarkAsStored(contentDefinition);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString());
            await Connection.Client.Post(address, contentDefinitionPart);

            MarkAsStored(contentDefinitionPart);
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            var contentDefinition = await Connection.Client.Get<ContentDefinition>(address);
            return contentDefinition;
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
