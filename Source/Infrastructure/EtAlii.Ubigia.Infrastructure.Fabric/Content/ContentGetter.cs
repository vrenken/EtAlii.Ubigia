// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentGetter : IContentGetter
    {
        private readonly IStorage _storage;

        public ContentGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<Content> Get(Identifier identifier)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var content = await _storage.Blobs.Retrieve<Content>(containerId).ConfigureAwait(false);
            return content;
        }
    }
}