namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IIdentifierSet
    {
        Identifier GetNextIdentifierFromStorage(Guid storageId, Guid accountId, Guid spaceId);
        Identifier GetNextIdentifierForPreviousHeadIdentifier(Guid storageId, Guid accountId, Guid spaceId, Identifier previousHeadIdentifier);
    }
}