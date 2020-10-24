namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    public interface IIdentifierRepository
    {
        Identifier GetTail(Guid spaceId);
        Identifier GetCurrentHead(Guid spaceId);
        Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}