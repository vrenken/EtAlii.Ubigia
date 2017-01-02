namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using EtAlii.Servus.Api;

    public interface IIdentifierRepository
    {
        Identifier GetTail(Guid spaceId);
        Identifier GetCurrentHead(Guid spaceId);
        Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}