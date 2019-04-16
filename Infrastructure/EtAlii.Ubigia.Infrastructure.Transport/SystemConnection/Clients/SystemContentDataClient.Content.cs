namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    internal partial class SystemContentDataClient
    {
        public Task Store(Identifier identifier, Content content)
        {
            _infrastructure.Content.Store(identifier, content);
            BlobHelper.SetStored(content, true);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString());
            //await _client.Post(address, content);

            //// TODO: Should this call be replaced by get instead? 
            //BlobHelper.SetStored(content, true);
            return Task.CompletedTask;
        }

        public Task Store(Identifier identifier, ContentPart contentPart)
        {
            _infrastructure.Content.Store(identifier, contentPart);
            BlobPartHelper.SetStored(contentPart, true);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString());
            //await _client.Post(address, contentPart);

            //BlobPartHelper.SetStored(contentPart, true);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            var result = _infrastructure.Content.Get(identifier);
            return Task.FromResult(result);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString());
            //var content = await _client.Get<Content>(address);
            //return content;
        }

        public Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var result = _infrastructure.Content.Get(identifier, contentPartId);
            return Task.FromResult(result);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString());
            //var contentPart = await _client.Get<ContentPart>(address);
            //return contentPart;
        }
    }
}
