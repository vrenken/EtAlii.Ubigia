namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface ILogicalIdentifierSet
    {
        Identifier GetTail(Guid spaceId);
        Identifier GetCurrentHead(Guid spaceId);
        Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}