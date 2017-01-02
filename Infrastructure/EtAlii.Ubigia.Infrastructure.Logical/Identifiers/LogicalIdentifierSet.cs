namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

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

        public Identifier GetTail(Guid spaceId)
        {
            return _identifierTailGetter.Get(spaceId);
        }

        public Identifier GetCurrentHead(Guid spaceId)
        {
            return _identifierHeadGetter.GetCurrent(spaceId);
        }

        public Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier)
        {
            return _identifierHeadGetter.GetNext(spaceId, out previousHeadIdentifier);
        }
    }
}