namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;

    internal partial class SystemContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            await Task.Run(() => { _infrastructure.Content.Store(identifier, content); });
            BlobHelper.SetStored(content, true);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString());
            //await _client.Post(address, content);

            //// TODO: Should this call be replaced by get instead? 
            //BlobHelper.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await Task.Run(() => { _infrastructure.Content.Store(identifier, contentPart); });
            BlobPartHelper.SetStored(contentPart, true);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString());
            //await _client.Post(address, contentPart);

            //BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            var result = _infrastructure.Content.Get(identifier);
            return await Task.FromResult(result);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString());
            //var content = await _client.Get<Content>(address);
            //return content;
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var result = _infrastructure.Content.Get(identifier, contentPartId);
            return await Task.FromResult(result);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString());
            //var contentPart = await _client.Get<ContentPart>(address);
            //return contentPart;
        }
    }
}
