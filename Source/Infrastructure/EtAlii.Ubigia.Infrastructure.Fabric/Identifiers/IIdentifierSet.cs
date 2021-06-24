// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IIdentifierSet
    {
        Identifier GetNextIdentifierFromStorage(Guid storageId, Guid accountId, Guid spaceId);
        Identifier GetNextIdentifierForPreviousHeadIdentifier(Guid storageId, Guid accountId, Guid spaceId, in Identifier previousHeadIdentifier);
    }
}