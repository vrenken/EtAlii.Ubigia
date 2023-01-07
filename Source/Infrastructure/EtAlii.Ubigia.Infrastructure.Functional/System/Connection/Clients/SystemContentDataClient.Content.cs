// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    internal partial class SystemContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            await _functionalContext.Content
                .Store(identifier, content)
                .ConfigureAwait(false);

            // Should this call be replaced by get instead?
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/80
            Blob.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await _functionalContext.Content
                .Store(identifier, contentPart)
                .ConfigureAwait(false);
            BlobPart.SetStored(contentPart, true);
        }

        public async Task<Content> Retrieve(Identifier identifier)
        {
            var result = await _functionalContext.Content
                .Get(identifier)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var result = await _functionalContext.Content
                .Get(identifier, contentPartId)
                .ConfigureAwait(false);
            return result;
        }
    }
}
