namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;

    public partial class LocalContentDataClient : LocalDataClientBase<IDataConnection>, IContentDataClient
    {
        public void StoreDefinition(in Identifier identifier, ContentDefinition contentDefinition)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString())
            //Infrastructure.Post<ContentDefinition>(address, contentDefinition)

            //MarkAsStored(contentDefinition)
        }

        public void StoreDefinition(in Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString())
            //Infrastructure.Post<ContentDefinitionPart>(address, contentDefinitionPart)

            //MarkAsStored(contentDefinitionPart)
        }

        public ContentDefinition RetrieveDefinition(in Identifier identifier)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString())
            //var contentDefinition = Infrastructure.Get<ContentDefinition>(address)
            //return contentDefinition as ContentDefinition
        }

        private void MarkAsStored(ContentDefinition contentDefinition)
        {
            throw new System.NotImplementedException();

            //Blob.SetStored(contentDefinition, true)

            //foreach (var contentDefinitionPart in contentDefinition.Parts)
            //[
            //    MarkAsStored(contentDefinitionPart)
            //]
        }

        private void MarkAsStored(ContentDefinitionPart contentDefinitionPart)
        {
            throw new System.NotImplementedException();

            //BlobPart.SetStored(contentDefinitionPart, true)
        }
    }
}
