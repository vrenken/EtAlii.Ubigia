// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence;

internal class RootUpdater : IRootUpdater
{
    private readonly IStorage _storage;
    private readonly IRootGetter _rootGetter;

    public RootUpdater(IStorage storage, IRootGetter rootGetter)
    {
        _storage = storage;
        _rootGetter = rootGetter;
    }

    /// <inheritdoc />
    public async Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot)
    {
        var rootToUpdate = await _rootGetter.Get(spaceId, rootId).ConfigureAwait(false);

        if (rootToUpdate.Name != updatedRoot.Name || rootToUpdate.Identifier != updatedRoot.Identifier)
        {
            rootToUpdate.Name = updatedRoot.Name;
            rootToUpdate.Identifier = updatedRoot.Identifier == Identifier.Empty
                ? rootToUpdate.Identifier
                : updatedRoot.Identifier;

            // RT2022: We cannot change the root type yet.
            // Currently there is no way we can change the root type. Let's consider this in the future.
            updatedRoot.Type = rootToUpdate.Type;

            var containerId = _storage.ContainerProvider.ForRoots(spaceId);
            _storage.Items.Store(rootToUpdate, rootId, containerId);
        }

        return rootToUpdate;
    }
}
