﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    internal partial class WebApiContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString());
            await Connection.Client.Post(address, content);

            // TODO: Should this call be replaced by get instead? 
            BlobHelper.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            // Remark. We cannot have two post methods at the same time. The hosting 
            // framework gets confused and does not out of the box know what method to choose.
            // Even if both have different parameters.
            // It might not be the best fit to alter this in PUT, but as the WebApi interface
            // is the least important one this will do for now.
            // We've got bigger fish to fry.
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString());
            await Connection.Client.Put(address, contentPart);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString());
            var content = await Connection.Client.Get<Content>(address);
            return content;
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString());
            var contentPart = await Connection.Client.Get<ContentPart>(address);
            return contentPart;
        }
    }
}