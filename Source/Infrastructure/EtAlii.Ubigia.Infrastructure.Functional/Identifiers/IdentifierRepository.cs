// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class IdentifierRepository : IIdentifierRepository
    {
        private readonly ILogicalContext _logicalContext;

        public IdentifierRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public Task<Identifier> GetCurrentHead(Guid spaceId)
        {
            return _logicalContext.Identifiers.GetCurrentHead(spaceId);
        }

        public Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid spaceId)
        {
            return _logicalContext.Identifiers.GetNextHead(spaceId);
        }

        public Task<Identifier> GetTail(Guid spaceId)
        {
            return _logicalContext.Identifiers.GetTail(spaceId);
        }
    }
}