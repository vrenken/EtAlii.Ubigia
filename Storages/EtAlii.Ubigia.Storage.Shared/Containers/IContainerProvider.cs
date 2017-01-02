namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IContainerProvider
    {
        ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId);
        ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId, ulong era, ulong period, ulong moment);
        ContainerIdentifier ForEntry(string storageId, string accountId, string spaceId, ulong era, ulong period, ulong moment);
        ContainerIdentifier ForEntry(string storageId, string accountId, string spaceId, string era, string period, string moment);
        ContainerIdentifier ForRoots(Guid spaceId);
        ContainerIdentifier ForItems(string folder);
        ContainerIdentifier FromIdentifier(Identifier id, bool trimTime = false);
        Identifier ToIdentifier(Guid storageId, Guid accountId, Guid spaceId, ContainerIdentifier containerId);
    }
}
