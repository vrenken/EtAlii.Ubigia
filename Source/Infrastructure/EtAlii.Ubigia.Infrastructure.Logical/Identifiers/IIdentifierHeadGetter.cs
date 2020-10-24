namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface IIdentifierHeadGetter
    {
        Identifier GetCurrent(Guid spaceId);
        Identifier GetNext(Guid spaceId, out Identifier previousHeadIdentifier);
    }
}