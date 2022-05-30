// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentStorer : IContentStorer
    {
        private readonly IStorage _storage;

        public ContentStorer(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public Task Store(in Identifier identifier, Content content)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentFabricException("No identifier was specified");
            }
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, content);

            return Task.CompletedTask;
        }
    }
}
