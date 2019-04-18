namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public partial class LocalContentDataClient : LocalDataClientBase<IDataConnection>, IContentDataClient
    {
        public void Store(Identifier identifier, EtAlii.Ubigia.Api.Content content)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString())
            //Infrastructure.Post<EtAlii.Ubigia.Api.Content>(address, content)

            //BlobHelper.SetStored(content, true)
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString())
            //Infrastructure.Post<ContentPart>(address, contentPart)

            //BlobPartHelper.SetStored(contentPart, true)
        }

        public IReadOnlyContent Retrieve(Identifier identifier)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString())
            //var content = Infrastructure.Get<EtAlii.Ubigia.Api.Content>(address)
            //return content as IReadOnlyContent
        }

        public IReadOnlyContentPart Retrieve(Identifier identifier, ulong contentPartId)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString())
            //var contentPart = Infrastructure.Get<ContentPart>(address)
            //return contentPart as IReadOnlyContentPart
        }
    }
}
