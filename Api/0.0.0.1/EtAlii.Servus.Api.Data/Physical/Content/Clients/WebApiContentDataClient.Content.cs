namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public partial class WebApiContentDataClient : WebApiDataClientBase<IDataConnection>, IContentDataClient
    {
        public void Store(Identifier identifier, EtAlii.Servus.Api.Content content)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString());
            Client.Post<EtAlii.Servus.Api.Content>(address, content);

            BlobHelper.SetStored(content, true);
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString());
            Client.Post<ContentPart>(address, contentPart);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public IReadOnlyContent Retrieve(Identifier identifier)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString());
            var content = Client.Get<EtAlii.Servus.Api.Content>(address);
            return content as IReadOnlyContent;
        }

        public IReadOnlyContentPart Retrieve(Identifier identifier, ulong contentPartId)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString());
            var contentPart = Client.Get<ContentPart>(address);
            return contentPart as IReadOnlyContentPart;
        }
    }
}
