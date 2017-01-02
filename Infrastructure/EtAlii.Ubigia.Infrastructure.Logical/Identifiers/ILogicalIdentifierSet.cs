namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface ILogicalIdentifierSet
    {
        Identifier GetTail(Guid spaceId);
        Identifier GetCurrentHead(Guid spaceId);
        Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}