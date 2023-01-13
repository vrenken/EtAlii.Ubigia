// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;

public interface IContainerProvider
{
    ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId);
    ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId, ulong era, ulong period, ulong moment);
    ContainerIdentifier ForEntry(string storageId, string accountId, string spaceId, ulong era, ulong period, ulong moment);
    ContainerIdentifier ForEntry(string storageId, string accountId, string spaceId, string era, string period, string moment);
    ContainerIdentifier ForRoots(Guid spaceId);
    ContainerIdentifier ForItems(string folder);
    ContainerIdentifier FromIdentifier(in Identifier id, bool trimTime = false);
    Identifier ToIdentifier(Guid storageId, Guid accountId, Guid spaceId, ContainerIdentifier containerId);
}
