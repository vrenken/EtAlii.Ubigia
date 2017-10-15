namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface IIdentifierHeadGetter
    {
        Identifier GetCurrent(Guid spaceId);
        Identifier GetNext(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}