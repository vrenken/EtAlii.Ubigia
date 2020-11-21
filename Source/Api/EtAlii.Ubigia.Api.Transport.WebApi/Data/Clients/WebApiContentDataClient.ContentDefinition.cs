﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    internal partial class WebApiContentDataClient
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            await Connection.Client.Post(address, contentDefinition).ConfigureAwait(false);

            MarkAsStored(contentDefinition);
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            // Remark. We cannot have two post methods at the same time. The hosting 
            // framework gets confused and does not out of the box know what method to choose.
            // Even if both have different parameters.
            // It might not be the best fit to alter this in PUT, but as the WebApi interface
            // is the least important one this will do for now.
            // We've got bigger fish to fry.
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString());
            await Connection.Client.Put(address, contentDefinitionPart).ConfigureAwait(false);

            MarkAsStored(contentDefinitionPart);
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.ContentDefinition, UriParameter.EntryId, identifier.ToString());
            var contentDefinition = await Connection.Client.Get<ContentDefinition>(address).ConfigureAwait(false);
            return contentDefinition;
        }

        private void MarkAsStored(ContentDefinition contentDefinition)
        {
            BlobHelper.SetStored(contentDefinition, true);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                MarkAsStored(contentDefinitionPart);
            }
        }

        private void MarkAsStored(ContentDefinitionPart contentDefinitionPart)
        {
            BlobPartHelper.SetStored(contentDefinitionPart, true);
        }
    }
}
