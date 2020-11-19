﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Threading.Tasks;

    public interface IIdentifierRepository
    {
        Task<Identifier> GetTail(Guid spaceId);
        Task<Identifier> GetCurrentHead(Guid spaceId);
        Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid spaceId);
    }
}