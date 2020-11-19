﻿namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface ILogicalIdentifierSet
    {
        Task<Identifier> GetTail(Guid spaceId);
        Task<Identifier> GetCurrentHead(Guid spaceId);
        Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid spaceId);
    }
}