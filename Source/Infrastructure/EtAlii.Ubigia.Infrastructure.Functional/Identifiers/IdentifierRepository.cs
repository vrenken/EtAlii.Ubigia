﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class IdentifierRepository : IIdentifierRepository
    {
        private readonly ILogicalContext _logicalContext;

        public IdentifierRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public Identifier GetCurrentHead(Guid spaceId)
        {
            return _logicalContext.Identifiers.GetCurrentHead(spaceId);
        }

        public Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier)
        {
            return _logicalContext.Identifiers.GetNextHead(spaceId, out previousHeadIdentifier);
        }

        public Identifier GetTail(Guid spaceId)
        {
            return _logicalContext.Identifiers.GetTail(spaceId);
        }
    }
}