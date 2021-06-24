// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;

    internal partial class SystemContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            await _infrastructure.Content
                .Store(identifier, content)
                .ConfigureAwait(false);
            Blob.SetStored(content, true);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString())
            //await _client.Post(address, content)

            //// TODO: Should this call be replaced by get instead? 
            //BlobHelper.SetStored(content, true)
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await _infrastructure.Content
                .Store(identifier, contentPart)
                .ConfigureAwait(false);
            BlobPart.SetStored(contentPart, true);

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString())
            //await _client.Post(address, contentPart)

            //BlobPartHelper.SetStored(contentPart, true)
        }

        public async Task<Content> Retrieve(Identifier identifier)
        {
            var result = await _infrastructure.Content
                .Get(identifier)
                .ConfigureAwait(false);
            return result;

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString())
            //var content = await _client.Get<Content>(address)
            //return content
        }

        public async Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var result = await _infrastructure.Content
                .Get(identifier, contentPartId)
                .ConfigureAwait(false);
            return result;

            //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString())
            //var contentPart = await _client.Get<ContentPart>(address)
            //return contentPart
        }
    }
}
