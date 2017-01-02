namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    internal partial class WebApiContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString());
            await Connection.Client.Post(address, content);

            // TODO: Should this call be replaced by get instead? 
            BlobHelper.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString());
            await Connection.Client.Post(address, contentPart);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString());
            var content = await Connection.Client.Get<Content>(address);
            return content;
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString());
            var contentPart = await Connection.Client.Get<ContentPart>(address);
            return contentPart;
        }
    }
}
