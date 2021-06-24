// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public class LogicalIdentifierSet : ILogicalIdentifierSet
    {
        private readonly IIdentifierHeadGetter _identifierHeadGetter;
        private readonly IIdentifierTailGetter _identifierTailGetter;

        public LogicalIdentifierSet(
            IIdentifierHeadGetter identifierHeadGetter, 
            IIdentifierTailGetter identifierTailGetter)
        {
            _identifierHeadGetter = identifierHeadGetter;
            _identifierTailGetter = identifierTailGetter;
        }

        public Task<Identifier> GetTail(Guid spaceId)
        {
            return _identifierTailGetter.Get(spaceId);
        }

        public Task<Identifier> GetCurrentHead(Guid spaceId)
        {
            return _identifierHeadGetter.GetCurrent(spaceId);
        }

        public Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid spaceId)
        {
            return _identifierHeadGetter.GetNext(spaceId);
        }
    }
}