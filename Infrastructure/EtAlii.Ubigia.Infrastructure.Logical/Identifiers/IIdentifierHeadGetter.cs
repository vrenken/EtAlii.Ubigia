namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IIdentifierHeadGetter
    {
        Identifier GetCurrent(Guid spaceId);
        Identifier GetNext(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}