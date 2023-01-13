// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence;

public class IdentifierSet : IIdentifierSet
{
    private readonly IStorage _storage;

    public IdentifierSet(IStorage storage)
    {
        _storage = storage;
    }

    /// <inheritdoc />
    public Task<Identifier> GetNextIdentifierFromStorage(Guid storageId, Guid accountId, Guid spaceId)
    {
        // Determine Head From Component Storage
        var containerId = _storage.ContainerProvider.ForEntry(storageId, accountId, spaceId);
        var newContainerId = _storage.Components.GetNextContainer(containerId);
        var identifier = _storage.ContainerProvider.ToIdentifier(storageId, accountId, spaceId, newContainerId);
        return Task.FromResult(identifier);
    }

    /// <inheritdoc />
    public Task<Identifier> GetNextIdentifierForPreviousHeadIdentifier(Guid storageId, Guid accountId, Guid spaceId, in Identifier previousHeadIdentifier)
    {
        // Calculate identifier.
        var containerId = _storage.ContainerProvider.FromIdentifier(previousHeadIdentifier, true);
        containerId = _storage.Components.GetNextContainer(containerId);
        var nextIdentifier = _storage.ContainerProvider.ToIdentifier(storageId, accountId, spaceId, containerId);
        return Task.FromResult(nextIdentifier);
    }
}
