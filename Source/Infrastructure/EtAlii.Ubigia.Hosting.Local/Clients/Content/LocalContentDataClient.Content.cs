namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using EtAlii.Ubigia.Api.Transport;

    public partial class LocalContentDataClient : LocalDataClientBase<IDataConnection>, IContentDataClient
    {
        public void Store(in Identifier identifier, EtAlii.Ubigia.Content content)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString())
            //Infrastructure.Post<EtAlii.Ubigia.Content>(address, content)

            //Blob.SetStored(content, true)
        }

        public void Store(in Identifier identifier, ContentPart contentPart)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString())
            //Infrastructure.Post<ContentPart>(address, contentPart)

            //BlobPart.SetStored(contentPart, true)
        }

        public IReadOnlyContent Retrieve(in Identifier identifier)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString())
            //var content = Infrastructure.Get<EtAlii.Ubigia.Content>(address)
            //return content as IReadOnlyContent
        }

        public IReadOnlyContentPart Retrieve(in Identifier identifier, ulong contentPartId)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString())
            //var contentPart = Infrastructure.Get<ContentPart>(address)
            //return contentPart as IReadOnlyContentPart
        }
    }
}
