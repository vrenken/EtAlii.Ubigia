namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IIdentifierRepository
    {
        Identifier GetTail(Guid spaceId);
        Identifier GetCurrentHead(Guid spaceId);
        Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}