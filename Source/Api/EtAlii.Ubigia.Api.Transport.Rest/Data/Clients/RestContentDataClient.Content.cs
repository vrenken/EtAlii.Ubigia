// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    internal partial class RestContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Content, UriParameter.EntryId, identifier.ToString());
            await Connection.Client.Post(address, content).ConfigureAwait(false);

            // TODO: Should this call be replaced by get instead?
            Blob.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            // Remark. We cannot have two post methods at the same time. The hosting
            // framework gets confused and does not out of the box know what method to choose.
            // Even if both have different parameters.
            // It might not be the best fit to alter this in PUT, but as the Rest interface
            // is the least important one this will do for now.
            // We've got bigger fish to fry.
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPart.Id.ToString());
            await Connection.Client.Put(address, contentPart).ConfigureAwait(false);

            BlobPart.SetStored(contentPart, true);
        }

        public async Task<Content> Retrieve(Identifier identifier)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Content, UriParameter.EntryId, identifier.ToString());
            var content = await Connection.Client.Get<Content>(address).ConfigureAwait(false);
            return content;
        }

        public async Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Content, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentPartId, contentPartId.ToString());
            var contentPart = await Connection.Client.Get<ContentPart>(address).ConfigureAwait(false);
            return contentPart;
        }
    }
}
