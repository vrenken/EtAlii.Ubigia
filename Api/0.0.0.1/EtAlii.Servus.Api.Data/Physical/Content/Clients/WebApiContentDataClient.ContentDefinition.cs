namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public partial class WebApiContentDataClient : WebApiDataClientBase<IDataConnection>, IContentDataClient
    {
        public void StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            Client.Post<ContentDefinition>(address, contentDefinition);

            MarkAsStored(contentDefinition);
        }

        public void StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString());
            Client.Post<ContentDefinitionPart>(address, contentDefinitionPart);

            MarkAsStored(contentDefinitionPart);
        }

        public IReadOnlyContentDefinition RetrieveDefinition(Identifier identifier)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            var contentDefinition = Client.Get<ContentDefinition>(address);
            return contentDefinition as IReadOnlyContentDefinition;
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
