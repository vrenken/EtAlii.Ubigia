// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface ILogicalIdentifierSet
    {
        Task<Identifier> GetTail(Guid storageId, Guid spaceId);
        Task<Identifier> GetCurrentHead(Guid storageId, Guid spaceId);
        Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid storageId, Guid spaceId);
    }
}
