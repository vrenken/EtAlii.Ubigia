// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;

    public class IdentifierSet : IIdentifierSet
    {
        private readonly IStorage _storage;

        public IdentifierSet(IStorage storage)
        {
            _storage = storage;
        }

        public Identifier GetNextIdentifierFromStorage(Guid storageId, Guid accountId, Guid spaceId)
        {
            // Determine Head From Component Storage
            var containerId = _storage.ContainerProvider.ForEntry(storageId, accountId, spaceId);
            var newContainerId = _storage.Components.GetNextContainer(containerId);
            return _storage.ContainerProvider.ToIdentifier(storageId, accountId, spaceId, newContainerId);
        }

        public Identifier GetNextIdentifierForPreviousHeadIdentifier(Guid storageId, Guid accountId, Guid spaceId, in Identifier previousHeadIdentifier)
        {
            // Calculate identifier.
            var containerId = _storage.ContainerProvider.FromIdentifier(previousHeadIdentifier, true);
            containerId = _storage.Components.GetNextContainer(containerId);
            var nextIdentifier = _storage.ContainerProvider.ToIdentifier(storageId, accountId, spaceId, containerId);
            return nextIdentifier;
        }
    }
}